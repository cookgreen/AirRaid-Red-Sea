using Mogre;
using Mogre.PhysX;
using MOIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector3 = Mogre.Vector3;

namespace AirRaidRedSea
{
    public class NavalAAGunController : GameObjectController
    {
        private SceneNode navalAAGunBarrelSceneNode;

        private Vector2 mousePosition;

        private float xAngle = 0;
        private float yAngle = 0;

        private float yRotateFloat = 0;

        private const float AAGUN_LIMIT_ANGLE_MIN = 0;
        private const float AAGUN_LIMIT_ANGLE_MAX = 90;

        public NavalAAGunController(Camera camera, string meshName, string meshMaterialName, SceneNode parentSceneNode, Vector3 initPosition)
            : base(camera, meshName, meshMaterialName, parentSceneNode, initPosition)
        {
        }

        public override void Initization()
        {
            SceneManager sceneManager = camera.SceneManager;
            var ent = sceneManager.CreateEntity(getRandomEntityName(), meshName);
            setMaterialName(ent, meshMaterialName);
            sceneNode = parentSceneNode.CreateChildSceneNode();
            sceneNode.SetPosition(position.x, position.y, position.z);
            sceneNode.AttachObject(ent);
        }

        public void AttachBarrel(string meshName, string meshMaterialName, Mogre.Vector3 position)
        {
            var ent = camera.SceneManager.CreateEntity(getRandomEntityName(), meshName);
            setMaterialName(ent, meshMaterialName);
            navalAAGunBarrelSceneNode = sceneNode.CreateChildSceneNode();
            navalAAGunBarrelSceneNode.AttachObject(ent);
            navalAAGunBarrelSceneNode.SetPosition(position.x, position.y, position.z);

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
                    //AmmoManager.Instance.RemoveAmmo(1);
                    //SoundManager.Instance.PlaySound("gun-fire.mp3");
                    //Shoot the bullet
                }
            }
        }

        public override void InjectMouseMove(MouseEvent evt)
        {
            mousePosition = new Vector2(evt.state.X.rel, evt.state.Y.rel);
        }

        public override void Update(double timeSinceLastFrame)
        {
            if (isUsing)
            {
                camera.Position = cameraSceneNode.Position;

                xAngle = mousePosition.x * -0.01f;
                yAngle = mousePosition.y * -0.01f;

                sceneNode.Yaw(new Radian(new Degree(xAngle)));

                if (yRotateFloat <= AAGUN_LIMIT_ANGLE_MAX && yRotateFloat >= AAGUN_LIMIT_ANGLE_MIN)
                {
                    navalAAGunBarrelSceneNode.Pitch(new Radian(new Degree(yAngle)));
                }

                yRotateFloat += yAngle * -1;
            }
        }
    }
}
