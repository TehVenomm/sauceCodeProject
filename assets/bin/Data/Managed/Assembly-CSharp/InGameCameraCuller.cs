// Decompiled with JetBrains decompiler
// Type: InGameCameraCuller
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (Camera))]
public class InGameCameraCuller : MonoBehaviourSingleton<InGameCameraCuller>
{
  private static readonly Plane[] _planes = new Plane[6];
  private Camera TargetCamera;
  private Matrix4x4 CamMatrix;

  public bool IsUpdated { get; private set; }

  private void Start() => this.TargetCamera = ((Component) this).GetComponent<Camera>();

  private void Update()
  {
    this.IsUpdated = false;
    if (!((Component) this).transform.hasChanged)
      return;
    this.CamMatrix = Matrix4x4.op_Multiply(this.TargetCamera.projectionMatrix, this.TargetCamera.worldToCameraMatrix);
    this.SetFrustumPlanes(this.CamMatrix);
    ((Component) this).transform.hasChanged = false;
    this.IsUpdated = true;
  }

  public void SetFrustumPlanes(Matrix4x4 worldProjectionMatrix)
  {
    GeometryUtility.CalculateFrustumPlanes(worldProjectionMatrix, InGameCameraCuller._planes);
  }

  public bool IsVisible(Bounds bound)
  {
    return GeometryUtility.TestPlanesAABB(InGameCameraCuller._planes, bound);
  }
}
