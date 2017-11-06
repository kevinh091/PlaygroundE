using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EleSpawn : NetworkBehaviour
{
    public GameObject Element;
    public int numOfEle;

    public override void OnStartServer()
    {
        for (int i = 0; i < numOfEle; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-400.0f, 400.0f), Random.Range(-400.0f, 400.0f));

            GameObject element = (GameObject)Instantiate(Element, spawnPos, Quaternion.identity);
            NetworkServer.Spawn(element);
        }
    }  
}
