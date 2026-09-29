// Decompiled with JetBrains decompiler
// Type: StoryMain
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StoryMain : GameSection, StoryDirector.IStoryEventReceiver
{
  private int messageNum;
  private IEnumerator coroutine;
  private Transform lastMessageItem;
  private bool lastMessageFocus;
  private UISprite balloon;
  private Transform tailLeft;
  private Transform tailRight;
  private Transform tailCenter;
  private UILabel nameLabel;
  private UILabel messageLabel;
  private TypewriterEffect typewriter;
  private int initBaseHeight;
  private int initMessageHeight;
  private int messageHeight;
  private System.Action addMessageFunc;
  private object[] eventData;
  private int? eventID;
  private string requestEndEvent;
  private EventData[] requestEndEventArray;
  private UIButton m_btnNext;

  public override void Initialize()
  {
    if (GameSection.GetEventData() is object[] eventData)
    {
      this.eventID = new int?((int) eventData[0]);
      this.eventData = new object[2]
      {
        eventData[1],
        eventData[2]
      };
      if (eventData.Length > 3)
      {
        this.requestEndEvent = eventData[3] as string;
        if (string.IsNullOrEmpty(this.requestEndEvent))
          this.requestEndEventArray = eventData[3] as EventData[];
      }
    }
    Transform ctrl = this.FindCtrl(((Component) this).transform, (Enum) StoryMain.UI.TEX_FADER_HEADER);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
      ((Component) ctrl).gameObject.SetActive(false);
    base.Initialize();
    this.SyncSpecialDeviceAnctor();
  }

  private void CollectTweens()
  {
  }

  private void SyncSpecialDeviceAnctor()
  {
    if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.NeedModifyStoryAnchor)
      return;
    DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
    Transform ctrl1 = this.FindCtrl(((Component) this).transform, (Enum) StoryMain.UI.MesBase);
    if (Object.op_Inequality((Object) ctrl1, (Object) null))
    {
      UIWidget component = ((Component) ctrl1).GetComponent<UIWidget>();
      component.leftAnchor.absolute = specialDeviceInfo.StoryMessageBaseAnchor.left;
      component.rightAnchor.absolute = specialDeviceInfo.StoryMessageBaseAnchor.right;
      component.bottomAnchor.absolute = specialDeviceInfo.StoryMessageBaseAnchor.bottom;
      component.topAnchor.absolute = specialDeviceInfo.StoryMessageBaseAnchor.top;
      component.UpdateAnchors();
    }
    Transform ctrl2 = this.FindCtrl(((Component) this).transform, (Enum) StoryMain.UI.fukidashibaseflame);
    if (!Object.op_Inequality((Object) ctrl2, (Object) null))
      return;
    UIWidget component1 = ((Component) ctrl2).GetComponent<UIWidget>();
    component1.leftAnchor.absolute = specialDeviceInfo.StoryMainFukidashiBaseFlameAnchor.left;
    component1.rightAnchor.absolute = specialDeviceInfo.StoryMainFukidashiBaseFlameAnchor.right;
    component1.bottomAnchor.absolute = specialDeviceInfo.StoryMainFukidashiBaseFlameAnchor.bottom;
    component1.topAnchor.absolute = specialDeviceInfo.StoryMainFukidashiBaseFlameAnchor.top;
    component1.UpdateAnchors();
  }

  private UIButton BtnNext
  {
    get
    {
      if (Object.op_Equality((Object) this.m_btnNext, (Object) null))
      {
        Transform ctrl = this.GetCtrl((Enum) StoryMain.UI.BTN_NEXT);
        if (Object.op_Inequality((Object) ctrl, (Object) null))
          this.m_btnNext = ((Component) ctrl).GetComponent<UIButton>();
      }
      return this.m_btnNext;
    }
  }

  protected override void OnOpen()
  {
    base.OnOpen();
    this.SetColor((Enum) StoryMain.UI.TEX_FADER, new Color(0.0f, 0.0f, 0.0f, 1f));
    this.SetActive((Enum) StoryMain.UI.SPR_NEXT, false);
    this.SetActive((Enum) StoryMain.UI.BTN_SKIP, false);
    MonoBehaviourSingleton<StoryDirector>.I.StartScript(this.eventID ?? 1, this.GetComponent<UITexture>((Enum) StoryMain.UI.TEX_LOCATION), this.GetComponent<UITexture>((Enum) StoryMain.UI.TEX_EFFECT), (StoryDirector.IStoryEventReceiver) this);
  }

  public override void UpdateUI()
  {
  }

  public void AddMessage(
    string name,
    string msg,
    StoryDirector.POS tail_dir,
    StoryDirector.MSG_TYPE msg_type,
    StoryDirector.LabelOption labelOption = null)
  {
    this.StartCoroutine(this.coroutine = this.DoAddMessage(name, msg, tail_dir, msg_type, labelOption));
  }

  private IEnumerator DoAddMessage(
    string name,
    string msg,
    StoryDirector.POS tail_dir,
    StoryDirector.MSG_TYPE msg_type,
    StoryDirector.LabelOption labelOption = null)
  {
    this.typewriter = (TypewriterEffect) null;
    Transform ctrl = this.GetCtrl((Enum) StoryMain.UI.TBL_MESSAGE);
    string prefab_name = "StoryMessageItem0";
    if (msg_type == StoryDirector.MSG_TYPE.MONOLOGUE)
      prefab_name = "StoryMessageItem1";
    Transform root = this.Realizes(prefab_name, ctrl);
    root.SetSiblingIndex(0);
    UIWidget message_item_w = ((Component) root).GetComponent<UIWidget>();
    this.lastMessageItem = root;
    this.balloon = this.GetComponent<UISprite>(root, (Enum) StoryMain.UI.SPR_BALLOON);
    this.tailLeft = this.FindCtrl(root, (Enum) StoryMain.UI.SPR_TAIL_L);
    this.tailRight = this.FindCtrl(root, (Enum) StoryMain.UI.SPR_TAIL_R);
    this.tailCenter = this.FindCtrl(root, (Enum) StoryMain.UI.SPR_TAIL_C);
    this.nameLabel = this.GetComponent<UILabel>(root, (Enum) StoryMain.UI.LBL_NAME);
    this.messageLabel = this.GetComponent<UILabel>(root, (Enum) StoryMain.UI.LBL_MESSAGE);
    this.initBaseHeight = message_item_w.height;
    this.messageLabel.text = " ";
    this.initMessageHeight = this.messageLabel.height;
    this.messageHeight = this.initMessageHeight;
    if (labelOption != null)
    {
      this.messageLabel.supportEncoding = labelOption.BBCode;
      this.messageLabel.alignment = labelOption.Alignment;
      this.messageLabel.fontSize = labelOption.FontSize;
    }
    string final = "";
    if (this.messageLabel.Wrap(msg, out final))
      msg = WordWrap.Convert(this.messageLabel, msg);
    this.SetLastMessageFocus(true);
    this.SetMessageDragEnabled(false);
    if (msg_type == StoryDirector.MSG_TYPE.NORMAL)
    {
      if (Object.op_Inequality((Object) this.tailLeft, (Object) null) && tail_dir != StoryDirector.POS.LEFT)
        ((Component) this.tailLeft).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.tailRight, (Object) null) && tail_dir != StoryDirector.POS.RIGHT)
        ((Component) this.tailRight).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.tailCenter, (Object) null) && tail_dir != StoryDirector.POS.CENTER)
        ((Component) this.tailCenter).gameObject.SetActive(false);
      this.nameLabel.text = name;
    }
    UIWidget next_arrow_w = this.GetComponent<UIWidget>((Enum) StoryMain.UI.SPR_NEXT);
    ((Component) next_arrow_w).gameObject.SetActive(false);
    List<UITweener> tweens = new List<UITweener>();
    ((Component) root).GetComponentsInChildren<UITweener>(tweens);
    while (Object.op_Inequality((Object) tweens.Find((Predicate<UITweener>) (o => ((Behaviour) o).enabled)), (Object) null))
      yield return (object) null;
    SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
    this.messageLabel.text = msg;
    this.typewriter = ((Component) this.messageLabel).gameObject.AddComponent<TypewriterEffect>();
    this.typewriter.charsPerSecond = StoryDirector.SPEED_TYPEWRITER;
    this.typewriter.ResetToBeginning();
    while (this.typewriter.isActive)
      yield return (object) null;
    yield return (object) null;
    ((Component) next_arrow_w).gameObject.SetActive(true);
    Vector3[] worldCorners1 = message_item_w.worldCorners;
    Vector3[] worldCorners2 = next_arrow_w.worldCorners;
    next_arrow_w.SetAnchor((Transform) null);
    next_arrow_w.cachedTransform.position = new Vector3((float) (((double) worldCorners2[0].x + (double) worldCorners2[2].x) * 0.5), worldCorners1[0].y - (float) (((double) worldCorners2[1].y - (double) worldCorners2[0].y) * 0.5), worldCorners2[0].z);
    this.SetMessageDragEnabled(true);
    Object.Destroy((Object) this.typewriter);
    this.typewriter = (TypewriterEffect) null;
    this.messageHeight = 0;
    ++this.messageNum;
    this.coroutine = (IEnumerator) null;
  }

  private void OnQuery_NEXT()
  {
    if (Object.op_Inequality((Object) this.typewriter, (Object) null))
      this.typewriter.charsPerSecond = 1000;
    if (this.coroutine != null)
      return;
    MonoBehaviourSingleton<StoryDirector>.I.OnNextMessage();
  }

  private void OnQuery_SKIP()
  {
    GameSection.StopEvent();
    int? eventId = this.eventID;
    int num = 80000001;
    if (eventId.GetValueOrDefault() == num & eventId.HasValue)
    {
      if (LoungeMatchingManager.IsValidInLounge())
        MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Lounge");
      else
        MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Home");
    }
    else if (!string.IsNullOrEmpty(this.requestEndEvent))
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
      {
        new EventData(this.requestEndEvent, (object) null)
      });
    else if (this.requestEndEventArray != null)
    {
      MonoBehaviourSingleton<StoryDirector>.I.HideBG();
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(this.requestEndEventArray);
    }
    else if (MonoBehaviourSingleton<InGameManager>.I.questTransferInfo != null)
    {
      ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).gameObject.SetActive(false);
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", ((Component) this).gameObject, "INTERVAL");
    }
    else
    {
      MonoBehaviourSingleton<StoryDirector>.I.HideBG();
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
      {
        new EventData(GameSection.GetGoingHomeEvent(), (object) null),
        new EventData("DELIVERY_CLEAR_REWARD", (object) this.eventData)
      });
    }
  }

  private void LateUpdate()
  {
    if (Object.op_Inequality((Object) this.lastMessageItem, (Object) null))
    {
      if (this.messageHeight > 0)
      {
        if (this.messageHeight < this.messageLabel.height)
        {
          this.messageHeight = this.messageLabel.height;
          ((Component) this.lastMessageItem).GetComponent<UIWidget>().height = this.initBaseHeight + this.messageHeight - this.initMessageHeight;
        }
        this.GetComponent<UITable>((Enum) StoryMain.UI.TBL_MESSAGE).Reposition();
        this.GetComponent<UIScrollView>((Enum) StoryMain.UI.SCR_MESSAGE).ResetPosition();
      }
      else if (this.lastMessageFocus)
      {
        Vector3[] worldCorners = this.GetComponent<UIPanel>((Enum) StoryMain.UI.SCR_MESSAGE).worldCorners;
        if ((double) UIUtility.GetWorldTopY((UIWidget) this.balloon) > (double) worldCorners[1].y)
          this.SetLastMessageFocus(false);
      }
    }
    if (this.addMessageFunc == null)
      return;
    this.addMessageFunc();
    this.addMessageFunc = (System.Action) null;
  }

  private void SetLastMessageFocus(bool is_focus)
  {
    this.lastMessageFocus = is_focus;
    float num1 = is_focus ? 0.6f : 1f;
    Transform ctrl = this.GetCtrl((Enum) StoryMain.UI.TBL_MESSAGE);
    int num2 = 1;
    for (int childCount = ctrl.childCount; num2 < childCount; ++num2)
      this.GetComponent<UISprite>(ctrl.GetChild(num2), (Enum) StoryMain.UI.SPR_BALLOON).alpha = num1;
  }

  private void SetMessageDragEnabled(bool is_enable)
  {
    Transform ctrl = this.GetCtrl((Enum) StoryMain.UI.TBL_MESSAGE);
    int num = 0;
    for (int childCount = ctrl.childCount; num < childCount; ++num)
      ((Behaviour) ((Component) ctrl.GetChild(num)).GetComponent<UIDragScrollView>()).enabled = is_enable;
  }

  void StoryDirector.IStoryEventReceiver.FadeIn()
  {
    if (Object.op_Equality((Object) this, (Object) null))
      return;
    TweenColor.Begin(((Component) this.GetCtrl((Enum) StoryMain.UI.TEX_FADER)).gameObject, 1f, new Color(0.0f, 0.0f, 0.0f, 0.0f));
  }

  void StoryDirector.IStoryEventReceiver.FadeOut(Color fadeout_color)
  {
    if (Object.op_Equality((Object) this, (Object) null))
      return;
    TweenColor.Begin(((Component) this.GetCtrl((Enum) StoryMain.UI.TEX_FADER)).gameObject, 1f, fadeout_color);
  }

  void StoryDirector.IStoryEventReceiver.FadeIn(float fade_time)
  {
    if (Object.op_Equality((Object) this, (Object) null))
      return;
    TweenColor.Begin(((Component) this.GetCtrl((Enum) StoryMain.UI.TEX_FADER)).gameObject, fade_time, new Color(0.0f, 0.0f, 0.0f, 0.0f));
  }

  void StoryDirector.IStoryEventReceiver.FadeOut(Color fadeout_color, float fade_time)
  {
    if (Object.op_Equality((Object) this, (Object) null))
      return;
    TweenColor.Begin(((Component) this.GetCtrl((Enum) StoryMain.UI.TEX_FADER)).gameObject, fade_time, fadeout_color);
  }

  void StoryDirector.IStoryEventReceiver.AddMessage(
    string name,
    string msg,
    StoryDirector.POS tail_dir,
    StoryDirector.MSG_TYPE msg_type,
    StoryDirector.LabelOption labelOption)
  {
    if (Object.op_Equality((Object) this, (Object) null))
      return;
    this.addMessageFunc = (System.Action) (() => this.AddMessage(name, msg, tail_dir, msg_type, labelOption));
  }

  UITexture StoryDirector.IStoryEventReceiver.GetModelUITexture(int id)
  {
    return Object.op_Equality((Object) this, (Object) null) ? (UITexture) null : this.GetComponent<UITexture>((Enum) (StoryMain.UI) (7 + id));
  }

  void StoryDirector.IStoryEventReceiver.EndLoadFirstBG()
  {
    if (Object.op_Equality((Object) this, (Object) null))
      return;
    this.SetActive((Enum) StoryMain.UI.BTN_SKIP, true);
  }

  void StoryDirector.IStoryEventReceiver.EndStory()
  {
    if (Object.op_Equality((Object) this, (Object) null))
      return;
    this.DispatchEvent("SKIP");
  }

  protected override void OnCloseStart()
  {
    SoundManager.StopVoice(fadeout_frame: 6);
    base.OnCloseStart();
  }

  private enum UI
  {
    BTN_NEXT,
    SCR_MESSAGE,
    TBL_MESSAGE,
    SPR_NEXT,
    TEX_FADER,
    TEX_LOCATION,
    TEX_IMAGE,
    TEX_MODEL0,
    TEX_MODEL1,
    TEX_MODEL2,
    TEX_MODEL3,
    TEX_EFFECT,
    TEX_FADER_HEADER,
    SPR_BALLOON,
    SPR_TAIL_L,
    SPR_TAIL_R,
    SPR_TAIL_C,
    LBL_NAME,
    LBL_MESSAGE,
    MesBase,
    fukidashibaseflame,
    BTN_SKIP,
  }
}
