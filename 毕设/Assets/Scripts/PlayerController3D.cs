using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    [Header("������")]
    public Transform groundCheck;
    public float checkDistance = 0.2f;
    public LayerMask groundLayer;

    // 3D�������
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // ��ȡ3D�������
        rb = GetComponent<Rigidbody>();

        // ������ת����ֹ��ɫ����
        rb.freezeRotation = true;
    }

    void Update()
    {
        // �����⣨ʹ��3D����
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, checkDistance, groundLayer);

        // ˮƽ�ƶ�����3D�ռ���������2Dƽ�棩
        float move = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(move * moveSpeed, rb.velocity.y, 0);
        rb.velocity = movement;

        // ��Ծ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // ��ת��ɫ����
        if (move > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // ���ӻ�����
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(groundCheck.position, Vector3.down * checkDistance);
    }
}