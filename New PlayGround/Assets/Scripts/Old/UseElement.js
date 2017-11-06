#pragma strict
import System.Collections.Generic;

    var Elements :List.<GameObject> ;
    var Mats :List.<Material>;
    private var next:int= 0;
    var ele: List.<int> ; // what elements player is holding;  0=none 1WBlue 2ARed 3SYellow 4DEarth
    var EEEE: GameObject;

    function clear()
    {
        Elements[0].transform.Find("Mesh").GetComponent.<Renderer>().material = Mats[0];
        Elements[1].transform.Find("Mesh").GetComponent.<Renderer>().material = Mats[0];
        Elements[2].transform.Find("Mesh").GetComponent.<Renderer>().material = Mats[0];
        Elements[3].transform.Find("Mesh").GetComponent.<Renderer>().material = Mats[0];
        ele[0] = 0;
        ele[1] = 0;
        ele[2] = 0;
        ele[3] = 0;
        next = 0;
    }

    function Start()
    {
        clear();
    }

    function Update()
    {
        if (Input.GetMouseButtonDown(0)) // cast eles
        {
            if(ele[0]==1 && ele[1]==1 && ele[2] == 1 && ele[3] == 1)
            {
                this.GetComponent.<Stats>().cur_health += 10;
                this.GetComponent.<Stats>().setHealthBar();
            }
            if (ele[0] == 4 && ele[1] == 4 && ele[2] == 4 && ele[3] == 4)  // bullet
            {
                var bullet:GameObject = Instantiate(EEEE, this.transform.position, Quaternion.identity);
                bullet.GetComponent.<Hit>().self = this.gameObject;
                bullet.GetComponent.<Hit>().Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                bullet.GetComponent.<Hit>().Target.z = transform.position.z;
                Destroy(bullet, 3.0f);
            }
            clear();
        }

        if (Input.GetButtonDown("Shift")) //clear eles
        {
            clear();
        }
        if (Input.GetButtonDown("W"))
        {
            if (next < 4)
            {
                Elements[next].transform.Find("Mesh").GetComponent.<Renderer>().material = Mats[1];
                ele[next] = 1;
                next++;
            }
        }
        if (Input.GetButtonDown("A"))
        {
            if (next < 4)
            {
                Elements[next].transform.Find("Mesh").GetComponent.<Renderer>().material = Mats[2];
                ele[next] = 2;
                next++;
            }

        }
        if (Input.GetButtonDown("S"))
        {
            if (next < 4)
            {
                Elements[next].transform.Find("Mesh").GetComponent.<Renderer>().material = Mats[3];
                ele[next] = 3;
                next++;
            }
        }
        if (Input.GetButtonDown("D"))
        {
            if (next < 4)
            {
                Elements[next].transform.Find("Mesh").GetComponent.<Renderer>().material = Mats[4];
                ele[next] = 4;
                next++;
            }
        }
    }