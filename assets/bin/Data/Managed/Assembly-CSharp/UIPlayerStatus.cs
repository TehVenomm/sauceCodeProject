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
		public GameObject gaugeMemoriObj;

		public GameObject GetRoot(SP_ATTACK_TYPE spAttackType)
		{
			if (spAttackType == SP_ATTACK_TYPE.BURST)
			{
				return timerRoot;
			}
			return root;
		}

		public UIHGauge GetGaugeUI(SP_ATTACK_TYPE spAttackType)
		{
			if (spAttackType == SP_ATTACK_TYPE.BURST)
			{
				return timerGaugeUI;
			}
			return gaugeUI;
		}

		public Renderer GetRenderer(SP_ATTACK_TYPE spAttackType)
		{
			if (spAttackType == SP_ATTACK_TYPE.BURST)
			{
				return timerRenderer;
			}
			return renderer;
		}

		public void SetActiveRoot(SP_ATTACK_TYPE spAttackType, bool isActive)
		{
			GameObject val = GetRoot(spAttackType);
			GameObject val2 = (!(root == val)) ? root : timerRoot;
			if (val != null && val.get_activeSelf() != isActive)
			{
				val.SetActive(isActive);
			}
			if (val2 != null && val2.get_activeSelf())
			{
				val2.SetActive(false);
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

	private bool isField;

	private bool permitHGPBoostUpdate = true;

	private int lastHP = -1;

	private int lastShieldHP = -1;

	private int lastLV = -1;

	private int lastMoney = -1;

	private int preWeaponIndex;

	private UIBurstBulletUIController m_burstBulletCtrl;

	public Player targetPlayer
	{
		get;
		protected set;
	}

	public bool PermitHGPBoostUpdate => permitHGPBoostUpdate;

	protected ShieldHpGaugeEffect GetSpActionGaugeADD_BOOST(SP_ATTACK_TYPE spAttackType)
	{
		if (spAttackType == SP_ATTACK_TYPE.BURST)
		{
			return spActionTimerGaugeADD_BOOST;
		}
		return spActionGaugeADD_BOOST;
	}

	protected ShieldHpGaugeEffect GetSpActionGaugeADD_HALF(SP_ATTACK_TYPE spAttackType)
	{
		if (spAttackType == SP_ATTACK_TYPE.BURST)
		{
			return spActionTimerGaugeADD_HALF;
		}
		return spActionGaugeADD_HALF;
	}

	protected override void Awake()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		boostRate.fontStyle = 2;
		boostTime.fontStyle = 2;
		this.get_gameObject().SetActive(false);
		CreateBurstBulletUI();
	}

	private void CreateBurstBulletUI()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0023: Expected O, but got Unknown
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_burstBulletCtrl != null))
		{
			Transform val = ResourceUtility.Realizes(Resources.Load(UI_BURST_BULLET), this.get_transform(), -1);
			if (!(val == null))
			{
				val.set_localPosition(UI_BURST_BULLET_POS);
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
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		targetPlayer = player;
		if (targetPlayer == null)
		{
			this.get_gameObject().SetActive(false);
		}
		else
		{
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
			this.get_gameObject().SetActive(true);
		}
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
				expGauge.SetPercent(userStatus.ExpProgress01, true);
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
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Expected O, but got Unknown
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Expected O, but got Unknown
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Expected O, but got Unknown
		//IL_0704: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Expected O, but got Unknown
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Expected O, but got Unknown
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_0780: Expected O, but got Unknown
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
			playerShieldHp.get_gameObject().SetActive(targetPlayer.IsValidShield());
			playerShieldHp.text = targetPlayer.ShieldHp.ToString();
		}
		if (hpGaugeUI != null)
		{
			float num = (targetPlayer.hpMax <= 0) ? 0f : ((float)targetPlayer.hpShow / (float)targetPlayer.hpMax);
			if (hpGaugeUI.nowPercent != num)
			{
				hpGaugeUI.SetPercent(num, true);
			}
		}
		if (healHpGaugeUI != null)
		{
			float num2 = (targetPlayer.hpMax <= 0) ? 0f : ((float)targetPlayer.healHp / (float)targetPlayer.hpMax);
			if (healHpGaugeUI.nowPercent != num2)
			{
				healHpGaugeUI.SetPercent(num2, false);
			}
		}
		if (shieldHpGaugeUI != null && shieldHpGaugeADD.sprite != null)
		{
			float num3 = ((int)targetPlayer.ShieldHpMax <= 0) ? 0f : ((float)(int)targetPlayer.ShieldHp / (float)(int)targetPlayer.ShieldHpMax);
			shieldHpGaugeUI.get_gameObject().SetActive(targetPlayer.IsValidShield());
			shieldHpGaugeADD.sprite.get_gameObject().SetActive(targetPlayer.IsValidShield());
			if (shieldHpGaugeUI.nowPercent != num3)
			{
				shieldHpGaugeUI.SetPercent(num3, false);
				shieldHpGaugeADD.sprite.width = (int)(num3 * (float)shieldHpGaugeADD.sizeMax + (1f - num3) * (float)shieldHpGaugeADD.sizeMin);
			}
		}
		OnUpdateWeaponIndex();
		bool flag = targetPlayer.IsValidSpActionMemori();
		if (spActionGaugeInfo.gaugeMemoriObj.get_activeSelf() != flag)
		{
			spActionGaugeInfo.gaugeMemoriObj.SetActive(flag);
		}
		bool flag2 = (!targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST)) ? targetPlayer.IsValidSpActionGauge() : (targetPlayer.isBoostMode && targetPlayer.IsValidSpActionGauge());
		GameObject root = spActionGaugeInfo.GetRoot(targetPlayer.spAttackType);
		spActionGaugeInfo.SetActiveRoot(targetPlayer.spAttackType, flag2);
		if (flag2)
		{
			Renderer renderer = spActionGaugeInfo.GetRenderer(targetPlayer.spAttackType);
			if (renderer != null)
			{
				int num4 = targetPlayer.CheckGaugeLevel();
				Color val = (num4 != -1) ? ((!targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL)) ? spActionGaugeInfo.gaugeColorJump[num4] : spActionGaugeInfo.gaugeColorSoulPairSwords[num4]) : ((targetPlayer.spAttackType == SP_ATTACK_TYPE.BURST) ? ((!targetPlayer.IsSpActionGaugeHalfCharged() && !targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorBurst[0] : spActionGaugeInfo.gaugeColorBurst[1]) : ((targetPlayer.spAttackType != SP_ATTACK_TYPE.SOUL) ? ((!targetPlayer.IsSpActionGaugeHalfCharged() && !targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorNormal : spActionGaugeInfo.gaugeColorCharged) : ((!targetPlayer.IsSpActionGaugeHalfCharged() && !targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorSoul[0] : spActionGaugeInfo.gaugeColorSoul[1])));
				if (val != renderer.get_material().get_color())
				{
					renderer.get_material().set_color(val);
				}
			}
			float num5 = (!(targetPlayer.CurrentWeaponSpActionGaugeMax > 0f)) ? 0f : (targetPlayer.CurrentWeaponSpActionGauge / targetPlayer.CurrentWeaponSpActionGaugeMax);
			bool flag3 = false;
			UIHGauge gaugeUI = spActionGaugeInfo.GetGaugeUI(targetPlayer.spAttackType);
			if (gaugeUI != null)
			{
				flag3 = (gaugeUI.nowPercent != num5);
				if (flag3)
				{
					gaugeUI.SetPercent(num5, false);
				}
			}
			ShieldHpGaugeEffect shieldHpGaugeEffect = GetSpActionGaugeADD_BOOST(targetPlayer.spAttackType);
			ShieldHpGaugeEffect shieldHpGaugeEffect2 = GetSpActionGaugeADD_HALF(targetPlayer.spAttackType);
			UISprite sprite = shieldHpGaugeEffect.sprite;
			if (flag3)
			{
				sprite.width = (int)(num5 * (float)shieldHpGaugeEffect.sizeMax + (1f - num5) * (float)shieldHpGaugeEffect.sizeMin);
			}
			if (root != null && root.get_activeInHierarchy())
			{
				if (targetPlayer.IsSpActionGaugeHalfCharged() && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.HALF))
				{
					sprite.get_gameObject().SetActive(false);
					shieldHpGaugeEffect2.sprite.get_gameObject().SetActive(true);
					UITweenCtrl.Reset(root.get_transform(), 1);
					UITweenCtrl.Play(root.get_transform(), true, null, false, 1);
					spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.HALF);
					SoundManager.PlayOneShotUISE(40000358);
				}
				if (targetPlayer.IsSpActionGaugeFullCharged() && !targetPlayer.isBoostMode && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.FULL))
				{
					sprite.get_gameObject().SetActive(false);
					shieldHpGaugeEffect2.sprite.get_gameObject().SetActive(true);
					UITweenCtrl.Reset(root.get_transform(), 2);
					UITweenCtrl.Play(root.get_transform(), true, null, false, 2);
					spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.FULL);
					SoundManager.PlayOneShotUISE(40000359);
				}
				if (targetPlayer.isBoostMode && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.BOOST))
				{
					sprite.get_gameObject().SetActive(true);
					shieldHpGaugeEffect2.sprite.get_gameObject().SetActive(false);
					UITweenCtrl.Reset(root.get_transform(), 0);
					UITweenCtrl.Play(root.get_transform(), true, null, false, 0);
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
	}

	private void OnUpdateWeaponIndex()
	{
		if (!(targetPlayer == null) && preWeaponIndex != targetPlayer.weaponIndex)
		{
			preWeaponIndex = targetPlayer.weaponIndex;
			ResetSpActionGaugeState();
			UpdateBurstUIInfo();
		}
	}

	public void SetGaugeEffectColor(SP_ATTACK_TYPE type)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		ShieldHpGaugeEffect shieldHpGaugeEffect = GetSpActionGaugeADD_BOOST(type);
		shieldHpGaugeEffect.sprite.color = shieldHpGaugeEffect.effectColor[(int)type];
	}

	public void ResetSpActionGaugeState()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		ShieldHpGaugeEffect shieldHpGaugeEffect = GetSpActionGaugeADD_BOOST(targetPlayer.spAttackType);
		ShieldHpGaugeEffect shieldHpGaugeEffect2 = GetSpActionGaugeADD_HALF(targetPlayer.spAttackType);
		shieldHpGaugeEffect.sprite.get_gameObject().SetActive(false);
		shieldHpGaugeEffect2.sprite.get_gameObject().SetActive(false);
		GameObject root = spActionGaugeInfo.GetRoot(targetPlayer.spAttackType);
		UITweenCtrl.Reset(root.get_transform(), 0);
		UITweenCtrl.Reset(root.get_transform(), 1);
		UITweenCtrl.Reset(root.get_transform(), 2);
		spActionGaugeInfo.state = 0;
		if (!(targetPlayer == null))
		{
			if (targetPlayer.IsSpActionGaugeHalfCharged())
			{
				shieldHpGaugeEffect2.sprite.get_gameObject().SetActive(true);
				if (shieldHpGaugeEffect2.sprite.get_gameObject().get_activeInHierarchy())
				{
					UITweenCtrl.Play(root.get_transform(), true, null, false, 1);
					spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.HALF);
				}
			}
			if (targetPlayer.IsSpActionGaugeFullCharged())
			{
				spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.FULL);
			}
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
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
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
				boostItems[i].anim.Play(true, delegate
				{
					panelChange.Lock();
				});
			}
		}
	}

	private void UpdateShowBoost(BoostStatus boost)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		switch (boost.type)
		{
		case 1:
		case 2:
		case 3:
		case 201:
		case 210:
			boostRate.text = boost.GetBoostRateText();
			boostRate.color = boostAnimator.GetRateColor(boost.value);
			boostTime.text = ((boost.type != 210) ? boost.GetRemainTime() : string.Empty);
			break;
		}
	}

	public void AddItemNum(Vector3 world_hit_pos, int rarity, bool is_right)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0 && this.get_gameObject().get_activeInHierarchy())
		{
			this.StartCoroutine(_AddItemNum(world_hit_pos, rarity, is_right));
		}
		else
		{
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
	}

	private IEnumerator _AddItemNum(Vector3 world_hit_pos, int rarity, bool is_right)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Transform parent;
		Vector3 offset;
		if (rarity > 0)
		{
			parent = dropIconR;
			offset = dropInfoR.get_transform().get_localPosition() - dropIconR.get_localPosition();
		}
		else
		{
			parent = dropIconN;
			offset = dropInfoN.get_transform().get_localPosition() - dropIconN.get_localPosition();
		}
		Transform effect = EffectManager.GetUIEffect("ef_ui_downenergy_01", parent, -0.001f, 0, null);
		if (!(effect == null))
		{
			Vector3 screen_pos = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos);
			Vector3 ui_pos = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screen_pos);
			ui_pos.z = 1f;
			effect.set_position(ui_pos);
			GameObject obj = effect.get_gameObject();
			TransformInterpolator interp = obj.AddComponent<TransformInterpolator>();
			if (!(interp == null))
			{
				interp.Translate(add_value: new Vector3((!is_right) ? ((0f - dropEffectAddRandomMax) * 2f) : dropEffectAddRandomMax, dropEffectAddRandomMax * 2f, 0f), _time: dropEffectTime, target: offset, ease_curve: dropEffectEaseCurve, add_curve: dropEffectAddCurve);
				yield return (object)new WaitForSeconds(dropEffectTime);
				EffectManager.ReleaseEffect(obj, true, false);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(true);
	}

	public void DoDisable()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(false);
	}

	public void SetHGPBoostUpdatePermitFlag(bool permit)
	{
		permitHGPBoostUpdate = permit;
	}

	public void DirectionSoulGauge(SoulEnergy soulEnergy, Vector3 worldHitPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_gameObject().get_activeInHierarchy())
		{
			this.StartCoroutine(_DirectionSoulGauge(soulEnergy, worldHitPos));
		}
	}

	private IEnumerator _DirectionSoulGauge(SoulEnergy soulEnergy, Vector3 worldHitPos)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
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

	public unsafe void PlayChangeEvolveIcon(bool start)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		if (!object.ReferenceEquals(evolveGauge, null) && !object.ReferenceEquals(evolveGauge.evolveIcon, null) && evolveGauge.evolveIcon.get_gameObject().get_activeSelf() != start)
		{
			if (!object.ReferenceEquals(weaponChange, null))
			{
				if (start)
				{
					weaponChange.PlayEvolveIconAnim(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
				else
				{
					weaponChange.PlayEvolveIconAnim(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
			}
			else
			{
				EnableEvolveIcon(true);
			}
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
			UIBurstBulletUIController.InitParam initParam = new UIBurstBulletUIController.InitParam();
			initParam.MaxBulletCount = targetPlayer.thsCtrl.CurrentMaxBulletCount;
			initParam.CurrentRestBulletCount = targetPlayer.thsCtrl.CurrentRestBulletCount;
			UIBurstBulletUIController.InitParam param = initParam;
			m_burstBulletCtrl.Initialize(param);
		}
	}
}
