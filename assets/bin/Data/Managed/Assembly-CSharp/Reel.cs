// Decompiled with JetBrains decompiler
// Type: Reel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class Reel : GameSection
{
  private Reel.InitData initData;
  private Reel.RecvData[] recvData;

  public override void Initialize()
  {
    this.initData = GameSection.GetEventData() as Reel.InitData;
    this.recvData = new Reel.RecvData[this.initData.digit.Length];
    for (int index = 0; index < this.recvData.Length; ++index)
    {
      this.recvData[index] = new Reel.RecvData();
      Reel.RecvData recvData = this.recvData[index];
      recvData.digit = this.initData.digit[index];
      recvData.selectValue = 0;
      recvData.ancer = 0;
    }
    base.Initialize();
  }

  public override void UpdateUI()
  {
    int width1 = this.GetWidth((Enum) Reel.UI.OBJ_REEL_SIZE_BASE);
    int digits = 0;
    Array.ForEach<int>(this.initData.digit, (Action<int>) (data => digits += data > 0 ? data : 1));
    int reel_list_width_base = width1 / digits;
    this.SetTable((Enum) Reel.UI.TBL_REEL, "ReelList", this.initData.digit.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (this.initData.digit[i] <= 0)
      {
        this.SetActive(t, false);
      }
      else
      {
        this.SetActive(t, true);
        float width = (float) (reel_list_width_base * this.initData.digit[i]);
        this.SetWidth(t, (int) width);
        UIScrollView component = this.GetComponent<UIScrollView>(t, (Enum) Reel.UI.SCR_LIST);
        Vector4 baseClipRegion = component.panel.baseClipRegion;
        baseClipRegion.z = width;
        component.panel.baseClipRegion = baseClipRegion;
        this.GetComponent<UIGrid>(t, (Enum) Reel.UI.GRD_REEL_LIST).cellWidth = (float) reel_list_width_base;
        this.SetGrid(t, (Enum) Reel.UI.GRD_REEL_LIST, "ReelListItem", 10, false, (Action<int, Transform, bool>) ((i2, t2, is_recycle2) =>
        {
          this.GetComponent<UIGrid>(t2, (Enum) Reel.UI.GRD_REEL_LIST_ITEM).cellWidth = (float) reel_list_width_base;
          this.SetWidth(t2, (int) width);
          this.SetGrid(t2, (Enum) Reel.UI.GRD_REEL_LIST_ITEM, "ReelListText", this.initData.digit[i], false, (Action<int, Transform, bool>) ((i3, t3, is_recycle3) =>
          {
            int num = i3 == 0 ? i2 : 0;
            this.SetLabelText(t3, num.ToString());
            this.SetWidth(t3, (int) width);
          }));
        }));
        int num = digits;
        int index1 = 0;
        for (int index2 = i; index1 < index2; ++index1)
          num -= this.initData.digit[index1];
        int index3 = this.initData.initValue / (int) Mathf.Pow(10f, (float) (num - 1)) % 10;
        this.SetCenter(t, (Enum) Reel.UI.GRD_REEL_LIST, index3, true);
        this.SetCenterOnChildFunc(t, (Enum) Reel.UI.GRD_REEL_LIST, new UICenterOnChild.OnCenterCallback(this.OnCenter));
      }
    }));
  }

  public void OnCenter(GameObject go)
  {
    int result1 = 0;
    if (!int.TryParse(((Object) go).name, out result1))
      return;
    int result2 = 0;
    if (!int.TryParse(((Object) go.transform.parent.parent.parent).name, out result2) || result2 >= this.recvData.Length)
      return;
    int digits = 0;
    Array.ForEach<int>(this.initData.digit, (Action<int>) (data => digits += data > 0 ? data : 1));
    int num = digits;
    int index1 = 0;
    for (int index2 = result2; index1 < index2; ++index1)
      num -= this.initData.digit[index1];
    this.recvData[result2].selectValue = result1;
    this.recvData[result2].ancer = result1 * (int) Mathf.Pow(10f, (float) (num - 1));
  }

  public void OnQuery_DECISION()
  {
    if (this.initData.callback == null)
      return;
    this.initData.callback(this.recvData);
  }

  private enum UI
  {
    TBL_REEL,
    OBJ_REEL_SIZE_BASE,
    GRD_REEL_LIST,
    SCR_LIST,
    GRD_REEL_LIST_ITEM,
  }

  public class InitData
  {
    public int[] digit;
    public int initValue;
    public Action<Reel.RecvData[]> callback;

    public InitData(int[] _digit, int _init_value = 0, Action<Reel.RecvData[]> _callback = null)
    {
      this.digit = _digit;
      this.initValue = _init_value;
      this.callback = _callback;
    }
  }

  public class RecvData
  {
    public int digit;
    public int selectValue;
    public int ancer;
  }
}
