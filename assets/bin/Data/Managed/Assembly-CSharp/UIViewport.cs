// Decompiled with JetBrains decompiler
// Type: UIViewport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("NGUI/UI/Viewport Camera")]
public class UIViewport : MonoBehaviour
{
  public Camera sourceCamera;
  public Transform topLeft;
  public Transform bottomRight;
  public float fullSize = 1f;
  private Camera mCam;

  private void Start()
  {
    this.mCam = ((Component) this).GetComponent<Camera>();
    if (!Object.op_Equality((Object) this.sourceCamera, (Object) null))
      return;
    this.sourceCamera = Camera.main;
  }

  private void LateUpdate()
  {
    if (!Object.op_Inequality((Object) this.topLeft, (Object) null) || !Object.op_Inequality((Object) this.bottomRight, (Object) null))
      return;
    if (((Component) this.topLeft).gameObject.activeInHierarchy)
    {
      Vector3 screenPoint1 = this.sourceCamera.WorldToScreenPoint(this.topLeft.position);
      Vector3 screenPoint2 = this.sourceCamera.WorldToScreenPoint(this.bottomRight.position);
      Rect rect;
      // ISSUE: explicit constructor call
      ((Rect) ref rect).\u002Ector(screenPoint1.x / (float) Screen.width, screenPoint2.y / (float) Screen.height, (screenPoint2.x - screenPoint1.x) / (float) Screen.width, (screenPoint1.y - screenPoint2.y) / (float) Screen.height);
      float num = this.fullSize * ((Rect) ref rect).height;
      if (Rect.op_Inequality(rect, this.mCam.rect))
        this.mCam.rect = rect;
      if ((double) this.mCam.orthographicSize != (double) num)
        this.mCam.orthographicSize = num;
      ((Behaviour) this.mCam).enabled = true;
    }
    else
      ((Behaviour) this.mCam).enabled = false;
  }
}
