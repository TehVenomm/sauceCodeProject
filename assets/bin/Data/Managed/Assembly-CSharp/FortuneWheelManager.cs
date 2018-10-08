using Network;
using System;
using System.Runtime.CompilerServices;

public class FortuneWheelManager : MonoBehaviourSingleton<FortuneWheelManager>
{
	public enum SpinType
	{
		One = 1,
		Ten = 10
	}

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

	public delegate void OnJackpot(JackpotWinData data);

	public const int JACKPOT_VALUE = 100;

	public FortuneWheelData WheelData;

	public FortuneWheelData SpinData;

	private string lastUpdateTime;

	public event OnJackpot OnJackpotWin;

	public event Action OnRequestUpdateUI
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.OnRequestUpdateUI = Delegate.Combine((Delegate)this.OnRequestUpdateUI, (Delegate)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.OnRequestUpdateUI = Delegate.Remove((Delegate)this.OnRequestUpdateUI, (Delegate)value);
		}
	}

	public void ReceivedJackpotWin(JackpotWinData data)
	{
		if (this.OnJackpotWin != null)
		{
			this.OnJackpotWin(data);
		}
		if ((MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "LoungeScene" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene") && !MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains("FortuneWheel") && !MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains("Jackpot"))
		{
			MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce($"{data.userName} just won {data.jackpot} from Dragon's Vault", string.Empty);
		}
	}

	public void RequestUpdateUI()
	{
		if (this.OnRequestUpdateUI != null)
		{
			this.OnRequestUpdateUI.Invoke();
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

	public void SendSpin(SpinType spinType, Action<bool> call_back)
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
