using UnityEngine;

public class WorldMapCameraController : DraggableCamera
{
	protected bool isInteractive_ = true;

	private float fov = 60f;

	public bool isInteractive
	{
		get
		{
			return isInteractive_;
		}
		set
		{
			isInteractive_ = value;
		}
	}

	public override Camera _camera
	{
		get
		{
			if (__camera == null)
			{
				__camera = GetComponent<Camera>();
			}
			return __camera;
		}
	}

	protected float cameraFovMin => MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraFovMin;

	protected float cameraFovMax => MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraFovMax;

	protected override Plane hitPlane => new Plane(Vector3.back, 1f);

	private void Awake()
	{
		base.distance = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraManualDistance;
		base.distanceManual = base.distance;
		CreateRenderTexture(isPortrait: false);
		_camera.fieldOfView = cameraFovMin;
		isInteractive_ = true;
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += CreateRenderTexture;
		}
	}

	private void CreateRenderTexture(bool isPortrait)
	{
		Restore();
	}

	private void OnDestroy()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid() && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= CreateRenderTexture;
		}
		if (_camera.targetTexture != null)
		{
			RenderTexture.ReleaseTemporary(_camera.targetTexture);
			_camera.targetTexture = null;
		}
	}

	protected override Vector3 GetCameraMove(Vector2 old_screen_pos, Vector2 now_screen_pos)
	{
		Plane hitPlane = this.hitPlane;
		Ray ray = _camera.ScreenPointToRay(old_screen_pos.ToVector3XY());
		Ray ray2 = _camera.ScreenPointToRay(now_screen_pos.ToVector3XY());
		if (!hitPlane.Raycast(ray, out float enter))
		{
			return Vector3.zero;
		}
		if (!hitPlane.Raycast(ray2, out float enter2))
		{
			return Vector3.zero;
		}
		Vector3 point = ray.GetPoint(enter);
		Vector3 result = ray2.GetPoint(enter2) - point;
		result.x = 0f - result.x;
		result.y = 0f - result.y;
		result.z = 0f;
		return result;
	}

	protected override Vector3 ClampEnableMapArea(Vector3 pos)
	{
		float num = 0f - MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraMoveClampLeft;
		float cameraMoveClampRight = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraMoveClampRight;
		float cameraMoveClampUpper = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraMoveClampUpper;
		float num2 = 0f - MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraMoveClampLower;
		if (pos.x < num)
		{
			pos.x = num;
		}
		else if (pos.x > cameraMoveClampRight)
		{
			pos.x = cameraMoveClampRight;
		}
		if (pos.y < num2)
		{
			pos.y = num2;
		}
		else if (pos.y > cameraMoveClampUpper)
		{
			pos.y = cameraMoveClampUpper;
		}
		return pos;
	}

	protected override void OnPinch(InputManager.TouchInfo touch_info0, InputManager.TouchInfo touch_info1, float pinch_length)
	{
		if (IsInteractive() && touch_info0 != null && touch_info1 != null)
		{
			Plane hitPlane = this.hitPlane;
			cameraMove = Vector3.zero;
			Vector2 vector = (touch_info0.position + touch_info1.position) * 0.5f;
			Ray ray = _camera.ScreenPointToRay(vector.ToVector3XY());
			if (hitPlane.Raycast(ray, out float _))
			{
				GlobalSettingsManager.WorldMapParam worldMapParam = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam;
				fov -= pinch_length * worldMapParam.cameraPinchSpeed;
				fov = Mathf.Clamp(fov, worldMapParam.cameraFovMin, worldMapParam.cameraFovMax);
				_camera.fieldOfView = fov;
				UpdateCameraTransform();
			}
		}
	}

	protected override bool IsInteractive()
	{
		return isInteractive_;
	}

	protected override void UpdateCameraTransform()
	{
		base.targetPos = ClampEnableMapArea(base.targetPos);
		cameraTransform.position = base.targetPos - cameraTransform.forward * base.distance;
	}

	public void Restore()
	{
		if (null != _camera.targetTexture)
		{
			RenderTexture.ReleaseTemporary(_camera.targetTexture);
			_camera.targetTexture = null;
		}
		_camera.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height);
	}
}
