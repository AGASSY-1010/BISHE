using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    [Header("地面检测")]
    public Transform groundCheck;
    public float checkDistance = 0.2f;
    public LayerMask groundLayer;

    // 3D组件引用
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // 获取3D刚体组件
        rb = GetComponent<Rigidbody>();

        // 锁定旋转，防止角色翻滚
        rb.freezeRotation = true;
    }

    void Update()
    {
        // 地面检测（使用3D物理）
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, checkDistance, groundLayer);

        // 水平移动（在3D空间中限制在2D平面）
        float move = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(move * moveSpeed, rb.velocity.y, 0);
        rb.velocity = movement;

        // 跳跃
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // 翻转角色朝向
        if (move > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // 可视化调试
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(groundCheck.position, Vector3.down * checkDistance);
    }
}