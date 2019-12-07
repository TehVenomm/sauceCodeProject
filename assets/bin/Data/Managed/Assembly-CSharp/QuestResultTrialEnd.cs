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
		BTN_NEXT,
		BTN_GACHA,
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
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(active: true);
		yield return new WaitForEndOfFrame();
		yield return MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(need_gc_collect: true);
		yield return new WaitForEndOfFrame();
		if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			base.Initialize();
			yield break;
		}
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
		playersModels = MonoBehaviourSingleton<InGameRecorder>.I.CreatePlayerModels();
		while (PlayerLoader.IsLoading(playersModels))
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
		GC.Collect();
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(active: false);
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			string text = "";
			if (MonoBehaviourSingleton<StatusManager>.I.assignedEquipmentData != null)
			{
				text = MonoBehaviourSingleton<StatusManager>.I.assignedEquipmentData.setName;
			}
			SetLabelText(UI.LBL_EQUIP_SET_NAME, text);
			SetActive(UI.BTN_NEXT, is_visible: false);
			SetActive(UI.BTN_GACHA, is_visible: false);
			SetActive(UI.BTN_RETRY, is_visible: false);
			UITweenCtrl component = GetCtrl(UI.OBJ_EQUIP_SET_NAME).GetComponent<UITweenCtrl>();
			component.Reset();
			component.Play(forward: true, delegate
			{
				SetActive(UI.BTN_NEXT, is_visible: true);
				SetActive(UI.BTN_GACHA, is_visible: true);
				SetActive(UI.BTN_RETRY, is_visible: true);
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
		MonoBehaviourSingleton<UIManager>.I.loading.SetShowTipsList(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		MonoBehaviourSingleton<GameSceneManager>.I.ReloadScene();
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
