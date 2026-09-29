// Decompiled with JetBrains decompiler
// Type: UISonarGizmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UISonarGizmo : UIStatusGizmoBase
{
  [SerializeField]
  protected GameObject arrow;
  [SerializeField]
  protected UISprite statusSprite;
  [SerializeField]
  protected Vector3 offset;
  [SerializeField]
  [Tooltip("スクリーン横オフセット")]
  protected float screenSideOffset = 22f;
  [SerializeField]
  [Tooltip("スクリーン下オフセット")]
  protected float screenBottomOffset = 112f;
  private FieldSonarObject _sonar;
  protected Transform portalTransform;
  protected Transform arrowTransform;

  public FieldSonarObject sonar
  {
    get => this._sonar;
    set
    {
      this._sonar = value;
      if (Object.op_Inequality((Object) this._sonar, (Object) null))
      {
        ((Component) this).gameObject.SetActive(true);
        this.portalTransform = ((Component) value).transform;
        this.UpdateParam();
      }
      else
        ((Component) this).gameObject.SetActive(false);
    }
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    if (!Object.op_Inequality((Object) this.arrow, (Object) null))
      return;
    this.arrowTransform = this.arrow.transform;
  }

  protected override void UpdateParam()
  {
    if (Object.op_Equality((Object) this.sonar, (Object) null) || !((Component) this.sonar).gameObject.activeSelf)
    {
      this.SetActiveSafe(((Component) this.statusSprite).gameObject, false);
      this.SetActiveSafe(this.arrow, false);
    }
    else
    {
      Vector3 screenUiPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, Vector3.op_Addition(this.portalTransform.position, this.offset));
      this.screenZ = screenUiPosition.z;
      screenUiPosition.z = 0.0f;
      float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
      Vector3 vector3_1 = screenUiPosition;
      bool flag = false;
      float width = (float) Screen.width;
      if ((double) screenUiPosition.x < (double) this.screenSideOffset * (double) num)
      {
        screenUiPosition.x = this.screenSideOffset * num;
        flag = true;
      }
      else if ((double) screenUiPosition.x > (double) width - (double) this.screenSideOffset * (double) num)
      {
        screenUiPosition.x = width - this.screenSideOffset * num;
        flag = true;
      }
      if ((double) screenUiPosition.y < (double) this.screenBottomOffset * (double) num)
      {
        screenUiPosition.y = this.screenBottomOffset * num;
        flag = true;
      }
      if (flag)
      {
        this.SetActiveSafe(((Component) this.statusSprite).gameObject, true);
        this.SetActiveSafe(this.arrow, true);
        Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUiPosition);
        Vector3 vector3_2 = Vector3.op_Subtraction(this.transform.position, worldPoint);
        if ((double) ((Vector3) ref vector3_2).sqrMagnitude >= 1.9999999494757503E-05)
          this.transform.position = worldPoint;
        if (!Object.op_Inequality((Object) this.arrowTransform, (Object) null))
          return;
        Vector3 vector3_3 = Vector3.op_Subtraction(vector3_1, screenUiPosition);
        if (Vector3.op_Inequality(vector3_3, Vector3.zero))
          this.arrowTransform.eulerAngles = new Vector3(0.0f, 0.0f, 90f - Vector3.Angle(Vector3.right, vector3_3));
        else
          this.arrowTransform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
      }
      else
      {
        this.SetActiveSafe(((Component) this.statusSprite).gameObject, false);
        this.SetActiveSafe(this.arrow, false);
      }
    }
  }
}
