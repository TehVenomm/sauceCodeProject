// Decompiled with JetBrains decompiler
// Type: NPCMessageTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class NPCMessageTable : Singleton<NPCMessageTable>, IDataTable
{
  private List<NPCMessageTable.Section> sections;

  public void CreateTable(string csv_text)
  {
    this.sections = new List<NPCMessageTable.Section>();
    CSVReader csvReader = new CSVReader(csv_text, "section,type,prm,priority,npcid,anim,posX,posY,posZ,rotX,rotY,rotZ,jp,voiceId");
    NPCMessageTable.Section section = (NPCMessageTable.Section) null;
    while (csvReader.NextLine())
    {
      string empty1 = string.Empty;
      NPC_MESSAGE_TYPE npcMessageType = NPC_MESSAGE_TYPE.NONE;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      float num4 = 0.0f;
      float num5 = 0.0f;
      float num6 = 0.0f;
      float num7 = 0.0f;
      float num8 = 0.0f;
      float num9 = 0.0f;
      int num10 = 0;
      csvReader.Pop(ref empty1);
      csvReader.Pop<NPC_MESSAGE_TYPE>(ref npcMessageType);
      csvReader.Pop(ref num1);
      csvReader.Pop(ref num2);
      csvReader.Pop(ref num3);
      csvReader.Pop(ref empty2);
      csvReader.Pop(ref num4);
      csvReader.Pop(ref num5);
      csvReader.Pop(ref num6);
      csvReader.Pop(ref num7);
      csvReader.Pop(ref num8);
      csvReader.Pop(ref num9);
      csvReader.Pop(ref empty3);
      csvReader.Pop(ref num10);
      if (empty1.Length > 0)
      {
        if (section != null)
          this.sections.Add(section);
        section = new NPCMessageTable.Section();
        section.name = empty1;
      }
      if (npcMessageType != NPC_MESSAGE_TYPE.NONE)
        section.messages.Add(new NPCMessageTable.Message()
        {
          type = npcMessageType,
          param = num1,
          priority = num2,
          npc = num3,
          animationStateName = empty2,
          pos = new Vector3(num4, num5, num6),
          rot = new Vector3(num7, num8, num9),
          message = empty3,
          voice_id = num10
        });
    }
    if (section != null)
      this.sections.Add(section);
    this.sections.TrimExcess();
  }

  public NPCMessageTable.Section GetSection(string section_name)
  {
    return this.sections.Find((Predicate<NPCMessageTable.Section>) (o => o.name == section_name));
  }

  public string GetNPCMessageBySectionData(GameSceneTables.SectionData sectionData)
  {
    NPCMessageTable.Section section = this.GetSection(sectionData.sectionName + "_TEXT");
    if (section == null)
      return sectionData.GetText("NPC_MESSAGE_" + (object) Random.Range(0, 3));
    return section.GetNPCMessage()?.message;
  }

  public class Message
  {
    public NPC_MESSAGE_TYPE type;
    public int param;
    public int priority;
    public int npc;
    public string animationStateName;
    public Vector3 pos;
    public Vector3 rot;
    public string message;
    public int voice_id;

    public bool has_voice => this.voice_id > 0;

    public string GetReplaceText()
    {
      string replaceText = this.message.Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
      switch (this.type)
      {
        case NPC_MESSAGE_TYPE.EVENT_QUEST:
        case NPC_MESSAGE_TYPE.ORDER_QUEST:
        case NPC_MESSAGE_TYPE.DELIVERY_QUEST:
        case NPC_MESSAGE_TYPE.NEW_NORMAL_QUEST:
        case NPC_MESSAGE_TYPE.NEW_ORDER_QUEST:
        case NPC_MESSAGE_TYPE.NEW_EVENT_QUEST:
        case NPC_MESSAGE_TYPE.NEW_DELIVERY_QUEST:
          string newValue = string.Empty;
          if (this.param != 0)
          {
            QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) this.param);
            if (questData != null)
              newValue = questData.questText;
          }
          replaceText = replaceText.Replace("{QUEST_NAME}", newValue);
          break;
        case NPC_MESSAGE_TYPE.ITEM_HAVE:
        case NPC_MESSAGE_TYPE.ITEM_NOT_HAVE:
          if (this.param != 0)
          {
            ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) this.param);
            replaceText = replaceText.Replace("{ITEM_NAME}", itemData != null ? itemData.name : string.Empty);
            break;
          }
          break;
      }
      return replaceText;
    }

    public bool IsEnable()
    {
      switch (this.type)
      {
        case NPC_MESSAGE_TYPE.NONE:
          return false;
        case NPC_MESSAGE_TYPE.ORDER_QUEST:
          if (MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() == 0)
            return false;
          bool any_have = false;
          MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory((Action<QuestItemInfo>) (quest_item =>
          {
            if (any_have || quest_item.infoData.questData.num <= 0)
              return;
            any_have = true;
          }));
          if (!any_have)
            return false;
          break;
        case NPC_MESSAGE_TYPE.DELIVERY_QUEST:
        case NPC_MESSAGE_TYPE.NEW_DELIVERY_QUEST:
          if (this.type == NPC_MESSAGE_TYPE.NEW_DELIVERY_QUEST && !GameSaveData.instance.IsRecommendedDeliveryCheck())
            return false;
          bool find_clear_status = false;
          bool find_not_clear = false;
          MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery.ForEach((Action<ClearStatusDelivery>) (data =>
          {
            if (find_clear_status)
              return;
            if (this.param != 0)
            {
              if (data.deliveryId != this.param)
                return;
              find_clear_status = true;
              if (data.deliveryStatus != 0)
                return;
              find_not_clear = true;
            }
            else
            {
              if (data.deliveryStatus != 0)
                return;
              find_clear_status = true;
              find_not_clear = true;
            }
          }));
          Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
          Delivery delivery = (Delivery) null;
          if (deliveryList.Length != 0 && this.param != 0)
            delivery = Array.Find<Delivery>(deliveryList, (Predicate<Delivery>) (data => data.dId == this.param));
          if (this.type == NPC_MESSAGE_TYPE.NEW_DELIVERY_QUEST)
          {
            if (this.param != 0 & find_clear_status || this.param == 0 && deliveryList.Length <= MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery.Count)
              return false;
            break;
          }
          if (find_clear_status)
            return find_not_clear;
          if (this.param == 0)
            return deliveryList.Length != 0;
          if (delivery == null)
            return false;
          break;
        case NPC_MESSAGE_TYPE.NEW_ORDER_QUEST:
          if (MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() == 0)
            return false;
          bool find_not_clear_quest = false;
          if (this.param == 0)
          {
            MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory((Action<QuestItemInfo>) (quest_item =>
            {
              if (find_not_clear_quest || quest_item.infoData.questData.num == 0 || MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.FindIndex((Predicate<ClearStatusQuest>) (clear_data => (long) clear_data.questId == (long) quest_item.tableID && clear_data.questStatus >= 3)) != -1)
                return;
              find_not_clear_quest = true;
            }));
            if (!find_not_clear_quest)
              return false;
            break;
          }
          if (MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem((uint) this.param) == null || MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.FindIndex((Predicate<ClearStatusQuest>) (clear_data => clear_data.questId == this.param && clear_data.questStatus >= 3)) != -1)
            return false;
          break;
        case NPC_MESSAGE_TYPE.MONEY_OVER:
        case NPC_MESSAGE_TYPE.MONEY_UNDER:
          int money = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money;
          if (this.type == NPC_MESSAGE_TYPE.MONEY_OVER && this.param >= money || this.type == NPC_MESSAGE_TYPE.MONEY_UNDER && this.param < money)
            return false;
          break;
        case NPC_MESSAGE_TYPE.CRYSTAL_OVER:
        case NPC_MESSAGE_TYPE.CRYSTAL_UNDER:
          int crystal = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
          if (this.type == NPC_MESSAGE_TYPE.CRYSTAL_OVER && this.param > crystal || this.type == NPC_MESSAGE_TYPE.CRYSTAL_UNDER && this.param < crystal)
            return false;
          break;
        case NPC_MESSAGE_TYPE.ATTACK_OVER:
        case NPC_MESSAGE_TYPE.ATTACK_UNDER:
          int _atk;
          MonoBehaviourSingleton<StatusManager>.I.CalcSelfStatusParam(MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo), out _atk, out int _, out int _);
          if (this.type == NPC_MESSAGE_TYPE.ATTACK_OVER && this.param > _atk || this.type == NPC_MESSAGE_TYPE.ATTACK_UNDER && this.param < _atk)
            return false;
          break;
        case NPC_MESSAGE_TYPE.ITEM_HAVE:
        case NPC_MESSAGE_TYPE.ITEM_NOT_HAVE:
          int haveingItemNum = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum((uint) this.param);
          if (this.type == NPC_MESSAGE_TYPE.ITEM_HAVE && haveingItemNum < 1 || this.type == NPC_MESSAGE_TYPE.ITEM_NOT_HAVE && haveingItemNum > 0)
            return false;
          break;
        case NPC_MESSAGE_TYPE.LEVEL_UP:
          int num = GameSaveData.instance.lvupMessageFlag == 1 ? 1 : 0;
          if (GameSaveData.instance.lvupMessageFlag != 0)
          {
            GameSaveData.instance.lvupMessageFlag = 0;
            GameSaveData.Save();
          }
          if (num == 0)
            return false;
          break;
        case NPC_MESSAGE_TYPE.TUTORIAL:
          if (TutorialStep.HasAllTutorialCompleted())
            return false;
          break;
      }
      return true;
    }
  }

  public class Section
  {
    public string name;
    public List<NPCMessageTable.Message> messages = new List<NPCMessageTable.Message>();

    public NPCMessageTable.Message GetNPCMessage()
    {
      NPCMessageTable.Message ret = (NPCMessageTable.Message) null;
      int select_priority = 0;
      List<NPCMessageTable.Message> priority = new List<NPCMessageTable.Message>();
      List<NPCMessageTable.Message> non_priority = new List<NPCMessageTable.Message>();
      int total = 0;
      this.messages.ForEach((Action<NPCMessageTable.Message>) (data =>
      {
        if (ret != null && data.priority >= 100)
        {
          if (this.GetSelectPriority(data.type) <= select_priority)
            return;
          ret = data;
          select_priority = this.GetSelectPriority(data.type);
        }
        else
        {
          if (!data.IsEnable())
            return;
          if (data.priority > 0)
          {
            total += data.priority;
            if (data.priority >= 100)
            {
              ret = data;
              select_priority = this.GetSelectPriority(data.type);
            }
            else
              priority.Add(data);
          }
          else
            non_priority.Add(data);
        }
      }));
      if (ret != null)
        return ret;
      int rnd = Random.Range(0, Mathf.Max(100, total));
      int index = -1;
      int cnt = 0;
      total = 0;
      priority.ForEach((Action<NPCMessageTable.Message>) (data =>
      {
        if (index > -1)
          return;
        total += data.priority;
        if (total > rnd)
          index = cnt;
        ++cnt;
      }));
      return index >= 0 ? priority[index] : non_priority[Random.Range(0, non_priority.Count)];
    }

    private int GetSelectPriority(NPC_MESSAGE_TYPE type)
    {
      if (type == NPC_MESSAGE_TYPE.DELIVERY_QUEST)
        return 1;
      return type != NPC_MESSAGE_TYPE.NEW_DELIVERY_QUEST ? 0 : 2;
    }
  }
}
