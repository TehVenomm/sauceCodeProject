using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTutorialManager
{
	private enum DialogType
	{
		TALK_WITH_PAMERA,
		LAST_TUTORIAL,
		AFTER_GACHA2
	}

	private bool is_loading;

	private HomeTop m_home_top;

	private Object prefab_dialog;

	private Object prefab_target_area;

	private Transform t_target_area;

	private UITutorialHomeDialog ui_tutorial_dialog;

	private UI_LastTutorial last_tutorial;

	public UITutorialHomeDialog dialog
	{
		get
		{
			if (ui_tutorial_dialog == null && prefab_dialog != null)
			{
				ui_tutorial_dialog = ResourceUtility.Realizes(prefab_dialog, -1).GetComponent<UITutorialHomeDialog>();
				ui_tutorial_dialog.Close(0, null);
			}
			return ui_tutorial_dialog;
		}
	}

	public HomeTutorialManager()
		: this()
	{
	}

	public void OnDestroy()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (dialog != null)
		{
			Object.Destroy(dialog.get_gameObject());
		}
	}

	public static bool DoesTutorial()
	{
		if (TutorialStep.IsPlayingFirstAccept() || TutorialStep.IsPlayingFirstDelivery())
		{
			return true;
		}
		return false;
	}

	public static bool DoesTutorialAfterGacha2()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2))
		{
			return true;
		}
		return false;
	}

	public static bool ShouldRunGachaTutorial()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_START))
		{
			return true;
		}
		return false;
	}

	public void Setup()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		is_loading = true;
		if (DoesTutorial())
		{
			this.StartCoroutine(DoSetupFirstHome());
		}
		else
		{
			is_loading = false;
		}
	}

	public void ExcuteDoSetupTutorialAfterGacha2()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoSetupTutorialAfterGacha2());
	}

	private IEnumerator DoSetupFirstHome()
	{
		if (m_home_top == null)
		{
			m_home_top = this.GetComponent<HomeTop>();
		}
		LoadingQueue lo_queue = new LoadingQueue(this);
		LoadObject obj_target_area = lo_queue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_tutorial_area_01", false);
		LoadObject obj_tutorial_dialog = lo_queue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialHomeDialog", false);
		if (lo_queue.IsLoading())
		{
			yield return (object)lo_queue.Wait();
		}
		prefab_target_area = obj_target_area.loadedObject;
		prefab_dialog = obj_tutorial_dialog.loadedObject;
		HidePassengers();
		SetTargetAreaNPC(0);
		SetDialog(DialogType.TALK_WITH_PAMERA);
		is_loading = false;
	}

	private IEnumerator DoSetupTutorialAfterGacha2()
	{
		if (m_home_top == null)
		{
			m_home_top = this.GetComponent<HomeTop>();
		}
		LoadingQueue lo_queue = new LoadingQueue(this);
		LoadObject obj_target_area = lo_queue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_tutorial_area_01", false);
		LoadObject obj_tutorial_dialog = lo_queue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialHomeDialog", false);
		if (lo_queue.IsLoading())
		{
			yield return (object)lo_queue.Wait();
		}
		prefab_target_area = obj_target_area.loadedObject;
		prefab_dialog = obj_tutorial_dialog.loadedObject;
		HidePassengers();
		SetDialog(DialogType.AFTER_GACHA2);
		is_loading = false;
	}

	public void SetupGachaQuestTutorial()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		is_loading = true;
		this.StartCoroutine(DoSetupGachaQuestTutorial());
	}

	private IEnumerator DoSetupGachaQuestTutorial()
	{
		if (m_home_top == null)
		{
			m_home_top = this.GetComponent<HomeTop>();
		}
		LoadingQueue lo_queue = new LoadingQueue(this);
		LoadObject obj_target_area = lo_queue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_tutorial_area_01", false);
		if (lo_queue.IsLoading())
		{
			yield return (object)lo_queue.Wait();
		}
		prefab_target_area = obj_target_area.loadedObject;
		HidePassengers();
		SetTargetAreaNPC(2);
		is_loading = false;
	}

	private void HidePassengers()
	{
		if (MonoBehaviourSingleton<HomeManager>.IsValid() && !(MonoBehaviourSingleton<HomeManager>.I.HomePeople == null))
		{
			List<HomeCharacterBase> charas = MonoBehaviourSingleton<HomeManager>.I.HomePeople.charas;
			charas.ForEach(delegate(HomeCharacterBase o)
			{
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0030: Unknown result type (might be due to invalid IL or missing references)
				if (o is HomePlayerCharacter)
				{
					o.get_gameObject().SetActive(false);
					Transform namePlate = o.GetNamePlate();
					if (null != namePlate)
					{
						o.GetNamePlate().get_gameObject().SetActive(false);
					}
				}
			});
		}
	}

	private void SetTargetAreaNPC(int npc_id)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (!(prefab_target_area == null) && MonoBehaviourSingleton<HomeManager>.IsValid() && !(MonoBehaviourSingleton<HomeManager>.I.HomePeople == null))
		{
			HomeNPCCharacter homeNPCCharacter = MonoBehaviourSingleton<HomeManager>.I.HomePeople.GetHomeNPCCharacter(npc_id);
			if (!(homeNPCCharacter == null))
			{
				t_target_area = ResourceUtility.Realizes(prefab_target_area, homeNPCCharacter.get_transform(), -1);
				t_target_area.set_position(homeNPCCharacter.get_transform().get_position());
			}
		}
	}

	private void SetDialog(DialogType type)
	{
		switch (type)
		{
		case DialogType.TALK_WITH_PAMERA:
			if (dialog != null)
			{
				dialog.Open(0, "Tutorial_Request_Text_0701");
			}
			break;
		case DialogType.LAST_TUTORIAL:
			if (last_tutorial != null)
			{
				last_tutorial.OpenLastTutorial();
			}
			break;
		case DialogType.AFTER_GACHA2:
			if (dialog != null)
			{
				dialog.OpenAfterGacha2();
			}
			break;
		}
	}

	private void OpenDialog()
	{
	}

	public void CloseDialog()
	{
		if (dialog != null)
		{
			dialog.Close(0, null);
		}
	}

	public bool IsLoading()
	{
		return is_loading;
	}

	public void DisableTargetArea()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (t_target_area != null)
		{
			t_target_area.get_gameObject().SetActive(false);
		}
	}
}
