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

	protected string lastIconName = "";

	protected string dispNameStr = "";

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
				base.gameObject.SetActive(value: true);
				portalTransform = value.transform;
				UpdateParam();
			}
			else
			{
				base.gameObject.SetActive(value: false);
			}
		}
	}

	public void Initialize()
	{
		if (_waveTarget == null)
		{
			return;
		}
		GameObject gameObject = null;
		if (!_waveTarget.info.name.IsNullOrWhiteSpace())
		{
			gameObject = MonoBehaviourSingleton<SceneSettingsManager>.I.GetWaveTarget(_waveTarget.info.name);
		}
		else if (_waveTarget.gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3)
		{
			gameObject = _waveTarget.gameObject;
		}
		if (gameObject != null)
		{
			targetAnimator = gameObject.GetComponentInChildren<Animator>();
			if (targetAnimator != null)
			{
				targetAnimator.enabled = true;
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
		dispName.gameObject.SetActive(flag);
	}

	protected override void OnEnable()
	{
		isEnable = true;
		base.OnEnable();
		nearUI.SetActive(value: false);
		farUI.SetActive(value: false);
		vitalUI.SetActive(value: true);
		arrowTransform = arrowUI.transform;
		arrowUI.SetActive(value: false);
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
		if (waveTarget == null || waveTarget.isDead || !waveTarget.gameObject.activeSelf)
		{
			OnDisable();
			return;
		}
		Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, portalTransform.position);
		screenZ = screenUIPosition.z;
		screenUIPosition.z = 0f;
		float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
		Vector3 a = screenUIPosition;
		bool flag = false;
		float num2 = Screen.width;
		_ = Screen.height;
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
		Vector3 vector = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUIPosition);
		vector.y += offsetY;
		if ((transform.position - vector).sqrMagnitude >= 2E-05f)
		{
			transform.position = vector;
		}
		if (flag)
		{
			SetActiveSafe(nearUI, active: false);
			SetActiveSafe(farUI, active: true);
			if (arrowTransform == null)
			{
				return;
			}
			Vector3 vector2 = a - screenUIPosition;
			if (vector2 == Vector3.zero)
			{
				SetActiveSafe(arrowUI, active: false);
				return;
			}
			float z = 90f - Vector3.Angle(Vector3.right, vector2);
			arrowTransform.eulerAngles = new Vector3(0f, 0f, z);
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
