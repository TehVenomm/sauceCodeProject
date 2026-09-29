// Decompiled with JetBrains decompiler
// Type: EffectInfoComponent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EffectInfoComponent : MonoBehaviour
{
  [Tooltip("ループエンドによる削除設定")]
  public bool destroyLoopEnd;
  private AudioObject loopAudioObject;
  [Tooltip("WEATHER CHANGE用のカメラとのZ方向のoffset")]
  public float CameraPosLinkOffsetZ;

  public void SetLoopAudioObject(AudioObject ao)
  {
    if (Object.op_Inequality((Object) this.loopAudioObject, (Object) null))
      this.loopAudioObject.Stop();
    this.loopAudioObject = ao;
  }
}
