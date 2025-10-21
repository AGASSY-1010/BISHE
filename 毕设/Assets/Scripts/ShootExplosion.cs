using UnityEngine;

public class ShootExplosion : MonoBehaviour
{
    [Header("爆炸特效预制体")]
    public GameObject explosionPrefab; // 在Inspector中分配你的爆炸特效Prefab

    [Header("玩家角色")]
    public Transform player; // 拖拽你的玩家角色到这里

    [Header("特效生成位置偏移")]
    public Vector3 spawnOffset = new Vector3(0, 0, 0); // 可以微调生成位置

    [Header("特效持续时间后自动销毁")]
    public float destroyDelay = 3.0f; // 根据你的特效持续时间设置

    void Update()
    {
        // 检测鼠标左键是否在当前帧按下
        if (Input.GetMouseButtonDown(0))
        {
            SpawnExplosionAtPlayer();
        }
    }

    void SpawnExplosionAtPlayer()
    {
        // 检查玩家角色是否已分配
        if (player == null)
        {
            Debug.LogError("玩家角色未分配！请将玩家角色拖拽到脚本的Player字段。");
            return;
        }

        // 在玩家位置生成爆炸特效
        Vector3 spawnPosition = player.position + spawnOffset;
        GameObject explosion = Instantiate(explosionPrefab, spawnPosition, Quaternion.identity);

        // 在特效播放完毕后自动销毁实例
        Destroy(explosion, destroyDelay);
    }
}