// Decompiled with JetBrains decompiler
// Type: UIRenderQueueUpdater
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIRenderQueueUpdater : MonoBehaviour
{
  private const float OFFSET_GLOBAL_Z = 0.5f;
  [SerializeField]
  private UIWidget baseWidget;
  [SerializeField]
  private bool offsetBack;
  private Renderer _renderer;

  private void Awake()
  {
    this._renderer = ((Component) this).GetComponent<Renderer>();
    if (!Object.op_Inequality((Object) this._renderer, (Object) null) || !Object.op_Inequality((Object) this.baseWidget, (Object) null))
      return;
    this._renderer.enabled = false;
    this.baseWidget.onRender += new UIDrawCall.OnRenderCallback(this.OnRender);
    ((Component) this).transform.position = Vector3.op_Addition(((Component) this).transform.position, new Vector3(0.0f, 0.0f, ((Component) this.baseWidget).transform.position.z + (this.offsetBack ? 0.5f : -0.5f)));
  }

  private void OnRender(Material mat)
  {
    if (!Object.op_Inequality((Object) this._renderer, (Object) null))
      return;
    this._renderer.material.renderQueue = mat.renderQueue;
    this._renderer.enabled = ((Behaviour) this.baseWidget).enabled;
  }
}
