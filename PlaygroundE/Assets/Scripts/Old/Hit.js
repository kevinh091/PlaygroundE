#pragma strict


    var damage : int;
    var Speed : float = 1;
    var Target : Vector3;
    var self : GameObject;

    function Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
    }

    function OnTriggerEnter(other: Collider)
    {
        if (other.gameObject != self && other.gameObject.tag == "Player")
        {
            var target = other.gameObject.GetComponent.<Stats>();
            target.cur_health -= damage;
            target.setHealthBar();
        }
    }
