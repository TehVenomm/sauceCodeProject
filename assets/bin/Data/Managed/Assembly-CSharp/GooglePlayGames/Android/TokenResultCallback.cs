// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Android.TokenResultCallback
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;
using System;

#nullable disable
namespace GooglePlayGames.Android;

internal class TokenResultCallback : ResultCallbackProxy<TokenResult>
{
  private Action<int, string, string, string> callback;

  public TokenResultCallback(Action<int, string, string, string> callback)
  {
    this.callback = callback;
  }

  public override void OnResult(TokenResult arg_Result_1)
  {
    if (this.callback == null)
      return;
    this.callback(arg_Result_1.getStatusCode(), arg_Result_1.getAuthCode(), arg_Result_1.getEmail(), arg_Result_1.getIdToken());
  }

  public string toString() => ((object) this).ToString();
}
