using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettingsManager : MonoBehaviourSingleton<GlobalSettingsManager>
{
	[Serializable]
	public class PlayerVisual
	{
		public int[] baseBodyIDs;

		public int[] manHeadIDs;

		public int[] womanHeadIDs;

		public int[] manFaceIDs;

		public int[] womanFaceIDs;

		public Color[] skinColors;

		public Color[] hairColors;

		public Color[] modelElementColors;

		public Color modelBaseColor;

		public Color[] modelElementColors2;

		public Color modelBaseColor2;

		public float skinColorCoef;

		public int[] primeLegIDs;

		public int[] mannequinBodyIDs;

		public int[] mannequinLegIDs;

		public Color mannequinSkinColor;

		public Material mannequinMaterial;

		public float shadowSize;

		public float height;

		public float radius;

		public StageObject.StampInfo[] stampInfos;

		public int[] GetHeadIDs(int sex)
		{
			if (sex == 0)
			{
				return manHeadIDs;
			}
			return womanHeadIDs;
		}

		public int[] GetFaceIDs(int sex)
		{
			if (sex == 0)
			{
				return manFaceIDs;
			}
			return womanFaceIDs;
		}

		public int GetHairModelID(int sex, int hair_id)
		{
			int[] headIDs = GetHeadIDs(sex);
			if (hair_id >= headIDs.Length)
			{
				hair_id = 0;
			}
			return headIDs[hair_id];
		}

		public int GetFaceModelID(int sex, int face_id)
		{
			int[] faceIDs = GetFaceIDs(sex);
			if (face_id >= faceIDs.Length)
			{
				face_id = 0;
			}
			return faceIDs[face_id];
		}

		public Color GetSkinColor(int id)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (id < 0 || id >= skinColors.Length)
			{
				return Color.get_white();
			}
			return skinColors[id];
		}

		public Color GetHairColor(int id)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (id < 0 || id >= hairColors.Length)
			{
				return Color.get_white();
			}
			return hairColors[id];
		}

		public Color GetModelElementColor(int id)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (id < 0 || id >= modelElementColors.Length)
			{
				return Color.get_white();
			}
			return modelElementColors[id];
		}

		public Color GetModelElementColor2(int id)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (id < 0 || id >= modelElementColors2.Length)
			{
				return Color.get_white();
			}
			return modelElementColors2[id];
		}
	}

	[Serializable]
	public class LinkResources
	{
		public GameObject shadowPrefab;

		public GameObject shadowPrefabLightweight;

		[NonSerialized]
		public GameObject itemIconPrefab;

		public string itemIconPackageName;

		[NonSerialized]
		public GameObject itemIconDetailPrefab;

		public string itemIconDetailPackageName;

		[NonSerialized]
		public GameObject itemIconDetailSmallPrefab;

		public string itemIconDetailSmallPackageName;

		[NonSerialized]
		public GameObject itemIconMaterialPrefab;

		public string itemIconMaterialPackageName;

		[NonSerialized]
		public GameObject itemIconEquipMaterialPrefab;

		public string itemIconEquipMaterialPackageName;

		[NonSerialized]
		public GameObject accessoryIconPrefab;

		public string accessoryIconPrefabPackageName;

		public Texture errorIcon;

		public InGameSettingsManager.UseResources inGameCommonResources;

		public InGameSettingsManager.UseResources inGameQuestResources;

		[Tooltip("キャラ出現時のエフェクト名")]
		public string battleStartEffectName;

		[Tooltip("武器切り替え時のエフェクト名")]
		public string changeWeaponEffectName;

		[Tooltip("武器固有アクション発動エフェクト")]
		public string spActionStartEffectName;

		[Tooltip("弓の出血継続他人用エフェクト名")]
		public string arrowBleedOtherEffectName;

		[Tooltip("影縫矢が刺さったエフェクト名")]
		public string shadowSealingEffectName;

		[Tooltip("スタン用エフェクト")]
		public string[] stunnedEffectList;

		[Tooltip("敵の麻痺ヒットエフェクト名")]
		public string enemyParalyzeHitEffectName;

		[Tooltip("敵の毒ヒットエフェクト名")]
		public string enemyPoisonHitEffectName;

		[Tooltip("敵の凍結ヒットエフェクト名")]
		public string enemyFreezeHitEffectName;

		[Tooltip("敵の他人用簡易ヒットエフェクト名")]
		public string enemyOtherSimpleHitEffectName;

		public Transform CreateShadow(float size, float body_radius, float scale, bool fixedY0, Transform parent = null, bool is_lightweight = false)
		{
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			Transform val = (!is_lightweight) ? ResourceUtility.Realizes(shadowPrefab, parent, -1) : ResourceUtility.Realizes(shadowPrefabLightweight, parent, -1);
			size *= 0.5f;
			val.set_localPosition(new Vector3(0f, 0.01f, 0f));
			val.set_localEulerAngles(Vector3.get_zero());
			val.set_localScale(new Vector3(size, size, size));
			if (fixedY0)
			{
				val.get_gameObject().AddComponent<CircleShadow>();
			}
			return val;
		}

		public IEnumerator LoadIconPrefabs(MonoBehaviour mono)
		{
			LoadingQueue loadQueue = new LoadingQueue(mono);
			LoadObject itemIconLoadObject = loadQueue.Load(RESOURCE_CATEGORY.UI, itemIconPackageName, true);
			LoadObject itemIconDetailLoadObject = loadQueue.Load(RESOURCE_CATEGORY.UI, itemIconDetailPackageName, true);
			LoadObject itemIconDetailSmallLoadObject = loadQueue.Load(RESOURCE_CATEGORY.UI, itemIconDetailSmallPackageName, true);
			LoadObject itemIconMaterialLoadObject = loadQueue.Load(RESOURCE_CATEGORY.UI, itemIconMaterialPackageName, true);
			LoadObject itemIconEquipMaterialLoadObject = loadQueue.Load(RESOURCE_CATEGORY.UI, itemIconEquipMaterialPackageName, true);
			LoadObject accessoryIconLoadObject = loadQueue.Load(RESOURCE_CATEGORY.UI, accessoryIconPrefabPackageName, true);
			if (loadQueue.IsLoading())
			{
				yield return (object)loadQueue.Wait();
			}
			itemIconPrefab = (itemIconLoadObject.loadedObject as GameObject);
			itemIconDetailPrefab = (itemIconDetailLoadObject.loadedObject as GameObject);
			itemIconDetailSmallPrefab = (itemIconDetailSmallLoadObject.loadedObject as GameObject);
			itemIconMaterialPrefab = (itemIconMaterialLoadObject.loadedObject as GameObject);
			itemIconEquipMaterialPrefab = (itemIconEquipMaterialLoadObject.loadedObject as GameObject);
			accessoryIconPrefab = (accessoryIconLoadObject.loadedObject as GameObject);
		}
	}

	[Serializable]
	public class CameraParam
	{
		public float outGameFieldOfView = 75f;

		public float inGamePortraitFieldOfView = 70f;

		public float inGameLandscapeFieldOfView = 54.4322f;

		public float myhouseFieldOfView = 75f;

		public Vector3 myhousePos = new Vector3(0f, 1f, -2.5f);

		public Vector3 myhouseRot = new Vector3(13f, 0f, 0f);

		public Vector3 smithPos = new Vector3(0f, 0.7f, -5.77f);

		public Vector3 smithRot = new Vector3(31.5f, 0f, 0f);

		public Vector3 friendPos = new Vector3(-10.263f, 2.072f, -2.332f);

		public Vector3 friendRot = new Vector3(6.882624f, 332.0222f, 0.1092472f);
	}

	[Serializable]
	public class UIModelRenderingParam
	{
		[Serializable]
		public class DisplayInfo
		{
			public float zFromCamera;

			public Vector3 mainPos;

			public Vector3 mainRot;

			public Vector3 subPos;

			public Vector3 subRot;

			public DisplayInfo(DisplayInfo displayInfo)
			{
				//IL_0034: Unknown result type (might be due to invalid IL or missing references)
				//IL_0039: Unknown result type (might be due to invalid IL or missing references)
				//IL_0060: Unknown result type (might be due to invalid IL or missing references)
				//IL_0065: Unknown result type (might be due to invalid IL or missing references)
				//IL_008c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0091: Unknown result type (might be due to invalid IL or missing references)
				//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
				//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
				zFromCamera = displayInfo.zFromCamera;
				mainPos = new Vector3(displayInfo.mainPos.x, displayInfo.mainPos.y, displayInfo.mainPos.z);
				mainRot = new Vector3(displayInfo.mainRot.x, displayInfo.mainRot.y, displayInfo.mainRot.z);
				subPos = new Vector3(displayInfo.subPos.x, displayInfo.subPos.y, displayInfo.subPos.z);
				subRot = new Vector3(displayInfo.subRot.x, displayInfo.subRot.y, displayInfo.subRot.z);
			}
		}

		public DisplayInfo[] WeaponDisplayInfos;

		public DisplayInfo armorDisplayInfo;

		public DisplayInfo helmDisplayInfo;

		public DisplayInfo armDisplayInfo;

		public DisplayInfo legDisplayInfo;

		public DisplayInfo itemDisplayInfo;

		public bool enableEnemyModelFoundationFromQuestStage;

		public bool enableEnemyModelEffectFromEnemyElement;

		public string[] enemyModelElementEffects;
	}

	[Serializable]
	public class PackParam
	{
		[Serializable]
		public class DisplayInfo
		{
			public Vector3 mainPos;

			public Vector3 mainRot;
		}

		[Serializable]
		public class PackInfo
		{
			public string bundleId;

			public string bundleName;

			public uint bundleImageId;

			public uint offerId;

			public float openAnimEndTime;

			public string eventName;

			public string chestName;

			public string popupAdsBanner;
		}

		[Serializable]
		public class SpecialInfo
		{
			public string specialId;

			public string specialEvent;
		}

		public string prefabBundleName;

		public List<PackInfo> packs;

		public List<SpecialInfo> specials;

		public PackInfo GetPack(string id)
		{
			return packs.Find((PackInfo o) => o.bundleId == id);
		}

		public bool HasPack(string id)
		{
			return packs.Find((PackInfo o) => o.bundleId == id) != null;
		}

		public int TotalPack()
		{
			return packs.Count;
		}

		public SpecialInfo GetSpecial(string id)
		{
			return specials.Find((SpecialInfo o) => o.specialId == id);
		}

		public bool HasSpecial(string id)
		{
			return specials.Find((SpecialInfo o) => o.specialId == id) != null;
		}
	}

	[Serializable]
	public class MysteryGiftParam
	{
		public float OpenAnimEndTime;
	}

	[Serializable]
	public class StatusBarParam
	{
		public int GoodIndex = 4;

		public int MediumIndex = 3;

		public int LowIndex = 2;

		public int BadIndex = 1;

		public int NoneIndex;

		public float GoodWifiState = 100f;

		public float MediumWifiState = 200f;

		public float LowWifiState = 300f;

		public float BadWifiState = 400f;

		public float GoodBattery = 80f;

		public float MediumBattery = 60f;

		public float LowBattery = 40f;

		public float BadBattery = 20f;

		public float HeartBeatUpdate = 5f;

		public int GetWifiLevelIndex(float latency)
		{
			if (latency <= GoodWifiState)
			{
				return GoodIndex;
			}
			if (latency <= MediumWifiState)
			{
				return MediumIndex;
			}
			if (latency <= LowWifiState)
			{
				return LowIndex;
			}
			if (latency <= BadWifiState)
			{
				return BadIndex;
			}
			return NoneIndex;
		}

		public int GetBatteryLevelIndex(float battery)
		{
			if (battery >= GoodBattery)
			{
				return GoodIndex;
			}
			if (battery >= MediumBattery)
			{
				return MediumIndex;
			}
			if (battery >= LowBattery)
			{
				return LowIndex;
			}
			if (battery >= BadBattery)
			{
				return BadIndex;
			}
			return NoneIndex;
		}
	}

	[Serializable]
	public class WorldMapParam
	{
		public float cameraManualDistance = 12f;

		public float cameraFovMin = 45f;

		public float cameraFovMax = 80f;

		public float cameraPinchSpeed = 0.03f;

		public float cameraMoveClampRight = 9.3f;

		public float cameraMoveClampLeft = 9.3f;

		public float cameraMoveClampUpper = 6f;

		public float cameraMoveClampLower = 8f;

		public float eventCameraDistance = 12f;

		public float eventCameraMoveTime = 0.5f;

		public float eventRemainTime = 0.5f;

		public float playerMarkerScaleTime = 0.3f;

		public float onlyCameraMoveDelay = 0.5f;

		public Vector3 playerMarkerOffset = new Vector3(-0.64f, -0.64f, 0f);

		public Vector3 playerMarkerUIOffset = new Vector3(-43f, 20f, -5f);

		public float encounterBossCutInTime = 3f;
	}

	[Serializable]
	public class ChatParam
	{
		public float limitDuration = 5.25f;

		public int limitCount = 3;
	}

	[Serializable]
	public class SkillItem
	{
		public float explanationAtkRateDispRate = 0.1f;

		public float explanationAtkDispRate = 0.02f;
	}

	[Serializable]
	public class HasVisuals
	{
		public int[] hasRawIds;

		public int[] hasManHeadIndexes;

		public int[] hasWomanHeadIndexes;

		public int[] hasManFaceIndexes;

		public int[] hasWomanFaceIndexes;

		public int[] hasSkinColorIndexes;

		public int[] hasHairColorIndexes;

		public int GetHeadIndex(bool isWoman, int index)
		{
			if (isWoman)
			{
				return hasWomanHeadIndexes[index];
			}
			return hasManHeadIndexes[index];
		}

		public int GetFaceIndex(bool isWoman, int index)
		{
			if (isWoman)
			{
				return hasWomanFaceIndexes[index];
			}
			return hasManFaceIndexes[index];
		}

		public void SetRawIds()
		{
			List<int> list = new List<int>();
			int[] array = hasManHeadIndexes;
			foreach (int index in array)
			{
				int rawId = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.ManHead, index);
				list.Add(rawId);
			}
			int[] array2 = hasWomanHeadIndexes;
			foreach (int index2 in array2)
			{
				int rawId2 = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.WomanHead, index2);
				list.Add(rawId2);
			}
			int[] array3 = hasManFaceIndexes;
			foreach (int index3 in array3)
			{
				int rawId3 = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.ManFace, index3);
				list.Add(rawId3);
			}
			int[] array4 = hasWomanFaceIndexes;
			foreach (int index4 in array4)
			{
				int rawId4 = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.WomanFace, index4);
				list.Add(rawId4);
			}
			int[] array5 = hasSkinColorIndexes;
			foreach (int index5 in array5)
			{
				int rawId5 = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.SkinColor, index5);
				list.Add(rawId5);
			}
			int[] array6 = hasHairColorIndexes;
			foreach (int index6 in array6)
			{
				int rawId6 = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.HairColor, index6);
				list.Add(rawId6);
			}
			hasRawIds = list.ToArray();
		}
	}

	public bool submissionVersion;

	public int tipsCount = 10;

	public float defaultUITransitionAnimTime = 0.25f;

	public PlayerVisual playerVisual;

	public int playerVoiceTypeCount;

	public float[] playerWeaponAttackRate;

	public CameraParam cameraParam;

	public UIModelRenderingParam uiModelRendering;

	public PackParam packParam;

	public MysteryGiftParam mysteryGiftParam;

	public StatusBarParam statusBarParam;

	public LinkResources linkResources;

	public Transform lightDirection;

	public Transform npcLightDirection;

	public Color defaultAmbientColor = new Color(0.8392157f, 0.8392157f, 0.8392157f);

	public WorldMapParam worldMapParam;

	public ChatParam chatParam;

	public SkillItem skillItem;

	public EquipModelHQTable equipModelHQTable;

	public string ignoreExternalSceneTableNamesAppVer;

	public List<string> useExternalSceneTableNames;

	public HasVisuals hasVisuals;

	public int unlockEventLevel = 20;

	public bool enableRepeatQuest = true;

	public bool enableBlackMarketBanner = true;

	private Quaternion initLightRot;

	private Quaternion initNpcLightRot;

	private Color initAmbientColor;

	private Quaternion lightRot;

	private Quaternion npcLightRot;

	private Color ambientColor;

	private void Start()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		initLightRot = lightDirection.get_localRotation();
		initNpcLightRot = npcLightDirection.get_localRotation();
		initAmbientColor = defaultAmbientColor;
	}

	public void ResetLightRot()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		lightDirection.set_localRotation(initLightRot);
		npcLightDirection.set_localRotation(initNpcLightRot);
	}

	public void ResetAmbientColor()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		defaultAmbientColor = initAmbientColor;
	}

	public void LoadLinkResources(Action callback)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(_LoadLinkResources(callback));
	}

	private IEnumerator _LoadLinkResources(Action callback)
	{
		yield return (object)this.StartCoroutine(linkResources.LoadIconPrefabs(this));
		callback.Invoke();
	}

	public void InitAvatarData()
	{
		if (Singleton<AvatarTable>.IsValid())
		{
			playerVisual.manHeadIDs = Singleton<AvatarTable>.I.manHeadIDs;
			playerVisual.womanHeadIDs = Singleton<AvatarTable>.I.womanHeadIDs;
			playerVisual.manFaceIDs = Singleton<AvatarTable>.I.manFaceIDs;
			playerVisual.womanFaceIDs = Singleton<AvatarTable>.I.womanFaceIDs;
			playerVisual.skinColors = Singleton<AvatarTable>.I.skinColors;
			playerVisual.hairColors = Singleton<AvatarTable>.I.hairColors;
			hasVisuals = new HasVisuals();
			hasVisuals.hasManHeadIndexes = Singleton<AvatarTable>.I.defaultHasManHeadIndexes;
			hasVisuals.hasWomanHeadIndexes = Singleton<AvatarTable>.I.defaultHasWomanHeadIndexes;
			hasVisuals.hasManFaceIndexes = Singleton<AvatarTable>.I.defaultHasManFaceIndexes;
			hasVisuals.hasWomanFaceIndexes = Singleton<AvatarTable>.I.defaultHasWomanFaceIndexes;
			hasVisuals.hasSkinColorIndexes = Singleton<AvatarTable>.I.defaultHasSkinColorIndexes;
			hasVisuals.hasHairColorIndexes = Singleton<AvatarTable>.I.defaultHasHairColorIndexes;
		}
	}

	public void SetHasVisuals(HasVisuals newVisuals)
	{
		if (newVisuals != null)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			List<int> list3 = new List<int>();
			List<int> list4 = new List<int>();
			List<int> list5 = new List<int>();
			List<int> list6 = new List<int>();
			list.AddRange(Singleton<AvatarTable>.I.defaultHasManHeadIndexes);
			list.AddRange(newVisuals.hasManHeadIndexes);
			list2.AddRange(Singleton<AvatarTable>.I.defaultHasWomanHeadIndexes);
			list2.AddRange(newVisuals.hasWomanHeadIndexes);
			list3.AddRange(Singleton<AvatarTable>.I.defaultHasManFaceIndexes);
			list3.AddRange(newVisuals.hasManFaceIndexes);
			list4.AddRange(Singleton<AvatarTable>.I.defaultHasWomanFaceIndexes);
			list4.AddRange(newVisuals.hasWomanFaceIndexes);
			list5.AddRange(Singleton<AvatarTable>.I.defaultHasSkinColorIndexes);
			list5.AddRange(newVisuals.hasSkinColorIndexes);
			list6.AddRange(Singleton<AvatarTable>.I.defaultHasHairColorIndexes);
			list6.AddRange(newVisuals.hasHairColorIndexes);
			hasVisuals = new HasVisuals();
			hasVisuals.hasManHeadIndexes = list.ToArray();
			hasVisuals.hasWomanHeadIndexes = list2.ToArray();
			hasVisuals.hasManFaceIndexes = list3.ToArray();
			hasVisuals.hasWomanFaceIndexes = list4.ToArray();
			hasVisuals.hasSkinColorIndexes = list5.ToArray();
			hasVisuals.hasHairColorIndexes = list6.ToArray();
			hasVisuals.SetRawIds();
		}
	}

	public void OnDiff(BaseModelDiff.DiffVisual diff)
	{
		SetHasVisuals(diff.update[0]);
	}

	private void LateUpdate()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if (lightRot != lightDirection.get_localRotation())
		{
			lightRot = lightDirection.get_localRotation();
			Shader.SetGlobalVector("_light_dir", lightDirection.get_forward().ToVector4());
		}
		if (npcLightRot != npcLightDirection.get_localRotation())
		{
			npcLightRot = npcLightDirection.get_localRotation();
			Shader.SetGlobalVector("_npc_light_dir", npcLightDirection.get_forward().ToVector4());
		}
		if (ambientColor != defaultAmbientColor)
		{
			ambientColor = defaultAmbientColor;
			Shader.SetGlobalVector("_ambient_color", Color.op_Implicit(ambientColor));
		}
	}

	public bool IsUnlockedAvatar(int itemId)
	{
		return Array.IndexOf(hasVisuals.hasRawIds, itemId) != -1;
	}
}
