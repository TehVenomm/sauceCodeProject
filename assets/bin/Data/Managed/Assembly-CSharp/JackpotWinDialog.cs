using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackpotWinDialog : GameSection
{
	private enum UI
	{
		TEX_NPCMODEL_PAMELA,
		TEX_NPCMODEL_DRAGON,
		BTN_CLOSE_NEXT,
		BTN_CLAIM,
		OBJ_JACKPOT_GROUP,
		OBJ_OURS,
		OBJ_THEIRS,
		LBL_HUNTER_WIN,
		JACKPOT_NUMBER,
		SPR_INFO,
		SPR_INFO_GRAND,
		BTN_SHARESCREENSHOT
	}

	private enum PamelaVoice
	{
		HAPPY = 228,
		BOOK = 223,
		THINK = 8,
		YES = 213,
		TALK_01 = 219
	}

	private int pamelaAnimIndex;

	private Dictionary<int, string> pamelaOursAnimList = new Dictionary<int, string>
	{
		{
			0,
			"HAPPY"
		},
		{
			1,
			"BOOK"
		},
		{
			2,
			"IDLE_01"
		}
	};

	private Dictionary<int, string> pamelaTheirsAnimList = new Dictionary<int, string>
	{
		{
			0,
			"THINK"
		},
		{
			1,
			"YES"
		},
		{
			2,
			"TALK_01"
		},
		{
			3,
			"IDLE_01"
		}
	};

	private Dictionary<int, string> pamelaActiveList;

	private int dragonAnimIndex;

	private Dictionary<int, string> dragonOursAnimList = new Dictionary<int, string>
	{
		{
			0,
			"HAPPY"
		},
		{
			1,
			"IDLE_02"
		}
	};

	private Dictionary<int, string> dragonTheirsAnimList = new Dictionary<int, string>
	{
		{
			0,
			"NO"
		},
		{
			1,
			"IDLE_02"
		}
	};

	private Dictionary<int, string> dragonActiveList;

	private NPCLoader pamelaLoader;

	private NPCLoader dragonLoader;

	private JackportNumber jackportNumber;

	private Transform fireball_;

	private bool isOurs;

	private FortuneWheelManager.JackpotWinData data;

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		data = (GameSection.GetEventData() as FortuneWheelManager.JackpotWinData);
		if (data != null)
		{
			isOurs = IsOurWin(int.Parse(data.userId));
			SoundManager.RequestBGM(isOurs ? 191 : 10, isLoop: false);
			pamelaActiveList = (isOurs ? pamelaOursAnimList : pamelaTheirsAnimList);
			dragonActiveList = (isOurs ? dragonOursAnimList : dragonTheirsAnimList);
			jackportNumber = GetCtrl(UI.JACKPOT_NUMBER).GetComponent<JackportNumber>();
			UpdateNPC();
			yield return null;
			base.Initialize();
		}
	}

	private void OnDisable()
	{
		SoundManager.RequestBGM(13);
	}

	public override void UpdateUI()
	{
		SetActive(UI.BTN_CLAIM, isOurs);
		SetActive(UI.BTN_CLOSE_NEXT, !isOurs);
		SetView(data.jackpot, isOurs, data.userName);
	}

	private void SetView(string jackpot, bool isOurs, string hunterWinName = "")
	{
		jackportNumber.ShowNumber(jackpot);
		Transform ctrl = GetCtrl(UI.OBJ_JACKPOT_GROUP);
		SetActive(ctrl.transform, UI.OBJ_OURS, isOurs);
		SetActive(ctrl.transform, UI.OBJ_THEIRS, !isOurs);
		SetActive(UI.BTN_SHARESCREENSHOT, isOurs);
		if (!isOurs)
		{
			SetLabelText(GetCtrl(UI.OBJ_THEIRS).transform, UI.LBL_HUNTER_WIN, string.Format(StringTable.Get(STRING_CATEGORY.DRAGON_VAULT, 3u), hunterWinName));
			return;
		}
		SetActive(GetCtrl(UI.OBJ_OURS), UI.SPR_INFO_GRAND, data.percentage == 100);
		SetActive(GetCtrl(UI.OBJ_OURS), UI.SPR_INFO, data.percentage != 100);
	}

	private bool IsOurWin(int id)
	{
		return MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == id;
	}

	private void UpdateNPC()
	{
		SetRenderNPCModel(UI.TEX_NPCMODEL_PAMELA, 0, new Vector3(0.4f, -1.3f, 6.87f), new Vector3(0f, -157.64f, 0f), MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, delegate(NPCLoader loader)
		{
			pamelaLoader = loader;
			pamelaLoader.GetAnimator().Play(pamelaActiveList[pamelaAnimIndex]);
			SoundManager.PlayVoice((int)Enum.Parse(typeof(PamelaVoice), pamelaActiveList[pamelaAnimIndex]));
		});
		SetRenderNPCModel(UI.TEX_NPCMODEL_DRAGON, 6, new Vector3(-0.1f, -2.18f, 12f), new Vector3(0f, 169f, 0f), MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, delegate(NPCLoader loader)
		{
			dragonLoader = loader;
			dragonLoader.GetAnimator().Play(dragonActiveList[dragonAnimIndex]);
			SoundManager.PlayVoice(isOurs ? 600026 : 600024);
		});
	}

	private void Update()
	{
		if (pamelaActiveList != null && pamelaLoader != null && pamelaAnimIndex < pamelaActiveList.Count && pamelaLoader.GetAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			pamelaAnimIndex++;
			if (pamelaAnimIndex < pamelaActiveList.Count)
			{
				if (!pamelaActiveList[pamelaAnimIndex].Contains("IDLE"))
				{
					SoundManager.PlayOneShotUISE((int)Enum.Parse(typeof(PamelaVoice), pamelaActiveList[pamelaAnimIndex]));
				}
				pamelaLoader.GetAnimator().Play(pamelaActiveList[pamelaAnimIndex]);
			}
		}
		if (dragonActiveList != null && dragonLoader != null && dragonAnimIndex < dragonActiveList.Count && !dragonLoader.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName(dragonActiveList[dragonAnimIndex]))
		{
			dragonAnimIndex++;
			if (dragonAnimIndex < dragonActiveList.Count)
			{
				dragonLoader.GetAnimator().Play(dragonActiveList[dragonAnimIndex]);
			}
		}
	}

	public void OnQuery_SHARE()
	{
	}

	public void OnQuery_CLOSE()
	{
		GameSection.BackSection();
	}
}
