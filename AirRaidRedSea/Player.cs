using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class PlayerHitpoint
    {
        private int initHitpoint;
        private int hitpoint;
        private Player player;

        public int Hitpoint
        {
            get { return hitpoint; }
        }

        public float CurrentHitpointPercent
        {
            get
            {
                float percent = (float)hitpoint / (float)initHitpoint;
                return float.Parse(percent.ToString("0.0"));
            }
        }

        public PlayerHitpoint(Player player)
        {
            hitpoint = 1200;
            initHitpoint = hitpoint;

            this.player = player;
        }

        public void ChangeHitpoint(int newHitpoint)
        {
            hitpoint = newHitpoint;
            if (hitpoint == 0)
            {
                player.Die();
            }
        }
    }

    public class Player
    {
        private string name;
        private PlayerUI playerUI;
        private PlayerHitpoint playerHitpoint;

        public event Action PlayerGameOver;
        public event Action PlayerWinFullGame;
        public event Action PlayerWinThisRound;

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

            playerHitpoint = new PlayerHitpoint(this);

            PlayerAmmoUI playerAmmoUI = new PlayerAmmoUI();
            playerUI = new PlayerUI(this, playerAmmoUI);
        }

        public void Die()
        {
            PlayerGameOver?.Invoke();
        }

        public void WinAllLevels()
        {
            PlayerWinFullGame?.Invoke();
        }

        public void WinThisRound()
        {
            PlayerWinThisRound?.Invoke();
        }
    }
}
