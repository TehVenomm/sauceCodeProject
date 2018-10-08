using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePlayer
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
			if (cutSceneData == null)
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

	public CutScenePlayer()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		_transform = this.get_transform();
		GameObject val = new GameObject("CameraController");
		val.get_transform().set_parent(_transform);
		cameraAnimator = val.AddComponent<Animator>();
		cameraAnimatorTransform = cameraAnimator.get_transform();
		for (int i = 0; i < 30; i++)
		{
			CUT_STATE_HASH[i] = Animator.StringToHash("Cut_" + i.ToString("D3"));
		}
		ENDING_STATE_ID = Animator.StringToHash("END");
	}

	public void Init(string cutSceneDataPath, Action<bool> _onComplete)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(InitImpl(cutSceneDataPath, _onComplete));
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
		if (cutSceneData == null)
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
					cutSceneCamera.transform = cameraObj.get_transform();
					cutSceneCamera.transform.set_parent(_transform);
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
				actorInfo[l].obj = Object.Instantiate<GameObject>(actorData.prefab);
				actorInfo[l].obj.get_transform().set_parent(_transform);
				actorInfo[l].animator = actorInfo[l].obj.GetComponent<Animator>();
				actorInfo[l].animator.set_cullingMode(0);
				actorInfo[l].animator.set_runtimeAnimatorController(actorData.animatorController);
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
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform != null)
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_gameObject().SetActive(true);
		}
	}

	public void Play(Action _onComplete = null)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Expected O, but got Unknown
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Expected O, but got Unknown
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Expected O, but got Unknown
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		if (cutSceneData == null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameMain", this.get_gameObject(), "HOME", null, null, true);
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
			cameraAnimator.set_cullingMode(0);
			cameraAnimator.set_runtimeAnimatorController(cutSceneData.cameraController);
			cameraAnimator.Play(CUT_STATE_HASH[cutNo]);
			UpdateCamera();
			MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_gameObject().SetActive(false);
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
						if (player == null)
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
						if (player == null)
						{
							continue;
						}
					}
					PlayerInfo playerInfo = new PlayerInfo();
					playerInfo.obj = player.get_gameObject();
					playerInfo.animator = player.animator;
					playerInfo.originalController = player.animator.get_runtimeAnimatorController();
					playerInfo.keyData = playerData;
					player.ActIdle(false, -1f);
					player._collider.set_enabled(false);
					player._rigidbody.set_constraints(126);
					player._transform.set_position(playerData.startPos);
					player._transform.set_rotation(Quaternion.AngleAxis(playerData.startAngleY, Vector3.get_up()));
					player.animator.set_cullingMode(0);
					player.animator.set_runtimeAnimatorController(playerData.controller);
					player.animator.Rebind();
					this.playerInfo[l] = playerInfo;
				}
			}
			for (int num2 = 0; num2 < this.playerInfo.Length; num2++)
			{
				if (this.playerInfo[num2] != null && !(this.playerInfo[num2].obj == null))
				{
					List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
					int i;
					for (i = 0; i < playerList.Count; i++)
					{
						PlayerInfo playerInfo2 = Array.Find(this.playerInfo, delegate(PlayerInfo info)
						{
							//IL_0024: Unknown result type (might be due to invalid IL or missing references)
							if (info == null)
							{
								return false;
							}
							return info.obj == playerList[i].get_gameObject();
						});
						if (playerInfo2 == null)
						{
							playerList[i].get_gameObject().SetActive(false);
						}
					}
					List<StageObject> npcList = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList;
					int j;
					for (j = 0; j < npcList.Count; j++)
					{
						PlayerInfo playerInfo3 = Array.Find(this.playerInfo, delegate(PlayerInfo info)
						{
							//IL_0024: Unknown result type (might be due to invalid IL or missing references)
							if (info == null)
							{
								return false;
							}
							return info.obj == npcList[j].get_gameObject();
						});
						if (playerInfo3 == null)
						{
							npcList[j].get_gameObject().SetActive(false);
						}
					}
				}
			}
			if (cutSceneData.enemyData != null && MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				CutSceneData.EnemyData enemyData = cutSceneData.enemyData;
				EnemyInfo enemyInfo = new EnemyInfo();
				Debug.Log((object)enemyData.startPos.y);
				Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
				enemyInfo.obj = boss.get_gameObject();
				enemyInfo.animator = boss.animator;
				enemyInfo.originalController = boss.animator.get_runtimeAnimatorController();
				enemyInfo.keyData = enemyData;
				this.enemyInfo = enemyInfo;
				if (boss.controller != null)
				{
					boss.controller.set_enabled(false);
					if (boss._rigidbody != null)
					{
						boss._rigidbody.set_constraints(126);
					}
				}
				boss.ActIdle(false, -1f);
				boss.animator.set_cullingMode(0);
				boss.animator.set_runtimeAnimatorController(enemyData.controller);
				boss.animator.Rebind();
				boss._transform.set_position(enemyData.startPos);
				boss._transform.set_rotation(Quaternion.AngleAxis(enemyData.startAngleY, Vector3.get_up()));
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
						if (parentNode != null)
						{
							actorInfo[num3].obj.get_transform().set_parent(parentNode);
						}
						actorInfo[num3].obj.get_transform().set_localPosition(keyData.position);
						actorInfo[num3].obj.get_transform().set_localRotation(Quaternion.Euler(keyData.rotation));
						SkinnedMeshRenderer[] componentsInChildren = actorInfo[num3].obj.GetComponentsInChildren<SkinnedMeshRenderer>();
						for (int num4 = 0; num4 < componentsInChildren.Length; num4++)
						{
							componentsInChildren[num4].set_localBounds(new Bounds(Vector3.get_zero(), Vector3.get_one() * 10000f));
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
			playingTime += Time.get_deltaTime();
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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		AnimatorStateInfo currentAnimatorStateInfo = cameraAnimator.GetCurrentAnimatorStateInfo(0);
		if (currentAnimatorStateInfo.get_normalizedTime() >= 1f)
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
			cameras[i].camera.get_gameObject().SetActive(num == i);
		}
		CutSceneCamera activeCamera = GetActiveCamera();
		activeCamera.transform.set_position(cameraAnimatorTransform.get_position());
		activeCamera.transform.set_rotation(cameraAnimatorTransform.get_rotation());
		Camera camera = activeCamera.camera;
		Vector3 localScale = cameraAnimatorTransform.get_localScale();
		camera.set_fieldOfView(localScale.x);
	}

	private void EndCutScene()
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
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
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameMain", this.get_gameObject(), "STORY", new object[4]
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
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < playerInfo.Length; i++)
		{
			if (playerInfo[i] != null)
			{
				AnimatorStateInfo currentAnimatorStateInfo = playerInfo[i].animator.GetCurrentAnimatorStateInfo(0);
				if (currentAnimatorStateInfo.get_shortNameHash() != CUT_STATE_HASH[cutNo])
				{
					playerInfo[i].animator.Play(CUT_STATE_HASH[cutNo]);
				}
			}
		}
	}

	private void UpdateEnemy()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		AnimatorStateInfo currentAnimatorStateInfo = enemyInfo.animator.GetCurrentAnimatorStateInfo(0);
		if (currentAnimatorStateInfo.get_shortNameHash() != CUT_STATE_HASH[cutNo])
		{
			enemyInfo.animator.Play(CUT_STATE_HASH[cutNo]);
		}
	}

	private void UpdateActor()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < this.actorInfo.Length; i++)
		{
			ActorInfo actorInfo = this.actorInfo[i];
			if (actorInfo != null)
			{
				if (actorInfo.animator.HasState(0, CUT_STATE_HASH[cutNo]))
				{
					actorInfo.obj.SetActive(true);
					AnimatorStateInfo currentAnimatorStateInfo = actorInfo.animator.GetCurrentAnimatorStateInfo(0);
					if (currentAnimatorStateInfo.get_shortNameHash() != CUT_STATE_HASH[cutNo])
					{
						actorInfo.animator.Play(CUT_STATE_HASH[cutNo]);
						CutSceneData.ActorData keyData = actorInfo.keyData;
						Transform parentNode = GetParentNode(keyData.attachmentType, keyData.nodeName);
						if (parentNode != null)
						{
							actorInfo.obj.get_transform().set_parent(parentNode);
						}
						actorInfo.obj.get_transform().set_localPosition(keyData.position);
						actorInfo.obj.get_transform().set_localRotation(Quaternion.Euler(keyData.rotation));
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
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
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
					return playerInfo[i].obj.get_transform();
				}
			}
		}
		return null;
	}

	private Transform GetActorTransform(int index)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		if (actorInfo != null && index < actorInfo.Length && actorInfo[index] != null && actorInfo[index].obj != null)
		{
			return actorInfo[index].obj.get_transform();
		}
		return null;
	}

	private Transform FindChildTransform(Transform root, string name)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (root == null)
		{
			return null;
		}
		int childCount = root.get_childCount();
		for (int i = 0; i < childCount; i++)
		{
			Transform val = root.GetChild(i);
			if (val.get_name() == name)
			{
				return val;
			}
			val = FindChildTransform(val, name);
			if (val != null)
			{
				return val;
			}
		}
		return null;
	}

	private Transform GetParentNode(CutSceneData.ATTACHMENT_TYPE attachmentType, string nodeName)
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		if (attachmentType == CutSceneData.ATTACHMENT_TYPE.NONE)
		{
			return null;
		}
		Transform val = null;
		switch (attachmentType)
		{
		case CutSceneData.ATTACHMENT_TYPE.CAMERA:
		{
			CutSceneCamera activeCamera = GetActiveCamera();
			val = activeCamera.transform;
			break;
		}
		case CutSceneData.ATTACHMENT_TYPE.MY_CHARACTER:
		case CutSceneData.ATTACHMENT_TYPE.PLAYER_1:
		case CutSceneData.ATTACHMENT_TYPE.PLAYER_2:
		case CutSceneData.ATTACHMENT_TYPE.PLAYER_3:
			val = GetPlayerNode(attachmentType);
			break;
		case CutSceneData.ATTACHMENT_TYPE.ENEMY:
			if (enemyInfo != null && enemyInfo.obj != null)
			{
				val = enemyInfo.obj.get_transform();
			}
			break;
		case CutSceneData.ATTACHMENT_TYPE.ACTOR_1:
			val = GetActorTransform(0);
			break;
		case CutSceneData.ATTACHMENT_TYPE.ACTOR_2:
			val = GetActorTransform(1);
			break;
		case CutSceneData.ATTACHMENT_TYPE.ACTOR_3:
			val = GetActorTransform(2);
			break;
		case CutSceneData.ATTACHMENT_TYPE.ACTOR_4:
			val = GetActorTransform(3);
			break;
		}
		if (val != null)
		{
			if (string.IsNullOrEmpty(nodeName))
			{
				return val;
			}
			return FindChildTransform(val, nodeName);
		}
		return null;
	}

	private void UpdateEffect()
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < this.effectInfo.Length; i++)
		{
			EffectInfo effectInfo = this.effectInfo[i];
			if (effectInfo != null && !effectInfo.isPlayed && oldTime <= effectInfo.keyData.time && effectInfo.keyData.time <= playingTime)
			{
				Transform parentNode = GetParentNode(effectInfo.keyData.attachmentType, effectInfo.keyData.nodeName);
				Transform effect = EffectManager.GetEffect(effectInfo.keyData.effectId, parentNode);
				if (effect != null)
				{
					effect.set_localPosition(effectInfo.keyData.position);
					effect.set_localRotation(Quaternion.Euler(effectInfo.keyData.rotation));
				}
				effectInfo.isPlayed = true;
			}
		}
	}
}
