using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class moneychange : MonoBehaviour
{
    float decel = 1f;
    Text text;

	void Start ()
	{
        transform.SetParent(GameObject.Find("Panel").transform);
        transform.localScale = new Vector3(1, 1, 1);
        Destroy(gameObject, 2);
	}

	void FixedUpdate ()
	{
        decel = decel * .99f;
        transform.Translate(Vector3.right * Time.deltaTime * decel);
	}

    public void addmoney(float money)
    {
        text = gameObject.GetComponent<Text>();
        text.color = Color.green;
        text.text = "+" + money.ToString();
    }

    public void subtractmoney(float money)
    {
        text = gameObject.GetComponent<Text>();
        text.color = Color.red;
        text.text = "-" + money.ToString();
    }

}