// Decompiled with JetBrains decompiler
// Type: FortuneWheelManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class FortuneWheelManager : MonoBehaviourSingleton<FortuneWheelManager>
{
  public const int JACKPOT_VALUE = 100;
  public FortuneWheelData WheelData;
  public FortuneWheelData SpinData;
  private string lastUpdateTime;

  public event FortuneWheelManager.OnJackpot OnJackpotWin;

  public event System.Action OnRequestUpdateUI;

  public void ReceivedJackpotWin(FortuneWheelManager.JackpotWinData data)
  {
    if (this.OnJackpotWin != null)
      this.OnJackpotWin(data);
    if (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene") && !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "LoungeScene") && !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene") || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains("FortuneWheel") || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains("Jackpot"))
      return;
    try
    {
      long num = long.Parse(data.jackpot);
      MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(string.Format(StringTable.Get(STRING_CATEGORY.DRAGON_VAULT, 1U), (object) data.userName, (object) num.ToString()), "");
    }
    catch
    {
    }
  }

  public void RequestUpdateUI()
  {
    if (this.OnRequestUpdateUI == null)
      return;
    this.OnRequestUpdateUI();
  }

  public void UpdateData(Action<bool> call_back)
  {
    Protocol.Send<FortuneRequestForm, FortuneWheelHistoryModel>(FortuneWheelHistoryModel.URL, new FortuneRequestForm()
    {
      lastUpdateTime = this.lastUpdateTime
    }, (Action<FortuneWheelHistoryModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.WheelData.vaultInfo.jackpot = ret.result.history.jackpot;
        this.lastUpdateTime = ret.result.lastUpdateTime;
      }
      call_back(flag);
    }));
  }

  public void SendSpin(FortuneWheelManager.SPIN_TYPE spinType, Action<bool> call_back)
  {
    Protocol.Send<FortuneRequestForm, FortuneWheelSpinModel>(FortuneWheelSpinModel.URL, new FortuneRequestForm()
    {
      number = (int) spinType,
      lastUpdateTime = this.lastUpdateTime
    }, (Action<FortuneWheelSpinModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.WheelData = ret.result;
        this.SpinData = ret.result;
        this.lastUpdateTime = ret.result.lastUpdateTime;
      }
      call_back(flag);
    }));
  }

  public void SendInfo(Action<bool> call_back)
  {
    Protocol.Send<FortuneWheelHomeModel>(FortuneWheelHomeModel.URL, (Action<FortuneWheelHomeModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.WheelData = ret.result;
        this.lastUpdateTime = this.WheelData.lastUpdateTime;
      }
      call_back(flag);
    }));
  }

  public void BuyTicket(int numTicket, Action<bool> call_back)
  {
    Protocol.Send<FortuneWheelBuyModel.FortuneWheelBuyForm, FortuneWheelBuyModel>(FortuneWheelBuyModel.URL, new FortuneWheelBuyModel.FortuneWheelBuyForm()
    {
      number = numTicket,
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal
    }, (Action<FortuneWheelBuyModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        this.WheelData.vaultInfo.curTicket = ret.result.curTicket;
        flag = true;
      }
      call_back(flag);
    }));
  }

  public enum SPIN_TYPE
  {
    X1 = 1,
    X10 = 10, // 0x0000000A
    X50 = 50, // 0x00000032
    X100 = 100, // 0x00000064
  }

  public delegate void OnJackpot(FortuneWheelManager.JackpotWinData data);

  public class JackpotWinData
  {
    public string userId;
    public string jackpot;
    public string userName;
    public int percentage;

    public JackpotWinData(string userId, string jackpot, string userName, int percentage = 0)
    {
      this.userId = userId;
      this.jackpot = jackpot;
      this.userName = userName;
      this.percentage = percentage;
    }
  }
}
