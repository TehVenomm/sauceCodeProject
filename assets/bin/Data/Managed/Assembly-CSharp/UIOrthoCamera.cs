// Decompiled with JetBrains decompiler
// Type: UIOrthoCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("NGUI/UI/Orthographic Camera")]
public class UIOrthoCamera : MonoBehaviour
{
  private Camera mCam;
  private Transform mTrans;

  private void Start()
  {
    this.mCam = ((Component) this).GetComponent<Camera>();
    this.mTrans = ((Component) this).transform;
    this.mCam.orthographic = true;
  }

  private void Update()
  {
    Rect rect1 = this.mCam.rect;
    float num1 = ((Rect) ref rect1).yMin * (float) Screen.height;
    Rect rect2 = this.mCam.rect;
    float num2 = (float) (((double) ((Rect) ref rect2).yMax * (double) Screen.height - (double) num1) * 0.5) * this.mTrans.lossyScale.y;
    if (Mathf.Approximately(this.mCam.orthographicSize, num2))
      return;
    this.mCam.orthographicSize = num2;
  }
}
