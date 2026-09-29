// Decompiled with JetBrains decompiler
// Type: OnceManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class OnceManager : MonoBehaviourSingleton<OnceManager>
{
  private bool firstSendOnce = true;

  public OnceAllModel.Param result { get; private set; }

  public void SendGetOnce(Action<bool> callBack)
  {
    if (!this.firstSendOnce)
      callBack(true);
    else
      Protocol.Send<OnceAllModel.RequestSendForm, OnceAllModel>(OnceAllModel.URL, new OnceAllModel.RequestSendForm()
      {
        req_e = 1,
        req_i = 1,
        req_qi = 1,
        req_s = 1,
        req_ai = 1,
        req_ac = 1,
        d = NetworkNative.getUniqueDeviceId()
      }, (Action<OnceAllModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          this.result = ret.result;
          this.firstSendOnce = false;
        }
        callBack(flag);
      }));
  }
}
