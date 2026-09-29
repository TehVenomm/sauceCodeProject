// Decompiled with JetBrains decompiler
// Type: PLACE_Extention
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
internal static class PLACE_Extention
{
  public static Vector2 GetVector2(this PLACE place)
  {
    switch (place)
    {
      case PLACE.FRONT:
        return Vector2.up;
      case PLACE.RIGHT:
        return Vector2.right;
      case PLACE.LEFT:
        return Vector2.left;
      case PLACE.BACK:
        return Vector2.down;
      default:
        return Vector2.zero;
    }
  }

  public static Vector3 GetVector3(this PLACE place)
  {
    switch (place)
    {
      case PLACE.FRONT:
        return Vector3.forward;
      case PLACE.RIGHT:
        return Vector3.right;
      case PLACE.LEFT:
        return Vector3.left;
      case PLACE.BACK:
        return Vector3.back;
      default:
        return Vector3.zero;
    }
  }

  public static PLACE Reverse(this PLACE place)
  {
    switch (place)
    {
      case PLACE.FRONT:
        return PLACE.BACK;
      case PLACE.RIGHT:
        return PLACE.LEFT;
      case PLACE.LEFT:
        return PLACE.RIGHT;
      case PLACE.BACK:
        return PLACE.FRONT;
      default:
        return place;
    }
  }
}
