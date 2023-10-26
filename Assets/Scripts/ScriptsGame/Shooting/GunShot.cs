using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    Rigidbody2D rb2D;
    float bulletSpeed = 10f;
    public GameObject hand;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(0, 1);
        transform.position = hand.transform.position;
        transform.rotation = hand.transform.rotation;
    }

    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }
}
