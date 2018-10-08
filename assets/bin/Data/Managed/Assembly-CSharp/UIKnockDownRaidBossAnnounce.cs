using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKnockDownRaidBossAnnounce : UIInGameSelfAnnounce
{
	private const int COUNT_WAIT = 3;

	private const int SE_KNOCK_DOWN = 40000067;

	private Coroutine m_coroutine;

	private AudioClip m_audioClip;

	private List<EventItemCounts> m_eventItemCountList = new List<EventItemCounts>();

	private string m_raidBossHp_Str = string.Empty;

	private bool isPlaying;

	private void Start()
	{
		StoreAudioClip();
	}

	private void StoreAudioClip()
	{
		string sE = ResourceName.GetSE(40000067);
		if (!string.IsNullOrEmpty(sE))
		{
			ResourceLink component = base.gameObject.GetComponent<ResourceLink>();
			if (!((UnityEngine.Object)component == (UnityEngine.Object)null))
			{
				m_audioClip = component.Get<AudioClip>(sE);
			}
		}
	}

	private void PlayAudioKnockDown()
	{
		if (!((UnityEngine.Object)m_audioClip == (UnityEngine.Object)null))
		{
			SoundManager.PlayOneshotJingle(m_audioClip, 40000067, null, null);
		}
	}

	private bool IsAbleToPlay()
	{
		if (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent())
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isQuestHappen)
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene)
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery() != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.GetEventCleardDeliveryData() != null)
		{
			return false;
		}
		if (MonoBehaviourSingleton<UIManager>.I.levelUp.IsWaitDelay())
		{
			return false;
		}
		if (MonoBehaviourSingleton<UIManager>.I.levelUp.IsPlaying())
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop")
		{
			return true;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "LoungeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop")
		{
			return true;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "InGameMain")
		{
			return true;
		}
		return false;
	}

	public void SetEventItemCountList(List<EventItemCounts> eventItemCounts)
	{
		m_eventItemCountList = eventItemCounts;
	}

	public void SetRaidBossHp(string raidBossHp_Str)
	{
		m_raidBossHp_Str = raidBossHp_Str;
	}

	public bool IsKnockDownRaidBossByEventItemCountList()
	{
		if (m_eventItemCountList.IsNullOrEmpty())
		{
			return false;
		}
		int i = 0;
		for (int count = m_eventItemCountList.Count; i < count; i++)
		{
			if (m_eventItemCountList[i].eventType == 28)
			{
				long result = 0L;
				long result2 = 0L;
				if (long.TryParse(m_eventItemCountList[i].maxCount, out result) && long.TryParse(m_eventItemCountList[i].count, out result2) && result2 >= result)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool IsKnockDownRaidBossByRaidBossHp()
	{
		if (string.IsNullOrEmpty(m_raidBossHp_Str))
		{
			return false;
		}
		long result = 0L;
		if (!long.TryParse(m_raidBossHp_Str, out result))
		{
			return false;
		}
		if (result <= 0)
		{
			return true;
		}
		return false;
	}

	private void ClearKnockDownData()
	{
		m_eventItemCountList.Clear();
		m_raidBossHp_Str = string.Empty;
	}

	public void PlayKnockDown(bool isForcePlay = false, Action callback = null)
	{
		if (TutorialStep.HasQuestSpecialUnlocked() && PlayerPrefs.GetInt("IS_SHOWED_RAID_BOSS_DIRECTION", 0) == 0 && (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.IsRush()) && !QuestManager.IsValidInGame())
		{
			if (!isForcePlay && !IsAbleToPlay())
			{
				if (m_coroutine == null)
				{
					m_coroutine = StartCoroutine(DelayPlay());
				}
			}
			else
			{
				if (isForcePlay && m_coroutine != null)
				{
					StopCoroutine(m_coroutine);
					m_coroutine = null;
				}
				Play(callback);
				PlayAudioKnockDown();
				PlayerPrefs.SetInt("IS_SHOWED_RAID_BOSS_DIRECTION", 1);
				ClearKnockDownData();
			}
		}
	}

	private IEnumerator DelayPlay()
	{
		int waitCount = 0;
		while (!IsAbleToPlay() || waitCount < 3)
		{
			waitCount = (IsAbleToPlay() ? (waitCount + 1) : 0);
			yield return (object)null;
		}
		Play(null);
		PlayAudioKnockDown();
		PlayerPrefs.SetInt("IS_SHOWED_RAID_BOSS_DIRECTION", 1);
		m_coroutine = null;
		ClearKnockDownData();
	}

	public void ClearAnnounce()
	{
		if (m_coroutine != null)
		{
			StopCoroutine(m_coroutine);
			m_coroutine = null;
		}
	}
}
