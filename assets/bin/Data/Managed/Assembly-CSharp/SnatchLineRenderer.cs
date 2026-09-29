// Decompiled with JetBrains decompiler
// Type: SnatchLineRenderer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SnatchLineRenderer : MonoBehaviour
{
  private LineRenderer lineRenderer;

  private void Start()
  {
    Transform transform = ResourceUtility.Realizes((Object) MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.snatchLine, MonoBehaviourSingleton<StageObjectManager>.I._transform);
    if (Object.op_Equality((Object) transform, (Object) null))
      return;
    this.lineRenderer = ((Component) transform).GetComponent<LineRenderer>();
    if (!Object.op_Inequality((Object) this.lineRenderer, (Object) null))
      return;
    ((Renderer) this.lineRenderer).enabled = false;
  }

  private void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.lineRenderer, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this.lineRenderer).gameObject);
    this.lineRenderer = (LineRenderer) null;
  }

  public void SetVisible()
  {
    if (!Object.op_Inequality((Object) this.lineRenderer, (Object) null))
      return;
    ((Renderer) this.lineRenderer).enabled = true;
  }

  public void SetInvisible()
  {
    if (!Object.op_Inequality((Object) this.lineRenderer, (Object) null))
      return;
    ((Renderer) this.lineRenderer).enabled = false;
  }

  public void SetPositonStart(Vector3 pos)
  {
    if (!Object.op_Inequality((Object) this.lineRenderer, (Object) null))
      return;
    this.lineRenderer.SetPosition(0, pos);
  }

  public void SetPositionEnd(Vector3 pos)
  {
    if (!Object.op_Inequality((Object) this.lineRenderer, (Object) null))
      return;
    this.lineRenderer.SetPosition(1, pos);
  }
}
