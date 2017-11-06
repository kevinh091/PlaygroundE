﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Move : NetworkBehaviour {

    public float Speed;
    private Vector3 Target;
    public int worldSize;
    public bool isStealth = false;


void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        int SpeedMod = gameObject.GetComponent<UseElement>().next;  // can be optimized if user sufers from fps; more eles you are carrying, slower
	if (Input.GetMouseButtonDown(1))
        {
            if (isStealth == true) {
                Vector3 stealth = new Vector3(0, 0, 4);
                this.transform.position = this.transform.position - stealth;
                isStealth = false;
            }
            Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Mathf.Abs(Target.x) > worldSize) { Target = transform.position; }
            if (Mathf.Abs(Target.y) > worldSize) { Target = transform.position; }
            if (Target.x>17 && Target.x<23 &&Target.y>17&&Target.y<23) { Target = transform.position; }
            Target.z = transform.position.z;
        }
        Vector3 temp = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, Target, (Speed - SpeedMod) * Time.deltaTime);
    }
}
