using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.SocialPlatforms.GameCenter;
using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;
#endif
public class LoginApple : MonoBehaviour
{
#if UNITY_IOS
    [HideInInspector] public static AppleAuthManager appleAuthManager;

    public static void AppleAwake()
    {
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            // Creates a default JSON deserializer, to transform JSON Native responses to C# instances
            var deserializer = new PayloadDeserializer();
            // Creates an Apple Authentication manager with the deserializer
            appleAuthManager = new AppleAuthManager(deserializer);
        }
    }

    /// <summary>
    /// Sign in playfab using apple id account credentials.
    /// </summary>
    /// <param name="onLinkSuccess">gets playfab login result.</param>
    /// <param name="onLinkError">gets playfab error message</param>
    /// <param name="getAcountInfo"> if true, return apple id account info</param>
    public static void AppleLinkAcount(System.Action<LoginResult> onLinkSuccess,
        System.Action<PlayFabError> onLinkError, bool getAcountInfo)
    {
        var quickLoginArgs = new AppleAuthQuickLoginArgs();

        // Quick login should succeed if the credential was authorized before and not revoked
        appleAuthManager.QuickLogin(
            quickLoginArgs,
            credential =>
            {
                // If it's an Apple credential, save the user ID, for later logins
                var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
                    PlayerPrefs.SetString("AppleUserIdKey", credential.User);

                    AppleLogin(appleIdCredential.IdentityToken, onLinkSuccess, onLinkError);
                }
            },
            error =>
            {
                if (getAcountInfo)
                    AppleAuthLink(onLinkSuccess, onLinkError);
                else
                    AppleAuthLinkWithoutEmail(onLinkSuccess, onLinkError);
            });
    }

    private static void AppleAuthLink(System.Action<LoginResult> onLinkSuccess,
        System.Action<PlayFabError> onLinkError)
    {
        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

        appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
                // Obtained credential, cast it to IAppleIDCredential
                var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
                    // Apple User ID
                    // You should save the user ID somewhere in the device
                    var userId = appleIdCredential.User;
                    PlayerPrefs.SetString("AppleUserIdKey", userId);

                    // Email (Received ONLY in the first login)
                    var email = appleIdCredential.Email;

                    // Full name (Received ONLY in the first login)
                    var fullName = appleIdCredential.FullName;

                    // Identity token
                    var identityToken = Encoding.UTF8.GetString(
                        appleIdCredential.IdentityToken,
                        0,
                        appleIdCredential.IdentityToken.Length);

                    // Authorization code
                    var authorizationCode = Encoding.UTF8.GetString(
                        appleIdCredential.AuthorizationCode,
                        0,
                        appleIdCredential.AuthorizationCode.Length);

                    // And now you have all the information to create/login a user in your system
                    AppleLogin(appleIdCredential.IdentityToken, onLinkSuccess, onLinkError);
                }
            },
            error =>
            {
                // Something went wrong
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
            });
    }

    private static void AppleAuthLinkWithoutEmail(System.Action<LoginResult> onLinkSuccess,
        System.Action<PlayFabError> onLinkError)
    {
        var loginArgs = new AppleAuthLoginArgs();

        appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
                // Obtained credential, cast it to IAppleIDCredential
                var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
                    // Apple User ID
                    // You should save the user ID somewhere in the device
                    var userId = appleIdCredential.User;
                    PlayerPrefs.SetString("AppleUserIdKey", userId);
 // Identity token
                    var identityToken = Encoding.UTF8.GetString(
                        appleIdCredential.IdentityToken,
                        0,
                        appleIdCredential.IdentityToken.Length);

                    // Authorization code
                    var authorizationCode = Encoding.UTF8.GetString(
                        appleIdCredential.AuthorizationCode,
                        0,
                        appleIdCredential.AuthorizationCode.Length);

                    // And now you have all the information to create/login a user in your system
                    AppleLogin(appleIdCredential.IdentityToken, onLinkSuccess, onLinkError);
                }
            },
            error =>
            {
                // Something went wrong
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
            });
    }

    private static void AppleLogin(byte[] identityToken, System.Action<LoginResult> onLinkSuccess,
        System.Action<PlayFabError> onLinkError)
    {
        PlayFabClientAPI.LoginWithApple(new LoginWithAppleRequest()
        {
            CreateAccount = true,
            IdentityToken = Encoding.UTF8.GetString(identityToken),
            TitleId = PlayFabSettings.TitleId
        }, onLinkSuccess, onLinkError);
    }

    private void Awake()
    {
        AppleAwake();
    }

    private void Update()
    {
        if (appleAuthManager != null)
        {
            appleAuthManager.Update();
        }
    }
#endif
}
