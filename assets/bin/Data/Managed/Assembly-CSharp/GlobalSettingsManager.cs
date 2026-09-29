// Decompiled with JetBrains decompiler
// Type: GlobalSettingsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
public class GlobalSettingsManager : MonoBehaviourSingleton<GlobalSettingsManager>
{
  public bool submissionVersion;
  public int tipsCount = 10;
  public float defaultUITransitionAnimTime = 0.25f;
  public GlobalSettingsManager.PlayerVisual playerVisual;
  public int playerVoiceTypeCount;
  public float[] playerWeaponAttackRate;
  public GlobalSettingsManager.CameraParam cameraParam;
  public GlobalSettingsManager.UIModelRenderingParam uiModelRendering;
  public GlobalSettingsManager.PackParam packParam;
  public GlobalSettingsManager.MysteryGiftParam mysteryGiftParam;
  public GlobalSettingsManager.StatusBarParam statusBarParam;
  public GlobalSettingsManager.LinkResources linkResources;
  public Transform lightDirection;
  public Transform npcLightDirection;
  public Color defaultAmbientColor = new Color(0.8392157f, 0.8392157f, 0.8392157f);
  public GlobalSettingsManager.WorldMapParam worldMapParam;
  public GlobalSettingsManager.ChatParam chatParam;
  public GlobalSettingsManager.SkillItem skillItem;
  public EquipModelHQTable equipModelHQTable;
  public string ignoreExternalSceneTableNamesAppVer;
  public List<string> useExternalSceneTableNames;
  public GlobalSettingsManager.HasVisuals hasVisuals;
  public GlobalSettingsManager.HasSymbols hasSymbols;
  public GlobalSettingsManager.InGameFieldSetting inGameFieldSetting;
  public int unlockEventLevel = 20;
  public bool enableRepeatQuest = true;
  public bool enableBlackMarketBanner = true;
  public bool enableFortuneWheelBanner = true;
  public bool enableTradingPostBanner = true;
  public List<int> bossIdqQuest = new List<int>()
  {
    994419202,
    994417601,
    994412511
  };
  public int AndroidMemoryClearDivider = 6;
  public int IosMemoryClearDivider = 4;
  public List<string> stageCache = new List<string>();
  public List<string> skyboxCaches = new List<string>()
  {
    "SK011D_01",
    "SK062D_01",
    "SK001D_01",
    "SK011D_01",
    "SK001D_02",
    "SK022D_02",
    "SK028D_01",
    "SK062D_01",
    "SK001D_01",
    "SK001D_01",
    "SK011D_01",
    "SK011D_01",
    "SK022N_01",
    "SK022N_01",
    "SK001N_01",
    "SK001N_01",
    "SK022N_01",
    "SK092N_02",
    "SK092N_01",
    "SK092N_01",
    "SK092N_03",
    "SK006D_01",
    "SK006D_01",
    "SK001D_01",
    "SK001D_01",
    "SK021N_02"
  };
  public List<string> stageEffectCaches = new List<string>()
  {
    "ef_btl_bg_hp001_01",
    "ef_btl_bg_questboard_01",
    "ef_btl_bg_hp001_02",
    "ef_btl_bg_questboard_02"
  };
  public List<string> stageShouldPreOpenCamera = new List<string>()
  {
    "HomeTop",
    "StatusTop",
    "ShopTop"
  };
  public List<int> noBlurEffectEventId = new List<int>()
  {
    99001301
  };
  public List<int> noBlurEffectBossId = new List<int>()
  {
    994419602,
    993318100,
    993218200,
    300000522,
    100000524
  };
  public List<ITEM_TYPE> itemMaterialType = new List<ITEM_TYPE>()
  {
    ITEM_TYPE.MATERIAL_BONE,
    ITEM_TYPE.MATERIAL_CLOTH,
    ITEM_TYPE.MATERIAL_EQUIP,
    ITEM_TYPE.MATERIAL_METAL,
    ITEM_TYPE.MATERIAL_PELT,
    ITEM_TYPE.MATERIAL_SCALE,
    ITEM_TYPE.MATERIAL_WOOD,
    ITEM_TYPE.UNIQUE_MATERIAL,
    ITEM_TYPE.LITHOGRAPH
  };
  public List<ITEM_TYPE> itemItemType = new List<ITEM_TYPE>()
  {
    ITEM_TYPE.ABILITY_ITEM,
    ITEM_TYPE.USE_ITEM
  };
  public List<ITEM_TYPE> itemLapisType = new List<ITEM_TYPE>()
  {
    ITEM_TYPE.LAPIS
  };
  public List<ITEM_TYPE> itemMagiType = new List<ITEM_TYPE>()
  {
    ITEM_TYPE.MATERIAL_MAGI
  };
  public List<string> noBlurEffectForDeviceModel = new List<string>()
  {
    "OnePlus"
  };
  private Quaternion initLightRot;
  private Quaternion initNpcLightRot;
  private Color initAmbientColor;
  private Quaternion lightRot;
  private Quaternion npcLightRot;
  private Color ambientColor;

