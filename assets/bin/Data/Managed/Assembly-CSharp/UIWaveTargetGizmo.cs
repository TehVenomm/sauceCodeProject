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
	protected UILabel dispName;

	[SerializeField]
	[Tooltip("スクリ\u30fcン横オフセット")]
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

	private readonly string kIconPrefix = "Ingame_portal_";

	protected Transform portalTransform;

	protected Transform arrowTransform;

	protected Animator targetAnimator;

	protected string lastIconName = string.Empty;

	protected string dispNameStr = string.Empty;

	protected float lastHp;

	protected bool isFirst = true;

	protected bool isEnable = true;

	private FieldWaveTargetObject _waveTarget;

	public FieldWaveTargetObject waveTarget
	{
		get
		{
			return _waveTarget;
		}
		set
		{
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
		if (_waveTarget == null)
		{
			return;
		}
		GameObject val = null;
		if (!_waveTarget.info.name.IsNullOrWhiteSpace())
		{
			val = MonoBehaviourSingleton<SceneSettingsManager>.I.GetWaveTarget(_waveTarget.info.name);
		}
		else if (_waveTarget.gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3)
		{
			val = _waveTarget.get_gameObject();
		}
		if (val != null)
		{
			targetAnimator = val.GetComponentInChildren<Animator>();
			if (targetAnimator != null)
			{
				targetAnimator.set_enabled(true);
			}
		}
		lastIconName = _waveTarget.GetIconName();
		if (!lastIconName.IsNullOrWhiteSpace())
		{
			vitalSpr.spriteName = kIconPrefix + lastIconName;
		}
		bool flag = !_waveTarget.info.dispName.IsNullOrWhiteSpace();
		if (flag)
		{
			dispName.text = _waveTarget.info.dispName;
			dispNameStr = _waveTarget.info.dispName;
		}
		dispName.get_gameObject().SetActive(flag);
	}

	protected override void OnEnable()
	{
		isEnable = true;
		base.OnEnable();
		nearUI.SetActive(false);
		farUI.SetActive(false);
		vitalUI.SetActive(true);
		arrowTransform = arrowUI.get_transform();
		arrowUI.SetActive(false);
	}

	protected override void OnDisable()
	{
		if (!isEnable)
		{
			return;
		}
		isEnable = false;
		base.OnDisable();
		SetActiveSafe(nearUI, active: false);
		SetActiveSafe(farUI, active: false);
		SetActiveSafe(vitalUI, active: false);
		SetActiveSafe(arrowUI, active: false);
		if (_waveTarget == null || _waveTarget.nowHp == 0)
		{
			if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestTextAnnounce(string.Format(StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 5u), dispNameStr));
			}
			if (targetAnimator != null)
			{
				targetAnimator.SetInteger("Rate", 0);
			}
		}
	}

	protected override void UpdateParam()
	{
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
			return;
		}
		Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, portalTransform.get_position());
		screenZ = screenUIPosition.z;
		screenUIPosition.z = 0f;
		float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
		Vector3 val = screenUIPosition;
		bool flag = false;
		float num2 = Screen.get_width();
		float num3 = Screen.get_height();
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
			SetActiveSafe(nearUI, active: false);
			SetActiveSafe(farUI, active: true);
			if (arrowTransform == null)
			{
				return;
			}
			Vector3 val4 = val - screenUIPosition;
			if (val4 == Vector3.get_zero())
			{
				SetActiveSafe(arrowUI, active: false);
				return;
			}
			float num4 = 90f - Vector3.Angle(Vector3.get_right(), val4);
			arrowTransform.set_eulerAngles(new Vector3(0f, 0f, num4));
			SetActiveSafe(arrowUI, active: true);
		}
		else
		{
			SetActiveSafe(nearUI, active: true);
			SetActiveSafe(farUI, active: false);
			SetActiveSafe(arrowUI, active: false);
		}
		CheckHp();
	}

	private void CheckHp()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (lastHp == (float)waveTarget.nowHp)
		{
			return;
		}
		lastHp = waveTarget.nowHp;
		float num = waveTarget.GetRate();
		if (num < 0f)
		{
			num = 0f;
		}
		for (int i = 0; i < this.hpColorInfo.Length; i++)
		{
			HpColorInfo hpColorInfo = this.hpColorInfo[i];
			if (!(num < hpColorInfo.rate))
			{
				continue;
			}
			if (gaugeSpr.color != hpColorInfo.color)
			{
				gaugeSpr.color = hpColorInfo.color;
				if (num <= 0f)
				{
					MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestTextAnnounce(string.Format(StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 5u), waveTarget.info.dispName));
				}
				else
				{
					MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestTextAnnounce(string.Format(StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 4u), waveTarget.info.dispName, hpColorInfo.rate));
				}
			}
			break;
		}
		gaugeUI.SetPercent(num);
		int num2 = Mathf.CeilToInt(num * 100f);
		if (targetAnimator != null)
		{
			targetAnimator.SetInteger("Rate", num2);
		}
		string iconName = _waveTarget.GetIconName(num2);
		if (!iconName.IsNullOrWhiteSpace() && iconName != lastIconName)
		{
			lastIconName = iconName;
			vitalSpr.spriteName = kIconPrefix + iconName;
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
