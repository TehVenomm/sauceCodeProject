// Decompiled with JetBrains decompiler
// Type: YesNoCloseDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class YesNoCloseDialog : CommonDialog
{
  private const int MIN_MSG_HEIGHT = 100;

  protected override string GetTransferUIName() => "UI_CommonCloseDialog";

  protected override void InitDialog(object data_object)
  {
    this.InitUI();
    this.SetupLabelText(data_object as string[]);
    this.AutoUILayout();
    SoundManager.PlaySystemSE(this.openingSound);
  }

  protected void SetupLabelText(string[] _msgs)
  {
    string[] strArray = _msgs;
    if (strArray == null || strArray.Length < 1)
      strArray = this.GetTexts((object[]) _msgs);
    int length = strArray.Length;
    string text = length > 0 ? strArray[0] : string.Empty;
    this.SetLabelText((Enum) CommonDialog.UI.LBL_TITLE_U, text);
    this.SetLabelText((Enum) CommonDialog.UI.LBL_TITLE_D, text);
    this.SetLabelText((Enum) CommonDialog.UI.MESSAGE, length > 1 ? strArray[1] : string.Empty);
    if (length > 2)
    {
      this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_0, strArray[2]);
      this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_0_R, strArray[2]);
    }
    if (length <= 3)
      return;
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_1, _msgs[3]);
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_1_R, _msgs[3]);
  }

  protected void AutoUILayout()
  {
    Transform ctrl1 = this.GetCtrl((Enum) CommonDialog.UI.BG);
    int height = this.GetHeight((Enum) CommonDialog.UI.MESSAGE);
    if (height < 100)
      return;
    int num = height - 100;
    this.SetHeight((Enum) CommonDialog.UI.BG, this.GetHeight((Enum) CommonDialog.UI.BG) + (int) ((double) num / (double) ctrl1.localScale.y));
    Transform ctrl2 = this.GetCtrl((Enum) CommonDialog.UI.HEADER);
    Vector3 position = ctrl2.position;
    position.y += (float) (num / 2);
    ctrl2.position = position;
    Transform ctrl3 = this.GetCtrl((Enum) CommonDialog.UI.FOOTER);
    position = ctrl3.position;
    position.y -= (float) (num / 2);
    ctrl3.position = position;
    this.UpdateAnchors();
  }
}
