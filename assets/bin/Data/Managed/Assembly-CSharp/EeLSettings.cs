// Decompiled with JetBrains decompiler
// Type: EeLSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System.IO;
using System.Text;
using UnityEngine;

#nullable disable
public static class EeLSettings
{
  public static void Startup()
  {
    // ISSUE: method pointer
    rymFXManager.ResourceLoadDelegate = new rymFXManager.ResourceLoadFunc((object) null, __methodptr(OnResourceLoad));
    // ISSUE: method pointer
    rymFXManager.PlaySoundDelegate = new rymFXManager.SoundFunc((object) null, __methodptr(PlaySound));
    // ISSUE: method pointer
    rymFXManager.InitFxDelegate = new rymFXManager.InitFxFunc((object) null, __methodptr(InitFx));
    // ISSUE: method pointer
    rymFXManager.QueryDestroyFxDelegate = new rymFXManager.QueryDestroyFxFunc((object) null, __methodptr(OnQueryDestroyFx));
    rymFXManager.MeshBounds = new Bounds(Vector3.zero, new Vector3(100000f, 100000f, 100000f));
    rymFXManager.EnableLog = false;
    // ISSUE: method pointer
    rymFXManager.GetShaderDelegate = new rymFXManager.GetShaderFunc((object) null, __methodptr(GetShader));
    rymFXTrail.ComplementMode = 0;
    rymFXManager.ClearPoolObjects();
    rymTPool<rymXorShift>.Precreate(32 /*0x20*/);
    rymTPool<rymFXParticle2.EmitParam>.Precreate(16 /*0x10*/);
    rymTPool<rymFXParticle2.PtclWorkBlock>.Precreate(32 /*0x20*/);
    rymTPool<rymList<rymFXParticle2.PtclWorkBlock>>.Precreate(32 /*0x20*/);
    rymTPool<rymFXParticle2.PtclWork>.Precreate(512 /*0x0200*/);
    rymTPool<rymFX4KeyAnimValue.WorkAccess>.Precreate(8);
    rymTPool<rymFXParticleForceBase.ApplyParam>.Precreate(8);
    rymTPool<rymFXParticle2ChildPlug>.Precreate(32 /*0x20*/);
    rymTPool<rymFXTrail.rymFXTrailPoint>.Precreate(64 /*0x40*/);
    rymTPool<rymList<rymFXTrail.rymFXTrailPoint>>.Precreate(32 /*0x20*/);
    rymTPool<rymFXTrail.rymFXTrailParam>.Precreate(8);
    rymTPool<rymFX4KeyAnimValue>.Precreate(32 /*0x20*/);
    rymTPool<rymFX4KeyAnimValue.Param>.Precreate(128 /*0x80*/);
    rymTPool<rymMemReader>.Precreate(1);
    rymTPool<StringBuilder>.Precreate(1);
    rymTPool<rymList<string>>.Precreate(16 /*0x10*/);
    rymTPool<rymList<float>>.Precreate(512 /*0x0200*/);
    rymTPool<rymList<int>>.Precreate(16 /*0x10*/);
    rymTPool<rymFXWorkFixPos>.Precreate(8);
    rymTPool<rymFXWorkFixRot>.Precreate(8);
    rymTPool<rymFXWorkFixScale>.Precreate(8);
    rymTPool<rymFXWorkFixColor>.Precreate(8);
    rymTPool<rymFXWorkLinearPos>.Precreate(8);
    rymTPool<rymFXWorkLinearRot>.Precreate(8);
    rymTPool<rymFXWorkLinearScale>.Precreate(8);
    rymTPool<rymFXWorkLinearColor>.Precreate(8);
    rymTPool<rymList<rymFXWork>>.Precreate(32 /*0x20*/);
    rymTPool<rymFXSpeedForce>.Precreate(8);
    rymTPool<rymFXAccelForce>.Precreate(8);
    rymTPool<rymFXShakeForce>.Precreate(8);
    rymTPool<rymFXScrewForce>.Precreate(8);
    rymTPool<rymFXAbsorbForce>.Precreate(8);
    rymTPool<rymList<rymFXParticleForceBase>>.Precreate(32 /*0x20*/);
    rymTPool<rymFXSoundTrigger>.Precreate(8);
    rymTPool<rymList<rymFXTrigger>>.Precreate(8);
    rymTPool<rymFXPlug>.Precreate(32 /*0x20*/);
    rymTPool<rymList<rymFXPlug>>.Precreate(16 /*0x10*/);
    rymTPool<rymList<int>>.Precreate(16 /*0x10*/);
    rymTPool<rymFXSoundInfo>.Precreate(4);
    rymTPool<rymList<rymFXSoundInfo>>.Precreate(4);
    rymTPool<rymFXParticle2>.Precreate(16 /*0x10*/);
    rymTPool<rymFXSprite>.Precreate(16 /*0x10*/);
    rymTPool<rymFXTrail>.Precreate(8);
    rymTPool<rymList<rymFXObject>>.Precreate(16 /*0x10*/);
    rymTPool<rymFXParticle2ChildWork>.Precreate(16 /*0x10*/);
    rymTPool<rymFXParticle2ChildParam>.Precreate(16 /*0x10*/);
    rymTPool<rymList<rymFXObjectBase>>.Precreate(32 /*0x20*/);
    rymTPool<rymFXParticle2.Param>.Precreate(16 /*0x10*/);
    object[] objArray1 = new object[96 /*0x60*/];
    int num1 = 0;
    int num2 = 0;
    for (; num1 < 32 /*0x20*/; ++num1)
    {
      rymList<Vector2> rymList1 = rymTPool<rymList<Vector2>>.Get();
      ((rymListBase) rymList1).Capacity = 400;
      rymList<Vector3> rymList2 = rymTPool<rymList<Vector3>>.Get();
      ((rymListBase) rymList2).Capacity = 400;
      rymList<Color> rymList3 = rymTPool<rymList<Color>>.Get();
      ((rymListBase) rymList3).Capacity = 400;
      object[] objArray2 = objArray1;
      int index1 = num2;
      int num3 = index1 + 1;
      rymList<Vector2> rymList4 = rymList1;
      objArray2[index1] = (object) rymList4;
      object[] objArray3 = objArray1;
      int index2 = num3;
      int num4 = index2 + 1;
      rymList<Vector3> rymList5 = rymList2;
      objArray3[index2] = (object) rymList5;
      object[] objArray4 = objArray1;
      int index3 = num4;
      num2 = index3 + 1;
      rymList<Color> rymList6 = rymList3;
      objArray4[index3] = (object) rymList6;
    }
    int num5 = 0;
    int num6 = 0;
    for (; num5 < 32 /*0x20*/; ++num5)
    {
      object[] objArray5 = objArray1;
      int index4 = num6;
      int num7 = index4 + 1;
      rymList<Vector2> rymList7 = objArray5[index4] as rymList<Vector2>;
      object[] objArray6 = objArray1;
      int index5 = num7;
      int num8 = index5 + 1;
      rymList<Vector3> rymList8 = objArray6[index5] as rymList<Vector3>;
      object[] objArray7 = objArray1;
      int index6 = num8;
      num6 = index6 + 1;
      rymList<Color> rymList9 = objArray7[index6] as rymList<Color>;
      rymTPool<rymList<Vector2>>.Release(ref rymList7);
      rymTPool<rymList<Vector3>>.Release(ref rymList8);
      rymTPool<rymList<Color>>.Release(ref rymList9);
    }
    rymTPool<rymList<int>>.poolCountLimit = 32 /*0x20*/;
    rymFXManager.EnableMaterialCache = false;
  }

