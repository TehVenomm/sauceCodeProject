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
		if (!((UnityEngine.Object)gameObject == (UnityEngine.Object)null))
		{
			DirectionPrefabObject component = gameObject.GetComponent<DirectionPrefabObject>();
			if (!((UnityEngine.Object)component == (UnityEngine.Object)null))
			{
				CreateEffect(component.prefab, component.transform);
			}
		}
	}

	public void __FUNCTION__PlayAudio(string game_object_name)
	{
		GameObject gameObject = GameObject.Find(game_object_name);
		if (!((UnityEngine.Object)gameObject == (UnityEngine.Object)null))
		{
			AudioSource component = gameObject.GetComponent<AudioSource>();
			if (!((UnityEngine.Object)component == (UnityEngine.Object)null))
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
		if ((UnityEngine.Object)commandReceiver != (UnityEngine.Object)null)
		{
			commandReceiver.SendMessage("OnDirectionCommand", command);
		}
	}

	protected Transform CreateEffect(GameObject effect_prefab, Transform parent)
	{
		Transform transform = ResourceUtility.Realizes(effect_prefab, parent, -1);
		if ((UnityEngine.Object)transform == (UnityEngine.Object)null)
		{
			return null;
		}
		transform.gameObject.tag = "Direction";
		rymFX component = transform.GetComponent<rymFX>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null && (UnityEngine.Object)useCamera != (UnityEngine.Object)null)
		{
			component.Cameras = new Camera[1]
			{
				MonoBehaviourSingleton<AppMain>.I.mainCamera
			};
		}
		return transform;
	}

	protected virtual void Awake()
	{
		if ((UnityEngine.Object)I == (UnityEngine.Object)null)
		{
			I = this;
		}
		_animator = GetComponent<Animator>();
		if (MonoBehaviourSingleton<AppMain>.IsValid() && (UnityEngine.Object)useCamera != (UnityEngine.Object)null)
		{
			useCamera.enabled = false;
		}
		if ((UnityEngine.Object)fader != (UnityEngine.Object)null)
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
			if ((UnityEngine.Object)I == (UnityEngine.Object)this)
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

	protected virtual void Update()
	{
		if (!((UnityEngine.Object)_animator == (UnityEngine.Object)null))
		{
			_animator.speed = ((!skip) ? 1f : 10000f);
			if (playingStateHash != 0)
			{
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
		if (!((UnityEngine.Object)useCamera == (UnityEngine.Object)null) && linkCamera != is_link)
		{
			linkCamera = is_link;
			if (is_link)
			{
				if ((UnityEngine.Object)saveCameraParams == (UnityEngine.Object)null)
				{
					saveCameraParamsObject = Utility.CreateGameObject("saveCameraParams", base.transform, -1);
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
				if ((UnityEngine.Object)fader != (UnityEngine.Object)null)
				{
					fader.SetActive(true);
				}
			}
			else
			{
				if ((UnityEngine.Object)saveCameraParams != (UnityEngine.Object)null)
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
				if ((UnityEngine.Object)fader != (UnityEngine.Object)null)
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
