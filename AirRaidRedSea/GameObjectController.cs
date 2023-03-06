using Mogre;
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

        public SceneNode SceneNode
        {
            get { return sceneNode; }
        }

        public GameObjectController(Camera camera, string meshName, string meshMaterialName) 
        {
            this.camera = camera;
            this.meshName = meshName;
            this.meshMaterialName = meshMaterialName;
        }

        public virtual void Initization() { }

        public virtual void Update(double timeSinceLastFrame) { }
    }
}
