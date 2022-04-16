using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rgb;

    void Start() {
        animator = GetComponent<Animator>();
        rgb = GetComponent<Rigidbody2D>();
    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag != "Player") {
            animator.SetTrigger("explosion");
            rgb.velocity = Vector2.zero;
        }
    }

    private void DestroyBullet() {
        Destroy(gameObject);
    }
}