using Mogre;
using Mogre.PhysX;
using MOIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class GameObjectController
    {
        protected Camera camera;
        protected string meshName;
        protected string meshMaterialName;
        protected SceneNode sceneNode;
        protected SceneNode parentSceneNode;
        protected SceneNode cameraSceneNode;

        public SceneNode SceneNode
        {
            get { return sceneNode; }
        }

        public SceneNode ParentSceneNode
        {
            get { return parentSceneNode; }
        }

        public GameObjectController(Camera camera, string meshName, string meshMaterialName, SceneNode parentSceneNode) 
        {
            this.camera = camera;
            this.meshName = meshName;
            this.meshMaterialName = meshMaterialName;
            this.parentSceneNode = parentSceneNode;
        }

        protected string getRandomEntityName()
        {
            return "Ent-" + Guid.NewGuid().ToString();
        }

        public virtual void Initization()
        {
            SceneManager sceneManager = camera.SceneManager;
            var ent = sceneManager.CreateEntity(getRandomEntityName(), meshName);
            sceneNode = parentSceneNode.CreateChildSceneNode();
            cameraSceneNode = sceneNode.CreateChildSceneNode();
            sceneNode.AttachObject(ent);
        }

        public virtual void AttachCamera() 
        {
            cameraSceneNode.AttachObject(camera);
        }

        public virtual void DetachCamera() 
        {
            cameraSceneNode.DetachObject(camera);
        }

        public virtual void Update(double timeSinceLastFrame) { }

        public virtual void InjectMouseMove(MouseEvent evt){ }

        public virtual void InjectMouseDown(MouseEvent evt, MouseButtonID id){ }

        public virtual void InjectMouseUp(MouseEvent evt, MouseButtonID id){ }

        public virtual void InjectKeyDown(KeyEvent evt) { }

        public virtual void InjectKeyUp(KeyEvent evt) { }

        public void Destroy()
        {
            cameraSceneNode.RemoveAndDestroyAllChildren();
            cameraSceneNode.Dispose();

            sceneNode.RemoveAndDestroyAllChildren();
            sceneNode.Dispose();
        }
    }
}
