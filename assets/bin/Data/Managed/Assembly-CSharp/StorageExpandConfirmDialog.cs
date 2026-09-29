// Decompiled with JetBrains decompiler
// Type: StorageExpandConfirmDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class StorageExpandConfirmDialog : CommonDialog
{
  protected override string GetTransferUIName() => "UI_StorageExpandConfirmDialog";

  protected override void SetupThreeButton(CommonDialog.Desc data)
  {
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_0, data.btnText[0]);
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_0_R, data.btnText[0]);
    this.SetEventName((Enum) CommonDialog.UI.SPR_BTN_0, "YES");
    this.SetButtonSprite((Enum) CommonDialog.UI.SPR_BTN_0, CommonDialog.BTN_SPRITE_NAME[2], true);
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_1, data.btnText[1]);
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_1_R, data.btnText[1]);
    this.SetEventName((Enum) CommonDialog.UI.SPR_BTN_1, "GO_ITEM_STORAGE");
  }
}
