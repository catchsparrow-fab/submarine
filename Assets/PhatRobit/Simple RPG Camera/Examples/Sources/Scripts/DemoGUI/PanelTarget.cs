using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PhatRobit
{
	public class PanelTarget : MonoBehaviour
	{
		public InputField inputOffsetX;
		public InputField inputOffsetY;
		public InputField inputOffsetZ;
		public Toggle toggleSmoothOffset;
		public InputField inputSmoothOffsetSpeed;
		public Toggle toggleRelativeOffset;
		public Toggle toggleUseTargetAxis;
		public Toggle toggleSoftTracking;
		public InputField inputSoftTrackingRadius;
		public InputField inputSoftTrackingSpeed;
	}
}