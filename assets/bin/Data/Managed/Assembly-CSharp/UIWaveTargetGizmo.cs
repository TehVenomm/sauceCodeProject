// Decompiled with JetBrains decompiler
// Type: UIWaveTargetGizmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UIWaveTargetGizmo : UIStatusGizmoBase
{
  [SerializeField]
  protected GameObject nearUI;
  [SerializeField]
  protected UIHGauge gaugeUI;
  [SerializeField]
  protected UISprite gaugeSpr;
  [SerializeField]
  protected GameObject farUI;
  [SerializeField]
  protected GameObject arrowUI;
  [SerializeField]
  protected GameObject vitalUI;
  [SerializeField]
  protected SimplePingPongAlpha vitalAddAnim;
  [SerializeField]
  protected UISprite vitalSpr;
  [SerializeField]
  protected UILabel dispName;
  [SerializeField]
  [Tooltip("スクリーン横オフセット")]
  protected float screenSideOffset = 50f;
  [SerializeField]
  [Tooltip("スクリーン下オフセット")]
  protected float screenBottomOffset = 140f;
  [SerializeField]
  [Tooltip("表示時のYオフセット")]
  protected float offsetY = 0.1f;
  [SerializeField]
  [Tooltip("HPの色")]
  protected UIWaveTargetGizmo.HpColorInfo[] hpColorInfo;
  private readonly string kIconPrefix = "Ingame_portal_";
  protected Transform portalTransform;
  protected Transform arrowTransform;
  protected Animator targetAnimator;
  protected string lastIconName = "";
  protected string dispNameStr = "";
  protected float lastHp;
  protected bool isFirst = true;
  protected bool isEnable = true;
  private FieldWaveTargetObject _waveTarget;

  public FieldWaveTargetObject waveTarget
  {
    get => this._waveTarget;
    set
    {
      this._waveTarget = value;
      if (Object.op_Inequality((Object) this._waveTarget, (Object) null))
      {
        ((Component) this).gameObject.SetActive(true);
        this.portalTransform = ((Component) value).transform;
        this.UpdateParam();
      }
      else
        ((Component) this).gameObject.SetActive(false);
    }
  }

  public void Initialize()
  {
    if (Object.op_Equality((Object) this._waveTarget, (Object) null))
      return;
    GameObject gameObject = (GameObject) null;
    if (!this._waveTarget.info.name.IsNullOrWhiteSpace())
      gameObject = MonoBehaviourSingleton<SceneSettingsManager>.I.GetWaveTarget(this._waveTarget.info.name);
    else if (this._waveTarget.gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3)
      gameObject = ((Component) this._waveTarget).gameObject;
    if (Object.op_Inequality((Object) gameObject, (Object) null))
    {
      this.targetAnimator = gameObject.GetComponentInChildren<Animator>();
      if (Object.op_Inequality((Object) this.targetAnimator, (Object) null))
        ((Behaviour) this.targetAnimator).enabled = true;
    }
    this.lastIconName = this._waveTarget.GetIconName();
    if (!this.lastIconName.IsNullOrWhiteSpace())
      this.vitalSpr.spriteName = this.kIconPrefix + this.lastIconName;
    bool flag = !this._waveTarget.info.dispName.IsNullOrWhiteSpace();
    if (flag)
    {
      this.dispName.text = this._waveTarget.info.dispName;
      this.dispNameStr = this._waveTarget.info.dispName;
    }
    ((Component) this.dispName).gameObject.SetActive(flag);
  }

  protected override void OnEnable()
  {
    this.isEnable = true;
    base.OnEnable();
    this.nearUI.SetActive(false);
    this.farUI.SetActive(false);
    this.vitalUI.SetActive(true);
    this.arrowTransform = this.arrowUI.transform;
    this.arrowUI.SetActive(false);
  }

  protected override void OnDisable()
  {
    if (!this.isEnable)
      return;
    this.isEnable = false;
    base.OnDisable();
    this.SetActiveSafe(this.nearUI, false);
    this.SetActiveSafe(this.farUI, false);
    this.SetActiveSafe(this.vitalUI, false);
    this.SetActiveSafe(this.arrowUI, false);
    if (!Object.op_Equality((Object) this._waveTarget, (Object) null) && this._waveTarget.nowHp != 0)
      return;
    if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
      MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestTextAnnounce(string.Format(StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 5U), (object) this.dispNameStr));
    if (!Object.op_Inequality((Object) this.targetAnimator, (Object) null))
      return;
    this.targetAnimator.SetInteger("Rate", 0);
  }

  protected override void UpdateParam()
  {
    if (Object.op_Equality((Object) this.waveTarget, (Object) null) || this.waveTarget.isDead || !((Component) this.waveTarget).gameObject.activeSelf)
    {
      this.OnDisable();
    }
    else
    {
      Vector3 screenUiPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, this.portalTransform.position);
      this.screenZ = screenUiPosition.z;
      screenUiPosition.z = 0.0f;
      float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
      Vector3 vector3_1 = screenUiPosition;
      bool flag = false;
      float width = (float) Screen.width;
      int height = Screen.height;
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
      Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUiPosition);
      worldPoint.y += this.offsetY;
      Vector3 vector3_2 = Vector3.op_Subtraction(this.transform.position, worldPoint);
      if ((double) ((Vector3) ref vector3_2).sqrMagnitude >= 1.9999999494757503E-05)
        this.transform.position = worldPoint;
      if (flag)
      {
        this.SetActiveSafe(this.nearUI, false);
        this.SetActiveSafe(this.farUI, true);
        if (Object.op_Equality((Object) this.arrowTransform, (Object) null))
          return;
        Vector3 vector3_3 = Vector3.op_Subtraction(vector3_1, screenUiPosition);
        if (Vector3.op_Equality(vector3_3, Vector3.zero))
        {
          this.SetActiveSafe(this.arrowUI, false);
          return;
        }
        this.arrowTransform.eulerAngles = new Vector3(0.0f, 0.0f, 90f - Vector3.Angle(Vector3.right, vector3_3));
        this.SetActiveSafe(this.arrowUI, true);
      }
      else
      {
        this.SetActiveSafe(this.nearUI, true);
        this.SetActiveSafe(this.farUI, false);
        this.SetActiveSafe(this.arrowUI, false);
      }
      this.CheckHp();
    }
  }

  private void CheckHp()
  {
    if ((double) this.lastHp == (double) this.waveTarget.nowHp)
      return;
    this.lastHp = (float) this.waveTarget.nowHp;
    float percent = this.waveTarget.GetRate();
    if ((double) percent < 0.0)
      percent = 0.0f;
    for (int index = 0; index < this.hpColorInfo.Length; ++index)
    {
      UIWaveTargetGizmo.HpColorInfo hpColorInfo = this.hpColorInfo[index];
      if ((double) percent < (double) hpColorInfo.rate)
      {
        if (Color.op_Inequality(this.gaugeSpr.color, hpColorInfo.color))
        {
          this.gaugeSpr.color = hpColorInfo.color;
          if ((double) percent <= 0.0)
          {
            MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestTextAnnounce(string.Format(StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 5U), (object) this.waveTarget.info.dispName));
            break;
          }
          MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestTextAnnounce(string.Format(StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 4U), (object) this.waveTarget.info.dispName, (object) hpColorInfo.rate));
          break;
        }
        break;
      }
    }
    this.gaugeUI.SetPercent(percent);
    int hpRate = Mathf.CeilToInt(percent * 100f);
    if (Object.op_Inequality((Object) this.targetAnimator, (Object) null))
      this.targetAnimator.SetInteger("Rate", hpRate);
    string iconName = this._waveTarget.GetIconName(hpRate);
    if (!iconName.IsNullOrWhiteSpace() && iconName != this.lastIconName)
    {
      this.lastIconName = iconName;
      this.vitalSpr.spriteName = this.kIconPrefix + iconName;
    }
    if (this.isFirst)
      this.isFirst = false;
    else
      MonoBehaviourSingleton<MiniMap>.I.ShowAlert();
  }

  [Serializable]
  public class HpColorInfo
  {
    public float rate;
    public Color color;
  }
}
