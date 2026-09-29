// Decompiled with JetBrains decompiler
// Type: TutorialMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TutorialMessage : UIBehaviour
{
  private UITexture m_textureBG;
  private UIPanel m_panelRoot;
  private TutorialMessage.State m_status;
  private bool waiting;
  private TutorialMessage.TutorialData m_tutorial;
  public Transform m_last_target;
  private const int TUTORIAL_MESSAGE_MAX = 8;
  private System.Action onCloseCallback;
  private bool enableSkip;
  private int skipSectionRunCount;
  [SerializeField]
  [Range(0.0f, 1f)]
  private Vector3 HOLE_SIZE = new Vector3(0.3f, 0.2f, 1f);
  private BetterList<TutorialMessage.CursorInfo> cursorAttachList = new BetterList<TutorialMessage.CursorInfo>();

  private UITexture TextureBG
  {
    get
    {
      if (Object.op_Equality((Object) this.m_textureBG, (Object) null))
      {
        Transform ctrl = this.GetCtrl((Enum) TutorialMessage.UI.TEX_BG);
        if (Object.op_Equality((Object) ctrl, (Object) null))
          return (UITexture) null;
        this.m_textureBG = ((Component) ctrl).GetComponent<UITexture>();
      }
      return this.m_textureBG;
    }
  }

  private UIPanel PanelRoot
  {
    get
    {
      if (Object.op_Equality((Object) this.m_panelRoot, (Object) null))
        this.m_panelRoot = ((Component) this).GetComponent<UIPanel>();
      return this.m_panelRoot;
    }
  }

  private int GetTweenCtrlID(TutorialMessage.TWEEN_CTRL_ID _enum) => (int) _enum;

  public bool IsEnableMessage() => this.m_tutorial != null || this.isOpen;

  public bool IsOnlyShowImage()
  {
    return this.m_tutorial != null && this.m_tutorial.CurrentShowImage() && !this.m_tutorial.CurrentShowMessage();
  }

  public bool isErrorResend { get; private set; }

  public bool isErrorResendQuestGacha { get; private set; }

  private void SetErrorResendFlag(FORCE_RESEND_DIALOG_FLAG flag)
  {
    if (flag == FORCE_RESEND_DIALOG_FLAG.NONE)
      return;
    if (flag != FORCE_RESEND_DIALOG_FLAG.FLAG_UP)
    {
      if (flag != FORCE_RESEND_DIALOG_FLAG.FLAG_DOWN)
        return;
      this.isErrorResend = false;
    }
    else
      this.isErrorResend = true;
  }

  public void SetErrorResendQuestGachaFlag()
  {
    this.isErrorResendQuestGacha = false;
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
      return;
    bool flag = MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1);
    if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_START))
    {
      this.isErrorResendQuestGacha = false;
    }
    else
    {
      if (!flag)
        return;
      this.isErrorResendQuestGacha = true;
    }
  }

  public override bool IsTransitioning() => false;

  private void OnEnable()
  {
    InputManager.OnTouchOffAlways += new InputManager.OnTouchDelegate(this.OnTouchOffAlways);
  }

  private void OnDisable()
  {
    InputManager.OnTouchOffAlways -= new InputManager.OnTouchDelegate(this.OnTouchOffAlways);
  }

  private void OnTouchOffAlways(InputManager.TouchInfo info)
  {
    if (!this.enableSkip || (double) this.GetComponent<UIWidget>((Enum) TutorialMessage.UI.SPR_MESSAGE).finalAlpha < 0.99000000953674316 || MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
      return;
    this.enableSkip = false;
    this.SkipTween((Enum) TutorialMessage.UI.OBJ_DESC_ROOT, tween_ctrl_id: this.GetTweenCtrlID(TutorialMessage.TWEEN_CTRL_ID.MESSAGE));
    this.SkipTween((Enum) TutorialMessage.UI.OBJ_DESC_ROOT, tween_ctrl_id: this.GetTweenCtrlID(TutorialMessage.TWEEN_CTRL_ID.IMAGE));
    this.SkipTween(TutorialMessage.GetCursor(), (Enum) TutorialMessage.UI.SPR_TUTORIAL_CURSOR_DOWN);
    this.HideFocusFrame();
  }

  protected override void OnDestroy()
  {
    if (AppMain.isApplicationQuit)
      return;
    if (Object.op_Inequality((Object) this.TextureBG, (Object) null))
    {
      this.TextureBG.material.SetVector("_HoleSize", Vector4.zero);
      this.TextureBG.material.SetVector("_HolePos", Vector4.zero);
    }
    base.OnDestroy();
  }

  public void SetSkipSectionRunCount(int len) => this.skipSectionRunCount = Mathf.Max(0, len);

  private bool IsTutorialCompleted(string scene_name, string section_name)
  {
    return !Singleton<TutorialMessageTable>.IsValid() || Singleton<TutorialMessageTable>.I.ReadData.HasReadAll() || !Singleton<TutorialMessageTable>.I.HasSection(section_name);
  }

  private bool SetupTutorialData(
    string scene_name,
    string section_name,
    bool is_force,
    bool is_new_section = false,
    string event_name = null)
  {
    this.m_tutorial = (TutorialMessage.TutorialData) null;
    if (!Singleton<TutorialMessageTable>.IsValid())
      return false;
    TutorialMessageTable.TutorialMessageData enableExecTutorial = Singleton<TutorialMessageTable>.I.GetEnableExecTutorial(section_name, is_force, is_new_section, event_name);
    if (enableExecTutorial == null || enableExecTutorial.messageData.Count == 0)
      return false;
    this.m_tutorial = new TutorialMessage.TutorialData();
    this.m_tutorial.Init(enableExecTutorial);
    this.StartCoroutine(this._LoadMessageImage(this.m_tutorial));
    return true;
  }

  private IEnumerator _LoadMessageImage(TutorialMessage.TutorialData tutorial_data)
  {
    LoadingQueue lo_queue = new LoadingQueue((MonoBehaviour) this);
    List<LoadObject> list = new List<LoadObject>();
    this.m_tutorial.Messages.messageData.ForEach((Action<TutorialMessageTable.TutorialMessageData.MessageData>) (msg =>
    {
      if (msg == null)
        list.Add((LoadObject) null);
      else if (string.IsNullOrEmpty(msg.imageResourceName))
        list.Add((LoadObject) null);
      else
        list.Add(lo_queue.Load(RESOURCE_CATEGORY.UI, msg.imageResourceName));
    }));
    if (lo_queue.IsLoading())
      yield return (object) lo_queue.Wait();
    int index = -1;
    list.ForEach((Action<LoadObject>) (data =>
    {
      ++index;
      if (data == null)
        return;
      Transform t = ResourceUtility.Realizes(data.loadedObject, this.GetCtrl((Enum) TutorialMessage.UI.OBJ_IMAGE_ROOT), 5);
      if (!Object.op_Inequality((Object) t, (Object) null))
        return;
      ((Object) t).name = index.ToString();
      tutorial_data.SetImage(index, t);
    }));
  }

  public void ForceRun(string scene_name, string section_name, System.Action callback = null)
  {
    if (!this.SetupTutorialData(scene_name, section_name, true))
    {
      if (callback == null)
        return;
      callback();
    }
    else
    {
      this.onCloseCallback = callback;
      if (section_name == "TutorialStep4_1_1")
        this.StartCoroutine(this.Delay(1f));
      else
        this.StartTutorial(false);
    }
  }

  private IEnumerator Delay(float delayTime)
  {
    yield return (object) new WaitForSeconds(delayTime);
    this.StartTutorial(false);
  }

  public void Run(
    string scene_name,
    string section_name,
    bool is_new_section,
    bool hide_cursol,
    System.Action callback = null)
  {
    if (this.skipSectionRunCount > 0)
    {
      --this.skipSectionRunCount;
    }
    else
    {
      if (hide_cursol)
        this.HideCursor(true);
      if (this.IsTutorialCompleted(scene_name, section_name))
      {
        if (callback == null)
          return;
        callback();
      }
      else if (!this.SetupTutorialData(scene_name, section_name, false, is_new_section))
      {
        if (callback == null)
          return;
        callback();
      }
      else
      {
        this.onCloseCallback = callback;
        this.StartTutorial(hide_cursol);
      }
    }
  }

  public void TriggerRun(string scene_name, string section_name, string event_name)
  {
    if (this.m_status != TutorialMessage.State.CLOSE)
      return;
    this.HideCursor(true);
    if (this.IsTutorialCompleted(scene_name, section_name) || !this.SetupTutorialData(scene_name, section_name, false, event_name: event_name))
      return;
    this.StartTutorial(true);
  }

  public void SubmitCursor(string sender_name, string event_name)
  {
    if (this.m_tutorial == null || MonoBehaviourSingleton<GameSceneManager>.I.isOpenImportantDialog || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "CommonErrorDialog")
      return;
    if (event_name == "TUTORIAL_NEXT")
    {
      if (this.waiting)
        return;
    }
    else
    {
      string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
      if (currentSectionName.Contains("InGameMain") && currentSectionName.Contains("Confirm"))
        return;
    }
    if (this.m_status == TutorialMessage.State.DESC)
    {
      this.m_tutorial.DestoryCurrentImage();
      this.ChangeNext();
    }
    else
    {
      if (this.m_status != TutorialMessage.State.WAIT)
        return;
      if (this.m_tutorial != null && this.m_tutorial.Current() != null && this.m_tutorial.Current().is_wait_event)
      {
        if (!this.IsExpectedEvent(sender_name, event_name))
          return;
        this.SetReadCurrentTutorial();
        this.m_tutorial.DestoryCurrentImage();
        this.m_status = TutorialMessage.State.CLOSE;
        this.HideCursor(true);
      }
      else
      {
        if (!this.IsExpectedEvent(sender_name, event_name))
          return;
        this.SetReadCurrentTutorial();
        if (this.m_tutorial != null)
          this.m_tutorial.DestoryCurrentImage();
        this.m_status = TutorialMessage.State.CLOSE;
        this.HideCursor(true);
      }
    }
  }

  private bool IsExpectedEvent(string sender_name, string event_name)
  {
    if (this.m_tutorial == null)
      return true;
    string str = this.m_tutorial.CurrentWaitEventName();
    return string.IsNullOrEmpty(str) || str == event_name;
  }

  private void Finish(bool show_image)
  {
    if (this.onCloseCallback != null)
      this.onCloseCallback();
    this.onCloseCallback = (System.Action) null;
    if (!Object.op_Inequality((Object) this.TextureBG, (Object) null) || this.waiting)
      return;
    if (!show_image)
      ((Behaviour) this.TextureBG).enabled = false;
    this.enableSkip = true;
  }

  private void SetReadCurrentTutorial()
  {
    if (!Singleton<TutorialMessageTable>.IsValid() || Singleton<TutorialMessageTable>.I.ReadData == null || this.m_tutorial == null || this.m_tutorial.Messages == null || this.m_tutorial.count < 1)
      return;
    Singleton<TutorialMessageTable>.I.ReadData.SetReadId(this.m_tutorial.Messages.tutorialId, true);
    this.SaveRead();
  }

  private void SaveRead()
  {
    if (!Singleton<TutorialMessageTable>.IsValid() || Singleton<TutorialMessageTable>.I.ReadData == null)
      return;
    Singleton<TutorialMessageTable>.I.ReadData.Save();
  }

  protected override void OnOpen() => base.OnOpen();

  protected override void OnClose() => base.OnClose();

  public void TutorialClose()
  {
    Transform ctrl = this.GetCtrl((Enum) TutorialMessage.UI.OBJ_IMAGE_ROOT);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
      ctrl.DestroyChildren();
    this.m_tutorial = (TutorialMessage.TutorialData) null;
    this.Close();
    this.m_status = TutorialMessage.State.CLOSE;
  }

  private void StartTutorial(bool is_hide_cursol)
  {
    this.skipSectionRunCount = 0;
    this.m_status = TutorialMessage.State.INIT;
    if (this.m_tutorial == null)
      return;
    this.SetErrorResendFlag(this.m_tutorial.Messages.resendFrag);
    this.HideCursor(is_hide_cursol, false);
    this.Open();
    this.m_status = TutorialMessage.State.DESC;
    this.UpdateMessage();
    if (this.m_tutorial != null && this.m_tutorial.Messages != null && !string.IsNullOrEmpty(this.m_tutorial.Messages.strSetBit))
    {
      TUTORIAL_MENU_BIT? setBit = this.m_tutorial.Messages.GetSetBit();
      if (setBit.HasValue)
        TutorialMessageTable.SendTutorialBit(setBit.Value);
    }
    this.CheckLastMessage();
  }

  private void UpdateMessage()
  {
    this.SetActiveMessage();
    if (this.m_status != TutorialMessage.State.DESC || this.m_tutorial == null)
      return;
    TutorialMessageTable.TutorialMessageData.MessageData messageData = this.m_tutorial.Current();
    if (messageData == null)
      return;
    string text = messageData.message.Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
    this.UpdateMessagePosition(messageData.position_type);
    this.PutText(TutorialMessage.UI.LBL_MESSAGE, text);
    if ((double) messageData.wait == 0.0)
    {
      this.StartMessage();
    }
    else
    {
      this.WaitingUI(true);
      this.waiting = true;
      this.enableSkip = false;
      this.StartCoroutine(this.DoWaitToStartMessage(messageData.wait));
    }
  }

  private void WaitingUI(bool is_wait_start)
  {
    if (is_wait_start)
    {
      this.SetActive((Enum) TutorialMessage.UI.OBJ_MESSAGE_ROOT, false);
      this.SetActive((Enum) TutorialMessage.UI.OBJ_IMAGE_ROOT, false);
      this.SetColor((Enum) TutorialMessage.UI.OBJ_DESC_ROOT, Color.white);
      this.HideBGHole();
    }
    else
      this.SetActiveMessage();
  }

  private IEnumerator DoWaitToStartMessage(float time)
  {
    yield return (object) new WaitForSeconds(time);
    this.WaitingUI(false);
    this.StartMessage();
    this.waiting = false;
    this.enableSkip = true;
    ((Behaviour) this.TextureBG).enabled = true;
    if (this.m_tutorial != null)
    {
      TutorialMessageTable.TutorialMessageData.MessageData messageData = this.m_tutorial.Current();
      if (messageData != null && messageData.has_target)
        ((Behaviour) this.TextureBG).enabled = false;
    }
  }

  private void StartMessage()
  {
    if (this.m_tutorial == null)
      return;
    TutorialMessageTable.TutorialMessageData.MessageData data = this.m_tutorial.Current();
    if (data == null)
      return;
    int tweenCtrlId = this.GetTweenCtrlID(this.m_tutorial.CurrentShowImage() ? TutorialMessage.TWEEN_CTRL_ID.IMAGE : TutorialMessage.TWEEN_CTRL_ID.MESSAGE);
    if (data.has_target)
    {
      EventDelegate.Callback callback = (EventDelegate.Callback) (() =>
      {
        this.enableSkip = false;
        this.HideFocusFrame();
      });
      this.ResetTween((Enum) TutorialMessage.UI.OBJ_DESC_ROOT, this.GetTweenCtrlID(TutorialMessage.TWEEN_CTRL_ID.MESSAGE));
      this.ResetTween((Enum) TutorialMessage.UI.OBJ_DESC_ROOT, this.GetTweenCtrlID(TutorialMessage.TWEEN_CTRL_ID.IMAGE));
      if (!this.m_tutorial.CurrentShowMessageOrImage() && !this.IsWaitTween())
        callback();
      else
        this.TutorialPlayTween((Enum) TutorialMessage.UI.OBJ_DESC_ROOT, callback, tweenCtrlId);
      this.FocusCursor(data);
    }
    else
    {
      EventDelegate.Callback callback = (EventDelegate.Callback) (() =>
      {
        bool flag = this.m_tutorial == null;
        if (!flag && this.m_tutorial != null && !this.m_tutorial.HasNext() && string.IsNullOrEmpty(this.m_tutorial.CurrentWaitEventName()))
          flag = true;
        if (flag)
        {
          this.TutorialClose();
        }
        else
        {
          if ((this.m_tutorial == null ? 0 : (this.m_tutorial.CurrentShowImage() ? 1 : 0)) != 0)
            return;
          this.SubmitCursor("SELF", "TUTORIAL_NEXT");
        }
      });
      this.ResetTween((Enum) TutorialMessage.UI.OBJ_DESC_ROOT, this.GetTweenCtrlID(TutorialMessage.TWEEN_CTRL_ID.MESSAGE));
      this.ResetTween((Enum) TutorialMessage.UI.OBJ_DESC_ROOT, this.GetTweenCtrlID(TutorialMessage.TWEEN_CTRL_ID.IMAGE));
      if (!this.m_tutorial.CurrentShowMessageOrImage() && !this.IsWaitTween())
        callback();
      else
        this.TutorialPlayTween((Enum) TutorialMessage.UI.OBJ_DESC_ROOT, callback, tweenCtrlId);
    }
    this.UpdateFocusFrame();
  }

  private bool IsWaitTween()
  {
    return MonoBehaviourSingleton<UIManager>.I.IsTransitioning() || GameSceneManager.isAutoEventSkip;
  }

  private void TutorialPlayTween(Enum ui, EventDelegate.Callback callback, int tween_ctrl_id)
  {
    if (this.IsWaitTween() || this.m_tutorial != null && this.m_tutorial.CurrentShowImage())
      this.StartCoroutine(this._TweenCoroutine(ui, callback, tween_ctrl_id));
    else
      this.PlayTween(ui, callback: callback, is_input_block: false, tween_ctrl_id: tween_ctrl_id);
  }

  private IEnumerator _TweenCoroutine(Enum ui, EventDelegate.Callback callback, int tween_ctrl_id)
  {
    while (this.IsWaitTween())
      yield return (object) null;
    if (this.m_tutorial != null && this.m_tutorial.CurrentShowImage())
    {
      while (this.m_tutorial.IsLoadingCurrentImage())
        yield return (object) null;
      this.m_tutorial.SetActiveCurrentImage();
    }
    this.PlayTween(ui, callback: callback, is_input_block: false, tween_ctrl_id: tween_ctrl_id);
  }

  private void UpdateFocusFrame()
  {
    this.HideFocusFrame();
    if (this.m_tutorial == null)
      return;
    TutorialMessageTable.TutorialMessageData.MessageData messageData = this.m_tutorial.Current();
    if (messageData == null || string.IsNullOrEmpty(messageData.focusFrame))
      return;
    string[] strArray = messageData.focusFrame.Split(',');
    if (strArray.Length < 4)
      return;
    UIWidget component = this.GetComponent<UIWidget>((Enum) TutorialMessage.UI.SPR_FORCS_FRAME);
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    float result1;
    float.TryParse(strArray[0], out result1);
    float result2;
    float.TryParse(strArray[1], out result2);
    int result3;
    int.TryParse(strArray[2], out result3);
    int result4;
    int.TryParse(strArray[3], out result4);
    component.cachedTransform.localPosition = new Vector3(result1, result2, 0.0f);
    component.width = result3;
    component.height = result4;
    this.SetActive((Enum) TutorialMessage.UI.WGT_FORCS_FRAME, true);
    this.ResetTween((Enum) TutorialMessage.UI.WGT_FORCS_FRAME);
    this.PlayTween((Enum) TutorialMessage.UI.WGT_FORCS_FRAME, is_input_block: false);
  }

  private void HideFocusFrame() => this.SetActive((Enum) TutorialMessage.UI.WGT_FORCS_FRAME, false);

  private void UpdateMessagePosition(
    TutorialMessageTable.TutorialMessageData.MessageData.Position pos)
  {
    TutorialMessage.UI label_enum = TutorialMessage.UI.OBJ_ANCHOR_MESSAGE_UP;
    switch (pos)
    {
      case TutorialMessageTable.TutorialMessageData.MessageData.Position.DOWN:
        label_enum = TutorialMessage.UI.OBJ_ANCHOR_MESSAGE_DOWN;
        break;
      case TutorialMessageTable.TutorialMessageData.MessageData.Position.CENTER:
        label_enum = TutorialMessage.UI.OBJ_ANCHOR_MESSAGE_CENTER;
        break;
    }
    Transform ctrl1 = this.GetCtrl((Enum) label_enum);
    Transform ctrl2 = this.GetCtrl((Enum) TutorialMessage.UI.OBJ_MESSAGE_ROOT);
    if (!Object.op_Inequality((Object) ctrl1, (Object) null) || !Object.op_Inequality((Object) ctrl2, (Object) null))
      return;
    ctrl2.position = ctrl1.position;
  }

  private void SetActiveMessage()
  {
    Transform ctrl1 = this.GetCtrl((Enum) TutorialMessage.UI.OBJ_DESC_ROOT);
    if (Object.op_Equality((Object) ctrl1, (Object) null))
      return;
    Transform ctrl2 = this.GetCtrl((Enum) TutorialMessage.UI.OBJ_MESSAGE_ROOT);
    if (Object.op_Equality((Object) ctrl2, (Object) null))
      return;
    Transform ctrl3 = this.GetCtrl((Enum) TutorialMessage.UI.OBJ_IMAGE_ROOT);
    if (Object.op_Equality((Object) ctrl3, (Object) null))
      return;
    if (this.m_tutorial != null)
    {
      ((Component) ctrl1).gameObject.SetActive(true);
      ((Component) ctrl3).gameObject.SetActive(false);
      ((Component) ctrl2).gameObject.SetActive(false);
      if (this.m_tutorial.CurrentShowImage())
        ((Component) ctrl3).gameObject.SetActive(true);
      if (!this.m_tutorial.CurrentShowMessage())
        return;
      ((Component) ctrl2).gameObject.SetActive(true);
    }
    else
      ((Component) ctrl1).gameObject.SetActive(false);
  }

  private void PutText(TutorialMessage.UI label_enum, string text)
  {
    Transform ctrl = this.GetCtrl((Enum) label_enum);
    if (!Object.op_Inequality((Object) ctrl, (Object) null))
      return;
    UILabel component = ((Component) ctrl).GetComponent<UILabel>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.text = text;
  }

  private void PlayVoice(int voice_id) => SoundManager.PlayVoice(voice_id);

  public void UpdateFocusCursol()
  {
    TutorialMessageTable.TutorialMessageData.MessageData data = this.m_tutorial.Current();
    if (data == null || !data.has_target)
      return;
    this.HideCursor(true, false);
    this.FocusCursor(data);
  }

  private void FocusCursor(
    TutorialMessageTable.TutorialMessageData.MessageData data)
  {
    GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
    if (Object.op_Equality((Object) currentSection, (Object) null) || Object.op_Equality((Object) currentSection._transform, (Object) null))
      return;
    Transform target = (Transform) null;
    Transform transform1 = currentSection._transform;
    string cursorTarget = data.cursorTarget;
    if (cursorTarget.Contains("[ID]") || cursorTarget.Contains("[ID!]"))
    {
      bool is_not_equal = cursorTarget.Contains("[ID!]");
      int id = 0;
      if (int.TryParse(cursorTarget.Remove(0, is_not_equal ? 5 : 4), out id))
      {
        ItemIcon target_icon = (ItemIcon) null;
        ItemIcon[] componentsInChildren = ((Component) MonoBehaviourSingleton<UIManager>.I.uiRootTransform).GetComponentsInChildren<ItemIcon>();
        if (componentsInChildren != null)
          Array.ForEach<ItemIcon>(componentsInChildren, (Action<ItemIcon>) (_data =>
          {
            if (Object.op_Inequality((Object) target, (Object) null) || Object.op_Equality((Object) _data, (Object) null) || Object.op_Equality((Object) _data.transform, (Object) null))
              return;
            if (is_not_equal)
            {
              if (_data.GetItemID == id)
                return;
              target_icon = _data;
            }
            else
            {
              if (_data.GetItemID != id)
                return;
              target_icon = _data;
            }
          }));
        if (Object.op_Inequality((Object) target_icon, (Object) null))
        {
          UIButton componentInParent = ((Component) target_icon).GetComponentInParent<UIButton>();
          if (Object.op_Inequality((Object) componentInParent, (Object) null))
            target = ((Component) componentInParent).transform;
        }
      }
    }
    else
    {
      string[] strArray = cursorTarget.Split('/');
      int index = 0;
      for (int length = strArray.Length; index < length; ++index)
      {
        target = Utility.FindChild(transform1, strArray[index]);
        transform1 = target;
      }
    }
    if (Object.op_Equality((Object) target, (Object) null) && MonoBehaviourSingleton<UIManager>.IsValid())
    {
      string[] strArray = cursorTarget.Split('/');
      Transform transform2 = MonoBehaviourSingleton<UIManager>.I.uiRootTransform;
      int index = 0;
      for (int length = strArray.Length; index < length; ++index)
      {
        target = Utility.FindActiveChild(transform2, strArray[index]);
        transform2 = target;
      }
      if (Object.op_Equality((Object) target, (Object) null))
        return;
    }
    this.m_last_target = TutorialMessage.AttachCursor(target, data);
    FOCUS_PATTERN focusPattern = this.m_tutorial.Current().focusPattern;
    if (this.m_tutorial != null && focusPattern != FOCUS_PATTERN.CLEAR_COLOR)
      this.ShowBGHole(((Component) target).transform.position, focusPattern, target);
    else
      this.HideBGHole();
  }

  private void HideBGHole()
  {
    UITexture textureBg = this.TextureBG;
    if (Object.op_Equality((Object) textureBg, (Object) null))
      return;
    ((Behaviour) textureBg).enabled = true;
    textureBg.material.SetVector("_HoleSize", new Vector4((float) Screen.width, (float) Screen.height, 1f));
    this.RefreeshDraw();
  }

  private void ShowBGHole(Vector3 hole_pos, FOCUS_PATTERN focus, Transform target = null)
  {
    UITexture textureBg = this.TextureBG;
    if (Object.op_Equality((Object) textureBg, (Object) null))
      return;
    ((Behaviour) textureBg).enabled = true;
    Vector3 viewportPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.WorldToViewportPoint(hole_pos);
    Vector4 vector4;
    // ISSUE: explicit constructor call
    ((Vector4) ref vector4).\u002Ector((float) ((double) viewportPoint.x * 2.0 - 1.0), (float) ((double) viewportPoint.y * 2.0 - 1.0), 1f);
    Vector3 holeSize = this.HOLE_SIZE;
    BoxCollider component = ((Component) target).GetComponent<BoxCollider>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      float num1 = focus == FOCUS_PATTERN.TARGET_FOCUS ? component.size.x * target.localScale.x : 0.0f;
      double num2 = focus == FOCUS_PATTERN.TARGET_FOCUS ? (double) component.size.y * (double) target.localScale.y : 0.0;
      float num3 = num1 / (float) Screen.width;
      float num4 = (float) num2 / (float) Screen.height;
      holeSize.x = num3;
      holeSize.y = num4;
      if (focus == FOCUS_PATTERN.TARGET_FOCUS)
      {
        vector4.x += component.center.x / (float) Screen.width * target.localScale.x;
        vector4.y += component.center.y / (float) Screen.height * target.localScale.y;
      }
      else
      {
        vector4.x = 0.0f;
        vector4.y = 0.0f;
      }
    }
    textureBg.material.SetVector("_HoleSize", Vector4.op_Implicit(holeSize));
    textureBg.material.SetVector("_HolePos", vector4);
    this.RefreeshDraw();
  }

  private void RefreeshDraw()
  {
    if (Object.op_Equality((Object) this.PanelRoot, (Object) null))
      return;
    this.PanelRoot.Refresh();
  }

  private void HideCursor(bool force = false, bool is_close = true)
  {
    this.HideBGHole();
    if (((this.m_status != TutorialMessage.State.WAIT ? 0 : (Object.op_Inequality((Object) this.m_last_target, (Object) null) ? 1 : 0)) | (force ? 1 : 0)) == 0)
      return;
    GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
    if (Object.op_Equality((Object) currentSection, (Object) null) || Object.op_Equality((Object) ((Component) currentSection).transform, (Object) null))
      return;
    TutorialMessage.DetachCursor(this.m_last_target, is_close);
    this.m_last_target = (Transform) null;
  }

  public void ChangeNext()
  {
    if (!this.isOpen || this.m_status != TutorialMessage.State.DESC || this.m_tutorial == null)
      return;
    if (this.m_tutorial.HasNext())
    {
      this.m_tutorial.ShiftNext();
      this.UpdateMessage();
    }
    this.CheckLastMessage();
  }

  private void CheckLastMessage()
  {
    if (this.m_tutorial == null || this.m_tutorial.HasNext())
      return;
    bool show_image = this.m_tutorial.CurrentShowImage();
    if (!string.IsNullOrEmpty(this.m_tutorial.CurrentWaitEventName()))
    {
      this.m_status = TutorialMessage.State.WAIT;
    }
    else
    {
      this.SetReadCurrentTutorial();
      this.m_tutorial = (TutorialMessage.TutorialData) null;
    }
    this.Finish(show_image);
  }

  private float CalcCenterXOffset(UIWidget widget)
  {
    double num = -(double) widget.pivotOffset.x * (double) widget.width;
    return (float) ((num + (double) ((float) num + (float) widget.width)) * 0.5);
  }

  private float CalcCenterYOffset(UIWidget widget)
  {
    double num = -(double) widget.pivotOffset.y * (double) widget.height;
    return (float) ((num + (double) ((float) num + (float) widget.height)) * 0.5);
  }

  private void SetupCursor(Transform cursor, Transform target, float center_x, float center_y)
  {
    float num1 = 10f;
    float num2 = num1 * target.lossyScale.x;
    UISprite component = ((Component) cursor).GetComponent<UISprite>();
    Vector2 vector2 = Vector2.op_Multiply(new Vector2(center_x, center_y), target.lossyScale.x);
    double y = (double) this.PanelRoot.cachedTransform.InverseTransformPoint(new Vector3(0.0f, target.position.y + (vector2.y + (float) component.height * component.cachedTransform.lossyScale.y), 0.0f)).y;
    float num3 = 427f;
    double num4 = (double) num1;
    Vector2 offset;
    if (y + num4 < (double) num3)
    {
      // ISSUE: explicit constructor call
      ((Vector2) ref offset).\u002Ector(vector2.x, vector2.y + num2);
      component.pivot = UIWidget.Pivot.Bottom;
      component.flip = UIBasicSprite.Flip.Nothing;
    }
    else
    {
      // ISSUE: explicit constructor call
      ((Vector2) ref offset).\u002Ector(vector2.x, vector2.y - num2);
      component.pivot = UIWidget.Pivot.Top;
      component.flip = UIBasicSprite.Flip.Vertically;
    }
    cursor.localRotation = Quaternion.identity;
    ((Component) cursor).gameObject.AddComponent<TutorialUIObjectFollower>().Setup(target, offset);
  }

  private void SetupCursor(Transform cursor, UIWidget target_widget)
  {
    this.SetupCursor(cursor, target_widget.cachedTransform, this.CalcCenterXOffset(target_widget), this.CalcCenterYOffset(target_widget));
  }

  private void SetupCursor(Transform cursor, BoxCollider collider)
  {
    this.SetupCursor(cursor, ((Component) collider).transform, collider.center.x, collider.center.y);
  }

  private Transform _AttachTutorialCursor(
    Transform target,
    TutorialMessageTable.TutorialMessageData.MessageData data)
  {
    if (Object.op_Equality((Object) target, (Object) null))
      return (Transform) null;
    Transform transform = (Transform) null;
    if (data != null && data.cursorType == TutorialMessageTable.TutorialMessageData.MessageData.CursorType.MANUAL)
    {
      transform = this.CreateTutorialCursor();
      ((Component) transform).GetComponent<UISprite>().pivot = UIWidget.Pivot.Bottom;
      ((Component) transform).gameObject.AddComponent<TutorialUIObjectFollower>().Setup(target, Vector2.op_Multiply(data.cursorOffset, target.lossyScale.x));
      transform.localRotation = Quaternion.AngleAxis((float) data.cursorRotDeg, Vector3.forward);
    }
    else
    {
      BoxCollider component1;
      if (Object.op_Implicit((Object) (component1 = ((Component) target).GetComponent<BoxCollider>())))
      {
        transform = this.CreateTutorialCursor();
        this.SetupCursor(transform, component1);
      }
      else
      {
        UIWidget component2;
        if (Object.op_Implicit((Object) (component2 = ((Component) target).GetComponent<UIWidget>())))
        {
          transform = this.CreateTutorialCursor();
          this.SetupCursor(transform, component2);
        }
      }
    }
    if (Object.op_Inequality((Object) transform, (Object) null))
    {
      bool flag = false;
      if (this.m_tutorial != null && !string.IsNullOrEmpty(this.m_tutorial.Messages.messageData[0].message))
        flag = true;
      if (data != null && (double) data.cursorDelay >= 0.0)
      {
        UITweener component = this.GetComponent<UITweener>(transform, (Enum) TutorialMessage.UI.SPR_TUTORIAL_CURSOR_DOWN);
        if (Object.op_Inequality((Object) component, (Object) null))
          component.delay = data.cursorDelay;
      }
      if (flag)
      {
        this.ResetTween(transform, (Enum) TutorialMessage.UI.SPR_TUTORIAL_CURSOR_DOWN);
        this.PlayTween(transform, (Enum) TutorialMessage.UI.SPR_TUTORIAL_CURSOR_DOWN, is_input_block: false);
      }
      else
        this.SkipTween(transform, (Enum) TutorialMessage.UI.SPR_TUTORIAL_CURSOR_DOWN);
    }
    return transform;
  }

  private Transform CreateTutorialCursor()
  {
    this.SetActive((Enum) TutorialMessage.UI.OBJ_TUTORIAL_CURSOR, true);
    Transform tutorialCursor = ResourceUtility.Realizes((Object) ((Component) this.GetCtrl((Enum) TutorialMessage.UI.SPR_TUTORIAL_CURSOR_DOWN)).gameObject, ((Component) this.PanelRoot).transform);
    this.SetActive((Enum) TutorialMessage.UI.OBJ_TUTORIAL_CURSOR, false);
    return tutorialCursor;
  }

  private void _AttachCursor(
    Transform target,
    TutorialMessageTable.TutorialMessageData.MessageData data)
  {
    UIButton uiButton = ((Component) target).GetComponent<UIButton>();
    if (Object.op_Equality((Object) uiButton, (Object) null))
    {
      uiButton = ((Component) target).GetComponentInChildren<UIButton>();
      if (Object.op_Equality((Object) uiButton, (Object) null))
      {
        uiButton = ((Component) target).GetComponentInParent<UIButton>();
        if (Object.op_Equality((Object) uiButton, (Object) null))
          return;
      }
    }
    int num = 0;
    for (int size = this.cursorAttachList.size; num < size; ++num)
    {
      TutorialMessage.CursorInfo cursorAttach = this.cursorAttachList[num];
      if (Object.op_Inequality((Object) cursorAttach.cursor, (Object) null))
      {
        Object.Destroy((Object) ((Component) cursorAttach.cursor).gameObject);
        this.cursorAttachList.RemoveAt(num);
        --num;
        --size;
      }
    }
    this.cursorAttachList.Clear();
    this.cursorAttachList.Add(new TutorialMessage.CursorInfo()
    {
      target = target,
      button = ((Component) uiButton).gameObject,
      cursor = this._AttachTutorialCursor(target, data),
      isInDynamicList = Object.op_Inequality((Object) ((Component) target).GetComponentInParent<UIDynamicList>(), (Object) null)
    });
    this.m_last_target = target;
  }

  private void _DetachCursor(Transform target)
  {
    int num = 0;
    for (int size = this.cursorAttachList.size; num < size; ++num)
    {
      TutorialMessage.CursorInfo cursorAttach = this.cursorAttachList[num];
      if (Object.op_Equality((Object) cursorAttach.target, (Object) target))
      {
        Object.Destroy((Object) ((Component) cursorAttach.cursor).gameObject);
        this.cursorAttachList.RemoveAt(num);
        --num;
        --size;
      }
    }
  }

  private void _RemoveCursor(Transform cursor)
  {
    int num = 0;
    for (int size = this.cursorAttachList.size; num < size; ++num)
    {
      TutorialMessage.CursorInfo cursorAttach = this.cursorAttachList[num];
      if (Object.op_Equality((Object) cursorAttach.cursor, (Object) cursor))
      {
        Object.Destroy((Object) ((Component) cursorAttach.cursor).gameObject);
        this.cursorAttachList.RemoveAt(num);
        --num;
        --size;
      }
    }
  }

  public static Transform AttachCursor(
    Transform t,
    TutorialMessageTable.TutorialMessageData.MessageData data = null)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return (Transform) null;
    if (Object.op_Equality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null))
      return (Transform) null;
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage._AttachCursor(t, data);
    return t;
  }

  public static void DetachCursor(Transform t, bool is_close = true)
  {
    if (Object.op_Equality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null))
      return;
    if (is_close && MonoBehaviourSingleton<UIManager>.I.tutorialMessage.isOpen)
      MonoBehaviourSingleton<UIManager>.I.tutorialMessage.TutorialClose();
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage._DetachCursor(t);
  }

  public static void RemoveCursor(Transform cursor)
  {
    if (Object.op_Equality((Object) cursor, (Object) null) || Object.op_Equality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage._RemoveCursor(cursor);
  }

  public static bool IsActiveButton(GameObject button)
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.IsValid() || MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Equality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null) || Object.op_Equality((Object) button, (Object) null) || MonoBehaviourSingleton<GameSceneManager>.IsValid() && (MonoBehaviourSingleton<GameSceneManager>.I.isOpenImportantDialog || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "CommonErrorDialog"))
      return true;
    string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
    if (!string.IsNullOrEmpty(currentSectionName) && currentSectionName.Contains("InGameMain") && currentSectionName.Contains("Confirm"))
      return true;
    if (currentSectionName == "HomeTop" && (HomeBase.OnAfterGacha2Tutorial && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2) || HomeBase.OnTalkPamelaTutorial) || currentSectionName == "HomeTop" && HomeBase.OnClickQuestForTutorial)
      return false;
    BetterList<TutorialMessage.CursorInfo> cursorAttachList = MonoBehaviourSingleton<UIManager>.I.tutorialMessage.cursorAttachList;
    if (cursorAttachList.size == 0)
      return true;
    int num = 0;
    for (int size = cursorAttachList.size; num < size; ++num)
    {
      TutorialMessage.CursorInfo cursorInfo = cursorAttachList[num];
      if (Object.op_Equality((Object) cursorInfo.target, (Object) null) || Object.op_Equality((Object) cursorInfo.button, (Object) null) || Object.op_Equality((Object) cursorInfo.cursor, (Object) null))
      {
        cursorAttachList.RemoveAt(num);
        --num;
        --size;
      }
      else if (cursorInfo.isInDynamicList)
      {
        for (Transform transform = button.transform; Object.op_Inequality((Object) transform, (Object) null); transform = transform.parent)
        {
          if (Object.op_Equality((Object) cursorInfo.target, (Object) transform))
            return true;
        }
      }
      else if (Object.op_Equality((Object) cursorInfo.button, (Object) button))
        return true;
    }
    return ((Object) button).name == "TEX_BG";
  }

  public static Transform GetCursor(int index = 0)
  {
    TutorialMessage tutorialMessage = MonoBehaviourSingleton<UIManager>.I.tutorialMessage;
    if (Object.op_Equality((Object) tutorialMessage, (Object) null))
      return (Transform) null;
    return index + 1 > tutorialMessage.cursorAttachList.size ? (Transform) null : tutorialMessage.cursorAttachList[index].cursor;
  }

  private enum UI
  {
    OBJ_ROOT,
    OBJ_DESC_ROOT,
    OBJ_MESSAGE_ROOT,
    OBJ_IMAGE_ROOT,
    OBJ_ANCHOR_MESSAGE_UP,
    OBJ_ANCHOR_MESSAGE_DOWN,
    OBJ_ANCHOR_MESSAGE_CENTER,
    SPR_MESSAGE,
    LBL_MESSAGE,
    TEX_BG,
    OBJ_TUTORIAL_CURSOR,
    SPR_TUTORIAL_CURSOR_DOWN,
    WGT_FORCS_FRAME,
    SPR_FORCS_FRAME,
  }

  private enum State
  {
    CLOSE,
    INIT,
    DESC,
    WAIT,
  }

  private enum TWEEN_CTRL_ID
  {
    MESSAGE,
    IMAGE,
  }

  private class TutorialData
  {
    public int m_current_index;
    private bool[] is_loading_flag;
    private Transform[] load_image;

    public TutorialMessageTable.TutorialMessageData Messages { get; private set; }

    public string name => this.Messages.sectionName;

    public int count => this.Messages.messageData.Count;

    public TutorialMessageTable.TutorialMessageData.MessageData Current()
    {
      return this.Messages == null ? (TutorialMessageTable.TutorialMessageData.MessageData) null : this.Messages.messageData[this.m_current_index];
    }

    public string CurrentWaitEventName()
    {
      TutorialMessageTable.TutorialMessageData.MessageData messageData = this.Current();
      return messageData == null || !messageData.is_wait_event ? string.Empty : messageData.waitEventName;
    }

    public bool CurrentShowMessageOrImage() => this.CurrentShowMessage() || this.CurrentShowImage();

    public bool CurrentShowMessage()
    {
      TutorialMessageTable.TutorialMessageData.MessageData messageData = this.Current();
      return messageData != null && !string.IsNullOrEmpty(messageData.message);
    }

    public bool CurrentShowImage()
    {
      TutorialMessageTable.TutorialMessageData.MessageData messageData = this.Current();
      return messageData != null && !string.IsNullOrEmpty(messageData.imageResourceName);
    }

    public bool IsLoadingCurrentImage()
    {
      return this.CurrentShowImage() && this.Current() != null && this.is_loading_flag[this.m_current_index];
    }

    public void SetImage(int index, Transform t)
    {
      if (this.load_image == null || this.load_image.Length <= index || this.is_loading_flag == null || this.is_loading_flag.Length <= index)
        return;
      this.load_image[index] = t;
      ((Component) this.load_image[index]).gameObject.SetActive(false);
      this.is_loading_flag[index] = false;
    }

    public void SetActiveCurrentImage()
    {
      if (this.load_image == null || this.load_image.Length <= this.m_current_index)
        return;
      ((Component) this.load_image[this.m_current_index]).gameObject.SetActive(true);
    }

    public void DestoryCurrentImage()
    {
      if (!Object.op_Inequality((Object) this.load_image[this.m_current_index], (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.load_image[this.m_current_index]).gameObject);
      this.load_image[this.m_current_index] = (Transform) null;
    }

    public void Init(TutorialMessageTable.TutorialMessageData data)
    {
      this.m_current_index = 0;
      this.Messages = data;
      this.load_image = new Transform[this.Messages.messageData.Count];
      this.is_loading_flag = new bool[this.Messages.messageData.Count];
      int index = 0;
      this.Messages.messageData.ForEach((Action<TutorialMessageTable.TutorialMessageData.MessageData>) (msg =>
      {
        this.load_image[index] = (Transform) null;
        this.is_loading_flag[index] = !string.IsNullOrEmpty(msg.imageResourceName);
        ++index;
      }));
    }

    public bool HasNext() => this.Messages != null && this.m_current_index + 1 < this.count;

    public bool ShiftNext()
    {
      if (!this.HasNext())
        return false;
      ++this.m_current_index;
      return true;
    }
  }

  private struct CursorInfo
  {
    public Transform target;
    public GameObject button;
    public Transform cursor;
    public bool isInDynamicList;
  }
}
