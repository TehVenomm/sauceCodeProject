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

	private Vector3 shakeTargetDefaultPos = Vector3.zero;

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
		base.gameObject.SetActive(value: false);
		SetGaugeType(GaugeType.HP);
		SetActiveTutorialObj(isActive: false);
	}

	private void Start()
	{
		if (!(targetEnemy == null))
		{
			if ((bool)hpGaugeUI)
			{
				float percent = (targetEnemy.hpMax > 0) ? ((float)targetEnemy.hp / (float)targetEnemy.hpMax) : 0f;
				hpGaugeUI.SetPercent(percent, anim: false);
			}
			if ((bool)downGaugeUI)
			{
				downGaugeUISpr = downGaugeUI.GetComponent<UISprite>();
				float percent2 = CalcDownPercent();
				downGaugeUI.SetPercent(percent2, anim: false);
				downPercent = percent2;
			}
			if ((bool)shadowSealingGaugeUI)
			{
				float percent3 = CalcShadowSealingPercent();
				shadowSealingGaugeUI.SetPercent(percent3, anim: false);
				shadowSealingPercent = percent3;
			}
			if ((bool)concussionGaugeUI)
			{
				float percent4 = CalcConcussionPercent();
				concussionGaugeUI.SetPercent(percent4, anim: false);
				concussionPercent = percent4;
			}
			if ((bool)shieldGaugeUI)
			{
				isLoadEffectComplete = false;
				StartCoroutine(DoLoadEffect());
				shieldGaugeUI.SetPercent(0f, anim: false);
			}
			if (shakeTarget != null)
			{
				shakeTargetDefaultPos = shakeTarget.transform.localPosition;
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
			base.gameObject.SetActive(value: false);
			return;
		}
		statusIcons.target = enemy;
		if ((bool)enemyName)
		{
			string charaName = enemy.charaName;
			enemyName.text = charaName;
			int num = enemy.enemyLevel;
			if (level == null && num > 0)
			{
				enemyName.text = $"Lv{num.ToString()} {charaName}";
			}
		}
		if ((bool)level)
		{
			level.text = enemy.enemyLevel.ToString();
		}
		if ((bool)hpGaugeUI)
		{
			float num2 = (targetEnemy.hpMax > 0) ? ((float)targetEnemy.hp / (float)targetEnemy.hpMax) : 0f;
			if (hpGaugeUI.nowPercent != num2)
			{
				hpGaugeUI.SetPercent(num2, anim: false);
			}
		}
		if ((bool)downGaugeUI)
		{
			float num3 = CalcDownPercent();
			if (downGaugeUI.nowPercent != num3)
			{
				downGaugeUI.SetPercent(num3, anim: false);
				downPercent = num3;
			}
		}
		if ((bool)shadowSealingGaugeUI)
		{
			float num4 = CalcShadowSealingPercent();
			if (shadowSealingGaugeUI.nowPercent != num4)
			{
				shadowSealingGaugeUI.SetPercent(num4, anim: false);
				shadowSealingPercent = num4;
			}
		}
		if ((bool)concussionGaugeUI)
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
			base.gameObject.SetActive(value: true);
		}
	}

	private void LateUpdate()
	{
		if (targetEnemy == null)
		{
			return;
		}
		if ((bool)hpGaugeUI)
		{
			float num = (targetEnemy.hpMax > 0) ? ((float)targetEnemy.hpShow / (float)targetEnemy.hpMax) : 0f;
			if (hpGaugeUI.nowPercent != num)
			{
				hpGaugeUI.SetPercent(num);
			}
		}
		if ((bool)downGaugeUI && (playEffectCount == 0 || targetEnemy.IsActDown()))
		{
			float num2 = CalcDownPercent();
			if (downGaugeUI.nowPercent != num2)
			{
				downGaugeUI.SetPercent(num2, anim: false);
				downPercent = num2;
			}
		}
		if ((bool)shadowSealingGaugeUI && (playShadowSealingEffectCount == 0 || targetEnemy.IsDebuffShadowSealing()))
		{
			float num3 = CalcShadowSealingPercent();
			if (shadowSealingGaugeUI.nowPercent != num3)
			{
				shadowSealingGaugeUI.SetPercent(num3, anim: false);
				shadowSealingPercent = num3;
			}
		}
		if ((bool)concussionGaugeUI && (playConcussionEffectCount == 0 || targetEnemy.IsConcussion()))
		{
			float num4 = CalcConcussionPercent();
			if (concussionGaugeUI.nowPercent != num4)
			{
				concussionGaugeUI.SetPercent(num4, anim: false);
				concussionPercent = num4;
			}
		}
		if ((bool)shieldGaugeUI && targetEnemy.IsValidShield())
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
					StartCoroutine(PlayShieldGaugeAnimation());
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
			StopCoroutine(shakeCoroutine);
			shakeCoroutine = null;
		}
		if (shakeCoroutine == null)
		{
			shakeCoroutine = StartCoroutine(_PlayShakeHpGauge(power, shakeTime, cycleTime));
		}
	}

	private IEnumerator _PlayShakeHpGauge(float power, float shakeTime, float cycleTime)
	{
		float timer = 0f;
		while (timer < shakeTime)
		{
			timer += Time.deltaTime;
			float d = power * Mathf.Sin(6.28f * timer / cycleTime);
			shakeTarget.transform.localPosition = shakeTargetDefaultPos + d * Vector3.up;
			power -= power * Time.deltaTime / shakeTime;
			yield return null;
		}
		shakeTarget.transform.localPosition = shakeTargetDefaultPos;
	}

	private IEnumerator PlayShieldGaugeAnimation()
	{
		Renderer component = shieldGaugeUI.GetComponent<Renderer>();
		Renderer component2 = hpGaugeUI.GetComponent<Renderer>();
		UISprite component3 = hpGaugeBase.GetComponent<UISprite>();
		component2.material.renderQueue = component3.material.renderQueue + 1;
		component.material.renderQueue = component2.material.renderQueue + 1;
		Transform effectTrans = null;
		if (isLoadEffectComplete)
		{
			effectTrans = EffectManager.GetUIEffect("ef_ui_shieldgauge_01", component3, -0.001f, component.material.renderQueue + 10);
			if (effectTrans != null)
			{
				effectTrans.rotation = Quaternion.identity;
				effectTrans.localScale = Vector3.one;
			}
		}
		float width = component3.width - component3.GetAtlasSprite().borderLeft - component3.GetAtlasSprite().borderRight;
		Vector3 offset = Vector3.right * component3.GetAtlasSprite().borderLeft + Vector3.down * component3.height / 2f;
		float shieldPercent = Mathf.Max(0f, (float)(int)targetEnemy.ShieldHp / (float)(int)targetEnemy.ShieldHpMax);
		while (shieldGaugeUI.nowPercent < shieldPercent)
		{
			shieldGaugeUI.SetPercent(shieldGaugeUI.nowPercent + Time.deltaTime / shieldChargeTime);
			if (effectTrans != null)
			{
				effectTrans.localPosition = offset + Vector3.right * width * shieldGaugeUI.nowPercent;
			}
			shieldPercent = Mathf.Max(0f, (float)(int)targetEnemy.ShieldHp / (float)(int)targetEnemy.ShieldHpMax);
			yield return null;
		}
		yield return null;
		if (effectTrans != null)
		{
			EffectManager.ReleaseEffect(effectTrans.gameObject);
		}
		isPlayShieldGaugeAnimation = false;
	}

	public void DirectionDownGauge(AttackedHitStatusFix status)
	{
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine(_DirectionDownGauge(status));
		}
	}

	private IEnumerator _DirectionDownGauge(AttackedHitStatusFix status)
	{
		Vector3 hitPos = status.hitPos;
		float down_percent = (targetEnemy.downMax > 0) ? (targetEnemy.downTotal / (float)targetEnemy.downMax) : 0f;
		float workDownTotal = 0f;
		int effect_num = (int)((down_percent - downPercent) * 10f) + 1;
		downPercent = down_percent;
		playEffectCount++;
		yield return StartCoroutine(_DirectionDownParticle(effect_num, hitPos));
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
		yield return new WaitForSeconds(1f);
		if (down_percent >= 1f && !targetEnemy.IsActDown())
		{
			yield return StartCoroutine(_DirectionDownCompleteEffect(isInActDown: false));
		}
		else if (workDownTotal > 0f && targetEnemy.IsActDown())
		{
			yield return StartCoroutine(_DirectionDownCompleteEffect(isInActDown: true));
		}
		playEffectCount--;
	}

	private IEnumerator _DirectionDownParticle(int effect_num, Vector3 world_hit_pos)
	{
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
		{
			yield break;
		}
		float num = (!targetEnemy.IsActDown()) ? 1 : 2;
		Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos);
		Vector3 position2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
		List<GameObject> effects = new List<GameObject>();
		for (int i = 0; i < effect_num; i++)
		{
			Transform uIEffect = EffectManager.GetUIEffect("ef_ui_downenergy_01", downGaugeUI.GetGaugeTransform());
			if (!(uIEffect == null))
			{
				position2.z = 1f;
				uIEffect.position = position2;
				GameObject gameObject = uIEffect.gameObject;
				TransformInterpolator transformInterpolator = gameObject.AddComponent<TransformInterpolator>();
				if (!(transformInterpolator == null))
				{
					transformInterpolator.Translate(add_value: new Vector3(Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), 0f), _time: downEffectTime / num, target: Vector3.zero, ease_curve: downEffectEaseCurve, add_curve: downEffectAddCurve);
					effects.Add(gameObject);
					playEffects.Add(gameObject);
				}
			}
		}
		yield return new WaitForSeconds(downEffectTime / num);
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
			playEffects.Add(trans.gameObject);
			bool isDownMotion = targetEnemy.IsActDown();
			while (isInActDown != isDownMotion)
			{
				yield return null;
			}
			playEffects.Remove(trans.gameObject);
			EffectManager.ReleaseEffect(trans.gameObject);
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
		if (targetEnemy.enemyTableData == null)
		{
			Log.Error("enemyData is null !!");
			return;
		}
		SetElementIcon((targetEnemy.changeElementIcon != ELEMENT_TYPE.MAX) ? targetEnemy.changeElementIcon : targetEnemy.GetElementTypeByRegion());
		SetWeakElementIcon((targetEnemy.changeWeakElementIcon != ELEMENT_TYPE.MAX) ? targetEnemy.changeWeakElementIcon : targetEnemy.GetAntiElementTypeByRegion());
	}

	public void SetElementIcon(ELEMENT_TYPE element)
	{
		if (sprElement != null)
		{
			sprElement.gameObject.SetActive(value: false);
			string iconElementSpriteName = ItemIcon.GetIconElementSpriteName(element);
			if (!string.IsNullOrEmpty(iconElementSpriteName))
			{
				sprElement.spriteName = iconElementSpriteName;
				sprElement.gameObject.SetActive(value: true);
			}
		}
	}

	public void SetWeakElementIcon(ELEMENT_TYPE weakElement)
	{
		if (weakRootName != null)
		{
			weakRootName.gameObject.SetActive(value: true);
		}
		if (sprWeakElement != null)
		{
			sprWeakElement.gameObject.SetActive(value: false);
		}
		if (nonWeakElementName != null)
		{
			nonWeakElementName.gameObject.SetActive(value: false);
		}
		if (weakElement == ELEMENT_TYPE.MAX)
		{
			if (nonWeakElementName != null)
			{
				nonWeakElementName.gameObject.SetActive(value: true);
			}
		}
		else if (sprWeakElement != null)
		{
			string iconElementSpriteName = ItemIcon.GetIconElementSpriteName(weakElement);
			if (!string.IsNullOrEmpty(iconElementSpriteName))
			{
				sprWeakElement.spriteName = iconElementSpriteName;
				sprWeakElement.gameObject.SetActive(value: true);
			}
		}
	}

	private void SetGaugeType(GaugeType type)
	{
		currentGaugeType = type;
		switch (type)
		{
		case GaugeType.HP:
			shieldGaugeUI.gameObject.SetActive(value: false);
			hpFocusFrame.SetActive(value: false);
			break;
		case GaugeType.SHIELD:
			shieldGaugeUI.gameObject.SetActive(value: true);
			hpFocusFrame.SetActive(value: true);
			break;
		}
	}

	private float CalcDownPercent()
	{
		bool flag = false;
		float result = (targetEnemy.downMax > 0) ? (targetEnemy.downTotal / (float)targetEnemy.downMax) : 0f;
		if (targetEnemy.IsActDown())
		{
			if (targetEnemy.stackBuffCtrl.GetStackCount(StackBuffController.STACK_TYPE.SNATCH) > 0)
			{
				flag = true;
			}
			result = targetEnemy.GetDownTimeRate();
		}
		if (isChangeDownGaugeSpr != flag && (object)downGaugeUISpr != null)
		{
			downGaugeUISpr.spriteName = (flag ? "enemy_kagenui_over" : "enemy_down_over");
			isChangeDownGaugeSpr = flag;
		}
		return result;
	}

	public void ShowShadowSealing(bool isVisible)
	{
		int num = 0;
		if ((object)targetEnemy == null)
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
		shadowSealingRoot.SetActive(value: false);
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
				shadowSealingPartitions[i].transform.localPosition = new Vector3(-61.5f + num2 * (float)(i + 1), 2f, 0f);
				shadowSealingPartitions[i].SetActive(value: true);
			}
			else
			{
				shadowSealingPartitions[i].SetActive(value: false);
			}
		}
		float percent = (float)shadowSealingStuckNum / (float)num;
		shadowSealingGaugeUI.SetPercent(percent, anim: false);
		shadowSealingRoot.SetActive(value: true);
	}

	private float CalcShadowSealingPercent()
	{
		if (targetEnemy.IsDebuffShadowSealing())
		{
			return targetEnemy.GetDebuffShadowSealingTimeRate();
		}
		int shadowSealingNum = targetEnemy.GetShadowSealingNum();
		int shadowSealingStuckNum = targetEnemy.GetShadowSealingStuckNum();
		if (shadowSealingNum <= 0 && shadowSealingStuckNum <= 0)
		{
			return 0f;
		}
		return (float)shadowSealingStuckNum / (float)shadowSealingNum;
	}

	public void DirectionShadowSealingGauge(Vector3 world_hit_pos)
	{
		if (base.gameObject.activeInHierarchy && shadowSealingRoot.activeInHierarchy)
		{
			StartCoroutine(_DirectionShadowSealingGauge(world_hit_pos));
		}
	}

	private IEnumerator _DirectionShadowSealingGauge(Vector3 world_hit_pos)
	{
		float per = CalcShadowSealingPercent();
		int num = (int)((per - shadowSealingPercent) * 10f) + 1;
		shadowSealingPercent = per;
		playShadowSealingEffectCount++;
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0)
		{
			Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos);
			Vector3 position2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
			List<GameObject> effects = new List<GameObject>();
			for (int i = 0; i < num; i++)
			{
				Transform uIEffect = EffectManager.GetUIEffect("ef_ui_downenergy_01", shadowSealingGaugeUI.GetGaugeTransform());
				if (!(uIEffect == null))
				{
					position2.z = 1f;
					uIEffect.position = position2;
					GameObject gameObject = uIEffect.gameObject;
					TransformInterpolator transformInterpolator = gameObject.AddComponent<TransformInterpolator>();
					if (!(transformInterpolator == null))
					{
						transformInterpolator.Translate(add_value: new Vector3(Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), 0f), _time: downEffectTime, target: Vector3.zero, ease_curve: downEffectEaseCurve, add_curve: downEffectAddCurve);
						effects.Add(gameObject);
						playShadowSealingEffects.Add(gameObject);
					}
				}
			}
			yield return new WaitForSeconds(downEffectTime);
			effects.ForEach(delegate(GameObject obj)
			{
				playShadowSealingEffects.Remove(obj);
				EffectManager.ReleaseEffect(obj);
			});
		}
		Transform uIEffect2 = EffectManager.GetUIEffect("ef_ui_downgauge_01", shadowSealingGaugeUI.GetGaugeTransform());
		if (uIEffect2 != null)
		{
			uIEffect2.localScale = kShadowSealingGaugeEffectScale;
		}
		shadowSealingGaugeUI.SetPercent(per, anim: false);
		yield return new WaitForSeconds(1f);
		if (per >= 1f && !targetEnemy.IsDebuffShadowSealing())
		{
			Transform trans = EffectManager.GetUIEffect("ef_ui_downgauge_02", shadowSealingGaugeUI.GetGaugeTransform());
			if (trans != null)
			{
				playShadowSealingEffects.Add(trans.gameObject);
				while (targetEnemy.actionID == (Character.ACTION_ID)19)
				{
					yield return null;
				}
				playShadowSealingEffects.Remove(trans.gameObject);
				EffectManager.ReleaseEffect(trans.gameObject);
			}
		}
		playShadowSealingEffectCount--;
	}

	public void ShowConcussion(bool isVisible)
	{
		if ((object)targetEnemy == null)
		{
			isVisible = false;
		}
		concussionRoot.SetActive(value: false);
		if (isVisible)
		{
			float percent = CalcConcussionPercent();
			concussionGaugeUI.SetPercent(percent, anim: false);
			concussionRoot.SetActive(value: true);
		}
	}

	private float CalcConcussionPercent()
	{
		if (targetEnemy.IsConcussion())
		{
			return targetEnemy.GetConcussionTimeRate();
		}
		if (!(targetEnemy.concussionMax > 0f))
		{
			return 0f;
		}
		return targetEnemy.concussionTotal / targetEnemy.concussionMax;
	}

	public void DirectionConcussionGauge(Vector3 world_hit_pos)
	{
		if (base.gameObject.activeInHierarchy && concussionRoot.activeInHierarchy)
		{
			StartCoroutine(_DirectionConcussionGauge(world_hit_pos));
		}
	}

	private IEnumerator _DirectionConcussionGauge(Vector3 world_hit_pos)
	{
		float per = CalcConcussionPercent();
		int num = (int)((per - concussionPercent) * 10f) + 1;
		concussionPercent = per;
		playConcussionEffectCount++;
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0)
		{
			Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos);
			Vector3 position2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
			List<GameObject> effects = new List<GameObject>();
			for (int i = 0; i < num; i++)
			{
				Transform uIEffect = EffectManager.GetUIEffect("ef_ui_downenergy_01", concussionGaugeUI.GetGaugeTransform());
				if (!(uIEffect == null))
				{
					position2.z = 1f;
					uIEffect.position = position2;
					GameObject gameObject = uIEffect.gameObject;
					TransformInterpolator transformInterpolator = gameObject.AddComponent<TransformInterpolator>();
					if (!(transformInterpolator == null))
					{
						transformInterpolator.Translate(add_value: new Vector3(Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), Random.Range(0f - downEffectAddRandomMax, downEffectAddRandomMax), 0f), _time: downEffectTime, target: Vector3.zero, ease_curve: downEffectEaseCurve, add_curve: downEffectAddCurve);
						effects.Add(gameObject);
						playConcussionEffects.Add(gameObject);
					}
				}
			}
			yield return new WaitForSeconds(downEffectTime);
			effects.ForEach(delegate(GameObject obj)
			{
				playConcussionEffects.Remove(obj);
				EffectManager.ReleaseEffect(obj);
			});
		}
		Transform uIEffect2 = EffectManager.GetUIEffect("ef_ui_downgauge_01", concussionGaugeUI.GetGaugeTransform());
		if (uIEffect2 != null)
		{
			uIEffect2.localScale = kConcussionGaugeEffectScale;
		}
		concussionGaugeUI.SetPercent(per, anim: false);
		yield return new WaitForSeconds(1f);
		if (per >= 1f && !targetEnemy.IsConcussion())
		{
			Transform trans = EffectManager.GetUIEffect("ef_ui_downgauge_02", concussionGaugeUI.GetGaugeTransform());
			if (trans != null)
			{
				playConcussionEffects.Add(trans.gameObject);
				while (targetEnemy.actionID == (Character.ACTION_ID)25)
				{
					yield return null;
				}
				playConcussionEffects.Remove(trans.gameObject);
				EffectManager.ReleaseEffect(trans.gameObject);
			}
		}
		playConcussionEffectCount--;
	}

	public void SetAegisBarPercent(float percent)
	{
		if ((object)hpFocusFrame == null)
		{
			return;
		}
		if (percent <= 0f)
		{
			aegisGaugeUI.gameObject.SetActive(value: false);
			hpFocusFrame.SetActive(value: false);
			return;
		}
		aegisGaugeUI.SetPercent(percent, anim: false);
		if (!aegisGaugeUI.gameObject.activeSelf)
		{
			aegisGaugeUI.gameObject.SetActive(value: true);
			hpFocusFrame.SetActive(value: true);
		}
	}

	public void PlayShakeHpMultiX()
	{
		if (hpMultiXTween != null)
		{
			Debug.Log("Shake");
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
			weakRootName.gameObject.SetActive(!isActive);
			enemyName.gameObject.SetActive(!isActive);
			sprElement.gameObject.SetActive(!isActive);
		}
	}
}
