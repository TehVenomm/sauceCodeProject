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

	public Transform parentObject;

	private AudioSource audioSource;

	private bool needParent;

	private float fadeoutVolume;

	private const int MIN_FADEOUT_FRAMECOUNT = 4;

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
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if (clip == null)
		{
			return null;
		}
		AudioObject audioObject = AudioObjectPool.Borrow();
		if (audioObject == null)
		{
		}
		audioObject._transform.set_parent(MonoBehaviourSingleton<SoundManager>.I._transform);
		audioObject.m_masterGroup = controlGroup;
		audioObject.m_IsSpatialSound = is3DSound;
		if (initPos.HasValue)
		{
			audioObject._transform.set_position((!initPos.HasValue) ? Vector3.get_zero() : initPos.Value);
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
		if (!(audioSource == null))
		{
			audioSource.set_outputAudioMixerGroup(null);
			audioSource.set_spatialBlend(0f);
			audioSource.set_spread(0f);
			audioSource.set_priority(128);
			audioSource.set_rolloffMode(1);
			audioSource.set_minDistance(0f);
			audioSource.set_maxDistance(999f);
			audioSource.set_pitch(1f);
			audioSource.set_dopplerLevel(0f);
			audioSource.set_clip(null);
			audioSource.set_loop(false);
			audioSource.set_volume(1f);
		}
	}

	private void Play(AudioClip clip, int clip_id, float volume, bool loop, AudioMixerGroup mixer_group, DisableNotifyMonoBehaviour master, Transform parent)
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if (master != null)
		{
			SetNotifyMaster(master);
		}
		else
		{
			ResetNotifyMaster();
		}
		clipId = clip_id;
		PlayPhase = Phase.PREPLAY;
		if (audioSource != null)
		{
			audioSource.set_outputAudioMixerGroup(mixer_group);
			if (m_IsSpatialSound)
			{
				audioSource.set_spatialBlend(1f);
				audioSource.set_spread(360f);
			}
			else
			{
				audioSource.set_spatialBlend(0f);
				audioSource.set_spread(0f);
			}
			audioSource.set_priority(100);
			audioSource.set_rolloffMode(MonoBehaviourSingleton<SoundManager>.I.CurrentPreset.rollOffMode);
			audioSource.set_minDistance(MonoBehaviourSingleton<SoundManager>.I.CurrentPreset.minDistance);
			audioSource.set_maxDistance(MonoBehaviourSingleton<SoundManager>.I.CurrentPreset.maxDistance);
			audioSource.set_pitch(1f);
		}
		float num = 1f;
		float dopplerLevel = 0f;
		SETable.Data seData = Singleton<SETable>.I.GetSeData((uint)clip_id);
		if (seData != null)
		{
			audioSource.set_priority((int)seData.priority);
			num = seData.volumeScale;
			dopplerLevel = seData.dopplerLevel;
			if (seData.minDistance > 0f)
			{
				audioSource.set_minDistance(seData.minDistance);
			}
			if (seData.maxDistance > 0f)
			{
				audioSource.set_maxDistance(seData.maxDistance);
			}
			if (seData.randomPitch > 0f)
			{
				audioSource.set_pitch(GenRandomPitch());
			}
		}
		audioSource.set_dopplerLevel(dopplerLevel);
		audioSource.set_clip(clip);
		audioSource.set_loop(loop);
		audioSource.set_volume(volume * num);
		parentObject = parent;
		needParent = (parent != null);
		fadeoutVolume = 0f;
		TraceParent();
		audioSource.Play();
		if (m_masterGroup != null)
		{
			m_masterGroup.NotifyOnStart(this);
		}
		PlayPhase = Phase.PLAYING;
		timeAtPlay = Time.get_time();
	}

	private float GenRandomPitch()
	{
		return Utility.Random(0.6f) + 0.7f;
	}

	protected override void OnDisableMaster()
	{
		if (!(audioSource == null))
		{
			if (audioSource.get_loop())
			{
				Stop();
			}
			parentObject = null;
			needParent = false;
		}
	}

	private void LateUpdate()
	{
		if (needParent)
		{
			if (parentObject != null)
			{
				TraceParent();
			}
			else if (audioSource.get_loop())
			{
				Stop();
				needParent = false;
			}
		}
		if (fadeoutVolume > 0f)
		{
			audioSource.set_volume(Mathf.Max(audioSource.get_volume() - fadeoutVolume, 0f));
			if (audioSource.get_volume() == 0f)
			{
				StopImmidiate();
			}
		}
		if (!audioSource.get_isPlaying())
		{
			if (m_masterGroup != null)
			{
				m_masterGroup.NotifyOnStop(this);
				m_masterGroup = null;
			}
			Dispose();
		}
	}

	private void TraceParent()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (!m_IsStaticPosition && needParent && parentObject != null)
		{
			base._transform.set_position(parentObject.get_position());
		}
	}

	public void Stop(int fadeout_framecount = 0)
	{
		if (!(audioSource == null) && !(fadeoutVolume > 0f))
		{
			if (fadeout_framecount < 4)
			{
				fadeout_framecount = 4;
			}
			fadeoutVolume = audioSource.get_volume() / (float)fadeout_framecount;
			if (m_masterGroup != null)
			{
				m_masterGroup.NotifyOnRelease(this);
				m_masterGroup = null;
			}
			PlayPhase = Phase.PRESTOP;
		}
	}

	public void SetLoopFlag(bool flag)
	{
		audioSource.set_loop(flag);
	}

	public bool GetLoopFlag()
	{
		return audioSource.get_loop();
	}

	private void StopImmidiate()
	{
		if (!(audioSource == null) && PlayPhase != 0 && PlayPhase != Phase.STOP)
		{
			audioSource.Stop();
			if (m_masterGroup != null)
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
		if (audioSource != null)
		{
			audioSource.Stop();
			InitAudioSource();
		}
		if (m_masterGroup != null)
		{
			m_masterGroup.NotifyOnStop(this);
			m_masterGroup = null;
		}
		AudioObjectPool.Release(this);
	}
}
