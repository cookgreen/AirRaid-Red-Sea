using Mogre;
using Mogre.PhysX;
using org.critterai.nav;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class Waypoint
    {

        private Vector3 position;
        public Vector3 Position
        {
            get { return position; }
        }

        public Waypoint(Vector3 position)
        {
            this.position = position;
        }
    }

    public class WaypointMesh
    {
        private Navmesh navmesh;
        private List<Waypoint> waypoints;

        public Navmesh Navmesh
        {
            get { return navmesh; }
        }
        public List<Waypoint> Waypoints
        {
            get { return waypoints; }
        }

        public WaypointMesh(Entity ent)
        {
            navmesh = MeshToNavmesh.LoadNavmesh(ent);
            waypoints = WaypointsManager.Instance.LoadWaypointsFromEntity(ent);
        }
    }

    public class WaypointsManager
    {
        private Dictionary<string, WaypointMesh> waypointsDic;
        private Dictionary<string, int> gameObjectAIWaypointDic;

        private static WaypointsManager instance;
        public static WaypointsManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new WaypointsManager();
                return instance;
            }
        }

        public WaypointsManager()
        {
            waypointsDic = new Dictionary<string, WaypointMesh>();
            gameObjectAIWaypointDic = new Dictionary<string, int>();
        }

        public void LoadWaypointsFromMesh(SceneManager sceneManager, string waypointMeshName)
        {
            string name = Path.GetFileNameWithoutExtension(waypointMeshName);

            if (waypointsDic.ContainsKey(name))
                return;

            List<Waypoint> waypoints = new List<Waypoint>();

            var ent = sceneManager.CreateEntity("WAYPOINTS-ENT-"+Guid.NewGuid().ToString(), waypointMeshName);
            waypointsDic[name] = new WaypointMesh(ent);
            
            sceneManager.DestroyEntity(ent);
            ent.Dispose();
        }

        public unsafe List<Waypoint> LoadWaypointsFromEntity(Entity ent)
        {
            Mesh mesh = ent.GetMesh();
            List<Waypoint> waypoints = null;
            for (ushort i = 0; i < mesh.NumSubMeshes; i++)
            {
                SubMesh subMesh = mesh.GetSubMesh(i);

                if (!subMesh.useSharedVertices)
                {
                    VertexData vd = subMesh.vertexData;
                    VertexElement posElement = vd.vertexDeclaration.FindElementBySemantic(VertexElementSemantic.VES_POSITION);
                    HardwareVertexBufferSharedPtr buffer = vd.vertexBufferBinding.GetBuffer(posElement.Source);
                    byte* ptr = (byte*)buffer.Lock(HardwareBuffer.LockOptions.HBL_NORMAL);
                    float* arr = default(float*);
                    for (uint j = 0; j < vd.vertexCount; j++)
                    {
                        posElement.BaseVertexPointerToElement(ptr, &arr);
                        float x = *arr;
                        float y = arr[1];
                        float z = arr[2];

                        Waypoint waypoint = new Waypoint(new Vector3(x, y, z));
                        waypoints.Add(waypoint);

                        ptr += buffer.VertexSize;
                    }

                    buffer.Unlock();
                }
            }
            ent.Dispose();
            return waypoints;
        }

        public WaypointMesh GetWaypointMesh(string name)
        { 
            return waypointsDic[name]; 
        }  

        public Waypoint SpawnGameObjectAIAtRandomWaypoint(GameObject gameObject)
        {
            switch(gameObject.TypeName)
            {
                case "AircraftAI":
                    AircraftInfo info = (gameObject.Info as AircraftInfo);
                    List<Waypoint> waypoints = null;
                    switch (info.AircraftType)
                    {
                        case AircraftType.Fighter:
                            waypoints = GetWaypointMesh("AircraftFight_Waypoint").Waypoints;
                            break;
                        case AircraftType.Bomber:
                            waypoints = GetWaypointMesh("AircraftBomber_Waypoint").Waypoints;
                            break;
                    }
                    var randomWayPoint = waypoints.Random();
                    int randomIndex = waypoints.IndexOf(randomWayPoint);
                    gameObjectAIWaypointDic[gameObject.ID] = randomIndex;
                    return randomWayPoint;
                case "WarshipAI":
                    break;
            }

            return null;
        }

        public Waypoint GetGameObjectAICurrentWaypoint(GameObject gameObject)
        {
            int currentIndex = gameObjectAIWaypointDic[gameObject.ID];
            switch (gameObject.TypeName)
            {
                case "AircraftAI":
                    List<Waypoint> waypoints = null;
                    AircraftInfo aircraftInfo = gameObject.Info as AircraftInfo;
                    switch (aircraftInfo.AircraftType)
                    {
                        case AircraftType.Fighter:
                            waypoints = GetWaypointMesh("AircraftFighter_Waypoint").Waypoints;
                            break;
                        case AircraftType.Bomber:
                            waypoints = GetWaypointMesh("AircraftBomber_Waypoint").Waypoints;
                            break;
                    }
                    return waypoints[currentIndex];
            }
            return null;
        }

        public Waypoint MoveToGameObjectAINextWaypoint(GameObject gameObject)
        {
            int currentIndex = gameObjectAIWaypointDic[gameObject.ID];
            switch (gameObject.TypeName)
            {
                case "AircraftAI":
                    List<Waypoint> waypoints = null;
                    AircraftInfo aircraftInfo = gameObject.Info as AircraftInfo;
                    switch (aircraftInfo.AircraftType)
                    {
                        case AircraftType.Fighter:
                            waypoints = GetWaypointMesh("AircraftFighter_Waypoint").Waypoints;
                            break;
                        case AircraftType.Bomber:
                            waypoints = GetWaypointMesh("AircraftBomber_Waypoint").Waypoints;
                            break;
                    }

                    if (currentIndex >= 0 && currentIndex < waypoints.Count)
                    {
                        currentIndex = currentIndex + 1;
                    }
                    else
                    {
                        currentIndex = 0;
                    }

                    gameObjectAIWaypointDic[gameObject.ID] = currentIndex;
                    return waypoints[currentIndex];
            }
            return null;
        }

        public Waypoint GetGameObjectAINextWaypoint(GameObject gameObject)
        {
            int nextIndex = -1;
            int currentIndex = gameObjectAIWaypointDic[gameObject.ID];
            switch(gameObject.TypeName)
            {
                case "AircraftAI":
                    List<Waypoint> waypoints = null;
                    AircraftInfo aircraftInfo = gameObject.Info as AircraftInfo;
                    switch(aircraftInfo.AircraftType)
                    {
                        case AircraftType.Fighter:
                            waypoints = GetWaypointMesh("AircraftFighter_Waypoint").Waypoints;
                            break;
                        case AircraftType.Bomber:
                            waypoints = GetWaypointMesh("AircraftBomber_Waypoint").Waypoints;
                            break;
                    }

                    if (currentIndex >= 0 && currentIndex < waypoints.Count)
                    {
                        nextIndex = currentIndex + 1;
                    }
                    else
                    {
                        nextIndex = 0;
                    }

                    gameObjectAIWaypointDic[gameObject.ID] = currentIndex;
                    return waypoints[nextIndex];
            }
            return null;
        }

        public void Update(double timeSinceLastFrame)
        {
            foreach (var kpl in gameObjectAIWaypointDic)
            {
                AIDrivedGameObject gameObject = GameObjectManager.Instance.GetGameObjectByID(kpl.Key) as AIDrivedGameObject;
                var vect = gameObject.Controller.Position - GetGameObjectAICurrentWaypoint(gameObject).Position;
                if (vect.SquaredLength < 0.1f)
                {
                    gameObject.WaypointReached();
                }
            }
        }

        public Stack<Waypoint> GetIntervalWaypointsBetweenTwoPoint(WaypointMesh waypointMesh, Waypoint startWaypoint, Waypoint endWaypoint)
        {
            Stack<Waypoint> waypoints = new Stack<Waypoint>();

            NavmeshQuery query;
            var status = NavmeshQuery.Create(waypointMesh.Navmesh, 1024, out query);
            if (!NavUtil.Failed(status))
            {
                org.critterai.Vector3 navStartPointVect;
                org.critterai.Vector3 navEndPointVect;
                var navStartPointStatus = query.GetNearestPoint(1, new org.critterai.Vector3(startWaypoint.Position.x, startWaypoint.Position.y, startWaypoint.Position.z), out navStartPointVect);
                var navEndPointStatus = query.GetNearestPoint(1, new org.critterai.Vector3(startWaypoint.Position.x, startWaypoint.Position.y, startWaypoint.Position.z), out navEndPointVect);
                if (navStartPointStatus == NavStatus.Sucess && navEndPointStatus == NavStatus.Sucess)
                {
                    NavmeshPoint navStartPoint = new NavmeshPoint(1, new org.critterai.Vector3(startWaypoint.Position.x, startWaypoint.Position.y, startWaypoint.Position.z));
                    NavmeshPoint navEndPoint = new NavmeshPoint(1, new org.critterai.Vector3(endWaypoint.Position.x, endWaypoint.Position.y, endWaypoint.Position.z));

                    uint[] path = new uint[1024];
                    int pathCount;
                    status = query.FindPath(navStartPoint, navEndPoint, new NavmeshQueryFilter(), path, out pathCount);
                    if (!NavUtil.Failed(status))
                    {
                        const int MaxStraightPath = 4;
                        int wpCount;
                        org.critterai.Vector3[] wpPoints = new org.critterai.Vector3[MaxStraightPath];
                        uint[] wpPath = new uint[MaxStraightPath];

                        WaypointFlag[] wpFlags = new WaypointFlag[MaxStraightPath];
                        status = query.GetStraightPath(navStartPoint.point, navEndPoint.point
                                                       , path, 0, pathCount, wpPoints, wpFlags, wpPath
                                                       , out wpCount);
                        if (!NavUtil.Failed(status) && wpCount > 0)
                        {
                            foreach (var wp in wpPoints)
                            {
                                Mogre.Vector3 wayPointPos = new Vector3(wp.x, wp.y, wp.z);
                                waypoints.Push(new Waypoint(wayPointPos));
                            }
                        }
                    }
                }
            }

            return waypoints;
        }
    }
}
