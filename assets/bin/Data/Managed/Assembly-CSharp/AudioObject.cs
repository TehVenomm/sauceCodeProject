using UnityEngine;
using UnityEngine.Audio;

public class AudioObject : DisableNotifyMonoBehaviour
{
	public enum Phase
	{
		NONE,
		PREPLAY,
		PLAYING,
		PRESTOP,
		STOP
	}

	private const int MIN_FADEOUT_FRAMECOUNT = 4;

	public Transform parentObject;

	private AudioSource audioSource;

	private bool needParent;

	private float fadeoutVolume;

	private AudioControlGroup m_masterGroup;

	private bool m_IsSpatialSound;

	private bool m_IsStaticPosition;

	public Phase PlayPhase
	{
		get;
		protected set;
	}

	public bool IsPlayingSound
	{
		get
		{
			if (PlayPhase == Phase.PLAYING || PlayPhase == Phase.PREPLAY)
			{
				return true;
			}
			return false;
		}
	}

	public int ID
	{
		get;
		private set;
	}

	public int clipId
	{
		get;
		private set;
	}

	public float timeAtPlay
	{
		get;
		private set;
	}

	public static AudioObject Create(AudioClip clip, int clip_id, float volume, bool loop, AudioMixerGroup mixer_group, AudioControlGroup controlGroup, bool is3DSound = false, DisableNotifyMonoBehaviour master = null, Transform parent = null, Vector3? initPos = default(Vector3?))
	{
		if ((Object)clip == (Object)null)
		{
			return null;
		}
		AudioObject audioObject = AudioObjectPool.Borrow();
		if (!((Object)audioObject == (Object)null))
		{
			goto IL_0020;
		}
		goto IL_0020;
		IL_0020:
		audioObject._transform.parent = MonoBehaviourSingleton<SoundManager>.I._transform;
		audioObject.m_masterGroup = controlGroup;
		audioObject.m_IsSpatialSound = is3DSound;
		if (initPos.HasValue)
		{
			audioObject._transform.position = ((!initPos.HasValue) ? Vector3.zero : initPos.Value);
			audioObject.m_IsStaticPosition = true;
		}
		audioObject.Play(clip, clip_id, volume, loop, mixer_group, master, parent);
		return audioObject;
	}

	public static void Init(AudioObject obj, AudioSource source, int managed_id = -1)
	{
		obj.InitParams();
		obj.audioSource = source;
		obj.ID = managed_id;
	}

	private void InitParams()
	{
		parentObject = null;
		clipId = 0;
		timeAtPlay = 0f;
		PlayPhase = Phase.NONE;
		fadeoutVolume = 0f;
		needParent = false;
		m_IsSpatialSound = false;
		m_IsStaticPosition = false;
	}

	private void InitAudioSource()
	{
		if (!((Object)audioSource == (Object)null))
		{
			audioSource.outputAudioMixerGroup = null;
			audioSource.spatialBlend = 0f;
			audioSource.spread = 0f;
			audioSource.priority = 128;
			audioSource.rolloffMode = AudioRolloffMode.Linear;
			audioSource.minDistance = 0f;
			audioSource.maxDistance = 999f;
			audioSource.pitch = 1f;
			audioSource.dopplerLevel = 0f;
			audioSource.clip = null;
			audioSource.loop = false;
			audioSource.volume = 1f;
		}
	}

