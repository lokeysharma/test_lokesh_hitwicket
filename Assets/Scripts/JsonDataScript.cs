using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class DataHitCoins
{
    public CoinAndPrice[] coin_and_price;
}

[System.Serializable]
public class DataMusky
{
    public MuskyAndPrice[] musky_and_price;
}

[System.Serializable]
public class CoinAndPrice
{
    public int product_price;
    public int quantity;
}

[System.Serializable]
public class MuskyAndPrice
{
    public int months;
    public int price;
}

public class JsonDataScript : MonoBehaviour
{

    //In game things.
    public GameObject itemPrefabM , itemPrefabH;
    public Transform parentObject;

    public Transform parentObjectHit;

    int dataIndex = 0;


    private void Start()
    {
        StartCoroutine(JsonFromUrl("https://s3-ap-southeast-1.amazonaws.com/cdn.hitwicket.co/sample_test_hitcoins.json",true));
        StartCoroutine(JsonFromUrl("https://s3-ap-southeast-1.amazonaws.com/cdn.hitwicket.co/sample_test_musky.json",false));
    }


    IEnumerator JsonFromUrl(string url,bool isHit)
    {
      //  string url = "https://s3-ap-southeast-1.amazonaws.com/cdn.hitwicket.co/sample_test_hitcoins.json";
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            if (isHit)
                ProcessjsonHIt(www.text);
            else
                Processjson(www.text);
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
        
    }


    private void ProcessjsonHIt(string jsonString)
    {
        Debug.Log(jsonString);
        DataHitCoins data1 = JsonUtility.FromJson<DataHitCoins>(jsonString);

        //putting data to game components.

        for (int i = 0; i < 50; i++)
        {
            GameObject temp = Instantiate(itemPrefabH, parentObjectHit);
            temp.transform.GetChild(1).GetComponent<Text>().text = data1.coin_and_price[i].quantity + "Coins";
            temp.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = data1.coin_and_price[i].product_price.ToString();


        }
    }


    DataMusky data = null;
    private void Processjson(string jsonString)
    {
        Debug.Log(jsonString);
        data = JsonUtility.FromJson<DataMusky>(jsonString);

        //putting data to game components.

        for (int i = 0; i < 50; i++)
        {
            GameObject temp = Instantiate(itemPrefabM, parentObject);
            temp.transform.GetChild(1).GetComponent<Text>().text = data.musky_and_price[i].months  + "Months";
            temp.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = data.musky_and_price[i].price.ToString();

        }
        dataIndex = 50;

    }

    public void ShowMoreData()
    {
        if (data.musky_and_price.Length >= dataIndex + 50)
        {

            for (int i = dataIndex; i < dataIndex + 50; i++)
            {
                GameObject temp = Instantiate(itemPrefabM, parentObject);
                temp.transform.GetChild(1).GetComponent<Text>().text = data.musky_and_price[i].months + "Months";
                temp.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = data.musky_and_price[i].price.ToString();

            }
            dataIndex += 50;
        }

        else
        {
            for (int i = dataIndex; i < data.musky_and_price.Length; i++)
            {
                GameObject temp = Instantiate(itemPrefabM, parentObject);
                temp.transform.GetChild(1).GetComponent<Text>().text = data.musky_and_price[i].months + "Months";
                temp.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = data.musky_and_price[i].price.ToString();

            }
        }
    }

    public ScrollRect scrollRect;
    bool doItOneTime = true;

    void OnEnable()
    {
        //Subscribe to the ScrollRect event
        scrollRect.onValueChanged.AddListener(scrollRectCallBack);
    }

    //Will be called when ScrollRect changes
    void scrollRectCallBack(Vector2 value)
    {
        Debug.Log("ScrollRect Changed: " + value);
        if(value.y <= 0.1f && doItOneTime)
        {
            doItOneTime = false;
            ShowMoreData();
        }
        else
        {
            doItOneTime = true;
        }
    }


}