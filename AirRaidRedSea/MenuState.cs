using Mogre;
using Mogre_Procedural.MogreBites;
using MOIS;
using NAudio.Wave;
using org.ogre.framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class MenuState : AppState
    {
        private bool isQuit;

        public override void Enter()
        {
            FontManager.Singleton.GetByName("SdkTrays/Caption").Load();

            OgreFramework.Instance.trayMgr.showCursor("AirRaidRedSea/UI/Cursor/Icon");

            SoundManager.Instance.PlayLoop("AirRaidRedSea-MainTheme.mp3");

            sceneMgr = OgreFramework.Instance.root.CreateSceneManager(Mogre.SceneType.ST_GENERIC);
            sceneMgr.AmbientLight = new ColourValue(0.7f, 0.7f, 0.7f);

            camera = sceneMgr.CreateCamera("MenuCamera");
            camera.SetPosition(0, 0, 800);
            camera.LookAt(0, 0, 0);
            camera.NearClipDistance = 1;
            camera.AspectRatio = OgreFramework.Instance.viewport.ActualWidth / 
                OgreFramework.Instance.viewport.ActualHeight;
            OgreFramework.Instance.viewport.Camera = camera;

            OgreFramework.Instance.trayMgr.destroyAllWidgets();

            createBackground("AirraidMainScreen.png");

            var btnStart = OgreFramework.Instance.trayMgr.createButton(TrayLocation.TL_CENTER, "btnStart", "Start", 200, 40, 0, 0);
            var btnOptions = OgreFramework.Instance.trayMgr.createButton(TrayLocation.TL_CENTER, "btnOptions", "Options", 200, 40, 0, 0);
            var btnCredit = OgreFramework.Instance.trayMgr.createButton(TrayLocation.TL_CENTER, "btnCredit", "Credit", 200, 40, 0, 0);
            var btnExit = OgreFramework.Instance.trayMgr.createButton(TrayLocation.TL_CENTER, "btnExit", "Exit", 200, 40, 0, 0);

            btnStart.UpMaterial = "AirRaidRedSea/UI/Button/Up";
            btnStart.DownMaterial = "AirRaidRedSea/UI/Button/Down";
            btnStart.OverMaterial = "AirRaidRedSea/UI/Button/Over";
            btnStart.FontColor = new ColourValue(1, 1, 1);

            btnOptions.UpMaterial = "AirRaidRedSea/UI/Button/Up";
            btnOptions.DownMaterial = "AirRaidRedSea/UI/Button/Down";
            btnOptions.OverMaterial = "AirRaidRedSea/UI/Button/Over";
            btnOptions.FontColor = new ColourValue(1, 1, 1);

            btnCredit.UpMaterial = "AirRaidRedSea/UI/Button/Up";
            btnCredit.DownMaterial = "AirRaidRedSea/UI/Button/Down";
            btnCredit.OverMaterial = "AirRaidRedSea/UI/Button/Over";
            btnCredit.FontColor = new ColourValue(1, 1, 1);

            btnExit.UpMaterial = "AirRaidRedSea/UI/Button/Up";
            btnExit.DownMaterial = "AirRaidRedSea/UI/Button/Down";
            btnExit.OverMaterial = "AirRaidRedSea/UI/Button/Over";
            btnExit.FontColor = new ColourValue(1, 1, 1);

            OgreFramework.Instance.mouse.MouseMoved += mouseMoved;
            OgreFramework.Instance.mouse.MousePressed += mousePressed;
            OgreFramework.Instance.mouse.MouseReleased += mouseReleased;
            OgreFramework.Instance.keyboard.KeyPressed += keyPressed;
            OgreFramework.Instance.keyboard.KeyReleased += keyReleased;
        }

        private void createBackground(string textureName)
        {
            MaterialPtr material = MaterialManager.Singleton.Create("Background", "General");
            material.GetTechnique(0).GetPass(0).CreateTextureUnitState(textureName);
            material.GetTechnique(0).GetPass(0).DepthCheckEnabled = false;
            material.GetTechnique(0).GetPass(0).DepthWriteEnabled = false;
            material.GetTechnique(0).GetPass(0).LightingEnabled = false;

            Rectangle2D rect = new Rectangle2D(true);
            rect.SetCorners(-1.0f, 1.0f, 1.0f, -1.0f);
            rect.SetMaterial("Background");

            rect.RenderQueueGroup = (byte)RenderQueueGroupID.RENDER_QUEUE_BACKGROUND;

            AxisAlignedBox aab = new AxisAlignedBox();
            aab.SetInfinite();
            rect.BoundingBox = aab;

            SceneNode node = sceneMgr.RootSceneNode.CreateChildSceneNode("Background");
            node.AttachObject(rect);
        }

        public override void Exit()
        {
            SoundManager.Instance.StopCurrentLoop();
        }

        public override void Update(double timeSinceLastFrame)
        {
            if(isQuit)
            {
                SoundManager.Instance.StopCurrentLoop();
                shutdown();
                return;
            }
        }

        public override void buttonHit(Button button)
        {
            SoundManager.Instance.PlaySound("button-click.mp3");

            switch(button.getName())
            {
                case "btnExit":
                    isQuit = true;
                    break;
            }
        }

        public override void buttonOver(Button button)
        {
            SoundManager.Instance.PlaySound("button-hover.mp3");
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
