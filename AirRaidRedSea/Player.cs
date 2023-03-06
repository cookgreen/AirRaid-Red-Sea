using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class Player
    {
        private string name;
        private PlayerUI playerUI;

        public string Name
        {
            get { return name; }
        }

        public PlayerUI UI
        {
            get { return playerUI; }
        }

        public Player(string name)
        {
            this.name = name;

            PlayerAmmoUI playerAmmoUI = new PlayerAmmoUI();
            playerUI = new PlayerUI(this, playerAmmoUI);
        }
    }
}
