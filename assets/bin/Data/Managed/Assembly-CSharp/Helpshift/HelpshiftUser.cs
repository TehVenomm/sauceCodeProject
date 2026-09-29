// Decompiled with JetBrains decompiler
// Type: Helpshift.HelpshiftUser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Helpshift;

public class HelpshiftUser
{
  public readonly string identifier;
  public readonly string email;
  public readonly string name;
  public readonly string authToken;

  private HelpshiftUser(string identifier, string email, string name, string authToken)
  {
    this.identifier = identifier;
    this.email = email;
    this.name = name;
    this.authToken = authToken;
  }

  public sealed class Builder
  {
    private string identifier;
    private string email;
    private string name;
    private string authToken;

    public Builder(string identifier, string email)
    {
      this.email = email;
      this.identifier = identifier;
    }

    public HelpshiftUser.Builder setName(string name)
    {
      this.name = name;
      return this;
    }

    public HelpshiftUser.Builder setAuthToken(string authToken)
    {
      this.authToken = authToken;
      return this;
    }

    public HelpshiftUser build()
    {
      return new HelpshiftUser(this.identifier, this.email, this.name, this.authToken);
    }
  }
}
