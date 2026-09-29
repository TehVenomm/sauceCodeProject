// Decompiled with JetBrains decompiler
// Type: Picker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class Picker : GameSection
{
  private Picker.DESC desc;
  private int selectIndex;

  public override void Initialize()
  {
    this.selectIndex = 0;
    this.desc = GameSection.GetEventData() as Picker.DESC;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetGrid((Enum) Picker.UI.GRD_PICKER, "PickerItem", this.desc.text.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetLabelText(t, (Enum) Picker.UI.LBL_PICKER, this.desc.text[i])));
    UIWrapContent component = this.GetComponent<UIWrapContent>((Enum) Picker.UI.GRD_PICKER);
    if (Object.op_Inequality((Object) component, (Object) null))
      ((Behaviour) component).enabled = this.desc.enableLoop;
    this.SetCenterOnChildFunc((Enum) Picker.UI.GRD_PICKER, new UICenterOnChild.OnCenterCallback(this.OnCenter));
    this.SetCenter((Enum) Picker.UI.GRD_PICKER, this.selectIndex);
  }

  public void OnCenter(GameObject go)
  {
    int result;
    if (!int.TryParse(((Object) go).name, out result))
      return;
    this.selectIndex = result;
  }

  private void OnQuery_DECISION()
  {
    GameSection.SetEventData((object) this.desc.text[this.selectIndex]);
  }

  public class DESC
  {
    public string[] text;
    public bool enableLoop;

    public DESC(string[] _texts, bool _enable_loop = true)
    {
      this.text = _texts;
      this.enableLoop = _enable_loop;
    }
  }

  private enum UI
  {
    GRD_PICKER,
    LBL_PICKER,
  }
}
