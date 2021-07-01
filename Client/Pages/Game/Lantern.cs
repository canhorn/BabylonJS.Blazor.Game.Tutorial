namespace BabylonJS.Blazor.Game.Tutorial.Client.Pages.Game
{
    using System.Linq;
    using BABYLON;

    public class Lantern
    {
        private readonly Scene _scene;
        private readonly PBRMetallicRoughnessMaterial _lightMaterial;
        private readonly Mesh _lightSphere;
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
            lightSphere.isVisible = true;
            lightSphere.isPickable = false;
            _lightSphere = lightSphere;

            this.LoadLantern(
                position
            );
        }

        public void SetEmissiveTexture()
        {
            IsLit = true;

            // Swap Texture
            Mesh.material = _lightMaterial;

            // Create light source for the lanterns
            var light = new PointLight(
                "lantern light",
                Mesh.getAbsolutePosition(),
                _scene
            )
            {
                intensity = 30,
                radius = 2,
                diffuse = new Color3(
                    0.45m,
                    0.56m,
                    0.80m
                )
            };
            FindNearestMesh(
                light
            );
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

    }
}
