using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    public static PickupWeapon Instance {  get; private set; }
    private PickupRifle pickuprifle;
    public Transform realHand;
    public GameObject rifle;
    public int weapon;
    //private Bomb bomb;
    //private GrenadeWeapon grenade;

    //private Transform handController;

    void Start()
    {
        Instance = this;
        pickuprifle = GameObject.FindObjectOfType<PickupRifle>();
        //bomb = GameObject.FindObjectOfType<Bomb>();
        //grenade = GameObject.FindObjectOfType<GrenadeWeapon>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PickupRifle>() != null)
        {
            //weapon = 1;
            var hej = transform.localScale;
            var rotationY = hej.x > 0 ? 0 : 180; // Check if local scale is positive or negative
            var newRotation = Quaternion.Euler(0, rotationY, 0);
            var newRifle = Instantiate(rifle, realHand.position, newRotation);
            newRifle.transform.parent = realHand;
            Destroy(other.gameObject);
        }

        else if (other.gameObject.GetComponent<Bomb>() != null)
        {
            //weapon = 2;
        }
        else
        {
            //weapon = 0;
        }
    }
}