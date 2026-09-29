// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Android.TokenResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;
using Google.Developers;
using System;

#nullable disable
namespace GooglePlayGames.Android;

internal class TokenResult(IntPtr ptr) : JavaObjWrapper(ptr), Result
{
  public Status getStatus()
  {
    return new Status(this.InvokeCall<IntPtr>(nameof (getStatus), "()Lcom/google/android/gms/common/api/Status;"));
  }

  public int getStatusCode() => this.InvokeCall<int>(nameof (getStatusCode), "()I");

  public string getAuthCode()
  {
    return this.InvokeCall<string>(nameof (getAuthCode), "()Ljava/lang/String;");
  }

  public string getEmail() => this.InvokeCall<string>(nameof (getEmail), "()Ljava/lang/String;");

  public string getIdToken()
  {
    return this.InvokeCall<string>(nameof (getIdToken), "()Ljava/lang/String;");
  }
}
