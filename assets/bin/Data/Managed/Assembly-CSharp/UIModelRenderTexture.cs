using System;
using System.Collections;
using UnityEngine;

public class UIModelRenderTexture : MonoBehaviour
{
	private enum LOADER_TYPE
	{
		PLAYER,
		NPC,
		ITEM,
		ENEMY
	}

	public enum ENEMY_MOVE_TYPE
	{
		DEFULT,
		DONT_MOVE,
		STOP
	}

	public const float NORMAL_FOV = 45f;

	public const float CHARA_FOV = 10f;

	public const float MAGI_FOV = 35f;

	public const float MAGI_SYMBOL_FOV = 13f;

	public const float ACCESSORY_FOV = 45f;

	public const float NORMAL_ROTATE_SPEED = 12f;

	public const float MAGI_ROTATE_SPEED = 22f;

	public const float ACCESSORY_ROTATE_SPEED = 22f;

	private UITexture uiTexture;

	private Transform model;

	private Vector3 modelPos;

	private Vector3 modelRot = new Vector3(0f, 180f, 0f);

	private float cameraFOV;

	private float rotateSpeed = 12f;

	private bool lightRotation;

	private PlayerLoader playerLoader;

	private PlayerLoadInfo playerLoadInfo;

	private int playerAnimID = -1;

	private bool isPriorityVisualEquip = true;

	private NPCLoader npcLoader;

	private NPCTable.NPCData npcData;

	private ItemLoader itemLoader;

	private int equipItemID = -1;

	private int skillItemID = -1;

	private int skillSymbolItemID = -1;

	private int itemID = -1;

	private int referenceSexID = -1;

	private int referenceFaceID = -1;

	private int accessoryID = -1;

	private EnemyLoader enemyLoader;

	private int enemyID = -1;

	private string foundationName;

	private OutGameSettingsManager.EnemyDisplayInfo enemyDispplayInfo;

	private AudioObject audioObject;

	private bool isEnemyHowl;

	private UIRenderTexture uiRenderTexture;

	private IEnumerator coroutine;

	private int modelLayer;

	private float uiModelScale = 1f;

	private Action<PlayerLoader> onPlayerLoadFinishedCallBack;

	private Action<NPCLoader> onNPCLoadFinishedCallBack;

	private OutGameSettingsManager.EnemyDisplayInfo.SCENE targetScene;

	private bool oneshot;

	private bool isRandomPlaying;

	private const float crossFadeTime = 0.5f;

	public EnemyAnimCtrl enemyAnimCtrl
	{
		get;
		private set;
	}

	public UIModelRenderTexture()
		: this()
	{
	}//IL_0010: Unknown result type (might be due to invalid IL or missing references)
	//IL_0015: Unknown result type (might be due to invalid IL or missing references)


	public static UIModelRenderTexture Get(Transform t)
	{
		UIModelRenderTexture uIModelRenderTexture = t.GetComponent<UIModelRenderTexture>();
		if (uIModelRenderTexture == null)
		{
			uIModelRenderTexture = t.get_gameObject().AddComponent<UIModelRenderTexture>();
		}
		return uIModelRenderTexture;
	}

	public void SetRotateSpeed(float val)
	{
		rotateSpeed = val;
	}

	public void InitPlayer(UITexture ui_tex, PlayerLoadInfo info, int anim_id, Vector3 pos, Vector3 rot, bool is_priority_visual_equip, Action<PlayerLoader> onload_callback)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if ((!(playerLoader != null) || !playerLoader.isLoading) && (playerLoadInfo == null || !playerLoadInfo.Equals(info) || isPriorityVisualEquip != is_priority_visual_equip))
		{
			InitPlayerInFact(ui_tex, info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
		}
	}

	public void InitPlayerOneShot(UITexture ui_tex, PlayerLoadInfo info, int anim_id, Vector3 pos, Vector3 rot, bool is_priority_visual_equip, Action<PlayerLoader> onload_callback)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		oneshot = true;
		InitPlayer(ui_tex, info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
	}

