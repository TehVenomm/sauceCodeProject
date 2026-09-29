// Decompiled with JetBrains decompiler
// Type: BestHTTP.Authentication.Credentials
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace BestHTTP.Authentication;

public sealed class Credentials
{
  public AuthenticationTypes Type { get; private set; }

  public string UserName { get; private set; }

  public string Password { get; private set; }

  public Credentials(string userName, string password)
    : this(AuthenticationTypes.Unknown, userName, password)
  {
  }

  public Credentials(AuthenticationTypes type, string userName, string password)
  {
    this.Type = type;
    this.UserName = userName;
    this.Password = password;
  }
}
