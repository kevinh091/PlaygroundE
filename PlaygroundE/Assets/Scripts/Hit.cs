using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour {

    public float damage;
    public float Speed = 1;

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var target = other.gameObject.GetComponent<Stats> ();
            target.Damage(damage);
        }
    }
}
