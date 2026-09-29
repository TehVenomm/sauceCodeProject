// Decompiled with JetBrains decompiler
// Type: MultiDisplayUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MultiDisplayUtils : MonoBehaviour
{
  public static bool IsMultiDisplay()
  {
    if (Display.displays.Length == 1)
      return false;
    for (int index = 1; index < Display.displays.Length; ++index)
    {
      if (Display.displays[index].active)
        return true;
    }
    return false;
  }
}
