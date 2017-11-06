using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour {
	public ItemObject[] Element;
	public int currentPower = 0;
	public Renderer rend;
	public Material[] materials;
	// Use this for initialization
	int num = 1;

	void Start(){
		currentPower = Random.Range (0, Element.Length);
		rend = GetComponent<Renderer> ();
		rend.enabled = true;


	}

	void Update(){
		rend.sharedMaterial = materials [currentPower];
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			Destroy (gameObject);
			var target = other.gameObject.GetComponent<UseElement> ();
			target.recEle (Element [currentPower].Ele, num);
		}
	}
}
