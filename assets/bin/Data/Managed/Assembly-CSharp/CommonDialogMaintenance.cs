// Decompiled with JetBrains decompiler
// Type: CommonDialogMaintenance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class CommonDialogMaintenance : CommonDialog
{
  protected override string GetTransferUIName() => "UI_CommonDialogMaintenance";

  protected override void InitDialog(object data_object)
  {
    base.InitDialog(data_object);
    CommonDialog.Desc desc = data_object as CommonDialog.Desc;
    if (desc.data == null)
      return;
    try
    {
      DateTime dateTime = CommonDialogMaintenance.UnixTimeStampToDateTime((double) long.Parse(desc.data.ToString()));
      this.SetLabelText((Enum) CommonDialog.UI.MESSAGE, string.Format(desc.text, (object) $"{this.GetFormartedText(dateTime.Day)}/{this.GetFormartedText(dateTime.Month)}, {this.GetFormartedText(dateTime.Hour)}:{this.GetFormartedText(dateTime.Minute)}"));
    }
    catch
    {
      this.SetLabelText((Enum) CommonDialog.UI.MESSAGE, desc.text);
    }
  }

  public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
  {
    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    dateTime = dateTime.AddSeconds(unixTimeStamp);
    return dateTime;
  }

  private string GetFormartedText(int num)
  {
    return string.Format(num > 9 ? $"{{{(object) 0}}}" : $"0{{{(object) 0}}}", (object) num);
  }
}
