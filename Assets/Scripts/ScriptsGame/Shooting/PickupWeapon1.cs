using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    private PickupRifle pickuprifle;
    public GameObject theHand;
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
            var newRifle = Instantiate(rifle, theHand.transform.position, transform.rotation);
            newRifle.transform.parent = theHand.transform;
            Destroy(other.gameObject);
        }

        else if (other.gameObject.GetComponent<Bomb>() != null)
        {
        }
    }
}

