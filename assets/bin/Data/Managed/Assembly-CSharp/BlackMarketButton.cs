using System;
using UnityEngine;

public class BlackMarketButton : UIBehaviour
{
	private enum UI
	{
		OBJ_TWEEN,
		TIME_COUNTDOWN_TXT,
		SPR_NOTE_UPDATE
	}

	private const float UPDATE_INTARVAL = 0.25f;

	private float timer;

	private UILabel timeLbl;

	protected override void OnOpen()
	{
		PlayTween((Enum)UI.OBJ_TWEEN, true, (EventDelegate.Callback)null, false, 0);
		OnInvitationBtnOpen(MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite || MonoBehaviourSingleton<UserInfoManager>.I.ExistsRallyInvite);
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
		}
	}

	private void Update()
	{
		if (base.state == STATE.OPEN)
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
					Close(UITransition.TYPE.CLOSE);
				}
			}
			else
			{
				Close(UITransition.TYPE.CLOSE);
			}
		}
	}

	public void UpdateNoteMarket()
	{
		SetActive((Enum)UI.SPR_NOTE_UPDATE, GameSaveData.instance.canShowNoteDarkMarket);
	}
}
