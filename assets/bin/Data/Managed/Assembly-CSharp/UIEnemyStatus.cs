using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyStatus : MonoBehaviourSingleton<UIEnemyStatus>
{
	public enum GaugeType
	{
		HP,
		SHIELD
	}

	private const float kShadowSealingGaugeWidth = 150f;

	[SerializeField]
	protected UILabel enemyName;

	[SerializeField]
	protected UILabel level;

	[SerializeField]
	protected UIHGauge hpGaugeUI;

	[SerializeField]
	protected GameObject hpGaugeBase;

	[SerializeField]
	protected UIHGauge shieldGaugeUI;

	[SerializeField]
	protected UIHGauge aegisGaugeUI;

	[SerializeField]
	protected UIHGauge downGaugeUI;

	[SerializeField]
	protected float downEffectTime = 1f;

	[SerializeField]
	protected AnimationCurve downEffectEaseCurve = Curves.CreateEaseInCurve();

	[SerializeField]
	protected float downEffectAddRandomMax;

	[SerializeField]
	protected AnimationCurve downEffectAddCurve;

	[SerializeField]
	protected UIStatusIcon statusIcons;

	[SerializeField]
	protected UILabel weakRootName;

	[SerializeField]
	protected UILabel nonWeakElementName;

	[SerializeField]
	protected UISprite sprWeakElement;

	[SerializeField]
	protected UISprite sprElement;

	[SerializeField]
	protected float shieldChargeTime = 2f;

	[SerializeField]
	protected GameObject shakeTarget;

	[SerializeField]
	protected GameObject hpFocusFrame;

	[SerializeField]
	protected GameObject shadowSealingRoot;

	[SerializeField]
	protected GameObject[] shadowSealingPartitions;

	[SerializeField]
	protected UIHGauge shadowSealingGaugeUI;

	[SerializeField]
	protected UITweenCtrl hpMultiXTween;

	[SerializeField]
	protected GameObject tutorialObj;

	[SerializeField]
	protected UILabel multiX;

	private Coroutine shakeCoroutine;

	private Vector3 shakeTargetDefaultPos = Vector3.get_zero();

	protected float downPercent;

	protected int playEffectCount;

	private List<GameObject> playEffects = new List<GameObject>();

	protected float shadowSealingPercent;

	protected int playShadowSealingEffectCount;

	private List<GameObject> playShadowSealingEffects = new List<GameObject>();

	protected UISprite downGaugeUISpr;

	protected bool isChangeDownGaugeSpr;

	private GaugeType currentGaugeType;

	private bool isLoadEffectComplete;

	private bool isPlayShieldGaugeAnimation;

	private readonly Vector3 kShadowSealingGaugeEffectScale = new Vector3(0.6f, 1f, 1f);

	public Enemy targetEnemy
	{
		get;
		protected set;
	}

	protected override void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		this.get_gameObject().SetActive(false);
		SetGaugeType(GaugeType.HP);
		SetActiveTutorialObj(false);
	}

	private void Start()
	{
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		if (!(targetEnemy == null))
		{
			if (Object.op_Implicit(hpGaugeUI))
			{
				float percent = (targetEnemy.hpMax <= 0) ? 0f : ((float)targetEnemy.hp / (float)targetEnemy.hpMax);
				hpGaugeUI.SetPercent(percent, false);
			}
			if (Object.op_Implicit(downGaugeUI))
			{
				downGaugeUISpr = downGaugeUI.GetComponent<UISprite>();
				float percent2 = CalcDownPercent();
				downGaugeUI.SetPercent(percent2, false);
				downPercent = percent2;
			}
			if (Object.op_Implicit(shadowSealingGaugeUI))
			{
				float percent3 = CalcShadowSealingPercent();
				shadowSealingGaugeUI.SetPercent(percent3, false);
				shadowSealingPercent = percent3;
			}
			if (Object.op_Implicit(shieldGaugeUI))
			{
				isLoadEffectComplete = false;
				this.StartCoroutine(DoLoadEffect());
				shieldGaugeUI.SetPercent(0f, false);
			}
			if (shakeTarget != null)
			{
				shakeTargetDefaultPos = shakeTarget.get_transform().get_localPosition();
			}
		}
	}

	private IEnumerator DoLoadEffect()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_shieldgauge_01");
		while (load_queue.IsLoading())
		{
			yield return (object)null;
		}
		isLoadEffectComplete = true;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		int i = 0;
		for (int count = playEffects.Count; i < count; i++)
		{
			EffectManager.ReleaseEffect(playEffects[i], true, false);
		}
		playEffects.Clear();
		playEffectCount = 0;
		int j = 0;
		for (int count2 = playShadowSealingEffects.Count; j < count2; j++)
		{
			EffectManager.ReleaseEffect(playShadowSealingEffects[j], true, false);
		}
		playShadowSealingEffects.Clear();
		playShadowSealingEffectCount = 0;
	}

	public void SetTarget(Enemy enemy)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		targetEnemy = enemy;
		if (targetEnemy == null)
		{
			this.get_gameObject().SetActive(false);
		}
		else
		{
			statusIcons.target = enemy;
			if (Object.op_Implicit(enemyName))
			{
				string charaName = enemy.charaName;
				enemyName.text = charaName;
				int num = enemy.enemyLevel;
				if (level == null && num > 0)
				{
					enemyName.text = $"Lv{num.ToString()} {charaName}";
				}
			}
			if (Object.op_Implicit(level))
			{
				level.text = enemy.enemyLevel.ToString();
			}
			if (Object.op_Implicit(hpGaugeUI))
			{
				float num2 = (targetEnemy.hpMax <= 0) ? 0f : ((float)targetEnemy.hp / (float)targetEnemy.hpMax);
				if (hpGaugeUI.nowPercent != num2)
				{
					hpGaugeUI.SetPercent(num2, false);
				}
			}
			if (Object.op_Implicit(downGaugeUI))
			{
				float num3 = CalcDownPercent();
				if (downGaugeUI.nowPercent != num3)
				{
					downGaugeUI.SetPercent(num3, false);
					downPercent = num3;
				}
			}
			if (Object.op_Implicit(shadowSealingGaugeUI))
			{
				float num4 = CalcShadowSealingPercent();
				if (shadowSealingGaugeUI.nowPercent != num4)
				{
					shadowSealingGaugeUI.SetPercent(num4, false);
					shadowSealingPercent = num4;
				}
			}
			UpdateElementIcon();
			UpDateStatusIcon();
			if (!MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
			{
				this.get_gameObject().SetActive(true);
			}
		}
	}

	private void LateUpdate()
	{
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		if (!(targetEnemy == null))
		{
			if (Object.op_Implicit(hpGaugeUI))
			{
				float num = (targetEnemy.hpMax <= 0) ? 0f : ((float)targetEnemy.hpShow / (float)targetEnemy.hpMax);
				if (hpGaugeUI.nowPercent != num)
				{
					hpGaugeUI.SetPercent(num, true);
				}
			}
			if (Object.op_Implicit(downGaugeUI) && (playEffectCount == 0 || targetEnemy.IsActDown()))
			{
				float num2 = CalcDownPercent();
				if (downGaugeUI.nowPercent != num2)
				{
					downGaugeUI.SetPercent(num2, false);
					downPercent = num2;
				}
			}
			if (Object.op_Implicit(shadowSealingGaugeUI) && (playShadowSealingEffectCount == 0 || targetEnemy.IsDebuffShadowSealing()))
			{
				float num3 = CalcShadowSealingPercent();
				if (shadowSealingGaugeUI.nowPercent != num3)
				{
					shadowSealingGaugeUI.SetPercent(num3, false);
					shadowSealingPercent = num3;
				}
			}
			if (Object.op_Implicit(shieldGaugeUI) && targetEnemy.IsValidShield())
			{
				if (currentGaugeType != GaugeType.SHIELD)
				{
					SetGaugeType(GaugeType.SHIELD);
				}
				else
				{
					float num4 = Mathf.Max(0f, (float)(int)targetEnemy.ShieldHp / (float)(int)targetEnemy.ShieldHpMax);
					if (shieldGaugeUI.nowPercent < num4)
					{
						if (!isPlayShieldGaugeAnimation)
						{
							isPlayShieldGaugeAnimation = true;
							this.StartCoroutine(PlayShieldGaugeAnimation());
						}
					}
					else if (shieldGaugeUI.nowPercent > num4 && !isPlayShieldGaugeAnimation)
					{
						shieldGaugeUI.SetPercent(num4, true);
					}
				}
			}
			else if (currentGaugeType != 0)
			{
				SetGaugeType(GaugeType.HP);
			}
		}
	}

	public void PlayShakeHpGauge(float power, float shakeTime, float cycleTime, bool reset = true)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		if (cycleTime == 0f)
		{
			Log.Error(LOG.INGAME, "cycleTime is 0 at PlayShakeHpGauge in UIEnemyStatus.cs");
		}
		else if (shakeTarget == null)
		{
			Log.Error(LOG.INGAME, "shakeTarget is null in UIEnemyStatus:InGameMain/StaticPanel/EnemyStatus");
		}
		else
		{
			if (reset && shakeCoroutine != null)
			{
				this.StopCoroutine(shakeCoroutine);
				shakeCoroutine = null;
			}
			if (shakeCoroutine == null)
			{
				shakeCoroutine = this.StartCoroutine(_PlayShakeHpGauge(power, shakeTime, cycleTime));
			}
		}
	}

	private IEnumerator _PlayShakeHpGauge(float power, float shakeTime, float cycleTime)
	{
		float timer = 0f;
		while (timer < shakeTime)
		{
			timer += Time.get_deltaTime();
			float y = power * Mathf.Sin(6.28f * timer / cycleTime);
			shakeTarget.get_transform().set_localPosition(shakeTargetDefaultPos + y * Vector3.get_up());
			power -= power * Time.get_deltaTime() / shakeTime;
			yield return (object)null;
		}
		shakeTarget.get_transform().set_localPosition(shakeTargetDefaultPos);
	}

	private IEnumerator PlayShieldGaugeAnimation()
	{
		Renderer r_shieldGauge = shieldGaugeUI.GetComponent<Renderer>();
		Renderer r_hpGauge = hpGaugeUI.GetComponent<Renderer>();
		UISprite r_base = hpGaugeBase.GetComponent<UISprite>();
		r_hpGauge.get_material().set_renderQueue(r_base.material.get_renderQueue() + 1);
		r_shieldGauge.get_material().set_renderQueue(r_hpGauge.get_material().get_renderQueue() + 1);
		Transform effectTrans = null;
		if (isLoadEffectComplete)
		{
			effectTrans = EffectManager.GetUIEffect("ef_ui_shieldgauge_01", (UIWidget)r_base, -0.001f, r_shieldGauge.get_material().get_renderQueue() + 10);
			if (effectTrans != null)
			{
				effectTrans.set_rotation(Quaternion.get_identity());
				effectTrans.set_localScale(Vector3.get_one());
			}
		}
		float width = (float)(r_base.width - r_base.GetAtlasSprite().borderLeft - r_base.GetAtlasSprite().borderRight);
		Vector3 offset = Vector3.get_right() * (float)r_base.GetAtlasSprite().borderLeft + Vector3.get_down() * (float)r_base.height / 2f;
		float shieldPercent = Mathf.Max(0f, (float)(int)targetEnemy.ShieldHp / (float)(int)targetEnemy.ShieldHpMax);
		while (shieldGaugeUI.nowPercent < shieldPercent)
		{
			shieldGaugeUI.SetPercent(shieldGaugeUI.nowPercent + Time.get_deltaTime() / shieldChargeTime, true);
			if (effectTrans != null)
			{
				effectTrans.set_localPosition(offset + Vector3.get_right() * width * shieldGaugeUI.nowPercent);
			}
			shieldPercent = Mathf.Max(0f, (float)(int)targetEnemy.ShieldHp / (float)(int)targetEnemy.ShieldHpMax);
			yield return (object)null;
		}
		yield return (object)null;
		if (effectTrans != null)
		{
			EffectManager.ReleaseEffect(effectTrans.get_gameObject(), true, false);
		}
		isPlayShieldGaugeAnimation = false;
	}

	public void DirectionDownGauge(AttackedHitStatusFix status)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_gameObject().get_activeInHierarchy())
		{
			this.StartCoroutine(_DirectionDownGauge(status));
		}
	}

	private IEnumerator _DirectionDownGauge(AttackedHitStatusFix status)
	{
		Vector3 world_hit_pos = status.hitPos;
		float down_percent = (targetEnemy.downMax <= 0) ? 0f : (targetEnemy.downTotal / (float)targetEnemy.downMax);
		float workDownTotal = 0f;
		int effect_num = (int)((down_percent - downPercent) * 10f) + 1;
		downPercent = down_percent;
		playEffectCount++;
		yield return (object)this.StartCoroutine(_DirectionDownParticle(effect_num, world_hit_pos));
		EffectManager.GetUIEffect("ef_ui_downgauge_01", downGaugeUI.GetGaugeTransform(), -0.001f, 0, null);
		if (!targetEnemy.IsActDown())
		{
			downGaugeUI.SetPercent(down_percent, false);
		}
		else
		{
			workDownTotal = targetEnemy.CalcWorkDownTotal(status.downAddBase, status.downAddWeak, status.regionID);
			targetEnemy.IncreaseDownTimeByAttack(workDownTotal);
		}
		yield return (object)new WaitForSeconds(1f);
		if (down_percent >= 1f && !targetEnemy.IsActDown())
		{
			yield return (object)this.StartCoroutine(_DirectionDownCompleteEffect(false));
		}
		else if (workDownTotal > 0f && targetEnemy.IsActDown())
		{
			yield return (object)this.StartCoroutine(_DirectionDownCompleteEffect(true));
		}
		playEffectCount--;
	}

	private IEnumerator _DirectionDownParticle(int effect_num, Vector3 world_hit_pos)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0)
		{
			float speed = (float)((!targetEnemy.IsActDown()) ? 1 : 2);
			Vector3 screen_pos = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos);
			Vector3 ui_pos = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screen_pos);
			List<GameObject> effects = new List<GameObject>();
			for (int i = 0; i < effect_num; i++)
			{
				Transform trans = EffectManager.GetUIEffect("ef_ui_downenergy_01", downGaugeUI.GetGaugeTransform(), -0.001f, 0, null);
				if (!(trans == null))
				{
					ui_pos.z = 1f;
					trans.set_position(ui_pos);
					GameObject obj2 = trans.get_gameObject();
					TransformInterpolator interp = obj2.AddComponent<TransformInterpolator>();
					if (!(interp == null))
					{
						interp.Translate(add_value: new Vector3(Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), 0f), _time: downEffectTime / speed, target: Vector3.get_zero(), ease_curve: downEffectEaseCurve, add_curve: downEffectAddCurve);
						effects.Add(obj2);
						playEffects.Add(obj2);
					}
				}
			}
			yield return (object)new WaitForSeconds(downEffectTime / speed);
			effects.ForEach(delegate(GameObject obj)
			{
				((_003C_DirectionDownParticle_003Ec__Iterator211)/*Error near IL_0233: stateMachine*/)._003C_003Ef__this.playEffects.Remove(obj);
				EffectManager.ReleaseEffect(obj, true, false);
			});
		}
	}

	private IEnumerator _DirectionDownCompleteEffect(bool isInActDown)
	{
		Transform trans = EffectManager.GetUIEffect("ef_ui_downgauge_02", downGaugeUI.GetGaugeTransform(), -0.001f, 0, null);
		if (!(trans == null))
		{
			playEffects.Add(trans.get_gameObject());
			bool isDownMotion = targetEnemy.actionID == (Character.ACTION_ID)13;
			while (isInActDown != isDownMotion)
			{
				yield return (object)null;
			}
			playEffects.Remove(trans.get_gameObject());
			EffectManager.ReleaseEffect(trans.get_gameObject(), true, false);
		}
	}

	public void UpDateStatusIcon()
	{
		statusIcons.UpDateStatusIcon();
	}

	private void UpdateElementIcon()
	{
		if (targetEnemy == null)
		{
			Log.Error("targetEnemy is null !!");
		}
		else
		{
			EnemyTable.EnemyData enemyTableData = targetEnemy.enemyTableData;
			if (enemyTableData == null)
			{
				Log.Error("enemyData is null !!");
			}
			else
			{
				SetElementIcon((targetEnemy.changeElementIcon == ELEMENT_TYPE.MAX) ? enemyTableData.element : targetEnemy.changeElementIcon);
				SetWeakElementIcon((targetEnemy.changeWeakElementIcon == ELEMENT_TYPE.MAX) ? enemyTableData.weakElement : targetEnemy.changeWeakElementIcon);
			}
		}
	}

	public void SetElementIcon(ELEMENT_TYPE element)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (sprElement != null)
		{
			sprElement.get_gameObject().SetActive(false);
			string iconElementSpriteName = ItemIcon.GetIconElementSpriteName(element);
			if (!string.IsNullOrEmpty(iconElementSpriteName))
			{
				sprElement.spriteName = iconElementSpriteName;
				sprElement.get_gameObject().SetActive(true);
			}
		}
	}

	public void SetWeakElementIcon(ELEMENT_TYPE weakElement)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (weakRootName != null)
		{
			weakRootName.get_gameObject().SetActive(true);
		}
		if (sprWeakElement != null)
		{
			sprWeakElement.get_gameObject().SetActive(false);
		}
		if (nonWeakElementName != null)
		{
			nonWeakElementName.get_gameObject().SetActive(false);
		}
		if (weakElement == ELEMENT_TYPE.MAX)
		{
			if (nonWeakElementName != null)
			{
				nonWeakElementName.get_gameObject().SetActive(true);
			}
		}
		else if (sprWeakElement != null)
		{
			string iconElementSpriteName = ItemIcon.GetIconElementSpriteName(weakElement);
			if (!string.IsNullOrEmpty(iconElementSpriteName))
			{
				sprWeakElement.spriteName = iconElementSpriteName;
				sprWeakElement.get_gameObject().SetActive(true);
			}
		}
	}

	private void SetGaugeType(GaugeType type)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		currentGaugeType = type;
		switch (type)
		{
		case GaugeType.HP:
			shieldGaugeUI.get_gameObject().SetActive(false);
			hpFocusFrame.SetActive(false);
			break;
		case GaugeType.SHIELD:
			shieldGaugeUI.get_gameObject().SetActive(true);
			hpFocusFrame.SetActive(true);
			break;
		}
	}

	private float CalcDownPercent()
	{
		bool flag = false;
		float result = (targetEnemy.downMax <= 0) ? 0f : (targetEnemy.downTotal / (float)targetEnemy.downMax);
		if (targetEnemy.IsActDown())
		{
			if (targetEnemy.stackBuffCtrl.GetStackCount(StackBuffController.STACK_TYPE.SNATCH) > 0)
			{
				flag = true;
			}
			result = targetEnemy.GetDownTimeRate();
		}
		if (isChangeDownGaugeSpr != flag && !object.ReferenceEquals(downGaugeUISpr, null))
		{
			downGaugeUISpr.spriteName = ((!flag) ? "enemy_down_over" : "enemy_kagenui_over");
			isChangeDownGaugeSpr = flag;
		}
		return result;
	}

	public void ShowShadowSealing(bool isVisible)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		if (object.ReferenceEquals(targetEnemy, null))
		{
			isVisible = false;
		}
		else
		{
			num = targetEnemy.GetShadowSealingNum();
			if (num == 0)
			{
				isVisible = false;
			}
		}
		shadowSealingRoot.SetActive(false);
		if (isVisible)
		{
			int shadowSealingStuckNum = targetEnemy.GetShadowSealingStuckNum();
			float num2 = 150f / (float)num;
			int i = 0;
			for (int num3 = shadowSealingPartitions.Length; i < num3; i++)
			{
				if (i < num - 1)
				{
					shadowSealingPartitions[i].get_transform().set_localPosition(new Vector3(-61.5f + num2 * (float)(i + 1), 2f, 0f));
					shadowSealingPartitions[i].SetActive(true);
				}
				else
				{
					shadowSealingPartitions[i].SetActive(false);
				}
			}
			float percent = (float)shadowSealingStuckNum / (float)num;
			shadowSealingGaugeUI.SetPercent(percent, false);
			shadowSealingRoot.SetActive(true);
		}
	}

	private float CalcShadowSealingPercent()
	{
		if (targetEnemy.IsDebuffShadowSealing())
		{
			return targetEnemy.GetDebuffShadowSealingTimeRate();
		}
		int shadowSealingNum = targetEnemy.GetShadowSealingNum();
		int shadowSealingStuckNum = targetEnemy.GetShadowSealingStuckNum();
		return (shadowSealingNum <= 0 && shadowSealingStuckNum <= 0) ? 0f : ((float)shadowSealingStuckNum / (float)shadowSealingNum);
	}

	public void DirectionShadowSealingGauge(Vector3 world_hit_pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_gameObject().get_activeInHierarchy() && shadowSealingRoot.get_activeInHierarchy())
		{
			this.StartCoroutine(_DirectionShadowSealingGauge(world_hit_pos));
		}
	}

	private IEnumerator _DirectionShadowSealingGauge(Vector3 world_hit_pos)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		float per = CalcShadowSealingPercent();
		int effect_num = (int)((per - shadowSealingPercent) * 10f) + 1;
		shadowSealingPercent = per;
		playShadowSealingEffectCount++;
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0)
		{
			Vector3 screen_pos = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos);
			Vector3 ui_pos = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screen_pos);
			List<GameObject> effects = new List<GameObject>();
			for (int i = 0; i < effect_num; i++)
			{
				Transform trans2 = EffectManager.GetUIEffect("ef_ui_downenergy_01", shadowSealingGaugeUI.GetGaugeTransform(), -0.001f, 0, null);
				if (!(trans2 == null))
				{
					ui_pos.z = 1f;
					trans2.set_position(ui_pos);
					GameObject obj2 = trans2.get_gameObject();
					TransformInterpolator interp = obj2.AddComponent<TransformInterpolator>();
					if (!(interp == null))
					{
						interp.Translate(add_value: new Vector3(Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), 0f), _time: downEffectTime, target: Vector3.get_zero(), ease_curve: downEffectEaseCurve, add_curve: downEffectAddCurve);
						effects.Add(obj2);
						playShadowSealingEffects.Add(obj2);
					}
				}
			}
			yield return (object)new WaitForSeconds(downEffectTime);
			effects.ForEach(delegate(GameObject obj)
			{
				((_003C_DirectionShadowSealingGauge_003Ec__Iterator213)/*Error near IL_025b: stateMachine*/)._003C_003Ef__this.playShadowSealingEffects.Remove(obj);
				EffectManager.ReleaseEffect(obj, true, false);
			});
		}
		Transform tmp = EffectManager.GetUIEffect("ef_ui_downgauge_01", shadowSealingGaugeUI.GetGaugeTransform(), -0.001f, 0, null);
		if (tmp != null)
		{
			tmp.set_localScale(kShadowSealingGaugeEffectScale);
		}
		shadowSealingGaugeUI.SetPercent(per, false);
		yield return (object)new WaitForSeconds(1f);
		if (per >= 1f && !targetEnemy.IsDebuffShadowSealing())
		{
			Transform trans = EffectManager.GetUIEffect("ef_ui_downgauge_02", shadowSealingGaugeUI.GetGaugeTransform(), -0.001f, 0, null);
			if (trans != null)
			{
				playShadowSealingEffects.Add(trans.get_gameObject());
				while (targetEnemy.actionID == (Character.ACTION_ID)18)
				{
					yield return (object)null;
				}
				playShadowSealingEffects.Remove(trans.get_gameObject());
				EffectManager.ReleaseEffect(trans.get_gameObject(), true, false);
			}
		}
		playShadowSealingEffectCount--;
	}

	public void SetAegisBarPercent(float percent)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		if (!object.ReferenceEquals(hpFocusFrame, null))
		{
			if (percent <= 0f)
			{
				aegisGaugeUI.get_gameObject().SetActive(false);
				hpFocusFrame.SetActive(false);
			}
			else
			{
				aegisGaugeUI.SetPercent(percent, false);
				if (!aegisGaugeUI.get_gameObject().get_activeSelf())
				{
					aegisGaugeUI.get_gameObject().SetActive(true);
					hpFocusFrame.SetActive(true);
				}
			}
		}
	}

	public void PlayShakeHpMultiX()
	{
		if (hpMultiXTween != null)
		{
			Debug.Log((object)"Shake");
			hpMultiXTween.Reset();
			hpMultiXTween.Play(true, null);
		}
	}

	public void SetHpMultiX(int x)
	{
		if (tutorialObj != null)
		{
			multiX.text = $"x.{x}";
		}
	}

	public void SetActiveTutorialObj(bool isActive)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		if (tutorialObj != null)
		{
			tutorialObj.SetActive(isActive);
			weakRootName.get_gameObject().SetActive(!isActive);
			enemyName.get_gameObject().SetActive(!isActive);
			sprElement.get_gameObject().SetActive(!isActive);
		}
	}
}
