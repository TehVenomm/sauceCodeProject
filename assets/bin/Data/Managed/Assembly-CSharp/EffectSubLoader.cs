using System;
using System.Collections;
using System.Collections.Generic;

public class EffectSubLoader : MonoBehaviourSingleton<EffectSubLoader>
{
	private Queue<IEnumerator> actions = new Queue<IEnumerator>();

	private LoadingQueue loadQueue;

	public static void CreateInstance()
	{
		if (!MonoBehaviourSingleton<EffectSubLoader>.IsValid())
		{
			Utility.CreateGameObjectAndComponent("EffectSubLoader");
		}
	}

	public void CacheAnimDataUseResource(AnimEventData animEventData, LoadingQueue.EffectNameAnalyzer name_analyzer = null, List<AnimEventData.EventData> cntAtkDataList = null)
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		loadQueue.CacheAnimDataUseResource(animEventData, name_analyzer, cntAtkDataList);
	}

	public void CacheEffect(RESOURCE_CATEGORY category, string ename)
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		loadQueue.CacheEffect(category, ename);
	}

	public void CacheBulletDataUseResource(BulletData bulletData, Player player = null)
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		loadQueue.CacheBulletDataUseResource(bulletData, player);
	}

	public void CacheSE(int se_id, List<LoadObject> los = null)
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		loadQueue.CacheSE(se_id, los);
	}

	public void StartLoad(Action onFinish = null)
	{
		StartCoroutine(ProcessAction(onFinish));
	}

	private IEnumerator ProcessAction(Action onFinish)
	{
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		onFinish?.Invoke();
	}
}
