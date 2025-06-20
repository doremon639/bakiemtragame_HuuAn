using UnityEngine;

public class Quai : MonoBehaviour
{
    public float moveSpeed = 3f;        
    public float moveDistance = 3f;     

    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Điều kiện để Quai không di chuyển khi Game Over
        if (GameController.instance != null && GameController.instance.isGameOver)
        {
            // Dừng di chuyển của quái vật khi game over
            return; 
        }

        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= startPos.x + moveDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= startPos.x - moveDistance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Đảo chiều để lật hướng sprite
        transform.localScale = scale;
    }

}