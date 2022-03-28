using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;

public class PlayFabLogin : MonoBehaviour
{
    public void Awake()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "";
        }
#if UNITY_ANDROID
        LoginGoogle.GoogleAwake();

        LoginGoogle.LoginWithGoogle();
            
#elif UNITY_IOS
        //login.IOSAwake();
        LoginApple.AppleAwake();
        LoginApple.AppleQuickLogin();
#endif
    }

#if UNITY_IOS

    private void Update()
    {
        if (LoginApple.appleAuthManager != null)
        {
            LoginApple.appleAuthManager.Update();
        }
    }

#endif

}