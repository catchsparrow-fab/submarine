using UnityEngine;
using System.Collections;
using UnityEngine.UI;

#if UNITY_5_3 || UNITY_5_4
using UnityEngine.SceneManagement;
#endif

namespace PhatRobit
{
	public class SrpgcDemoGUI : MonoBehaviour
	{
		public PanelCollision panelCollision;
		public PanelTarget panelTarget;
		public PanelMovement panelMovement;
		public PanelRotation panelRotation;
		public PanelZoom panelZoom;
		public PanelFade panelFade;

		private SimpleRpgCamera _rpgCamera;

		private bool _initialized = false;

		void Start()
		{
			_rpgCamera = Camera.main.GetComponent<SimpleRpgCamera>();

			TogglePanel((int)PanelType.Collision);
		}

		public void TogglePanel(int panelType)
		{
			_initialized = false;

			panelCollision.gameObject.SetActive(false);
			panelTarget.gameObject.SetActive(false);
			panelMovement.gameObject.SetActive(false);
			panelRotation.gameObject.SetActive(false);
			panelZoom.gameObject.SetActive(false);
			panelFade.gameObject.SetActive(false);

			switch((PanelType)panelType)
			{
				case PanelType.Collision:
					panelCollision.gameObject.SetActive(true);
					UpdatePanelCollision();
					break;
				case PanelType.Target:
					panelTarget.gameObject.SetActive(true);
					UpdatePanelTarget();
					break;
				case PanelType.Movement:
					panelMovement.gameObject.SetActive(true);
					UpdatePanelMovement();
					break;
				case PanelType.Rotation:
					panelRotation.gameObject.SetActive(true);
					UpdatePanelRotation();
					break;
				case PanelType.Zoom:
					panelZoom.gameObject.SetActive(true);
					UpdatePanelZoom();
					break;
				case PanelType.Fade:
					panelFade.gameObject.SetActive(true);
					UpdatePanelFade();
					break;
			}

			_initialized = true;
		}

		private void UpdatePanelCollision()
		{
			if(_rpgCamera)
			{
				panelCollision.inputBuffer.text = _rpgCamera.collisionBuffer.ToString();
				panelCollision.toggleClipping.isOn = _rpgCamera.collisionClipping;
				panelCollision.toggleIgnoreTarget.isOn = _rpgCamera.ignoreCurrentTarget;
				panelCollision.toggleClampDistance.isOn = _rpgCamera.clampCollision;
			}
		}

		private void UpdatePanelTarget()
		{
			if(_rpgCamera)
			{
				panelTarget.inputOffsetX.text = _rpgCamera.targetOffset.x.ToString();
				panelTarget.inputOffsetY.text = _rpgCamera.targetOffset.y.ToString();
				panelTarget.inputOffsetZ.text = _rpgCamera.targetOffset.z.ToString();
				panelTarget.toggleSmoothOffset.isOn = _rpgCamera.smoothOffset;
				panelTarget.inputSmoothOffsetSpeed.text = _rpgCamera.smoothOffsetSpeed.ToString();
				panelTarget.toggleRelativeOffset.isOn = _rpgCamera.relativeOffset;
				panelTarget.toggleUseTargetAxis.isOn = _rpgCamera.useTargetAxis;
				panelTarget.toggleSoftTracking.isOn = _rpgCamera.softTracking;
				panelTarget.inputSoftTrackingRadius.text = _rpgCamera.softTrackingRadius.ToString();
				panelTarget.inputSoftTrackingSpeed.text = _rpgCamera.softTrackingSpeed.ToString();
			}
		}

		private void UpdatePanelMovement()
		{
			if(_rpgCamera)
			{
				panelMovement.toggleAllowMouseDrag.isOn = _rpgCamera.allowMouseDrag;
				panelMovement.toggleAllowEdgeMovement.isOn = _rpgCamera.allowEdgeMovement;
				panelMovement.toggleAllowKeys.isOn = _rpgCamera.allowEdgeKeys;
				panelMovement.toggleLimitBounds.isOn = _rpgCamera.limitBounds;
				panelMovement.inputScrollSpeed.text = _rpgCamera.scrollSpeed.ToString();
			}
		}

