using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float initialRotationZ, distanceFromTarget = 0f;
    [SerializeField]
    private float sensitivity = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(target.transform.position.x + distanceFromTarget, target.transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckMovement();
    }

    void CheckMovement()
    {
        if(Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                RotatePaddle(touch.deltaPosition.x);
            }
        }
    }
    void RotatePaddle(float angle)
    {
        transform.RotateAround(target.transform.position, new Vector3(0f, 0f, 1f), angle * sensitivity);
    }
}
