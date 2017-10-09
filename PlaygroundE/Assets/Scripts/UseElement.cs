using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UseElement : NetworkBehaviour {

    public List<GameObject> Elements;
    [SyncVar] public int n;
    [SyncVar] public string s;
    [SyncVar(hook = "OnChangeEle")] public bool changed;
    public int numOfW;
    public int numOfA; 
    public int numOfS;
    public int numOfD;
    public int numOfQ;
    public Canvas mainCan;
    public List<Material> Mats;
    public int next= 0;
    public List<int> ele; // what elements player is holding;  0=none 1WBlue 2ARed 3SYellow 4DEarth 5QViod
    public GameObject Ept;
    public float EEEESpeed;


    public void recEle(char ele, int num)
    {
        char e = ele;
        switch (e)
        {
            case 'W':
                numOfW += num;
                break;
            case 'A':
                numOfA += num;
                break;
            case 'S':
                numOfS += num;
                break;
            case 'D':
                numOfD += num;
                break;
            case 'Q':
                numOfQ += num;
                break;
        }
        gameObject.GetComponentInChildren<SetText>().text1.text = numOfW.ToString();
        gameObject.GetComponentInChildren<SetText>().text2.text = numOfA.ToString();
        gameObject.GetComponentInChildren<SetText>().text3.text = numOfS.ToString();
        gameObject.GetComponentInChildren<SetText>().text4.text = numOfD.ToString();
        gameObject.GetComponentInChildren<SetText>().text5.text = numOfQ.ToString();
    }

    void clear()
    {
        Elements[0].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[0];
        Elements[1].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[0];
        Elements[2].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[0];
        Elements[3].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[0];
        ele[0] = 0;
        ele[1] = 0;
        ele[2] = 0;
        ele[3] = 0;
        next = 0;
    }

    void Start()
    {
        clear();
        if (isLocalPlayer)
        {
            gameObject.transform.Find("Mesh").GetComponent<Renderer>().material = Mats[1];
            Instantiate(mainCan, this.transform, false);
        }
        if (!isLocalPlayer)
        {
            Destroy(gameObject.transform.Find("Cam").gameObject);
        }
        recEle('W', 10);
        recEle('A', 10);
        recEle('S', 10);
        recEle('D', 10);
        recEle('Q', 0);
    }
    //

    void OnChangeEle(bool what)
    {
        if(s == "Clear")
        {
            Elements[0].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[0];
            Elements[1].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[0];
            Elements[2].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[0];
            Elements[3].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[0];
        }
        if (s == "W")
        {
            Elements[n].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[1];
        }
        if (s == "A")
        {
            Elements[n].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[2];
        }
        if (s == "S")
        {
            Elements[n].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[3];
        }
        if (s == "D")
        {
            Elements[n].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[4];
        }
        if (s == "Q")
        {
            Elements[n].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[5];
        }
    }


    [Command]
    void CmdEpt(Vector3 a)
    {
        GameObject bullet = Instantiate(Ept, this.transform.position, Quaternion.identity);
        a.z = 0;
        float temp = Mathf.Sqrt((a.x * a.x) + (a.y * a.y));
        bullet.GetComponent<Rigidbody>().velocity = new Vector3(a.x / temp, a.y / temp, 0) * EEEESpeed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 3.0f);
    }
    [Command]
    void CmdWWWW()
    {
        this.GetComponent<Stats>().Damage(-10f);
    }
    [Command]
    void CmdButton(string input, int next)
    {
        n = next;
        s = input;
        changed = !changed;
    }



    //
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0)) // cast eles
        {
            int[] num = new int[6]; //  n_emp, n_w, n_a, n_s, n_d, n_q;
            for(int k=0; k < 4; k++)
            {
                num[ele[k]] += 1; 
            }  /////////////////////////////////////////
            if (num[1] ==4)
            {
                CmdWWWW();
            } 
            if (num[0] ==4)  // bullet
            {
                Vector3 a = Input.mousePosition - Camera.main.WorldToScreenPoint(this.transform.position);
                CmdEpt(a);
            }
            if (num[1]==1 && num[2] == 1 &&num[3] == 1 && num[4] == 1 )
            {
                recEle('Q', 1);
            }
            clear();
            CmdButton("Clear", next);
        }

        if (Input.GetButtonDown("Shift")) //clear eles
        {
            for (int i = 0; i < 4; i++)
            {
                int e = ele[i];
                switch (e)
                {
                    case 0:
                        break;
                    case 1:
                        recEle('W', 1);
                        break;
                    case 2:
                        recEle('A', 1);
                        break;
                    case 3:
                        recEle('S', 1);
                        break;
                    case 4:
                        recEle('D', 1);
                        break;
                    case 5:
                        recEle('Q', 1);
                        break;
                }
            }
            clear();
            CmdButton("Clear", next);
        }
        if (Input.GetButtonDown("W"))
        {
            if (next < 4)
            {
                if(numOfW < 1)
                {
                    return;
                }
                recEle('W', -1);
                Elements[next].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[1];
                ele[next] = 1;
                CmdButton("W", next);
                next++;
            }
        }
        if (Input.GetButtonDown("A"))
        {
            if (next < 4)
            {
                if (numOfA < 1)
                {
                    return;
                }
                recEle('A', -1);
                Elements[next].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[2];
                ele[next] = 2;
                CmdButton("A", next);
                next++;
            }

        }
        if (Input.GetButtonDown("S"))
        {
            if (next < 4)
            {
                if (numOfS < 1)
                {
                    return;
                }
                recEle('S', -1);
                Elements[next].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[3];
                ele[next] = 3;
                CmdButton("S", next);
                next++;
            }
        }
        if (Input.GetButtonDown("D"))
        {
            if (next < 4)
            {
                if (numOfD < 1)
                {
                    return;
                }
                recEle('D', -1);
                Elements[next].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[4];
                ele[next] = 4;
                CmdButton("D", next);
                next++;
            }
        }
        if (Input.GetButtonDown("Q"))
        {
            if (next < 4)
            {
                if (numOfQ < 1)
                {
                    return;
                }
                recEle('Q', -1);
                Elements[next].transform.Find("Mesh").GetComponent<Renderer>().material = Mats[5];
                ele[next] = 5;
                CmdButton("Q", next);
                next++;
            }
        }
    }
}
