// Decompiled with JetBrains decompiler
// Type: TestStone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TestStone : BreakObject
{
  protected override void Awake()
  {
    base.Awake();
    if (!string.IsNullOrEmpty(this.breakEffectName))
      return;
    this.breakEffectName = "ef_btl_bg_rockbreak_01";
  }

  protected override void Initialize()
  {
    base.Initialize();
    Renderer componentInChildren = (Renderer) ((Component) this).gameObject.GetComponentInChildren<MeshRenderer>();
    if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
      return;
    ((Component) componentInChildren).gameObject.AddComponent<SphereCollider>().radius = 2.2f;
  }
}
