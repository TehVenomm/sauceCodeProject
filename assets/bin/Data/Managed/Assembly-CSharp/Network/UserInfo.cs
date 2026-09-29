// Decompiled with JetBrains decompiler
// Type: Network.UserInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class UserInfo
{
  public int id;
  public string name = "";
  public string comment = "";
  public string lastLogin = "";
  public int isParentPassSet;
  public int isStopperSet;
  public bool isAdvancedUser;
  public bool isAdvancedUserGoogle;
  public bool isAdvancedUserFacebook;
  public bool isSetGooglePassword;
  public string advancedUserMail;
  public string code = "";
  public bool inputInviteFlag;
  public string birthday = "";
  public bool communityFlag;
  public bool codeDispFlag = true;
  public int pushEnable;
  public EndDate editNameAt = new EndDate();
  public ServerConstDefine constDefine = new ServerConstDefine();
  public bool isCharged;

  public bool IsParentPassSet
  {
    get => this.isParentPassSet != 0;
    set => this.isParentPassSet = value ? 1 : 0;
  }

  public bool IsStopperSet
  {
    get => this.isStopperSet != 0;
    set => this.isStopperSet = value ? 1 : 0;
  }

  public bool IsAdvanced => this.isAdvancedUser || this.isAdvancedUserGoogle;

  public bool IsModiedName => this.name != "/colopl_rob";
}
