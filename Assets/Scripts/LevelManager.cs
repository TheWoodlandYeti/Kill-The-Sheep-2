using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject player;
    [NonSerialized]
    public GameObject playerOrigin;

    void Start()
    {
        int indexToUse = UnityEngine.Random.Range(0, levels.Length);
        SinglePerameterInstantiate(levels[indexToUse]);
        GetPlayerOrigin();
        MultiPerameterInstantiate(player, new Vector3(playerOrigin.transform.position.x, playerOrigin.transform.position.y, playerOrigin.transform.position.z), Quaternion.identity);
    }
    //not really necessary, but gameobject is the object you want to spawn, the vector is its position, the quaternion is its rotation. Ta da.
    public void MultiPerameterInstantiate(GameObject gameObject,Vector3 vector3, Quaternion quaternion)
    {
        Instantiate(gameObject, new Vector3(vector3.x,vector3.y,vector3.z), quaternion);
    }

    public void SinglePerameterInstantiate(GameObject gameObject)
    {
        Instantiate(gameObject, new Vector3(0,0,0), Quaternion.identity);
    }

    public void GetPlayerOrigin()
    {
        playerOrigin = GameObject.FindGameObjectWithTag("PlayerOrigin");
    }
}
