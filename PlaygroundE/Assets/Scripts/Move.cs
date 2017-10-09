using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Move : NetworkBehaviour {

    public float Speed;
    private Vector3 Target;
    public int worldSize;


void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        int SpeedMod = gameObject.GetComponent<UseElement>().next;  // can be optimized if user sufers from fps; more eles you are carrying, slower
	if (Input.GetMouseButtonDown(1))
        {
            Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Mathf.Abs(Target.x) > worldSize) { Target = transform.position; }
            if (Mathf.Abs(Target.y) > worldSize) { Target = transform.position; }
            Target.z = transform.position.z;
        }
        Vector3 temp = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, Target, (Speed - SpeedMod) * Time.deltaTime);
    }
}
