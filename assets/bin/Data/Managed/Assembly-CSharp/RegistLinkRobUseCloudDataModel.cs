// Decompiled with JetBrains decompiler
// Type: RegistLinkRobUseCloudDataModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class RegistLinkRobUseCloudDataModel : BaseModel
{
  public static string URL = "ajax/regist/overridelinkedrobbycloud";
  public RegistLinkRobUseCloudDataModel.Param result = new RegistLinkRobUseCloudDataModel.Param();

  public class Param : RegistCreateModel.Param
  {
    public int newsId = 1;
  }

  public class RequestSendForm
  {
    public string email;
    public string d;
  }
}
