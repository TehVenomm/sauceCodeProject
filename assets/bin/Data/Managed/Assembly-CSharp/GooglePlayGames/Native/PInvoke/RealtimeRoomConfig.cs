// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.RealtimeRoomConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class RealtimeRoomConfig : BaseReferenceHolder
{
  internal RealtimeRoomConfig(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    RealTimeRoomConfig.RealTimeRoomConfig_Dispose(selfPointer);
  }

  internal static RealtimeRoomConfig FromPointer(IntPtr selfPointer)
  {
    return selfPointer.Equals((object) IntPtr.Zero) ? (RealtimeRoomConfig) null : new RealtimeRoomConfig(selfPointer);
  }
}
