// Decompiled with JetBrains decompiler
// Type: RenderTargetSetter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

#nullable disable
public class RenderTargetSetter : MonoBehaviour
{
  [SerializeField]
  private RenderTargetSetter.TextureSetInfo[] infos;
  [SerializeField]
  private CameraEvent cameraEvent = (CameraEvent) 11;
  private RenderTexture renderTexture;
  private GrabCommand grabCommand;

  private void OnDestroy()
  {
    this.renderTexture = (RenderTexture) null;
    if (!Object.op_Inequality((Object) this.grabCommand, (Object) null))
      return;
    this.grabCommand.releaseRenderTexture(((Component) this).gameObject);
  }

  private IEnumerator Start()
  {
    if (this.infos != null)
    {
      while (!MonoBehaviourSingleton<AppMain>.IsValid() || Object.op_Equality((Object) MonoBehaviourSingleton<AppMain>.I.mainCamera, (Object) null))
        yield return (object) null;
      this.grabCommand = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).gameObject.GetComponent<GrabCommand>();
      if (Object.op_Equality((Object) this.grabCommand, (Object) null))
      {
        this.grabCommand = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).gameObject.AddComponent<GrabCommand>();
        this.grabCommand.ApplyCommandBuffer(this.cameraEvent);
      }
      this.renderTexture = this.grabCommand.useRenderTexture(((Component) this).gameObject);
      for (int index1 = 0; index1 < this.infos.Length; ++index1)
      {
        if (!Object.op_Equality((Object) this.infos[index1].targetRenderer, (Object) null) && !string.IsNullOrEmpty(this.infos[index1].texturePropertyName))
        {
          RenderTargetSetter.TextureSetInfo info = this.infos[index1];
          Renderer targetRenderer = info.targetRenderer;
          for (int index2 = 0; index2 < targetRenderer.sharedMaterials.Length; ++index2)
          {
            if (targetRenderer.sharedMaterials[index2].HasProperty(info.texturePropertyName))
              targetRenderer.sharedMaterials[index2].SetTexture(info.texturePropertyName, (Texture) this.renderTexture);
          }
        }
      }
    }
  }

  [Serializable]
  public class TextureSetInfo
  {
    public Renderer targetRenderer;
    public string texturePropertyName;
  }
}
