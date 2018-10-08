public static class ResourceName
{
	public const string BOMB_ROCK_ATTACK_INFO_NAME = "bombrock";

	public const string CANNONBALL_HEAVY_ATTACK_INFO_NAME = "cannonball_heavy";

	public const string CANNONBALL_RAPID_ATTACK_INFO_NAME = "cannonball_rapid";

	public const string HEAL_ATTACK_INFO_NAME = "sk_heal_atk";

	public const string HEAL_ATTACK_ZONE_INFO_NAME = "sk_heal_atk_zone";

	public const string CHARACTER_FREEZE_EFFECT_NAME = "ef_btl_pl_frozen_01";

	public const string ENEMY_SHADOW_SEALING_EFFECT_NAME = "ef_btl_wsk_bow_01_04";

	public const string ENEMY_ELECTRIC_SHOCK_EFFECT_NAME = "ef_btl_enm_shock_01";

	public const string ENEMY_BURNING_EFFECT_NAME = "ef_btl_enm_fire_01";

	public const string ENEMY_SPEED_DOWN_EFFECT_NAME = "ef_btl_pl_movedown_01";

	public const string ENEMY_BOSS_ENTRY_EXIT_EFFECT = "ef_btl_enemy_entry_01";

	public static readonly string CANNONBALL_SPECIAL_ATTACK_INFO_NAME = "cannonball_special";

	public static string ToHash256String(this RESOURCE_CATEGORY category, byte hash)
	{
		return $"{category.ToString()}_{hash:X2}";
	}

	public static string ToHash256String(this RESOURCE_CATEGORY category, string name)
	{
		return $"{category.ToString()}_{(byte)Utility.GetHash(name):X2}";
	}

	public static string ToAssetBundleName(this RESOURCE_CATEGORY category, string package_name = null)
	{
		string text = category.ToString();
		if (ResourceDefine.types[(int)category] != ResourceManager.CATEGORY_TYPE.PACK)
		{
			text = text + "/" + package_name;
		}
		if (category == RESOURCE_CATEGORY.UI_ATLAS)
		{
			if (!text.EndsWith("_bundle"))
			{
				text += "_bundle";
			}
			text = text + ResourceDefine.suffix[(int)category] + GoGameResourceManager.GetDefaultAssetBundleExtension();
		}
		else
		{
			text = text + ResourceDefine.suffix[(int)category] + GoGameResourceManager.GetDefaultAssetBundleExtension();
		}
		return text.ToLower();
	}

	public static string Normalize(string name)
	{
		if (!name.StartsWith("internal__"))
		{
			return name;
		}
		return name.Substring(name.LastIndexOf("__") + 2);
	}

	public static string GetSE(int se_id)
	{
		if (MonoBehaviourSingleton<ResourceManager>.IsValid() && MonoBehaviourSingleton<ResourceManager>.I.cache != null)
		{
			return MonoBehaviourSingleton<ResourceManager>.I.cache.GetSEName(se_id);
		}
		return CreateSEName(se_id);
	}

	public static string CreateSEName(int se_id)
	{
		return $"SE_{se_id:D8}";
	}

	public static string GetSEPackage(int se_id)
	{
		return RESOURCE_CATEGORY.SOUND_SE.ToHash256String(GetSE(se_id));
	}

	public static string GetBGM(int bgm_id)
	{
		return $"BGM_{bgm_id:D8}";
	}

	public static string GetActionVoicePackageName(int sex, int voice_type_id)
	{
		return $"ACV_{voice_type_id * 10 + sex:D4}";
	}

	public static string GetActionVoicePackageNameFromVoiceID(int voice_id)
	{
		return $"ACV_{voice_id / 10000:D4}";
	}

	public static string GetActionVoiceName(int voice_id)
	{
		return $"ACV_{voice_id:D8}";
	}

	public static string GetActionVoiceName(int sex, int voice_type_id, int voice_id)
	{
		return $"ACV_{(voice_type_id * 10 + sex) * 10000 + voice_id:D8}";
	}

	public static string GetStoryVoicePackageNameFromVoiceID(int voice_id)
	{
		return GetNPCVoicePackageNameFromVoiceID(voice_id);
	}

	public static string GetStoryVoiceName(int voice_id)
	{
		return GetNPCVoiceName(voice_id);
	}

	public static string GetNPCVoicePackageName(int npc_id)
	{
		return $"NPV_{npc_id:D3}";
	}

	public static string GetNPCVoicePackageNameFromVoiceID(int voice_id)
	{
		int num = voice_id / 100000;
		return $"NPV_{num:D3}";
	}

	public static string GetNPCVoiceName(int npc_id, int type, int no)
	{
		int npc_voice_id = npc_id * 100000 + type * 100 + no;
		return GetNPCVoiceName(npc_voice_id);
	}

	public static string GetNPCVoiceName(int npc_id, int no)
	{
		int npc_voice_id = npc_id * 100000 + no;
		return GetNPCVoiceName(npc_voice_id);
	}

	public static string GetNPCVoiceName(int npc_voice_id)
	{
		return $"NPV_{npc_voice_id:D8}";
	}

	public static string AddAttributID(string name)
	{
		if (name[name.Length - 1] == '_')
		{
			int num = (!MonoBehaviourSingleton<SceneSettingsManager>.IsValid()) ? 1 : MonoBehaviourSingleton<SceneSettingsManager>.I.attributeID;
			name = $"{name}{num:00}";
		}
		return name;
	}

	public static string GetTipsImage(int id)
	{
		return $"TPS_{id:D8}";
	}

	public static string GetRushTipsImage(int id)
	{
		return $"TPS_RUSH_{id:D8}";
	}

	public static string GetSkillGachaBannerImage(int id)
	{
		return $"SGI_{id:D9}";
	}

	public static string GetPlayerBody(int id)
	{
		return $"BDY{id / 1000:D2}_{id % 1000:D3}";
	}

	public static string GetPlayerArm(int id)
	{
		return $"ARM{id / 1000:D2}_{id % 1000:D3}";
	}

	public static string GetPlayerLeg(int id)
	{
		return $"LEG{id / 1000:D2}_{id % 1000:D3}";
	}

	public static string GetPlayerFace(int id)
	{
		return $"PLF{id / 1000:D2}_{id % 1000:D3}";
	}

	public static string GetPlayerHead(int id)
	{
		return $"HED{id / 1000:D2}_{id % 1000:D3}";
	}

	public static string GetPlayerWeapon(int id)
	{
		if (id <= 0)
		{
			return string.Empty;
		}
		return $"WEP{id / 1000:D2}_{id % 1000:D3}";
	}

	public static string GetPlayerAnim(int anim_id)
	{
		return $"PLC{anim_id:D2}_Anim";
	}

	public static string GetPlayerSubAnim(int anim_id, string ctrl_name)
	{
		return string.Format("PLC_{1}_Anim", anim_id, ctrl_name);
	}

	public static string GetPlayerAnimFromWeaponID(int weapon_id)
	{
		return GetPlayerAnim(weapon_id / 1000);
	}

	public static void GetPlayerEvolveAnim(uint evolveId, out string ctrlName, out string animName)
	{
		ctrlName = $"EVOLVE_{evolveId}";
		animName = $"PLC_{ctrlName}_Anim";
	}

	public static bool isZakoEnemy(int id)
	{
		return id >= 70000;
	}

	private static string _GetEnemyBaseModelName(int id)
	{
		if (!isZakoEnemy(id))
		{
			return $"ENM{id / 1000:D2}_{id % 1000:D3}";
		}
		id -= id % 100 - id % 10;
		return $"ENM{id / 10000:D1}_{id % 10000:D4}";
	}

	private static string _GetEnemyNameDirect(int id)
	{
		if (!isZakoEnemy(id))
		{
			return $"ENM{id / 1000:D2}_{id % 1000:D3}";
		}
		return $"ENM{id / 10000:D1}_{id % 10000:D4}";
	}

	public static string GetEnemyBody(int id)
	{
		return _GetEnemyBaseModelName(id);
	}

	public static string GetEnemyMaterial(int id)
	{
		if (!isZakoEnemy(id))
		{
			return null;
		}
		return $"ENM{id / 10000:D1}_{id % 10000:D4}";
	}

	public static string GetEnemyAnim(int id)
	{
		if (id < 100000)
		{
			return $"{_GetEnemyBaseModelName(id)}_Anim";
		}
		id %= 100000;
		return $"QENM{id / 100:D3}_Anim";
	}

	public static string GetEnemyIcon(int id)
	{
		return $"EIC_{_GetEnemyNameDirect(id)}";
	}

	public static string GetEnemyIconItem(int id)
	{
		return $"EII_{_GetEnemyNameDirect(id)}";
	}

	public static string GetNPCModel(int id)
	{
		return $"NPC{id / 1000:D3}_{id % 1000:D3}";
	}

	public static string GetNPCAnim(int id)
	{
		return $"NPC{id / 1000:D3}_Anim";
	}

	public static string GetStageDetailTex(string stage_name, int tex_id)
	{
		return $"{stage_name}_{tex_id:D3}";
	}

	public static string GetQuestLocationImage(int id)
	{
		return $"QLI_{id:D8}";
	}

	public static string GetQuestMap(int id)
	{
		return $"QMP_{id:D8}";
	}

	public static string GetQuestIcon(string stage_name)
	{
		stage_name = stage_name.Substring(0, 6);
		string s = stage_name.Substring(2, 3);
		int result = 0;
		if (int.TryParse(s, out result))
		{
			result *= 10;
			string text = stage_name.Substring(stage_name.Length - 1);
			switch (text)
			{
			case "S":
				result++;
				break;
			case "N":
				result += 2;
				break;
			}
		}
		return $"QIC_{result:D8}";
	}

	public static string GetQuestIcon(int questIconId)
	{
		return $"QIC_{questIconId:D8}";
	}

	public static string GetFoundationName(string name)
	{
		return $"F{name.Substring(0, 5)}";
	}

	public static string GetRegionIcon(int id)
	{
		return $"RIC_{id:D8}";
	}

	public static string GetEvolveIcon(uint id)
	{
		return $"EVL_{id}";
	}

	public static string GetItemIcon(int id)
	{
		if (id < 0)
		{
			return string.Empty;
		}
		if (id < 100000000)
		{
			return $"IIC_{id:D8}";
		}
		return $"EIC_{id % 100000000:D8}{id / 100000000 - 1}";
	}

	public static string GetItemModel(int id)
	{
		return $"ITM_{id:D8}";
	}

	public static string GetSkillItemModel(int id)
	{
		return $"MAG_{id:D8}";
	}

	public static string GetSkillItemSymbolModel(int id)
	{
		return $"SIS_{id:D8}";
	}

	public static string GetMagiGachaModelTexutre(int number)
	{
		return $"HP004_gacha{number:D2}";
	}

	public static string GetNPCIcon(int id, bool is_smile)
	{
		string text = "NIC_NPC{0:D3}_{1:D3}";
		if (is_smile)
		{
			text += "_smile";
		}
		return string.Format(text, id / 1000, id % 1000);
	}

	public static int GetSkillIconID(int type_id)
	{
		return type_id + 80000000;
	}

	public static int GetSkillSlotIconID(int type_id)
	{
		return type_id + 89000000;
	}

	public static string GetBackgroundImage(int id)
	{
		return $"BGI_{id:D8}";
	}

	public static string GetPlaceableObject(uint modelId)
	{
		return $"PLOBJ_{modelId:D8}";
	}

	public static string GetPlaceableMap(uint modelId)
	{
		return $"PLMAP_{modelId:D8}";
	}

	public static string GetStoryScript(int script_id)
	{
		return $"scr_{script_id:D8}";
	}

	public static string GetStoryLocationImage(int image_id)
	{
		return $"SLI_{image_id:D8}";
	}

	public static string GetStoryLocationSky(int sky_id)
	{
		return $"SLS_{sky_id:D8}";
	}

	public static string GetChatStamp(int stamp_id)
	{
		return $"STMP_{stamp_id:D8}";
	}

	public static string GetGatherPointModel(uint model_id)
	{
		return $"GAP_{model_id:D8}";
	}

	public static string GetGatherPointIcon(uint icon_id)
	{
		return $"GIC_{icon_id:D8}";
	}

	public static string GetFieldHealingPointModel()
	{
		return "CMN_healpoint01";
	}

	public static string GetFieldGimmickModel(FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE gimmickType)
	{
		switch (gimmickType)
		{
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.HEALING:
			return "CMN_healpoint01";
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON:
			return "CMN_cannon01";
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_HEAVY:
			return "CMN_cannon02";
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_RAPID:
			return "CMN_cannon03";
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_SPECIAL:
			return "CMN_cannon04";
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BOMBROCK:
			return "CMN_bombrock01";
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR:
			return "CMN_sensor01";
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET:
			return "CMN_wavetarget01";
		case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET2:
			return "CMN_wavetarget02";
		default:
			return string.Empty;
		}
	}

	public static string GetFieldGimmickCannonTargetEffect()
	{
		return "ef_btl_target_cannon_01";
	}

	public static string GetSonarTargetEffect()
	{
		return "ef_btl_target_common_01";
	}

	public static string GetEventBanner(int banner_id)
	{
		return $"EBI_{banner_id:D8}";
	}

	public static string GetAreaBanner(int regionId)
	{
		return $"ABI_{regionId:D8}";
	}

	public static string GetHomeBannerImage(int banner_id)
	{
		return $"HBI_{banner_id:D8}";
	}

	public static string GetFriendPromotionBannerImage(int banner_id)
	{
		return $"IMG_{banner_id:D8}";
	}

	public static string GetCountdownImage(int imageId)
	{
		return $"CDN_{imageId:D8}";
	}

	public static string GetLoginBonusTopImage(int loginbonus_id)
	{
		return $"LIB_{loginbonus_id:D8}";
	}

	public static string GetEventBG(int banner_id)
	{
		return $"EBG_{banner_id:D8}";
	}

	public static string GetAreaBG(int regionId)
	{
		return $"ABG_{regionId:D8}";
	}

	public static string GetQuestEventBannerResult(int event_id)
	{
		return $"EBR_{event_id:D8}";
	}

	public static string GetQuestEventBannerResultBG(int event_id)
	{
		return $"EBB_{event_id:D8}";
	}

	public static string GetGachaDecoImage(int image_id)
	{
		return $"GDI_{image_id:D8}";
	}

	public static string GetDungeonIcon(uint icon_id)
	{
		return $"DIC_{icon_id:D8}";
	}

	public static string GetCommmonImageName(int imageId)
	{
		return $"CIC_{imageId:d8}";
	}

	public static string GetShopImageName(int imageId)
	{
		return $"SIC_{imageId:d8}";
	}

	public static string GetShopImageOfferName(int imageId)
	{
		return $"SIOC_{imageId:d8}";
	}

	public static string GetShopImageGemOfferName(int imageId)
	{
		return $"OFT_{imageId:d8}";
	}

	public static string GetPointIconImageName(int imageId)
	{
		return $"PIC_{imageId:d8}";
	}

	public static string GetGrayPointIconImageName(int imageId)
	{
		return $"PIG_{imageId:d8}";
	}

	public static string GetPointShopBannerImageName(int imageId)
	{
		return $"PBI_{imageId:d8}";
	}

	public static string GetPointSHopBGImageName(int imageId)
	{
		return $"PBG_{imageId:d8}";
	}

	public static string GetHomePointSHopBannerImageName(int imageId)
	{
		return $"HPB_{imageId:d8}";
	}

	public static string GetDegreeFrameName(int degreeId)
	{
		return $"DF_{degreeId:d8}";
	}

	public static string GetDegreeIcon(DEGREE_TYPE degreeType)
	{
		return $"DIC_{degreeType.ToString()}";
	}

	public static string GetRushQuestIconName(int iconId)
	{
		return $"RQIC_{iconId:d8}";
	}

	public static string GetRushResultIconName(int questId)
	{
		return $"RRIC_{questId:d8}";
	}

	public static string GetRushResultTitleName(int questId)
	{
		return $"RRT_{questId:d8}";
	}

	public static string GetArenaRankIconName(ARENA_RANK rank)
	{
		return $"ARIC_{(int)rank:d9}";
	}

	public static string GetChapterImageName(int worldId)
	{
		if (worldId == 0)
		{
			return "WorldMap_None";
		}
		return "WorldMap_Chapter" + worldId.ToString();
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
		return new string[2]
		{
			"sk",
			"evolve"
		};
	}
}
