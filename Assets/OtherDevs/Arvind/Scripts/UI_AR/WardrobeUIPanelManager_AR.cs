using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeUIPanelManager_AR : MonoBehaviour 
{
    public static WardrobeUIPanelManager_AR instance;
    public GameObject[] inventoryPanels;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        PanelsDisable();
        inventoryPanels[0].SetActive(true);
    }

    public void SetName(string name, string desc)
    {
        itemName.text = name;
        itemDesc.text = desc;
    }

    public void PanelsDisable()
    {
        foreach(GameObject panels in inventoryPanels)
        {
            if (panels.activeInHierarchy)
            {
                panels.SetActive(false);
            }
        }
    }
}
