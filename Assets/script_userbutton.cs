using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class script_userbutton : MonoBehaviour 
{
    elderhandler elderhandler;
    script_Initializer init;
    float scalerate = .80f;
    float scalemax = 25f;
    float scalemin = .03f;
    float alpha = 1;
    float alphascale = .91f;
    public bool grow = false;
    public bool shrink = false;
    Button button;
    Image image;
    Text text;

	void Start () 
    {
        elderhandler = gameObject.GetComponentInParent<elderhandler>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => { prebuttonclick(); });
        button = gameObject.GetComponent<Button>();
        image = button.GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();
    }
	
	void FixedUpdate () 
    {
        if (grow == true)
        {
            transform.localScale += Vector3.one * scalerate;
            alpha *= alphascale;
            image.color = new Color(1, 1, 1, alpha);

            if (transform.localScale.x > scalemax)
            {
                transform.localScale = Vector3.one;
                image.color = new Color(1, 1, 1, 1);
                alpha = 1;
                button.interactable = true;
                grow = false;
                transform.SetParent(GameObject.FindWithTag("homeparent").transform);

                elderhandler.userbuttonclicked();
            }
        }
        else if (shrink == true)
        {
            button.interactable = false;
            alpha *= alphascale;
            image.color = new Color(1, 1, 1, alpha);
            text.color = new Color(0, 0, 0, alpha);

            if (alpha < scalemin)
            {
                image.color = new Color(1, 1, 1, 1);
                text.color = new Color(0, 0, 0, 1);
                alpha = 1;
                button.interactable = true;

                shrink = false;
            }
        }
    }

    void prebuttonclick()
    {
        Invoke("buttonclick", .05f);
    }

    void buttonclick()
    {
        transform.SetParent(transform.parent.parent);
        elderhandler.userbuttonclickedpre();

        grow = true;
        button.interactable = false;

        foreach (Transform child in transform.parent)
        {
            if (child.gameObject.tag == "buyparent" || child.gameObject.tag == "!home")
                child.gameObject.SetActive(true);
        }
        GameObject.FindGameObjectWithTag("buyparent").GetComponent<buymenu>().loadteas();
    }

    public void shrinken()
    {
        shrink = true;
    }


}
