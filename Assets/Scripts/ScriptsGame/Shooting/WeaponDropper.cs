using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropper : MonoBehaviour
{
    public GameObject[] weapons;

    void Start()
    {
        DropWeapon();
    }

    private void DropWeapon()
    {
        int num = Random.Range(0, weapons.Length);
        float screenWidth = Camera.main.orthographicSize * 2 * Screen.width / Screen.height;
        float xPos = Random.Range(-screenWidth / 2, screenWidth / 2);
        Vector2 spawnPos = new Vector2(xPos, 10);
        Instantiate(weapons[num], spawnPos, Quaternion.identity);
        Invoke(nameof(DropWeapon), 7f);
    }

}

