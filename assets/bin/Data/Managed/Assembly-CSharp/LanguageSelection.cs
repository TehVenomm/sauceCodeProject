// Decompiled with JetBrains decompiler
// Type: LanguageSelection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (UIPopupList))]
[AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection : MonoBehaviour
{
  private UIPopupList mList;

  private void Awake()
  {
    this.mList = ((Component) this).GetComponent<UIPopupList>();
    this.Refresh();
  }

  private void Start()
  {
    EventDelegate.Add(this.mList.onChange, (EventDelegate.Callback) (() => Localization.language = UIPopupList.current.value));
  }

  public void Refresh()
  {
    if (!Object.op_Inequality((Object) this.mList, (Object) null) || Localization.knownLanguages == null)
      return;
    this.mList.Clear();
    int index = 0;
    for (int length = Localization.knownLanguages.Length; index < length; ++index)
      this.mList.items.Add(Localization.knownLanguages[index]);
    this.mList.value = Localization.language;
  }
}
