// Decompiled with JetBrains decompiler
// Type: EscapePointObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EscapePointObject : StageObject
{
  public bool isEnemyOnEscapePoint { get; private set; }

  protected override bool IsValidAttackedHit(StageObject from_object) => false;

  protected override void Awake()
  {
    base.Awake();
    Utility.SetLayerWithChildren(((Component) this).transform, 31 /*0x1F*/);
  }

  private void OnTriggerStay(Collider collider)
  {
    if (((Component) collider).gameObject.layer != 10)
      return;
    this.isEnemyOnEscapePoint = true;
  }

  private void OnTriggerExit(Collider collider)
  {
    if (((Component) collider).gameObject.layer != 10)
      return;
    this.isEnemyOnEscapePoint = false;
  }
}
