// Decompiled with JetBrains decompiler
// Type: StatusScreenShotViewerSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class StatusScreenShotViewerSetting : GameSection
{
  private StatusScreenShotViewerSetting.UI[] toggle = new StatusScreenShotViewerSetting.UI[5]
  {
    StatusScreenShotViewerSetting.UI.BTN_NAME,
    StatusScreenShotViewerSetting.UI.BTN_LEVEL,
    StatusScreenShotViewerSetting.UI.BTN_ID,
    StatusScreenShotViewerSetting.UI.BTN_COMMENT,
    StatusScreenShotViewerSetting.UI.BTN_TITLE
  };
  private int filter;

  public override void Initialize()
  {
    base.Initialize();
    this.filter = (int) GameSection.GetEventData();
  }

  public override void UpdateUI()
  {
    for (int event_data = 0; event_data < this.toggle.Length; ++event_data)
    {
      bool flag = (this.filter & 1 << event_data) != 0;
      this.SetEvent((Enum) this.toggle[event_data], "FILTER", event_data);
      this.SetToggle(this.GetCtrl((Enum) this.toggle[event_data]).parent, flag);
    }
  }

  private void OnQuery_FILTER()
  {
    int _index;
    bool _is_enable;
    this.OnQueryEvent_Filter(out _index, out _is_enable);
    this.SetToggle(this.GetCtrl((Enum) this.toggle[_index]).parent, _is_enable);
  }

  private void OnQuery_OK()
  {
    GameSection.SetEventData((object) this.filter);
    GameSaveData.instance.SetScreenShotUIFilterType(this.filter);
  }

  private void OnQueryEvent_Filter(out int _index, out bool _is_enable)
  {
    _index = (int) GameSection.GetEventData();
    int num = 1 << _index;
    if ((this.filter & num) == 0)
    {
      _is_enable = true;
      this.filter += num;
    }
    else
    {
      _is_enable = false;
      this.filter -= num;
    }
  }

  public enum UI
  {
    BTN_NAME,
    BTN_LEVEL,
    BTN_ID,
    BTN_COMMENT,
    BTN_TITLE,
  }
}
