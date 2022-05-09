using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float initialRotationZ, distanceFromTarget = 3f;
    [SerializeField]
    private float sensitivity = 0.5f;

    [SerializeField]
    private float paddleRadius = 2.0f;

    [SerializeField]
    private float maxEpsilon = 40.0f;
    [SerializeField]
    private float maxOmega = 40.0f;

    [SerializeField]
    private float spring_constant_linear = 1.0f;
    [SerializeField]
    private float spring_constant_quadratic = 0.1f;
    [SerializeField]
    private float spring_constant_sqrt = 0.5f;
    [SerializeField]
    private float b_factor = 1.5f;

    private float target_theta = 0.0f;
    private float theta = 0.0f;
    private float omega = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        float scaleCoefficient = distanceFromTarget / paddleRadius;

        transform.localScale = new Vector3(scaleCoefficient, scaleCoefficient, scaleCoefficient);
        transform.position = new Vector2(distanceFromTarget, 0.0f);
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
                target_theta += touch.deltaPosition.x * sensitivity;
            }
        }

        RotatePaddle();
    }
    void RotatePaddle()
    {
        float delta_theta = target_theta - theta;
        float epsilon = delta_theta * spring_constant_linear + Mathf.Sign(delta_theta) * (delta_theta * delta_theta * spring_constant_quadratic + Mathf.Sqrt(Mathf.Abs(delta_theta)) * spring_constant_sqrt);
        epsilon -= b_factor * omega;

        if(epsilon < -maxEpsilon)
            epsilon = -maxEpsilon;
        else if(epsilon > maxEpsilon)
            epsilon = maxEpsilon;

        if(Mathf.Abs(omega + epsilon * Time.deltaTime) < maxOmega)
            theta += (omega + epsilon * Time.deltaTime * 0.5f) * Time.deltaTime;
        else
            theta += omega * Time.deltaTime;
        omega += epsilon * Time.deltaTime;

        if(omega < -maxOmega)
            omega = -maxOmega;
        else if(omega > maxOmega)
            omega = maxOmega;

        transform.position = new Vector2(distanceFromTarget, 0.0f);
        transform.rotation = Quaternion.identity;
        transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 1.0f), theta);
    }
}
