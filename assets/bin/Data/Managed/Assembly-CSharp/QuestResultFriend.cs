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

	private Transform[] itemsL = (Transform[])new Transform[4];

	private Transform[] itemsR = (Transform[])new Transform[4];

	private Vector3 cameraTarget;

	private List<int> score_list = new List<int>();

	private List<float> score_truncation_list = new List<float>();

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
		yield return (object)new WaitForEndOfFrame();
		yield return (object)MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(true);
		yield return (object)new WaitForEndOfFrame();
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			playerRecords = MonoBehaviourSingleton<InGameRecorder>.I.players;
			int i = 0;
			while (i < playerRecords.Count)
			{
				InGameRecorder.PlayerRecord p = playerRecords[i];
				if (p == null || p.playerLoadInfo == null)
				{
					playerRecords.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
			bool waitLoad = true;
			MonoBehaviourSingleton<InGameRecorder>.I.CreatePlayerModelsAsync(delegate(PlayerLoader[] loaders)
			{
				((_003CDoInitialize_003Ec__Iterator143)/*Error near IL_013a: stateMachine*/)._003C_003Ef__this.playersModels = loaders;
				((_003CDoInitialize_003Ec__Iterator143)/*Error near IL_013a: stateMachine*/)._003CwaitLoad_003E__2 = false;
			});
			while (waitLoad)
			{
				yield return (object)null;
			}
			Transform camera_t = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
			if (MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
			{
				int k = 0;
				for (int j = playersModels.Length; k < j; k++)
				{
					PlayerLoader player = playersModels[k];
					if (player != null)
					{
						player.animator.set_applyRootMotion(false);
						player.animator.Play("win_loop");
					}
				}
				camera_t.set_position(camera_t.get_position() + camera_t.get_forward() * 1.5f);
			}
			else if (playersModels.Length > 0)
			{
				OutGameSettingsManager.QuestResult param = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult;
				SoundManager.RequestBGM(10, false);
				PlayerLoader player_loader = playersModels[0];
				if (player_loader != null)
				{
					Transform player_t = player_loader.get_transform();
					cameraTarget = player_t.get_position() + new Vector3(0f, param.loseCameraHeight, 0f);
					Vector3 camera_pos = cameraTarget + player_t.get_forward() * param.loseCameraDistance;
					Quaternion camera_rot = Quaternion.LookRotation(cameraTarget - camera_pos);
					camera_t.set_position(camera_pos);
					camera_t.set_rotation(camera_rot);
					PlayerAnimCtrl.Get(default_anim: (Random.Range(0, 2) != 0) ? PLCA.IDLE_02 : PLCA.IDLE_01, _animator: player_loader.animator, on_play: null, on_change: null, on_end: null);
				}
				MonoBehaviourSingleton<AppMain>.I.mainCamera.set_fieldOfView(param.cameraFieldOfView);
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
			SetActive((Enum)UI.SPR_TITLE, !MonoBehaviourSingleton<InGameManager>.I.IsRush());
			SetActive((Enum)UI.SPR_RUSH_TITLE, MonoBehaviourSingleton<InGameManager>.I.IsRush());
		}
		if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			MonoBehaviourSingleton<SceneSettingsManager>.I.DisableWaveTarget();
		}
		GC.Collect();
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			float num = (float)MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
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
					InGameRecorder.PlayerRecord playerRecord = playerRecords[i];
					float num6 = (float)playerRecord.givenTotalDamage / num * 100f;
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
					List<int> list;
					List<int> list2 = list = score_list;
					int index;
					int index2 = index = j;
					index = list[index];
					list2[index2] = index + num8;
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
					InGameRecorder.PlayerRecord playerRecord2 = playerRecords[k];
					if (playerRecord2 != null && playerRecord2.charaInfo != null)
					{
						bool flag = false;
						if (playerRecord2.id != 0 && playerRecord2.charaInfo.userId != 0)
						{
							QuestResultUserCollection.ResultUserInfo userInfo = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserInfo(playerRecord2.charaInfo.userId);
							if (userInfo != null && !userInfo.CanSendFollow)
							{
								flag = true;
							}
							if (userInfo != null)
							{
								playerRecord2.charaInfo.selectedDegrees = userInfo.selectDegrees;
							}
						}
						Transform root = SetPrefab(itemsL[k], "QuestResultFriendItemL", true);
						if (playerRecord2.isSelf)
						{
							playerRecord2.charaInfo.selectedDegrees = MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds;
						}
						CharaInfo.ClanInfo clanInfo = playerRecord2.charaInfo.clanInfo;
						if (clanInfo == null)
						{
							clanInfo = new CharaInfo.ClanInfo();
							clanInfo.clanId = -1;
							clanInfo.tag = string.Empty;
						}
						bool isSameTeam = clanInfo.clanId > -1 && MonoBehaviourSingleton<GuildManager>.I.guildData != null && clanInfo.clanId == MonoBehaviourSingleton<GuildManager>.I.guildData.clanId;
						if (playerRecord2.isSelf)
						{
							SetSupportEncoding(root, UI.LBL_NAME_OWN, true);
							SetLabelText(root, UI.LBL_NAME_OWN, Utility.GetNameWithColoredClanTag(clanInfo.tag, playerRecord2.charaInfo.name, playerRecord2.id == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
							SetActive(root, UI.LBL_NAME, false);
						}
						else
						{
							SetSupportEncoding(root, UI.LBL_NAME, true);
							SetLabelText(root, UI.LBL_NAME, Utility.GetNameWithColoredClanTag(clanInfo.tag, playerRecord2.charaInfo.name, playerRecord2.id == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
							SetActive(root, UI.LBL_NAME_OWN, false);
						}
						if (!playerRecord2.isNPC)
						{
							int num9 = playerRecord2.charaInfo.level;
							if (playerRecord2.isSelf)
							{
								num9 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
							}
							SetLabelText(root, UI.LBL_LEVEL, string.Format(base.sectionData.GetText("LEVEL"), num9));
						}
						else
						{
							SetActive(root, UI.LBL_LEVEL, false);
							if (FindCtrl(root, UI.BTN_DETAIL) != null && base.GetComponent<UINoAuto>(root, (Enum)UI.BTN_DETAIL) == null)
							{
								FindCtrl(root, UI.BTN_DETAIL).get_gameObject().AddComponent<UINoAuto>();
							}
						}
						SetEvent(root, UI.BTN_DETAIL, "DETAIL", k);
						if (playerRecord2.isSelf)
						{
							SetButtonSprite(root, UI.BTN_DETAIL, "ResultPlatemine", true);
						}
						PlayerLoadInfo playerLoadInfo = playerRecord2.playerLoadInfo.Clone();
						playerLoadInfo.armModelID = -1;
						playerLoadInfo.weaponModelID = -1;
						playerLoadInfo.legModelID = -1;
						SetRenderPlayerModel(root, UI.TEX_MODEL, playerLoadInfo, 98, new Vector3(0f, -1.613f, 2.342f), new Vector3(0f, 154f, 0f), true, null);
						if (playerRecord2.charaInfo.selectedDegrees != null && playerRecord2.charaInfo.selectedDegrees.Count == GameDefine.DEGREE_PART_COUNT)
						{
							DegreePlate component = FindCtrl(root, UI.OBJ_DEGREE_PLATE).GetComponent<DegreePlate>();
							component.Initialize(playerRecord2.charaInfo.selectedDegrees, false, delegate
							{
							});
						}
						root = SetPrefab(itemsR[k], "QuestResultFriendItemR", true);
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
							SetActive(root, UI.SPR_NUM_1, false);
						}
						if (num12 != 0)
						{
							SetSprite(root, UI.SPR_NUM_2, num12.ToString("D2"));
						}
						else
						{
							SetActive(root, UI.SPR_NUM_2, false);
						}
						if (!playerRecord2.isSelf && !playerRecord2.isNPC)
						{
							SetEvent(root, UI.BTN_FOLLOW, "FOLLOW", k);
							if (!flag)
							{
								SetButtonSprite(root, UI.BTN_FOLLOW, "ResultfollowBtn", true);
								SetButtonEnabled(root, UI.BTN_FOLLOW, true);
							}
							else
							{
								SetButtonSprite(root, UI.BTN_FOLLOW, "ResultfollowBtnOff", true);
								SetButtonEnabled(root, UI.BTN_FOLLOW, false);
							}
						}
						else
						{
							SetActive(root, UI.BTN_FOLLOW, false);
						}
					}
				}
				else
				{
					Transform root2 = SetPrefab(itemsL[k], "QuestResultFriendItemL", true);
					SetActive(root2, UI.LBL_NAME, false);
					SetActive(root2, UI.LBL_NAME_OWN, false);
					SetActive(root2, UI.LBL_LEVEL, false);
					SetButtonSprite(root2, UI.BTN_DETAIL, "ResultPlateGrey", true);
					SetButtonEnabled(root2, UI.BTN_DETAIL, false);
					SetEvent(root2, UI.BTN_DETAIL, "DETAIL", k);
					SetActive(root2, UI.TEX_MODEL, false);
					root2 = SetPrefab(itemsR[k], "QuestResultFriendItemR", true);
					SetActive(root2, UI.SPR_NUM_0, false);
					SetActive(root2, UI.SPR_NUM_1, false);
					SetActive(root2, UI.SPR_NUM_2, false);
					SetActive(root2, UI.SPR_PER, false);
					SetActive(root2, UI.BTN_FOLLOW, false);
				}
			}
			PlayTween((Enum)UI.OBJ_ITEMS, true, (EventDelegate.Callback)null, true, 0);
		}
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
		}
		else
		{
			InGameRecorder.PlayerRecord playerRecord = playerRecords[index];
			if (MonoBehaviourSingleton<StatusManager>.I.HasEventEquipSet())
			{
				playerRecord.playerLoadInfo = PlayerLoadInfo.FromCharaInfo(playerRecord.charaInfo, true, true, true, true);
			}
			GameSection.ChangeEvent("DETAIL", playerRecord);
		}
	}

	private unsafe void OnQuery_FOLLOW()
	{
		int playerIndex = (int)GameSection.GetEventData();
		InGameRecorder.PlayerRecord record = playerRecords[playerIndex];
		if (record == null)
		{
			GameSection.StopEvent();
		}
		else if (MonoBehaviourSingleton<FriendManager>.I.followNum >= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow)
		{
			GameSection.ChangeEvent("FOLLOW_MAX", null);
		}
		else
		{
			GameSection.StayEvent();
			List<int> list = new List<int>();
			list.Add(record.charaInfo.userId);
			_003COnQuery_FOLLOW_003Ec__AnonStorey443 _003COnQuery_FOLLOW_003Ec__AnonStorey;
			MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(list, new Action<Error, List<int>>((object)_003COnQuery_FOLLOW_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
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
				GameSection.ChangeStayEvent("QUEST_TO_FIELD", null);
			}
			GameSection.ResumeEvent(true, null, false);
		};
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isQuestResultFieldLeave)
		{
			bool toHome = !MonoBehaviourSingleton<InGameManager>.I.IsQuestInField() && !MonoBehaviourSingleton<InGameManager>.I.IsQuestInPortal();
			MonoBehaviourSingleton<CoopApp>.I.LeaveWithParty(action, toHome, false);
		}
		else
		{
			action(true);
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
	}

	private void Update()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && !MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
		{
			float loseCameraRotateSpeed = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.loseCameraRotateSpeed;
			if (loseCameraRotateSpeed != 0f)
			{
				MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.RotateAround(cameraTarget, Vector3.get_up(), loseCameraRotateSpeed * Time.get_deltaTime());
			}
		}
	}
}
