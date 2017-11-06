using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class AddPowerUps : NetworkBehaviour
{
	public GameObject itemPrefab;
	public Transform pos;
	Vector3 spawn_pos;
	float x,y,z;
	float timer = 0.0f;

    void spawn_PwrUp(){
        for (int i = 0; i < 2; i++)
        {
            x = pos.position.x + Random.Range(-200.0f, 200.0f);
            y = pos.position.y + Random.Range(-200.0f, 200.0f);
            z = pos.position.z;
            spawn_pos = new Vector3(x, y, z);
            GameObject itemClone = Instantiate(itemPrefab, spawn_pos, itemPrefab.transform.rotation);
            NetworkServer.Spawn(itemClone);
        }
    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 500; i++)
        {
            x = pos.position.x + Random.Range(-200.0f, 200.0f);
            y = pos.position.y + Random.Range(-200.0f, 200.0f);
            z = pos.position.z;
            spawn_pos = new Vector3(x, y, z);
            GameObject itemClone = Instantiate(itemPrefab, spawn_pos, itemPrefab.transform.rotation);
            NetworkServer.Spawn(itemClone);
        }
    }

	// Update is called once per frame
	void Update () {
        if (!isServer)
        {
            return;
        }
        timer += Time.deltaTime;
		if (timer > 1) {
			spawn_PwrUp ();
			timer = 0.0f;
		}
	}
}