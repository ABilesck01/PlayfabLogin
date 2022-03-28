using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class LoginGoogle : MonoBehaviour
{

#if UNITY_ANDROID
    public static void LoginWithGoogle()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            var serverAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();

            if (success)
            {
                PlayFabClientAPI.LoginWithGoogleAccount(new LoginWithGoogleAccountRequest()
                {
                    TitleId = PlayFabSettings.TitleId,
                    ServerAuthCode = serverAuthCode,
                    CreateAccount = true

                }, OnMobileLoginSuccess, OnMobileLoginFailure);
            }
            else
            {
                Debug.LogError("Erro!");
            }

        });

    }

    public static void GoogleAwake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .AddOauthScope("profile")
            .RequestServerAuthCode(false)
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

#endif

    private static void OnMobileLoginSuccess(LoginResult result)
    {

    }

    private static void OnMobileLoginFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}
