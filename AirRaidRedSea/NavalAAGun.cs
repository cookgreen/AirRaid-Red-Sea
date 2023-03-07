using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class NavalAAGunInfo : GameObjectInfo
    {
        public float ShootSpeed { get; set; }
        public float Ammo { get; set; }
    }

    public class NavalAAGun : GameObject
    {
        public NavalAAGunInfo NavalAAGunInfo { get; set; }

        public NavalAAGun(GameObjectInfo gameObjectInfo, Camera camera, string meshName, string meshMaterialName, SceneNode parentSceneNode, Vector3 initPosition) : 
            base(gameObjectInfo, camera, meshName, meshMaterialName, parentSceneNode, initPosition)
        {
            controller = new NavalAAGunController(camera, meshName, meshMaterialName, parentSceneNode, initPosition);
        }

        public void AttachBarrel(string meshName, string meshMaterialName, Vector3 position)
        {
            ((NavalAAGunController)controller).AttachBarrel(meshName, meshMaterialName, position);
        }

        public void AttachCamera(Vector3 cameraOffset)
        {
            controller.AttachCamera(cameraOffset);
        }
    }
}
