// Decompiled with JetBrains decompiler
// Type: GuildMessageDonateListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class GuildMessageDonateListItem : MonoBehaviour
{
  [SerializeField]
  private UISprite m_PinButton;
  [SerializeField]
  private UISprite m_OwnerBackground;
  [SerializeField]
  private UISprite m_TargetBackground;
  [SerializeField]
  private UISprite m_Clock;
  [SerializeField]
  private UILabel m_TimeExpire;
  [SerializeField]
  private UIButton m_ButtonGift;
  [SerializeField]
  private UIButton m_AskForHelp;
  private DonateInfo info;
  private double timeLeft;
  private double minChangeColorTime;
  private double minShowLastTime;
  private bool canPinMsg;
  private float tick = 1f;
  private float counter;
  private float delayShow = 3f;
  private float startPressTime;
  private Vector2 mousePosition;
  private bool checkLongPress;

  private void OnStart()
  {
    if (!Object.op_Inequality((Object) this.m_PinButton, (Object) null))
      return;
    ((Component) this.m_PinButton).gameObject.SetActive(false);
  }

  public void SetDonateInfo(DonateInfo _info)
  {
    this.info = _info;
    this.timeLeft = (this.info.expired - MonoBehaviourSingleton<GuildManager>.I.SystemTime) / 1000.0;
    this.m_TimeExpire.text = this.SecondToTime(this.timeLeft);
    this.minChangeColorTime = 900.0;
    this.SetUIActive();
    bool instant = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) this.info.itemId), 1) > 0 && this.info.itemNum < this.info.quantity;
    this.m_ButtonGift.SetState(UIButtonColor.State.Normal, instant);
    this.m_ButtonGift.state = instant ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled;
    this.SetupUI();
  }

  private void OnReceiveUpdateStatus(ClanUpdateStatusData clanUpdateStatusData)
  {
    if (clanUpdateStatusData.type != 2 || clanUpdateStatusData.status != 2 || this.timeLeft > 0.0)
      return;
    this.SetUIDisable();
  }

  private void SetUIActive()
  {
    this.m_TimeExpire.color = Color.white;
    this.m_OwnerBackground.color = Color.white;
    this.m_TargetBackground.color = Color.white;
    this.m_Clock.color = Color.white;
    if (((Component) this.m_AskForHelp).gameObject.activeInHierarchy)
    {
      this.m_AskForHelp.SetState(UIButtonColor.State.Normal, true);
      this.m_AskForHelp.isEnabled = true;
    }
    if (!((Component) this.m_ButtonGift).gameObject.activeInHierarchy)
      return;
    this.m_ButtonGift.SetState(UIButtonColor.State.Normal, true);
    this.m_ButtonGift.isEnabled = true;
  }

  public void SetUIDisable()
  {
    this.m_TimeExpire.text = "Expired!";
    this.m_TimeExpire.color = Color.gray;
    this.m_OwnerBackground.color = Color.gray;
    this.m_TargetBackground.color = Color.gray;
    this.m_Clock.color = Color.gray;
    if (((Component) this.m_AskForHelp).gameObject.activeInHierarchy)
    {
      this.m_AskForHelp.SetState(UIButtonColor.State.Disabled, true);
      this.m_AskForHelp.isEnabled = false;
    }
    if (!((Component) this.m_ButtonGift).gameObject.activeInHierarchy)
      return;
    this.m_ButtonGift.isEnabled = false;
    this.m_ButtonGift.SetState(UIButtonColor.State.Disabled, true);
  }

  protected void OnDestroy()
  {
  }

  private void Start()
  {
    if (MonoBehaviourSingleton<GuildManager>.I.guildData == null || MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      return;
    this.canPinMsg = true;
  }

  private void Update()
  {
    if (this.checkLongPress)
    {
      if ((double) Vector2.Distance(this.mousePosition, Vector2.op_Implicit(Input.mousePosition)) > 10.0)
      {
        this.checkLongPress = false;
        return;
      }
      if ((double) Time.time - (double) this.startPressTime > 1.0)
      {
        this.checkLongPress = false;
        ((Component) this.m_PinButton).gameObject.SetActive(true);
        ((Component) this.m_PinButton).gameObject.GetComponent<UIGameSceneEventSender>().eventData = (object) this.info;
        ((Component) this.m_PinButton).gameObject.SetActive(true);
      }
    }
    if ((double) this.counter <= (double) this.tick)
    {
      this.counter += Time.deltaTime;
    }
    else
    {
      this.counter = 0.0f;
      --this.timeLeft;
      this.SetupUI();
    }
  }

  private void SetupUI()
  {
    if (this.timeLeft <= this.minChangeColorTime && this.timeLeft >= 0.0 && Color.op_Inequality(this.m_Clock.color, Color.yellow))
    {
      this.m_Clock.color = Color.yellow;
      this.m_TimeExpire.color = Color.yellow;
      this.m_TimeExpire.text = this.SecondToTime(this.timeLeft);
    }
    if (this.timeLeft < 0.0)
      this.SetUIDisable();
    else
      this.m_TimeExpire.text = this.SecondToTime(this.timeLeft);
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

  private double DateTimeToTimestampSeconds()
  {
    return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
  }

  private string SecondToTime(double time)
  {
    int num = (int) time;
    return $"{num / 3600:D2}:{num / 60 % 60:D2}:{num % 60:D2}";
  }
}
