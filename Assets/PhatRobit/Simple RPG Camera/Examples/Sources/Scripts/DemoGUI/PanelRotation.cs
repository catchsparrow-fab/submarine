using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PhatRobit
{
	public class PanelRotation : MonoBehaviour
	{
		public Toggle toggleStayBehindTarget;
		public Toggle toggleReturnToOrigin;
		public Toggle toggleAllowRotation;
		public Toggle toggleDisableOverGui;
		public Toggle toggleMouseLook;
		public Toggle toggleMouseLeft;
		public Toggle toggleMouseMid;
		public Toggle toggleMouseRight;
		public Toggle toggleMouseLockLeft;
		public Toggle toggleMouseLockMid;
		public Toggle toggleMouseLockRight;
		public InputField inputMinAngle;
		public InputField inputMaxAngle;
		public Toggle toggleAllowKeys;
		public Toggle toggleTimeoutRotation;
		public InputField inputTimeoutDelay;
		public InputField inputTimeoutSpeed;
	}
}