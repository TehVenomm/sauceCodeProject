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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	public unsafe IEnumerator DoInitialize()
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
				List<uint> source = popMapIds;
				if (_003CDoInitialize_003Ec__Iterator175._003C_003Ef__am_0024cache18 == null)
				{
					_003CDoInitialize_003Ec__Iterator175._003C_003Ef__am_0024cache18 = new Func<uint, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				popMapIds = source.Where(_003CDoInitialize_003Ec__Iterator175._003C_003Ef__am_0024cache18).ToList();
			}
			if (string.IsNullOrEmpty(foundationName))
			{
				foundationName = base.sectionData.GetText("DEFAULT_STGE_NAME");
			}
			List<AchievementCounter> source2 = achievementCounter;
			if (_003CDoInitialize_003Ec__Iterator175._003C_003Ef__am_0024cache19 == null)
			{
				_003CDoInitialize_003Ec__Iterator175._003C_003Ef__am_0024cache19 = new Func<AchievementCounter, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			sameMonsterCounter = source2.Where(_003CDoInitialize_003Ec__Iterator175._003C_003Ef__am_0024cache19).Where(new Func<AchievementCounter, bool>((object)/*Error near IL_0597: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToList();
			SetLabelText((Enum)UI.LBL_ENEMY_NAME, enemyData.name);
			SetFrame(GetCtrl(UI.OBJ_FRAME), (int)enemyCollectionData.collectionType);
			isUnknown = !sameMonsterCounter.Any(new Func<AchievementCounter, bool>((object)/*Error near IL_0612: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			SetActive((Enum)UI.OBJ_UNKNOWN, isUnknown);
			SetActive((Enum)UI.LBL_UNKNOWN_WEAKPOINT, isUnknown || enemyData.weakElement == ELEMENT_TYPE.MAX);
			SetActive((Enum)UI.LBL_ELEMENT, isUnknown || enemyData.element == ELEMENT_TYPE.MAX);
			SetActive((Enum)UI.SPR_WEAK_ELEMENT, !isUnknown);
			SetActive((Enum)UI.SPR_ELEMENT, !isUnknown);
			InitRenderTexture(UI.TEX_ENEMY_MODEL, -1f, false);
			bool activeAppearButton = !isUnknown && popMapIds != null && popMapIds.Any();
			SetActive((Enum)UI.OBJ_APPEAR_MAP_ON, activeAppearButton);
			SetActive((Enum)UI.OBJ_APPEAR_MAP_OFF, !activeAppearButton);
			if (isUnknown)
			{
				SetRenderEnemyModel((Enum)UI.TEX_ENEMY_MODEL, enemyData.id, foundationName, OutGameSettingsManager.EnemyDisplayInfo.SCENE.GACHA, null, UIModelRenderTexture.ENEMY_MOVE_TYPE.STOP, true);
				enemyModelRenderTexture = base.GetComponent<UIModelRenderTexture>((Enum)UI.TEX_ENEMY_MODEL);
				UITexture rendererTexture2 = base.GetComponent<UITexture>((Enum)UI.TEX_ENEMY_MODEL);
				rendererTexture2.color = Color.get_black();
				SetActive((Enum)UI.TEX_ENEMYICON, false);
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
				SetRenderEnemyModel((Enum)UI.TEX_ENEMY_MODEL, enemyData.id, foundationName, OutGameSettingsManager.EnemyDisplayInfo.SCENE.GACHA, null, UIModelRenderTexture.ENEMY_MOVE_TYPE.DONT_MOVE, true);
				enemyModelRenderTexture = base.GetComponent<UIModelRenderTexture>((Enum)UI.TEX_ENEMY_MODEL);
				UITexture rendererTexture = base.GetComponent<UITexture>((Enum)UI.TEX_ENEMY_MODEL);
				rendererTexture.color = Color.get_white();
				SetActive((Enum)UI.TEX_ENEMYICON, true);
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
				SetLabelText((Enum)UI.LBL_FIELD_DEFEAT, sameMonsterCounter.First(new Func<AchievementCounter, bool>((object)/*Error near IL_0a9a: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).count);
				object label_enum = UI.LBL_TOTAL_DEFEAT;
				List<AchievementCounter> source3 = sameMonsterCounter;
				if (_003CDoInitialize_003Ec__Iterator175._003C_003Ef__am_0024cache1A == null)
				{
					_003CDoInitialize_003Ec__Iterator175._003C_003Ef__am_0024cache1A = new Func<AchievementCounter, long>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				SetLabelText((Enum)label_enum, source3.Sum(_003CDoInitialize_003Ec__Iterator175._003C_003Ef__am_0024cache1A).ToString());
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
			enemyModelRenderTexture.SetApplyEnemyRootMotion(false);
			enemyModelRenderTexture.PlayRandomEnemyAnimation();
		}
	}

	private void OnDrag(GameObject obj, Vector2 delta)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (!(enemyModelRenderTexture == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable())
		{
			foreach (Transform item in enemyModelRenderTexture.GetModelTransform().get_parent())
			{
				Transform val = item;
				val.Rotate(GameDefine.GetCharaRotateVector(delta));
			}
		}
	}

	private void OnQuery_TO_FLAVOR()
	{
		if (!reInitialize)
		{
			SetActive((Enum)UI.OBJ_FLAVOR, true);
			SetActive((Enum)UI.OBJ_FRONT, false);
		}
	}

	private void OnQuery_TO_FRONT()
	{
		if (!reInitialize)
		{
			SetActive((Enum)UI.OBJ_FLAVOR, false);
			SetActive((Enum)UI.OBJ_FRONT, true);
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

	private unsafe string GetDefeatRewardText()
	{
		string text = string.Empty;
		List<uint> list = new List<uint>();
		foreach (EnemyFieldDropItemTable.EnemyFieldDropItemData enemyDatum in Singleton<EnemyFieldDropItemTable>.I.GetEnemyData(enemyData.id))
		{
			List<int> partIds = enemyDatum.partIds;
			if (_003C_003Ef__am_0024cacheF == null)
			{
				_003C_003Ef__am_0024cacheF = new Func<int, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			if (partIds.Any(_003C_003Ef__am_0024cacheF) && !list.Contains(enemyDatum.itemId))
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

	private unsafe string GetBreakRewardText()
	{
		string text = string.Empty;
		List<uint> list = new List<uint>();
		foreach (EnemyFieldDropItemTable.EnemyFieldDropItemData enemyDatum in Singleton<EnemyFieldDropItemTable>.I.GetEnemyData(enemyData.id))
		{
			List<int> partIds = enemyDatum.partIds;
			if (_003C_003Ef__am_0024cache10 == null)
			{
				_003C_003Ef__am_0024cache10 = new Func<int, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			if (partIds.Any(_003C_003Ef__am_0024cache10) && !list.Contains(enemyDatum.itemId))
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