		private void UpdatePanelRotation()
		{
			if(_rpgCamera)
			{
				panelRotation.toggleStayBehindTarget.isOn = _rpgCamera.stayBehindTarget;
				panelRotation.toggleReturnToOrigin.isOn = _rpgCamera.returnToOrigin;
				panelRotation.toggleAllowRotation.isOn = _rpgCamera.allowRotation;
				panelRotation.toggleDisableOverGui.isOn = _rpgCamera.disableRotationOverGui;
				panelRotation.toggleMouseLook.isOn = _rpgCamera.mouseLook;
				panelRotation.toggleMouseLeft.isOn = _rpgCamera.allowRotationLeft;
				panelRotation.toggleMouseMid.isOn = _rpgCamera.allowRotationMiddle;
				panelRotation.toggleMouseRight.isOn = _rpgCamera.allowRotationRight;
				panelRotation.toggleMouseLockLeft.isOn = _rpgCamera.lockLeft;
				panelRotation.toggleMouseLockMid.isOn = _rpgCamera.lockMiddle;
				panelRotation.toggleMouseLockRight.isOn = _rpgCamera.lockRight;
				panelRotation.inputMinAngle.text = _rpgCamera.minAngleY.ToString();
				panelRotation.inputMaxAngle.text = _rpgCamera.maxAngleY.ToString();
				panelRotation.toggleAllowKeys.isOn = _rpgCamera.allowRotationKeys;
				panelRotation.toggleTimeoutRotation.isOn = _rpgCamera.timeoutRotation;
				panelRotation.inputTimeoutDelay.text = _rpgCamera.timeoutRotationDelay.ToString();
				panelRotation.inputTimeoutSpeed.text = _rpgCamera.timeoutRotationSpeed.ToString();
			}
		}

		private void UpdatePanelZoom()
		{
			if(_rpgCamera)
			{
				panelZoom.toggleAllowZoom.isOn = _rpgCamera.allowZoom;
				panelZoom.toggleDisableOverGui.isOn = _rpgCamera.disableZoomOverGui;
				panelZoom.toggleAutoZoomSpeed.isOn = _rpgCamera.autoAdjustZoomSpeed;
				panelZoom.inputZoomSpeed.text = _rpgCamera.zoomSpeed.ToString();
				panelZoom.inputZoomSmoothing.text = _rpgCamera.zoomSmoothing.ToString();
				panelZoom.toggleInvert.isOn = _rpgCamera.invertZoom;
				panelZoom.toggleKeys.isOn = _rpgCamera.allowZoomKeys;
				panelZoom.inputMinDistance.text = _rpgCamera.minDistance.ToString();
				panelZoom.inputMaxDistance.text = _rpgCamera.maxDistance.ToString();
			}
		}

		private void UpdatePanelFade()
		{
			if(_rpgCamera)
			{
				panelFade.toggleFadeTarget.isOn = _rpgCamera.fadeCurrentTarget;
				panelFade.inputFadeDistance.text = _rpgCamera.fadeDistance.ToString();
			}
		}

		public void UpdateCameraCollision()
		{
			if(_rpgCamera && _initialized)
			{
				float.TryParse(panelCollision.inputBuffer.text, out _rpgCamera.collisionBuffer);
				_rpgCamera.collisionClipping = panelCollision.toggleClipping.isOn;
				_rpgCamera.ignoreCurrentTarget = panelCollision.toggleIgnoreTarget.isOn;
				_rpgCamera.clampCollision = panelCollision.toggleClampDistance.isOn;

				UpdatePanelCollision();
			}
		}

		public void UpdateCameraTarget()
		{
			if(_rpgCamera && _initialized)
			{
				float offsetX = _rpgCamera.targetOffset.x;
				float offsetY = _rpgCamera.targetOffset.y;
				float offsetZ = _rpgCamera.targetOffset.z;

				float.TryParse(panelTarget.inputOffsetX.text, out offsetX);
				float.TryParse(panelTarget.inputOffsetY.text, out offsetY);
				float.TryParse(panelTarget.inputOffsetZ.text, out offsetZ);

				_rpgCamera.targetOffset = new Vector3(offsetX, offsetY, offsetZ);
				_rpgCamera.smoothOffset = panelTarget.toggleSmoothOffset.isOn;
				float.TryParse(panelTarget.inputSmoothOffsetSpeed.text, out _rpgCamera.smoothOffsetSpeed);
				_rpgCamera.relativeOffset = panelTarget.toggleRelativeOffset.isOn;
				_rpgCamera.useTargetAxis = panelTarget.toggleUseTargetAxis.isOn;
				_rpgCamera.softTracking = panelTarget.toggleSoftTracking.isOn;
				float.TryParse(panelTarget.inputSoftTrackingRadius.text, out _rpgCamera.softTrackingRadius);
				float.TryParse(panelTarget.inputSoftTrackingSpeed.text, out _rpgCamera.softTrackingSpeed);

				UpdatePanelTarget();
			}
		}

