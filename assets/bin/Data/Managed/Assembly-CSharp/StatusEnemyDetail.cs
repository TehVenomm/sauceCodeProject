using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Obsolete]
public class StatusEnemyDetail : GameSection
{
	public enum UI
	{
		TEX_ENEMY_MODEL,
		TEX_ENEMYICON,
		SPR_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_BREAK_REWARD_TITLE,
		STR_REWARD_TITLE,
		STR_TOTAL_DEFEAT_TITLE,
		STR_FIELD_DEFEAT_TITLE,
		STR_FLAVOR_TITLE,
		LBL_ENEMY_NAME,
		LBL_FIELD_DEFEAT,
		LBL_TOTAL_DEFEAT,
		LBL_BREAK_REWARD_LIST,
		LBL_REWARD_LIST,
		LBL_ELEMENT,
		LBL_UNKNOWN_WEAKPOINT,
		LBL_FLAVOR_TEXT,
		OBJ_FRAME,
		OBJ_COMMON_FRAME,
		OBJ_RARE_FRAME,
		OBJ_BIGRARE_FRAME,
		OBJ_FIELD_FRAME,
		OBJ_UNKNOWN,
		OBJ_FRONT,
		OBJ_FLAVOR,
		OBJ_APPEAR_MAP_ON,
		OBJ_APPEAR_MAP_OFF,
		TEX_BREAK_REWARD_LINE,
		TEX_REWARD_LINE,
		TEX_FLAVOR_LINE,
		TEX_TOTAL_BG,
		TEX_TOTAL_LINE,
		TEX_FIELD_BG,
		TEX_FIELD_LINE,
		TEX_BG,
		TEX_ATTRIBUTE_BG
	}

	private const string UI_BASE_SPRITE_FORMAT = "MonsterWindowBase_{0}";

	private const string UI_TOTAL_SPRITE_FORMAT = "ItemWindowPartsPlate02_{0}";

	private const string UI_FIELD_SPRITE_FORMAT = "ItemWindowPartsPlate01_{0}";

	private const string UI_LINE_FORMAT = "MonsterWindowLine_{0}";

	private const string UI_ATTRIBUTE_BT_FORMAT = "MonsterInfoWindow_attribute_{0}";

	private UI[] rarityTable = new UI[4]
	{
		UI.OBJ_COMMON_FRAME,
		UI.OBJ_RARE_FRAME,
		UI.OBJ_BIGRARE_FRAME,
		UI.OBJ_FIELD_FRAME
	};

	private readonly string[] RARITY_FUTTER = new string[4]
	{
		"G",
		"B",
		"Y",
		"R"
	};

	private readonly Dictionary<UI, string> UI_COLOR_CHANGE_TARGETS = new Dictionary<UI, string>
	{
		{
			UI.TEX_BREAK_REWARD_LINE,
			"MonsterWindowLine_{0}"
		},
		{
			UI.TEX_REWARD_LINE,
			"MonsterWindowLine_{0}"
		},
		{
			UI.TEX_FLAVOR_LINE,
			"MonsterWindowLine_{0}"
		},
		{
			UI.TEX_TOTAL_BG,
			"ItemWindowPartsPlate02_{0}"
		},
		{
			UI.TEX_TOTAL_LINE,
			"MonsterWindowLine_{0}"
		},
		{
			UI.TEX_FIELD_BG,
			"ItemWindowPartsPlate01_{0}"
		},
		{
			UI.TEX_FIELD_LINE,
			"MonsterWindowLine_{0}"
		},
		{
			UI.TEX_BG,
			"MonsterWindowBase_{0}"
		},
		{
			UI.TEX_ATTRIBUTE_BG,
			"MonsterInfoWindow_attribute_{0}"
		}
	};

	protected uint enemyCollectionId;

	protected List<EnemyCollectionTable.EnemyCollectionData> currentRegionCollectionData;

	protected EnemyTable.EnemyData enemyData;

	protected EnemyCollectionTable.EnemyCollectionData enemyCollectionData;

	protected RegionTable.Data regionData;

	protected List<AchievementCounter> achievementCounter;

	protected List<AchievementCounter> sameMonsterCounter;

	protected UIModelRenderTexture enemyModelRenderTexture;

	protected bool isUnknown;

	protected bool reInitialize;

	protected List<uint> popMapIds;

