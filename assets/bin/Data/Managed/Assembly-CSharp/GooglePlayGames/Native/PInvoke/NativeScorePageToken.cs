// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeScorePageToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeScorePageToken : BaseReferenceHolder
{
  internal NativeScorePageToken(IntPtr selfPtr)
    : base(selfPtr)
  {
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    ScorePage.ScorePage_ScorePageToken_Dispose(selfPointer);
  }
}
