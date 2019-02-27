using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject deathScreen;
    [SerializeField]
    private GameObject bagBar;

    private bool isActivated = false;

    private static DeathScreen instance;

    public static DeathScreen MyInstance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<DeathScreen>();

            return instance;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Player.MyInstance.MyHealth.MyCurrentValue <= 0)
        {
            bagBar.SetActive(false);
            ActivateDeathScreen();
        }
        else if (Player.MyInstance.MyHealth.MyCurrentValue > 0)
        {
            bagBar.SetActive(true);
            deathScreen.SetActive(false);
            //Time.timeScale = 1f;
        }
    }

    public void ActivateDeathScreen()
    {
        if (Player.MyInstance.MyHealth.MyCurrentValue <= 0)
        {
            //Time.timeScale = 0f;
            bagBar.SetActive(false);
            isActivated = true;
            deathScreen.SetActive(true);
        }
    }

    //private void DeActivateDeathScreen()
    //{
    //    if (Time.timeScale == 0f)
    //    {

    //    }
    //}

    public void MainMenu()
    {
        deathScreen.SetActive(false);
        bagBar.SetActive(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        deathScreen.SetActive(false);
        bagBar.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
