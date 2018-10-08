using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultTrialEnd : GameSection
{
	private enum UI
	{
		OBJ_EQUIP_SET_NAME,
		LBL_EQUIP_SET_NAME,
		OBJ_NEXT,
		OBJ_GACHA,
		BTN_RETRY
	}

	private PlayerLoader[] playersModels;

	private List<InGameRecorder.PlayerRecord> playerRecords;

	private Vector3 cameraTarget;

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
		yield return (object)new WaitForEndOfFrame();
		yield return (object)MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(true);
		yield return (object)new WaitForEndOfFrame();
		if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			base.Initialize();
		}
		else
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
			playersModels = MonoBehaviourSingleton<InGameRecorder>.I.CreatePlayerModels();
			while (PlayerLoader.IsLoading(playersModels))
			{
				yield return (object)null;
			}
			Transform cameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
			if (MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
			{
				int k = 0;
				for (int j = playersModels.Length; k < j; k++)
				{
					PlayerLoader player = playersModels[k];
					if ((UnityEngine.Object)player != (UnityEngine.Object)null)
					{
						player.animator.applyRootMotion = false;
						player.animator.Play("win_loop");
					}
				}
			}
			else if (playersModels.Length > 0)
			{
				OutGameSettingsManager.QuestResult param = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult;
				SoundManager.RequestBGM(10, false);
				PlayerLoader player_loader = playersModels[0];
				if ((UnityEngine.Object)player_loader != (UnityEngine.Object)null)
				{
					Transform playerTransform = player_loader.transform;
					cameraTarget = playerTransform.position + new Vector3(0f, param.loseCameraHeight, 0f);
					Vector3 camera_pos = cameraTarget + playerTransform.forward * param.loseCameraDistance;
					Quaternion camera_rot = Quaternion.LookRotation(cameraTarget - camera_pos);
					cameraTransform.position = camera_pos;
					cameraTransform.rotation = camera_rot;
					PlayerAnimCtrl.Get(default_anim: (UnityEngine.Random.Range(0, 2) != 0) ? PLCA.IDLE_02 : PLCA.IDLE_01, _animator: player_loader.animator, on_play: null, on_change: null, on_end: null);
				}
				MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView = param.cameraFieldOfView;
			}
			GC.Collect();
			MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
			if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null)
			{
				MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
				MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
			}
			base.Initialize();
		}
	}

	public override void UpdateUI()
	{
		if (MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			string text = string.Empty;
			if (MonoBehaviourSingleton<StatusManager>.I.assignedEquipmentData != null)
			{
				text = MonoBehaviourSingleton<StatusManager>.I.assignedEquipmentData.setName;
			}
			SetLabelText(UI.LBL_EQUIP_SET_NAME, text);
			UITweenCtrl component = GetCtrl(UI.OBJ_EQUIP_SET_NAME).GetComponent<UITweenCtrl>();
			component.Reset();
			component.Play(true, delegate
			{
				SetActive(UI.OBJ_NEXT, true);
				SetActive(UI.OBJ_GACHA, true);
				SetActive(UI.BTN_RETRY, true);
			});
		}
	}

	protected override void OnClose()
	{
		try
		{
			if (MonoBehaviourSingleton<InGameManager>.IsValid() && !MonoBehaviourSingleton<InGameManager>.I.isRetry)
			{
				ResetAssignedEquipmentInfo();
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
			GameSection.ResumeEvent(true, null);
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
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
	}

	private void OnQuery_GACHA()
	{
		EventData[] autoEvents = new EventData[2]
		{
			new EventData("NEXT", null),
			new EventData("MAIN_MENU_SHOP", null)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private void OnQuery_RETRY()
	{
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.Clear();
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.isRetry = true;
		}
		MonoBehaviourSingleton<GameSceneManager>.I.ReloadScene(UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
	}

	private void ResetAssignedEquipmentInfo()
	{
		if (MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusManager>.I.ClearTrial();
		}
		if (QuestManager.IsValidTrial())
		{
			MonoBehaviourSingleton<QuestManager>.I.ClearTrial();
		}
	}
}
