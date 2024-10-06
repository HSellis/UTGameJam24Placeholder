using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject GameMenuPanel;

    public TextMeshProUGUI HUDText;
    public TextMeshProUGUI GameOverText;

    // Start is called before the first frame update
    void Start()
    {
        GameMenuPanel.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePresentsText(int presentsNumber)
    {
        HUDText.text = "Presents: " + presentsNumber.ToString();
    }

    public void DisplayGameOver(int presentsNumber)
    {
        GameMenuPanel.SetActive(true);
        GameOverText.text = "Max number of presets in bag: " + presentsNumber.ToString();
        Time.timeScale = 0.01f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
