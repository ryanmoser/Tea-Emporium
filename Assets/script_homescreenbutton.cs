using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class script_homescreenbutton : MonoBehaviour 
{
    elderhandler elderhandler;
    script_Initializer init;
    float scalerate = 2f;
    float scalemax = 70f;
    float alpha = 1;
    float alphascale = .9f;
    public bool grow = false;
    Button button;
    Image image;

    void Start()
    {
        elderhandler = gameObject.GetComponentInParent<elderhandler>();
        init = GameObject.FindGameObjectWithTag("initializer").GetComponent<script_Initializer>();
        button = gameObject.GetComponent<Button>();
        image = button.GetComponent<Image>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => { prebuttonclick(); });
    }

    void Update()
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

                if (init.currentuser == "none")
                    transform.parent.gameObject.SetActive(false);
            }
        }
    }

    void prebuttonclick()
    {
        Invoke("buttonclick", .05f);
    }

    void buttonclick()
    {
        elderhandler.homebuttonclicked();
        grow = true;
        button.interactable = false;
    }

}
