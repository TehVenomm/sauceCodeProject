// Decompiled with JetBrains decompiler
// Type: UIPhaseNumber
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UIPhaseNumber : MonoBehaviour
{
  [SerializeField]
  protected UILabel label;
  [SerializeField]
  protected UITweener[] anims;

  protected void Awake()
  {
    this.label.text = $"Phase {(ValueType) (uint) ((int) MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex + 1)} / {MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum()}";
  }

  private void Update()
  {
    int index = 0;
    for (int length = this.anims.Length; index < length; ++index)
    {
      if (((Behaviour) this.anims[index]).enabled)
        return;
    }
    Object.Destroy((Object) ((Component) this).gameObject);
  }
}
