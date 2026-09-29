// Decompiled with JetBrains decompiler
// Type: UIDragResize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Drag-Resize Widget")]
public class UIDragResize : MonoBehaviour
{
  public UIWidget target;
  public UIWidget.Pivot pivot = UIWidget.Pivot.BottomRight;
  public int minWidth = 100;
  public int minHeight = 100;
  public int maxWidth = 100000;
  public int maxHeight = 100000;
  public bool updateAnchors;
  private Plane mPlane;
  private Vector3 mRayPos;
  private Vector3 mLocalPos;
  private int mWidth;
  private int mHeight;
  private bool mDragging;

  private void OnDragStart()
  {
    if (!Object.op_Inequality((Object) this.target, (Object) null))
      return;
    Vector3[] worldCorners = this.target.worldCorners;
    this.mPlane = new Plane(worldCorners[0], worldCorners[1], worldCorners[3]);
    Ray currentRay = UICamera.currentRay;
    float num;
    if (!((Plane) ref this.mPlane).Raycast(currentRay, ref num))
      return;
    this.mRayPos = ((Ray) ref currentRay).GetPoint(num);
    this.mLocalPos = this.target.cachedTransform.localPosition;
    this.mWidth = this.target.width;
    this.mHeight = this.target.height;
    this.mDragging = true;
  }

  private void OnDrag(Vector2 delta)
  {
    if (!this.mDragging || !Object.op_Inequality((Object) this.target, (Object) null))
      return;
    Ray currentRay = UICamera.currentRay;
    float num;
    if (!((Plane) ref this.mPlane).Raycast(currentRay, ref num))
      return;
    Transform cachedTransform = this.target.cachedTransform;
    cachedTransform.localPosition = this.mLocalPos;
    this.target.width = this.mWidth;
    this.target.height = this.mHeight;
    Vector3 vector3_1 = Vector3.op_Subtraction(((Ray) ref currentRay).GetPoint(num), this.mRayPos);
    cachedTransform.position = Vector3.op_Addition(cachedTransform.position, vector3_1);
    Vector3 vector3_2 = Quaternion.op_Multiply(Quaternion.Inverse(cachedTransform.localRotation), Vector3.op_Subtraction(cachedTransform.localPosition, this.mLocalPos));
    cachedTransform.localPosition = this.mLocalPos;
    NGUIMath.ResizeWidget(this.target, this.pivot, vector3_2.x, vector3_2.y, this.minWidth, this.minHeight, this.maxWidth, this.maxHeight);
    if (!this.updateAnchors)
      return;
    ((Component) this.target).BroadcastMessage("UpdateAnchors");
  }

  private void OnDragEnd() => this.mDragging = false;
}
