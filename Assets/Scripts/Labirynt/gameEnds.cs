using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEnds : MonoBehaviour
{
    private GameObject Labyrinth;
    private playerMove changingPosition;
    private mapgen mapa;
    private Vector2Int playerPosition, firstExitTile, secondExitTile;
    private bool win, initialized = false;
    public bool playerWins(Vector2Int position, Vector2Int firstExit, Vector2Int secondExit){
        if(position == firstExit || position == secondExit)
            return true;
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        Labyrinth = GameObject.Find("Labyrinth");
        mapa = Labyrinth.GetComponent<mapgen>();
        changingPosition = gameObject.GetComponent<playerMove>();
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mapa.isReady() && changingPosition.isReady() && !initialized) {
            firstExitTile = mapa.firstExit();
            secondExitTile = mapa.secondExit();
            initialized = true;  
        } else if (initialized) {
            playerPosition = changingPosition.getPlayerPos();
            win = playerWins(playerPosition, firstExitTile, secondExitTile);
            if(win){
                Object.Destroy(Labyrinth);
            }
        } 
    }
}