	public void ForceInitPlayer(UITexture ui_tex, PlayerLoadInfo info, int anim_id, Vector3 pos, Vector3 rot, bool is_priority_visual_equip, Action<PlayerLoader> onload_callback)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		if (playerLoadInfo != null && playerLoadInfo.Equals(info) && isPriorityVisualEquip == is_priority_visual_equip)
		{
			onload_callback?.Invoke(null);
			return;
		}
		bool flag = IsLoadingPlayer();
		bool flag2 = npcLoader != null;
		if (flag || flag2)
		{
			if (flag2)
			{
				npcData = null;
			}
			DeleteModel();
		}
		InitPlayerInFact(ui_tex, info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
	}

	public void InitPlayerInFact(UITexture ui_tex, PlayerLoadInfo info, int anim_id, Vector3 pos, Vector3 rot, bool is_priority_visual_equip, Action<PlayerLoader> onload_callback)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		isPriorityVisualEquip = is_priority_visual_equip;
		Init(ui_tex, 10f, LOADER_TYPE.PLAYER);
		if (uiRenderTexture != null && uiRenderTexture.nearClipPlane == -1f && pos.z > 13f)
		{
			uiRenderTexture.nearClipPlane = pos.z - 5f;
		}
		playerLoadInfo = info;
		modelPos = pos;
		modelRot = rot;
		playerAnimID = anim_id;
		onPlayerLoadFinishedCallBack = onload_callback;
		playerLoader.StartLoad(info, modelLayer, playerAnimID, need_anim_event: false, need_foot_stamp: false, need_shadow: false, enable_light_probes: false, need_action_voice: false, need_high_reso_tex: false, need_res_ref_count: true, need_dev_frame_instantiate: true, SHADER_TYPE.UI, OnPlayerLoadFinished);
		LoadStart();
	}

	public bool IsLoadingPlayer()
	{
		if (playerLoader != null && playerLoader.isLoading)
		{
			return true;
		}
		return false;
	}

	public void InitNPC(UITexture ui_tex, int npc_id, Vector3 pos, Vector3 rot, float fov, Action<NPCLoader> onload_callback)
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		if (npcLoader != null && npcLoader.isLoading)
		{
			return;
		}
		NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData(npc_id);
		if (nPCData != null && nPCData != npcData)
		{
			if (playerLoader != null)
			{
				playerLoadInfo = null;
				DeleteModel();
			}
			Init(ui_tex, (fov == -1f) ? 10f : fov, LOADER_TYPE.NPC);
			if (uiRenderTexture != null && uiRenderTexture.nearClipPlane == -1f && ((fov >= 40f && pos.z > 13f) || (fov < 40f && pos.z > 5f)))
			{
				uiRenderTexture.nearClipPlane = pos.z - 5f;
			}
			npcData = nPCData;
			modelPos = pos;
			modelRot = rot;
			cameraFOV = fov;
			onNPCLoadFinishedCallBack = onload_callback;
			int num = (nPCData.specialModelID <= 0) ? nPCData.npcModelID : nPCData.specialModelID;
			HomeThemeTable.HomeThemeData homeThemeData = Singleton<HomeThemeTable>.I.GetHomeThemeData(Singleton<HomeThemeTable>.I.CurrentHomeTheme);
			int num2 = -1;
			if (homeThemeData != null)
			{
				num2 = Singleton<HomeThemeTable>.I.GetNpcModelID(homeThemeData, nPCData.id);
			}
			num = ((num2 <= 0) ? num : num2);
			npcLoader.Load(num, modelLayer, need_shadow: false, enable_light_probes: false, SHADER_TYPE.UI, OnNPCLoadFinished);
			LoadStart();
		}
	}

	public bool IsLoadingNPC()
	{
		if (npcLoader != null && npcLoader.isLoading)
		{
			return true;
		}
		return false;
	}

	public void Init(UITexture ui_tex, SortCompareData data)
	{
		Init(ui_tex, 45f);
		if (data is EquipItemSortData || data is SmithCreateSortData)
		{
			InitEquip(ui_tex, data.GetTableID(), referenceSexID, referenceFaceID, uiModelScale);
		}
		else if (data is ItemSortData)
		{
			InitItem(ui_tex, data.GetTableID());
		}
		else if (data is SkillItemSortData)
		{
			InitSkillItem(ui_tex, data.GetTableID());
		}
	}

	public void InitEquip(UITexture ui_tex, uint equip_item_id, int sex_id, int face_id, float scale)
	{
		referenceSexID = sex_id;
		referenceFaceID = face_id;
		Init(ui_tex, 45f);
		equipItemID = (int)equip_item_id;
		uiModelScale = scale;
		itemLoader.LoadEquip(equip_item_id, model, modelLayer, referenceSexID, referenceFaceID, OnLoadFinished);
		LoadStart();
	}

	public void InitItem(UITexture ui_tex, uint item_id, bool rotation = true)
	{
		Init(ui_tex, 45f);
		itemID = (int)item_id;
		itemLoader.LoadItem(item_id, model, modelLayer, OnLoadFinished);
		LoadStart();
		if (rotation)
		{
			rotateSpeed = 12f;
		}
		else
		{
			rotateSpeed = 0f;
		}
	}

	public void InitSkillItem(UITexture ui_tex, uint skill_item_id, bool rotation = true, bool light_rotation = false, float fov = 35f)
	{
		Init(ui_tex, fov);
		skillItemID = (int)skill_item_id;
		itemLoader.LoadSkillItem(skill_item_id, model, modelLayer, OnLoadFinished);
		LoadStart();
		if (rotation)
		{
			rotateSpeed = 22f;
		}
		else
		{
			rotateSpeed = 0f;
		}
		lightRotation = light_rotation;
	}

	public void InitSkillItemSymbol(UITexture ui_tex, uint skill_item_id, bool rotation = true, float fov = 13f)
	{
		Init(ui_tex, fov);
		skillSymbolItemID = (int)skill_item_id;
		itemLoader.LoadSkillItemSymbol(skill_item_id, model, modelLayer, OnLoadFinished);
		BlurFilter blurFilter = this.get_gameObject().GetComponent<BlurFilter>();
		if (blurFilter == null)
		{
			blurFilter = this.get_gameObject().AddComponent<BlurFilter>();
			blurFilter.downSample = 1;
			blurFilter.blurStrength = 0.4f;
		}
		uiRenderTexture.postEffectFilter = blurFilter;
		LoadStart();
		if (rotation)
		{
			rotateSpeed = 22f;
		}
		else
		{
			rotateSpeed = 0f;
		}
	}

	public void InitEnemy(UITexture ui_tex, uint enemy_id, string foundation_name, OutGameSettingsManager.EnemyDisplayInfo.SCENE target_scene, Action<bool, EnemyLoader> callback = null, ENEMY_MOVE_TYPE moveType = ENEMY_MOVE_TYPE.DEFULT, bool is_Howl = true)
	{
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(enemy_id);
		if (enemyData == null)
		{
			Clear();
			if (callback != null)
			{
				callback(arg1: false, null);
			}
			return;
		}
		Init(ui_tex, 45f, LOADER_TYPE.ENEMY);
		enemyID = (int)enemy_id;
		foundationName = foundation_name;
		targetScene = target_scene;
		isEnemyHowl = is_Howl;
		int anim_id = enemyData.animId;
		float scale = enemyData.modelScale;
		if (targetScene == OutGameSettingsManager.EnemyDisplayInfo.SCENE.QUEST)
		{
			enemyDispplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForQuestSelect(enemyData);
		}
		else
		{
			enemyDispplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemyData);
		}
		if (enemyDispplayInfo != null)
		{
			if (enemyDispplayInfo.animID > 0)
			{
				anim_id = enemyDispplayInfo.animID;
			}
			scale = enemyDispplayInfo.scale;
		}
		enemyLoader.StartLoad(enemyData.modelId, anim_id, scale, enemyData.baseEffectName, enemyData.baseEffectNode, need_shadow: false, enable_light_probes: false, need_anim_event_res_cache: true, SHADER_TYPE.UI, modelLayer, foundation_name, need_stamp_effect: false, will_stock: false, string.Empty, delegate(Enemy enemy)
		{
			if (callback != null)
			{
				callback(arg1: true, enemyLoader);
			}
			if (enemyLoader != null && enemyLoader.animator != null)
			{
				if (moveType == ENEMY_MOVE_TYPE.DONT_MOVE)
				{
					enemyLoader.animator.set_applyRootMotion(false);
				}
				else if (moveType == ENEMY_MOVE_TYPE.STOP)
				{
					enemyLoader.animator.set_speed(0f);
				}
			}
			OnEnemyLoadFinished(enemy);
		});
		LoadStart();
	}

	public void InitAccessory(UITexture ui_tex, uint accessory_id, float scale, bool rotation = true, bool light_rotation = false)
	{
		Init(ui_tex, 45f);
		accessoryID = (int)accessory_id;
		uiModelScale = scale;
		itemLoader.LoadAccessory(accessory_id, model, modelLayer, OnLoadFinished);
		LoadStart();
		rotateSpeed = ((!rotation) ? 0f : 22f);
		lightRotation = light_rotation;
	}

	private void Init(UITexture ui_tex, float fov, LOADER_TYPE loader_type = LOADER_TYPE.ITEM)
	{
		if (model == null)
		{
			uiTexture = ui_tex;
			uiRenderTexture = UIRenderTexture.Get(ui_tex, fov);
			model = Utility.CreateGameObject("UIModel", uiRenderTexture.modelTransform, uiRenderTexture.renderLayer);
			switch (loader_type)
			{
			case LOADER_TYPE.PLAYER:
				playerLoader = model.get_gameObject().AddComponent<PlayerLoader>();
				break;
			case LOADER_TYPE.NPC:
				npcLoader = model.get_gameObject().AddComponent<NPCLoader>();
				break;
			case LOADER_TYPE.ITEM:
				itemLoader = model.get_gameObject().AddComponent<ItemLoader>();
				break;
			case LOADER_TYPE.ENEMY:
				enemyLoader = model.get_gameObject().AddComponent<EnemyLoader>();
				break;
			}
			modelLayer = uiRenderTexture.renderLayer;
		}
	}

	private void OnLoadFinished()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if (model == null)
		{
			return;
		}
		if (!this.get_gameObject().get_activeInHierarchy())
		{
			DeleteModel();
			return;
		}
		if (coroutine != null)
		{
			this.StopCoroutine(coroutine);
			coroutine = null;
		}
		if (uiModelScale != 1f)
		{
			model.set_localScale(new Vector3(uiModelScale, uiModelScale, uiModelScale));
		}
		coroutine = DoViewing();
		this.StartCoroutine(coroutine);
	}

	private void OnPlayerLoadFinished(object o)
	{
		OnLoadFinished();
		if (onPlayerLoadFinishedCallBack != null)
		{
			onPlayerLoadFinishedCallBack(playerLoader);
		}
	}

	private void OnNPCLoadFinished()
	{
		OnLoadFinished();
		if (onNPCLoadFinishedCallBack != null)
		{
			onNPCLoadFinishedCallBack(npcLoader);
		}
	}

	private void OnEnemyLoadFinished(Enemy o)
	{
		OnLoadFinished();
		if (enemyLoader == null || enemyLoader.body == null)
		{
			return;
		}
		enemyLoader.body.GetComponentsInChildren<Renderer>(Temporary.rendererList);
		int i = 0;
		for (int count = Temporary.rendererList.Count; i < count; i++)
		{
			Renderer val = Temporary.rendererList[i];
			if (!(val is MeshRenderer) && !(val is SkinnedMeshRenderer))
			{
				continue;
			}
			Material[] materials = val.get_materials();
			int j = 0;
			for (int num = materials.Length; j < num; j++)
			{
				Material val2 = materials[j];
				if (!(val2 != null))
				{
					continue;
				}
				string name = val2.get_shader().get_name();
				if (name.Contains("cut"))
				{
					val2.set_shader(ResourceUtility.FindShader("Transparent/Cutout/Diffuse"));
					continue;
				}
				if (name.Contains("_zako_"))
				{
					val2.set_shader(ResourceUtility.FindShader(name + "__s"));
				}
				else if (CanChangeHighQualityShader(name))
				{
					Shader val3 = ResourceUtility.FindShader("mobile/Custom/Enemy/enemy_single_tex__no_cull");
					if (val2.HasProperty("_CullMode") && val2.GetInt("_CullMode") == 0 && val3 != null)
					{
						val2.set_shader(val3);
					}
					else
					{
						val2.set_shader(ResourceUtility.FindShader("mobile/Custom/Enemy/enemy_single_tex__s"));
					}
				}
				if (enemyLoader.bodyID == 2023)
				{
					val2.set_shader(ResourceUtility.FindShader("mobile/Custom/Enemy/enemy_reflective_simple"));
				}
				else if (enemyLoader.bodyID == 2043)
				{
					val2.set_shader(ResourceUtility.FindShader("mobile/Custom/Enemy/enemy_reflective_for_shadow"));
				}
			}
		}
		Temporary.rendererList.Clear();
	}

	private bool CanChangeHighQualityShader(string currentShaderName)
	{
		if (currentShaderName.Contains("enemy_reflective"))
		{
			return false;
		}
		return true;
	}

	private void DeleteModel()
	{
		if (model != null)
		{
			if (playerLoader != null)
			{
				playerLoader.DeleteLoadedObjects();
				Object.Destroy(playerLoader);
				playerLoader = null;
			}
			if (npcLoader != null)
			{
				npcLoader.Clear();
				Object.Destroy(npcLoader);
				npcLoader = null;
			}
			if (enemyLoader != null)
			{
				enemyLoader.DeleteLoadedObjects();
				Object.Destroy(enemyLoader);
				enemyLoader = null;
				enemyAnimCtrl = null;
			}
			if (itemLoader != null)
			{
				itemLoader.Clear();
				Object.Destroy(itemLoader);
				itemLoader = null;
			}
			Object.Destroy(model.get_gameObject());
			model = null;
			if (uiRenderTexture != null && uiRenderTexture.postEffectFilter != null)
			{
				uiRenderTexture.postEffectFilter = null;
			}
		}
		if (coroutine != null)
		{
			this.StopCoroutine(coroutine);
			coroutine = null;
		}
	}

	private void ReloadModel()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		if (model == null)
		{
			if (playerLoadInfo != null)
			{
				PlayerLoadInfo info = playerLoadInfo;
				playerLoadInfo = null;
				InitPlayer(uiTexture, info, playerAnimID, modelPos, modelRot, isPriorityVisualEquip, onPlayerLoadFinishedCallBack);
			}
			else if (npcData != null)
			{
				NPCTable.NPCData nPCData = npcData;
				npcData = null;
				InitNPC(uiTexture, nPCData.id, modelPos, modelRot, cameraFOV, onNPCLoadFinishedCallBack);
			}
			else if (equipItemID != -1)
			{
				InitEquip(uiTexture, (uint)equipItemID, referenceSexID, referenceFaceID, uiModelScale);
			}
			else if (itemID != -1)
			{
				InitItem(uiTexture, (uint)itemID);
			}
			else if (skillItemID != -1)
			{
				InitSkillItem(uiTexture, (uint)skillItemID);
			}
			else if (skillSymbolItemID != -1)
			{
				InitSkillItemSymbol(uiTexture, (uint)skillSymbolItemID);
			}
			else if (enemyID != -1)
			{
				InitEnemy(uiTexture, (uint)enemyID, foundationName, targetScene);
			}
			else if (accessoryID != -1)
			{
				InitAccessory(uiTexture, (uint)accessoryID, uiModelScale);
			}
		}
		else if (equipItemID != -1)
		{
			if (itemLoader != null && !itemLoader.IsLoading())
			{
				OnLoadFinished();
			}
		}
		else if (itemID != -1)
		{
			if (itemLoader != null && !itemLoader.IsLoading())
			{
				OnLoadFinished();
			}
		}
		else if (skillItemID != -1)
		{
			if (itemLoader != null && !itemLoader.IsLoading())
			{
				OnLoadFinished();
			}
		}
		else if (skillSymbolItemID != -1)
		{
			if (itemLoader != null && !itemLoader.IsLoading())
			{
				OnLoadFinished();
			}
		}
		else if (enemyID == -1 && accessoryID != -1 && itemLoader != null && !itemLoader.IsLoading())
		{
			OnLoadFinished();
		}
	}

	public void Clear()
	{
		if (uiRenderTexture != null)
		{
			uiRenderTexture.Release();
			Object.Destroy(uiRenderTexture);
			uiRenderTexture = null;
		}
		DeleteModel();
		playerLoadInfo = null;
		npcData = null;
		equipItemID = -1;
		skillItemID = -1;
		skillSymbolItemID = -1;
		itemID = -1;
		enemyID = -1;
		accessoryID = -1;
		foundationName = null;
		uiTexture = null;
		referenceSexID = -1;
		referenceFaceID = -1;
	}

	private void OnEnable()
	{
		ReloadModel();
	}

	private void OnDisable()
	{
		if (!AppMain.isApplicationQuit)
		{
			StopAudio(15);
		}
	}

	private void OnDestroy()
	{
		if (!AppMain.isApplicationQuit)
		{
			StopAudio(0);
			Clear();
		}
	}

	private void LoadStart()
	{
		if ((!(itemLoader != null) || itemLoader.isLoading) && (!(npcLoader != null) || npcLoader.isLoading) && (!(enemyLoader != null) || enemyLoader.isLoading))
		{
			if (coroutine != null)
			{
				this.StopCoroutine(coroutine);
				coroutine = null;
			}
			uiRenderTexture.enableTexture = false;
		}
	}

	private IEnumerator DoViewing()
	{
		if (model == null)
		{
			Log.Error("model is null!!");
			yield break;
		}
		model.set_localEulerAngles(Vector3.get_zero());
		uiRenderTexture.enableTexture = true;
		float rot_wait = 1f;
		if (playerLoader != null)
		{
			model.set_localPosition(modelPos);
			model.set_localEulerAngles(modelRot);
		}
		else if (npcLoader != null)
		{
			model.set_localPosition(modelPos);
			model.set_localEulerAngles(modelRot);
		}
		else if (enemyLoader != null)
		{
			if (enemyDispplayInfo == null)
			{
				Bounds val = default(Bounds);
				int i = 0;
				for (int num = enemyLoader.renderersBody.Length; i < num; i++)
				{
					val.Encapsulate(enemyLoader.renderersBody[i].get_bounds());
				}
				Vector3 extents = val.get_extents();
				float num2 = extents.x * 0.5f / Mathf.Tan((float)Math.PI / 8f) + 1f;
				Transform obj = model;
				Vector3 extents2 = val.get_extents();
				obj.set_localPosition(new Vector3(0f, extents2.y * -0.5f, num2));
				model.set_localEulerAngles(new Vector3(0f, 180f, 0f));
			}
			else
			{
				model.set_localPosition(new Vector3(0f, -0.8f, 5f));
				if (enemyDispplayInfo.seIdhowl > 0 && isEnemyHowl)
				{
					audioObject = SoundManager.PlayUISE(enemyDispplayInfo.seIdhowl);
				}
				enemyLoader.body.set_localPosition(enemyDispplayInfo.pos);
				enemyLoader.body.set_localEulerAngles(new Vector3(0f, enemyDispplayInfo.angleY, 0f));
				enemyAnimCtrl = model.get_gameObject().AddComponent<EnemyAnimCtrl>();
				enemyAnimCtrl.Init(enemyLoader, uiRenderTexture.renderCamera);
				Animator animator = enemyLoader.GetAnimator();
				if (animator != null)
				{
					int num3 = Animator.StringToHash("Base Layer.GACHA_HOWL");
					if (animator.HasState(0, num3))
					{
						animator.Play(num3, 0, 0f);
						animator.Update(0f);
					}
				}
			}
		}
		else if (itemLoader != null && accessoryID != -1)
		{
			model.set_localEulerAngles(new Vector3(0f, 180f, 0f));
		}
		Vector3 lightDir = new Vector3(1.19f, -1.59f, -1f);
		Quaternion rotation = Quaternion.AngleAxis(1f, new Vector3(-0.07124705f, 0f, -0.9974587f));
		MeshRenderer renderer = null;
		if (lightRotation)
		{
			renderer = model.GetComponentInChildren<MeshRenderer>();
		}
		if (oneshot)
		{
			yield return (object)new WaitForEndOfFrame();
			if (uiRenderTexture != null && uiRenderTexture.renderCamera != null)
			{
				uiRenderTexture.renderCamera.set_enabled(false);
			}
			DeleteModel();
			yield break;
		}
		while (true)
		{
			if (itemLoader != null)
			{
				model.set_localPosition(new Vector3(0f, 0f, itemLoader.displayInfo.zFromCamera));
				itemLoader.ApplyDisplayInfo();
				if (rot_wait <= 0f)
				{
					model.Rotate(new Vector3(0f, rotateSpeed, 0f) * Time.get_deltaTime());
				}
				else
				{
					rot_wait -= Time.get_deltaTime();
				}
				if (lightRotation)
				{
					lightDir = rotation * lightDir;
					renderer.get_material().SetVector("_LightDir", Vector4.op_Implicit(lightDir));
				}
			}
			yield return null;
		}
	}

	public void SetApplyEnemyRootMotion(bool enable)
	{
		if (!(enemyLoader == null) && !(enemyLoader.animator == null))
		{
			enemyLoader.animator.set_applyRootMotion(enable);
		}
	}

	public void PlayRandomEnemyAnimation()
	{
		if (!(enemyLoader == null) && !(enemyLoader.animator == null))
		{
			Object.Destroy(enemyAnimCtrl);
			this.StartCoroutine(_PlayRandomEnemyAnimation());
		}
	}

	private IEnumerator _PlayRandomEnemyAnimation()
	{
		if (!isRandomPlaying)
		{
			isRandomPlaying = true;
			int animIndex = Random.Range(0, enemyLoader.animEventData.animations.Length);
			enemyLoader.animator.Play(enemyLoader.animEventData.animations[animIndex].name);
			yield return (object)new WaitForSeconds(enemyLoader.animator.GetCurrentAnimatorClipInfo(0)[0].get_clip().get_length() - 0.5f);
			isRandomPlaying = false;
			enemyLoader.animator.CrossFade("IDLE", 0.5f);
		}
	}

	public Transform GetModelTransform()
	{
		return model;
	}

	public void StopAudio(int fade_count)
	{
		if (audioObject != null)
		{
			audioObject.Stop(fade_count);
			audioObject = null;
		}
	}
}
