// Decompiled with JetBrains decompiler
// Type: QuestResultTrialEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestResultTrialEnd : GameSection
{
  private PlayerLoader[] playersModels;
  private List<InGameRecorder.PlayerRecord> playerRecords;
  private Vector3 cameraTarget;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
    yield return (object) new WaitForEndOfFrame();
    yield return (object) MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(true);
    yield return (object) new WaitForEndOfFrame();
    if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
    {
      base.Initialize();
    }
    else
    {
      this.playerRecords = MonoBehaviourSingleton<InGameRecorder>.I.players;
      int index1 = 0;
      while (index1 < this.playerRecords.Count)
      {
        InGameRecorder.PlayerRecord playerRecord = this.playerRecords[index1];
        if (playerRecord == null || playerRecord.playerLoadInfo == null)
          this.playerRecords.RemoveAt(index1);
        else
          ++index1;
      }
      this.playersModels = MonoBehaviourSingleton<InGameRecorder>.I.CreatePlayerModels();
      while (PlayerLoader.IsLoading(this.playersModels))
        yield return (object) null;
      Transform mainCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
      if (MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
      {
        int index2 = 0;
        for (int length = this.playersModels.Length; index2 < length; ++index2)
        {
          PlayerLoader playersModel = this.playersModels[index2];
          if (Object.op_Inequality((Object) playersModel, (Object) null))
          {
            playersModel.animator.applyRootMotion = false;
            playersModel.animator.Play(playersModel.GetWinLoopMotionState());
          }
        }
      }
      else if (this.playersModels.Length != 0)
      {
        OutGameSettingsManager.QuestResult questResult = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult;
        SoundManager.RequestBGM(10, false);
        PlayerLoader playersModel = this.playersModels[0];
        if (Object.op_Inequality((Object) playersModel, (Object) null))
        {
          Transform transform = ((Component) playersModel).transform;
          this.cameraTarget = Vector3.op_Addition(transform.position, new Vector3(0.0f, questResult.loseCameraHeight, 0.0f));
          Vector3 vector3 = Vector3.op_Addition(this.cameraTarget, Vector3.op_Multiply(transform.forward, questResult.loseCameraDistance));
          Quaternion quaternion = Quaternion.LookRotation(Vector3.op_Subtraction(this.cameraTarget, vector3));
          mainCameraTransform.position = vector3;
          mainCameraTransform.rotation = quaternion;
          PLCA default_anim = Random.Range(0, 2) == 0 ? PLCA.IDLE_01 : PLCA.IDLE_02;
          PlayerAnimCtrl.Get(playersModel.animator, default_anim);
        }
        MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView = questResult.cameraFieldOfView;
      }
      GC.Collect();
      MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      {
        MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
        MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
      }
      base.Initialize();
    }
  }

  public override void UpdateUI()
  {
    if (!MonoBehaviourSingleton<StatusManager>.IsValid())
      return;
    string text = "";
    if (MonoBehaviourSingleton<StatusManager>.I.assignedEquipmentData != null)
      text = MonoBehaviourSingleton<StatusManager>.I.assignedEquipmentData.setName;
    this.SetLabelText((Enum) QuestResultTrialEnd.UI.LBL_EQUIP_SET_NAME, text);
    this.SetActive((Enum) QuestResultTrialEnd.UI.BTN_NEXT, false);
    this.SetActive((Enum) QuestResultTrialEnd.UI.BTN_GACHA, false);
    this.SetActive((Enum) QuestResultTrialEnd.UI.BTN_RETRY, false);
    UITweenCtrl component = ((Component) this.GetCtrl((Enum) QuestResultTrialEnd.UI.OBJ_EQUIP_SET_NAME)).GetComponent<UITweenCtrl>();
    component.Reset();
    component.Play(onFinished: (EventDelegate.Callback) (() =>
    {
      this.SetActive((Enum) QuestResultTrialEnd.UI.BTN_NEXT, true);
      this.SetActive((Enum) QuestResultTrialEnd.UI.BTN_GACHA, true);
      this.SetActive((Enum) QuestResultTrialEnd.UI.BTN_RETRY, true);
    }));
  }

  protected override void OnClose()
  {
    try
    {
      if (MonoBehaviourSingleton<InGameManager>.IsValid() && !MonoBehaviourSingleton<InGameManager>.I.isRetry)
        this.ResetAssignedEquipmentInfo();
      base.OnClose();
    }
    catch (Exception ex)
    {
      Log.Warning(LOG.UI, "QuestRequest OnClose\n{0}\n{1}", (object) ex.Message, (object) ex.StackTrace);
    }
  }

  protected override void OnDestroy()
  {
    if (AppMain.isApplicationQuit)
      return;
    base.OnDestroy();
    if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
      return;
    MonoBehaviourSingleton<InGameRecorder>.I.DeletePlayerModels();
  }

  private void Update()
  {
    if (!MonoBehaviourSingleton<InGameRecorder>.IsValid() || MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
      return;
    float cameraRotateSpeed = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.loseCameraRotateSpeed;
    if ((double) cameraRotateSpeed == 0.0)
      return;
    MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.RotateAround(this.cameraTarget, Vector3.up, cameraRotateSpeed * Time.deltaTime);
  }

  private void OnQuery_NEXT()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ChatManager>.I.SwitchRoomChatConnectionToCoopConnection();
    Action<bool> call_back = (Action<bool>) (tf =>
    {
      if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsQuestInField())
      {
        MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToField = true;
        GameSection.ChangeStayEvent("QUEST_TO_FIELD");
      }
      GameSection.ResumeEvent(true);
    });
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isQuestResultFieldLeave)
    {
      bool toHome = !MonoBehaviourSingleton<InGameManager>.I.IsQuestInField() && !MonoBehaviourSingleton<InGameManager>.I.IsQuestInPortal();
      MonoBehaviourSingleton<CoopApp>.I.LeaveWithParty(call_back, toHome);
    }
    else
      call_back(true);
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
    MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
  }

  private void OnQuery_GACHA()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData("NEXT", (object) null),
      new EventData("MAIN_MENU_SHOP", (object) null)
    });
  }

  private void OnQuery_RETRY()
  {
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.Clear();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.isRetry = true;
    MonoBehaviourSingleton<UIManager>.I.loading.SetShowTipsList(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
    MonoBehaviourSingleton<GameSceneManager>.I.ReloadScene();
  }

  private void ResetAssignedEquipmentInfo()
  {
    if (MonoBehaviourSingleton<StatusManager>.IsValid())
      MonoBehaviourSingleton<StatusManager>.I.ClearTrial();
    if (!QuestManager.IsValidTrial())
      return;
    MonoBehaviourSingleton<QuestManager>.I.ClearTrial();
  }

  private enum UI
  {
    OBJ_EQUIP_SET_NAME,
    LBL_EQUIP_SET_NAME,
    BTN_NEXT,
    BTN_GACHA,
    BTN_RETRY,
  }
}
