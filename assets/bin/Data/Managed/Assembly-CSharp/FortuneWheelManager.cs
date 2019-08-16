using Network;
using System;

public class FortuneWheelManager : MonoBehaviourSingleton<FortuneWheelManager>
{
	public enum SPIN_TYPE
	{
		X1 = 1,
		X10 = 10,
		X50 = 50,
		X100 = 100
	}

	public delegate void OnJackpot(JackpotWinData data);

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

	public const int JACKPOT_VALUE = 100;

	public FortuneWheelData WheelData;

	public FortuneWheelData SpinData;

	private string lastUpdateTime;

	public event OnJackpot OnJackpotWin;

	public event Action OnRequestUpdateUI;

	public void ReceivedJackpotWin(JackpotWinData data)
	{
		if (this.OnJackpotWin != null)
		{
			this.OnJackpotWin(data);
		}
		if ((MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "LoungeScene" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene") && !MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains("FortuneWheel") && !MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains("Jackpot"))
		{
			long num = 0L;
			try
			{
				num = long.Parse(data.jackpot);
				MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(string.Format(StringTable.Get(STRING_CATEGORY.DRAGON_VAULT, 1u), data.userName, num.ToString()), string.Empty);
			}
			catch
			{
			}
		}
	}

	public void RequestUpdateUI()
	{
		if (this.OnRequestUpdateUI != null)
		{
			this.OnRequestUpdateUI();
		}
	}

	public void UpdateData(Action<bool> call_back)
	{
		FortuneRequestForm fortuneRequestForm = new FortuneRequestForm();
		fortuneRequestForm.lastUpdateTime = lastUpdateTime;
		Protocol.Send(FortuneWheelHistoryModel.URL, fortuneRequestForm, delegate(FortuneWheelHistoryModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				WheelData.vaultInfo.jackpot = ret.result.history.jackpot;
				lastUpdateTime = ret.result.lastUpdateTime;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendSpin(SPIN_TYPE spinType, Action<bool> call_back)
	{
		FortuneRequestForm fortuneRequestForm = new FortuneRequestForm();
		fortuneRequestForm.number = (int)spinType;
		fortuneRequestForm.lastUpdateTime = lastUpdateTime;
		Protocol.Send(FortuneWheelSpinModel.URL, fortuneRequestForm, delegate(FortuneWheelSpinModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				WheelData = ret.result;
				SpinData = ret.result;
				lastUpdateTime = ret.result.lastUpdateTime;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendInfo(Action<bool> call_back)
	{
		Protocol.Send(FortuneWheelHomeModel.URL, delegate(FortuneWheelHomeModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				WheelData = ret.result;
				lastUpdateTime = WheelData.lastUpdateTime;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void BuyTicket(int numTicket, Action<bool> call_back)
	{
		FortuneWheelBuyModel.FortuneWheelBuyForm fortuneWheelBuyForm = new FortuneWheelBuyModel.FortuneWheelBuyForm();
		fortuneWheelBuyForm.number = numTicket;
		fortuneWheelBuyForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		Protocol.Send(FortuneWheelBuyModel.URL, fortuneWheelBuyForm, delegate(FortuneWheelBuyModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				WheelData.vaultInfo.curTicket = ret.result.curTicket;
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}
}
