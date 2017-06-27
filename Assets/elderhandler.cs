using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class elderhandler : MonoBehaviour
{
	void Start ()
	{
	}

	void Update ()
	{
	}

    public void homebuttonclicked()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag != "homeparent" && child.gameObject.tag != "!home")
                child.gameObject.SetActive(false);
            else
                child.gameObject.SetActive(true);
        }
    }

    public void donationsbuttonclicked()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "donatewindowparent" || child.gameObject.tag == "!home")
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);
        }
    }

    public void userbuttonclickedpre()
    {
        foreach (Transform child in GameObject.FindWithTag("homeparent").transform)
        {
            child.gameObject.GetComponent<script_userbutton>().shrinken();
        }
    }

    public void userbuttonclicked()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "homeparent")
                child.gameObject.SetActive(false);
        }
    }

    public void donateinputbuttonclicked()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "buyparent" || child.gameObject.tag == "!home")
                child.gameObject.SetActive(true);
            else if (child.gameObject.tag == "donatewindowparent")
                child.gameObject.SetActive(false);
        }
    }



}