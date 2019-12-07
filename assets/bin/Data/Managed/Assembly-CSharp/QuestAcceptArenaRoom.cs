using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAcceptArenaRoom : OffLineQuestRoomBase
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
		OBJ_CHAT,
		LBL_ARENA_NAME,
		LBL_LIMIT_TIME,
		TBL_LIST,
		LBL_ENEMY_NAME,
		LBL_ENEMY_LEVEL,
		SPR_ELEMENT_ROOT,
		SPR_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		OBJ_ENEMY,
		TEX_ICON,
		BTN_START,
		BTN_NG,
		LBL_LIMIT,
		LBL_CONDITION,
		SPR_TYPE_DIFFICULTY,
		BTN_START_DISABLE
	}

	private ArenaTable.ArenaData arenaData;

	private DeliveryTable.DeliveryData deliveryData;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "ArenaTable";
		}
	}

	public override void Initialize()
	{
		base.Initialize();
		arenaData = Singleton<ArenaTable>.I.GetArenaData(MonoBehaviourSingleton<QuestManager>.I.currentArenaId);
		deliveryData = (GameSection.GetEventData() as DeliveryTable.DeliveryData);
		StartCoroutine(StartPredownload());
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		UpdateTopBar();
		UpdateEnemyList();
		UpdateLimitText();
		UpdateConditionText();
		UpdateStartButton();
		SetDifficultySprite();
	}

	private void UpdateTopBar()
	{
		int num = QuestUtility.ToSecByMilliSec(arenaData.timeLimit);
		SetLabelText(UI.LBL_LIMIT_TIME, $"{num / 60}:{num % 60:D2}");
		string text = "";
		if (deliveryData != null)
		{
			text = QuestUtility.GetArenaTitle(arenaData.group, deliveryData.name);
		}
		else
		{
			string str = StringTable.Format(STRING_CATEGORY.ARENA, 0u, arenaData.group);
			string str2 = StringTable.Format(STRING_CATEGORY.ARENA, 1u, arenaData.rank);
			text = str + "\u3000" + str2;
		}
		SetLabelText(UI.LBL_ARENA_NAME, text);
		ResourceLoad.LoadWithSetUITexture(GetCtrl(UI.TEX_ICON).GetComponent<UITexture>(), RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(arenaData.rank));
	}

	private void UpdateEnemyList()
	{
		if (arenaData != null)
		{
			List<QuestTable.QuestTableData> questDataArray = arenaData.GetQuestDataArray();
			SetTable(UI.TBL_LIST, "QuestArenaRoomEnemyListItem", questDataArray.Count, reset: false, delegate(int i, Transform t, bool b)
			{
				InitEnemyItem(i, t, b, questDataArray[i]);
			});
		}
	}

	private void InitEnemyItem(int i, Transform t, bool isRecycle, QuestTable.QuestTableData questData)
	{
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
		if (enemyData != null)
		{
			SetLabelText(t, UI.LBL_ENEMY_LEVEL, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1u, arenaData.level));
			SetLabelText(t, UI.LBL_ENEMY_NAME, enemyData.name);
			ItemIcon.Create(ItemIcon.GetItemIconType(questData.questType), enemyData.iconId, questData.rarity, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(is_enable: false);
			SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
			SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData.element);
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
		}
	}

	private void UpdateLimitText()
	{
		SetLabelText(UI.LBL_LIMIT, QuestUtility.GetLimitText(arenaData));
	}

	private void UpdateConditionText()
	{
		SetLabelText(UI.LBL_CONDITION, QuestUtility.GetConditionText(arenaData));
	}

	private void UpdateStartButton()
	{
		bool flag = QuestUtility.JudgeLimit(arenaData, userInfo.equipSet);
		SetActive(UI.BTN_START, flag);
		SetActive(UI.BTN_NG, !flag);
	}

	protected void OnQuery_START()
	{
		StartQuest();
	}

	private void StartQuest()
	{
		GameSection.StayEvent();
		CoopApp.EnterArenaQuestOffline(delegate(bool isMatching, bool isConnect, bool isRegist, bool isStart)
		{
			GameSection.ResumeEvent(isStart);
		});
	}

	private void SetDifficultySprite()
	{
		SetActive(UI.SPR_TYPE_DIFFICULTY, (deliveryData != null && deliveryData.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}

	private IEnumerator StartPredownload()
	{
		yield return null;
		List<ResourceInfo> list = new List<ResourceInfo>();
		List<QuestTable.QuestTableData> questDataArray = arenaData.GetQuestDataArray();
		if (questDataArray.IsNullOrEmpty())
		{
			yield break;
		}
		for (int i = 0; i < questDataArray.Count; i++)
		{
			uint mapId = questDataArray[i].mapId;
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
			if (fieldMapData == null)
			{
				yield break;
			}
			string text = fieldMapData.stageName;
			if (string.IsNullOrEmpty(text))
			{
				text = "ST011D_01";
			}
			StageTable.StageData data = Singleton<StageTable>.I.GetData(text);
			if (data == null)
			{
				yield break;
			}
			list.Add(new ResourceInfo(RESOURCE_CATEGORY.STAGE_SCENE, data.scene));
			list.Add(new ResourceInfo(RESOURCE_CATEGORY.STAGE_SKY, data.sky));
			if (!string.IsNullOrEmpty(data.cameraLinkEffect))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffect));
			}
			if (!string.IsNullOrEmpty(data.cameraLinkEffectY0))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffectY0));
			}
			if (!string.IsNullOrEmpty(data.rootEffect))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.rootEffect));
			}
			for (int j = 0; j < 8; j++)
			{
				if (!string.IsNullOrEmpty(data.useEffects[j]))
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.useEffects[j]));
				}
			}
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questDataArray[i].enemyID[0]);
			int modelId = enemyData.modelId;
			string enemyBody = ResourceName.GetEnemyBody(modelId);
			string enemyMaterial = ResourceName.GetEnemyMaterial(modelId);
			string enemyAnim = ResourceName.GetEnemyAnim(enemyData.animId);
			if (!string.IsNullOrEmpty(enemyBody))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody));
			}
			if (!string.IsNullOrEmpty(enemyMaterial))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody));
			}
			if (!string.IsNullOrEmpty(enemyAnim))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim));
			}
			if (!string.IsNullOrEmpty(enemyData.baseEffectName))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, enemyData.baseEffectName));
			}
		}
		if (list.Find((ResourceInfo x) => !MonoBehaviourSingleton<ResourceManager>.I.IsCached(x.category, x.packageName)) == null)
		{
			yield break;
		}
		List<string> assetNames = new List<string>();
		foreach (ResourceInfo item in list)
		{
			if (!string.IsNullOrEmpty(item.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(item.category, item.packageName))
			{
				assetNames.Add(item.category.ToAssetBundleName(item.packageName));
			}
		}
		SetActive(UI.BTN_START_DISABLE, is_visible: true);
		SetActive(UI.BTN_START, is_visible: false);
		yield return ResourceSizeInfo.Init();
		string act = null;
		yield return ResourceSizeInfo.OpenConfirmDialog(ResourceSizeInfo.GetAssetsSizeMB(assetNames.ToArray()), 3002u, CommonDialog.TYPE.YES_NO, delegate(string str)
		{
			act = str;
		});
		if (act == "NO")
		{
			while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
			{
				yield return null;
			}
			DispatchEvent("[BACK]");
		}
		else
		{
			LoadingQueue loadQueue = new LoadingQueue(this);
			foreach (ResourceInfo item2 in list)
			{
				if (!string.IsNullOrEmpty(item2.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(item2.category, item2.packageName))
				{
					ResourceManager.downloadOnly = true;
					loadQueue.Load(item2.category, item2.packageName, null);
					ResourceManager.downloadOnly = false;
					yield return loadQueue.Wait();
				}
			}
			SetActive(UI.BTN_START_DISABLE, is_visible: false);
			SetActive(UI.BTN_START, is_visible: true);
		}
	}
}
