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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
		yield return (object)new WaitForEndOfFrame();
		yield return (object)MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(true);
		yield return (object)new WaitForEndOfFrame();
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && MonoBehaviourSingleton<InGameRecorder>.I.players.Count > 0)
		{
			LoadingQueue load_queue = new LoadingQueue(this);
			LoadObject lo = load_queue.Load(RESOURCE_CATEGORY.UI, "QuestResultDirector", false);
			List<InGameRecorder.PlayerRecord> playerRecords = MonoBehaviourSingleton<InGameRecorder>.I.players;
			int k = 0;
			while (k < playerRecords.Count)
			{
				InGameRecorder.PlayerRecord p = playerRecords[k];
				if (p == null || p.playerLoadInfo == null)
				{
					playerRecords.RemoveAt(k);
				}
				else
				{
					k++;
				}
			}
			bool waitLoad = true;
			MonoBehaviourSingleton<InGameRecorder>.I.CreatePlayerModelsAsync(delegate(PlayerLoader[] loaders)
			{
				((_003CDoInitialize_003Ec__Iterator13D)/*Error near IL_016d: stateMachine*/)._003C_003Ef__this.players = loaders;
				((_003CDoInitialize_003Ec__Iterator13D)/*Error near IL_016d: stateMachine*/)._003CwaitLoad_003E__5 = false;
			});
			while (waitLoad)
			{
				yield return (object)null;
			}
			winnder_voice_id = 0;
			if (players != null)
			{
				winnder_voice_id = players[0].GetVoiceId(ACTION_VOICE_ID.HAPPY_01);
				load_queue.CacheActionVoice(winnder_voice_id, null);
			}
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
			int j = 0;
			for (int i = players.Length; j < i; j++)
			{
				players[j].animator.set_applyRootMotion(false);
			}
			director = ResourceUtility.Realizes(lo.loadedObject, MonoBehaviourSingleton<StageManager>.I._transform, -1).GetComponent<QuestResultDirector>();
			director.players = players;
		}
		if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			MonoBehaviourSingleton<SceneSettingsManager>.I.DisableWaveTarget();
		}
		GC.Collect();
		yield return (object)new WaitForEndOfFrame();
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
		if (QuestManager.IsValidTrial() && MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
		base.Initialize();
	}

	private void LateUpdate()
	{
		if (director != null && director.get_enabled() && director.targetAnim != null && !director.targetAnim.get_isPlaying())
		{
			OnDirectionFinished();
		}
	}

	protected override void OnDestroy()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		base.OnDestroy();
		if (director != null)
		{
			Object.Destroy(director.get_gameObject());
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
			SoundManager.PlayActionVoice(winnder_voice_id, 1f, 0u, null, null);
		}
		director.set_enabled(false);
		if (QuestManager.IsValidTrial())
		{
			DispatchEvent("NEXT_TRIAL", null);
		}
		else
		{
			DispatchEvent("NEXT", null);
		}
	}

	private void OnQuery_NEXT()
	{
		if (director.get_enabled())
		{
			director.Skip();
			GameSection.StopEvent();
		}
	}

	private void OnQuery_NEXT_TRIAL()
	{
		if (director.get_enabled())
		{
			director.Skip();
			GameSection.StopEvent();
		}
	}

	public override void UpdateUI()
	{
	}
}
