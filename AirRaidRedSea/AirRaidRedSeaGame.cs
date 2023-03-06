using Mogre;
using MyGUI.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class AirRaidRedSeaGame
    {
        private Player player;

        public void Setup(Camera camera)
        {
            AmmoManager.Instance.InitAmmo(300);

            setupUI();
            createScene();
        }

        private void setupUI()
        {
            var imgLevel = Gui.Instance.CreateWidget<StaticImage>("StaticImage", new IntCoord(360, 10, 200, 100), Align.Top, "Main");
            imgLevel.SetImageTexture("UI-level.png");

            var txtLevelNumber = imgLevel.CreateWidget<StaticText>("StaticText", new IntCoord(93, 20, 100, 50), Align.Default);
            txtLevelNumber.TextColour = Colour.White;
            txtLevelNumber.FontName = "Airal";
            txtLevelNumber.FontHeight = 30;
            txtLevelNumber.TextAlign = Align.Center;
            txtLevelNumber.SetCaption("1");

            var imgHealthBarEmpty = Gui.Instance.CreateWidget<StaticImage>("StaticImage", new IntCoord(0, 655, 200, 115), Align.Bottom | Align.HStretch, "Main");
            imgHealthBarEmpty.SetImageTexture("UI-Healthbar.png");

            var imgHealthBarHitpoint = imgHealthBarEmpty.CreateWidget<StaticImage>("StaticImage", new IntCoord(0, 0, 200, 115), Align.Default);
            imgHealthBarHitpoint.SetImageTexture("UI-Healthbar-Hitpoint.png");

            var imgScore = Gui.Instance.CreateWidget<StaticImage>("StaticImage", new IntCoord(0, 0, 200, 106), Align.Default, "Main");
            imgScore.SetImageTexture("UI-score.png");
            Helper.SnapToParent(imgScore, Align.Right | Align.Top);

            var txtScore = imgScore.CreateWidget<StaticText>("StaticText", new IntCoord(0, 0, 100, 50), Align.Center);
            txtScore.TextColour = Colour.Black;
            txtScore.FontName = "Airal";
            txtScore.FontHeight = 30;
            txtScore.TextAlign = Align.Center;
            txtScore.SetCaption("0");

            player = new Player("Player1");
            player.UI.PlayerAmmoUI.Init();
        }

        private void createScene()
        {
        }

        public void Update(double timeSinceLastFrame)
        {
            AmmoManager.Instance.RemoveAmmo(1);
            Thread.Sleep(100);
        }
    }
}
