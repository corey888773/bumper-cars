using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject obst;
    private Camera cam;
    private float cameraWidth,cameraHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; 
        cameraHeight = cam.orthographicSize;
        cameraWidth = Screen.width/Screen.height * cam.orthographicSize;
        
        for (int i = 0; i < 10; i++)
        {
            Instantiate(obst, new Vector3(Random.Range(-cameraWidth, cameraWidth),
                Random.Range(-cameraHeight, cameraHeight), 0), Quaternion.identity);
        }
    }
}
