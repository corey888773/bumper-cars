using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Animator _animator;
    private Rigidbody2D _rgb;
    private float _distanceToDeath;

    void Start() {
        _animator = GetComponent<Animator>();
        _rgb = GetComponent<Rigidbody2D>();
        _distanceToDeath = Camera.main.orthographicSize * (Screen.width / Screen.height) * 2;
        StartCoroutine(CheckOutOfTheWorld());
    }

    IEnumerator CheckOutOfTheWorld() {
        while (true) {
            yield return new WaitForSeconds(2);
            if (Vector3.Distance(Vector3.zero, transform.position) > _distanceToDeath)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Player") {
            _animator.SetTrigger("explosion");
            _rgb.velocity = Vector2.zero;
        }
    }

    private void DestroyBullet() {
        Destroy(gameObject);
    }
}