using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.Tab;
    public GameObject targetObject;
    public Button teleportButton;
    public Button teleportButton1;
    public Button teleportButton2;

    void Start()
    {
        if (teleportButton != null)
        {
            teleportButton.onClick.AddListener(OnTeleportButtonClicked);
        }

        if (teleportButton1 != null)
        {
            teleportButton1.onClick.AddListener(OnTeleportButtonClicked);
        }

        if (teleportButton2 != null)
        {
            teleportButton2.onClick.AddListener(OnTeleportButtonClicked);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }

    public void OnTeleportButtonClicked()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }
}
