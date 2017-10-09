using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Stats : NetworkBehaviour {

    public float max_health;
    [SyncVar(hook = "OnChangeHealth")]public float cur_health;
    public GameObject healthBar;
    private float timer = 0f;

    void Start()
    {
        //max_health = 100;
        //cur_health = 100;
        OnChangeHealth(cur_health);
    }

    void Update()
    {
        if (timer > 1f)
        {
            this.Damage(1.0f);
            timer = 0f;
        }else{
            timer += Time.deltaTime;
        }
    }

    public void Damage(float input) 
    {
        if (!isServer)
        {
            return;
        }
        cur_health -= input;
        if (cur_health < 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnChangeHealth(float health)
    {
        var myHealth = health / max_health;
        if (myHealth < 0) { myHealth = 0; }
        if (myHealth > 1) { myHealth = 1; }
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

}