  private void Start()
  {
    this.initLightRot = this.lightDirection.localRotation;
    this.initNpcLightRot = this.npcLightDirection.localRotation;
    this.initAmbientColor = this.defaultAmbientColor;
    AppMain.amountMemoryClear = SystemInfo.systemMemorySize / this.AndroidMemoryClearDivider;
  }

  public void ResetLightRot()
  {
    this.lightDirection.localRotation = this.initLightRot;
    this.npcLightDirection.localRotation = this.initNpcLightRot;
  }

  public void ResetAmbientColor() => this.defaultAmbientColor = this.initAmbientColor;

  public void LoadLinkResources(System.Action callback)
  {
    this.StartCoroutine(this._LoadLinkResources(callback));
  }

  private IEnumerator _LoadLinkResources(System.Action callback)
  {
    yield return (object) this.StartCoroutine(this.linkResources.LoadIconPrefabs((MonoBehaviour) this));
    callback();
  }

  public void InitAvatarData()
  {
    if (!Singleton<AvatarTable>.IsValid())
      return;
    this.playerVisual.manHeadIDs = Singleton<AvatarTable>.I.manHeadIDs;
    this.playerVisual.womanHeadIDs = Singleton<AvatarTable>.I.womanHeadIDs;
    this.playerVisual.manFaceIDs = Singleton<AvatarTable>.I.manFaceIDs;
    this.playerVisual.womanFaceIDs = Singleton<AvatarTable>.I.womanFaceIDs;
    this.playerVisual.skinColors = Singleton<AvatarTable>.I.skinColors;
    this.playerVisual.hairColors = Singleton<AvatarTable>.I.hairColors;
    this.hasVisuals = new GlobalSettingsManager.HasVisuals();
    this.hasVisuals.hasManHeadIndexes = Singleton<AvatarTable>.I.defaultHasManHeadIndexes;
    this.hasVisuals.hasWomanHeadIndexes = Singleton<AvatarTable>.I.defaultHasWomanHeadIndexes;
    this.hasVisuals.hasManFaceIndexes = Singleton<AvatarTable>.I.defaultHasManFaceIndexes;
    this.hasVisuals.hasWomanFaceIndexes = Singleton<AvatarTable>.I.defaultHasWomanFaceIndexes;
    this.hasVisuals.hasSkinColorIndexes = Singleton<AvatarTable>.I.defaultHasSkinColorIndexes;
    this.hasVisuals.hasHairColorIndexes = Singleton<AvatarTable>.I.defaultHasHairColorIndexes;
  }

  public void SetHasVisuals(GlobalSettingsManager.HasVisuals newVisuals)
  {
    if (newVisuals == null)
      return;
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    List<int> intList3 = new List<int>();
    List<int> intList4 = new List<int>();
    List<int> intList5 = new List<int>();
    List<int> intList6 = new List<int>();
    intList1.AddRange((IEnumerable<int>) Singleton<AvatarTable>.I.defaultHasManHeadIndexes);
    intList1.AddRange((IEnumerable<int>) newVisuals.hasManHeadIndexes);
    intList2.AddRange((IEnumerable<int>) Singleton<AvatarTable>.I.defaultHasWomanHeadIndexes);
    intList2.AddRange((IEnumerable<int>) newVisuals.hasWomanHeadIndexes);
    intList3.AddRange((IEnumerable<int>) Singleton<AvatarTable>.I.defaultHasManFaceIndexes);
    intList3.AddRange((IEnumerable<int>) newVisuals.hasManFaceIndexes);
    intList4.AddRange((IEnumerable<int>) Singleton<AvatarTable>.I.defaultHasWomanFaceIndexes);
    intList4.AddRange((IEnumerable<int>) newVisuals.hasWomanFaceIndexes);
    intList5.AddRange((IEnumerable<int>) Singleton<AvatarTable>.I.defaultHasSkinColorIndexes);
    intList5.AddRange((IEnumerable<int>) newVisuals.hasSkinColorIndexes);
    intList6.AddRange((IEnumerable<int>) Singleton<AvatarTable>.I.defaultHasHairColorIndexes);
    intList6.AddRange((IEnumerable<int>) newVisuals.hasHairColorIndexes);
    this.hasVisuals = new GlobalSettingsManager.HasVisuals();
    this.hasVisuals.hasManHeadIndexes = intList1.ToArray();
    this.hasVisuals.hasWomanHeadIndexes = intList2.ToArray();
    this.hasVisuals.hasManFaceIndexes = intList3.ToArray();
    this.hasVisuals.hasWomanFaceIndexes = intList4.ToArray();
    this.hasVisuals.hasSkinColorIndexes = intList5.ToArray();
    this.hasVisuals.hasHairColorIndexes = intList6.ToArray();
    this.hasVisuals.SetRawIds();
  }

