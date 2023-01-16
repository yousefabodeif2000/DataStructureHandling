using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using TMPro;
using Dimensional.UI;
using UnityEngine.Audio;
namespace Dimensional.Game
{
    public class MainMenuController : Singleton<MainMenuController>
    {
        static public MainMenuEvents Events = new MainMenuEvents();
        public MainMenuSettings Settings = new MainMenuSettings();

        [Header("References")]
        public CanvasGroup mainCanvas;
        public TMP_Text promptText; //welcome text
        public TMP_Text activityWindowTitle;
        public InputField playerName;
        public Button playButton;

        [Header("Resources")]
        public ActivityPrompt ActivityPrompt; //Activity prompt window 

        protected override void Awake()
        {
            base.Awake();
            //playButton.interactable = false;
        }
        public void SavePlayerSettings()
        {
            if (string.IsNullOrEmpty(playerName.text))
            {
                UIController.Instance.Dialogue("Please enter your name!");
                return;
            }
            else
            {
                GameData.PlayerName = playerName.text;
                PromptText("<color=red>Welcome! " + GameData.PlayerName + "</color>. Click the joystick button to start.");
                UIController.Instance.Dialogue("Name changed successfully!");
                activityWindowTitle.text = "Choose an activity, " + GameData.PlayerName + "!";
                playButton.interactable = true;
            }
        }
        public void InitActivityCenter()
        {
            mainCanvas.DOFade(1, 1);
            PromptText("<color=red>Welcome!</color> Click the joystick button to start.");

            var buttons = FindObjectsOfType<Button>();
            foreach(var button in buttons)
            {
                button.onClick.AddListener(() => 
                Settings.PlaySoundFX(Settings.SoundModule.buttonClicks[UnityEngine.Random.Range(0, Settings.SoundModule.buttonClicks.Count)]));
            }
            Settings.Init();
        }
        public void PromptText(string text)
        {
            promptText.text = text;
        }
        public void PromptAnActivityToPlay(Activity activity)
        {
            var promptActivity = Instantiate(ActivityPrompt, mainCanvas.transform);
            promptActivity.IntegrateActivity(activity);
            promptActivity.OpenWindow();
        }
        public void ChangeMusicSlider()
        {
            Settings.MuteMusic();
        }
        public void ChangeSoundSlider()
        {
            Settings.MuteSound();
        }
    }
}

[Serializable]
public class MainMenuEvents
{
    public event Action OnSwiped;
    public void InvokeSwipeEvent()
    {
        OnSwiped?.Invoke();
    }
}
[Serializable]
public class MainMenuSettings
{
    public bool IsMusicMuted;
    public bool IsSoundMuted;
    public AudioSource musicSource;
    public AudioSource soundSource;
    public SoundModule SoundModule;
    public AudioMixer musicMixer;
    public void PlaySoundFX(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }
    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }
    public void Init()
    {
        PlayMusic(SoundModule.backgroundMusic[UnityEngine.Random.Range(0, SoundModule.backgroundMusic.Count)]);
    }
    public void MuteSound()
    {
        IsSoundMuted = !IsSoundMuted;
        if (IsSoundMuted)
            musicMixer.DOSetFloat("SoundParam", -80, 0.25f);
        else
            musicMixer.DOSetFloat("SoundParam", 1, 0.25f);
    }
    public void MuteMusic()
    {
        IsMusicMuted = !IsMusicMuted;
        if (IsMusicMuted)
            musicMixer.DOSetFloat("MusicParam", -80, 0.25f);
        else
            musicMixer.DOSetFloat("MusicParam", 1, 0.25f);
    }
}