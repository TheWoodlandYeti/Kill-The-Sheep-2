using System;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [NonSerialized]
    public GameObject player;
    public GameObject defaultGun;
    public GameObject[] gunList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        AssignItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignItems()
    {
        //defaultGun.transform.SetParent(player.transform);
    }
}
