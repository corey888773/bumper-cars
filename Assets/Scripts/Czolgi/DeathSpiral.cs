using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Czolgi
{
    public class DeathSpiral : MonoBehaviour
    {
        /*
         * Functions:
         *  MakeSpiral - generates spiral of obstacles frame by frame
         *      arguments:
         *          obstacle - GameObject witch will be ingredient of spiral
         */
        [SerializeField] private GameObject obst;
        [SerializeField] private Transform parent;

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
                    Instantiate(obst, new Vector3(-cameraWidth+rev[0]*0.7f+i*0.7f, cameraHeight-rev[0]*0.7f, 0), Quaternion.identity, parent);
                    yield return null;
                }
                rev[0]++;
                y--;
                
                //right edge of rectangle
                for (float i = 0; i <= y; i++)
                {
                    Instantiate(obst, new Vector3(cameraWidth-rev[1]*0.7f, cameraHeight-0.7f-rev[1]*0.7f-i*0.7f, 0), Quaternion.identity, parent);
                    yield return null;
                }
                rev[1]++;
                x--;
                
                //down edge of rectangle
                for (float i = 0; i <= x; i++)
                {
                    Instantiate(obst, new Vector3(cameraWidth-0.7f-rev[2]*0.7f-i*0.7f, -cameraHeight+rev[2]*0.7f, 0), Quaternion.identity, parent);
                    yield return null;
                }
                rev[2]++;
                y--;
                
                //left of rectangle
                for (float i = 0; i <= y; i++)
                {
                    Instantiate(obst, new Vector3(-cameraWidth+rev[3]*0.7f, -cameraHeight+0.7f+rev[3]*0.7f+i*0.7f, 0), Quaternion.identity, parent);
                    yield return null;
                }
                rev[3]++;
                x--;
            }
        }
    }    
}
