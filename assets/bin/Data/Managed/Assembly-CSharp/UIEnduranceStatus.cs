using Network;
using System;
using System.Collections;
using UnityEngine;

public class UIEnduranceStatus : MonoBehaviourSingleton<UIEnduranceStatus>
{
	[Serializable]
	public class boostItem
	{
		public GameObject obj;

		public UITweenCtrl anim;

		public USE_ITEM_EFFECT_TYPE type;
	}

	[Serializable]
	public class boosColor
	{
		public float rate;

		public Color color;
	}

	[Serializable]
	protected class ShieldHpGaugeEffect
	{
		[SerializeField]
		public UISprite sprite;

		[SerializeField]
		public int sizeMin;

		[SerializeField]
		public int sizeMax;

		[SerializeField]
		public Color[] effectColor;
	}

	[Serializable]
	protected class SpActionGaugeInfo
	{
		public enum ANIM_STATE
		{
			RESET,
			HALF,
			FULL,
			BOOST
		}

		[SerializeField]
		public GameObject root;

		[SerializeField]
		public Color gaugeColorNormal;

		[SerializeField]
		public Color gaugeColorCharged;

		[SerializeField]
		public UIHGauge gaugeUI;

		[SerializeField]
		public Renderer renderer;

		[NonSerialized]
		public int state;

		[SerializeField]
		public Color[] gaugeColorJump;

		[SerializeField]
		public Color[] gaugeColorSoul;

		[SerializeField]
		public Color[] gaugeColorSoulPairSwords;

		[SerializeField]
		public GameObject gaugeMemoriObj;

		public bool IsPlayAnim(ANIM_STATE s)
		{
			return (state & (1 << (int)s)) > 0;
		}

		public void SetState(ANIM_STATE s)
		{
			state |= 1 << (int)s;
		}
	}

	[Serializable]
	public class CannonSpecialGaugeInfo
	{
		public enum ANIM_STATE
		{
			RESET,
			FULL
		}

		[SerializeField]
		public GameObject root;

		[SerializeField]
		public GameObject gaugeAddObj;

		[SerializeField]
		public GameObject fullTipObj;

		[SerializeField]
		public GameObject gaugeNormalObj;

		[SerializeField]
		public GameObject gaugeChargedObj;

		[SerializeField]
		public UIHGauge gaugeUI;

		[SerializeField]
		public Color gaugeColorNormal;

		[SerializeField]
		public Color gaugeColorCharged;

		[SerializeField]
		public Renderer renderer;

		[NonSerialized]
		public int state;

		public bool IsPlayAnim(ANIM_STATE s)
		{
			return (state & (1 << (int)s)) > 0;
		}

		public void SetState(ANIM_STATE s)
		{
			state |= 1 << (int)s;
		}
	}

	[SerializeField]
	protected UILabel enduranceName;

	[SerializeField]
	protected UILabel enduranceHp;

	[SerializeField]
	protected UIHGauge hpGaugeUI;

	[SerializeField]
	protected UIWeaponChange weaponChange;

	[SerializeField]
	protected UIEvolveGauge evolveGauge;

	[SerializeField]
	protected SpActionGaugeInfo spActionGaugeInfo;

	[SerializeField]
	protected ShieldHpGaugeEffect spActionGaugeADD_BOOST;

	[SerializeField]
	protected ShieldHpGaugeEffect spActionGaugeADD_HALF;

	[SerializeField]
	protected StatusBoostAnimator boostAnimator;

	[SerializeField]
	protected boostItem[] boostItems;

	[SerializeField]
	protected UILabel boostRate;

	[SerializeField]
	protected UILabel boostTime;

	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	[SerializeField]
	private Transform soulEffectDirection;

	[SerializeField]
	private float soulEffectTime = 1f;

	[SerializeField]
	private float soulEffectAddRandomMax = 200f;

	[SerializeField]
	private AnimationCurve soulEffectEaseCurve = Curves.CreateEaseInCurve();

	[SerializeField]
	private AnimationCurve soulEffectAddCurve;

	[SerializeField]
	private CannonSpecialGaugeInfo cannonSpecialGaugePortrait;

	[SerializeField]
	private CannonSpecialGaugeInfo cannonSpecialGaugeLandscape;

	private CannonSpecialGaugeInfo validCannonSpecialGaugeInfo;

	private bool permitHGPBoostUpdate = true;

	private int preWeaponIndex;

	private int preUniqueEquipmentIndex;

	private UIBurstBulletUIController m_burstBulletCtrl;

	public Player targetPlayer
	{
		get;
		protected set;
	}

