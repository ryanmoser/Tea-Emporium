using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class clickcatcher : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("buybutton").GetComponent<buybutton>().index = -1;
    }
}