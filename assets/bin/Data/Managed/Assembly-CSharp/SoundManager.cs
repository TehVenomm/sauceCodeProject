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

		public AudioRolloffMode rollOffMode => 0;

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

	private bool m_IsLoaded;

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
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		if (!(audioMixer == null))
		{
			AudioMixerSnapshot val = audioMixer.FindSnapshot(snapshotName);
			if (!(val == null) && !(mixerCurrent == val))
			{
				val.TransitionTo(transitionTime);
				mixerCurrent = val;
			}
		}
	}

	private void TransitionTo(AudioMixerSnapshot current, AudioMixerSnapshot next, float transitionTime = 1f)
	{
		if (!(current == null) && !(next == null) && !(audioMixer == null))
		{
			AudioMixerSnapshot[] array = (AudioMixerSnapshot[])new AudioMixerSnapshot[2]
			{
				current,
				next
			};
			float[] array2 = new float[2]
			{
				0f,
				1f
			};
			audioMixer.TransitionToSnapshots(array, array2, transitionTime);
		}
	}

	public void SetAudioMixer(AudioMixer audio_mixer)
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		audioMixer = audio_mixer;
		if (audio_mixer != null)
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
		if (audioControlSESelf == null)
		{
			audioControlSESelf = AudioControlGroup.Create(AudioControlGroup.CullingTypes.NONE, 2147483647);
		}
		if (audioControlJingle == null)
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
				if (audioSourceBGM != null && audioSourceBGM.get_isPlaying())
				{
					bool is_play_fadeout = true;
					EventDelegate.Callback OnFinishedCallBack = delegate
					{
						((_003CStart_003Ec__Iterator29A)/*Error near IL_00f3: stateMachine*/)._003Cis_play_fadeout_003E__1 = false;
					};
					TweenVolume fadeout = TweenVolume.Begin(this.get_gameObject(), fadeOutTime, 0f);
					EventDelegate.Add(fadeout.onFinished, OnFinishedCallBack);
					while (is_play_fadeout)
					{
						audioSourceBGM.set_volume(fadeout.value);
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
					if (!(lo_bgm.loadedObject != null))
					{
						int num3 = playingBGMID = (requestBGMID = 0);
					}
					else
					{
						if (audioSourceBGM == null)
						{
							audioSourceBGM = this.get_gameObject().AddComponent<AudioSource>();
						}
						if (audioSourceBGM != null)
						{
							audioSourceBGM.set_priority(0);
							audioSourceBGM.set_reverbZoneMix(0f);
							audioSourceBGM.set_spread(360f);
							audioSourceBGM.set_spatialBlend(0f);
							audioSourceBGM.set_outputAudioMixerGroup(mixerGroupBGM);
							audioSourceBGM.set_loop(m_IsNextBGMLoop);
							audioSourceBGM.set_enabled(true);
							audioSourceBGM.set_clip(lo_bgm.loadedObject as AudioClip);
							audioSourceBGM.set_volume(volumeBGM);
							audioSourceBGM.Play(0uL);
						}
					}
				}
				if (playingBGMID == 0 && audioSourceBGM != null)
				{
					audioSourceBGM.Stop();
				}
				changingBGM = false;
			}
		}
	}

	public void UpdateConfigVolume()
	{
		if (audioMixer != null && GameSaveData.instance != null)
		{
			ApplyVolume("ConfigBGMVolume", GameSaveData.instance.volumeBGM);
			ApplyVolume("ConfigSEVolume", GameSaveData.instance.volumeSE);
		}
	}

	private void ApplyVolume(string volumeLabel, float configValue = 1f)
	{
		if (!(audioMixer == null))
		{
			float num = Utility.VolumeToDecibel(configValue);
			audioMixer.SetFloat(volumeLabel, num);
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
			AudioClip val = MonoBehaviourSingleton<SoundManager>.I.m_SystemSEClips.Get((uint)SEType);
			if (!(val == null))
			{
				PlayUISE(val, volume, false, null, (int)SEType);
			}
		}
	}

	public static AudioObject PlaySE(AudioClip clip, bool loop, Transform parent)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			return null;
		}
		if (clip == null)
		{
			return null;
		}
		string s = (!string.IsNullOrEmpty(clip.get_name())) ? clip.get_name().Substring(3) : string.Empty;
		int result = 0;
		if (int.TryParse(s, out result))
		{
			Vector3 position = parent.get_position();
			return MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(clip, result, MonoBehaviourSingleton<SoundManager>.I.volumeSE, loop, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, true, null, null, position);
		}
		return AudioObject.Create(clip, 0, MonoBehaviourSingleton<SoundManager>.I.volumeSE, loop, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf, true, null, parent, null);
	}

	public static void PlayOneShotSE(int se_id, Vector3 pos)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
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
		if (MonoBehaviourSingleton<SoundManager>.IsValid() && !(clip == null))
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
		if (!(audioControlVoice == null))
		{
			audioControlVoice.StopAll(fadeout_frame);
		}
	}

	public static void PlayVoice(AudioClip audio_clip, int voice_id, float volume = 1f, uint ch_id = 0, DisableNotifyMonoBehaviour master = null, Transform parent = null)
	{
		if (GameSaveData.instance.voiceOption != 2 && MonoBehaviourSingleton<SoundManager>.IsValid() && !(audio_clip == null))
		{
			float num = volume * MonoBehaviourSingleton<SoundManager>.I.volumeVOICE;
			if (!(num < 0.05f))
			{
				bool is3DSound = (!(parent == null)) ? true : false;
				AudioControlGroup audioControlVoice = MonoBehaviourSingleton<SoundManager>.I.GetAudioControlVoice(ch_id);
				if (!(audioControlVoice == null))
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
			bool is3DSound = (!(parent == null)) ? true : false;
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
			while (!(audioObject != null) || audioObject.clipId != se_id);
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
			while (!(audioObject != null) || audioObject.clipId != se_id || !audioObject.GetLoopFlag());
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
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		return MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(category, name);
	}

	private static AudioClip GetAudioClip(RESOURCE_CATEGORY category, string package_name, string name)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		return MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(category, package_name, name);
	}

	private static AudioClip GetActionVoiceAudioClip(int voice_id)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		return MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageNameFromVoiceID(voice_id), ResourceName.GetActionVoiceName(voice_id));
	}

	public static AudioClip GetStoryVoiceAudioClip(int voice_id)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		return MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetStoryVoicePackageNameFromVoiceID(voice_id), ResourceName.GetStoryVoiceName(voice_id));
	}

	public uint GetVoiceChannel(StageObject stageObject)
	{
		if (stageObject == null)
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
		if (player != null)
		{
			return GetReservedChannel(player);
		}
		return 0u;
	}

	public void OnDetachedObject(StageObject stageObject)
	{
		if (!(stageObject == null))
		{
			Player player = stageObject as Player;
			if (player != null)
			{
				ChancelResavationChannel(player);
			}
		}
	}

	private uint GetReservedChannel(Player player)
	{
		if (player == null)
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
		if (!(player == null) && m_dicReservedVoiceChannel != null && m_dicReservedVoiceChannel.ContainsKey(player.id))
		{
			m_dicReservedVoiceChannel.Remove(player.id);
		}
	}

	public void LoadParmanentAudioClip()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		m_IsLoading = true;
		this.StartCoroutine(DoLoading());
	}

	public Coroutine WaitLoading()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		return this.StartCoroutine(DoWaitLoading());
	}

	public bool IsLoadingAudioClip()
	{
		if (!MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			return false;
		}
		return m_IsLoading;
	}

	public bool IsLoadedAudioClip()
	{
		if (!MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			return false;
		}
		return m_IsLoaded;
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
		m_IsLoaded = true;
	}

	public static AudioClip GetAttachedAudio(GameObject go, string filter = null)
	{
		if (go == null)
		{
			return null;
		}
		ResourceLink component = go.GetComponent<ResourceLink>();
		if (component == null)
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
					if (lo != null && lo.loadedObject != null && !string.IsNullOrEmpty(lo.loadedObject.get_name()))
					{
						string b = ResourceName.Normalize(lo.loadedObject.get_name());
						if (sE == b)
						{
							loadObject = lo;
						}
					}
				}
				if (loadObject != null)
				{
					AudioClip val = loadObject.loadedObject as AudioClip;
					if (val != null)
					{
						m_SystemSEClips.Add((uint)num, val);
					}
				}
			}
		}
	}
}
