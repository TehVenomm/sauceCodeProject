using System;
using UnityEngine;

public class UIWaveTargetGizmo : UIStatusGizmoBase
{
	[Serializable]
	public class HpColorInfo
	{
		public float rate;

		public Color color;
	}

	[SerializeField]
	protected GameObject nearUI;

	[SerializeField]
	protected UIHGauge gaugeUI;

	[SerializeField]
	protected UISprite gaugeSpr;

	[SerializeField]
	protected GameObject farUI;

	[SerializeField]
	protected GameObject arrowUI;

	[SerializeField]
	protected GameObject vitalUI;

	[SerializeField]
	protected SimplePingPongAlpha vitalAddAnim;

	[SerializeField]
	protected UISprite vitalSpr;

	[SerializeField]
	protected UISprite vitalAddSpr;

	[Tooltip("スクリ\u30fcン横オフセット")]
	[SerializeField]
	protected float screenSideOffset = 50f;

	[SerializeField]
	[Tooltip("スクリ\u30fcン下オフセット")]
	protected float screenBottomOffset = 140f;

	[SerializeField]
	[Tooltip("表示時のYオフセット")]
	protected float offsetY = 0.1f;

	[SerializeField]
	[Tooltip("HPの色")]
	protected HpColorInfo[] hpColorInfo;

	protected Transform portalTransform;

	protected Transform arrowTransform;

	protected Animator targetAnimator;

	protected float lastHp;

	protected bool isFirst = true;

	private FieldWaveTargetObject _waveTarget;

	public FieldWaveTargetObject waveTarget
	{
		get
		{
			return _waveTarget;
		}
		set
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			_waveTarget = value;
			if (_waveTarget != null)
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

	public void Initialize()
	{
		if (!(_waveTarget == null))
		{
			if (!_waveTarget.info.name.IsNullOrWhiteSpace())
			{
				GameObject waveTarget = MonoBehaviourSingleton<SceneSettingsManager>.I.GetWaveTarget(_waveTarget.info.name);
				if (waveTarget != null)
				{
					targetAnimator = waveTarget.GetComponent<Animator>();
					if (targetAnimator != null)
					{
						targetAnimator.set_enabled(true);
					}
				}
			}
			if (!_waveTarget.info.iconName.IsNullOrWhiteSpace())
			{
				vitalSpr.spriteName = "Ingame_portal_" + _waveTarget.info.iconName;
				vitalAddSpr.spriteName = "Ingame_portal_" + _waveTarget.info.iconName + "_add";
			}
		}
	}

	protected override void OnEnable()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		base.OnEnable();
		nearUI.SetActive(false);
		farUI.SetActive(false);
		vitalUI.SetActive(true);
		arrowTransform = arrowUI.get_transform();
		arrowUI.SetActive(false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		SetActiveSafe(nearUI, false);
		SetActiveSafe(farUI, false);
		SetActiveSafe(vitalUI, false);
		SetActiveSafe(arrowUI, false);
		if (_waveTarget == null && targetAnimator != null)
		{
			targetAnimator.SetInteger("Rate", 0);
		}
	}

	protected override void UpdateParam()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		if (waveTarget == null || waveTarget.isDead || !waveTarget.get_gameObject().get_activeSelf())
		{
			OnDisable();
		}
		else
		{
			Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, portalTransform.get_position());
			screenZ = screenUIPosition.z;
			screenUIPosition.z = 0f;
			float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
			Vector3 val = screenUIPosition;
			bool flag = false;
			float num2 = (float)Screen.get_width();
			float num3 = (float)Screen.get_height();
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
			Vector3 val2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUIPosition);
			val2.y += offsetY;
			Vector3 val3 = transform.get_position() - val2;
			if (val3.get_sqrMagnitude() >= 2E-05f)
			{
				transform.set_position(val2);
			}
			if (flag)
			{
				SetActiveSafe(nearUI, false);
				SetActiveSafe(farUI, true);
				if (arrowTransform == null)
				{
					return;
				}
				Vector3 val4 = val - screenUIPosition;
				if (val4 == Vector3.get_zero())
				{
					SetActiveSafe(arrowUI, false);
					return;
				}
				float num4 = 90f - Vector3.Angle(Vector3.get_right(), val4);
				arrowTransform.set_eulerAngles(new Vector3(0f, 0f, num4));
				SetActiveSafe(arrowUI, true);
			}
			else
			{
				SetActiveSafe(nearUI, true);
				SetActiveSafe(farUI, false);
				SetActiveSafe(arrowUI, false);
			}
			CheckHp();
		}
	}

	private void CheckHp()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (lastHp != (float)waveTarget.nowHp)
		{
			lastHp = (float)waveTarget.nowHp;
			float num = waveTarget.GetRate();
			if (num < 0f)
			{
				num = 0f;
			}
			for (int i = 0; i < this.hpColorInfo.Length; i++)
			{
				HpColorInfo hpColorInfo = this.hpColorInfo[i];
				if (num < hpColorInfo.rate)
				{
					gaugeSpr.color = hpColorInfo.color;
					break;
				}
			}
			gaugeUI.SetPercent(num, true);
			if (targetAnimator != null)
			{
				targetAnimator.SetInteger("Rate", (int)(num * 100f));
			}
			if (isFirst)
			{
				isFirst = false;
			}
			else
			{
				MonoBehaviourSingleton<MiniMap>.I.ShowAlert();
			}
		}
	}
}
