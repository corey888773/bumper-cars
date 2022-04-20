using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI finishedOnPositionTxt;

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


    //Dostosowywanie tekstu
    public void finishedOnPosition(int finishingPosition)
    {
        finishedOnPositionTxt.text = finishingPosition.ToString() + ". position";
    }

    private void Start()
    {
        finishedOnPosition(1);
    }
}
