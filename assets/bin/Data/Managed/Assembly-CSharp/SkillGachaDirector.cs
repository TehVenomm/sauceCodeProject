using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGachaDirector : AnimationDirector
{
	public interface ISectionCommand
	{
		void OnShowRarity(RARITY_TYPE rarity);

		void OnHideRarity();

		void OnShowSkillModel(uint skill_item_id);

		void OnHideSkillModel();

		void OnEnd();
	}

	public enum AUDIO
	{
		RARITY_01 = 40000126,
		RARITY_02 = 40000127,
		DROP_HEAVY = 40000101,
		BALL_ROLL_REAM = 40000104,
		FLASH_RARITY_01 = 40000136,
		FLASH_RARITY_02 = 40000137,
		OPENING_01 = 40000142,
		OPENING_02 = 40000143,
		ROLLING_01 = 40000144,
		ROLLING_02 = 40000145,
		BALL_DROP = 40000146,
		BALL_ROLLING = 40000147,
		BALL_SHINE = 40000148,
		BALL_POP = 40000149,
		BALL_BREAK = 40000150,
		BALL_BREAK_S = 40000151
	}

	public enum FLASH_TYPE
	{
		INVALID = -1,
		GREEN,
		SILVER,
		GOLD
	}

	public class MagiBall : MonoBehaviour
	{
		public FLASH_TYPE FlashType
		{
			get;
			set;
		}
	}

	public Animator gachaAnimator;

	public Animator cameraAnimator;

	public AnimationClip[] camereAnimClips;

	public Transform ballSocket;

	public Transform basket;

	public Renderer line;

	public GameObject[] balls;

	public GameObject[] ballsRen;

	public GameObject[] openEffects;

	public GameObject npcEffect;

	public GameObject jumpBallEffect;

	public Transform jumpBallEffectPosition;

	public GameObject dropBallEffect;

	public Transform dropBallEffectPosition;

	public GameObject[] uiRarityEffectPrefabs;

	public Transform flashEffectPosition;

	public GameObject flashEffectPrefab;

	public GameObject[] flashEffectRarityPrefabs;

	public const string STATE_SINGLE = "SKILL_GACHA_SINGLE";

	public const string STATE_REAM = "SKILL_GACHA_REAM";

	public const string STATE_REAM_DROP_1 = "SKILL_GACHA_REAM_DROP_1";

	public const string STATE_REAM_DROP_2 = "SKILL_GACHA_REAM_DROP_2";

	public const int DISPLAY_BALL_MAX = 20;

	public const float BALL_EXTERNAL_FORCE_RATE = 120f;

	public const int REAM_NUM = 11;

	private ISectionCommand sectionCommandReceiver;

	private IEnumerator coroutine;

	private Transform npcModel;

	private Animator npcAnimator;

	private Transform mainBall;

	private Collider[] basketColliders;

	private float saveFixedUpdateTime;

	private List<GameObject> managedObjects = new List<GameObject>();

	private List<GameObject> ballObjects = new List<GameObject>();

	private List<Transform> managedEffects = new List<Transform>();

	private List<Transform> flashEffectList = new List<Transform>();

	private int rarityIndex;

	private RARITY_TYPE rarity;

	private FLASH_TYPE firstFlashType;

	private int dropCount;

	private int flashCount;

	private GachaResult.GachaReward reward;

	private bool isReam;

	private bool m_isAlreadySkipped;

	private bool m_isFinishLoad;

	public const int SINGLE_FLASH_EFFECT_MAX = 3;

	public const int REAM_FLASH_EFFECT_MAX = 4;

	public const int MODEL_TEXTURE_MAX = 3;

	public const int MODEL_TEXTURE_ID_TOP = 2;

	private Texture2D[] basketModelTextureList = new Texture2D[3];

	private Texture backupBasketModelTexture;

	private Color backupBasketSpeColor = Color.white;

	private bool IsSingleGacha => !isReam;

	protected override void Awake()
	{
		base.Awake();
		commandReceiver = this;
		if (balls.Length != 0)
		{
			for (int i = 0; i < balls.Length; i++)
			{
				if (balls[i] != null)
				{
					balls[i].SetActive(value: false);
				}
			}
		}
		SetActiveRenBalls(isActive: false);
		if (ballSocket != null)
		{
			basketColliders = ballSocket.GetComponentsInChildren<Collider>(includeInactive: true);
			SetActivateBasketCollider(isActivate: false);
		}
		if (line != null)
		{
			line.enabled = false;
		}
		Material material = GetMaterial(basket);
		if (material != null)
		{
			backupBasketModelTexture = material.mainTexture;
			backupBasketSpeColor = material.GetColor("_SpeLightColor");
		}
	}

	protected override void OnDestroy()
	{
		backupBasketModelTexture = null;
	}

	private void Start()
	{
		Init();
	}

	private void Init()
	{
		skip = false;
		m_isAlreadySkipped = false;
		Play("INIT");
	}

	public void StartDirection(ISectionCommand command_receiver)
	{
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
			coroutine = null;
		}
		if (command_receiver != null)
		{
			sectionCommandReceiver = command_receiver;
			StartCoroutine(coroutine = DoSkillGacha());
		}
	}

	private IEnumerator DoSkillGacha()
	{
		Transform transform = base.transform;
		Reset();
		if (line != null)
		{
			line.enabled = true;
		}
		isReam = false;
		if (MonoBehaviourSingleton<GachaManager>.IsValid() && MonoBehaviourSingleton<GachaManager>.I.IsReam())
		{
			isReam = true;
		}
		SetActivateBasketCollider(isActivate: true);
		Vector3 position = basket.position;
		for (int i = 0; i < 20; i++)
		{
			CreateBall(transform, 0, position + Quaternion.AngleAxis(i * 45, Vector3.right) * new Vector3(0.04f * (float)(i - 10), 0.08f, 0f), is_main: false);
		}
		LoadingQueue load_queue = new LoadingQueue(this);
		if (isReam)
		{
			GachaResult currentGachaResult = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult();
			int j = 0;
			for (int count = currentGachaResult.reward.Count; j < count; j++)
			{
				GachaResult.GachaReward gachaReward = currentGachaResult.reward[j];
				SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)gachaReward.itemId);
				if (skillItemData != null)
				{
					load_queue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(skillItemData.modelID));
					load_queue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemSymbolModel(skillItemData.iconID));
				}
			}
			List<GachaResult.GachaReward> list = currentGachaResult.reward;
			if (ballsRen != null)
			{
				for (int k = 0; k < ballsRen.Length; k++)
				{
					if (k < list.Count)
					{
						GachaResult.GachaReward gachaReward2 = list[k];
						SkillItemTable.SkillItemData skillItemData2 = Singleton<SkillItemTable>.I.GetSkillItemData((uint)gachaReward2.itemId);
						if (skillItemData2 != null)
						{
							MeshRenderer component = ballsRen[k].GetComponent<MeshRenderer>();
							int num = skillItemData2.rarity.ToRarityExpressionID();
							MeshRenderer component2 = balls[num].GetComponent<MeshRenderer>();
							component.sharedMaterial = component2.sharedMaterial;
						}
					}
				}
			}
		}
		int[] array = (int[])Enum.GetValues(typeof(AUDIO));
		foreach (int se_id in array)
		{
			load_queue.CacheSE(se_id);
		}
		LoadingQueue loadQueue = new LoadingQueue(this);
		for (int tx = 0; tx < 3; tx++)
		{
			LoadObject loadObj = loadQueue.Load(RESOURCE_CATEGORY.MAGI_BASKET_MODEL_TEX, ResourceName.GetMagiGachaModelTexutre(2 + tx));
			while (loadQueue.IsLoading())
			{
				yield return loadQueue.Wait();
			}
			basketModelTextureList[tx] = (Texture2D)loadObj.loadedObject;
		}
		if (backupBasketModelTexture != null)
		{
			Material material = GetMaterial(basket);
			if (material != null)
			{
				material.mainTexture = backupBasketModelTexture;
				material.SetColor("_SpeLightColor", backupBasketSpeColor);
			}
		}
		yield return new WaitForSeconds(1f);
		npcModel = Utility.CreateGameObject("NPC", base.transform.parent);
		managedObjects.Add(npcModel.gameObject);
		NPCLoader npc_loader = npcModel.gameObject.AddComponent<NPCLoader>();
		npc_loader.Load(Singleton<NPCTable>.I.GetNPCData(1).npcModelID, 0, need_shadow: false, enable_light_probes: true, SHADER_TYPE.NORMAL, null);
		while (npc_loader.isLoading)
		{
			yield return null;
		}
		if (load_queue != null && load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		npcAnimator = npc_loader.animator;
		yield return null;
		m_isFinishLoad = true;
		npcAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		npcAnimator.Rebind();
		npcAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		gachaAnimator.Rebind();
		cameraAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		cameraAnimator.Rebind();
		string state_name = (!IsSingleGacha) ? "SKILL_GACHA_REAM" : "SKILL_GACHA_SINGLE";
		dropCount = 0;
		Drop();
		Play(state_name);
		if (isReam)
		{
			while (dropCount < 11)
			{
				if (skip && IsFinishDrop10())
				{
					if (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
					{
						yield return null;
					}
					Time.timeScale = 1f;
					skip = false;
					yield return MonoBehaviourSingleton<TransitionManager>.I.In();
				}
				yield return null;
			}
		}
		while (mainBall.gameObject.activeSelf)
		{
			yield return null;
		}
		if (skip)
		{
			yield return MonoBehaviourSingleton<TransitionManager>.I.In();
			Time.timeScale = 1f;
		}
		yield return null;
		sectionCommandReceiver.OnEnd();
		coroutine = null;
	}

	private void Play3Anim(string state_name)
	{
	}

	private void Drop()
	{
		sectionCommandReceiver.OnHideRarity();
		int i = 0;
		for (int count = managedEffects.Count; i < count; i++)
		{
			if (managedEffects[i] != null)
			{
				UnityEngine.Object.Destroy(managedEffects[i].gameObject);
			}
		}
		managedEffects.Clear();
		rarity = RARITY_TYPE.D;
		reward = null;
		if (MonoBehaviourSingleton<GachaManager>.IsValid() && MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null && dropCount < MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward.Count)
		{
			reward = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward[dropCount];
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)reward.itemId);
			if (skillItemData != null)
			{
				rarity = skillItemData.rarity;
			}
		}
		if (reward == null)
		{
			reward = new GachaResult.GachaReward();
		}
		rarityIndex = rarity.ToRarityExpressionID();
		if (mainBall != null)
		{
			UnityEngine.Object.Destroy(mainBall.gameObject);
		}
		mainBall = CreateBall(ballSocket, rarityIndex, Vector3.zero, is_main: true);
		string text;
		if (dropCount != 0)
		{
			text = ((dropCount >= 10) ? "SKILL_GACHA_REAM_DROP_2" : "SKILL_GACHA_REAM_DROP_1");
		}
		else
		{
			text = ((!IsSingleGacha) ? "SKILL_GACHA_REAM" : "SKILL_GACHA_SINGLE");
			Play(text);
		}
		npcAnimator.Play(text, 0, 0f);
		gachaAnimator.Play(text, 0, 0f);
		cameraAnimator.Play(text, 0, 0f);
	}

	private Transform CreateBall(Transform parent, int rarityType, Vector3 pos, bool is_main)
	{
		GameObject gameObject = ResourceUtility.Instantiate(balls[rarityType]);
		Transform transform = gameObject.transform;
		transform.parent = parent;
		transform.localPosition = pos;
		transform.localScale = Vector3.one;
		if (is_main)
		{
			UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
			UnityEngine.Object.Destroy(gameObject.GetComponent<Collider>());
		}
		else
		{
			MagiBall magiBall = gameObject.AddComponent<MagiBall>();
			if (magiBall != null)
			{
				magiBall.FlashType = (FLASH_TYPE)rarityType;
			}
			ballObjects.Add(gameObject);
		}
		gameObject.SetActive(value: true);
		return transform;
	}

	protected override void LateUpdate()
	{
		_ = (useCamera != null);
		base.LateUpdate();
	}

	private void Delete()
	{
		int i = 0;
		for (int count = managedObjects.Count; i < count; i++)
		{
			UnityEngine.Object.DestroyImmediate(managedObjects[i]);
			managedObjects[i] = null;
		}
		managedObjects.Clear();
		if (flashEffectList != null && flashEffectList.Count > 0)
		{
			int count2 = flashEffectList.Count;
			for (int j = 0; j < count2; j++)
			{
				UnityEngine.Object.DestroyImmediate(flashEffectList[j].gameObject);
				flashEffectList[j] = null;
			}
		}
		flashEffectList.Clear();
		mainBall = null;
		npcModel = null;
		npcAnimator = null;
		int count3 = ballObjects.Count;
		for (int k = 0; k < count3; k++)
		{
			UnityEngine.Object.DestroyImmediate(ballObjects[k]);
			ballObjects[k] = null;
		}
		ballObjects.Clear();
		if (line != null)
		{
			line.enabled = false;
		}
		Play("INIT");
	}

	public override void Reset()
	{
		skip = false;
		m_isAlreadySkipped = false;
		m_isFinishLoad = false;
		dropCount = 0;
		flashCount = 0;
		reward = null;
		SetActiveRenBalls(isActive: false);
		Delete();
	}

	public override void Skip()
	{
		if (!skip && !IsFinishDrop10() && !m_isAlreadySkipped)
		{
			m_isAlreadySkipped = true;
			AudioObjectPool.StopAllLentObjects();
			base.Skip();
			StartCoroutine(DoSkip());
		}
	}

	private IEnumerator DoSkip()
	{
		yield return MonoBehaviourSingleton<TransitionManager>.I.Out();
		Time.timeScale = 100f;
		while (!m_isFinishLoad)
		{
			yield return null;
		}
		if (IsSingleGacha)
		{
			UpdateGachaModelEffectSingle(3);
		}
	}

	public bool IsFinishDrop10()
	{
		if (isReam)
		{
			return dropCount >= 10;
		}
		return false;
	}

	private void OnDirectionCommand(string cmd)
	{
		bool flag = !MonoBehaviourSingleton<TransitionManager>.I.isChanging && MonoBehaviourSingleton<TransitionManager>.I.isTransing;
		if (cmd == "NPC_EFFECT")
		{
			if (npcModel != null)
			{
				Transform transform = Utility.Find(npcModel, "Head");
				if (transform != null)
				{
					transform = ResourceUtility.Realizes(npcEffect, transform);
				}
			}
		}
		else if (cmd == "DROP_EFFECT")
		{
			if (!flag && dropBallEffect != null && dropBallEffectPosition != null)
			{
				managedEffects.Add(ResourceUtility.Realizes(dropBallEffect, dropBallEffectPosition));
			}
		}
		else if (cmd == "JUMP_EFFECT")
		{
			if (!flag)
			{
				PlayAUDIO(AUDIO.BALL_POP);
				if (jumpBallEffect != null && jumpBallEffectPosition != null)
				{
					managedEffects.Add(ResourceUtility.Realizes(jumpBallEffect, jumpBallEffectPosition));
				}
			}
		}
		else if (cmd == "SHOW_RARITY")
		{
			if (!flag)
			{
				if (isReam)
				{
					sectionCommandReceiver.OnShowRarity(rarity);
				}
				PlayAUDIO(AUDIO.BALL_SHINE);
			}
		}
		else if (cmd == "OPEN_EFFECT")
		{
			if (!flag)
			{
				managedEffects.Add(ResourceUtility.Realizes(openEffects[rarityIndex], ballSocket));
				if ((rarityIndex == 0 && IsSingleGacha) || rarityIndex == 10)
				{
					PlayAUDIO(AUDIO.BALL_BREAK);
				}
				else
				{
					PlayAUDIO(AUDIO.BALL_BREAK_S);
				}
			}
		}
		else if (cmd == "FLASH_EFFECT")
		{
			EventFlashEffect(flag);
		}
		else if (cmd == "HIDE_BALL")
		{
			if (mainBall != null)
			{
				mainBall.gameObject.SetActive(value: false);
			}
			if (isReam && sectionCommandReceiver != null && reward != null)
			{
				sectionCommandReceiver.OnShowSkillModel((uint)reward.itemId);
			}
		}
		else if (cmd == "NEXT_DROP")
		{
			if (isReam)
			{
				int num = 9;
				if (skip && flag && dropCount < num)
				{
					dropCount = num;
				}
				dropCount++;
				if (dropCount <= 10)
				{
					Drop();
				}
				if (sectionCommandReceiver != null)
				{
					sectionCommandReceiver.OnHideSkillModel();
				}
			}
		}
		else
		{
			if (cmd == "SHOW_REN_BALLS")
			{
				SetActiveRenBalls(isActive: true);
			}
			if (cmd == "HIDE_REN_BALLS")
			{
				SetActiveRenBalls(isActive: false);
			}
		}
	}

	private void EventFlashEffect(bool isSkip)
	{
		if (isSkip)
		{
			if (isReam)
			{
				flashCount++;
				UpdateGachaModelEffectReam(flashCount);
			}
		}
		else
		{
			if (flashEffectPrefab == null || flashEffectRarityPrefabs == null)
			{
				return;
			}
			ApplyRandomVectorForBalls();
			flashCount++;
			GameObject gameObject = flashEffectPrefab;
			if (isReam)
			{
				if (flashCount >= 4)
				{
					FLASH_TYPE flashTypeAtLast = GetFlashTypeAtLast();
					gameObject = flashEffectRarityPrefabs[(int)flashTypeAtLast];
					SwitchBasketModelTexture(flashTypeAtLast);
					SwitchBasketBallColor(flashTypeAtLast);
					PlayAUDIOFlash(flashTypeAtLast);
				}
				else
				{
					FLASH_TYPE fLASH_TYPE = UpdateGachaModelEffectReam(flashCount);
					gameObject = flashEffectRarityPrefabs[(int)fLASH_TYPE];
				}
			}
			else
			{
				FLASH_TYPE fLASH_TYPE2 = UpdateGachaModelEffectSingle(flashCount);
				gameObject = flashEffectRarityPrefabs[(int)fLASH_TYPE2];
			}
			if (gameObject != null && flashEffectPosition != null)
			{
				Transform transform = ResourceUtility.Realizes(gameObject, flashEffectPosition);
				if (transform != null)
				{
					flashEffectList.Add(transform);
				}
			}
		}
	}

	private FLASH_TYPE UpdateGachaModelEffectSingle(int targetFlashCount)
	{
		FLASH_TYPE fLASH_TYPE = FLASH_TYPE.GREEN;
		if (targetFlashCount > 1)
		{
			if (CalcNumRarityData(RARITY_TYPE.SS) > 0 || CalcNumRarityData(RARITY_TYPE.S) > 0)
			{
				fLASH_TYPE = FLASH_TYPE.GOLD;
				if (targetFlashCount < 3)
				{
					fLASH_TYPE = ((UnityEngine.Random.Range(0, 100) <= 50) ? FLASH_TYPE.SILVER : FLASH_TYPE.GREEN);
				}
			}
			else if (CalcNumRarityData(RARITY_TYPE.A) > 0)
			{
				fLASH_TYPE = FLASH_TYPE.SILVER;
				if (targetFlashCount < 3)
				{
					fLASH_TYPE = ((UnityEngine.Random.Range(0, 100) <= 50) ? FLASH_TYPE.SILVER : FLASH_TYPE.GREEN);
				}
			}
		}
		PlayAUDIOFlash(fLASH_TYPE);
		SwitchBasketBallColor(fLASH_TYPE);
		SwitchBasketModelTexture(fLASH_TYPE);
		return fLASH_TYPE;
	}

	private void SwitchBasketBallColor(FLASH_TYPE targetFlashType)
	{
		foreach (GameObject ballObject in ballObjects)
		{
			MeshRenderer component = ballObject.GetComponent<MeshRenderer>();
			if (!(component == null))
			{
				Material material = component.material;
				if (!(material == null))
				{
					Material material2 = balls[(int)targetFlashType].GetComponent<MeshRenderer>().material;
					material.mainTexture = material2.mainTexture;
					material.SetColor("_SpeLightColor", material2.GetColor("_SpeLightColor"));
					material.SetFloat("_SpeWidth", material2.GetFloat("_SpeWidth"));
				}
			}
		}
	}

	private void SwitchBasketBallColorReam(FLASH_TYPE targetFlashType, int numChange)
	{
		int num = 0;
		foreach (GameObject ballObject in ballObjects)
		{
			if (num >= numChange)
			{
				break;
			}
			MagiBall component = ballObject.GetComponent<MagiBall>();
			if (!(component == null) && component.FlashType == FLASH_TYPE.GREEN)
			{
				MeshRenderer component2 = ballObject.GetComponent<MeshRenderer>();
				if (!(component2 == null))
				{
					Material material = component2.material;
					if (!(material == null))
					{
						Material material2 = balls[(int)targetFlashType].GetComponent<MeshRenderer>().material;
						material.mainTexture = material2.mainTexture;
						material.SetColor("_SpeLightColor", material2.GetColor("_SpeLightColor"));
						material.SetFloat("_SpeWidth", material2.GetFloat("_SpeWidth"));
						component.FlashType = targetFlashType;
						num++;
					}
				}
			}
		}
	}

	private FLASH_TYPE UpdateGachaModelEffectReam(int targetFlashCount)
	{
		FLASH_TYPE fLASH_TYPE = FLASH_TYPE.GREEN;
		switch (targetFlashCount)
		{
		case 2:
		{
			int numRarityData = GetNumRarityData10(RARITY_TYPE.A);
			if (numRarityData > 0)
			{
				fLASH_TYPE = FLASH_TYPE.SILVER;
				SwitchBasketBallColorReam(fLASH_TYPE, numRarityData * 2);
			}
			firstFlashType = fLASH_TYPE;
			break;
		}
		case 3:
		{
			fLASH_TYPE = firstFlashType;
			int num = GetNumRarityData10(RARITY_TYPE.SS) + GetNumRarityData10(RARITY_TYPE.S);
			if (num > 0)
			{
				fLASH_TYPE = FLASH_TYPE.GOLD;
				SwitchBasketBallColorReam(fLASH_TYPE, num * 2);
			}
			break;
		}
		}
		SwitchBasketModelTexture(fLASH_TYPE);
		PlayAUDIOFlash(fLASH_TYPE);
		return fLASH_TYPE;
	}

	private void SwitchBasketModelTexture(FLASH_TYPE targetRarityType)
	{
		if (basketModelTextureList == null)
		{
			Log.Error("Not found downloaded texture!!");
			return;
		}
		Texture2D texture2D = basketModelTextureList[(int)targetRarityType];
		if (texture2D == null)
		{
			Log.Error("Not found texture for basket model!!");
			return;
		}
		Material material = GetMaterial(basket);
		if (material != null)
		{
			material.mainTexture = texture2D;
			material.SetColor("_SpeLightColor", Color.white);
		}
	}

	private Material GetMaterial(Transform targetTrans)
	{
		MeshRenderer component = targetTrans.GetComponent<MeshRenderer>();
		if (component == null)
		{
			Log.Error("Not found MeshRender!!");
			return null;
		}
		Material[] materials = component.materials;
		if (materials == null)
		{
			Log.Error("material list is null!!");
			return null;
		}
		return materials[0];
	}

	private void ApplyRandomVectorForBalls()
	{
		foreach (GameObject ballObject in ballObjects)
		{
			if (!(ballObject == null))
			{
				Rigidbody component = ballObject.GetComponent<Rigidbody>();
				if (!(component == null))
				{
					component.AddForce(Vector3.up * 120f);
				}
			}
		}
	}

	private FLASH_TYPE GetFlashTypeAtLast()
	{
		FLASH_TYPE result = FLASH_TYPE.GREEN;
		if (CheckContainRarityLast(RARITY_TYPE.SS) || CheckContainRarityLast(RARITY_TYPE.S))
		{
			result = FLASH_TYPE.GOLD;
		}
		else if (CheckContainRarityLast(RARITY_TYPE.A))
		{
			result = FLASH_TYPE.SILVER;
		}
		return result;
	}

	private bool CheckContainRarityLast(RARITY_TYPE targetRarityType)
	{
		return CalcNumRarityData(targetRarityType, isCheckOnlyLast: true) > 0;
	}

	private int GetNumRarityData10(RARITY_TYPE targetRarityType)
	{
		return CalcNumRarityData(targetRarityType, isCheckOnlyLast: false, isCheck10: true);
	}

	private int CalcNumRarityData(RARITY_TYPE targetRarityType, bool isCheckOnlyLast = false, bool isCheck10 = false)
	{
		int num = 0;
		if (!MonoBehaviourSingleton<GachaManager>.IsValid())
		{
			Log.Error("Invalid GachaManager!!");
			return num;
		}
		GachaResult currentGachaResult = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult();
		if (currentGachaResult == null)
		{
			Log.Error("gachaResult is null!!");
			return num;
		}
		int num2 = currentGachaResult.reward.Count;
		List<GachaResult.GachaReward> list = currentGachaResult.reward;
		if (isCheckOnlyLast)
		{
			if (Singleton<SkillItemTable>.I.GetSkillItemData((uint)list[num2 - 1].itemId).rarity != targetRarityType)
			{
				return 0;
			}
			return 1;
		}
		if (isCheck10 && isReam)
		{
			num2--;
		}
		for (int i = 0; i < num2; i++)
		{
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)list[i].itemId);
			if (skillItemData != null && skillItemData.rarity == targetRarityType)
			{
				num++;
			}
		}
		return num;
	}

	private void PlayAUDIOFlash(FLASH_TYPE flash)
	{
		AUDIO audio = AUDIO.FLASH_RARITY_01;
		if (flash > FLASH_TYPE.GREEN)
		{
			audio = AUDIO.FLASH_RARITY_02;
		}
		PlayAUDIO(audio);
	}

	private void PlayAUDIO(AUDIO audio)
	{
		if (!skip)
		{
			SoundManager.PlayOneShotUISE((int)audio);
		}
	}

	public void PlayUIRarityEffect(RARITY_TYPE rarity, Transform effect_parent_ui, Transform effect_target_ui)
	{
		GameObject gameObject = null;
		int num = rarity.ToRarityExpressionID2();
		if (num > 0)
		{
			gameObject = uiRarityEffectPrefabs[num - 1];
		}
		if (!(gameObject == null))
		{
			UIWidget componentInChildren = effect_target_ui.GetComponentInChildren<UIWidget>();
			Transform transform = ResourceUtility.Realizes(gameObject, effect_parent_ui, 5);
			transform.position = effect_parent_ui.position;
			EffectManager.SetUIEffectDepth(transform, effect_parent_ui, -0.001f, 10, componentInChildren);
			PlayRarityAudio(rarity);
		}
	}

	public void PlayRarityAudio(RARITY_TYPE rarity_type)
	{
		int num = 4;
		int num2 = -1;
		num2 = (((int)rarity_type < num) ? 40000126 : 40000127);
		SoundManager.PlayOneShotUISE(num2);
	}

	public override void __FUNCTION__PlayCachedAudio(int se_id)
	{
		if (!skip)
		{
			base.__FUNCTION__PlayCachedAudio(se_id);
		}
	}

	public void SetActiveRenBalls(bool isActive)
	{
		if (ballsRen == null || ballsRen.Length == 0)
		{
			return;
		}
		int i = 0;
		if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null)
		{
			int count = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward.Count;
			for (int num = Mathf.Min(ballsRen.Length, count); i < num; i++)
			{
				if (ballsRen[i] != null)
				{
					ballsRen[i].SetActive(isActive);
				}
			}
		}
		for (; i < ballsRen.Length; i++)
		{
			if (ballsRen[i] != null)
			{
				ballsRen[i].SetActive(value: false);
			}
		}
	}

	public void SetActivateBasketCollider(bool isActivate)
	{
		if (basketColliders == null)
		{
			Log.Error("basketCollider is null!!");
			return;
		}
		int i = 0;
		for (int num = basketColliders.Length; i < num; i++)
		{
			basketColliders[i].enabled = isActivate;
		}
	}
}
