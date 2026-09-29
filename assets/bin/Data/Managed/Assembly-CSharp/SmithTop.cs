// Decompiled with JetBrains decompiler
// Type: SmithTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SmithTop : GameSection
{
  public override void Initialize() => base.Initialize();

  public override void UpdateUI() => base.UpdateUI();

  private void OnCloseDialog() => MonoBehaviourSingleton<SmithManager>.I.InitSmithData();

  private enum UI
  {
    BTN_EQUIP_CREATE,
    BTN_EQUIP_GROW,
    BTN_SKILL_GROW,
    BTN_TO_STATUS,
  }
}
