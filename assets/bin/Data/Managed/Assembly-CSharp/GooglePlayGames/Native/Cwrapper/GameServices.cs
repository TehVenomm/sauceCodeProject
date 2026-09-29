// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.GameServices
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class GameServices
{
  [DllImport("gpg")]
  internal static extern void GameServices_Flush(
    HandleRef self,
    GameServices.FlushCallback callback,
    IntPtr callback_arg);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool GameServices_IsAuthorized(HandleRef self);

  [DllImport("gpg")]
  internal static extern void GameServices_Dispose(HandleRef self);

  [DllImport("gpg")]
  internal static extern void GameServices_SignOut(HandleRef self);

  [DllImport("gpg")]
  internal static extern void GameServices_StartAuthorizationUI(HandleRef self);

  internal delegate void FlushCallback(CommonErrorStatus.FlushStatus arg0, IntPtr arg1);
}
