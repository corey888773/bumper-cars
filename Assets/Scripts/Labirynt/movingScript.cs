using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingScript : MonoBehaviour
{
    private GameObject Labyrinth;
    private bool[,] leftWalls, topWalls;
    private Vector2 halfBlock;
    private float blockLength, wallHeight;
    private int kx, ky;
    private int[,] paths;
    private Camera cam_;
    private mapgen mapa;
    [SerializeField] private GameObject pathTile;
    private GameObject tiles;

    private Vector2Int endOfPath, position;
    private int lastValue = 2;

    private bool isUpdated = false;

    void newPathTile(int i, int j)
    {
        GameObject new_tile = Instantiate(pathTile, (new Vector2(i * blockLength + wallHeight * 0.5f, j * blockLength + wallHeight * 0.5f)) + mapa.getStart() + halfBlock, Quaternion.identity);
        new_tile.transform.parent = tiles.transform;
        new_tile.transform.localScale *= mapa.getScalingRatio();
        new_tile.name = "pathTile" + i.ToString() + "," + j.ToString();
    }

    void removePathTile(int i, int j)
    {
        Destroy(GameObject.Find("pathTile" + i.ToString() + "," + j.ToString()));
    }

    void updatePlayerPath()
    {
        if (Input.touchCount > 0)
        {
            bool canGo = false;
            Touch touch = Input.GetTouch(0);
            Vector2 position_got = cam_.ScreenToWorldPoint(touch.position);
            position.x = (int)((position_got.x - mapa.getStart().x) / blockLength);
            position.y = (int)((position_got.y - mapa.getStart().y) / blockLength);

            //checking if neighbour tile and no wall between them
            if (position.x >= 0 && position.x < kx && position.y >= 0 && position.y < ky)
            {
                if (Mathf.Abs(position.x - endOfPath.x) + Mathf.Abs(position.y - endOfPath.y) == 1)
                {
                    if (position.x != endOfPath.x)
                    {
                        if (position.x > endOfPath.x && !leftWalls[position.x, position.y])
                            canGo = true;
                        else if (position.x < endOfPath.x && !leftWalls[endOfPath.x, endOfPath.y])
                            canGo = true;
                    }
                    else if (position.y != endOfPath.y)
                    {
                        if (position.y < endOfPath.y && !topWalls[endOfPath.x, endOfPath.y])
                            canGo = true;
                        else if (position.y > endOfPath.y && !topWalls[position.x, position.y])
                            canGo = true;
                    }
                }

                if (canGo)
                {
                    if (paths[position.x, position.y] != 0)
                    {
                        int dx = 1, dy = 0, value = paths[position.x, position.y];
                        bool canRemove = true;
                        int wspx = endOfPath.x + dx, wspy = endOfPath.y + dy;

                        for (int i = 0; i < 4; i++)
                        {
                            if (wspx >= 0 && wspx < kx && wspy >= 0 && wspy < ky && paths[wspx, wspy] > value)
                            {
                                canRemove = false;
                                break;
                            }

                            int t = dx;
                            dx = -dy;
                            dy = t;

                            wspx = endOfPath.x + dx;
                            wspy = endOfPath.y + dy;
                        }

                        if (canRemove)
                        {
                            paths[endOfPath.x, endOfPath.y] = 0;
                            removePathTile(endOfPath.x, endOfPath.y);

                            endOfPath = position;
                            lastValue--;
                        }
                    }
                    else
                    {
                        paths[position.x, position.y] = lastValue++;
                        endOfPath = position;

                        newPathTile(position.x, position.y);
                    }
                }
            }
            //if moving
            if (touch.phase == TouchPhase.Moved)
            {

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //getting important map values 
        Labyrinth = GameObject.Find("Labyrinth");
        mapa = Labyrinth.GetComponent<mapgen>();
    }

    void initialize()
    {
        leftWalls = mapa.getLeftWalls();
        topWalls = mapa.getTopWalls();
        blockLength = mapa.getBlockLength();
        wallHeight = mapa.getWallHeight();

        halfBlock = (new Vector2(blockLength, blockLength)) * 0.5f;
        kx = mapa.getKX();
        ky = mapa.getKY();
        paths = new int[kx, ky];

        cam_ = GameObject.Find("Main Camera").GetComponent<Camera>();
        for (int i = 0; i < kx; i++)
            for (int j = 0; j < ky; j++)
                paths[i, j] = 0;

        tiles = GameObject.Find("PathTiles");

        position = endOfPath = (new Vector2Int(kx, ky)) / 2;
        paths[position.x, position.y] = 1;
        newPathTile(position.x, position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUpdated)
        {
            if (mapa.isReady())
            {
                isUpdated = true;
                initialize();
            }
        }
        else
        {
            updatePlayerPath();
        }
    }

}

