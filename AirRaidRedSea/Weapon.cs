using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class WeaponInfo : GameObjectInfo
    {
        public string Name { get; set; }
        public float Damage { get; set; }
        public float ShootSpeed { get; set; }
        public Vector3 ShootPosition { get; set; }
        public float RateOfFire { get; set; }
        public float Range { get; set; }
    }

    public class Weapon : GameObject
    {
        protected GameObject attacher;
        protected Camera camera;
        protected WeaponInfo aircraftWeaponInfo;

        public Weapon(
            GameObject attacher,
            WeaponInfo aircraftWeaponInfo,
            Camera camera,
            string meshName,
            string meshMaterialName,
            SceneNode parentNode,
            Vector3 initPosition) :
            base(aircraftWeaponInfo, camera, meshName, meshMaterialName, parentNode, initPosition)
        {
            this.aircraftWeaponInfo = aircraftWeaponInfo;
            this.attacher = attacher;
        }

        public virtual void Shoot()
        { }

        public virtual void HitTarget(GameObject targetGameObject)
        { }
    }
}
