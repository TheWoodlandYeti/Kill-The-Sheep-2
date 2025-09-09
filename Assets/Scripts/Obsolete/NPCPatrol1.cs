using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol1 : MonoBehaviour
{
    // if agent wait
    [SerializeField]
    bool patrolWaiting;

    // total time waiting
    [SerializeField]
    float totalWaitTime = 3f;

    // Probability of directional switch
    [SerializeField]
    float switchProbability = 0.2f;

    // lift of all patrol points
    [SerializeField]
    List<WayPoint> patrolPoints;

    // private vars for base behaviour
    NavMeshAgent navMeshAgent;
    int currentPatrolIndex;
    bool travelling;
    bool waiting;
    bool patrolForward;
    float waitTimer;

    // initialization
    public void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("this is not the nav mesh you're looking for on " + gameObject.name);
        }

        else
        {
            if (patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 0;
                SetDestination();
            }

            else
            {
                Debug.Log("Where's all my patrol points?");
            }
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

                //if wait, then wait
                if (patrolWaiting)
                {
                    waiting = true;
                    waitTimer = 0f;
                }

                else
                {
                    ChangePatrolPoint();
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

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= switchProbability)
        {
            patrolForward = !patrolForward;
        }
        
        if (patrolForward)
        {
            currentPatrolIndex++;

            if (currentPatrolIndex >= patrolPoints.Count)
            {
                currentPatrolIndex = 0;
            }
           // currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }

        else
        {

            if(--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }
    }







































}