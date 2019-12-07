using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultFriend : GameSection
{
	private enum UI
	{
		OBJ_ITEMS,
		OBJ_ITEM_POS_L_0,
		OBJ_ITEM_POS_L_1,
		OBJ_ITEM_POS_L_2,
		OBJ_ITEM_POS_L_3,
		OBJ_ITEM_POS_R_0,
		OBJ_ITEM_POS_R_1,
		OBJ_ITEM_POS_R_2,
		OBJ_ITEM_POS_R_3,
		LBL_NAME_OWN,
		LBL_NAME,
		LBL_LEVEL,
		BTN_DETAIL,
		BTN_FOLLOW,
		SPR_NUM_0,
		SPR_NUM_1,
		SPR_NUM_2,
		SPR_PER,
		TEX_MODEL,
		OBJ_DEGREE_PLATE,
		SPR_TITLE,
		SPR_RUSH_TITLE
	}

	public PlayerLoader[] playersModels;

	private List<InGameRecorder.PlayerRecord> playerRecords;

	private Transform[] itemsL = new Transform[4];

	private Transform[] itemsR = new Transform[4];

	private Vector3 cameraTarget;

	private List<int> score_list = new List<int>();

	private List<float> score_truncation_list = new List<float>();

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(active: true);
		yield return new WaitForEndOfFrame();
		yield return MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(need_gc_collect: true);
		yield return new WaitForEndOfFrame();
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			playerRecords = MonoBehaviourSingleton<InGameRecorder>.I.players;
			int num = 0;
			while (num < playerRecords.Count)
			{
				InGameRecorder.PlayerRecord playerRecord = playerRecords[num];
				if (playerRecord == null || playerRecord.playerLoadInfo == null)
				{
					playerRecords.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
			bool waitLoad = true;
			MonoBehaviourSingleton<InGameRecorder>.I.CreatePlayerModelsAsync(delegate(PlayerLoader[] loaders)
			{
				playersModels = loaders;
				waitLoad = false;
			});
			while (waitLoad)
			{
				yield return null;
			}
			Transform mainCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
			if (MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
			{
				int i = 0;
				for (int num2 = playersModels.Length; i < num2; i++)
				{
					PlayerLoader playerLoader = playersModels[i];
					if (playerLoader != null)
					{
						playerLoader.animator.applyRootMotion = false;
						playerLoader.animator.Play(playerLoader.GetWinLoopMotionState());
					}
				}
				mainCameraTransform.position += mainCameraTransform.forward * 1.5f;
			}
			else if (playersModels.Length != 0)
			{
				OutGameSettingsManager.QuestResult questResult = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult;
				SoundManager.RequestBGM(10, isLoop: false);
				PlayerLoader playerLoader2 = playersModels[0];
				if (playerLoader2 != null)
				{
					Transform transform = playerLoader2.transform;
					cameraTarget = transform.position + new Vector3(0f, questResult.loseCameraHeight, 0f);
					Vector3 vector = cameraTarget + transform.forward * questResult.loseCameraDistance;
					Quaternion rotation = Quaternion.LookRotation(cameraTarget - vector);
					mainCameraTransform.position = vector;
					mainCameraTransform.rotation = rotation;
					PLCA default_anim = (UnityEngine.Random.Range(0, 2) == 0) ? PLCA.IDLE_01 : PLCA.IDLE_02;
					PlayerAnimCtrl.Get(playerLoader2.animator, default_anim);
				}
				MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView = questResult.cameraFieldOfView;
			}
		}
		itemsL[0] = GetCtrl(UI.OBJ_ITEM_POS_L_0);
		itemsL[1] = GetCtrl(UI.OBJ_ITEM_POS_L_1);
		itemsL[2] = GetCtrl(UI.OBJ_ITEM_POS_L_2);
		itemsL[3] = GetCtrl(UI.OBJ_ITEM_POS_L_3);
		itemsR[0] = GetCtrl(UI.OBJ_ITEM_POS_R_0);
		itemsR[1] = GetCtrl(UI.OBJ_ITEM_POS_R_1);
		itemsR[2] = GetCtrl(UI.OBJ_ITEM_POS_R_2);
		itemsR[3] = GetCtrl(UI.OBJ_ITEM_POS_R_3);
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			SetActive(UI.SPR_TITLE, !MonoBehaviourSingleton<InGameManager>.I.IsRush());
			SetActive(UI.SPR_RUSH_TITLE, MonoBehaviourSingleton<InGameManager>.I.IsRush());
		}
		if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			MonoBehaviourSingleton<SceneSettingsManager>.I.DisableWaveTarget();
		}
		GC.Collect();
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(active: false);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			return;
		}
		float num = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHpContainsHealed();
		int num2 = 0;
		float num3 = 0f;
		score_list.Clear();
		score_truncation_list.Clear();
		for (int i = 0; i < 4; i++)
		{
			int num4 = 0;
			float num5 = 0f;
			if (playerRecords != null && i < playerRecords.Count && playerRecords[i] != null)
			{
				float num6 = (float)playerRecords[i].givenTotalDamage / num * 100f;
				num4 = (int)num6;
				num5 = num6 - (float)num4;
				if (num2 + num4 > 100)
				{
					num4 = 100 - num2;
				}
				num2 += num4;
			}
			score_list.Add(num4);
			score_truncation_list.Add(num5);
			num3 += num5;
		}
		int num7 = (int)(num3 + 0.1f);
		if (num7 > 0 && num2 < 100)
		{
			for (int j = 0; j < 4; j++)
			{
				int num8 = Mathf.CeilToInt(score_truncation_list[j]);
				score_list[j] += num8;
				num7 -= num8;
				num2 += num8;
				if (num7 <= 0 || num2 >= 100)
				{
					break;
				}
			}
		}
		InGameRecorder.CheckAndRepairIsSelf(ref playerRecords);
		for (int k = 0; k < 4; k++)
		{
			if (playerRecords != null && k < playerRecords.Count && playerRecords[k] != null)
			{
				InGameRecorder.PlayerRecord playerRecord = playerRecords[k];
				if (playerRecord == null || playerRecord.charaInfo == null)
				{
					continue;
				}
				bool flag = false;
				if (playerRecord.id != 0 && playerRecord.charaInfo.userId != 0)
				{
					QuestResultUserCollection.ResultUserInfo userInfo = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserInfo(playerRecord.charaInfo.userId);
					if (userInfo != null && !userInfo.CanSendFollow)
					{
						flag = true;
					}
					if (userInfo != null)
					{
						playerRecord.charaInfo.selectedDegrees = userInfo.selectDegrees;
					}
				}
				Transform root = SetPrefab(itemsL[k], "QuestResultFriendItemL");
				if (playerRecord.isSelf)
				{
					playerRecord.charaInfo.selectedDegrees = MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds;
				}
				CharaInfo.ClanInfo clanInfo = playerRecord.charaInfo.clanInfo;
				if (clanInfo == null)
				{
					clanInfo = new CharaInfo.ClanInfo();
					clanInfo.clanId = -1;
					clanInfo.tag = string.Empty;
				}
				bool isSameTeam = clanInfo.clanId > -1 && MonoBehaviourSingleton<GuildManager>.I.guildData != null && clanInfo.clanId == MonoBehaviourSingleton<GuildManager>.I.guildData.clanId;
				if (playerRecord.isSelf)
				{
					SetSupportEncoding(root, UI.LBL_NAME_OWN, isEnable: true);
					SetLabelText(root, UI.LBL_NAME_OWN, Utility.GetNameWithColoredClanTag(clanInfo.tag, playerRecord.charaInfo.name, playerRecord.id == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
					SetActive(root, UI.LBL_NAME, is_visible: false);
				}
				else
				{
					SetSupportEncoding(root, UI.LBL_NAME, isEnable: true);
					SetLabelText(root, UI.LBL_NAME, Utility.GetNameWithColoredClanTag(clanInfo.tag, playerRecord.charaInfo.name, playerRecord.id == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
					SetActive(root, UI.LBL_NAME_OWN, is_visible: false);
				}
				if (!playerRecord.isNPC)
				{
					int num9 = playerRecord.charaInfo.level;
					if (playerRecord.isSelf)
					{
						num9 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
					}
					SetLabelText(root, UI.LBL_LEVEL, string.Format(base.sectionData.GetText("LEVEL"), num9));
				}
				else
				{
					SetActive(root, UI.LBL_LEVEL, is_visible: false);
					if (FindCtrl(root, UI.BTN_DETAIL) != null && GetComponent<UINoAuto>(root, UI.BTN_DETAIL) == null)
					{
						FindCtrl(root, UI.BTN_DETAIL).gameObject.AddComponent<UINoAuto>();
					}
				}
				SetEvent(root, UI.BTN_DETAIL, "DETAIL", k);
				if (playerRecord.isSelf)
				{
					SetButtonSprite(root, UI.BTN_DETAIL, "ResultPlatemine", with_press: true);
				}
				PlayerLoadInfo playerLoadInfo = playerRecord.playerLoadInfo.Clone();
				playerLoadInfo.armModelID = -1;
				playerLoadInfo.weaponModelID = -1;
				playerLoadInfo.legModelID = -1;
				SetRenderPlayerModel(root, UI.TEX_MODEL, playerLoadInfo, 98, new Vector3(0f, -1.613f, 2.342f), new Vector3(0f, 154f, 0f), is_priority_visual_equip: true);
				if (playerRecord.charaInfo.selectedDegrees != null && playerRecord.charaInfo.selectedDegrees.Count == GameDefine.DEGREE_PART_COUNT)
				{
					FindCtrl(root, UI.OBJ_DEGREE_PLATE).GetComponent<DegreePlate>().Initialize(playerRecord.charaInfo.selectedDegrees, isButton: false, delegate
					{
					});
				}
				root = SetPrefab(itemsR[k], "QuestResultFriendItemR");
				int num10 = score_list[k] % 10;
				int num11 = score_list[k] / 10 % 10;
				int num12 = score_list[k] / 100;
				SetSprite(root, UI.SPR_NUM_0, num10.ToString("D2"));
				if (num12 != 0 || num11 != 0)
				{
					SetSprite(root, UI.SPR_NUM_1, num11.ToString("D2"));
				}
				else
				{
					SetActive(root, UI.SPR_NUM_1, is_visible: false);
				}
				if (num12 != 0)
				{
					SetSprite(root, UI.SPR_NUM_2, num12.ToString("D2"));
				}
				else
				{
					SetActive(root, UI.SPR_NUM_2, is_visible: false);
				}
				if (!playerRecord.isSelf && !playerRecord.isNPC)
				{
					SetEvent(root, UI.BTN_FOLLOW, "FOLLOW", k);
					if (!flag)
					{
						SetButtonSprite(root, UI.BTN_FOLLOW, "ResultfollowBtn", with_press: true);
						SetButtonEnabled(root, UI.BTN_FOLLOW, is_enabled: true);
					}
					else
					{
						SetButtonSprite(root, UI.BTN_FOLLOW, "ResultfollowBtnOff", with_press: true);
						SetButtonEnabled(root, UI.BTN_FOLLOW, is_enabled: false);
					}
				}
				else
				{
					SetActive(root, UI.BTN_FOLLOW, is_visible: false);
				}
			}
			else
			{
				Transform root2 = SetPrefab(itemsL[k], "QuestResultFriendItemL");
				SetActive(root2, UI.LBL_NAME, is_visible: false);
				SetActive(root2, UI.LBL_NAME_OWN, is_visible: false);
				SetActive(root2, UI.LBL_LEVEL, is_visible: false);
				SetButtonSprite(root2, UI.BTN_DETAIL, "ResultPlateGrey", with_press: true);
				SetButtonEnabled(root2, UI.BTN_DETAIL, is_enabled: false);
				SetEvent(root2, UI.BTN_DETAIL, "DETAIL", k);
				SetActive(root2, UI.TEX_MODEL, is_visible: false);
				root2 = SetPrefab(itemsR[k], "QuestResultFriendItemR");
				SetActive(root2, UI.SPR_NUM_0, is_visible: false);
				SetActive(root2, UI.SPR_NUM_1, is_visible: false);
				SetActive(root2, UI.SPR_NUM_2, is_visible: false);
				SetActive(root2, UI.SPR_PER, is_visible: false);
				SetActive(root2, UI.BTN_FOLLOW, is_visible: false);
			}
		}
		PlayTween(UI.OBJ_ITEMS);
	}

	protected override void OnClose()
	{
		UILabel.OutlineLimit = false;
		try
		{
			if (MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameManager>.I.ClearRush();
			}
			base.OnClose();
		}
		catch (Exception ex)
		{
			Log.Warning(LOG.UI, "QuestRequest OnClose\n{0}\n{1}", ex.Message, ex.StackTrace);
		}
	}

	protected override void OnDestroy()
	{
		if (!AppMain.isApplicationQuit)
		{
			base.OnDestroy();
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				MonoBehaviourSingleton<InGameRecorder>.I.DeletePlayerModels();
			}
		}
	}

	private void OnQuery_DETAIL()
	{
		int index = (int)GameSection.GetEventData();
		if (playerRecords[index].isNPC)
		{
			GameSection.StopEvent();
			return;
		}
		InGameRecorder.PlayerRecord playerRecord = playerRecords[index];
		if (MonoBehaviourSingleton<StatusManager>.I.HasEventEquipSet())
		{
			playerRecord.playerLoadInfo = PlayerLoadInfo.FromCharaInfo(playerRecord.charaInfo, need_weapon: true, need_helm: true, need_leg: true, is_priority_visual_equip: true);
		}
		GameSection.ChangeEvent("DETAIL", playerRecord);
	}

	private void OnQuery_FOLLOW()
	{
		int playerIndex = (int)GameSection.GetEventData();
		InGameRecorder.PlayerRecord record = playerRecords[playerIndex];
		if (record == null)
		{
			GameSection.StopEvent();
			return;
		}
		if (MonoBehaviourSingleton<FriendManager>.I.followNum >= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow)
		{
			GameSection.ChangeEvent("FOLLOW_MAX");
			return;
		}
		GameSection.StayEvent();
		List<int> list = new List<int>();
		list.Add(record.charaInfo.userId);
		MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(list, delegate(Error err, List<int> follow_list)
		{
			if (err == Error.None)
			{
				GameSection.ChangeStayEvent("FOLLOW_DIALOG", new object[1]
				{
					record.charaInfo.name
				});
				Transform root = itemsL[playerIndex];
				SetButtonSprite(root, UI.BTN_FOLLOW, "ResultfollowBtnOff", with_press: true);
				SetButtonEnabled(root, UI.BTN_FOLLOW, is_enabled: false);
				if (MonoBehaviourSingleton<CoopApp>.IsValid())
				{
					CoopApp.UpdateField();
				}
			}
			else if (follow_list.Count == 0)
			{
				GameSection.ChangeStayEvent("FAILED_FOLLOW", new object[1]
				{
					record.charaInfo.name
				});
			}
			GameSection.ResumeEvent(err == Error.None);
		});
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_FRIEND_PARAM;
	}

	private void OnQuery_NEXT()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ChatManager>.I.SwitchRoomChatConnectionToCoopConnection();
		Action<bool> action = delegate
		{
			if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsQuestInField())
			{
				MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToField = true;
				GameSection.ChangeStayEvent("QUEST_TO_FIELD");
			}
			GameSection.ResumeEvent(is_resume: true);
		};
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isQuestResultFieldLeave)
		{
			bool toHome = !MonoBehaviourSingleton<InGameManager>.I.IsQuestInField() && !MonoBehaviourSingleton<InGameManager>.I.IsQuestInPortal();
			MonoBehaviourSingleton<CoopApp>.I.LeaveWithParty(action, toHome);
		}
		else
		{
			action(obj: true);
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
	}

	private void Update()
	{
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && !MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
		{
			float loseCameraRotateSpeed = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.loseCameraRotateSpeed;
			if (loseCameraRotateSpeed != 0f)
			{
				MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.RotateAround(cameraTarget, Vector3.up, loseCameraRotateSpeed * Time.deltaTime);
			}
		}
	}
}
