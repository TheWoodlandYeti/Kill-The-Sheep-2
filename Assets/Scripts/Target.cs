using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public GameObject cube;
    public bool dead;

    public void TakeDamage(float amount)
    {
        // allows ragdoll
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        Rigidbody cubeRigidbody = cube.GetComponent<Rigidbody>();
        cubeRigidbody.isKinematic = false;
        //reduces health by amount 
        health -= amount;
    }

    public void Update()
    {
        if (health <= 0f)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
