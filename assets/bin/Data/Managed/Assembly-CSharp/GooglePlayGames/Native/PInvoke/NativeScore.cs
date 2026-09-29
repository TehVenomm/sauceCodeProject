// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeScore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeScore : BaseReferenceHolder
{
  private const ulong MinusOne = 18446744073709551615 /*0xFFFFFFFFFFFFFFFF*/;

  internal NativeScore(IntPtr selfPtr)
    : base(selfPtr)
  {
  }

  protected override void CallDispose(HandleRef selfPointer) => Score.Score_Dispose(this.SelfPtr());

  internal ulong GetDate() => ulong.MaxValue;

  internal string GetMetadata()
  {
    return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Score.Score_Metadata(this.SelfPtr(), out_string, out_size)));
  }

  internal ulong GetRank() => Score.Score_Rank(this.SelfPtr());

  internal ulong GetValue() => Score.Score_Value(this.SelfPtr());

  internal PlayGamesScore AsScore(string leaderboardId, string selfPlayerId)
  {
    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    ulong num = this.GetDate();
    if (num == ulong.MaxValue)
      num = 0UL;
    return new PlayGamesScore(dateTime.AddMilliseconds((double) num), leaderboardId, this.GetRank(), selfPlayerId, this.GetValue(), this.GetMetadata());
  }

  internal static NativeScore FromPointer(IntPtr pointer)
  {
    return pointer.Equals((object) IntPtr.Zero) ? (NativeScore) null : new NativeScore(pointer);
  }
}
