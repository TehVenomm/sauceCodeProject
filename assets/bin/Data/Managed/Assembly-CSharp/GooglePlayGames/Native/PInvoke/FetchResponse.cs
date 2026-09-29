// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.FetchResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class FetchResponse : BaseReferenceHolder
{
  internal FetchResponse(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_FetchResponse_Dispose(this.SelfPtr());
  }

  internal NativeLeaderboard Leaderboard()
  {
    return NativeLeaderboard.FromPointer(GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_FetchResponse_GetData(this.SelfPtr()));
  }

  internal CommonErrorStatus.ResponseStatus GetStatus()
  {
    return GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_FetchResponse_GetStatus(this.SelfPtr());
  }

  internal static FetchResponse FromPointer(IntPtr pointer)
  {
    return pointer.Equals((object) IntPtr.Zero) ? (FetchResponse) null : new FetchResponse(pointer);
  }
}
