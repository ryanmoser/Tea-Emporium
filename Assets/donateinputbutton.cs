using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class donateinputbutton : MonoBehaviour
{
    elderhandler elderhandler;

    InputField brandfield;
    InputField flavorfield;
    InputField qtyfield;
    InputField costfield;
    script_Initializer init;
    buymenu inventoryscript;

    string fileloc_donations;
    public GameObject moneyslide;

	void Start ()
	{
        init = GameObject.FindGameObjectWithTag("initializer").GetComponent<script_Initializer>();

        fileloc_donations = init.fileloc_donations;

        gameObject.GetComponent<Button>().onClick.AddListener(() => { buttonclick(); });
        elderhandler = gameObject.GetComponentInParent<elderhandler>();

        brandfield = GameObject.Find("InputField_brand").GetComponent<InputField>();
        flavorfield = GameObject.Find("InputField_flavor").GetComponent<InputField>();
        qtyfield = GameObject.Find("InputField_qty").GetComponent<InputField>();
        costfield = GameObject.Find("InputField_cost").GetComponent<InputField>();
    }

	void Update ()
	{
        if (brandfield.text != "" && flavorfield.text != "" && qtyfield.text != "" && costfield.text != "")
            gameObject.GetComponent<Button>().interactable = true;
	}

    void buttonclick()
    {
        List<Donation> dtlist = new List<Donation>();
        elderhandler.donateinputbuttonclicked();
        inventoryscript = GameObject.FindGameObjectWithTag("buyparent").GetComponent<buymenu>();

        if (File.Exists(fileloc_donations))
        {
            string file = fileloc_donations;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Donation>));
            FileStream stream = new FileStream(file, FileMode.Open);
            dtlist = serializer.Deserialize(stream) as List<Donation>;
            stream.Close();

            Donation dt = new Donation();
            dt.donator = init.currentuser;
            dt.brand = brandfield.text;
            dt.flavor = flavorfield.text;
            dt.qty = int.Parse(qtyfield.text);
            dt.cost = float.Parse(costfield.text);
            dt.date = DateTime.Now.ToString("MM/dd/yy HH:mm");
            dtlist.Insert(0,dt);

            addmoney(init.currentuser, dt.cost);
            inventoryscript.adddonation(dt.brand, dt.flavor, dt.qty, dt.cost);

            FileStream stream2 = new FileStream(file, FileMode.Create);
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<Donation>));
            serializer2.Serialize(stream2, dtlist);
            stream2.Close();

            brandfield.text = "";
            flavorfield.text = "";
            qtyfield.text = "";
            costfield.text = "";
        }
    }

    [Serializable]
    public class Donation
    {
        public string donator;
        public string brand;
        public string flavor;
        public int qty;
        public float cost;
        public string date;
    }

    public void addmoney(string user, float money)
    {
        money *= 100;

        GameObject ins = Instantiate(moneyslide) as GameObject;
        ins.GetComponent<moneychange>().addmoney(money);

        switch (user)
        {
            case "Enrique":
                init.Enriquebal += money;
                ins.transform.position = GameObject.Find("balance3").transform.position;
                ins.transform.position = new Vector3(ins.transform.position.x + .8f, ins.transform.position.y, ins.transform.position.z);
                break;
            case "Rain":
                init.Rainbal += money;
                ins.transform.position = GameObject.Find("balance2").transform.position;
                ins.transform.position = new Vector3(ins.transform.position.x + .8f, ins.transform.position.y, ins.transform.position.z);
                break;
            case "Semm":
                init.Semmbal += money;
                ins.transform.position = GameObject.Find("balance1").transform.position;
                ins.transform.position = new Vector3(ins.transform.position.x + .8f, ins.transform.position.y, ins.transform.position.z);
                break;
            default:
                Debug.Log("Error: No user on donation");
                break;
        }

        init.SaveData();
    }


}