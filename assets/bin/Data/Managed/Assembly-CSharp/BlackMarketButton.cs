using UnityEngine;

public class BlackMarketButton : UIBehaviour
{
	private enum UI
	{
		OBJ_TWEEN,
		TIME_COUNTDOWN_TXT,
		SPR_NOTE_UPDATE,
		BTN_OPEN,
		BTN_CLOSE
	}

	private const float UPDATE_INTARVAL = 0.25f;

	private float timer;

	private bool isDarkMarketOpen;

	private UILabel timeLbl;

	protected override void OnOpen()
	{
		if (timeLbl == null)
		{
			timeLbl = GetCtrl(UI.TIME_COUNTDOWN_TXT).GetComponent<UILabel>();
		}
		PlayTween(UI.OBJ_TWEEN, forward: true, null, is_input_block: false);
		OnInvitationBtnOpen(MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite || MonoBehaviourSingleton<UserInfoManager>.I.ExistsRallyInvite);
		if (!string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
		{
			if ((int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds > 0)
			{
				UpdateDrakMarketState(isOpen: true);
			}
			else
			{
				UpdateDrakMarketState(isOpen: false);
			}
		}
		else
		{
			UpdateDrakMarketState(isOpen: false);
		}
		base.OnOpen();
	}

	public void OnInvitationBtnOpen(bool isOpen)
	{
		PlayTween(UI.OBJ_TWEEN, isOpen, null, is_input_block: true, 1);
	}

	public void InitTime(int time)
	{
		if (timeLbl == null)
		{
			timeLbl = GetCtrl(UI.TIME_COUNTDOWN_TXT).GetComponent<UILabel>();
		}
		SetActive(UI.SPR_NOTE_UPDATE, GameSaveData.instance.canShowNoteDarkMarket);
		UpdateDrakMarketState(isOpen: true);
	}

	public void ResetMarketTime()
	{
		if (timeLbl == null)
		{
			timeLbl = GetCtrl(UI.TIME_COUNTDOWN_TXT).GetComponent<UILabel>();
		}
		if ((int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds > 0)
		{
			GameSaveData.instance.canShowNoteDarkMarket = true;
			SetActive(UI.SPR_NOTE_UPDATE, is_visible: true);
			MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 37u), "");
			UpdateDrakMarketState(isOpen: true);
		}
		else
		{
			UpdateDrakMarketState(isOpen: false);
		}
	}

	public void UpdateDrakMarketState(bool isOpen)
	{
		isDarkMarketOpen = isOpen;
		if (!isOpen)
		{
			SetActive(UI.BTN_CLOSE, is_visible: true);
			SetActive(UI.BTN_OPEN, is_visible: false);
			timeLbl.text = "Preparing...";
		}
		else
		{
			SetActive(UI.BTN_CLOSE, is_visible: false);
			SetActive(UI.BTN_OPEN, is_visible: true);
		}
	}

	private void Update()
	{
		if (isDarkMarketOpen)
		{
			UpdateTimers();
		}
	}

	private void UpdateTimers()
	{
		if (timer < 0.25f)
		{
			timer += Time.deltaTime;
		}
		if (timer < 0.25f)
		{
			return;
		}
		timer = 0f;
		if (!string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
		{
			int num = (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
			timeLbl.text = UIUtility.TimeFormat(num, isHours: true);
			if (num <= 0)
			{
				UpdateDrakMarketState(isOpen: false);
			}
		}
		else
		{
			UpdateDrakMarketState(isOpen: false);
		}
	}

	public void UpdateNoteMarket()
	{
		SetActive(UI.SPR_NOTE_UPDATE, GameSaveData.instance.canShowNoteDarkMarket);
	}
}
