using Mogre;
using MOIS;
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
        private Camera camera;
        private SceneManager sceneManager;
        private GameLevel currentLevel;
        private GameLevelsXml gameLevelDataList;
        private Player player;

        public AirRaidRedSeaGame(GameLevelsXml gameLevelDataList)
        {
            this.gameLevelDataList = gameLevelDataList;
        }

        public void Setup(Camera camera)
        {
            this.camera = camera;

            setupUI();
            setupScene();
            setupGame();
            switchNextLevel();
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
        }

        private void setupScene()
        {
            sceneManager = camera.SceneManager;
            //Create Ship

            //Create AAGun
        }

        private void setupGame()
        {
            AmmoManager.Instance.InitAmmo(300);

            player = new Player("Player1");
            player.UI.PlayerAmmoUI.Init();

            player.PlayerWinFullGame += Player_PlayerWinFullGame;
            player.PlayerGameOver += Player_PlayerGameOver;
            player.PlayerWinThisRound += Player_PlayerWinThisRound;

            NavalWarshipInfo navalWarshipInfo = new NavalWarshipInfo();
            navalWarshipInfo.SlotPosition = new List<Mogre.Vector3>
            {
                new Mogre.Vector3(10, 0, 0),
                new Mogre.Vector3(20, 0, 0),
                new Mogre.Vector3(0, 0, 10),
                new Mogre.Vector3(0, 0, 20),
            };
            navalWarshipInfo.Speed = 20;
            navalWarshipInfo.Hitpoint = player.PlayerHitpoint.CurrentHitpointPercent;
            navalWarshipInfo.SlotNumber = 4;

            GameObjectManager.Instance.CreateGameObject("NavalWarship", camera, 
                "NavalWarship.mesh", "NavalWarship", 
                navalWarshipInfo, camera.SceneManager.RootSceneNode);
        }

        private void switchNextLevel()
        {
            int currentLevelNumber;
            if (currentLevel != null)
            {
                currentLevelNumber = currentLevel.LevelNumber;
                currentLevel.Stop();
                currentLevelNumber++;
            }
            else
            {
                currentLevelNumber = 1;
            }
            currentLevel = new GameLevel(currentLevelNumber, gameLevelDataList.Levels[currentLevelNumber - 1], camera);
            currentLevel.Start();
        }

        private void Player_PlayerWinThisRound()
        {
            switchNextLevel();
        }

        private void Player_PlayerGameOver()
        {
            //Show the Mission failed Screen
            //Change to MainMenu
        }

        private void Player_PlayerWinFullGame()
        {
            //Show the Victory Screen
            //Change to Credit
        }

        public void InjectMouseMove(MouseEvent evt)
        {
            currentLevel.InjectMouseMove(evt);
        }

        public void InjectMouseDown(MouseEvent evt, MouseButtonID id) 
        { 
            currentLevel.InjectMouseDown(evt, id);
        }

        public void InjectMouseUp(MouseEvent evt, MouseButtonID id)
        {
            currentLevel.InjectMouseUp(evt, id);
        }

        public void InjectKeyDown(KeyEvent evt)
        {
            currentLevel.InjectKeyDown(evt);
        }

        public void InjectKeyUp(KeyEvent evt)
        {
            currentLevel.InjectKeyUp(evt);
        }

        public void Update(double timeSinceLastFrame)
        {
        }
    }

    public class GameLevel
    {
        private int levelNumber;
        private GameLevelXml levelData;
        private Camera camera;
        private GameObject currentControlledObject;

        public int LevelNumber
        {
            get { return levelNumber; }
        }

        public GameLevel(int levelNumber, GameLevelXml levelData, Camera camera)
        {
            this.levelNumber = levelNumber;
            this.levelData = levelData;
            this.camera = camera;
        }

        public void Start()
        {
            GameObjectManager.Instance.OnPlayerControlGameObjectChanged += PlayerControlGameObjectChanged;

            //Create Enemeies
        }

        public void InjectMouseMove(MouseEvent evt)
        {
            currentControlledObject.InjectMouseMove(evt);
        }

        public void InjectMouseDown(MouseEvent evt, MouseButtonID id)
        {
            currentControlledObject.InjectMouseDown(evt, id);
        }

        public void InjectMouseUp(MouseEvent evt, MouseButtonID id)
        {
            currentControlledObject.InjectMouseUp(evt, id);
        }

        public void InjectKeyDown(KeyEvent evt)
        {
            currentControlledObject.InjectKeyDown(evt);
        }

        public void InjectKeyUp(KeyEvent evt)
        {
            currentControlledObject.InjectKeyDown(evt);
        }

        private void PlayerControlGameObjectChanged(GameObject gameObject, string gameObjectType)
        {
            currentControlledObject = gameObject;
        }

        public void Stop()
        {
            //Clear the scene
            currentControlledObject.Destroy();
        }
    }
}
