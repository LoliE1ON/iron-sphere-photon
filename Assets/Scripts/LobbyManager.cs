using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbyManager : MonoBehaviourPunCallbacks {

    public InputField nickName;
    public Text logText;
    public Text helloText;
    public GameObject intancesListContainer;
    public GameObject instanceUiPrefab;
    
    // Tabs
    public GameObject selectUsername;
    public GameObject lobby;

    public void Connect() {
        
        PhotonNetwork.NickName = this.nickName.text;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.AutomaticallySyncScene = true;
        
        PhotonNetwork.ConnectUsingSettings();
        this.Log("Connection to Photon Server...");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        foreach (var roomInfo in roomList)
        {
            
            // Computed offset height position of instance
            float offsetHeight = -55f * this.intancesListContainer.transform.childCount;

            GameObject newItem = Instantiate(this.instanceUiPrefab, Vector3.zero, Quaternion.identity);
            newItem.transform.Find("Name").GetComponent<Text>().text = roomInfo.Name;
            newItem.transform.Find("TotalPlayers").GetComponent<Text>().text = roomInfo.PlayerCount.ToString();
            newItem.transform.Find("Connect").GetComponent<Button>().onClick.AddListener(delegate() {
                this.JoinRoomByName(roomInfo.Name);
            });
            
            newItem.transform.parent = this.intancesListContainer.transform;
            newItem.GetComponent<RectTransform>().localPosition  = new Vector3(0, offsetHeight, 0);

        }
        
    }

    public override void OnConnectedToMaster() {
        
        PhotonNetwork.JoinLobby();
        
        // Switch tabs
        this.selectUsername.SetActive(false);
        this.lobby.SetActive(true);

        this.helloText.text = "Hello, <b>" + PhotonNetwork.NickName + "</b>!";
        
        this.Log("Connected to Master");
    }

    public void CreateRoom()
    {
        string roomName = "Room #" + ((int)Random.Range(1000f, 99000f)).ToString();
        PhotonNetwork.CreateRoom(roomName, new RoomOptions {
            MaxPlayers = 20,
            IsOpen = true,
            IsVisible = true
        });
    }

    public void JoinRoom() {
        PhotonNetwork.JoinRandomRoom();
    }
    
    public void JoinRoomByName(string name) {
        PhotonNetwork.JoinRoom(name);
    }
    
    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("Game");
    }

    private void Log(string message) {
        Debug.Log(message);
        this.logText.text += "\n" + message;
    }
}
