using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Stats : NetworkBehaviour {

    public float max_health;
    [SyncVar(hook = "OnChangeHealth")]public float cur_health;
    public GameObject healthBar;
    private float timer = 0f;
    private GameObject NetPreb;
    public GameObject Guide;
    public List<GameObject> heros;

    void Start()
    {
        //max_health = 100;
        //cur_health = 100;
        OnChangeHealth(cur_health);
        NetPreb = GameObject.Find("Network");
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (timer > 3f)
        {
            this.Damage(1.0f);
            timer = 0f;
        }else{
            timer += Time.deltaTime;
        }
    }
    public void OnClickedKnight()
    {
        CmdKnight(this.gameObject);
        Destroy(this.gameObject.transform.Find("heroSelect").gameObject);

    }
    [Command]
    void CmdKnight(GameObject a)
    {
        GameObject hero = Instantiate(heros[0], a.transform.Find("Mesh").transform, false);
        NetworkServer.Spawn(hero);
    }
    public void OnClickedArchor()
    {
        CmdArchor(this.gameObject);
        Destroy(this.gameObject.transform.Find("heroSelect").gameObject);

    }
    [Command]
    void CmdArchor(GameObject a)
    {
        GameObject hero = Instantiate(heros[1], a.transform.Find("Mesh").transform, false);
        NetworkServer.Spawn(hero);
    }
    public void OnClickedMage()
    {
        CmdMage(this.gameObject);
        Destroy(this.gameObject.transform.Find("heroSelect").gameObject);

    }
    [Command]
    void CmdMage(GameObject a)
    {
        GameObject hero = Instantiate(heros[2], a.transform.Find("Mesh").transform, false);
        NetworkServer.Spawn(hero);
    }

    public void Damage(float input) 
    {
        if (!isServer)
        {
            return;
        }
        cur_health -= input;
    }

    void OnChangeHealth(float health)
    {
        if (health < 0)
        {
            if (isLocalPlayer)
            {
                GameObject guide = Instantiate(Guide) as GameObject;
                guide.transform.SetParent(this.transform.parent, false);
                NetPreb.GetComponentInChildren<NewHUD>().manager.StopHost();
                StartCoroutine(Restart());
            }
            if (isServer)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            var myHealth = health / max_health;
            if (myHealth < 0) { myHealth = 0; }
            if (myHealth > 1) { myHealth = 1; }
            healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
    }
    IEnumerator Restart() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Main");
    }

}
