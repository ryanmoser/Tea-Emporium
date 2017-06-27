using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class users : MonoBehaviour
{
    Text txt;
    bool grow = false;
    bool shrink = false;
    float scalerate = .05f;
    float scalemax = 1.25f;
    float scalemin = 1;

	void Start ()
	{
	}

	void FixedUpdate ()
	{
        if (grow == true)
        {
            txt.transform.localScale += Vector3.one * scalerate;

            if (txt.transform.localScale.x >= scalemax)
            {
                shrink = true;
                grow = false;
            }
        }
        else if (shrink == true)
        {
            txt.transform.localScale -= Vector3.one * scalerate;

            if (txt.transform.localScale.x <= scalemin)
            {
                shrink = false;
            }
        }
	}

    public void activate(string user)
    {
        if (user == "Semm")
            txt = transform.GetChild(3).GetComponent<Text>();
        else if (user == "Rain")
            txt = transform.GetChild(4).GetComponent<Text>();
        else if (user == "Enrique")
            txt = transform.GetChild(5).GetComponent<Text>();

        grow = true;
    }

}