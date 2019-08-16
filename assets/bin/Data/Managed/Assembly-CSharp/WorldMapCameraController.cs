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
				__camera = this.GetComponent<Camera>();
			}
			return __camera;
		}
	}

	protected float cameraFovMin => MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraFovMin;

	protected float cameraFovMax => MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraFovMax;

	protected override Plane hitPlane => new Plane(Vector3.get_back(), 1f);

	private void Awake()
	{
		base.distance = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.cameraManualDistance;
		base.distanceManual = base.distance;
		CreateRenderTexture(isPortrait: false);
		_camera.set_fieldOfView(cameraFovMin);
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
		if (_camera.get_targetTexture() != null)
		{
			RenderTexture.ReleaseTemporary(_camera.get_targetTexture());
			_camera.set_targetTexture(null);
		}
	}

	protected override Vector3 GetCameraMove(Vector2 old_screen_pos, Vector2 now_screen_pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		Plane hitPlane = this.hitPlane;
		Ray val = _camera.ScreenPointToRay(old_screen_pos.ToVector3XY());
		Ray val2 = _camera.ScreenPointToRay(now_screen_pos.ToVector3XY());
		float num = default(float);
		if (!hitPlane.Raycast(val, ref num))
		{
			return Vector3.get_zero();
		}
		float num2 = default(float);
		if (!hitPlane.Raycast(val2, ref num2))
		{
			return Vector3.get_zero();
		}
		Vector3 point = val.GetPoint(num);
		Vector3 point2 = val2.GetPoint(num2);
		Vector3 result = point2 - point;
		result.x = 0f - result.x;
		result.y = 0f - result.y;
		result.z = 0f;
		return result;
	}

	protected override Vector3 ClampEnableMapArea(Vector3 pos)
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (IsInteractive() && touch_info0 != null && touch_info1 != null)
		{
			Plane hitPlane = this.hitPlane;
			cameraMove = Vector3.get_zero();
			Vector2 vector = (touch_info0.position + touch_info1.position) * 0.5f;
			Ray val = _camera.ScreenPointToRay(vector.ToVector3XY());
			float num = default(float);
			if (hitPlane.Raycast(val, ref num))
			{
				GlobalSettingsManager.WorldMapParam worldMapParam = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam;
				fov -= pinch_length * worldMapParam.cameraPinchSpeed;
				fov = Mathf.Clamp(fov, worldMapParam.cameraFovMin, worldMapParam.cameraFovMax);
				_camera.set_fieldOfView(fov);
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
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		base.targetPos = ClampEnableMapArea(base.targetPos);
		cameraTransform.set_position(base.targetPos - cameraTransform.get_forward() * base.distance);
	}

	public void Restore()
	{
		if (null != _camera.get_targetTexture())
		{
			RenderTexture.ReleaseTemporary(_camera.get_targetTexture());
			_camera.set_targetTexture(null);
		}
		_camera.set_targetTexture(RenderTexture.GetTemporary(Screen.get_width(), Screen.get_height()));
	}
}
