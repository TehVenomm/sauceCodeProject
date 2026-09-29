// Decompiled with JetBrains decompiler
// Type: Com.Google.Android.Gms.Games.Games_BaseGamesApiMethodImpl`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;
using Google.Developers;
using System;

#nullable disable
namespace Com.Google.Android.Gms.Games;

public class Games_BaseGamesApiMethodImpl<R> : JavaObjWrapper where R : Result
{
  private const string CLASS_NAME = "com/google/android/gms/games/Games$BaseGamesApiMethodImpl";

  public Games_BaseGamesApiMethodImpl(IntPtr ptr)
    : base(ptr)
  {
  }

  public Games_BaseGamesApiMethodImpl(GoogleApiClient arg_GoogleApiClient_1)
  {
    this.CreateInstance("com/google/android/gms/games/Games$BaseGamesApiMethodImpl", (object) arg_GoogleApiClient_1);
  }
}
