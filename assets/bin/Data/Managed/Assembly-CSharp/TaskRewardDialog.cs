// Decompiled with JetBrains decompiler
// Type: TaskRewardDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class TaskRewardDialog : GameSection
{
  public override void Initialize()
  {
    TaskTop.TaskData eventData = GameSection.GetEventData() as TaskTop.TaskData;
    this.SetLabelText((Enum) TaskRewardDialog.UI.LBL_ACHIEVE_NAME, eventData.tableData.title);
    this.SetLabelText((Enum) TaskRewardDialog.UI.LBL_ITEM_NAME, eventData.tableData.GetRewardString());
    base.Initialize();
  }

  public override void UpdateUI() => base.UpdateUI();

  private enum UI
  {
    LBL_ACHIEVE_NAME,
    LBL_ITEM_NAME,
  }
}
