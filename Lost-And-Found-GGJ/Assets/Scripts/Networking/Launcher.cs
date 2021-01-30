using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class Launcher : MonoBehaviourPunCallbacks
{
    private static Launcher _instance;
    public static Launcher Instance { get { return _instance; } }

    private string gameVersion = "1";

    [Tooltip("Maximum number of players per room.")]
    [SerializeField]
    private byte maxPlayerPerRoom= 2;

    private string roomName;

    public string RoomName { get { return roomName; } set { roomName = value; } }

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

        PhotonNetwork.AutomaticallySyncScene = true;

        DontDestroyOnLoad(this);
    }
        
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            if(!string.IsNullOrEmpty(roomName))
            {
                UIHandler.Instance.TogglePreConnectionUI();
                UIHandler.Instance.ToggleConnectingUI();
                PhotonNetwork.JoinRoom(roomName);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(roomName))
            {
                UIHandler.Instance.TogglePreConnectionUI();
                UIHandler.Instance.ToggleConnectingUI();
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Launcher: ConnectedToMaster was called by PUN");
            
        if(!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Launcher: OnDisconnected was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Launcher: OnJoinRoomFailed was called by PUN. No random room available, so we create one. /nCalling: PhotonNetwork.CreateRoom");
            
        if(!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayerPerRoom });
        }
    }

    public override void OnJoinedRoom()
    {
        UIHandler.Instance.ToggleConnectingUI();
        UIHandler.Instance.TogglePostConnectionUI();
        Debug.Log("PUN Launcher: OnJoinedRoom() called by PUN. Now this client is in room: " + roomName);
    }

    public void LoadScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }     
}

