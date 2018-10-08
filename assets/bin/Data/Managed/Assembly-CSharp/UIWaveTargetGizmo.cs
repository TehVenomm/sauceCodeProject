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

	[Tooltip("HPの色")]
	[SerializeField]
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
			if ((UnityEngine.Object)_waveTarget != (UnityEngine.Object)null)
			{
				base.gameObject.SetActive(true);
				portalTransform = value.transform;
				UpdateParam();
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	public void Initialize()
	{
		if (!((UnityEngine.Object)_waveTarget == (UnityEngine.Object)null))
		{
			GameObject gameObject = null;
			if (!_waveTarget.info.name.IsNullOrWhiteSpace())
			{
				gameObject = MonoBehaviourSingleton<SceneSettingsManager>.I.GetWaveTarget(_waveTarget.info.name);
			}
			else if (_waveTarget.gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3)
			{
				gameObject = _waveTarget.gameObject;
			}
			if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
			{
				targetAnimator = gameObject.GetComponentInChildren<Animator>();
				if ((UnityEngine.Object)targetAnimator != (UnityEngine.Object)null)
				{
					targetAnimator.enabled = true;
				}
			}
			lastIconName = _waveTarget.GetIconName(100);
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
	}

	protected override void OnEnable()
	{
		isEnable = true;
		base.OnEnable();
		nearUI.SetActive(false);
		farUI.SetActive(false);
		vitalUI.SetActive(true);
		arrowTransform = arrowUI.transform;
		arrowUI.SetActive(false);
	}

	protected override void OnDisable()
	{
		if (isEnable)
		{
			isEnable = false;
			base.OnDisable();
			SetActiveSafe(nearUI, false);
			SetActiveSafe(farUI, false);
			SetActiveSafe(vitalUI, false);
			SetActiveSafe(arrowUI, false);
			if ((UnityEngine.Object)_waveTarget == (UnityEngine.Object)null || _waveTarget.nowHp == 0)
			{
				if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
				{
					MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestTextAnnounce(string.Format(StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 5u), dispNameStr));
				}
				if ((UnityEngine.Object)targetAnimator != (UnityEngine.Object)null)
				{
					targetAnimator.SetInteger("Rate", 0);
				}
			}
		}
	}

	protected override void UpdateParam()
	{
		if ((UnityEngine.Object)waveTarget == (UnityEngine.Object)null || waveTarget.isDead || !waveTarget.gameObject.activeSelf)
		{
			OnDisable();
		}
		else
		{
			Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, portalTransform.position);
			screenZ = screenUIPosition.z;
			screenUIPosition.z = 0f;
			float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
			Vector3 a = screenUIPosition;
			bool flag = false;
			float num2 = (float)Screen.width;
			float num3 = (float)Screen.height;
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
				SetActiveSafe(nearUI, false);
				SetActiveSafe(farUI, true);
				if ((UnityEngine.Object)arrowTransform == (UnityEngine.Object)null)
				{
					return;
				}
				Vector3 vector2 = a - screenUIPosition;
				if (vector2 == Vector3.zero)
				{
					SetActiveSafe(arrowUI, false);
					return;
				}
				float z = 90f - Vector3.Angle(Vector3.right, vector2);
				arrowTransform.eulerAngles = new Vector3(0f, 0f, z);
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
			}
			gaugeUI.SetPercent(num, true);
			int num2 = Mathf.CeilToInt(num * 100f);
			if ((UnityEngine.Object)targetAnimator != (UnityEngine.Object)null)
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
}
