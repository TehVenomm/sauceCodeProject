// Decompiled with JetBrains decompiler
// Type: QuestResultFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestResultFriend : GameSection
{
  public PlayerLoader[] playersModels;
  private List<InGameRecorder.PlayerRecord> playerRecords;
  private Transform[] itemsL = new Transform[4];
  private Transform[] itemsR = new Transform[4];
  private Vector3 cameraTarget;
  private List<int> score_list = new List<int>();
  private List<float> score_truncation_list = new List<float>();

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
    yield return (object) new WaitForEndOfFrame();
    yield return (object) MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(true);
    yield return (object) new WaitForEndOfFrame();
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
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
      bool waitLoad = true;
      MonoBehaviourSingleton<InGameRecorder>.I.CreatePlayerModelsAsync((Action<PlayerLoader[]>) (loaders =>
      {
        this.playersModels = loaders;
        waitLoad = false;
      }));
      while (waitLoad)
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
        mainCameraTransform.position = Vector3.op_Addition(mainCameraTransform.position, Vector3.op_Multiply(mainCameraTransform.forward, 1.5f));
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
    }
    this.itemsL[0] = this.GetCtrl((Enum) QuestResultFriend.UI.OBJ_ITEM_POS_L_0);
    this.itemsL[1] = this.GetCtrl((Enum) QuestResultFriend.UI.OBJ_ITEM_POS_L_1);
    this.itemsL[2] = this.GetCtrl((Enum) QuestResultFriend.UI.OBJ_ITEM_POS_L_2);
    this.itemsL[3] = this.GetCtrl((Enum) QuestResultFriend.UI.OBJ_ITEM_POS_L_3);
    this.itemsR[0] = this.GetCtrl((Enum) QuestResultFriend.UI.OBJ_ITEM_POS_R_0);
    this.itemsR[1] = this.GetCtrl((Enum) QuestResultFriend.UI.OBJ_ITEM_POS_R_1);
    this.itemsR[2] = this.GetCtrl((Enum) QuestResultFriend.UI.OBJ_ITEM_POS_R_2);
    this.itemsR[3] = this.GetCtrl((Enum) QuestResultFriend.UI.OBJ_ITEM_POS_R_3);
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
    {
      this.SetActive((Enum) QuestResultFriend.UI.SPR_TITLE, !MonoBehaviourSingleton<InGameManager>.I.IsRush());
      this.SetActive((Enum) QuestResultFriend.UI.SPR_RUSH_TITLE, MonoBehaviourSingleton<InGameManager>.I.IsRush());
    }
    if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
      MonoBehaviourSingleton<SceneSettingsManager>.I.DisableWaveTarget();
    GC.Collect();
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
      return;
    float hpContainsHealed = (float) MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHpContainsHealed();
    int num1 = 0;
    float num2 = 0.0f;
    this.score_list.Clear();
    this.score_truncation_list.Clear();
    for (int index = 0; index < 4; ++index)
    {
      int num3 = 0;
      float num4 = 0.0f;
      if ((this.playerRecords == null || index >= this.playerRecords.Count ? 0 : (this.playerRecords[index] != null ? 1 : 0)) != 0)
      {
        num4 = (float) (num3 = (int) ((double) this.playerRecords[index].givenTotalDamage / (double) hpContainsHealed * 100.0)) - (float) num3;
        if (num1 + num3 > 100)
          num3 = 100 - num1;
        num1 += num3;
      }
      this.score_list.Add(num3);
      this.score_truncation_list.Add(num4);
      num2 += num4;
    }
    int num5 = (int) ((double) num2 + 0.10000000149011612);
    if (num5 > 0 && num1 < 100)
    {
      for (int index = 0; index < 4; ++index)
      {
        int num6 = Mathf.CeilToInt(this.score_truncation_list[index]);
        this.score_list[index] += num6;
        num5 -= num6;
        num1 += num6;
        if (num5 <= 0 || num1 >= 100)
          break;
      }
    }
    InGameRecorder.CheckAndRepairIsSelf(ref this.playerRecords);
    for (int index = 0; index < 4; ++index)
    {
      if ((this.playerRecords == null || index >= this.playerRecords.Count ? 0 : (this.playerRecords[index] != null ? 1 : 0)) != 0)
      {
        InGameRecorder.PlayerRecord playerRecord = this.playerRecords[index];
        if (playerRecord != null && playerRecord.charaInfo != null)
        {
          bool flag = false;
          if (playerRecord.id != 0 && playerRecord.charaInfo.userId != 0)
          {
            QuestResultUserCollection.ResultUserInfo userInfo = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserInfo(playerRecord.charaInfo.userId);
            if (userInfo != null && !userInfo.CanSendFollow)
              flag = true;
            if (userInfo != null)
              playerRecord.charaInfo.selectedDegrees = userInfo.selectDegrees;
          }
          Transform root1 = this.SetPrefab(this.itemsL[index], "QuestResultFriendItemL");
          if (playerRecord.isSelf)
            playerRecord.charaInfo.selectedDegrees = MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds;
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
            this.SetSupportEncoding(root1, (Enum) QuestResultFriend.UI.LBL_NAME_OWN, true);
            this.SetLabelText(root1, (Enum) QuestResultFriend.UI.LBL_NAME_OWN, Utility.GetNameWithColoredClanTag(clanInfo.tag, playerRecord.charaInfo.name, playerRecord.id == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
            this.SetActive(root1, (Enum) QuestResultFriend.UI.LBL_NAME, false);
          }
          else
          {
            this.SetSupportEncoding(root1, (Enum) QuestResultFriend.UI.LBL_NAME, true);
            this.SetLabelText(root1, (Enum) QuestResultFriend.UI.LBL_NAME, Utility.GetNameWithColoredClanTag(clanInfo.tag, playerRecord.charaInfo.name, playerRecord.id == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
            this.SetActive(root1, (Enum) QuestResultFriend.UI.LBL_NAME_OWN, false);
          }
          if (!playerRecord.isNPC)
          {
            int level = (int) playerRecord.charaInfo.level;
            if (playerRecord.isSelf)
              level = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
            this.SetLabelText(root1, (Enum) QuestResultFriend.UI.LBL_LEVEL, string.Format(this.sectionData.GetText("LEVEL"), (object) level));
          }
          else
          {
            this.SetActive(root1, (Enum) QuestResultFriend.UI.LBL_LEVEL, false);
            if (Object.op_Inequality((Object) this.FindCtrl(root1, (Enum) QuestResultFriend.UI.BTN_DETAIL), (Object) null) && Object.op_Equality((Object) this.GetComponent<UINoAuto>(root1, (Enum) QuestResultFriend.UI.BTN_DETAIL), (Object) null))
              ((Component) this.FindCtrl(root1, (Enum) QuestResultFriend.UI.BTN_DETAIL)).gameObject.AddComponent<UINoAuto>();
          }
          this.SetEvent(root1, (Enum) QuestResultFriend.UI.BTN_DETAIL, "DETAIL", index);
          if (playerRecord.isSelf)
            this.SetButtonSprite(root1, (Enum) QuestResultFriend.UI.BTN_DETAIL, "ResultPlatemine", true);
          PlayerLoadInfo info = playerRecord.playerLoadInfo.Clone();
          info.armModelID = -1;
          info.weaponModelID = -1;
          info.legModelID = -1;
          this.SetRenderPlayerModel(root1, (Enum) QuestResultFriend.UI.TEX_MODEL, info, 98, new Vector3(0.0f, -1.613f, 2.342f), new Vector3(0.0f, 154f, 0.0f), true);
          if (playerRecord.charaInfo.selectedDegrees != null && playerRecord.charaInfo.selectedDegrees.Count == GameDefine.DEGREE_PART_COUNT)
            ((Component) this.FindCtrl(root1, (Enum) QuestResultFriend.UI.OBJ_DEGREE_PLATE)).GetComponent<DegreePlate>().Initialize(playerRecord.charaInfo.selectedDegrees, false, (Action<DegreePlate>) (x => { }));
          Transform root2 = this.SetPrefab(this.itemsR[index], "QuestResultFriendItemR");
          int num7 = this.score_list[index] % 10;
          int num8 = this.score_list[index] / 10 % 10;
          int num9 = this.score_list[index] / 100;
          this.SetSprite(root2, (Enum) QuestResultFriend.UI.SPR_NUM_0, num7.ToString("D2"));
          if (num9 != 0 || num8 != 0)
            this.SetSprite(root2, (Enum) QuestResultFriend.UI.SPR_NUM_1, num8.ToString("D2"));
          else
            this.SetActive(root2, (Enum) QuestResultFriend.UI.SPR_NUM_1, false);
          if (num9 != 0)
            this.SetSprite(root2, (Enum) QuestResultFriend.UI.SPR_NUM_2, num9.ToString("D2"));
          else
            this.SetActive(root2, (Enum) QuestResultFriend.UI.SPR_NUM_2, false);
          if (!playerRecord.isSelf && !playerRecord.isNPC)
          {
            this.SetEvent(root2, (Enum) QuestResultFriend.UI.BTN_FOLLOW, "FOLLOW", index);
            if (!flag)
            {
              this.SetButtonSprite(root2, (Enum) QuestResultFriend.UI.BTN_FOLLOW, "ResultfollowBtn", true);
              this.SetButtonEnabled(root2, (Enum) QuestResultFriend.UI.BTN_FOLLOW, true);
            }
            else
            {
              this.SetButtonSprite(root2, (Enum) QuestResultFriend.UI.BTN_FOLLOW, "ResultfollowBtnOff", true);
              this.SetButtonEnabled(root2, (Enum) QuestResultFriend.UI.BTN_FOLLOW, false);
            }
          }
          else
            this.SetActive(root2, (Enum) QuestResultFriend.UI.BTN_FOLLOW, false);
        }
      }
      else
      {
        Transform root3 = this.SetPrefab(this.itemsL[index], "QuestResultFriendItemL");
        this.SetActive(root3, (Enum) QuestResultFriend.UI.LBL_NAME, false);
        this.SetActive(root3, (Enum) QuestResultFriend.UI.LBL_NAME_OWN, false);
        this.SetActive(root3, (Enum) QuestResultFriend.UI.LBL_LEVEL, false);
        this.SetButtonSprite(root3, (Enum) QuestResultFriend.UI.BTN_DETAIL, "ResultPlateGrey", true);
        this.SetButtonEnabled(root3, (Enum) QuestResultFriend.UI.BTN_DETAIL, false);
        this.SetEvent(root3, (Enum) QuestResultFriend.UI.BTN_DETAIL, "DETAIL", index);
        this.SetActive(root3, (Enum) QuestResultFriend.UI.TEX_MODEL, false);
        Transform root4 = this.SetPrefab(this.itemsR[index], "QuestResultFriendItemR");
        this.SetActive(root4, (Enum) QuestResultFriend.UI.SPR_NUM_0, false);
        this.SetActive(root4, (Enum) QuestResultFriend.UI.SPR_NUM_1, false);
        this.SetActive(root4, (Enum) QuestResultFriend.UI.SPR_NUM_2, false);
        this.SetActive(root4, (Enum) QuestResultFriend.UI.SPR_PER, false);
        this.SetActive(root4, (Enum) QuestResultFriend.UI.BTN_FOLLOW, false);
      }
    }
    this.PlayTween((Enum) QuestResultFriend.UI.OBJ_ITEMS);
  }

  protected override void OnClose()
  {
    UILabel.OutlineLimit = false;
    try
    {
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.ClearRush();
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

  private void OnQuery_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    if (this.playerRecords[eventData].isNPC)
    {
      GameSection.StopEvent();
    }
    else
    {
      InGameRecorder.PlayerRecord playerRecord = this.playerRecords[eventData];
      if (MonoBehaviourSingleton<StatusManager>.I.HasEventEquipSet())
        playerRecord.playerLoadInfo = PlayerLoadInfo.FromCharaInfo(playerRecord.charaInfo, true, true, true, true);
      GameSection.ChangeEvent("DETAIL", (object) playerRecord);
    }
  }

  private void OnQuery_FOLLOW()
  {
    int playerIndex = (int) GameSection.GetEventData();
    InGameRecorder.PlayerRecord record = this.playerRecords[playerIndex];
    if (record == null)
      GameSection.StopEvent();
    else if (MonoBehaviourSingleton<FriendManager>.I.followNum >= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow)
    {
      GameSection.ChangeEvent("FOLLOW_MAX");
    }
    else
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(new List<int>()
      {
        record.charaInfo.userId
      }, (Action<Error, List<int>>) ((err, follow_list) =>
      {
        if (err == Error.None)
        {
          GameSection.ChangeStayEvent("FOLLOW_DIALOG", (object) new object[1]
          {
            (object) record.charaInfo.name
          });
          Transform root = this.itemsL[playerIndex];
          this.SetButtonSprite(root, (Enum) QuestResultFriend.UI.BTN_FOLLOW, "ResultfollowBtnOff", true);
          this.SetButtonEnabled(root, (Enum) QuestResultFriend.UI.BTN_FOLLOW, false);
          if (MonoBehaviourSingleton<CoopApp>.IsValid())
            CoopApp.UpdateField();
        }
        else if (follow_list.Count == 0)
          GameSection.ChangeStayEvent("FAILED_FOLLOW", (object) new object[1]
          {
            (object) record.charaInfo.name
          });
        GameSection.ResumeEvent(err == Error.None);
      }));
    }
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM;
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

  private void Update()
  {
    if (!MonoBehaviourSingleton<InGameRecorder>.IsValid() || MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
      return;
    float cameraRotateSpeed = MonoBehaviourSingleton<OutGameSettingsManager>.I.questResult.loseCameraRotateSpeed;
    if ((double) cameraRotateSpeed == 0.0)
      return;
    MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.RotateAround(this.cameraTarget, Vector3.up, cameraRotateSpeed * Time.deltaTime);
  }

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
    SPR_RUSH_TITLE,
  }
}
