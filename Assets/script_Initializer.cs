using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;

public class script_Initializer : MonoBehaviour
{
    public Rect screenPosition;

    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);
    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;

    public string currentuser = "none";
    public string fileloc_balances;
    public string fileloc_donations;
    public string fileloc_inventory;
    public string fileloc_transacts;

    Text semmbaltext;
    Text rainbaltext;
    Text enriqbaltext;

    public float Semmbal = 1;
    public float Rainbal = 1;
    public float Enriquebal = 1;

    void Start()
    {
        Screen.SetResolution(800, 400, false);
        //SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_BORDER);
        //bool result = SetWindowPos(GetForegroundWindow(), 0, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
        ////////////////////
        //GUI.DragWindow();

        GetFiles();
        LoadData();
    }

    void Update()
    {
        semmbaltext.text = "§" + Semmbal.ToString("0");
        rainbaltext.text = "§" + Rainbal.ToString("0");
        enriqbaltext.text = "§" + Enriquebal.ToString("0");
    }

    public void SaveData()
    {
        TransactionData data = new TransactionData();
        data.Enribal = Enriquebal;
        data.Rainbal = Rainbal;
        data.Semmbal = Semmbal;

        XmlSerializer serializer = new XmlSerializer(typeof(TransactionData));
        FileStream stream = new FileStream(fileloc_balances, FileMode.Create);
        serializer.Serialize(stream, data);
        stream.Close();
    }

    public void LoadData()
    {
        if (File.Exists(fileloc_balances))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TransactionData));
            FileStream stream = new FileStream(fileloc_balances, FileMode.Open);
            TransactionData data = serializer.Deserialize(stream) as TransactionData;

            Enriquebal = data.Enribal;
            Rainbal = data.Rainbal;
            Semmbal = data.Semmbal;

            stream.Close();

            semmbaltext = GameObject.Find("balance1").GetComponent<Text>();
            semmbaltext.text = "§" + data.Semmbal.ToString();
            rainbaltext = GameObject.Find("balance2").GetComponent<Text>();
            rainbaltext.text = "§" + data.Rainbal.ToString();
            enriqbaltext = GameObject.Find("balance3").GetComponent<Text>();
            enriqbaltext.text = "§" + data.Enribal.ToString();
        }
        else
            Console.WriteLine("Error: No user on donation");
    }

    [Serializable]
    public class TransactionData
    {
        public float Enribal;
        public float Rainbal;
        public float Semmbal;
    }

    public void setuser_melo()
    {
        currentuser = "Enrique";
    }
    public void setuser_ryan()
    {
        currentuser = "Rain";
    }
    public void setuser_sam()
    {
        currentuser = "Semm";
    }
    public void setuser_none()
    {
        currentuser = "error";
    }

    public void GetFiles()
    {
        string temp = Application.dataPath;

        fileloc_balances =  temp + @"/dgtebal.xml";
        fileloc_donations = temp + @"/dgtedo.xml";
        fileloc_inventory = temp + @"/dgteinv.xml";
        fileloc_transacts = temp + @"/dgtetrans.xml";
    }

}
