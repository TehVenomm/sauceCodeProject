using rhyme;
using System;
using UnityEngine;

public class AnimationDirector
{
	public const string TAG = "Direction";

	public Camera useCamera;

	public GameObject fader;

	public Component commandReceiver;

	protected bool skip;

	private bool linkCamera;

	private Transform saveCameraParamsObject;

	private Camera saveCameraParams;

	private Vector3 saveCameraPos;

	private Quaternion saveCameraRot;

	private bool isDestroy;

	private Animator _animator;

	private int playingStateHash;

	private Action endCallback;

	public static AnimationDirector I
	{
		get;
		private set;
	}

	public bool isPlaying => playingStateHash != 0;

	public AnimationDirector()
		: this()
	{
	}

	public void __FUNCTION__InstantiatePrefab(string game_object_name)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		GameObject val = GameObject.Find(game_object_name);
		if (!(val == null))
		{
			DirectionPrefabObject component = val.GetComponent<DirectionPrefabObject>();
			if (!(component == null))
			{
				CreateEffect(component.prefab, component.get_transform());
			}
		}
	}

	public void __FUNCTION__PlayAudio(string game_object_name)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		GameObject val = GameObject.Find(game_object_name);
		if (!(val == null))
		{
			AudioSource component = val.GetComponent<AudioSource>();
			if (!(component == null))
			{
				val.set_tag("Direction");
				component.Play();
			}
		}
	}

	public virtual void __FUNCTION__PlayCachedAudio(int se_id)
	{
		SoundManager.PlayOneShotUISE(se_id);
	}

	public void __FUNCTION_Command(string command)
	{
		if (commandReceiver != null)
		{
			commandReceiver.SendMessage("OnDirectionCommand", (object)command);
		}
	}

	protected Transform CreateEffect(GameObject effect_prefab, Transform parent)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ResourceUtility.Realizes(effect_prefab, parent, -1);
		if (val == null)
		{
			return null;
		}
		val.get_gameObject().set_tag("Direction");
		rymFX component = val.GetComponent<rymFX>();
		if (component != null && useCamera != null)
		{
			component.Cameras = (Camera[])new Camera[1]
			{
				MonoBehaviourSingleton<AppMain>.I.mainCamera
			};
		}
		return val;
	}

	protected virtual void Awake()
	{
		if (I == null)
		{
			I = this;
		}
		_animator = this.GetComponent<Animator>();
		if (MonoBehaviourSingleton<AppMain>.IsValid() && useCamera != null)
		{
			useCamera.set_enabled(false);
		}
		if (fader != null)
		{
			fader.SetActive(false);
		}
	}

	protected virtual void OnDestroy()
	{
		if (!AppMain.isApplicationQuit)
		{
			isDestroy = true;
			SetLinkCamera(false);
			if (I == this)
			{
				I = null;
			}
		}
	}

	public void Play(string state_name, Action end_callback = null, float normalizedTime = 0f)
	{
		playingStateHash = Animator.StringToHash("Base Layer." + state_name);
		endCallback = end_callback;
		_animator.set_enabled(true);
		_animator.Play(playingStateHash, 0, normalizedTime);
		_animator.Update(0f);
	}

	protected virtual void Update()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		if (!(_animator == null))
		{
			_animator.set_speed((!skip) ? 1f : 10000f);
			if (playingStateHash != 0)
			{
				AnimatorStateInfo currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
				if (currentAnimatorStateInfo.get_fullPathHash() == playingStateHash && currentAnimatorStateInfo.get_normalizedTime() >= 1f)
				{
					playingStateHash = 0;
					if (endCallback != null)
					{
						Action action = endCallback;
						endCallback = null;
						action();
					}
				}
			}
		}
	}

	protected virtual void LateUpdate()
	{
		if (linkCamera)
		{
			MonoBehaviourSingleton<AppMain>.I.mainCamera.CopyFrom(useCamera);
		}
	}

	public void SetLinkCamera(bool is_link)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		if (!(useCamera == null) && linkCamera != is_link)
		{
			linkCamera = is_link;
			if (is_link)
			{
				if (saveCameraParams == null)
				{
					saveCameraParamsObject = Utility.CreateGameObject("saveCameraParams", this.get_transform(), -1);
					saveCameraParams = saveCameraParamsObject.get_gameObject().AddComponent<Camera>();
				}
				Transform val = this.get_transform();
				Vector3 position = val.get_position();
				Quaternion rotation = val.get_rotation();
				saveCameraParams.CopyFrom(MonoBehaviourSingleton<AppMain>.I.mainCamera);
				saveCameraPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.get_position();
				saveCameraRot = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.get_rotation();
				val.set_position(position);
				val.set_rotation(rotation);
				saveCameraParams.set_enabled(false);
				if (fader != null)
				{
					fader.SetActive(true);
				}
			}
			else
			{
				if (saveCameraParams != null)
				{
					MonoBehaviourSingleton<AppMain>.I.mainCamera.CopyFrom(saveCameraParams);
					MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.set_position(saveCameraPos);
					MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.set_rotation(saveCameraRot);
					if (!isDestroy)
					{
						Object.DestroyImmediate(saveCameraParamsObject.get_gameObject());
					}
					saveCameraParamsObject = null;
					saveCameraParams = null;
				}
				if (fader != null)
				{
					fader.SetActive(false);
				}
			}
		}
	}

	public virtual void Skip()
	{
		skip = true;
	}

	public bool IsSkip()
	{
		return skip;
	}

	public virtual void SkipAll()
	{
	}

	public virtual void Reset()
	{
		SetLinkCamera(false);
	}
}
