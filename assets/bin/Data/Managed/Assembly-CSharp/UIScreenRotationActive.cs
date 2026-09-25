// Decompiled with JetBrains decompiler
// Type: UIScreenRotationActive
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIScreenRotationActive : UIScreenRotationHandler
{
  [SerializeField]
  private GameObject target;
  [SerializeField]
  private bool activeIfPortrait;

  protected override void OnScreenRotate(bool is_portrait)
  {
    this.target.SetActive(this.activeIfPortrait == is_portrait);
  }
}
