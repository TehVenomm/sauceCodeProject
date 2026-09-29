// Decompiled with JetBrains decompiler
// Type: StampNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class StampNode : MonoBehaviour
{
  public Vector3 offset;
  public StampNode.StampTrigger[] triggers;
  public int autoStampInfoID;
  public float autoBaseY = -1f;
  private bool up;

  public Transform _transform { get; private set; }

  public Vector3 scaledeOffset { get; private set; }

  private void Awake()
  {
    this._transform = ((Component) this).transform;
    this.scaledeOffset = this.offset.Mul(this._transform.lossyScale);
    if ((double) this.autoBaseY != -1.0)
      this.autoBaseY *= this._transform.lossyScale.y;
    this.up = false;
  }

  private void Start()
  {
  }

  public bool UpdateStamp(float base_y)
  {
    float num1 = this._transform.position.y - base_y;
    Matrix4x4 localToWorldMatrix = this._transform.localToWorldMatrix;
    float num2 = ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint(this.offset).y - base_y;
    if ((double) this.autoBaseY == -1.0)
      this.autoBaseY = num2 + 0.0f;
    if (!this.up)
    {
      if ((double) num2 > (double) this.autoBaseY)
        this.up = true;
    }
    else if ((double) num2 < (double) this.autoBaseY)
    {
      this.up = false;
      return true;
    }
    return false;
  }

  [Serializable]
  public class StampTrigger
  {
    public int eventID;
    public int StampInfoID;
  }
}
