namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BABYLON;

    public class GameEnvironment
    {
        private readonly Scene _scene;
        private readonly List<Lantern> _lanternObjs;
        private readonly PBRMetallicRoughnessMaterial _lightMaterial;
        private readonly List<Firework> _fireworksObjects;

        public bool StartFireworks { get; set; }

        public GameEnvironment(
            Scene scene
        )
        {
            _scene = scene;

            _lanternObjs = new List<Lantern>();
            // Create emissive material for when a Lantern is lit
            var lightMaterial = new PBRMetallicRoughnessMaterial(
                "lantern-mesh-light",
                _scene
            );
            lightMaterial.emissiveTexture = new Texture(
                _scene,
                "/textures/litLantern.png",
                true,
                false
            );
            lightMaterial.emissiveColor = new Color3(
                0.8784313725490196m,
                0.7568627450980392m,
                0.6235294117647059m
            );
            _lightMaterial = lightMaterial;

            _fireworksObjects = new List<Firework>();
        }

        public async Task Load()
        {
            var assets = await LoadAsset();

            // All meshes should have a certain set of values;
            foreach (var mesh in assets.AllMeshes)
            {
                mesh.receiveShadows = true;
                mesh.checkCollisions = true;

                if (mesh.name == "ground")
                {
                    // dont check for collisions, 
                    mesh.checkCollisions = false;
                    // dont allow for raycasting to detect the ground
                    mesh.isPickable = false;
                }

                // Areas that will use box collisions
                if (mesh.name.Contains("stairs")
                    || mesh.name == "cityentranceground"
                    || mesh.name == "fishingground.001"
                    || mesh.name.Contains("lilyflwr"))
                {
                    mesh.checkCollisions = false;
                    mesh.isPickable = false;
                }

                // Collision Meshes
                if (mesh.name.Contains("collision"))
                {
                    //mesh.isVisible = false;
                    mesh.visibility = 0.0m;
                    mesh.isPickable = true;
                }

                // Trigger meshes
                if (mesh.name.Contains("Trigger"))
                {
                    mesh.isVisible = false;
                    mesh.isPickable = false;
                    mesh.checkCollisions = false;
                }
            }

            // --LANTERNS--
            // Original mesh is not visible
            assets.Lantern.isVisible = false;
            // A transform node to hold all lanterns
            var lanternHolder = new TransformNode(
                "lanternHolder",
                _scene,
                true
            );
            for (int i = 0; i < 22; i++)
            {
                // Mesh Cloning
                var lanternInstance = assets.Lantern.clone(
                    $"lantern {i}",
                    lanternHolder
                );
                lanternInstance.isVisible = true;
                lanternInstance.setParent(lanternHolder);

                // Animation Cloning
                var animationGroupClone = new AnimationGroup(
                    "lanternAnimationGroup " + i
                );
                animationGroupClone.addTargetedAnimation(
                    assets.AnimationGroup.targetedAnimations[0].animation,
                    lanternInstance
                );

                // Create new Lantern
                var newLantern = new Lantern(
                    _lightMaterial,
                    lanternInstance,
                    _scene,
                    assets.Env.getChildTransformNodes(false)
                        .FirstOrDefault(a => a.name == $"lantern {i}")
                        .getAbsolutePosition(),
                    animationGroupClone
                );
                _lanternObjs.Add(
                    newLantern
                );
            }

            assets.Lantern.dispose();
            assets.AnimationGroup.dispose();

            // -- FIREWORKS --
            for (int i = 0; i < 5; i++)
            {
                _fireworksObjects.Add(
                    new Firework(
                        _scene,
                        i
                    )
                );
            }
            // Before the scene render
            // Check to see if the fireworks have started
            // If they have, trigger the fireworks sequence
            _scene.onBeforeRenderObservable.add((_, __) =>
            {
                foreach (var firework in _fireworksObjects)
                {
                    if (StartFireworks)
                    {
                        firework.Start();
                    }
                }
                return Task.CompletedTask;
            });
        }

        public void Initialize()
        {
            foreach (var firework in _fireworksObjects)
            {
                firework.Initialize();
            }
        }

        private async Task<EnvSettingAsset> LoadAsset()
        {
            var result = await SceneLoader.ImportMeshAsync(
                null,
                "./models/",
                "envSetting.glb",
                _scene
            );

            // Load Lanterns Mesh
            var lanternResult = await SceneLoader.ImportMeshAsync(
                "",
                "./models/",
                "lantern.glb",
                _scene
            );
            // Extract the actual lanterns mesh from teh root of the mesh that is imported
            var lantern = lanternResult.meshes[0].getChildren()[0];
            lantern.parent = null;
            lanternResult.meshes[0].dispose();

            // Animation Groups
            var importedAnimations = lanternResult.animationGroups;
            var animation = new List<Animation>
            {
                importedAnimations[0].targetedAnimations[0].animation
            };
            importedAnimations[0].dispose();
            // Create new Animation Group
            var animationGroup = new AnimationGroup(
                "lanternAnimationGroup"
            );
            animationGroup.addTargetedAnimation(
                animation[0],
                result.meshes[1]
            );

            return new(
                result.meshes[0],
                result.meshes[0].getChildMeshes(),
                // Here we assign the lantern to a Mesh,
                // This sets a new Mesh to delegate all of its functionaity to the instances of the lantern from above.
                new Mesh(lantern),
                animationGroup
            );
        }

        private struct EnvSettingAsset
        {
            public AbstractMesh Env { get; }
            public AbstractMesh[] AllMeshes { get; }
            public Mesh Lantern { get; }
            public AnimationGroup AnimationGroup { get; }

            public EnvSettingAsset(
                AbstractMesh env,
                AbstractMesh[] allMeshes,
                Mesh lantern,
                AnimationGroup animationGroup
            )
            {
                Env = env;
                AllMeshes = allMeshes;
                Lantern = lantern;
                AnimationGroup = animationGroup;
            }
        }

        public void CheckLanterns(
            Player player
        )
        {
            if (!_lanternObjs[0].IsLit)
            {
                _lanternObjs[0].SetEmissiveTexture();
            }

            foreach (var lantern in _lanternObjs)
            {
                player.Mesh.actionManager.registerAction(
                    new ExecuteCodeAction(
                        new
                        {
                            trigger = ActionManager.OnIntersectionEnterTrigger,
                            parameter = lantern.Mesh
                        },
                        new EventHorizon.Blazor.Interop.Callbacks.ActionCallback<ActionEvent>(
                            _ =>
                            {
                                if (!lantern.IsLit
                                    && player.SparkLit
                                )
                                {
                                    // Increment the lantern count
                                    player.LanternsLit += 1;
                                    // Light up the lantern
                                    lantern.SetEmissiveTexture();

                                    // reset the Sparkler 
                                    player.SparkReset = true;
                                    player.SparkLit = true;
                                }
                                // If the lantern is lit already, reset the Sparkler
                                else if (lantern.IsLit)
                                {
                                    player.SparkReset = true;
                                    player.SparkLit = true;
                                }

                                return Task.CompletedTask;
                            }
                        )
                    )
                );
            }
        }
    }
}
