// Decompiled with JetBrains decompiler
// Type: UIChatGimmickGizmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIChatGimmickGizmo : UIStatusGizmoBase
{
  [SerializeField]
  protected GameObject chatUI;
  [SerializeField]
  protected UILabel chatLabel;
  [SerializeField]
  protected TweenScale chatTween;
  [SerializeField]
  [Tooltip("スクリーン横オフセット")]
  protected float screenSideOffset = 36f;
  [SerializeField]
  [Tooltip("スクリーン下オフセット")]
  protected float screenBottomOffset = 107f;
  [SerializeField]
  [Tooltip("スクリーン下オフセット、フィールド時")]
  protected float screenBottomFieldOffset = 107f;
  [SerializeField]
  [Tooltip("チャット横オフセット")]
  protected float chatSideOffset = 60f;
  [SerializeField]
  [Tooltip("チャット上オフセット")]
  protected float chatTopOffset = 120f;
  private float sizeAdjust = 1f;
  protected FieldChatGimmickObject chatGimmickObj;
  protected Vector3 chatUILocalPos = Vector3.zero;
  protected Transform chatTransform;

  public void Initialize(FieldChatGimmickObject obj) => this.chatGimmickObj = obj;

  protected override void OnEnable()
  {
    this.sizeAdjust = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
    base.OnEnable();
    if (Object.op_Inequality((Object) this.chatUI, (Object) null))
    {
      this.chatTransform = this.chatUI.transform;
      this.chatUILocalPos = this.chatTransform.localPosition;
      this.chatUI.SetActive(false);
    }
    if (!Object.op_Inequality((Object) this.chatTween, (Object) null))
      return;
    this.chatTween.SetOnFinished(new EventDelegate.Callback(this.OnFinishChat));
  }

  protected override void UpdateParam()
  {
    if (!this.chatUI.activeSelf)
      return;
    Vector3 screenUiPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, this.chatGimmickObj.GetPosition());
    screenUiPosition.z = 0.0f;
    float width = (float) Screen.width;
    float height = (float) Screen.height;
    if ((double) screenUiPosition.x < (double) this.screenSideOffset * (double) this.sizeAdjust)
      screenUiPosition.x = this.screenSideOffset * this.sizeAdjust;
    else if ((double) screenUiPosition.x > (double) width - (double) this.screenSideOffset * (double) this.sizeAdjust)
      screenUiPosition.x = width - this.screenSideOffset * this.sizeAdjust;
    float num = this.screenBottomOffset;
    if (FieldManager.IsValidInGameNoQuest())
      num = this.screenBottomFieldOffset;
    if ((double) screenUiPosition.y < (double) num * (double) this.sizeAdjust)
      screenUiPosition.y = num * this.sizeAdjust;
    Vector3 vector3_1 = screenUiPosition;
    if (this.chatUI.activeSelf)
    {
      if ((double) vector3_1.x < (double) this.chatSideOffset * (double) this.sizeAdjust)
        vector3_1.x = this.chatSideOffset * this.sizeAdjust;
      else if ((double) vector3_1.x > (double) width - (double) this.chatSideOffset * (double) this.sizeAdjust)
        vector3_1.x = width - this.chatSideOffset * this.sizeAdjust;
      if ((double) vector3_1.y > (double) height - (double) this.chatTopOffset * (double) this.sizeAdjust)
        vector3_1.y = height - this.chatTopOffset * this.sizeAdjust;
    }
    Vector3 worldPoint1 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUiPosition);
    Vector3 worldPoint2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(vector3_1);
    Vector3 vector3_2 = Vector3.op_Subtraction(this.transform.position, worldPoint1);
    if ((double) ((Vector3) ref vector3_2).sqrMagnitude >= 1.9999999494757503E-05)
      this.transform.position = worldPoint1;
    Matrix4x4 worldToLocalMatrix = this.transform.worldToLocalMatrix;
    Vector3 vector3_3 = ((Matrix4x4) ref worldToLocalMatrix).MultiplyPoint3x4(worldPoint2);
    vector3_3.y += this.chatUILocalPos.y;
    vector3_3.z = 0.0f;
    this.chatTransform.localPosition = vector3_3;
  }

  public void SayChat(string message)
  {
    this.chatLabel.text = message;
    this.chatUI.SetActive(true);
    if (!Object.op_Inequality((Object) this.chatTween, (Object) null))
      return;
    this.chatTween.ResetToBeginning();
    this.chatTween.PlayForward();
  }

  public void OnFinishChat() => this.chatUI.SetActive(false);

  public bool isDisp() => this.chatUI.activeSelf;
}
