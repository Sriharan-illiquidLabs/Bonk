using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_AR : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public enum Catatories
    {
        Overview,
        Featured,
        Goat,
        Head,
        Body,
        Back,
        Feet,
        Fur,
        Horns
    }



    [Header("Item")]

    public Catatories type;
    public Sprite itemImage;
    public string itemName;
    [TextArea]
    public string itemDesc;

    [Header("Item Assign")]
    public Image itemImageTag;
    public Image itemImageSelected;
    public Image itemImageHighlight;
    
    public Image itemCheckBG;
    public TextMeshProUGUI itemTextTag;



    void Start()
    {
        itemImageTag.sprite = itemImage;
        itemTextTag.text = itemName;
        if (GetComponent<Toggle>().isOn)
        {
            itemImageSelected.enabled = true;
            itemCheckBG.enabled = true;
        }
    }

    public void OnClick()
    {
        WardrobeUIPanelManager_AR.instance.SetName(itemName, itemDesc);

        Debug.Log("Item: " + itemName);
        Debug.Log("Type: " + type.ToString());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!itemImageSelected.enabled)
            itemImageHighlight.enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        itemImageHighlight.enabled = false;
    }
    public void ToggleEnable(Toggle toggle)
    {
        if (toggle.isOn)
        {
            itemImageSelected.enabled = true;
            itemCheckBG.enabled = true;
        }
        else
        {
            itemImageSelected.enabled = false;
            itemCheckBG.enabled = false;
        }
    }

}
