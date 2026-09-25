// Decompiled with JetBrains decompiler
// Type: UIParticle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UIParticle : UIWidget
{
  private Renderer[] _renderers;
  private int _lastQueue;
  private ParticleSystemRenderer _particleRenderer;
  private bool _createMaterial;

  public override Material material
  {
    get
    {
      if (Object.op_Equality((Object) this._particleRenderer, (Object) null))
        this._particleRenderer = ((Component) this).GetComponentInChildren<ParticleSystemRenderer>(true);
      return ((Renderer) this._particleRenderer).sharedMaterial;
    }
    set
    {
      throw new NotImplementedException(((object) this).GetType().ToString() + " has no material setter");
    }
  }

  protected override void OnStart()
  {
    base.OnStart();
    this._renderers = ((Component) this).GetComponentsInChildren<Renderer>(true);
    if (Application.isPlaying && !this._createMaterial)
    {
      this._createMaterial = true;
      foreach (Renderer renderer in this._renderers)
      {
        int length = renderer.materials.Length;
        Material[] materialArray = new Material[length];
        for (int index = 0; index < length; ++index)
        {
          Material material = new Material(renderer.materials[index]);
          materialArray[index] = material;
        }
        renderer.materials = materialArray;
      }
    }
    this._lastQueue = -1;
  }

  protected override void OnUpdate()
  {
    base.OnUpdate();
    if (Object.op_Equality((Object) this.drawCall, (Object) null))
      return;
    int renderQueue = this.drawCall.renderQueue;
    if (this._lastQueue == renderQueue)
      return;
    this._lastQueue = renderQueue;
    foreach (Renderer renderer in this._renderers)
    {
      foreach (Material sharedMaterial in renderer.sharedMaterials)
        sharedMaterial.renderQueue = this._lastQueue;
      renderer.sortingOrder = this.drawCall.sortingOrder;
    }
  }

  public override void OnFill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
    for (int index = 0; index < 4; ++index)
      verts.Add(Vector3.zero);
    uvs.Add(Vector2.zero);
    cols.Add(new Color32((byte) 1, (byte) 1, (byte) 1, (byte) 1));
  }
}
