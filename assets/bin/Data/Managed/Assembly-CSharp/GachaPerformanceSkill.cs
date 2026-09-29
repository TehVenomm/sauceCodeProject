// Decompiled with JetBrains decompiler
// Type: GachaPerformanceSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class GachaPerformanceSkill : GachaPerformanceBase, SkillGachaDirector.ISectionCommand
{
  protected override void OnOpen()
  {
    this.SetActive((Enum) GachaPerformanceSkill.UI.BTN_SKIP, true);
    if (!Object.op_Inequality((Object) AnimationDirector.I, (Object) null))
      return;
    (AnimationDirector.I as SkillGachaDirector).StartDirection((SkillGachaDirector.ISectionCommand) this);
  }

  void SkillGachaDirector.ISectionCommand.OnShowSkillModel(uint skill_item_id)
  {
    this.SetRenderSkillItemModel((Enum) GachaPerformanceSkill.UI.TEX_MODEL, skill_item_id, false);
    this.SetRenderSkillItemSymbolModel((Enum) GachaPerformanceSkill.UI.TEX_INNER_MODEL, skill_item_id, false);
  }

  void SkillGachaDirector.ISectionCommand.OnHideSkillModel()
  {
    this.ClearRenderModel((Enum) GachaPerformanceSkill.UI.TEX_MODEL);
    this.ClearRenderModel((Enum) GachaPerformanceSkill.UI.TEX_INNER_MODEL);
  }

  void SkillGachaDirector.ISectionCommand.OnShowRarity(RARITY_TYPE rarity)
  {
    this.ShowRarity(rarity);
  }

  void SkillGachaDirector.ISectionCommand.OnHideRarity() => this.HideRarity();

  void SkillGachaDirector.ISectionCommand.OnEnd() => this.End();

  public new enum UI
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
    TEX_MODEL,
    TEX_INNER_MODEL,
  }
}
