using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffLineQuestRoomBase : GameSection
{
	private enum UI
	{
		GRD_PLAYER_INFO,
		LBL_NAME,
		LBL_LV,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		SPR_USER_READY,
		SPR_USER_READY_WAIT,
		SPR_USER_EMPTY,
		SPR_USER_BATTLE,
		BTN_EMO_0,
		BTN_EMO_1,
		BTN_EMO_2,
		SPR_WEAPON_1,
		SPR_WEAPON_2,
		SPR_WEAPON_3,
		BTN_NAME_BG,
		BTN_FRAME,
		OBJ_CHAT
	}

	protected class ResourceInfo
	{
		public RESOURCE_CATEGORY category;

		public string packageName;

		public ResourceInfo(RESOURCE_CATEGORY category, string packageName)
		{
			this.category = category;
			this.packageName = packageName;
		}
	}

	private const int ROOM_MEMBER_MAX = 4;

	private QuestTable.QuestTableData questData;

	private Coroutine preDownloadCoroutine;

	private bool goToInGame;

	private UI[] weaponIcon = new UI[3]
	{
		UI.SPR_WEAPON_1,
		UI.SPR_WEAPON_2,
		UI.SPR_WEAPON_3
	};

	protected CharaInfo userInfo;

	private static readonly string[] ITEM_TYPE_ICON_SPRITE_NAME = new string[5]
	{
		"Sword",
		"Brade",
		"Lance",
		"Edge",
		"Arrow"
	};

	protected int equipSetNo;

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateUser();
	}

	protected void UpdateUser()
	{
		SetGrid(UI.GRD_PLAYER_INFO, string.Empty, 1, false, delegate(int i, Transform t)
		{
			string prefab_name = "QuestRoomUserInfoSelf";
			return Realizes(prefab_name, t, false);
		}, delegate(int i, Transform t, bool is_recycle)
		{
			UpdateRoomUserInfo(t, i);
			SetEvent(t, UI.BTN_NAME_BG, "CHANGE_EQUIP", i);
			SetEvent(t, UI.BTN_FRAME, "CHANGE_EQUIP", i);
		});
	}

	protected void UpdateRoomUserInfo(Transform trans, int index)
	{
		SetActive(trans, UI.SPR_USER_EMPTY, false);
		SetActive(trans, UI.SPR_USER_BATTLE, false);
		SetActive(trans, UI.SPR_USER_READY, false);
		SetActive(trans, UI.SPR_USER_READY_WAIT, false);
		SetActive(trans, UI.OBJ_CHAT, false);
		QuestRoomUserInfo component = trans.GetComponent<QuestRoomUserInfo>();
		if (!(component == null))
		{
			userInfo = MonoBehaviourSingleton<StatusManager>.I.GetCreatePlayerInfo().charaInfo;
			if (userInfo == null)
			{
				component.LoadModel(index, null);
			}
			else
			{
				equipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
				SetActive(trans, UI.SPR_WEAPON_1, false);
				SetActive(trans, UI.SPR_WEAPON_2, false);
				SetActive(trans, UI.SPR_WEAPON_3, false);
				int weapon_index = 0;
				userInfo.equipSet.ForEach(delegate(CharaInfo.EquipItem data)
				{
					if (data != null)
					{
						EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)data.eId);
						if (equipItemData != null && equipItemData.IsWeapon())
						{
							SetActive(trans, weaponIcon[weapon_index], true);
							int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(equipItemData.type);
							SetSprite(trans, weaponIcon[weapon_index], ITEM_TYPE_ICON_SPRITE_NAME[equipmentTypeIndex]);
							weapon_index++;
						}
					}
				});
				component.LoadModel(index, userInfo);
				EquipSetCalculator equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetCalculator(equipSetNo);
				SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, userInfo.hp, userInfo.atk, userInfo.def);
				SetLabelText(trans, UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
				SetLabelText(trans, UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
				SetLabelText(trans, UI.LBL_HP, finalStatus.hp.ToString());
				SetLabelText(trans, UI.LBL_NAME, userInfo.name);
				SetLabelText(trans, UI.LBL_LV, userInfo.level.ToString());
			}
		}
	}

	private void ActiveAndTween(Transform root, Enum _enum, bool is_active)
	{
		SetActive(root, _enum, is_active);
		if (is_active)
		{
			ResetTween(root, _enum, 0);
			PlayTween(root, _enum, true, null, false, 0);
		}
	}

	protected IEnumerator StartPredownload()
	{
		List<ResourceInfo> list = new List<ResourceInfo>();
		uint mapId = questData.mapId;
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
		if (fieldMapData != null)
		{
			string stageName = fieldMapData.stageName;
			if (string.IsNullOrEmpty(stageName))
			{
				stageName = "ST011D_01";
			}
			StageTable.StageData stageData = Singleton<StageTable>.I.GetData(stageName);
			if (stageData != null)
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.STAGE_SCENE, stageData.scene));
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.STAGE_SKY, stageData.sky));
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.cameraLinkEffect));
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.cameraLinkEffectY0));
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.rootEffect));
				for (int i = 0; i < 8; i++)
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.useEffects[i]));
				}
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.enemyID[0]);
				int bodyId = enemyData.modelId;
				string bodyName = ResourceName.GetEnemyBody(bodyId);
				string mateName = ResourceName.GetEnemyMaterial(bodyId);
				string animName = ResourceName.GetEnemyAnim(enemyData.animId);
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MODEL, bodyName));
				if (!string.IsNullOrEmpty(mateName))
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MATERIAL, bodyName));
				}
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_ANIM, animName));
				LoadingQueue load_queue = new LoadingQueue(this);
				foreach (ResourceInfo item in list)
				{
					if (!string.IsNullOrEmpty(item.packageName))
					{
						ResourceManager.downloadOnly = true;
						load_queue.Load(item.category, item.packageName, null, false);
						ResourceManager.downloadOnly = false;
						yield return (object)load_queue.Wait();
					}
				}
			}
		}
	}
}
