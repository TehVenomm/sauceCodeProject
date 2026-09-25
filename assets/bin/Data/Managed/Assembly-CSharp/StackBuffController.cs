// Decompiled with JetBrains decompiler
// Type: StackBuffController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class StackBuffController
{
  private int[] stackCounts;

  public void Init() => this.stackCounts = new int[2];

  public int GetStackCount(StackBuffController.STACK_TYPE type) => this.stackCounts[(int) type];

  public void IncrementStackCount(StackBuffController.STACK_TYPE type)
  {
    ++this.stackCounts[(int) type];
  }

  public void DecrementStackCount(StackBuffController.STACK_TYPE type)
  {
    this.stackCounts[(int) type] = Mathf.Max(0, this.stackCounts[(int) type] - 1);
  }

  public enum STACK_TYPE
  {
    NONE,
    SNATCH,
    MAX,
  }
}
