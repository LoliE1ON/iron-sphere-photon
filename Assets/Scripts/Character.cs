using System;
using Photon.Pun;
using UnityEngine;

public class Character : MonoBehaviour {

    public Material[] characterMaterials;
    
    private void Start() {
        
        PhotonView view = this.GetComponent<PhotonView>();
        MeshRenderer renderer = this.GetComponent<MeshRenderer>();

        int characterType = (int) view.Owner.CustomProperties["character"];
        
        switch (characterType) {
            case (int) Types.CharacterEnum.Iron:
                renderer.material = this.getMaterialByName(Types.CharacterEnum.Iron.ToString());
                break;
            case (int) Types.CharacterEnum.Knuckles:
                renderer.material = this.getMaterialByName(Types.CharacterEnum.Knuckles.ToString());
                break;
        }
        
    }

    private Material getMaterialByName(string name) {
        foreach (Material material in this.characterMaterials) {
            if (material.name == name) {
                return material;
            }
        }
        return this.characterMaterials[0];
    }
    
}
