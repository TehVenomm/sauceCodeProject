using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelUpAnnounce : UIInGameSelfAnnounce
{
	private enum PARAM_POS
	{
		HP,
		ATK,
		DEF,
		END_POS
	}

	private const int WAIT_COUNT = 3;

	private const int LEVELUP_SE = 40000017;

	[SerializeField]
	private UILabel m_upStatusLevel;

	[SerializeField]
	private List<UILabel> m_upParamList;

	[SerializeField]
	private List<UILabel> m_upTitleList;

	private string m_lvupTextFormat = string.Empty;

	private string m_paramTextFormat = string.Empty;

	private string m_titleTextHp = string.Empty;

	private string m_titleTextAtk = string.Empty;

	private string m_titleTextDef = string.Empty;

	private bool m_isReceiveRequest = true;

	private int m_oldHp;

	private int m_oldAtk;

	private int m_oldDef;

	private int m_oldLevel;

	private Coroutine m_coroutine;

	private AudioClip m_AudioClip;

	public void GetNowStatus()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && m_coroutine == null)
		{
			m_oldAtk = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.atk;
			m_oldDef = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.def;
			m_oldHp = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hp;
			m_oldLevel = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
		}
	}

	public bool IsLevelUp()
	{
		return m_oldLevel < (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
	}

	public void Lock()
	{
		m_isReceiveRequest = false;
	}

	public void Unlock()
	{
		m_isReceiveRequest = true;
	}

	public bool IsLocked()
	{
		return !m_isReceiveRequest;
	}

	public bool IsWaitDelay()
	{
		if (m_coroutine != null)
		{
			return true;
		}
		return false;
	}

	public void PlayLevelUp()
	{
		SetNewParameter();
		PlayLevelUpInner(false, null);
	}

	public void PlayLevelUpForce(Action callback = null)
	{
		SetNewParameter();
		PlayLevelUpInner(true, callback);
	}

	public void SkipAnim()
	{
		Skip();
	}

	private void StoreAudioClip()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		string sE = ResourceName.GetSE(40000017);
		if (!string.IsNullOrEmpty(sE))
		{
			ResourceLink component = this.get_gameObject().GetComponent<ResourceLink>();
			if (!(component == null))
			{
				m_AudioClip = component.Get<AudioClip>(sE);
			}
		}
	}

	private void PlayAudioLevelUp()
	{
		if (m_AudioClip != null)
		{
			SoundManager.PlayOneshotJingle(m_AudioClip, 40000017, null, null);
		}
	}

	private void Start()
	{
		StoreAudioClip();
	}

	private void SetNewParameter()
	{
		int num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
		int num2 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hp;
		int num3 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.atk;
		int num4 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.def;
		if (string.IsNullOrEmpty(m_lvupTextFormat))
		{
			m_lvupTextFormat = m_upStatusLevel.text;
			m_paramTextFormat = m_upParamList[0].text;
			m_titleTextHp = m_upTitleList[0].text;
			m_titleTextAtk = m_upTitleList[1].text;
			m_titleTextDef = m_upTitleList[2].text;
		}
		int i = 0;
		m_upStatusLevel.text = string.Format(m_lvupTextFormat, m_oldLevel, num);
		if (num2 - m_oldHp > 0)
		{
			m_upParamList[i].text = string.Format(m_paramTextFormat, num2 - m_oldHp);
			m_upTitleList[i].text = m_titleTextHp;
			i++;
		}
		if (num3 - m_oldAtk > 0)
		{
			m_upParamList[i].text = string.Format(m_paramTextFormat, num3 - m_oldAtk);
			m_upTitleList[i].text = m_titleTextAtk;
			i++;
		}
		if (num4 - m_oldDef > 0)
		{
			m_upParamList[i].text = string.Format(m_paramTextFormat, num4 - m_oldDef);
			m_upTitleList[i].text = m_titleTextDef;
			i++;
		}
		for (; i < 3; i++)
		{
			m_upParamList[i].text = string.Empty;
			m_upTitleList[i].text = string.Empty;
		}
	}

	private bool IsPlay()
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
		if (!m_isReceiveRequest)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
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

	private void PlayLevelUpInner(bool forcePlay = false, Action callback = null)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		if (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.IsRush())
		{
			if (!forcePlay && !IsPlay())
			{
				if (m_coroutine == null)
				{
					m_coroutine = this.StartCoroutine("DelayPlay");
				}
			}
			else
			{
				if (forcePlay && m_coroutine != null)
				{
					this.StopCoroutine(m_coroutine);
					m_coroutine = null;
				}
				Play(callback);
				PlayAudioLevelUp();
				GetNowStatus();
			}
		}
	}

	private IEnumerator DelayPlay()
	{
		int waitCount = 0;
		while (!IsPlay() || waitCount < 3)
		{
			waitCount = (IsPlay() ? (waitCount + 1) : 0);
			yield return (object)null;
		}
		Play(null);
		PlayAudioLevelUp();
		m_coroutine = null;
		GetNowStatus();
	}
}
