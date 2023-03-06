using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public enum AmmoChangeType
    {
        Add,
        Remove,
        Change
    }
    public class AmmoManager
    {
        private int ammoNumber;
        private bool isInited;

        public int CurrentAmmoNumber { get { return ammoNumber; } }

        public event Action<AmmoChangeType> AmmoChanged;

        private static AmmoManager instance;
        public static AmmoManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new AmmoManager();
                return instance;
            }
        }

        public void InitAmmo(int ammoNumber)
        {
            if(!isInited)
            {
                this.ammoNumber= ammoNumber;
                isInited = true;
            }
        }

        public void AddAmmo(int increaseNumber)
        {
            ammoNumber += increaseNumber;
            AmmoChanged?.Invoke(AmmoChangeType.Add);
        }

        public void RemoveAmmo(int decreaseNumber)
        {
            ammoNumber -= decreaseNumber;
            AmmoChanged?.Invoke(AmmoChangeType.Remove);
        }

        public void ChangeAmmo(int newAmmoNumber)
        {
            ammoNumber = newAmmoNumber;
            AmmoChanged?.Invoke(AmmoChangeType.Change);
        }
    }
}
