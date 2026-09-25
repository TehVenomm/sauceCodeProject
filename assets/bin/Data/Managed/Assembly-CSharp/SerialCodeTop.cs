// Decompiled with JetBrains decompiler
// Type: SerialCodeTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class SerialCodeTop : GameSection
{
  private SerialListModel.Param serialList;

  private void SendGetSerialCodeList(Action<bool> callback)
  {
    Protocol.Send<SerialListModel>(SerialListModel.URL, (Action<SerialListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.serialList = ret.result;
      }
      callback(flag);
    }));
  }

  private void SendInputSerialCode(int id, string code, Action<bool, string> callback)
  {
    Protocol.Send<SerialInputModel.RequestSendForm, SerialInputModel>(SerialInputModel.URL, new SerialInputModel.RequestSendForm()
    {
      id = id,
      code = code
    }, (Action<SerialInputModel>) (ret =>
    {
      bool flag = false;
      string str = string.Empty;
      if (ret.Error == Error.None)
      {
        flag = true;
        str = ret.result.message;
      }
      callback(flag, str);
    }));
  }

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    bool wait = true;
    this.SendGetSerialCodeList((Action<bool>) (b => wait = false));
    while (wait)
      yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.SetTable((Enum) SerialCodeTop.UI.TBL_LIST, "SerialCodeListItem", this.serialList.serials.Count, false, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      SerialListModel.Serials serial = this.serialList.serials[i];
      this.SetInput(t, (Enum) SerialCodeTop.UI.IPT_CODE, string.Empty, 64 /*0x40*/);
      this.SetLabelText(t, (Enum) SerialCodeTop.UI.LBL_NAME, serial.name);
      this.SetEvent(t, (Enum) SerialCodeTop.UI.BTN_SEND, "SEND", i);
    }));
  }

  private void OnQuery_SEND()
  {
    int eventData = (int) GameSection.GetEventData();
    Transform t = this.GetCtrl((Enum) SerialCodeTop.UI.TBL_LIST).Find(eventData.ToString());
    string inputValue = this.GetInputValue(t, (Enum) SerialCodeTop.UI.IPT_CODE);
    if (string.IsNullOrEmpty(inputValue))
    {
      GameSection.StopEvent();
    }
    else
    {
      GameSection.StayEvent();
      this.SendInputSerialCode(this.serialList.serials[eventData].serialId, inputValue, (Action<bool, string>) ((is_success, msg) =>
      {
        if (is_success)
        {
          GameSection.SetEventData((object) msg);
          this.SetInputValue(t, (Enum) SerialCodeTop.UI.IPT_CODE, string.Empty);
        }
        GameSection.ResumeEvent(is_success);
      }));
    }
  }

  private enum UI
  {
    TBL_LIST,
    LBL_NAME,
    IPT_CODE,
    BTN_SEND,
  }
}
