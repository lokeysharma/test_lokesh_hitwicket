using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


#region Model Classes

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

#endregion

public class JsonDataScript : MonoBehaviour
{

    //In game things.
    public GameObject itemPrefabM , itemPrefabH;
    public Transform parentObjectMuskey;
    public Transform parentObjectHit;

    public ScrollRect scrollRect;
    private bool doItOneTime = true;
    private DataMusky data = null;
    private int dataIndex = 0;

    void OnEnable()
    {
        //Subscribe to the ScrollRect event
        scrollRect.onValueChanged.AddListener(scrollRectCallBack);
    }

    private void Start()
    {
        StartCoroutine(JsonFromUrl("https://s3-ap-southeast-1.amazonaws.com/cdn.hitwicket.co/sample_test_hitcoins.json",true));
        StartCoroutine(JsonFromUrl("https://s3-ap-southeast-1.amazonaws.com/cdn.hitwicket.co/sample_test_musky.json",false));
    }

    IEnumerator JsonFromUrl(string url,bool isHit)
    {
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
        DataHitCoins data1 = JsonUtility.FromJson<DataHitCoins>(jsonString);

        //putting data to game components.

        for (int i = 0; i < data1.coin_and_price.Length; i++)
        {
            GameObject temp = Instantiate(itemPrefabH, parentObjectHit);
            temp.transform.GetChild(1).GetComponent<Text>().text = data1.coin_and_price[i].quantity + "Coins";
            temp.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = data1.coin_and_price[i].product_price.ToString();
        }
    }

    private void Processjson(string jsonString)
    {
        data = JsonUtility.FromJson<DataMusky>(jsonString);

        //putting data to game components.

        for (int i = 0; i < 20; i++)
        {
            GameObject temp = Instantiate(itemPrefabM, parentObjectMuskey);
            temp.transform.GetChild(1).GetComponent<Text>().text = data.musky_and_price[i].months  + "Months";
            temp.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = data.musky_and_price[i].price.ToString();

        }
        dataIndex = 20;

    }

    public void ShowMoreData()
    {
        if (data.musky_and_price.Length >= dataIndex + 20)
        {

            for (int i = dataIndex; i < dataIndex + 20; i++)
            {
                GameObject temp = Instantiate(itemPrefabM, parentObjectMuskey);
                temp.transform.GetChild(1).GetComponent<Text>().text = data.musky_and_price[i].months + "Months";
                temp.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = data.musky_and_price[i].price.ToString();

            }
            dataIndex += 20;
        }

        else
        {
            for (int i = dataIndex; i < data.musky_and_price.Length; i++)
            {
                GameObject temp = Instantiate(itemPrefabM, parentObjectMuskey);
                temp.transform.GetChild(1).GetComponent<Text>().text = data.musky_and_price[i].months + "Months";
                temp.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = data.musky_and_price[i].price.ToString();

            }

            scrollRect.onValueChanged.RemoveAllListeners();
        }
    }

    void scrollRectCallBack(Vector2 value)
    {
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