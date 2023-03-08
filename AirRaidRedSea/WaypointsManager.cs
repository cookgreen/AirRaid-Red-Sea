using Mogre;
using Mogre.PhysX;
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

    public class WaypointsManager
    {
        private Dictionary<string, Stack<Waypoint>> waypointsDic;

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
            waypointsDic = new Dictionary<string, Stack<Waypoint>>();
        }

        public unsafe void LoadWaypointsFromMesh(SceneManager sceneManager, string waypointMeshName)
        {
            string name = Path.GetFileNameWithoutExtension(waypointMeshName);

            if (waypointsDic.ContainsKey(name))
                return;

            Stack<Waypoint> waypoints = new Stack<Waypoint>();

            var ent = sceneManager.CreateEntity("WAYPOINTS-ENT-"+Guid.NewGuid().ToString(), waypointMeshName);
            Mesh mesh = ent.GetMesh();
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
                        waypoints.Push(waypoint);

                        ptr += buffer.VertexSize;
                    }

                    buffer.Unlock();
                }
            }
            ent.Dispose();
            waypointsDic[name] = waypoints;
        }

        public Stack<Waypoint> GetWaypoints(string name)
        { 
            return waypointsDic[name]; 
        }  
    }
}
