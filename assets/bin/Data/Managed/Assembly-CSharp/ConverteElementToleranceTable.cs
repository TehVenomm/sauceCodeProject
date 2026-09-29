// Decompiled with JetBrains decompiler
// Type: ConverteElementToleranceTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class ConverteElementToleranceTable
{
  public ELEMENT_TYPE fire;
  public ELEMENT_TYPE water = ELEMENT_TYPE.WATER;
  public ELEMENT_TYPE thunder = ELEMENT_TYPE.THUNDER;
  public ELEMENT_TYPE soil = ELEMENT_TYPE.SOIL;
  public ELEMENT_TYPE light = ELEMENT_TYPE.LIGHT;
  public ELEMENT_TYPE dark = ELEMENT_TYPE.DARK;

  public int GetChangeElement(int no)
  {
    int changeElement = -1;
    switch (no)
    {
      case 0:
        changeElement = (int) this.fire;
        break;
      case 1:
        changeElement = (int) this.water;
        break;
      case 2:
        changeElement = (int) this.thunder;
        break;
      case 3:
        changeElement = (int) this.soil;
        break;
      case 4:
        changeElement = (int) this.light;
        break;
      case 5:
        changeElement = (int) this.dark;
        break;
    }
    if (changeElement < 0)
      Debug.LogError((object) "GetElement Not No");
    return changeElement;
  }
}
