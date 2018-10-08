using System;
using System.Collections;
using UnityEngine;

public class UIInGameFieldQuestWarning : MonoBehaviourSingleton<UIInGameFieldQuestWarning>
{
	[Serializable]
	public class EffectData
	{
		public Transform link;

		public string effectName;

		public float delayTime;
	}

	public enum AUDIO
	{
		BOSS_WARNING = 40000031,
		BOSS_WARNING_SR = 40000163
	}

	[SerializeField]
	protected UITweenCtrl tweenCtrl;

	[SerializeField]
	protected EffectData[] effect;

	[SerializeField]
	protected UITweenCtrl rareBossTweenCtrl;

	[SerializeField]
	protected UITweenCtrl fieldEnemyBossTweenCtrl;

	[SerializeField]
	protected UITweenCtrl fieldEnemyRareTweenCtrl;

	[SerializeField]
	protected EffectData[] fishingEffect;

	[SerializeField]
	protected UITweenCtrl fieldFishingTweenCtrl;

	[SerializeField]
	protected UITweenCtrl fieldFishingRareTweenCtrl;

	public void Load(LoadingQueue load_queue)
	{
		int i = 0;
		for (int num = effect.Length; i < num; i++)
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, effect[i].effectName);
		}
		int[] array = (int[])Enum.GetValues(typeof(AUDIO));
		int[] array2 = array;
		foreach (int se_id in array2)
		{
			load_queue.CacheSE(se_id, null);
		}
	}

	protected override void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		this.get_gameObject().SetActive(false);
	}

	protected override void OnDisable()
	{
		tweenCtrl.Skip(true);
		if (rareBossTweenCtrl != null)
		{
			rareBossTweenCtrl.Skip(true);
		}
		base.OnDisable();
	}

	public void Play(ENEMY_TYPE type, int rareBossType = 0, bool isFieldBoss = false)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(true);
		TweenAlpha.Begin(this.get_gameObject(), 0f, 1f);
		if (fieldFishingTweenCtrl != null)
		{
			fieldFishingTweenCtrl.get_gameObject().SetActive(false);
		}
		if (fieldFishingRareTweenCtrl != null)
		{
			fieldFishingRareTweenCtrl.get_gameObject().SetActive(false);
		}
		if (isFieldBoss)
		{
			if (rareBossTweenCtrl != null)
			{
				rareBossTweenCtrl.get_gameObject().SetActive(false);
			}
			if (tweenCtrl != null)
			{
				tweenCtrl.get_gameObject().SetActive(false);
			}
			if (fieldEnemyRareTweenCtrl != null)
			{
				fieldEnemyRareTweenCtrl.get_gameObject().SetActive(false);
			}
			fieldEnemyBossTweenCtrl.get_gameObject().SetActive(true);
			fieldEnemyBossTweenCtrl.Reset();
			fieldEnemyBossTweenCtrl.Play(true, null);
			SoundManager.PlayOneshotJingle(40000031, null, null);
			int i = 0;
			for (int num = effect.Length; i < num; i++)
			{
				this.StartCoroutine(Direction(effect[i]));
			}
		}
		else if (rareBossType > 0)
		{
			tweenCtrl.get_gameObject().SetActive(false);
			if (fieldEnemyBossTweenCtrl != null)
			{
				fieldEnemyBossTweenCtrl.get_gameObject().SetActive(false);
			}
			if (fieldEnemyRareTweenCtrl != null)
			{
				fieldEnemyRareTweenCtrl.get_gameObject().SetActive(false);
			}
			if (rareBossTweenCtrl != null)
			{
				rareBossTweenCtrl.get_gameObject().SetActive(true);
				rareBossTweenCtrl.Reset();
				rareBossTweenCtrl.Play(true, null);
			}
			SoundManager.PlayOneshotJingle(40000163, null, null);
		}
		else
		{
			if (rareBossTweenCtrl != null)
			{
				rareBossTweenCtrl.get_gameObject().SetActive(false);
			}
			if (fieldEnemyBossTweenCtrl != null)
			{
				fieldEnemyBossTweenCtrl.get_gameObject().SetActive(false);
			}
			if (fieldEnemyRareTweenCtrl != null)
			{
				fieldEnemyRareTweenCtrl.get_gameObject().SetActive(false);
			}
			tweenCtrl.get_gameObject().SetActive(true);
			tweenCtrl.Reset();
			tweenCtrl.Play(true, null);
			SoundManager.PlayOneshotJingle(40000031, null, null);
			int j = 0;
			for (int num2 = effect.Length; j < num2; j++)
			{
				this.StartCoroutine(Direction(effect[j]));
			}
		}
		SoundManager.RequestBGM(12, true);
	}

	public void PlayRareFieldEnemy()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(true);
		TweenAlpha.Begin(this.get_gameObject(), 0f, 1f);
		if (rareBossTweenCtrl != null)
		{
			rareBossTweenCtrl.get_gameObject().SetActive(false);
		}
		if (fieldEnemyBossTweenCtrl != null)
		{
			fieldEnemyBossTweenCtrl.get_gameObject().SetActive(false);
		}
		if (fieldFishingTweenCtrl != null)
		{
			fieldFishingTweenCtrl.get_gameObject().SetActive(false);
		}
		if (fieldFishingRareTweenCtrl != null)
		{
			fieldFishingRareTweenCtrl.get_gameObject().SetActive(false);
		}
		if (tweenCtrl != null)
		{
			tweenCtrl.get_gameObject().SetActive(false);
		}
		fieldEnemyRareTweenCtrl.get_gameObject().SetActive(true);
		fieldEnemyRareTweenCtrl.Reset();
		fieldEnemyRareTweenCtrl.Play(true, null);
		SoundManager.PlayOneshotJingle(40000031, null, null);
		int i = 0;
		for (int num = effect.Length; i < num; i++)
		{
			this.StartCoroutine(Direction(effect[i]));
		}
	}

	public void PlayFieldFishingEnemy(bool isRare)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(true);
		TweenAlpha.Begin(this.get_gameObject(), 0f, 1f);
		if (rareBossTweenCtrl != null)
		{
			rareBossTweenCtrl.get_gameObject().SetActive(false);
		}
		if (fieldEnemyBossTweenCtrl != null)
		{
			fieldEnemyBossTweenCtrl.get_gameObject().SetActive(false);
		}
		if (fieldEnemyRareTweenCtrl != null)
		{
			fieldEnemyRareTweenCtrl.get_gameObject().SetActive(false);
		}
		if (tweenCtrl != null)
		{
			tweenCtrl.get_gameObject().SetActive(false);
		}
		if (isRare)
		{
			fieldFishingTweenCtrl.get_gameObject().SetActive(false);
			fieldFishingRareTweenCtrl.get_gameObject().SetActive(true);
			fieldFishingRareTweenCtrl.Reset();
			fieldFishingRareTweenCtrl.Play(true, null);
		}
		else
		{
			fieldFishingRareTweenCtrl.get_gameObject().SetActive(false);
			fieldFishingTweenCtrl.get_gameObject().SetActive(true);
			fieldFishingTweenCtrl.Reset();
			fieldFishingTweenCtrl.Play(true, null);
			int i = 0;
			for (int num = fishingEffect.Length; i < num; i++)
			{
				this.StartCoroutine(Direction(fishingEffect[i]));
			}
		}
		SoundManager.PlayOneshotJingle(40000031, null, null);
	}

	private IEnumerator Direction(EffectData data)
	{
		yield return (object)new WaitForSeconds(data.delayTime);
		EffectManager.GetUIEffect(data.effectName, data.link, -0.001f, 0, null);
	}

	public void FadeOut(float delay, float duration, Action onComplete)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoFadeOut(delay, duration, onComplete));
	}

	private IEnumerator DoFadeOut(float delay, float duration, Action onComplete)
	{
		yield return (object)new WaitForSeconds(delay);
		TweenAlpha.Begin(this.get_gameObject(), duration, 0f);
		yield return (object)new WaitForSeconds(duration);
		if (onComplete != null)
		{
			onComplete.Invoke();
		}
		if (this.get_gameObject() != null)
		{
			this.get_gameObject().SetActive(false);
		}
	}
}
