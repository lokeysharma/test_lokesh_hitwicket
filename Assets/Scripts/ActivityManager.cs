using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityManager : MonoBehaviour
{

    private Tabs m_currentOpenTab = Tabs.special;

    public GameObject specialTab, hitcoinTab, musketeerTab;
    public Button specialbtn, hitcoinBtn, musketeerBtn;

    public Sprite selectedSprite, unSelectedSprite;

    private void Start()
    {
        specialbtn.onClick.AddListener(() => onBtnSpecialTab());
        hitcoinBtn.onClick.AddListener(() => onBtnHitCoinTab());
        musketeerBtn.onClick.AddListener(() => onBtnMusketeerTab());
    }


    #region ButtonEvnts

    public void onBtnSpecialTab()
    {
        OnChangingTab(Tabs.special);
    }

    public void onBtnHitCoinTab()
    {
        OnChangingTab(Tabs.hitcoins);
    }

    public void onBtnMusketeerTab()
    {
        OnChangingTab(Tabs.musketeer);
    }

    #endregion


    private void OnChangingTab(Tabs tab)
    {
        switch (tab)
        {
            case Tabs.special:
                {
                    specialTab.SetActive(true);
                    hitcoinTab.SetActive(false);
                    musketeerTab.SetActive(false);
                    m_currentOpenTab = Tabs.special;
                    specialbtn.GetComponent<Image>().sprite = selectedSprite;
                    hitcoinBtn.GetComponent<Image>().sprite = unSelectedSprite;
                    musketeerBtn.GetComponent<Image>().sprite = unSelectedSprite;

                }
                break;
            case Tabs.hitcoins:
                {
                    hitcoinTab.SetActive(true);
                    specialTab.SetActive(false);
                    musketeerTab.SetActive(false);
                    m_currentOpenTab = Tabs.hitcoins;
                    specialbtn.GetComponent<Image>().sprite = unSelectedSprite;
                    hitcoinBtn.GetComponent<Image>().sprite = selectedSprite;
                    musketeerBtn.GetComponent<Image>().sprite = unSelectedSprite;
                }
                break;
            case Tabs.musketeer:
                {
                    musketeerTab.SetActive(true);
                    specialTab.SetActive(false);
                    hitcoinTab.SetActive(false);
                    m_currentOpenTab = Tabs.musketeer;
                    specialbtn.GetComponent<Image>().sprite = unSelectedSprite;
                    hitcoinBtn.GetComponent<Image>().sprite = unSelectedSprite;
                    musketeerBtn.GetComponent<Image>().sprite = selectedSprite;
                }
                break;
        }
    }


}

public enum Tabs
{
    hitcoins = 0,
    special = 1,
    musketeer = 2
}