	public bool PermitHGPBoostUpdate => permitHGPBoostUpdate;

	protected override void Awake()
	{
		base.Awake();
		boostRate.fontStyle = 2;
		boostTime.fontStyle = 2;
		this.get_gameObject().SetActive(false);
		CreateBurstBulletUI();
	}

	private void CreateBurstBulletUI()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (m_burstBulletCtrl != null)
		{
			return;
		}
		Transform val = ResourceUtility.Realizes(Resources.Load(UIPlayerStatus.UI_BURST_BULLET), this.get_transform());
		if (!(val == null))
		{
			val.set_localPosition(UIPlayerStatus.UI_BURST_BULLET_POS);
			m_burstBulletCtrl = val.GetComponent<UIBurstBulletUIController>();
			if (m_burstBulletCtrl != null)
			{
				UIBurstBulletUIController.InitParam initParam = new UIBurstBulletUIController.InitParam();
				initParam.MaxBulletCount = 6;
				initParam.CurrentRestBulletCount = 6;
				UIBurstBulletUIController.InitParam param = initParam;
				m_burstBulletCtrl.Initialize(param);
			}
		}
	}

	private void Start()
	{
		EffectCtrl[] componentsInChildren = boostAnimator.GetComponentsInChildren<EffectCtrl>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetRenderQueue(2000);
		}
	}

	public void SetTarget(Player player)
	{
		targetPlayer = player;
		if (targetPlayer == null)
		{
			this.get_gameObject().SetActive(false);
			return;
		}
		if (weaponChange != null)
		{
			weaponChange.SetTarget(targetPlayer);
		}
		UpdateUI();
		SetUpBoostAnimator();
		this.get_gameObject().SetActive(true);
	}

	public void SetUpBoostAnimator()
	{
		boostAnimator.SetupUI(delegate(BoostStatus update_boost)
		{
			if (update_boost != null)
			{
				UpdateShowBoost(update_boost);
			}
			else
			{
				EndShowBoost();
			}
		}, delegate(BoostStatus change_boost)
		{
			if (change_boost != null)
			{
				ChangeShowBoost((USE_ITEM_EFFECT_TYPE)change_boost.type);
				UpdateShowBoost(change_boost);
			}
			else
			{
				EndShowBoost();
			}
		});
	}

	private void LateUpdate()
	{
		if (!(targetPlayer == null))
		{
			UpdateUI();
		}
	}

	private void UpdateUI()
	{
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && !string.IsNullOrEmpty(MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.enduranceObjectName))
		{
			enduranceName.text = MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.enduranceObjectName;
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			enduranceHp.text = MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEndurance.ToString();
		}
		if (hpGaugeUI != null)
		{
			float percent = (!(MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEnduranceMax > 0f)) ? 0f : (MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEndurance / MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEnduranceMax);
			hpGaugeUI.SetPercent(percent);
		}
		OnUpdateWeaponIndex();
		bool flag = targetPlayer.IsValidSpActionMemori();
		if (spActionGaugeInfo.gaugeMemoriObj.get_activeSelf() != flag)
		{
			spActionGaugeInfo.gaugeMemoriObj.SetActive(flag);
		}
		bool flag2 = targetPlayer.IsValidSpActionGauge();
		if (spActionGaugeInfo.root != null && spActionGaugeInfo.root.get_activeSelf() != flag2)
		{
			spActionGaugeInfo.root.SetActive(flag2);
		}
		if (flag2)
		{
			if (spActionGaugeInfo.renderer != null)
			{
				int num = targetPlayer.CheckGaugeLevel();
				Color val = (num != -1) ? ((!targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL)) ? spActionGaugeInfo.gaugeColorJump[num] : spActionGaugeInfo.gaugeColorSoulPairSwords[num]) : ((targetPlayer.spAttackType != SP_ATTACK_TYPE.SOUL) ? ((!targetPlayer.IsSpActionGaugeHalfCharged() && !targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorNormal : spActionGaugeInfo.gaugeColorCharged) : ((!targetPlayer.IsSpActionGaugeHalfCharged() && !targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorSoul[0] : spActionGaugeInfo.gaugeColorSoul[1]));
				if (val != spActionGaugeInfo.renderer.get_material().get_color())
				{
					spActionGaugeInfo.renderer.get_material().set_color(val);
				}
			}
			float num2 = (!(targetPlayer.CurrentWeaponSpActionGaugeMax > 0f)) ? 0f : (targetPlayer.CurrentWeaponSpActionGauge / targetPlayer.CurrentWeaponSpActionGaugeMax);
			bool flag3 = false;
			if (spActionGaugeInfo.gaugeUI != null)
			{
				flag3 = (spActionGaugeInfo.gaugeUI.nowPercent != num2);
				if (flag3)
				{
					spActionGaugeInfo.gaugeUI.SetPercent(num2, anim: false);
				}
			}
			UISprite sprite = spActionGaugeADD_BOOST.sprite;
			if (flag3)
			{
				sprite.width = (int)(num2 * (float)spActionGaugeADD_BOOST.sizeMax + (1f - num2) * (float)spActionGaugeADD_BOOST.sizeMin);
			}
			if (spActionGaugeInfo.root != null && spActionGaugeInfo.root.get_activeInHierarchy())
			{
				if (targetPlayer.IsSpActionGaugeHalfCharged() && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.HALF))
				{
					sprite.get_gameObject().SetActive(false);
					spActionGaugeADD_HALF.sprite.get_gameObject().SetActive(true);
					UITweenCtrl.Reset(spActionGaugeInfo.root.get_transform(), 1);
					UITweenCtrl.Play(spActionGaugeInfo.root.get_transform(), forward: true, null, is_input_block: false, 1);
					spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.HALF);
					SoundManager.PlayOneShotUISE(40000358);
				}
				if (targetPlayer.IsSpActionGaugeFullCharged() && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.FULL))
				{
					sprite.get_gameObject().SetActive(false);
					spActionGaugeADD_HALF.sprite.get_gameObject().SetActive(true);
					UITweenCtrl.Reset(spActionGaugeInfo.root.get_transform(), 2);
					UITweenCtrl.Play(spActionGaugeInfo.root.get_transform(), forward: true, null, is_input_block: false, 2);
					spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.FULL);
					SoundManager.PlayOneShotUISE(40000359);
				}
				if (targetPlayer.isBoostMode && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.BOOST))
				{
					sprite.get_gameObject().SetActive(true);
					spActionGaugeADD_HALF.sprite.get_gameObject().SetActive(false);
					UITweenCtrl.Reset(spActionGaugeInfo.root.get_transform());
					UITweenCtrl.Play(spActionGaugeInfo.root.get_transform(), forward: true, null, is_input_block: false);
					spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.BOOST);
				}
			}
			if ((!targetPlayer.isBoostMode && spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.BOOST)) || (spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.FULL) && !targetPlayer.IsSpActionGaugeFullCharged()))
			{
				ResetSpActionGaugeState();
			}
			if (targetPlayer.isDead && spActionGaugeInfo.state != 0)
			{
				ResetSpActionGaugeState();
			}
		}
		else if (spActionGaugeInfo.state != 0)
		{
			ResetSpActionGaugeState();
		}
		bool flag4 = targetPlayer.IsOnCannonMode();
		bool flag5 = targetPlayer.targetFieldGimmickCannon is FieldGimmickCannonSpecial;
		bool flag6 = MonoBehaviourSingleton<InGameCameraManager>.IsValid() && MonoBehaviourSingleton<InGameCameraManager>.I.IsCameraModeBeam();
		cannonSpecialGaugePortrait.root.SetActive(flag4 && flag5 && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait && !flag6);
		cannonSpecialGaugeLandscape.root.SetActive(flag4 && flag5 && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait && !flag6);
		if (flag4 && flag5)
		{
			if (MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
			{
				validCannonSpecialGaugeInfo = cannonSpecialGaugePortrait;
			}
			else
			{
				validCannonSpecialGaugeInfo = cannonSpecialGaugeLandscape;
			}
		}
		if (validCannonSpecialGaugeInfo == null)
		{
			return;
		}
		float num3 = (!(targetPlayer.GetCannonChargeMax() > 0f)) ? 0f : targetPlayer.GetCannonChargeRate();
		if (validCannonSpecialGaugeInfo.root != null && validCannonSpecialGaugeInfo.root.get_activeInHierarchy())
		{
			bool flag7 = false;
			if (validCannonSpecialGaugeInfo.gaugeUI != null && validCannonSpecialGaugeInfo.gaugeUI.nowPercent != num3)
			{
				validCannonSpecialGaugeInfo.gaugeUI.SetPercent(num3, anim: false);
			}
		}
		validCannonSpecialGaugeInfo.gaugeNormalObj.SetActive(!targetPlayer.IsCannonFullCharged());
		validCannonSpecialGaugeInfo.gaugeChargedObj.SetActive(targetPlayer.IsCannonFullCharged());
		validCannonSpecialGaugeInfo.gaugeAddObj.SetActive(targetPlayer.IsCannonFullCharged());
		validCannonSpecialGaugeInfo.fullTipObj.SetActive(targetPlayer.IsCannonFullCharged());
		if (targetPlayer.IsCannonFullCharged())
		{
			if (!validCannonSpecialGaugeInfo.IsPlayAnim(CannonSpecialGaugeInfo.ANIM_STATE.FULL))
			{
				UITweenCtrl.Reset(validCannonSpecialGaugeInfo.gaugeAddObj.get_transform(), 3);
				UITweenCtrl.Play(validCannonSpecialGaugeInfo.gaugeAddObj.get_transform(), forward: true, null, is_input_block: false, 3);
				validCannonSpecialGaugeInfo.SetState(CannonSpecialGaugeInfo.ANIM_STATE.FULL);
			}
		}
		else
		{
			UITweenCtrl.Reset(validCannonSpecialGaugeInfo.gaugeAddObj.get_transform(), 3);
			validCannonSpecialGaugeInfo.state = 0;
		}
	}

	private void OnUpdateWeaponIndex()
	{
		if (!(targetPlayer == null) && (preWeaponIndex != targetPlayer.weaponIndex || preUniqueEquipmentIndex != targetPlayer.uniqueEquipmentIndex))
		{
			preWeaponIndex = targetPlayer.weaponIndex;
			preUniqueEquipmentIndex = targetPlayer.uniqueEquipmentIndex;
			ResetSpActionGaugeState();
			UpdateBurstUIInfo();
		}
	}

	public void SetGaugeEffectColor(SP_ATTACK_TYPE type)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		spActionGaugeADD_BOOST.sprite.color = spActionGaugeADD_BOOST.effectColor[(int)type];
	}

	public void ResetSpActionGaugeState()
	{
		spActionGaugeADD_BOOST.sprite.get_gameObject().SetActive(false);
		spActionGaugeADD_HALF.sprite.get_gameObject().SetActive(false);
		UITweenCtrl.Reset(spActionGaugeInfo.root.get_transform());
		UITweenCtrl.Reset(spActionGaugeInfo.root.get_transform(), 1);
		UITweenCtrl.Reset(spActionGaugeInfo.root.get_transform(), 2);
		spActionGaugeInfo.state = 0;
		if (targetPlayer == null)
		{
			return;
		}
		if (targetPlayer.IsSpActionGaugeHalfCharged())
		{
			spActionGaugeADD_HALF.sprite.get_gameObject().SetActive(true);
			if (spActionGaugeADD_HALF.sprite.get_gameObject().get_activeInHierarchy())
			{
				UITweenCtrl.Play(spActionGaugeInfo.root.get_transform(), forward: true, null, is_input_block: false, 1);
				spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.HALF);
			}
		}
		if (targetPlayer.IsSpActionGaugeFullCharged())
		{
			spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.FULL);
		}
	}

	private void EndShowBoost()
	{
		ChangeShowBoost(USE_ITEM_EFFECT_TYPE.NONE);
	}

	private void ChangeShowBoost(USE_ITEM_EFFECT_TYPE type)
	{
		boostRate.get_gameObject().SetActive(type != USE_ITEM_EFFECT_TYPE.NONE);
		boostTime.get_gameObject().SetActive(type != USE_ITEM_EFFECT_TYPE.NONE);
		int i = 0;
		for (int num = boostItems.Length; i < num; i++)
		{
			bool flag = boostItems[i].type == type;
			boostItems[i].obj.SetActive(flag);
			if (flag)
			{
				panelChange.UnLock();
				boostItems[i].anim.Reset();
				boostItems[i].anim.Play(forward: true, delegate
				{
					panelChange.Lock();
				});
			}
		}
	}

	private void UpdateShowBoost(BoostStatus boost)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		switch (boost.type)
		{
		case 1:
		case 2:
		case 3:
		case 201:
		case 210:
		case 212:
			boostRate.text = boost.GetBoostRateText();
			boostRate.color = boostAnimator.GetRateColor(boost.value);
			boostTime.text = ((boost.type != 210) ? boost.GetRemainTime() : string.Empty);
			break;
		}
	}

	public void SetDisableButtons(bool disable)
	{
		if (weaponChange != null)
		{
			weaponChange.SetDisableButtons(disable);
		}
	}

	public void DoEnable()
	{
		this.get_gameObject().SetActive(true);
	}

	public void DoDisable()
	{
		this.get_gameObject().SetActive(false);
	}

	public void SetHGPBoostUpdatePermitFlag(bool permit)
	{
		permitHGPBoostUpdate = permit;
	}

	public void DirectionSoulGauge(SoulEnergy soulEnergy, Vector3 worldHitPos)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_gameObject().get_activeInHierarchy())
		{
			this.StartCoroutine(_DirectionSoulGauge(soulEnergy, worldHitPos));
		}
	}

	private IEnumerator _DirectionSoulGauge(SoulEnergy soulEnergy, Vector3 worldHitPos)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		Transform trans = soulEnergy.GetEffectTrans(soulEffectDirection);
		if (!object.ReferenceEquals(trans, null))
		{
			Vector3 screenPos = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(worldHitPos);
			Vector3 uiPos = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenPos);
			uiPos.z = 1f;
			trans.set_position(uiPos);
			TransformInterpolator interp = trans.get_gameObject().GetComponent<TransformInterpolator>();
			if (object.ReferenceEquals(interp, null))
			{
				interp = trans.get_gameObject().AddComponent<TransformInterpolator>();
			}
			interp.Translate(add_value: new Vector3(Random.Range(0f - soulEffectAddRandomMax, soulEffectAddRandomMax), Random.Range(0f - soulEffectAddRandomMax, soulEffectAddRandomMax), 0f), _time: soulEffectTime, target: Vector3.get_zero(), ease_curve: soulEffectEaseCurve, add_curve: soulEffectAddCurve);
			yield return (object)new WaitForSeconds(soulEffectTime);
			soulEnergy.Absorbed();
		}
	}

	public void PlayChangeEvolveIcon(bool start)
	{
		if (object.ReferenceEquals(evolveGauge, null) || object.ReferenceEquals(evolveGauge.evolveIcon, null) || evolveGauge.evolveIcon.get_gameObject().get_activeSelf() == start)
		{
			return;
		}
		if (!object.ReferenceEquals(weaponChange, null))
		{
			if (start)
			{
				weaponChange.PlayEvolveIconAnim(delegate
				{
					EnableEvolveIcon(isEnable: true);
				});
			}
			else
			{
				weaponChange.PlayEvolveIconAnim(delegate
				{
					EnableEvolveIcon(isEnable: false);
				});
			}
		}
		else
		{
			EnableEvolveIcon(isEnable: true);
		}
	}

	public void SetEvolveIcon(uint evolveId)
	{
		if (!object.ReferenceEquals(evolveGauge, null))
		{
			evolveGauge.SetEvolveIcon(evolveId);
		}
	}

	public void EnableEvolveIcon(bool isEnable)
	{
		if (!object.ReferenceEquals(evolveGauge, null))
		{
			evolveGauge.EnableEvolveIcon(isEnable);
		}
	}

	public void SetEvolveRate(float rate)
	{
		if (!object.ReferenceEquals(evolveGauge, null))
		{
			evolveGauge.SetRate(rate);
			if (rate >= 1f)
			{
				SoundManager.PlayOneShotUISE(10000091);
			}
		}
	}

	public void RestrictPopMenu(bool isRestrict)
	{
		if (!object.ReferenceEquals(weaponChange, null))
		{
			weaponChange.SetRestrictPopMenu(isRestrict);
		}
	}

	public bool DoFullBurstAction()
	{
		if (m_burstBulletCtrl == null)
		{
			return false;
		}
		return m_burstBulletCtrl.FullBurstAction();
	}

	public bool DoShootAction()
	{
		if (m_burstBulletCtrl == null)
		{
			return false;
		}
		return m_burstBulletCtrl.ConsumeBulletAction();
	}

	public bool DoReloadAction()
	{
		if (m_burstBulletCtrl == null)
		{
			return false;
		}
		return m_burstBulletCtrl.ReloadAction();
	}

	public void CheckVisibleBulletUI()
	{
		if (!(m_burstBulletCtrl == null))
		{
			if (targetPlayer.IsValidBurstBulletUI())
			{
				m_burstBulletCtrl.SetActivateIconRoot();
			}
			else
			{
				m_burstBulletCtrl.SetDeactivateIconRoot();
			}
		}
	}

	public void UpdateBurstUIInfo()
	{
		if (!(m_burstBulletCtrl == null) && !(targetPlayer == null) && targetPlayer.thsCtrl != null)
		{
			UIBurstBulletUIController.InitParam initParam = new UIBurstBulletUIController.InitParam();
			initParam.MaxBulletCount = targetPlayer.thsCtrl.CurrentMaxBulletCount;
			initParam.CurrentRestBulletCount = targetPlayer.thsCtrl.CurrentRestBulletCount;
			UIBurstBulletUIController.InitParam param = initParam;
			m_burstBulletCtrl.Initialize(param);
		}
	}
}
