using System.Timers;
using BumperCars;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject gameObject;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public TextTypes type;
    public string endOfTimeMessage;

    public void Show()
    {
        active = true;
        gameObject.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        gameObject.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        switch (type)
        {
            case TextTypes.Text:
            
                if (!active)
                    return;
                
                gameObject.transform.position += motion * Time.deltaTime;
                duration -= Time.deltaTime;
               
                if (duration < 0)
                    Hide();
                break;
            
            case TextTypes.Timer:
                if (!active)
                    return;
                
                duration -= Time.deltaTime;
                txt.text = duration.ToString("0.0");

                if (duration < 0)
                {
                    txt.text = endOfTimeMessage;
                    txt.color = Color.red;
                    if(duration < -1)
                        Hide();
                }
                break;

        }
        
    }
}