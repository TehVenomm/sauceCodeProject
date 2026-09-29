// Decompiled with JetBrains decompiler
// Type: UIEnemyStatusGizmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIEnemyStatusGizmo : UIStatusGizmoBase
{
  [SerializeField]
  protected UIHGauge gaugeUI;
  [SerializeField]
  protected UILabel nameLabel;
  [SerializeField]
  protected UITexture dropTarget;
  [SerializeField]
  protected UIHGauge gaugeAegisUI;
  private Enemy _targetEnemy;
  private float hight;

  public Enemy targetEnemy
  {
    get => this._targetEnemy;
    set
    {
      this._targetEnemy = value;
      if (Object.op_Inequality((Object) this._targetEnemy, (Object) null))
      {
        ((Component) this).gameObject.SetActive(true);
        this.hight = this._targetEnemy.uiHeight;
        if (this._targetEnemy.enemyTableData != null)
          this.hight *= this._targetEnemy.enemyTableData.modelScale;
        if (Object.op_Inequality((Object) this.nameLabel, (Object) null))
          this.nameLabel.text = $"{this._targetEnemy.charaName} Lv.{this._targetEnemy.enemyLevel}";
        this.SetTargetIcon((Texture) null);
        this.UpdateParam();
      }
      else
        ((Component) this).gameObject.SetActive(false);
    }
  }

  protected override void UpdateParam()
  {
    if (Object.op_Equality((Object) this.targetEnemy, (Object) null))
      return;
    Vector3 position = this.targetEnemy._position;
    position.y += this.hight;
    Vector3 vector3_1 = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(position);
    this.screenZ = vector3_1.z;
    if ((double) vector3_1.z < 0.0)
      vector3_1 = Vector3.op_Multiply(vector3_1, -1f);
    vector3_1.z = 0.0f;
    Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(vector3_1);
    Vector3 vector3_2 = Vector3.op_Subtraction(this.transform.position, worldPoint);
    if ((double) ((Vector3) ref vector3_2).sqrMagnitude >= 1.9999999494757503E-05)
      this.transform.position = worldPoint;
    if (Object.op_Inequality((Object) this.gaugeUI, (Object) null))
    {
      float percent = (float) this.targetEnemy.hpShow / (float) this.targetEnemy.hpMax;
      if ((double) percent < 0.0)
        percent = 0.0f;
      if ((double) this.gaugeUI.nowPercent != (double) percent)
        this.gaugeUI.SetPercent(percent);
    }
    if (!Object.op_Inequality((Object) this.gaugeAegisUI, (Object) null))
      return;
    float aegisPercent = this.targetEnemy.GetAegisPercent();
    bool flag = (double) aegisPercent > 0.0;
    if ((double) this.gaugeAegisUI.nowPercent != (double) aegisPercent)
      this.gaugeAegisUI.SetPercent(aegisPercent);
    if (((Component) this.gaugeAegisUI).gameObject.activeSelf == flag)
      return;
    ((Component) this.gaugeAegisUI).gameObject.SetActive(flag);
  }

  public void SetTargetIcon(Texture texture) => this.dropTarget.mainTexture = texture;
}
