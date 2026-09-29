// Decompiled with JetBrains decompiler
// Type: UIPlayerStatusGizmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIPlayerStatusGizmo : UIStatusGizmoBase
{
  [SerializeField]
  protected Rigidbody2D rigidbody;
  [SerializeField]
  protected GameObject nearUI;
  [SerializeField]
  protected UIHGauge gaugeUI;
  [SerializeField]
  protected UIHGauge healGaugeUI;
  [SerializeField]
  protected UIHGauge shieldGaugeUI;
  [SerializeField]
  protected UILabel nameLabel;
  [SerializeField]
  protected GameObject farUI;
  [SerializeField]
  protected GameObject arrowUI;
  [SerializeField]
  protected UILabel distanceLabel;
  [SerializeField]
  protected UISprite vitalSprite;
  [SerializeField]
  protected UISprite hostEffect;
  [SerializeField]
  protected GameObject chatUI;
  [SerializeField]
  protected UILabel chatLabel;
  [SerializeField]
  protected TweenScale chatTween;
  [SerializeField]
  protected GameObject chatStampUI;
  [SerializeField]
  protected UITexture chatStampTexture;
  [SerializeField]
  protected TweenScale chatStampTween;
  [SerializeField]
  protected UIHGauge prayerGauge;
  [SerializeField]
  protected UIHGauge prayerGaugeAdd;
  [SerializeField]
  protected UILabel prayerGaugeText;
  [SerializeField]
  protected GameObject nowPrayer;
  [SerializeField]
  protected UISprite prayerGaugeSprite;
  [SerializeField]
  protected UISprite prayerGaugeAddSprite;
  [SerializeField]
  protected Color prayerGaugeColor1;
  [SerializeField]
  protected Color prayerGaugeColor2;
  [SerializeField]
  protected Color prayerGaugeColor3;
  [SerializeField]
  [Tooltip("自キャラ表示時間")]
  protected float selfShowTime = 5f;
  [SerializeField]
  [Tooltip("矢印横表示時のXオフセット")]
  protected float arrowSideOffset = 25f;
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
  [SerializeField]
  [Tooltip("チャットスタンプ横オフセット")]
  protected float chatStampSideOffset = 60f;
  [SerializeField]
  [Tooltip("チャットスタンプ上オフセット")]
  protected float chatStampTopOffset = 120f;
  [SerializeField]
  protected UISprite friendIcon;
  [SerializeField]
  protected float nameLabelOffsetWithFriendIconX;
  [SerializeField]
  protected GameObject emotionUI;
  [SerializeField]
  protected TweenScale emotionTweenS;
  [SerializeField]
  protected TweenAlpha emotionTweenA;
  private Player _targetPlayer;
  protected float damagedTimer = -1f;
  protected Vector3 chatUILocalPos = Vector3.zero;
  protected Transform chatTransform;
  protected Vector3 chatStampUILocalPos = Vector3.zero;
  protected Transform chatStampTransform;
  protected Vector3 emotionUILocalPos = Vector3.zero;
  protected Transform emotionTransform;
  protected Transform arrowTransform;
  private int currentUserId = -1;

  public Player targetPlayer
  {
    get => this._targetPlayer;
    set
    {
      this._targetPlayer = value;
      if (Object.op_Inequality((Object) this._targetPlayer, (Object) null))
      {
        ((Component) this).gameObject.SetActive(true);
        this.currentUserId = -1;
        this.UpdateParam();
      }
      else
        ((Component) this).gameObject.SetActive(false);
    }
  }

  public bool isVisible { get; protected set; }

  public UIPlayerStatusGizmo() => this.isVisible = true;

  protected override void OnEnable()
  {
    base.OnEnable();
    if (Object.op_Inequality((Object) this.chatUI, (Object) null))
    {
      this.chatTransform = this.chatUI.transform;
      this.chatUILocalPos = this.chatTransform.localPosition;
      this.chatUI.SetActive(false);
    }
    if (Object.op_Inequality((Object) this.chatStampUI, (Object) null))
    {
      this.chatStampTransform = this.chatStampUI.transform;
      this.chatStampUILocalPos = this.chatStampTransform.localPosition;
      this.chatStampUI.SetActive(false);
    }
    if (Object.op_Inequality((Object) this.emotionUI, (Object) null))
    {
      this.emotionTransform = this.emotionUI.transform;
      this.emotionUILocalPos = this.emotionTransform.localPosition;
      this.emotionUI.SetActive(false);
    }
    if (Object.op_Inequality((Object) this.chatTween, (Object) null))
      this.chatTween.SetOnFinished(new EventDelegate.Callback(this.OnFinishChat));
    if (Object.op_Inequality((Object) this.chatStampTween, (Object) null))
      this.chatStampTween.SetOnFinished(new EventDelegate.Callback(this.OnFinishedChatStamp));
    if (Object.op_Inequality((Object) this.prayerGauge, (Object) null))
      ((Component) this.prayerGauge).gameObject.SetActive(false);
    if (Object.op_Inequality((Object) this.prayerGaugeAdd, (Object) null))
      ((Component) this.prayerGaugeAdd).gameObject.SetActive(false);
    this.nearUI.SetActive(false);
    this.farUI.SetActive(false);
    if (Object.op_Inequality((Object) this.arrowUI, (Object) null))
    {
      this.arrowTransform = this.arrowUI.transform;
      this.arrowUI.SetActive(false);
    }
    if (Object.op_Inequality((Object) this.friendIcon, (Object) null))
      ((Component) this.friendIcon).gameObject.SetActive(false);
    if (!Object.op_Inequality((Object) this.hostEffect, (Object) null))
      return;
    ((Behaviour) this.hostEffect).enabled = false;
  }

  protected override void UpdateParam()
  {
    if (Object.op_Equality((Object) this.targetPlayer, (Object) null) || !((Component) this.targetPlayer).gameObject.activeSelf || this.targetPlayer.isLoading || !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop && this.targetPlayer.isStopCounter || !this.targetPlayer.isCoopInitialized && this.targetPlayer.IsPuppet() || !this.isVisible)
    {
      this.SetActiveSafe(this.nearUI, false);
      this.SetActiveSafe(this.farUI, false);
      if (Object.op_Inequality((Object) this.arrowUI, (Object) null))
        this.SetActiveSafe(this.arrowUI, false);
      if (!Object.op_Inequality((Object) this.prayerGauge, (Object) null))
        return;
      this.SetActiveSafe(((Component) this.prayerGauge).gameObject, false);
    }
    else
    {
      if (MonoBehaviourSingleton<CoopManager>.IsValid())
        this.isHostPlayer = MonoBehaviourSingleton<CoopManager>.I.GetPartyOwnerPlayerID() == this.targetPlayer.id;
      Vector3 position = this.targetPlayer._position;
      position.y += this.targetPlayer.playerParameter.uiHeight;
      Vector3 screenUiPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, position);
      this.screenZ = screenUiPosition.z;
      screenUiPosition.z = 0.0f;
      float num1 = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
      if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyPlayerStatusGizmo)
      {
        if (SpecialDeviceManager.IsPortrait)
        {
          this.screenSideOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPlayerStatusGizmoScreenSideOffsetPortrait;
          this.screenBottomOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPlayerStatusGizmoScreenBottomOffsetPortrait;
        }
        else
        {
          this.screenSideOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPlayerStatusGizmoScreenSideOffsetLandScape;
          this.screenBottomOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPlayerStatusGizmoScreenBottomOffsetLandScape;
        }
      }
      Vector3 vector3_1 = screenUiPosition;
      bool flag1 = false;
      float width = (float) Screen.width;
      float height = (float) Screen.height;
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
      float num2 = this.screenBottomOffset;
      if (FieldManager.IsValidInGameNoQuest())
        num2 = this.screenBottomFieldOffset;
      if ((double) screenUiPosition.y < (double) num2 * (double) num1)
      {
        screenUiPosition.y = num2 * num1;
        flag1 = true;
      }
      Vector3 vector3_2 = screenUiPosition;
      if (this.chatUI.activeSelf)
      {
        if ((double) vector3_2.x < (double) this.chatSideOffset * (double) num1)
          vector3_2.x = this.chatSideOffset * num1;
        else if ((double) vector3_2.x > (double) width - (double) this.chatSideOffset * (double) num1)
          vector3_2.x = width - this.chatSideOffset * num1;
        if ((double) vector3_2.y > (double) height - (double) this.chatTopOffset * (double) num1)
          vector3_2.y = height - this.chatTopOffset * num1;
      }
      Vector3 vector3_3 = screenUiPosition;
      if (this.chatStampUI.activeSelf || this.emotionUI.activeSelf)
      {
        if ((double) vector3_3.x < (double) this.chatStampSideOffset * (double) num1)
          vector3_3.x = this.chatStampSideOffset * num1;
        else if ((double) vector3_3.x > (double) width - (double) this.chatStampSideOffset * (double) num1)
          vector3_3.x = width - this.chatStampSideOffset * num1;
        if ((double) vector3_3.y > (double) height - (double) this.chatStampTopOffset * (double) num1)
          vector3_3.y = height - this.chatStampTopOffset * num1;
      }
      Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUiPosition);
      Vector3 vector3_4 = worldPoint;
      Vector3 vector3_5 = worldPoint;
      if (this.chatUI.activeSelf)
        vector3_4 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(vector3_2);
      if (this.chatStampUI.activeSelf || this.emotionUI.activeSelf)
        vector3_5 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(vector3_3);
      Vector3 vector3_6 = Vector3.op_Subtraction(this.transform.position, worldPoint);
      if ((double) ((Vector3) ref vector3_6).sqrMagnitude >= 1.9999999494757503E-05)
        this.transform.position = worldPoint;
      Matrix4x4 worldToLocalMatrix;
      if (this.chatUI.activeSelf)
      {
        worldToLocalMatrix = this.transform.worldToLocalMatrix;
        Vector3 vector3_7 = ((Matrix4x4) ref worldToLocalMatrix).MultiplyPoint3x4(vector3_4);
        vector3_7.y += this.chatUILocalPos.y;
        vector3_7.z = 0.0f;
        this.chatTransform.localPosition = vector3_7;
      }
      if (this.chatStampUI.activeSelf || this.emotionUI.activeSelf)
      {
        worldToLocalMatrix = this.transform.worldToLocalMatrix;
        Vector3 vector3_8 = ((Matrix4x4) ref worldToLocalMatrix).MultiplyPoint3x4(vector3_5);
        vector3_8.y += this.chatUILocalPos.y;
        vector3_8.z = 0.0f;
        this.chatStampTransform.localPosition = vector3_8;
        this.emotionTransform.localPosition = vector3_8;
      }
      if (Object.op_Inequality((Object) this.prayerGauge, (Object) null))
      {
        if ((double) this.targetPlayer.revivalTimePercent > 0.0)
        {
          this.SetActiveSafe(((Component) this.prayerGauge).gameObject, true);
          this.prayerGauge.SetPercent(this.targetPlayer.revivalTimePercent);
          if (Object.op_Inequality((Object) this.nowPrayer, (Object) null))
            this.SetActiveSafe(this.nowPrayer.gameObject, this.targetPlayer.IsPrayed());
          if (this.targetPlayer.IsPrayed())
          {
            if (this.targetPlayer.isDead)
              this.prayerGaugeText.text = string.Format(StringTable.Get(STRING_CATEGORY.IN_GAME, 1011U));
            else if (this.targetPlayer.IsStone())
              this.prayerGaugeText.text = string.Format(StringTable.Get(STRING_CATEGORY.IN_GAME, 1012U));
            this.SetActiveSafe(((Component) this.prayerGaugeAdd).gameObject, true);
            this.prayerGaugeAdd.SetPercent(this.targetPlayer.revivalTimePercent);
            switch (this.targetPlayer.prayerIds.Count)
            {
              case 2:
                this.SetUISpriteColor(this.prayerGaugeSprite, this.prayerGaugeColor2);
                this.SetUISpriteColor(this.prayerGaugeAddSprite, this.prayerGaugeColor2);
                break;
              case 3:
                this.SetUISpriteColor(this.prayerGaugeSprite, this.prayerGaugeColor3);
                this.SetUISpriteColor(this.prayerGaugeAddSprite, this.prayerGaugeColor3);
                break;
              default:
                this.SetUISpriteColor(this.prayerGaugeSprite, this.prayerGaugeColor1);
                this.SetUISpriteColor(this.prayerGaugeAddSprite, this.prayerGaugeColor1);
                break;
            }
          }
          else
            this.SetActiveSafe(((Component) this.prayerGaugeAdd).gameObject, false);
        }
        else
          this.SetActiveSafe(((Component) this.prayerGauge).gameObject, false);
      }
      Self targetPlayer = this.targetPlayer as Self;
      if (Object.op_Inequality((Object) targetPlayer, (Object) null))
      {
        bool flag2 = false;
        if ((double) this.damagedTimer >= 0.0)
        {
          if ((double) Time.time - (double) this.damagedTimer >= (double) this.selfShowTime)
            this.damagedTimer = -1f;
          else
            flag2 = true;
        }
        if (!flag2)
        {
          this.SetActiveSafe(this.nearUI, false);
          this.SetActiveSafe(this.farUI, false);
          return;
        }
      }
      if (flag1 && FieldManager.IsValidInGameNoQuest() || !GameSaveData.instance.headName)
      {
        this.SetActiveSafe(this.nearUI, false);
        this.SetActiveSafe(this.farUI, false);
      }
      else
      {
        if ((double) this.targetPlayer.rescueTime > 0.0 || (double) this.targetPlayer.stoneRescueTime > 0.0)
          flag1 = true;
        if (flag1 && Object.op_Equality((Object) targetPlayer, (Object) null))
        {
          this.SetActiveSafe(this.nearUI, false);
          this.SetActiveSafe(this.farUI, true);
          if (Object.op_Inequality((Object) this.arrowUI, (Object) null))
          {
            Vector3 vector3_9 = Vector3.op_Subtraction(vector3_1, screenUiPosition);
            if (Vector3.op_Inequality(vector3_9, Vector3.zero))
            {
              float num3 = 90f - Vector3.Angle(Vector3.right, vector3_9);
              this.arrowTransform.eulerAngles = new Vector3(0.0f, 0.0f, num3);
              this.SetActiveSafe(this.arrowUI, true);
              Vector3 vector3_10;
              // ISSUE: explicit constructor call
              ((Vector3) ref vector3_10).\u002Ector(0.0f, 0.0f, 0.0f);
              float num4 = Mathf.Sin(num3 * ((float) Math.PI / 180f));
              if ((double) num4 > 0.0099999997764825821)
                vector3_10.x = this.arrowSideOffset;
              else if ((double) num4 < -0.0099999997764825821)
                vector3_10.x = -this.arrowSideOffset;
              this.arrowTransform.localPosition = vector3_10;
            }
            else
              this.SetActiveSafe(this.arrowUI, false);
          }
          if (Object.op_Inequality((Object) this.distanceLabel, (Object) null))
          {
            if ((double) this.targetPlayer.rescueTime > 0.0)
              this.distanceLabel.text = Mathf.CeilToInt(this.targetPlayer.rescueTime).ToString() + "　";
            else if ((double) this.targetPlayer.stoneRescueTime > 0.0)
              this.distanceLabel.text = Mathf.CeilToInt(this.targetPlayer.stoneRescueTime).ToString() + " ";
            else if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
            {
              Vector3 vector3_11 = Vector3.op_Subtraction(this.targetPlayer._position, MonoBehaviourSingleton<StageObjectManager>.I.self._position);
              this.distanceLabel.text = ((int) ((Vector3) ref vector3_11).magnitude).ToString() + "m";
            }
          }
          this.SetVitalSprite(this.isHostPlayer);
        }
        else
        {
          this.SetActiveSafe(this.nearUI, true);
          this.SetActiveSafe(this.farUI, false);
          if (Object.op_Inequality((Object) this.gaugeUI, (Object) null))
          {
            float percent = (float) this.targetPlayer.hp / (float) this.targetPlayer.hpMax;
            if ((double) percent < 0.0)
              percent = 0.0f;
            if ((double) this.gaugeUI.nowPercent != (double) percent)
              this.gaugeUI.SetPercent(percent);
          }
          if (Object.op_Inequality((Object) this.healGaugeUI, (Object) null))
          {
            float percent = (float) this.targetPlayer.healHp / (float) this.targetPlayer.hpMax;
            if ((double) percent < 0.0)
              percent = 0.0f;
            if ((double) this.healGaugeUI.nowPercent != (double) percent)
              this.healGaugeUI.SetPercent(percent);
          }
          if (Object.op_Inequality((Object) this.shieldGaugeUI, (Object) null))
          {
            float percent = !this.targetPlayer.IsValidShield() ? 0.0f : (float) (int) this.targetPlayer.ShieldHp / (float) (int) this.targetPlayer.ShieldHpMax;
            if ((double) percent < 0.0)
              percent = 0.0f;
            if ((double) this.shieldGaugeUI.nowPercent != (double) percent)
              this.shieldGaugeUI.SetPercent(percent);
          }
          if (!Object.op_Inequality((Object) this.nameLabel, (Object) null))
            return;
          if (this.targetPlayer.createInfo != null && this.targetPlayer.createInfo.charaInfo != null && MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.fieldData != null && MonoBehaviourSingleton<FieldManager>.I.fieldData.field != null && MonoBehaviourSingleton<FieldManager>.I.fieldData.field.slotInfos != null && this.currentUserId != this.targetPlayer.createInfo.charaInfo.userId)
          {
            List<FieldModel.SlotInfo> slotInfos = MonoBehaviourSingleton<FieldManager>.I.fieldData.field.slotInfos;
            FriendCharaInfo friendCharaInfo = (FriendCharaInfo) null;
            int index = 0;
            for (int count = slotInfos.Count; index < count; ++index)
            {
              if (slotInfos[index].userId == this.targetPlayer.createInfo.charaInfo.userId)
                friendCharaInfo = slotInfos[index].userInfo;
            }
            if (friendCharaInfo != null)
            {
              if (friendCharaInfo.follower && friendCharaInfo.following)
                ((Component) this.friendIcon).gameObject.SetActive(true);
              else
                ((Component) this.friendIcon).gameObject.SetActive(false);
              this.currentUserId = this.targetPlayer.createInfo.charaInfo.userId;
            }
          }
          this.nameLabel.text = this.targetPlayer.fullName;
          this.nameLabel.supportEncoding = true;
        }
      }
    }
  }

  private void SetVitalSprite(bool _isOwner)
  {
    if (Object.op_Equality((Object) this.vitalSprite, (Object) null) || Object.op_Equality((Object) this.hostEffect, (Object) null))
      return;
    GameObject gameObject = ((Component) this.hostEffect).gameObject;
    ((Behaviour) this.hostEffect).enabled = false;
    string str;
    if (this.targetPlayer.hp <= 0)
    {
      if (this.targetPlayer.IsRescuable() || this.targetPlayer.IsStone())
      {
        str = "Ingame_member_vitalsign_red";
        if (!((Behaviour) this.hostEffect).enabled & _isOwner)
          ((Behaviour) this.hostEffect).enabled = true;
      }
      else
        str = "Ingame_member_vitalsign_gray";
    }
    else
      str = (double) this.targetPlayer.hp > (double) this.targetPlayer.hpMax * 0.25 ? "Ingame_member_vitalsign_green" : "Ingame_member_vitalsign_yellow";
    this.vitalSprite.spriteName = str;
  }

  public void SetUISpriteColor(UISprite sprite, Color c)
  {
    if (!Object.op_Inequality((Object) sprite, (Object) null))
      return;
    Color color = sprite.color;
    if ((double) color.r == (double) c.r && (double) color.g == (double) c.g && (double) color.b == (double) c.b)
      return;
    color.r = c.r;
    color.g = c.g;
    color.b = c.b;
    sprite.color = color;
  }

  public void SetVisible(bool visible) => this.isVisible = visible;

  public void SayChat(int chatID)
  {
    this.DisplayChat(MonoBehaviourSingleton<UIChatButtonBase>.I.GetChatSayText(chatID));
  }

  public void SayChat(string message) => this.DisplayChat(message);

  public void SetChatDuration(float duration)
  {
    if (!Object.op_Inequality((Object) this.chatTween, (Object) null))
      return;
    this.chatTween.duration = duration;
  }

  private void DisplayChat(string message)
  {
    if (!this.CanDisplayChat())
      return;
    this.chatLabel.text = message;
    this.chatUI.SetActive(true);
    if (!Object.op_Inequality((Object) this.chatTween, (Object) null))
      return;
    this.chatTween.ResetToBeginning();
    this.chatTween.PlayForward();
  }

  public void SayChatStamp(int stampId) => this.StartCoroutine(this.DoDisplayChatStamp(stampId));

  private IEnumerator DoDisplayChatStamp(int stampId)
  {
    this.chatStampUI.SetActive(false);
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lostamp = loadingQueue.LoadChatStamp(stampId, true);
    yield return (object) loadingQueue.Wait();
    if (!Object.op_Equality(lostamp.loadedObject, (Object) null))
    {
      if (Object.op_Inequality((Object) this.chatStampTexture, (Object) null))
        this.chatStampTexture.mainTexture = (Texture) (lostamp.loadedObject as Texture2D);
      if (this.CanDisplayChat())
      {
        this.chatStampUI.SetActive(true);
        if (Object.op_Inequality((Object) this.chatStampTween, (Object) null))
        {
          this.chatStampTween.ResetToBeginning();
          this.chatStampTween.PlayForward();
        }
      }
    }
  }

  private bool CanDisplayChat()
  {
    return Object.op_Inequality((Object) this.targetPlayer, (Object) null) && this.targetPlayer.isInitialized;
  }

  public void OnFinishChat() => this.chatUI.SetActive(false);

  public void OnFinishedChatStamp() => this.chatStampUI.SetActive(false);

  public void OnDamageSelf() => this.damagedTimer = Time.time;

  public void OnDispEmotion(bool isDisp)
  {
    this.emotionUI.SetActive(isDisp);
    if (!isDisp)
      return;
    this.emotionTweenS.ResetToBeginning();
    this.emotionTweenS.PlayForward();
    this.emotionTweenA.ResetToBeginning();
    this.emotionTweenA.PlayForward();
  }

  public void SetEmotionDuration(float duration)
  {
    this.emotionTweenS.duration = duration;
    this.emotionTweenA.duration = duration;
  }

  protected override void SortAll()
  {
    UIStatusGizmoBase.uiList.Sort((Comparison<UIStatusGizmoBase>) ((a, b) =>
    {
      if (a.isHostPlayer)
        return 1;
      if (b.isHostPlayer)
        return -1;
      if ((double) a.ScreenZ == (double) b.ScreenZ)
        return 0;
      return (double) a.ScreenZ >= (double) b.ScreenZ ? -1 : 1;
    }));
    this.UpdateUIDepth();
  }

  protected void UpdateUIDepth()
  {
    int index = 0;
    for (int count = UIStatusGizmoBase.uiList.Count; index < count; ++index)
    {
      UIStatusGizmoBase ui = UIStatusGizmoBase.uiList[index];
      int num = index * 10;
      if (ui.depthOffset != num)
      {
        UIStatusGizmoBase.AdjustDepth(((Component) ui).gameObject, num - ui.depthOffset);
        ui.BasePanel.depth = num;
        ui.depthOffset = num;
      }
    }
  }
}