	protected UIEventListener eventListener;

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	public IEnumerator DoInitialize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		if (reInitialize)
		{
			yield return (object)0;
		}
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		MonoBehaviourSingleton<UIManager>.I.enableShadow = true;
		if (GameSection.GetEventData() is object[])
		{
			object[] obj = GameSection.GetEventData() as object[];
			if (obj.Length == 2)
			{
				enemyCollectionId = (uint)obj[0];
				currentRegionCollectionData = (obj[1] as List<EnemyCollectionTable.EnemyCollectionData>);
			}
		}
		if (currentRegionCollectionData == null)
		{
			Log.Warning("図鑑デ\u30fcタが正しくありません");
		}
		else
		{
			enemyData = Singleton<EnemyTable>.I.GetEnemyDataByEnemyCollectionId(enemyCollectionId).FirstOrDefault();
			enemyCollectionData = Singleton<EnemyCollectionTable>.I.GetEnemyCollectionData(enemyCollectionId);
			regionData = Singleton<RegionTable>.I.GetData(enemyCollectionData.regionId);
			achievementCounter = MonoBehaviourSingleton<AchievementManager>.I.monsterCollectionList;
			SetText(UI.STR_TOTAL_DEFEAT_TITLE, "TOTAL_DEFEAT");
			SetLabelText(text: string.Format(base.sectionData.GetText("AREA_DEFEAT"), regionData.regionName), label_enum: UI.STR_FIELD_DEFEAT_TITLE);
			SetText(UI.STR_FLAVOR_TITLE, "FLAVOR_TITLE");
			SetText(UI.STR_BREAK_REWARD_TITLE, "BREAK_REWARD_TITLE");
			SetText(UI.STR_REWARD_TITLE, "DEFEAT_REWARD_TITLE");
			OnQuery_TO_FRONT();
			string foundationName = string.Empty;
			popMapIds = new List<uint>();
			if (enemyCollectionData.collectionType == COLLECTION_TYPE.NORMAL)
			{
				foreach (EnemyTable.EnemyData item in Singleton<EnemyTable>.I.GetEnemyDataByEnemyCollectionId(enemyCollectionId))
				{
					uint mapId = Singleton<FieldMapTable>.I.GetTargetEnemyPopMapID(item.id);
					FieldMapTable.FieldMapTableData fieldmap = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
					if (fieldmap != null)
					{
						popMapIds.AddRange(Singleton<FieldMapTable>.I.GetTargetEnemyPopMapIDs(item.id));
						if (string.IsNullOrEmpty(foundationName))
						{
							foundationName = ResourceName.GetFoundationName(fieldmap.stageName);
						}
					}
				}
			}
			else
			{
				List<EnemyTable.EnemyData> targetEnemyAllData = Singleton<EnemyTable>.I.GetEnemyDataByEnemyCollectionId(enemyCollectionId);
				foreach (EnemyTable.EnemyData item2 in targetEnemyAllData)
				{
					IEnumerable<QuestTable.QuestTableData> questData = Singleton<QuestTable>.I.GetEnemyAppearQuestData(item2.id);
					if (questData != null)
					{
						foreach (QuestTable.QuestTableData item3 in questData)
						{
							FieldMapTable.FieldMapTableData[] fieldData = Singleton<QuestToFieldTable>.I.GetFieldMapTableFromQuestId(item3.questID, false);
							if (fieldData != null)
							{
								FieldMapTable.FieldMapTableData[] array = fieldData;
								foreach (FieldMapTable.FieldMapTableData field in array)
								{
									popMapIds.Add(field.mapID);
									if (string.IsNullOrEmpty(foundationName))
									{
										foundationName = questData.FirstOrDefault().GetFoundationName();
									}
								}
							}
						}
					}
				}
			}
			if (popMapIds != null)
			{
				popMapIds = (from x in popMapIds
				where MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)x)
				select x).ToList();
			}
			if (string.IsNullOrEmpty(foundationName))
			{
				foundationName = base.sectionData.GetText("DEFAULT_STGE_NAME");
			}
			sameMonsterCounter = (from x in achievementCounter
			where x.Count != 0
			where Singleton<EnemyCollectionTable>.I.GetEnemyCollectionData((uint)x.subType).enemySpeciesId == ((_003CDoInitialize_003Ec__Iterator168)/*Error near IL_0597: stateMachine*/)._003C_003Ef__this.enemyCollectionData.enemySpeciesId
			select x).ToList();
			SetLabelText(UI.LBL_ENEMY_NAME, enemyData.name);
			SetFrame(GetCtrl(UI.OBJ_FRAME), (int)enemyCollectionData.collectionType);
			isUnknown = !sameMonsterCounter.Any((AchievementCounter x) => x.subType == ((_003CDoInitialize_003Ec__Iterator168)/*Error near IL_0612: stateMachine*/)._003C_003Ef__this.enemyCollectionId);
			SetActive(UI.OBJ_UNKNOWN, isUnknown);
			SetActive(UI.LBL_UNKNOWN_WEAKPOINT, isUnknown || enemyData.weakElement == ELEMENT_TYPE.MAX);
			SetActive(UI.LBL_ELEMENT, isUnknown || enemyData.element == ELEMENT_TYPE.MAX);
			SetActive(UI.SPR_WEAK_ELEMENT, !isUnknown);
			SetActive(UI.SPR_ELEMENT, !isUnknown);
			InitRenderTexture(UI.TEX_ENEMY_MODEL, -1f, false);
			bool activeAppearButton = !isUnknown && popMapIds != null && popMapIds.Any();
			SetActive(UI.OBJ_APPEAR_MAP_ON, activeAppearButton);
			SetActive(UI.OBJ_APPEAR_MAP_OFF, !activeAppearButton);
			if (isUnknown)
			{
				SetRenderEnemyModel(UI.TEX_ENEMY_MODEL, enemyData.id, foundationName, OutGameSettingsManager.EnemyDisplayInfo.SCENE.GACHA, null, UIModelRenderTexture.ENEMY_MOVE_TYPE.STOP, true);
				enemyModelRenderTexture = GetComponent<UIModelRenderTexture>(UI.TEX_ENEMY_MODEL);
				UITexture rendererTexture2 = GetComponent<UITexture>(UI.TEX_ENEMY_MODEL);
				rendererTexture2.color = Color.black;
				SetActive(UI.TEX_ENEMYICON, false);
				SetText(UI.LBL_ENEMY_NAME, "???");
				SetText(UI.LBL_REWARD_LIST, "???");
				SetText(UI.LBL_BREAK_REWARD_LIST, "???");
				SetText(UI.LBL_FLAVOR_TEXT, "???");
				SetText(UI.LBL_UNKNOWN_WEAKPOINT, "?");
				SetText(UI.LBL_ELEMENT, "?");
				SetLabelText(UI.LBL_FIELD_DEFEAT, "0");
				SetLabelText(UI.LBL_TOTAL_DEFEAT, "0");
			}
			else
			{
				SetRenderEnemyModel(UI.TEX_ENEMY_MODEL, enemyData.id, foundationName, OutGameSettingsManager.EnemyDisplayInfo.SCENE.GACHA, null, UIModelRenderTexture.ENEMY_MOVE_TYPE.DONT_MOVE, true);
				enemyModelRenderTexture = GetComponent<UIModelRenderTexture>(UI.TEX_ENEMY_MODEL);
				UITexture rendererTexture = GetComponent<UITexture>(UI.TEX_ENEMY_MODEL);
				rendererTexture.color = Color.white;
				SetActive(UI.TEX_ENEMYICON, true);
				SetEnemyIcon(UI.TEX_ENEMYICON, enemyData.iconId);
				SetElementSprite(UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
				SetElementSprite(UI.SPR_ELEMENT, (int)enemyData.element);
				if (enemyData.weakElement == ELEMENT_TYPE.MAX)
				{
					SetText(UI.LBL_UNKNOWN_WEAKPOINT, "NONE_WEAK_POINT");
				}
				if (enemyData.element == ELEMENT_TYPE.MAX)
				{
					SetText(UI.LBL_ELEMENT, "NONE_WEAK_POINT");
				}
				SetLabelText(UI.LBL_ENEMY_NAME, enemyData.name);
				SetLabelText(UI.LBL_REWARD_LIST, GetDefeatRewardText());
				SetLabelText(UI.LBL_BREAK_REWARD_LIST, GetBreakRewardText());
				SetLabelText(UI.LBL_FLAVOR_TEXT, enemyCollectionData.flavorText);
				SetLabelText(UI.LBL_FIELD_DEFEAT, sameMonsterCounter.First((AchievementCounter x) => x.subType == ((_003CDoInitialize_003Ec__Iterator168)/*Error near IL_0a9a: stateMachine*/)._003C_003Ef__this.enemyCollectionData.id).count);
				SetLabelText(UI.LBL_TOTAL_DEFEAT, sameMonsterCounter.Sum((AchievementCounter x) => x.Count).ToString());
			}
			eventListener = GetComponent<UIEventListener>(UI.TEX_ENEMY_MODEL);
			if ((UnityEngine.Object)eventListener != (UnityEngine.Object)null)
			{
				UIEventListener uIEventListener = eventListener;
				uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(OnTap));
				UIEventListener uIEventListener2 = eventListener;
				uIEventListener2.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener2.onDrag, new UIEventListener.VectorDelegate(OnDrag));
			}
			reInitialize = false;
			base.Initialize();
		}
	}

	protected override void OnClose()
	{
		if ((UnityEngine.Object)eventListener != (UnityEngine.Object)null)
		{
			UIEventListener uIEventListener = eventListener;
			uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uIEventListener.onClick, new UIEventListener.VoidDelegate(OnTap));
			UIEventListener uIEventListener2 = eventListener;
			uIEventListener2.onDrag = (UIEventListener.VectorDelegate)Delegate.Remove(uIEventListener2.onDrag, new UIEventListener.VectorDelegate(OnDrag));
		}
		MonoBehaviourSingleton<UIManager>.I.enableShadow = false;
		base.OnClose();
	}

	private void SetFrame(Transform iconRoot, int rarity)
	{
		rarity = Mathf.Clamp(rarity, 0, rarityTable.Length - 1);
		for (int i = 0; i < rarityTable.Length; i++)
		{
			SetActive(iconRoot, rarityTable[i], rarity == i);
		}
		foreach (KeyValuePair<UI, string> uI_COLOR_CHANGE_TARGET in UI_COLOR_CHANGE_TARGETS)
		{
			SetSprite(uI_COLOR_CHANGE_TARGET.Key, string.Format(uI_COLOR_CHANGE_TARGET.Value, RARITY_FUTTER[rarity]));
		}
	}

	private void OnTap(GameObject obj)
	{
		if (!isUnknown && (UnityEngine.Object)enemyModelRenderTexture != (UnityEngine.Object)null)
		{
			enemyModelRenderTexture.SetApplyEnemyRootMotion(false);
			enemyModelRenderTexture.PlayRandomEnemyAnimation();
		}
	}

	private void OnDrag(GameObject obj, Vector2 delta)
	{
		if (!((UnityEngine.Object)enemyModelRenderTexture == (UnityEngine.Object)null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable())
		{
			foreach (Transform item in enemyModelRenderTexture.GetModelTransform().parent)
			{
				item.Rotate(GameDefine.GetCharaRotateVector(delta));
			}
		}
	}

	private void OnQuery_TO_FLAVOR()
	{
		if (!reInitialize)
		{
			SetActive(UI.OBJ_FLAVOR, true);
			SetActive(UI.OBJ_FRONT, false);
		}
	}

	private void OnQuery_TO_FRONT()
	{
		if (!reInitialize)
		{
			SetActive(UI.OBJ_FLAVOR, false);
			SetActive(UI.OBJ_FRONT, true);
		}
	}

	private void OnQuery_GOTO_APPEAR_FIELD()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
		{
			new EventData("OPEN_REGION_CHANGE", (int)regionData.regionId),
			new EventData("VIEW_POP_ENEMY_MAP", popMapIds)
		});
	}

	private void OnQuery_RIGHT()
	{
		if (!reInitialize)
		{
			enemyModelRenderTexture.Clear();
			enemyModelRenderTexture = null;
			int num = currentRegionCollectionData.IndexOf(enemyCollectionData);
			if (++num >= currentRegionCollectionData.Count)
			{
				num = 0;
			}
			GameSection.SetEventData(new object[2]
			{
				currentRegionCollectionData[num].id,
				currentRegionCollectionData
			});
			reInitialize = true;
			Initialize();
		}
	}

	private void OnQuery_LEFT()
	{
		if (!reInitialize)
		{
			enemyModelRenderTexture.Clear();
			enemyModelRenderTexture = null;
			int num = currentRegionCollectionData.IndexOf(enemyCollectionData);
			if (--num < 0)
			{
				num = currentRegionCollectionData.Count - 1;
			}
			GameSection.SetEventData(new object[2]
			{
				currentRegionCollectionData[num].id,
				currentRegionCollectionData
			});
			reInitialize = true;
			Initialize();
		}
	}

	private string GetDefeatRewardText()
	{
		string text = string.Empty;
		List<uint> list = new List<uint>();
		foreach (EnemyFieldDropItemTable.EnemyFieldDropItemData enemyDatum in Singleton<EnemyFieldDropItemTable>.I.GetEnemyData(enemyData.id))
		{
			if (enemyDatum.partIds.Any((int x) => x == 0) && !list.Contains(enemyDatum.itemId))
			{
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(enemyDatum.itemId);
				if (!string.IsNullOrEmpty(text))
				{
					text += Environment.NewLine;
				}
				text += itemData.name;
				list.Add(enemyDatum.itemId);
			}
		}
		if (string.IsNullOrEmpty(text))
		{
			text = base.sectionData.GetText("NO_REWARD");
		}
		return text;
	}

	private string GetBreakRewardText()
	{
		string text = string.Empty;
		List<uint> list = new List<uint>();
		foreach (EnemyFieldDropItemTable.EnemyFieldDropItemData enemyDatum in Singleton<EnemyFieldDropItemTable>.I.GetEnemyData(enemyData.id))
		{
			if (enemyDatum.partIds.Any((int x) => x > 0) && !list.Contains(enemyDatum.itemId))
			{
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(enemyDatum.itemId);
				if (!string.IsNullOrEmpty(text))
				{
					text += Environment.NewLine;
				}
				text += itemData.name;
				list.Add(enemyDatum.itemId);
			}
		}
		if (string.IsNullOrEmpty(text))
		{
			text = base.sectionData.GetText("NO_REWARD");
		}
		return text;
	}
}
