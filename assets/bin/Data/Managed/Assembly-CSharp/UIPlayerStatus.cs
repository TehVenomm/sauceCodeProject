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
			GameObject gameObject = GetRoot(spAttackType);
			GameObject gameObject2 = (!((UnityEngine.Object)root == (UnityEngine.Object)gameObject)) ? root : timerRoot;
			if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null && gameObject.activeSelf != isActive)
			{
				gameObject.SetActive(isActive);
			}
			if ((UnityEngine.Object)gameObject2 != (UnityEngine.Object)null && gameObject2.activeSelf)
			{
				gameObject2.SetActive(false);
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
		base.Awake();
		boostRate.fontStyle = FontStyle.Italic;
		boostTime.fontStyle = FontStyle.Italic;
		base.gameObject.SetActive(false);
		CreateBurstBulletUI();
	}

	private void CreateBurstBulletUI()
	{
		if (!((UnityEngine.Object)m_burstBulletCtrl != (UnityEngine.Object)null))
		{
			Transform transform = ResourceUtility.Realizes(Resources.Load(UI_BURST_BULLET), base.transform, -1);
			if (!((UnityEngine.Object)transform == (UnityEngine.Object)null))
			{
				transform.localPosition = UI_BURST_BULLET_POS;
				m_burstBulletCtrl = transform.GetComponent<UIBurstBulletUIController>();
				if ((UnityEngine.Object)m_burstBulletCtrl != (UnityEngine.Object)null)
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
		targetPlayer = player;
		if ((UnityEngine.Object)targetPlayer == (UnityEngine.Object)null)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			statusIcons.target = player;
			if ((UnityEngine.Object)weaponChange != (UnityEngine.Object)null)
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
			base.gameObject.SetActive(true);
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
		if (!((UnityEngine.Object)targetPlayer == (UnityEngine.Object)null))
		{
			UpdateUI();
			UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
			if ((UnityEngine.Object)lv != (UnityEngine.Object)null && lastLV != (int)userStatus.level)
			{
				lv.text = userStatus.level.ToString();
				lastLV = userStatus.level;
			}
			if ((UnityEngine.Object)expGauge != (UnityEngine.Object)null)
			{
				expGauge.SetPercent(userStatus.ExpProgress01, true);
			}
			if (isField && (UnityEngine.Object)coins != (UnityEngine.Object)null && lastMoney != userStatus.Money)
			{
				coins.text = userStatus.Money.ToString();
				lastMoney = userStatus.Money;
			}
		}
	}

	private void UpdateUI()
	{
		if ((UnityEngine.Object)playerName != (UnityEngine.Object)null && !string.IsNullOrEmpty(targetPlayer.charaName))
		{
			playerName.text = targetPlayer.charaName;
		}
		if ((UnityEngine.Object)playerHp != (UnityEngine.Object)null && lastHP != targetPlayer.hpShow)
		{
			lastHP = targetPlayer.hpShow;
			playerHp.text = targetPlayer.hpShow.ToString();
		}
		if ((UnityEngine.Object)playerShieldHp != (UnityEngine.Object)null && lastShieldHP != (int)targetPlayer.ShieldHp)
		{
			lastShieldHP = targetPlayer.ShieldHp;
			playerShieldHp.gameObject.SetActive(targetPlayer.IsValidShield());
			playerShieldHp.text = targetPlayer.ShieldHp.ToString();
		}
		if ((UnityEngine.Object)hpGaugeUI != (UnityEngine.Object)null)
		{
			float num = (targetPlayer.hpMax <= 0) ? 0f : ((float)targetPlayer.hpShow / (float)targetPlayer.hpMax);
			if (hpGaugeUI.nowPercent != num)
			{
				hpGaugeUI.SetPercent(num, true);
			}
		}
		if ((UnityEngine.Object)healHpGaugeUI != (UnityEngine.Object)null)
		{
			float num2 = (targetPlayer.hpMax <= 0) ? 0f : ((float)targetPlayer.healHp / (float)targetPlayer.hpMax);
			if (healHpGaugeUI.nowPercent != num2)
			{
				healHpGaugeUI.SetPercent(num2, false);
			}
		}
		if ((UnityEngine.Object)shieldHpGaugeUI != (UnityEngine.Object)null && (UnityEngine.Object)shieldHpGaugeADD.sprite != (UnityEngine.Object)null)
		{
			float num3 = ((int)targetPlayer.ShieldHpMax <= 0) ? 0f : ((float)(int)targetPlayer.ShieldHp / (float)(int)targetPlayer.ShieldHpMax);
			shieldHpGaugeUI.gameObject.SetActive(targetPlayer.IsValidShield());
			shieldHpGaugeADD.sprite.gameObject.SetActive(targetPlayer.IsValidShield());
			if (shieldHpGaugeUI.nowPercent != num3)
			{
				shieldHpGaugeUI.SetPercent(num3, false);
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
		GameObject root = spActionGaugeInfo.GetRoot(targetPlayer.spAttackType);
		spActionGaugeInfo.SetActiveRoot(targetPlayer.spAttackType, flag2);
		if (flag2)
		{
			Renderer renderer = spActionGaugeInfo.GetRenderer(targetPlayer.spAttackType);
			if ((UnityEngine.Object)renderer != (UnityEngine.Object)null)
			{
				int num4 = targetPlayer.CheckGaugeLevel();
				Color color = (num4 != -1) ? ((!targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL)) ? spActionGaugeInfo.gaugeColorJump[num4] : spActionGaugeInfo.gaugeColorSoulPairSwords[num4]) : ((targetPlayer.spAttackType == SP_ATTACK_TYPE.BURST) ? ((!targetPlayer.IsSpActionGaugeHalfCharged() && !targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorBurst[0] : spActionGaugeInfo.gaugeColorBurst[1]) : ((targetPlayer.spAttackType != SP_ATTACK_TYPE.SOUL) ? ((!targetPlayer.IsSpActionGaugeHalfCharged() && !targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorNormal : spActionGaugeInfo.gaugeColorCharged) : ((!targetPlayer.IsSpActionGaugeHalfCharged() && !targetPlayer.isBoostMode) ? spActionGaugeInfo.gaugeColorSoul[0] : spActionGaugeInfo.gaugeColorSoul[1])));
				if (color != renderer.material.color)
				{
					renderer.material.color = color;
				}
			}
			float num5 = (!(targetPlayer.CurrentWeaponSpActionGaugeMax > 0f)) ? 0f : (targetPlayer.CurrentWeaponSpActionGauge / targetPlayer.CurrentWeaponSpActionGaugeMax);
			bool flag3 = false;
			UIHGauge gaugeUI = spActionGaugeInfo.GetGaugeUI(targetPlayer.spAttackType);
			if ((UnityEngine.Object)gaugeUI != (UnityEngine.Object)null)
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
			if ((UnityEngine.Object)root != (UnityEngine.Object)null && root.activeInHierarchy)
			{
				if (targetPlayer.IsSpActionGaugeHalfCharged() && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.HALF))
				{
					sprite.gameObject.SetActive(false);
					shieldHpGaugeEffect2.sprite.gameObject.SetActive(true);
					UITweenCtrl.Reset(root.transform, 1);
					UITweenCtrl.Play(root.transform, true, null, false, 1);
					spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.HALF);
					SoundManager.PlayOneShotUISE(40000358);
				}
				if (targetPlayer.IsSpActionGaugeFullCharged() && !targetPlayer.isBoostMode && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.FULL))
				{
					sprite.gameObject.SetActive(false);
					shieldHpGaugeEffect2.sprite.gameObject.SetActive(true);
					UITweenCtrl.Reset(root.transform, 2);
					UITweenCtrl.Play(root.transform, true, null, false, 2);
					spActionGaugeInfo.SetState(SpActionGaugeInfo.ANIM_STATE.FULL);
					SoundManager.PlayOneShotUISE(40000359);
				}
				if (targetPlayer.isBoostMode && !spActionGaugeInfo.IsPlayAnim(SpActionGaugeInfo.ANIM_STATE.BOOST))
				{
					sprite.gameObject.SetActive(true);
					shieldHpGaugeEffect2.sprite.gameObject.SetActive(false);
					UITweenCtrl.Reset(root.transform, 0);
					UITweenCtrl.Play(root.transform, true, null, false, 0);
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
		if (!((UnityEngine.Object)targetPlayer == (UnityEngine.Object)null) && preWeaponIndex != targetPlayer.weaponIndex)
		{
			preWeaponIndex = targetPlayer.weaponIndex;
			ResetSpActionGaugeState();
			UpdateBurstUIInfo();
		}
	}

	public void SetGaugeEffectColor(SP_ATTACK_TYPE type)
	{
		ShieldHpGaugeEffect shieldHpGaugeEffect = GetSpActionGaugeADD_BOOST(type);
		shieldHpGaugeEffect.sprite.color = shieldHpGaugeEffect.effectColor[(int)type];
	}

	public void ResetSpActionGaugeState()
	{
		ShieldHpGaugeEffect shieldHpGaugeEffect = GetSpActionGaugeADD_BOOST(targetPlayer.spAttackType);
		ShieldHpGaugeEffect shieldHpGaugeEffect2 = GetSpActionGaugeADD_HALF(targetPlayer.spAttackType);
		shieldHpGaugeEffect.sprite.gameObject.SetActive(false);
		shieldHpGaugeEffect2.sprite.gameObject.SetActive(false);
		GameObject root = spActionGaugeInfo.GetRoot(targetPlayer.spAttackType);
		UITweenCtrl.Reset(root.transform, 0);
		UITweenCtrl.Reset(root.transform, 1);
		UITweenCtrl.Reset(root.transform, 2);
		spActionGaugeInfo.state = 0;
		if (!((UnityEngine.Object)targetPlayer == (UnityEngine.Object)null))
		{
			if (targetPlayer.IsSpActionGaugeHalfCharged())
			{
				shieldHpGaugeEffect2.sprite.gameObject.SetActive(true);
				if (shieldHpGaugeEffect2.sprite.gameObject.activeInHierarchy)
				{
					UITweenCtrl.Play(root.transform, true, null, false, 1);
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
			if ((UnityEngine.Object)dropInfoR != (UnityEngine.Object)null)
			{
				dropInfoR.text = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropRare.ToString();
			}
			if ((UnityEngine.Object)dropInfoN != (UnityEngine.Object)null)
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
				boostItems[i].anim.Play(true, delegate
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
			boostRate.text = boost.GetBoostRateText();
			boostRate.color = boostAnimator.GetRateColor(boost.value);
			boostTime.text = ((boost.type != 210) ? boost.GetRemainTime() : string.Empty);
			break;
		}
	}

	public void AddItemNum(Vector3 world_hit_pos, int rarity, bool is_right)
	{
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0 && base.gameObject.activeInHierarchy)
		{
			StartCoroutine(_AddItemNum(world_hit_pos, rarity, is_right));
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
		Transform parent;
		Vector3 offset;
		if (rarity > 0)
		{
			parent = dropIconR;
			offset = dropInfoR.transform.localPosition - dropIconR.localPosition;
		}
		else
		{
			parent = dropIconN;
			offset = dropInfoN.transform.localPosition - dropIconN.localPosition;
		}
		Transform effect = EffectManager.GetUIEffect("ef_ui_downenergy_01", parent, -0.001f, 0, null);
		if (!((UnityEngine.Object)effect == (UnityEngine.Object)null))
		{
			Vector3 screen_pos = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos);
			Vector3 ui_pos = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screen_pos);
			ui_pos.z = 1f;
			effect.position = ui_pos;
			GameObject obj = effect.gameObject;
			TransformInterpolator interp = obj.AddComponent<TransformInterpolator>();
			if (!((UnityEngine.Object)interp == (UnityEngine.Object)null))
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
		if ((UnityEngine.Object)weaponChange != (UnityEngine.Object)null)
		{
			weaponChange.SetDisableButtons(disable);
		}
	}

	public void DoEnable()
	{
		base.gameObject.SetActive(true);
	}

	public void DoDisable()
	{
		base.gameObject.SetActive(false);
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
		Transform trans = soulEnergy.GetEffectTrans(soulEffectDirection);
		if (!object.ReferenceEquals(trans, null))
		{
			Vector3 screenPos = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(worldHitPos);
			Vector3 uiPos = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenPos);
			uiPos.z = 1f;
			trans.position = uiPos;
			TransformInterpolator interp = trans.gameObject.GetComponent<TransformInterpolator>();
			if (object.ReferenceEquals(interp, null))
			{
				interp = trans.gameObject.AddComponent<TransformInterpolator>();
			}
			interp.Translate(add_value: new Vector3(UnityEngine.Random.Range(0f - soulEffectAddRandomMax, soulEffectAddRandomMax), UnityEngine.Random.Range(0f - soulEffectAddRandomMax, soulEffectAddRandomMax), 0f), _time: soulEffectTime, target: Vector3.zero, ease_curve: soulEffectEaseCurve, add_curve: soulEffectAddCurve);
			yield return (object)new WaitForSeconds(soulEffectTime);
			soulEnergy.Absorbed();
		}
	}

	public void PlayChangeEvolveIcon(bool start)
	{
		if (!object.ReferenceEquals(evolveGauge, null) && !object.ReferenceEquals(evolveGauge.evolveIcon, null) && evolveGauge.evolveIcon.gameObject.activeSelf != start)
		{
			if (!object.ReferenceEquals(weaponChange, null))
			{
				if (start)
				{
					weaponChange.PlayEvolveIconAnim(delegate
					{
						EnableEvolveIcon(true);
					});
				}
				else
				{
					weaponChange.PlayEvolveIconAnim(delegate
					{
						EnableEvolveIcon(false);
					});
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
		if ((UnityEngine.Object)m_burstBulletCtrl == (UnityEngine.Object)null)
		{
			return false;
		}
		return m_burstBulletCtrl.FullBurstAction();
	}

	public bool DoShootAction()
	{
		if ((UnityEngine.Object)m_burstBulletCtrl == (UnityEngine.Object)null)
		{
			return false;
		}
		return m_burstBulletCtrl.ConsumeBulletAction();
	}

	public bool DoReloadAction()
	{
		if ((UnityEngine.Object)m_burstBulletCtrl == (UnityEngine.Object)null)
		{
			return false;
		}
		return m_burstBulletCtrl.ReloadAction();
	}

	private void CheckVisibleBulletUI()
	{
		if (!((UnityEngine.Object)m_burstBulletCtrl == (UnityEngine.Object)null))
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
		if (!((UnityEngine.Object)m_burstBulletCtrl == (UnityEngine.Object)null) && !((UnityEngine.Object)targetPlayer == (UnityEngine.Object)null) && targetPlayer.thsCtrl != null)
		{
			UIBurstBulletUIController.InitParam initParam = new UIBurstBulletUIController.InitParam();
			initParam.MaxBulletCount = targetPlayer.thsCtrl.CurrentMaxBulletCount;
			initParam.CurrentRestBulletCount = targetPlayer.thsCtrl.CurrentRestBulletCount;
			UIBurstBulletUIController.InitParam param = initParam;
			m_burstBulletCtrl.Initialize(param);
		}
	}
}
