// Decompiled with JetBrains decompiler
// Type: StampAppeal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class StampAppeal : UIBehaviour
{
  private Transform rootPosition;
  private bool isAdjustment;

  private void Update()
  {
    if (!Object.op_Inequality((Object) this.rootPosition, (Object) null))
      return;
    this.SetPosition();
  }

  public void View(int stampId, Transform root, bool isAdjustmentPos)
  {
    this.rootPosition = root;
    this.isAdjustment = isAdjustmentPos;
    this.StartCoroutine(this.DoDisplayChatStamp(stampId));
  }

  private IEnumerator DoDisplayChatStamp(int stampId)
  {
    this.SetActive((Enum) StampAppeal.UI.OBJ_TWEEN_ROOT, false);
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lostamp = loadingQueue.LoadChatStamp(stampId, true);
    yield return (object) loadingQueue.Wait();
    if (!Object.op_Equality(lostamp.loadedObject, (Object) null))
    {
      this.SetTexture((Enum) StampAppeal.UI.TXT_STAMP, (Texture) (lostamp.loadedObject as Texture2D));
      this.SetActive((Enum) StampAppeal.UI.OBJ_TWEEN_ROOT, true);
      TweenScale component = ((Component) this.GetCtrl((Enum) StampAppeal.UI.OBJ_TWEEN_ROOT)).GetComponent<TweenScale>();
      component.SetOnFinished(new EventDelegate.Callback(this.OnFinish));
      component.ResetToBeginning();
      component.PlayForward();
    }
  }

  private void OnFinish()
  {
    this.SetActive((Enum) StampAppeal.UI.OBJ_TWEEN_ROOT, false);
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
      ((Component) this).transform.position = worldPoint;
      this.SetActive((Enum) StampAppeal.UI.OBJ_TWEEN_ROOT, true);
    }
    else
      this.SetActive((Enum) StampAppeal.UI.OBJ_TWEEN_ROOT, false);
  }

  public enum UI
  {
    OBJ_TWEEN_ROOT,
    SPR_STAMP_BG,
    TXT_STAMP,
  }
}
