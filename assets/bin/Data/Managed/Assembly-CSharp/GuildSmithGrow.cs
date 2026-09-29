// Decompiled with JetBrains decompiler
// Type: GuildSmithGrow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GuildSmithGrow : EquipMaterialBase
{
  private int aimLv;
  private int terminateAimLv;
  private bool terminating;
  private int baseLv;
  private NeedMaterial[][] needMmaterialDB;
  private int[] needMoneyDB;
  private uint modelID;
  private int chooseIndex = -1;
  private bool backSection;

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE;
  }

  public override void Initialize()
  {
    this.smithType = SmithEquipBase.SmithType.GROW;
    GameSection.SetEventData((object) MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData);
    base.Initialize();
    if (this.GetEquipData() != null)
      MonoBehaviourSingleton<UIManager>.I.common.AttachCaption((UIBehaviour) this, this.sectionData.backButtonIndex, this.sectionData.GetText("CAPTION_GUILD_REQUEST"));
    this.aimLv = this.GetEquipData().level + 1;
  }

  protected override void OnOpen()
  {
    if (this.aimLv == 0)
      this.aimLv = this.GetEquipData().level + 1;
    this.NewNeedDB();
    this.terminateAimLv = -1;
    this.terminating = false;
    if (this.aimLv <= this.GetEquipData().tableData.maxLv)
    {
      EquipItemInfo equipData = this.GetEquipData();
      if (equipData != null && (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(equipData.nextNeedTableData.needMaterial) || MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < equipData.nextNeedTableData.needMoney))
      {
        this.terminateAimLv = this.aimLv;
        this.terminating = true;
      }
    }
    base.OnOpen();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    int num = Mathf.Min(this.aimLv, this.GetEquipData().tableData.maxLv);
    this.SetLabelText((Enum) EquipMaterialBase.UI.LBL_AIM_LV, num.ToString());
    this.SetActive((Enum) EquipMaterialBase.UI.STR_ONLY_EXCEED, false);
    Color color = Color.red;
    if (num == this.GetEquipData().level)
    {
      this.SetActive((Enum) EquipMaterialBase.UI.STR_ONLY_EXCEED, true);
      color = Color.gray;
    }
    else if (this.IsHavingMaterialAndMoney() && num > this.GetEquipData().level)
      color = Color.white;
    this.SetColor((Enum) EquipMaterialBase.UI.LBL_AIM_LV, color);
    bool is_enabled1 = this.aimLv > this.GetEquipData().level + 1;
    bool is_enabled2 = this.aimLv < this.GetEquipData().tableData.maxLv;
    this.SetColor((Enum) EquipMaterialBase.UI.SPR_AIM_L, is_enabled1 ? Color.white : Color.clear);
    this.SetColor((Enum) EquipMaterialBase.UI.SPR_AIM_R, is_enabled2 ? Color.white : Color.clear);
    this.SetButtonEnabled((Enum) EquipMaterialBase.UI.BTN_AIM_L, is_enabled1);
    this.SetButtonEnabled((Enum) EquipMaterialBase.UI.BTN_AIM_R, is_enabled2);
    this.SetActive((Enum) EquipMaterialBase.UI.BTN_AIM_L_INACTIVE, !is_enabled1);
    this.SetActive((Enum) EquipMaterialBase.UI.BTN_AIM_R_INACTIVE, !is_enabled2);
    this.SetRepeatButton((Enum) EquipMaterialBase.UI.BTN_AIM_L, "AIM_L");
    this.SetRepeatButton((Enum) EquipMaterialBase.UI.BTN_AIM_R, "AIM_R");
    this.SetActive((Enum) EquipMaterialBase.UI.BTN_EXCEED, false);
    this.SetActive((Enum) EquipMaterialBase.UI.BTN_DECISION, false);
    this.SetActive((Enum) EquipMaterialBase.UI.BTN_INACTIVE, false);
    this.SetActive((Enum) EquipMaterialBase.UI.LBL_GOLD, false);
    this.SetActive((Enum) EquipMaterialBase.UI.LinePartsR01, false);
  }

  protected override void InitNeedMaterialData()
  {
    if (this.GetEquipData() == null)
      return;
    if (this.aimLv <= this.GetEquipData().tableData.maxLv)
    {
      this.needMaterial = this.MaterialSort(this.GetMaterialDB(this.aimLv));
      this.needMoney = this.GetMoneyDB(this.aimLv);
    }
    else
    {
      this.needMaterial = (NeedMaterial[]) null;
      this.needMoney = 0;
    }
    this.CheckNeedMaterialNumFromInventory();
  }

  protected override string CreateItemDetailPrefabName() => "SmithGrowItem";

  protected override void EquipParam()
  {
    int num1 = Mathf.Min(this.aimLv, this.GetEquipData().tableData.maxLv);
    EquipItemInfo equipData = this.GetEquipData();
    EquipItemTable.EquipItemData tableData = equipData.tableData;
    GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(tableData.growID, (uint) num1);
    if (equipData == null || tableData == null)
      return;
    this.SetLabelText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_NAME, tableData.name);
    this.SetLabelText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_LV_MAX, tableData.maxLv.ToString());
    this.SetLabelCompareParam(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_LV_NOW, num1, equipData.level);
    this.SetEquipmentTypeIcon(this.detailBase, (Enum) EquipMaterialBase.UI.SPR_TYPE_ICON, (Enum) EquipMaterialBase.UI.SPR_TYPE_ICON_BG, (Enum) EquipMaterialBase.UI.SPR_TYPE_ICON_RARITY, equipData.tableData);
    if (growEquipItemData != null)
    {
      EquipItemExceedParamTable.EquipItemExceedParamAll itemExceedParamAll = equipData.tableData.GetExceedParam((uint) equipData.exceed) ?? new EquipItemExceedParamTable.EquipItemExceedParamAll();
      int num2 = growEquipItemData.GetGrowParamAtk((int) equipData.tableData.baseAtk) + (int) itemExceedParamAll.atk;
      int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(equipData.tableData.atkElement);
      int index1 = 0;
      for (int length = growParamElemAtk.Length; index1 < length; ++index1)
        growParamElemAtk[index1] += itemExceedParamAll.atkElement[index1];
      int num3 = Mathf.Max(growParamElemAtk);
      this.SetElementSprite(this.detailBase, (Enum) EquipMaterialBase.UI.SPR_ELEM, equipData.GetElemAtkType());
      Transform detailBase1 = this.detailBase;
      // ISSUE: variable of a boxed type
      __Boxed<EquipMaterialBase.UI> label_enum1 = (Enum) EquipMaterialBase.UI.LBL_ATK;
      int num4 = equipData.atk;
      string text1 = num4.ToString();
      this.SetLabelText(detailBase1, (Enum) label_enum1, text1);
      this.SetActive(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_ATK, num2 > equipData.atk);
      this.SetStatusBuffText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_ATK, num2 - equipData.atk, false);
      Transform detailBase2 = this.detailBase;
      // ISSUE: variable of a boxed type
      __Boxed<EquipMaterialBase.UI> label_enum2 = (Enum) EquipMaterialBase.UI.LBL_ELEM;
      num4 = equipData.elemAtk;
      string text2 = num4.ToString();
      this.SetLabelText(detailBase2, (Enum) label_enum2, text2);
      this.SetActive(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_ELEM, num3 > equipData.elemAtk);
      this.SetStatusBuffText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_ELEM, num3 - equipData.elemAtk, false);
      int num5 = growEquipItemData.GetGrowParamDef((int) equipData.tableData.baseDef) + (int) itemExceedParamAll.def;
      int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(equipData.tableData.defElement);
      int index2 = 0;
      for (int length = growParamElemDef.Length; index2 < length; ++index2)
        growParamElemDef[index2] += itemExceedParamAll.defElement[index2];
      int num6 = Mathf.Max(growParamElemDef);
      this.SetDefElementSprite(this.detailBase, (Enum) EquipMaterialBase.UI.SPR_ELEM_DEF, equipData.GetElemDefType());
      Transform detailBase3 = this.detailBase;
      // ISSUE: variable of a boxed type
      __Boxed<EquipMaterialBase.UI> label_enum3 = (Enum) EquipMaterialBase.UI.LBL_DEF;
      num4 = equipData.def;
      string text3 = num4.ToString();
      this.SetLabelText(detailBase3, (Enum) label_enum3, text3);
      this.SetActive(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_DEF, num5 > equipData.def);
      this.SetStatusBuffText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_DEF, num5 - equipData.def, false);
      int elemDef = equipData.elemDef;
      if (equipData.tableData.isFormer)
        elemDef = Mathf.FloorToInt((float) elemDef * 0.1f);
      this.SetLabelText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_ELEM_DEF, elemDef.ToString());
      this.SetActive(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_ELEM_DEF, num6 > elemDef);
      this.SetStatusBuffText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_ELEM_DEF, num6 - elemDef, false);
      int num7 = growEquipItemData.GetGrowParamHp((int) equipData.tableData.baseHp) + (int) itemExceedParamAll.hp;
      Transform detailBase4 = this.detailBase;
      // ISSUE: variable of a boxed type
      __Boxed<EquipMaterialBase.UI> label_enum4 = (Enum) EquipMaterialBase.UI.LBL_HP;
      num4 = equipData.hp;
      string text4 = num4.ToString();
      this.SetLabelText(detailBase4, (Enum) label_enum4, text4);
      this.SetActive(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_HP, num7 > equipData.hp);
      this.SetStatusBuffText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_HP, num7 - equipData.hp, false);
    }
    else
    {
      int atk = equipData.atk;
      int elemAtk = equipData.elemAtk;
      this.SetElementSprite(this.detailBase, (Enum) EquipMaterialBase.UI.SPR_ELEM, equipData.GetElemAtkType());
      this.SetLabelText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_ATK, atk.ToString());
      this.SetLabelText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_ELEM, elemAtk.ToString());
      this.SetActive(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_ATK, false);
      this.SetActive(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_ELEM, false);
      int def = equipData.def;
      int elemDef = equipData.elemDef;
      this.SetDefElementSprite(this.detailBase, (Enum) EquipMaterialBase.UI.SPR_ELEM_DEF, equipData.GetElemDefType());
      this.SetLabelText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_DEF, def.ToString());
      this.SetLabelText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_ELEM_DEF, elemDef.ToString());
      this.SetActive(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_DEF, false);
      this.SetActive(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_ELEM_DEF, false);
      this.SetLabelText(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_HP, equipData.hp.ToString());
      this.SetActive(this.detailBase, (Enum) EquipMaterialBase.UI.LBL_AFTER_HP, false);
    }
  }

  protected override void EquipImg()
  {
    uint id = this.GetEquipTableData().id;
    if ((int) this.modelID == (int) id)
      return;
    this.modelID = id;
    this.SetRenderEquipModel((Enum) EquipMaterialBase.UI.TEX_MODEL, id);
  }

  private void OnQuery_AIM_L()
  {
    if (this.aimLv - 1 == this.GetEquipData().level)
      return;
    --this.aimLv;
    if (this.aimLv < this.terminateAimLv)
    {
      this.terminateAimLv = -1;
      this.terminating = false;
    }
    this.SetDirty((Enum) EquipMaterialBase.UI.GRD_NEED_MATERIAL);
    this.RefreshUI();
  }

  private void OnQuery_AIM_R()
  {
    if (this.aimLv == this.GetEquipData().tableData.maxLv)
      return;
    ++this.aimLv;
    this.SetDirty((Enum) EquipMaterialBase.UI.GRD_NEED_MATERIAL);
    this.RefreshUI();
    if (this.IsHavingMaterialAndMoney())
      return;
    if (this.terminateAimLv < 0)
      this.terminateAimLv = this.aimLv;
    if (this.terminateAimLv != this.aimLv || this.terminating)
      return;
    this.terminating = true;
    this.TerminateRepeatButton((Enum) EquipMaterialBase.UI.BTN_AIM_R);
  }

  private void OnQuery_CLEARLEVEL()
  {
    this.baseLv = this.GetEquipData().level;
    this.aimLv = this.baseLv + 1;
    this.SetDirty((Enum) EquipMaterialBase.UI.GRD_NEED_MATERIAL);
    this.RefreshUI();
  }

  private void OnQuery_SmithConfirmGrow_YES() => this.OnQueryConfirmYES();

  protected override void Send()
  {
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    if (smithData == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      EquipItemInfo selectEquipData = smithData.selectEquipData;
      if (selectEquipData == null)
      {
        GameSection.StopEvent();
      }
      else
      {
        SmithManager.ResultData result_data = new SmithManager.ResultData();
        result_data.beforeRarity = (int) selectEquipData.tableData.rarity;
        result_data.beforeLevel = selectEquipData.level;
        result_data.beforeMaxLevel = selectEquipData.tableData.maxLv;
        result_data.beforeExceedCnt = selectEquipData.exceed;
        result_data.beforeAtk = selectEquipData.atk;
        result_data.beforeDef = selectEquipData.def;
        result_data.beforeHp = selectEquipData.hp;
        result_data.beforeElemAtk = selectEquipData.elemAtk;
        result_data.beforeElemDef = selectEquipData.elemDef;
        GameSection.SetEventData((object) result_data);
        this.isNotifySelfUpdate = true;
        GameSection.StayEvent();
        MonoBehaviourSingleton<SmithManager>.I.SendGrowEquipItem(selectEquipData.uniqueID, this.aimLv, (Action<Error, EquipItemInfo>) ((err, grow_item) =>
        {
          if (err == Error.None)
          {
            this.aimLv = grow_item.level + 1;
            result_data.itemData = (object) grow_item;
            MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
            GameSection.ResumeEvent(true);
          }
          else
          {
            this.isNotifySelfUpdate = false;
            GameSection.ResumeEvent(false);
          }
        }));
      }
    }
  }

  private void NewNeedDB()
  {
    this.baseLv = this.GetEquipData().level;
    int length = this.GetEquipData().tableData.maxLv - this.baseLv;
    this.needMmaterialDB = new NeedMaterial[length][];
    this.needMoneyDB = new int[length];
  }

  private NeedMaterial[] GetMaterialDB(int aim_lv)
  {
    int index1 = aim_lv - this.baseLv - 1;
    if (this.needMmaterialDB[index1] == null)
    {
      int num1 = index1;
      if (index1 > 0)
      {
        for (int index2 = index1; index2 >= 0 && this.needMmaterialDB[index2] == null; --index2)
        {
          if (this.needMmaterialDB[index2] == null)
            num1 = index2;
        }
      }
      for (int index3 = num1; index3 <= index1; ++index3)
      {
        int lv = this.baseLv + 1 + index3;
        GrowEquipItemTable.GrowEquipItemNeedItemData itemNeedItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemNeedUniqueItemData(this.GetEquipData().tableData.needUniqueId, (uint) lv) ?? Singleton<GrowEquipItemTable>.I.GetGrowEquipItemNeedItemData(this.GetEquipData().tableData.needId, (uint) lv);
        NeedMaterial[] needMaterial1 = itemNeedItemData.needMaterial;
        int needMoney = itemNeedItemData.needMoney;
        if (index3 > 0)
        {
          List<NeedMaterial> before_material = new List<NeedMaterial>();
          Array.ForEach<NeedMaterial>(this.needMmaterialDB[index3 - 1], (Action<NeedMaterial>) (_mat => before_material.Add(new NeedMaterial(_mat.itemID, _mat.num))));
          Array.ForEach<NeedMaterial>(needMaterial1, (Action<NeedMaterial>) (_material =>
          {
            NeedMaterial needMaterial2 = before_material.Find((Predicate<NeedMaterial>) (_data => (int) _data.itemID == (int) _material.itemID));
            if (needMaterial2 != null)
              needMaterial2.num += _material.num;
            else
              before_material.Add(new NeedMaterial(_material.itemID, _material.num));
          }));
          int num2 = needMoney + this.needMoneyDB[index3 - 1];
          this.needMmaterialDB[index3] = before_material.ToArray();
          this.needMoneyDB[index3] = num2;
        }
        else
        {
          this.needMmaterialDB[index3] = itemNeedItemData.needMaterial;
          this.needMoneyDB[index3] = itemNeedItemData.needMoney;
        }
      }
    }
    return this.needMmaterialDB[index1];
  }

  private int GetMoneyDB(int aim_lv) => this.needMoneyDB[aim_lv - this.baseLv - 1];

  private void OnQuery_SECTION_BACK()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("GuildSmithGrowItemSelect"))
      return;
    GameSection.StopEvent();
    this.OnQuery_MAIN_MENU_STATUS();
  }

  protected new void OnQuery_MATERIAL() => this.chooseIndex = (int) GameSection.GetEventData();

  private void OnCloseDialog_GuildDonateSendDialog()
  {
    string eventData = GameSection.GetEventData() as string;
    int itemId = (int) this.needMaterial[this.chooseIndex].itemID;
    string name = Singleton<ItemTable>.I.GetItemData(this.needMaterial[this.chooseIndex].itemID).name;
    try
    {
      int numRequest = int.Parse(eventData);
      if (numRequest > 0)
      {
        if (this.chooseIndex >= 0)
        {
          this.StartCoroutine(this.CRSendDonateRequest(itemId, name, "", numRequest));
          this.chooseIndex = -1;
        }
      }
    }
    catch
    {
    }
    this.chooseIndex = -1;
  }

  private IEnumerator CRSendDonateRequest(
    int itemID,
    string itemName,
    string request,
    int numRequest)
  {
    yield return (object) new WaitUntil((Func<bool>) (() => !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible()));
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendDonateRequest(itemID, itemName, request, numRequest, (Action<bool>) (success =>
    {
      GameSection.ResumeEvent(success);
      if (!success)
        return;
      this.backSection = true;
    }));
  }

  private void Update()
  {
    if (!this.backSection || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      return;
    this.backSection = false;
    if (LoungeMatchingManager.IsValidInLounge())
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Lounge", "GuildDonateMaterialSelectDialog");
    else
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Home", "GuildDonateMaterialSelectDialog");
  }
}
