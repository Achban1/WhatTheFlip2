using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb1 : MonoBehaviour
{
    private RotationController rotationController;
    void Awake()
    {
        rotationController = GetComponent<RotationController>();
        Invoke(nameof(CallFlip), 50f);
    }
    private void CallFlip()
    {
        Debug.Log(transform.position.y);
        if (transform.position.y > -30)
        {
            RotationController.Instance.StartFlip();
            Destroy(gameObject);
            Debug.Log("flip");
        }
    }
}
