// Decompiled with JetBrains decompiler
// Type: UIInGameEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIInGameEffect : MonoBehaviour
{
  [SerializeField]
  protected UITweenCtrl tweenCtrl;

  private void Start()
  {
    this.tweenCtrl.Reset();
    this.tweenCtrl.Play();
  }

  private void Update()
  {
    int index = 0;
    for (int length = this.tweenCtrl.tweens.Length; index < length; ++index)
    {
      if (this.tweenCtrl.tweens[index].style != UITweener.Style.Loop && ((Behaviour) this.tweenCtrl.tweens[index]).isActiveAndEnabled)
        return;
    }
    Object.Destroy((Object) ((Component) this).gameObject);
  }
}
