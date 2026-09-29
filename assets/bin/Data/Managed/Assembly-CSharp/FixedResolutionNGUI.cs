// Decompiled with JetBrains decompiler
// Type: FixedResolutionNGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FixedResolutionNGUI : MonoBehaviour
{
  private const float BASERATIO = 0.5625f;

  private void Start() => this.fixedNGUI();

  private void fixedNGUI()
  {
    float aspect = Camera.main.aspect;
    float num = aspect / (9f / 16f);
    Vector3 vector3 = Vector3.op_Multiply(Vector3.one, num);
    Debug.LogError((object) $"ratio{(object) num} aspect {(object) aspect} localScale {(object) vector3}");
    if ((double) aspect < 9.0 / 16.0)
      ((Component) this).transform.localScale = vector3;
    Debug.LogError((object) ("Local Scale " + (object) ((Component) this).transform.localScale));
  }
}
