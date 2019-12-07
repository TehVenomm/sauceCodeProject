using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultDirection : GameSection
{
	private QuestResultDirector director;

	private PlayerLoader[] players;

	private int winnder_voice_id;

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(active: true);
		yield return new WaitForEndOfFrame();
		yield return MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(need_gc_collect: true);
		yield return new WaitForEndOfFrame();
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && MonoBehaviourSingleton<InGameRecorder>.I.players.Count > 0)
		{
			LoadingQueue load_queue = new LoadingQueue(this);
			LoadObject lo = load_queue.Load(RESOURCE_CATEGORY.UI, "QuestResultDirector");
			List<InGameRecorder.PlayerRecord> list = MonoBehaviourSingleton<InGameRecorder>.I.players;
			int num = 0;
			while (num < list.Count)
			{
				InGameRecorder.PlayerRecord playerRecord = list[num];
				if (playerRecord == null || playerRecord.playerLoadInfo == null)
				{
					list.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
			bool waitLoad = true;
			MonoBehaviourSingleton<InGameRecorder>.I.CreatePlayerModelsAsync(delegate(PlayerLoader[] loaders)
			{
				players = loaders;
				waitLoad = false;
			});
			while (waitLoad)
			{
				yield return null;
			}
			winnder_voice_id = 0;
			if (players != null)
			{
				winnder_voice_id = players[0].GetVoiceId(ACTION_VOICE_ID.HAPPY_01);
				load_queue.CacheActionVoice(winnder_voice_id);
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			int i = 0;
			for (int num2 = players.Length; i < num2; i++)
			{
				players[i].animator.applyRootMotion = false;
			}
			director = ResourceUtility.Realizes(lo.loadedObject, MonoBehaviourSingleton<StageManager>.I._transform).GetComponent<QuestResultDirector>();
			director.players = players;
		}
		if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			MonoBehaviourSingleton<SceneSettingsManager>.I.DisableWaveTarget();
		}
		GC.Collect();
		yield return new WaitForEndOfFrame();
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(active: false);
		if (QuestManager.IsValidTrial() && MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
		base.Initialize();
	}

	private void LateUpdate()
	{
		if (director != null && director.enabled && director.targetAnim != null && !director.targetAnim.isPlaying)
		{
			OnDirectionFinished();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (director != null)
		{
			UnityEngine.Object.Destroy(director.gameObject);
		}
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			MonoBehaviourSingleton<InGameRecorder>.I.DeletePlayerModels();
		}
	}

	public override void StartSection()
	{
		if (!MonoBehaviourSingleton<InGameRecorder>.IsValid() || MonoBehaviourSingleton<InGameRecorder>.I.players.Count == 0)
		{
			OnDirectionFinished();
		}
	}

	private void OnDirectionFinished()
	{
		if (winnder_voice_id > 0)
		{
			SoundManager.PlayActionVoice(winnder_voice_id);
		}
		director.enabled = false;
		if (QuestManager.IsValidTrial())
		{
			DispatchEvent("NEXT_TRIAL");
		}
		else
		{
			DispatchEvent("NEXT");
		}
	}

	private void OnQuery_NEXT()
	{
		if (director.enabled)
		{
			director.Skip();
			GameSection.StopEvent();
		}
	}

	private void OnQuery_NEXT_TRIAL()
	{
		if (director.enabled)
		{
			director.Skip();
			GameSection.StopEvent();
		}
	}

	public override void UpdateUI()
	{
	}
}
