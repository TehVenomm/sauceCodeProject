// Decompiled with JetBrains decompiler
// Type: ChatAppeal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ChatAppeal : UIBehaviour
{
  private Transform rootPosition;
  private bool isAdjustment;

  private void Update()
  {
    if (!Object.op_Inequality((Object) this.rootPosition, (Object) null))
      return;
    this.SetPosition();
  }

  public void View(string text, Transform root, bool isAdjustmentPos)
  {
    this.rootPosition = root;
    this.isAdjustment = isAdjustmentPos;
    this.SetActive((Enum) ChatAppeal.UI.OBJ_TWEEN_ROOT, true);
    this.SetLabelText((Enum) ChatAppeal.UI.LBL_CHAT, text);
    TweenScale component = ((Component) this.GetCtrl((Enum) ChatAppeal.UI.OBJ_TWEEN_ROOT)).GetComponent<TweenScale>();
    component.SetOnFinished(new EventDelegate.Callback(this.OnFinish));
    component.ResetToBeginning();
    component.PlayForward();
  }

  private void OnFinish()
  {
    this.SetActive((Enum) ChatAppeal.UI.OBJ_TWEEN_ROOT, false);
    this.rootPosition = (Transform) null;
  }

  private void SetPosition()
  {
    Vector3 vector3 = this.rootPosition.position;
    if (this.isAdjustment)
      vector3 = Vector3.op_Addition(vector3, new Vector3(0.0f, 2.5f, 0.0f));
    Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(vector3));
    if ((double) worldPoint.z >= 0.0)
    {
      worldPoint.z = 0.0f;
      this.GetCtrl((Enum) ChatAppeal.UI.OBJ_TWEEN_ROOT).position = worldPoint;
      this.SetActive((Enum) ChatAppeal.UI.OBJ_TWEEN_ROOT, true);
    }
    else
      this.SetActive((Enum) ChatAppeal.UI.OBJ_TWEEN_ROOT, false);
  }

  public enum UI
  {
    OBJ_TWEEN_ROOT,
    SPR_CHAT_BG,
    LBL_CHAT,
  }
}
