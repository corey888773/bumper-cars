using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-5.0f, 5.0f));
        transform.position = position;
    }
}
