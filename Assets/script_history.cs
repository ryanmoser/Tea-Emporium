using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class script_history : MonoBehaviour
{

    script_Initializer init;
    public string fileloc_trans;
    List<TransactionItem> translist = new List<TransactionItem>();
    public int index = 99;
    bool en = true;

    void Start ()
    {
        if (name == "button_recents")
            gameObject.GetComponent<Button>().onClick.AddListener(() => { buttonclick(); });
    }

    void Awake()
    {
        init = GameObject.FindGameObjectWithTag("initializer").GetComponent<script_Initializer>();
        fileloc_trans = init.fileloc_transacts;

        if (name == "recent1" || name == "recent2" || name == "recent3")
            gethistory();
    }

	void Update ()
	{

	}

    public void gethistory()
    {
        if (name == "recent1")
        {
            index = 0;
        }
        else if (name == "recent2")
        {
            index = 1;
        }
        else if (name == "recent3")
        {
            index = 2;
        }
        else
            index = 99;

        if (File.Exists(fileloc_trans))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TransactionItem>));
            FileStream stream = new FileStream(fileloc_trans, FileMode.Open);
            translist = serializer.Deserialize(stream) as List<TransactionItem>;
            stream.Close();
        }

        GetComponent<Text>().text = translist[index].date + " " + translist[index].buyer + " " + translist[index].brand + " " + translist[index].flavor;

    }

    void buttonclick()
    {
        if (en == true)
        {
            for (int j = 1; j <= 3; j++)
            {
                transform.GetChild(j).gameObject.SetActive(true);
                transform.GetChild(j).GetComponent<script_history>().gethistory();
            }
            en = false;
        }
        else
        {
            for (int j = 1; j <= 3; j++)
            {
                transform.GetChild(j).gameObject.SetActive(false);
            }
            en = true;
        }
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