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
		base.Awake();
		base.gameObject.SetActive(false);
	}

	protected override void OnDisable()
	{
		tweenCtrl.Skip(true);
		if ((UnityEngine.Object)rareBossTweenCtrl != (UnityEngine.Object)null)
		{
			rareBossTweenCtrl.Skip(true);
		}
		base.OnDisable();
	}

	public void Play(ENEMY_TYPE type, int rareBossType = 0, bool isFieldBoss = false)
	{
		base.gameObject.SetActive(true);
		TweenAlpha.Begin(base.gameObject, 0f, 1f);
		if ((UnityEngine.Object)fieldFishingTweenCtrl != (UnityEngine.Object)null)
		{
			fieldFishingTweenCtrl.gameObject.SetActive(false);
		}
		if ((UnityEngine.Object)fieldFishingRareTweenCtrl != (UnityEngine.Object)null)
		{
			fieldFishingRareTweenCtrl.gameObject.SetActive(false);
		}
		if (isFieldBoss)
		{
			if ((UnityEngine.Object)rareBossTweenCtrl != (UnityEngine.Object)null)
			{
				rareBossTweenCtrl.gameObject.SetActive(false);
			}
			if ((UnityEngine.Object)tweenCtrl != (UnityEngine.Object)null)
			{
				tweenCtrl.gameObject.SetActive(false);
			}
			if ((UnityEngine.Object)fieldEnemyRareTweenCtrl != (UnityEngine.Object)null)
			{
				fieldEnemyRareTweenCtrl.gameObject.SetActive(false);
			}
			fieldEnemyBossTweenCtrl.gameObject.SetActive(true);
			fieldEnemyBossTweenCtrl.Reset();
			fieldEnemyBossTweenCtrl.Play(true, null);
			SoundManager.PlayOneshotJingle(40000031, null, null);
			int i = 0;
			for (int num = effect.Length; i < num; i++)
			{
				StartCoroutine(Direction(effect[i]));
			}
		}
		else if (rareBossType > 0)
		{
			tweenCtrl.gameObject.SetActive(false);
			if ((UnityEngine.Object)fieldEnemyBossTweenCtrl != (UnityEngine.Object)null)
			{
				fieldEnemyBossTweenCtrl.gameObject.SetActive(false);
			}
			if ((UnityEngine.Object)fieldEnemyRareTweenCtrl != (UnityEngine.Object)null)
			{
				fieldEnemyRareTweenCtrl.gameObject.SetActive(false);
			}
			if ((UnityEngine.Object)rareBossTweenCtrl != (UnityEngine.Object)null)
			{
				rareBossTweenCtrl.gameObject.SetActive(true);
				rareBossTweenCtrl.Reset();
				rareBossTweenCtrl.Play(true, null);
			}
			SoundManager.PlayOneshotJingle(40000163, null, null);
		}
		else
		{
			if ((UnityEngine.Object)rareBossTweenCtrl != (UnityEngine.Object)null)
			{
				rareBossTweenCtrl.gameObject.SetActive(false);
			}
			if ((UnityEngine.Object)fieldEnemyBossTweenCtrl != (UnityEngine.Object)null)
			{
				fieldEnemyBossTweenCtrl.gameObject.SetActive(false);
			}
			if ((UnityEngine.Object)fieldEnemyRareTweenCtrl != (UnityEngine.Object)null)
			{
				fieldEnemyRareTweenCtrl.gameObject.SetActive(false);
			}
			tweenCtrl.gameObject.SetActive(true);
			tweenCtrl.Reset();
			tweenCtrl.Play(true, null);
			SoundManager.PlayOneshotJingle(40000031, null, null);
			int j = 0;
			for (int num2 = effect.Length; j < num2; j++)
			{
				StartCoroutine(Direction(effect[j]));
			}
		}
		SoundManager.RequestBGM(12, true);
	}

	public void PlayRareFieldEnemy()
	{
		base.gameObject.SetActive(true);
		TweenAlpha.Begin(base.gameObject, 0f, 1f);
		if ((UnityEngine.Object)rareBossTweenCtrl != (UnityEngine.Object)null)
		{
			rareBossTweenCtrl.gameObject.SetActive(false);
		}
		if ((UnityEngine.Object)fieldEnemyBossTweenCtrl != (UnityEngine.Object)null)
		{
			fieldEnemyBossTweenCtrl.gameObject.SetActive(false);
		}
		if ((UnityEngine.Object)fieldFishingTweenCtrl != (UnityEngine.Object)null)
		{
			fieldFishingTweenCtrl.gameObject.SetActive(false);
		}
		if ((UnityEngine.Object)fieldFishingRareTweenCtrl != (UnityEngine.Object)null)
		{
			fieldFishingRareTweenCtrl.gameObject.SetActive(false);
		}
		if ((UnityEngine.Object)tweenCtrl != (UnityEngine.Object)null)
		{
			tweenCtrl.gameObject.SetActive(false);
		}
		fieldEnemyRareTweenCtrl.gameObject.SetActive(true);
		fieldEnemyRareTweenCtrl.Reset();
		fieldEnemyRareTweenCtrl.Play(true, null);
		SoundManager.PlayOneshotJingle(40000031, null, null);
		int i = 0;
		for (int num = effect.Length; i < num; i++)
		{
			StartCoroutine(Direction(effect[i]));
		}
	}

	public void PlayFieldFishingEnemy(bool isRare)
	{
		base.gameObject.SetActive(true);
		TweenAlpha.Begin(base.gameObject, 0f, 1f);
		if ((UnityEngine.Object)rareBossTweenCtrl != (UnityEngine.Object)null)
		{
			rareBossTweenCtrl.gameObject.SetActive(false);
		}
		if ((UnityEngine.Object)fieldEnemyBossTweenCtrl != (UnityEngine.Object)null)
		{
			fieldEnemyBossTweenCtrl.gameObject.SetActive(false);
		}
		if ((UnityEngine.Object)fieldEnemyRareTweenCtrl != (UnityEngine.Object)null)
		{
			fieldEnemyRareTweenCtrl.gameObject.SetActive(false);
		}
		if ((UnityEngine.Object)tweenCtrl != (UnityEngine.Object)null)
		{
			tweenCtrl.gameObject.SetActive(false);
		}
		if (isRare)
		{
			fieldFishingTweenCtrl.gameObject.SetActive(false);
			fieldFishingRareTweenCtrl.gameObject.SetActive(true);
			fieldFishingRareTweenCtrl.Reset();
			fieldFishingRareTweenCtrl.Play(true, null);
		}
		else
		{
			fieldFishingRareTweenCtrl.gameObject.SetActive(false);
			fieldFishingTweenCtrl.gameObject.SetActive(true);
			fieldFishingTweenCtrl.Reset();
			fieldFishingTweenCtrl.Play(true, null);
			int i = 0;
			for (int num = fishingEffect.Length; i < num; i++)
			{
				StartCoroutine(Direction(fishingEffect[i]));
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
		StartCoroutine(DoFadeOut(delay, duration, onComplete));
	}

	private IEnumerator DoFadeOut(float delay, float duration, Action onComplete)
	{
		yield return (object)new WaitForSeconds(delay);
		TweenAlpha.Begin(base.gameObject, duration, 0f);
		yield return (object)new WaitForSeconds(duration);
		onComplete?.Invoke();
		if ((UnityEngine.Object)base.gameObject != (UnityEngine.Object)null)
		{
			base.gameObject.SetActive(false);
		}
	}
}
