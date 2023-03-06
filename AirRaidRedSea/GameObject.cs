using Mogre;
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

        public GameObject(Camera camera, string meshName, string meshMaterialName, SceneNode paretSceneNode = null)
        {
            id = Guid.NewGuid().ToString();
            this.meshName = meshName;
            this.meshMaterialName = meshMaterialName;
            controller = new GameObjectController(camera, meshName, meshMaterialName);
            attachedGameObjects = new List<GameObject>();
        }

        public void AttachGameObject(GameObject gameObject)
        {
            controller.SceneNode.AddChild(gameObject.Controller.SceneNode);
            attachedGameObjects.Add(gameObject);
        }
    }
}
