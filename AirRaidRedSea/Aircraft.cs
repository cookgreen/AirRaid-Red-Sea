using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class AvaiationMachineGun : Weapon
    {
        public AvaiationMachineGun(GameObject attacher, Camera camera, SceneNode parentNode, Vector3 initPosition) :
            base(attacher,
                new WeaponInfo()
                {
                    Damage = 5,
                    Name = "Avaiation Machine Gun",
                    RateOfFire = 10,
                    ShootPosition = new Vector3(2, 0, 0),
                    Range = 100
                },
                camera, "AvaiationMachineGun_Bullet.mesh", "AvaiationMachineGun_Bullet", parentNode, initPosition)
        {
        }

        public override void Shoot()
        {
            base.Shoot();
        }
    }

    public class AvaiationBomb : Weapon
    {
        public AvaiationBomb(GameObject attacher, Camera camera, SceneNode parentNode, Vector3 initPosition) :
            base(attacher,
                new WeaponInfo()
                {
                    Damage = 15,
                    Name = "Avaiation Bomb",
                    RateOfFire = 20,
                    ShootPosition = new Vector3(0, 0, -5),
                    ShootSpeed = 10,
                    Range = 999
                },
                camera, "AvaiationMachineGun_Bullet.mesh", "AvaiationMachineGun_Bullet", parentNode, initPosition)
        {
        }

        public override void Shoot()
        {
            base.Shoot();
        }
    }

    public class AvaiationTorpedo : Weapon
    {
        public AvaiationTorpedo(GameObject attacher, Camera camera, SceneNode parentNode, Vector3 initPosition) :
            base(attacher, new WeaponInfo()
            {
                Damage = 20,
                Range = 200,
                Name = "Avaiation Torpedo",
                RateOfFire = -1,
                ShootPosition = new Vector3(0, 0, -3)
            }, camera, "AvaiationTorpedo.mesh", "AvaiationTorpedo", parentNode, initPosition)
        {
        }
    }

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

    public class AircraftAIDecsionSystem : AIDecisionSystem
    {
        public AircraftAIDecsionSystem(AIDrivedGameObjectController aiController) : base(aiController)
        {
        }

        public override void Think(double timeSinceLastFrame)
        {
            AircraftAI aircraftObject = aiController.AIObject as AircraftAI;
            foreach(var weapon in aircraftObject.Weapons)
            {
                float range = ((WeaponInfo)weapon.Info).Range;
                var resultGameObjects = GameObjectManager.Instance.FindGameObjectsInRangeWithTypeName(aircraftObject, range, new
                    string[] { "NavalWarship", "NavalAAGun" });
                if (resultGameObjects .Count > 0)
                {
                    weapon.Shoot();
                }
            }
        }
    }

    public class AircraftAI : AIDrivedGameObject
    {
        private int HP;
        private List<Weapon> weapons;

        public List<Weapon> Weapons
        {
            get { return weapons; }
        }
        public AircraftInfo AircraftInfo { get; set; }

        public AircraftAI(GameObjectInfo gameObjectInfo, Camera camera, string meshName, string meshMaterialName, SceneNode parentSceneNode, Vector3 initPosition) : 
            base(gameObjectInfo, camera, meshName, meshMaterialName, parentSceneNode, initPosition)
        {
            id = "Aircraft-" + id;
            controller = new AircraftAIController(this, camera, meshName, meshMaterialName, parentSceneNode, initPosition);
            weapons = new List<Weapon>();
            AircraftInfo aircraftInfo = gameObjectInfo as AircraftInfo;
            switch(aircraftInfo.AircraftType)
            {
                case AircraftType.Fighter:
                    HP = 600;
                    break;
                case AircraftType.Bomber:
                    HP = 800;
                    break;
                case AircraftType.Torpedo:
                    HP = 850;
                    break;
                case AircraftType.Scout:
                    HP = 750;
                    break;
                case AircraftType.Assult:
                    HP = 900;
                    break;
            }
        }

        public void OnHit(GameObject hitter, int damage)
        {
            HP -= damage;
            if (HP < 0)
            {
                HP = 0;
            }
        }

        public override void Update(double deltaTime)
        {
            if (HP == 0)
            {
                ((AircraftAIController)controller).Crash();
            }
        }
    }

    public class PropelleredAircraftInfo : AircraftInfo
    {
        public List<string> PropellerMeshNames { get; set; }
        public List<Vector3> PropellerOffsets { get; set; }

        public PropelleredAircraftInfo()
        {
            PropellerMeshNames = new List<string>();
            PropellerOffsets = new List<Vector3>();
        }
    }

    public class PropelleredAircraftAI : AircraftAI
    {
        public PropelleredAircraftAI(
            GameObjectInfo gameObjectInfo, Camera camera, 
            string meshName, string meshMaterialName, 
            SceneNode parentSceneNode, Vector3 initPosition) 
            : base(gameObjectInfo, camera, meshName, meshMaterialName, parentSceneNode, initPosition)
        {
            controller = new PropelleredAircraftAIController(this, camera, meshName, meshMaterialName, parentSceneNode, initPosition);
        }
    }
}
