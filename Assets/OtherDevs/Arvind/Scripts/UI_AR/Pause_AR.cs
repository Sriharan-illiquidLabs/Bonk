using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_AR : MonoBehaviour
{
    [SerializeField]
    private UIManager_AR UIManager_AR;

    public void UnPause()
    {
        UIManager_AR.UnPause();
    }
}
