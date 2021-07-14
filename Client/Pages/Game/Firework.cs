namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Timers;
    using BABYLON;

    public class Firework
    {
        private readonly Scene _scene;

        // Variable sued by Environment
        private Mesh _emitter;
        private ParticleSystem _rocket;
        private bool _exploded;
        private decimal _height;
        private decimal _delay;
        private bool _started;
        private List<ParticleSystem> _explisionParticles;
        private decimal _startingHeight;
        private decimal _startingDelay;

        public Firework(
            Scene scene,
            int index
        )
        {
            _scene = scene;

            // Emitter for rocket of Firework
            var sphere = Mesh.CreateSphere(
                "rocket",
                4,
                1,
                _scene
            );
            sphere.isVisible = false;

            // The origin spawn point for the fireworks is determined by the TransformNode called "fireworks"
            // This TransformNode was placed in Blender
            var randomPosition = (decimal)Random.Shared.NextDouble() * 10;
            var fireworksPosition = scene.getTransformNodeByName("fireworks").getAbsolutePosition();
            sphere.position = new Vector3(
                fireworksPosition.x + randomPosition * -1,
                fireworksPosition.y,
                fireworksPosition.z
            );
            _emitter = sphere;

            // Rocket Particle System
            var rocket = new ParticleSystem(
                "rocket",
                350,
                _scene
            )
            {
                particleTexture = new Texture(
                    _scene,
                    "/textures/flare.png"
                ),
                emitter = sphere,
                emitRate = 20,
                minEmitBox = new Vector3(
                    0,
                    0,
                    0
                ),
                maxEmitBox = new Vector3(
                    0,
                    0,
                    0
                ),
                color1 = new Color4(
                    0.49m,
                    0.57m,
                    0.76m
                ),
                color2 = new Color4(
                    0.29m,
                    0.29m,
                    0.66m
                ),
                colorDead = new Color4(
                    0,
                    0,
                    0.2m,
                    0.5m
                ),
                minSize = 1,
                maxSize = 1
            };
            rocket.addSizeGradient(
                0,
                1
            );
            rocket.addSizeGradient(
                1,
                0.01m
            );
            _rocket = rocket;

            // Set how high the rocket will travel before exploding
            // and how long it will take before shooting the rocket
            _height = sphere.position.y + (decimal)Random.Shared.NextDouble() * (15 + 4) + 4;
            // This delay is Frame based
            _delay = ((decimal)Random.Shared.NextDouble() * index + 1) * 60;
            _startingHeight = sphere.position.y;
            _startingDelay = _delay;
        }

        public void Initialize()
        {
            // We prebuild the explosions, so we do not have to when they are not going off.
            _explisionParticles = Explosions(
                new Vector3(
                    _emitter.position.x,
                    _height,
                    _emitter.position.z
                )
            );
        }

        private List<ParticleSystem> Explosions(
            Vector3 position
        )
        {
            //mesh that gets split into vertices
            var explosion = Mesh.CreateSphere(
                "explosion",
                1,
                1,
                _scene
            );
            explosion.isVisible = false;
            explosion.position = position;

            var emitter = explosion;
            emitter.useVertexColors = true;
            var vertPos = emitter.getVerticesData(VertexBuffer.PositionKind);
            var vertNorms = emitter.getVerticesData(VertexBuffer.NormalKind);
            var vertColors = new decimal[4 * vertPos.Length];

            var particles = new List<ParticleSystem>();
            //for each vertex, create a particle system
            for (var i = 0; i < vertPos.Length; i += 3)
            {
                var vertPosition = new Vector3(
                    vertPos[i], vertPos[i + 1], vertPos[i + 2]
                );
                var vertNormal = new Vector3(
                    vertNorms[i], vertNorms[i + 1], vertNorms[i + 2]
                );
                var r = (decimal)Random.Shared.NextDouble();
                var g = (decimal)Random.Shared.NextDouble();
                var b = (decimal)Random.Shared.NextDouble();
                var alpha = 1.0m;
                var color = new Color4(
                    r,
                    g,
                    b,
                    alpha
                );
                vertColors[i] = r;
                vertColors[i + 1] = g;
                vertColors[i + 2] = b;
                vertColors[i + 3] = alpha;

                //Emitter for the particle system

                var gizmo = Mesh.CreateBox(
                    "gizmo",
                    0.001m,
                    _scene
                );
                gizmo.position = vertPosition;
                gizmo.parent = emitter;
                var direction = vertNormal
                    .normalize()
                    // Move in the direction of the normal
                    .scale(1);

                // Actual particle system for each exploding piece
                var particleSys = new ParticleSystem(
                    "particles",
                    500,
                    _scene
                )
                {
                    particleTexture = new Texture(
                        _scene,
                        "textures/flare.png"
                    ),
                    emitter = gizmo,
                    minEmitBox = new Vector3(
                        1,
                        0,
                        0
                    ),
                    maxEmitBox = new Vector3(
                        1,
                        0,
                        0
                    ),
                    minSize = 0.1m,
                    maxSize = 0.1m,
                    color1 = color,
                    color2 = color,
                    colorDead = new Color4(
                        0,
                        0,
                        0,
                        0.0m
                    ),
                    minLifeTime = 1,
                    maxLifeTime = 2,
                    emitRate = 500,
                    gravity = new Vector3(
                        0,
                        -9.8m,
                        0
                    ),
                    direction1 = direction,
                    direction2 = direction,
                    minEmitPower = 10,
                    maxEmitPower = 13,
                    updateSpeed = 0.01m,
                    targetStopDuration = 0.2m,
                    disposeOnStop = true
                };
                particles.Add(particleSys);
            }

            return particles;
        }


        public void Start()
        {
            if (_started)
            {
                // If started, rocket flies up to height & then explodes
                if (_emitter.position.y >= _height
                    && !_exploded
                )
                {
                    // Transition to the explosion particle system
                    // Do not explode again
                    _exploded = !_exploded;
                    foreach (var particle in _explisionParticles)
                    {
                        particle.start();
                    }
                    // Reset the rocket, will run until scene ends
                    _rocket.stop();
                    _emitter.position.y = _startingHeight;
                    _delay = _startingDelay;
                    _started = false;
                    _exploded = false;
                }
                else
                {
                    // Move the rocket up
                    _emitter.position.y += 0.2m;
                }
            }
            else
            {
                // Delay shooting the firework
                if (_delay <= 0)
                {
                    _started = true;
                    //start particle system
                    _rocket.start();
                }
                else
                {
                    _delay--;
                }
            }
        }
    }
}
