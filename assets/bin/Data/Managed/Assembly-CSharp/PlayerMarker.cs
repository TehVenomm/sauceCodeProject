// Decompiled with JetBrains decompiler
// Type: PlayerMarker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class PlayerMarker : MonoBehaviour
{
  [SerializeField]
  private float speed;
  [SerializeField]
  private float alphaSpeed;
  private float angle;
  private Transform _transform;
  private float baseAngle = -45f;
  private float alpha;
  private Material _mat;
  private Transform camTransform;

  private void Awake()
  {
    this._transform = ((Component) this).transform;
    MeshRenderer component = ((Component) this).GetComponent<MeshRenderer>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    this._mat = ((Renderer) component).material;
  }

  private void Update()
  {
    this.alpha += Time.deltaTime * this.alphaSpeed;
    this.alpha = Mathf.Clamp01(this.alpha);
    this._mat.SetFloat("_Alpha", this.alpha);
    this.angle += Time.deltaTime * this.speed;
    this.angle = Mathf.Repeat(this.angle, 360f);
    if (Object.op_Inequality((Object) null, (Object) this.camTransform))
    {
      Vector3 position = this.camTransform.position;
      position.x = this._transform.position.x;
      this._transform.LookAt(position);
      this._transform.localRotation = Quaternion.op_Multiply(this._transform.localRotation, Quaternion.AngleAxis(this.angle, Vector3.up));
    }
    else
      this._transform.localRotation = Quaternion.op_Multiply(Quaternion.AngleAxis(this.baseAngle, Vector3.right), Quaternion.AngleAxis(this.angle, Vector3.up));
  }

  public void SetWorldMode(bool enable)
  {
    if (enable)
    {
      this.baseAngle = 45f;
      float num = 10f;
      this._transform.localScale = new Vector3(num, num, num);
    }
    else
    {
      this.baseAngle = -45f;
      this._transform.localScale = Vector3.one;
    }
  }

  public void SetCamera(Transform c) => this.camTransform = c;
}
