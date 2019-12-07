using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanTopBalloonControl : UIBehaviour
{
	private enum UI
	{
		OBJ_BALOON_ROOT
	}

	private const float RequestTimerInterval = 10f;

	private Transform m_clanQuestBalloon;

	private Transform m_clanDailyBalloon;

	private Vector3 m_clanQuestBalloonPos;

	private Vector3 m_clanDailyBalloonPos;

	private float m_requesTimer;

	private List<ClanDelivery> questList = new List<ClanDelivery>();

	private void InitBalloonObj()
	{
		m_clanDailyBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(UI_Common.BALLOON_TYPE.NEW_NORMAL_R, FindCtrl(base.transform, UI.OBJ_BALOON_ROOT));
		m_clanQuestBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateLoungeQuestBalloon(FindCtrl(base.transform, UI.OBJ_BALOON_ROOT));
		m_clanDailyBalloon.parent.gameObject.SetActive(value: false);
		m_clanQuestBalloon.parent.gameObject.SetActive(value: false);
		if (MonoBehaviourSingleton<StageManager>.I.stageObject != null)
		{
			Transform transform = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/QUESTBOARD_ICON_POS");
			if (transform != null)
			{
				m_clanQuestBalloonPos = transform.position;
			}
			transform = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/DAILY_ICON_POS");
			if (transform != null)
			{
				m_clanDailyBalloonPos = transform.position;
			}
		}
	}

	private void Start()
	{
		InitBalloonObj();
	}

	private void LateUpdate()
	{
		m_requesTimer += Time.deltaTime;
		if (m_requesTimer >= 10f)
		{
			StartCoroutine(UpdateBalloon());
			m_requesTimer = 0f;
		}
		SetBalloonPosition(m_clanDailyBalloon, m_clanDailyBalloonPos);
		SetBalloonPosition(m_clanQuestBalloon, m_clanQuestBalloonPos);
	}

	private IEnumerator UpdateBalloon()
	{
		bool wait = true;
		Protocol.Try(delegate
		{
			MonoBehaviourSingleton<ClanMatchingManager>.I.SendRoomParty(delegate
			{
				wait = false;
			});
		});
		while (wait)
		{
			yield return null;
		}
		if (MonoBehaviourSingleton<ClanMatchingManager>.I.clanRoomParties != null && MonoBehaviourSingleton<ClanMatchingManager>.I.clanRoomParties.Count != 0 && MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level >= 2)
		{
			if (!m_clanQuestBalloon.parent.gameObject.activeSelf)
			{
				m_clanQuestBalloon.parent.gameObject.SetActive(value: true);
				ResetTween(m_clanQuestBalloon);
				PlayTween(m_clanQuestBalloon, forward: true, null, is_input_block: false);
			}
		}
		else
		{
			m_clanQuestBalloon.parent.gameObject.SetActive(value: false);
		}
		wait = true;
		Protocol.Try(delegate
		{
			RequestDeliveryQuest(delegate
			{
				wait = false;
			});
		});
		while (wait)
		{
			yield return null;
		}
		if (!IsDailyComplete())
		{
			if (!m_clanDailyBalloon.parent.gameObject.activeSelf)
			{
				m_clanDailyBalloon.parent.gameObject.SetActive(value: true);
				ResetTween(m_clanDailyBalloon);
				PlayTween(m_clanDailyBalloon, forward: true, null, is_input_block: false);
			}
		}
		else
		{
			m_clanDailyBalloon.parent.gameObject.SetActive(value: false);
		}
		MonoBehaviourSingleton<ClanMatchingManager>.I.StartRequestClanData();
	}

	private bool IsDailyComplete()
	{
		if (questList == null || questList.Count == 0)
		{
			return true;
		}
		foreach (ClanDelivery quest in questList)
		{
			if (!quest.isComplete)
			{
				return false;
			}
		}
		return true;
	}

	private void SetBalloonPosition(Transform balloon, Vector3 iconPos)
	{
		if (!(balloon == null))
		{
			Vector3 position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(iconPos));
			position.z = ((position.z >= 0f) ? 0f : (-100f));
			balloon.position = position;
		}
	}

	public void RequestDeliveryQuest(Action<bool> call_back)
	{
		Protocol.Send(ClanDeliveryModel.URL, delegate(ClanDeliveryModel ret)
		{
			questList = ret.result.deliveryList;
			call_back(ret.Error == Error.None);
		});
	}
}
