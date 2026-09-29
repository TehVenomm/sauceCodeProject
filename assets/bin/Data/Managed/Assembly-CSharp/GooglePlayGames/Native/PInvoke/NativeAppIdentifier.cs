// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeAppIdentifier
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeAppIdentifier : BaseReferenceHolder
{
  [DllImport("gpg")]
  internal static extern IntPtr NearbyUtils_ConstructAppIdentifier(string appId);

  internal NativeAppIdentifier(IntPtr pointer)
    : base(pointer)
  {
  }

  internal string Id()
  {
    return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_arg, out_size) => NearbyConnectionTypes.AppIdentifier_GetIdentifier(this.SelfPtr(), out_arg, out_size)));
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    NearbyConnectionTypes.AppIdentifier_Dispose(selfPointer);
  }

  internal static NativeAppIdentifier FromString(string appId)
  {
    return new NativeAppIdentifier(NativeAppIdentifier.NearbyUtils_ConstructAppIdentifier(appId));
  }
}
