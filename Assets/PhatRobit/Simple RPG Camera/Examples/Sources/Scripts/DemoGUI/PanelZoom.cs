using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PhatRobit
{
	public class PanelZoom : MonoBehaviour
	{
		public Toggle toggleAllowZoom;
		public Toggle toggleDisableOverGui;
		public Toggle toggleAutoZoomSpeed;
		public InputField inputZoomSpeed;
		public InputField inputZoomSmoothing;
		public Toggle toggleInvert;
		public Toggle toggleKeys;
		public InputField inputMinDistance;
		public InputField inputMaxDistance;
	}
}