using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class NavalWarshipInfo : GameObjectInfo
    {
        public float Speed { get; set; }
        public float SlotNumber { get; set; }
        public List<Vector3> SlotPosition { get; set; }
        public float Hitpoint { get; set; }
    }

    public class NavalWarship : GameObject
    {
        private NavalWarshipInfo shipInfo;
        private Camera camera;
        private List<NavalWarshipOperatorSlot> slots;

        public bool IsFull
        {
            get { return slots.Where(o => o.IsUsed).Count() == slots.Count; }
        }      
        public List<NavalWarshipOperatorSlot> Slots
        {
            get { return slots; }
        }

        public NavalWarship(GameObjectInfo shipInfo, Camera camera, string meshName, string meshMaterialName, SceneNode parentNode) : 
            base(shipInfo, camera, meshName, meshMaterialName)
        {
            this.camera = camera;
            this.shipInfo = shipInfo as NavalWarshipInfo;
            controller = new NavalWarshipController(camera, meshName, meshMaterialName, parentNode);
            slots = new List<NavalWarshipOperatorSlot>();
        }

        public void Initization()
        {
            for (int i = 0; i < shipInfo.SlotNumber; i++)
            {
                NavalWarshipOperatorSlot slot = new NavalWarshipOperatorSlot(i + 1, this, shipInfo.SlotPosition[i]);
                slot.Initization(camera);
                slots.Add(slot);
            }
            controller.Initization();
            SwitchSlot();
        }

        public void SwitchSlot()
        {
            if (!IsFull)
            {
                var unusedSlots = slots.Where(o => o.IsUsed);
                var unusedRandomSlot = unusedSlots.Random();
                unusedRandomSlot.Switch();
            }
        }

        public void DetachCamera()
        {
            controller.DetachCamera();
        }
    }


    public class NavalWarshipOperatorSlot
    {
        private int slotIndex;
        private NavalWarship ship;
        private Vector3 position;
        private bool isUsed;
        private NavalAAGun navalAAGun;

        public int Index
        {
            get { return slotIndex; }
        }

        public NavalAAGun NavalAAGun
        {
            get { return navalAAGun; }
        }

        public bool IsUsed
        {
            get { return isUsed; }
        }

        public Vector3 Position
        {
            get { return position; }
        }

        public NavalWarshipOperatorSlot(int index, NavalWarship ship, Vector3 position) 
        {
            this.ship = ship;
            this.position = position;
            slotIndex = index;
            isUsed = false;
        }

        public void Initization(Camera camera)
        {
            NavalAAGunInfo navalAAGunInfo = new NavalAAGunInfo();
            navalAAGunInfo.ShootSpeed = 30;
            navalAAGunInfo.Ammo = 300;
            
            navalAAGun = new NavalAAGun(navalAAGunInfo, camera, "NavalAAGun.mesh", "NavalAAGun", ship.Controller.SceneNode);
            ship.AttachGameObject(navalAAGun);
        }

        public void Switch()
        {
            ship.DetachCamera();
            navalAAGun.AttachCamera();
            GameObjectManager.Instance.PlayerControlGameObjectChanged(navalAAGun, "NavalAAGun");
        }
    }
}
