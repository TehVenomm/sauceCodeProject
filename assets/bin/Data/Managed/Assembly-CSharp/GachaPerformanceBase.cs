// Decompiled with JetBrains decompiler
// Type: GachaPerformanceBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class GachaPerformanceBase : GameSection
{
  private GachaPerformanceBase.UI[] rarityAnimRoot = new GachaPerformanceBase.UI[7]
  {
    GachaPerformanceBase.UI.OBJ_RARITY_D,
    GachaPerformanceBase.UI.OBJ_RARITY_C,
    GachaPerformanceBase.UI.OBJ_RARITY_B,
    GachaPerformanceBase.UI.OBJ_RARITY_A,
    GachaPerformanceBase.UI.OBJ_RARITY_S,
    GachaPerformanceBase.UI.OBJ_RARITY_SS,
    GachaPerformanceBase.UI.OBJ_RARITY_SSS
  };

  public override void UpdateUI()
  {
    this.SetActive((Enum) GachaPerformanceBase.UI.OBJ_RARITY_ROOT, false);
  }

  protected void ShowRarity(RARITY_TYPE rarity)
  {
    if (AnimationDirector.I.IsSkip())
      return;
    this.SetActive((Enum) GachaPerformanceBase.UI.OBJ_RARITY_ROOT, true);
    GachaPerformanceBase.UI label_enum = this.rarityAnimRoot[(int) rarity];
    int index = 0;
    for (int length = this.rarityAnimRoot.Length; index < length; ++index)
      this.SetActive((Enum) this.rarityAnimRoot[index], this.rarityAnimRoot[index] == label_enum);
    this.ResetTween((Enum) GachaPerformanceBase.UI.OBJ_RARITY_TEXT_ROOT);
    this.ResetTween((Enum) this.rarityAnimRoot[(int) rarity]);
    if (rarity <= RARITY_TYPE.C)
    {
      this.ResetTween((Enum) GachaPerformanceBase.UI.OBJ_RARITY_LIGHT);
      this.PlayTween((Enum) GachaPerformanceBase.UI.OBJ_RARITY_LIGHT, is_input_block: false);
    }
    this.PlayTween((Enum) this.rarityAnimRoot[(int) rarity], is_input_block: false);
    this.PlayTween((Enum) GachaPerformanceBase.UI.OBJ_RARITY_TEXT_ROOT, is_input_block: false);
    switch (AnimationDirector.I)
    {
      case QuestGachaDirectorBase _:
        (AnimationDirector.I as QuestGachaDirectorBase).PlayUIRarityEffect(rarity, this.GetCtrl((Enum) GachaPerformanceBase.UI.OBJ_RARITY_ROOT), this.GetCtrl((Enum) label_enum));
        break;
      case SkillGachaDirector _:
        (AnimationDirector.I as SkillGachaDirector).PlayUIRarityEffect(rarity, this.GetCtrl((Enum) GachaPerformanceBase.UI.OBJ_RARITY_ROOT), this.GetCtrl((Enum) label_enum));
        break;
    }
  }

  protected void HideRarity()
  {
    int index = 0;
    for (int length = this.rarityAnimRoot.Length; index < length; ++index)
      this.SetActive((Enum) this.rarityAnimRoot[index], false);
    this.SetActive((Enum) GachaPerformanceBase.UI.OBJ_RARITY_ROOT, false);
  }

  protected void End() => this.DispatchEvent("NEXT");

  protected void OnQuery_SKIP()
  {
    this.SetActive((Enum) GachaPerformanceBase.UI.BTN_SKIP, false);
    this.SetActive((Enum) GachaPerformanceBase.UI.OBJ_RARITY_ROOT, false);
    AnimationDirector.I.Skip();
  }

  protected void OnQuery_SKIPALL() => AnimationDirector.I.SkipAll();

  protected void ActivateButtonSkip()
  {
    this.SetActive((Enum) GachaPerformanceBase.UI.BTN_SKIP, true);
  }

  public enum UI
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
  }
}
