using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class teablock : MonoBehaviour
{
    public int index;
    public string brand;
    public string flavor;
    public float qty;
    public float price;
    public int itemamount;
    public bool selected;

    Text text_brand;
    Text text_flavor;
    Text text_qty;
    Text text_price;

    buybutton buyb;
    script_Initializer init;

	void Start ()
	{
        gameObject.GetComponent<Button>().onClick.AddListener(() => { buttonclick(); });
        init = GameObject.FindGameObjectWithTag("initializer").GetComponent<script_Initializer>();
        buyb = GameObject.FindGameObjectWithTag("buybutton").GetComponent<buybutton>();

        text_brand = transform.GetChild(0).GetComponent<Text>();
        text_flavor = transform.GetChild(1).GetComponent<Text>();
        text_qty = transform.GetChild(2).GetComponent<Text>();
        text_price = transform.GetChild(3).GetComponent<Text>();

        text_brand.text = brand;
        text_flavor.text = flavor;
        text_qty.text = qty.ToString();
        text_price.text = "§" + price.ToString();

        transform.SetParent(GameObject.FindGameObjectWithTag("buyparent").transform);
        transform.localScale = Vector3.one;

        if (index > 3 && itemamount > 4)
            transform.localPosition = new Vector3(960 / (itemamount - 3) * (index - 3) - 480, -90, 0);
        else if (index <= 3 && itemamount > 4)
            transform.localPosition = new Vector3(960 / (4 + 1) * (index + 1) - 480, 20, 0);
        else
            transform.localPosition = new Vector3(960 / (itemamount + 1) * (index + 1) - 480, 0, 0);

        buyb.index = -1;
	}

    void buttonclick()
    {
        teablock childcode;
        float currentbal;

        foreach (Transform child in transform.parent)
        {
            if (child.tag == "teablock")
            {
                childcode = child.GetComponent<teablock>();
                if (childcode.selected == true)
                {
                    childcode.selected = false;
                    childcode.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
            }
        }

        selected = true;

        if (init.currentuser == "Semm")
            currentbal = init.Semmbal;
        else if (init.currentuser == "Rain")
            currentbal = init.Rainbal;
        else if (init.currentuser == "Enrique")
            currentbal = init.Enriquebal;
        else
            currentbal = 0;

        if (currentbal < price)
            gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 1);
        else
        {
            gameObject.GetComponent<Image>().color = new Color(1, .9f, .45f, 1);
            buyb.index = index;
        }
    }

}