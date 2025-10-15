using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [Header("传送设置")]
    public float delayTime = 0.5f;

    [Header("黑屏设置")]
    public float fadeInDuration = 0.1f;
    public float fadeOutDuration = 0.1f;
    public float blackScreenDuration = 1f;
    public Image blackScreen;

    [Header("按钮设置")]
    public Button teleportButton1;
    public Button teleportButton2;
    public Button teleportButton3;

    private bool isTeleporting = false;
    private Rigidbody rb;
    private float currentTargetY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (teleportButton1 != null)
        {
            teleportButton1.onClick.AddListener(() => OnTeleportButtonClicked(50f));
        }

        if (teleportButton2 != null)
        {
            teleportButton2.onClick.AddListener(() => OnTeleportButtonClicked(100f));
        }

        if (teleportButton3 != null)
        {
            teleportButton3.onClick.AddListener(() => OnTeleportButtonClicked(0f));
        }

        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true);
            SetBlackScreenAlpha(0f);
        }
    }

    public void OnTeleportButtonClicked(float targetY)
    {
        if (!isTeleporting)
        {
            currentTargetY = targetY;
            StartCoroutine(TeleportRoutine());
        }
    }

    IEnumerator TeleportRoutine()
    {
        isTeleporting = true;

        SetAllButtonsInteractable(false);

        yield return StartCoroutine(FadeToBlack());

        yield return new WaitForSeconds(blackScreenDuration / 2);

        PerformTeleport();

        yield return new WaitForSeconds(blackScreenDuration / 2);

        yield return StartCoroutine(FadeFromBlack());

        SetAllButtonsInteractable(true);

        isTeleporting = false;
    }

    void PerformTeleport()
    {
        Vector3 currentPos = transform.position;
        Vector3 newPos = new Vector3(currentPos.x, currentTargetY, currentPos.z);

        if (rb != null)
        {
            rb.MovePosition(newPos);
            rb.velocity = Vector3.zero;
        }
        else
        {
            transform.position = newPos;
        }
    }

    void SetAllButtonsInteractable(bool interactable)
    {
        if (teleportButton1 != null)
            teleportButton1.interactable = interactable;
        if (teleportButton2 != null)
            teleportButton2.interactable = interactable;
        if (teleportButton3 != null)
            teleportButton3.interactable = interactable;
    }

    IEnumerator FadeToBlack()
    {
        if (blackScreen == null) yield break;

        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            SetBlackScreenAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetBlackScreenAlpha(1f);
    }

    IEnumerator FadeFromBlack()
    {
        if (blackScreen == null) yield break;

        float elapsedTime = 0f;

        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            SetBlackScreenAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetBlackScreenAlpha(0f);
    }

    void SetBlackScreenAlpha(float alpha)
    {
        if (blackScreen != null)
        {
            Color color = blackScreen.color;
            color.a = alpha;
            blackScreen.color = color;
        }
    }
}