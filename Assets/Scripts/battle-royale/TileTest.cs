using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTest : MonoBehaviour {
    public Tilemap tilemap;
    public List<Vector3> tileWorldLocations;

    // Use this for initialization
    void Start () {
        tileWorldLocations = new List<Vector3>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {   
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemap.CellToWorld(localPlace);
            tileWorldLocations.Add(place);
            if (tilemap.HasTile(localPlace))
            {
                tileWorldLocations.Remove(place);
            }
        }
    }  
}