using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviourPunCallbacks {

    public Text totalPlayers;
    public Text listPlayers;
    
    // Fps counter
    public Text fpsCounter;
    private float hudRefreshRate = 1f;
    private float timer;
    
    private void Start() {
        this.refreshPlayers();
    }

    private void Update() {
        
        // FPS
        if (Time.unscaledTime > this.timer) {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            this.fpsCounter.text = fps.ToString();
            this.timer = Time.unscaledTime + this.hudRefreshRate;
        }
    }

    public void refreshPlayers() {
        
        Dictionary<int, Player> players = PhotonNetwork.CurrentRoom.Players;
        
        this.setTotalPlayers(players.Count.ToString());
        this.setListPlayers(this.generateListPlayers(players));
    }
    
    // Generate player list
    private string generateListPlayers(Dictionary<int, Player> players) {
        
        string playersStr = "";
        foreach (KeyValuePair<int,Player> player in players) {
            playersStr += "\n" + player.Value.NickName;
            if (player.Value.IsMasterClient) playersStr += " [MASTER]";
        }

        return playersStr;
    }

    private void setTotalPlayers(string total) {
        this.totalPlayers.text = total;
    }

    private void setListPlayers(string players) {
        this.listPlayers.text = players;
    }
}