// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.PostEffectsHelper
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using System;
using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
internal class PostEffectsHelper : MonoBehaviour
{
  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    Debug.Log((object) "OnRenderImage in Helper called ...");
  }

  private static void DrawLowLevelPlaneAlignedWithCamera(
    float dist,
    RenderTexture source,
    RenderTexture dest,
    Material material,
    Camera cameraForProjectionMatrix)
  {
    RenderTexture.active = dest;
    material.SetTexture("_MainTex", (Texture) source);
    bool flag = true;
    GL.PushMatrix();
    GL.LoadIdentity();
    GL.LoadProjectionMatrix(cameraForProjectionMatrix.projectionMatrix);
    float num1 = (float) ((double) cameraForProjectionMatrix.fieldOfView * 0.5 * (Math.PI / 180.0));
    float num2 = Mathf.Cos(num1) / Mathf.Sin(num1);
    double aspect = (double) cameraForProjectionMatrix.aspect;
    float num3 = (float) (aspect / -(double) num2);
    float num4 = (float) aspect / num2;
    float num5 = (float) (1.0 / -(double) num2);
    float num6 = 1f / num2;
    float num7 = 1f;
    float num8 = num3 * (dist * num7);
    float num9 = num4 * (dist * num7);
    float num10 = num5 * (dist * num7);
    float num11 = num6 * (dist * num7);
    float num12 = -dist;
    for (int index = 0; index < material.passCount; ++index)
    {
      material.SetPass(index);
      GL.Begin(7);
      float num13;
      float num14;
      if (flag)
      {
        num13 = 1f;
        num14 = 0.0f;
      }
      else
      {
        num13 = 0.0f;
        num14 = 1f;
      }
      GL.TexCoord2(0.0f, num13);
      GL.Vertex3(num8, num10, num12);
      GL.TexCoord2(1f, num13);
      GL.Vertex3(num9, num10, num12);
      GL.TexCoord2(1f, num14);
      GL.Vertex3(num9, num11, num12);
      GL.TexCoord2(0.0f, num14);
      GL.Vertex3(num8, num11, num12);
      GL.End();
    }
    GL.PopMatrix();
  }

  private static void DrawBorder(RenderTexture dest, Material material)
  {
    RenderTexture.active = dest;
    bool flag = true;
    GL.PushMatrix();
    GL.LoadOrtho();
    for (int index = 0; index < material.passCount; ++index)
    {
      material.SetPass(index);
      float num1;
      float num2;
      if (flag)
      {
        num1 = 1f;
        num2 = 0.0f;
      }
      else
      {
        num1 = 0.0f;
        num2 = 1f;
      }
      double num3 = 0.0;
      float num4 = (float) (0.0 + 1.0 / ((double) ((Texture) dest).width * 1.0));
      float num5 = 0.0f;
      float num6 = 1f;
      GL.Begin(7);
      GL.TexCoord2(0.0f, num1);
      GL.Vertex3((float) num3, num5, 0.1f);
      GL.TexCoord2(1f, num1);
      GL.Vertex3(num4, num5, 0.1f);
      GL.TexCoord2(1f, num2);
      GL.Vertex3(num4, num6, 0.1f);
      GL.TexCoord2(0.0f, num2);
      GL.Vertex3((float) num3, num6, 0.1f);
      double num7 = 1.0 - 1.0 / ((double) ((Texture) dest).width * 1.0);
      float num8 = 1f;
      float num9 = 0.0f;
      float num10 = 1f;
      GL.TexCoord2(0.0f, num1);
      GL.Vertex3((float) num7, num9, 0.1f);
      GL.TexCoord2(1f, num1);
      GL.Vertex3(num8, num9, 0.1f);
      GL.TexCoord2(1f, num2);
      GL.Vertex3(num8, num10, 0.1f);
      GL.TexCoord2(0.0f, num2);
      GL.Vertex3((float) num7, num10, 0.1f);
      double num11 = 0.0;
      float num12 = 1f;
      float num13 = 0.0f;
      float num14 = (float) (0.0 + 1.0 / ((double) ((Texture) dest).height * 1.0));
      GL.TexCoord2(0.0f, num1);
      GL.Vertex3((float) num11, num13, 0.1f);
      GL.TexCoord2(1f, num1);
      GL.Vertex3(num12, num13, 0.1f);
      GL.TexCoord2(1f, num2);
      GL.Vertex3(num12, num14, 0.1f);
      GL.TexCoord2(0.0f, num2);
      GL.Vertex3((float) num11, num14, 0.1f);
      double num15 = 0.0;
      float num16 = 1f;
      float num17 = (float) (1.0 - 1.0 / ((double) ((Texture) dest).height * 1.0));
      float num18 = 1f;
      GL.TexCoord2(0.0f, num1);
      GL.Vertex3((float) num15, num17, 0.1f);
      GL.TexCoord2(1f, num1);
      GL.Vertex3(num16, num17, 0.1f);
      GL.TexCoord2(1f, num2);
      GL.Vertex3(num16, num18, 0.1f);
      GL.TexCoord2(0.0f, num2);
      GL.Vertex3((float) num15, num18, 0.1f);
      GL.End();
    }
    GL.PopMatrix();
  }

  private static void DrawLowLevelQuad(
    float x1,
    float x2,
    float y1,
    float y2,
    RenderTexture source,
    RenderTexture dest,
    Material material)
  {
    RenderTexture.active = dest;
    material.SetTexture("_MainTex", (Texture) source);
    bool flag = true;
    GL.PushMatrix();
    GL.LoadOrtho();
    for (int index = 0; index < material.passCount; ++index)
    {
      material.SetPass(index);
      GL.Begin(7);
      float num1;
      float num2;
      if (flag)
      {
        num1 = 1f;
        num2 = 0.0f;
      }
      else
      {
        num1 = 0.0f;
        num2 = 1f;
      }
      GL.TexCoord2(0.0f, num1);
      GL.Vertex3(x1, y1, 0.1f);
      GL.TexCoord2(1f, num1);
      GL.Vertex3(x2, y1, 0.1f);
      GL.TexCoord2(1f, num2);
      GL.Vertex3(x2, y2, 0.1f);
      GL.TexCoord2(0.0f, num2);
      GL.Vertex3(x1, y2, 0.1f);
      GL.End();
    }
    GL.PopMatrix();
  }
}
