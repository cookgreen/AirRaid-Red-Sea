using Mogre;
using MOIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class NavalAAGunController : GameObjectController
    {
        private SceneNode navalAAGunBarrelSceneNode;

        public NavalAAGunController(Camera camera, string meshName, string meshMaterialName, SceneNode parentSceneNode)
            : base(camera, meshName, meshMaterialName, parentSceneNode)
        {
        }

        public override void Initization()
        {
            SceneManager sceneManager = camera.SceneManager;
            var ent = sceneManager.CreateEntity(getRandomEntityName(), meshName);
            sceneNode = parentSceneNode.CreateChildSceneNode();
            sceneNode.AttachObject(ent);

            ent = camera.SceneManager.CreateEntity(getRandomEntityName(), "NavalAAGunBarrel.mesh");
            navalAAGunBarrelSceneNode = sceneNode.CreateChildSceneNode();
            navalAAGunBarrelSceneNode.AttachObject(ent);

            cameraSceneNode = navalAAGunBarrelSceneNode.CreateChildSceneNode();
        }

        public override void InjectMouseDown(MouseEvent evt, MouseButtonID id)
        {
            if(id == MouseButtonID.MB_Left)
            {
                if (AmmoManager.Instance.IsEmpty)
                {
                    SoundManager.Instance.PlaySound("ammo-empty.mp3");
                }
                else
                {
                    AmmoManager.Instance.RemoveAmmo(1);
                    SoundManager.Instance.PlaySound("gun-fire.mp3");
                    //Shoot the bullet
                }
            }
        }

        public override void InjectMouseMove(MouseEvent evt)
        {
            base.InjectMouseMove(evt);
        }
    }
}
