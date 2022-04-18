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

    }

    private void Start()
    {
#if UNITY_ANDROID
        LoginGoogle.LinkPlayfabToGoogle(onLinkSuccess, onLinkFailure);

#elif UNITY_IOS
        //login.IOSAwake();
        LoginApple.AppleLinkAcount(onLinkSuccess, onLinkFailure, false);
#endif
    }

    private static void onLinkSuccess(LoginResult result)
    {

    }

    private static void onLinkFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

}