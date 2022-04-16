using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
    [SerializeField] private GameObject obst;
    [SerializeField] private Transform parent;
    private Camera cam;
    private float cameraWidth, cameraHeight;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;
        cameraHeight = cam.orthographicSize;
        cameraWidth = Screen.width / Screen.height * cam.orthographicSize;

        var horSecSize = cameraWidth / 2;
        var verSecSize = cameraHeight / 2;

        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                Instantiate(obst, new Vector3(Random.Range(-cameraWidth + i * horSecSize, -cameraWidth + (i + 1) * horSecSize - 0.7f),
                    Random.Range(-cameraHeight + j * verSecSize, -cameraHeight + (j + 1) * verSecSize - 0.7f), 0), Quaternion.identity, parent);
            }
        }
    }
}