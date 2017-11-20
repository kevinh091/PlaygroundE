using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deleteCanvas : MonoBehaviour
{
    public void OnClicked()
    {
        Destroy(GameObject.Find("LostMessege(Clone)"));

    }
}
