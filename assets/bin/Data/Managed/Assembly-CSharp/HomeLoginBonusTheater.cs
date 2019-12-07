using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeLoginBonusTheater : GameSection
{
	public enum AUDIO
	{
		SE_TOP = 40000040,
		SE_FIRE
	}

	public enum VOICE
	{
		PAMERA_GREET_0 = 14,
		PAMERA_GREET_1 = 0xF,
		PAMERA_GREET_2 = 0x10,
		PAMERA_GREET_3 = 19,
		PAMERA_CHEER_0 = 301,
		PAMERA_CHEER_1 = 302,
		PAMERA_CHEER_2 = 303
	}

	private class FSMInfo
	{
		public float deltaTime;

		public HomeNPCCharacter npc00;

		public HomeNPCCharacter npc06;

		public Transform light;

		public Transform dragonJaw;

		public Transform fireball;

		public Transform fireEffect1;

		public Transform fireEffect2;

		public Transform itemModel;

		public Vector3 fireballStartPos = Vector3.zero;

		public Vector3 fireballEndPos = Vector3.zero;

		public int dayIndex;

		public Material todayPaper;

		public Material todayPanel;

		public Material todayPanel2;

		public TransformInterpolator interpolator;

		public AnimationCurve moveCurve;

		public AnimationCurve scaleCurve;

		public bool goNextMain;

		public bool goNextNpc00;

		public bool goNextNpc00Facial;

		public bool goNextBoard;

		public bool goNextNpc06;

		public bool goNextFireball;

		public bool goNextCamera;

		public Vector3 previousNPC00Position;

		public Quaternion previousNPC00Rotation;
	}

	private abstract class FSM
	{
		protected delegate bool Act();

		protected Act act_;

		protected float waitTime_;

		protected FSMInfo info_;

		public virtual void Init(FSMInfo info)
		{
			info_ = info;
		}

		public bool DoAction()
		{
			waitTime_ -= info_.deltaTime;
			if (act_ != null)
			{
				return act_();
			}
			return false;
		}

		protected bool IsWaitComplete()
		{
			return 0f >= waitTime_;
		}

		protected void StartTimer(float time)
		{
			waitTime_ = time;
		}

		protected virtual bool PhaseExit()
		{
			return false;
		}

		protected void ToExit()
		{
			act_ = PhaseExit;
		}
	}

	private class FSMNpc00 : FSM
	{
		private HomeNPCCharacter npc00_;

		private PlayerAnimCtrl npc00AnimCtrl_;

		private Action<PlayerAnimCtrl, PLCA> tempAction_;

		private Vector3 previousNPC00Position_;

		private Quaternion previousNPC00Rotation_;

		public override void Init(FSMInfo info)
		{
			base.Init(info);
			act_ = Phase00;
			npc00_ = info.npc00;
			npc00AnimCtrl_ = npc00_.gameObject.GetComponentInChildren<PlayerAnimCtrl>();
			tempAction_ = npc00AnimCtrl_.onEnd;
			npc00AnimCtrl_.onEnd = null;
			npc00AnimCtrl_.Play(PLCA.BOW);
			previousNPC00Position_ = info.previousNPC00Position;
			previousNPC00Rotation_ = info.previousNPC00Rotation;
			PlayRandomVoice(voiceGreetings);
		}

		private bool Phase00()
		{
			if (npc00AnimCtrl_.IsPlayingIdleAnims(0))
			{
				info_.goNextCamera = true;
				act_ = Phase01;
			}
			return true;
		}

		private bool Phase01()
		{
			if (info_.goNextNpc00)
			{
				info_.goNextNpc00 = false;
				StartTimer(0.15f);
				act_ = Phase02;
			}
			return true;
		}

		private bool Phase02()
		{
			if (IsWaitComplete())
			{
				npc00AnimCtrl_.Play(PLCA.HAPPY);
				StartTimer(3.5f);
				act_ = Phase03;
			}
			return true;
		}

		private bool Phase03()
		{
			if (npc00AnimCtrl_.IsCurrentState(PLCA.HAPPY) || IsWaitComplete())
			{
				npc00AnimCtrl_.onEnd = tempAction_;
				info_.goNextNpc00Facial = true;
				act_ = Phase04;
				StartTimer(2f);
			}
			return true;
		}

		private bool Phase04()
		{
			if (IsWaitComplete())
			{
				info_.goNextMain = true;
				act_ = Phase99;
			}
			return true;
		}

		private bool Phase99()
		{
			if (info_.goNextNpc00)
			{
				info_.goNextNpc00 = false;
				npc00_.defaultPosition = previousNPC00Position_;
				npc00_.defaultRotation = previousNPC00Rotation_;
				npc00_.PopState();
				npc00_.PushBackPosition();
				ToExit();
			}
			return true;
		}
	}

	private class FSMNpc00Facial : FSM
	{
		private NPCFacial facial_;

		public override void Init(FSMInfo info)
		{
			base.Init(info);
			act_ = Phase00;
			facial_ = info.npc00.gameObject.GetComponentInChildren<NPCFacial>();
		}

		private bool Phase00()
		{
			if (info_.goNextNpc00Facial)
			{
				info_.goNextNpc00Facial = false;
				facial_.eyeType = NPCFacial.TYPE.JOY;
				facial_.mouthType = NPCFacial.TYPE.JOY;
				act_ = Phase01;
				StartTimer(2f);
			}
			return true;
		}

		private bool Phase01()
		{
			if (IsWaitComplete())
			{
				facial_.eyeType = NPCFacial.TYPE.NORMAL;
				facial_.mouthType = NPCFacial.TYPE.NORMAL;
				ToExit();
			}
			return true;
		}
	}

	private class FSMNpc06 : FSM
	{
		private HomeNPCCharacter npc06_;

		private PlayerAnimCtrl npc06AnimCtrl_;

		private Transform jaw_;

		private PLCA tempDefaultAnim_;

		private Action<PlayerAnimCtrl, PLCA> tempAction_;

		public override void Init(FSMInfo info)
		{
			base.Init(info);
			act_ = Phase00;
			npc06_ = info.npc06;
			npc06AnimCtrl_ = npc06_.gameObject.GetComponentInChildren<PlayerAnimCtrl>();
			jaw_ = Utility.Find(npc06_._transform, "Jaw");
			info_.dragonJaw = jaw_;
			tempDefaultAnim_ = npc06AnimCtrl_.defaultAnim;
			npc06AnimCtrl_.defaultAnim = PLCA.LOGIN_IDLE;
			tempAction_ = npc06AnimCtrl_.onEnd;
			npc06AnimCtrl_.onEnd = null;
		}

		private bool Phase00()
		{
			if (info_.goNextNpc06)
			{
				info_.goNextNpc06 = false;
				npc06AnimCtrl_.Play(PLCA.LOGIN_FIRE);
				act_ = Phase01;
				info_.goNextFireball = true;
			}
			return true;
		}

		private bool Phase01()
		{
			if (info_.goNextNpc06)
			{
				info_.goNextNpc06 = false;
				npc06AnimCtrl_.defaultAnim = tempDefaultAnim_;
				npc06AnimCtrl_.onEnd = tempAction_;
				npc06AnimCtrl_.moveAnim = PLCA.LOGIN_FLY;
				npc06AnimCtrl_.Play(PLCA.LOGIN_FLY);
				npc06_.PopState();
				npc06_.PushLeave();
				ToExit();
			}
			return true;
		}
	}

	private class FSMFireball : FSM
	{
		private Transform fireball_;

		private Transform fireEffect1_;

		private Transform fireEffect2_;

		private Vector3 endPos_ = Vector3.zero;

		private Vector3 dir_ = Vector3.up;

		private float velocity_ = 0.098f;

		private Vector3 position_;

		public override void Init(FSMInfo info)
		{
			base.Init(info);
			act_ = Phase00;
			fireball_ = info.fireball;
			fireEffect1_ = info.fireEffect1;
			fireEffect2_ = info.fireEffect2;
			endPos_ = info.fireballEndPos;
			fireEffect1_.position = endPos_;
			fireEffect2_.position = endPos_;
		}

		private bool Phase00()
		{
			if (info_.goNextFireball)
			{
				info_.goNextFireball = false;
				float time = 1.06f;
				if (info_.dayIndex % 3 == 1)
				{
					time = 0.99f;
				}
				else if (info_.dayIndex % 3 == 2)
				{
					time = 0.9f;
				}
				StartTimer(time);
				act_ = Phase01;
			}
			return true;
		}

		private bool Phase01()
		{
			if (IsWaitComplete())
			{
				fireball_.gameObject.SetActive(value: true);
				position_ = info_.dragonJaw.TransformPoint(new Vector3(-0.12f, 0f, 0.01f));
				dir_ = endPos_ - position_;
				dir_.Normalize();
				fireball_.position = position_;
				act_ = Phase02;
				PlayAudio(AUDIO.SE_FIRE);
			}
			return true;
		}

		private bool Phase02()
		{
			position_ += dir_ * velocity_;
			fireball_.position = position_;
			Vector3 lhs = endPos_ - position_;
			lhs.Normalize();
			if (0.8f >= Vector3.Dot(lhs, dir_))
			{
				position_ = endPos_;
				fireball_.position = endPos_;
				rymFX component = fireball_.gameObject.GetComponent<rymFX>();
				if (component != null)
				{
					component.AutoDelete = true;
					component.LoopEnd = true;
				}
				fireEffect1_.gameObject.SetActive(value: true);
				fireEffect2_.gameObject.SetActive(value: true);
				info_.goNextBoard = true;
				info_.goNextNpc00 = true;
				info_.goNextBoard = true;
				StartTimer(0.5f);
				act_ = Phase03;
			}
			return true;
		}

		private bool Phase03()
		{
			if (IsWaitComplete())
			{
				rymFX component = fireEffect2_.gameObject.GetComponent<rymFX>();
				if (null != component)
				{
					component.AutoDelete = true;
					component.LoopEnd = true;
				}
				ToExit();
			}
			return true;
		}
	}

	private class FSMBoard : FSM
	{
		private Material paperMat_;

		private Material panelMat_;

		private Material panel2Mat_;

		private Transform light_;

		private float offset_;

		private float speed_;

		private Vector3 itemEndPos_ = new Vector3(-0.004f, 1.431f, 7.3f);

		private float itemEndScale_ = 0.4128781f;

		private Vector3 moveDir_ = Vector3.up;

		private Vector3 movePos_ = Vector3.zero;

		private Vector3 lightPosOffset_ = new Vector3(0f, 0f, 0.2f);

		private float moveScale_ = 1f;

		private Transform itemModel_;

		private int animFrame_ = 17;

		private float scaleVelocity_;

		private float itemMoveTime = 0.84f;

		private Vector3 itemInitPos;

		private float itemStartScale;

		private float phase3Time;

		private float endRotation = 720f;

		private float rotation;

		public override void Init(FSMInfo info)
		{
			base.Init(info);
			act_ = Phase00;
			paperMat_ = info.todayPaper;
			panelMat_ = info.todayPanel;
			panel2Mat_ = info.todayPanel2;
			light_ = info.light;
			itemModel_ = info.itemModel;
		}

		private bool Phase00()
		{
			if (info_.goNextBoard)
			{
				info_.goNextBoard = false;
				act_ = Phase01;
				offset_ = 0f;
				speed_ = 0.2f;
				itemModel_.gameObject.SetActive(value: true);
				itemModel_.position = info_.fireballEndPos;
				PlayAudio(AUDIO.SE_TOP);
				movePos_ = info_.fireballEndPos;
				light_.position = movePos_ + lightPosOffset_;
				itemInitPos = itemModel_.position;
				itemStartScale = itemModel_.localScale.x;
			}
			return true;
		}

		private bool Phase01()
		{
			offset_ += speed_;
			paperMat_.SetFloat("_Offset", offset_);
			panelMat_.SetFloat("_Offset", offset_);
			panel2Mat_.SetFloat("_Offset", offset_);
			if (offset_ > 0.5f)
			{
				Phase03();
			}
			if (1f < offset_)
			{
				act_ = Phase03;
			}
			return true;
		}

		private bool Phase02()
		{
			if (IsWaitComplete())
			{
				act_ = Phase03;
				scaleVelocity_ = (itemEndScale_ - itemModel_.localScale.x) / (float)animFrame_;
				movePos_ = info_.fireballEndPos;
				moveScale_ = itemModel_.localScale.x;
				itemModel_.position = movePos_;
				light_.position = movePos_ + lightPosOffset_;
				moveDir_ = itemEndPos_ - movePos_;
				moveDir_.Normalize();
				light_.gameObject.SetActive(value: true);
				itemInitPos = itemModel_.position;
				itemStartScale = itemModel_.localScale.x;
			}
			return true;
		}

		private bool Phase03()
		{
			phase3Time += Time.deltaTime;
			float num = phase3Time / itemMoveTime;
			float t = info_.moveCurve.Evaluate(num);
			float num2 = info_.scaleCurve.Evaluate(num);
			movePos_ = Vector3.Lerp(itemInitPos, itemEndPos_, t);
			moveScale_ = (itemEndScale_ - itemStartScale) * num2 + itemStartScale;
			rotation = endRotation * num;
			if (phase3Time > itemMoveTime)
			{
				light_.gameObject.SetActive(value: true);
				act_ = Phase05;
				movePos_ = itemEndPos_;
				moveScale_ = itemEndScale_;
				rotation = endRotation;
			}
			itemModel_.position = movePos_;
			itemModel_.localScale = new Vector3(moveScale_, moveScale_, moveScale_);
			itemModel_.localRotation = Quaternion.AngleAxis(rotation, Vector3.up);
			light_.position = movePos_ + lightPosOffset_;
			return true;
		}

		private bool Phase04()
		{
			moveScale_ += scaleVelocity_;
			if (moveScale_ >= itemEndScale_)
			{
				moveScale_ = itemEndScale_;
				act_ = Phase05;
			}
			itemModel_.localScale = new Vector3(moveScale_, moveScale_, moveScale_);
			return true;
		}

		private bool Phase05()
		{
			if (info_.goNextBoard)
			{
				itemModel_.gameObject.SetActive(value: false);
				light_.gameObject.SetActive(value: false);
				ToExit();
			}
			return true;
		}
	}

	private class FSMCamera : FSM
	{
		public override void Init(FSMInfo info)
		{
			base.Init(info);
			act_ = Phase00;
		}

		private bool Phase00()
		{
			if (info_.goNextCamera)
			{
				info_.goNextCamera = false;
				act_ = Phase01;
				OutGameSettingsManager.LoginBonusScene loginBonusScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.loginBonusScene;
				Vector3 cameraPos = loginBonusScene.cameraPos;
				Vector3 cameraRot = loginBonusScene.cameraRot;
				info_.interpolator.Translate(0.7f, cameraPos);
				info_.interpolator.Rotate(0.7f, cameraRot);
			}
			return true;
		}

		private bool Phase01()
		{
			if (!info_.interpolator.IsPlaying())
			{
				info_.goNextNpc06 = true;
				ToExit();
			}
			return true;
		}
	}

	public static readonly VOICE[] voiceGreetings = new VOICE[4]
	{
		VOICE.PAMERA_GREET_0,
		VOICE.PAMERA_GREET_1,
		VOICE.PAMERA_GREET_2,
		VOICE.PAMERA_GREET_3
	};

	public static readonly VOICE[] voiceCheers = new VOICE[3]
	{
		VOICE.PAMERA_CHEER_0,
		VOICE.PAMERA_CHEER_1,
		VOICE.PAMERA_CHEER_2
	};

	private List<FSM> fsmList_ = new List<FSM>();

	private FSMInfo fsmInfo_ = new FSMInfo();

	private Action mainAction_;

	private float waitTime_;

	private Camera homeCamera_;

	private bool isMoveEndCamera_;

	private Transform board_;

	private Transform light_;

	private Transform fireball_;

	private Transform itemModel_;

	private Transform itemLoader_;

	private Transform fireEffect1_;

	private Transform fireEffect2_;

	private TransformInterpolator interpolator_;

	private float homeFieldOfView_;

	private float fovSpeed_ = 1.5f;

	private Vector3 previousCameraPosition = Vector3.zero;

	private Quaternion previousCameraRotation = Quaternion.identity;

	private Vector3 previousNPC00Position = Vector3.zero;

	private Quaternion previousNPC00Rotation = Quaternion.identity;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "ItemTable";
		}
	}

	public void PreInitialize()
	{
		HomeCamera homeCamera = null;
		HomeNPCCharacter homeNPCCharacter = null;
		HomeNPCCharacter homeNPCCharacter2 = null;
		HomeSelfCharacter homeSelfCharacter = null;
		new List<HomeCharacterBase>();
		IHomeManager currentIHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
		if (currentIHomeManager != null)
		{
			homeCamera = currentIHomeManager.HomeCamera;
			homeNPCCharacter = currentIHomeManager.IHomePeople.GetHomeNPCCharacter(0);
			homeNPCCharacter2 = currentIHomeManager.IHomePeople.GetHomeNPCCharacter(6);
			homeSelfCharacter = currentIHomeManager.IHomePeople.selfChara;
			List<HomeCharacterBase> charas = currentIHomeManager.IHomePeople.charas;
			HomeSelfCharacter.CTRL = false;
			OutGameSettingsManager.LoginBonusScene loginBonusScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.loginBonusScene;
			Vector3 npc00CameraPos = loginBonusScene.npc00CameraPos;
			Quaternion localRotation = Quaternion.Euler(loginBonusScene.npc00CameraRot);
			previousCameraPosition = homeCamera.targetCamera.transform.position;
			previousCameraRotation = homeCamera.targetCamera.transform.rotation;
			homeCamera.targetCamera.transform.localPosition = npc00CameraPos;
			homeCamera.targetCamera.transform.localRotation = localRotation;
			homeCamera.targetCamera.fieldOfView = MonoBehaviourSingleton<OutGameSettingsManager>.I.loginBonusScene.cameraFov;
			if (null != homeNPCCharacter)
			{
				Transform transform = homeNPCCharacter.transform;
				previousNPC00Position = transform.position;
				previousNPC00Rotation = transform.rotation;
				Vector3 npc00Pos = loginBonusScene.npc00Pos;
				Quaternion rotation = Quaternion.Euler(loginBonusScene.npc00Rot);
				transform.position = npc00Pos;
				transform.rotation = rotation;
				homeNPCCharacter.PushOutControll();
			}
			if (null != homeNPCCharacter2)
			{
				Transform transform2 = homeNPCCharacter2.transform;
				Vector3 npc06Pos = loginBonusScene.npc06Pos;
				Quaternion rotation2 = Quaternion.Euler(loginBonusScene.npc06Rot);
				transform2.position = npc06Pos;
				transform2.rotation = rotation2;
				homeNPCCharacter2.PushOutControll();
				homeNPCCharacter2.gameObject.GetComponentInChildren<PlayerAnimCtrl>().Play(PLCA.LOGIN_IDLE);
				homeNPCCharacter2.HideShadow();
			}
			if (null != homeSelfCharacter)
			{
				homeSelfCharacter.gameObject.SetActive(value: false);
			}
			charas.ForEach(delegate(HomeCharacterBase o)
			{
				if (o is HomePlayerCharacter || o is LoungePlayer)
				{
					o.gameObject.SetActive(value: false);
					Transform namePlate = o.GetNamePlate();
					if (null != namePlate)
					{
						o.GetNamePlate().gameObject.SetActive(value: false);
					}
				}
			});
		}
	}

	public override void Initialize()
	{
		PreInitialize();
		StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		IHomeManager currentIHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
		HomeCamera homeCamera = currentIHomeManager.HomeCamera;
		HomeNPCCharacter npc7 = currentIHomeManager.IHomePeople.GetHomeNPCCharacter(0);
		HomeNPCCharacter npc6 = currentIHomeManager.IHomePeople.GetHomeNPCCharacter(6);
		MonoBehaviourSingleton<AccountManager>.I.DisplayLogInBonusSection();
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject boardLO = loadQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, "LIB_00000001");
		LoadObject lightLO = loadQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, "LIB_00000002");
		LoadObject fireballLO = loadQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_dragon_breath_01");
		LoadObject fireEffect1LO = loadQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_damage_slash_fire_01");
		LoadObject fireEffect2LO = loadQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_damage_fire_01");
		int[] array = (int[])Enum.GetValues(typeof(VOICE));
		foreach (int voice_id in array)
		{
			loadQueue.CacheVoice(voice_id);
		}
		array = (int[])Enum.GetValues(typeof(AUDIO));
		foreach (int se_id in array)
		{
			loadQueue.CacheSE(se_id);
		}
		LoginBonus bonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus.Find((LoginBonus obj) => obj.type == 0);
		if (bonus == null)
		{
			yield break;
		}
		List<LoginBonus.NextReward> next = bonus.next;
		if (next == null)
		{
			yield break;
		}
		LoadObject[] itemIconLOs = new LoadObject[9];
		LoadObject[] itemBGIconLOs = new LoadObject[9];
		string iconName = "";
		string iconBGName = "";
		GetIconName(bonus.reward[0], out iconName, out iconBGName);
		itemIconLOs[bonus.nowCount - 1] = loadQueue.LoadItemIcon(iconName);
		if (string.Empty != iconBGName)
		{
			itemBGIconLOs[bonus.nowCount - 1] = loadQueue.LoadItemIcon(iconBGName);
		}
		next.ForEach(delegate(LoginBonus.NextReward o)
		{
			if (0 < o.reward.Count && 0 < o.count && 9 >= o.count)
			{
				GetIconName(o.reward[0], out iconName, out iconBGName);
				itemIconLOs[o.count - 1] = loadQueue.LoadItemIcon(iconName);
				if (string.Empty != iconBGName)
				{
					itemBGIconLOs[o.count - 1] = loadQueue.LoadItemIcon(iconBGName);
				}
			}
		});
		itemLoader_ = Utility.CreateGameObject("ItemLoader", MonoBehaviourSingleton<AppMain>.I._transform);
		ItemLoader loader = itemLoader_.gameObject.AddComponent<ItemLoader>();
		uint itemModelID = GetItemModelID((REWARD_TYPE)bonus.reward[0].type, bonus.reward[0].itemId);
		loader.LoadItem(itemModelID, itemModel_, 0);
		while (loader.isLoading)
		{
			yield return null;
		}
		itemModel_ = Utility.CreateGameObject("ItemModel", MonoBehaviourSingleton<AppMain>.I._transform);
		loader.nodeMain.SetParent(itemModel_);
		itemModel_.gameObject.SetActive(value: false);
		float num = 0.16f;
		itemModel_.localScale = new Vector3(num, num, num);
		homeCamera_ = homeCamera.targetCamera;
		interpolator_ = homeCamera_.gameObject.GetComponent<TransformInterpolator>();
		if (null == interpolator_)
		{
			interpolator_ = homeCamera_.gameObject.AddComponent<TransformInterpolator>();
		}
		homeCamera_.fieldOfView = MonoBehaviourSingleton<OutGameSettingsManager>.I.loginBonusScene.cameraFov;
		homeFieldOfView_ = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.outGameFieldOfView;
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		Transform parent = Utility.Find(npc6._transform, "Move");
		board_ = ResourceUtility.Realizes(boardLO.loadedObject, parent);
		light_ = ResourceUtility.Realizes(lightLO.loadedObject, npc6._transform);
		light_.gameObject.SetActive(value: false);
		fireball_ = ResourceUtility.Realizes(fireballLO.loadedObject, npc6._transform);
		fireball_.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		fireball_.gameObject.SetActive(value: false);
		fireEffect1_ = ResourceUtility.Realizes(fireEffect1LO.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform);
		fireEffect1_.localScale = new Vector3(0.18f, 0.18f, 0.18f);
		fireEffect1_.gameObject.SetActive(value: false);
		fireEffect2_ = ResourceUtility.Realizes(fireEffect2LO.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform);
		fireEffect2_.localScale = new Vector3(0.25f, 0.25f, 0.25f);
		fireEffect2_.gameObject.SetActive(value: false);
		Material[] array2 = new Material[9];
		Transform[] array3 = new Transform[9];
		Material[] array4 = new Material[9];
		Transform[] array5 = new Transform[9];
		Renderer[] array6 = new Renderer[9];
		Transform transform = board_.Find("Day_set");
		if (null != transform)
		{
			for (int j = 0; j <= 8; j++)
			{
				string text = "Day" + (j + 1).ToString();
				Transform transform2 = transform.Find(text + "/" + text + "_panel");
				if (null == transform2)
				{
					continue;
				}
				Renderer component = transform2.GetComponent<Renderer>();
				if (null != component)
				{
					array2[j] = component.material;
				}
				array3[j] = transform2;
				Transform transform3 = transform.Find(text + "/" + text + "_panel2");
				if (!(null == transform2))
				{
					Renderer component2 = transform3.GetComponent<Renderer>();
					if (null != component)
					{
						array4[j] = component2.material;
					}
					array5[j] = transform3;
					Transform transform4 = transform.Find(text + "/" + text + "_paper");
					if (!(null == transform4))
					{
						array6[j] = transform4.gameObject.GetComponent<Renderer>();
					}
				}
			}
		}
		Texture[] array7 = new Texture[9];
		for (int k = 0; k < itemIconLOs.Length; k++)
		{
			if (itemIconLOs[k] != null)
			{
				array7[k] = (itemIconLOs[k].loadedObject as Texture);
			}
		}
		Texture[] array8 = new Texture[9];
		for (int l = 0; l < itemBGIconLOs.Length; l++)
		{
			if (itemBGIconLOs[l] != null)
			{
				array8[l] = (itemBGIconLOs[l].loadedObject as Texture);
			}
		}
		int m;
		for (m = 0; m < bonus.nowCount - 1; m++)
		{
			array3[m].gameObject.SetActive(value: false);
			array5[m].gameObject.SetActive(value: false);
			array6[m].material.SetFloat("_Offset", 1f);
		}
		for (; m < 9; m++)
		{
			if (null != array8[m])
			{
				array2[m].mainTexture = array8[m];
				array4[m].mainTexture = array7[m];
			}
			else
			{
				array2[m].mainTexture = array7[m];
			}
		}
		fsmInfo_.npc00 = npc7;
		fsmInfo_.npc06 = npc6;
		fsmInfo_.light = light_;
		fsmInfo_.fireball = fireball_;
		fsmInfo_.fireEffect1 = fireEffect1_;
		fsmInfo_.fireEffect2 = fireEffect2_;
		fsmInfo_.itemModel = itemModel_;
		fsmInfo_.fireballEndPos = array3[bonus.nowCount - 1].position;
		fsmInfo_.fireballEndPos.z -= 0.08f;
		fsmInfo_.dayIndex = bonus.nowCount - 1;
		fsmInfo_.todayPanel = array2[bonus.nowCount - 1];
		fsmInfo_.todayPanel2 = array4[bonus.nowCount - 1];
		fsmInfo_.todayPaper = array6[bonus.nowCount - 1].material;
		fsmInfo_.interpolator = interpolator_;
		fsmInfo_.moveCurve = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.loginBonusMoveCureve;
		fsmInfo_.scaleCurve = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.loginBonusScaleCureve;
		fsmInfo_.previousNPC00Position = previousNPC00Position;
		fsmInfo_.previousNPC00Rotation = previousNPC00Rotation;
		fsmList_.Add(new FSMNpc00());
		fsmList_.Add(new FSMNpc00Facial());
		fsmList_.Add(new FSMNpc06());
		fsmList_.Add(new FSMBoard());
		fsmList_.Add(new FSMFireball());
		fsmList_.Add(new FSMCamera());
		fsmList_.ForEach(delegate(FSM o)
		{
			o.Init(fsmInfo_);
		});
		mainAction_ = Phase00;
		base.Initialize();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (null != light_)
		{
			UnityEngine.Object.Destroy(light_.gameObject);
			light_ = null;
		}
		if (null != fireball_)
		{
			UnityEngine.Object.Destroy(fireball_.gameObject);
			fireball_ = null;
		}
		if (null != itemModel_)
		{
			UnityEngine.Object.Destroy(itemModel_.gameObject);
			itemModel_ = null;
		}
		if (null != itemLoader_)
		{
			UnityEngine.Object.Destroy(itemLoader_.gameObject);
			itemLoader_ = null;
		}
	}

	private void Update()
	{
		fsmInfo_.deltaTime = Time.deltaTime;
		waitTime_ -= fsmInfo_.deltaTime;
		List<FSM> removeList = new List<FSM>();
		fsmList_.ForEach(delegate(FSM o)
		{
			if (!o.DoAction())
			{
				removeList.Add(o);
			}
		});
		removeList.ForEach(delegate(FSM o)
		{
			fsmList_.Remove(o);
		});
		if (mainAction_ != null)
		{
			mainAction_();
		}
	}

	private void Phase00()
	{
		if (fsmInfo_.goNextMain)
		{
			fsmInfo_.goNextMain = false;
			DispatchEvent("NOTICE");
			mainAction_ = Phase01;
			StartTimer(0.5f);
		}
	}

	private void Phase01()
	{
		if (isWaitComplete())
		{
			fsmInfo_.goNextBoard = true;
			mainAction_ = Phase02;
		}
	}

	private void Phase02()
	{
		if (fsmInfo_.goNextMain)
		{
			fsmInfo_.goNextMain = false;
			if (null != interpolator_)
			{
				interpolator_.Translate(1.3f, previousCameraPosition);
				interpolator_.Rotate(1.3f, previousCameraRotation.eulerAngles);
			}
			if (null != light_)
			{
				light_.gameObject.SetActive(value: false);
			}
			mainAction_ = Phase03;
			StartTimer(0.3f);
			fsmInfo_.goNextNpc00 = true;
			fsmInfo_.goNextNpc06 = true;
		}
	}

	private void Phase03()
	{
		float fieldOfView = homeCamera_.fieldOfView;
		fieldOfView += fovSpeed_;
		if (fieldOfView >= homeFieldOfView_)
		{
			fieldOfView = homeFieldOfView_;
		}
		homeCamera_.fieldOfView = fieldOfView;
		if (isWaitComplete())
		{
			HomeSelfCharacter homeSelfCharacter = null;
			homeSelfCharacter = GameSceneGlobalSettings.GetCurrentIHomeManager().IHomePeople.selfChara;
			if (null != homeSelfCharacter)
			{
				homeSelfCharacter.gameObject.SetActive(value: true);
			}
			mainAction_ = Phase04;
		}
	}

	private void Phase04()
	{
		float fieldOfView = homeCamera_.fieldOfView;
		fieldOfView += fovSpeed_;
		if (fieldOfView >= homeFieldOfView_)
		{
			fieldOfView = homeFieldOfView_;
		}
		homeCamera_.fieldOfView = fieldOfView;
		if (null != homeCamera_ && !isMoveEndCamera_ && !interpolator_.IsPlaying())
		{
			isMoveEndCamera_ = true;
		}
		if (isMoveEndCamera_ && CanChangeScene())
		{
			GameSection.BackSection();
			HomeSelfCharacter.CTRL = true;
			new List<HomeCharacterBase>();
			GameSceneGlobalSettings.GetCurrentIHomeManager().IHomePeople.charas.ForEach(delegate(HomeCharacterBase o)
			{
				if (o is HomePlayerCharacter || o is LoungePlayer)
				{
					o.gameObject.SetActive(value: true);
					Transform namePlate = o.GetNamePlate();
					if (null != namePlate)
					{
						o.GetNamePlate().gameObject.SetActive(value: true);
					}
				}
			});
			mainAction_ = null;
		}
	}

	private void StartTimer(float t)
	{
		waitTime_ = t;
	}

	private bool isWaitComplete()
	{
		return 0f > waitTime_;
	}

	private void OnCloseDialog(string close_section_name)
	{
		if (MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count <= 0)
		{
			fsmInfo_.goNextMain = true;
		}
	}

	private bool CanChangeScene()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			return !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing;
		}
		return false;
	}

	private static int GetIconBGID(ITEM_ICON_TYPE icon_type, int icon_id)
	{
		int result = -1;
		if ((uint)(icon_type - 10) <= 4u)
		{
			result = ItemIcon.GetIconBGID(icon_type, icon_id, null);
		}
		return result;
	}

	private static uint GetItemModelID(REWARD_TYPE type, int itemID)
	{
		uint result = uint.MaxValue;
		switch (type)
		{
		case REWARD_TYPE.CRYSTAL:
			result = 1u;
			break;
		case REWARD_TYPE.MONEY:
			result = 2u;
			break;
		case REWARD_TYPE.ITEM:
			result = (uint)itemID;
			break;
		}
		return result;
	}

	private static void GetIconName(LoginBonus.LoginBonusReward reward, out string iconName, out string iconBGName)
	{
		ITEM_ICON_TYPE icon_type = ITEM_ICON_TYPE.NONE;
		RARITY_TYPE? rarity = null;
		ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
		ELEMENT_TYPE element2 = ELEMENT_TYPE.MAX;
		ItemIcon.GetIconShowData((REWARD_TYPE)reward.type, (uint)reward.itemId, out int icon_id, out icon_type, out rarity, out element, out element2, out EQUIPMENT_TYPE? _, out int _, out int _, out GET_TYPE _);
		if (icon_type == ITEM_ICON_TYPE.ACCESSORY)
		{
			iconName = ResourceName.GetAccessoryIcon(icon_id);
		}
		else
		{
			iconName = ResourceName.GetItemIcon(icon_id);
		}
		int iconBGID = GetIconBGID(icon_type, icon_id);
		iconBGName = ResourceName.GetItemIcon(iconBGID);
	}

	public static void PlayAudio(AUDIO audio)
	{
		SoundManager.PlayOneShotSE((int)audio);
	}

	public static void PlayRandomVoice(VOICE[] voiceList)
	{
		int num = voiceList.Length;
		if (num >= 1)
		{
			int num2 = Utility.Random(num);
			SoundManager.PlayVoice((int)voiceList[num2]);
		}
	}
}
