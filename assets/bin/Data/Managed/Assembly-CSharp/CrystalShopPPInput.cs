// Decompiled with JetBrains decompiler
// Type: CrystalShopPPInput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class CrystalShopPPInput : PPInputBase
{
  public override void UpdateUI()
  {
    this.SetActive((Enum) PPInputBase.UI.STR_REMOVE_PASS, false);
    base.UpdateUI();
  }

  private void OnQuery_OK()
  {
    this.RequestEvent("PP_TO_BUY", (object) this.GetInputValue((Enum) PPInputBase.UI.IPT_PW));
  }
}
