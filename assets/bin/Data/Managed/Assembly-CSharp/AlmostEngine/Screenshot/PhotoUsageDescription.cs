// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.PhotoUsageDescription
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

public class PhotoUsageDescription : ScriptableObject
{
  public string m_UsageDescription = "This application requires the access to the photo library to allow the user to take screenshots that are automatically added to the Camera Roll.";
  private static PhotoUsageDescription m_Usage;
}
