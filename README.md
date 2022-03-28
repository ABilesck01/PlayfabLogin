# Introduction
Use this unity package to use google or apple account to login with playfab. It is easily modifiable and will probably suit in yours needs.

## How to install
1. Import the unity package.
2. Login in your playfab account.
    * In unity, go to Window > Playfab > Editor Extensions.
    * If you already has an account, click in login, and insert yours credentials.
    * Or you can create an account.
3. Change your build platform to Android and force resolve the android manifest.
    * To force resolve, go to Assets > External Dependency Manager > Android resolver > Force Resolve.
4. Open PlayFabLogin script, located at GoogleAppleLogin > Scripts folder and change the playfab title Id to your game title id.

## how to use

1. Drag and drop the LoginManager prefab to the scene you want to login. The prefab is located at GoogleAppleLogin > Prefabs folder.
2. Switch your platform to android and open LoginGoogle, located at GoogleAppleLogin > Scripts folder
3. Change the methods OnLoginSuccess and OnLoginError to fit in your needs.
4. Switch your platform to IOS and repeat steps 2 and 3 with LoginApple.

### Atention
* your game must be in store (internal test for android and testflight to IOS) to login, else it will not work, but the game should run normaly. 
To test with login in editor, switch your platform to a pc build.