		public void UpdateCameraMovement()
		{
			if(_rpgCamera && _initialized)
			{
				_rpgCamera.allowMouseDrag = panelMovement.toggleAllowMouseDrag.isOn;
				_rpgCamera.allowEdgeMovement = panelMovement.toggleAllowEdgeMovement.isOn;
				_rpgCamera.allowEdgeKeys = panelMovement.toggleAllowKeys.isOn;
				_rpgCamera.limitBounds = panelMovement.toggleLimitBounds.isOn;
				float.TryParse(panelMovement.inputScrollSpeed.text, out _rpgCamera.scrollSpeed);

				if(_rpgCamera.allowMouseDrag)
				{
					_rpgCamera.mouseDragButton = MouseButton.Left;
				}

				UpdatePanelMovement();
			}
		}

		public void UpdateCameraRotation()
		{
			if(_rpgCamera && _initialized)
			{
				_rpgCamera.stayBehindTarget = panelRotation.toggleStayBehindTarget.isOn;
				_rpgCamera.returnToOrigin = panelRotation.toggleReturnToOrigin.isOn;
				_rpgCamera.allowRotation = panelRotation.toggleAllowRotation.isOn;
				_rpgCamera.disableRotationOverGui = panelRotation.toggleDisableOverGui.isOn;
				_rpgCamera.mouseLook = panelRotation.toggleMouseLook.isOn;
				_rpgCamera.allowRotationLeft = panelRotation.toggleMouseLeft.isOn;
				_rpgCamera.allowRotationMiddle = panelRotation.toggleMouseMid.isOn;
				_rpgCamera.allowRotationRight = panelRotation.toggleMouseRight.isOn;
				_rpgCamera.lockLeft = panelRotation.toggleMouseLockLeft.isOn;
				_rpgCamera.lockMiddle = panelRotation.toggleMouseLockMid.isOn;
				_rpgCamera.lockRight = panelRotation.toggleMouseLockRight.isOn;
				float.TryParse(panelRotation.inputMinAngle.text, out _rpgCamera.minAngleY);
				float.TryParse(panelRotation.inputMaxAngle.text, out _rpgCamera.maxAngleY);
				_rpgCamera.allowRotationKeys = panelRotation.toggleAllowKeys.isOn;
				_rpgCamera.timeoutRotation = panelRotation.toggleTimeoutRotation.isOn;
				float.TryParse(panelRotation.inputTimeoutDelay.text, out _rpgCamera.timeoutRotationDelay);
				float.TryParse(panelRotation.inputTimeoutSpeed.text, out _rpgCamera.timeoutRotationSpeed);

				UpdatePanelRotation();
			}
		}

		public void UpdateCameraZoom()
		{
			if(_rpgCamera && _initialized)
			{
				_rpgCamera.allowZoom = panelZoom.toggleAllowZoom.isOn;
				_rpgCamera.disableZoomOverGui = panelZoom.toggleDisableOverGui.isOn;
				_rpgCamera.autoAdjustZoomSpeed = panelZoom.toggleAutoZoomSpeed.isOn;
				float.TryParse(panelZoom.inputZoomSpeed.text, out _rpgCamera.zoomSpeed);
				float.TryParse(panelZoom.inputZoomSmoothing.text, out _rpgCamera.zoomSmoothing);
				_rpgCamera.invertZoom = panelZoom.toggleInvert.isOn;
				_rpgCamera.allowZoomKeys = panelZoom.toggleKeys.isOn;
				float.TryParse(panelZoom.inputMinDistance.text, out _rpgCamera.minDistance);
				float.TryParse(panelZoom.inputMaxDistance.text, out _rpgCamera.maxDistance);

				UpdatePanelZoom();
			}
		}

		public void UpdateCameraFade()
		{
			if(_rpgCamera && _initialized)
			{
				_rpgCamera.fadeCurrentTarget = panelFade.toggleFadeTarget.isOn;
				float.TryParse(panelFade.inputFadeDistance.text, out _rpgCamera.fadeDistance);

				UpdatePanelFade();
			}
		}

		public void LoadDemoScene(string sceneName)
		{
#if UNITY_5_3 || UNITY_5_4
			SceneManager.LoadScene(sceneName);
#else
			Application.LoadLevel(sceneName);
#endif
		}
	}
}