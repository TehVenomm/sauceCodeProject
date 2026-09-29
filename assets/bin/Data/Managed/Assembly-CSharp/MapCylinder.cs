// Decompiled with JetBrains decompiler
// Type: MapCylinder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MapCylinder : MonoBehaviour
{
  [Tooltip("半径")]
  public float radius = 40f;
  [Tooltip("表示状態にする距離")]
  public float showLength = 3f;
  [Tooltip("非表示状態にする距離")]
  public float hideLength = 5f;

  public Transform _transform { get; protected set; }

  public MeshRenderer meshRenderer { get; protected set; }

  private void Start()
  {
    this._transform = ((Component) this).transform;
    this.meshRenderer = ((Component) this).GetComponent<MeshRenderer>();
    if (!Object.op_Inequality((Object) this.meshRenderer, (Object) null))
      return;
    ((Renderer) this.meshRenderer).enabled = false;
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.meshRenderer, (Object) null) || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Equality((Object) self, (Object) null))
      return;
    Vector3 vector3 = Vector3.op_Subtraction(self._transform.position, this._transform.position);
    float magnitude = ((Vector3) ref vector3).magnitude;
    if ((double) magnitude <= (double) this.radius - (double) this.hideLength)
    {
      ((Renderer) this.meshRenderer).enabled = false;
    }
    else
    {
      if ((double) magnitude < (double) this.radius - (double) this.showLength)
        return;
      ((Renderer) this.meshRenderer).enabled = true;
    }
  }
}
