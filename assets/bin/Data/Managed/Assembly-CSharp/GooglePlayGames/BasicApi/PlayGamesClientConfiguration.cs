// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.PlayGamesClientConfiguration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.OurUtils;
using System.Collections.Generic;

#nullable disable
namespace GooglePlayGames.BasicApi;

public struct PlayGamesClientConfiguration
{
  public static readonly PlayGamesClientConfiguration DefaultConfiguration = new PlayGamesClientConfiguration.Builder().Build();
  private readonly bool mEnableSavedGames;
  private readonly string[] mScopes;
  private readonly bool mRequestAuthCode;
  private readonly bool mForceRefresh;
  private readonly bool mHidePopups;
  private readonly bool mRequestEmail;
  private readonly bool mRequestIdToken;
  private readonly string mAccountName;
  private readonly InvitationReceivedDelegate mInvitationDelegate;
  private readonly MatchDelegate mMatchDelegate;

  private PlayGamesClientConfiguration(PlayGamesClientConfiguration.Builder builder)
  {
    this.mEnableSavedGames = builder.HasEnableSaveGames();
    this.mInvitationDelegate = builder.GetInvitationDelegate();
    this.mMatchDelegate = builder.GetMatchDelegate();
    this.mScopes = builder.getScopes();
    this.mHidePopups = builder.IsHidingPopups();
    this.mRequestAuthCode = builder.IsRequestingAuthCode();
    this.mForceRefresh = builder.IsForcingRefresh();
    this.mRequestEmail = builder.IsRequestingEmail();
    this.mRequestIdToken = builder.IsRequestingIdToken();
    this.mAccountName = builder.GetAccountName();
  }

  public bool EnableSavedGames => this.mEnableSavedGames;

  public bool IsHidingPopups => this.mHidePopups;

  public bool IsRequestingAuthCode => this.mRequestAuthCode;

  public bool IsForcingRefresh => this.mForceRefresh;

  public bool IsRequestingEmail => this.mRequestEmail;

  public bool IsRequestingIdToken => this.mRequestIdToken;

  public string AccountName => this.mAccountName;

  public string[] Scopes => this.mScopes;

  public InvitationReceivedDelegate InvitationDelegate => this.mInvitationDelegate;

  public MatchDelegate MatchDelegate => this.mMatchDelegate;

  public class Builder
  {
    private bool mEnableSaveGames;
    private List<string> mScopes;
    private bool mHidePopups;
    private bool mRequestAuthCode;
    private bool mForceRefresh;
    private bool mRequestEmail;
    private bool mRequestIdToken;
    private string mAccountName;
    private InvitationReceivedDelegate mInvitationDelegate = (InvitationReceivedDelegate) ((_param1, _param2) => { });
    private MatchDelegate mMatchDelegate = (MatchDelegate) ((_param1, _param2) => { });

    public PlayGamesClientConfiguration.Builder EnableSavedGames()
    {
      this.mEnableSaveGames = true;
      return this;
    }

    public PlayGamesClientConfiguration.Builder EnableHidePopups()
    {
      this.mHidePopups = true;
      return this;
    }

    public PlayGamesClientConfiguration.Builder RequestServerAuthCode(bool forceRefresh)
    {
      this.mRequestAuthCode = true;
      this.mForceRefresh = forceRefresh;
      return this;
    }

    public PlayGamesClientConfiguration.Builder RequestEmail()
    {
      this.mRequestEmail = true;
      return this;
    }

    public PlayGamesClientConfiguration.Builder RequestIdToken()
    {
      this.mRequestIdToken = true;
      return this;
    }

    public PlayGamesClientConfiguration.Builder SetAccountName(string accountName)
    {
      this.mAccountName = accountName;
      return this;
    }

    public PlayGamesClientConfiguration.Builder AddOauthScope(string scope)
    {
      if (this.mScopes == null)
        this.mScopes = new List<string>();
      this.mScopes.Add(scope);
      return this;
    }

    public PlayGamesClientConfiguration.Builder WithInvitationDelegate(
      InvitationReceivedDelegate invitationDelegate)
    {
      this.mInvitationDelegate = Misc.CheckNotNull<InvitationReceivedDelegate>(invitationDelegate);
      return this;
    }

    public PlayGamesClientConfiguration.Builder WithMatchDelegate(MatchDelegate matchDelegate)
    {
      this.mMatchDelegate = Misc.CheckNotNull<MatchDelegate>(matchDelegate);
      return this;
    }

    public PlayGamesClientConfiguration Build() => new PlayGamesClientConfiguration(this);

    internal bool HasEnableSaveGames() => this.mEnableSaveGames;

    internal bool IsRequestingAuthCode() => this.mRequestAuthCode;

    internal bool IsHidingPopups() => this.mHidePopups;

    internal bool IsForcingRefresh() => this.mForceRefresh;

    internal bool IsRequestingEmail() => this.mRequestEmail;

    internal bool IsRequestingIdToken() => this.mRequestIdToken;

    internal string GetAccountName() => this.mAccountName;

    internal string[] getScopes() => this.mScopes != null ? this.mScopes.ToArray() : new string[0];

    internal MatchDelegate GetMatchDelegate() => this.mMatchDelegate;

    internal InvitationReceivedDelegate GetInvitationDelegate() => this.mInvitationDelegate;
  }
}
