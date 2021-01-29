using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    private static UIHandler _instance;

    public static UIHandler Instance { get { return _instance; } }

    public GameObject PreConnectionUI;

    public GameObject ConnectingUI;

    public GameObject PostConnectionUI;

    public GameObject P1AssignedSprite;

    public GameObject P2AssignedSprite;

    public TMPro.TextMeshProUGUI PostConnectionRoomText;

    // Update is called once per frame
    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void SetRoomName(string inputRoomName)
    {
        Launcher.Instance.RoomName = inputRoomName;
    }

    public void TogglePreConnectionUI()
    {
        if (PreConnectionUI.activeInHierarchy)
        {
            PreConnectionUI.SetActive(false);
            return;
        }
        else
        {
            PreConnectionUI.SetActive(true);
        }
    }

    public void ToggleConnectingUI()
    {
        if (ConnectingUI.activeInHierarchy)
        {
            ConnectingUI.SetActive(false);
            return;
        }
        else
        {
            ConnectingUI.SetActive(true);
        }
    }

    public void TogglePostConnectionUI()
    {
        if (PostConnectionUI.activeInHierarchy)
        {
            PostConnectionUI.SetActive(false);
            return;
        }
        else
        {
            PostConnectionRoomText.text = Launcher.Instance.RoomName;
            PostConnectionUI.SetActive(true);
        }
    }

    public void TogglePlayerAssignedSprite(int playerIndex)
    {
        switch (playerIndex)
        {
            case 1:
                if(!P1AssignedSprite.activeInHierarchy)
                {
                    P1AssignedSprite.SetActive(true);
                }
                else
                {
                    P1AssignedSprite.SetActive(false);
                }
                break;
            case 2:
                if (!P2AssignedSprite.activeInHierarchy)
                {
                    P2AssignedSprite.SetActive(true);
                }
                else
                {
                    P2AssignedSprite.SetActive(false);
                }
                break;
        }
    }
}

