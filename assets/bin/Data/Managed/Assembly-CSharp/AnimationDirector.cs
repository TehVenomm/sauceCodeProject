using rhyme;
using System;
using UnityEngine;

public class AnimationDirector : MonoBehaviour
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

	public void __FUNCTION__InstantiatePrefab(string game_object_name)
	{
		GameObject gameObject = GameObject.Find(game_object_name);
		if (!(gameObject == null))
		{
			DirectionPrefabObject component = gameObject.GetComponent<DirectionPrefabObject>();
			if (!(component == null))
			{
				CreateEffect(component.prefab, component.transform);
			}
		}
	}

	public void __FUNCTION__PlayAudio(string game_object_name)
	{
		GameObject gameObject = GameObject.Find(game_object_name);
		if (!(gameObject == null))
		{
			AudioSource component = gameObject.GetComponent<AudioSource>();
			if (!(component == null))
			{
				gameObject.tag = "Direction";
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
			commandReceiver.SendMessage("OnDirectionCommand", command);
		}
	}

	protected Transform CreateEffect(GameObject effect_prefab, Transform parent)
	{
		Transform transform = ResourceUtility.Realizes(effect_prefab, parent);
		if (transform == null)
		{
			return null;
		}
		transform.gameObject.tag = "Direction";
		rymFX component = transform.GetComponent<rymFX>();
		if (component != null && useCamera != null)
		{
			component.Cameras = new Camera[1]
			{
				MonoBehaviourSingleton<AppMain>.I.mainCamera
			};
		}
		return transform;
	}

	protected virtual void OnEnable()
	{
		I = this;
	}

	protected virtual void OnDisable()
	{
		if (I == this)
		{
			I = null;
		}
	}

	protected virtual void Awake()
	{
		I = this;
		_animator = GetComponent<Animator>();
		if (MonoBehaviourSingleton<AppMain>.IsValid() && useCamera != null)
		{
			useCamera.enabled = false;
		}
		if (fader != null)
		{
			fader.SetActive(value: false);
		}
	}

	protected virtual void OnDestroy()
	{
		if (!AppMain.isApplicationQuit)
		{
			isDestroy = true;
			SetLinkCamera(is_link: false);
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
		_animator.enabled = true;
		_animator.Play(playingStateHash, 0, normalizedTime);
		_animator.Update(0f);
	}

	public void SetAnimatorInteger(string name, int value)
	{
		_animator.SetInteger(name, value);
	}

	protected virtual void Update()
	{
		if (_animator == null)
		{
			return;
		}
		_animator.speed = (skip ? 10000f : 1f);
		if (playingStateHash == 0)
		{
			return;
		}
		AnimatorStateInfo currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
		if (currentAnimatorStateInfo.fullPathHash == playingStateHash && currentAnimatorStateInfo.normalizedTime >= 1f)
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

	protected virtual void LateUpdate()
	{
		if (linkCamera)
		{
			MonoBehaviourSingleton<AppMain>.I.mainCamera.CopyFrom(useCamera);
		}
	}

	public void SetLinkCamera(bool is_link)
	{
		if (useCamera == null || linkCamera == is_link)
		{
			return;
		}
		linkCamera = is_link;
		if (is_link)
		{
			if (saveCameraParams == null)
			{
				saveCameraParamsObject = Utility.CreateGameObject("saveCameraParams", base.transform);
				saveCameraParams = saveCameraParamsObject.gameObject.AddComponent<Camera>();
			}
			Transform transform = base.transform;
			Vector3 position = transform.position;
			Quaternion rotation = transform.rotation;
			saveCameraParams.CopyFrom(MonoBehaviourSingleton<AppMain>.I.mainCamera);
			saveCameraPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
			saveCameraRot = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation;
			transform.position = position;
			transform.rotation = rotation;
			saveCameraParams.enabled = false;
			if (fader != null)
			{
				fader.SetActive(value: true);
			}
			return;
		}
		if (saveCameraParams != null)
		{
			MonoBehaviourSingleton<AppMain>.I.mainCamera.CopyFrom(saveCameraParams);
			MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = saveCameraPos;
			MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation = saveCameraRot;
			if (!isDestroy)
			{
				UnityEngine.Object.DestroyImmediate(saveCameraParamsObject.gameObject);
			}
			saveCameraParamsObject = null;
			saveCameraParams = null;
		}
		if (fader != null)
		{
			fader.SetActive(value: false);
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
		SetLinkCamera(is_link: false);
	}
}
