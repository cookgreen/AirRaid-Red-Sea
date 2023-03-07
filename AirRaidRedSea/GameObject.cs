using Mogre;
using MOIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class GameObject
    {
        protected string id;
        protected string meshName;
        protected string meshMaterialName;
        protected GameObjectController controller;
        protected List<GameObject> attachedGameObjects;

        public GameObjectController Controller
        {
            get { return controller; }
        }

        public GameObject(GameObjectInfo gameObjectInfo, Camera camera, string meshName, string meshMaterialName, SceneNode paretSceneNode, Mogre.Vector3 initPosition)
        {
            id = Guid.NewGuid().ToString();
            this.meshName = meshName;
            this.meshMaterialName = meshMaterialName;
            controller = new GameObjectController(camera, meshName, meshMaterialName, paretSceneNode, initPosition);
            attachedGameObjects = new List<GameObject>();
        }

        public virtual void Initization()
        {
            controller.Initization();
        }

        public void AttachGameObject(GameObject gameObject)
        {
            controller.SceneNode.AddChild(gameObject.Controller.SceneNode);
            attachedGameObjects.Add(gameObject);
        }

        public void InjectMouseMove(MouseEvent evt)
        {
            controller.InjectMouseMove(evt);
        }

        public void InjectMouseDown(MouseEvent evt, MouseButtonID id)
        {
            controller.InjectMouseDown(evt, id);
        }

        public void InjectMouseUp(MouseEvent evt, MouseButtonID id)
        {
            controller.InjectMouseUp(evt, id);
        }

        public void InjectKeyDown(KeyEvent evt)
        {
            controller.InjectKeyDown(evt);
        }

        public void InjectKeyUp(KeyEvent evt)
        {
            controller.InjectKeyUp(evt);
        }

        public void Destroy()
        {
            controller.Destroy();
        }

        public void Update(double deltaTime)
        {
            controller.Update(deltaTime);
        }
    }
}
