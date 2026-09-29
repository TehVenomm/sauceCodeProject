// Decompiled with JetBrains decompiler
// Type: UINameInput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UINameInput : UIInput
{
  private bool isCaretDirection;

  private void OnDisable() => base.Cleanup();

  protected override void Cleanup()
  {
    if (Object.op_Implicit((Object) this.mHighlight))
      ((Behaviour) this.mHighlight).enabled = false;
    if (!Object.op_Implicit((Object) this.mCaret) || this.isCaretDirection)
      return;
    ((Behaviour) this.mCaret).enabled = false;
  }

  private void SetColor(Color col)
  {
    this.mDefaultColor = col;
    this.activeTextColor = col;
    ((Component) this).GetComponent<UILabel>().color = col;
  }

  public void SetName(string _name)
  {
    if (string.IsNullOrEmpty(_name))
      return;
    this.value = _name;
  }

  public void CreateCaret(bool is_enable)
  {
    if (Object.op_Equality((Object) this.mBlankTex, (Object) null))
    {
      this.mBlankTex = new Texture2D(2, 2, (TextureFormat) 5, false);
      for (int index1 = 0; index1 < 2; ++index1)
      {
        for (int index2 = 0; index2 < 2; ++index2)
          this.mBlankTex.SetPixel(index2, index1, Color.white);
      }
      this.mBlankTex.Apply();
    }
    if (Object.op_Equality((Object) this.mCaret, (Object) null))
    {
      this.mCaret = NGUITools.AddWidget<UITexture>(this.label.cachedGameObject);
      ((Object) this.mCaret).name = "Input Caret";
      this.mCaret.mainTexture = (Texture) this.mBlankTex;
      this.mCaret.fillGeometry = false;
      this.mCaret.pivot = this.label.pivot;
      this.mCaret.SetAnchor(this.label.cachedTransform);
    }
    ((Behaviour) this.mCaret).enabled = is_enable;
    this.InActiveName();
    this.Init();
  }

  public void ActiveName()
  {
    this.SetColor(Color.white);
    this.isCaretDirection = false;
    this.mNextBlink = RealTime.time;
  }

  public void InActiveName()
  {
    UILabel component = ((Component) this).GetComponent<UILabel>();
    string str = this.value;
    string defaultText = this.defaultText;
    component.text = defaultText;
    this.value = string.Empty;
    this.SetInActiveNameColor();
    if (Object.op_Inequality((Object) this.mCaret, (Object) null) && string.IsNullOrEmpty(str))
      this.label.PrintOverlay(0, 0, this.mCaret.geometry, (UIGeometry) null, this.caretColor, this.selectionColor);
    this.isCaretDirection = true;
    this.mNextBlink = RealTime.time;
  }

  public void SetInActiveNameColor() => this.SetColor(Color.gray);

  protected override void Update()
  {
    base.Update();
    if (!this.isCaretDirection || this.isSelected || !Object.op_Inequality((Object) this.mCaret, (Object) null) || (double) this.mNextBlink >= (double) RealTime.time)
      return;
    this.mNextBlink = RealTime.time + 0.5f;
    ((Behaviour) this.mCaret).enabled = !((Behaviour) this.mCaret).enabled;
  }
}
