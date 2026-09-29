// Decompiled with JetBrains decompiler
// Type: InformationDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
public class InformationDialog : WebViewDialog
{
  protected IEnumerator GetDiff(Action<bool> call_back)
  {
    bool wait = true;
    bool isSuccess = true;
    MonoBehaviourSingleton<PresentManager>.I.SendGetPresentTotalCount((Action<bool>) (b =>
    {
      wait = false;
      isSuccess = b;
    }));
    while (wait)
      yield return (object) null;
    if (MonoBehaviourSingleton<DeliveryManager>.I.IsExistNotClearDelivery(new DELIVERY_CONDITION_TYPE[1]
    {
      DELIVERY_CONDITION_TYPE.VIEW_NEWS_LINK
    }))
    {
      wait = true;
      DeliveryManager i = MonoBehaviourSingleton<DeliveryManager>.I;
      List<DELIVERY_CONDITION_TYPE> condiditionTypeList = new List<DELIVERY_CONDITION_TYPE>();
      condiditionTypeList.Add(DELIVERY_CONDITION_TYPE.VIEW_NEWS_LINK);
      Action<bool, DeliveryGetClearStatusModel.Param> call_back1 = (Action<bool, DeliveryGetClearStatusModel.Param>) ((b, param) =>
      {
        if (b)
          MonoBehaviourSingleton<DeliveryManager>.I.UpdateClearStatuses(param.clearStatusDelivery);
        wait = false;
        isSuccess &= b;
      });
      i.SendGetClearStatusList(condiditionTypeList, call_back1);
    }
    while (wait)
      yield return (object) null;
    call_back(isSuccess);
  }

  private void OnQuery_CLOSE()
  {
    GameSection.StayEvent();
    this.StartCoroutine(this.GetDiff((Action<bool>) (b => GameSection.ResumeEvent(b))));
  }
}
