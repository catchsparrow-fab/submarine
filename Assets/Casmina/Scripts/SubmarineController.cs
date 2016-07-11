using UnityEngine;
using System.Collections;

namespace Casmina
{
    public class SubmarineController : MonoBehaviour
    {
        public float EngineForce = 1000f;
        public float EngineVerticalForce = 50f;
        public float Waterline = 2f;

        private Rigidbody rigidBody = null;
        public float Speed { get { return rigidBody.velocity.magnitude; } }

        public void OnGUI()
        {
            GUI.Label(new Rect(20, 20, 100, 20), "Speed: " + Speed.ToString()); ;
        }

        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        bool IsUnderwater
        {
            get
            {
                return this.transform.position.y < Waterline;
            }
        }

        private float currentPitch = 0f;

        void SetOrientation()
        {
            float pitch = Mathf.Clamp(Mathf.Rad2Deg * 
                Mathf.Atan2(-rigidBody.velocity.y, rigidBody.velocity.z) 
                * Mathf.Abs(rigidBody.velocity.z) / 30f, 
                -25, 25);
            pitch = Mathf.Lerp(currentPitch, pitch, 0.5f);
            //print(Mathf.Rad2Deg * Mathf.Atan2(rigidBody.velocity.normalized.z, rigidBody.velocity.normalized.y));
            transform.rotation = Quaternion.Euler(pitch, 0f, 0f);
        }

        // Update is called once per frame
        void Update()
        {
            ApplyGravity();
            SetOrientation();

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
                MoveVertically(-EngineVerticalForce);
            }

            if (Input.GetKey(KeyCode.E) && IsUnderwater)
            {
                MoveVertically(EngineVerticalForce);
            }
        }

        void MoveVertically(float force)
        {
            Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
            rigidBody.AddRelativeForce(direction * force * Time.deltaTime * 1000f);
        }

        void ApplyGravity()
        {
            Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
            if (!IsUnderwater)
                MoveVertically(-30f);
        }

        void MoveHorizontally(float force)
        {
            Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.forward);
            rigidBody.AddRelativeForce(direction * force * Time.deltaTime * 1000f);
        }

        void r2Force(float force)
        {
            //Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);
            //print(right);
            //rot_timer += Time.deltaTime;
            //if (rot_timer < r_time)
            //{
            //    this.GetComponent<Rigidbody>().AddRelativeTorque(-direction * force);
            //}
            //else
            //{
            //    right = false;
            //    rot_timer = 0f;
            //}
        }

        void l2Force(float force)
        {
            //Vector3 direction = transform.worldToLocalMatrix.MultiplyVector(this.transform.up);

            //rot_timer += Time.deltaTime;
            //if (rot_timer < r_time)
            //{
            //    this.GetComponent<Rigidbody>().AddRelativeTorque(direction * force);
            //}
            //else
            //{
            //    left = false;
            //    rot_timer = 0f;
            //}
        }
    }
}