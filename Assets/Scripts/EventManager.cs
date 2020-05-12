using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    public Text playersLog;
    private Dictionary<string, long> playerLog = new Dictionary<string, long>();
    
    private void FixedUpdate() {
        
        if (this.playerLog.Count > 0) {
            
            long nowTimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

            for (int i = 0; i < this.playerLog.Count; i++) {
                if (nowTimeStamp > this.playerLog.ElementAt(i).Value+10) {
                    this.playerLog.Remove(this.playerLog.ElementAt(i).Key);
                }
            }

            this.renderPlayersLog();
        }
    }
    
    private void renderPlayersLog() {
        
        string logList = "";
        foreach (var item in this.playerLog) {
            logList += "\n" + item.Key;
        }
        this.playersLog.text = logList;
    }
    
    public void appendPlayerLog(string username) {
        this.playerLog.Add(username, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
    }
    
}
