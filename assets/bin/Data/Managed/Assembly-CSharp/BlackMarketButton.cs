using System;
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
		PlayTween((Enum)UI.OBJ_TWEEN, true, (EventDelegate.Callback)null, false, 0);
		OnInvitationBtnOpen(MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite || MonoBehaviourSingleton<UserInfoManager>.I.ExistsRallyInvite);
		if (!string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
		{
			int num = (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
			if (num > 0)
			{
				UpdateDrakMarketState(true);
			}
			else
			{
				UpdateDrakMarketState(false);
			}
		}
		else
		{
			UpdateDrakMarketState(false);
		}
		base.OnOpen();
	}

	public void OnInvitationBtnOpen(bool isOpen)
	{
		PlayTween((Enum)UI.OBJ_TWEEN, isOpen, (EventDelegate.Callback)null, true, 1);
	}

	public void InitTime(int time)
	{
		if (timeLbl == null)
		{
			timeLbl = GetCtrl(UI.TIME_COUNTDOWN_TXT).GetComponent<UILabel>();
		}
		SetActive((Enum)UI.SPR_NOTE_UPDATE, GameSaveData.instance.canShowNoteDarkMarket);
		UpdateDrakMarketState(true);
	}

	public void ResetMarketTime()
	{
		if (timeLbl == null)
		{
			timeLbl = GetCtrl(UI.TIME_COUNTDOWN_TXT).GetComponent<UILabel>();
		}
		int num = (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
		if (num > 0)
		{
			GameSaveData.instance.canShowNoteDarkMarket = true;
			SetActive((Enum)UI.SPR_NOTE_UPDATE, true);
			MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 37u), string.Empty);
			UpdateDrakMarketState(true);
		}
		else
		{
			UpdateDrakMarketState(false);
		}
	}

	public void UpdateDrakMarketState(bool isOpen)
	{
		isDarkMarketOpen = isOpen;
		if (!isOpen)
		{
			SetActive((Enum)UI.BTN_CLOSE, true);
			SetActive((Enum)UI.BTN_OPEN, false);
			timeLbl.text = "Preparing...";
		}
		else
		{
			SetActive((Enum)UI.BTN_CLOSE, false);
			SetActive((Enum)UI.BTN_OPEN, true);
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
			timer += Time.get_deltaTime();
		}
		if (!(timer < 0.25f))
		{
			timer = 0f;
			if (!string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
			{
				int num = (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
				timeLbl.text = UIUtility.TimeFormat(num, true);
				if (num <= 0)
				{
					UpdateDrakMarketState(false);
				}
			}
			else
			{
				UpdateDrakMarketState(false);
			}
		}
	}

	public void UpdateNoteMarket()
	{
		SetActive((Enum)UI.SPR_NOTE_UPDATE, GameSaveData.instance.canShowNoteDarkMarket);
	}
}
