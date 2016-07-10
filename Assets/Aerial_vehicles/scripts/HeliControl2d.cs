using UnityEngine;
using System.Collections;

public class HeliControl2d : MonoBehaviour {
		float force_k=0f;
		private Vector3 speed;
	public float stability = 0.3f;
	public float m_speed = 2.0f;
	private Transform propeller;

	// Use this for initialization
	void Start () {
		propeller = this.transform.FindChild("helicopter").transform.FindChild("prop").transform;//find the propeller to rotate it - you may have to change it for your model
		
	GetComponent<Rigidbody>().centerOfMass = transform.FindChild("Center_of_mass").transform.localPosition;
	}
	
		void OnGUI(){
		GUI.Label(new Rect(10,50,100,20),"Up - Gas");
		GUI.Label(new Rect(10,70,100,20),"Left/Right - Pitch");

		
		
	}
	
	// Update is called once per frame
	void Update () {
	propeller.localEulerAngles += new Vector3(0f,force_k*30f,0f);
			
	this.GetComponent<Rigidbody>().AddForce(Vector3.up*-300f);  // Extra Gravity for realistic behaviour
		
		
	if (!System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.x) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.y) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.z)){
			speed=this.GetComponent<Rigidbody>().velocity;
		} else {
			speed = new Vector3(0f,0f,0f);
		}	//errors protection
		
		
	
		if (Input.GetKey (KeyCode.LeftArrow))
       dForce(6f);
		if (Input.GetKey (KeyCode.RightArrow)){
       uForce(6f);
	
		}
		

		
		if (Input.GetKey (KeyCode.UpArrow)){
				
				if (force_k<=1f){
				force_k+=.004f;

			}
			upForce(460f*force_k);	
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			
				if (force_k>=0f){
				force_k-=.006f;

			}	
			upForce(460f*force_k);	
		} else {
			upForce(300f*force_k);	//minimum force in order not to fall down
		}

		
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

	//--Stabilization
	void FixedUpdate () {
		Vector3 predictedUp = Quaternion.AngleAxis(
			GetComponent<Rigidbody>().angularVelocity.magnitude * Mathf.Rad2Deg * stability / m_speed,
			GetComponent<Rigidbody>().angularVelocity
			) * transform.up;
		
		Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
		GetComponent<Rigidbody>().AddTorque(torqueVector * m_speed * m_speed);
		

	}
	
}
