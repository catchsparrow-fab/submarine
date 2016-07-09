using UnityEngine;
using System.Collections;

namespace PhatRobit
{
	public class SrpgcNavmeshAgentController : MonoBehaviour
	{
		public float maxRayDistance = 100;
		public LayerMask clickableLayers = new LayerMask();

		public string speedFloat = "Speed";

		private Animator _animator;
		private NavMeshAgent _agent;

		void Start()
		{
			_animator = GetComponent<Animator>();
			_agent = GetComponent<NavMeshAgent>();

			if(!_animator)
			{
				Debug.LogWarning("There is no Animator Component attached to this gameObject");
			}

			if(!_agent)
			{
				Debug.LogWarning("There is no NavMeshAgent Component attached to this gameObject");
			}
		}

		void Update()
		{
			if(_agent)
			{
				if(Input.GetMouseButton(0))
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if(Physics.Raycast(ray, out hit, maxRayDistance, clickableLayers))
					{
						_agent.destination = hit.point;
					}
				}

				if(_animator)
				{
					_animator.SetFloat(speedFloat, (_agent.velocity.x == 0 && _agent.velocity.z == 0 ? 0 : 1));
				}
			}
		}
	}
}