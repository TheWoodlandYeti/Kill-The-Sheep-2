using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class NPCPatrol2 : MonoBehaviour
    {
        //if wait
        [SerializeField]
        bool patrolWaiting;

        //time of wait
        [SerializeField]
        float totalWaitTime = 3f;

        //probability of change direction
        [SerializeField]
        float switchProbability = 0.2f;

        //private vars for base behaviour
        NavMeshAgent navMeshAgent;
        ConnectedWaypoint currentWaypoint;
        ConnectedWaypoint previousWaypoint;

        bool travelling;
        bool waiting;
        float waitTimer;
        int waypointsVisited;

        public void Start()
        {
            navMeshAgent = this.GetComponent<NavMeshAgent>();

            if (navMeshAgent == null)
            {
                Debug.LogError("Nav mesh not attached to" + gameObject.name);
            }

            else
            {
                if (currentWaypoint == null)
                {
                    //set random
                    //get all waypoints

                    GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                    if (allWaypoints.Length > 0)
                    {
                        while (currentWaypoint == null)
                        {
                            int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                            ConnectedWaypoint startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoint>();

                            //found waypoint
                            if (startingWaypoint != null)
                            {
                                currentWaypoint = startingWaypoint;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Failed to find waypoints for use");
                    }
                }

                SetDestination();
            }
        }

        public void Update()
        {
            //Check if close to destination
            if (gameObject.GetComponent<NavMeshAgent>().isActiveAndEnabled == true)
            {
                if (travelling && navMeshAgent.remainingDistance <= 1f)
                {
                    travelling = false;
                    waypointsVisited++;

                    //if wait, then wait
                    if (patrolWaiting)
                    {
                        waiting = true;
                        waitTimer = 0f;
                    }

                    else
                    {
                        SetDestination();
                    }
                }
            }

            if (waiting)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= totalWaitTime)
                {
                    waiting = false;

                    SetDestination();
                }
            }
        }

        private void SetDestination()
        {
            if (waypointsVisited > 0)
            {
                ConnectedWaypoint nextWaypoint = currentWaypoint.NextWaypoint(previousWaypoint);
                previousWaypoint = currentWaypoint;
                currentWaypoint = nextWaypoint;
            }

            Vector3 targetVector = currentWaypoint.transform.position;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
    }

}
