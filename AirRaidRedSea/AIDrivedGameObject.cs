using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class AIDecisionSystem
    {
        protected AIDrivedGameObjectController aiController;

        public AIDecisionSystem(AIDrivedGameObjectController aiController) 
        { 
            this.aiController = aiController;
        }

        public virtual void Think(double timeSinceLastFrame)
        {
        }
    }

    public class AIDrivedGameObjectController : GameObjectController
    {
        protected AIDrivedGameObject aiObject;
        protected AIDecisionSystem aiBrain;

        public AIDrivedGameObject AIObject
        {
            get { return aiObject; }
        }

        public AIDrivedGameObjectController(AIDrivedGameObject aiObject, Camera camera, string meshName, string meshMaterialName, SceneNode parentSceneNode, Vector3 initPosition) : 
            base(camera, meshName, meshMaterialName, parentSceneNode, initPosition)
        {
            this.aiObject = aiObject;
            aiBrain = new AIDecisionSystem(this);
        }

        public override void Update(double timeSinceLastFrame)
        {
            aiBrain.Think(timeSinceLastFrame);
        }

        public virtual void WaypointReached()
        {

        }
    }

    /// <summary>
    /// This game object will be controlled by AI
    /// </summary>
    public class AIDrivedGameObject : GameObject
    {
        protected AIDrivedGameObjectController aiController;

        public AIDrivedGameObject(GameObjectInfo gameObjectInfo, Camera camera, string meshName, string meshMaterialName, SceneNode paretSceneNode, Vector3 initPosition) : 
            base(gameObjectInfo, camera, meshName, meshMaterialName, paretSceneNode, initPosition)
        {
            controller = new AIDrivedGameObjectController(this, camera, meshName, meshMaterialName, paretSceneNode, initPosition);
            aiController = controller as AIDrivedGameObjectController;
        }

        public override void Update(double deltaTime)
        {
            aiController.Update(deltaTime);
        }

        public void WaypointReached()
        {
            aiController.WaypointReached();
        }
    }
}
