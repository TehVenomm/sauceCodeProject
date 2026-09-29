// Decompiled with JetBrains decompiler
// Type: NPCFacial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class NPCFacial : MonoBehaviour
{
  public Renderer faceRenderer;
  public Material eyeMaterial;
  public Texture[] eyeTextures;
  public Material mouthMaterial;
  public Texture[] mouthTextures;
  private NPCFacial.TYPE _eyeType;
  private NPCFacial.TYPE _mouthType;
  private Vector3 lastAnimValue;
  private NPCFacial.TYPE lastAnimEyeType;
  private NPCFacial.TYPE lastAnimMouthType;
  private bool enableEyeBlick;
  private float eyeBlinkTime;

  public NPCFacial.TYPE eyeType
  {
    get => this._eyeType;
    set
    {
      this._eyeType = value;
      this.ResetEyeBlinkTime();
      this.SetTexture(this.eyeMaterial, this.eyeTextures, this._eyeType);
    }
  }

  public NPCFacial.TYPE mouthType
  {
    get => this._mouthType;
    set
    {
      this._mouthType = value;
      this.SetTexture(this.mouthMaterial, this.mouthTextures, this._mouthType);
    }
  }

  public bool enableAnim { get; set; }

  public Transform animNode { get; set; }

  public Action<NPCFacial.TYPE> animChangeEyeCallback { get; set; }

  public Action<NPCFacial.TYPE> animChangeMouthCallback { get; set; }

  private void Awake()
  {
    if (!Object.op_Inequality((Object) this.faceRenderer, (Object) null))
      return;
    Material[] materials = this.faceRenderer.materials;
    if (Object.op_Inequality((Object) this.eyeMaterial, (Object) null))
      this.eyeMaterial = Array.Find<Material>(materials, (Predicate<Material>) (o => ((Object) o).name.StartsWith(((Object) this.eyeMaterial).name)));
    if (Object.op_Inequality((Object) this.mouthMaterial, (Object) null))
      this.mouthMaterial = Array.Find<Material>(materials, (Predicate<Material>) (o => ((Object) o).name.StartsWith(((Object) this.mouthMaterial).name)));
    if (Object.op_Inequality((Object) this.eyeMaterial, (Object) null) && Object.op_Inequality((Object) this.eyeTextures[1], (Object) null))
    {
      this.enableEyeBlick = true;
      this.ResetEyeBlinkTime();
    }
    this.enableAnim = true;
  }

  private void Update()
  {
    this.UpdateAnim();
    this.UpdateEyeBlink();
  }

  private void UpdateAnim()
  {
    if (Object.op_Equality((Object) this.animNode, (Object) null) || !this.enableAnim)
      return;
    Vector3 localPosition = this.animNode.localPosition;
    NPCFacial.TYPE type1 = (NPCFacial.TYPE) ((double) localPosition.y * 100.0 + 1.0 / 1000.0);
    NPCFacial.TYPE type2 = (NPCFacial.TYPE) ((double) localPosition.z * 100.0 + 1.0 / 1000.0);
    if (this.lastAnimEyeType != type1 && (double) Mathf.Abs(this.lastAnimValue.y - localPosition.y) < 9.9999997473787516E-05)
    {
      this.lastAnimEyeType = type1;
      if (this.animChangeEyeCallback != null)
        this.animChangeEyeCallback(type1);
      else
        this.eyeType = type1;
    }
    if (this.lastAnimMouthType != type2 && (double) Mathf.Abs(this.lastAnimValue.z - localPosition.z) < 9.9999997473787516E-05)
    {
      this.lastAnimMouthType = type2;
      if (this.animChangeMouthCallback != null)
        this.animChangeMouthCallback(type2);
      else
        this.mouthType = type2;
    }
    this.lastAnimValue = localPosition;
  }

  private void UpdateEyeBlink()
  {
    if (Object.op_Equality((Object) this.faceRenderer, (Object) null) || !this.enableEyeBlick || this.eyeType != NPCFacial.TYPE.NORMAL)
      return;
    this.eyeBlinkTime -= Time.deltaTime;
    if ((double) this.eyeBlinkTime > 0.0)
      return;
    if (Object.op_Inequality((Object) this.eyeMaterial.mainTexture, (Object) this.eyeTextures[1]))
    {
      this.SetTexture(this.eyeMaterial, this.eyeTextures, NPCFacial.TYPE.CLOSE);
      this.eyeBlinkTime = Random.Range(0.1f, 0.3f);
    }
    else
    {
      this.SetTexture(this.eyeMaterial, this.eyeTextures, this._eyeType);
      this.ResetEyeBlinkTime();
    }
  }

  private void SetTexture(Material material, Texture[] textures, NPCFacial.TYPE type)
  {
    if (textures == null || textures.Length == 0)
      return;
    int index = (int) type;
    if (index < 0 || index >= textures.Length || Object.op_Equality((Object) textures[index], (Object) null))
      index = 0;
    Texture texture = textures[index];
    if (Object.op_Equality((Object) texture, (Object) null))
      return;
    material.mainTexture = texture;
  }

  private void ResetEyeBlinkTime() => this.eyeBlinkTime = Random.Range(3f, 6f);

  public enum TYPE
  {
    NORMAL,
    CLOSE,
    ANGER,
    SAD,
    JOY,
    HALF,
    SURPRISED,
  }
}
