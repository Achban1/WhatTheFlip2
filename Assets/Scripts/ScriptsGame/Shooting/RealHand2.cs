using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealHand2 : MonoBehaviour
{
    public GameObject rifleBullet;
    public Transform gunPoint;
    private GameObject player;
    public float bulletSpeed = 10;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player2");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            if (transform.GetChild(1) == null)
            {
                return;
            }
            var bullet = Instantiate(rifleBullet, gunPoint.position, Quaternion.identity);
            Vector2 playerScale = player.transform.localScale;
            bullet.transform.right = new Vector2(playerScale.x, 0);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
        }
    }
}