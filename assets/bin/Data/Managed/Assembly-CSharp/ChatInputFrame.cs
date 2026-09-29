// Decompiled with JetBrains decompiler
// Type: ChatInputFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ChatInputFrame : MonoBehaviour
{
  [SerializeField]
  private UITweener[] m_OpenTweens;
  [SerializeField]
  private UITweener[] m_CloseTweens;
  [SerializeField]
  private UIButton m_OpenCloseBtn;
  [SerializeField]
  private UISprite m_OpenCloseSprite;
  [SerializeField]
  private UILabel m_InputTextLabel;
  [SerializeField]
  private UISprite m_BackgroundSprite;
  [SerializeField]
  private BoxCollider m_InputCollider;
  private int m_BusyCount;
  private const float INPUT_COLLIDER_MARGIN = 10f;
  private const float FRAME_OFFSET = 4f;
  private bool m_IsOpen;
  public System.Action onChange;
  public System.Action onSubmit;
  [SerializeField]
  private UISprite m_AgeConfirmSprite;
  [SerializeField]
  private UISprite m_DenyChatSprite;

  private bool IsBusy => this.m_BusyCount != 0;

  private void Awake()
  {
    this.InitTweens(this.m_OpenTweens);
    this.InitTweens(this.m_CloseTweens);
    this.m_IsOpen = true;
  }

  private void InitTweens(UITweener[] tweens)
  {
    if (tweens == null)
      return;
    int index = 0;
    for (int length = tweens.Length; index < length; ++index)
    {
      ((Behaviour) tweens[index]).enabled = false;
      tweens[index].AddOnFinished(new EventDelegate(new EventDelegate.Callback(this.OnFinished)));
    }
  }

  private void OnFinished()
  {
    --this.m_BusyCount;
    if (this.IsBusy)
      return;
    ((Collider) this.m_InputCollider).enabled = this.m_IsOpen;
  }

  public void Open()
  {
    if (this.StartAnim(this.m_OpenTweens))
      this.SetOpenCloseBtnSprite("ChatBtnHome");
    this.m_IsOpen = true;
  }

  public void Close()
  {
    if (!this.StartAnim(this.m_CloseTweens))
      return;
    this.m_IsOpen = false;
    this.SetOpenCloseBtnSprite("ChatBtnHome");
  }

  public void Reset()
  {
    this.UpdateAgeConfirm();
    this.m_IsOpen = false;
    this.SetOpenCloseBtnSprite("ChatBtnHome");
    this.FrameResize();
  }

  private void SetOpenCloseBtnSprite(string spriteName)
  {
  }

  private bool StartAnim(UITweener[] tweens)
  {
    if (this.IsBusy)
      return false;
    this.m_BusyCount = tweens.Length;
    ((Collider) this.m_InputCollider).enabled = false;
    int index = 0;
    for (int length = tweens.Length; index < length; ++index)
    {
      ((Behaviour) tweens[index]).enabled = true;
      tweens[index].ResetToBeginning();
    }
    return true;
  }

  public bool IsEnableInput() => !this.IsBusy && this.m_IsOpen;

  public bool IsOpenOrBusy() => this.m_IsOpen || this.IsBusy;

  public void FrameResize()
  {
    this.m_InputCollider.size = new Vector3(this.m_InputCollider.size.x, (float) this.m_BackgroundSprite.height + 10f, this.m_InputCollider.size.z);
  }

  public void ChangeText()
  {
    if (this.onChange == null)
      return;
    this.onChange();
  }

  public void SubmitText()
  {
    if (this.onSubmit == null)
      return;
    this.onSubmit();
  }

  public void OnTouchOpenCloseBtn()
  {
    int num = this.IsBusy ? 1 : 0;
  }

  public void OnTouchCloseBtn()
  {
    int num = this.IsBusy ? 1 : 0;
  }

  public void UpdateAgeConfirm()
  {
    bool flag1 = !UserInfoManager.IsRegisterdAge();
    bool flag2 = UserInfoManager.IsRegisterdAge() && !UserInfoManager.IsEnableCommunication();
    if (Object.op_Inequality((Object) this.m_AgeConfirmSprite, (Object) null))
      ((Component) this.m_AgeConfirmSprite).gameObject.SetActive(flag1);
    if (!Object.op_Inequality((Object) this.m_DenyChatSprite, (Object) null))
      return;
    ((Component) this.m_DenyChatSprite).gameObject.SetActive(flag2);
  }
}
