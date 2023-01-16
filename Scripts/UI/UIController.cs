using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using Dimensional.Game;
using DG.Tweening;
namespace Dimensional.UI
{
    public sealed class UIController : Controller
    {
        static public UIController Instance;
        [Header("References")]
        public CanvasGroup LoadingScreen;
        public CanvasGroup DialogueScreen;
        public TMP_Text DialoguePrompt;

        public List<Window> Windows
        {
            get
            {
                return FindObjectsOfType<Window>().ToList();
            }
        }
        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }
        public override void Initialize()
        {
            if (SceneManager.GetActiveScene().name == "menu")
                OpenWindow("main screen");

            var windows = FindObjectsOfType<Window>();
            foreach (var window in windows)
                window.CloseWindow();
        }
        public void OpenWindow(string windowName)
        {
            Window windowToOpen = Windows.Where(window => window.windowName == windowName).FirstOrDefault();
            if (windowToOpen)
            {
                var otherWindows = Windows.Where(window => window != windowToOpen).ToList();
                foreach(var window in otherWindows)
                {
                    window.CloseWindow();
                }
                windowToOpen.OpenWindow();
            }
        }
        public void AddCoins(int amount)
        {
            int coinNumber = GameData.MazeCoins + amount;
            GameData.MazeCoins = coinNumber;
            print("Added coins of amount: " + amount);
        }
        public void Dialogue(string text)
        {
            DialogueScreen.DOFade(1, 0.25f);
            DialoguePrompt.text = text;
            DialogueScreen.interactable = true;
            DialogueScreen.blocksRaycasts = true;
        }
        public void DialogueOff()
        {
            DialogueScreen.DOFade(0, 0.25f);
            DialogueScreen.interactable = false;
            DialogueScreen.blocksRaycasts = false;
        }
    }
}