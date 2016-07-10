using UnityEngine;
using System.Collections;

public class UfoControl_height : MonoBehaviour {
		float force_k=0f;
		private Vector3 speed;
	public float max_Height;// max and min heights
	public float min_Height;
	
	private float cur_height=0f;// current height(%)
	private Transform propeller;
	// Use this for initialization
	void Start () {
		propeller = this.transform.FindChild("ufo").transform;//the whole body is rotating 
		this.GetComponent<Rigidbody>().drag =5f;
	}
	
		
		void OnGUI(){
		cur_height = (this.transform.position.y-min_Height)/(max_Height-min_Height);
		GUI.Label(new Rect(10,50,100,20),"U/J - Height Control");
		GUI.Label(new Rect(10,70,100,20),"W/S - Pitch");
		GUI.Label(new Rect(10,90,100,20),"A/D - Yaw");
		
		GUI.Label(new Rect(10,200,100,20),"Height:" + Mathf.Round(cur_height*100f)+"%");
		
	}
	// Update is called once per frame
	void Update () {
	propeller.localEulerAngles += new Vector3(0f,force_k*30f,0f);//the whole body is rotating 
	
	this.GetComponent<Rigidbody>().AddForce(Vector3.up*-300f);  // Extra Gravity(for realistic behaviour)
		
		
	if (!System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.x) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.y) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.z)){
			speed=this.GetComponent<Rigidbody>().velocity;
		} else {
			speed = new Vector3(0f,0f,0f);
		}	//errors protection
		
		
		if (Input.GetKey (KeyCode.D))//rotation
		{
        l2Force(15f);
		
		}
		if (Input.GetKey (KeyCode.A))//rotation
		{
      
		 r2Force(15f);
		}
		
		if (Input.GetKey (KeyCode.W))//forward
       dForce(500f);
		if (Input.GetKey (KeyCode.S)){//backward
       uForce(500f);
	
		}
		
		
		
		
		if (Input.GetKey (KeyCode.U) && this.transform.position.y<max_Height){
				
			force_k=.75f;//koefficient for main force
			
       
		} else if (Input.GetKey (KeyCode.J) && this.transform.position.y>min_Height) {
			
			force_k=-.25f;	//koefficient for main force
				} else{
			force_k=0.475f; //koefficient for main force
			this.GetComponent<Rigidbody>().velocity += -new Vector3(0f,this.GetComponent<Rigidbody>().velocity.y,0f)/30f;// slowing down when height control btn is released
		}
	upForce(800f*force_k);	//main force	
		
    }
		
		
		 void upForce(float force) {
		
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
      this.GetComponent<Rigidbody>().AddRelativeForce(direction*force);
		
    }
	
	
	 void dForce(float force) {
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
        this.GetComponent<Rigidbody>().AddRelativeForce(direction*force);
	
    }
	
	void uForce(float force) {
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
        this.GetComponent<Rigidbody>().AddRelativeForce(direction*force);
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
