using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks {

    public GameObject playerPrefab;
    public PlayerUI playerUi;
    
    private void Start() {
        PhotonNetwork.Instantiate(this.playerPrefab.name, Vector3.zero, Quaternion.identity);
    }

    public void Leave() {
        PhotonNetwork.LeaveRoom();
    }
    
    public override void OnLeftRoom() {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        this.playerUi.refreshPlayers();
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        this.playerUi.refreshPlayers();
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
}
