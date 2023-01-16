using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
namespace Dimensional.Game
{
    public static class GameEvents
    {
        static public event Action OnLevelStart;
        static public event Action OnLevelEnd;
        static public void CheckGameEvents()
        {
            switch (GameController.Instance.GameState)
            {
                case GameController.GameStates.InGame:
                    Debug.Log("Invoking OnLevelStart...");
                    OnLevelStart?.Invoke();
                    break;
                case GameController.GameStates.PostGame:
                    OnLevelEnd?.Invoke();
                    break;

            }
        }
    }
}