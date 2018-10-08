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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		data = (GameSection.GetEventData() as FortuneWheelManager.JackpotWinData);
		if (data != null)
		{
			isOurs = IsOurWin(int.Parse(data.userId));
			SoundManager.RequestBGM((!isOurs) ? 10 : 191, false);
			pamelaActiveList = ((!isOurs) ? pamelaTheirsAnimList : pamelaOursAnimList);
			dragonActiveList = ((!isOurs) ? dragonTheirsAnimList : dragonOursAnimList);
			jackportNumber = GetCtrl(UI.JACKPOT_NUMBER).GetComponent<JackportNumber>();
			UpdateNPC();
			yield return (object)null;
			base.Initialize();
		}
	}

	private void OnDisable()
	{
		SoundManager.RequestBGM(13, true);
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.BTN_CLAIM, isOurs);
		SetActive((Enum)UI.BTN_CLOSE_NEXT, !isOurs);
		SetView(data.jackpot, isOurs, data.userName);
	}

	private void SetView(string jackpot, bool isOurs, string hunterWinName = "")
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		jackportNumber.ShowNumber(jackpot);
		Transform ctrl = GetCtrl(UI.OBJ_JACKPOT_GROUP);
		SetActive(ctrl.get_transform(), UI.OBJ_OURS, isOurs);
		SetActive(ctrl.get_transform(), UI.OBJ_THEIRS, !isOurs);
		SetActive((Enum)UI.BTN_SHARESCREENSHOT, isOurs);
		if (!isOurs)
		{
			SetLabelText(GetCtrl(UI.OBJ_THEIRS).get_transform(), UI.LBL_HUNTER_WIN, $"{hunterWinName} hit the Jackpot!");
		}
		else
		{
			SetActive(GetCtrl(UI.OBJ_OURS), UI.SPR_INFO_GRAND, data.percentage == 100);
			SetActive(GetCtrl(UI.OBJ_OURS), UI.SPR_INFO, data.percentage != 100);
		}
	}

	private bool IsOurWin(int id)
	{
		return MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == id;
	}

	private void UpdateNPC()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		SetRenderNPCModel((Enum)UI.TEX_NPCMODEL_PAMELA, 0, new Vector3(0.4f, -1.3f, 6.87f), new Vector3(0f, -157.64f, 0f), MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, (Action<NPCLoader>)delegate(NPCLoader loader)
		{
			pamelaLoader = loader;
			pamelaLoader.GetAnimator().Play(pamelaActiveList[pamelaAnimIndex]);
			SoundManager.PlayVoice((int)Enum.Parse(typeof(PamelaVoice), pamelaActiveList[pamelaAnimIndex]), 1f, 0u, null, null);
		});
		SetRenderNPCModel((Enum)UI.TEX_NPCMODEL_DRAGON, 6, new Vector3(-0.1f, -2.18f, 12f), new Vector3(0f, 169f, 0f), MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, (Action<NPCLoader>)delegate(NPCLoader loader)
		{
			dragonLoader = loader;
			dragonLoader.GetAnimator().Play(dragonActiveList[dragonAnimIndex]);
			SoundManager.PlayVoice((!isOurs) ? 600024 : 600026, 1f, 0u, null, null);
		});
	}

	private void Update()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		if (pamelaActiveList != null && pamelaLoader != null && pamelaAnimIndex < pamelaActiveList.Count)
		{
			AnimatorStateInfo currentAnimatorStateInfo = pamelaLoader.GetAnimator().GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.get_normalizedTime() >= 1f)
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
		}
		if (dragonActiveList != null && dragonLoader != null && dragonAnimIndex < dragonActiveList.Count)
		{
			AnimatorStateInfo currentAnimatorStateInfo2 = dragonLoader.GetAnimator().GetCurrentAnimatorStateInfo(0);
			if (!currentAnimatorStateInfo2.IsName(dragonActiveList[dragonAnimIndex]))
			{
				dragonAnimIndex++;
				if (dragonAnimIndex < dragonActiveList.Count)
				{
					dragonLoader.GetAnimator().Play(dragonActiveList[dragonAnimIndex]);
				}
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
