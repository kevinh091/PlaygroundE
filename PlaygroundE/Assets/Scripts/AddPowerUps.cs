using UnityEngine;
using System.Collections;

public class AddPowerUps : MonoBehaviour {
	public GameObject itemPrefab;
	GameObject itemClone;
	public Transform pos;
	Vector3 spawn_pos;
	float x,y,z;
	float timer = 0.0f;

	void spawn_PwrUp(){
		x = pos.position.x + Random.Range (-200.0f, 200.0f);
		y = pos.position.y + Random.Range (-200.0f, 200.0f);
		z = pos.position.z;
		spawn_pos = new Vector3(x,y,z);

		itemClone = Instantiate (itemPrefab, spawn_pos, itemPrefab.transform.rotation) as GameObject;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 1) {
			spawn_PwrUp ();
			timer = 0.0f;
		}
	}
}