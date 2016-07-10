using UnityEngine;
using System.Collections;

public class DirigibleControl : MonoBehaviour
{
    float force_k = 0f;
    private Vector3 speed;
    private Transform propeller;
    private bool right = false; //current status (rotating left or right)
    private bool left = false;//current status (rotating left or right)
    private float rot_timer = 0f;// rotation timer
    private float r_time = 4f;// total rotation time
                              // Use this for initialization
    void Start()
    {

        GetComponent<Rigidbody>().centerOfMass = transform.FindChild("Center_of_mass").transform.localPosition;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 50, 100, 20), "SPACE - Gas");
        GUI.Label(new Rect(10, 70, 100, 20), "W/S - Pitch");
        GUI.Label(new Rect(10, 90, 100, 20), "A/D - Yaw");


    }

    // Update is called once per frame
    void Update()
    {


        //this.GetComponent<Rigidbody>().AddForce(Vector3.up * -300f);  // Extra Gravity


        if (!System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.x) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.y) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.z))
        {
            speed = this.GetComponent<Rigidbody>().velocity;
        }
        else
        {
            speed = new Vector3(0f, 0f, 0f);
        }   //errors protection




        if (Input.GetKey(KeyCode.W))
            ffForce(50f);
        if (Input.GetKey(KeyCode.S))
        {
            bForce(10f);

        }

        if (Input.GetKey(KeyCode.A) || right)
        {

            right = true;
            r2Force(5f);

        }
        if (Input.GetKey(KeyCode.D) || left)
        {
            left = true;

            l2Force(5f);
        }

        if (Input.GetKey(KeyCode.Space))
        {

            if (force_k <= 1f)
            {
                force_k += .012f;
            }

        }
        //else
        //{

        //    if (force_k >= 0f)
        //    {
        //        force_k -= .0001f;
        //    }
        //}
        upForce(450f * force_k);

    }


    void upForce(float force)
    {
        Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
        this.GetComponent<Rigidbody>().AddRelativeForce(direction * force);

    }


    void ffForce(float force)
    {
        Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
        this.GetComponent<Rigidbody>().AddRelativeForce(direction * force);
    }

    void bForce(float force)
    {
        Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
        this.GetComponent<Rigidbody>().AddRelativeForce(-direction * force);
    }

    void r2Force(float force)
    {
        Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
        print(right);
        rot_timer += Time.deltaTime;
        if (rot_timer < r_time)
        {
            this.GetComponent<Rigidbody>().AddRelativeTorque(-direction * force);
        }
        else
        {
            right = false;
            rot_timer = 0f;
        }
    }

    void l2Force(float force)
    {
        Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);

        rot_timer += Time.deltaTime;
        if (rot_timer < r_time)
        {
            this.GetComponent<Rigidbody>().AddRelativeTorque(direction * force);
        }
        else
        {
            left = false;
            rot_timer = 0f;
        }
    }



}
