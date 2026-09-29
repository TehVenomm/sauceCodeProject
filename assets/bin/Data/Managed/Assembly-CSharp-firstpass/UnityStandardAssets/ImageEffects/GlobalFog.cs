// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.GlobalFog
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using System;
using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Rendering/Global Fog")]
internal class GlobalFog : PostEffectsBase
{
  [Tooltip("Apply distance-based fog?")]
  public bool distanceFog = true;
  [Tooltip("Exclude far plane pixels from distance-based fog? (Skybox or clear color)")]
  public bool excludeFarPixels = true;
  [Tooltip("Distance fog is based on radial distance from camera when checked")]
  public bool useRadialDistance;
  [Tooltip("Apply height-based fog?")]
  public bool heightFog = true;
  [Tooltip("Fog top Y coordinate")]
  public float height = 1f;
  [Range(0.001f, 10f)]
  public float heightDensity = 2f;
  [Tooltip("Push fog away from the camera by this amount")]
  public float startDistance;
  public Shader fogShader;
  private Material fogMaterial;

  public override bool CheckResources()
  {
    this.CheckSupport(true);
    this.fogMaterial = this.CheckShaderAndCreateMaterial(this.fogShader, this.fogMaterial);
    if (!this.isSupported)
      this.ReportAutoDisable();
    return this.isSupported;
  }

  [ImageEffectOpaque]
  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources() || !this.distanceFog && !this.heightFog)
    {
      Graphics.Blit((Texture) source, destination);
    }
    else
    {
      Camera component = ((Component) this).GetComponent<Camera>();
      Transform transform = ((Component) component).transform;
      float nearClipPlane = component.nearClipPlane;
      float farClipPlane = component.farClipPlane;
      float fieldOfView = component.fieldOfView;
      float aspect = component.aspect;
      Matrix4x4 identity = Matrix4x4.identity;
      float num1 = fieldOfView * 0.5f;
      Vector3 vector3_1 = Vector3.op_Multiply(Vector3.op_Multiply(Vector3.op_Multiply(transform.right, nearClipPlane), Mathf.Tan(num1 * ((float) Math.PI / 180f))), aspect);
      Vector3 vector3_2 = Vector3.op_Multiply(Vector3.op_Multiply(transform.up, nearClipPlane), Mathf.Tan(num1 * ((float) Math.PI / 180f)));
      Vector3 vector3_3 = Vector3.op_Addition(Vector3.op_Subtraction(Vector3.op_Multiply(transform.forward, nearClipPlane), vector3_1), vector3_2);
      float num2 = ((Vector3) ref vector3_3).magnitude * farClipPlane / nearClipPlane;
      ((Vector3) ref vector3_3).Normalize();
      vector3_3 = Vector3.op_Multiply(vector3_3, num2);
      Vector3 vector3_4 = Vector3.op_Addition(Vector3.op_Addition(Vector3.op_Multiply(transform.forward, nearClipPlane), vector3_1), vector3_2);
      ((Vector3) ref vector3_4).Normalize();
      Vector3 vector3_5 = Vector3.op_Multiply(vector3_4, num2);
      Vector3 vector3_6 = Vector3.op_Subtraction(Vector3.op_Addition(Vector3.op_Multiply(transform.forward, nearClipPlane), vector3_1), vector3_2);
      ((Vector3) ref vector3_6).Normalize();
      Vector3 vector3_7 = Vector3.op_Multiply(vector3_6, num2);
      Vector3 vector3_8 = Vector3.op_Subtraction(Vector3.op_Subtraction(Vector3.op_Multiply(transform.forward, nearClipPlane), vector3_1), vector3_2);
      ((Vector3) ref vector3_8).Normalize();
      Vector3 vector3_9 = Vector3.op_Multiply(vector3_8, num2);
      ((Matrix4x4) ref identity).SetRow(0, Vector4.op_Implicit(vector3_3));
      ((Matrix4x4) ref identity).SetRow(1, Vector4.op_Implicit(vector3_5));
      ((Matrix4x4) ref identity).SetRow(2, Vector4.op_Implicit(vector3_7));
      ((Matrix4x4) ref identity).SetRow(3, Vector4.op_Implicit(vector3_9));
      Vector3 position = transform.position;
      float num3 = position.y - this.height;
      float num4 = (double) num3 <= 0.0 ? 1f : 0.0f;
      float num5 = this.excludeFarPixels ? 1f : 2f;
      this.fogMaterial.SetMatrix("_FrustumCornersWS", identity);
      this.fogMaterial.SetVector("_CameraWS", Vector4.op_Implicit(position));
      this.fogMaterial.SetVector("_HeightParams", new Vector4(this.height, num3, num4, this.heightDensity * 0.5f));
      this.fogMaterial.SetVector("_DistanceParams", new Vector4(-Mathf.Max(this.startDistance, 0.0f), num5, 0.0f, 0.0f));
      FogMode fogMode = RenderSettings.fogMode;
      float fogDensity = RenderSettings.fogDensity;
      float fogStartDistance = RenderSettings.fogStartDistance;
      float fogEndDistance = RenderSettings.fogEndDistance;
      bool flag = fogMode == 1;
      float num6 = flag ? fogEndDistance - fogStartDistance : 0.0f;
      float num7 = (double) Mathf.Abs(num6) > 9.9999997473787516E-05 ? 1f / num6 : 0.0f;
      Vector4 vector4;
      vector4.x = fogDensity * 1.2011224f;
      vector4.y = fogDensity * 1.442695f;
      vector4.z = flag ? -num7 : 0.0f;
      vector4.w = flag ? fogEndDistance * num7 : 0.0f;
      this.fogMaterial.SetVector("_SceneFogParams", vector4);
      this.fogMaterial.SetVector("_SceneFogMode", new Vector4((float) fogMode, this.useRadialDistance ? 1f : 0.0f, 0.0f, 0.0f));
      int passNr = !this.distanceFog || !this.heightFog ? (!this.distanceFog ? 2 : 1) : 0;
      GlobalFog.CustomGraphicsBlit(source, destination, this.fogMaterial, passNr);
    }
  }

  private static void CustomGraphicsBlit(
    RenderTexture source,
    RenderTexture dest,
    Material fxMaterial,
    int passNr)
  {
    RenderTexture.active = dest;
    fxMaterial.SetTexture("_MainTex", (Texture) source);
    GL.PushMatrix();
    GL.LoadOrtho();
    fxMaterial.SetPass(passNr);
    GL.Begin(7);
    GL.MultiTexCoord2(0, 0.0f, 0.0f);
    GL.Vertex3(0.0f, 0.0f, 3f);
    GL.MultiTexCoord2(0, 1f, 0.0f);
    GL.Vertex3(1f, 0.0f, 2f);
    GL.MultiTexCoord2(0, 1f, 1f);
    GL.Vertex3(1f, 1f, 1f);
    GL.MultiTexCoord2(0, 0.0f, 1f);
    GL.Vertex3(0.0f, 1f, 0.0f);
    GL.End();
    GL.PopMatrix();
  }
}
