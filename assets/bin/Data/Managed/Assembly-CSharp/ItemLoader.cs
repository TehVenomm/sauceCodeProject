using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : ModelLoaderBase
{
	private IEnumerator coroutine;

	private LoadingQueue loadingQueue;

	private Action callback;

	private int sexID = -1;

	private int faceID = -1;

	private int faceModelID = -1;

	public Transform _transform
	{
		get;
		private set;
	}

	public Transform nodeMain
	{
		get;
		private set;
	}

	public Transform nodeSub
	{
		get;
		private set;
	}

	public uint equipItemID
	{
		get;
		private set;
	}

	public uint itemID
	{
		get;
		private set;
	}

	public uint skillItemID
	{
		get;
		private set;
	}

	public GlobalSettingsManager.UIModelRenderingParam.DisplayInfo displayInfo
	{
		get;
		private set;
	}

	public bool isLoading => coroutine != null;

	public override bool IsLoading()
	{
		return isLoading;
	}

	public override Animator GetAnimator()
	{
		throw new NotImplementedException();
	}

	public override Transform GetHead()
	{
		throw new NotImplementedException();
	}

	public override void SetEnabled(bool is_enable)
	{
		throw new NotImplementedException();
	}

	private void Awake()
	{
		_transform = base.transform;
		Clear();
	}

	public void Load(SortCompareData data, Transform parent, int layer, int sex_id, int face_id, Action _callback = null)
	{
		if (data is EquipItemSortData || data is SmithCreateSortData)
		{
			LoadEquip(data.GetTableID(), parent, layer, sex_id, face_id, _callback);
		}
		else if (data is ItemSortData)
		{
			LoadItem(data.GetTableID(), parent, layer, _callback);
		}
		else if (data is SkillItemSortData)
		{
			LoadSkillItem(data.GetTableID(), parent, layer, _callback);
		}
		else
		{
			Clear();
		}
	}

	public void LoadEquip(uint equip_item_id, Transform parent, int layer, int sex_id, int face_id, Action _callback = null)
	{
		if (equipItemID == equip_item_id && sexID == sex_id && faceID == face_id)
		{
			_callback?.Invoke();
		}
		else
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equip_item_id);
			if (equipItemData != null)
			{
				switch (equipItemData.type)
				{
				case EQUIPMENT_TYPE.ARMOR:
				case EQUIPMENT_TYPE.VISUAL_ARMOR:
					Init(DoLoadFullBody(equipItemData), parent, layer, sex_id, face_id, _callback);
					break;
				case EQUIPMENT_TYPE.HELM:
				case EQUIPMENT_TYPE.VISUAL_HELM:
					Init(DoLoadHelm(equipItemData), parent, layer, sex_id, face_id, _callback);
					break;
				case EQUIPMENT_TYPE.ARM:
				case EQUIPMENT_TYPE.VISUAL_ARM:
					Init(DoLoadFullBody(equipItemData), parent, layer, sex_id, face_id, _callback);
					break;
				case EQUIPMENT_TYPE.LEG:
				case EQUIPMENT_TYPE.VISUAL_LEG:
					Init(DoLoadFullBody(equipItemData), parent, layer, sex_id, face_id, _callback);
					break;
				default:
					Init(DoLoadWeapon(equipItemData), parent, layer, sex_id, face_id, _callback);
					break;
				}
				equipItemID = equip_item_id;
			}
			else
			{
				Clear();
			}
		}
	}

	public void LoadItem(uint item_id, Transform parent, int layer, Action _callback = null)
	{
		if (itemID != item_id)
		{
			if (1000000 > item_id)
			{
				if (item_id != 1 && item_id != 2)
				{
					item_id = 2u;
				}
				Init(DoLoadItem(item_id), parent, layer, -1, -1, _callback);
			}
			else
			{
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(item_id);
				Init(DoLoadItem(itemData), parent, layer, -1, -1, _callback);
			}
			itemID = item_id;
		}
	}

	public void LoadSkillItem(uint skill_item_id, Transform parent, int layer, Action _callback = null)
	{
		if (skillItemID != skill_item_id)
		{
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(skill_item_id);
			if (skillItemData != null)
			{
				Init(DoLoadSkillItem(skillItemData), parent, layer, -1, -1, _callback);
				skillItemID = skill_item_id;
			}
			else
			{
				Clear();
			}
		}
	}

	public void LoadSkillItemSymbol(uint skill_item_id, Transform parent, int layer, Action _callback = null)
	{
		if (itemID != skill_item_id)
		{
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(skill_item_id);
			if (skillItemData != null)
			{
				if (skillItemData.iconID <= 0)
				{
					Clear();
				}
				else
				{
					Init(DoLoadSkillItemSymbol(skillItemData), parent, layer, -1, -1, _callback);
					itemID = skill_item_id;
				}
			}
			else
			{
				Clear();
			}
		}
	}

	public void LoadAccessory(uint accessory_id, Transform parent, int layer, Action _callback = null)
	{
		if (itemID != accessory_id)
		{
			AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData(accessory_id);
			Init(DoLoadAccessory(accessory_id), parent, layer, -1, -1, _callback);
			itemID = accessory_id;
		}
	}

	private void Init(IEnumerator _coroutine, Transform parent, int layer, int sex_id, int face_id, Action _callback)
	{
		Clear();
		if (sex_id == -1)
		{
			sexID = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		}
		else
		{
			sexID = sex_id;
		}
		faceID = face_id;
		if (face_id == -1)
		{
			faceModelID = MonoBehaviourSingleton<UserInfoManager>.I.GetFaceModelID();
		}
		else
		{
			faceModelID = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetFaceModelID(sexID, face_id);
		}
		callback = _callback;
		loadingQueue = new LoadingQueue(this);
		_transform.gameObject.layer = layer;
		coroutine = _coroutine;
		StartCoroutine(_coroutine);
	}

	public void Clear()
	{
		for (int num = _transform.childCount - 1; num >= 0; num--)
		{
			UnityEngine.Object.Destroy(_transform.GetChild(num).gameObject);
		}
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
			coroutine = null;
		}
		nodeSub = null;
		nodeMain = null;
		callback = null;
		equipItemID = 0u;
		itemID = uint.MaxValue;
		skillItemID = 0u;
		loadingQueue = null;
		sexID = -1;
		faceID = -1;
		faceModelID = 0;
	}

	private void OnDisable()
	{
		if (!AppMain.isApplicationQuit)
		{
			Clear();
		}
	}

	private IEnumerator DoLoadWeapon(EquipItemTable.EquipItemData data)
	{
		int modelID = data.GetModelID(sexID);
		string name = ResourceName.GetPlayerWeapon(modelID);
		byte highTex = 0;
		if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			EquipModelHQTable hqTable = MonoBehaviourSingleton<GlobalSettingsManager>.I.equipModelHQTable;
			highTex = hqTable.GetWeaponFlag(modelID);
		}
		LoadObject lo = loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_WEAPON, name);
		LoadObject lo_high_reso_tex = PlayerLoader.LoadHighResoTexs(loadingQueue, name, highTex);
		yield return (object)loadingQueue.Wait();
		Transform weapon = lo.Realizes(_transform, _transform.gameObject.layer);
		weapon.localPosition = Vector3.zero;
		weapon.localRotation = Quaternion.identity;
		Renderer[] renderers = weapon.GetComponentsInChildren<Renderer>();
		PlayerLoader.SetEquipColor3(renderers, NGUIMath.IntToColor(data.modelColor0), NGUIMath.IntToColor(data.modelColor1), NGUIMath.IntToColor(data.modelColor2));
		Material materialR = null;
		Material materialL = null;
		int j = 0;
		for (int i = renderers.Length; j < i; j++)
		{
			Renderer r = renderers[j];
			if (r.name.EndsWith("_L"))
			{
				materialL = r.material;
				nodeSub = r.transform;
			}
			else
			{
				materialR = r.material;
				nodeMain = r.transform;
			}
		}
		yield return (object)StartCoroutine(InitRoopEffect(loadingQueue, weapon, SHADER_TYPE.NORMAL));
		PlayerLoader.ApplyWeaponHighResoTexs(lo_high_reso_tex, highTex, materialR, materialL);
		displayInfo = new GlobalSettingsManager.UIModelRenderingParam.DisplayInfo(MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.WeaponDisplayInfos[(int)data.type]);
		if (data.id == 50020201 || data.id == 50020200)
		{
			displayInfo.mainPos = new Vector3(0f, 0f, -0.21f);
			displayInfo.mainRot.x += 180f;
			displayInfo.subRot.x += 180f;
		}
		if (data.id == 60020200 || data.id == 60020201 || data.id == 60020202)
		{
			displayInfo.mainPos = new Vector3(0f, 0f, 0f);
			displayInfo.mainRot = new Vector3(-64.50903f, 93.68915f, -118.1268f);
		}
		OnLoadFinished();
	}

	private IEnumerator DoLoadFullBody(EquipItemTable.EquipItemData data)
	{
		EquipModelTable.Data model_data = data.GetModelData(sexID);
		bool is_bdy = data.type == EQUIPMENT_TYPE.ARMOR || data.type == EQUIPMENT_TYPE.VISUAL_ARMOR;
		bool is_arm = data.type == EQUIPMENT_TYPE.ARM || data.type == EQUIPMENT_TYPE.VISUAL_ARM;
		bool is_leg = data.type == EQUIPMENT_TYPE.LEG || data.type == EQUIPMENT_TYPE.VISUAL_LEG;
		int sex_id = sexID;
		int face_id = faceModelID;
		int hair_id = -1;
		int body_id = (!is_bdy) ? MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.mannequinBodyIDs[sex_id] : data.GetModelID(sex_id);
		int arm_id = (!is_arm) ? (-1) : data.GetModelID(sex_id);
		int leg_id = (!is_leg) ? MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.mannequinLegIDs[sex_id] : data.GetModelID(sex_id);
		Color mannequin_color = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.mannequinSkinColor;
		Color skin_color = mannequin_color;
		Color hair_color = mannequin_color;
		Color equip_color = NGUIMath.IntToColor(data.modelColor0);
		LoadObject lo_face = (!model_data.needFace) ? null : loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_FACE, ResourceName.GetPlayerFace(face_id));
		LoadObject lo_hair = (hair_id <= -1) ? null : loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_HEAD, ResourceName.GetPlayerHead(hair_id));
		LoadObject lo_body = loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_BDY, ResourceName.GetPlayerBody(body_id));
		LoadObject lo_arm = (!model_data.needArm || arm_id <= -1) ? null : loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ARM, ResourceName.GetPlayerArm(arm_id));
		LoadObject lo_leg = (!model_data.needLeg || leg_id <= -1) ? null : loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_LEG, ResourceName.GetPlayerLeg(leg_id));
		yield return (object)loadingQueue.Wait();
		Transform body = lo_body.Realizes(_transform, _transform.gameObject.layer);
		body.localPosition = Vector3.zero;
		body.localRotation = Quaternion.identity;
		PlayerLoader.SetSkinAndEquipColor(body, skin_color, (!is_bdy) ? mannequin_color : equip_color, 0f);
		if (!is_bdy)
		{
			_SetMannequinMaterial(body);
		}
		else
		{
			yield return (object)StartCoroutine(InitRoopEffect(loadingQueue, body, SHADER_TYPE.NORMAL));
		}
		nodeMain = body;
		SkinnedMeshRenderer body_skin = body.GetComponentInChildren<SkinnedMeshRenderer>();
		Transform head_node = Utility.Find(body, "Head");
		Transform L_Upperarm = Utility.Find(body, "L_Upperarm");
		Transform R_Upperarm = Utility.Find(body, "R_Upperarm");
		if ((UnityEngine.Object)L_Upperarm != (UnityEngine.Object)null && (UnityEngine.Object)R_Upperarm != (UnityEngine.Object)null)
		{
			Vector3 angle = L_Upperarm.localEulerAngles;
			angle.y = -40f;
			L_Upperarm.localEulerAngles = angle;
			angle = R_Upperarm.localEulerAngles;
			angle.y = -40f;
			R_Upperarm.localEulerAngles = angle;
		}
		if (lo_face != null)
		{
			Transform face = lo_face.Realizes(head_node, _transform.gameObject.layer);
			PlayerLoader.SetSkinColor(face, skin_color);
			_SetMannequinMaterial(face);
		}
		if (lo_hair != null)
		{
			Transform hair = lo_hair.Realizes(head_node, _transform.gameObject.layer);
			PlayerLoader.SetEquipColor(hair, hair_color);
			_SetMannequinMaterial(hair);
		}
		if (lo_arm != null)
		{
			Transform arm = PlayerLoader.AddSkin(lo_arm, body_skin, _transform.gameObject.layer);
			PlayerLoader.SetSkinAndEquipColor(arm, skin_color, (!is_arm) ? mannequin_color : equip_color, (!is_arm) ? 0.0001f : model_data.GetZBias());
			if (is_arm)
			{
				PlayerLoader.InvisibleBodyTriangles(model_data.bodyDraw, body_skin);
			}
			if (!is_arm)
			{
				_SetMannequinMaterial(arm);
			}
		}
		if (lo_leg != null)
		{
			Transform leg = PlayerLoader.AddSkin(lo_leg, body_skin, _transform.gameObject.layer);
			PlayerLoader.SetSkinAndEquipColor(leg, skin_color, (!is_leg) ? mannequin_color : equip_color, (!is_leg) ? 0.0001f : model_data.GetZBias());
			if (!is_leg)
			{
				_SetMannequinMaterial(leg);
			}
		}
		if (is_bdy)
		{
			displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.armorDisplayInfo;
		}
		else if (is_arm)
		{
			displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.armDisplayInfo;
		}
		else if (is_leg)
		{
			displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.legDisplayInfo;
		}
		OnLoadFinished();
	}

	private void _SetMannequinMaterial(Transform t)
	{
		Renderer[] componentsInChildren = t.GetComponentsInChildren<Renderer>();
		int i = 0;
		for (int num = componentsInChildren.Length; i < num; i++)
		{
			componentsInChildren[i].material = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.mannequinMaterial;
		}
	}

	private IEnumerator DoLoadHelm(EquipItemTable.EquipItemData data)
	{
		EquipModelTable.Data model_data = data.GetModelData(sexID);
		LoadObject lo_head = loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_HEAD, ResourceName.GetPlayerHead(data.GetModelID(sexID)));
		LoadObject lo_face = null;
		if (model_data.needFace)
		{
			lo_face = loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_FACE, ResourceName.GetPlayerFace(faceModelID));
		}
		yield return (object)loadingQueue.Wait();
		Transform head = lo_head.Realizes(_transform, _transform.gameObject.layer);
		head.localPosition = Vector3.zero;
		head.localRotation = Quaternion.identity;
		PlayerLoader.SetEquipColor(head, NGUIMath.IntToColor(data.modelColor0));
		nodeMain = head;
		yield return (object)StartCoroutine(InitRoopEffect(loadingQueue, head, SHADER_TYPE.NORMAL));
		if (lo_face != null)
		{
			Transform face = lo_face.Realizes(head, _transform.gameObject.layer);
			_SetMannequinMaterial(face);
		}
		displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.helmDisplayInfo;
		OnLoadFinished();
	}

	private IEnumerator DoLoadItem(ItemTable.ItemData data)
	{
		return DoLoadItem((uint)data.iconID);
	}

	private IEnumerator DoLoadItem(uint itemID)
	{
		LoadObject lo = loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetItemModel((int)itemID));
		yield return (object)loadingQueue.Wait();
		Transform item = lo.Realizes(_transform, _transform.gameObject.layer);
		item.localPosition = Vector3.zero;
		item.localRotation = Quaternion.identity;
		nodeMain = item;
		displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.itemDisplayInfo;
		OnLoadFinished();
	}

	private IEnumerator DoLoadSkillItem(SkillItemTable.SkillItemData data)
	{
		LoadObject lo = loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(data.modelID));
		yield return (object)loadingQueue.Wait();
		Transform item = lo.Realizes(_transform, _transform.gameObject.layer);
		item.localPosition = Vector3.zero;
		item.localRotation = Quaternion.identity;
		PlayerLoader.SetEquipColor(item, data.modelColor.ToColor());
		nodeMain = item;
		displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.itemDisplayInfo;
		OnLoadFinished();
	}

	private IEnumerator DoLoadSkillItemSymbol(SkillItemTable.SkillItemData data)
	{
		LoadObject lo = loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemSymbolModel(data.iconID));
		yield return (object)loadingQueue.Wait();
		Transform item = lo.Realizes(_transform, _transform.gameObject.layer);
		item.localPosition = Vector3.zero;
		item.localRotation = Quaternion.identity;
		nodeMain = item;
		displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.itemDisplayInfo;
		OnLoadFinished();
	}

	private IEnumerator DoLoadAccessory(uint accessoryID)
	{
		LoadObject lo = loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ACCESSORY, ResourceName.GetPlayerAccessory(accessoryID));
		yield return (object)loadingQueue.Wait();
		Transform item = lo.Realizes(_transform, _transform.gameObject.layer);
		item.localPosition = Vector3.zero;
		item.localRotation = Quaternion.identity;
		nodeMain = item;
		displayInfo = MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.itemDisplayInfo;
		OnLoadFinished();
	}

	private void OnLoadFinished()
	{
		ApplyDisplayInfo();
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
			coroutine = null;
		}
		Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
		ShaderGlobal.ChangeWantUIShader(componentsInChildren);
		if (callback != null)
		{
			callback();
		}
	}

	public void ApplyDisplayInfo()
	{
		if (displayInfo != null)
		{
			if ((UnityEngine.Object)nodeMain != (UnityEngine.Object)null)
			{
				nodeMain.localPosition = displayInfo.mainPos;
				nodeMain.localEulerAngles = displayInfo.mainRot;
			}
			if ((UnityEngine.Object)nodeSub != (UnityEngine.Object)null)
			{
				nodeSub.localPosition = displayInfo.subPos;
				nodeSub.localEulerAngles = displayInfo.subRot;
			}
		}
	}

	public static IEnumerator InitRoopEffect(LoadingQueue queue, Transform equipItemRoot, SHADER_TYPE shaderType = SHADER_TYPE.NORMAL)
	{
		EffectPlayProcessor processor = equipItemRoot.gameObject.GetComponentInChildren<EffectPlayProcessor>();
		if ((UnityEngine.Object)processor != (UnityEngine.Object)null && processor.effectSettings != null)
		{
			int j = 0;
			for (int len = processor.effectSettings.Length; j < len; j++)
			{
				if (!string.IsNullOrEmpty(processor.effectSettings[j].effectName))
				{
					queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, processor.effectSettings[j].effectName);
				}
			}
		}
		yield return (object)queue.Wait();
		if ((UnityEngine.Object)processor != (UnityEngine.Object)null)
		{
			List<Transform> trans = processor.PlayEffect("InitRoop", null);
			if (trans != null)
			{
				for (int i = 0; i < trans.Count; i++)
				{
					Utility.SetLayerWithChildren(trans[i], equipItemRoot.gameObject.layer);
					if (shaderType != 0)
					{
						Renderer[] rs = trans[i].GetComponentsInChildren<Renderer>();
						switch (shaderType)
						{
						case SHADER_TYPE.LIGHTWEIGHT:
							ShaderGlobal.ChangeWantLightweightShader(rs);
							break;
						case SHADER_TYPE.UI:
							ShaderGlobal.ChangeWantUIShader(rs);
							break;
						}
					}
				}
			}
		}
	}
}
