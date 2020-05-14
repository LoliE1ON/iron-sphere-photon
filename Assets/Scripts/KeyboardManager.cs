using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardManager : MonoBehaviour {

    public RectTransform trackKeysContainer;
    private List<KeyCode> keyMappings;
    
    void Start() {

        this.keyMappings = new List<KeyCode>() {
            { KeyCode.W },
            { KeyCode.A },
            { KeyCode.S },
            { KeyCode.D },
            { KeyCode.LeftShift },
        };
    }
    
    void Update() {
        this.TrackKeys();
    }

    private void TrackKeys() {
        
        foreach (var key in this.keyMappings) {
            if (Input.GetKeyDown(key)) {
                Image image = this.getImageKey(key.ToString());
                Color color = Color.white;
                color.a = 1f;
                image.color = color;
            }
            if (Input.GetKeyUp(key)) {
                Image image = this.getImageKey(key.ToString());
                Color color = Color.white;
                color.a = 0.3f;
                image.color = color;
            }
        }
    }

    private Image getImageKey(string name) {
        return this.trackKeysContainer.transform.Find(name).GetComponent<Image>();
    }
}
