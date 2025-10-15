 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamTeleport : MonoBehaviour
{
    [Header("ÐéÄâÉãÏñ»ú")]
    public CinemachineVirtualCamera virtualCamera;

    [Header("YÖµÆ«ÒÆ")]
    public float yOffset = 0f;

    private CinemachineTransposer transposer;
    private Vector3 originalFollowOffset;

    void Start()
    {
        if (virtualCamera != null)
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer != null)
            {
                originalFollowOffset = transposer.m_FollowOffset;
            }
        }
    }

    void Update()
    {
        if (transposer != null)
        {
            Vector3 newOffset = originalFollowOffset;
            newOffset.y = originalFollowOffset.y + yOffset;
            transposer.m_FollowOffset = newOffset;
        }
    }
}
