using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
namespace Dimensional.UI
{
    public class Window : MonoBehaviour
    {
        public string windowName;
        public float scaleSpeed;
        public Button closeButton;
        public bool DestroyOnWindowClose;
        public virtual void Awake()
        {
            if (closeButton)
                closeButton.onClick.AddListener(CloseWindow);

            transform.localScale = Vector3.zero;
        }
        internal void CloseWindow()
        {
            //Tween it
            print(windowName + " window closing.");
            var sequence = DOTween.Sequence().Join(transform.DOScale(0, scaleSpeed));
            if (DestroyOnWindowClose)
            {
                sequence.Play().OnComplete(() => Destroy(gameObject));
                GetComponent<CanvasGroup>().DOFade(0, scaleSpeed);
            }
            else
            {
                sequence.Play();
                GetComponent<CanvasGroup>().DOFade(0, scaleSpeed);
            }


        }
        internal void OpenWindow()
        {
            //Tween it
            print(windowName + " window opening.");
            var sequence = DOTween.Sequence().Join(transform.DOScale(1, scaleSpeed));
            sequence.Play();
            GetComponent<CanvasGroup>().DOFade(1, scaleSpeed);
            
        }
    }
}
