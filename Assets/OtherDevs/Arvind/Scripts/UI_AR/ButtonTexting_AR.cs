using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTexting_AR : MonoBehaviour
{
    #region Temp
    //Remove on deployment.
    public bool ChangeText;
    private void OnValidate()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = gameObject.name;
    }

    #endregion


    

    //public void TextColor(Toggle toggle)
    //{
    //    if (toggle.isOn)
    //    {
            
            
    //    }
    //    else
    //    {
            
            
    //    }
    //}
}
