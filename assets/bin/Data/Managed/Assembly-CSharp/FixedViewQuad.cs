// Decompiled with JetBrains decompiler
// Type: FixedViewQuad
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
public class FixedViewQuad : MonoBehaviour
{
  public Camera targetCamera;
  public float planeZ = 100f;
  private Transform _transform;
  private MeshRenderer _meshRenderer;

  private void Awake()
  {
    this._transform = ((Component) this).transform;
    this._meshRenderer = ((Component) this).GetComponent<MeshRenderer>();
    if (!Object.op_Equality((Object) this.targetCamera, (Object) null))
      return;
    this.targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
  }

  private void LateUpdate()
  {
    if (Object.op_Inequality((Object) this._meshRenderer, (Object) null))
      ((Renderer) this._meshRenderer).enabled = Object.op_Inequality((Object) this.targetCamera, (Object) null);
    float num1 = (float) Screen.width * 0.5f;
    float num2 = (float) Screen.height * 0.5f;
    Vector3 worldPoint = this.targetCamera.ScreenToWorldPoint(new Vector3(num1, num2, this.planeZ));
    Vector3 vector3_1;
    if ((double) num1 < (double) num2)
    {
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_1).\u002Ector(num1, -1f, this.planeZ);
    }
    else
    {
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_1).\u002Ector(-1f, num2, this.planeZ);
    }
    Vector3 vector3_2 = Vector3.op_Subtraction(this.targetCamera.ScreenToWorldPoint(vector3_1), worldPoint);
    float num3 = ((Vector3) ref vector3_2).magnitude * 2f;
    this._transform.localScale = new Vector3(num3, num3, 1f);
    this._transform.position = worldPoint;
    this._transform.rotation = ((Component) this.targetCamera).transform.rotation;
  }
}
