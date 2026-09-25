// Decompiled with JetBrains decompiler
// Type: PuniController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class PuniController : MonoBehaviour
{
  public Camera uiCamera;
  public SlimeController slime;
  public int renderQueue = 3000;
  public float posZ;
  public float updateAddAnimTime = 0.075f;
  private Transform slimeParentTransform;
  private Vector3 endPos = Vector3.zero;

  private void Awake()
  {
    this.slimeParentTransform = ((Component) this).transform;
    Vector3 position = this.slimeParentTransform.position;
    position.z = this.posZ;
    this.slimeParentTransform.position = position;
    ((Renderer) ((Component) this.slime).GetComponent<MeshRenderer>()).material.renderQueue = this.renderQueue;
    this.slime.updateAnimTime = this.updateAddAnimTime;
  }

  private void LateUpdate()
  {
    if (!Vector3.op_Inequality(this.endPos, Vector3.zero))
      return;
    Vector3 vector3_1 = Vector3.op_Subtraction(this.endPos, this.slimeParentTransform.position);
    this.slimeParentTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, -(Vector3.Angle(Vector3.up, vector3_1) * Mathf.Sign(vector3_1.x)));
    SlimeController slime = this.slime;
    Vector3 vector3_2 = Vector3.op_Subtraction(this.endPos, this.slimeParentTransform.position);
    Vector3 target = new Vector3(0.0f, (float) ((double) ((Vector3) ref vector3_2).magnitude / (double) this.slimeParentTransform.lossyScale.x), 0.0f);
    slime.SetTargetPos(target);
  }

  public void SetStartPosition(Vector3 start_screen_pos)
  {
    this.slimeParentTransform.position = this.uiCamera.ScreenToWorldPoint(start_screen_pos);
    this.endPos = Vector3.zero;
    this.slime.TouchStartSlime();
  }

  public void SetEndPosition(Vector3 end_screen_pos)
  {
    this.endPos = this.uiCamera.ScreenToWorldPoint(end_screen_pos);
  }

  public void Reset()
  {
    this.endPos = Vector3.zero;
    this.slime.TouchEndSlime();
  }
}
