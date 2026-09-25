// Decompiled with JetBrains decompiler
// Type: ChatHeaderButtonController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ChatHeaderButtonController : MonoBehaviour
{
  private const int DEPTH_OFFSET = 30;
  private const float ANCHOR_LEFT = 0.0f;
  private const float ANCHOR_RIGHT = 1f;
  private static readonly string SPRITE_NAME_SIDE = "ChatPartyTab0_02";
  private static readonly string SPRITE_NAME_LEFT_MIDDLE = "ChatPartyTab1_02";
  private const int WIDTH_SPRITE_NAME_SIDE = 140;
  private const int WIDTH_SPRITE_NAME_LEFT_MIDDLE = 165;
  private readonly Vector3 BG_SPRITE_POS = new Vector3(0.0f, -27.8f, 0.0f);
  private readonly Color BASE_COLOR_ACTIVE = Color.white;
  private readonly Color BASE_COLOR_DEACTIVE = new Color(0.5f, 0.5f, 0.5f, 1f);
  private readonly Color OUTLINE_COLOR_ACTIVE = new Color(0.0f, 0.25f, 0.31f, 1f);
  private readonly Color OUTLINE_COLOR_DEACTIVE = new Color(0.12f, 0.12f, 0.12f, 1f);
  private readonly string SELECTED_SPRITE = "PartyBtn_on_02";
  private readonly string NOT_SELECTED_SPRITE = "PartyBtn_off_02";
  [SerializeField]
  private UISprite m_bgSprite;
  [SerializeField]
  private UIButton m_buttonObject;
  private UIWidget m_buttonWidget;
  [SerializeField]
  private UILabel m_buttonLabel;
  [SerializeField]
  private UISprite m_backGroundSprite;
  private MainChat.CHAT_TYPE m_myType;
  private ChatHeaderButtonController.STATE m_currentState;
  private System.Action m_onActivateCallBack;
  private System.Action m_onDeactivateCallBack;
  private System.Action m_onInvisibleCallBack;
  private System.Action m_onSelectCallBack;
  private int m_selectedDepth;
  private int m_myButtonIndex;

  private UIWidget ButtonWidget
  {
    get
    {
      return this.m_buttonWidget ?? (this.m_buttonWidget = Object.op_Equality((Object) this.m_buttonObject, (Object) null) ? (UIWidget) null : ((Component) this.m_buttonObject).GetComponent<UIWidget>());
    }
  }

  public MainChat.CHAT_TYPE MyChatType => this.m_myType;

  public ChatHeaderButtonController.STATE CurrentState => this.m_currentState;

  private int ButtonIndex => this.m_myButtonIndex;

  public bool Initialize(ChatHeaderButtonController.InitParam _param)
  {
    if (_param == null)
      return false;
    this.m_myType = _param.ChatType;
    this.m_myButtonIndex = _param.ButtonIndex;
    this.InitUILabel(this.m_myType);
    this.m_selectedDepth = Object.op_Inequality((Object) this.m_backGroundSprite, (Object) null) ? this.m_backGroundSprite.depth : 0;
    this.SetBgSprite(this.ButtonIndex);
    this.SetDepth();
    this.m_onActivateCallBack = _param.OnActivateCallBack;
    this.m_onDeactivateCallBack = _param.OnDeactivateCallBack;
    this.m_onInvisibleCallBack = _param.OnInvisibleCallBack;
    this.m_onSelectCallBack = _param.OnSelectCallBack;
    return true;
  }

  private void InitDepth(int _index)
  {
    if (Object.op_Inequality((Object) this.m_backGroundSprite, (Object) null))
      this.m_backGroundSprite.depth = _index * 3;
    if (Object.op_Inequality((Object) this.m_bgSprite, (Object) null))
      this.m_bgSprite.depth = 1 + _index * 3;
    if (!Object.op_Inequality((Object) this.m_buttonLabel, (Object) null))
      return;
    this.m_buttonLabel.depth = 2 + _index * 3;
  }

  public void InitUILabel(MainChat.CHAT_TYPE _t)
  {
    if (Object.op_Equality((Object) this.m_buttonLabel, (Object) null))
      return;
    switch (_t)
    {
      case MainChat.CHAT_TYPE.HOME:
        this.m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 1U);
        break;
      case MainChat.CHAT_TYPE.ROOM:
        this.m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 2U);
        break;
      case MainChat.CHAT_TYPE.LOUNGE:
        this.m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 7U);
        break;
      case MainChat.CHAT_TYPE.FIELD:
        this.m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 3U);
        break;
      case MainChat.CHAT_TYPE.PERSONAL:
        this.m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 8U);
        break;
      case MainChat.CHAT_TYPE.CLAN:
        this.m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 9U);
        break;
      default:
        this.m_buttonLabel.text = "";
        break;
    }
    if (this.m_buttonLabel.text.Length <= 3)
      this.m_buttonLabel.fontSize = 16 /*0x10*/;
    else
      this.m_buttonLabel.fontSize = 12;
  }

  private void SetBgSprite(int _index)
  {
    if (Object.op_Equality((Object) this.m_backGroundSprite, (Object) null))
      return;
    bool flag1 = 0 < _index && _index < 3;
    bool flag2 = _index <= 0;
    this.m_backGroundSprite.spriteName = flag1 ? ChatHeaderButtonController.SPRITE_NAME_LEFT_MIDDLE : ChatHeaderButtonController.SPRITE_NAME_SIDE;
    this.m_backGroundSprite.width = flag1 ? 165 : 140;
    this.m_backGroundSprite.flip = flag2 ? UIBasicSprite.Flip.Horizontally : UIBasicSprite.Flip.Nothing;
    this.m_backGroundSprite.pivot = flag1 ? UIWidget.Pivot.Center : (flag2 ? UIWidget.Pivot.Left : UIWidget.Pivot.Right);
    if (flag2)
    {
      this.ButtonWidget.leftAnchor.Set(0.0f, 14f);
      this.ButtonWidget.rightAnchor.Set(1f, -44f);
    }
    else if (flag1)
    {
      this.ButtonWidget.leftAnchor.Set(0.0f, 44f);
      this.ButtonWidget.rightAnchor.Set(1f, -44f);
    }
    else
    {
      this.ButtonWidget.leftAnchor.Set(0.0f, 44f);
      this.ButtonWidget.rightAnchor.Set(1f, -14f);
    }
    ((Component) this.m_backGroundSprite).transform.localPosition = this.BG_SPRITE_POS;
  }

  private void SetDepth()
  {
    int num = this.CurrentState == ChatHeaderButtonController.STATE.SELECTED ? this.ButtonIndex * 3 + 30 : this.ButtonIndex * 3;
    if (Object.op_Inequality((Object) this.m_backGroundSprite, (Object) null))
      this.m_backGroundSprite.depth = num;
    if (Object.op_Inequality((Object) this.m_bgSprite, (Object) null))
      this.m_bgSprite.depth = num + 1;
    if (!Object.op_Inequality((Object) this.m_buttonLabel, (Object) null))
      return;
    this.m_buttonLabel.depth = num + 2;
  }

  public void OnClick() => this.Select();

  private void SetNextState(ChatHeaderButtonController.STATE _s)
  {
    if (this.CurrentState == _s)
      return;
    this.m_currentState = _s;
  }

  public void Activate()
  {
    if (!this.UnSelect() || this.m_onActivateCallBack == null)
      return;
    this.m_onActivateCallBack();
  }

  public bool Select()
  {
    if (!this.IsValidObjects())
      return false;
    this.SetNextState(ChatHeaderButtonController.STATE.SELECTED);
    this.m_buttonObject.normalSprite = this.SELECTED_SPRITE;
    this.m_buttonObject.SetState(UIButtonColor.State.Normal, true);
    this.m_buttonLabel.color = this.BASE_COLOR_ACTIVE;
    this.m_buttonLabel.effectColor = this.OUTLINE_COLOR_ACTIVE;
    if (this.m_onSelectCallBack != null)
      this.m_onSelectCallBack();
    this.SetBgSprite(this.ButtonIndex);
    this.SetDepth();
    return true;
  }

  public bool UnSelect()
  {
    if (!this.IsValidObjects())
      return false;
    this.SetNextState(ChatHeaderButtonController.STATE.UNSELECTED);
    this.m_buttonObject.normalSprite = this.NOT_SELECTED_SPRITE;
    this.m_buttonObject.SetState(UIButtonColor.State.Normal, true);
    this.m_buttonLabel.color = this.BASE_COLOR_ACTIVE;
    this.m_buttonLabel.effectColor = this.OUTLINE_COLOR_ACTIVE;
    this.SetBgSprite(this.ButtonIndex);
    this.SetDepth();
    return true;
  }

  public void Deactivate()
  {
    if (!this.IsValidObjects())
      return;
    this.SetNextState(ChatHeaderButtonController.STATE.DEACTIVATE);
    this.SetBgSprite(this.ButtonIndex);
    this.SetDepth();
    this.m_buttonObject.SetState(UIButtonColor.State.Disabled, true);
    this.m_buttonLabel.color = this.BASE_COLOR_DEACTIVE;
    this.m_buttonLabel.effectColor = this.OUTLINE_COLOR_DEACTIVE;
    if (this.m_onDeactivateCallBack == null)
      return;
    this.m_onDeactivateCallBack();
  }

  public void Show()
  {
    this.UnSelect();
    ((Component) this).gameObject.SetActive(true);
  }

  public void Hide()
  {
    this.SetNextState(ChatHeaderButtonController.STATE.INVISIBLE);
    ((Component) this).gameObject.SetActive(false);
    if (this.m_onInvisibleCallBack == null)
      return;
    this.m_onInvisibleCallBack();
  }

  private bool IsValidObjects()
  {
    return Object.op_Inequality((Object) this.m_bgSprite, (Object) null) && Object.op_Inequality((Object) this.m_buttonObject, (Object) null) && Object.op_Inequality((Object) this.m_buttonLabel, (Object) null);
  }

  public enum STATE
  {
    UNDEFINED,
    SELECTED,
    UNSELECTED,
    DEACTIVATE,
    INVISIBLE,
  }

  public class InitParam
  {
    public MainChat.CHAT_TYPE ChatType;
    public int ButtonIndex;
    public System.Action OnActivateCallBack;
    public System.Action OnDeactivateCallBack;
    public System.Action OnInvisibleCallBack;
    public System.Action OnSelectCallBack;
  }
}
