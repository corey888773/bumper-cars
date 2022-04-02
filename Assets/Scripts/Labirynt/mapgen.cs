using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapgen : MonoBehaviour
{
    [SerializeField] float labyrinthWidth, labyrinthHeight, wallHeight, blockLength, parametrP;
    private Vector2 start = new Vector2();
    public GameObject leftWall, topWall;
    public int randomSeed; 
    private bool[,] leftWalls, topWalls, visited;
    //bloki w pionie i poziomie
        private int kx, ky; 
    private float wallWidth; 
    //generates walls
    void labyrinthGenerator(){
        //zmienne
        wallWidth = wallHeight + blockLength;
        kx = (int) ( (labyrinthWidth - wallWidth + blockLength) / blockLength );
        ky = (int) ( (labyrinthHeight - wallHeight + blockLength) / blockLength );

        leftWalls = new bool [kx+1,ky+1];
        topWalls = new bool [kx+1,ky+1];
        visited = new bool [kx+1,ky+1];
        for(int i = 0; i < kx + 1; i++)
            for(int j = 0; j < ky + 1; j++){
                leftWalls[i,j]=true;
                topWalls[i,j]=true;
                visited[i,j] = false;
            }
        
        //Tera generujemy labirynt
        Random.seed = randomSeed;
        labDigger(kx / 2, ky / 2);

        Vector2 Lwsp = new Vector2(start.x+wallHeight/2, start.y + wallHeight/2 + blockLength/2);
        Vector2 Pwsp = new Vector2(start.x+wallHeight/2 + blockLength/2, start.y + wallHeight/2);
        foreach(Transform child in transform){
            Destroy(child);
        }
        for(int i = 0; i < kx + 1; i++)
            for(int j = 0; j < ky + 1; j++){
                if(leftWalls[i,j] && j < ky){
                    GameObject newWall = Instantiate(leftWall,new Vector2(Lwsp.x+i*blockLength, Lwsp.y+j*blockLength),Quaternion.identity);
                    newWall.transform.parent = transform;
                    newWall.transform.localScale = new Vector3(wallHeight, wallWidth, 1.0f);
                }
                if(topWalls[i,j] && i < kx){ 
                    GameObject newWall = Instantiate(topWall,new Vector2(Pwsp.x+i*blockLength, Pwsp.y+j*blockLength),Quaternion.identity);
                    newWall.transform.parent = transform;
                    newWall.transform.localScale = new Vector3(wallWidth, wallHeight, 1.0f);
                }
            } 
    }
    //dfs generujący korytarze
    void labDigger(int x, int y){
        bool naPlanszy(int x, int y){
            return x>=0 && y>=0 && x<kx && y<ky;
        }

        int 
            r = (int) Random.Range(0.0f, 4.0f),
            kier = ((int) Random.Range(0.0f, 2.0f))*2 - 1;
        int[]
            dxKier = {1, 0, -1, 0}, 
            dyKier = {0, 1, 0, -1}; 
        
        visited[x, y] = true;             
        for(int i = 0; i < 4; i++)
        {
            if(r > 3)
            r = 0;
            if(r < 0)
            r = 3;
            int xNowy = x + dxKier[r], yNowy = y + dyKier[r];
            
            if(naPlanszy(xNowy, yNowy) && (!visited[xNowy, yNowy] || Random.Range(0.0f, 1.0f) <= parametrP))
            {     
            if(y != yNowy)
            {
                if(yNowy < y)
                {
                topWalls[x, y] = false;
                }
                else
                {
                topWalls[xNowy, yNowy] = false;
                }
            }
            else
            {
                if(xNowy < x)
                {
                leftWalls[x, y] = false;
                }
                else
                {
                leftWalls[xNowy, yNowy] = false;
                }
            }
            
            if(!visited[xNowy, yNowy])
                labDigger(xNowy, yNowy);
            }
            
            r += kier;
        
    }
}
    

    //onStart
    void Start()
    {
        start = new Vector2(transform.position.x - 0.5f * labyrinthWidth, transform.position.y - 0.5f * labyrinthHeight);
        labyrinthGenerator(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