  public void OnDiff(BaseModelDiff.DiffVisual diff) => this.SetHasVisuals(diff.update[0]);

  private void LateUpdate()
  {
    if (Quaternion.op_Inequality(this.lightRot, this.lightDirection.localRotation))
    {
      this.lightRot = this.lightDirection.localRotation;
      Shader.SetGlobalVector("_light_dir", this.lightDirection.forward.ToVector4());
    }
    if (Quaternion.op_Inequality(this.npcLightRot, this.npcLightDirection.localRotation))
    {
      this.npcLightRot = this.npcLightDirection.localRotation;
      Shader.SetGlobalVector("_npc_light_dir", this.npcLightDirection.forward.ToVector4());
    }
    if (!Color.op_Inequality(this.ambientColor, this.defaultAmbientColor))
      return;
    this.ambientColor = this.defaultAmbientColor;
    Shader.SetGlobalVector("_ambient_color", Color.op_Implicit(this.ambientColor));
  }

  public bool IsUnlockedAvatar(int itemId)
  {
    return Array.IndexOf<int>(this.hasVisuals.hasRawIds, itemId) != -1;
  }

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

    public int[] GetHeadIDs(int sex) => sex == 0 ? this.manHeadIDs : this.womanHeadIDs;

    public int[] GetFaceIDs(int sex) => sex == 0 ? this.manFaceIDs : this.womanFaceIDs;

    public int GetHairModelID(int sex, int hair_id)
    {
      int[] headIds = this.GetHeadIDs(sex);
      if (hair_id >= headIds.Length)
        hair_id = 0;
      return headIds[hair_id];
    }

    public int GetFaceModelID(int sex, int face_id)
    {
      int[] faceIds = this.GetFaceIDs(sex);
      if (face_id >= faceIds.Length)
        face_id = 0;
      return faceIds[face_id];
    }

    public Color GetSkinColor(int id)
    {
      return id < 0 || id >= this.skinColors.Length ? Color.white : this.skinColors[id];
    }

    public Color GetHairColor(int id)
    {
      return id < 0 || id >= this.hairColors.Length ? Color.white : this.hairColors[id];
    }

    public Color GetModelElementColor(int id)
    {
      return id < 0 || id >= this.modelElementColors.Length ? Color.white : this.modelElementColors[id];
    }

