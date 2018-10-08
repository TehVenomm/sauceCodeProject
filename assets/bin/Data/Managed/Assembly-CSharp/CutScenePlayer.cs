using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePlayer : MonoBehaviour
{
	public class CutSceneCamera
	{
		public Camera camera;

		public Transform transform;
	}

	public class PlayerInfo
	{
		public GameObject obj;

		public Animator animator;

		public RuntimeAnimatorController originalController;

		public CutSceneData.PlayerData keyData;
	}

	public class EnemyInfo
	{
		public GameObject obj;

		public Animator animator;

		public RuntimeAnimatorController originalController;

		public CutSceneData.EnemyData keyData;
	}

	public class ActorInfo
	{
		public GameObject obj;

		public Animator animator;

		public RuntimeAnimatorController originalController;

		public CutSceneData.ActorData keyData;
	}

	public class EffectInfo
	{
		public CutSceneData.EffectKeyData keyData;

		public bool isPlayed;
	}

	public class SeInfo
	{
		public CutSceneData.SEKeyData keyData;

		public bool isPlayed;
	}

	private const int MAX_CAMERA_NUM = 2;

	private const int MAX_PLAYER_NUM = 4;

	private const int MAX_CUT_SCENE_ANIMATOR_STATE_NUM = 30;

	private Transform _transform;

	[SerializeField]
	private CutSceneData cutSceneData;

	private bool isInitialized;

	private Action onComplete;

	private CutSceneCamera[] cameras = new CutSceneCamera[2];

	private Animator cameraAnimator;

	private Transform cameraAnimatorTransform;

	private PlayerInfo[] playerInfo;

	private EnemyInfo enemyInfo;

	private ActorInfo[] actorInfo;

	private EffectInfo[] effectInfo;

	private SeInfo[] seInfo;

	[SerializeField]
	private int cutNo;

	[SerializeField]
	private float playingTime;

	private float oldTime;

	private int[] CUT_STATE_HASH = new int[30];

	private int ENDING_STATE_ID;

	public bool hasStory
	{
		get
		{
			if ((UnityEngine.Object)cutSceneData == (UnityEngine.Object)null)
			{
				return false;
			}
			return cutSceneData.storyId != 0;
		}
	}

	public bool isPlaying
	{
		get;
		private set;
	}

	private void Awake()
	{
		_transform = base.transform;
		GameObject gameObject = new GameObject("CameraController");
		gameObject.transform.parent = _transform;
		cameraAnimator = gameObject.AddComponent<Animator>();
		cameraAnimatorTransform = cameraAnimator.transform;
		for (int i = 0; i < 30; i++)
		{
			CUT_STATE_HASH[i] = Animator.StringToHash("Cut_" + i.ToString("D3"));
		}
		ENDING_STATE_ID = Animator.StringToHash("END");
	}

	public void Init(string cutSceneDataPath, Action<bool> _onComplete)
	{
		StartCoroutine(InitImpl(cutSceneDataPath, _onComplete));
	}

	private IEnumerator InitImpl(string cutSceneDataPath, Action<bool> _onComplete)
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		if (!string.IsNullOrEmpty(cutSceneDataPath))
		{
			LoadObject loadedCutSceneObj = loadQueue.Load(RESOURCE_CATEGORY.CUTSCENE, cutSceneDataPath, false);
			if (loadQueue.IsLoading())
			{
				yield return (object)loadQueue.Wait();
			}
			cutSceneData = (loadedCutSceneObj.loadedObject as CutSceneData);
		}
		if ((UnityEngine.Object)cutSceneData == (UnityEngine.Object)null)
		{
			_onComplete?.Invoke(false);
		}
		else
		{
			for (int n = 0; n < cutSceneData.seDataList.Count; n++)
			{
				loadQueue.CacheSE(cutSceneData.seDataList[n].seId, null);
			}
			for (int m = 0; m < cutSceneData.effectKeyData.Count; m++)
			{
				loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, cutSceneData.effectKeyData[m].effectId);
			}
			for (int k = 0; k < 2; k++)
			{
				if (cameras[k] == null)
				{
					CutSceneCamera cutSceneCamera = new CutSceneCamera();
					GameObject cameraObj = new GameObject("cut_scene_camera_ " + k.ToString());
					cutSceneCamera.camera = cameraObj.AddComponent<Camera>();
					cutSceneCamera.transform = cameraObj.transform;
					cutSceneCamera.transform.parent = _transform;
					cameraObj.SetActive(false);
					cameras[k] = cutSceneCamera;
				}
			}
			playerInfo = new PlayerInfo[4];
			enemyInfo = new EnemyInfo();
			actorInfo = new ActorInfo[cutSceneData.actorData.Length];
			effectInfo = new EffectInfo[cutSceneData.effectKeyData.Count];
			seInfo = new SeInfo[cutSceneData.seDataList.Count];
			for (int l = 0; l < cutSceneData.actorData.Length; l++)
			{
				CutSceneData.ActorData actorData = cutSceneData.actorData[l];
				actorInfo[l] = new ActorInfo();
				actorInfo[l].keyData = actorData;
				actorInfo[l].obj = UnityEngine.Object.Instantiate(actorData.prefab);
				actorInfo[l].obj.transform.parent = _transform;
				actorInfo[l].animator = actorInfo[l].obj.GetComponent<Animator>();
				actorInfo[l].animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
				actorInfo[l].animator.runtimeAnimatorController = actorData.animatorController;
				actorInfo[l].animator.Rebind();
				actorInfo[l].obj.SetActive(false);
			}
			for (int j = 0; j < cutSceneData.effectKeyData.Count; j++)
			{
				EffectInfo effect = new EffectInfo
				{
					keyData = cutSceneData.effectKeyData[j]
				};
				effectInfo[j] = effect;
			}
			for (int i = 0; i < cutSceneData.seDataList.Count; i++)
			{
				SeInfo se = new SeInfo
				{
					keyData = cutSceneData.seDataList[i]
				};
				seInfo[i] = se;
			}
			if (loadQueue.IsLoading())
			{
				yield return (object)loadQueue.Wait();
			}
			_onComplete?.Invoke(true);
		}
	}

	private void OnDestroy()
	{
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.gameObject.SetActive(true);
		}
	}

	public void Play(Action _onComplete = null)
	{
		if ((UnityEngine.Object)cutSceneData == (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameMain", base.gameObject, "HOME", null, null, true);
			_onComplete?.Invoke();
		}
		else
		{
			onComplete = _onComplete;
			isPlaying = true;
			cutNo = 0;
			playingTime = 0f;
			oldTime = 0f;
			MonoBehaviourSingleton<SoundManager>.I.TransitionTo(cutSceneData.mixerName, 1f);
			if (cutSceneData.bgm != 0)
			{
				SoundManager.RequestBGM(cutSceneData.bgm, true);
			}
			cameraAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
			cameraAnimator.runtimeAnimatorController = cutSceneData.cameraController;
			cameraAnimator.Play(CUT_STATE_HASH[cutNo]);
			UpdateCamera();
			MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.gameObject.SetActive(false);
			for (int k = 0; k < this.playerInfo.Length; k++)
			{
				this.playerInfo[k] = null;
			}
			for (int l = 0; l < cutSceneData.playerData.Length; l++)
			{
				CutSceneData.PlayerData playerData = cutSceneData.playerData[l];
				if (playerData != null)
				{
					if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
					{
						break;
					}
					StageObjectManager i2 = MonoBehaviourSingleton<StageObjectManager>.I;
					Player player = null;
					if (playerData.type == CutSceneData.PlayerData.TYPE.MY_CHARACTER)
					{
						player = i2.self;
					}
					else
					{
						if (i2.playerList.Count <= (int)playerData.type)
						{
							continue;
						}
						int num = 0;
						for (int m = 0; m < i2.playerList.Count; m++)
						{
							if (!(i2.playerList[m] is Self))
							{
								if ((int)playerData.type <= num)
								{
									player = (i2.playerList[m] as Player);
									break;
								}
								num++;
							}
						}
						if ((UnityEngine.Object)player == (UnityEngine.Object)null)
						{
							for (int n = 0; n < i2.playerList.Count; n++)
							{
								if ((int)playerData.type <= num)
								{
									player = (i2.nonplayerList[n] as Player);
									break;
								}
								num++;
							}
						}
						if ((UnityEngine.Object)player == (UnityEngine.Object)null)
						{
							continue;
						}
					}
					PlayerInfo playerInfo = new PlayerInfo();
					playerInfo.obj = player.gameObject;
					playerInfo.animator = player.animator;
					playerInfo.originalController = player.animator.runtimeAnimatorController;
					playerInfo.keyData = playerData;
					player.ActIdle(false, -1f);
					player._collider.enabled = false;
					player._rigidbody.constraints = RigidbodyConstraints.FreezeAll;
					player._transform.position = playerData.startPos;
					player._transform.rotation = Quaternion.AngleAxis(playerData.startAngleY, Vector3.up);
					player.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
					player.animator.runtimeAnimatorController = playerData.controller;
					player.animator.Rebind();
					this.playerInfo[l] = playerInfo;
				}
			}
			for (int num2 = 0; num2 < this.playerInfo.Length; num2++)
			{
				if (this.playerInfo[num2] != null && !((UnityEngine.Object)this.playerInfo[num2].obj == (UnityEngine.Object)null))
				{
					List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
					int i;
					for (i = 0; i < playerList.Count; i++)
					{
						PlayerInfo playerInfo2 = Array.Find(this.playerInfo, delegate(PlayerInfo info)
						{
							if (info == null)
							{
								return false;
							}
							return (UnityEngine.Object)info.obj == (UnityEngine.Object)playerList[i].gameObject;
						});
						if (playerInfo2 == null)
						{
							playerList[i].gameObject.SetActive(false);
						}
					}
					List<StageObject> npcList = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList;
					int j;
					for (j = 0; j < npcList.Count; j++)
					{
						PlayerInfo playerInfo3 = Array.Find(this.playerInfo, delegate(PlayerInfo info)
						{
							if (info == null)
							{
								return false;
							}
							return (UnityEngine.Object)info.obj == (UnityEngine.Object)npcList[j].gameObject;
						});
						if (playerInfo3 == null)
						{
							npcList[j].gameObject.SetActive(false);
						}
					}
				}
			}
			if (cutSceneData.enemyData != null && MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				CutSceneData.EnemyData enemyData = cutSceneData.enemyData;
				EnemyInfo enemyInfo = new EnemyInfo();
				Debug.Log(enemyData.startPos.y);
				Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
				enemyInfo.obj = boss.gameObject;
				enemyInfo.animator = boss.animator;
				enemyInfo.originalController = boss.animator.runtimeAnimatorController;
				enemyInfo.keyData = enemyData;
				this.enemyInfo = enemyInfo;
				if ((UnityEngine.Object)boss.controller != (UnityEngine.Object)null)
				{
					boss.controller.enabled = false;
					if ((UnityEngine.Object)boss._rigidbody != (UnityEngine.Object)null)
					{
						boss._rigidbody.constraints = RigidbodyConstraints.FreezeAll;
					}
				}
				boss.ActIdle(false, -1f);
				boss.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
				boss.animator.runtimeAnimatorController = enemyData.controller;
				boss.animator.Rebind();
				boss._transform.position = enemyData.startPos;
				boss._transform.rotation = Quaternion.AngleAxis(enemyData.startAngleY, Vector3.up);
				boss.animator.Play(CUT_STATE_HASH[cutNo]);
			}
			for (int num3 = 0; num3 < actorInfo.Length; num3++)
			{
				if (actorInfo[num3] != null)
				{
					actorInfo[num3].obj.SetActive(true);
					bool flag = actorInfo[num3].animator.HasState(0, CUT_STATE_HASH[cutNo]);
					if (flag)
					{
						actorInfo[num3].obj.SetActive(flag);
						CutSceneData.ActorData keyData = actorInfo[num3].keyData;
						Transform parentNode = GetParentNode(keyData.attachmentType, keyData.nodeName);
						if ((UnityEngine.Object)parentNode != (UnityEngine.Object)null)
						{
							actorInfo[num3].obj.transform.parent = parentNode;
						}
						actorInfo[num3].obj.transform.localPosition = keyData.position;
						actorInfo[num3].obj.transform.localRotation = Quaternion.Euler(keyData.rotation);
						SkinnedMeshRenderer[] componentsInChildren = actorInfo[num3].obj.GetComponentsInChildren<SkinnedMeshRenderer>();
						for (int num4 = 0; num4 < componentsInChildren.Length; num4++)
						{
							componentsInChildren[num4].localBounds = new Bounds(Vector3.zero, Vector3.one * 10000f);
						}
						actorInfo[num3].animator.Play(CUT_STATE_HASH[cutNo]);
					}
				}
			}
		}
	}

	private void Update()
	{
		if (isPlaying)
		{
			UpdateCamera();
			oldTime = playingTime;
			playingTime += Time.deltaTime;
			UpdatePlayer();
			UpdateEnemy();
			UpdateActor();
			UpdateSound();
			UpdateEffect();
		}
	}

	private CutSceneCamera GetActiveCamera()
	{
		return cameras[cutNo % 2];
	}

	private void UpdateCamera()
	{
		if (cameraAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			cutNo++;
			if (cameraAnimator.HasState(0, CUT_STATE_HASH[cutNo]))
			{
				cameraAnimator.Play(CUT_STATE_HASH[cutNo]);
			}
			else if (cameraAnimator.HasState(0, ENDING_STATE_ID))
			{
				cameraAnimator.Play(ENDING_STATE_ID);
				EndCutScene();
			}
			else
			{
				EndCutScene();
			}
		}
		int num = cutNo % 2;
		for (int i = 0; i < cameras.Length; i++)
		{
			cameras[i].camera.gameObject.SetActive(num == i);
		}
		CutSceneCamera activeCamera = GetActiveCamera();
		activeCamera.transform.position = cameraAnimatorTransform.position;
		activeCamera.transform.rotation = cameraAnimatorTransform.rotation;
		Camera camera = activeCamera.camera;
		Vector3 localScale = cameraAnimatorTransform.localScale;
		camera.fieldOfView = localScale.x;
	}

	private void EndCutScene()
	{
		if (onComplete != null)
		{
			onComplete();
		}
		isPlaying = false;
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid() && hasStory)
		{
			string text = "MAIN_MENU_HOME";
			if (MonoBehaviourSingleton<LoungeMatchingManager>.IsValid() && MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
			{
				text = "MAIN_MENU_LOUNGE";
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameMain", base.gameObject, "STORY", new object[4]
			{
				cutSceneData.storyId,
				0,
				0,
				text
			}, null, true);
		}
	}

	private void UpdatePlayer()
	{
		for (int i = 0; i < playerInfo.Length; i++)
		{
			if (playerInfo[i] != null && playerInfo[i].animator.GetCurrentAnimatorStateInfo(0).shortNameHash != CUT_STATE_HASH[cutNo])
			{
				playerInfo[i].animator.Play(CUT_STATE_HASH[cutNo]);
			}
		}
	}

	private void UpdateEnemy()
	{
		if (enemyInfo.animator.GetCurrentAnimatorStateInfo(0).shortNameHash != CUT_STATE_HASH[cutNo])
		{
			enemyInfo.animator.Play(CUT_STATE_HASH[cutNo]);
		}
	}

	private void UpdateActor()
	{
		for (int i = 0; i < this.actorInfo.Length; i++)
		{
			ActorInfo actorInfo = this.actorInfo[i];
			if (actorInfo != null)
			{
				if (actorInfo.animator.HasState(0, CUT_STATE_HASH[cutNo]))
				{
					actorInfo.obj.SetActive(true);
					if (actorInfo.animator.GetCurrentAnimatorStateInfo(0).shortNameHash != CUT_STATE_HASH[cutNo])
					{
						actorInfo.animator.Play(CUT_STATE_HASH[cutNo]);
						CutSceneData.ActorData keyData = actorInfo.keyData;
						Transform parentNode = GetParentNode(keyData.attachmentType, keyData.nodeName);
						if ((UnityEngine.Object)parentNode != (UnityEngine.Object)null)
						{
							actorInfo.obj.transform.parent = parentNode;
						}
						actorInfo.obj.transform.localPosition = keyData.position;
						actorInfo.obj.transform.localRotation = Quaternion.Euler(keyData.rotation);
					}
				}
				else
				{
					actorInfo.obj.SetActive(false);
				}
			}
		}
	}

	private void UpdateSound()
	{
		for (int i = 0; i < this.seInfo.Length; i++)
		{
			SeInfo seInfo = this.seInfo[i];
			if (seInfo != null && !seInfo.isPlayed && oldTime <= seInfo.keyData.time && seInfo.keyData.time <= playingTime)
			{
				SoundManager.PlayOneShotUISE(seInfo.keyData.seId);
				seInfo.isPlayed = true;
			}
		}
	}

	private Transform GetPlayerNode(CutSceneData.ATTACHMENT_TYPE attachmentType)
	{
		CutSceneData.PlayerData.TYPE tYPE = CutSceneData.PlayerData.TYPE.MAX_NUM;
		switch (attachmentType)
		{
		case CutSceneData.ATTACHMENT_TYPE.MY_CHARACTER:
			tYPE = CutSceneData.PlayerData.TYPE.MY_CHARACTER;
			break;
		case CutSceneData.ATTACHMENT_TYPE.PLAYER_1:
			tYPE = CutSceneData.PlayerData.TYPE.PLAYER_1;
			break;
		case CutSceneData.ATTACHMENT_TYPE.PLAYER_2:
			tYPE = CutSceneData.PlayerData.TYPE.PLAYER_2;
			break;
		case CutSceneData.ATTACHMENT_TYPE.PLAYER_3:
			tYPE = CutSceneData.PlayerData.TYPE.PLAYER_3;
			break;
		default:
			return null;
		}
		if (playerInfo != null)
		{
			for (int i = 0; i < playerInfo.Length; i++)
			{
				if (playerInfo[i] != null && playerInfo[i].keyData.type == tYPE)
				{
					return playerInfo[i].obj.transform;
				}
			}
		}
		return null;
	}

	private Transform GetActorTransform(int index)
	{
		if (actorInfo != null && index < actorInfo.Length && actorInfo[index] != null && (UnityEngine.Object)actorInfo[index].obj != (UnityEngine.Object)null)
		{
			return actorInfo[index].obj.transform;
		}
		return null;
	}

	private Transform FindChildTransform(Transform root, string name)
	{
		if ((UnityEngine.Object)root == (UnityEngine.Object)null)
		{
			return null;
		}
		int childCount = root.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = root.GetChild(i);
			if (child.name == name)
			{
				return child;
			}
			child = FindChildTransform(child, name);
			if ((UnityEngine.Object)child != (UnityEngine.Object)null)
			{
				return child;
			}
		}
		return null;
	}

	private Transform GetParentNode(CutSceneData.ATTACHMENT_TYPE attachmentType, string nodeName)
	{
		if (attachmentType == CutSceneData.ATTACHMENT_TYPE.NONE)
		{
			return null;
		}
		Transform transform = null;
		switch (attachmentType)
		{
		case CutSceneData.ATTACHMENT_TYPE.CAMERA:
		{
			CutSceneCamera activeCamera = GetActiveCamera();
			transform = activeCamera.transform;
			break;
		}
		case CutSceneData.ATTACHMENT_TYPE.MY_CHARACTER:
		case CutSceneData.ATTACHMENT_TYPE.PLAYER_1:
		case CutSceneData.ATTACHMENT_TYPE.PLAYER_2:
		case CutSceneData.ATTACHMENT_TYPE.PLAYER_3:
			transform = GetPlayerNode(attachmentType);
			break;
		case CutSceneData.ATTACHMENT_TYPE.ENEMY:
			if (enemyInfo != null && (UnityEngine.Object)enemyInfo.obj != (UnityEngine.Object)null)
			{
				transform = enemyInfo.obj.transform;
			}
			break;
		case CutSceneData.ATTACHMENT_TYPE.ACTOR_1:
			transform = GetActorTransform(0);
			break;
		case CutSceneData.ATTACHMENT_TYPE.ACTOR_2:
			transform = GetActorTransform(1);
			break;
		case CutSceneData.ATTACHMENT_TYPE.ACTOR_3:
			transform = GetActorTransform(2);
			break;
		case CutSceneData.ATTACHMENT_TYPE.ACTOR_4:
			transform = GetActorTransform(3);
			break;
		}
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			if (string.IsNullOrEmpty(nodeName))
			{
				return transform;
			}
			return FindChildTransform(transform, nodeName);
		}
		return null;
	}

	private void UpdateEffect()
	{
		for (int i = 0; i < this.effectInfo.Length; i++)
		{
			EffectInfo effectInfo = this.effectInfo[i];
			if (effectInfo != null && !effectInfo.isPlayed && oldTime <= effectInfo.keyData.time && effectInfo.keyData.time <= playingTime)
			{
				Transform parentNode = GetParentNode(effectInfo.keyData.attachmentType, effectInfo.keyData.nodeName);
				Transform effect = EffectManager.GetEffect(effectInfo.keyData.effectId, parentNode);
				if ((UnityEngine.Object)effect != (UnityEngine.Object)null)
				{
					effect.localPosition = effectInfo.keyData.position;
					effect.localRotation = Quaternion.Euler(effectInfo.keyData.rotation);
				}
				effectInfo.isPlayed = true;
			}
		}
	}
}
