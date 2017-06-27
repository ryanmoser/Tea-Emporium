using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buybutton : MonoBehaviour
{
    public int index = -1;
    Button butt;

	void Start ()
	{
        butt = gameObject.GetComponent<Button>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => { buttonclick(); });
	}

	void Update ()
	{
        if (index >= 0 && butt.interactable == false)
        {
            butt.interactable = true;
            transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0, 1);
        }
        else if (index < 0 && butt.interactable == true)
        {
            butt.interactable = false;
            transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0, 0f);
        }
	}

    void buttonclick()
    {
        buymenu dib = transform.parent.GetComponent<buymenu>();
        dib.teabought(index);

        index = -1;
        butt.interactable = false;
        transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0, 0f);
    }

}