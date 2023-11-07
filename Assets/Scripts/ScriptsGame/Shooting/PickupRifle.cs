using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRifle : MonoBehaviour
{
    public GameObject rifleBullet;
    private Transform theHand;
    private Transform player;

    private bool isPickedUp;
    private Rigidbody2D rb;
    private Transform rifleTransform;

    private Vector2 rifleDir;
    private Vector2 playerDir;
    private Quaternion rot;

    void Start()
    {
        //gunPoint = transform.GetChild(0);
        player = GameObject.FindGameObjectWithTag("Player1").transform;
        rb = GetComponent<Rigidbody2D>();
        rifleTransform = GetComponent<Transform>();
        rot = transform.rotation;
        rifleDir = transform.localScale;
        playerDir = player.transform.localScale;
    }

    public void FlipRifle()
    {
        rifleDir = transform.localScale;
        rifleDir.x *= -1;
        transform.localScale = rifleDir;
    }

    public void MoveRifle(Transform hand)
    {
        if (theHand == null)
        {
            theHand = hand;
        }
        isPickedUp = true;

        transform.SetParent(theHand, true);
        transform.position = hand.position;
    }
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
