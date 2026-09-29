// Decompiled with JetBrains decompiler
// Type: GuildChatPinItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class GuildChatPinItem : MonoBehaviour
{
  [SerializeField]
  private UISprite m_UnPinButton;
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
  private UITexture m_Avatar;
  private const int MIN_HEIGHT = 140;
  private bool canUnPinMsg;
  [SerializeField]
  private UITexture m_TexStamp;
  private IEnumerator m_CoroutineLoadStamp;
  private float startPressTime;
  private Vector2 mousePosition;
  private bool checkLongPress;

  public int GetHeight => this.m_SpriteBase.height;

  public void ShowPinMsg(string userPin, string pinMsg)
  {
    ((Component) this.m_TexStamp).gameObject.SetActive(false);
    ((Component) this.m_LabelMessage).gameObject.SetActive(true);
    this.m_LabelMessage.text = pinMsg;
    this.m_LabelSender.text = userPin;
    int num = (int) ((double) this.m_LabelMessage.printedSize.y + 50.0);
    this.m_SpriteBase.height = num > 140 ? num : 140;
    this.m_BoxCollider.center = new Vector3(0.0f, (float) (-(double) this.m_SpriteBase.height / 2.0), 0.0f);
    ((Component) this.m_UnPinButton).transform.localPosition = new Vector3(((Component) this.m_UnPinButton).transform.localPosition.x, (float) -((double) this.m_SpriteBase.height - 35.0), 0.0f);
    if (MonoBehaviourSingleton<GuildManager>.I.guildData == null || MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      return;
    this.canUnPinMsg = true;
  }

  public void ShowPinStamp(string userPin, int stampId)
  {
    this.m_LabelMessage.text = string.Empty;
    this.m_LabelSender.text = userPin;
    ((Component) this.m_LabelMessage).gameObject.SetActive(false);
    this.RequestLoadStamp(stampId);
    this.m_SpriteBase.height = 140;
    this.m_BoxCollider.center = new Vector3(0.0f, (float) (-(double) this.m_SpriteBase.height / 2.0), 0.0f);
    ((Component) this.m_UnPinButton).transform.localPosition = new Vector3(((Component) this.m_UnPinButton).transform.localPosition.x, (float) -((double) this.m_SpriteBase.height - 35.0), 0.0f);
    if (MonoBehaviourSingleton<GuildManager>.I.guildData == null || MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      return;
    this.canUnPinMsg = true;
  }

  private void RequestLoadStamp(int stampId)
  {
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
      ((Component) this.m_UnPinButton).gameObject.SetActive(true);
    }
  }

  private void OnPress(bool isDown)
  {
    if (!this.canUnPinMsg)
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

  public void HideUnPinButton() => ((Component) this.m_UnPinButton).gameObject.SetActive(false);
}
