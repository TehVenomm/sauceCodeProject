using Network;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class HomeTopBonusTime : UIBehaviour
{
	private enum UI
	{
		OBJ_BONUS_TIME_VISIBLE,
		LBL_BONUS_TIME_NAME,
		LBL_BONUS_TIME_DATE,
		BTN_BONUS_TIME,
		OBJ_TWEEN_BONUS_TIME,
		SPR_ALL,
		SPR_MORNING,
		SPR_AFTERNOON,
		SPR_NIGHT
	}

	private bool isClosedBonusTime = true;

	private UITweenCtrl tweenCtrl;

	private Transform myTrans;

	private bool isFirst = true;

	private bool isActiveTap = true;

	private static readonly UI[] PlateUI = new UI[4]
	{
		UI.SPR_ALL,
		UI.SPR_MORNING,
		UI.SPR_AFTERNOON,
		UI.SPR_NIGHT
	};

	private List<TimeSlotEvent> timeSlotEvents = new List<TimeSlotEvent>();

	private int currentIndex;

	private int previousIndex = -1;

	private string currentStartDate = string.Empty;

	private string currentEndDate = string.Empty;

	private bool isVisible;

	private float time;

	private static readonly float ChangeIndexTime = 2f;

	public void SetUp()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		myTrans = this.get_transform();
		tweenCtrl = myTrans.GetComponent<UITweenCtrl>();
		isFirst = true;
		timeSlotEvents = MonoBehaviourSingleton<StatusManager>.I.timeSlotEvents;
		SetActive(myTrans, UI.OBJ_BONUS_TIME_VISIBLE, false);
	}

	public void UpdateBonusTime()
	{
		bool flag = IsBonusTime();
		if (isVisible != flag)
		{
			isVisible = flag;
			SetActive(myTrans, UI.OBJ_BONUS_TIME_VISIBLE, flag);
		}
		if (flag)
		{
			if (timeSlotEvents.Count >= 2)
			{
				UpdateVisualIndex();
			}
			if (previousIndex != currentIndex)
			{
				previousIndex = currentIndex;
				Transform plate = UpdatePlate();
				UpdateName(plate);
			}
			if (IsUpdateDate())
			{
				UpdateDate();
				if (isFirst)
				{
					isActiveTap = true;
					TweenFirst();
					isFirst = false;
					isClosedBonusTime = false;
					time = 0f;
				}
			}
		}
	}

	private void UpdateVisualIndex()
	{
		time += Time.get_deltaTime();
		if (time >= ChangeIndexTime)
		{
			currentIndex++;
			if (currentIndex >= timeSlotEvents.Count)
			{
				currentIndex = 0;
			}
			time = 0f;
		}
	}

	public void OnTap()
	{
		if (isActiveTap)
		{
			TweenBonusTime(isClosedBonusTime);
			isClosedBonusTime = !isClosedBonusTime;
		}
	}

	private void TweenFirst()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		TweenPosition tween = FindCtrl(myTrans, UI.OBJ_TWEEN_BONUS_TIME).GetComponent<TweenPosition>();
		Vector3 defaultFrom = default(Vector3);
		if (tween != null)
		{
			defaultFrom = tween.from;
			Vector3 from = defaultFrom;
			from.x = 350f;
			tween.from = from;
		}
		if (!(tweenCtrl == null))
		{
			isActiveTap = false;
			tweenCtrl.Play(true, delegate
			{
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				tween.from = defaultFrom;
				isActiveTap = true;
			});
		}
	}

	private void TweenBonusTime(bool isCloseBonusTime)
	{
		if (!(tweenCtrl == null))
		{
			isActiveTap = false;
			tweenCtrl.Play(isCloseBonusTime, delegate
			{
				isActiveTap = true;
			});
		}
	}

	private void UpdateName(Transform plate)
	{
		TimeSlotEvent timeSlotEvent = timeSlotEvents[currentIndex];
		SetLabelText(plate, UI.LBL_BONUS_TIME_NAME, timeSlotEvent.description);
	}

	private void UpdateDate()
	{
		string startData = timeSlotEvents[currentIndex].startData;
		string endDate = timeSlotEvents[currentIndex].endDate;
		currentStartDate = startData;
		currentEndDate = endDate;
		StringBuilder stringBuilder = new StringBuilder(13);
		stringBuilder.Append(GetTime(startData));
		stringBuilder.Append("\u3000〜\u3000");
		stringBuilder.Append(GetTime(endDate));
		string text = stringBuilder.ToString();
		SetLabelText(myTrans, UI.LBL_BONUS_TIME_DATE, text);
	}

	private string GetTime(string date)
	{
		string[] array = date.Split(' ');
		string[] array2 = array[1].Split(':');
		if (int.TryParse(array2[0], out int result) && int.TryParse(array2[1], out int result2))
		{
			return $"{result:d1}:{result2:d2}";
		}
		return $"{array2[0]:d1}:{array2[1]:d2}";
	}

	private Transform UpdatePlate()
	{
		int i = 0;
		for (int num = PlateUI.Length; i < num; i++)
		{
			SetActive(myTrans, PlateUI[i], false);
		}
		int num2 = timeSlotEvents[currentIndex].timeSlotType;
		if (num2 >= PlateUI.Length)
		{
			num2 = 1;
		}
		SetActive(myTrans, PlateUI[num2], true);
		return FindCtrl(myTrans, PlateUI[num2]);
	}

	private bool IsBonusTime()
	{
		if (timeSlotEvents == null)
		{
			return false;
		}
		if (timeSlotEvents.Count <= 0)
		{
			return false;
		}
		DateTime now = TimeManager.GetNow();
		DateTime dateTime = DateTime.Parse(timeSlotEvents[currentIndex].endDate);
		DateTime dateTime2 = DateTime.Parse(timeSlotEvents[currentIndex].startData);
		if (dateTime.CompareTo(now) <= 0 || dateTime2.CompareTo(now) >= 0)
		{
			timeSlotEvents.RemoveAt(currentIndex);
			currentIndex = 0;
			return false;
		}
		return true;
	}

	private bool IsUpdateDate()
	{
		string startData = timeSlotEvents[currentIndex].startData;
		string endDate = timeSlotEvents[currentIndex].endDate;
		if (currentStartDate == startData && currentEndDate == endDate)
		{
			return false;
		}
		currentStartDate = startData;
		currentEndDate = endDate;
		return true;
	}
}
