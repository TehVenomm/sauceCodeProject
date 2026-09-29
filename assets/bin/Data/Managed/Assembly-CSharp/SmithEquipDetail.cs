// Decompiled with JetBrains decompiler
// Type: SmithEquipDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class SmithEquipDetail : ItemDetailEquip
{
  protected override bool IsShowFrameBG() => true;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.SetActive((Enum) ItemDetailEquip.UI.BTN_CHANGE, false);
    this.SetActive((Enum) ItemDetailEquip.UI.BTN_CREATE, false);
    this.SetActive((Enum) ItemDetailEquip.UI.BTN_GROW, false);
  }
}
