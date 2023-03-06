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
        private bool IsFull
        {
            get { return slots.Where(o => o.IsUsed).Count() == slots.Count; }
        }
        
        public List<NavalWarshipOperatorSlot> Slots
        {
            get { return slots; }
        }

        public NavalWarship(GameObjectInfo shipInfo, Camera camera, string meshName, string meshMaterialName) : 
            base(camera, meshName, meshMaterialName)
        {
            this.camera = camera;
            this.shipInfo = shipInfo as NavalWarshipInfo;
            controller = new NavalWarshipController(camera, meshName, meshMaterialName);
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
        }

        public void Initization(Camera camera)
        {
            navalAAGun = new NavalAAGun(camera, "NavalAAGun.mesh", "NavalAAGun");
            ship.AttachGameObject(navalAAGun);
        }
    }
}
