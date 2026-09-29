// Decompiled with JetBrains decompiler
// Type: TutorialMessageTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TutorialMessageTable : Singleton<TutorialMessageTable>, IDataTable
{
  private TutorialReadData m_ReadData;
  private const string NT = "id,scene_name,section_name,message_id,trigger_event_name,completed_tutorial_step,appear_tutorial_bit,finish_tutorial_bit,appear,appear_delivery_id,only_new_section,set_bit,force_resend,check_keyword,position,npc_id,face_type,message,image_resource_name,voice_id,cursor_target,focus_pattern,wait_event_name,focus_frame,wait,cursor_offset_x,cursor_offset_y,cursor_angle,cursor_delay";
  private UIntKeyTable<TutorialMessageTable.TutorialMessageData> tutorialSectionMessages = new UIntKeyTable<TutorialMessageTable.TutorialMessageData>();

  public TutorialReadData ReadData
  {
    get
    {
      if (this.m_ReadData == null)
      {
        TutorialStep.isChangeLocalEquip = false;
        TutorialStep.isSendFirstRewardComplete = false;
        this.m_ReadData = TutorialReadData.CreateAndLoad();
      }
      return this.m_ReadData;
    }
  }

  public bool HasSection(string section_name)
  {
    bool is_find = false;
    this.tutorialSectionMessages.ForEach((Action<TutorialMessageTable.TutorialMessageData>) (data =>
    {
      if (is_find || !(data.sectionName == section_name))
        return;
      is_find = true;
    }));
    return is_find;
  }

  public static bool HasReadTutorialEnd() => TutorialStep.HasAllTutorialCompleted();

  public static void SendTutorialBit(TUTORIAL_MENU_BIT bit, Action<bool> callback = null)
  {
    Protocol.Force((System.Action) (() => MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialBit(bit, callback)));
  }

  public int[] GetTutorialIds()
  {
    List<int> ret = new List<int>();
    this.tutorialSectionMessages.ForEach((Action<TutorialMessageTable.TutorialMessageData>) (data => ret.Add(data.tutorialId)));
    return ret.ToArray();
  }

  public TutorialMessageTable.TutorialMessageData GetTutorialTheSection(
    string section_name,
    int message_id)
  {
    TutorialMessageTable.TutorialMessageData ret = (TutorialMessageTable.TutorialMessageData) null;
    this.tutorialSectionMessages.ForEach((Action<TutorialMessageTable.TutorialMessageData>) (o =>
    {
      if (ret != null || !(o.sectionName == section_name) || o.messageId != message_id)
        return;
      ret = o;
    }));
    return ret;
  }

  public TutorialMessageTable.TutorialMessageData GetEnableExecTutorial(
    string section_name,
    bool is_force,
    bool is_new_section,
    string event_name = null)
  {
    TutorialReadData save_data = Singleton<TutorialMessageTable>.I.ReadData;
    List<TutorialMessageTable.TutorialMessageData> list = new List<TutorialMessageTable.TutorialMessageData>();
    this.tutorialSectionMessages.ForEach((Action<TutorialMessageTable.TutorialMessageData>) (o =>
    {
      if (o.sectionName != section_name)
        return;
      if (!TutorialStep.HasAllTutorialCompleted() && o.completedTutorialStep != -1)
      {
        if (o.completedTutorialStep == 0 || MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep >= o.completedTutorialStep || o.sceneName == "StatusScene" && o.sectionName == "StatusTop" && o.messageId == 1 && TutorialStep.isChangeLocalEquip)
          return;
      }
      else if (o.completedTutorialStep != 0 && o.completedTutorialStep != -1 || !is_force && !o.GetFinishTutorialBit().HasValue && o.completedTutorialStep >= 0 && save_data.HasRead(o.tutorialId) || o.appearId > 0 && !save_data.HasRead(o.appearId) || o.appearId < 0 && save_data.LastRead() != -o.appearId)
        return;
      if (o.GetFinishTutorialBit().HasValue && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(o.GetFinishTutorialBit().Value) || o.GetAppearTutorialBit().HasValue && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(o.GetAppearTutorialBit().Value) || o.appearDeliveryId != 0 && (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery((uint) o.appearDeliveryId)) || o.isNewSectionOnly && !is_new_section)
        return;
      if (!string.IsNullOrEmpty(event_name))
      {
        if (o.triggerEventName != event_name)
          return;
      }
      else if (!string.IsNullOrEmpty(o.triggerEventName))
        return;
      if ((o.sectionName == "WorldMap" || o.sectionName == "RegionMap") && string.IsNullOrEmpty(event_name) && MonoBehaviourSingleton<WorldMapManager>.IsValid() && (MonoBehaviourSingleton<WorldMapManager>.I.isDisplayQuestTargetMode() || MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial))
        return;
      if (o.sectionName == "EquipSetDetailAttachSkillDialog" && !string.IsNullOrEmpty(o.checkKeyword))
      {
        uint target_id = uint.Parse(o.checkKeyword);
        bool find_non_equip_attack_skill = false;
        MonoBehaviourSingleton<InventoryManager>.I.ForAllSkillItemInventory((Action<SkillItemInfo>) (data =>
        {
          if (find_non_equip_attack_skill || data == null || data.tableData.type != SKILL_SLOT_TYPE.ATTACK || data.isAttached || (int) target_id == (int) data.tableID)
            return;
          find_non_equip_attack_skill = true;
        }));
        if (!find_non_equip_attack_skill)
          return;
        bool is_equip_first_slot = false;
        if (MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet() == null)
          MonoBehaviourSingleton<StatusManager>.I.CreateLocalEquipSetData();
        int eSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
        EquipItemInfo main_weapon = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet()[eSetNo].item[0];
        if (main_weapon != null)
          MonoBehaviourSingleton<InventoryManager>.I.ForAllSkillItemInventory((Action<SkillItemInfo>) (data =>
          {
            if (is_equip_first_slot || data == null)
              return;
            EquipSetSkillData equipSetSkillData = data.equipSetSkill.Find((Predicate<EquipSetSkillData>) (x => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo()));
            if (equipSetSkillData == null || (long) equipSetSkillData.equipItemUniqId != (long) main_weapon.uniqueID || equipSetSkillData.equipSlotNo != 0)
              return;
            is_equip_first_slot = true;
          }));
        if (!is_equip_first_slot)
        {
          int index = o.messageData.Count - 1;
          o.messageData[index].waitEventName = o.messageData[index].waitEventName.Replace("_DETAIL", "");
          Debug.LogWarning((object) ("replace : " + o.messageData[index].waitEventName));
        }
      }
      list.Add(o);
    }));
    if (list.Count == 0)
      return (TutorialMessageTable.TutorialMessageData) null;
    list.Sort((Comparison<TutorialMessageTable.TutorialMessageData>) ((l, r) => l.messageId - r.messageId));
    return list[0];
  }

  public void CreateTable(string csv_text)
  {
    CSVReader csvReader = new CSVReader(csv_text, "id,scene_name,section_name,message_id,trigger_event_name,completed_tutorial_step,appear_tutorial_bit,finish_tutorial_bit,appear,appear_delivery_id,only_new_section,set_bit,force_resend,check_keyword,position,npc_id,face_type,message,image_resource_name,voice_id,cursor_target,focus_pattern,wait_event_name,focus_frame,wait,cursor_offset_x,cursor_offset_y,cursor_angle,cursor_delay");
    TutorialMessageTable.TutorialMessageData tutorialMessageData = (TutorialMessageTable.TutorialMessageData) null;
    while (csvReader.NextLine())
    {
      uint num1 = 0;
      csvReader.Pop(ref num1);
      if (num1 != 0U)
      {
        if (tutorialMessageData != null)
          this.tutorialSectionMessages.Add((uint) tutorialMessageData.tutorialId, tutorialMessageData);
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        int num2 = 0;
        string empty4 = string.Empty;
        string empty5 = string.Empty;
        int num3 = 0;
        int num4 = 0;
        int message_id = 0;
        bool flag = false;
        string empty6 = string.Empty;
        FORCE_RESEND_DIALOG_FLAG resendDialogFlag = FORCE_RESEND_DIALOG_FLAG.NONE;
        string empty7 = string.Empty;
        csvReader.Pop(ref empty1);
        csvReader.Pop(ref empty2);
        csvReader.Pop(ref message_id);
        csvReader.Pop(ref empty3);
        csvReader.Pop(ref num2);
        csvReader.Pop(ref empty4);
        csvReader.Pop(ref empty5);
        csvReader.Pop(ref num3);
        csvReader.Pop(ref num4);
        csvReader.Pop(ref flag);
        csvReader.Pop(ref empty6);
        csvReader.Pop<FORCE_RESEND_DIALOG_FLAG>(ref resendDialogFlag);
        csvReader.Pop(ref empty7);
        if (this.GetTutorialTheSection(empty2, message_id) != null)
        {
          Log.Warning(LOG.SYSTEM, $"同一セクション内で message_id が重複しています : id {(object) num1} : {empty2} - {(object) message_id}");
          continue;
        }
        tutorialMessageData = new TutorialMessageTable.TutorialMessageData();
        tutorialMessageData.tutorialId = (int) num1;
        tutorialMessageData.sceneName = empty1;
        tutorialMessageData.sectionName = empty2;
        tutorialMessageData.messageId = message_id;
        tutorialMessageData.triggerEventName = empty3;
        tutorialMessageData.completedTutorialStep = num2;
        tutorialMessageData.strAppearTutorialBit = empty4;
        tutorialMessageData.strFinishTutorialBit = empty5;
        tutorialMessageData.appearId = num3;
        tutorialMessageData.appearDeliveryId = num4;
        tutorialMessageData.isNewSectionOnly = flag;
        tutorialMessageData.strSetBit = empty6;
        tutorialMessageData.resendFrag = resendDialogFlag;
        tutorialMessageData.checkKeyword = empty7;
        tutorialMessageData.messageData = new List<TutorialMessageTable.TutorialMessageData.MessageData>();
      }
      else
      {
        string empty = string.Empty;
        int num5 = 0;
        bool flag = false;
        csvReader.Pop(ref empty);
        csvReader.Pop(ref empty);
        csvReader.Pop(ref num5);
        csvReader.Pop(ref empty);
        csvReader.Pop(ref num5);
        csvReader.Pop(ref empty);
        csvReader.Pop(ref empty);
        csvReader.Pop(ref num5);
        csvReader.Pop(ref num5);
        csvReader.Pop(ref flag);
        csvReader.Pop(ref empty);
        csvReader.Pop(ref empty);
        csvReader.Pop(ref empty);
      }
      if (tutorialMessageData != null && tutorialMessageData.messageData != null)
      {
        TutorialMessageTable.TutorialMessageData.MessageData messageData = new TutorialMessageTable.TutorialMessageData.MessageData();
        csvReader.Pop(ref messageData.positionId);
        csvReader.Pop(ref messageData.npcId);
        csvReader.Pop(ref messageData.faceType);
        csvReader.Pop(ref messageData.message);
        csvReader.Pop(ref messageData.imageResourceName);
        csvReader.Pop(ref messageData.voiceId);
        csvReader.Pop(ref messageData.cursorTarget);
        csvReader.Pop<FOCUS_PATTERN>(ref messageData.focusPattern);
        csvReader.Pop(ref messageData.waitEventName);
        csvReader.Pop(ref messageData.focusFrame);
        csvReader.Pop(ref messageData.wait);
        if (!csvReader.IsEmpty())
        {
          messageData.cursorType = TutorialMessageTable.TutorialMessageData.MessageData.CursorType.MANUAL;
          csvReader.Pop(ref messageData.cursorOffset);
        }
        else
        {
          csvReader.NextValue();
          csvReader.NextValue();
        }
        if (!csvReader.IsEmpty())
          messageData.cursorType = TutorialMessageTable.TutorialMessageData.MessageData.CursorType.MANUAL;
        csvReader.Pop(ref messageData.cursorRotDeg);
        csvReader.Pop(ref messageData.cursorDelay);
        tutorialMessageData.messageData.Add(messageData);
      }
    }
    if (tutorialMessageData == null)
      return;
    this.tutorialSectionMessages.Add((uint) tutorialMessageData.tutorialId, tutorialMessageData);
  }

  public class TutorialMessageData
  {
    public int tutorialId;
    public int messageId;
    public string sceneName = string.Empty;
    public string sectionName = string.Empty;
    public string triggerEventName = string.Empty;
    public int completedTutorialStep;
    public string strAppearTutorialBit = string.Empty;
    public string strFinishTutorialBit = string.Empty;
    public int appearId;
    public int appearDeliveryId;
    public bool isNewSectionOnly;
    public string strSetBit = string.Empty;
    public FORCE_RESEND_DIALOG_FLAG resendFrag;
    public string checkKeyword = string.Empty;
    public List<TutorialMessageTable.TutorialMessageData.MessageData> messageData;

    public TUTORIAL_MENU_BIT? GetAppearTutorialBit()
    {
      return string.IsNullOrEmpty(this.strAppearTutorialBit) ? new TUTORIAL_MENU_BIT?() : new TUTORIAL_MENU_BIT?((TUTORIAL_MENU_BIT) Enum.Parse(typeof (TUTORIAL_MENU_BIT), this.strAppearTutorialBit));
    }

    public TUTORIAL_MENU_BIT? GetFinishTutorialBit()
    {
      return string.IsNullOrEmpty(this.strFinishTutorialBit) ? new TUTORIAL_MENU_BIT?() : new TUTORIAL_MENU_BIT?((TUTORIAL_MENU_BIT) Enum.Parse(typeof (TUTORIAL_MENU_BIT), this.strFinishTutorialBit));
    }

    public TUTORIAL_MENU_BIT? GetSetBit()
    {
      return string.IsNullOrEmpty(this.strSetBit) ? new TUTORIAL_MENU_BIT?() : new TUTORIAL_MENU_BIT?((TUTORIAL_MENU_BIT) Enum.Parse(typeof (TUTORIAL_MENU_BIT), this.strSetBit));
    }

    public class MessageData
    {
      public int positionId;
      public int npcId;
      public int faceType;
      public string message = string.Empty;
      public string imageResourceName = string.Empty;
      public int voiceId;
      public string cursorTarget = string.Empty;
      public TutorialMessageTable.TutorialMessageData.MessageData.CursorType cursorType;
      public Vector2 cursorOffset = Vector2.zero;
      public int cursorRotDeg;
      public float cursorDelay = -1f;
      public FOCUS_PATTERN focusPattern;
      public string waitEventName = string.Empty;
      public string focusFrame;
      public float wait;

      public TutorialMessageTable.TutorialMessageData.MessageData.Position position_type
      {
        get => (TutorialMessageTable.TutorialMessageData.MessageData.Position) this.positionId;
      }

      public bool is_smile => this.faceType == 1;

      public bool has_voice => this.voiceId > 0;

      public bool has_target => !string.IsNullOrEmpty(this.cursorTarget);

      public bool is_wait_event => !string.IsNullOrEmpty(this.waitEventName);

      public enum Position
      {
        UP,
        DOWN,
        CENTER,
      }

      public enum CursorType
      {
        AUTO,
        MANUAL,
      }
    }
  }
}
