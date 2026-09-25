// Decompiled with JetBrains decompiler
// Type: YesNoDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class YesNoDialog : CommonDialog
{
  protected override void InitDialog(object data_object)
  {
    if (!(data_object is string _text))
    {
      string[] texts = this.GetTexts(data_object as object[]);
      base.InitDialog((object) new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, texts.Length != 0 ? texts[0] : string.Empty, texts.Length > 1 ? texts[1] : string.Empty, texts.Length > 2 ? texts[2] : string.Empty));
    }
    else
      base.InitDialog((object) new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, _text));
  }
}
