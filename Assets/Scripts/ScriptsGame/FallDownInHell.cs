using UnityEngine;

public class FallDownInHell : MonoBehaviour
{
    private Vector2 a = new Vector2(0, -44.5f);
    private Vector2 b = new Vector2(0, 15.8f);
    private bool falling = false;
    private float timer = 0f;
    private float lerpDuration = 2.7f;
    private SpriteRenderer spriteRenderer;
    private AudioListener audioListener;

    private void Start()
    {   
        transform.position = a;
        falling = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FallDownBackground()
    {
        if (falling)
        {
            float t = timer / lerpDuration;
            float easedT = EaseInAndOut(0f, 1f, t * 2);
            transform.position = Vector2.Lerp(a, b, easedT);
            timer += Time.deltaTime;
            
        }
        else if (transform.position.y == b.y)
        {
            if (!falling)
            {
                return;
            }
            falling = false;
        }
    }

    private float EaseInAndOut(float edge0, float edge1, float x)
    {
        x = Mathf.Clamp01((x - edge0) / (edge1 - edge0));
        return x * x * (3 - 2 * x);
    }


    private void Update()
    {
        FallDownBackground();
    }
}
