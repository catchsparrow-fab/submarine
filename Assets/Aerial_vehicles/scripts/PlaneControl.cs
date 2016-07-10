using UnityEngine;
using System.Collections;

public class PlaneControl : MonoBehaviour {

	public Transform fw;
	public Transform bw;
	private float adj;
	private Vector3 speed;
	private Vector3 upf;
	int k;
	float force_k=0f;
	float back=0f;
	float back1=0f;
	private Transform propeller;
	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody>().AddForce(Vector3.forward);
		fw= this.transform.FindChild("fw");// used to adjust the force according to the plane position
		bw= this.transform.FindChild("bw");//
		propeller = this.transform.FindChild("Plane").transform.FindChild("prop").transform;//find the propeller to rotate it - you may have to change it for your model
		
	}
	
		void OnGUI(){
		GUI.Label(new Rect(10,50,100,20),"SPACE - Gas");
		GUI.Label(new Rect(10,70,100,20),"W/S - Pitch");
		GUI.Label(new Rect(10,90,100,20),"A/D - Roll");
		GUI.Label(new Rect(10,110,100,20),"Q/E - Yaw");
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		propeller.localEulerAngles += new Vector3(0f,0f,transform.InverseTransformDirection(speed).z*2f);//rotating the propeller
		adj= (fw.transform.position.y-bw.transform.position.y);
		if (adj<0f){adj=0f;}
		
		if (this.transform.position.y<0f){
			 uForce(1f);
		}
		
		if (this.transform.eulerAngles.z>=90 && this.transform.eulerAngles.z<270){
			k=-1;
		}
		else{
			k=1;
		}
		
	   
		if (Input.GetKey (KeyCode.D))
		{
       lForce(6f);
		
		}
		if (Input.GetKey (KeyCode.A))
		{
       rForce(6f);
		
		}
		if (Input.GetKey (KeyCode.W))
       dForce(6f);
		if (Input.GetKey (KeyCode.S)){
       uForce(6f);
		this.GetComponent<Rigidbody>().AddForce(Vector3.up*40f);
		}
		
		if (Input.GetKey (KeyCode.Q))
       r2Force(4f);
		
		if (Input.GetKey (KeyCode.E))
       l2Force(4f);
		
		if (this.GetComponent<Rigidbody>().velocity.magnitude<75f){
		if (Input.GetKey (KeyCode.Space)){
				
				if (force_k<=1f){
				force_k+=.002f;
			}
       
		} else {
			
				if (force_k>=0f){
				force_k-=.003f;
			}	
				
	
		
			}
		}
		
		this.GetComponent<Rigidbody>().AddForce(Vector3.up*-300f);//additional gravity(for more realistic behaviour)
		if (!System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.x) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.y) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.z)){
			speed=this.GetComponent<Rigidbody>().velocity;
		} else {
			speed = new Vector3(0f,0f,0f);
		}//error protection
		if (transform.InverseTransformDirection(speed).z*transform.InverseTransformDirection(speed).z<1500f){
		fForce(600f*force_k);//main movement force from the propeller
		this.GetComponent<Rigidbody>().AddRelativeForce(transform.worldToLocalMatrix.MultiplyVector(this.transform.up)*transform.InverseTransformDirection(speed).z*transform.InverseTransformDirection(speed).z/4.5f);//realistic lift force from wings
		this.GetComponent<Rigidbody>().AddForce(Vector3.up*(40f*Mathf.Sqrt(Mathf.Abs(transform.InverseTransformDirection(speed).z))-adj*adj*adj*25f));//fake additional force for realistic behaviour
		
		}
			
		
	}
	
	  void lForce(float force) {
	 Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
       this.GetComponent<Rigidbody>().AddRelativeTorque(-direction*force);
	
    }
	
	
	  void rForce(float force) {
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
       this.GetComponent<Rigidbody>().AddRelativeTorque(direction*force);
    }
	
	  void fForce(float force) {
		Vector3	direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
      this.GetComponent<Rigidbody>().AddRelativeForce(direction*force);
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
	
}
