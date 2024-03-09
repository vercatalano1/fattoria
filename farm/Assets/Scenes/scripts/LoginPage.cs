using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPAge : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI Toptext;
    [SerializeField] TextMeshProUGUI MessageText;

    [Header("Login")]
    [SerializeField] TMP_InputField EmailLoginInput;
    [SerializeField] TMP_InputField PasswordLoginInput;
    [SerializeField] GameObject LoginPage;

    [Header("Register")]
    [SerializeField] TMP_InputField UsernameRegisterInput;
    [SerializeField] TMP_InputField EmailRegisterInput;
    [SerializeField] TMP_InputField PasswordRegisterInput;
    [SerializeField] GameObject RegisterPage;

    [Header("Recovery")]
    [SerializeField] TMP_InputField EmailRecoveryInput;
    [SerializeField] GameObject RecoveryPage;

    [SerializeField]
    private GameObject WelcomeObject;
    [SerializeField] private GameManager gameManager;

    [SerializeField]
    private Text WelcomeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Buttom Function

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = EmailLoginInput.text,
            Password = PasswordLoginInput.text,

            InfoRequestParameters= new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile=true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }


    private void OnLoginSuccess(LoginResult Result)
    {
        String name = null;
        if(Result.InfoResultPayload != null)
        {
           name = Result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        
        WelcomeObject.SetActive(true);

        WelcomeText.text = "Welcome " + name;
        if(gameManager != null)
        {
            gameManager.playerName = name;
        }
        StartCoroutine(LoadNextScene());
    }

    public void RecoverUser()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = EmailRecoveryInput.text,
            TitleId = "E9C70",
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySuccess, OnErrorRecovery);
    }

    private void OnErrorRecovery(PlayFabError error)
    {
        MessageText.text = "No Email Found";
    }

    private void OnRecoverySuccess(SendAccountRecoveryEmailResult obj)
    {
        OpenLoginPage();
        MessageText.text = "Recovery Mail Sent";
    }

    public void RegisterUser()
    {
        //if password is less


        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = UsernameRegisterInput.text,
            Email = EmailRegisterInput.text,
            Password = PasswordRegisterInput.text,

            RequireBothUsernameAndEmail = false,
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnregisterSuccess, OnError);
    }

    private void OnError(PlayFabError Error)
    {
        MessageText.text = Error.ErrorMessage;
        Debug.Log(Error.GenerateErrorReport());
    }

    private void OnregisterSuccess(RegisterPlayFabUserResult Result)
    {
        MessageText.text = "New Account Is Created";
        OpenLoginPage();
    }

    public void OpenLoginPage()
    {
        LoginPage.SetActive(true);
        RegisterPage.SetActive(false);
        RecoveryPage.SetActive(false);
        Toptext.text = "Login";
    }

    public void OpenRegisterPage()
    {
        LoginPage.SetActive(false);
        RegisterPage.SetActive(true);
        RecoveryPage.SetActive(false);
        Toptext.text = "Register";
    }

    public void OpenRecoveryPage()
    {
        LoginPage.SetActive(false);
        RegisterPage.SetActive(false);
        RecoveryPage.SetActive(true);
        Toptext.text = "Recovery";

    }

    #endregion


    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2);
        MessageText.text = "Loggin in";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
