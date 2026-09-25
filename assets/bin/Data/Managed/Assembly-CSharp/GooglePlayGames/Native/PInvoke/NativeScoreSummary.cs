// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeScoreSummary
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeScoreSummary : BaseReferenceHolder
{
  internal NativeScoreSummary(IntPtr selfPtr)
    : base(selfPtr)
  {
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    ScoreSummary.ScoreSummary_Dispose(selfPointer);
  }

  internal ulong ApproximateResults()
  {
    return ScoreSummary.ScoreSummary_ApproximateNumberOfScores(this.SelfPtr());
  }

  internal NativeScore LocalUserScore()
  {
    return NativeScore.FromPointer(ScoreSummary.ScoreSummary_CurrentPlayerScore(this.SelfPtr()));
  }

  internal static NativeScoreSummary FromPointer(IntPtr pointer)
  {
    return pointer.Equals((object) IntPtr.Zero) ? (NativeScoreSummary) null : new NativeScoreSummary(pointer);
  }
}
