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
        }

        public async Task Load()
        {
            var assets = await LoadAsset();

            // All meshes should have a certain set of values;
            foreach (var mesh in assets.AllMeshes)
            {
                mesh.receiveShadows = true;
                mesh.checkCollisions = true;
            }

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
                var lanternInstance = assets.Lantern.clone(
                    $"lantern{i}",
                    lanternHolder
                );
                lanternInstance.isVisible = true;
                lanternInstance.setParent(lanternHolder);

                // Create new Lantern
                var newLantern = new Lantern(
                    _lightMaterial,
                    lanternInstance,
                    _scene,
                    assets.Env.getChildTransformNodes(false)
                        .FirstOrDefault(a => a.name == $"lantern {i}")
                        .getAbsolutePosition()
                );
                _lanternObjs.Add(
                    newLantern
                );
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

            return new(
                result.meshes[0],
                result.meshes[0].getChildMeshes(),
                // Here we assign the lantern to a Mesh,
                // This sets a new Mesh to delegate all of its functionaity to the instances of the lantern from above.
                new Mesh(lantern)
            );
        }

        private struct EnvSettingAsset
        {
            public AbstractMesh Env { get; }
            public AbstractMesh[] AllMeshes { get; }
            public Mesh Lantern { get; }

            public EnvSettingAsset(
                AbstractMesh env,
                AbstractMesh[] allMeshes,
                Mesh lantern
            )
            {
                Env = env;
                AllMeshes = allMeshes;
                Lantern = lantern;
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
                                    player.SparkRest = true;
                                    player.SparkLit = true;
                                }
                                // If the lantern is lit already, reset the Sparkler
                                else if (lantern.IsLit)
                                {
                                    player.SparkRest = true;
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
