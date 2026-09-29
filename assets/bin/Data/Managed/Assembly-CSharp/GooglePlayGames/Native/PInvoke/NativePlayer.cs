// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativePlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativePlayer : BaseReferenceHolder
{
  internal NativePlayer(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  internal string Id()
  {
    return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => GooglePlayGames.Native.Cwrapper.Player.Player_Id(this.SelfPtr(), out_string, out_size)));
  }

  internal string Name()
  {
    return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => GooglePlayGames.Native.Cwrapper.Player.Player_Name(this.SelfPtr(), out_string, out_size)));
  }

  internal string AvatarURL()
  {
    return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => GooglePlayGames.Native.Cwrapper.Player.Player_AvatarUrl(this.SelfPtr(), Types.ImageResolution.ICON, out_string, out_size)));
  }

  protected override void CallDispose(HandleRef selfPointer) => GooglePlayGames.Native.Cwrapper.Player.Player_Dispose(selfPointer);

  internal GooglePlayGames.BasicApi.Multiplayer.Player AsPlayer()
  {
    return new GooglePlayGames.BasicApi.Multiplayer.Player(this.Name(), this.Id(), this.AvatarURL());
  }
}
