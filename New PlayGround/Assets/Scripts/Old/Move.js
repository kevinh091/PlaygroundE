#pragma strict

public var Speed : float;
private var Target : Vector3;

function Update () {
	if (Input.GetMouseButtonDown(1))
        {
            Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Target.z = transform.position.z;
        }

        transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
}
