using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTutorialManager : MonoBehaviour
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
			if ((Object)ui_tutorial_dialog == (Object)null && prefab_dialog != (Object)null)
			{
				ui_tutorial_dialog = ResourceUtility.Realizes(prefab_dialog, -1).GetComponent<UITutorialHomeDialog>();
				ui_tutorial_dialog.Close(0, null);
			}
			return ui_tutorial_dialog;
		}
	}

	public void OnDestroy()
	{
		if ((Object)dialog != (Object)null)
		{
			Object.Destroy(dialog.gameObject);
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
		is_loading = true;
		if (DoesTutorial())
		{
			StartCoroutine(DoSetupFirstHome());
		}
		else
		{
			is_loading = false;
		}
	}

	public void ExcuteDoSetupTutorialAfterGacha2()
	{
		StartCoroutine(DoSetupTutorialAfterGacha2());
	}

	private IEnumerator DoSetupFirstHome()
	{
		if ((Object)m_home_top == (Object)null)
		{
			m_home_top = GetComponent<HomeTop>();
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
		if ((Object)m_home_top == (Object)null)
		{
			m_home_top = GetComponent<HomeTop>();
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
		is_loading = true;
		StartCoroutine(DoSetupGachaQuestTutorial());
	}

	private IEnumerator DoSetupGachaQuestTutorial()
	{
		if ((Object)m_home_top == (Object)null)
		{
			m_home_top = GetComponent<HomeTop>();
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
		if (MonoBehaviourSingleton<HomeManager>.IsValid() && !((Object)MonoBehaviourSingleton<HomeManager>.I.HomePeople == (Object)null))
		{
			List<HomeCharacterBase> charas = MonoBehaviourSingleton<HomeManager>.I.HomePeople.charas;
			charas.ForEach(delegate(HomeCharacterBase o)
			{
				if (o is HomePlayerCharacter)
				{
					o.gameObject.SetActive(false);
					Transform namePlate = o.GetNamePlate();
					if ((Object)null != (Object)namePlate)
					{
						o.GetNamePlate().gameObject.SetActive(false);
					}
				}
			});
		}
	}

	private void SetTargetAreaNPC(int npc_id)
	{
		if (!(prefab_target_area == (Object)null) && MonoBehaviourSingleton<HomeManager>.IsValid() && !((Object)MonoBehaviourSingleton<HomeManager>.I.HomePeople == (Object)null))
		{
			HomeNPCCharacter homeNPCCharacter = MonoBehaviourSingleton<HomeManager>.I.HomePeople.GetHomeNPCCharacter(npc_id);
			if (!((Object)homeNPCCharacter == (Object)null))
			{
				t_target_area = ResourceUtility.Realizes(prefab_target_area, homeNPCCharacter.transform, -1);
				t_target_area.position = homeNPCCharacter.transform.position;
			}
		}
	}

	private void SetDialog(DialogType type)
	{
		switch (type)
		{
		case DialogType.TALK_WITH_PAMERA:
			if ((Object)dialog != (Object)null)
			{
				dialog.Open(0, "Tutorial_Request_Text_0701");
			}
			break;
		case DialogType.LAST_TUTORIAL:
			if ((Object)last_tutorial != (Object)null)
			{
				last_tutorial.OpenLastTutorial();
			}
			break;
		case DialogType.AFTER_GACHA2:
			if ((Object)dialog != (Object)null)
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
		if ((Object)dialog != (Object)null)
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
		if ((Object)t_target_area != (Object)null)
		{
			t_target_area.gameObject.SetActive(false);
		}
	}
}
