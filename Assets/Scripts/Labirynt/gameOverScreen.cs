using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScreen : MonoBehaviour
{
    public void mainMenuButton(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void quitButton()
    {
        Application.Quit();
    }
    public void readyCheckbox()
    {
        //Jezeli minigierka ma byc jedna z wielu, gracz potwierdza chec udzialu w kolejnej
    }
}
