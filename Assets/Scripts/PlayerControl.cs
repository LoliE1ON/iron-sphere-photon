using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
    
    // Controll
    public float accelerationForce = 10f;
    public float dragDividend = 100f;
    private Rigidbody rigidBody;
    private Dictionary<KeyCode, Vector3> keyMappings;
    
    // Camera
    private GameObject camera;

    // Player UI
    public Transform ui;
    public Text userName;
    
    // Photon
    private PhotonView view;
    
    private LensDistortion lensDistortion;
    
    public void Start()
    {

        this.camera = Camera.main.transform.gameObject;
        this.rigidBody = GetComponent<Rigidbody>();
        this.view = GetComponent<PhotonView>();

        this.userName.text = this.view.Owner.NickName;
        
        // Get Lens distortion
        GameObject pp = GameObject.Find("Post Process Volume");
        if (pp) {
            LensDistortion tmp;
            pp.GetComponent<Volume>().profile.TryGet<LensDistortion>(out tmp);
            this.lensDistortion = tmp;
        }

    }

    public void FixedUpdate()
    {

        this.ui.position = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
        this.ui.LookAt(new Vector3(this.camera.transform.position.x, this.transform.position.y ,this.camera.transform.position.z));
        
        if (this.view.IsMine)
        {
            
            // Max speed
            if(this.rigidBody.velocity.magnitude > 20) {
                this.rigidBody.velocity = this.rigidBody.velocity.normalized * 20;
            }
            
            // Lens distortion
            this.lensDistortion.intensity.value = -this.rigidBody.velocity.magnitude / 35;

            float drag, force;
            if (Input.GetKey(KeyCode.LeftShift)) {
                drag = this.dragDividend * 8;
                force = this.accelerationForce * 2;
            } else {
                drag = this.dragDividend;
                force = this.accelerationForce;
            }

            
            // Define directions
            this.keyMappings = new Dictionary<KeyCode, Vector3>()
            {
                {KeyCode.W, camera.transform.TransformDirection(new Vector3(0, 0, 1))},
                {KeyCode.A, camera.transform.TransformDirection(new Vector3(-1, 0, 0))},
                {KeyCode.S, camera.transform.TransformDirection(new Vector3(0, 0, -1))},
                {KeyCode.D, camera.transform.TransformDirection(new Vector3(1, 0, 0))},

            };
            
            // Control
            foreach (KeyValuePair<KeyCode, Vector3> keyMapping in this.keyMappings)
            {
                if (Input.GetKey(keyMapping.Key) && this.IsGrounded())
                {
                    Vector3 objectForce = keyMapping.Value * force;
                    this.rigidBody.drag = objectForce.sqrMagnitude / drag;
                    this.rigidBody.AddForce(objectForce);
                }
            }
            
        }



    }
    
    public bool IsGrounded() {
        return Physics.Raycast(this.transform.position, -Vector3.up, 0.65f);
    }
    
}
