// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.Score
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class Score
{
  [DllImport("gpg")]
  internal static extern ulong Score_Value(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool Score_Valid(HandleRef self);

  [DllImport("gpg")]
  internal static extern ulong Score_Rank(HandleRef self);

  [DllImport("gpg")]
  internal static extern void Score_Dispose(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr Score_Metadata(HandleRef self, [In, Out] byte[] out_arg, UIntPtr out_size);
}
