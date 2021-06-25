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

        public Task Load()
        {
            var ground = Mesh.CreateBox(
                name: "ground",
                size: 24,
                _scene
            );
            ground.scaling = new Vector3(
                1,
                0.02m,
                1
            );

            return Task.CompletedTask;
        }
    }
}
