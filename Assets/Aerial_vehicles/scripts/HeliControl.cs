using UnityEngine;
using System.Collections;

public class HeliControl : MonoBehaviour {
		float force_k=0f;
		private Vector3 speed;
	private Transform propeller;
	// Use this for initialization
	void Start () {
		propeller = this.transform.FindChild("helicopter").transform.FindChild("prop").transform;//find the propeller to rotate it - you may have to change it for your model
		
	GetComponent<Rigidbody>().centerOfMass = transform.FindChild("Center_of_mass").transform.localPosition;
	}
	
		void OnGUI(){
		GUI.Label(new Rect(10,50,100,20),"SPACE - Gas");
		GUI.Label(new Rect(10,70,100,20),"W/S - Pitch");
		GUI.Label(new Rect(10,90,100,20),"A/D - Roll");
		GUI.Label(new Rect(10,110,100,20),"Q/E - Yaw");
		
		
	}
	
	// Update is called once per frame
	void Update () {
	propeller.localEulerAngles += new Vector3(0f,force_k*30f,0f);
			
	this.GetComponent<Rigidbody>().AddForce(Vector3.up*-300f);  // Extra Gravity
		
		
	if (!System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.x) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.y) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.z)){
			speed=this.GetComponent<Rigidbody>().velocity;
		} else {
			speed = new Vector3(0f,0f,0f);
		}	//errors protection
		
		
		if (Input.GetKey (KeyCode.D))
		{
       lForce(1f);
		
		}
		if (Input.GetKey (KeyCode.A))
		{
       rForce(1f);
		
		}
		
		if (Input.GetKey (KeyCode.W))
       dForce(2f);
		if (Input.GetKey (KeyCode.S)){
       uForce(2f);
	
		}
		
		if (Input.GetKey (KeyCode.Q))
       r2Force(1f);
		
		if (Input.GetKey (KeyCode.E))
       l2Force(1f);
		
		
		if (Input.GetKey (KeyCode.Space)){
				
				if (force_k<=1f){
				force_k+=.002f;
			}
       
		} else {
			
				if (force_k>=0f){
				force_k-=.003f;
			}	}
	upForce(480f*force_k);		
		
    }
		
		
		 void upForce(float force) {
		//Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(fw.transform.forward);
       // this.rigidbody.AddForceAtPosition(direction*force,fw.transform.position);
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
      this.GetComponent<Rigidbody>().AddRelativeForce(direction*force);
		print (direction*force);
    }
	
	
	 void dForce(float force) {
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.right);
        this.GetComponent<Rigidbody>().AddRelativeTorque(direction*force);
    }
	
	void uForce(float force) {
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.right);
        this.GetComponent<Rigidbody>().AddRelativeTorque(-direction*force);
    }
	
	 void r2Force(float force) {
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
        this.GetComponent<Rigidbody>().AddRelativeTorque(-direction*force);
    }
	
	void l2Force(float force) {
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
        this.GetComponent<Rigidbody>().AddRelativeTorque(direction*force);
    }
	
	  void lForce(float force) {
		 Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
       this.GetComponent<Rigidbody>().AddRelativeTorque(-direction*force);
	
    }
	
	
	  void rForce(float force) {
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
       this.GetComponent<Rigidbody>().AddRelativeTorque(direction*force);
    }
	
}
