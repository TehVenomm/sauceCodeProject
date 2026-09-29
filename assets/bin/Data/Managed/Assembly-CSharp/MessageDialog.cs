// Decompiled with JetBrains decompiler
// Type: MessageDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class MessageDialog : CommonDialog
{
  protected override void InitDialog(object data_object)
  {
    this.InitDialog(data_object, STRING_CATEGORY.COMMON_DIALOG);
  }

  protected void InitDialog(object data_object, STRING_CATEGORY message_category)
  {
    if (!(data_object is string _text))
    {
      string[] texts = this.GetTexts(data_object as object[], message_category);
      base.InitDialog((object) new CommonDialog.Desc(CommonDialog.TYPE.OK, texts.Length != 0 ? texts[0] : string.Empty, texts.Length > 1 ? texts[1] : string.Empty));
    }
    else
      base.InitDialog((object) new CommonDialog.Desc(CommonDialog.TYPE.OK, _text));
  }
}
