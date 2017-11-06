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
    public float EEEESpeed;
    public List<Vector3> elementPosition;
    private bool viewingMap;
    private bool viewingP;
    public int rotation = 0;
    public GameObject explosion;
    public GameObject Map;
    public GameObject SkillBook;
    public GameObject Ept;

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
        viewingMap = false;
        viewingP = false;
        clear();
        if (isLocalPlayer)
        {
            gameObject.transform.Find("Mesh").GetComponent<Renderer>().material = Mats[1];
            Instantiate(mainCan, this.transform, false);
        }
        if (!isLocalPlayer)
        {
            Destroy(gameObject.transform.Find("Cam").gameObject);
            Destroy(this.gameObject.transform.Find("heroSelect").gameObject);
        }
        recEle('W', 10);
        recEle('A', 10);
        recEle('S', 10);
        recEle('D', 10);
        recEle('Q', 0);

        elementPosition[0] = new Vector3(0,-1,0);
        elementPosition[1] = new Vector3(1, 0, 0);
        elementPosition[2] = new Vector3(0, 1, 0);
        elementPosition[3] = new Vector3(-1, 0, 0);
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
        bullet.GetComponent<Hit>().owner = this.gameObject;
        a.z = 0;
        float temp = Mathf.Sqrt((a.x * a.x) + (a.y * a.y));
        bullet.GetComponent<Rigidbody>().velocity = new Vector3(a.x / temp, a.y / temp, 0) * EEEESpeed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 3.0f);
    }
    [Command]
    void CmdWWWW()
    {
        this.GetComponent<Stats>().Damage(-20f);
    }
    [Command]
    void CmdAAAA()
    {
    }
    //[Command]  // local
    IEnumerator SSSS()     // 4 thunders: speed boost 5 seconds
    {
        this.GetComponent<Move>().Speed = 10f;
        yield return new WaitForSeconds(5);
        this.GetComponent<Move>().Speed = 5f;
    }
    [Command]
    void CmdAAAD(Vector3 location) {
        GameObject AAAD = Instantiate(explosion, location, Quaternion.identity);
        //AAAD.GetComponent<explosion>().owner = AAAD;
        NetworkServer.Spawn(AAAD);
        Destroy(AAAD, 1.0f);
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
        if (Elements[0].transform.localPosition.x == 0 && Elements[0].transform.localPosition.y == -1) {
            rotation = 0;
        }
        else if(Elements[0].transform.localPosition.x == 1 && Elements[0].transform.localPosition.y == 0)
            {
                rotation = 1;
            }
        else if (Elements[0].transform.localPosition.x == 0 && Elements[0].transform.localPosition.y == 1)
        {
            rotation = 2;
        }
        else if (Elements[0].transform.localPosition.x == -1 && Elements[0].transform.localPosition.y == 0)
        {
            rotation = 3;
        }
        Elements[0].transform.localPosition = Vector3.MoveTowards(Elements[0].transform.localPosition, elementPosition[(rotation+1) % 4], (1f) * Time.deltaTime);
        Elements[1].transform.localPosition = Vector3.MoveTowards(Elements[1].transform.localPosition, elementPosition[(rotation+2) % 4], (1f) * Time.deltaTime);
        Elements[2].transform.localPosition = Vector3.MoveTowards(Elements[2].transform.localPosition, elementPosition[(rotation+3) % 4], (1f) * Time.deltaTime);
        Elements[3].transform.localPosition = Vector3.MoveTowards(Elements[3].transform.localPosition, elementPosition[(rotation+4) % 4], (1f) * Time.deltaTime);

        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetButtonDown("P"))   // view skill book
        {
            if (viewingP == false)
            {
                GameObject viewP = Instantiate(SkillBook) as GameObject;
                viewP.transform.SetParent(this.transform, false);
                viewingP = true;
            }
            else
            {
                Destroy(this.transform.Find("SkillBook(Clone)").gameObject);
                viewingP = false;
            }

        }
        if (Input.GetButtonDown("M")) {
            if (viewingMap == false)
            {
                GameObject viewMap = Instantiate(Map) as GameObject;
                viewMap.transform.SetParent(this.transform, false);
                viewingMap = true;
            }
            else {
                Destroy(this.transform.Find("Map(Clone)").gameObject);
                viewingMap = false;
            }

        }
        if (Input.GetMouseButtonDown(0)) // cast eles
        {
            int[] num = new int[6]; //  n_emp, n_w, n_a, n_s, n_d, n_q;
            for(int k=0; k < 4; k++)    //num[] count how many of the specific element is casted 
            {
                num[ele[k]] += 1; 
            }  /////////////////////////////////////////
            if (num[1] ==4)  //WWWW
            {
                CmdWWWW();
            }
            if (num[2] == 4) //AAAA
            {
                CmdAAAA();
            }
            if (num[3] == 4) //SSSS
            {
                StartCoroutine(SSSS());
            }
            if (num[0] ==4)  // bullet, empty left click
            {
                Vector3 a = Input.mousePosition - Camera.main.WorldToScreenPoint(this.transform.position);
                CmdEpt(a);
            }
            if (num[1]==1 && num[2] == 1 &&num[3] == 1 && num[4] == 1 )  //WASD to craft Q
            {
                recEle('Q', 1);
            }
            if (num[2] == 3 &&num[4]==1) {  //3 fire(A), 1 earth(D)
                Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                location.z = -1;
                CmdAAAD(location);
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
