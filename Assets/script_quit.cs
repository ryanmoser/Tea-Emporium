using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class script_quit : MonoBehaviour 
{

	void Start () 
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => { prebuttonclick(); });	
	}
	
	void Update () 
    {
	
	}

    void prebuttonclick()
    {
        Invoke("buttonclick", .1f);
    }

    void buttonclick()
    {
        Application.Quit();
    }
}