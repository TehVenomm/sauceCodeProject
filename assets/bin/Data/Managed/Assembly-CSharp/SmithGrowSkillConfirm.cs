// Decompiled with JetBrains decompiler
// Type: SmithGrowSkillConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;
using UnityEngine;

#nullable disable
public class SmithGrowSkillConfirm : GameSection
{
  private SkillItemInfo baseSkill;
  private SkillItemInfo[] material;
  private int total;
  private bool isRareConfirm;
  private bool isEquipConfirm;
  private bool isExceedConfirm;
  private bool isExceed;

  public override string overrideBackKeyEvent => "CANCEL";

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.baseSkill = eventData[0] as SkillItemInfo;
    SkillItemInfo[] skillItemInfoArray = eventData[1] as SkillItemInfo[];
    this.material = new SkillItemInfo[skillItemInfoArray.Length];
    int index1 = 0;
    for (int length = skillItemInfoArray.Length; index1 < length; ++index1)
      this.material[index1] = skillItemInfoArray[index1];
    this.total = (int) ((double) this.baseSkill.growCost * (double) this.material.Length);
    this.isEquipConfirm = false;
    this.isRareConfirm = false;
    int index2 = 0;
    for (int length = this.material.Length; index2 < length && (!this.isRareConfirm || !this.isEquipConfirm || !this.isExceedConfirm); ++index2)
    {
      if (!this.isRareConfirm && GameDefine.IsRare(this.material[index2].tableData.rarity))
        this.isRareConfirm = true;
      if (!this.isEquipConfirm && (this.material[index2].isAttached || this.material[index2].isUniqueAttached))
        this.isEquipConfirm = true;
      if (!this.isExceedConfirm && this.material[index2].IsExceeded())
        this.isExceedConfirm = true;
    }
    Array.Sort<SkillItemInfo>(this.material, (Comparison<SkillItemInfo>) ((l, r) =>
    {
      ulong num1 = (ulong) (((long) r.tableData.rarity << 61) + ((long) (uint.MaxValue - r.tableData.id) << 16 /*0x10*/)) + (ulong) r.level;
      ulong num2 = (ulong) (((long) l.tableData.rarity << 61) + ((long) (uint.MaxValue - l.tableData.id) << 16 /*0x10*/)) + (ulong) l.level;
      int num3 = (long) num1 == (long) num2 ? 0 : (num1 > num2 ? 1 : -1);
      if (num3 == 0)
      {
        num3 = r.exp == l.exp ? 0 : (r.exp > l.exp ? 1 : -1);
        if (num3 == 0)
          num3 = (long) r.uniqueID == (long) l.uniqueID ? 0 : (r.uniqueID > l.uniqueID ? 1 : -1);
      }
      return num3;
    }));
    this.isExceed = this.baseSkill.IsLevelMax();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) SmithGrowSkillConfirm.UI.STR_TITLE_R, this.sectionData.GetText("STR_TITLE"));
    this.SetLabelText((Enum) SmithGrowSkillConfirm.UI.LBL_NAME, this.baseSkill.tableData.name);
    this.SetLabelText((Enum) SmithGrowSkillConfirm.UI.LBL_MATERIAL_2, this.isExceed ? this.sectionData.GetText("TEXT_EXCEED") : this.sectionData.GetText("TEXT_GROW"));
    this.SetActive((Enum) SmithGrowSkillConfirm.UI.OBJ_MONEY, !this.isExceed);
    if (!this.isExceed)
      this.SetLabelText((Enum) SmithGrowSkillConfirm.UI.LBL_MONEY, this.total.ToString());
    this.SetGrid((Enum) SmithGrowSkillConfirm.UI.GRD_MATERIAL, (string) null, this.material.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      SkillItemInfo skillItemInfo = this.material[i];
      ItemIcon itemIcon = ItemIcon.Create(ItemIcon.GetItemIconType(skillItemInfo.tableData.type), skillItemInfo.tableData.iconID, new RARITY_TYPE?(skillItemInfo.tableData.rarity), t, magi_enable_icon_type: skillItemInfo.tableData.GetEnableEquipType(), event_name: "DETAIL", event_data: i, is_equipping: skillItemInfo.isAttached || skillItemInfo.isUniqueAttached);
      Transform ctrl = this.GetCtrl((Enum) SmithGrowSkillConfirm.UI.PNL_MATERIAL_INFO);
      this.SetMaterialInfo(itemIcon.transform, REWARD_TYPE.SKILL_ITEM, skillItemInfo.tableID, ctrl);
    }));
  }

  private void OnQuery_DECISION()
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (this.isRareConfirm || this.isEquipConfirm || this.isExceedConfirm)
    {
      stringBuilder.Append("[BB]");
      stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_CONFIRM"));
      if (this.isRareConfirm)
        stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_RARE"));
      if (this.isEquipConfirm)
        stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_EQUIP"));
      if (this.isExceedConfirm)
        stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_EXCEED"));
      stringBuilder.AppendLine("");
      stringBuilder.Append(this.sectionData.GetText("TEXT_GROW_CONFIRM"));
      GameSection.ChangeEvent("INCLUDE_RARE", (object) stringBuilder.ToString());
    }
    else
    {
      GameSection.StayEvent();
      if (this.isExceed)
        MonoBehaviourSingleton<SmithManager>.I.SendExceedSkill(this.baseSkill, this.material, (Action<SkillItemInfo, bool>) ((ret_skill_item, isGreat) =>
        {
          if (ret_skill_item != null)
          {
            GameSection.ChangeStayEvent("DECISION", (object) new object[4]
            {
              (object) new SmithManager.ResultData()
              {
                itemData = (object) ret_skill_item,
                beforeRarity = (int) this.baseSkill.tableData.rarity,
                beforeMaxLevel = this.baseSkill.tableData.GetMaxLv(ret_skill_item.exceedCnt),
                beforeExceedCnt = this.baseSkill.exceedCnt,
                beforeLevel = this.baseSkill.level,
                beforeExp = this.baseSkill.exp,
                beforeAtk = this.baseSkill.atk,
                beforeDef = this.baseSkill.def,
                beforeHp = this.baseSkill.hp,
                beforeElemAtk = this.baseSkill.elemAtk,
                beforeElemDef = this.baseSkill.elemDef
              },
              (object) isGreat,
              (object) this.material,
              (object) this.isExceed
            });
            MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
            GameSection.ResumeEvent(true);
          }
          else
            GameSection.ResumeEvent(false);
        }));
      else
        MonoBehaviourSingleton<SmithManager>.I.SendGrowSkill(this.baseSkill, this.material, (Action<SkillItemInfo, bool>) ((ret_skill_item, is_great) =>
        {
          if (ret_skill_item != null)
          {
            GameSection.ChangeStayEvent("DECISION", (object) new object[4]
            {
              (object) new SmithManager.ResultData()
              {
                itemData = (object) ret_skill_item,
                beforeRarity = (int) this.baseSkill.tableData.rarity,
                beforeMaxLevel = this.baseSkill.tableData.GetMaxLv(ret_skill_item.exceedCnt),
                beforeExceedCnt = this.baseSkill.exceedCnt,
                beforeLevel = this.baseSkill.level,
                beforeExp = this.baseSkill.exp,
                beforeAtk = this.baseSkill.atk,
                beforeDef = this.baseSkill.def,
                beforeHp = this.baseSkill.hp,
                beforeElemAtk = this.baseSkill.elemAtk,
                beforeElemDef = this.baseSkill.elemDef
              },
              (object) is_great,
              (object) this.material,
              (object) this.isExceed
            });
            MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
            GameSection.ResumeEvent(true);
          }
          else
            GameSection.ResumeEvent(false);
        }));
    }
  }

  private void OnQuery_DETAIL()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.SMITH_SKILL_MATERIAL,
      (object) this.material[(int) GameSection.GetEventData()]
    });
  }

  private void OnQuery_SmithMaterialIncludeRareConfirm_YES()
  {
    bool isRareConfirm = this.isRareConfirm;
    bool isEquipConfirm = this.isEquipConfirm;
    bool isExceedConfirm = this.isExceedConfirm;
    this.isRareConfirm = false;
    this.isEquipConfirm = false;
    this.isExceedConfirm = false;
    this.OnQuery_DECISION();
    this.isRareConfirm = isRareConfirm;
    this.isEquipConfirm = isEquipConfirm;
    this.isExceedConfirm = isExceedConfirm;
  }

  private enum UI
  {
    LBL_NAME,
    LBL_MONEY,
    GRD_MATERIAL,
    STR_TITLE_R,
    PNL_MATERIAL_INFO,
    LBL_MATERIAL_2,
    OBJ_MONEY,
  }
}
