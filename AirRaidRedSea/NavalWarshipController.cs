using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class NavalWarshipController : GameObjectController
    {
        public NavalWarshipController(Camera camera, string meshName, string meshMaterialName, SceneNode parentSceneNode, Mogre.Vector3 initPosition) : 
            base(camera, meshName, meshMaterialName, parentSceneNode, initPosition)
        {
        }
    }
}
