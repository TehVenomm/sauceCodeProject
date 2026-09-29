// Decompiled with JetBrains decompiler
// Type: GachaResultBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public abstract class GachaResultBase : GameSection
{
  protected bool isRetry;
  protected int userCrystalNum;
  protected GameObject buttonObj;
  protected Transform footerRoot;
  protected GachaGuaranteeCampaignInfo currentGachaGuarantee;
  protected GachaGuaranteeCampaignInfo nextGachaGuarantee;
  protected bool isExistDetailButton;

  public override void Initialize()
  {
    if (this.nextGachaGuarantee != null && this.nextGachaGuarantee.IsValid())
      MonoBehaviourSingleton<GachaManager>.I.selectGacha.SetCrystalNum(this.nextGachaGuarantee.crystalNum);
    this.userCrystalNum = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
    base.Initialize();
  }

  protected virtual IEnumerator LoadMultiResultUI(LoadingQueue loadQueue)
  {
    this.SetActive((Enum) GachaResultBase.UI.FOOTER_ROOT, false);
    this.SetActive((Enum) GachaResultBase.UI.FOOTER_GUARANTEE_ROOT, false);
    this.SetActive((Enum) GachaResultBase.UI.FOOTER_MULTI_RESULT_ROOT, true);
    this.footerRoot = this.GetCtrl((Enum) GachaResultBase.UI.FOOTER_MULTI_RESULT_ROOT);
    string buttonName = this.CreateButtonName();
    yield return (object) this.LoadGachaButton(loadQueue, this.FindCtrl(this.footerRoot, (Enum) GachaResultBase.UI.BTN_NEXT), buttonName);
    this.SetEvent(this.FindCtrl(this.footerRoot, (Enum) GachaResultBase.UI.BTN_NEXT).GetChild(0), "NEXT_PERFORMANCE", -1);
    yield return (object) this.LoadGachaGuaranteeCounter(loadQueue, this.nextGachaGuarantee, (Action<LoadObject>) (lo_guarantee => this.SetTexture(this.footerRoot, (Enum) GachaResultBase.UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture)));
  }

  protected string CreateButtonName()
  {
    return MonoBehaviourSingleton<GachaManager>.I.CreateButtonBaseName(MonoBehaviourSingleton<GachaManager>.I.selectGacha, this.nextGachaGuarantee, true) + "_RESULT";
  }

  protected IEnumerator LoadGachaButton(
    LoadingQueue loadQueue,
    Transform parent,
    string buttonName)
  {
    LoadObject lo_button = loadQueue.Load(RESOURCE_CATEGORY.GACHA_BUTTON, buttonName);
    if (loadQueue.IsLoading())
      yield return (object) loadQueue.Wait();
    this.buttonObj = Object.Instantiate(lo_button.loadedObject) as GameObject;
    this.buttonObj.transform.parent = parent;
    ((Object) this.buttonObj.transform).name = ((Object) parent).name;
    this.buttonObj.transform.localScale = new Vector3(1f, 1f, 1f);
    this.buttonObj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
  }

  protected IEnumerator LoadGachaGuaranteeCounter(
    LoadingQueue loadQueue,
    GachaGuaranteeCampaignInfo guarantee,
    Action<LoadObject> callback)
  {
    string resource_name = "";
    if (guarantee.IsValid())
      resource_name = guarantee.GetTitleImageName();
    GachaResult currentGachaResult = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult();
    if (currentGachaResult.detailButtonImg == null)
      currentGachaResult.detailButtonImg = "";
    if (currentGachaResult.detailButtonImg != "")
      resource_name = currentGachaResult.detailButtonImg;
    if (resource_name != "")
    {
      this.isExistDetailButton = true;
      LoadObject loadObject = loadQueue.Load(RESOURCE_CATEGORY.GACHA_GUARANTEE_COUNTER, resource_name);
      if (loadQueue.IsLoading())
        yield return (object) loadQueue.Wait();
      callback(loadObject);
      loadObject = (LoadObject) null;
    }
  }

  protected override void OnQuery_GachaConfirm_YES()
  {
    this.isRetry = true;
    base.OnQuery_GachaConfirm_YES();
  }

  protected void CheckUpdateCrystalNum()
  {
    if (this.userCrystalNum < MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal)
      this.isRetry = false;
    this.userCrystalNum = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
  }

  protected void OnQuery_GACHA()
  {
    if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.IsEnd)
    {
      GameSection.ChangeEvent("END");
      this.SetGachaButtonActive(false);
    }
    else
    {
      string str;
      if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0)
      {
        int ticketId = MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId;
        int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) ticketId), 1);
        ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) ticketId);
        str = $"{itemData.name} {(object) MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum}{StringTable.Get(STRING_CATEGORY.COMMON, 4000U)}\n";
        if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum > itemNum)
        {
          GameSection.ChangeEvent("NOT_ENOUGH_GACHA_TICKET", (object) new object[2]
          {
            (object) itemData.name,
            (object) ((MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum - itemNum).ToString() + StringTable.Get(STRING_CATEGORY.COMMON, 4000U))
          });
          return;
        }
      }
      else
        str = $"{StringTable.Get(STRING_CATEGORY.COMMON, 100U)} {(object) this.GetCrystalNum()}{StringTable.Get(STRING_CATEGORY.COMMON, 3000U)}";
      GameSection.SetEventData((object) new object[1]
      {
        (object) str
      });
    }
  }

  public void OnQuery_NEXT_PERFORMANCE()
  {
    if (!MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult())
      return;
    if (MonoBehaviourSingleton<GachaManager>.I.GetNextGachaResult().reward[0].rewardType == 6)
      GameSection.ChangeEvent("NEXT_PERFORMANCE_QUEST");
    else
      GameSection.ChangeEvent("NEXT_PERFORMANCE_SKILL");
    MonoBehaviourSingleton<GachaManager>.I.IncrementGachaIndex();
  }

  public void OnQuery_FEVER_PERFORMANCE()
  {
    if (!MonoBehaviourSingleton<GachaManager>.I.IsResultBonus())
      return;
    if (MonoBehaviourSingleton<GachaManager>.I.gachaResultBonus.reward[0].rewardType == 6)
      GameSection.ChangeEvent("NEXT_PERFORMANCE_QUEST");
    else
      GameSection.ChangeEvent("NEXT_PERFORMANCE_SKILL");
    MonoBehaviourSingleton<GachaManager>.I.SetNextFever();
  }

  public int GetCrystalNum()
  {
    return this.nextGachaGuarantee == null || !this.nextGachaGuarantee.IsValid() || !this.nextGachaGuarantee.hasFreeGachaReward ? MonoBehaviourSingleton<GachaManager>.I.selectGacha.crystalNum : 0;
  }

  protected bool IsEnableEntry()
  {
    return MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult() || !MonoBehaviourSingleton<GachaManager>.I.IsSelectTutorialGacha() && MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().remainCount != 0;
  }

  protected void SetGachaButtonActive(bool enableRetry)
  {
    Transform ctrl = this.FindCtrl(this.buttonObj.transform, (Enum) (GachaResultBase.UI) (enableRetry ? 61 : 62));
    this.SetActive(this.buttonObj.transform, (Enum) GachaResultBase.UI.OBJ_GACHA_ENABLE_ROOT, enableRetry);
    this.SetActive(this.buttonObj.transform, (Enum) GachaResultBase.UI.OBJ_GACHA_DISABLE_ROOT, !enableRetry);
    int num = MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0 ? MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum : this.GetCrystalNum();
    this.SetLabelText(ctrl, (Enum) GachaResultBase.UI.LBL_PRICE, num.ToString());
    if (enableRetry)
      return;
    this.SetButtonEnabled(this.buttonObj.transform, false);
  }

  private enum UI
  {
    TEX_MODEL,
    TEX_INNER_MODEL,
    LBL_NAME,
    LBL_CRYSTAL_NUM,
    TEX_TICKET,
    TEX_TICKET_HAVE,
    SPR_CRYSTAL,
    LBL_PRICE,
    OBJ_DIFFICULTY_ROOT,
    TEX_GUARANTEE_COUNT_DOWN,
    OBJ_RARITY_ROOT,
    OBJ_RARITY_D,
    OBJ_RARITY_C,
    OBJ_RARITY_B,
    OBJ_RARITY_A,
    OBJ_RARITY_S,
    OBJ_RARITY_SS,
    OBJ_RARITY_SSS,
    OBJ_RARITY_LIGHT,
    OBJ_RARITY_TEXT_ROOT,
    OBJ_SINGLE_ROOT,
    OBJ_MULTI_ROOT,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_DESCRIPTION,
    OBJ_ICONS_ROOT,
    OBJ_ICON_ROOT_0,
    OBJ_ICON_ROOT_1,
    OBJ_ICON_ROOT_2,
    OBJ_ICON_ROOT_3,
    OBJ_ICON_ROOT_4,
    OBJ_ICON_ROOT_5,
    OBJ_ICON_ROOT_6,
    OBJ_ICON_ROOT_7,
    OBJ_ICON_ROOT_8,
    OBJ_ICON_ROOT_9,
    OBJ_ICON_ROOT_10,
    LBL_ENEMY_LV_0,
    LBL_ENEMY_LV_1,
    LBL_ENEMY_LV_2,
    LBL_ENEMY_LV_3,
    LBL_ENEMY_LV_4,
    LBL_ENEMY_LV_5,
    LBL_ENEMY_LV_6,
    LBL_ENEMY_LV_7,
    LBL_ENEMY_LV_8,
    LBL_ENEMY_LV_9,
    LBL_ENEMY_LV_10,
    LBL_MAGI_NAME_0,
    LBL_MAGI_NAME_1,
    LBL_MAGI_NAME_2,
    LBL_MAGI_NAME_3,
    LBL_MAGI_NAME_4,
    LBL_MAGI_NAME_5,
    LBL_MAGI_NAME_6,
    LBL_MAGI_NAME_7,
    LBL_MAGI_NAME_8,
    LBL_MAGI_NAME_9,
    LBL_MAGI_NAME_10,
    BTN_GACHA,
    OBJ_GACHA_ENABLE_ROOT,
    OBJ_GACHA_DISABLE_ROOT,
    OBJ_BG_SINGLE,
    OBJ_BG_MULTI,
    SPR_LINE_TOP,
    SPR_LINE_BOTTOM,
    FOOTER_ROOT,
    FOOTER_GUARANTEE_ROOT,
    FOOTER_MULTI_RESULT_ROOT,
    OBJ_GUARANTEE,
    BG_MULTI,
    BTN_NEXT,
    BTN_BACK,
    BTN_BATTLE,
    BTN_EQUIP,
  }
}
