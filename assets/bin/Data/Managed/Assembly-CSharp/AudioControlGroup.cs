using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControlGroup : DisableNotifyMonoBehaviour
{
	public enum CullingTypes
	{
		NONE,
		REJECT,
		OVERWRITE,
		PRIORITY,
		TYPE_MAX
	}

	private struct ClipPriorityInfo
	{
		private uint clipId;

		private uint priority;
	}

	private bool m_bUnique;

	private AudioObject m_lastAudio;

	private Dictionary<int, PlayingAudioList> m_dicPlayingAudio;

	private List<ClipPriorityInfo> m_ClipIdsSortByPriority;

	public CullingTypes CullingType
	{
		get;
		private set;
	}

	public int PlayingLimitNum
	{
		get;
		private set;
	}

	public int PlayingCount
	{
		get;
		private set;
	}

	public static AudioControlGroup Create(CullingTypes type = CullingTypes.NONE, int LimitNum = int.MaxValue)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Expected O, but got Unknown
		GameObject val = new GameObject("AudioControlGroup");
		AudioControlGroup audioControlGroup = val.AddComponent<AudioControlGroup>();
		audioControlGroup._transform.set_parent(MonoBehaviourSingleton<SoundManager>.I._transform);
		audioControlGroup.Setup(type, LimitNum);
		return audioControlGroup;
	}

	private void Setup(CullingTypes type, int LimitNum)
	{
		if (m_dicPlayingAudio == null)
		{
			m_dicPlayingAudio = new Dictionary<int, PlayingAudioList>();
		}
		else
		{
			m_dicPlayingAudio.Clear();
		}
		CullingType = type;
		PlayingLimitNum = LimitNum;
		PlayingCount = 0;
		if (type == CullingTypes.OVERWRITE && LimitNum == 1)
		{
			m_bUnique = true;
		}
	}

	protected override void OnDisableMaster()
	{
	}

	private void AddPlayingList(int clip_id)
	{
		SETable.Data seData = Singleton<SETable>.I.GetSeData((uint)clip_id);
		if (seData != null)
		{
			PlayingAudioList playingAudioList = new PlayingAudioList();
			playingAudioList.Setup(clip_id, seData.priority, seData.limitNum, seData.intervalLimit, seData.CullingType);
			m_dicPlayingAudio.Add(clip_id, playingAudioList);
		}
	}

	private void PreparePlayingList(int clip_id)
	{
		if (!m_dicPlayingAudio.ContainsKey(clip_id))
		{
			AddPlayingList(clip_id);
		}
	}

	private void PrepareKeyOn(int clip_id)
	{
		if (m_bUnique && m_lastAudio != null)
		{
			m_lastAudio.Stop(0);
			m_lastAudio = null;
		}
		if (m_dicPlayingAudio.ContainsKey(clip_id))
		{
			m_dicPlayingAudio[clip_id].OpanPlaySlot(1, false);
		}
	}

	private bool CanPlay(int clip_id)
	{
		if (CullingType == CullingTypes.REJECT && PlayingCount >= PlayingLimitNum)
		{
			return false;
		}
		if (!m_dicPlayingAudio.ContainsKey(clip_id))
		{
			return true;
		}
		return m_dicPlayingAudio[clip_id].CanPlay();
	}

	private bool NeedControl(int clip_id)
	{
		if (m_dicPlayingAudio == null)
		{
			return false;
		}
		return m_dicPlayingAudio.ContainsKey(clip_id);
	}

	public AudioObject CreateAudio(AudioClip clip, int clip_id, float volume, bool loop, AudioMixerGroup mixer_group, bool is3DSound = false, DisableNotifyMonoBehaviour master = null, Transform _parent = null, Vector3? initPos = default(Vector3?))
	{
		if (clip == null)
		{
			return null;
		}
		PreparePlayingList(clip_id);
		if (!CanPlay(clip_id))
		{
			return null;
		}
		PrepareKeyOn(clip_id);
		Transform parent = (!(_parent == null)) ? ((object)_parent) : ((object)base._transform);
		return m_lastAudio = AudioObject.Create(clip, clip_id, volume, loop, mixer_group, this, is3DSound, master, parent, initPos);
	}

	public void NotifyOnStart(AudioObject startAudio)
	{
		if (!(startAudio == null))
		{
			JoinAudio(startAudio);
		}
	}

	public void NotifyOnRelease(AudioObject releaseAudio)
	{
		if (!(releaseAudio == null))
		{
			LeaveAudio(releaseAudio);
		}
	}

	public void NotifyOnStop(AudioObject stoppedAudio)
	{
		if (!(stoppedAudio == null))
		{
			LeaveAudio(stoppedAudio);
		}
	}

	private void JoinAudio(AudioObject ao)
	{
		if (m_dicPlayingAudio.ContainsKey(ao.clipId))
		{
			m_dicPlayingAudio[ao.clipId].Add(ao);
		}
	}

	private void LeaveAudio(AudioObject ao)
	{
		if (m_lastAudio != null && m_lastAudio == ao)
		{
			m_lastAudio = null;
		}
		if (m_dicPlayingAudio.ContainsKey(ao.clipId))
		{
			m_dicPlayingAudio[ao.clipId].Remove(ao);
		}
	}

	public void StopAll(int fadeout_frames = 0)
	{
		if (m_lastAudio != null)
		{
			m_lastAudio.Stop(fadeout_frames);
			m_lastAudio = null;
		}
		Dictionary<int, PlayingAudioList>.ValueCollection values = m_dicPlayingAudio.Values;
		foreach (PlayingAudioList item in values)
		{
			item.StopAll(fadeout_frames);
		}
	}
}
