using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviourSingleton<SoundManager>
{
	public class AudioPreset
	{
		public string name
		{
			get;
			private set;
		}

		public float minDistance
		{
			get;
			private set;
		}

		public float maxDistance
		{
			get;
			private set;
		}

		public AudioRolloffMode rollOffMode => AudioRolloffMode.Logarithmic;

		public AudioPreset(string _name, float _minDistance, float _maxDistance)
		{
			name = _name;
			minDistance = _minDistance;
			maxDistance = _maxDistance;
		}
	}

	public const float BGM_FADEOUT_TIME = 1f;

	private const string DEFAULT_SNAPSHOT_NAME = "Default";

	private const string MIXER_PATH_MAIN = "Audio/MainMixer";

	private const string PARAM_LABEL_VOLUME_MASTER = "MasterVolume";

	private const string PARAM_LABEL_VOLUME_BGM = "ConfigBGMVolume";

	private const string PARAM_LABEL_VOLUME_SE = "ConfigSEVolume";

	private const float DEFAULT_MIN_DISTANCE = 15f;

	private const float DEFAULT_MAX_DISTANCE = 60f;

	private const uint VOICE_CH_MAX = 6u;

	public const uint VOICE_CH_DEFAULT = 0u;

	public const uint VOICE_CH_ENEMY = 1u;

	private const uint VOICE_CH_PLAYER_HEAD = 2u;

	private int m_requestBGMID;

	private bool m_IsNextBGMLoop = true;

	public float volumeSE = 1f;

	public float volumeBGM = 1f;

	public float volumeVOICE = 1f;

	public float fadeOutTime;

	private AudioMixerSnapshot mixerCurrent;

	private AudioMixerSnapshot mixerSnapshotDefault;

	private AudioControlGroup[] audioControllVoice;

	public Dictionary<int, uint> m_dicReservedVoiceChannel;

	private bool m_IsLoading;

	public UIntKeyTable<AudioClip> m_SystemSEClips;

	public AudioPreset CurrentPreset
	{
		get;
		private set;
	}

	public int requestBGMID
	{
		get
		{
			return m_requestBGMID;
		}
		set
		{
			m_requestBGMID = value;
			m_IsNextBGMLoop = true;
		}
	}

	public int playingBGMID
	{
		get;
		private set;
	}

	public bool changingBGM
	{
		get;
		private set;
	}

	public AudioSource audioSourceBGM
	{
		get;
		private set;
	}

	public AudioMixer audioMixer
	{
		get;
		private set;
	}

	public AudioMixerGroup mixerGroupMaster
	{
		get;
		private set;
	}

	public AudioMixerGroup mixerGroupBGM
	{
		get;
		private set;
	}

	public AudioMixerGroup mixerGroupSE
	{
		get;
		private set;
	}

	public AudioMixerGroup mixerGroupUISE
	{
		get;
		private set;
	}

	public AudioMixerGroup mixerGroupVoice
	{
		get;
		private set;
	}

	public AudioMixerGroup mixerGroupJingle
	{
		get;
		private set;
	}

	public AudioMixerGroup mixerGroupFX
	{
		get;
		private set;
	}

	public AudioMixerGroup mixerConfigBGM
	{
		get;
		private set;
	}

	public AudioMixerGroup mixerConfigSE
	{
		get;
		private set;
	}

	public bool IsActiveSoundEffect
	{
		get;
		private set;
	}

	public AudioControlGroup audioControlSESelf
	{
		get;
		private set;
	}

	public AudioControlGroup audioControlJingle
	{
		get;
		private set;
	}

	public static void RequestBGM(int bgmId, bool isLoop = true)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.m_requestBGMID = bgmId;
			MonoBehaviourSingleton<SoundManager>.I.m_IsNextBGMLoop = isLoop;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		LoadAudioMixer();
		SetAudioMixer(audioMixer);
		SetupAudioControlGroup();
		mixerCurrent = mixerSnapshotDefault;
		TransitionPreset(0u);
		IsActiveSoundEffect = true;
	}

	private void LoadAudioMixer()
	{
		audioMixer = Resources.Load<AudioMixer>("Audio/MainMixer");
	}

	public void TransitionPreset(uint presetId = 0)
	{
		string text = "Default";
		float minDistance = 15f;
		float maxDistance = 60f;
		if (presetId != 0 && Singleton<AudioSettingTable>.IsValid())
		{
			AudioSettingTable.Data data = Singleton<AudioSettingTable>.I.GetData(presetId);
			if (data != null)
			{
				text = data.name;
				minDistance = data.minDistance;
				maxDistance = data.maxDistance;
			}
		}
		CurrentPreset = new AudioPreset(text, minDistance, maxDistance);
		TransitionTo(text, 1f);
	}

	public void TransitionTo(string snapshotName, float transitionTime = 1f)
	{
		if (!((UnityEngine.Object)audioMixer == (UnityEngine.Object)null))
		{
			AudioMixerSnapshot audioMixerSnapshot = audioMixer.FindSnapshot(snapshotName);
			if (!((UnityEngine.Object)audioMixerSnapshot == (UnityEngine.Object)null) && !((UnityEngine.Object)mixerCurrent == (UnityEngine.Object)audioMixerSnapshot))
			{
				audioMixerSnapshot.TransitionTo(transitionTime);
				mixerCurrent = audioMixerSnapshot;
			}
		}
	}

	private void TransitionTo(AudioMixerSnapshot current, AudioMixerSnapshot next, float transitionTime = 1f)
	{
		if (!((UnityEngine.Object)current == (UnityEngine.Object)null) && !((UnityEngine.Object)next == (UnityEngine.Object)null) && !((UnityEngine.Object)audioMixer == (UnityEngine.Object)null))
		{
			AudioMixerSnapshot[] snapshots = new AudioMixerSnapshot[2]
			{
				current,
				next
			};
			float[] weights = new float[2]
			{
				0f,
				1f
			};
			audioMixer.TransitionToSnapshots(snapshots, weights, transitionTime);
		}
	}

	public void SetAudioMixer(AudioMixer audio_mixer)
	{
		audioMixer = audio_mixer;
		if ((UnityEngine.Object)audio_mixer != (UnityEngine.Object)null)
		{
			mixerGroupMaster = GetAudioMixerGroup("Master");
			mixerGroupBGM = GetAudioMixerGroup("Master/CONFIG_BGM/BGM");
			mixerGroupFX = GetAudioMixerGroup("Master/CONFIG_SE/FX");
			mixerGroupSE = GetAudioMixerGroup("Master/CONFIG_SE/FX/SE");
			mixerGroupUISE = GetAudioMixerGroup("Master/CONFIG_SE/UISE");
			mixerGroupVoice = GetAudioMixerGroup("Master/CONFIG_SE/FX/Voice");
			mixerGroupJingle = GetAudioMixerGroup("Master/CONFIG_SE/Jingle");
			mixerConfigBGM = GetAudioMixerGroup("Master/CONFIG_BGM");
			mixerConfigSE = GetAudioMixerGroup("Master/CONFIG_SE");
			mixerSnapshotDefault = audioMixer.FindSnapshot("Default");
		}
		else
		{
			mixerGroupMaster = null;
			mixerGroupFX = null;
			mixerGroupBGM = null;
			mixerGroupSE = null;
			mixerGroupUISE = null;
			mixerGroupVoice = null;
			mixerGroupJingle = null;
			mixerCurrent = null;
			mixerSnapshotDefault = null;
		}
		UpdateConfigVolume();
	}

	private AudioMixerGroup GetAudioMixerGroup(string path)
	{
		AudioMixerGroup[] array = audioMixer.FindMatchingGroups(path);
		if (array.Length > 0)
		{
			return array[0];
		}
		return null;
	}

	public void SetupAudioControlGroup()
	{
		if ((UnityEngine.Object)audioControlSESelf == (UnityEngine.Object)null)
		{
			audioControlSESelf = AudioControlGroup.Create(AudioControlGroup.CullingTypes.NONE, 2147483647);
		}
		if ((UnityEngine.Object)audioControlJingle == (UnityEngine.Object)null)
		{
			audioControlJingle = AudioControlGroup.Create(AudioControlGroup.CullingTypes.NONE, 2147483647);
		}
		if (audioControllVoice == null)
		{
			audioControllVoice = new AudioControlGroup[6];
			for (int i = 0; (long)i < 6L; i++)
			{
				audioControllVoice[i] = AudioControlGroup.Create(AudioControlGroup.CullingTypes.OVERWRITE, 1);
			}
		}
	}

	private IEnumerator Start()
	{
		while (true)
		{
			yield return (object)null;
			if (playingBGMID != requestBGMID)
			{
				changingBGM = true;
				playingBGMID = requestBGMID;
				LoadObject lo_bgm = null;
				if (playingBGMID != 0)
				{
					ResourceManager.enableCache = false;
					lo_bgm = new LoadObject(this, RESOURCE_CATEGORY.SOUND_BGM, ResourceName.GetBGM(requestBGMID), false);
					ResourceManager.enableCache = true;
				}
				if ((UnityEngine.Object)audioSourceBGM != (UnityEngine.Object)null && audioSourceBGM.isPlaying)
				{
					bool is_play_fadeout = true;
					EventDelegate.Callback OnFinishedCallBack = delegate
					{
						((_003CStart_003Ec__Iterator27B)/*Error near IL_00f3: stateMachine*/)._003Cis_play_fadeout_003E__1 = false;
					};
					TweenVolume fadeout = TweenVolume.Begin(base.gameObject, fadeOutTime, 0f);
					EventDelegate.Add(fadeout.onFinished, OnFinishedCallBack);
					while (is_play_fadeout)
					{
						audioSourceBGM.volume = fadeout.value;
						yield return (object)null;
					}
					EventDelegate.Remove(fadeout.onFinished, OnFinishedCallBack);
				}
				if (lo_bgm != null)
				{
					if (lo_bgm.isLoading)
					{
						yield return (object)lo_bgm.Wait(this);
					}
					if (!(lo_bgm.loadedObject != (UnityEngine.Object)null))
					{
						int num3 = playingBGMID = (requestBGMID = 0);
					}
					else
					{
						if ((UnityEngine.Object)audioSourceBGM == (UnityEngine.Object)null)
						{
							audioSourceBGM = base.gameObject.AddComponent<AudioSource>();
						}
						if ((UnityEngine.Object)audioSourceBGM != (UnityEngine.Object)null)
						{
							audioSourceBGM.priority = 0;
							audioSourceBGM.reverbZoneMix = 0f;
							audioSourceBGM.spread = 360f;
							audioSourceBGM.spatialBlend = 0f;
							audioSourceBGM.outputAudioMixerGroup = mixerGroupBGM;
							audioSourceBGM.loop = m_IsNextBGMLoop;
							audioSourceBGM.enabled = true;
							audioSourceBGM.clip = (lo_bgm.loadedObject as AudioClip);
							audioSourceBGM.volume = volumeBGM;
							audioSourceBGM.Play(0uL);
						}
					}
				}
				if (playingBGMID == 0 && (UnityEngine.Object)audioSourceBGM != (UnityEngine.Object)null)
				{
					audioSourceBGM.Stop();
				}
				changingBGM = false;
			}
		}
	}

	public void UpdateConfigVolume()
	{
		if ((UnityEngine.Object)audioMixer != (UnityEngine.Object)null && GameSaveData.instance != null)
		{
			ApplyVolume("ConfigBGMVolume", GameSaveData.instance.volumeBGM);
			ApplyVolume("ConfigSEVolume", GameSaveData.instance.volumeSE);
		}
	}

	private void ApplyVolume(string volumeLabel, float configValue = 1f)
	{
		if (!((UnityEngine.Object)audioMixer == (UnityEngine.Object)null))
		{
			float value = Utility.VolumeToDecibel(configValue);
			audioMixer.SetFloat(volumeLabel, value);
		}
	}

	public static AudioObject PlayUISE(AudioClip clip, float volume, bool loop, Transform parent, int config_id = 0)
	{
		if (!MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			return null;
		}
		float volume2 = volume * MonoBehaviourSingleton<SoundManager>.I.volumeSE;
		return MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(clip, config_id, volume2, loop, MonoBehaviourSingleton<SoundManager>.I.mixerGroupUISE, false, null, parent, null);
	}

	public static void PlaySystemSE(SoundID.UISE SEType, float volume = 1f)
	{
		if (MonoBehaviourSingleton<SoundManager>.I.m_SystemSEClips != null)
		{
			AudioClip audioClip = MonoBehaviourSingleton<SoundManager>.I.m_SystemSEClips.Get((uint)SEType);
			if (!((UnityEngine.Object)audioClip == (UnityEngine.Object)null))
			{
				PlayUISE(audioClip, volume, false, null, (int)SEType);
			}
		}
	}

	public static AudioObject PlaySE(AudioClip clip, bool loop, Transform parent)
	{
		if (!MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			return null;
		}
		if ((UnityEngine.Object)clip == (UnityEngine.Object)null)
		{
			return null;
		}
		string s = (!string.IsNullOrEmpty(clip.name)) ? clip.name.Substring(3) : string.Empty;
		int result = 0;
		if (int.TryParse(s, out result))
		{
			Vector3 position = parent.position;
			return MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(clip, result, MonoBehaviourSingleton<SoundManager>.I.volumeSE, loop, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, true, null, null, position);
		}
		return AudioObject.Create(clip, 0, MonoBehaviourSingleton<SoundManager>.I.volumeSE, loop, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf, true, null, parent, null);
	}

	public static void PlayOneShotSE(int se_id, Vector3 pos)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, true, null, null, pos);
		}
	}

	public static void StopSEAll(int fadeout_framecount = 0)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.StopAll(fadeout_framecount);
		}
	}

	public static void StopAudioObjectAll()
	{
		if (MonoBehaviourSingleton<AudioObjectPool>.IsValid())
		{
			AudioObjectPool.StopAll();
		}
	}

	public static void PlayOneShotUISE(int se_id)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupUISE, false, null, null, null);
		}
	}

	public static void PlayOneShotUISE(AudioClip clip, int se_id)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid() && !((UnityEngine.Object)clip == (UnityEngine.Object)null))
		{
			MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(clip, se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupUISE, false, null, null, null);
		}
	}

	public static AudioObject PlayUISE(int se_id)
	{
		if (!MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			return null;
		}
		return MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupUISE, false, null, null, null);
	}

	public static void PlayOneShotSE(int se_id, DisableNotifyMonoBehaviour master = null, Transform parent = null)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, true, master, parent, null);
		}
	}

	public static void SetMuteVoice(bool isMute)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.volumeVOICE = ((!isMute) ? 1f : 0f);
		}
	}

	public static void PlayVoice(int voice_id, float volume = 1f, uint ch_id = 0, DisableNotifyMonoBehaviour master = null, Transform parent = null)
	{
		AudioClip storyVoiceAudioClip = GetStoryVoiceAudioClip(voice_id);
		PlayVoice(storyVoiceAudioClip, voice_id, volume, ch_id, master, parent);
	}

	public static void PlayActionVoice(int voice_id, float volume = 1f, uint ch_id = 0, DisableNotifyMonoBehaviour master = null, Transform parent = null)
	{
		AudioClip actionVoiceAudioClip = GetActionVoiceAudioClip(voice_id);
		PlayVoice(actionVoiceAudioClip, voice_id, volume, ch_id, master, parent);
	}

	public static void StopVoice(uint ch_id = 0, int fadeout_frame = 2)
	{
		AudioControlGroup audioControlVoice = MonoBehaviourSingleton<SoundManager>.I.GetAudioControlVoice(ch_id);
		if (!((UnityEngine.Object)audioControlVoice == (UnityEngine.Object)null))
		{
			audioControlVoice.StopAll(fadeout_frame);
		}
	}

	public static void PlayVoice(AudioClip audio_clip, int voice_id, float volume = 1f, uint ch_id = 0, DisableNotifyMonoBehaviour master = null, Transform parent = null)
	{
		if (GameSaveData.instance.voiceOption != 2 && MonoBehaviourSingleton<SoundManager>.IsValid() && !((UnityEngine.Object)audio_clip == (UnityEngine.Object)null))
		{
			float num = volume * MonoBehaviourSingleton<SoundManager>.I.volumeVOICE;
			if (!(num < 0.05f))
			{
				bool is3DSound = (!((UnityEngine.Object)parent == (UnityEngine.Object)null)) ? true : false;
				AudioControlGroup audioControlVoice = MonoBehaviourSingleton<SoundManager>.I.GetAudioControlVoice(ch_id);
				if (!((UnityEngine.Object)audioControlVoice == (UnityEngine.Object)null))
				{
					audioControlVoice.CreateAudio(audio_clip, voice_id, num, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupVoice, is3DSound, master, parent, null);
				}
			}
		}
	}

	public AudioControlGroup GetAudioControlVoice(uint ch_id = 0)
	{
		if (audioControllVoice == null)
		{
			return null;
		}
		if (ch_id >= 6 || ch_id >= audioControllVoice.Length)
		{
			return null;
		}
		return audioControllVoice[ch_id];
	}

	public static void PlayOneshotJingle(int se_id, DisableNotifyMonoBehaviour master = null, Transform parent = null)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.audioControlJingle.CreateAudio(GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupJingle, false, master, parent, null);
		}
	}

	public static void PlayOneshotJingle(AudioClip clip, int se_id, DisableNotifyMonoBehaviour master = null, Transform parent = null)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.audioControlJingle.CreateAudio(clip, se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupJingle, false, master, parent, null);
		}
	}

	public static void PlayLoopSE(int se_id, DisableNotifyMonoBehaviour master, Transform parent = null)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			bool is3DSound = (!((UnityEngine.Object)parent == (UnityEngine.Object)null)) ? true : false;
			MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, true, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, is3DSound, master, parent, null);
		}
	}

	public static void StopLoopSE(int se_id, DisableNotifyMonoBehaviour master)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid() && master.notifyServants != null)
		{
			List<DisableNotifyMonoBehaviour>.Enumerator enumerator = master.notifyServants.GetEnumerator();
			AudioObject audioObject;
			do
			{
				if (!enumerator.MoveNext())
				{
					return;
				}
				audioObject = (enumerator.Current as AudioObject);
			}
			while (!((UnityEngine.Object)audioObject != (UnityEngine.Object)null) || audioObject.clipId != se_id);
			audioObject.Stop(0);
		}
	}

	public static void LoopOff(int se_id, DisableNotifyMonoBehaviour master)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid() && master.notifyServants != null)
		{
			List<DisableNotifyMonoBehaviour>.Enumerator enumerator = master.notifyServants.GetEnumerator();
			AudioObject audioObject;
			do
			{
				if (!enumerator.MoveNext())
				{
					return;
				}
				audioObject = (enumerator.Current as AudioObject);
			}
			while (!((UnityEngine.Object)audioObject != (UnityEngine.Object)null) || audioObject.clipId != se_id || !audioObject.GetLoopFlag());
			audioObject.SetLoopFlag(false);
		}
	}

	public static AudioClip GetSEAudioClip(int se_id)
	{
		return GetSEAudioClip(ResourceName.GetSE(se_id));
	}

	public static AudioClip GetSEAudioClip(string name)
	{
		return GetAudioClip(RESOURCE_CATEGORY.SOUND_SE, name.Substring(0, 5), name);
	}

	private static AudioClip GetAudioClip(RESOURCE_CATEGORY category, string name)
	{
		return (AudioClip)MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(category, name);
	}

	private static AudioClip GetAudioClip(RESOURCE_CATEGORY category, string package_name, string name)
	{
		return (AudioClip)MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(category, package_name, name);
	}

	private static AudioClip GetActionVoiceAudioClip(int voice_id)
	{
		return (AudioClip)MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageNameFromVoiceID(voice_id), ResourceName.GetActionVoiceName(voice_id));
	}

	public static AudioClip GetStoryVoiceAudioClip(int voice_id)
	{
		return (AudioClip)MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetStoryVoicePackageNameFromVoiceID(voice_id), ResourceName.GetStoryVoiceName(voice_id));
	}

	public uint GetVoiceChannel(StageObject stageObject)
	{
		if ((UnityEngine.Object)stageObject == (UnityEngine.Object)null)
		{
			return 0u;
		}
		if (stageObject is Enemy)
		{
			return 1u;
		}
		if (stageObject is Self)
		{
			return 2u;
		}
		Player player = stageObject as Player;
		if ((UnityEngine.Object)player != (UnityEngine.Object)null)
		{
			return GetReservedChannel(player);
		}
		return 0u;
	}

	public void OnDetachedObject(StageObject stageObject)
	{
		if (!((UnityEngine.Object)stageObject == (UnityEngine.Object)null))
		{
			Player player = stageObject as Player;
			if ((UnityEngine.Object)player != (UnityEngine.Object)null)
			{
				ChancelResavationChannel(player);
			}
		}
	}

	private uint GetReservedChannel(Player player)
	{
		if ((UnityEngine.Object)player == (UnityEngine.Object)null)
		{
			return 0u;
		}
		if (player is Self)
		{
			return 2u;
		}
		if (m_dicReservedVoiceChannel == null)
		{
			m_dicReservedVoiceChannel = new Dictionary<int, uint>();
		}
		if (m_dicReservedVoiceChannel.ContainsKey(player.id))
		{
			return m_dicReservedVoiceChannel[player.id];
		}
		uint num = (uint)(3 + m_dicReservedVoiceChannel.Keys.Count);
		if (num >= 6)
		{
			num = 0u;
		}
		m_dicReservedVoiceChannel.Add(player.id, num);
		return m_dicReservedVoiceChannel[player.id];
	}

	private void ChancelResavationChannel(Player player)
	{
		if (!((UnityEngine.Object)player == (UnityEngine.Object)null) && m_dicReservedVoiceChannel != null && m_dicReservedVoiceChannel.ContainsKey(player.id))
		{
			m_dicReservedVoiceChannel.Remove(player.id);
		}
	}

	public void LoadParmanentAudioClip()
	{
		m_IsLoading = true;
		StartCoroutine(DoLoading());
	}

	public Coroutine WaitLoading()
	{
		return StartCoroutine(DoWaitLoading());
	}

	public bool IsLoadingAudioClip()
	{
		if (!MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			return false;
		}
		return m_IsLoading;
	}

	private IEnumerator DoWaitLoading()
	{
		while (IsLoadingAudioClip())
		{
			yield return (object)null;
		}
	}

	private IEnumerator DoLoading()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		int[] values = (int[])Enum.GetValues(typeof(SoundID.UISE));
		List<LoadObject> los = new List<LoadObject>();
		bool internal_mode = ResourceManager.internalMode;
		bool enable_cache = ResourceManager.enableCache;
		ResourceManager.internalMode = true;
		ResourceManager.enableCache = false;
		int[] array = values;
		foreach (int id in array)
		{
			if (id > 0)
			{
				LoadObject lo = load_queue.LoadSE(id);
				los.Add(lo);
			}
		}
		ResourceManager.internalMode = internal_mode;
		ResourceManager.enableCache = enable_cache;
		yield return (object)load_queue.Wait();
		SetSystemSEClips(values, los);
		m_IsLoading = false;
	}

	public static AudioClip GetAttachedAudio(GameObject go, string filter = null)
	{
		if ((UnityEngine.Object)go == (UnityEngine.Object)null)
		{
			return null;
		}
		ResourceLink component = go.GetComponent<ResourceLink>();
		if ((UnityEngine.Object)component == (UnityEngine.Object)null)
		{
			return null;
		}
		return component.GetFirstObject<AudioClip>(filter);
	}

	public void SetSystemSEClips(int[] values, List<LoadObject> los)
	{
		m_SystemSEClips = new UIntKeyTable<AudioClip>();
		foreach (int num in values)
		{
			if (num > 0)
			{
				string sE = ResourceName.GetSE(num);
				LoadObject loadObject = null;
				foreach (LoadObject lo in los)
				{
					if (lo != null && lo.loadedObject != (UnityEngine.Object)null && !string.IsNullOrEmpty(lo.loadedObject.name))
					{
						string b = ResourceName.Normalize(lo.loadedObject.name);
						if (sE == b)
						{
							loadObject = lo;
						}
					}
				}
				if (loadObject != null)
				{
					AudioClip audioClip = loadObject.loadedObject as AudioClip;
					if ((UnityEngine.Object)audioClip != (UnityEngine.Object)null)
					{
						m_SystemSEClips.Add((uint)num, audioClip);
					}
				}
			}
		}
	}
}
