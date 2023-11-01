using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealHand : MonoBehaviour
{
    public GameObject rifleBullet;
    public Transform gunPoint;
    private GameObject player;
    public float bulletSpeed = 10;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player1");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (PickupWeapon.Instance.weapon == 0) //no weapon
            {
                return;
            }
            if (PickupWeapon.Instance.weapon == 1) //rifle
            {
                var bullet = Instantiate(rifleBullet, gunPoint.position, Quaternion.identity);
                Vector2 playerScale = player.transform.localScale;
                bullet.transform.right = new Vector2(playerScale.x, 0);
                bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
            }
            if (PickupWeapon.Instance.weapon == 2) //light gun
            {
                var bullet = Instantiate(rifleBullet, gunPoint.position, Quaternion.identity);
                Vector2 playerScale = player.transform.localScale;
                bullet.transform.right = new Vector2(playerScale.x, 0);
                bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
            }
        }
    }
}