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

    private Vector2Int endOfPath, position, beginningOfPath;
    private int lastValue = 2;

    private bool isUpdated = false;

    public bool isReady()
    {
        return isUpdated;
    }

    public Vector2Int getBeginningOfPathInt()
    {
        return beginningOfPath;
    }

    public Vector2Int getEndOfPathInt()
    {
        return endOfPath;
    }

    public Vector2Int findNextTilePos()
    {
        Vector2Int pos = beginningOfPath, dr = new Vector2Int(1, 0), best_pos = beginningOfPath;
        bool znaleziono = false;

        for(int i = 0; i < 4; i++)
        {
            pos = beginningOfPath + dr;

            if(mapa.canGoFromTo(beginningOfPath, pos) && paths[pos.x, pos.y] != 0 && (!znaleziono || paths[best_pos.x, best_pos.y] > paths[pos.x, pos.y]))
            {
                best_pos = pos;
                znaleziono = true;
            }

            int tmp = dr.x;
            dr.x = -dr.y;
            dr.y = tmp;
        }

        return best_pos;
    }

    // set new path beginning if possible
    public void nextPosition()
    {
        Vector2Int nextPos = findNextTilePos();

        if(nextPos == beginningOfPath)
            return;

        paths[beginningOfPath.x, beginningOfPath.y] = 0;
        removePathTile(beginningOfPath.x, beginningOfPath.y);
        beginningOfPath = nextPos;
    }

    private void newPathTile(int i, int j)
    {
        GameObject new_tile = Instantiate(pathTile, mapa.getPosInLabyrinth(new Vector2Int(i, j)),  Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        new_tile.transform.parent = tiles.transform;
        new_tile.transform.localScale *= mapa.getScalingRatio();
        new_tile.transform.position = new Vector3(new_tile.transform.position.x, new_tile.transform.position.y, 8.0f);
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

        beginningOfPath = position = endOfPath = (new Vector2Int(kx, ky)) / 2;
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

