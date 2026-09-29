// Decompiled with JetBrains decompiler
// Type: GachaPerformanceQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class GachaPerformanceQuest : GachaPerformanceBase, QuestGachaDirectorBase.ISectionCommand
{
  private bool m_isReam;

  protected override void OnOpen()
  {
    this.SetActive((Enum) GachaPerformanceQuest.UI.BTN_SKIP, true);
    this.m_isReam = AnimationDirector.I is QuestReamGachaDirector || AnimationDirector.I is QuestFeverGachaLegacyDirector;
    this.SetActive((Enum) GachaPerformanceQuest.UI.BTN_SKIPALL, this.m_isReam);
    if (!Object.op_Inequality((Object) AnimationDirector.I, (Object) null))
      return;
    (AnimationDirector.I as QuestGachaDirectorBase).StartDirection((QuestGachaDirectorBase.ISectionCommand) this);
  }

  void QuestGachaDirectorBase.ISectionCommand.OnShowRarity(RARITY_TYPE rarity)
  {
    this.SetActive((Enum) GachaPerformanceQuest.UI.BTN_SKIPALL, false);
    this.ShowRarity(rarity);
  }

  void QuestGachaDirectorBase.ISectionCommand.OnHideRarity()
  {
    this.SetActive((Enum) GachaPerformanceQuest.UI.BTN_SKIPALL, this.m_isReam);
    this.HideRarity();
  }

  void QuestGachaDirectorBase.ISectionCommand.OnEnd()
  {
    this.SetActive((Enum) GachaPerformanceQuest.UI.BTN_SKIPALL, false);
    this.End();
  }

  void QuestGachaDirectorBase.ISectionCommand.ActivateSkipButton() => this.ActivateButtonSkip();

  private new enum UI
  {
    BTN_SKIP,
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
    BTN_SKIPALL,
  }
}
