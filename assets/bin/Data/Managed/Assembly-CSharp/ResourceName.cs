// Decompiled with JetBrains decompiler
// Type: ResourceName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public static class ResourceName
{
  public const string BOMB_ROCK_ATTACK_INFO_NAME = "bombrock";
  public const string CANNONBALL_HEAVY_ATTACK_INFO_NAME = "cannonball_heavy";
  public const string CANNONBALL_RAPID_ATTACK_INFO_NAME = "cannonball_rapid";
  public static readonly string CANNONBALL_SPECIAL_ATTACK_INFO_NAME = "cannonball_special";
  public const string HEAL_ATTACK_INFO_NAME = "sk_heal_atk";
  public const string HEAL_ATTACK_ZONE_INFO_NAME = "sk_heal_atk_zone";
  public const string CHARACTER_FREEZE_EFFECT_NAME = "ef_btl_pl_frozen_01";
  public const string ENEMY_SHADOW_SEALING_EFFECT_NAME = "ef_btl_wsk_bow_01_04";
  public const string ENEMY_CONCUSSION_EFFECT_NAME = "ef_btl_enm_flinch_01";
  public const string ENEMY_ELECTRIC_SHOCK_EFFECT_NAME = "ef_btl_enm_shock_01";
  public const string ENEMY_SOIL_SHOCK_EFFECT_NAME = "ef_btl_enm_gravity_01";
  public const string ENEMY_BURNING_EFFECT_NAME = "ef_btl_enm_fire_01";
  public const string ENEMY_SPEED_DOWN_EFFECT_NAME = "ef_btl_pl_movedown_01";
  public const string ENEMY_LIGHT_RING_EFFECT_NAME = "ef_btl_enm_bindring_01";
  public const string ENEMY_EROSION_EFFECT_NAME = "ef_btl_enm_erosion_01";
  public const string CHARACTER_STONE_EFFECT_NAME = "ef_btl_pl_stone_01";
  public const string ENEMY_ACID_EFFECT_NAME = "ef_btl_enm_acid_01";
  public const string ENEMY_CORRUPTION_EFFECT_NAME = "ef_btl_enm_corruption_01";
  public const string ENEMY_BOSS_ENTRY_EXIT_EFFECT = "ef_btl_enemy_entry_01";
  public const string ENEMY_SUMMON_EFFECT = "ef_btl_enm_summon_01";
  public const string PLAYER_TELEPORTATION_ENTER_EFFECT = "ef_btl_sk_warp_02_01";
  public const string PLAYER_TELEPORTATION_EXIT_EFFECT = "ef_btl_sk_warp_02_02";
  public const string PLAYER_COUNTER_ATTACK_EFFECT = "ef_btl_ab_charge_01";
  public const string ORACLE_OHS_PROTECTION_EFFECT = "ef_btl_wsk4_sword_dragon_veil";
  public const string ORACLE_OHS_PROTECTION_DOUBLE_PROBABIRITY_EFFECT = "ef_btl_wsk4_sword_dragon_veil_re";
  public const string ORACLE_OHS_BOOST_EFFECT_FORMAT = "ef_btl_wsk4_sword_02_{0:D2}";

  public static string ToHash256String(this RESOURCE_CATEGORY category, byte hash)
  {
    return $"{category.ToString()}_{hash:X2}";
  }

  public static string ToHash256String(this RESOURCE_CATEGORY category, string name)
  {
    return $"{category.ToString()}_{(ValueType) (byte) Utility.GetHash(name):X2}";
  }

  public static string ToAssetBundleName(this RESOURCE_CATEGORY category, string package_name = null)
  {
    string str1 = category.ToString();
    if (ResourceDefine.types[(int) category] != ResourceManager.CATEGORY_TYPE.PACK && category != RESOURCE_CATEGORY.ASSETBUNDLEINFO)
      str1 = $"{str1}/{package_name}";
    string str2;
    if (category == RESOURCE_CATEGORY.UI_ATLAS)
    {
      if (!str1.EndsWith("_bundle"))
        str1 += "_bundle";
      str2 = str1 + ResourceDefine.suffix[(int) category] + GoGameResourceManager.GetDefaultAssetBundleExtension();
    }
    else
      str2 = str1 + ResourceDefine.suffix[(int) category] + GoGameResourceManager.GetDefaultAssetBundleExtension();
    return str2.ToLower();
  }

  public static string Normalize(string name)
  {
    return !name.StartsWith("internal__") ? name : name.Substring(name.LastIndexOf("__") + 2);
  }

  public static string GetSE(int se_id)
  {
    return MonoBehaviourSingleton<ResourceManager>.IsValid() && MonoBehaviourSingleton<ResourceManager>.I.cache != null ? MonoBehaviourSingleton<ResourceManager>.I.cache.GetSEName(se_id) : ResourceName.CreateSEName(se_id);
  }

  public static string CreateSEName(int se_id) => $"SE_{se_id:D8}";

  public static string GetSEPackage(int se_id)
  {
    return RESOURCE_CATEGORY.SOUND_SE.ToHash256String(ResourceName.GetSE(se_id));
  }

  public static string GetBGM(int bgm_id) => $"BGM_{bgm_id:D8}";

  public static string GetActionVoicePackageName(int sex, int voice_type_id)
  {
    return $"ACV_{voice_type_id * 10 + sex:D4}";
  }

  public static string GetActionVoicePackageNameFromVoiceID(int voice_id)
  {
    return $"ACV_{voice_id / 10000:D4}";
  }

  public static string GetActionVoiceName(int voice_id) => $"ACV_{voice_id:D8}";

  public static string GetActionVoiceName(int sex, int voice_type_id, int voice_id)
  {
    return $"ACV_{(voice_type_id * 10 + sex) * 10000 + voice_id:D8}";
  }

  public static string GetStoryVoicePackageNameFromVoiceID(int voice_id)
  {
    return ResourceName.GetNPCVoicePackageNameFromVoiceID(voice_id);
  }

  public static string GetStoryVoiceName(int voice_id) => ResourceName.GetNPCVoiceName(voice_id);

  public static string GetNPCVoicePackageName(int npc_id) => $"NPV_{npc_id:D3}";

  public static string GetNPCVoicePackageNameFromVoiceID(int voice_id)
  {
    return $"NPV_{voice_id / 100000:D3}";
  }

  public static string GetNPCVoiceName(int npc_id, int type, int no)
  {
    return ResourceName.GetNPCVoiceName(npc_id * 100000 + type * 100 + no);
  }

  public static string GetNPCVoiceName(int npc_id, int no)
  {
    return ResourceName.GetNPCVoiceName(npc_id * 100000 + no);
  }

  public static string GetNPCVoiceName(int npc_voice_id) => $"NPV_{npc_voice_id:D8}";

  public static string AddAttributID(string name)
  {
    if (name[name.Length - 1] == '_')
    {
      int num = !MonoBehaviourSingleton<SceneSettingsManager>.IsValid() ? 1 : MonoBehaviourSingleton<SceneSettingsManager>.I.attributeID;
      name = $"{name}{num:00}";
    }
    return name;
  }

  public static string GetTipsImage(int id) => $"TPS_{id:D8}";

  public static string GetRushTipsImage(int id) => $"TPS_RUSH_{id:D8}";

  public static string GetSkillGachaBannerImage(int id) => $"SGI_{id:D9}";

  public static string GetPlayerBody(int id) => $"BDY{id / 1000:D2}_{id % 1000:D3}";

  public static string GetPlayerArm(int id) => $"ARM{id / 1000:D2}_{id % 1000:D3}";

  public static string GetPlayerLeg(int id) => $"LEG{id / 1000:D2}_{id % 1000:D3}";

  public static string GetPlayerFace(int id) => $"PLF{id / 1000:D2}_{id % 1000:D3}";

  public static string GetPlayerHead(int id) => $"HED{id / 1000:D2}_{id % 1000:D3}";

  public static string GetPlayerWeapon(int id)
  {
    return id <= 0 ? string.Empty : $"WEP{id / 1000:D2}_{id % 1000:D3}";
  }

  public static string GetPlayerAccessory(uint id) => $"ACC_{id:D8}";

  public static string GetPlayerAnim(int anim_id) => $"PLC{anim_id:D2}_Anim";

  public static string GetPlayerSubAnim(int anim_id, string ctrl_name)
  {
    return string.Format("PLC_{1}_Anim", (object) anim_id, (object) ctrl_name);
  }

  public static string GetPlayerAnimFromWeaponID(int weapon_id)
  {
    return ResourceName.GetPlayerAnim(weapon_id / 1000);
  }

  public static void GetPlayerEvolveAnim(uint evolveId, out string ctrlName, out string animName)
  {
    ctrlName = $"EVOLVE_{evolveId}";
    animName = $"PLC_{ctrlName}_Anim";
  }

  public static bool isZakoEnemy(int id) => id >= 70000;

  private static string _GetEnemyBaseModelName(int id)
  {
    if (!ResourceName.isZakoEnemy(id))
      return $"ENM{id / 1000:D2}_{id % 1000:D3}";
    id -= id % 100 - id % 10;
    return $"ENM{id / 10000:D1}_{id % 10000:D4}";
  }

  private static string _GetEnemyNameDirect(int id)
  {
    return !ResourceName.isZakoEnemy(id) ? $"ENM{id / 1000:D2}_{id % 1000:D3}" : $"ENM{id / 10000:D1}_{id % 10000:D4}";
  }

  public static string GetEnemyBody(int id) => ResourceName._GetEnemyBaseModelName(id);

  public static string GetEnemyMaterial(int id)
  {
    return !ResourceName.isZakoEnemy(id) ? (string) null : $"ENM{id / 10000:D1}_{id % 10000:D4}";
  }

  public static string GetEnemyAnim(int id)
  {
    if (id < 100000)
      return $"{ResourceName._GetEnemyBaseModelName(id)}_Anim";
    id %= 100000;
    return $"QENM{id / 100:D3}_Anim";
  }

  public static string GetEnemyIcon(int id) => $"EIC_{ResourceName._GetEnemyNameDirect(id)}";

  public static string GetEnemyIconItem(int id) => $"EII_{ResourceName._GetEnemyNameDirect(id)}";

  public static string GetNPCModel(int id) => $"NPC{id / 1000:D3}_{id % 1000:D3}";

  public static string GetNPCAnim(int id) => $"NPC{id / 1000:D3}_Anim";

  public static string GetStageDetailTex(string stage_name, int tex_id)
  {
    return $"{stage_name}_{tex_id:D3}";
  }

  public static string GetQuestLocationImage(int id) => $"QLI_{id:D8}";

  public static string GetQuestMap(int id) => $"QMP_{id:D8}";

  public static string GetQuestIcon(string stage_name)
  {
    stage_name = stage_name.Substring(0, 6);
    string s = stage_name.Substring(2, 3);
    int num = 0;
    ref int local = ref num;
    if (int.TryParse(s, out local))
    {
      num *= 10;
      switch (stage_name.Substring(stage_name.Length - 1))
      {
        case "S":
          ++num;
          break;
        case "N":
          num += 2;
          break;
      }
    }
    return $"QIC_{num:D8}";
  }

  public static string GetQuestIcon(int questIconId) => $"QIC_{questIconId:D8}";

  public static string GetFoundationName(string name) => $"F{name.Substring(0, 5)}";

  public static string GetRegionIcon(int id) => $"RIC_{id:D8}";

  public static string GetEvolveIcon(uint id) => $"EVL_{id}";

  public static string GetItemIcon(int id)
  {
    if (id < 0)
      return string.Empty;
    return id < 100000000 ? $"IIC_{id:D8}" : $"EIC_{id % 100000000:D8}{id / 100000000 - 1}";
  }

  public static string GetAccessoryIcon(int id) => id < 0 ? "AIC_00000000" : $"AIC_{id:D8}";

  public static string GetItemModel(int id) => $"ITM_{id:D8}";

  public static string GetSkillItemModel(int id) => $"MAG_{id:D8}";

  public static string GetSkillItemSymbolModel(int id) => $"SIS_{id:D8}";

  public static string GetMagiGachaModelTexutre(int number) => $"HP004_gacha{number:D2}";

  public static string GetNPCIcon(int id, bool is_smile)
  {
    string format = "NIC_NPC{0:D3}_{1:D3}";
    if (is_smile)
      format += "_smile";
    return string.Format(format, (object) (id / 1000), (object) (id % 1000));
  }

  public static int GetSkillIconID(int type_id) => type_id + 80000000;

  public static int GetSkillSlotIconID(int type_id) => type_id + 89000000;

  public static string GetBackgroundImage(int id) => $"BGI_{id:D8}";

  public static string GetPlaceableObject(uint modelId) => $"PLOBJ_{modelId:D8}";

  public static string GetPlaceableMap(uint modelId) => $"PLMAP_{modelId:D8}";

  public static string GetStoryScript(int script_id) => $"scr_{script_id:D8}";

  public static string GetStoryLocationImage(int image_id) => $"SLI_{image_id:D8}";

  public static string GetStoryLocationSky(int sky_id) => $"SLS_{sky_id:D8}";

  public static string GetChatStamp(int stamp_id) => $"STMP_{stamp_id:D8}";

  public static string GetSymbolImageName(int stamp_id) => $"SYMBOL_{stamp_id:D8}";

  public static string GetSymbolFrameImageName(int stamp_id) => $"SYMBOL_FRAME_{stamp_id:D8}";

  public static string GetGatherPointModel(uint model_id) => $"GAP_{model_id:D8}";

  public static string GetGatherPointIcon(uint icon_id) => $"GIC_{icon_id:D8}";

  public static string GetFieldHealingPointModel() => "CMN_healpoint01";

  public static string GetFieldGimmickModel(
    FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE gimmickType,
    int index)
  {
    switch (gimmickType)
    {
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.HEALING:
        return "CMN_healpoint01";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON:
        return "CMN_cannon01";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BOMBROCK:
        return "CMN_bombrock01";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR:
        return "CMN_sensor01";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_HEAVY:
        return "CMN_cannon02";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_RAPID:
        return "CMN_cannon03";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_SPECIAL:
        return "CMN_cannon04";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET:
        return "CMN_wavetarget01";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET2:
        return "CMN_wavetarget02";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_FIELD:
        return "CMN_cannon05";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.READ_STORY:
        return "CMN_readstory01";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.FISHING:
        return "CMN_fishing01";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CHAT:
        return "CMN_chat01";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3:
        return "CMN_wavetarget03";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANDYWOOD:
        return "CMN_candy01";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_TURRET:
        return $"CMN_turret{index + 1:D2}";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_EVOLVE_ITEM:
        return $"CMN_itembag{index + 1:D2}";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_DECOY:
        return $"CMN_decoytrap{index + 1:D2}";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BUFF_POINT:
        return $"CMN_slowtrap{index + 1:D2}";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BOMB:
        return $"CMN_timebomb{index + 1:D2}";
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.QUEST:
        return ResourceName.GetGatherPointModel((uint) index);
      default:
        return string.Empty;
    }
  }

  public static string GetFieldGimmickCannonTargetEffect() => "ef_btl_target_cannon_01";

  public static string GetSonarTargetEffect() => "ef_btl_target_common_01";

  public static string GetReadStoryTargetEffectName() => "ef_btl_target_readstory_01";

  public static string GetEventBanner(int banner_id) => $"EBI_{banner_id:D8}";

  public static string GetEventBanner(int banner_id, string suffix)
  {
    return $"EBI_{banner_id:D8}" + suffix;
  }

  public static string GetEventBannerVer2(int banner_id) => $"EBI2_{banner_id:D8}";

  public static string GetEventBannerVer2(int banner_id, string suffix)
  {
    return $"EBI2_{banner_id:D8}" + suffix;
  }

  public static string GetAreaBanner(int regionId) => $"ABI_{regionId:D8}";

  public static string GetCloseAreaBanner(int regionId) => $"ABU_{regionId:D8}";

  public static string GetHomeBannerImage(int banner_id) => $"HBI_{banner_id:D8}";

  public static string GetFriendPromotionBannerImage(int banner_id) => $"IMG_{banner_id:D8}";

  public static string GetCountdownImage(int imageId) => $"CDN_{imageId:D8}";

  public static string GetLoginBonusTopImage(int loginbonus_id) => $"LIB_{loginbonus_id:D8}";

  public static string GetEventBG(int banner_id) => $"EBG_{banner_id:D8}";

  public static string GetAreaBG(int regionId) => $"ABG_{regionId:D8}";

  public static string GetQuestEventBannerResult(int event_id) => $"EBR_{event_id:D8}";

  public static string GetQuestEventBannerResultBG(int event_id) => $"EBB_{event_id:D8}";

  public static string GetGachaDecoImage(int image_id) => $"GDI_{image_id:D8}";

  public static string GetDungeonIcon(uint icon_id) => $"DIC_{icon_id:D8}";

  public static string GetCommmonImageName(int imageId) => $"CIC_{imageId:d8}";

  public static string GetShopImageName(int imageId) => $"SIC_{imageId:d8}";

  public static string GetShopImageOfferName(int imageId) => $"SIOC_{imageId:d8}";

  public static string GetShopImageGemOfferName(int imageId) => $"OFT_{imageId:d8}";

  public static string GetPointIconImageName(int imageId) => $"PIC_{imageId:d8}";

  public static string GetGrayPointIconImageName(int imageId) => $"PIG_{imageId:d8}";

  public static string GetBannerImageName(int imageId) => $"BNI_{imageId:d8}";

  public static string GetPointShopBannerImageName(int imageId) => $"PBI_{imageId:d8}";

  public static string GetPointSHopBGImageName(int imageId) => $"PBG_{imageId:d8}";

  public static string GetHomePointSHopBannerImageName(int imageId) => $"HPB_{imageId:d8}";

  public static string GetDegreeFrameName(int degreeId) => $"DF_{degreeId:d8}";

  public static string GetDegreeIcon(DEGREE_TYPE degreeType) => $"DIC_{degreeType.ToString()}";

  public static string GetRushQuestIconName(int iconId) => $"RQIC_{iconId:d8}";

  public static string GetRushResultIconName(int questId) => $"RRIC_{questId:d8}";

  public static string GetRushResultTitleName(int questId) => $"RRT_{questId:d8}";

  public static string GetArenaRankIconName(ARENA_RANK rank) => $"ARIC_{(int) rank:d9}";

  public static string GetSeriesArenaRankIconName(ARENA_RANK rank) => $"HERO_ARIC_{(int) rank:d9}";

  public static string GetSeriesArenaRankIconName(RARITY_TYPE rank)
  {
    return $"HERO_ARIC_{(int) (rank - 1):d9}";
  }

  public static string GetChapterImageName(int worldId)
  {
    return worldId == 0 ? "WorldMap_None" : "WorldMap_Chapter" + worldId.ToString();
  }

  public static string[] GetCannonAttackInfoNames()
  {
    return new string[7]
    {
      "cannonball_normal",
      "cannonball_fire",
      "cannonball_soil",
      "cannonball_thunder",
      "cannonball_water",
      "cannonball_light",
      "cannonball_dark"
    };
  }

  public static string[] GetNamesNeededLoadAtkInfoFromAnimEvent()
  {
    return new string[2]{ "sk", "evolve" };
  }

  public static string GetHomeBanner(int banner_id) => $"HBA_{banner_id:D8}";
}
