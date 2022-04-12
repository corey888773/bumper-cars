using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject gameObject;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        gameObject.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        gameObject.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active)
            return;
        
        // if (Time.time - lastShown > duration) Hole.green += 0.4f / 255f;
        //
        // foreach (var floatingText in FloatingTextManager.floatingTexts)
        // {
        //     if (Hole.green <= 1)
        //         floatingText.txt.color = new Color(Hole.red, Hole.green, Hole.blue);
        //     else
        //         floatingText.txt.text = "Active";
        // }

        gameObject.transform.position += motion * Time.deltaTime;
    }
}