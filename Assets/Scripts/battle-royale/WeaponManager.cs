using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weaponPrefab;
    public List<Weapon> weapons;
    public float Count;
    public int weaponCount = 0;
    public int maxWeaponsAmount = 4;
    private Camera _camera;
    public Tilemap tilemap;
    public List<Vector3> tileWorldLocations;

    void Start()
    {
        _camera = Camera.main;
        weaponPrefab = new List<GameObject>(Resources.LoadAll<GameObject>("battle-royale"));
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
    public void GenerateRandomSpot()
    {
        var random = new System.Random();
        int choice = random.Next(tileWorldLocations.Count);
        //specifies the amount of holes
        if (weaponCount < maxWeaponsAmount)
        {
            InstantiateWeapon(tileWorldLocations[choice]);
        }
    }
   
    
    //spawn boosts in previously generated position
    private void InstantiateWeapon(Vector3 position)
    {
        var weaponPicker = Random.Range(0, 3);
        var weapon = weaponPrefab[weaponPicker];
        Instantiate(weapon, position, Quaternion.identity);
        weaponCount += 1;
        print(weaponCount);
    }

    
    public void SpawnWeapon()
    {
        // spawning delay
        Count -= Time.deltaTime; 
        if (Count <= 0)
        {
            GenerateRandomSpot();
            Count = 3f;
        }
    }
}