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
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && MonoBehaviourSingleton<InGameRecorder>.I.players.Count > 0)
		{
			LoadingQueue load_queue = new LoadingQueue(this);
			LoadObject lo = load_queue.Load(RESOURCE_CATEGORY.UI, "QuestResultDirector", false);
			List<InGameRecorder.PlayerRecord> playerRecords = MonoBehaviourSingleton<InGameRecorder>.I.players;
			int m = 0;
			while (m < playerRecords.Count)
			{
				InGameRecorder.PlayerRecord p = playerRecords[m];
				if (p == null || p.playerLoadInfo == null)
				{
					playerRecords.RemoveAt(m);
				}
				else
				{
					m++;
				}
			}
			players = MonoBehaviourSingleton<InGameRecorder>.I.CreatePlayerModels();
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
			while (true)
			{
				bool wait = false;
				int l = 0;
				for (int k = players.Length; l < k; l++)
				{
					if (players[l].isLoading)
					{
						wait = true;
						break;
					}
				}
				if (!wait)
				{
					break;
				}
				yield return (object)null;
			}
			int j = 0;
			for (int i = players.Length; j < i; j++)
			{
				players[j].animator.set_applyRootMotion(false);
			}
			director = ResourceUtility.Realizes(lo.loadedObject, MonoBehaviourSingleton<StageManager>.I._transform, -1).GetComponent<QuestResultDirector>();
			director.players = players;
		}
		base.Initialize();
	}

	private void LateUpdate()
	{
		if (director != null && director.get_enabled() && !director.cameraAnim.get_isPlaying())
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
		DispatchEvent("NEXT", null);
	}

	private void OnQuery_NEXT()
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
