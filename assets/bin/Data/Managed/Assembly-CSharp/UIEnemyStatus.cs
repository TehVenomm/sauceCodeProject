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
	protected GameObject concussionRoot;

	[SerializeField]
	protected UIHGauge concussionGaugeUI;

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

	protected float concussionPercent;

	protected int playConcussionEffectCount;

	private List<GameObject> playConcussionEffects = new List<GameObject>();

	protected UISprite downGaugeUISpr;

	protected bool isChangeDownGaugeSpr;

	private GaugeType currentGaugeType;

	private bool isLoadEffectComplete;

	private bool isPlayShieldGaugeAnimation;

	private const float kShadowSealingGaugeWidth = 150f;

	private readonly Vector3 kShadowSealingGaugeEffectScale = new Vector3(0.6f, 1f, 1f);

	private readonly Vector3 kConcussionGaugeEffectScale = new Vector3(0.66f, 1f, 1f);

	public Enemy targetEnemy
	{
		get;
		protected set;
	}

	protected override void Awake()
	{
		base.Awake();
		this.get_gameObject().SetActive(false);
		SetGaugeType(GaugeType.HP);
		SetActiveTutorialObj(isActive: false);
	}

	private void Start()
	{
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		if (!(targetEnemy == null))
		{
			if (Object.op_Implicit(hpGaugeUI))
			{
				float percent = (targetEnemy.hpMax <= 0) ? 0f : ((float)targetEnemy.hp / (float)targetEnemy.hpMax);
				hpGaugeUI.SetPercent(percent, anim: false);
			}
			if (Object.op_Implicit(downGaugeUI))
			{
				downGaugeUISpr = downGaugeUI.GetComponent<UISprite>();
				float percent2 = CalcDownPercent();
				downGaugeUI.SetPercent(percent2, anim: false);
				downPercent = percent2;
			}
			if (Object.op_Implicit(shadowSealingGaugeUI))
			{
				float percent3 = CalcShadowSealingPercent();
				shadowSealingGaugeUI.SetPercent(percent3, anim: false);
				shadowSealingPercent = percent3;
			}
			if (Object.op_Implicit(concussionGaugeUI))
			{
				float percent4 = CalcConcussionPercent();
				concussionGaugeUI.SetPercent(percent4, anim: false);
				concussionPercent = percent4;
			}
			if (Object.op_Implicit(shieldGaugeUI))
			{
				isLoadEffectComplete = false;
				this.StartCoroutine(DoLoadEffect());
				shieldGaugeUI.SetPercent(0f, anim: false);
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
			yield return null;
		}
		isLoadEffectComplete = true;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		int i = 0;
		for (int count = playEffects.Count; i < count; i++)
		{
			EffectManager.ReleaseEffect(playEffects[i]);
		}
		playEffects.Clear();
		playEffectCount = 0;
		int j = 0;
		for (int count2 = playShadowSealingEffects.Count; j < count2; j++)
		{
			EffectManager.ReleaseEffect(playShadowSealingEffects[j]);
		}
		playShadowSealingEffects.Clear();
		playShadowSealingEffectCount = 0;
	}

	public void SetTarget(Enemy enemy)
	{
		targetEnemy = enemy;
		if (targetEnemy == null)
		{
			this.get_gameObject().SetActive(false);
			return;
		}
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
				hpGaugeUI.SetPercent(num2, anim: false);
			}
		}
		if (Object.op_Implicit(downGaugeUI))
		{
			float num3 = CalcDownPercent();
			if (downGaugeUI.nowPercent != num3)
			{
				downGaugeUI.SetPercent(num3, anim: false);
				downPercent = num3;
			}
		}
		if (Object.op_Implicit(shadowSealingGaugeUI))
		{
			float num4 = CalcShadowSealingPercent();
			if (shadowSealingGaugeUI.nowPercent != num4)
			{
				shadowSealingGaugeUI.SetPercent(num4, anim: false);
				shadowSealingPercent = num4;
			}
		}
		if (Object.op_Implicit(concussionGaugeUI))
		{
			float num5 = CalcConcussionPercent();
			if (concussionGaugeUI.nowPercent != num5)
			{
				concussionGaugeUI.SetPercent(num5, anim: false);
				concussionPercent = num5;
			}
		}
		UpdateElementIcon();
		UpDateStatusIcon();
		if (!MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
		{
			this.get_gameObject().SetActive(true);
		}
	}

	private void LateUpdate()
	{
		if (targetEnemy == null)
		{
			return;
		}
		if (Object.op_Implicit(hpGaugeUI))
		{
			float num = (targetEnemy.hpMax <= 0) ? 0f : ((float)targetEnemy.hpShow / (float)targetEnemy.hpMax);
			if (hpGaugeUI.nowPercent != num)
			{
				hpGaugeUI.SetPercent(num);
			}
		}
		if (Object.op_Implicit(downGaugeUI) && (playEffectCount == 0 || targetEnemy.IsActDown()))
		{
			float num2 = CalcDownPercent();
			if (downGaugeUI.nowPercent != num2)
			{
				downGaugeUI.SetPercent(num2, anim: false);
				downPercent = num2;
			}
		}
		if (Object.op_Implicit(shadowSealingGaugeUI) && (playShadowSealingEffectCount == 0 || targetEnemy.IsDebuffShadowSealing()))
		{
			float num3 = CalcShadowSealingPercent();
			if (shadowSealingGaugeUI.nowPercent != num3)
			{
				shadowSealingGaugeUI.SetPercent(num3, anim: false);
				shadowSealingPercent = num3;
			}
		}
		if (Object.op_Implicit(concussionGaugeUI) && (playConcussionEffectCount == 0 || targetEnemy.IsConcussion()))
		{
			float num4 = CalcConcussionPercent();
			if (concussionGaugeUI.nowPercent != num4)
			{
				concussionGaugeUI.SetPercent(num4, anim: false);
				concussionPercent = num4;
			}
		}
		if (Object.op_Implicit(shieldGaugeUI) && targetEnemy.IsValidShield())
		{
			if (currentGaugeType != GaugeType.SHIELD)
			{
				SetGaugeType(GaugeType.SHIELD);
				return;
			}
			float num5 = Mathf.Max(0f, (float)(int)targetEnemy.ShieldHp / (float)(int)targetEnemy.ShieldHpMax);
			if (shieldGaugeUI.nowPercent < num5)
			{
				if (!isPlayShieldGaugeAnimation)
				{
					isPlayShieldGaugeAnimation = true;
					this.StartCoroutine(PlayShieldGaugeAnimation());
				}
			}
			else if (shieldGaugeUI.nowPercent > num5 && !isPlayShieldGaugeAnimation)
			{
				shieldGaugeUI.SetPercent(num5);
			}
		}
		else if (currentGaugeType != 0)
		{
			SetGaugeType(GaugeType.HP);
		}
	}

	public void PlayShakeHpGauge(float power, float shakeTime, float cycleTime, bool reset = true)
	{
		if (cycleTime == 0f)
		{
			Log.Error(LOG.INGAME, "cycleTime is 0 at PlayShakeHpGauge in UIEnemyStatus.cs");
			return;
		}
		if (shakeTarget == null)
		{
			Log.Error(LOG.INGAME, "shakeTarget is null in UIEnemyStatus:InGameMain/StaticPanel/EnemyStatus");
			return;
		}
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

	private IEnumerator _PlayShakeHpGauge(float power, float shakeTime, float cycleTime)
	{
		float timer = 0f;
		while (timer < shakeTime)
		{
			timer += Time.get_deltaTime();
			float y = power * Mathf.Sin(6.28f * timer / cycleTime);
			shakeTarget.get_transform().set_localPosition(shakeTargetDefaultPos + y * Vector3.get_up());
			power -= power * Time.get_deltaTime() / shakeTime;
			yield return null;
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
		float width = r_base.width - r_base.GetAtlasSprite().borderLeft - r_base.GetAtlasSprite().borderRight;
		Vector3 offset = Vector3.get_right() * (float)r_base.GetAtlasSprite().borderLeft + Vector3.get_down() * (float)r_base.height / 2f;
		float shieldPercent = Mathf.Max(0f, (float)(int)targetEnemy.ShieldHp / (float)(int)targetEnemy.ShieldHpMax);
		while (shieldGaugeUI.nowPercent < shieldPercent)
		{
			shieldGaugeUI.SetPercent(shieldGaugeUI.nowPercent + Time.get_deltaTime() / shieldChargeTime);
			if (effectTrans != null)
			{
				effectTrans.set_localPosition(offset + Vector3.get_right() * width * shieldGaugeUI.nowPercent);
			}
			shieldPercent = Mathf.Max(0f, (float)(int)targetEnemy.ShieldHp / (float)(int)targetEnemy.ShieldHpMax);
			yield return null;
		}
		yield return null;
		if (effectTrans != null)
		{
			EffectManager.ReleaseEffect(effectTrans.get_gameObject());
		}
		isPlayShieldGaugeAnimation = false;
	}

	public void DirectionDownGauge(AttackedHitStatusFix status)
	{
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
		yield return this.StartCoroutine(_DirectionDownParticle(effect_num, world_hit_pos));
		EffectManager.GetUIEffect("ef_ui_downgauge_01", downGaugeUI.GetGaugeTransform());
		if (!targetEnemy.IsActDown())
		{
			downGaugeUI.SetPercent(down_percent, anim: false);
		}
		else
		{
			workDownTotal = targetEnemy.CalcWorkDownTotal(status.downAddBase, status.downAddWeak, status.regionID);
			targetEnemy.IncreaseDownTimeByAttack(workDownTotal);
		}
		yield return (object)new WaitForSeconds(1f);
		if (down_percent >= 1f && !targetEnemy.IsActDown())
		{
			yield return this.StartCoroutine(_DirectionDownCompleteEffect(isInActDown: false));
		}
		else if (workDownTotal > 0f && targetEnemy.IsActDown())
		{
			yield return this.StartCoroutine(_DirectionDownCompleteEffect(isInActDown: true));
		}
		playEffectCount--;
	}

	private IEnumerator _DirectionDownParticle(int effect_num, Vector3 world_hit_pos)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
		{
			yield break;
		}
		float speed = (!targetEnemy.IsActDown()) ? 1 : 2;
		Vector3 screen_pos = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos);
		Vector3 ui_pos = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screen_pos);
		List<GameObject> effects = new List<GameObject>();
		for (int i = 0; i < effect_num; i++)
		{
			Transform uIEffect = EffectManager.GetUIEffect("ef_ui_downenergy_01", downGaugeUI.GetGaugeTransform());
			if (!(uIEffect == null))
			{
				ui_pos.z = 1f;
				uIEffect.set_position(ui_pos);
				GameObject gameObject = uIEffect.get_gameObject();
				TransformInterpolator transformInterpolator = gameObject.AddComponent<TransformInterpolator>();
				if (!(transformInterpolator == null))
				{
					Vector3 add_value = default(Vector3);
					add_value._002Ector(Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), 0f);
					transformInterpolator.Translate(downEffectTime / speed, Vector3.get_zero(), downEffectEaseCurve, add_value, downEffectAddCurve);
					effects.Add(gameObject);
					playEffects.Add(gameObject);
				}
			}
		}
		yield return (object)new WaitForSeconds(downEffectTime / speed);
		effects.ForEach(delegate(GameObject obj)
		{
			playEffects.Remove(obj);
			EffectManager.ReleaseEffect(obj);
		});
	}

	private IEnumerator _DirectionDownCompleteEffect(bool isInActDown)
	{
		Transform trans = EffectManager.GetUIEffect("ef_ui_downgauge_02", downGaugeUI.GetGaugeTransform());
		if (!(trans == null))
		{
			playEffects.Add(trans.get_gameObject());
			bool isDownMotion = targetEnemy.IsActDown();
			while (isInActDown != isDownMotion)
			{
				yield return null;
			}
			playEffects.Remove(trans.get_gameObject());
			EffectManager.ReleaseEffect(trans.get_gameObject());
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
			return;
		}
		EnemyTable.EnemyData enemyTableData = targetEnemy.enemyTableData;
		if (enemyTableData == null)
		{
			Log.Error("enemyData is null !!");
			return;
		}
		SetElementIcon((targetEnemy.changeElementIcon == ELEMENT_TYPE.MAX) ? targetEnemy.GetElementTypeByRegion() : targetEnemy.changeElementIcon);
		SetWeakElementIcon((targetEnemy.changeWeakElementIcon == ELEMENT_TYPE.MAX) ? targetEnemy.GetAntiElementTypeByRegion() : targetEnemy.changeWeakElementIcon);
	}

	public void SetElementIcon(ELEMENT_TYPE element)
	{
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
		if (!isVisible)
		{
			return;
		}
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
		shadowSealingGaugeUI.SetPercent(percent, anim: false);
		shadowSealingRoot.SetActive(true);
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
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_gameObject().get_activeInHierarchy() && shadowSealingRoot.get_activeInHierarchy())
		{
			this.StartCoroutine(_DirectionShadowSealingGauge(world_hit_pos));
		}
	}

	private IEnumerator _DirectionShadowSealingGauge(Vector3 world_hit_pos)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
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
				Transform uIEffect = EffectManager.GetUIEffect("ef_ui_downenergy_01", shadowSealingGaugeUI.GetGaugeTransform());
				if (!(uIEffect == null))
				{
					ui_pos.z = 1f;
					uIEffect.set_position(ui_pos);
					GameObject gameObject = uIEffect.get_gameObject();
					TransformInterpolator transformInterpolator = gameObject.AddComponent<TransformInterpolator>();
					if (!(transformInterpolator == null))
					{
						Vector3 add_value = default(Vector3);
						add_value._002Ector(Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), 0f);
						transformInterpolator.Translate(downEffectTime, Vector3.get_zero(), downEffectEaseCurve, add_value, downEffectAddCurve);
						effects.Add(gameObject);
						playShadowSealingEffects.Add(gameObject);
					}
				}
			}
			yield return (object)new WaitForSeconds(downEffectTime);
			effects.ForEach(delegate(GameObject obj)
			{
				playShadowSealingEffects.Remove(obj);
				EffectManager.ReleaseEffect(obj);
			});
		}
		Transform tmp = EffectManager.GetUIEffect("ef_ui_downgauge_01", shadowSealingGaugeUI.GetGaugeTransform());
		if (tmp != null)
		{
			tmp.set_localScale(kShadowSealingGaugeEffectScale);
		}
		shadowSealingGaugeUI.SetPercent(per, anim: false);
		yield return (object)new WaitForSeconds(1f);
		if (per >= 1f && !targetEnemy.IsDebuffShadowSealing())
		{
			Transform trans = EffectManager.GetUIEffect("ef_ui_downgauge_02", shadowSealingGaugeUI.GetGaugeTransform());
			if (trans != null)
			{
				playShadowSealingEffects.Add(trans.get_gameObject());
				while (targetEnemy.actionID == (Character.ACTION_ID)19)
				{
					yield return null;
				}
				playShadowSealingEffects.Remove(trans.get_gameObject());
				EffectManager.ReleaseEffect(trans.get_gameObject());
			}
		}
		playShadowSealingEffectCount--;
	}

	public void ShowConcussion(bool isVisible)
	{
		int num = 0;
		if (object.ReferenceEquals(targetEnemy, null))
		{
			isVisible = false;
		}
		concussionRoot.SetActive(false);
		if (isVisible)
		{
			float percent = CalcConcussionPercent();
			concussionGaugeUI.SetPercent(percent, anim: false);
			concussionRoot.SetActive(true);
		}
	}

	private float CalcConcussionPercent()
	{
		if (targetEnemy.IsConcussion())
		{
			return targetEnemy.GetConcussionTimeRate();
		}
		return (!(targetEnemy.concussionMax > 0f)) ? 0f : (targetEnemy.concussionTotal / targetEnemy.concussionMax);
	}

	public void DirectionConcussionGauge(Vector3 world_hit_pos)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_gameObject().get_activeInHierarchy() && concussionRoot.get_activeInHierarchy())
		{
			this.StartCoroutine(_DirectionConcussionGauge(world_hit_pos));
		}
	}

	private IEnumerator _DirectionConcussionGauge(Vector3 world_hit_pos)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		float per = CalcConcussionPercent();
		int effect_num = (int)((per - concussionPercent) * 10f) + 1;
		concussionPercent = per;
		playConcussionEffectCount++;
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0)
		{
			Vector3 screen_pos = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos);
			Vector3 ui_pos = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screen_pos);
			List<GameObject> effects = new List<GameObject>();
			for (int i = 0; i < effect_num; i++)
			{
				Transform uIEffect = EffectManager.GetUIEffect("ef_ui_downenergy_01", concussionGaugeUI.GetGaugeTransform());
				if (!(uIEffect == null))
				{
					ui_pos.z = 1f;
					uIEffect.set_position(ui_pos);
					GameObject gameObject = uIEffect.get_gameObject();
					TransformInterpolator transformInterpolator = gameObject.AddComponent<TransformInterpolator>();
					if (!(transformInterpolator == null))
					{
						Vector3 add_value = default(Vector3);
						add_value._002Ector(Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), 0f);
						transformInterpolator.Translate(downEffectTime, Vector3.get_zero(), downEffectEaseCurve, add_value, downEffectAddCurve);
						effects.Add(gameObject);
						playConcussionEffects.Add(gameObject);
					}
				}
			}
			yield return (object)new WaitForSeconds(downEffectTime);
			effects.ForEach(delegate(GameObject obj)
			{
				playConcussionEffects.Remove(obj);
				EffectManager.ReleaseEffect(obj);
			});
		}
		Transform tmp = EffectManager.GetUIEffect("ef_ui_downgauge_01", concussionGaugeUI.GetGaugeTransform());
		if (tmp != null)
		{
			tmp.set_localScale(kConcussionGaugeEffectScale);
		}
		concussionGaugeUI.SetPercent(per, anim: false);
		yield return (object)new WaitForSeconds(1f);
		if (per >= 1f && !targetEnemy.IsConcussion())
		{
			Transform trans = EffectManager.GetUIEffect("ef_ui_downgauge_02", concussionGaugeUI.GetGaugeTransform());
			if (trans != null)
			{
				playConcussionEffects.Add(trans.get_gameObject());
				while (targetEnemy.actionID == (Character.ACTION_ID)25)
				{
					yield return null;
				}
				playConcussionEffects.Remove(trans.get_gameObject());
				EffectManager.ReleaseEffect(trans.get_gameObject());
			}
		}
		playConcussionEffectCount--;
	}

	public void SetAegisBarPercent(float percent)
	{
		if (object.ReferenceEquals(hpFocusFrame, null))
		{
			return;
		}
		if (percent <= 0f)
		{
			aegisGaugeUI.get_gameObject().SetActive(false);
			hpFocusFrame.SetActive(false);
			return;
		}
		aegisGaugeUI.SetPercent(percent, anim: false);
		if (!aegisGaugeUI.get_gameObject().get_activeSelf())
		{
			aegisGaugeUI.get_gameObject().SetActive(true);
			hpFocusFrame.SetActive(true);
		}
	}

	public void PlayShakeHpMultiX()
	{
		if (hpMultiXTween != null)
		{
			Debug.Log((object)"Shake");
			hpMultiXTween.Reset();
			hpMultiXTween.Play();
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
		if (tutorialObj != null)
		{
			tutorialObj.SetActive(isActive);
			weakRootName.get_gameObject().SetActive(!isActive);
			enemyName.get_gameObject().SetActive(!isActive);
			sprElement.get_gameObject().SetActive(!isActive);
		}
	}
}
