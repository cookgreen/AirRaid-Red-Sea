using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class GameObjectManager
    {
        private static GameObjectManager instance;
        public static GameObjectManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new GameObjectManager();
                return instance;
            }
        }

        public GameObjectManager()
        {

        }

        public GameObject CreateGameObject(
            string typeName, Camera camera, 
            string meshName, string meshMaterialName,
            GameObjectInfo gameObjectInfo)
        {
            GameObject gameObject = null;

            switch(typeName)
            {
                case "Aircraft":
                    gameObject = new Aircraft(camera, meshName, meshMaterialName);
                    break;
                case "NavalWarship":
                    gameObject = new NavalWarship(gameObjectInfo, camera, meshName, meshMaterialName);
                    break;
                default:
                    break;
            }
            return gameObject;
        }
    }
}
