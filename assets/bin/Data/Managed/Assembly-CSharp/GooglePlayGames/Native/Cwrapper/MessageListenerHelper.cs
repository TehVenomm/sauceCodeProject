// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.MessageListenerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class MessageListenerHelper
{
  [DllImport("gpg")]
  internal static extern void MessageListenerHelper_SetOnMessageReceivedCallback(
    HandleRef self,
    MessageListenerHelper.OnMessageReceivedCallback callback,
    IntPtr callback_arg);

  [DllImport("gpg")]
  internal static extern void MessageListenerHelper_SetOnDisconnectedCallback(
    HandleRef self,
    MessageListenerHelper.OnDisconnectedCallback callback,
    IntPtr callback_arg);

  [DllImport("gpg")]
  internal static extern IntPtr MessageListenerHelper_Construct();

  [DllImport("gpg")]
  internal static extern void MessageListenerHelper_Dispose(HandleRef self);

  internal delegate void OnMessageReceivedCallback(
    long arg0,
    string arg1,
    IntPtr arg2,
    UIntPtr arg3,
    [MarshalAs(UnmanagedType.I1)] bool arg4,
    IntPtr arg5);

  internal delegate void OnDisconnectedCallback(long arg0, string arg1, IntPtr arg2);
}
