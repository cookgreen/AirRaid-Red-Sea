using org.ogre.framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class MainGameApp
    {
        private AppStateManager appStateManager;

        public MainGameApp()
        {
            appStateManager = null;
        }

        public void Start()
        {
            if (!OgreFramework.Instance.InitOgre("Air Raid : Red Sea", "AirRaidRedSea.ico", "AirRaidRedSeaGame", "resources.cfg"))
                return;

            OgreFramework.Instance.log.LogMessage("Game initialized!");

            appStateManager = new AppStateManager();

            AppState.Create<MenuState>(appStateManager, "MainMenu");
            AppState.Create<GameState>(appStateManager, "GameState");

            appStateManager.Start(appStateManager.FindByName("MainMenu"));
        }
    }
}
