// Decompiled with JetBrains decompiler
// Type: UIButtonActivate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate : MonoBehaviour
{
  public GameObject target;
  public bool state = true;

  private void OnClick()
  {
    if (!Object.op_Inequality((Object) this.target, (Object) null))
      return;
    NGUITools.SetActive(this.target, this.state);
  }
}
