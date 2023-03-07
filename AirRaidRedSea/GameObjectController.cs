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
        protected Mogre.Vector3 position;

        protected bool isUsing;

        public SceneNode SceneNode
        {
            get { return sceneNode; }
        }

        public SceneNode ParentSceneNode
        {
            get { return parentSceneNode; }
        }

        public Mogre.Vector3 Position
        {
            get { return sceneNode.Position; }
            set 
            { 
                sceneNode.Position = value;
                position = value;
            }
        }

        public GameObjectController(Camera camera, string meshName, string meshMaterialName, SceneNode parentSceneNode, Mogre.Vector3 initPosition) 
        {
            this.camera = camera;
            this.meshName = meshName;
            this.meshMaterialName = meshMaterialName;
            this.parentSceneNode = parentSceneNode;
            position = initPosition;
            isUsing = false;
        }

        protected string getRandomEntityName()
        {
            return "Ent-" + Guid.NewGuid().ToString();
        }

        protected void setMaterialName(Entity ent, string materialName)
        {
            ent.SetMaterialName(materialName);
            uint num = ent.NumSubEntities;
            for (uint i = 0; i < num; i++)
            {
                var subEnt = ent.GetSubEntity(i);
                subEnt.SetMaterialName(materialName);
            }
        }

        public virtual void Initization()
        {
            SceneManager sceneManager = camera.SceneManager;
            var ent = sceneManager.CreateEntity(getRandomEntityName(), meshName);
            setMaterialName(ent, meshMaterialName);
            sceneNode = parentSceneNode.CreateChildSceneNode();
            cameraSceneNode = sceneNode.CreateChildSceneNode();
            sceneNode.AttachObject(ent);
            sceneNode.SetPosition(position.x, position.y, position.z);
        }

        public virtual void AttachCamera(Mogre.Vector3 cameraOffset) 
        {
            cameraSceneNode.AttachObject(camera);
            var cameraPos = cameraSceneNode.Position + cameraOffset;
            cameraSceneNode.SetPosition(
                cameraPos.x,
                cameraPos.y,
                cameraPos.z);

            var lookPos = sceneNode.Position;
            lookPos.z += 10f;
            camera.LookAt(lookPos);
            isUsing = true;
        }

        public virtual void DetachCamera() 
        {
            cameraSceneNode.DetachObject(camera);
            isUsing = false;
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
