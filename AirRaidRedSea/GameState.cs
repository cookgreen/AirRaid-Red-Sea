using Mogre;
using MOIS;
using MyGUI.OgrePlatform;
using MyGUI.Sharp;
using org.ogre.framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class GameState : AppState
    {
        private AirRaidRedSeaGame game;
        private int currentLevel = 1;
        private bool isQuit = false;

        public GameState()
        {
            game = new AirRaidRedSeaGame();
        }

        public override void Enter()
        {
            OgreFramework.Instance.trayMgr.destroyAllWidgets();

            sceneMgr = OgreFramework.Instance.root.CreateSceneManager(Mogre.SceneType.ST_GENERIC);
            sceneMgr.SetSkyDome(true, "Examples/CloudySky", 5, 8);

            camera = sceneMgr.CreateCamera("GameCamera");
            camera.SetPosition(0, 0, 800);
            camera.LookAt(0, 0, 0);
            camera.NearClipDistance = 1;
            camera.AspectRatio = OgreFramework.Instance.viewport.ActualWidth /
                OgreFramework.Instance.viewport.ActualHeight;
            OgreFramework.Instance.viewport.Camera = camera;

            OgreFramework.Instance.mouse.MouseMoved += mouseMoved;
            OgreFramework.Instance.mouse.MousePressed += mousePressed;
            OgreFramework.Instance.mouse.MouseReleased += mouseReleased;
            OgreFramework.Instance.keyboard.KeyPressed += keyPressed;
            OgreFramework.Instance.keyboard.KeyReleased += keyReleased;

            Export.CreateGUI();
            Export.SetRenderWindow(OgreFramework.Instance.renderWnd);
            Export.SetSceneManager(sceneMgr);
            Export.SetActiveViewport(0);

            OgreFramework.Instance.trayMgr.hideCursor();
            PointerManager.Instance.Visible = false;

            game.Setup(camera);
        }

        public override void Exit()
        {
            OgreFramework.Instance.mouse.MouseMoved -= mouseMoved;
            OgreFramework.Instance.mouse.MousePressed -= mousePressed;
            OgreFramework.Instance.mouse.MouseReleased -= mouseReleased;
            OgreFramework.Instance.keyboard.KeyPressed -= keyPressed;
            OgreFramework.Instance.keyboard.KeyReleased -= keyReleased;

            if(sceneMgr!= null) 
            {
                sceneMgr.DestroyCamera(camera);
                OgreFramework.Instance.root.DestroySceneManager(sceneMgr);
            }

            OgreFramework.Instance.trayMgr.clearAllTrays();
            OgreFramework.Instance.trayMgr.destroyAllWidgets();
            OgreFramework.Instance.trayMgr.setListener(null);
        }

        public override void Update(double timeSinceLastFrame)
        {
            game.Update(timeSinceLastFrame);
        }

        public bool keyPressed(KeyEvent keyEventRef)
        {
            if (OgreFramework.Instance.keyboard.IsKeyDown(MOIS.KeyCode.KC_ESCAPE))
            {
                isQuit = true;
                return true;
            }

            OgreFramework.Instance.KeyPressed(keyEventRef);
            return true;
        }
        public bool keyReleased(KeyEvent keyEventRef)
        {
            OgreFramework.Instance.KeyReleased(keyEventRef);
            return true;
        }

        public bool mouseMoved(MouseEvent evt)
        {
            if (OgreFramework.Instance.trayMgr.injectMouseMove(evt)) return true;
            return true;
        }
        public bool mousePressed(MouseEvent evt, MouseButtonID id)
        {
            if (OgreFramework.Instance.trayMgr.injectMouseDown(evt, id)) return true;
            return true;
        }
        public bool mouseReleased(MouseEvent evt, MouseButtonID id)
        {
            if (OgreFramework.Instance.trayMgr.injectMouseUp(evt, id)) return true;
            return true;
        }
    }
}
