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
        private List<GameObject> gameObjects;

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
            gameObjects = new List<GameObject>();
        }

        public GameObject CreateGameObject(
            string typeName, Camera camera, 
            string meshName, string meshMaterialName,
            GameObjectInfo gameObjectInfo,
            SceneNode parentSceneNode, 
            Vector3 initPosition)
        {
            GameObject gameObject = null;

            switch(typeName)
            {
                case "Aircraft":
                    gameObject = new Aircraft(gameObjectInfo, 
                        camera, meshName, meshMaterialName, 
                        parentSceneNode, initPosition);
                    break;
                case "NavalWarship":
                    gameObject = new NavalWarship(gameObjectInfo, 
                        camera, meshName, meshMaterialName, 
                        parentSceneNode, initPosition);
                    break;
                case "NavalAAGun":
                    gameObject = new NavalAAGun(gameObjectInfo, 
                        camera, meshName, meshMaterialName, 
                        parentSceneNode, initPosition);
                    break;
                case "EnemyWarship":
                    break;
                default:
                    break;
            }
            gameObject.TypeName = typeName;
            gameObjects.Add(gameObject);

            return gameObject;
        }

        public void PlayerControlGameObjectChanged(GameObject gameObject, string typeName)
        {
            OnPlayerControlGameObjectChanged?.Invoke(gameObject, typeName);
        }

        public List<GameObject> FindGameObjectsInRange(GameObject srcGameObject, float range)
        {
            List<GameObject> resultGameObjects = new List<GameObject>();

            foreach(var gameObject in gameObjects)
            {
                var vect = gameObject.Controller.Position - srcGameObject.Controller.Position;
                if (vect.SquaredLength < range)
                {
                    resultGameObjects.Add(gameObject);
                }
            }

            return resultGameObjects;
        }

        public List<GameObject> FindGameObjectsInRangeWithTypeName(GameObject srcGameObject, float range, string[] typeNames)
        {
            List<GameObject> resultGameObjects = new List<GameObject>();

            foreach (var gameObject in gameObjects)
            {
                var vect = gameObject.Controller.Position - srcGameObject.Controller.Position;
                if (vect.SquaredLength < range && typeNames.Contains(gameObject.TypeName))
                {
                    resultGameObjects.Add(gameObject);
                }
            }

            return resultGameObjects;
        }
    }
}
