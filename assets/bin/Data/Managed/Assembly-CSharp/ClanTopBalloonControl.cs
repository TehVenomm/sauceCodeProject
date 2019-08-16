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
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		m_clanDailyBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(UI_Common.BALLOON_TYPE.NEW_NORMAL_R, FindCtrl(this.get_transform(), UI.OBJ_BALOON_ROOT));
		m_clanQuestBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateLoungeQuestBalloon(FindCtrl(this.get_transform(), UI.OBJ_BALOON_ROOT));
		m_clanDailyBalloon.get_parent().get_gameObject().SetActive(false);
		m_clanQuestBalloon.get_parent().get_gameObject().SetActive(false);
		if (MonoBehaviourSingleton<StageManager>.I.stageObject != null)
		{
			Transform val = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/QUESTBOARD_ICON_POS");
			if (val != null)
			{
				m_clanQuestBalloonPos = val.get_position();
			}
			val = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/DAILY_ICON_POS");
			if (val != null)
			{
				m_clanDailyBalloonPos = val.get_position();
			}
		}
	}

	private void Start()
	{
		InitBalloonObj();
	}

	private void LateUpdate()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		m_requesTimer += Time.get_deltaTime();
		if (m_requesTimer >= 10f)
		{
			this.StartCoroutine(UpdateBalloon());
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
			if (!m_clanQuestBalloon.get_parent().get_gameObject().get_activeSelf())
			{
				m_clanQuestBalloon.get_parent().get_gameObject().SetActive(true);
				ResetTween(m_clanQuestBalloon);
				PlayTween(m_clanQuestBalloon, forward: true, null, is_input_block: false);
			}
		}
		else
		{
			m_clanQuestBalloon.get_parent().get_gameObject().SetActive(false);
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
			if (!m_clanDailyBalloon.get_parent().get_gameObject().get_activeSelf())
			{
				m_clanDailyBalloon.get_parent().get_gameObject().SetActive(true);
				ResetTween(m_clanDailyBalloon);
				PlayTween(m_clanDailyBalloon, forward: true, null, is_input_block: false);
			}
		}
		else
		{
			m_clanDailyBalloon.get_parent().get_gameObject().SetActive(false);
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
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (!(balloon == null))
		{
			Vector3 position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(iconPos));
			position.z = ((!(position.z >= 0f)) ? (-100f) : 0f);
			balloon.set_position(position);
		}
	}

	public void RequestDeliveryQuest(Action<bool> call_back)
	{
		Protocol.Send(ClanDeliveryModel.URL, delegate(ClanDeliveryModel ret)
		{
			questList = ret.result.deliveryList;
			call_back(ret.Error == Error.None);
		}, string.Empty);
	}
}
