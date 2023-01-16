using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using Dimensional.UI;
using Dimensional.Player;
namespace Dimensional.Game
{
    public sealed class GameController: Controller
    {
        static public GameController Instance;
        [Header("Modules")]
        public MainmenuBackgroundModule MenuBackgroundsModules;
        #region Events
        static public event Action OnLevelUp;
        static public event Action OnLevelStart;
        static public event Action OnLevelEnd;
        #endregion
        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        public override void Initialize()
        {
            Instantiate(MenuBackgroundsModules.backgroundVFXs[UnityEngine.Random.Range(0, MenuBackgroundsModules.backgroundVFXs.Count)]);
            print("Game controller is initialized");
        }
        public enum GameStates
        {
            MainMenu, Store, Settings, About, Tutorial, InGame, PostGame
        }
        GameStates state;
        public GameStates GameState
        {
            get => state;
            set
            {
                state = value;
                GameEvents.CheckGameEvents();
                switch (state)
                {
                    case GameStates.MainMenu:
                        //Scene Check
                        if (SceneManager.GetActiveScene().name != "menu")
                            ChangeScene("menu");
                        break;
                    case GameStates.Settings:
                        UIController.Instance.OpenWindow("settings");
                        break;
                    case GameStates.Store:
                        UIController.Instance.OpenWindow("store");
                        break;
                    case GameStates.About:
                        UIController.Instance.OpenWindow("about");
                        break;
                    case GameStates.Tutorial:
                        if (SceneManager.GetActiveScene().name != "tutorial")
                            ChangeScene("tutorial");
                        break;
                    case GameStates.InGame:
                        if (SceneManager.GetActiveScene().name != "game")
                            ChangeScene("game");
                        break;
                    case GameStates.PostGame:
                        //Level is done
                        LevelUp();
                        OnLevelEnd?.Invoke();
                        break;

                }
            }
        }
        public void LevelUp()
        {
            GameData.Level++;
            OnLevelUp?.Invoke();

        }
        private void OnLevelWasLoaded(int level)
        {
            if(SceneManager.GetActiveScene().name == "game")
            {
                OnLevelStart?.Invoke();
            }
        }
        public void ChangeScene(string sceneName)
        {
            UIController.Instance.LoadingScreen.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(sceneName));
        }

    }
}