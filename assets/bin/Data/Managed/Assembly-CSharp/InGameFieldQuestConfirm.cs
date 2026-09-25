// Decompiled with JetBrains decompiler
// Type: InGameFieldQuestConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class InGameFieldQuestConfirm : GameSection
{
  private float timeLimit;
  private int prevTime;
  private bool isAnswer;
  private int countAnimStep;

  public override void Initialize()
  {
    base.Initialize();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
  }

  public override void UpdateUI()
  {
    if (!(GameSection.GetEventData() is InGameFieldQuestConfirm.Desc eventData))
      return;
    QuestTable.QuestTableData questData = eventData.questData;
    if (questData == null)
      return;
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
    if (enemyData == null)
      return;
    int mainEnemyLv = questData.GetMainEnemyLv();
    this.SetLabelText((Enum) InGameFieldQuestConfirm.UI.LBL_NAME, enemyData.name);
    this.SetLabelText((Enum) InGameFieldQuestConfirm.UI.NUM_LV, mainEnemyLv.ToString());
    this.SetElementSprite((Enum) InGameFieldQuestConfirm.UI.STR_ELEM, (int) enemyData.weakElement);
    if (enemyData.weakElement != ELEMENT_TYPE.MAX)
      this.SetActive((Enum) InGameFieldQuestConfirm.UI.STR_WEAK_NONE, false);
    this.SetLabelText((Enum) InGameFieldQuestConfirm.UI.NUM_TIMER, $"{(int) ((double) questData.limitTime / 60.0):D2}:{(int) ((double) questData.limitTime % 60.0):D2}");
    InGameFieldQuestConfirm.UI[] uiArray1 = new InGameFieldQuestConfirm.UI[10]
    {
      InGameFieldQuestConfirm.UI.OBJ_DIFFICULT_STAR_1,
      InGameFieldQuestConfirm.UI.OBJ_DIFFICULT_STAR_2,
      InGameFieldQuestConfirm.UI.OBJ_DIFFICULT_STAR_3,
      InGameFieldQuestConfirm.UI.OBJ_DIFFICULT_STAR_4,
      InGameFieldQuestConfirm.UI.OBJ_DIFFICULT_STAR_5,
      InGameFieldQuestConfirm.UI.OBJ_DIFFICULT_STAR_6,
      InGameFieldQuestConfirm.UI.OBJ_DIFFICULT_STAR_7,
      InGameFieldQuestConfirm.UI.OBJ_DIFFICULT_STAR_8,
      InGameFieldQuestConfirm.UI.OBJ_DIFFICULT_STAR_9,
      InGameFieldQuestConfirm.UI.OBJ_DIFFICULT_STAR_10
    };
    int num1 = (int) (questData.difficulty + 1);
    int index1 = 0;
    for (int length = uiArray1.Length; index1 < length; ++index1)
      this.SetActive((Enum) uiArray1[index1], index1 < num1);
    this.PlayTween((Enum) InGameFieldQuestConfirm.UI.TWN_DIFFICULT_STAR, is_input_block: false);
    QuestInfoData.Mission[] missionData = QuestInfoData.CreateMissionData(questData);
    if (missionData != null)
    {
      ((Component) this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.MISSION)).gameObject.SetActive(true);
      InGameFieldQuestConfirm.UI[] uiArray2 = new InGameFieldQuestConfirm.UI[3]
      {
        InGameFieldQuestConfirm.UI.MISSION_LABEL_01,
        InGameFieldQuestConfirm.UI.MISSION_LABEL_02,
        InGameFieldQuestConfirm.UI.MISSION_LABEL_03
      };
      InGameFieldQuestConfirm.UI[] uiArray3 = new InGameFieldQuestConfirm.UI[3]
      {
        InGameFieldQuestConfirm.UI.MISSION_CROWN_ON_01,
        InGameFieldQuestConfirm.UI.MISSION_CROWN_ON_02,
        InGameFieldQuestConfirm.UI.MISSION_CROWN_ON_03
      };
      InGameFieldQuestConfirm.UI[] uiArray4 = new InGameFieldQuestConfirm.UI[3]
      {
        InGameFieldQuestConfirm.UI.MISSION_CROWN_OFF_01,
        InGameFieldQuestConfirm.UI.MISSION_CROWN_OFF_02,
        InGameFieldQuestConfirm.UI.MISSION_CROWN_OFF_03
      };
      int num2 = Mathf.Min(missionData.Length, 3);
      for (int index2 = 0; index2 < num2; ++index2)
      {
        QuestInfoData.Mission mission = missionData[index2];
        this.SetActive((Enum) uiArray3[index2], CLEAR_STATUS.CLEAR == mission.state);
        this.SetActive((Enum) uiArray4[index2], CLEAR_STATUS.CLEAR != mission.state);
        this.SetLabelText((Enum) uiArray2[index2], mission.tableData.missionText);
      }
    }
    if (eventData.reward != null)
      Array.Sort<QuestInfoData.Quest.Reward>(eventData.reward, (Comparison<QuestInfoData.Quest.Reward>) ((l, r) => l.priority - r.priority));
    this.SetFontStyle((Enum) InGameFieldQuestConfirm.UI.LBL_NAME, (FontStyle) 2);
    this.SetFontStyle((Enum) InGameFieldQuestConfirm.UI.NUM_LV, (FontStyle) 2);
    this.SetFontStyle((Enum) InGameFieldQuestConfirm.UI.LBL_LV, (FontStyle) 2);
    this.countAnimStep = 0;
    this.timeLimit = MonoBehaviourSingleton<InGameSettingsManager>.I.happenQuestDirection.confirmUITime;
    this.prevTime = -1;
    this.isAnswer = false;
    this.Update();
    this.UpdateAnchors();
    this.PlayTween((Enum) InGameFieldQuestConfirm.UI.OBJ_FRAME);
    this.PlayTween((Enum) InGameFieldQuestConfirm.UI.COUNT_ANIM_0, is_input_block: false);
  }

  public override void Exit()
  {
    base.Exit();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  private void Update()
  {
    if (this.isAnswer)
      return;
    this.timeLimit -= Time.deltaTime;
    if ((double) this.timeLimit <= 0.0)
      this.timeLimit = 0.0f;
    int timeLimit = (int) this.timeLimit;
    if ((double) this.timeLimit > 0.0 && (double) this.timeLimit - (double) timeLimit > 0.0)
      ++timeLimit;
    switch (this.countAnimStep)
    {
      case 0:
        if (timeLimit < 10)
        {
          this.ResetTween((Enum) InGameFieldQuestConfirm.UI.COUNT_ANIM_0);
          this.PlayTween((Enum) InGameFieldQuestConfirm.UI.COUNT_ANIM_1, is_input_block: false);
          ++this.countAnimStep;
          break;
        }
        break;
      case 1:
        if (timeLimit < 6)
        {
          this.ResetTween((Enum) InGameFieldQuestConfirm.UI.COUNT_ANIM_1);
          this.PlayTween((Enum) InGameFieldQuestConfirm.UI.COUNT_ANIM_2, is_input_block: false);
          this.SetColor((Enum) InGameFieldQuestConfirm.UI.LBL_TIME, Color.red);
          ++this.countAnimStep;
          break;
        }
        break;
      case 2:
        if (this.prevTime != timeLimit)
        {
          this.ResetTween((Enum) InGameFieldQuestConfirm.UI.COUNT_ANIM_2);
          this.PlayTween((Enum) InGameFieldQuestConfirm.UI.COUNT_ANIM_2, is_input_block: false);
          break;
        }
        break;
    }
    if (this.prevTime != timeLimit)
    {
      this.SetLabelText((Enum) InGameFieldQuestConfirm.UI.LBL_TIME, timeLimit.ToString());
      this.prevTime = timeLimit;
    }
    if ((double) this.timeLimit > 0.0 || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<InGameProgress>.I.endHappenQuestDirection)
      return;
    this.ResetTween((Enum) InGameFieldQuestConfirm.UI.COUNT_ANIM_0);
    this.ResetTween((Enum) InGameFieldQuestConfirm.UI.COUNT_ANIM_1);
    this.ResetTween((Enum) InGameFieldQuestConfirm.UI.COUNT_ANIM_2);
    this.OnQuery_YES();
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();
  }

  private void OnScreenRotate(bool is_portrait)
  {
    Transform ctrl1 = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.BOSS_INFO_MAIN);
    Transform ctrl2 = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.BOSS_INFO_SUB);
    Transform ctrl3 = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.BTN_FRAME);
    Transform ctrl4 = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.MISSION);
    Transform ctrl5 = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.TIME);
    Transform ctrl6 = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.SPR_BTN_0);
    Transform ctrl7 = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.SPR_BTN_2);
    Vector3 localPosition1 = ctrl1.localPosition;
    Vector3 localPosition2 = ctrl2.localPosition;
    Vector3 localPosition3 = ctrl3.localPosition;
    Vector3 localPosition4 = ctrl4.localPosition;
    if (is_portrait)
    {
      ctrl1.parent = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.PORTRAIT_MAIN);
      ctrl2.parent = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.PORTRAIT_SUB);
      ctrl3.parent = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.PORTRAIT_BTN);
      ctrl4.parent = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.PORTRAIT_MISSION);
      ctrl5.localPosition = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.PORTRAIT_TIME).localPosition;
      ctrl6.localPosition = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.PORTRAIT_BTN_0).localPosition;
      ctrl7.localPosition = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.PORTRAIT_BTN_2).localPosition;
    }
    else
    {
      ctrl1.parent = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.LANDSCAPE_MAIN);
      ctrl2.parent = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.LANDSCAPE_SUB);
      ctrl3.parent = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.LANDSCAPE_BTN);
      ctrl4.parent = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.LANDSCAPE_MISSION);
      ctrl5.localPosition = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.LANDSCAPE_TIME).localPosition;
      ctrl6.localPosition = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.LANDSCAPE_BTN_0).localPosition;
      ctrl7.localPosition = this.GetCtrl((Enum) InGameFieldQuestConfirm.UI.LANDSCAPE_BTN_2).localPosition;
    }
    ctrl1.localPosition = localPosition1;
    ctrl2.localPosition = localPosition2;
    ctrl3.localPosition = localPosition3;
    ctrl4.localPosition = localPosition4;
  }

  private void OnQuery_YES()
  {
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      MonoBehaviourSingleton<InGameProgress>.I.OnFieldQuestConfirm(true);
    this.isAnswer = true;
  }

  private void OnQuery_NO()
  {
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      MonoBehaviourSingleton<InGameProgress>.I.OnFieldQuestConfirm(false);
    this.isAnswer = true;
  }

  private enum UI
  {
    PORTRAIT_MAIN,
    PORTRAIT_SUB,
    PORTRAIT_DROP,
    PORTRAIT_BTN,
    PORTRAIT_TIME,
    PORTRAIT_BTN_0,
    PORTRAIT_BTN_2,
    PORTRAIT_MISSION,
    LANDSCAPE_MAIN,
    LANDSCAPE_SUB,
    LANDSCAPE_DROP,
    LANDSCAPE_BTN,
    LANDSCAPE_TIME,
    LANDSCAPE_BTN_0,
    LANDSCAPE_BTN_2,
    LANDSCAPE_MISSION,
    BOSS_INFO_MAIN,
    BOSS_INFO_SUB,
    BTN_FRAME,
    TIME,
    SPR_BTN_0,
    SPR_BTN_2,
    MISSION,
    LBL_NAME,
    NUM_LV,
    LBL_LV,
    STR_ELEM,
    STR_WEAK_NONE,
    NUM_TIMER,
    LBL_TIME,
    TWN_DIFFICULT_STAR,
    OBJ_DIFFICULT_STAR_1,
    OBJ_DIFFICULT_STAR_2,
    OBJ_DIFFICULT_STAR_3,
    OBJ_DIFFICULT_STAR_4,
    OBJ_DIFFICULT_STAR_5,
    OBJ_DIFFICULT_STAR_6,
    OBJ_DIFFICULT_STAR_7,
    OBJ_DIFFICULT_STAR_8,
    OBJ_DIFFICULT_STAR_9,
    OBJ_DIFFICULT_STAR_10,
    COUNT_ANIM_0,
    COUNT_ANIM_1,
    COUNT_ANIM_2,
    OBJ_FRAME,
    MISSION_LABEL_01,
    MISSION_LABEL_02,
    MISSION_LABEL_03,
    MISSION_CROWN_ON_01,
    MISSION_CROWN_ON_02,
    MISSION_CROWN_ON_03,
    MISSION_CROWN_OFF_01,
    MISSION_CROWN_OFF_02,
    MISSION_CROWN_OFF_03,
  }

  public class Desc
  {
    public QuestTable.QuestTableData questData;
    public QuestInfoData.Quest.Reward[] reward;
  }
}
