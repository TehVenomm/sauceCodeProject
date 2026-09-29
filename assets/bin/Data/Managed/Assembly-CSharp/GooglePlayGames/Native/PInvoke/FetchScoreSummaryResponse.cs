// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.FetchScoreSummaryResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class FetchScoreSummaryResponse : BaseReferenceHolder
{
  internal FetchScoreSummaryResponse(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_FetchScoreSummaryResponse_Dispose(selfPointer);
  }

  internal CommonErrorStatus.ResponseStatus GetStatus()
  {
    return GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_FetchScoreSummaryResponse_GetStatus(this.SelfPtr());
  }

  internal NativeScoreSummary GetScoreSummary()
  {
    return NativeScoreSummary.FromPointer(GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_FetchScoreSummaryResponse_GetData(this.SelfPtr()));
  }

  internal static FetchScoreSummaryResponse FromPointer(IntPtr pointer)
  {
    return pointer.Equals((object) IntPtr.Zero) ? (FetchScoreSummaryResponse) null : new FetchScoreSummaryResponse(pointer);
  }
}
