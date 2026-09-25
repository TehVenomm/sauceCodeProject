// Decompiled with JetBrains decompiler
// Type: UICenterOnChildCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UICenterOnChildCtrl : MonoBehaviour
{
  public UICenterOnChild.OnCenterCallback onCenter;
  public SpringPanel.OnFinished onFinished;
  private UICenterOnChild centerOnChild;
  private float springStrength;
  private Transform reserveTarget;

  public static UICenterOnChildCtrl Get(GameObject go)
  {
    if (Object.op_Equality((Object) go.GetComponent<UICenterOnChild>(), (Object) null))
    {
      Log.Error("UICenterOnChild is not found.");
      return (UICenterOnChildCtrl) null;
    }
    UICenterOnChildCtrl centerOnChildCtrl = go.GetComponent<UICenterOnChildCtrl>();
    if (Object.op_Equality((Object) centerOnChildCtrl, (Object) null))
      centerOnChildCtrl = go.AddComponent<UICenterOnChildCtrl>();
    return centerOnChildCtrl;
  }

  public Transform lastTarget { get; private set; }

  private void Start()
  {
    if (Object.op_Equality((Object) this.centerOnChild, (Object) null))
    {
      this.centerOnChild = ((Component) this).GetComponent<UICenterOnChild>();
      if (Object.op_Equality((Object) this.centerOnChild, (Object) null))
      {
        Object.Destroy((Object) this);
        return;
      }
      this.springStrength = this.centerOnChild.springStrength;
      this.centerOnChild.onFinished = new SpringPanel.OnFinished(this.OnFinised);
      this.centerOnChild.onCenter = new UICenterOnChild.OnCenterCallback(this.OnCenter);
    }
    if (!Object.op_Inequality((Object) this.reserveTarget, (Object) null))
      return;
    this.Centering(this.reserveTarget, true);
    this.reserveTarget = (Transform) null;
  }

  private void OnCenter(GameObject go)
  {
    if (!Object.op_Inequality((Object) go.transform, (Object) this.lastTarget))
      return;
    this.lastTarget = go.transform;
    if (this.onCenter == null)
      return;
    this.onCenter(go);
  }

  private void OnFinised()
  {
    this.centerOnChild.springStrength = this.springStrength;
    if (this.onFinished == null)
      return;
    this.onFinished();
  }

  public void Centering(Transform target, bool is_instant = false)
  {
    if (Object.op_Equality((Object) this.centerOnChild, (Object) null))
    {
      this.reserveTarget = target;
    }
    else
    {
      if (is_instant)
      {
        this.centerOnChild.springStrength = 99999f;
        this.lastTarget = (Transform) null;
      }
      this.centerOnChild.CenterOn(target);
    }
  }
}