	private void Play(AudioClip clip, int clip_id, float volume, bool loop, AudioMixerGroup mixer_group, DisableNotifyMonoBehaviour master, Transform parent)
	{
		if ((Object)master != (Object)null)
		{
			SetNotifyMaster(master);
		}
		else
		{
			ResetNotifyMaster();
		}
		clipId = clip_id;
		PlayPhase = Phase.PREPLAY;
		if ((Object)audioSource != (Object)null)
		{
			audioSource.outputAudioMixerGroup = mixer_group;
			if (m_IsSpatialSound)
			{
				audioSource.spatialBlend = 1f;
				audioSource.spread = 360f;
			}
			else
			{
				audioSource.spatialBlend = 0f;
				audioSource.spread = 0f;
			}
			audioSource.priority = 100;
			audioSource.rolloffMode = MonoBehaviourSingleton<SoundManager>.I.CurrentPreset.rollOffMode;
			audioSource.minDistance = MonoBehaviourSingleton<SoundManager>.I.CurrentPreset.minDistance;
			audioSource.maxDistance = MonoBehaviourSingleton<SoundManager>.I.CurrentPreset.maxDistance;
			audioSource.pitch = 1f;
		}
		float num = 1f;
		float dopplerLevel = 0f;
		SETable.Data seData = Singleton<SETable>.I.GetSeData((uint)clip_id);
		if (seData != null)
		{
			audioSource.priority = (int)seData.priority;
			num = seData.volumeScale;
			dopplerLevel = seData.dopplerLevel;
			if (seData.minDistance > 0f)
			{
				audioSource.minDistance = seData.minDistance;
			}
			if (seData.maxDistance > 0f)
			{
				audioSource.maxDistance = seData.maxDistance;
			}
			if (seData.randomPitch > 0f)
			{
				audioSource.pitch = GenRandomPitch();
			}
		}
		audioSource.dopplerLevel = dopplerLevel;
		audioSource.clip = clip;
		audioSource.loop = loop;
		audioSource.volume = volume * num;
		parentObject = parent;
		needParent = ((Object)parent != (Object)null);
		fadeoutVolume = 0f;
		TraceParent();
		audioSource.Play();
		if ((Object)m_masterGroup != (Object)null)
		{
			m_masterGroup.NotifyOnStart(this);
		}
		PlayPhase = Phase.PLAYING;
		timeAtPlay = Time.time;
	}

	private float GenRandomPitch()
	{
		return Utility.Random(0.6f) + 0.7f;
	}

	protected override void OnDisableMaster()
	{
		if (!((Object)audioSource == (Object)null))
		{
			if (audioSource.loop)
			{
				Stop(0);
			}
			parentObject = null;
			needParent = false;
		}
	}

	private void LateUpdate()
	{
		if (needParent)
		{
			if ((Object)parentObject != (Object)null)
			{
				TraceParent();
			}
			else if (audioSource.loop)
			{
				Stop(0);
				needParent = false;
			}
		}
		if (fadeoutVolume > 0f)
		{
			audioSource.volume = Mathf.Max(audioSource.volume - fadeoutVolume, 0f);
			if (audioSource.volume == 0f)
			{
				StopImmidiate();
			}
		}
		if (!audioSource.isPlaying)
		{
			if ((Object)m_masterGroup != (Object)null)
			{
				m_masterGroup.NotifyOnStop(this);
				m_masterGroup = null;
			}
			Dispose();
		}
	}

	private void TraceParent()
	{
		if (!m_IsStaticPosition && needParent && (Object)parentObject != (Object)null)
		{
			base._transform.position = parentObject.position;
		}
	}

	public void Stop(int fadeout_framecount = 0)
	{
		if (!((Object)audioSource == (Object)null) && !(fadeoutVolume > 0f))
		{
			if (fadeout_framecount < 4)
			{
				fadeout_framecount = 4;
			}
			fadeoutVolume = audioSource.volume / (float)fadeout_framecount;
			if ((Object)m_masterGroup != (Object)null)
			{
				m_masterGroup.NotifyOnRelease(this);
				m_masterGroup = null;
			}
			PlayPhase = Phase.PRESTOP;
		}
	}

	public void SetLoopFlag(bool flag)
	{
		audioSource.loop = flag;
	}

	public bool GetLoopFlag()
	{
		return audioSource.loop;
	}

	private void StopImmidiate()
	{
		if (!((Object)audioSource == (Object)null) && PlayPhase != 0 && PlayPhase != Phase.STOP)
		{
			audioSource.Stop();
			if ((Object)m_masterGroup != (Object)null)
			{
				m_masterGroup.NotifyOnStop(this);
				m_masterGroup = null;
			}
			PlayPhase = Phase.STOP;
		}
	}

	private void Dispose()
	{
		InitParams();
		if ((Object)audioSource != (Object)null)
		{
			audioSource.Stop();
			InitAudioSource();
		}
		if ((Object)m_masterGroup != (Object)null)
		{
			m_masterGroup.NotifyOnStop(this);
			m_masterGroup = null;
		}
		AudioObjectPool.Release(this);
	}
}
