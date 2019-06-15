using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OranUnityUtils;

public class ODG_Movement : MonoBehaviour {

	public Rigidbody main_rb;
    private bool isTouchingGround;
    public float speed = 1f;
	public float max_speed = 100f;
    public float jumpForce = 1f;

    // Use this for initialization
    void Start () {
		var collisionBitch = main_rb.gameObject.AddOrGetComponent<CollisionBitch>();
		collisionBitch.OnTriggerEnter_fn += On_MainRb_TriggerEnter;
		collisionBitch.OnTriggerExit_fn += On_MainRb_TriggerExit;
	}
	
	void On_MainRb_TriggerEnter(Collider other){
		if(other.tag == "Ground"){
			isTouchingGround = true;
		} 
	}

	void On_MainRb_TriggerExit(Collider other){
		if(other.tag == "Ground"){
			isTouchingGround = false;
		} 
	}

	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

        Vector3 movement = (this.transform.forward * v) + (this.transform.right * h);
		movement = movement * speed;

		if(main_rb.velocity.magnitude < max_speed){
        	// main_rb.AddForce(movement);
			main_rb.velocity += movement;
			main_rb.velocity = main_rb.velocity.ClampXZ(-max_speed, max_speed);
			Debug.Log("Velocity = "+main_rb.velocity.magnitude);
		}

		if(isTouchingGround){
			if(Input.GetKeyDown(KeyCode.Space)){
				Jump();
			}
		}
	}

	void Jump(){
        main_rb.AddForce(Vector3.up * jumpForce);
	}
}
