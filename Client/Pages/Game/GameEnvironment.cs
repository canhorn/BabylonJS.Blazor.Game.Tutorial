namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System.Threading.Tasks;
    using BABYLON;

    public class GameEnvironment
    {
        private readonly Scene _scene;

        public GameEnvironment(
            Scene scene
        )
        {
            _scene = scene;
        }

        public async Task Load()
        {
            var assets = await LoadAsset();


            foreach (var mesh in assets.AllMeshes)
            {
                mesh.receiveShadows = true;
                mesh.checkCollisions = true;
            }


            //var ground = Mesh.CreateBox(
            //    name: "ground",
            //    size: 24,
            //    _scene
            //);
            //ground.scaling = new Vector3(
            //    10,
            //    0.02m,
            //    2
            //);

            //// A Slop for testing Stairs
            //var stairs = Mesh.CreateBox(
            //    "stairs",
            //    12,
            //    _scene
            //);
            //stairs.scaling = new Vector3(
            //    1,
            //    0.02m,
            //    2
            //);
            //stairs.position = new Vector3(
            //    0,
            //    2,
            //    -10
            //);
            //stairs.rotate(Vector3.Left(), -145);
            //var bodyMaterial = new StandardMaterial(
            //    "green",
            //    _scene
            //)
            //{
            //    diffuseColor = new Color3(0.5m, 0.8m, 0.5m)
            //};
            //stairs.material = bodyMaterial;
            //stairs.checkCollisions = true;
        }

        private async Task<EnvSettingAsset> LoadAsset()
        {
            var result = await SceneLoader.ImportMeshAsync(
                null,
                "./models/",
                "envSetting.glb",
                _scene
            );

            return new(
                result.meshes[0],
                result.meshes[0].getChildMeshes()
            );
        }

        private struct EnvSettingAsset
        {
            public AbstractMesh Env { get; }
            public AbstractMesh[] AllMeshes { get; }

            public EnvSettingAsset(
                AbstractMesh env,
                AbstractMesh[] allMeshes
            )
            {
                Env = env;
                AllMeshes = allMeshes;
            }
        }
    }
}
