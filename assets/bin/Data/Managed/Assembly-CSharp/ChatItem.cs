// Decompiled with JetBrains decompiler
// Type: ChatItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class ChatItem : MonoBehaviour
{
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
  private Vector3 MESSAGE_LABEL_POSITION_OTHER = new Vector3(-173f, -36f, 0.0f);
  private Vector3 MESSAGE_LABEL_POSITION_SELF = new Vector3(170f, -12f, 0.0f);
  public int stampId;
  public bool isMyMessage;
  public string chatItemId;
  public static Color DefaultMessageColor = new Color(0.0f, 0.0f, 0.0f);
  private IEnumerator m_CoroutineLoadStamp;

  public UIWidget widget => this.m_Widget;

  public float height => (float) this.m_Widget.height;

  private void Init(
    int userId,
    string userName,
    bool isText,
    bool isNotification,
    string chatItemId = "")
  {
    this.isMyMessage = userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    this.m_LabelSender.text = this.isMyMessage ? string.Empty : userName;
    this.chatItemId = chatItemId;
    this.CancelLoadStamp();
    ((Component) this.m_SpriteBase).gameObject.SetActive(isText && !isNotification);
    ((Component) this.m_LabelMessage).gameObject.SetActive(isText);
    this.m_LabelMessage.color = ChatItem.DefaultMessageColor;
    this.m_LabelMessage.supportEncoding = false;
    ((Component) this.m_TexStamp).gameObject.SetActive(!isText);
    ((Component) this.m_NotificationSpriteBase).gameObject.SetActive(isText & isNotification);
    ((Component) this.m_LabelSender).gameObject.SetActive(!isNotification);
  }

  public void Init(int userId, string userName, string desc, string chatItemId = "")
  {
    this.Init(userId, userName, true, false, chatItemId);
    this.SetMessage(desc);
    this.UpdateWidgetSize(true);
  }

  public void Init(string desc, string chatItemId = "", bool fontWhite = false)
  {
    this.Init(-1, "", true, true, chatItemId);
    if (fontWhite)
    {
      this.m_LabelMessage.color = Color.white;
      ((Component) this.m_NotificationSpriteBase).gameObject.SetActive(false);
    }
    this.SetMessage(desc);
    this.UpdateWidgetSize(true);
    this.m_LabelMessage.supportEncoding = true;
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
    GameObject gameObject = this.isMyMessage ? this.m_PivotMessageRight : this.m_PivotMessaageLeft;
    UIWidget.Pivot pivot = this.isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft;
    string str = this.isMyMessage ? "ChatHukidashiMine" : "ChatHukidashiBlue";
    ((Component) this.m_SpriteBase).transform.parent = gameObject.transform;
    this.m_SpriteBase.pivot = pivot;
    ((Component) this.m_SpriteBase).transform.localPosition = Vector3.zero;
    this.m_SpriteBase.spriteName = str;
    this.m_SpriteBase.width = (int) ((double) this.m_LabelMessage.printedSize.x + 50.0);
    this.m_SpriteBase.height = (int) ((double) this.m_LabelMessage.printedSize.y + 25.0);
    this.m_NotificationSpriteBase.height = (int) ((double) this.m_LabelMessage.printedSize.y + 25.0);
  }

  public void Init(int userId, string userName, int stampId, string chatItemId = "")
  {
    this.Init(userId, userName, false, false, chatItemId);
    ((Component) this.m_TexStamp).transform.parent = (this.isMyMessage ? this.m_PivotStampRight : this.m_PivotStampLeft).transform;
    this.m_TexStamp.pivot = this.isMyMessage ? UIWidget.Pivot.TopRight : UIWidget.Pivot.TopLeft;
    ((Component) this.m_TexStamp).transform.localPosition = Vector3.zero;
    this.RequestLoadStamp(stampId);
    this.UpdateWidgetSize(false);
  }

  private void UpdateWidgetSize(bool isText)
  {
    int height1 = this.isMyMessage ? 0 : this.m_LabelSender.height;
    int height2;
    int width;
    if (isText)
    {
      height2 = this.m_SpriteBase.height;
      width = this.m_SpriteBase.width;
    }
    else
    {
      height2 = this.m_TexStamp.height;
      width = this.m_TexStamp.width;
    }
    this.m_Widget.width = width;
    this.m_Widget.height = height2 + height1;
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
}
