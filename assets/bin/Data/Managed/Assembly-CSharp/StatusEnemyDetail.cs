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

	private const string UI_BASE_SPRITE_FORMAT = "MonsterWindowBase_{0}";

	private const string UI_TOTAL_SPRITE_FORMAT = "ItemWindowPartsPlate02_{0}";

	private const string UI_FIELD_SPRITE_FORMAT = "ItemWindowPartsPlate01_{0}";

	private const string UI_LINE_FORMAT = "MonsterWindowLine_{0}";

	private const string UI_ATTRIBUTE_BT_FORMAT = "MonsterInfoWindow_attribute_{0}";

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
		this.StartCoroutine(DoInitialize());
	}

	public IEnumerator DoInitialize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		if (reInitialize)
		{
			yield return 0;
		}
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		MonoBehaviourSingleton<UIManager>.I.enableShadow = true;
		if (GameSection.GetEventData() is object[])
		{
			object[] array = GameSection.GetEventData() as object[];
			if (array.Length == 2)
			{
				enemyCollectionId = (uint)array[0];
				currentRegionCollectionData = (array[1] as List<EnemyCollectionTable.EnemyCollectionData>);
			}
		}
		if (currentRegionCollectionData == null)
		{
			Log.Warning("図鑑デ\u30fcタが正しくありません");
			yield break;
		}
		enemyData = Singleton<EnemyTable>.I.GetEnemyDataByEnemyCollectionId(enemyCollectionId).FirstOrDefault();
		enemyCollectionData = Singleton<EnemyCollectionTable>.I.GetEnemyCollectionData(enemyCollectionId);
		regionData = Singleton<RegionTable>.I.GetData(enemyCollectionData.regionId);
		achievementCounter = MonoBehaviourSingleton<AchievementManager>.I.monsterCollectionList;
		SetText((Enum)UI.STR_TOTAL_DEFEAT_TITLE, "TOTAL_DEFEAT");
		SetLabelText(text: string.Format(base.sectionData.GetText("AREA_DEFEAT"), regionData.regionName), label_enum: UI.STR_FIELD_DEFEAT_TITLE);
		SetText((Enum)UI.STR_FLAVOR_TITLE, "FLAVOR_TITLE");
		SetText((Enum)UI.STR_BREAK_REWARD_TITLE, "BREAK_REWARD_TITLE");
		SetText((Enum)UI.STR_REWARD_TITLE, "DEFEAT_REWARD_TITLE");
		OnQuery_TO_FRONT();
		string foundationName = string.Empty;
		popMapIds = new List<uint>();
		if (enemyCollectionData.collectionType == COLLECTION_TYPE.NORMAL)
		{
			foreach (EnemyTable.EnemyData item in Singleton<EnemyTable>.I.GetEnemyDataByEnemyCollectionId(enemyCollectionId))
			{
				uint targetEnemyPopMapID = Singleton<FieldMapTable>.I.GetTargetEnemyPopMapID(item.id);
				FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(targetEnemyPopMapID);
				if (fieldMapData != null)
				{
					popMapIds.AddRange(Singleton<FieldMapTable>.I.GetTargetEnemyPopMapIDs(item.id));
					if (string.IsNullOrEmpty(foundationName))
					{
						foundationName = ResourceName.GetFoundationName(fieldMapData.stageName);
					}
				}
			}
		}
		else
		{
			List<EnemyTable.EnemyData> enemyDataByEnemyCollectionId = Singleton<EnemyTable>.I.GetEnemyDataByEnemyCollectionId(enemyCollectionId);
			foreach (EnemyTable.EnemyData item2 in enemyDataByEnemyCollectionId)
			{
				IEnumerable<QuestTable.QuestTableData> enemyAppearQuestData = Singleton<QuestTable>.I.GetEnemyAppearQuestData(item2.id);
				if (enemyAppearQuestData != null)
				{
					foreach (QuestTable.QuestTableData item3 in enemyAppearQuestData)
					{
						FieldMapTable.FieldMapTableData[] fieldMapTableFromQuestId = Singleton<QuestToFieldTable>.I.GetFieldMapTableFromQuestId(item3.questID);
						if (fieldMapTableFromQuestId != null)
						{
							FieldMapTable.FieldMapTableData[] array2 = fieldMapTableFromQuestId;
							foreach (FieldMapTable.FieldMapTableData fieldMapTableData in array2)
							{
								popMapIds.Add(fieldMapTableData.mapID);
								if (string.IsNullOrEmpty(foundationName))
								{
									foundationName = enemyAppearQuestData.FirstOrDefault().GetFoundationName();
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
		where Singleton<EnemyCollectionTable>.I.GetEnemyCollectionData((uint)x.subType).enemySpeciesId == enemyCollectionData.enemySpeciesId
		select x).ToList();
		SetLabelText((Enum)UI.LBL_ENEMY_NAME, enemyData.name);
		SetFrame(GetCtrl(UI.OBJ_FRAME), (int)enemyCollectionData.collectionType);
		isUnknown = !sameMonsterCounter.Any((AchievementCounter x) => x.subType == enemyCollectionId);
		SetActive((Enum)UI.OBJ_UNKNOWN, isUnknown);
		SetActive((Enum)UI.LBL_UNKNOWN_WEAKPOINT, isUnknown || enemyData.weakElement == ELEMENT_TYPE.MAX);
		SetActive((Enum)UI.LBL_ELEMENT, isUnknown || enemyData.element == ELEMENT_TYPE.MAX);
		SetActive((Enum)UI.SPR_WEAK_ELEMENT, !isUnknown);
		SetActive((Enum)UI.SPR_ELEMENT, !isUnknown);
		InitRenderTexture(UI.TEX_ENEMY_MODEL);
		bool activeAppearButton = !isUnknown && popMapIds != null && popMapIds.Any();
		SetActive((Enum)UI.OBJ_APPEAR_MAP_ON, activeAppearButton);
		SetActive((Enum)UI.OBJ_APPEAR_MAP_OFF, !activeAppearButton);
		if (isUnknown)
		{
			SetRenderEnemyModel((Enum)UI.TEX_ENEMY_MODEL, enemyData.id, foundationName, OutGameSettingsManager.EnemyDisplayInfo.SCENE.GACHA, (Action<bool, EnemyLoader>)null, UIModelRenderTexture.ENEMY_MOVE_TYPE.STOP, is_Howl: true);
			enemyModelRenderTexture = base.GetComponent<UIModelRenderTexture>((Enum)UI.TEX_ENEMY_MODEL);
			UITexture component = base.GetComponent<UITexture>((Enum)UI.TEX_ENEMY_MODEL);
			component.color = Color.get_black();
			SetActive((Enum)UI.TEX_ENEMYICON, is_visible: false);
			SetText((Enum)UI.LBL_ENEMY_NAME, "???");
			SetText((Enum)UI.LBL_REWARD_LIST, "???");
			SetText((Enum)UI.LBL_BREAK_REWARD_LIST, "???");
			SetText((Enum)UI.LBL_FLAVOR_TEXT, "???");
			SetText((Enum)UI.LBL_UNKNOWN_WEAKPOINT, "?");
			SetText((Enum)UI.LBL_ELEMENT, "?");
			SetLabelText((Enum)UI.LBL_FIELD_DEFEAT, "0");
			SetLabelText((Enum)UI.LBL_TOTAL_DEFEAT, "0");
		}
		else
		{
			SetRenderEnemyModel((Enum)UI.TEX_ENEMY_MODEL, enemyData.id, foundationName, OutGameSettingsManager.EnemyDisplayInfo.SCENE.GACHA, (Action<bool, EnemyLoader>)null, UIModelRenderTexture.ENEMY_MOVE_TYPE.DONT_MOVE, is_Howl: true);
			enemyModelRenderTexture = base.GetComponent<UIModelRenderTexture>((Enum)UI.TEX_ENEMY_MODEL);
			UITexture component2 = base.GetComponent<UITexture>((Enum)UI.TEX_ENEMY_MODEL);
			component2.color = Color.get_white();
			SetActive((Enum)UI.TEX_ENEMYICON, is_visible: true);
			SetEnemyIcon((Enum)UI.TEX_ENEMYICON, enemyData.iconId);
			SetElementSprite((Enum)UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
			SetElementSprite((Enum)UI.SPR_ELEMENT, (int)enemyData.element);
			if (enemyData.weakElement == ELEMENT_TYPE.MAX)
			{
				SetText((Enum)UI.LBL_UNKNOWN_WEAKPOINT, "NONE_WEAK_POINT");
			}
			if (enemyData.element == ELEMENT_TYPE.MAX)
			{
				SetText((Enum)UI.LBL_ELEMENT, "NONE_WEAK_POINT");
			}
			SetLabelText((Enum)UI.LBL_ENEMY_NAME, enemyData.name);
			SetLabelText((Enum)UI.LBL_REWARD_LIST, GetDefeatRewardText());
			SetLabelText((Enum)UI.LBL_BREAK_REWARD_LIST, GetBreakRewardText());
			SetLabelText((Enum)UI.LBL_FLAVOR_TEXT, enemyCollectionData.flavorText);
			SetLabelText((Enum)UI.LBL_FIELD_DEFEAT, sameMonsterCounter.First((AchievementCounter x) => x.subType == enemyCollectionData.id).count);
			SetLabelText((Enum)UI.LBL_TOTAL_DEFEAT, sameMonsterCounter.Sum((AchievementCounter x) => x.Count).ToString());
		}
		eventListener = base.GetComponent<UIEventListener>((Enum)UI.TEX_ENEMY_MODEL);
		if (eventListener != null)
		{
			UIEventListener uIEventListener = eventListener;
			uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(OnTap));
			UIEventListener uIEventListener2 = eventListener;
			uIEventListener2.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener2.onDrag, new UIEventListener.VectorDelegate(OnDrag));
		}
		reInitialize = false;
		base.Initialize();
	}

	protected override void OnClose()
	{
		if (eventListener != null)
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
			SetSprite((Enum)uI_COLOR_CHANGE_TARGET.Key, string.Format(uI_COLOR_CHANGE_TARGET.Value, RARITY_FUTTER[rarity]));
		}
	}

	private void OnTap(GameObject obj)
	{
		if (!isUnknown && enemyModelRenderTexture != null)
		{
			enemyModelRenderTexture.SetApplyEnemyRootMotion(enable: false);
			enemyModelRenderTexture.PlayRandomEnemyAnimation();
		}
	}

	private void OnDrag(GameObject obj, Vector2 delta)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (!(enemyModelRenderTexture == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable())
		{
			IEnumerator enumerator = enemyModelRenderTexture.GetModelTransform().get_parent().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform val = enumerator.Current;
					val.Rotate(GameDefine.GetCharaRotateVector(delta));
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	private void OnQuery_TO_FLAVOR()
	{
		if (!reInitialize)
		{
			SetActive((Enum)UI.OBJ_FLAVOR, is_visible: true);
			SetActive((Enum)UI.OBJ_FRONT, is_visible: false);
		}
	}

	private void OnQuery_TO_FRONT()
	{
		if (!reInitialize)
		{
			SetActive((Enum)UI.OBJ_FLAVOR, is_visible: false);
			SetActive((Enum)UI.OBJ_FRONT, is_visible: true);
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
