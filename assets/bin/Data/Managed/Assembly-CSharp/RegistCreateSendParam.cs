// Decompiled with JetBrains decompiler
// Type: RegistCreateSendParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class RegistCreateSendParam
{
  public string d = "";
  public string fromCode = "";
  public string fromParam = "";
  public string fromAffiliate = "";

  public void SetAttribute(UserFromAttributeData data)
  {
    if (data == null)
      return;
    this.fromCode = data.fromCode;
    this.fromParam = data.fromParam;
    this.fromAffiliate = data.fromAffiliate;
  }
}
