using UnityEngine;

public class ShootExplosion : MonoBehaviour
{
    [Header("��ը��ЧԤ����")]
    public GameObject explosionPrefab; // ��Inspector�з�����ı�ը��ЧPrefab

    [Header("��ҽ�ɫ")]
    public Transform player; // ��ק�����ҽ�ɫ������

    [Header("��Ч����λ��ƫ��")]
    public Vector3 spawnOffset = new Vector3(0, 0, 0); // ����΢������λ��

    [Header("��Ч����ʱ����Զ�����")]
    public float destroyDelay = 3.0f; // ���������Ч����ʱ������

    void Update()
    {
        // ����������Ƿ��ڵ�ǰ֡����
        if (Input.GetMouseButtonDown(0))
        {
            SpawnExplosionAtPlayer();
        }
    }

    void SpawnExplosionAtPlayer()
    {
        // �����ҽ�ɫ�Ƿ��ѷ���
        if (player == null)
        {
            Debug.LogError("��ҽ�ɫδ���䣡�뽫��ҽ�ɫ��ק���ű���Player�ֶΡ�");
            return;
        }

        // �����λ�����ɱ�ը��Ч
        Vector3 spawnPosition = player.position + spawnOffset;
        GameObject explosion = Instantiate(explosionPrefab, spawnPosition, Quaternion.identity);

        // ����Ч������Ϻ��Զ�����ʵ��
        Destroy(explosion, destroyDelay);
    }
}