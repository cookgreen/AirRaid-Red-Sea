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

        public event Action<GameObject, string> OnPlayerControlGameObjectChanged;

        public GameObjectManager()
        {

        }

        public GameObject CreateGameObject(
            string typeName, Camera camera, 
            string meshName, string meshMaterialName,
            GameObjectInfo gameObjectInfo,
            SceneNode parentSceneNode)
        {
            GameObject gameObject = null;

            switch(typeName)
            {
                case "Aircraft":
                    gameObject = new Aircraft(gameObjectInfo, camera, meshName, meshMaterialName, parentSceneNode);
                    break;
                case "NavalWarship":
                    gameObject = new NavalWarship(gameObjectInfo, camera, meshName, meshMaterialName, parentSceneNode);
                    break;
                case "NavalAAGun":
                    gameObject = new NavalAAGun(gameObjectInfo, camera, meshName, meshMaterialName, parentSceneNode);
                    break;
                default:
                    break;
            }
            return gameObject;
        }

        public void PlayerControlGameObjectChanged(GameObject gameObject, string typeName)
        {
            OnPlayerControlGameObjectChanged?.Invoke(gameObject, typeName);
        }
    }
}
