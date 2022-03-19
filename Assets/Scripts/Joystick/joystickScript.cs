using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickScript : MonoBehaviour
{
    [SerializeField]
    private float maxR = 1;

    [SerializeField]
    private float speed = 4.0f;

    private Vector2 currentTargetPosition_;
    private Camera cam_;
    private float invMaxR_;

    // Start is called before the first frame update
    void Start()
    {
        cam_ = GameObject.Find("Main Camera").GetComponent<Camera>();
        invMaxR_ = 1.0f / maxR;
    }

    void OnValidate()
    {
        invMaxR_ = 1.0f / maxR;
    }

    // Update is called once per frame
    void Update()
    {
        int best_index = -1;
        float best_dist_sqr = 100000f;
        Vector2 par_pos = (Vector2) transform.parent.position, best_pos = new Vector2(0.0f, 0.0f);

        for(int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector2 pos = ((Vector2) cam_.ScreenToWorldPoint(touch.position)) - par_pos;

            if(best_index == -1 || pos.sqrMagnitude < best_dist_sqr)
            {
                best_index = i;
                best_dist_sqr = pos.sqrMagnitude;
                best_pos = pos;
            }
        }

        if(best_index != -1)
        {
            currentTargetPosition_ = best_pos;

            if(best_dist_sqr > maxR)
            {
                currentTargetPosition_.Normalize();
                currentTargetPosition_ *= maxR;
            }

            currentTargetPosition_ += par_pos;
        }
        else
            currentTargetPosition_ = par_pos;
        
        transform.position = Vector2.Lerp(transform.position, currentTargetPosition_, Time.deltaTime * speed);
    }

    // Zwraca obecna pozycje joysticka (dlugosc wektora [0; 1])
    public Vector2 getValue()
    {
        return (Vector2) (transform.position - transform.parent.position) * invMaxR_;
    }

    // zwraca docelowa pozycje joysticka (dlugosc wektora [0; 1])
    public Vector2 getTargetValue()
    {
        return currentTargetPosition_;
    }
}
