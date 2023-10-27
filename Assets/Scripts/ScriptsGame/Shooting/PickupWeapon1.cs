using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    private PickupRifle pickuprifle;
    public Transform realHand;
    public GameObject rifle;
    //private Bomb bomb;
    //private GrenadeWeapon grenade;

    //private Transform handController;

    void Start()
    {
        pickuprifle = GameObject.FindObjectOfType<PickupRifle>();
        //bomb = GameObject.FindObjectOfType<Bomb>();
        //grenade = GameObject.FindObjectOfType<GrenadeWeapon>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PickupRifle>() != null)
        {
            var hej = transform.localScale;
            var rotationY = hej.x > 0 ? 0 : 180; // Check if local scale is positive or negative
            var newRotation = Quaternion.Euler(0, rotationY, 0);
            var newRifle = Instantiate(rifle, realHand.position, newRotation);
            newRifle.transform.parent = realHand;
            Destroy(other.gameObject);
        }



        else if (other.gameObject.GetComponent<Bomb>() != null)
        {
        }
    }
}