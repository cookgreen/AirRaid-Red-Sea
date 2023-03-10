using MyGUI.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class PlayerUI
    {
        private Player player;
        private PlayerAmmoUI playerAmmoUI;
        private StaticText txtLevelNumber;
        private StaticText txtLevelScore;

        public StaticText TextBoxLevelNumber
        {
            get { return txtLevelNumber; }
            set { txtLevelNumber = value; }
        }

        public StaticText TextBoxScore
        {
            get { return txtLevelScore; }
            set { txtLevelScore = value; }
        }

        public PlayerAmmoUI PlayerAmmoUI 
        { 
            get { return playerAmmoUI; } 
        }

        public PlayerUI(Player player, PlayerAmmoUI playerAmmoUI)
        {
            this.player = player;
            this.playerAmmoUI= playerAmmoUI;
            ScoreManager.Instance.ScoreChanged += ScoreChanged;
        }

        private void ScoreChanged()
        {
            txtLevelScore.SetCaption(ScoreManager.Instance.Score.ToString());
        }
    }
}
