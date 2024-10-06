using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        AudioManager.instance.PlayMenuButtonSound();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        AudioManager.instance.PlayMenuButtonSound();
        Application.Quit();
    }
}
