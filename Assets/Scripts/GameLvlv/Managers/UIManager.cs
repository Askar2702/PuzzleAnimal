using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _exit;

    private void Awake()
    {
        _exit.onClick.AddListener(() => ExitGame());
    }

   
    private void ExitGame()
    {
        SceneManager.LoadScene("Menu");
       // Application.Quit();
    }
}