  private static void OnResourceLoad(rymFX.ResourceLoadWork work)
  {
    if (Object.op_Equality((Object) work.fx, (Object) null))
      return;
    ResourceLink component = ((Component) work.fx).GetComponent<ResourceLink>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      rymList<string> textureNameList = work.fx.GetTextureNameList();
      if (textureNameList != null)
      {
        int index = 0;
        for (int size = ((rymListBase) textureNameList).size; index < size; ++index)
        {
          string withoutExtension = Path.GetFileNameWithoutExtension(textureNameList[index]);
          if (!string.IsNullOrEmpty(withoutExtension))
            work.textures[index] = component.Get<Texture>(withoutExtension);
        }
      }
      else
        Debug.LogWarning((object) ((Object) work.fx).name);
    }
    work.fx.ResourceLoadComplete();
  }

  private static void PlaySound(rymFX fx, rymFXSoundInfo info)
  {
    if (!((Behaviour) fx).enabled || info.loop && Object.op_Inequality((Object) info.audio_source, (Object) null) || string.IsNullOrEmpty(info.clip_name))
      return;
    ResourceLink component1 = ((Component) fx).GetComponent<ResourceLink>();
    if (!Object.op_Inequality((Object) component1, (Object) null))
      return;
    string withoutExtension = Path.GetFileNameWithoutExtension(info.clip_name);
    AudioClip clip = component1.Get<AudioClip>(withoutExtension);
    if (Object.op_Inequality((Object) clip, (Object) null))
    {
      AudioObject ao = SoundManager.PlaySE(clip, info.loop, fx._transform);
      if (!Object.op_Inequality((Object) ao, (Object) null) || !info.loop)
        return;
      EffectInfoComponent component2 = ((Component) fx).GetComponent<EffectInfoComponent>();
      if (!Object.op_Inequality((Object) component2, (Object) null))
        return;
      component2.SetLoopAudioObject(ao);
    }
    else
      Log.Error(LOG.RESOURCE, "{0} is not found. ({1})", (object) withoutExtension, (object) ((Object) fx).name);
  }

  private static void InitFx(rymFX fx, bool binary)
  {
    if (((Component) fx).gameObject.layer == 5)
      return;
    SceneSettingsManager.ApplyEffect(fx, false);
  }

  private static Shader GetShader(string name) => ResourceUtility.FindShader(name);

  private static bool OnQueryDestroyFx(rymFX fx)
  {
    return !MonoBehaviourSingleton<EffectManager>.IsValid() || !MonoBehaviourSingleton<EffectManager>.I.StockOrDestroy(((Component) fx).gameObject, false);
  }
}
