// Decompiled with JetBrains decompiler
// Type: GuildChatItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections;
using UnityEngine;

#nullable disable
public class GuildChatItem : MonoBehaviour
{
  [SerializeField]
  private UILabel m_LabelNotification;
  [SerializeField]
  private UISprite m_PinButton;
  [SerializeField]
  private BoxCollider m_BoxCollider;
  [SerializeField]
  private UIWidget m_Widget;
  [SerializeField]
  private UILabel m_LabelSender;
  [SerializeField]
  private UILabel m_LabelMessage;
  [SerializeField]
  private UISprite m_SpriteBase;
  [SerializeField]
  private UITexture m_TexStamp;
  [SerializeField]
  private UISprite m_NotificationSpriteBase;
  [SerializeField]
  private GameObject m_PivotMessageRight;
  [SerializeField]
  private GameObject m_PivotMessaageLeft;
  [SerializeField]
  private GameObject m_PivotStampRight;
  [SerializeField]
  private GameObject m_PivotStampLeft;
  [SerializeField]
  private const int MESSAGE_SPRITE_WIDTH_MARGIN = 50;
  [SerializeField]
  private const int MESSAGE_SPRITE_HEIGHT_MARGIN = 25;
  private Vector3 MESSAGE_LABEL_POSITION_NOTIFI = new Vector3(-178f, -12f, 0.0f);
  private Vector3 MESSAGE_LABEL_POSITION_OTHER = new Vector3(-178f, -36f, 0.0f);
  private Vector3 MESSAGE_LABEL_POSITION_SELF = new Vector3(185f, -12f, 0.0f);
  private int stampId;
  private bool isMyMessage;
  private bool isNotifi;
  private bool canPinMsg;
  private int msgId_;
  private string uuId_;
  private int senderId_;
  private IEnumerator m_CoroutineLoadStamp;
  private float startPressTime;
  private Vector2 mousePosition;
  private bool checkLongPress;

  public UIWidget widget => this.m_Widget;

  public float height => (float) this.m_Widget.height;

  public int msgId => this.msgId_;

  public string uuId => this.uuId_;

  public string msg => this.m_LabelMessage.text;

  public int senderId => this.senderId_;

