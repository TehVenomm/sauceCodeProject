// Decompiled with JetBrains decompiler
// Type: GachaResultSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class GachaResultSkill : GachaResultBase
{
  private GachaResultSkill.UI[] iconRootAry = new GachaResultSkill.UI[11]
  {
    GachaResultSkill.UI.OBJ_ICON_ROOT_0,
    GachaResultSkill.UI.OBJ_ICON_ROOT_1,
    GachaResultSkill.UI.OBJ_ICON_ROOT_2,
    GachaResultSkill.UI.OBJ_ICON_ROOT_3,
    GachaResultSkill.UI.OBJ_ICON_ROOT_4,
    GachaResultSkill.UI.OBJ_ICON_ROOT_5,
    GachaResultSkill.UI.OBJ_ICON_ROOT_6,
    GachaResultSkill.UI.OBJ_ICON_ROOT_7,
    GachaResultSkill.UI.OBJ_ICON_ROOT_8,
    GachaResultSkill.UI.OBJ_ICON_ROOT_9,
    GachaResultSkill.UI.OBJ_ICON_ROOT_10
  };
  private GachaResultSkill.UI[] magiNameAry = new GachaResultSkill.UI[11]
  {
    GachaResultSkill.UI.LBL_MAGI_NAME_0,
    GachaResultSkill.UI.LBL_MAGI_NAME_1,
    GachaResultSkill.UI.LBL_MAGI_NAME_2,
    GachaResultSkill.UI.LBL_MAGI_NAME_3,
    GachaResultSkill.UI.LBL_MAGI_NAME_4,
    GachaResultSkill.UI.LBL_MAGI_NAME_5,
    GachaResultSkill.UI.LBL_MAGI_NAME_6,
    GachaResultSkill.UI.LBL_MAGI_NAME_7,
    GachaResultSkill.UI.LBL_MAGI_NAME_8,
    GachaResultSkill.UI.LBL_MAGI_NAME_9,
    GachaResultSkill.UI.LBL_MAGI_NAME_10
  };
  private GachaResultSkill.UI[] rarityAnimRoot = new GachaResultSkill.UI[7]
  {
    GachaResultSkill.UI.OBJ_RARITY_D,
    GachaResultSkill.UI.OBJ_RARITY_C,
    GachaResultSkill.UI.OBJ_RARITY_B,
    GachaResultSkill.UI.OBJ_RARITY_A,
    GachaResultSkill.UI.OBJ_RARITY_S,
    GachaResultSkill.UI.OBJ_RARITY_SS,
    GachaResultSkill.UI.OBJ_RARITY_SSS
  };

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
    this.currentGachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee;
    this.nextGachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().gachaGuaranteeCampaignInfo;
    MonoBehaviourSingleton<GachaManager>.I.SetSelectGachaGuarantee(this.nextGachaGuarantee);
    if (MonoBehaviourSingleton<GachaManager>.I.IsMultiResult())
      yield return (object) this.LoadMultiResultUI(loadQueue);
    else
      yield return (object) this.LoadNormalUI(loadQueue);
    base.Initialize();
  }

  private IEnumerator LoadNormalUI(LoadingQueue loadQueue)
  {
    this.SetActive((Enum) GachaResultSkill.UI.FOOTER_MULTI_RESULT_ROOT, false);
    if (this.nextGachaGuarantee.IsValid())
    {
      this.footerRoot = this.GetCtrl((Enum) GachaResultSkill.UI.FOOTER_GUARANTEE_ROOT);
      this.SetActive((Enum) GachaResultSkill.UI.FOOTER_ROOT, false);
      this.SetActive((Enum) GachaResultSkill.UI.FOOTER_GUARANTEE_ROOT, true);
    }
    else
    {
      this.footerRoot = this.GetCtrl((Enum) GachaResultSkill.UI.FOOTER_ROOT);
      this.SetActive((Enum) GachaResultSkill.UI.FOOTER_ROOT, true);
      this.SetActive((Enum) GachaResultSkill.UI.FOOTER_GUARANTEE_ROOT, false);
    }
    string buttonName = this.CreateButtonName();
    yield return (object) this.LoadGachaButton(loadQueue, this.FindCtrl(this.footerRoot, (Enum) GachaResultSkill.UI.BTN_GACHA), buttonName);
    yield return (object) this.LoadGachaGuaranteeCounter(loadQueue, this.nextGachaGuarantee, (Action<LoadObject>) (lo_guarantee => this.SetTexture(this.footerRoot, (Enum) GachaResultSkill.UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture)));
  }

  public override void UpdateUI()
  {
    bool is_visible = MonoBehaviourSingleton<GachaManager>.I.selectGacha.num == 1;
    this.SetActive((Enum) GachaResultSkill.UI.OBJ_SINGLE_ROOT, is_visible);
    this.SetActive((Enum) GachaResultSkill.UI.OBJ_MULTI_ROOT, !is_visible);
    if (is_visible)
      this.UpdateSingleGachaUI();
    else
      this.UpdateMultiGachaUI();
    if (MonoBehaviourSingleton<GachaManager>.I.IsMultiResult())
      this.UpdateMultiResultFooterUI();
    else
      this.UpdateSingleResultFooterUI();
  }

  protected void UpdateSingleGachaUI()
  {
    GachaResult.GachaReward gachaReward = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward[0];
    SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) gachaReward.itemId);
    if (skillItemData == null)
      this.SetActive((Enum) GachaResultSkill.UI.OBJ_SINGLE_ROOT, false);
    this.SetLabelText((Enum) GachaResultSkill.UI.LBL_NAME, skillItemData.name);
    this.SetLabelText((Enum) GachaResultSkill.UI.LBL_ATK, skillItemData.baseAtk.ToString());
    this.SetLabelText((Enum) GachaResultSkill.UI.LBL_DEF, skillItemData.baseDef.ToString());
    this.SetLabelText((Enum) GachaResultSkill.UI.LBL_HP, skillItemData.baseHp.ToString());
    this.SetLabelText((Enum) GachaResultSkill.UI.LBL_DESCRIPTION, skillItemData.GetExplanationText());
    this.SetRenderSkillItemModel((Enum) GachaResultSkill.UI.TEX_MODEL, skillItemData.id);
    this.SetRenderSkillItemSymbolModel((Enum) GachaResultSkill.UI.TEX_INNER_MODEL, skillItemData.id);
    RARITY_TYPE[] values = (RARITY_TYPE[]) Enum.GetValues(typeof (RARITY_TYPE));
    int index = 0;
    for (int length = values.Length; index < length; ++index)
      this.SetActive((Enum) this.rarityAnimRoot[index], skillItemData.rarity == values[index]);
    this.ResetTween((Enum) this.rarityAnimRoot[(int) skillItemData.rarity]);
    this.ResetTween((Enum) GachaResultSkill.UI.OBJ_RARITY_TEXT_ROOT);
    if (skillItemData.rarity <= RARITY_TYPE.C)
    {
      this.ResetTween((Enum) GachaResultSkill.UI.OBJ_RARITY_LIGHT);
      this.PlayTween((Enum) GachaResultSkill.UI.OBJ_RARITY_LIGHT, is_input_block: false);
    }
    this.PlayTween((Enum) this.rarityAnimRoot[(int) skillItemData.rarity], is_input_block: false);
    this.PlayTween((Enum) GachaResultSkill.UI.OBJ_RARITY_TEXT_ROOT, is_input_block: false);
    if (!(AnimationDirector.I is SkillGachaDirector))
      return;
    (AnimationDirector.I as SkillGachaDirector).PlayUIRarityEffect(skillItemData.rarity, this.GetCtrl((Enum) GachaResultSkill.UI.OBJ_RARITY_ROOT), this.GetCtrl((Enum) this.rarityAnimRoot[(int) skillItemData.rarity]));
  }

  protected void UpdateMultiGachaUI()
  {
    int index = 0;
    MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward.ForEach((Action<GachaResult.GachaReward>) (reward =>
    {
      bool is_new = false;
      Transform ctrl = this.GetCtrl((Enum) this.iconRootAry[index]);
      SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) reward.itemId);
      if (skillItemData == null)
      {
        this.SetActive(ctrl, false);
      }
      else
      {
        this.SetActive(ctrl, true);
        ItemIcon.CreateRewardItemIcon(REWARD_TYPE.SKILL_ITEM, (uint) reward.itemId, ctrl, is_new: is_new).SetEnableCollider(false);
        this.SetLabelText(this.GetCtrl((Enum) this.magiNameAry[index]), skillItemData.name);
        this.SetEvent(this.GetCtrl((Enum) this.iconRootAry[index]), "SKILL_DETAIL", index);
        ++index;
      }
    }));
  }

  public void UpdateSingleResultFooterUI()
  {
    if (this.nextGachaGuarantee.IsValid())
    {
      this.SetActive((Enum) GachaResultSkill.UI.FOOTER_ROOT, false);
      this.SetActive((Enum) GachaResultSkill.UI.FOOTER_GUARANTEE_ROOT, true);
    }
    else
    {
      this.SetActive((Enum) GachaResultSkill.UI.FOOTER_ROOT, true);
      this.SetActive((Enum) GachaResultSkill.UI.FOOTER_GUARANTEE_ROOT, false);
    }
    int num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
    if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0)
    {
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId);
      UITexture[] uiTextureArray = new UITexture[3]
      {
        ((Component) this.FindCtrl(this.GetCtrl((Enum) GachaResultSkill.UI.OBJ_GACHA_DISABLE_ROOT), (Enum) GachaResultSkill.UI.TEX_TICKET)).GetComponent<UITexture>(),
        ((Component) this.FindCtrl(this.GetCtrl((Enum) GachaResultSkill.UI.OBJ_GACHA_ENABLE_ROOT), (Enum) GachaResultSkill.UI.TEX_TICKET)).GetComponent<UITexture>(),
        ((Component) this.GetCtrl((Enum) GachaResultSkill.UI.TEX_TICKET_HAVE)).GetComponent<UITexture>()
      };
      foreach (UITexture ui_tex in uiTextureArray)
        ResourceLoad.LoadItemIconTexture(ui_tex, itemData.iconID);
      num = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (int) x.tableData.id == (int) itemData.id), 1);
    }
    this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.SPR_CRYSTAL, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId == 0);
    this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.TEX_TICKET_HAVE, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0);
    this.SetLabelText(this.footerRoot, (Enum) GachaResultSkill.UI.LBL_CRYSTAL_NUM, num.ToString());
    this.SetGachaButtonActive(this.IsEnableEntry());
    this.SetEventDetailImageButton();
  }

  protected void UpdateMultiResultFooterUI()
  {
    if (MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult())
    {
      this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.BTN_NEXT, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.BTN_BACK, false);
      this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.BTN_EQUIP, false);
      this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.OBJ_GUARANTEE, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.SPR_LINE_BOTTOM, false);
      this.GetCtrl((Enum) GachaResultSkill.UI.OBJ_ICONS_ROOT).localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }
    else
    {
      this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.BTN_NEXT, false);
      this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.BTN_BACK, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.BTN_EQUIP, true);
      this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.OBJ_GUARANTEE, false);
      this.SetActive(this.footerRoot, (Enum) GachaResultSkill.UI.SPR_LINE_BOTTOM, true);
      this.GetCtrl((Enum) GachaResultSkill.UI.OBJ_ICONS_ROOT).localPosition = new Vector3(0.0f, -50f, 0.0f);
    }
    this.SetGachaButtonActive(this.IsEnableEntry());
    this.SetEventDetailImageButton();
  }

  private void SetEventDetailImageButton()
  {
    if (!this.isExistDetailButton)
      return;
    Transform ctrl = this.FindCtrl(this.footerRoot, (Enum) GachaResultSkill.UI.TEX_GUARANTEE_COUNT_DOWN);
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
          this.SetEvent(ctrl, "GUARANTEE_SKILL_DETAIL", (object) null);
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

  private void OnQuery_SECTION_BACK()
  {
    if (!Object.op_Inequality((Object) AnimationDirector.I, (Object) null))
      return;
    AnimationDirector.I.Reset();
  }

  private void OnQuery_EQUIP()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData("MAIN_MENU_STUDIO", (object) null),
      new EventData("SKILL_LIST", (object) null)
    });
  }

  private void OnQuery_SKILL_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    int count = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward.Count;
    if (eventData < 0 || eventData >= count)
    {
      GameSection.StopEvent();
    }
    else
    {
      uint itemId = (uint) MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward[eventData].itemId;
      SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(itemId);
      if (skillItemData == null)
        GameSection.StopEvent();
      else
        GameSection.SetEventData((object) new object[2]
        {
          (object) ItemDetailEquip.CURRENT_SECTION.GACHA_RESULT,
          (object) skillItemData
        });
    }
  }

  private void OnQuery_GUARANTEE_SKILL_DETAIL()
  {
    uint itemId = (uint) this.nextGachaGuarantee.itemId;
    if (Singleton<SkillItemTable>.I.GetSkillItemData(itemId) == null)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[2]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
        (object) Singleton<SkillItemTable>.I.GetSkillItemData((uint) this.nextGachaGuarantee.itemId)
      });
  }

  protected override void OnDestroy()
  {
    this._OnDestroy();
    if (AppMain.isApplicationQuit || this.isRetry || !Object.op_Inequality((Object) AnimationDirector.I, (Object) null))
      return;
    AnimationDirector.I.Reset();
    AnimationDirector.I.SetLinkCamera(false);
  }

  protected void _OnDestroy() => base.OnDestroy();

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.CheckUpdateCrystalNum();
      if (!this.isRetry)
        this.SetLabelText((Enum) GachaResultSkill.UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString());
    }
    base.OnNotify(flags);
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
  }
}
