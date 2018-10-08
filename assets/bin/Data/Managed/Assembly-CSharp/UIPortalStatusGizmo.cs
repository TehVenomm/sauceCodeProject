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

	[Tooltip("スクリ\u30fcン下オフセット")]
	[SerializeField]
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
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Expected O, but got Unknown
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Expected O, but got Unknown
		if (portal == null || !portal.get_gameObject().get_activeSelf())
		{
			SetActiveSafe(statusSprite.get_gameObject(), false);
			SetActiveSafe(arrow, false);
			SetActiveSafe(addTween.get_gameObject(), false);
			SetActiveSafe(fullTween.get_gameObject(), false);
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
				bool flag2 = false;
				if (statusSprite != null)
				{
					switch (portal.viewType)
					{
					case PortalObject.VIEW_TYPE.NORMAL:
						statusSprite.spriteName = "Ingame_portal_vitalsign_blue";
						break;
					case PortalObject.VIEW_TYPE.NOT_TRAVELED:
						if (portal.isFull)
						{
							flag2 = true;
							statusSprite.spriteName = "Ingame_portal_vitalsign_red";
						}
						else
						{
							float num4 = (float)portal.nowPoint / (float)portal.maxPoint;
							int num5 = (int)(num4 * (float)MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointRankNum);
							if (num5 >= MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointRankNum - 1)
							{
								num5 = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointRankNum - 2;
							}
							statusSprite.spriteName = string.Format("Ingame_portal_vitalsign_red_{0}", num5.ToString("D2"));
						}
						break;
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
					SetActiveSafe(fullTween.get_gameObject(), true);
					fullTween.Play(true, null);
				}
				else
				{
					SetActiveSafe(fullTween.get_gameObject(), false);
				}
			}
			else
			{
				SetActiveSafe(statusSprite.get_gameObject(), false);
				SetActiveSafe(arrow, false);
				SetActiveSafe(addTween.get_gameObject(), false);
				SetActiveSafe(fullTween.get_gameObject(), false);
			}
		}
	}

	public void OnGetPortalPoint()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		SetActiveSafe(addTween.get_gameObject(), true);
		addTween.Reset();
		addTween.Play(true, delegate
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Expected O, but got Unknown
			SetActiveSafe(fullTween.get_gameObject(), false);
		});
	}
}
