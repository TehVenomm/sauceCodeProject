// Decompiled with JetBrains decompiler
// Type: UIBlurWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIBlurWindow : MonoBehaviour
{
  private Material mat;

  private void Start()
  {
    if (Object.op_Equality((Object) this.mat, (Object) null))
    {
      Renderer component = ((Component) this).GetComponent<Renderer>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      this.mat = component.material;
    }
    Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    if (Object.op_Equality((Object) mainCamera, (Object) null))
      return;
    RenderTargetCacher component1 = ((Component) mainCamera).GetComponent<RenderTargetCacher>();
    if (Object.op_Equality((Object) component1, (Object) null))
      return;
    this.mat.mainTexture = (Texture) component1.GetTexture();
  }

  private void Update() => this.Start();
}
