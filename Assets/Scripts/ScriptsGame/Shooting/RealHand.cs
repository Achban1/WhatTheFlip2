using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealHand : MonoBehaviour
{
    //fire bullets from this script

    public GameObject rifleBullet;
    private Transform parentTransform;
    public GameObject parent;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //Vector2 bulletOffset = new Vector2(0, 0.68f);
            Vector2 bulletOffset = new Vector2(0, 0f);
            if (transform.localScale.x > 0)
            {
                Instantiate(rifleBullet, (Vector2)parent.transform.position + bulletOffset, Quaternion.Euler(0, 0, 180));
            }
            else
            {
                Instantiate(rifleBullet, (Vector2)transform.position - bulletOffset, Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
