using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class AircraftAIController : AIDrivedGameObjectController
    {
        protected int initDyingTime = 1000;
        protected bool isDying;
        protected int currentDyingTime;

        public AircraftAIController(AircraftAI aircraft, Camera camera, string meshName, string meshMaterialName, SceneNode parentSceneNode, Vector3 initPosition) :
            base(aircraft, camera, meshName, meshMaterialName, parentSceneNode, initPosition)
        {
            isDying = false;
            currentDyingTime = 0;
        }

        public virtual void Crash()
        {

        }
    }

    public class PropelleredAircraftAIController : AircraftAIController
    {
        private Waypoint nextWaypoint;
        private Stack<Waypoint> intervalWaypoints;

        public PropelleredAircraftAIController(PropelleredAircraftAI aircraft, Camera camera, string meshName, string meshMaterialName, SceneNode parentSceneNode, Vector3 initPosition) : 
            base(aircraft, camera, meshName, meshMaterialName, parentSceneNode, initPosition)
        {
        }

        public override void Initization()
        {
            base.Initization();
            
            PropelleredAircraftInfo propelleredAircraftInfo = aiObject.Info as PropelleredAircraftInfo; ;
            for (int i = 0; i < propelleredAircraftInfo.PropellerMeshNames.Count; i++)
            {
                AddNewSubEntity(
                    propelleredAircraftInfo.PropellerMeshNames[i],
                    propelleredAircraftInfo.PropellerOffsets[i],
                    sceneNode);
            }
            nextWaypoint = WaypointsManager.Instance.SpawnGameObjectAIAtRandomWaypoint(aiObject);
            sceneNode.Position = nextWaypoint.Position;
            generatedWaypoints();
        }

        public override void WaypointReached()
        {
            nextWaypoint = WaypointsManager.Instance.GetGameObjectAINextWaypoint(aiObject);
            generatedWaypoints();
        }

        private void generatedWaypoints()
        {
            string waypointMeshName = getWaypointMeshName();
            var waypointMesh = WaypointsManager.Instance.GetWaypointMesh(waypointMeshName);
            intervalWaypoints = WaypointsManager.Instance.GetIntervalWaypointsBetweenTwoPoint(waypointMesh, nextWaypoint,
                WaypointsManager.Instance.GetGameObjectAINextWaypoint(aiObject));
        }

        private string getWaypointMeshName()
        {
            switch (getAircraftType())
            {
                case AircraftType.Fighter:
                    return "AircraftFight_Waypoint";
                case AircraftType.Bomber:
                    return "AircraftBomber_Waypoint";
                case AircraftType.Torpedo:
                    return "AircraftTorpedo_Waypoint";
            }
            return null;
        }

        private AircraftType getAircraftType()
        {
            AircraftInfo aircraftInfo = aiObject.Info as AircraftInfo;
            return aircraftInfo.AircraftType;
        }

        public override void Update(double timeSinceLastFrame)
        {
            if (!isDying)
            {
                WaypointsManager.Instance.Update(timeSinceLastFrame);
                moveTo();
            }
            else
            {
                if (currentDyingTime == initDyingTime)
                {
                    camera.SceneManager.DestroySceneNode(sceneNode);
                    currentDyingTime = 0;
                }
                else
                {
                    sceneNode.Translate(new Vector3(0, 0, 20));
                    currentDyingTime++;
                }
            }
        }

        public override void Crash()
        {
            switch(getAircraftType())
            {
                case AircraftType.Fighter:
                case AircraftType.Torpedo:
                    sceneNode.Pitch(new Radian(new Degree(-45)));
                    isDying = true;
                    break;
                case AircraftType.Bomber:
                    sceneNode.Pitch(new Radian(new Degree(-90)));
                    isDying = true;
                    break;
            }
        }

        private void moveTo()
        {
            Waypoint waypoint = intervalWaypoints.Pop();
            sceneNode.Position = waypoint.Position;
        }
    }
}
