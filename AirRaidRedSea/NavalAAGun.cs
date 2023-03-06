using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class NavalAAGunInfo : GameObjectInfo
    {
        public float ShootSpeed { get; set; }
        public float Ammo { get; set; }
    }

    public class NavalAAGun : GameObject
    {
        public NavalAAGunInfo NavalAAGunInfo { get; set; }

        public NavalAAGun(Camera camera, string meshName, string meshMaterialName) : 
            base(camera, meshName, meshMaterialName)
        {

        }
    }
}
