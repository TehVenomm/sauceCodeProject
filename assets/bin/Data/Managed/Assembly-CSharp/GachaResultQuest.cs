// Decompiled with JetBrains decompiler
// Type: GachaResultQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GachaResultQuest : GachaResultBase
{
  private GachaResultQuest.UI[] iconRootAry = new GachaResultQuest.UI[11]
  {
    GachaResultQuest.UI.OBJ_ICON_ROOT_0,
    GachaResultQuest.UI.OBJ_ICON_ROOT_1,
    GachaResultQuest.UI.OBJ_ICON_ROOT_2,
    GachaResultQuest.UI.OBJ_ICON_ROOT_3,
    GachaResultQuest.UI.OBJ_ICON_ROOT_4,
    GachaResultQuest.UI.OBJ_ICON_ROOT_5,
    GachaResultQuest.UI.OBJ_ICON_ROOT_6,
    GachaResultQuest.UI.OBJ_ICON_ROOT_7,
    GachaResultQuest.UI.OBJ_ICON_ROOT_8,
    GachaResultQuest.UI.OBJ_ICON_ROOT_9,
    GachaResultQuest.UI.OBJ_ICON_ROOT_10
  };
  private GachaResultQuest.UI[] iconLevelAry = new GachaResultQuest.UI[11]
  {
    GachaResultQuest.UI.LBL_ENEMY_LV_0,
    GachaResultQuest.UI.LBL_ENEMY_LV_1,
    GachaResultQuest.UI.LBL_ENEMY_LV_2,
    GachaResultQuest.UI.LBL_ENEMY_LV_3,
    GachaResultQuest.UI.LBL_ENEMY_LV_4,
    GachaResultQuest.UI.LBL_ENEMY_LV_5,
    GachaResultQuest.UI.LBL_ENEMY_LV_6,
    GachaResultQuest.UI.LBL_ENEMY_LV_7,
    GachaResultQuest.UI.LBL_ENEMY_LV_8,
    GachaResultQuest.UI.LBL_ENEMY_LV_9,
    GachaResultQuest.UI.LBL_ENEMY_LV_10
  };
  private GachaResultQuest.UI[] rarityAnimRoot = new GachaResultQuest.UI[7]
  {
    GachaResultQuest.UI.OBJ_RARITY_D,
    GachaResultQuest.UI.OBJ_RARITY_C,
    GachaResultQuest.UI.OBJ_RARITY_B,
    GachaResultQuest.UI.OBJ_RARITY_A,
    GachaResultQuest.UI.OBJ_RARITY_S,
    GachaResultQuest.UI.OBJ_RARITY_SS,
    GachaResultQuest.UI.OBJ_RARITY_SSS
  };
  private bool isJumpToBattle;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    this.currentGachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee;
    this.nextGachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().gachaGuaranteeCampaignInfo;
    MonoBehaviourSingleton<GachaManager>.I.SetSelectGachaGuarantee(this.nextGachaGuarantee);
    LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
    if (MonoBehaviourSingleton<GachaManager>.I.IsResultBonus() && !MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult())
      yield return (object) this.LoadFeverResultUI(loadQueue);
    else if (MonoBehaviourSingleton<GachaManager>.I.IsMultiResult())
      yield return (object) this.LoadMultiResultUI(loadQueue);
    else
      yield return (object) this.LoadNormalUI(loadQueue);
    base.Initialize();
  }

  protected IEnumerator LoadFeverResultUI(LoadingQueue loadQueue)
  {
    this.SetActive((Enum) GachaResultQuest.UI.FOOTER_ROOT, false);
    if (MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector)
    {
      this.SetActive((Enum) GachaResultQuest.UI.FOOTER_GUARANTEE_ROOT, true);
      this.SetActive((Enum) GachaResultQuest.UI.FOOTER_MULTI_RESULT_ROOT, false);
      this.footerRoot = this.GetCtrl((Enum) GachaResultQuest.UI.FOOTER_GUARANTEE_ROOT);
      string buttonName = this.CreateButtonName();
      yield return (object) this.LoadGachaButton(loadQueue, this.FindCtrl(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_GACHA), buttonName);
      yield return (object) this.LoadGachaGuaranteeCounter(loadQueue, this.nextGachaGuarantee, (Action<LoadObject>) (lo_guarantee => this.SetTexture(this.footerRoot, (Enum) GachaResultQuest.UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture)));
    }
    else
    {
      this.SetActive((Enum) GachaResultQuest.UI.FOOTER_GUARANTEE_ROOT, false);
      this.SetActive((Enum) GachaResultQuest.UI.FOOTER_MULTI_RESULT_ROOT, true);
      this.footerRoot = this.GetCtrl((Enum) GachaResultQuest.UI.FOOTER_MULTI_RESULT_ROOT);
      yield return (object) this.LoadFeverGachaGuaranteeCounter(loadQueue, this.currentGachaGuarantee, (Action<LoadObject>) (lo_guarantee => this.SetTexture(this.footerRoot, (Enum) GachaResultQuest.UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture)));
      yield return (object) this.LoadGachaButton(loadQueue, this.FindCtrl(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_NEXT), "BTN_GACHA_FEVER_NEXT");
    }
  }

  protected IEnumerator LoadFeverGachaGuaranteeCounter(
    LoadingQueue loadQueue,
    GachaGuaranteeCampaignInfo guarantee,
    Action<LoadObject> callback)
  {
    string resource_name = "";
    if (guarantee.IsValid())
      resource_name = guarantee.GetTitleImageName() + "_FEVER";
    if (resource_name != "")
    {
      LoadObject loadObject = loadQueue.Load(RESOURCE_CATEGORY.GACHA_GUARANTEE_COUNTER, resource_name);
      if (loadQueue.IsLoading())
        yield return (object) loadQueue.Wait();
      callback(loadObject);
      loadObject = (LoadObject) null;
    }
  }

  protected override IEnumerator LoadMultiResultUI(LoadingQueue loadQueue)
  {
    yield return (object) base.LoadMultiResultUI(loadQueue);
    ((Component) this.GetCtrl((Enum) GachaResultQuest.UI.BG_MULTI)).GetComponent<UISprite>().height = 750;
  }

  private IEnumerator LoadNormalUI(LoadingQueue loadQueue)
  {
    if (this.nextGachaGuarantee.IsValid())
      this.footerRoot = this.GetCtrl((Enum) GachaResultQuest.UI.FOOTER_GUARANTEE_ROOT);
    else
      this.footerRoot = this.GetCtrl((Enum) GachaResultQuest.UI.FOOTER_ROOT);
    string buttonName = this.CreateButtonName();
    yield return (object) this.LoadGachaButton(loadQueue, this.FindCtrl(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_GACHA), buttonName);
    yield return (object) this.LoadGachaGuaranteeCounter(loadQueue, this.nextGachaGuarantee, (Action<LoadObject>) (lo_guarantee => this.SetTexture(this.footerRoot, (Enum) GachaResultQuest.UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture)));
  }

  public override void UpdateUI()
  {
    bool is_visible = MonoBehaviourSingleton<GachaManager>.I.selectGacha.num == 1;
    this.SetActive((Enum) GachaResultQuest.UI.OBJ_SINGLE_ROOT, is_visible);
    this.SetActive((Enum) GachaResultQuest.UI.OBJ_MULTI_ROOT, !is_visible);
    this.SetActive((Enum) GachaResultQuest.UI.OBJ_BG_SINGLE, is_visible);
    this.SetActive((Enum) GachaResultQuest.UI.OBJ_BG_MULTI, !is_visible);
    if (is_visible)
      this.UpdateSingleGachaUI();
    else
      this.UpdateMultiGachaUI();
    if (!MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult() && MonoBehaviourSingleton<GachaManager>.I.IsResultBonus())
      this.UpdateFeverResultFooterUI();
    else if (MonoBehaviourSingleton<GachaManager>.I.IsMultiResult())
      this.UpdateMultiResultFooterUI();
    else
      this.UpdateSingleResultFooterUI();
  }

  protected void UpdateSingleGachaUI()
  {
    string text = string.Empty;
    int star_num = 0;
    GachaResult.GachaReward gachaReward = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward[0];
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) gachaReward.itemId);
    if (questData != null)
    {
      text = questData.questText;
      star_num = (int) questData.difficulty;
    }
    this.SetLabelText((Enum) GachaResultQuest.UI.LBL_NAME, text);
    RARITY_TYPE[] values = (RARITY_TYPE[]) Enum.GetValues(typeof (RARITY_TYPE));
    int index = 0;
    for (int length = values.Length; index < length; ++index)
      this.SetActive((Enum) this.rarityAnimRoot[index], questData.rarity == values[index]);
    this.SetGachaQuestDifficulty((Enum) GachaResultQuest.UI.OBJ_DIFFICULTY_ROOT, star_num);
    this.ResetTween((Enum) GachaResultQuest.UI.OBJ_DIFFICULTY_ROOT);
    this.ResetTween((Enum) this.rarityAnimRoot[(int) questData.rarity]);
    this.ResetTween((Enum) GachaResultQuest.UI.OBJ_RARITY_TEXT_ROOT);
    if (questData.rarity <= RARITY_TYPE.C)
    {
      this.ResetTween((Enum) GachaResultQuest.UI.OBJ_RARITY_LIGHT);
      this.PlayTween((Enum) GachaResultQuest.UI.OBJ_RARITY_LIGHT, is_input_block: false);
    }
    this.PlayTween((Enum) GachaResultQuest.UI.OBJ_RARITY_TEXT_ROOT, is_input_block: false);
    this.PlayTween((Enum) this.rarityAnimRoot[(int) questData.rarity], callback: (EventDelegate.Callback) (() => this.PlayTween((Enum) GachaResultQuest.UI.OBJ_DIFFICULTY_ROOT, is_input_block: false)), is_input_block: false);
    QuestGachaDirectorBase i = AnimationDirector.I as QuestGachaDirectorBase;
    if (!Object.op_Inequality((Object) i, (Object) null))
      return;
    i.PlayRarityAudio(questData.rarity, true);
    i.PlayUIRarityEffect(questData.rarity, this.GetCtrl((Enum) GachaResultQuest.UI.OBJ_RARITY_ROOT), this.GetCtrl((Enum) this.rarityAnimRoot[(int) questData.rarity]));
  }

  protected void UpdateMultiGachaUI()
  {
    List<GachaResult.GachaReward> gachaRewardList = !MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector ? MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward : MonoBehaviourSingleton<GachaManager>.I.gachaResultBonus.reward;
    int index = 0;
    gachaRewardList.ForEach((Action<GachaResult.GachaReward>) (reward =>
    {
      bool is_new = false;
      int num = 0;
      QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem((uint) reward.itemId);
      if (questItem != null)
      {
        GameSaveData.instance.IsNewItem(ITEM_ICON_TYPE.QUEST_ITEM, questItem.uniqueID);
        is_new = this.IsNewItemQuestEnemySpecies(questItem);
        num = questItem.infoData.questData.tableData.GetMainEnemyLv();
      }
      ItemIcon.CreateRewardItemIcon(REWARD_TYPE.QUEST_ITEM, (uint) reward.itemId, this.GetCtrl((Enum) this.iconRootAry[index]), is_new: is_new).SetEnableCollider(false);
      string text = string.Empty;
      if (num > 0)
        text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1U), (object) num.ToString());
      this.SetLabelText(this.GetCtrl((Enum) this.iconRootAry[index]), (Enum) this.iconLevelAry[index], text);
      this.SetEvent(this.GetCtrl((Enum) this.iconRootAry[index]), "QUEST_DETAIL", index);
      ++index;
    }));
    for (int index1 = index; index1 < this.iconRootAry.Length; ++index1)
      this.SetActive((Enum) this.iconRootAry[index1], false);
    Vector3[] vector3Array = (Vector3[]) null;
    switch (gachaRewardList.Count)
    {
      case 2:
        vector3Array = new Vector3[2]
        {
          new Vector3(-90f, -160f),
          new Vector3(90f, -160f)
        };
        break;
      case 4:
        vector3Array = new Vector3[4]
        {
          new Vector3(-90f, -160f),
          new Vector3(90f, -160f),
          new Vector3(-90f, -296f),
          new Vector3(90f, -296f)
        };
        break;
      case 7:
        vector3Array = new Vector3[7]
        {
          new Vector3(-90f, -92f),
          new Vector3(90f, -92f),
          new Vector3(-90f, -228f),
          new Vector3(90f, -228f),
          new Vector3(-133f, -364f),
          new Vector3(0.0f, -364f),
          new Vector3(133f, -364f)
        };
        break;
      case 9:
        vector3Array = new Vector3[9]
        {
          new Vector3(-133f, -92f),
          new Vector3(0.0f, -92f),
          new Vector3(133f, -92f),
          new Vector3(-133f, -228f),
          new Vector3(0.0f, -228f),
          new Vector3(133f, -228f),
          new Vector3(-133f, -364f),
          new Vector3(0.0f, -364f),
          new Vector3(133f, -364f)
        };
        break;
    }
    if (vector3Array == null)
      return;
    for (int index2 = 0; index2 < gachaRewardList.Count; ++index2)
      this.GetCtrl((Enum) this.iconRootAry[index2]).localPosition = vector3Array[index2];
  }

  protected void UpdateSingleResultFooterUI()
  {
    this.SetActive((Enum) GachaResultQuest.UI.FOOTER_MULTI_RESULT_ROOT, false);
    if (this.nextGachaGuarantee.IsValid())
    {
      this.SetActive((Enum) GachaResultQuest.UI.FOOTER_ROOT, false);
      this.SetActive((Enum) GachaResultQuest.UI.FOOTER_GUARANTEE_ROOT, true);
      ((Component) this.GetCtrl((Enum) GachaResultQuest.UI.BG_MULTI)).GetComponent<UISprite>().height = 750;
    }
    else
    {
      this.SetActive((Enum) GachaResultQuest.UI.FOOTER_ROOT, true);
      this.SetActive((Enum) GachaResultQuest.UI.FOOTER_GUARANTEE_ROOT, false);
      ((Component) this.GetCtrl((Enum) GachaResultQuest.UI.BG_MULTI)).GetComponent<UISprite>().height = 740;
    }
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_BACK, false);
    int num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
    if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0)
    {
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId);
      UITexture[] uiTextureArray = new UITexture[3]
      {
        ((Component) this.FindCtrl(this.FindCtrl(this.footerRoot, (Enum) GachaResultQuest.UI.OBJ_GACHA_DISABLE_ROOT), (Enum) GachaResultQuest.UI.TEX_TICKET)).GetComponent<UITexture>(),
        ((Component) this.FindCtrl(this.FindCtrl(this.footerRoot, (Enum) GachaResultQuest.UI.OBJ_GACHA_ENABLE_ROOT), (Enum) GachaResultQuest.UI.TEX_TICKET)).GetComponent<UITexture>(),
        ((Component) this.FindCtrl(this.footerRoot, (Enum) GachaResultQuest.UI.TEX_TICKET_HAVE)).GetComponent<UITexture>()
      };
      foreach (UITexture ui_tex in uiTextureArray)
        ResourceLoad.LoadItemIconTexture(ui_tex, itemData.iconID);
      num = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (int) x.tableData.id == (int) itemData.id), 1);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.GACHATICKETCOUNTERSRESULT, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.S_COUNTER, false);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.S_AVAILABLE, true);
      if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null && MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter >= 0)
      {
        this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.GACHATICKETCOUNTERSRESULT, false);
        this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.S_COUNTER, false);
        this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.S_AVAILABLE, true);
      }
      else if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null && MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter > 0)
      {
        this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.GACHATICKETCOUNTERSRESULT, true);
        this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.S_COUNTER, true);
        this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.S_AVAILABLE, false);
        this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.NUMBER_COUNTER_IMG, true);
        ((Component) this.FindCtrl(this.footerRoot, (Enum) GachaResultQuest.UI.NUMBER_COUNTER_IMG)).GetComponent<UISprite>().spriteName = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter.ToString();
        ((Component) this.FindCtrl(this.FindCtrl(this.footerRoot, (Enum) GachaResultQuest.UI.GACHATICKETCOUNTERSRESULT), (Enum) GachaResultQuest.UI.COUNTER_PROGRESSBAR_FOREGROUND)).GetComponent<UISprite>().fillAmount = (float) (10 - MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter) / 10f;
        this.SetLabelText(this.footerRoot, (Enum) GachaResultQuest.UI.COUNTER_LBL, (object) MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter);
      }
      else
      {
        this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.GACHATICKETCOUNTERSRESULT, true);
        this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.S_COUNTER, false);
        this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.S_AVAILABLE, true);
      }
    }
    this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.SPR_CRYSTAL, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId == 0);
    this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.TEX_TICKET_HAVE, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0);
    this.SetLabelText(this.footerRoot, (Enum) GachaResultQuest.UI.LBL_CRYSTAL_NUM, num.ToString());
    this.SetGachaButtonActive(this.IsEnableEntry());
    this.SetEventDetailImageButton();
  }

  protected void UpdateMultiResultFooterUI()
  {
    if (MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult())
    {
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_NEXT, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_BACK, false);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_BATTLE, false);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.OBJ_GUARANTEE, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.SPR_LINE_BOTTOM, false);
      this.GetCtrl((Enum) GachaResultQuest.UI.OBJ_ICONS_ROOT).localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }
    else
    {
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_NEXT, false);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_BACK, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_BATTLE, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.OBJ_GUARANTEE, false);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.SPR_LINE_BOTTOM, true);
      this.GetCtrl((Enum) GachaResultQuest.UI.OBJ_ICONS_ROOT).localPosition = new Vector3(0.0f, -90f, 0.0f);
    }
    this.SetGachaButtonActive(this.IsEnableEntry());
    this.SetEventDetailImageButton();
  }

  protected void UpdateFeverResultFooterUI()
  {
    if (MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector)
    {
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_BACK, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_BATTLE, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.OBJ_GUARANTEE, true);
      this.SetLabelText(this.footerRoot, (Enum) GachaResultQuest.UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString());
    }
    else
    {
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_BACK, false);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_BATTLE, false);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.OBJ_GUARANTEE, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultQuest.UI.SPR_LINE_BOTTOM, false);
      ((Component) this.GetCtrl((Enum) GachaResultQuest.UI.BG_MULTI)).GetComponent<UISprite>().height = 830;
      this.FindCtrl(this.footerRoot, (Enum) GachaResultQuest.UI.BTN_NEXT).localPosition = new Vector3(5f, -258f);
    }
    this.SetGachaButtonActive(this.IsEnableEntry());
    this.SetEventDetailImageButton();
  }

  private void SetEventDetailImageButton()
  {
    Transform ctrl = this.FindCtrl(this.footerRoot, (Enum) GachaResultQuest.UI.TEX_GUARANTEE_COUNT_DOWN);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    if (this.isExistDetailButton)
    {
      if (!this.nextGachaGuarantee.IsValid() || !this.nextGachaGuarantee.IsItemConfirmed())
      {
        if (!this.nextGachaGuarantee.link.IsNullOrWhiteSpace())
        {
          ((Behaviour) ((Component) ctrl).GetComponent<UIButton>()).enabled = true;
          this.SetEvent(ctrl, "GUARANTEE_GACHA_DETAIL_WEB", (object) this.nextGachaGuarantee.link);
        }
        else
          ((Behaviour) ((Component) ctrl).GetComponent<UIButton>()).enabled = false;
      }
      else
      {
        switch ((REWARD_TYPE) this.nextGachaGuarantee.type)
        {
          case REWARD_TYPE.SKILL_ITEM:
            ((Behaviour) ((Component) ctrl).GetComponent<UIButton>()).enabled = true;
            this.SetEvent(ctrl, "SKILL_DETAIL", (object) new object[2]
            {
              (object) ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
              (object) Singleton<SkillItemTable>.I.GetSkillItemData((uint) this.nextGachaGuarantee.itemId)
            });
            break;
          case REWARD_TYPE.ACCESSORY:
            ((Behaviour) ((Component) ctrl).GetComponent<UIButton>()).enabled = true;
            AccessorySortData accessorySortData = new AccessorySortData();
            AccessoryInfo accessoryInfo = new AccessoryInfo();
            accessoryInfo.SetValue((uint) this.nextGachaGuarantee.itemId);
            accessorySortData.SetItem((object) accessoryInfo);
            this.SetEvent(ctrl, "ACCESSORY_SELECT", (object) new object[2]
            {
              (object) ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
              (object) accessorySortData
            });
            break;
          default:
            ((Behaviour) ((Component) ctrl).GetComponent<UIButton>()).enabled = false;
            break;
        }
      }
    }
    else
      ((Behaviour) ((Component) ctrl).GetComponent<UIButton>()).enabled = false;
  }

  private bool IsNewItemQuestEnemySpecies(QuestItemInfo questItem)
  {
    bool flag = true;
    if (questItem == null)
      return flag;
    QuestInfoData infoData = questItem.infoData;
    if (infoData == null)
      return flag;
    QuestInfoData.Quest questData = infoData.questData;
    if (questData == null)
      return flag;
    QuestTable.QuestTableData tableData = questData.tableData;
    if (tableData == null)
      return flag;
    ClearStatusQuestEnemySpecies questEnemySpecies = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestEnemySpecies(tableData.questID);
    if (questEnemySpecies == null || questEnemySpecies.questStatus == 1)
      return flag;
    flag = false;
    return flag;
  }

  private void OnQuery_QUEST_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    List<GachaResult.GachaReward> gachaRewardList = !MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector ? MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward : MonoBehaviourSingleton<GachaManager>.I.gachaResultBonus.reward;
    int count = gachaRewardList.Count;
    if (eventData < 0 || eventData >= count)
    {
      GameSection.StopEvent();
    }
    else
    {
      QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem((uint) gachaRewardList[eventData].itemId);
      if (questItem == null)
      {
        GameSection.StopEvent();
      }
      else
      {
        QuestSortData event_data = new QuestSortData();
        event_data.SetItem((object) questItem);
        GameSection.SetEventData((object) event_data);
      }
    }
  }

  private void OnQuery_BATTLE()
  {
    this.isJumpToBattle = true;
    this.OnCloseDialog_GachaResultToBattleConfirm();
  }

  private void OnQuery_GachaResultToBattleConfirm_YES() => this.isJumpToBattle = true;

  private void OnQuery_GachaResultToBattleConfirm_NO() => this.isJumpToBattle = false;

  private void OnCloseDialog_GachaResultToBattleConfirm()
  {
    if (!this.isJumpToBattle)
      return;
    int num = MonoBehaviourSingleton<GachaManager>.I.selectGacha.num == 1 ? 1 : 0;
    string goingHomeEvent = GameSection.GetGoingHomeEvent();
    EventData[] event_datas;
    if (num != 0)
      event_datas = new EventData[3]
      {
        new EventData(goingHomeEvent, (object) null),
        new EventData("GACHA_QUEST_COUNTER", (object) null),
        new EventData("TO_GACHA_QUEST_COUNTER", (object) null)
      };
    else
      event_datas = new EventData[3]
      {
        new EventData(goingHomeEvent, (object) null),
        new EventData("GACHA_QUEST_COUNTER", (object) null),
        new EventData("TO_GACHA_QUEST_COUNTER", (object) null)
      };
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
    this.isJumpToBattle = false;
  }

  protected override void OnDestroy()
  {
    if (AppMain.isApplicationQuit)
      return;
    this._OnDestroy();
    if (this.isRetry || !Object.op_Inequality((Object) AnimationDirector.I, (Object) null))
      return;
    AnimationDirector.I.Reset();
  }

  protected void _OnDestroy() => base.OnDestroy();

  protected void SetGachaQuestDifficulty(Enum _enum, int star_num)
  {
    Transform ctrl = this.GetCtrl(_enum);
    int num = 0;
    for (int childCount = ctrl.childCount; num < childCount; ++num)
      ((Component) ctrl.GetChild(num)).gameObject.SetActive(num <= star_num);
    ((Component) ctrl).GetComponent<UIGrid>().Reposition();
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.CheckUpdateCrystalNum();
      if (!this.isRetry)
        this.SetLabelText(this.footerRoot, (Enum) GachaResultQuest.UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString());
    }
    base.OnNotify(flags);
  }

  private void OnQuery_RESET() => this.TEST_RESET(RARITY_TYPE.D, DIFFICULTY_TYPE.LV10);

  private void OnQuery_D()
  {
    this.TEST_RESET(RARITY_TYPE.D, DIFFICULTY_TYPE.LV10);
    this.TEST_PLAY(RARITY_TYPE.D, DIFFICULTY_TYPE.LV10);
  }

  private void OnQuery_C()
  {
    this.TEST_RESET(RARITY_TYPE.C, DIFFICULTY_TYPE.LV10);
    this.TEST_PLAY(RARITY_TYPE.C, DIFFICULTY_TYPE.LV10);
  }

  private void OnQuery_B()
  {
    this.TEST_RESET(RARITY_TYPE.B, DIFFICULTY_TYPE.LV10);
    this.TEST_PLAY(RARITY_TYPE.B, DIFFICULTY_TYPE.LV10);
  }

  private void OnQuery_A()
  {
    this.TEST_RESET(RARITY_TYPE.A, DIFFICULTY_TYPE.LV10);
    this.TEST_PLAY(RARITY_TYPE.A, DIFFICULTY_TYPE.LV10);
  }

  private void OnQuery_S()
  {
    this.TEST_RESET(RARITY_TYPE.S, DIFFICULTY_TYPE.LV10);
    this.TEST_PLAY(RARITY_TYPE.S, DIFFICULTY_TYPE.LV10);
  }

  private void OnQuery_SS()
  {
    this.TEST_RESET(RARITY_TYPE.SS, DIFFICULTY_TYPE.LV10);
    this.TEST_PLAY(RARITY_TYPE.SS, DIFFICULTY_TYPE.LV10);
  }

  private void OnQuery_SSS()
  {
    this.TEST_RESET(RARITY_TYPE.SSS, DIFFICULTY_TYPE.LV10);
    this.TEST_PLAY(RARITY_TYPE.SSS, DIFFICULTY_TYPE.LV10);
  }

  private void TEST_RESET(RARITY_TYPE rarity, DIFFICULTY_TYPE difficulty)
  {
    RARITY_TYPE[] values = (RARITY_TYPE[]) Enum.GetValues(typeof (RARITY_TYPE));
    int star_num = (int) (difficulty + 1);
    int index = 0;
    for (int length = values.Length; index < length; ++index)
      this.SetActive((Enum) this.rarityAnimRoot[index], rarity == values[index]);
    this.SetGachaQuestDifficulty((Enum) GachaResultQuest.UI.OBJ_DIFFICULTY_ROOT, star_num);
    this.ResetTween((Enum) GachaResultQuest.UI.OBJ_DIFFICULTY_ROOT);
    this.ResetTween((Enum) this.rarityAnimRoot[(int) rarity]);
    this.ResetTween((Enum) GachaResultQuest.UI.OBJ_RARITY_TEXT_ROOT);
    if (rarity > RARITY_TYPE.C)
      return;
    this.ResetTween((Enum) GachaResultQuest.UI.OBJ_RARITY_LIGHT);
  }

  private void TEST_PLAY(RARITY_TYPE rarity, DIFFICULTY_TYPE difficulty)
  {
    if (rarity <= RARITY_TYPE.C)
      this.PlayTween((Enum) GachaResultQuest.UI.OBJ_RARITY_LIGHT, is_input_block: false);
    this.PlayTween((Enum) GachaResultQuest.UI.OBJ_RARITY_TEXT_ROOT, is_input_block: false);
    this.PlayTween((Enum) this.rarityAnimRoot[(int) rarity], callback: (EventDelegate.Callback) (() => this.PlayTween((Enum) GachaResultQuest.UI.OBJ_DIFFICULTY_ROOT, is_input_block: false)), is_input_block: false);
    if (!(AnimationDirector.I is QuestGachaDirectorBase))
      return;
    (AnimationDirector.I as QuestGachaDirectorBase).PlayUIRarityEffect(rarity, this.GetCtrl((Enum) GachaResultQuest.UI.OBJ_RARITY_ROOT), this.GetCtrl((Enum) this.rarityAnimRoot[(int) rarity]));
  }

  private new enum UI
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
    GACHATICKETCOUNTERSRESULT,
    COUNTER_PROGRESSBAR_FOREGROUND,
    COUNTER_LBL,
    S_COUNTER,
    NUMBER_COUNTER_IMG,
    S_AVAILABLE,
  }
}
