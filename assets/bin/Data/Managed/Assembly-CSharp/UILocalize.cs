// Decompiled with JetBrains decompiler
// Type: UILocalize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[RequireComponent(typeof (UIWidget))]
[AddComponentMenu("NGUI/UI/Localize")]
public class UILocalize : MonoBehaviour
{
  public string key;
  private bool mStarted;

  public string value
  {
    set
    {
      if (string.IsNullOrEmpty(value))
        return;
      UIWidget component = ((Component) this).GetComponent<UIWidget>();
      UILabel uiLabel = component as UILabel;
      UISprite uiSprite = component as UISprite;
      if (Object.op_Inequality((Object) uiLabel, (Object) null))
      {
        UIInput inParents = NGUITools.FindInParents<UIInput>(((Component) uiLabel).gameObject);
        if (Object.op_Inequality((Object) inParents, (Object) null) && Object.op_Equality((Object) inParents.label, (Object) uiLabel))
          inParents.defaultText = value;
        else
          uiLabel.SetTextOnly(value);
      }
      else
      {
        if (!Object.op_Inequality((Object) uiSprite, (Object) null))
          return;
        UIButton inParents = NGUITools.FindInParents<UIButton>(((Component) uiSprite).gameObject);
        if (Object.op_Inequality((Object) inParents, (Object) null) && Object.op_Equality((Object) inParents.tweenTarget, (Object) ((Component) uiSprite).gameObject))
          inParents.normalSprite = value;
        uiSprite.spriteName = value;
        uiSprite.MakePixelPerfect();
      }
    }
  }

  private void OnEnable()
  {
    if (!this.mStarted || !Localization.dictionary.ContainsKey(this.key))
      return;
    this.OnLocalize();
  }

  private void Start()
  {
    this.mStarted = true;
    this.OnLocalize();
  }

  private void OnLocalize()
  {
    if (!Localization.dictionary.ContainsKey(this.key))
      return;
    if (string.IsNullOrEmpty(this.key))
    {
      UILabel component = ((Component) this).GetComponent<UILabel>();
      if (Object.op_Inequality((Object) component, (Object) null))
        this.key = component.text;
    }
    if (string.IsNullOrEmpty(this.key))
      return;
    this.value = Localization.Get(this.key);
  }
}
