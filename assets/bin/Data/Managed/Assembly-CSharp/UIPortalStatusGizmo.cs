// Decompiled with JetBrains decompiler
// Type: UIPortalStatusGizmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIPortalStatusGizmo : UIStatusGizmoBase
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
  [SerializeField]
  protected UITweenCtrl addTween;
  [SerializeField]
  protected UITweenCtrl fullTween;
  private PortalObject _portal;
  protected Transform portalTransform;
  protected Transform arrowTransform;

  public PortalObject portal
  {
    get => this._portal;
    set
    {
      if (Object.op_Inequality((Object) this._portal, (Object) null))
        this._portal.uiGizmo = (UIPortalStatusGizmo) null;
      this._portal = value;
      if (Object.op_Inequality((Object) this._portal, (Object) null))
      {
        this._portal.uiGizmo = this;
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
    if (Object.op_Equality((Object) this.portal, (Object) null) || !((Component) this.portal).gameObject.activeSelf)
    {
      this.SetActiveSafe(((Component) this.statusSprite).gameObject, false);
      this.SetActiveSafe(this.arrow, false);
      this.SetActiveSafe(((Component) this.addTween).gameObject, false);
      this.SetActiveSafe(((Component) this.fullTween).gameObject, false);
    }
    else
    {
      if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyPlayerStatusGizmo)
      {
        if (SpecialDeviceManager.IsPortrait)
        {
          this.screenSideOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPortalGizmoScreenSideOffsetPortrait;
          this.screenBottomOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPortalGizmoScreenBottomOffsetPortrait;
        }
        else
        {
          this.screenSideOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPortalGizmoScreenSideOffsetLandscape;
          this.screenBottomOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPortalGizmoScreenBottomOffsetLandscape;
        }
      }
      Vector3 screenUiPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, Vector3.op_Addition(this.portalTransform.position, this.offset));
      this.screenZ = screenUiPosition.z;
      screenUiPosition.z = 0.0f;
      float num1 = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
      Vector3 vector3_1 = screenUiPosition;
      bool flag1 = false;
      float width = (float) Screen.width;
      if ((double) screenUiPosition.x < (double) this.screenSideOffset * (double) num1)
      {
        screenUiPosition.x = this.screenSideOffset * num1;
        flag1 = true;
      }
      else if ((double) screenUiPosition.x > (double) width - (double) this.screenSideOffset * (double) num1)
      {
        screenUiPosition.x = width - this.screenSideOffset * num1;
        flag1 = true;
      }
      if ((double) screenUiPosition.y < (double) this.screenBottomOffset * (double) num1)
      {
        screenUiPosition.y = this.screenBottomOffset * num1;
        flag1 = true;
      }
      if (flag1)
      {
        this.SetActiveSafe(((Component) this.statusSprite).gameObject, true);
        this.SetActiveSafe(this.arrow, true);
        Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUiPosition);
        Vector3 vector3_2 = Vector3.op_Subtraction(this.transform.position, worldPoint);
        if ((double) ((Vector3) ref vector3_2).sqrMagnitude >= 1.9999999494757503E-05)
          this.transform.position = worldPoint;
        if (Object.op_Inequality((Object) this.arrowTransform, (Object) null))
        {
          Vector3 vector3_3 = Vector3.op_Subtraction(vector3_1, screenUiPosition);
          this.arrowTransform.eulerAngles = !Vector3.op_Inequality(vector3_3, Vector3.zero) ? new Vector3(0.0f, 0.0f, 0.0f) : new Vector3(0.0f, 0.0f, 90f - Vector3.Angle(Vector3.right, vector3_3));
        }
        bool flag2 = false;
        if (Object.op_Inequality((Object) this.statusSprite, (Object) null))
        {
          switch (this.portal.viewType)
          {
            case PortalObject.VIEW_TYPE.NORMAL:
              this.statusSprite.spriteName = "Ingame_portal_vitalsign_blue";
              break;
            case PortalObject.VIEW_TYPE.NOT_TRAVELED:
              if (this.portal.isFull)
              {
                flag2 = true;
                this.statusSprite.spriteName = "Ingame_portal_vitalsign_red";
                break;
              }
              int num2 = (int) ((double) this.portal.nowPoint / (double) this.portal.maxPoint * (double) MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointRankNum);
              if (num2 >= MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointRankNum - 1)
                num2 = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointRankNum - 2;
              this.statusSprite.spriteName = $"Ingame_portal_vitalsign_red_{num2.ToString("D2")}";
              break;
            case PortalObject.VIEW_TYPE.TO_HOME:
              this.statusSprite.spriteName = "Ingame_portal_vitalsign_green";
              break;
            case PortalObject.VIEW_TYPE.TO_HARD_MAP:
              this.statusSprite.spriteName = "Ingame_portal_vitalsign_purple";
              break;
            case PortalObject.VIEW_TYPE.NOT_CLEAR_ORDER:
              this.statusSprite.spriteName = "Ingame_portal_vitalsign_not_clear_order";
              break;
          }
        }
        if (flag2)
        {
          this.SetActiveSafe(((Component) this.fullTween).gameObject, true);
          this.fullTween.Play();
        }
        else
          this.SetActiveSafe(((Component) this.fullTween).gameObject, false);
      }
      else
      {
        this.SetActiveSafe(((Component) this.statusSprite).gameObject, false);
        this.SetActiveSafe(this.arrow, false);
        this.SetActiveSafe(((Component) this.addTween).gameObject, false);
        this.SetActiveSafe(((Component) this.fullTween).gameObject, false);
      }
    }
  }

  public void OnGetPortalPoint()
  {
    this.SetActiveSafe(((Component) this.addTween).gameObject, true);
    this.addTween.Reset();
    this.addTween.Play(onFinished: (EventDelegate.Callback) (() => this.SetActiveSafe(((Component) this.fullTween).gameObject, false)));
  }
}
