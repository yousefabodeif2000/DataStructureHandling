using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dimensional.UI;
using Dimensional.Game;
using TMPro;
using UnityEngine.UI;
public class ActivityPrompt : Window
{
    [Header("References")]
    public TMP_Text activityNameHolder;
    public Image activityIconHolder;
    public TMP_Text activityDescriptionHolder;
    public Button playButton;
    public override void Awake()
    {
        base.Awake();
    }
    public void IntegrateActivity(Activity activity)
    {
        activityNameHolder.text = activity.ActivityName;
        activityIconHolder.sprite = activity.ActivityIcon;
        activityDescriptionHolder.text = activity.ActivityDescription;
        playButton.onClick.AddListener(() => GameController.Instance.ChangeScene(activity.sceneToLoad));
        playButton.onClick.AddListener(() =>
                MainMenuController.Instance.Settings.PlaySoundFX(MainMenuController.Instance.Settings.SoundModule.buttonClicks[Random.Range(0, MainMenuController.Instance.Settings.SoundModule.buttonClicks.Count)]));
    }

}
