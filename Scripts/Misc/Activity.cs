using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Dimensional.Game
{
    [CreateAssetMenu(fileName = "New Activity", menuName = "Dimensional/Activity")]
    public class Activity : ScriptableObject
    {
        public string ActivityName;
        public Sprite ActivityIcon;
        public string ActivityDescription;
        public string sceneToLoad;
    }
}
