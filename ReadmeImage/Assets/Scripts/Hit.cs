using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour {

    public float damage;
    public float Speed = 1;
    public GameObject owner;

    void Update()
    {

    }
    void Start() {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner || owner==null) {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            var target = other.gameObject.GetComponent<Stats> ();
            target.Damage(damage);
            Destroy(this.gameObject);
        }
    }
}
