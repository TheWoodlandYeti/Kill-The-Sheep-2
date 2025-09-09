using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ConnectedWaypoint : WayPoint
    {   
        [SerializeField]
        protected float connectivityRadius = 50f;

        List<ConnectedWaypoint> connections;
        // Start is called before the first frame update
        void Start()
        {
            //get all waypoint objects
            GameObject[] allwaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

            //create list of waypoints
            connections = new List<ConnectedWaypoint>();

            //check if connected
            for (int i = 0; i < allwaypoints.Length; i++)
            {
                ConnectedWaypoint nextWaypoint = allwaypoints[i].GetComponent<ConnectedWaypoint>();

                //if found
                if(nextWaypoint != null)
                {
                    if(Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= connectivityRadius && nextWaypoint != this)
                    {
                        connections.Add(nextWaypoint);
                    }
                }
            }
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, connectivityRadius);
        }

        public ConnectedWaypoint NextWaypoint(ConnectedWaypoint previousWaypoint)
        {
            if (connections.Count == 0)
            {
                //no waypoint found, error
                Debug.LogError("Not enough countable waypoints, chief");
                return null;
            }

            else if (connections.Count == 1 && connections.Contains(previousWaypoint))
            {
                //only one waypoint and is previous, go back to previous
                return previousWaypoint;
            }

            else //otherwise, pick a random one from the choices given that isnt the previous one
            {
                ConnectedWaypoint nextWaypoint;
                int nextIndex = 0;

                do
                {
                    nextIndex = UnityEngine.Random.Range(0, connections.Count);
                    nextWaypoint = connections[nextIndex];

                } while (nextWaypoint == previousWaypoint);

                return nextWaypoint;
            }
        }
    }
}
