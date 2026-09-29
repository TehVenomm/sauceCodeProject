// Decompiled with JetBrains decompiler
// Type: UIInGameMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIInGameMenu : MonoBehaviourSingleton<UIInGameMenu>
{
  [SerializeField]
  protected GameObject partyMenuUI;
  [SerializeField]
  protected GameObject normalMenuUI;
  [SerializeField]
  protected GameObject happenMenuUI;
  [SerializeField]
  protected GameObject retryableMenuUI;
  [SerializeField]
  protected UILabel partyNumber;
  [SerializeField]
  protected GameObject m_missionRoot;
  [SerializeField]
  protected UILabel[] m_missionTexts;
  [SerializeField]
  protected GameObject[] m_missionCrownOn;
  [SerializeField]
  protected GameObject[] m_missionCrownOff;

  protected override void Awake()
  {
    base.Awake();
    ((Component) this).gameObject.SetActive(false);
  }

  public void Initialize()
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
    if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
    {
      this.partyMenuUI.SetActive(true);
      this.normalMenuUI.SetActive(false);
      this.happenMenuUI.SetActive(false);
      this.retryableMenuUI.SetActive(false);
      string partyNumber = MonoBehaviourSingleton<PartyManager>.I.GetPartyNumber();
      this.partyNumber.text = !string.IsNullOrEmpty(partyNumber) ? partyNumber : "-";
    }
    else if (questData != null && (questData.questType == QUEST_TYPE.HAPPEN || questData.questType == QUEST_TYPE.SERIES_ARENA))
    {
      this.partyMenuUI.SetActive(false);
      this.normalMenuUI.SetActive(false);
      this.happenMenuUI.SetActive(true);
      this.retryableMenuUI.SetActive(false);
      QuestInfoData.Mission[] missionData = QuestInfoData.CreateMissionData(questData);
      if (missionData != null)
      {
        int index = 0;
        for (int length = missionData.Length; index < length; ++index)
        {
          QuestInfoData.Mission mission = missionData[index];
          this.m_missionCrownOn[index].SetActive(CLEAR_STATUS.CLEAR == mission.state);
          this.m_missionCrownOff[index].SetActive(CLEAR_STATUS.CLEAR != mission.state);
          this.m_missionTexts[index].text = mission.tableData.missionText;
        }
      }
    }
    else if (questData != null && questData.questType == QUEST_TYPE.ARENA)
    {
      this.partyMenuUI.SetActive(false);
      this.normalMenuUI.SetActive(false);
      this.happenMenuUI.SetActive(false);
      this.retryableMenuUI.SetActive(true);
    }
    else
    {
      this.partyMenuUI.SetActive(false);
      this.normalMenuUI.SetActive(true);
      this.happenMenuUI.SetActive(false);
      this.retryableMenuUI.SetActive(false);
    }
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
    ((Component) this).gameObject.SetActive(true);
  }

  public void OnClickClose()
  {
    MonoBehaviourSingleton<InGameProgress>.I.CloseDialog();
    this.Close();
  }

  public void Close() => ((Component) this).gameObject.SetActive(false);

  public void OnClickOption()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIInGameMenu.OnClickOption", ((Component) this).gameObject, "OPTION");
  }

  public void OnClickRetire()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      return;
    if (FieldManager.IsValidInGameNoBoss())
    {
      Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
      if (Object.op_Inequality((Object) self, (Object) null) && (double) self.rescueTime > 0.0)
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", ((Component) this).gameObject, "RETIRE", (object) StringTable.Get(STRING_CATEGORY.IN_GAME, 1008U));
      else
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", ((Component) this).gameObject, "RETIRE", (object) StringTable.Get(STRING_CATEGORY.IN_GAME, 1009U));
    }
    else
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIInGameMenu.OnClickRetire", ((Component) this).gameObject, "RETIRE");
  }

  public void DoRetire()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
  }

  public void OnClickRetry()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIInGameMenu.OnClickRetry", ((Component) this).gameObject, "RETRY");
  }
}
