// Decompiled with JetBrains decompiler
// Type: FieldCarriableEvolveItemGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FieldCarriableEvolveItemGimmickObject : FieldCarriableGimmickObject
{
  private static readonly int kShiftIndex = 1000;

  protected override bool IsDefenseTool() => false;

  public void Use2Evolve(FieldCarriableGimmickObject gimmick)
  {
    gimmick.Evolve();
    ((Component) this).gameObject.SetActive(false);
  }

  public override bool HasDeploied() => false;
}
