using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class AircraftController : GameObjectController
    {
        public AircraftController(Camera camera, string meshName, string meshMaterialName) : 
            base(camera, meshName, meshMaterialName)
        {

        }
    }
}
