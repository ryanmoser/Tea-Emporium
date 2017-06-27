using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class buymenu : MonoBehaviour
{
    script_Initializer init;
    public GameObject moneyslide;

    public string fileloc_inv;
    public string fileloc_trans;
    public GameObject go;
    List<InventoryItem> tealist = new List<InventoryItem>();

	void Start ()
	{
	}

	void Update ()
	{
	}

    void Awake()
    {
        init = GameObject.FindGameObjectWithTag("initializer").GetComponent<script_Initializer>();
        fileloc_inv = init.fileloc_inventory;
        fileloc_trans = init.fileloc_transacts;
        loadteas();
    }

    public void loadteas()
    {
        if (File.Exists(fileloc_inv))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<InventoryItem>));
            FileStream stream = new FileStream(fileloc_inv, FileMode.Open);
            tealist = serializer.Deserialize(stream) as List<InventoryItem>;
            stream.Close();

            updateteas(tealist);
        }
    }

    public void adddonation(string brand, string flavor, int qty, float cost)
    {
        if (File.Exists(fileloc_inv))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<InventoryItem>));
            FileStream stream = new FileStream(fileloc_inv, FileMode.Open);
            tealist = serializer.Deserialize(stream) as List<InventoryItem>;
            stream.Close();

            InventoryItem teaitem = new InventoryItem();
            teaitem.brand = brand;
            teaitem.flavor = flavor;
            teaitem.qty = qty;
            teaitem.price = (int)cost*100 / qty;

            tealist.Add(teaitem);

            FileStream stream2 = new FileStream(fileloc_inv, FileMode.Create);
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<InventoryItem>));
            serializer2.Serialize(stream2, tealist);
            stream2.Close();

            updateteas(tealist);
        }
    }

    public void teabought(int index)
    {
        subtractmoney(init.currentuser, tealist[index].price);
        savetransaction(tealist[index]);

        tealist[index].qty--;
        if (tealist[index].qty == 0)
            tealist.RemoveAt(index);

        FileStream stream = new FileStream(fileloc_inv, FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(List<InventoryItem>));
        serializer.Serialize(stream, tealist);
        stream.Close();
       
        updateteas(tealist);
        GameObject.FindGameObjectWithTag("userparent").GetComponent<users>().activate(init.currentuser);

    }

    public void updateteas(List<InventoryItem> tealist)
    {
        for (int i = transform.childCount-1; i >= 0; i--)
        {
            if (transform.GetChild(i).gameObject.tag == "teablock")
                Destroy(transform.GetChild(i).gameObject);
        }
        
        for (int i=0; i<tealist.Count; i++)
        {
            GameObject teab = Instantiate(go) as GameObject;
            teablock sc = teab.GetComponent<teablock>();
            sc.index = i;
            sc.brand = tealist[i].brand;
            sc.flavor = tealist[i].flavor;
            sc.qty = tealist[i].qty;
            sc.price = tealist[i].price;
            sc.itemamount = tealist.Count;
        }

        updaterecents();
    }

    public void updaterecents()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<TransactionItem>));
        FileStream stream = new FileStream(fileloc_trans, FileMode.Open);
        List<TransactionItem> transs = new List<TransactionItem>();
        transs = serializer.Deserialize(stream) as List<TransactionItem>;
        stream.Close();

        GameObject recents = GameObject.FindGameObjectWithTag("recentstext");
        recents.gameObject.GetComponent<Text>().text = transs[0].buyer + " " + transs[0].date + " " + transs[0].brand + " " + transs[0].flavor + "\n" +
                                                       transs[1].buyer + " " + transs[1].date + " " + transs[1].brand + " " + transs[1].flavor + "\n" +
                                                       transs[2].buyer + " " + transs[2].date + " " + transs[2].brand + " " + transs[2].flavor + "\n"; ;
    }

    public void subtractmoney(string user, float money)
    {
        GameObject ins = Instantiate(moneyslide) as GameObject;
        ins.GetComponent<moneychange>().subtractmoney(money);

        switch (user)
        {
            case "Enrique":
                init.Enriquebal -= money;
                ins.transform.position = GameObject.Find("balance3").transform.position;
                ins.transform.position = new Vector3(ins.transform.position.x + .8f, ins.transform.position.y, ins.transform.position.z);
                break;
            case "Rain":
                init.Rainbal -= money;
                ins.transform.position = GameObject.Find("balance2").transform.position;
                ins.transform.position = new Vector3(ins.transform.position.x + .8f, ins.transform.position.y, ins.transform.position.z);
                break;
            case "Semm":
                init.Semmbal -= money;
                ins.transform.position = GameObject.Find("balance1").transform.position;
                ins.transform.position = new Vector3(ins.transform.position.x + .8f, ins.transform.position.y, ins.transform.position.z);
                break;
            default:
                Debug.Log("Error: No user on donation");
                break;
        }

        init.SaveData();
    }

    public void savetransaction(InventoryItem teab)
    {
        List<TransactionItem> transactions = new List<TransactionItem>();

        if (File.Exists(fileloc_trans))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TransactionItem>));
            FileStream stream = new FileStream(fileloc_trans, FileMode.Open);
            transactions = serializer.Deserialize(stream) as List<TransactionItem>;
            stream.Close();

            TransactionItem trans = new TransactionItem();
            trans.buyer = init.currentuser;
            trans.date = DateTime.Now.ToString("MM/dd/yy HH:mm");
            trans.brand = teab.brand;
            trans.flavor = teab.flavor;
            trans.price = teab.price;

            transactions.Insert(0, trans);

            FileStream stream2 = new FileStream(fileloc_trans, FileMode.Create);
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<TransactionItem>));
            serializer2.Serialize(stream2, transactions);
            stream2.Close();
        }
        else 
        {
            Debug.Log("########## Transaction not saved. ###########");
        }
    }

    public class InventoryItem
    {
        public string brand;
        public string flavor;
        public int qty;
        public int price;
    }

    public class TransactionItem
    {
        public string buyer;
        public string date;
        public string brand;
        public string flavor;
        public int price;
    }

}