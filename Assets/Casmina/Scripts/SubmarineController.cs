using UnityEngine;
using System.Collections;

namespace Casmina
{
    public class SubmarineController : MonoBehaviour
    {
        float force_k = 0f;
        private Vector3 speed;
        private Transform propeller;
        private bool right = false; //current status (rotating left or right)
        private bool left = false;//current status (rotating left or right)
        private float rot_timer = 0f;// rotation timer
        private float r_time = 4f;// total rotation time
                                  // Use this for initialization

        public float EngineForce = 1000f;
        public float EngineVerticalForce = 50f;

        void Start()
        {
            //GetComponent<Rigidbody>().centerOfMass = transform.FindChild("Center_of_mass").transform.localPosition;
        }


        // Update is called once per frame
        void Update()
        {
            if (!System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.x) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.y) || !System.Single.IsNaN(this.GetComponent<Rigidbody>().velocity.z))
            {
                speed = this.GetComponent<Rigidbody>().velocity;
            }
            else
            {
                speed = new Vector3(0f, 0f, 0f);
            }  

            if (Input.GetKey(KeyCode.W))
                MoveHorizontally(EngineForce);
            if (Input.GetKey(KeyCode.S))
                MoveHorizontally(-EngineForce);

            //if (Input.GetKey(KeyCode.A) || right)
            //{

            //    right = true;
            //    r2Force(EngineForce / 10f);

            //}
            //if (Input.GetKey(KeyCode.D) || left)
            //{
            //    left = true;

            //    l2Force(EngineForce / 10f);
            //}

            if (Input.GetKey(KeyCode.Q))
            {
                MoveVertically(EngineVerticalForce);
            }

            if (Input.GetKey(KeyCode.E))
            {
                MoveVertically(-EngineVerticalForce);
            }
        }


        void MoveVertically(float force)
        {
            Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
            this.GetComponent<Rigidbody>().AddRelativeForce(direction * force * Time.deltaTime * 1000f);
        }

        void MoveHorizontally(float force)
        {
            Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
            this.GetComponent<Rigidbody>().AddRelativeForce(direction * force * Time.deltaTime * 1000f);
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

}