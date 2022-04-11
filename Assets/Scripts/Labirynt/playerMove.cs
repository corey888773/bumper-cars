using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    private mapgen mapa;
    private movingScript pathsScript;

    private GameObject players, labyrinth;

    private bool isUpdated = false;

    private Vector2Int positionIntCurrentGoal;
    private Vector2 positionCurrentGoal;
    
    private void initialize()
    {
        transform.localScale *= mapa.getScalingRatio();
        transform.position = mapa.getPosInLabyrinth(pathsScript.getBeginningOfPathInt());
        positionCurrentGoal = transform.position;
        positionIntCurrentGoal = pathsScript.getBeginningOfPathInt();
    }

    private void updateMove()
    {
        Vector2Int positionIntNow = pathsScript.getBeginningOfPathInt(), positionIntNewCurrentGoal = pathsScript.findNextTilePos();
        bool wracac = false;

        if(positionIntCurrentGoal != positionIntNewCurrentGoal)
        {
            positionCurrentGoal = mapa.getPosInLabyrinth(positionIntNow);
            wracac = true;
        }
        else
            positionCurrentGoal = mapa.getPosInLabyrinth(positionIntNewCurrentGoal);

        float deltaS = (positionCurrentGoal - ((Vector2) transform.position)).magnitude;
        float deltaParamT = 1.0f;

        if(deltaS <= 0.000001)
        {
            // dotarlismy do celu

            if(!wracac)
                pathsScript.nextPosition();
            positionIntCurrentGoal = positionIntNewCurrentGoal;
        }
        else
            deltaParamT = Mathf.Min(1.0f, speed / deltaS * Time.deltaTime);

        transform.position = Vector2.Lerp(transform.position, positionCurrentGoal, deltaParamT);
    }
    public Vector2Int getPlayerPos(){
        return pathsScript.getBeginningOfPathInt();
    }
    public bool isReady(){
        return isUpdated;
    } 
    // Start is called before the first frame update
    private void Start()
    {
        players = gameObject.transform.parent.gameObject;
        labyrinth = players.transform.parent.gameObject;

        pathsScript = labyrinth.GetComponent<movingScript>();
        mapa = labyrinth.GetComponent<mapgen>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(!isUpdated)
        {
            if(pathsScript.isReady())
            {
                isUpdated = true;
                initialize();
            }
        }
        else
        {
            updateMove();
        }
    }
}
