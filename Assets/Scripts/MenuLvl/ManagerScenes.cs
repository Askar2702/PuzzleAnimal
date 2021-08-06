using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerScenes : MonoBehaviour
{
    [SerializeField] private Button _startGame;

    private void Awake()
    {
        _startGame.onClick.AddListener(() => LoadLvl());
    }

    private void LoadLvl()
    {
        SceneManager.LoadScene("GameLvl");
    }
}
