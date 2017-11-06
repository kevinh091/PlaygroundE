#pragma strict


    var max_health : float;
    var cur_health : float;
    var healthBar: GameObject;

    function Start()
    {
        setHealthBar();
    }

    function Update()
    {
        cur_health -= 10f * Time.deltaTime;
        setHealthBar();
        if(cur_health < 1)
        {
            Destroy(this.gameObject);
        }
    }

    function dec(input : float)
    {
        cur_health -= input;
    }

    function inc(input : float)
    {
        cur_health += input;
    }

    function setHealthBar()
    {
        var myHealth = cur_health / max_health;
        if (myHealth < 0) { myHealth = 0; }
        if (myHealth > 1) { myHealth = 1; }
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

