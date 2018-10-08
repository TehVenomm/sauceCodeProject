using UnityEngine;

public class UISonarGizmo : UIStatusGizmoBase
{
	[SerializeField]
	protected GameObject arrow;

	[SerializeField]
	protected UISprite statusSprite;

	[SerializeField]
	protected Vector3 offset;

	[SerializeField]
	[Tooltip("スクリ\u30fcン横オフセット")]
	protected float screenSideOffset = 22f;

	[SerializeField]
	[Tooltip("スクリ\u30fcン下オフセット")]
	protected float screenBottomOffset = 112f;

	private FieldSonarObject _sonar;

	protected Transform portalTransform;

	protected Transform arrowTransform;

	public FieldSonarObject sonar
	{
		get
		{
			return _sonar;
		}
		set
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			_sonar = value;
			if (_sonar != null)
			{
				this.get_gameObject().SetActive(true);
				portalTransform = value.get_transform();
				UpdateParam();
			}
			else
			{
				this.get_gameObject().SetActive(false);
			}
		}
	}

	protected override void OnEnable()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		base.OnEnable();
		if (arrow != null)
		{
			arrowTransform = arrow.get_transform();
		}
	}

	protected override void UpdateParam()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		if (sonar == null || !sonar.get_gameObject().get_activeSelf())
		{
			SetActiveSafe(statusSprite.get_gameObject(), false);
			SetActiveSafe(arrow, false);
		}
		else
		{
			Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, portalTransform.get_position() + offset);
			screenZ = screenUIPosition.z;
			screenUIPosition.z = 0f;
			float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
			Vector3 val = screenUIPosition;
			bool flag = false;
			float num2 = (float)Screen.get_width();
			if (screenUIPosition.x < screenSideOffset * num)
			{
				screenUIPosition.x = screenSideOffset * num;
				flag = true;
			}
			else if (screenUIPosition.x > num2 - screenSideOffset * num)
			{
				screenUIPosition.x = num2 - screenSideOffset * num;
				flag = true;
			}
			if (screenUIPosition.y < screenBottomOffset * num)
			{
				screenUIPosition.y = screenBottomOffset * num;
				flag = true;
			}
			if (flag)
			{
				SetActiveSafe(statusSprite.get_gameObject(), true);
				SetActiveSafe(arrow, true);
				Vector3 val2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUIPosition);
				Vector3 val3 = transform.get_position() - val2;
				if (val3.get_sqrMagnitude() >= 2E-05f)
				{
					transform.set_position(val2);
				}
				if (arrowTransform != null)
				{
					Vector3 val4 = val - screenUIPosition;
					if (val4 != Vector3.get_zero())
					{
						float num3 = 90f - Vector3.Angle(Vector3.get_right(), val4);
						arrowTransform.set_eulerAngles(new Vector3(0f, 0f, num3));
					}
					else
					{
						arrowTransform.set_eulerAngles(new Vector3(0f, 0f, 0f));
					}
				}
			}
			else
			{
				SetActiveSafe(statusSprite.get_gameObject(), false);
				SetActiveSafe(arrow, false);
			}
		}
	}
}
