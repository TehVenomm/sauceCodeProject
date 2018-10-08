using UnityEngine;

public class HomeNPCCharacter : HomeCharacterBase
{
	private const float LoungeBorudonScaleRate = 1.3f;

	private NPCTable.NPCData npcData;

	public OutGameSettingsManager.HomeScene.NPC npcInfo
	{
		get;
		private set;
	}

	public PLCA nearAnim
	{
		get;
		private set;
	}

	public void SetNPCInfo(OutGameSettingsManager.HomeScene.NPC npcInfo)
	{
		this.npcInfo = npcInfo;
	}

	public void SetNPCData(NPCTable.NPCData data)
	{
		npcData = data;
	}

	protected override ModelLoaderBase LoadModel()
	{
		bool useSpecialModel = false;
		HomeThemeTable.HomeThemeData homeThemeData = Singleton<HomeThemeTable>.I.GetHomeThemeData(Singleton<HomeThemeTable>.I.CurrentHomeTheme);
		if (homeThemeData != null && (npcData.specialModelID > 0 || homeThemeData.name != "NORMAL"))
		{
			useSpecialModel = true;
		}
		return npcData.LoadModel(base.gameObject, true, true, null, useSpecialModel);
	}

	protected override void InitCollider()
	{
		if (!string.IsNullOrEmpty(npcInfo.eventName))
		{
			base.InitCollider();
		}
		else
		{
			SetCollider(0.3f, 0.1f);
		}
	}

	protected override void ChangeScale()
	{
		if (MonoBehaviourSingleton<LoungeManager>.IsValid() && npcInfo.npcID == 4)
		{
			Vector3 localScale = base.transform.localScale;
			float num = 1.3f;
			base.transform.localScale = new Vector3(localScale.x * num, localScale.y * num, localScale.z * num);
		}
	}

	protected override void InitAnim()
	{
		PLCA default_anim = PLCA.IDLE_01;
		string loopAnim = npcInfo.GetLoopAnim();
		if (!string.IsNullOrEmpty(loopAnim))
		{
			default_anim = PlayerAnimCtrl.StringToEnum(loopAnim);
		}
		animCtrl = PlayerAnimCtrl.Get(animator, default_anim, OnAnimPlay, null, base.OnAnimEnd);
		string nearAnim = npcInfo.GetNearAnim();
		if (!string.IsNullOrEmpty(nearAnim))
		{
			this.nearAnim = PlayerAnimCtrl.StringToEnum(nearAnim);
		}
		else
		{
			this.nearAnim = PLCA.IDLE_01;
		}
	}

	public void Play(PLCA anim, bool instant)
	{
		if ((Object)animCtrl == (Object)null)
		{
			InitAnim();
		}
		animCtrl.Play(anim, instant);
	}

	public override bool DispatchEvent()
	{
		if (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || (Object)TutorialMessage.GetCursor(0) != (Object)null)
		{
			return false;
		}
		if (HomeBase.OnAfterGacha2Tutorial)
		{
			return false;
		}
		if (state == STATE.FREE && npcInfo != null && !string.IsNullOrEmpty(npcInfo.eventName))
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("HomeNPCCharacter", base.gameObject, npcInfo.eventName, null, null, true);
			return true;
		}
		return false;
	}

	public void SetQuestBalloon(Transform t)
	{
		t.name = HomeBase.QuestBalloonName;
		namePlate = t;
	}

	protected override bool IsVisibleNamePlate()
	{
		if (state != 0)
		{
			return false;
		}
		return base.IsVisibleNamePlate();
	}

	public bool IsLeaveState()
	{
		return state == STATE.LEAVE;
	}

	public void HideShadow()
	{
		NPCLoader nPCLoader = base.loader as NPCLoader;
		if ((bool)nPCLoader)
		{
			if ((bool)nPCLoader.shadow)
			{
				nPCLoader.shadow.gameObject.SetActive(false);
			}
		}
		else
		{
			Transform transform = base._transform.Find("CircleShadow");
			if ((Object)null != (Object)transform)
			{
				transform.gameObject.SetActive(false);
			}
		}
	}
}
