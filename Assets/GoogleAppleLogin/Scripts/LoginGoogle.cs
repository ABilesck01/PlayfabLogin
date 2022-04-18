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

    /// <summary>
    /// Sign in PlayFab with Google account credentials.
    /// </summary>
    /// <param name="onLinkSuccess">gets playfab login result.</param>
    /// <param name="onLinkError">gets playfab error message</param>
    public static void LinkPlayfabToGoogle(System.Action<LoginResult> onLinkSuccess, 
        System.Action<PlayFabError> onLinkError)
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

                }, onLinkSuccess, onLinkError);
            }
            else
            {
                Debug.LogError("Error");
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

    private void Awake()
    {
        GoogleAwake();
    }

#endif
}
