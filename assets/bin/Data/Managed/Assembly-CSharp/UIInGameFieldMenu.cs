// Decompiled with JetBrains decompiler
// Type: UIInGameFieldMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIInGameFieldMenu : UIInGamePopBase
{
  private static UIInGameFieldMenu instance;
  [SerializeField]
  protected Transform portrait;
  [SerializeField]
  protected Transform landscape;
  [SerializeField]
  protected UIButton[] menuBtns;
  private Transform _transform;
  private const string HomeButtonName = "BTN_HOME";
  private const string LoungeButtonName = "BTN_LOUNGE";
  private const string ClanButtonName = "BTN_CLAN";
  private const string FieldMemberButtonName = "BTN_MEMBER_LIST";
  private const string LoungeMemberButtonName = "BTN_LOUNGE_MEMBER";
  private const string mapButtonName = "BTN_MAP";
  private const string eventButtonName = "BTN_EVENT";

  public static UIInGameFieldMenu I => UIInGameFieldMenu.instance;

  public static bool IsValid()
  {
    return Object.op_Inequality((Object) UIInGameFieldMenu.instance, (Object) null);
  }

  protected override void Awake()
  {
    UIInGameFieldMenu.instance = this;
    this._transform = ((Component) this).transform;
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
    {
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
      this.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    }
    base.Awake();
    if (!FieldManager.IsValidInGameNoQuest())
      ((Component) this).gameObject.SetActive(false);
    bool isVisible = LoungeMatchingManager.IsValidInLounge();
    bool flag = ClanMatchingManager.IsValidInClan();
    this.SetVisibleButton("BTN_HOME", isVisible | flag);
    this.SetVisibleButton("BTN_LOUNGE", !isVisible);
    this.SetVisibleButton("BTN_CLAN", !flag);
    this.SetVisibleButton("BTN_MEMBER_LIST", isVisible);
    this.SetVisibleButton("BTN_LOUNGE_MEMBER", !isVisible);
  }

  private void OnDestroy()
  {
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    if (!Object.op_Equality((Object) UIInGameFieldMenu.instance, (Object) this))
      return;
    UIInGameFieldMenu.instance = (UIInGameFieldMenu) null;
  }

  public void OnClickReturn()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      return;
    string event_name = "RETURN";
    if (LoungeMatchingManager.IsValidInLounge())
      event_name = "RETURN_LOUNGE";
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIInGameFieldMenu.OnClickReturn", ((Component) this).gameObject, event_name);
  }

  private void OnScreenRotate(bool is_portrait)
  {
    if (is_portrait)
    {
      Utility.Attach(this.portrait, this._transform);
    }
    else
    {
      if (Object.op_Inequality((Object) this.landscape, (Object) null) && SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyInGameMenuPosition)
      {
        DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
        UIWidget component = ((Component) this.landscape).GetComponent<UIWidget>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.leftAnchor.absolute = specialDeviceInfo.InGameMenuAnchorLandscape.left;
          component.rightAnchor.absolute = specialDeviceInfo.InGameMenuAnchorLandscape.right;
          component.bottomAnchor.absolute = specialDeviceInfo.InGameMenuAnchorLandscape.bottom;
          component.topAnchor.absolute = specialDeviceInfo.InGameMenuAnchorLandscape.top;
          component.UpdateAnchors();
        }
      }
      Utility.Attach(this.landscape, this._transform);
    }
  }

  public void SetDisableEventButton(bool disable)
  {
    if (this.menuBtns == null)
      return;
    int index = 0;
    for (int length = this.menuBtns.Length; index < length; ++index)
    {
      if (Object.op_Inequality((Object) this.menuBtns[index], (Object) null) && ((Object) this.menuBtns[index]).name == "BTN_EVENT")
        this.menuBtns[index].isEnabled = !disable;
    }
  }

  public void SetDisableMapButton(bool disable)
  {
    if (this.menuBtns == null)
      return;
    int index = 0;
    for (int length = this.menuBtns.Length; index < length; ++index)
    {
      if (Object.op_Inequality((Object) this.menuBtns[index], (Object) null) && ((Object) this.menuBtns[index]).name == "BTN_MAP")
        this.menuBtns[index].isEnabled = !disable;
    }
  }

  public void SetDisableButtons(bool disable)
  {
    if (this.menuBtns == null)
      return;
    int index = 0;
    for (int length = this.menuBtns.Length; index < length; ++index)
    {
      if (Object.op_Inequality((Object) this.menuBtns[index], (Object) null))
        this.menuBtns[index].isEnabled = !disable;
    }
  }

  public void SetEnableButton(string btn_name, bool is_enable)
  {
    if (this.menuBtns == null)
      return;
    int index = 0;
    for (int length = this.menuBtns.Length; index < length; ++index)
    {
      if (Object.op_Inequality((Object) this.menuBtns[index], (Object) null) && ((Object) this.menuBtns[index]).name == btn_name)
        this.menuBtns[index].isEnabled = is_enable;
    }
  }

  private void SetVisibleButton(string btn_name, bool isVisible)
  {
    if (this.menuBtns == null)
      return;
    for (int index = 0; index < this.menuBtns.Length; ++index)
    {
      if (Object.op_Inequality((Object) this.menuBtns[index], (Object) null) && ((Object) this.menuBtns[index]).name == btn_name)
        ((Component) this.menuBtns[index]).gameObject.SetActive(!isVisible);
    }
  }

  public void SetButtonOnTutorial()
  {
    foreach (Behaviour menuBtn in this.menuBtns)
      menuBtn.enabled = false;
  }

  public bool IsPopMenu() => this.isPopMenu;
}
