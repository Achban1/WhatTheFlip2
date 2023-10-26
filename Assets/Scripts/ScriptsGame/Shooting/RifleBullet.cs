using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : MonoBehaviour
{
    float bulletSpeed = 18f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.right * bulletSpeed * Time.deltaTime;
    }
}
