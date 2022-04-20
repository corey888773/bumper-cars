using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapgen : MonoBehaviour
{
    [SerializeField] float labyrinthWidth, labyrinthHeight, wallHeight, blockLength, parametrP;
    private Vector2 start = new Vector2();

    private Vector2 halfBlock;

    public GameObject leftWall, topWall, walls, exitTile;
    public int randomSeed;
    private bool[,] leftWalls, topWalls, visited;
    private GameObject twoExitTiles;
    //bloki w pionie i poziomie
    private int kx, ky;
    private float wallWidth;
    private bool isInitialized = false;

    private float scalingRatio = 1;

    [SerializeField] private int middleHoleRadius;
    //pozycja wyjsc pierwszego i drugiego
    Vector2Int first, second;
    private Camera cam_;

    //generates walls
    void labyrinthGenerator()
    {
        //zmienne
        wallWidth = wallHeight + blockLength;

        kx = (int)((labyrinthWidth - wallWidth + blockLength) / blockLength);
        ky = (int)((labyrinthHeight - wallHeight + blockLength) / blockLength);

        start += 0.5f * (new Vector2(labyrinthWidth - kx * blockLength, labyrinthHeight - ky * blockLength));

        leftWalls = new bool[kx + 1, ky + 1];
        topWalls = new bool[kx + 1, ky + 1];
        visited = new bool[kx + 1, ky + 1];
        for (int i = 0; i < kx + 1; i++)
            for (int j = 0; j < ky + 1; j++)
            {
                leftWalls[i, j] = true;
                topWalls[i, j] = true;
                visited[i, j] = false;
            }

        //Tera generujemy labirynt
        //Random.seed = randomSeed;
        Random.InitState(randomSeed);
        middleHole(kx / 2, ky / 2, middleHoleRadius);
        labDigger(kx / 2, ky / 2);

        Vector2 Lwsp = new Vector2(start.x + wallHeight / 2, start.y + wallHeight / 2 + blockLength / 2);
        Vector2 Pwsp = new Vector2(start.x + wallHeight / 2 + blockLength / 2, start.y + wallHeight / 2);
        foreach (Transform child in walls.transform)
        {
            Destroy(child);
        }
        for (int i = 0; i < kx + 1; i++)
            for (int j = 0; j < ky + 1; j++)
            {
                if (leftWalls[i, j] && j < ky)
                {
                    GameObject newWall = Instantiate(leftWall, new Vector2(Lwsp.x + i * blockLength, Lwsp.y + j * blockLength), Quaternion.identity);
                    newWall.transform.parent = walls.transform;
                    newWall.transform.localScale = new Vector3(wallHeight, wallWidth, 1.0f);
                }
                if (topWalls[i, j] && i < kx)
                {
                    GameObject newWall = Instantiate(topWall, new Vector2(Pwsp.x + i * blockLength, Pwsp.y + j * blockLength), Quaternion.identity);
                    newWall.transform.parent = walls.transform;
                    newWall.transform.localScale = new Vector3(wallWidth, wallHeight, 1.0f);
                }
            }
    }
    //dfs generujący korytarze
    void labDigger(int x, int y)
    {
        bool naPlanszy(int x, int y)
        {
            return x >= 0 && y >= 0 && x < kx && y < ky;
        }

        int
            r = (int)Random.Range(0.0f, 4.0f),
            kier = ((int)Random.Range(0.0f, 2.0f)) * 2 - 1;
        int[]
            dxKier = { 1, 0, -1, 0 },
            dyKier = { 0, 1, 0, -1 };

        visited[x, y] = true;
        for (int i = 0; i < 4; i++)
        {
            if (r > 3)
                r = 0;
            if (r < 0)
                r = 3;
            int xNowy = x + dxKier[r], yNowy = y + dyKier[r];

            if (naPlanszy(xNowy, yNowy) && (!visited[xNowy, yNowy] || Random.Range(0.0f, 1.0f) <= parametrP))
            {
                if (y != yNowy)
                {
                    if (yNowy < y)
                        topWalls[x, y] = false;
                    else
                        topWalls[xNowy, yNowy] = false;
                }
                else
                {
                    if (xNowy < x)
                        leftWalls[x, y] = false;
                    else
                        leftWalls[xNowy, yNowy] = false;
                }

                if (!visited[xNowy, yNowy])
                    labDigger(xNowy, yNowy);
            }

            r += kier;

        }
    }
    public void middleHole(int x, int y, int r)
    {
        int poczX = Mathf.Max(x - r + 1, 0);
        int poczY = Mathf.Max(y - r + 1, 0);
        int konX = Mathf.Min(x + r, kx - 1);
        int konY = Mathf.Min(y + r, ky - 1);

        for (int i = poczX; i <= konX; i++)
        {
            for (int j = poczY; j <= konY; j++)
                leftWalls[i, j] = topWalls[i, j] = false;
            leftWalls[i, y - r] = false;
        }
        for (int j = poczY; j <= konY; j++)
            topWalls[x - r, j] = false;
    }
    public void exitTileGen(){
        first = new Vector2Int(kx-1, ky/2); 
        second = new Vector2Int(0, ky/2);
        GameObject firstExitTile = Instantiate(exitTile, getPosInLabyrinth(first), Quaternion.identity);
        firstExitTile.transform.parent = twoExitTiles.transform;
        firstExitTile.transform.localScale *= scalingRatio;
        GameObject secondExitTile = Instantiate(exitTile, getPosInLabyrinth(second), Quaternion.identity);
        secondExitTile.transform.parent = twoExitTiles.transform;
        secondExitTile.transform.localScale *= scalingRatio;
    }
    //GETTERS
    public Vector2 getStart()
    {
        return start;
    }

    public bool canGoFromTo(Vector2Int from, Vector2Int to)
    {
        if(to.x < 0 || to.x >= kx || to.y < 0 || to.y >= ky || Mathf.Abs(to.x - from.x) > 1 || Mathf.Abs(to.y - from.y) > 1)
            return false;

        return (from.x > to.x && !leftWalls[from.x, from.y]) || (from.x < to.x && !leftWalls[to.x, to.y]) ||
                (from.y > to.y && !topWalls[from.x, from.y]) || (from.y < to.y && !topWalls[to.x, to.y]);
    }

    public bool[,] getTopWalls()
    {
        return topWalls;
    }
    public bool[,] getLeftWalls()
    {
        return leftWalls;
    }
    public float getBlockLength()
    {
        return blockLength;
    }
    public int getKX()
    {
        return kx;
    }
    public int getKY()
    {
        return ky;
    }

    public float getWallHeight()
    {
        return wallHeight;
    }

    public float getScalingRatio()
    {
        return scalingRatio;
    }

    public Vector2 getPosInLabyrinth(Vector2Int posInt)
    {
        return (new Vector2(posInt.x * blockLength + wallHeight * 0.5f, posInt.y * blockLength + wallHeight * 0.5f)) + start + halfBlock;
    }
    public Vector2Int firstExit(){
        return first;
    }
    public Vector2Int secondExit(){
        return second;
    }
    //onStart
    void Start()
    {
        //SCALING
        cam_ = GameObject.Find("Main Camera").GetComponent<Camera>();
        twoExitTiles = GameObject.Find("twoExitTiles");
        Vector2 screenSize = cam_.ScreenToWorldPoint(new Vector2(Screen.safeArea.width, Screen.safeArea.height));

        float screenRatio = screenSize.y / screenSize.x;
        float labyrinthRatio = labyrinthHeight / labyrinthWidth;

        if (screenRatio > labyrinthRatio)
            scalingRatio = screenSize.x / labyrinthWidth;
        else
            scalingRatio = screenSize.y / labyrinthHeight;
        scalingRatio *= 2.0f;

        labyrinthHeight *= scalingRatio;
        labyrinthWidth *= scalingRatio;
        blockLength *= scalingRatio;
        wallHeight *= scalingRatio;

        halfBlock = (new Vector2(blockLength, blockLength)) * 0.5f;

        walls = GameObject.Find("Walls");
        start = new Vector2(transform.position.x - 0.5f * labyrinthWidth, transform.position.y - 0.5f * labyrinthHeight);
        labyrinthGenerator();
        exitTileGen();
        isInitialized = true;
    }

    public bool isReady()
    {
        return isInitialized;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
