using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public enum AircraftType
    {
        Fighter,
        Bomber,
        Torpedo,
        Assult,
        Scout,
    }

    public enum AircraftBehavior
    {
        Attack,
        ThrowBomb,
        SendTorpedo,
        Patrol
    }

    public class AircraftInfo : GameObjectInfo
    {
        public AircraftType AircraftType { get; set; }
        public AircraftBehavior AircraftBehavior { get; set; }
        public float Speed { get; set; }
    }

    public class AircraftWeapon
    {
        public float Damage { get; set; }
        
        public AircraftWeapon(Camera camera, string meshName, string meshMaterialName)
        {
            
        }
    }

    public class Aircraft : GameObject
    {
        private List<AircraftWeapon> weapons;

        public List<AircraftWeapon> Weapons
        {
            get { return weapons; }
        }
        public AircraftInfo AircraftInfo { get; set; }

        public Aircraft(Camera camera, string meshName, string meshMaterialName) : base(camera, meshName, meshMaterialName)
        {
            id = "Aircraft-" + id;
            controller = new AircraftController(camera, meshName, meshMaterialName);
        }
    }
}
