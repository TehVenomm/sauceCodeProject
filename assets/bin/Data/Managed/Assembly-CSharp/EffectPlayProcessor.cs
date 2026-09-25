// Decompiled with JetBrains decompiler
// Type: EffectPlayProcessor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EffectPlayProcessor : MonoBehaviour
{
  public EffectPlayProcessor.EffectSetting[] effectSettings;

  public bool IsContainSetting(string setting_name)
  {
    if (string.IsNullOrEmpty(setting_name))
      return false;
    int index = 0;
    for (int length = this.effectSettings.Length; index < length; ++index)
    {
      if (this.effectSettings[index].name == setting_name)
        return true;
    }
    return false;
  }

  public List<EffectPlayProcessor.EffectSetting> GetSettings(string setting_name)
  {
    if (string.IsNullOrEmpty(setting_name))
      return (List<EffectPlayProcessor.EffectSetting>) null;
    List<EffectPlayProcessor.EffectSetting> effectSettingList = new List<EffectPlayProcessor.EffectSetting>();
    int index = 0;
    for (int length = this.effectSettings.Length; index < length; ++index)
    {
      if (this.effectSettings[index].name == setting_name)
        effectSettingList.Add(this.effectSettings[index]);
    }
    return effectSettingList.Count <= 0 ? (List<EffectPlayProcessor.EffectSetting>) null : effectSettingList;
  }

  public List<Transform> PlayEffect(string setting_name, Transform owner_node = null)
  {
    return this.PlayEffect(this.GetSettings(setting_name), owner_node);
  }

  public List<Transform> PlayEffect(
    List<EffectPlayProcessor.EffectSetting> settings,
    Transform owner_node = null)
  {
    if (settings == null)
      return (List<Transform>) null;
    List<Transform> transformList = new List<Transform>();
    int index = 0;
    for (int count = settings.Count; index < count; ++index)
    {
      Transform transform = this.PlayEffect(settings[index], owner_node);
      if (Object.op_Inequality((Object) transform, (Object) null))
        transformList.Add(transform);
    }
    return transformList.Count <= 0 ? (List<Transform>) null : transformList;
  }

  public Transform PlayEffect(EffectPlayProcessor.EffectSetting setting, Transform owner_node = null)
  {
    if (setting == null)
      return (Transform) null;
    if (string.IsNullOrEmpty(setting.effectName))
      return (Transform) null;
    if (Object.op_Equality((Object) owner_node, (Object) null))
      owner_node = ((Component) this).transform;
    Transform parent = !string.IsNullOrEmpty(setting.nodeName) ? Utility.Find(owner_node, setting.nodeName) : owner_node;
    if (Object.op_Equality((Object) parent, (Object) null))
      parent = owner_node;
    Transform effect = EffectManager.GetEffect(setting.effectName, parent);
    if (Object.op_Inequality((Object) effect, (Object) null))
    {
      effect.localPosition = setting.position;
      effect.localRotation = Quaternion.Euler(setting.rotation);
      float num = setting.scale;
      if ((double) num == 0.0)
        num = 1f;
      effect.localScale = Vector3.op_Multiply(Vector3.one, num);
    }
    return effect;
  }

  [Serializable]
  public class EffectSetting
  {
    [Tooltip("名前")]
    public string name;
    [Tooltip("エフェクト名")]
    public string effectName;
    [Tooltip("ノード名")]
    public string nodeName;
    [Tooltip("オフセット座標")]
    public Vector3 position = Vector3.zero;
    [Tooltip("回転")]
    public Vector3 rotation = Vector3.zero;
    [Tooltip("スケール")]
    public float scale = 1f;

    public EffectPlayProcessor.EffectSetting Clone()
    {
      return new EffectPlayProcessor.EffectSetting()
      {
        name = this.name,
        effectName = this.effectName,
        nodeName = this.nodeName,
        position = this.position,
        rotation = this.rotation,
        scale = this.scale
      };
    }
  }
}
