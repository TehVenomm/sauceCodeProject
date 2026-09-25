// Decompiled with JetBrains decompiler
// Type: GrabCommand
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#nullable disable
public class GrabCommand : MonoBehaviour
{
  public const float TEXTURE_REDUCE_RATE_FOR_IPHONE5 = 0.5f;
  public const int DEPTH_BUFFER = 0;
  private Camera _camera;
  private CommandBuffer _commandBuffer;
  private RenderTexture renderTexture;
  private CameraEvent cameraEvent = (CameraEvent) 11;
  private List<GameObject> referenceObjects = new List<GameObject>(10);

  public RenderTexture useRenderTexture(GameObject parent)
  {
    if (!this.referenceObjects.Contains(parent))
      this.referenceObjects.Add(parent);
    return this.renderTexture;
  }

  public void releaseRenderTexture(GameObject parent)
  {
    this.referenceObjects.Remove(parent);
    if (this.referenceObjects.Count != 0)
      return;
    Object.Destroy((Object) this);
  }

  private void OnDestroy()
  {
    if (Object.op_Inequality((Object) this.renderTexture, (Object) null))
    {
      RenderTexture.ReleaseTemporary(this.renderTexture);
      this.renderTexture = (RenderTexture) null;
    }
    if (!Object.op_Inequality((Object) this._camera, (Object) null) || this._commandBuffer == null)
      return;
    this._camera.RemoveCommandBuffer(this.cameraEvent, this._commandBuffer);
  }

  private void CreateTexture()
  {
    if (Object.op_Inequality((Object) this.renderTexture, (Object) null))
      return;
    this.renderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height);
  }

  public void ApplyCommandBuffer(CameraEvent _cameraEvent)
  {
    this.cameraEvent = _cameraEvent;
    this._camera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    this.CreateTexture();
    this._commandBuffer = new CommandBuffer();
    this._commandBuffer.name = "Grab texture";
    this._commandBuffer.Blit(RenderTargetIdentifier.op_Implicit((BuiltinRenderTextureType) 1), RenderTargetIdentifier.op_Implicit((Texture) this.renderTexture));
    this._camera.AddCommandBuffer(this.cameraEvent, this._commandBuffer);
  }
}
