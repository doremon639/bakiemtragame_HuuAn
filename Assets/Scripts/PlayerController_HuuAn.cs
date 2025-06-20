using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 15f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Scoring")]
    public int score = 0;
    public Text scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        UpdateScoreUI(); // Gọi cập nhật điểm ngay từ đầu
    }

    void Update()
    {
        // Di chuyển trái/phải
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Đổi hướng nhân vật
        if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);

        // Kiểm tra chạm đất
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Nhảy
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            FindObjectOfType<SoundManager>()?.PlayJump();
        }

        // Cập nhật trạng thái animator
        animator.SetBool("isRunning", moveInput != 0);
        animator.SetBool("isJumping", !isGrounded);
    }

   void OnTriggerEnter2D(Collider2D other)
{
    // Nếu đụng phải Coin
    if (other.CompareTag("Coin"))
    {
        score += 4;
        UpdateScoreUI();
        Destroy(other.gameObject);
    }
    // Nếu đụng phải vật cản hoặc quái
    else if (other.CompareTag("Obstacle"))
    {
        Debug.Log("Player va chạm với chướng ngại vật!");
        if (GameController.instance != null)
        {
            GameController.instance.GameOver();
        }
        gameObject.SetActive(false);
    }
    // Nếu đụng phải nấm
    else if (other.CompareTag("Mushroom"))
    {
        // Phóng to player
        transform.localScale = new Vector3(2f, 2f, 1f); // Tùy chỉnh tỉ lệ phóng to
        Destroy(other.gameObject); // Xóa nấm sau khi ăn
    }
}

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Điểm: " + score.ToString();
        }
    }
}
