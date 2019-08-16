using UnityEngine;

public class UIPortalStatusGizmo : UIStatusGizmoBase
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

	[SerializeField]
	protected UITweenCtrl addTween;

	[SerializeField]
	protected UITweenCtrl fullTween;

	private PortalObject _portal;

	protected Transform portalTransform;

	protected Transform arrowTransform;

	public PortalObject portal
	{
		get
		{
			return _portal;
		}
		set
		{
			if (_portal != null)
			{
				_portal.uiGizmo = null;
			}
			_portal = value;
			if (_portal != null)
			{
				_portal.uiGizmo = this;
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
		base.OnEnable();
		if (arrow != null)
		{
			arrowTransform = arrow.get_transform();
		}
	}

	protected override void UpdateParam()
	{
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		if (portal == null || !portal.get_gameObject().get_activeSelf())
		{
			SetActiveSafe(statusSprite.get_gameObject(), active: false);
			SetActiveSafe(arrow, active: false);
			SetActiveSafe(addTween.get_gameObject(), active: false);
			SetActiveSafe(fullTween.get_gameObject(), active: false);
			return;
		}
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyPlayerStatusGizmo)
		{
			if (SpecialDeviceManager.IsPortrait)
			{
				screenSideOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPortalGizmoScreenSideOffsetPortrait;
				screenBottomOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPortalGizmoScreenBottomOffsetPortrait;
			}
			else
			{
				screenSideOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPortalGizmoScreenSideOffsetLandscape;
				screenBottomOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPortalGizmoScreenBottomOffsetLandscape;
			}
		}
		Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, portalTransform.get_position() + offset);
		screenZ = screenUIPosition.z;
		screenUIPosition.z = 0f;
		float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
		Vector3 val = screenUIPosition;
		bool flag = false;
		float num2 = Screen.get_width();
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
			SetActiveSafe(statusSprite.get_gameObject(), active: true);
			SetActiveSafe(arrow, active: true);
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
			bool flag2 = false;
			if (statusSprite != null)
			{
				switch (portal.viewType)
				{
				case PortalObject.VIEW_TYPE.NORMAL:
					statusSprite.spriteName = "Ingame_portal_vitalsign_blue";
					break;
				case PortalObject.VIEW_TYPE.NOT_TRAVELED:
				{
					if (portal.isFull)
					{
						flag2 = true;
						statusSprite.spriteName = "Ingame_portal_vitalsign_red";
						break;
					}
					float num4 = (float)portal.nowPoint / (float)portal.maxPoint;
					int num5 = (int)(num4 * (float)MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointRankNum);
					if (num5 >= MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointRankNum - 1)
					{
						num5 = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointRankNum - 2;
					}
					statusSprite.spriteName = string.Format("Ingame_portal_vitalsign_red_{0}", num5.ToString("D2"));
					break;
				}
				case PortalObject.VIEW_TYPE.TO_HOME:
					statusSprite.spriteName = "Ingame_portal_vitalsign_green";
					break;
				case PortalObject.VIEW_TYPE.TO_HARD_MAP:
					statusSprite.spriteName = "Ingame_portal_vitalsign_purple";
					break;
				case PortalObject.VIEW_TYPE.NOT_CLEAR_ORDER:
					statusSprite.spriteName = "Ingame_portal_vitalsign_not_clear_order";
					break;
				}
			}
			if (flag2)
			{
				SetActiveSafe(fullTween.get_gameObject(), active: true);
				fullTween.Play();
			}
			else
			{
				SetActiveSafe(fullTween.get_gameObject(), active: false);
			}
		}
		else
		{
			SetActiveSafe(statusSprite.get_gameObject(), active: false);
			SetActiveSafe(arrow, active: false);
			SetActiveSafe(addTween.get_gameObject(), active: false);
			SetActiveSafe(fullTween.get_gameObject(), active: false);
		}
	}

	public void OnGetPortalPoint()
	{
		SetActiveSafe(addTween.get_gameObject(), active: true);
		addTween.Reset();
		addTween.Play(forward: true, delegate
		{
			SetActiveSafe(fullTween.get_gameObject(), active: false);
		});
	}
}
