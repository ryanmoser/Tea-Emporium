using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class donationsbutton : MonoBehaviour
{
    elderhandler elderhandler;

    void Start()
    {
        elderhandler = gameObject.GetComponentInParent<elderhandler>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => { buttonclick(); });
    }

    void Update()
    {

    }

    void buttonclick()
    {
        elderhandler.donationsbuttonclicked();
    }
}
