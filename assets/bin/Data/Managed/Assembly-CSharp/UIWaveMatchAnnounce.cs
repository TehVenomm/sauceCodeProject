using UnityEngine;

public class UIWaveMatchAnnounce : UIAnnounceBase<UIWaveMatchAnnounce>
{
	private enum eState
	{
		None,
		Announce,
		CountDown,
		CutIn
	}

	private const int kCountDownSec = 5;

	private const string kCountDownSprPrefix = "WaveEncount_";

	private const float kCutInDispSec = 2f;

	private const string kCutInSprPrefix = "WaveCount_";

	[SerializeField]
	protected UILabel timeLabel;

	[SerializeField]
	protected GameObject CountDownObj;

	[SerializeField]
	protected UISprite CountDownSpr;

	[SerializeField]
	protected UITweener[] CutInAnim;

	[SerializeField]
	protected GameObject CutInObj;

	[SerializeField]
	protected GameObject CutInNumberObj;

	[SerializeField]
	protected UISprite CutInNumberSpr10;

	[SerializeField]
	protected UISprite CutInNumberSpr01;

	[SerializeField]
	protected GameObject CutInFinalObj;

	private InGameSettingsManager.WaveMatchParam wmSetting;

	private Coop_Model_WaveMatchInfo wmInfo;

	private eState state;

	private float countSec;

	private int lastInteger;

	private int dispDispSec = 3;

	private bool isEvent;

	protected override float GetDispSec()
	{
		return (float)dispDispSec;
	}

	public void Announce(Coop_Model_WaveMatchInfo info)
	{
		wmInfo = info;
		isEvent = QuestManager.IsValidInGameWaveMatch(true);
		CutInAnim[0].enabled = false;
		CutInAnim[0].ResetToBeginning();
		CutInAnim[1].enabled = false;
		CutInAnim[1].ResetToBeginning();
		if (wmInfo.popGuardSec <= 0)
		{
			StartCutIn();
		}
		else if (wmInfo.popGuardSec <= 5)
		{
			StartCountDown();
		}
		else
		{
			StartAnnounce();
		}
	}

	private void Update()
	{
		switch (state)
		{
		case eState.Announce:
			UpdateAnnounce();
			break;
		case eState.CountDown:
			UpdateCountDown();
			break;
		case eState.CutIn:
			UpdateCutIn();
			break;
		}
	}

	private void StartAnnounce()
	{
		dispDispSec = wmInfo.popGuardSec - 5;
		if (AnnounceStart())
		{
			timeLabel.text = InGameProgress.GetTimeToStringMMSS(wmInfo.popGuardSec);
			lastInteger = wmInfo.popGuardSec;
			countSec = (float)wmInfo.popGuardSec;
			state = eState.Announce;
		}
	}

	private void UpdateAnnounce()
	{
		countSec -= Time.deltaTime;
		int num = Mathf.FloorToInt(countSec);
		if (lastInteger != num)
		{
			lastInteger = num;
			timeLabel.text = InGameProgress.GetTimeToStringMMSS(num);
		}
		if (lastInteger <= 5)
		{
			StartCountDown();
		}
	}

	private void StartCountDown()
	{
		CountDownSpr.spriteName = "WaveEncount_5";
		CountDownObj.SetActive(true);
		state = eState.CountDown;
	}

	private void UpdateCountDown()
	{
		countSec -= Time.deltaTime;
		int num = Mathf.FloorToInt(countSec);
		if (lastInteger != num)
		{
			lastInteger = num;
			CountDownSpr.spriteName = "WaveEncount_" + lastInteger;
		}
		if (countSec <= 1f)
		{
			CountDownObj.SetActive(false);
			StartCutIn();
		}
	}

	private void StartCutIn()
	{
		bool flag = wmInfo.isFinal > 0;
		if (wmSetting == null)
		{
			wmSetting = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam();
		}
		SoundManager.PlayOneshotJingle(wmSetting.waveJingleId, null, null);
		panelChange.UnLock();
		CutInAnim[0].enabled = true;
		CutInAnim[0].PlayForward();
		CutInAnim[1].enabled = true;
		CutInAnim[1].PlayForward();
		if (!flag)
		{
			CutInNumberSpr10.spriteName = "WaveCount_" + wmInfo.no / 10;
			CutInNumberSpr01.spriteName = "WaveCount_" + wmInfo.no % 10;
		}
		CutInFinalObj.SetActive(flag);
		CutInNumberObj.SetActive(!flag);
		CutInObj.SetActive(true);
		countSec = 2f;
		if (MonoBehaviourSingleton<UIQuestInfoWaveMatch>.IsValid())
		{
			MonoBehaviourSingleton<UIQuestInfoWaveMatch>.I.SetWaveNow(wmInfo.no, wmInfo.finalNo, flag);
		}
		if (isEvent && MonoBehaviourSingleton<StageObjectManager>.IsValid() && (Object)MonoBehaviourSingleton<StageObjectManager>.I.self != (Object)null)
		{
			MonoBehaviourSingleton<StageObjectManager>.I.self.CheckWaveMatchAutoRevive();
		}
		state = eState.CutIn;
	}

	private void UpdateCutIn()
	{
		countSec -= Time.deltaTime;
		if (countSec <= 0f)
		{
			CutInFinalObj.SetActive(false);
			CutInNumberObj.SetActive(false);
			CutInObj.SetActive(false);
			panelChange.Lock();
			state = eState.None;
		}
	}
}
