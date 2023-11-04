using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb1 : MonoBehaviour
{
    private RotationController rotationController;
    public GameObject explosion;
    private float delay = 5f;
    void Awake()
    {
        rotationController = GetComponent<RotationController>();
        Invoke(nameof(CallFlip), delay);
        Invoke(nameof(Anim), delay - 0.3f);

    }
    private void Anim()
    {
        if (transform.position.y > -20 && transform.position.y < 5.66f)
        {
            Vector3 offset = new Vector3(0, 2f, 0);
            var expl = Instantiate(explosion, transform.position - offset, Quaternion.identity);
            Destroy(expl, 0.6f);
        }
    }
    private void CallFlip()
    {
        if (transform.position.y > -20 && transform.position.y < 5.66f)
        {
            var allPlayerMovements = FindObjectsOfType<PlayerMovement>(); 
            if (allPlayerMovements != null)
            {
                foreach (var playerMovement in allPlayerMovements)
                {
                    playerMovement.DisableAllMovement();
                    Transform hand = playerMovement.transform.GetChild(0); 
                    if (hand != null)
                    {
                        hand.gameObject.SetActive(false); 
                    }
                }
                RotationController.Instance.StartFlip();
                SoundEffects.instance.BombExplosionSound();

                Destroy(gameObject);
            }
        }
    }


    private void Update()
    {
        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
}
