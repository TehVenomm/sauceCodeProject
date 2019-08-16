using System.Collections.Generic;
using UnityEngine;

public class PlayingAudioList
{
	private const float DEFAULT_INTERVAL = 0.002f;

	public const int HIGHEST_PRIORITY = 0;

	public const int LOWEST_PRIORITY = 255;

	private List<AudioObject> m_AudioLists;

	private const int LIST_DEFAULT_SIZE = 32;

	public int ClipId
	{
		get;
		private set;
	}

	public uint Priority
	{
		get;
		private set;
	}

	public float IntervalTime
	{
		get;
		private set;
	}

	public int LimitNum
	{
		get;
		private set;
	}

	public int PlayingCount
	{
		get;
		private set;
	}

	public AudioControlGroup.CullingTypes CullingType
	{
		get;
		private set;
	}

	public PlayingAudioList()
	{
		ClipId = -1;
		Priority = 255u;
		IntervalTime = 0.002f;
		LimitNum = int.MaxValue;
		PlayingCount = 0;
		CullingType = AudioControlGroup.CullingTypes.NONE;
	}

	public PlayingAudioList(int clipId, uint priority, int limitNum, float intervalTime, AudioControlGroup.CullingTypes type)
	{
		Setup(clipId, priority, limitNum, intervalTime, type);
	}

	public void Setup(int clipId, uint priority, int limitNum, float intervalTime, AudioControlGroup.CullingTypes type)
	{
		ClipId = clipId;
		Priority = priority;
		IntervalTime = intervalTime;
		LimitNum = limitNum;
		PlayingCount = 0;
		CullingType = type;
		if (CullingType != 0)
		{
			if (m_AudioLists == null)
			{
				int limitNum2 = LimitNum;
				m_AudioLists = new List<AudioObject>(limitNum2);
			}
			else
			{
				m_AudioLists.Clear();
			}
		}
	}

	private void DoCullingAudio(int needCullingNum)
	{
		if (needCullingNum < 1 || m_AudioLists == null)
		{
			return;
		}
		int num = 0;
		while (num < m_AudioLists.Count && needCullingNum >= 1)
		{
			if (m_AudioLists[num] != null)
			{
				m_AudioLists[num].Stop();
			}
			needCullingNum--;
		}
	}

	public void StopAll(int fadeout_framecount = 0)
	{
		if (m_AudioLists == null)
		{
			return;
		}
		int num = 0;
		while (num < m_AudioLists.Count)
		{
			if (m_AudioLists[num] != null)
			{
				m_AudioLists[num].Stop(fadeout_framecount);
			}
		}
	}

	public void OpanPlaySlot(int slotCount, bool force = false)
	{
		if (!force && CullingType != AudioControlGroup.CullingTypes.OVERWRITE)
		{
			return;
		}
		int num = 0;
		if (LimitNum > 1)
		{
			num = PlayingCount + slotCount - LimitNum;
			if (num < 0)
			{
				num = 0;
			}
		}
		DoCullingAudio(num);
	}

	public bool CanPlay()
	{
		if (PlayingCount == 0)
		{
			return true;
		}
		if (m_AudioLists == null)
		{
			return true;
		}
		if (m_AudioLists.Count < 1)
		{
			return true;
		}
		int index = m_AudioLists.Count - 1;
		AudioObject audioObject = m_AudioLists[index];
		float time = Time.get_time();
		if (audioObject != null && time - audioObject.timeAtPlay < IntervalTime)
		{
			return false;
		}
		if (CullingType == AudioControlGroup.CullingTypes.OVERWRITE)
		{
			return true;
		}
		if (CullingType == AudioControlGroup.CullingTypes.REJECT)
		{
			return (PlayingCount < LimitNum) ? true : false;
		}
		return true;
	}

	public void Add(AudioObject ao)
	{
		if (m_AudioLists != null)
		{
			m_AudioLists.Add(ao);
			PlayingCount++;
			if (LimitNum < PlayingCount)
			{
				DoCullingAudio(LimitNum - PlayingCount);
			}
		}
	}

	public void Remove(AudioObject ao)
	{
		if (m_AudioLists != null)
		{
			m_AudioLists.Remove(ao);
			PlayingCount--;
			if (PlayingCount < 0)
			{
				PlayingCount = 0;
			}
		}
	}
}
