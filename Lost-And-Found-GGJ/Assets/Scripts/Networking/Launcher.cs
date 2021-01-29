using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace AL.AsAbove
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private string gameVersion = "1";

        [Tooltip("Maximum number of players per room.")]
        [SerializeField]
        private byte maxPlayerPerRoom= 2;

        [Tooltip("Name of the room to connect to.")]
        [SerializeField]
        private string roomName = "Test Room";

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            Connect();
        }

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRoom(roomName);
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN Launcher: ConnectedToMaster was called by PUN");
            PhotonNetwork.JoinRoom(roomName);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("PUN Launcher: OnDisconnected was called by PUN with reason {0}", cause);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Launcher: OnJoinRandomFailed was called by PUN. No random room available, so we create one. /nCalling: PhotonNetwork.CreateRoom");
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayerPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Launcher: OnJoinedRoom() called by PUN. Now this client is in room: " + roomName);
        }
    }
}

