using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int health;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            health--;
            Destroy(other.gameObject);
            if (health == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
