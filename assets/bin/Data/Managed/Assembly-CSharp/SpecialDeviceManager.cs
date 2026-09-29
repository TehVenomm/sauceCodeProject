// Decompiled with JetBrains decompiler
// Type: SpecialDeviceManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SpecialDeviceManager
{
  private static DeviceIndividualInfo static_device;

  public static bool HasSpecialDeviceInfo => SpecialDeviceManager.static_device != null;

  public static DeviceIndividualInfo SpecialDeviceInfo => SpecialDeviceManager.static_device;

  public static bool IsPortrait => Screen.width < Screen.height;

  public static void StartUp()
  {
    if (SpecialDeviceManager.static_device != null)
      return;
    SpecialDeviceManager.static_device = !AndroidAdjustUIDeviceInfo.MustBeAdustUI ? (DeviceIndividualInfo) new AndroidDefaultDeviceInfo() : (DeviceIndividualInfo) new AndroidAdjustUIDeviceInfo();
    if (SpecialDeviceManager.static_device == null)
      return;
    SpecialDeviceManager.static_device.OnStart();
  }
}
