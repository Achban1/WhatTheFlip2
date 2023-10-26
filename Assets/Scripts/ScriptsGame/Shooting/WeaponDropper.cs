using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropper : MonoBehaviour
{
    public GameObject[] weapons;

    //void Start()
    //{
    //    DropWeapon();
    //}

    //private void DropWeapon()
    //{
    //    Debug.Log("Dropping weapon...");
    //    int num = Random.Range(0, weapons.Length);
    //    float screenWidth = Camera.main.orthographicSize * 2 * Screen.width / Screen.height;
    //    float xPos = Random.Range(-screenWidth / 2, screenWidth / 2);
    //    Vector2 spawnPos = new Vector2(xPos, Screen.height * (6 / 7));
    //    Debug.Log("Spawn position: " + spawnPos);
    //    Instantiate(weapons[num], spawnPos, Quaternion.identity);
    //    Invoke(nameof(DropWeapon), 3f);
    //}
    //private void DropWeaponClone()
    //{

    //}
}