    public Color GetModelElementColor2(int id)
    {
      return id < 0 || id >= this.modelElementColors2.Length ? Color.white : this.modelElementColors2[id];
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
    [Tooltip("魅了用エフェクト")]
    public string[] charmEffectList;
    [Tooltip("敵の麻痺ヒットエフェクト名")]
    public string enemyParalyzeHitEffectName;
    [Tooltip("敵の毒ヒットエフェクト名")]
    public string enemyPoisonHitEffectName;
    [Tooltip("敵の凍結ヒットエフェクト名")]
    public string enemyFreezeHitEffectName;
    [Tooltip("敵の他人用簡易ヒットエフェクト名")]
    public string enemyOtherSimpleHitEffectName;

    public List<string> GetEffectNames()
    {
      List<string> effectNames = new List<string>();
      foreach (FieldInfo field in this.GetType().GetFields())
      {
        object obj = field.GetValue((object) this);
        if (obj != null)
        {
          if (field.Name.EndsWith("EffectName"))
          {
            if (obj.GetType() == typeof (string))
            {
              string str = (string) obj;
              effectNames.Add(str);
            }
          }
          else if (field.Name.EndsWith("EffectList") && obj.GetType() == typeof (string[]))
          {
            string[] collection = (string[]) obj;
            effectNames.AddRange((IEnumerable<string>) collection);
          }
        }
      }
      return effectNames;
    }

    public Transform CreateShadow(
      float size,
      float body_radius,
      float scale,
      bool fixedY0,
      Transform parent = null,
      bool is_lightweight = false)
    {
      Transform shadow = !is_lightweight ? ResourceUtility.Realizes((Object) this.shadowPrefab, parent) : ResourceUtility.Realizes((Object) this.shadowPrefabLightweight, parent);
      size *= 0.5f;
      shadow.localPosition = new Vector3(0.0f, 0.01f, 0.0f);
      shadow.localEulerAngles = Vector3.zero;
      shadow.localScale = new Vector3(size, size, size);
      if (fixedY0)
        ((Component) shadow).gameObject.AddComponent<CircleShadow>();
      return shadow;
    }

    public IEnumerator LoadIconPrefabs(MonoBehaviour mono)
    {
      LoadingQueue loadingQueue = new LoadingQueue(mono);
      LoadObject itemIconLoadObject = loadingQueue.Load(RESOURCE_CATEGORY.UI, this.itemIconPackageName, true);
      LoadObject itemIconDetailLoadObject = loadingQueue.Load(RESOURCE_CATEGORY.UI, this.itemIconDetailPackageName, true);
      LoadObject itemIconDetailSmallLoadObject = loadingQueue.Load(RESOURCE_CATEGORY.UI, this.itemIconDetailSmallPackageName, true);
      LoadObject itemIconMaterialLoadObject = loadingQueue.Load(RESOURCE_CATEGORY.UI, this.itemIconMaterialPackageName, true);
      LoadObject itemIconEquipMaterialLoadObject = loadingQueue.Load(RESOURCE_CATEGORY.UI, this.itemIconEquipMaterialPackageName, true);
      LoadObject accessoryIconLoadObject = loadingQueue.Load(RESOURCE_CATEGORY.UI, this.accessoryIconPrefabPackageName, true);
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.itemIconPrefab = itemIconLoadObject.loadedObject as GameObject;
      this.itemIconDetailPrefab = itemIconDetailLoadObject.loadedObject as GameObject;
      this.itemIconDetailSmallPrefab = itemIconDetailSmallLoadObject.loadedObject as GameObject;
      this.itemIconMaterialPrefab = itemIconMaterialLoadObject.loadedObject as GameObject;
      this.itemIconEquipMaterialPrefab = itemIconEquipMaterialLoadObject.loadedObject as GameObject;
      this.accessoryIconPrefab = accessoryIconLoadObject.loadedObject as GameObject;
    }
  }

  [Serializable]
  public class CameraParam
  {
    public float outGameFieldOfView = 75f;
    public float inGamePortraitFieldOfView = 70f;
    public float inGameLandscapeFieldOfView = 54.4322f;
    public float myhouseFieldOfView = 75f;
    public Vector3 myhousePos = new Vector3(0.0f, 1f, -2.5f);
    public Vector3 myhouseRot = new Vector3(13f, 0.0f, 0.0f);
    public Vector3 smithPos = new Vector3(0.0f, 0.7f, -5.77f);
    public Vector3 smithRot = new Vector3(31.5f, 0.0f, 0.0f);
    public Vector3 friendPos = new Vector3(-10.263f, 2.072f, -2.332f);
    public Vector3 friendRot = new Vector3(6.882624f, 332.0222f, 0.1092472f);
  }

  [Serializable]
  public class UIModelRenderingParam
  {
    public GlobalSettingsManager.UIModelRenderingParam.DisplayInfo[] WeaponDisplayInfos;
    public GlobalSettingsManager.UIModelRenderingParam.DisplayInfo armorDisplayInfo;
    public GlobalSettingsManager.UIModelRenderingParam.DisplayInfo helmDisplayInfo;
    public GlobalSettingsManager.UIModelRenderingParam.DisplayInfo armDisplayInfo;
    public GlobalSettingsManager.UIModelRenderingParam.DisplayInfo legDisplayInfo;
    public GlobalSettingsManager.UIModelRenderingParam.DisplayInfo itemDisplayInfo;
    public bool enableEnemyModelFoundationFromQuestStage;
    public bool enableEnemyModelEffectFromEnemyElement;
    public string[] enemyModelElementEffects;

    [Serializable]
    public class DisplayInfo
    {
      public float zFromCamera;
      public Vector3 mainPos;
      public Vector3 mainRot;
      public Vector3 subPos;
      public Vector3 subRot;

