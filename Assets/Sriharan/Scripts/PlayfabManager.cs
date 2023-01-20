using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    private static PlayfabManager instance;
    public static PlayfabManager Instance { get { return instance; } }

    private string playerName = null;
    [SerializeField] private GameObject namePanel;
    [SerializeField] private TMP_InputField nameField;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }


    #region Login

    public void LoginWithCustomID(string _customID)
    {
        var request = new LoginWithCustomIDRequest()
        {
            CustomId = _customID,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("LOGIN SUCCESSFUL");

        if (result.InfoResultPayload.PlayerProfile != null)
            playerName = result.InfoResultPayload.PlayerProfile.DisplayName;

        if (playerName == null)
        {
            namePanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void OnLoginError(PlayFabError error)
    {
        Debug.LogError("LOGIN FAILED");
    }

    #endregion

    #region Display Name

    public void OnClick_SubmitPlayerName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameField.text
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdated, OnDisplayNameUpdateFailed);
    }

    private void OnDisplayNameUpdated(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Name Updated Successfully");

        namePanel.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDisplayNameUpdateFailed(PlayFabError error)
    {
        Debug.LogError("Failed to update player name: " + error.ErrorMessage);
    }

    #endregion
}
