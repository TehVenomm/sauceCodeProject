// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.IosPlatformConfiguration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal sealed class IosPlatformConfiguration : PlatformConfiguration
{
  private IosPlatformConfiguration(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  internal void SetClientId(string clientId)
  {
    Misc.CheckNotNull<string>(clientId);
    GooglePlayGames.Native.Cwrapper.IosPlatformConfiguration.IosPlatformConfiguration_SetClientID(this.SelfPtr(), clientId);
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    GooglePlayGames.Native.Cwrapper.IosPlatformConfiguration.IosPlatformConfiguration_Dispose(selfPointer);
  }

  internal static IosPlatformConfiguration Create()
  {
    return new IosPlatformConfiguration(GooglePlayGames.Native.Cwrapper.IosPlatformConfiguration.IosPlatformConfiguration_Construct());
  }
}
