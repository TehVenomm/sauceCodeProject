using Network;
using System;
using System.Collections;
using UnityEngine;

public class UIPlayerStatus : MonoBehaviourSingleton<UIPlayerStatus>
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
		public UIHGauge gaugeUI;

		[SerializeField]
		public Renderer renderer;

		[SerializeField]
		public GameObject timerRoot;

		[SerializeField]
		public UIHGauge timerGaugeUI;

		[SerializeField]
		public Renderer timerRenderer;

		[SerializeField]
		public Color gaugeColorNormal;

		[SerializeField]
		public Color gaugeColorCharged;

		[NonSerialized]
		public int state;

		[SerializeField]
		public Color[] gaugeColorJump;

		[SerializeField]
		public Color[] gaugeColorSoul;

		[SerializeField]
		public Color[] gaugeColorSoulPairSwords;

		[SerializeField]
		public Color[] gaugeColorBurst;

		[SerializeField]
		public Color[] gaugeColorOracle;

		[SerializeField]
		public GameObject gaugeMemoriObj;

		public GameObject GetRoot(SP_ATTACK_TYPE spAttackType, Player.ATTACK_MODE mode)
		{
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.BURST:
				return timerRoot;
			case SP_ATTACK_TYPE.ORACLE:
				if (mode == Player.ATTACK_MODE.SPEAR)
				{
					return timerRoot;
				}
				return root;
			default:
				return root;
			}
		}

		public UIHGauge GetGaugeUI(SP_ATTACK_TYPE spAttackType, Player.ATTACK_MODE mode)
		{
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.BURST:
				return timerGaugeUI;
			case SP_ATTACK_TYPE.ORACLE:
				if (mode == Player.ATTACK_MODE.SPEAR)
				{
					return timerGaugeUI;
				}
				return gaugeUI;
			default:
				return gaugeUI;
			}
		}

		public Renderer GetRenderer(SP_ATTACK_TYPE spAttackType, Player.ATTACK_MODE mode)
		{
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.BURST:
				return timerRenderer;
			case SP_ATTACK_TYPE.ORACLE:
				if (mode == Player.ATTACK_MODE.SPEAR)
				{
					return timerRenderer;
				}
				return renderer;
			default:
				return renderer;
			}
		}

		public void SetActiveRoot(SP_ATTACK_TYPE spAttackType, Player.ATTACK_MODE mode, bool isActive)
		{
			GameObject gameObject = GetRoot(spAttackType, mode);
			GameObject gameObject2 = (root == gameObject) ? timerRoot : root;
			if (gameObject != null && gameObject.activeSelf != isActive)
			{
				gameObject.SetActive(isActive);
			}
			if (gameObject2 != null && gameObject2.activeSelf)
			{
				gameObject2.SetActive(value: false);
			}
		}

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
	protected class CoopFishingGaugeInfo
	{
		[SerializeField]
		public GameObject root;

		[SerializeField]
		public UISprite gaugeBlue;

		[SerializeField]
		public UISprite gaugeRed;

		[SerializeField]
		public GameObject fishBlue;

		[SerializeField]
		public GameObject fishRed;

		[SerializeField]
		public UISprite fishBlueSprite;

		[SerializeField]
		public UISprite fishRedSprite;

		[SerializeField]
		private Vector3 startPos = Vector3.zero;

		[SerializeField]
		private Vector3 endPos = Vector3.zero;

		public void SetRate(float rate)
		{
			gaugeBlue.fillAmount = rate;
			gaugeRed.fillAmount = rate;
			fishBlue.transform.localPosition = Vector3.Lerp(startPos, endPos, rate);
			fishRed.transform.localPosition = Vector3.Lerp(startPos, endPos, rate);
			fishBlueSprite.MarkAsChanged();
			fishRedSprite.MarkAsChanged();
		}

		public void SetPositive(bool isPositive)
		{
			gaugeBlue.gameObject.SetActive(isPositive);
			gaugeRed.gameObject.SetActive(!isPositive);
			fishBlue.SetActive(isPositive);
			fishRed.SetActive(!isPositive);
		}
	}

	public static readonly string UI_BURST_BULLET = "InternalUI/UI_InGame/Burst/InGameUIBurstBullet";

	public static readonly Vector3 UI_BURST_BULLET_POS = new Vector3(-35.3f, -55.5f, 0f);

	[SerializeField]
	protected UILabel playerName;

	[SerializeField]
	protected UILabel playerHp;

	[SerializeField]
	protected UILabel playerShieldHp;

	[SerializeField]
	protected UIHGauge hpGaugeUI;

	[SerializeField]
	protected UIHGauge healHpGaugeUI;

	[SerializeField]
	protected UIHGauge shieldHpGaugeUI;

	[SerializeField]
	protected ShieldHpGaugeEffect shieldHpGaugeADD;

	[SerializeField]
	protected GameObject itemInfo;

	[SerializeField]
	protected Transform dropIconN;

	[SerializeField]
	protected UILabel dropInfoN;

	[SerializeField]
	protected Transform dropIconR;

	[SerializeField]
	protected UILabel dropInfoR;

	[SerializeField]
	protected UIWeaponChange weaponChange;

	[SerializeField]
	protected UIEvolveGauge evolveGauge;

	[SerializeField]
	protected UIStatusIcon statusIcons;

	[SerializeField]
	protected UILabel lv;

	[SerializeField]
	protected UIHGauge expGauge;

	[SerializeField]
	protected SpActionGaugeInfo spActionGaugeInfo;

	[SerializeField]
	protected ShieldHpGaugeEffect spActionGaugeADD_BOOST;

	[SerializeField]
	protected ShieldHpGaugeEffect spActionGaugeADD_HALF;

	[SerializeField]
	protected ShieldHpGaugeEffect spActionTimerGaugeADD_BOOST;

	[SerializeField]
	protected ShieldHpGaugeEffect spActionTimerGaugeADD_HALF;

	[SerializeField]
	protected ShieldHpGaugeEffect burstSpActionTimerGaugeADD_HALF;

	[SerializeField]
	protected UILabel coins;

	[SerializeField]
	protected GameObject[] fieldInfo;

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
	protected float dropEffectTime = 1f;

	[SerializeField]
	protected AnimationCurve dropEffectEaseCurve = Curves.CreateEaseInCurve();

	[SerializeField]
	protected float dropEffectAddRandomMax;

	[SerializeField]
	protected AnimationCurve dropEffectAddCurve;

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

	public UIAutoBattleButton autoBattleButton;

	[SerializeField]
	private CoopFishingGaugeInfo coopFishingGaugeInfoPortrait;

	[SerializeField]
	private CoopFishingGaugeInfo coopFishingGaugeInfoLandscape;

	[SerializeField]
	private UIOracleStockUIController oracleStock;

	private CoopFishingGaugeInfo validCoopFishingGaugeInfo;

	private bool isField;

	private bool permitHGPBoostUpdate = true;

	private int lastHP = -1;

	private int lastShieldHP = -1;

	private int lastLV = -1;

	private int lastMoney = -1;

	private int preWeaponIndex;

	private int preUniqueEquipmentIndex;

	private UIBurstBulletUIController m_burstBulletCtrl;

	public Player targetPlayer
	{
		get;
		protected set;
	}

	public bool PermitHGPBoostUpdate => permitHGPBoostUpdate;

	protected ShieldHpGaugeEffect GetSpActionGaugeADD_BOOST(Player.ATTACK_MODE mode, SP_ATTACK_TYPE spAttackType)
	{
		switch (spAttackType)
		{
		case SP_ATTACK_TYPE.BURST:
			return spActionTimerGaugeADD_BOOST;
		case SP_ATTACK_TYPE.ORACLE:
			if (mode == Player.ATTACK_MODE.SPEAR)
			{
				return spActionTimerGaugeADD_BOOST;
			}
			return spActionGaugeADD_BOOST;
		default:
			return spActionGaugeADD_BOOST;
		}
	}

	protected ShieldHpGaugeEffect GetSpActionGaugeADD_HALF(Player.ATTACK_MODE mode, SP_ATTACK_TYPE spAttackType)
	{
		switch (spAttackType)
		{
		case SP_ATTACK_TYPE.BURST:
			return burstSpActionTimerGaugeADD_HALF;
		case SP_ATTACK_TYPE.ORACLE:
			switch (mode)
			{
			case Player.ATTACK_MODE.SPEAR:
				return burstSpActionTimerGaugeADD_HALF;
			case Player.ATTACK_MODE.PAIR_SWORDS:
				return spActionGaugeADD_HALF;
			default:
				return spActionTimerGaugeADD_HALF;
			}
		default:
			return spActionGaugeADD_HALF;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		boostRate.fontStyle = FontStyle.Italic;
		boostTime.fontStyle = FontStyle.Italic;
		base.gameObject.SetActive(value: false);
		CreateBurstBulletUI();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
		SyncRotatePosition();
	}

	protected override void OnDestroySingleton()
	{
		base.OnDestroySingleton();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		SyncRotatePosition();
	}

	private void SyncRotatePosition()
	{
		if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.NeedModifyInGamePlayerStatusPosition)
		{
			return;
		}
		DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
		UIWidget component = base.gameObject.GetComponent<UIWidget>();
		if (component != null)
		{
			if (SpecialDeviceManager.IsPortrait)
			{
				component.leftAnchor.absolute = specialDeviceInfo.InGameStatusAnchorPortrait.left;
				component.rightAnchor.absolute = specialDeviceInfo.InGameStatusAnchorPortrait.right;
				component.bottomAnchor.absolute = specialDeviceInfo.InGameStatusAnchorPortrait.bottom;
				component.topAnchor.absolute = specialDeviceInfo.InGameStatusAnchorPortrait.top;
			}
			else
			{
				component.leftAnchor.absolute = specialDeviceInfo.InGameStatusAnchorLandscape.left;
				component.rightAnchor.absolute = specialDeviceInfo.InGameStatusAnchorLandscape.right;
				component.bottomAnchor.absolute = specialDeviceInfo.InGameStatusAnchorLandscape.bottom;
				component.topAnchor.absolute = specialDeviceInfo.InGameStatusAnchorLandscape.top;
			}
			component.UpdateAnchors();
		}
	}

	private void CreateBurstBulletUI()
	{
		if (m_burstBulletCtrl != null)
		{
			return;
		}
		Transform transform = ResourceUtility.Realizes(Resources.Load(UI_BURST_BULLET), base.transform);
		if (!(transform == null))
		{
			transform.localPosition = UI_BURST_BULLET_POS;
			m_burstBulletCtrl = transform.GetComponent<UIBurstBulletUIController>();
			if (m_burstBulletCtrl != null)
			{
				UIBurstBulletUIController.InitParam param = new UIBurstBulletUIController.InitParam
				{
					MaxBulletCount = 6,
					CurrentRestBulletCount = 6
				};
				m_burstBulletCtrl.Initialize(param);
			}
		}
	}

	private void Start()
	{
		EffectCtrl[] componentsInChildren = boostAnimator.GetComponentsInChildren<EffectCtrl>(includeInactive: true);
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
			base.gameObject.SetActive(value: false);
			return;
		}
		statusIcons.target = player;
		if (weaponChange != null)
		{
			weaponChange.SetTarget(targetPlayer);
		}
		UpdateUI();
		isField = FieldManager.IsValidInGameNoQuest();
		itemInfo.SetActive(!isField);
		int i = 0;
		for (int num = fieldInfo.Length; i < num; i++)
		{
			fieldInfo[i].SetActive(isField);
		}
		if (!isField)
		{
			DropInfoUpdate();
		}
		UpDateStatusIcon();
		SetUpBoostAnimator();
		base.gameObject.SetActive(value: true);
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
			UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
			if (lv != null && lastLV != (int)userStatus.level)
			{
				lv.text = userStatus.level.ToString();
				lastLV = userStatus.level;
			}
			if (expGauge != null)
			{
				expGauge.SetPercent(userStatus.ExpProgress01);
			}
			if (isField && coins != null && lastMoney != userStatus.Money)
			{
				coins.text = userStatus.Money.ToString();
				lastMoney = userStatus.Money;
			}
		}
	}

	private void UpdateUI()
	{
		if (playerName != null && !string.IsNullOrEmpty(targetPlayer.charaName))
		{
			playerName.text = targetPlayer.charaName;
		}
		if (playerHp != null && lastHP != targetPlayer.hpShow)
		{
			lastHP = targetPlayer.hpShow;
			playerHp.text = targetPlayer.hpShow.ToString();
		}
		if (playerShieldHp != null && lastShieldHP != (int)targetPlayer.ShieldHp)
		{
			lastShieldHP = targetPlayer.ShieldHp;
			playerShieldHp.gameObject.SetActive(targetPlayer.IsValidShield());
			playerShieldHp.text = targetPlayer.ShieldHp.ToString();
		}
		if (hpGaugeUI != null)
		{
			float num = (targetPlayer.hpMax > 0) ? ((float)targetPlayer.hpShow / (float)targetPlayer.hpMax) : 0f;
			if (hpGaugeUI.nowPercent != num)
			{
				hpGaugeUI.SetPercent(num);
			}
		}
		if (healHpGaugeUI != null)
		{
			float num2 = (targetPlayer.hpMax > 0) ? ((float)targetPlayer.healHp / (float)targetPlayer.hpMax) : 0f;
			if (healHpGaugeUI.nowPercent != num2)
			{
				healHpGaugeUI.SetPercent(num2, anim: false);
			}
		}
		if (shieldHpGaugeUI != null && shieldHpGaugeADD.sprite != null)
		{
			float num3 = ((int)targetPlayer.ShieldHpMax > 0) ? ((float)(int)targetPlayer.ShieldHp / (float)(int)targetPlayer.ShieldHpMax) : 0f;
			shieldHpGaugeUI.gameObject.SetActive(targetPlayer.IsValidShield());
			if (!targetPlayer.IsValidShield())
			{
				shieldHpGaugeADD.sprite.alpha = 0.1f;
			}
			if (shieldHpGaugeUI.nowPercent != num3)
			{
				shieldHpGaugeUI.SetPercent(num3, anim: false);
				shieldHpGaugeADD.sprite.width = (int)(num3 * (float)shieldHpGaugeADD.sizeMax + (1f - num3) * (float)shieldHpGaugeADD.sizeMin);
			}
		}
		OnUpdateWeaponIndex();
		bool flag = targetPlayer.IsValidSpActionMemori();
		if (spActionGaugeInfo.gaugeMemoriObj.activeSelf != flag)
		{
			spActionGaugeInfo.gaugeMemoriObj.SetActive(flag);
		}
		bool flag2 = (!targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST)) ? targetPlayer.IsValidSpActionGauge() : (targetPlayer.isBoostMode && targetPlayer.IsValidSpActionGauge());
		GameObject root = spActionGaugeInfo.GetRoot(targetPlayer.spAttackType, targetPlayer.attackMode);
		spActionGaugeInfo.SetActiveRoot(targetPlayer.spAttackType, targetPlayer.attackMode, flag2);
		if (flag2)
		{
			Renderer renderer = spActionGaugeInfo.GetRenderer(targetPlayer.spAttackType, targetPlayer.attackMode);
			if (renderer != null)
			{
				int num4 = targetPlayer.CheckGaugeLevel();
				Color color = (num4 != -1) ? ((!targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL)) ? spActionGaugeInfo.gaugeColorJump[num4] : spActionGaugeInfo.gaugeColorSoulPairSwords[num4]) : ((targetPlayer.spAttackType == SP_ATTACK_TYPE.ORACLE) ? ((targetPlayer.IsSpActionGaugeHalfCharged() || targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorOracle[1] : spActionGaugeInfo.gaugeColorOracle[0]) : ((targetPlayer.spAttackType == SP_ATTACK_TYPE.BURST) ? ((targetPlayer.IsSpActionGaugeHalfCharged() || targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorBurst[1] : spActionGaugeInfo.gaugeColorBurst[0]) : ((targetPlayer.spAttackType != SP_ATTACK_TYPE.SOUL) ? ((targetPlayer.IsSpActionGaugeHalfCharged() || targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorCharged : spActionGaugeInfo.gaugeColorNormal) : ((targetPlayer.IsSpActionGaugeHalfCharged() || targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorSoul[1] : spActionGaugeInfo.gaugeColorSoul[0]))));
				if (color != renderer.material.color)
				{
					renderer.material.color = color;
				}
			}
			float num5 = (targetPlayer.CurrentWeaponSpActionGaugeMax > 0f) ? (targetPlayer.CurrentWeaponSpActionGauge / targetPlayer.CurrentWeaponSpActionGaugeMax) : 0f;
			bool flag3 = false;
			UIHGauge gaugeUI = spActionGaugeInfo.GetGaugeUI(targetPlayer.spAttackType, targetPlayer.attackMode);
			if (gaugeUI != null)
			{
				flag3 = (gaugeUI.nowPercent != num5);
				if (flag3)
				{
					gaugeUI.SetPercent(num5, anim: false);
				}
			}
			ShieldHpGaugeEffect shieldHpGaugeEffect = GetSpActionGaugeADD_BOOST(targetPlayer.attackMode, targetPlayer.spAttackType);
			ShieldHpGaugeEffect shieldHpGaugeEffect2 = GetSpActionGaugeADD_HALF(targetPlayer.attackMode, targetPlayer.spAttackType);
			UISprite sprite = shieldHpGaugeEffect.sprite;
			if (flag3)
			{
				sprite.width = (int)(num5 * (float)shieldHpGaugeEffect.sizeMax + (1f - num5) * (float)shieldHpGaugeEffect.sizeMin);
			}
			if (root != null && root.activeInHierarchy)
			{
				if (targetPlayer.IsSpActionGaugeHalfCharged() && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.HALF))
				{
					sprite.gameObject.SetActive(value: false);
					shieldHpGaugeEffect2.sprite.gameObject.SetActive(value: true);
					UITweenCtrl.Reset(root.transform, 1);
					UITweenCtrl.Play(root.transform, forward: true, null, is_input_block: false, 1);
					spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.HALF);
					SoundManager.PlayOneShotUISE(40000358);
				}
				if (targetPlayer.IsSpActionGaugeFullCharged() && !targetPlayer.isBoostMode && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.FULL))
				{
					sprite.gameObject.SetActive(value: false);
					shieldHpGaugeEffect2.sprite.gameObject.SetActive(value: true);
					UITweenCtrl.Reset(root.transform, 2);
					UITweenCtrl.Play(root.transform, forward: true, null, is_input_block: false, 2);
					spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.FULL);
					SoundManager.PlayOneShotUISE(40000359);
				}
				if (targetPlayer.isBoostMode && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.BOOST))
				{
					sprite.gameObject.SetActive(value: true);
					shieldHpGaugeEffect2.sprite.gameObject.SetActive(value: false);
					UITweenCtrl.Reset(root.transform);
					UITweenCtrl.Play(root.transform, forward: true, null, is_input_block: false);
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
		CheckVisibleBulletUI();
		UpdateOracleStock();
		bool flag4 = targetPlayer.fishingCtrl.IsFighting();
		bool flag5 = targetPlayer.fishingCtrl.IsCooperating();
		coopFishingGaugeInfoPortrait.root.SetActive((flag4 | flag5) && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		coopFishingGaugeInfoLandscape.root.SetActive((flag4 | flag5) && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		if (flag4 | flag5)
		{
			validCoopFishingGaugeInfo = (MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait ? coopFishingGaugeInfoPortrait : coopFishingGaugeInfoLandscape);
		}
		if (validCoopFishingGaugeInfo != null && validCoopFishingGaugeInfo.root != null && validCoopFishingGaugeInfo.root.activeInHierarchy)
		{
			validCoopFishingGaugeInfo.SetRate(targetPlayer.fishingCtrl.GetCoopFishingGaugeRate());
			validCoopFishingGaugeInfo.SetPositive(targetPlayer.fishingCtrl.IsGaugePositive());
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

	public void SetGaugeEffectColor(Player.ATTACK_MODE mode, SP_ATTACK_TYPE type)
	{
		ShieldHpGaugeEffect shieldHpGaugeEffect = GetSpActionGaugeADD_BOOST(mode, type);
		shieldHpGaugeEffect.sprite.color = shieldHpGaugeEffect.effectColor[(int)type];
	}

	public void ResetSpActionGaugeState()
	{
		ShieldHpGaugeEffect shieldHpGaugeEffect = GetSpActionGaugeADD_BOOST(targetPlayer.attackMode, targetPlayer.spAttackType);
		ShieldHpGaugeEffect shieldHpGaugeEffect2 = GetSpActionGaugeADD_HALF(targetPlayer.attackMode, targetPlayer.spAttackType);
		shieldHpGaugeEffect.sprite.gameObject.SetActive(value: false);
		shieldHpGaugeEffect2.sprite.gameObject.SetActive(value: false);
		GameObject root = spActionGaugeInfo.GetRoot(targetPlayer.spAttackType, targetPlayer.attackMode);
		UITweenCtrl.Reset(root.transform);
		UITweenCtrl.Reset(root.transform, 1);
		UITweenCtrl.Reset(root.transform, 2);
		spActionGaugeInfo.state = 0;
		if (targetPlayer == null)
		{
			return;
		}
		if (targetPlayer.IsSpActionGaugeHalfCharged())
		{
			shieldHpGaugeEffect2.sprite.gameObject.SetActive(value: true);
			if (shieldHpGaugeEffect2.sprite.gameObject.activeInHierarchy)
			{
				UITweenCtrl.Play(root.transform, forward: true, null, is_input_block: false, 1);
				spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.HALF);
			}
		}
		if (targetPlayer.IsSpActionGaugeFullCharged())
		{
			spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.FULL);
		}
	}

	public void DropInfoUpdate()
	{
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			if (dropInfoR != null)
			{
				dropInfoR.text = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropRare.ToString();
			}
			if (dropInfoN != null)
			{
				dropInfoN.text = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropNormal.ToString();
			}
		}
	}

	public void UpDateStatusIcon()
	{
		statusIcons.UpDateStatusIcon();
	}

	private void EndShowBoost()
	{
		ChangeShowBoost(USE_ITEM_EFFECT_TYPE.NONE);
	}

	private void ChangeShowBoost(USE_ITEM_EFFECT_TYPE type)
	{
		boostRate.gameObject.SetActive(type != USE_ITEM_EFFECT_TYPE.NONE);
		boostTime.gameObject.SetActive(type != USE_ITEM_EFFECT_TYPE.NONE);
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
			boostTime.text = ((boost.type == 210) ? "" : boost.GetRemainTime());
			break;
		}
	}

	public void AddItemNum(Vector3 world_hit_pos, int rarity, bool is_right)
	{
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0 && base.gameObject.activeInHierarchy)
		{
			StartCoroutine(_AddItemNum(world_hit_pos, rarity, is_right));
			return;
		}
		if (rarity > 0)
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropRare++;
		}
		else
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropNormal++;
		}
		MonoBehaviourSingleton<UIPlayerStatus>.I.DropInfoUpdate();
	}

	private IEnumerator _AddItemNum(Vector3 world_hit_pos, int rarity, bool is_right)
	{
		Transform parent;
		Vector3 target;
		if (rarity > 0)
		{
			parent = dropIconR;
			target = dropInfoR.transform.localPosition - dropIconR.localPosition;
		}
		else
		{
			parent = dropIconN;
			target = dropInfoN.transform.localPosition - dropIconN.localPosition;
		}
		Transform uIEffect = EffectManager.GetUIEffect("ef_ui_downenergy_01", parent);
		if (uIEffect == null)
		{
			yield break;
		}
		Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos);
		Vector3 position2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
		position2.z = 1f;
		uIEffect.position = position2;
		GameObject obj = uIEffect.gameObject;
		TransformInterpolator transformInterpolator = obj.AddComponent<TransformInterpolator>();
		if (!(transformInterpolator == null))
		{
			Vector3 add_value = new Vector3(is_right ? dropEffectAddRandomMax : ((0f - dropEffectAddRandomMax) * 2f), dropEffectAddRandomMax * 2f, 0f);
			transformInterpolator.Translate(dropEffectTime, target, dropEffectEaseCurve, add_value, dropEffectAddCurve);
			yield return new WaitForSeconds(dropEffectTime);
			EffectManager.ReleaseEffect(obj);
			if (rarity > 0)
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropRare++;
				SoundManager.PlayOneShotUISE(40000154);
			}
			else
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropNormal++;
				SoundManager.PlayOneShotUISE(40000153);
			}
			DropInfoUpdate();
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
		base.gameObject.SetActive(value: true);
	}

	public void DoDisable()
	{
		base.gameObject.SetActive(value: false);
	}

	public void SetHGPBoostUpdatePermitFlag(bool permit)
	{
		permitHGPBoostUpdate = permit;
	}

	public void DirectionSoulGauge(SoulEnergy soulEnergy, Vector3 worldHitPos)
	{
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine(_DirectionSoulGauge(soulEnergy, worldHitPos));
		}
	}

	private IEnumerator _DirectionSoulGauge(SoulEnergy soulEnergy, Vector3 worldHitPos)
	{
		Transform effectTrans = soulEnergy.GetEffectTrans(soulEffectDirection);
		if ((object)effectTrans != null)
		{
			Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(worldHitPos);
			Vector3 position2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
			position2.z = 1f;
			effectTrans.position = position2;
			TransformInterpolator transformInterpolator = effectTrans.gameObject.GetComponent<TransformInterpolator>();
			if ((object)transformInterpolator == null)
			{
				transformInterpolator = effectTrans.gameObject.AddComponent<TransformInterpolator>();
			}
			transformInterpolator.Translate(add_value: new Vector3(UnityEngine.Random.Range(0f - soulEffectAddRandomMax, soulEffectAddRandomMax), UnityEngine.Random.Range(0f - soulEffectAddRandomMax, soulEffectAddRandomMax), 0f), _time: soulEffectTime, target: Vector3.zero, ease_curve: soulEffectEaseCurve, add_curve: soulEffectAddCurve);
			yield return new WaitForSeconds(soulEffectTime);
			soulEnergy.Absorbed();
		}
	}

	public void PlayChangeEvolveIcon(bool start)
	{
		if ((object)evolveGauge == null || (object)evolveGauge.evolveIcon == null || evolveGauge.evolveIcon.gameObject.activeSelf == start)
		{
			return;
		}
		if ((object)weaponChange != null)
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
		if ((object)evolveGauge != null)
		{
			evolveGauge.SetEvolveIcon(evolveId);
		}
	}

	public void EnableEvolveIcon(bool isEnable)
	{
		if ((object)evolveGauge != null)
		{
			evolveGauge.EnableEvolveIcon(isEnable);
		}
	}

	public void SetEvolveRate(float rate)
	{
		if ((object)evolveGauge != null)
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
		if ((object)weaponChange != null)
		{
			weaponChange.SetRestrictPopMenu(isRestrict);
		}
	}

	public void SetDisableRalltBtn(bool isDisable)
	{
		weaponChange.SetDisableRallyBtn(isDisable);
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

	private void CheckVisibleBulletUI()
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
			UIBurstBulletUIController.InitParam param = new UIBurstBulletUIController.InitParam
			{
				MaxBulletCount = targetPlayer.thsCtrl.CurrentMaxBulletCount,
				CurrentRestBulletCount = targetPlayer.thsCtrl.CurrentRestBulletCount
			};
			m_burstBulletCtrl.Initialize(param);
		}
	}

	public static void OnLoadComplete()
	{
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.UpdateBurstUIInfo();
			MonoBehaviourSingleton<UIPlayerStatus>.I.InitializeOracleStock();
		}
	}

	public void UpdateOracleStock()
	{
		if (targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
		{
			oracleStock.SetActive(enabled: true);
			oracleStock.UpdateStock(targetPlayer.spearCtrl.StockedCount);
		}
		else
		{
			oracleStock.SetActive(enabled: false);
		}
	}

	public void InitializeOracleStock()
	{
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid() && targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
		{
			oracleStock.Initialize(targetPlayer.spearCtrl.MaxStockCount);
		}
	}

	public void ChangeUniqueEquipment()
	{
		weaponChange.InitWepIcons();
	}

	public void SetEnableWeaponChangeButton(bool enabled)
	{
		weaponChange.SetEnableChangeButton(enabled);
	}

	public bool IsEnableWeaponChangeButton()
	{
		return weaponChange.IsEnableChangeButton();
	}
}
