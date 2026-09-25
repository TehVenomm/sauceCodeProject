// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.TokenClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace GooglePlayGames;

internal interface TokenClient
{
  string GetEmail();

  string GetAuthCode();

  string GetIdToken();

  void Signout();

  void SetRequestAuthCode(bool flag, bool forceRefresh);

  void SetRequestEmail(bool flag);

  void SetRequestIdToken(bool flag);

  void SetWebClientId(string webClientId);

  void SetAccountName(string accountName);

  void AddOauthScopes(string[] scopes);

  void SetHidePopups(bool flag);

  bool NeedsToRun();

  void FetchTokens(Action callback);
}
