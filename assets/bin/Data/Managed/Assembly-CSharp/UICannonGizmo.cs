// Decompiled with JetBrains decompiler
// Type: UICannonGizmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UICannonGizmo : UIStatusGizmoBase
{
  [SerializeField]
  protected UISprite arrowSprite;
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
  private FieldGimmickCannonObject _owner;
  protected Transform targetTransform;
  protected Transform arrowTransform;
  private Self myPlayer;
  private UIPanel panel;
  private bool isPlayAnimation;

  public FieldGimmickCannonObject owner
  {
    get => this._owner;
    set
    {
      this._owner = value;
      if (Object.op_Inequality((Object) this._owner, (Object) null))
      {
        ((Component) this).gameObject.SetActive(true);
        this.targetTransform = ((Component) value).transform;
        this.UpdateParam();
      }
      else
        ((Component) this).gameObject.SetActive(false);
    }
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    if (Object.op_Inequality((Object) this.arrowSprite, (Object) null))
      this.arrowTransform = ((Component) this.arrowSprite).transform.parent;
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.playerList != null)
      this.myPlayer = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Find((Predicate<StageObject>) (x => x is Self)) as Self;
    this.panel = ((Component) this).GetComponent<UIPanel>();
    this.isPlayAnimation = false;
  }

  protected override void UpdateParam()
  {
    if (Object.op_Equality((Object) this.owner, (Object) null) || !((Component) this.owner).gameObject.activeSelf)
    {
      this.SetSpriteEnable(false);
      this.isPlayAnimation = false;
    }
    else
    {
      Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      if (Object.op_Inequality((Object) boss, (Object) null))
      {
        if (boss.IsValidShield())
        {
          if (Object.op_Inequality((Object) this.myPlayer, (Object) null))
          {
            if (this.myPlayer.IsOnCannonMode())
            {
              this.SetSpriteEnable(false);
              this.isPlayAnimation = false;
              return;
            }
            this.SetSpriteEnable(true);
          }
          if (!this.isPlayAnimation)
          {
            this.isPlayAnimation = true;
            UITweenCtrl component = ((Component) this).GetComponent<UITweenCtrl>();
            component.Reset();
            component.Play();
          }
        }
        else
        {
          this.SetSpriteEnable(false);
          this.isPlayAnimation = false;
          return;
        }
      }
      Vector3 screenUiPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, Vector3.op_Addition(this.targetTransform.position, this.offset));
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
        this.SetSpriteEnable(true);
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
        this.SetSpriteEnable(false);
    }
  }

  private void SetSpriteEnable(bool enable)
  {
    if (!Object.op_Inequality((Object) this.panel, (Object) null))
      return;
    ((Behaviour) this.panel).enabled = enable;
  }
}
