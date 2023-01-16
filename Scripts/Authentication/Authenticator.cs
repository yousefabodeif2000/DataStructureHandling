using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using Dimensional.UI;
using Dimensional.Game;
using DG.Tweening;
namespace Dimensional.Authentication
{
    public class Authenticator : Controller
    {

        public string PlayerIDCache
        {
            get;set;
        }
        static public Authenticator Instance;

        public bool IsAuthenticated { get; set; }

        public enum AuthenticationType
        {
            SignUpLogin, AutoAuth
        }
        public AuthenticationType SignInType;
        [Header("References")]
        public TMP_InputField LoginUser;
        public TMP_InputField LoginPass;
        public TMP_InputField RegUser;
        public TMP_InputField RegEmail;
        public TMP_InputField RegPass;
        public CanvasGroup authenticationEffect;

        public override void Initialize()
        {
            StartCoroutine(Authentication());
        }
        protected override void Awake()
        {
            base.Awake();
            Instance = this;

        }
        IEnumerator Authentication()
        {
            if (SignInType == AuthenticationType.AutoAuth)
            {
                Authenticate();
            }
            while (!IsAuthenticated)
            {
                authenticationEffect.alpha = 1;
                yield return null;
            }
            authenticationEffect.DOFade(0, 1).OnComplete(() => MainMenuController.Instance.InitActivityCenter());
        }
        public void Authenticate()
        {
            print("Authenticating...");
            switch (SignInType)
            {
                case AuthenticationType.AutoAuth:
                    LoginWithCustomIDRequest customrequest = new LoginWithCustomIDRequest();
                    customrequest.CreateAccount = true;
                    customrequest.CustomId = PlayFabSettings.DeviceUniqueIdentifier;
                    PlayFabClientAPI.LoginWithCustomID(customrequest, RequestToken, OnError);
                    break;
                case AuthenticationType.SignUpLogin:
                    LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
                    request.Username = LoginUser.text;
                    request.Password = LoginPass.text;

                    PlayFabClientAPI.LoginWithPlayFab(request, RequestToken, OnError);
                    break;
            }



        }

        public void Register()
        {
            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
            request.Username = RegUser.text;
            request.Email = RegEmail.text;
            request.Password = RegPass.text;

            PlayFabClientAPI.RegisterPlayFabUser(request, result =>
            {
                print("Successfully Registered!");
            }, OnError);
        }
        void RequestToken(LoginResult result)
        {
            PlayerIDCache = result.PlayFabId;
            GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest();
            request.PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;

            PlayFabClientAPI.GetPhotonAuthenticationToken(request, AuthenticateWithPhoton, OnError);
        }
        void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult result)
        {
            var CustomAuth = new AuthenticationValues
            {
                AuthType = CustomAuthenticationType.Custom

            };

            CustomAuth.AddAuthParameter("username", PlayerIDCache);
            CustomAuth.AddAuthParameter("token", result.PhotonCustomAuthenticationToken);

            PhotonNetwork.AuthValues = CustomAuth;
            IsAuthenticated = true;
            //PhotonManager.ConnectToPhoton();

        }

        void OnError(PlayFabError error)
        {
            Debug.LogError("PlayfabError: " + error.ErrorMessage);
           // UIManager.Instance.NoInternetConnection = true;
        }

    }
}