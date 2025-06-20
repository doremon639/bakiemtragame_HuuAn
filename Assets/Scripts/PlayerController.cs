using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 15f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isJumping; // Thêm cờ này để điều khiển animation nhảy

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
        UpdateScoreUI();
        isJumping = false;
    }

    void Update()
{
    float moveInput = Input.GetAxisRaw("Horizontal");

    rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

    if (moveInput < 0)
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    else if (moveInput > 0)
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    if (isGrounded && isJumping)
    {
        isJumping = false;
    }

    //  Nhảy
    if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isJumping = true;
        FindObjectOfType<SoundManager>()?.PlayJump();
    }

    //Cập nhật animation
    animator.SetBool("isRunning", moveInput != 0);
    animator.SetBool("isJumping", isJumping);
}


    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Mushroom"))
{
    transform.localScale = new Vector3(1.5f, 1.5f, 1f);
    Debug.Log("Scale sau khi ăn nấm: " + transform.localScale);
    Destroy(other.gameObject);
    return;
}


    // Ăn xu
    if (other.CompareTag("Coin"))
    {
        score += 4;
        UpdateScoreUI();
        Destroy(other.gameObject);
    }
    // Va vào chướng ngại vật
    else if (other.CompareTag("Obstacle"))
    {
        Debug.Log("Player va chạm với chướng ngại vật!");
        GameController.instance?.GameOver();
        gameObject.SetActive(false); // <-- Player bị tắt sau xử lý
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
