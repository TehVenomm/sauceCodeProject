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
		foreach (int se_id in array)
		{
			load_queue.CacheSE(se_id);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		base.gameObject.SetActive(value: false);
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
		base.gameObject.SetActive(value: true);
		TweenAlpha.Begin(base.gameObject, 0f, 1f);
		if (fieldFishingTweenCtrl != null)
		{
			fieldFishingTweenCtrl.gameObject.SetActive(value: false);
		}
		if (fieldFishingRareTweenCtrl != null)
		{
			fieldFishingRareTweenCtrl.gameObject.SetActive(value: false);
		}
		if (isFieldBoss)
		{
			if (rareBossTweenCtrl != null)
			{
				rareBossTweenCtrl.gameObject.SetActive(value: false);
			}
			if (tweenCtrl != null)
			{
				tweenCtrl.gameObject.SetActive(value: false);
			}
			if (fieldEnemyRareTweenCtrl != null)
			{
				fieldEnemyRareTweenCtrl.gameObject.SetActive(value: false);
			}
			fieldEnemyBossTweenCtrl.gameObject.SetActive(value: true);
			fieldEnemyBossTweenCtrl.Reset();
			fieldEnemyBossTweenCtrl.Play();
			SoundManager.PlayOneshotJingle(40000031);
			int i = 0;
			for (int num = effect.Length; i < num; i++)
			{
				StartCoroutine(Direction(effect[i]));
			}
		}
		else if (rareBossType > 0)
		{
			tweenCtrl.gameObject.SetActive(value: false);
			if (fieldEnemyBossTweenCtrl != null)
			{
				fieldEnemyBossTweenCtrl.gameObject.SetActive(value: false);
			}
			if (fieldEnemyRareTweenCtrl != null)
			{
				fieldEnemyRareTweenCtrl.gameObject.SetActive(value: false);
			}
			if (rareBossTweenCtrl != null)
			{
				rareBossTweenCtrl.gameObject.SetActive(value: true);
				rareBossTweenCtrl.Reset();
				rareBossTweenCtrl.Play();
			}
			SoundManager.PlayOneshotJingle(40000163);
		}
		else
		{
			if (rareBossTweenCtrl != null)
			{
				rareBossTweenCtrl.gameObject.SetActive(value: false);
			}
			if (fieldEnemyBossTweenCtrl != null)
			{
				fieldEnemyBossTweenCtrl.gameObject.SetActive(value: false);
			}
			if (fieldEnemyRareTweenCtrl != null)
			{
				fieldEnemyRareTweenCtrl.gameObject.SetActive(value: false);
			}
			tweenCtrl.gameObject.SetActive(value: true);
			tweenCtrl.Reset();
			tweenCtrl.Play();
			SoundManager.PlayOneshotJingle(40000031);
			int j = 0;
			for (int num2 = effect.Length; j < num2; j++)
			{
				StartCoroutine(Direction(effect[j]));
			}
		}
		SoundManager.RequestBGM(12);
	}

	public void PlayRareFieldEnemy()
	{
		base.gameObject.SetActive(value: true);
		TweenAlpha.Begin(base.gameObject, 0f, 1f);
		if (rareBossTweenCtrl != null)
		{
			rareBossTweenCtrl.gameObject.SetActive(value: false);
		}
		if (fieldEnemyBossTweenCtrl != null)
		{
			fieldEnemyBossTweenCtrl.gameObject.SetActive(value: false);
		}
		if (fieldFishingTweenCtrl != null)
		{
			fieldFishingTweenCtrl.gameObject.SetActive(value: false);
		}
		if (fieldFishingRareTweenCtrl != null)
		{
			fieldFishingRareTweenCtrl.gameObject.SetActive(value: false);
		}
		if (tweenCtrl != null)
		{
			tweenCtrl.gameObject.SetActive(value: false);
		}
		fieldEnemyRareTweenCtrl.gameObject.SetActive(value: true);
		fieldEnemyRareTweenCtrl.Reset();
		fieldEnemyRareTweenCtrl.Play();
		SoundManager.PlayOneshotJingle(40000031);
		int i = 0;
		for (int num = effect.Length; i < num; i++)
		{
			StartCoroutine(Direction(effect[i]));
		}
	}

	public void PlayFieldFishingEnemy(bool isRare)
	{
		base.gameObject.SetActive(value: true);
		TweenAlpha.Begin(base.gameObject, 0f, 1f);
		if (rareBossTweenCtrl != null)
		{
			rareBossTweenCtrl.gameObject.SetActive(value: false);
		}
		if (fieldEnemyBossTweenCtrl != null)
		{
			fieldEnemyBossTweenCtrl.gameObject.SetActive(value: false);
		}
		if (fieldEnemyRareTweenCtrl != null)
		{
			fieldEnemyRareTweenCtrl.gameObject.SetActive(value: false);
		}
		if (tweenCtrl != null)
		{
			tweenCtrl.gameObject.SetActive(value: false);
		}
		if (isRare)
		{
			fieldFishingTweenCtrl.gameObject.SetActive(value: false);
			fieldFishingRareTweenCtrl.gameObject.SetActive(value: true);
			fieldFishingRareTweenCtrl.Reset();
			fieldFishingRareTweenCtrl.Play();
		}
		else
		{
			fieldFishingRareTweenCtrl.gameObject.SetActive(value: false);
			fieldFishingTweenCtrl.gameObject.SetActive(value: true);
			fieldFishingTweenCtrl.Reset();
			fieldFishingTweenCtrl.Play();
			int i = 0;
			for (int num = fishingEffect.Length; i < num; i++)
			{
				StartCoroutine(Direction(fishingEffect[i]));
			}
		}
		SoundManager.PlayOneshotJingle(40000031);
	}

	private IEnumerator Direction(EffectData data)
	{
		yield return new WaitForSeconds(data.delayTime);
		EffectManager.GetUIEffect(data.effectName, data.link);
	}

	public void FadeOut(float delay, float duration, Action onComplete)
	{
		StartCoroutine(DoFadeOut(delay, duration, onComplete));
	}

	private IEnumerator DoFadeOut(float delay, float duration, Action onComplete)
	{
		yield return new WaitForSeconds(delay);
		TweenAlpha.Begin(base.gameObject, duration, 0f);
		yield return new WaitForSeconds(duration);
		onComplete?.Invoke();
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