  private void Init(
    string uuId,
    int chatId,
    int userId,
    string userName,
    bool isText,
    bool isNotification)
  {
    this.msgId_ = chatId;
    this.senderId_ = userId;
    this.uuId_ = uuId;
    this.isNotifi = isNotification;
    this.isMyMessage = userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    this.m_LabelSender.text = this.isMyMessage ? string.Empty : userName;
    this.CancelLoadStamp();
    ((Component) this.m_SpriteBase).gameObject.SetActive(isText && !isNotification);
    ((Component) this.m_LabelMessage).gameObject.SetActive(isText);
    ((Component) this.m_TexStamp).gameObject.SetActive(!isText);
    ((Component) this.m_NotificationSpriteBase).gameObject.SetActive(false);
    ((Component) this.m_LabelSender).gameObject.SetActive(!isNotification);
    if (MonoBehaviourSingleton<GuildManager>.I.guildData == null || MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      return;
    this.canPinMsg = true;
  }

  public void Init(string uuId, int chatId, int userId, string userName, string desc)
  {
    this.Init(uuId, chatId, userId, userName, true, false);
    this.SetMessage(desc);
    this.UpdateWidgetSize(true);
  }

  public void Init(string desc)
  {
    this.Init(string.Empty, 0, -1, "", true, true);
    this.m_LabelMessage.supportEncoding = true;
    this.m_LabelMessage.color = Color.white;
    this.m_LabelMessage.fontStyle = (FontStyle) 2;
    this.SetMessage(desc);
    this.UpdateWidgetSize(true);
  }

  private void SetMessage(string message)
  {
    this.m_LabelMessage.text = message;
    this.m_LabelMessage.pivot = this.isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft;
    if (this.isMyMessage)
    {
      Vector3 labelPositionSelf = this.MESSAGE_LABEL_POSITION_SELF;
      labelPositionSelf.x += (float) this.m_LabelMessage.width - this.m_LabelMessage.printedSize.x;
      ((Component) this.m_LabelMessage).transform.localPosition = labelPositionSelf;
    }
    else
      ((Component) this.m_LabelMessage).transform.localPosition = this.MESSAGE_LABEL_POSITION_OTHER;
    if (this.isNotifi)
      ((Component) this.m_LabelMessage).transform.localPosition = this.MESSAGE_LABEL_POSITION_NOTIFI;
    GameObject gameObject = this.isMyMessage ? this.m_PivotMessageRight : this.m_PivotMessaageLeft;
    UIWidget.Pivot pivot = this.isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft;
    string str = this.isMyMessage ? "ChatHukidashiMine" : "ChatHukidashiBlue";
    if (this.isNotifi)
      gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, 0.0f, gameObject.transform.localPosition.z);
    ((Component) this.m_SpriteBase).transform.parent = gameObject.transform;
    this.m_SpriteBase.pivot = pivot;
    ((Component) this.m_SpriteBase).transform.localPosition = Vector3.zero;
    this.m_SpriteBase.spriteName = str;
    this.m_SpriteBase.width = (int) ((double) this.m_LabelMessage.printedSize.x + 50.0);
    this.m_SpriteBase.height = (int) ((double) this.m_LabelMessage.printedSize.y + 25.0);
    ((Component) this.m_PinButton).transform.SetParent(gameObject.transform);
    if (this.isMyMessage)
    {
      ((Component) this.m_PinButton).transform.localPosition = new Vector3(-55f, 0.0f, 0.0f);
      this.m_BoxCollider.size = new Vector3((float) this.m_SpriteBase.width, (float) this.m_SpriteBase.height, 1f);
      this.m_BoxCollider.center = new Vector3(gameObject.transform.localPosition.x - (float) this.m_SpriteBase.width / 2f, (float) (-(double) this.m_SpriteBase.height / 2.0), 0.0f);
    }
    else
    {
      ((Component) this.m_PinButton).transform.localPosition = new Vector3(55f, 0.0f, 0.0f);
      this.m_BoxCollider.size = new Vector3((float) this.m_SpriteBase.width, (float) this.m_SpriteBase.height, 1f);
      this.m_BoxCollider.center = new Vector3(gameObject.transform.localPosition.x + (float) this.m_SpriteBase.width / 2f, (float) (-(double) this.m_SpriteBase.height / 2.0 - 16.0), 0.0f);
    }
  }

  public void Init(string uuID, int chatId, int userId, string userName, int stampId)
  {
    this.Init(uuID, chatId, userId, userName, false, false);
    GameObject gameObject = this.isMyMessage ? this.m_PivotStampRight : this.m_PivotStampLeft;
    ((Component) this.m_TexStamp).transform.parent = gameObject.transform;
    this.m_TexStamp.pivot = this.isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft;
    ((Component) this.m_TexStamp).transform.localPosition = Vector3.zero;
    this.RequestLoadStamp(stampId);
    this.UpdateWidgetSize(false);
    ((Component) this.m_PinButton).transform.SetParent(gameObject.transform);
    this.m_BoxCollider.size = new Vector3((float) this.m_TexStamp.width, (float) this.m_TexStamp.height, 1f);
    if (this.isMyMessage)
    {
      ((Component) this.m_PinButton).transform.localPosition = new Vector3(-40f, 0.0f, 0.0f);
      this.m_BoxCollider.center = new Vector3(gameObject.transform.localPosition.x - (float) this.m_TexStamp.width / 2f, (float) (-(double) this.m_TexStamp.height / 2.0), 0.0f);
    }
    else
    {
      ((Component) this.m_PinButton).transform.localPosition = new Vector3(40f, 0.0f, 0.0f);
      this.m_BoxCollider.center = new Vector3(gameObject.transform.localPosition.x + (float) this.m_TexStamp.width / 2f, (float) (-(double) this.m_TexStamp.height / 2.0 - 16.0), 0.0f);
    }
  }

