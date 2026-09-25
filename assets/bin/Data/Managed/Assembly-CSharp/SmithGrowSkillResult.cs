// Decompiled with JetBrains decompiler
// Type: SmithGrowSkillResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class SmithGrowSkillResult : ItemDetailSkill
{
  protected SmithManager.ResultData resultData;
  private bool isGreat;
  private bool isExceed;
  private bool isPlayExceedAnimation;
  private const float DELAY_TIME = 0.3f;

  public override string overrideBackKeyEvent => "TO_SELECT";

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.resultData = (SmithManager.ResultData) eventData[0];
    this.isGreat = (bool) eventData[1];
    this.isExceed = (bool) eventData[2];
    SkillItemInfo itemData = this.resultData.itemData as SkillItemInfo;
    if (this.isExceed && this.resultData.beforeExceedCnt < itemData.exceedCnt)
      this.isPlayExceedAnimation = true;
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.UI_PARTS,
      this.resultData.itemData
    });
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    string str1 = "ef_ui_magi_result_02";
    string str2 = "ef_ui_magi_result_01";
    string effectName = this.isGreat ? str1 : str2;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, effectName);
    yield return (object) loadingQueue.Wait();
    Transform uiEffect = EffectManager.GetUIEffect(effectName, this.GetCtrl((Enum) SmithGrowSkillResult.UI.TEX_MODEL), -1f, -2, ((Component) this.GetCtrl((Enum) SmithGrowSkillResult.UI.TEX_MODEL)).GetComponent<UIWidget>());
    if (Object.op_Inequality((Object) uiEffect, (Object) null))
      uiEffect.localScale = new Vector3(100f, 100f, 1f);
    MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = false;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    bool is_visible = true;
    if (this.resultData.itemData is SkillItemInfo itemData && itemData.IsLevelMax())
      is_visible = itemData.IsEnableExceed();
    this.SetActive((Enum) SmithGrowSkillResult.UI.SPR_BG_NORMAL, !this.isExceed);
    this.SetActive((Enum) SmithGrowSkillResult.UI.SPR_BG_EXCEED, this.isExceed);
    this.SetActive((Enum) SmithGrowSkillResult.UI.BTN_NEXT, is_visible);
    this.SetActive((Enum) SmithGrowSkillResult.UI.BTN_NEXT_GRAY, !is_visible);
    this.SetLabelText((Enum) SmithGrowSkillResult.UI.LBL_NEXT_GRAY_BTN, this.sectionData.GetText("STR_NEXT"));
    if (!this.isExceed)
      return;
    if (this.resultData != null)
    {
      this.SetLabelText((Enum) SmithGrowSkillResult.UI.LBL_EXCEED_PREV, StringTable.Format(STRING_CATEGORY.SMITH, 9U, (object) this.resultData.beforeExceedCnt));
      this.SetLabelText((Enum) SmithGrowSkillResult.UI.LBL_EXCEED_PREV_2, StringTable.Format(STRING_CATEGORY.SMITH, 9U, (object) this.resultData.beforeExceedCnt));
    }
    if (itemData == null)
      return;
    int exceedCnt = itemData.exceedCnt;
    this.SetLabelText((Enum) SmithGrowSkillResult.UI.LBL_EXCEED_NEXT, StringTable.Format(STRING_CATEGORY.SMITH, 9U, (object) exceedCnt));
    this.SetLabelText((Enum) SmithGrowSkillResult.UI.LBL_EXCEED_NEXT_2, StringTable.Format(STRING_CATEGORY.SMITH, 9U, (object) exceedCnt));
    ExceedSkillItemTable.ExceedSkillItemData exceedSkillItemData = Singleton<ExceedSkillItemTable>.I.GetExceedSkillItemData(exceedCnt);
    if (exceedSkillItemData != null)
    {
      this.SetLabelText((Enum) SmithGrowSkillResult.UI.LBL_ADD_EXCEED, StringTable.Format(STRING_CATEGORY.SMITH, 8U, (object) exceedSkillItemData.GetDecreaseUseGaugePercent()));
      this.SetLabelText((Enum) SmithGrowSkillResult.UI.LBL_ADD_EXCEED_2, StringTable.Format(STRING_CATEGORY.SMITH, 8U, (object) exceedSkillItemData.GetDecreaseUseGaugePercent()));
    }
    this.SetLabelText((Enum) SmithGrowSkillResult.UI.LBL_ADD_EXCEED_EXTRA, itemData.GetExceedExtraText());
  }

  protected override void OnOpen()
  {
    if (this.isPlayExceedAnimation)
      this.StartCoroutine(this.DoPlayExceedAnimation());
    base.OnOpen();
  }

  private IEnumerator DoPlayExceedAnimation()
  {
    yield return (object) new WaitForSeconds(0.3f);
    Transform root = !(this.resultData.itemData is SkillItemInfo itemData) || itemData.GetExceedExtraText().IsNullOrWhiteSpace() ? this.GetCtrl((Enum) SmithGrowSkillResult.UI.OBJ_ADD_EXCEED) : this.GetCtrl((Enum) SmithGrowSkillResult.UI.OBJ_ADD_EXCEED_2);
    UITweenCtrl.Play(root, is_input_block: false);
    UITweenCtrl.Play(root, is_input_block: false, tween_ctrl_id: 1);
    SoundManager.PlayOneShotUISE(40000157);
  }

  private new enum UI
  {
    OBJ_DETAIL_ROOT,
    TEX_MODEL,
    TEX_INNER_MODEL,
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_SELL,
    LBL_DESCRIPTION,
    OBJ_FAVORITE_ROOT,
    TWN_FAVORITE,
    TWN_UNFAVORITE,
    OBJ_SUB_STATUS,
    SPR_SKILL_TYPE_ICON,
    SPR_SKILL_TYPE_ICON_BG,
    SPR_SKILL_TYPE_ICON_RARITY,
    STR_TITLE_ITEM_INFO,
    STR_TITLE_DESCRIPTION,
    STR_TITLE_STATUS,
    STR_TITLE_SELL,
    PRG_EXP_BAR,
    OBJ_NEXT_EXP_ROOT,
    PRG_EXP_BAR_BG,
    BTN_NEXT,
    BTN_NEXT_GRAY,
    LBL_NEXT_GRAY_BTN,
    OBJ_ADD_EXCEED,
    LBL_ADD_EXCEED,
    LBL_EXCEED_PREV,
    LBL_EXCEED_NEXT,
    SPR_BG_NORMAL,
    SPR_BG_EXCEED,
    OBJ_ADD_EXCEED_2,
    LBL_ADD_EXCEED_2,
    LBL_EXCEED_PREV_2,
    LBL_EXCEED_NEXT_2,
    LBL_ADD_EXCEED_EXTRA,
  }

  public enum AUDIO
  {
    RESULT_EXCEEED = 40000157, // 0x02625A9D
  }

  private enum EFFECT_TYPE
  {
    NONE,
    NORMAL,
    GREAT,
  }
}
