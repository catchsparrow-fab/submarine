using UnityEngine;
using System.Collections;

public class Dragon_control : MonoBehaviour {
		float force_k=0f;
		private Vector3 speed;

	private Vector3 mousePosition;
	private float max_thrust_k=1f;
	public float stability = 0.3f;
	public float m_speed = 2.0f;
	private float br_speed = 0f;
	private bool br=false; // are we currently doing a barrel roll?
	private bool  hover=false; // are we currently hovering?
	private bool gl=false;// are we currently gliding?
	private float x_mouse_k = .1f; //mouse control koef-ts - lower value = higher influence
	private float y_mouse_k = .25f;//mouse control koef-ts

	private float br_delta_angle=0f;

	// Use this for initialization
	void Start () {

		GetComponent<Rigidbody>().centerOfMass = transform.FindChild("Center_of_mass").transform.localPosition;
	}
	
		void OnGUI(){
		GUI.Label(new Rect(10,50,100,20),"SPACE - Gas");
		GUI.Label(new Rect(10,70,200,20),"Mouse up/down - Pitch");
		GUI.Label(new Rect(10,90,200,20),"Mouse left/right - Yaw");
		GUI.Label(new Rect(10,110,200,20),"Press A/D for a Barrel Roll");
		GUI.Label(new Rect(10,130,200,20),"Hold W to glide");
		GUI.Label(new Rect(10,150,200,20),"Release space to hover");
		GUI.Label(new Rect(10,150,200,20),"Sorry, don't have a dragon model");
	}
	
	// Update is called once per frame
	void Update () {

		mousePosition = Input.mousePosition;
		uForce((Screen.height/2f -mousePosition.y)/(x_mouse_k*Screen.height));//Applying force 
		r2Force((Screen.width/2f - mousePosition.x)/(y_mouse_k*Screen.width));
		rForce((Screen.width/2f - mousePosition.x)/(y_mouse_k*Screen.width)/5f);

		if (!br) {		
			this.GetComponent<Rigidbody>().AddForce(Vector3.up*-400f*Time.deltaTime*100f);  // Extra Gravity if not doing a barrel roll
		} else {
			this.GetComponent<Rigidbody>().AddForce(Vector3.up*-50f*Time.deltaTime*100f);  // if doing br, then a small gravity force is applied for a better looking result
		}
		
		
	if (!System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.x) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.y) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.z)){
			speed=this.GetComponent<Rigidbody>().velocity;
		} else {
			speed = new Vector3(0f,0f,0f);
		}	//errors protection
		
		
		if (Input.GetKey (KeyCode.D) && !br)
		{
			br_speed=-1f;//br direction
			br=true;//do a br
		
		}
		if (Input.GetKey (KeyCode.A) && !br)
		{
			br_speed=1f;//br direction
			br=true;//do a br
		}






		if (Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.Space)&& !br){//if space is not pressed and w is pressed and not doing a br, then glide
			hover=false;
			gl=true;
			upForce(380f*Time.deltaTime*100f);//applying vertical force
			glForce(80f*Time.deltaTime*100f);//applying forward force 
		} 

		if (Input.GetKeyUp(KeyCode.W) || Input.GetKey (KeyCode.Space)){// if w btn is released or space is pressed - stop gliding
			gl=false;
		}


	if (Input.GetKey (KeyCode.Space)){
			hover=false;
			if (force_k<=max_thrust_k){//force koef-t, it goes up and down to simulate wings flapping
			force_k+=.006f;//faster it increase, more often it will receive lift
			max_thrust_k=1f;
			} else {//when it reaches 1, it goes down
				max_thrust_k=.43f;//bigger this is, smaller will be the drop after lift
				force_k -= 0.1f;// bigger this is, smaller will be the drop after lift(this is how fast the force is reduced while wings go up)

			}
		
	} else if (!gl){
			hover=true;
			upForce(380f*Time.deltaTime*100f);//small upforce to compensate part of gravity when hovering(reduce it to descend faster)
		}
		if (!br && !gl && !hover)
			upForce(600f*force_k*Time.deltaTime*100f);//flapping force	
	
}


	void doBarrelRoll(float br_sp){
		Vector3 localAngularVelocity = transform.InverseTransformDirection(this.GetComponent<Rigidbody>().angularVelocity);
		br_delta_angle += Mathf.Abs(localAngularVelocity.z);//getting current rotation angle
		if (br_delta_angle>200f ){
			this.GetComponent<Rigidbody>().angularVelocity = this.GetComponent<Rigidbody>().angularVelocity/((1.51f-(300f-br_delta_angle)/200f));//empirical equation for barrel roll
		}
		if (br_delta_angle>300f){
			this.GetComponent<Rigidbody>().angularVelocity = this.GetComponent<Rigidbody>().angularVelocity/2f;//extra slowing down to reduce overshoot
			br_delta_angle = 0f;
			br_speed=0f;
			br=false;
		} else {
			rForce(50f*br_sp*Time.deltaTime*70f);//br force
		}
	
	}

	//--Stabilization
	void FixedUpdate () {
		Vector3 predictedUp = Quaternion.AngleAxis(
			GetComponent<Rigidbody>().angularVelocity.magnitude * Mathf.Rad2Deg * stability / m_speed,
			GetComponent<Rigidbody>().angularVelocity
			) * transform.up;
		
		Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
		GetComponent<Rigidbody>().AddTorque(torqueVector * m_speed * m_speed);

		if (br){
			doBarrelRoll(br_speed);//do barrel roll
		}
	}
	//----
		
		 void upForce(float force) {
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
      	this.GetComponent<Rigidbody>().AddRelativeForce(direction*force);

    }

	void glForce(float force) {
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
		this.GetComponent<Rigidbody>().AddRelativeForce(direction*force);
		
	}
	

	
	void uForce(float force) {//Pitch
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.right);
        this.GetComponent<Rigidbody>().AddRelativeTorque(-direction*force);
    }
	
	 void r2Force(float force) {//YAW
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
		this.GetComponent<Rigidbody>().AddRelativeTorque(-direction*force);
    }
	

	
	  void rForce(float force) {//Roll
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
       this.GetComponent<Rigidbody>().AddRelativeTorque(direction*force);
    }
	
}
