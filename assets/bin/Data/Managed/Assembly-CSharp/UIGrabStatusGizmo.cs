// Decompiled with JetBrains decompiler
// Type: UIGrabStatusGizmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIGrabStatusGizmo : UIStatusGizmoBase
{
  private const float OFFSET_Y = -1f;
  [SerializeField]
  protected UIHGauge gaugeUI;
  private Enemy _targetEnemy;
  private TargetPoint _targetPoint;

  public Enemy targetEnemy
  {
    get => this._targetEnemy;
    set
    {
      this._targetEnemy = value;
      if (Object.op_Inequality((Object) this._targetEnemy, (Object) null))
      {
        ((Component) this).gameObject.SetActive(true);
        this.UpdateParam();
      }
      else
        ((Component) this).gameObject.SetActive(false);
    }
  }

  public TargetPoint targetPoint
  {
    get => this._targetPoint;
    set
    {
      this._targetPoint = value;
      if (Object.op_Inequality((Object) this._targetPoint, (Object) null))
      {
        ((Component) this).gameObject.SetActive(true);
        this.UpdateParam();
      }
      else
        ((Component) this).gameObject.SetActive(false);
    }
  }

  protected override void UpdateParam()
  {
    if (Object.op_Equality((Object) this.targetEnemy, (Object) null) || Object.op_Equality((Object) this.targetPoint, (Object) null))
      return;
    Vector3 targetPoint = this.targetPoint.GetTargetPoint();
    targetPoint.y += -1f;
    Vector3 vector3_1 = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(targetPoint);
    this.screenZ = vector3_1.z;
    if ((double) vector3_1.z < 0.0)
      vector3_1 = Vector3.op_Multiply(vector3_1, -1f);
    vector3_1.z = 0.0f;
    Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(vector3_1);
    Vector3 vector3_2 = Vector3.op_Subtraction(this.transform.position, worldPoint);
    if ((double) ((Vector3) ref vector3_2).sqrMagnitude >= 1.9999999494757503E-05)
      this.transform.position = worldPoint;
    if (!Object.op_Inequality((Object) this.gaugeUI, (Object) null))
      return;
    if (this.targetEnemy.IsValidGrabHp)
    {
      if (!((Behaviour) this.gaugeUI).isActiveAndEnabled)
        ((Component) this.gaugeUI).gameObject.SetActive(true);
      float percent = (float) (int) this.targetEnemy.GrabHp / (float) (int) this.targetEnemy.GrabHpMax;
      if ((double) percent < 0.0)
        percent = 0.0f;
      if ((double) this.gaugeUI.nowPercent == (double) percent)
        return;
      this.gaugeUI.SetPercent(percent);
    }
    else
    {
      if (!((Behaviour) this.gaugeUI).isActiveAndEnabled)
        return;
      ((Component) this.gaugeUI).gameObject.SetActive(false);
    }
  }
}
