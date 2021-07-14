namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System.Linq;
    using BABYLON;
    using BabylonJS.Blazor.Game.Tutorial.Client.BabylonJSExtensions;

    public class Lantern
    {
        private readonly Scene _scene;
        private readonly PBRMetallicRoughnessMaterial _lightMaterial;
        private readonly Mesh _lightSphere;
        private  PointLight _light;
        private readonly AnimationGroup _spinAnimation;
        private ParticleSystem _stars;

        public Mesh Mesh { get; }
        public bool IsLit { get; internal set; }

        public Lantern(
            PBRMetallicRoughnessMaterial lightMaterial,
            Mesh mesh,
            Scene scene,
            Vector3 position,
            AnimationGroup animationGroup = null
        )
        {
            _scene = scene;
            _lightMaterial = lightMaterial;
            Mesh = mesh;

            // Set Animations
            _spinAnimation = animationGroup;

            var lightSphere = Mesh.CreateSphere(
                "illum",
                4,
                20,
                _scene
            );
            lightSphere.scaling.y = 2;
            lightSphere.setAbsolutePosition(
                position
            );
            lightSphere.parent = mesh;
            lightSphere.isVisible = false;
            lightSphere.isPickable = false;
            _lightSphere = lightSphere;

            LoadLantern(
                position
            );

            LoadStars();
        }

        public void SetEmissiveTexture()
        {
            IsLit = true;

            // Create light source for the lanterns
            _light = new PointLight(
                "lantern light",
                Mesh.getAbsolutePosition(),
                _scene
            )
            {
                intensity = 0,
                radius = 2,
                diffuse = new Color3(
                    0.45m,
                    0.56m,
                    0.80m
                )
            };
            FindNearestMesh(
                _light
            );

            _spinAnimation.play();
            _stars.start();

            // Swap Texture
            Mesh.material = _lightMaterial;
            _light.intensity = 30;
        }

        private void FindNearestMesh(
            PointLight light
        )
        {
            var includedOnlyMeshes = light.includedOnlyMeshes.ToList();
            var childMeshes = _scene.getMeshByName(
                "__root__"
            ).getChildMeshes();
            foreach (var childMesh in childMeshes)
            {
                if (_lightSphere.intersectsMesh(childMesh))
                {
                    includedOnlyMeshes.Add(childMesh);
                }
            }

            _lightSphere.dispose();
        }

        private void LoadLantern(
            Vector3 position
        )
        {
            Mesh.scaling = new Vector3(
                0.8m,
                0.8m,
                0.8m
            );
            Mesh.setAbsolutePosition(
                position
            );
            Mesh.isPickable = false;
        }

        private void LoadStars()
        {
            var particleSystem = new ParticleSystem(
                "stars",
                1000,
                _scene
            );

            particleSystem.particleTexture = new Texture(
                _scene,
                "textures/solidStar.png"
            );
            particleSystem.emitterPosition(
                new Vector3(
                    Mesh.position.x,
                    Mesh.position.y + 1.5m,
                    Mesh.position.z
                )
            );
            particleSystem.createPointEmitter(new Vector3(0.6m, 1, 0), new Vector3(0, 1, 0));
            particleSystem.color1 = new Color4(1, 1, 1);
            particleSystem.color2 = new Color4(1, 1, 1);
            particleSystem.colorDead = new Color4(1, 1, 1, 1);
            particleSystem.emitRate = 12;
            particleSystem.minEmitPower = 14;
            particleSystem.maxEmitPower = 14;
            particleSystem.addStartSizeGradient(0, 2);
            particleSystem.addStartSizeGradient(1, 0.8m);
            particleSystem.minAngularSpeed = 0;
            particleSystem.maxAngularSpeed = 2;
            particleSystem.addDragGradient(0, 0.7m, 0.7m);
            particleSystem.targetStopDuration = .25m;

            _stars = particleSystem;
        }
    }
}
