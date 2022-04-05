using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingScript : MonoBehaviour
{
    private GameObject Labyrinth;
    private bool[,] leftWalls, topWalls;
    private Vector2 start;
    private float blockLength;
    private int kx, ky;
    private int [,] paths;
    // Start is called before the first frame update
    void Start()
    {
        //getting important map values 
        Labyrinth = GameObject.Find("Labyrinth");
        mapgen mapa = Labyrinth.GetComponent<mapgen>();
        leftWalls = mapa.getLeftWalls();
        topWalls = mapa.getTopWalls();
        start = mapa.getStart();
        blockLength = mapa.getBlockLength();
        kx = mapa.getKX();
        ky = mapa.getKY();
        paths = new int[kx,ky];
    }
    for(int i = 0; i < kx; i++)
        for(int j = 0; j < ky; j++)
            paths[i,j]=0;
    private Vector2Int endOfPath, position;
    private int lastValue = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            bool canGo;
            Touch touch = Input.GetTouch(0);
            position = cam_.ScreenToWorldPoint(touch.position);
            position.x = (position.x - start.x) / blockLength;
            position.y = (position.y - start.y) / blockLength;
            //checking if neighbour tile and no wall between them
            if ( position.x >=0 && position.x < kx && position.y >=0 && position.y < ky ) 
            { 
                if ( abs(position.x-endOfPath.x) + abs(position.y-endOfPath.y) == 1 ){
                    if(position.x != endOfPath.x){
                        if(position.x > endOfPath.x && !leftWalls[position.x, position.y])
                            canGo=true;
                        else if (position.x < endOfPath.x && !leftWalls[endOfPath.x, endOfPath.y])
                            canGo=true;
                    }
                    else if(position.y != endOfPath.y){
                        if(position.y > endOfPath.y && !topWalls[endOfPath.x, endOfPath.y])
                            canGo=true;
                        else if(position.y < endOfPath.y && !topWalls[position.x,position.y])
                            canGo=true;
                    } 
                }
                if(canGo){
                    if(paths[position.x,position.y] != 0){
                        int dx = 1, dy = 0, t, wartosc = paths[position.x, position.y];
                        bool poprzedni = true;

                        for(int i = 0; i < 4; i++)
                        {
                            int wspx = endOfPath.x + dx, wspy = endOfPath.y + dy;

                            if(wspx >= 0 && wspx < kx && wspy >= 0 && wspy < ky && paths[wspx, wspy] > wartosc)
                            {
                                poprzedni = false;
                                break;
                            }
                            
                            t = dx;
                            dx = -dy;
                            dy = t;
                        }

                        if(poprzedni)
                        {
                            // usunac
                        }
                    }
                }
            }
            //if moving
            if (touch.phase == TouchPhase.Moved)
            {
                
            }
        }
    }

}

