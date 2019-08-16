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
			load_queue.CacheSE(se_id);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		this.get_gameObject().SetActive(false);
	}

	protected override void OnDisable()
	{
		tweenCtrl.Skip();
		if (rareBossTweenCtrl != null)
		{
			rareBossTweenCtrl.Skip();
		}
		base.OnDisable();
	}

	public void Play(ENEMY_TYPE type, int rareBossType = 0, bool isFieldBoss = false)
	{
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
			fieldEnemyBossTweenCtrl.Play();
			SoundManager.PlayOneshotJingle(40000031);
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
				rareBossTweenCtrl.Play();
			}
			SoundManager.PlayOneshotJingle(40000163);
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
			tweenCtrl.Play();
			SoundManager.PlayOneshotJingle(40000031);
			int j = 0;
			for (int num2 = effect.Length; j < num2; j++)
			{
				this.StartCoroutine(Direction(effect[j]));
			}
		}
		SoundManager.RequestBGM(12);
	}

	public void PlayRareFieldEnemy()
	{
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
		fieldEnemyRareTweenCtrl.Play();
		SoundManager.PlayOneshotJingle(40000031);
		int i = 0;
		for (int num = effect.Length; i < num; i++)
		{
			this.StartCoroutine(Direction(effect[i]));
		}
	}

	public void PlayFieldFishingEnemy(bool isRare)
	{
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
			fieldFishingRareTweenCtrl.Play();
		}
		else
		{
			fieldFishingRareTweenCtrl.get_gameObject().SetActive(false);
			fieldFishingTweenCtrl.get_gameObject().SetActive(true);
			fieldFishingTweenCtrl.Reset();
			fieldFishingTweenCtrl.Play();
			int i = 0;
			for (int num = fishingEffect.Length; i < num; i++)
			{
				this.StartCoroutine(Direction(fishingEffect[i]));
			}
		}
		SoundManager.PlayOneshotJingle(40000031);
	}

	private IEnumerator Direction(EffectData data)
	{
		yield return (object)new WaitForSeconds(data.delayTime);
		EffectManager.GetUIEffect(data.effectName, data.link);
	}

	public void FadeOut(float delay, float duration, Action onComplete)
	{
		this.StartCoroutine(DoFadeOut(delay, duration, onComplete));
	}

	private IEnumerator DoFadeOut(float delay, float duration, Action onComplete)
	{
		yield return (object)new WaitForSeconds(delay);
		TweenAlpha.Begin(this.get_gameObject(), duration, 0f);
		yield return (object)new WaitForSeconds(duration);
		onComplete?.Invoke();
		if (this.get_gameObject() != null)
		{
			this.get_gameObject().SetActive(false);
		}
	}
}
