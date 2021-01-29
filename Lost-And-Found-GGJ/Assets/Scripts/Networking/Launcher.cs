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

        private string roomName;

        public GameObject preConnectionUI;

        public GameObject connectingUI;

        public GameObject postConnectionUI;

        public TMPro.TextMeshProUGUI postConnectionRoomText;

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                if(!string.IsNullOrEmpty(roomName))
                {
                    TogglePreConnectionUI();
                    ToggleConnectingUI();
                    PhotonNetwork.JoinRoom(roomName);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(roomName))
                {
                    TogglePreConnectionUI();
                    ToggleConnectingUI();
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
            ToggleConnectingUI();
            TogglePostConnectionUI();
            Debug.Log("PUN Launcher: OnJoinedRoom() called by PUN. Now this client is in room: " + roomName);
        }

        public void SetRoomName(string inputRoomName)
        {
            roomName = inputRoomName;
        }

        public void TogglePreConnectionUI()
        {
            if (preConnectionUI.activeInHierarchy)
            {
                preConnectionUI.SetActive(false);
                return;
            }
            else
            {
                preConnectionUI.SetActive(true);
            }
        }

        public void ToggleConnectingUI()
        {
            if (connectingUI.activeInHierarchy)
            {
                connectingUI.SetActive(false);
                return;
            }
            else
            {
                connectingUI.SetActive(true);
            }
        }

        public void TogglePostConnectionUI()
        {
            if (postConnectionUI.activeInHierarchy)
            {
                postConnectionUI.SetActive(false);
                return;
            }
            else
            {
                postConnectionRoomText.text = roomName;
                postConnectionUI.SetActive(true);
            }
        }
    }
}

