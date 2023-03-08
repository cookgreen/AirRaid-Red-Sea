using Mogre;
using MOIS;
using MyGUI.Sharp;
using System;
using System.Collections.Generic;
using System.IO;
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
            switchNextLevel();
            setupGame();
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
        }

        private void setupGame()
        {
            WaypointsManager.Instance.LoadWaypointsFromMesh(sceneManager, "AircraftFight_Waypoint.mesh");
            WaypointsManager.Instance.LoadWaypointsFromMesh(sceneManager, "AircraftBomber_Waypoint.mesh");
            WaypointsManager.Instance.LoadWaypointsFromMesh(sceneManager, "AircraftTorpedo_Waypoint.mesh");

            AmmoManager.Instance.InitAmmo(300);

            player = new Player("Player1");
            player.UI.PlayerAmmoUI.Init();

            player.PlayerWinFullGame += Player_PlayerWinFullGame;
            player.PlayerGameOver += Player_PlayerGameOver;
            player.PlayerWinThisRound += Player_PlayerWinThisRound;

            NavalWarshipInfo navalWarshipInfo = new NavalWarshipInfo();
            navalWarshipInfo.SlotPositions = new List<Mogre.Vector3>
            {
                new Mogre.Vector3(-4f, 1.5f, 1),
                new Mogre.Vector3(-5, 2, 0),
                new Mogre.Vector3(0, 2, 5),
                new Mogre.Vector3(0, 2, -5),
            };
            navalWarshipInfo.OffsetPositions = new List<Mogre.Vector3>
            {
                new Mogre.Vector3(0, 0.6f, 0),
                new Mogre.Vector3(0, 0, -2.1459f),
                new Mogre.Vector3(0, 0, -2.1459f),
                new Mogre.Vector3(0, 0, -2.1459f),
            };
            navalWarshipInfo.Speed = 20;
            navalWarshipInfo.Hitpoint = player.PlayerHitpoint.CurrentHitpointPercent;
            navalWarshipInfo.SlotNumber = 1;

            var navalWarshipObject = GameObjectManager.Instance.CreateGameObject("NavalWarship", camera, 
                "NavalWarship.mesh", "NavalWarshipMat", 
                navalWarshipInfo,
                new Mogre.Vector3(0, 0, 0));
            navalWarshipObject.Initization();
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
            if (evt.key == MOIS.KeyCode.KC_W)
                camera.MoveRelative(new Mogre.Vector3(0, 0, -0.1f));
            if (evt.key == MOIS.KeyCode.KC_S)
                camera.MoveRelative(new Mogre.Vector3(0, 0, 0.1f));
            if (evt.key == MOIS.KeyCode.KC_A)
                camera.MoveRelative(new Mogre.Vector3(-0.1f, 0, 0));
            if (evt.key == MOIS.KeyCode.KC_D)
                camera.MoveRelative(new Mogre.Vector3(0.1f, 0, 0));

            if (evt.key == MOIS.KeyCode.KC_G)
                camera.MoveRelative(new Mogre.Vector3(0, 0.1f, 0));
            if (evt.key == MOIS.KeyCode.KC_T)
                camera.MoveRelative(new Mogre.Vector3(0, -0.1f, 0));

            if (evt.key == MOIS.KeyCode.KC_Z)
                camera.Yaw(0.1f);
            if (evt.key == MOIS.KeyCode.KC_X)
                camera.Pitch(0.1f);

            currentLevel.InjectKeyDown(evt);
        }

        public void InjectKeyUp(KeyEvent evt)
        {
            currentLevel.InjectKeyUp(evt);
        }

        public void Update(double timeSinceLastFrame)
        {
            currentLevel.Update(timeSinceLastFrame);
        }
    }

    public class GameLevel
    {
        private int levelNumber;
        private GameLevelXml levelData;
        private Camera camera;
        private GameObject currentControlledObject;
        private Stack<string> enemyTypes;

        private int currentDelay;
        private int initDelay;

        public int LevelNumber
        {
            get { return levelNumber; }
        }

        public GameLevel(int levelNumber, GameLevelXml levelData, Camera camera)
        {
            this.levelNumber = levelNumber;
            this.levelData = levelData;
            this.camera = camera;
            enemyTypes = new Stack<string>();
            SoundManager.Instance.OnSoundPlayerTriggerEvent += OnSoundPlayTriggeredEvent;
        }

        private void OnSoundPlayTriggeredEvent(string soundName)
        {
            if (soundName == Path.GetFileNameWithoutExtension(levelData.RadioMusic))
            {
                SoundManager.Instance.PlayEvent("alarm.ogg");
            }
            else if (soundName == "alarm")
            {
                currentDelay = 0;
                SoundManager.Instance.PlayLoop(levelData.AmbientMusic);
                SoundManager.Instance.PlayLoop(levelData.AmbientBattle);
            }
        }

        public void Start()
        {
            currentDelay = -1;
            initDelay = 100;

            GameObjectManager.Instance.OnPlayerControlGameObjectChanged += PlayerControlGameObjectChanged;
            var aircraftFighterNumber = levelData.AircraftFighterNumber;
            var aircraftBomberNumber = levelData.AircraftBomberNumber;
            var aircraftAssultNumber = levelData.AircraftAssultNumber;
            var aircraftTorpedoNumber = levelData.AircraftTorpedoNumber;
            int numMax = 1;
            if (aircraftBomberNumber > 0)
            {
                numMax++;
            }
            if(aircraftTorpedoNumber > 0)
            {
                numMax++;
            }
            if (aircraftAssultNumber > 0)
            {
                numMax++;
            }
            Random rand = new Random();

            for (int i = 0; i < levelData.AircraftNumber; i++)
            {
                int random = rand.Next(0, numMax);
                if (random == 0)
                {
                    enemyTypes.Push("AircraftFighter");
                }
                else if (random == 1)
                {
                    enemyTypes.Push("AircraftBomber");
                }
                else if (random == 2)
                {
                    enemyTypes.Push("AircraftTorpedo");
                }
                else if (random == 3)
                {
                    enemyTypes.Push("AircraftAssult");
                }
            }

            SoundManager.Instance.PlayEvent(levelData.RadioMusic);
        }

        public void InjectMouseMove(MouseEvent evt)
        {
            if (currentControlledObject == null)
                return;

            currentControlledObject.InjectMouseMove(evt);
        }

        public void InjectMouseDown(MouseEvent evt, MouseButtonID id)
        {
            if (currentControlledObject == null)
                return;

            currentControlledObject.InjectMouseDown(evt, id);
        }

        public void InjectMouseUp(MouseEvent evt, MouseButtonID id)
        {
            if (currentControlledObject == null)
                return;

            currentControlledObject.InjectMouseUp(evt, id);
        }

        public void InjectKeyDown(KeyEvent evt)
        {
            if (currentControlledObject == null)
                return;

            currentControlledObject.InjectKeyDown(evt);
        }

        public void InjectKeyUp(KeyEvent evt)
        {
            if (currentControlledObject == null)
                return;

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

        public void Update(double deltaTime)
        {
            if (currentControlledObject == null)
                return;

            currentControlledObject.Update(deltaTime);

            if (currentDelay == -1)
                return;

            if (currentDelay == initDelay)
            {
                if (enemyTypes.Count == 0)
                {
                    currentDelay = -1;
                    return;
                }

                GameObject gameObject;
                PropelleredAircraftInfo propelleredAircraftInfo = new PropelleredAircraftInfo();
                string enemyType = enemyTypes.Pop();
                switch (enemyType)
                {
                    case "AircraftFighter":
                        propelleredAircraftInfo.AircraftType = AircraftType.Fighter;
                        propelleredAircraftInfo.Speed = 30;
                        propelleredAircraftInfo.PropellerOffsets = new List<Mogre.Vector3>()
                    {
                        new Mogre.Vector3(0, 0, 0.5f)
                    };
                        propelleredAircraftInfo.PropellerMeshNames = new List<string>()
                    {
                        "AircraftFighter_BF109_Propeller.mesh"
                    };
                        gameObject = GameObjectManager.Instance.CreateGameObject("AircraftAI", camera,
                            "AircraftFighter_BF109.mesh", "AircraftFighter_BF109",
                            propelleredAircraftInfo, new Mogre.Vector3());
                        gameObject.Initization();
                        break;
                    case "AircraftBomber":
                        propelleredAircraftInfo.AircraftType = AircraftType.Fighter;
                        propelleredAircraftInfo.Speed = 15;
                        propelleredAircraftInfo.PropellerOffsets = new List<Mogre.Vector3>()
                    {
                        new Mogre.Vector3(0, 0, 0.5f)
                    };
                        propelleredAircraftInfo.PropellerMeshNames = new List<string>()
                    {
                        "AircraftBomber_Junker88_Propeller_1.mesh",
                        "AircraftBomber_Junker88_Propeller_2.mesh"
                    };
                        gameObject = GameObjectManager.Instance.CreateGameObject(
                            "AircraftAI", camera, "AircraftBomber_Junker88.mesh",
                            "AircraftFighter_BF109", propelleredAircraftInfo, new Mogre.Vector3());
                        gameObject.Initization();
                        break;
                    case "AircraftTorpedo":
                        propelleredAircraftInfo.AircraftType = AircraftType.Fighter;
                        propelleredAircraftInfo.Speed = 35;
                        propelleredAircraftInfo.PropellerOffsets = new List<Mogre.Vector3>()
                    {
                        new Mogre.Vector3(0, 0, 0.5f)
                    };
                        propelleredAircraftInfo.PropellerMeshNames = new List<string>()
                    {
                        "AircraftTorpedo_Propeller.mesh"
                    };
                        gameObject = GameObjectManager.Instance.CreateGameObject(
                            "AircraftAI", camera, "AircraftTorpedo.mesh",
                            "AircraftTorpedo", propelleredAircraftInfo, new Mogre.Vector3());
                        gameObject.Initization();
                        break;
                    case "AircraftAssult":
                        propelleredAircraftInfo.AircraftType = AircraftType.Fighter;
                        propelleredAircraftInfo.Speed = 50;
                        propelleredAircraftInfo.PropellerOffsets = new List<Mogre.Vector3>()
                    {
                        new Mogre.Vector3(0, 0, 0.5f)
                    };
                        propelleredAircraftInfo.PropellerMeshNames = new List<string>()
                    {
                        "AircraftAssult_Propeller.mesh"
                    };
                        gameObject = GameObjectManager.Instance.CreateGameObject(
                            "AircraftAI", camera, "AircraftAssult.mesh",
                            "AircraftAssult", propelleredAircraftInfo, new Mogre.Vector3());
                        gameObject.Initization();
                        break;
                }
                currentDelay = 0;
            }
            else
            {
                currentDelay++;
            }
        }
    }
}
