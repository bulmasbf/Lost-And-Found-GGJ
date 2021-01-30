using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class PlayerAssigner : MonoBehaviour
{
    private static PlayerAssigner _instance;
    public static PlayerAssigner Instance { get { return _instance; } }

    PhotonView photonView;

    private string Player1ID;
    private string Player2ID;

    public bool P1AssignedBool = false;
    public bool P2AssignedBool = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        photonView = PhotonView.Get(this);

        DontDestroyOnLoad(this);
    }

    private void SendRPCMethod(string methodName, RpcTarget targets, string arg)
    {
        photonView.RPC(methodName, targets, arg);
    }

    public void AssignPlayer1()
    {
        if (P1AssignedBool)
        {
            return;
        }

        if(string.IsNullOrEmpty(Player1ID))
        {
            Player1ID = PhotonNetwork.LocalPlayer.UserId;
            UIHandler.Instance.TogglePlayerAssignedSprite(1);
            P1AssignedBool = true;
            SendRPCMethod(nameof(SendPlayer1Assignment), RpcTarget.Others, Player1ID);

            if(P1AssignedBool && P2AssignedBool)
            {
                UIHandler.Instance.ToggleStartGameButton();
            }
            
        }
    }
    
    [PunRPC]
    public void SendPlayer1Assignment(string sentId)
    {
        if (P1AssignedBool)
        {
            return;
        }

        if (string.IsNullOrEmpty(Player1ID))
        {
            Player1ID = sentId;
            UIHandler.Instance.TogglePlayerAssignedSprite(1);
            P1AssignedBool = true;

            if (P1AssignedBool && P2AssignedBool)
            {
                UIHandler.Instance.ToggleStartGameButton();
            }
        }
    }

    public void AssignPlayer2()
    {
        if (P2AssignedBool)
        {
            return;
        }

        if (string.IsNullOrEmpty(Player2ID))
        {
            Player2ID = PhotonNetwork.LocalPlayer.UserId;
            UIHandler.Instance.TogglePlayerAssignedSprite(2);
            P2AssignedBool = true;
            SendRPCMethod(nameof(SendPlayer2Assignment), RpcTarget.Others, Player2ID);

            if (P1AssignedBool && P2AssignedBool)
            {
                UIHandler.Instance.ToggleStartGameButton();
            }
        }
    }

    [PunRPC]
    public void SendPlayer2Assignment(string sentId)
    {
        if (P2AssignedBool)
        {
            return;
        }

        if (string.IsNullOrEmpty(Player2ID))
        {
            Player2ID = sentId;
            UIHandler.Instance.TogglePlayerAssignedSprite(2);
            P2AssignedBool = true;

            if (P1AssignedBool && P2AssignedBool)
            {
                UIHandler.Instance.ToggleStartGameButton();
            }
        }
    }

    public void BeginGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Only the master client is allowed to start the game.");
        }

        Launcher.Instance.LoadScene("Game");
    }
}
