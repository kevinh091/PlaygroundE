using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public GameObject back;
	void Start () {
        for (int i = 0; i < 8; i++) {
            for(int k = 0; k < 8; k++)
            {
                GameObject newBack = Instantiate(back, this.transform, true);
                newBack.transform.position = new Vector3(100 * i + transform.position.x, 100 * k + transform.position.y, 0);
                newBack.transform.localScale =new Vector3(1,1,1);
            }
        }
	}
}