  private void UpdateWidgetSize(bool isText)
  {
    int num = this.isMyMessage ? 0 : this.m_LabelSender.height;
    if (this.isNotifi)
      num = 0;
    ((Collider) this.m_BoxCollider).enabled = true;
    int height;
    int width;
    if (isText)
    {
      height = this.m_SpriteBase.height;
      width = this.m_SpriteBase.width;
    }
    else
    {
      height = this.m_TexStamp.height;
      width = this.m_TexStamp.width;
    }
    this.m_Widget.width = width;
    this.m_Widget.height = height + num;
  }

  private void RequestLoadStamp(int stampId)
  {
    this.stampId = stampId;
    this.CancelLoadStamp();
    this.m_CoroutineLoadStamp = this.CoroutineLoadStamp(stampId);
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    this.StartCoroutine(this._Update());
  }

  private IEnumerator CoroutineLoadStamp(int stampId)
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_stamp = load_queue.LoadChatStamp(stampId);
    while (load_queue.IsLoading())
      yield return (object) null;
    if (Object.op_Inequality(lo_stamp.loadedObject, (Object) null))
    {
      Texture2D loadedObject = lo_stamp.loadedObject as Texture2D;
      ((Component) this.m_TexStamp).gameObject.SetActive(true);
      this.m_TexStamp.mainTexture = (Texture) loadedObject;
    }
    this.m_CoroutineLoadStamp = (IEnumerator) null;
  }

  private void CancelLoadStamp()
  {
    if (this.m_CoroutineLoadStamp == null)
      return;
    this.m_CoroutineLoadStamp = (IEnumerator) null;
    ((Component) this.m_TexStamp).gameObject.SetActive(false);
  }

  private IEnumerator _Update()
  {
    while (this.m_CoroutineLoadStamp != null && this.m_CoroutineLoadStamp.MoveNext())
      yield return (object) null;
  }

  private void OnEnable()
  {
    if (this.m_CoroutineLoadStamp == null)
      return;
    this.RequestLoadStamp(this.stampId);
  }

  private void Update()
  {
    if (!this.checkLongPress)
      return;
    if ((double) Vector2.Distance(this.mousePosition, Vector2.op_Implicit(Input.mousePosition)) > 10.0)
    {
      this.checkLongPress = false;
    }
    else
    {
      if ((double) Time.time - (double) this.startPressTime <= 1.0)
        return;
      this.checkLongPress = false;
      ClanChatLogMessageData chatLogMessageData = new ClanChatLogMessageData();
      chatLogMessageData.fromUserId = this.senderId;
      chatLogMessageData.id = this.msgId;
      chatLogMessageData.uuid = this.uuId;
      chatLogMessageData.stampId = this.stampId;
      if (this.stampId <= 0)
      {
        chatLogMessageData.type = 0;
        chatLogMessageData.message = this.msg;
      }
      else
      {
        chatLogMessageData.type = 1;
        chatLogMessageData.message = this.stampId.ToString();
      }
      ((Component) this.m_PinButton).gameObject.GetComponent<UIGameSceneEventSender>().eventData = (object) chatLogMessageData;
      ((Component) this.m_PinButton).gameObject.SetActive(true);
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (GuildChatItem), ((Component) this).gameObject, "HIDE_PIN_BTN", (object) this.msgId_.ToString());
    }
  }

  private void OnPress(bool isDown)
  {
    if (!this.canPinMsg)
      return;
    if (isDown)
    {
      this.checkLongPress = true;
      this.startPressTime = Time.time;
      this.mousePosition = Vector2.op_Implicit(Input.mousePosition);
    }
    else
      this.checkLongPress = false;
  }

  public void HidePinButton() => ((Component) this.m_PinButton).gameObject.SetActive(false);
}
