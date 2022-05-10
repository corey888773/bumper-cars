using System.Collections;
using UnityEngine;
namespace Czolgi {
    public class ObstacleGenerator : MonoBehaviour {
        [SerializeField] private GameObject obst;
        [SerializeField] private Transform parent;
        private Camera cam;
        private float cameraWidth, cameraHeight;
        //private DeathSpiral dp;

        // Start is called before the first frame update
        void Start() {
            cam = Camera.main;
            cameraHeight = cam.orthographicSize;
            cameraWidth = Screen.width / Screen.height * cam.orthographicSize;

            var horSecSize = cameraWidth / 2;
            var verSecSize = cameraHeight / 2;
/*
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    Instantiate(obst, new Vector3(Random.Range(-cameraWidth + i * horSecSize, -cameraWidth + (i + 1) * horSecSize - 0.7f),
                        Random.Range(-cameraHeight + j * verSecSize, -cameraHeight + (j + 1) * verSecSize - 0.7f), 0), Quaternion.identity, parent);
                }
            }*/
            StartCoroutine(MakeSpiral(cameraWidth, cameraHeight));
        }
        
        public IEnumerator MakeSpiral(float cameraWidth, float cameraHeight)
        {
            float x = 2*cameraWidth / 0.7f;
            float y = 2*cameraHeight / 0.7f;

            /*
             * Numbers of iterations made
             * 0-top, 1-right, 2-down, 3-left
             */
            float[] rev = new float[4] {0, 0, 0, 0};

            //here hell starts
            while (x>0 && y>0)
            {
                //top edge of rectangle
                for (float i = 0; i <= x; i++)
                {
                    Instantiate(obst, new Vector3(-cameraWidth+rev[0]*0.7f+i*0.7f, cameraHeight-0.7f-rev[0]*0.7f, 0), Quaternion.identity, parent);
                    yield return new WaitForSeconds(.5f);
                }
                rev[0]++;
                y--;
                
                //right edge of rectangle
                for (float i = 0; i <= y; i++)
                {
                    Instantiate(obst, new Vector3(cameraWidth-rev[1]*0.7f, cameraHeight-0.7f-rev[1]*0.7f-i*0.7f, 0), Quaternion.identity, parent);
                    yield return new WaitForSeconds(.5f);
                }
                rev[1]++;
                x--;
                
                //down edge of rectangle
                for (float i = 0; i <= x; i++)
                {
                    Instantiate(obst, new Vector3(cameraWidth-0.7f-rev[2]*0.7f-i*0.7f, -cameraHeight+rev[2]*0.7f, 0), Quaternion.identity, parent);
                    yield return new WaitForSeconds(.5f);
                }
                rev[2]++;
                y--;
                
                //left of rectangle
                for (float i = 0; i <= y; i++)
                {
                    Instantiate(obst, new Vector3(-cameraWidth+rev[3]*0.7f, -cameraHeight+0.7f+rev[3]*0.7f+i*0.7f, 0), Quaternion.identity, parent);
                    yield return new WaitForSeconds(.5f);
                }
                rev[3]++;
                x--;
            }
        }
    }
}