      public DisplayInfo(
        GlobalSettingsManager.UIModelRenderingParam.DisplayInfo displayInfo)
      {
        this.zFromCamera = displayInfo.zFromCamera;
        this.mainPos = new Vector3(displayInfo.mainPos.x, displayInfo.mainPos.y, displayInfo.mainPos.z);
        this.mainRot = new Vector3(displayInfo.mainRot.x, displayInfo.mainRot.y, displayInfo.mainRot.z);
        this.subPos = new Vector3(displayInfo.subPos.x, displayInfo.subPos.y, displayInfo.subPos.z);
        this.subRot = new Vector3(displayInfo.subRot.x, displayInfo.subRot.y, displayInfo.subRot.z);
      }
    }
  }

  [Serializable]
  public class PackParam
  {
    public string prefabBundleName;
    public List<GlobalSettingsManager.PackParam.PackInfo> packs;
    public List<GlobalSettingsManager.PackParam.SpecialInfo> specials;

    public GlobalSettingsManager.PackParam.SpecialInfo GetSpecial(string id)
    {
      return this.specials.Find((Predicate<GlobalSettingsManager.PackParam.SpecialInfo>) (o => o.specialId == id));
    }

    public bool HasSpecial(string id)
    {
      return this.specials.Find((Predicate<GlobalSettingsManager.PackParam.SpecialInfo>) (o => o.specialId == id)) != null;
    }

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
      if ((double) latency <= (double) this.GoodWifiState)
        return this.GoodIndex;
      if ((double) latency <= (double) this.MediumWifiState)
        return this.MediumIndex;
      if ((double) latency <= (double) this.LowWifiState)
        return this.LowIndex;
      return (double) latency <= (double) this.BadWifiState ? this.BadIndex : this.NoneIndex;
    }

    public int GetBatteryLevelIndex(float battery)
    {
      if ((double) battery >= (double) this.GoodBattery)
        return this.GoodIndex;
      if ((double) battery >= (double) this.MediumBattery)
        return this.MediumIndex;
      if ((double) battery >= (double) this.LowBattery)
        return this.LowIndex;
      return (double) battery >= (double) this.BadBattery ? this.BadIndex : this.NoneIndex;
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
    public Vector3 playerMarkerOffset = new Vector3(-0.64f, -0.64f, 0.0f);
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
  public class HasSymbols
  {
    public int[] hasRawIds;
    public int[] hasMarkIndexes;
    public int[] hasFrameIndexes;
    public int[] hasPatternIndexes;
    public int[] hasColorIndexes;
    public Color[] symbolColors;
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
      return isWoman ? this.hasWomanHeadIndexes[index] : this.hasManHeadIndexes[index];
    }

    public int GetFaceIndex(bool isWoman, int index)
    {
      return isWoman ? this.hasWomanFaceIndexes[index] : this.hasManFaceIndexes[index];
    }

    public void SetRawIds()
    {
      List<int> intList = new List<int>();
      foreach (int hasManHeadIndex in this.hasManHeadIndexes)
      {
        int rawId = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.ManHead, hasManHeadIndex);
        intList.Add(rawId);
      }
      foreach (int hasWomanHeadIndex in this.hasWomanHeadIndexes)
      {
        int rawId = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.WomanHead, hasWomanHeadIndex);
        intList.Add(rawId);
      }
      foreach (int hasManFaceIndex in this.hasManFaceIndexes)
      {
        int rawId = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.ManFace, hasManFaceIndex);
        intList.Add(rawId);
      }
      foreach (int hasWomanFaceIndex in this.hasWomanFaceIndexes)
      {
        int rawId = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.WomanFace, hasWomanFaceIndex);
        intList.Add(rawId);
      }
      foreach (int hasSkinColorIndex in this.hasSkinColorIndexes)
      {
        int rawId = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.SkinColor, hasSkinColorIndex);
        intList.Add(rawId);
      }
      foreach (int hasHairColorIndex in this.hasHairColorIndexes)
      {
        int rawId = Singleton<AvatarTable>.I.GetRawId(AvatarTable.Type.HairColor, hasHairColorIndex);
        intList.Add(rawId);
      }
      this.hasRawIds = intList.ToArray();
    }
  }

  [Serializable]
  public class InGameFieldSetting
  {
    [Tooltip("ステージのコリジョンを高くする")]
    public bool isRaiseWallCollider;
    [Tooltip("高くする場合のサイズ(Box)")]
    public float raiseWallColliderSizeY = 20f;
    [Tooltip("オフセットY(Box)")]
    public float raiseWallColliderOffsetY;
    [Tooltip("スケールY(Mesh)")]
    public float raiseWallColliderScaleY = 1f;
  }
}
