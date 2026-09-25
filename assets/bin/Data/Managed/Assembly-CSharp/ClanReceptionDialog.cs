// Decompiled with JetBrains decompiler
// Type: ClanReceptionDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class ClanReceptionDialog : GameSection
{
  private static readonly Color32 buffGreen = new Color32((byte) 53, byte.MaxValue, (byte) 0, byte.MaxValue);
  private UserClanData m_clanData;
  private SymbolMarkCtrl symbolMark;

  private void Start() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    this.m_clanData = MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData;
    this.SetLabelText((Enum) ClanReceptionDialog.UI.LBL_CLAN_NAME, this.m_clanData.name);
    this.SetClanLv();
    this.SetLabelText((Enum) ClanReceptionDialog.UI.LBL_CLAN_PT_NUM, string.Format("{0:#,0}  pt", (object) this.m_clanData.exp));
    this.SetClanPointGauge();
    yield return (object) this.StartCoroutine(this.CreateSymbolMark());
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader())
      this.SetActive((Enum) ClanReceptionDialog.UI.BTN_CLAN_SETTING, true);
    else
      this.SetActive((Enum) ClanReceptionDialog.UI.BTN_CLAN_SETTING, false);
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader() || MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsSubLeader())
      this.SetButtonEnabled((Enum) ClanReceptionDialog.UI.BTN_CLAN_SYMBOL_EDIT, true);
    else
      this.SetButtonEnabled((Enum) ClanReceptionDialog.UI.BTN_CLAN_SYMBOL_EDIT, false);
    // ISSUE: reference to a compiler-generated method
    this.\u003C\u003En__0();
  }

  public override void InitializeReopen()
  {
    base.InitializeReopen();
    this.LoadSymbolMark();
  }

  private IEnumerator CreateSymbolMark()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject symbolMarkLoadObj = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
    yield return (object) loadingQueue.Wait();
    Transform transform = ResourceUtility.Realizes((Object) (symbolMarkLoadObj.loadedObject as GameObject), 5);
    transform.parent = this.GetCtrl((Enum) ClanReceptionDialog.UI.OBJ_SYMBOL);
    transform.localScale = Vector3.one;
    transform.localPosition = Vector3.zero;
    this.symbolMark = ((Component) transform).GetComponent<SymbolMarkCtrl>();
    this.symbolMark.Initilize();
    this.LoadSymbolMark();
  }

  private void LoadSymbolMark()
  {
    if (!Object.op_Inequality((Object) this.symbolMark, (Object) null))
      return;
    this.symbolMark.LoadSymbol(MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.sym);
  }

  private void SetClanLv()
  {
    this.SetLabelText((Enum) ClanReceptionDialog.UI.LBL_CLAN_LV_NUM, this.m_clanData.level.ToString());
    if (this.m_clanData.isMaxLevel)
    {
      this.SetColor((Enum) ClanReceptionDialog.UI.LBL_CLAN_LV_NUM, Color32.op_Implicit(ClanReceptionDialog.buffGreen));
      this.SetColor((Enum) ClanReceptionDialog.UI.LBL_CLAN_LV, Color32.op_Implicit(ClanReceptionDialog.buffGreen));
      this.SetActive((Enum) ClanReceptionDialog.UI.SPR_GAUGE_MAX_BG, true);
    }
    else
      this.SetActive((Enum) ClanReceptionDialog.UI.SPR_GAUGE_MAX_BG, false);
  }

  private void SetClanPointGauge()
  {
    Transform ctrl = this.FindCtrl(((Component) this).transform, (Enum) ClanReceptionDialog.UI.SPR_GAUGE);
    if (!Object.op_Inequality((Object) ctrl, (Object) null))
      return;
    int num1 = this.m_clanData.exp - this.m_clanData.expPrev;
    int num2 = this.m_clanData.expNext - this.m_clanData.expPrev;
    ctrl.localScale = num2 != 0 ? new Vector3(Mathf.Clamp((float) num1 / (float) num2, 0.0f, 1f), 1f, 1f) : new Vector3(0.0f, 1f, 1f);
    if (!this.m_clanData.isMaxLevel)
      return;
    ctrl.localScale = new Vector3(1f, 1f, 1f);
  }

  private void OnQuery_CLAN_REWARD_INFO()
  {
    GameSection.SetEventData((object) WebViewManager.ClanReward);
  }

  private void OnQuery_CLAN_SYMBOL_EDIT()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) MonoBehaviourSingleton<UserInfoManager>.I.userInfo,
      (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus
    });
  }

  private enum UI
  {
    SPR_FRAME,
    BTN_CLANINFO,
    BTN_CLAN_SEARCH,
    BTN_CLOSE,
    LBL_CLAN_NAME,
    BTN_CLAN_DETAIL,
    BTN_CLAN_SETTING,
    BTN_CLAN_SYMBOL_EDIT,
    LBL_CLAN_LV_NUM,
    LBL_CLAN_LV,
    LBL_CLAN_PT_NUM,
    SPR_GAUGE,
    SPR_GAUGE_MAX_BG,
    OBJ_SYMBOL,
  }
}
