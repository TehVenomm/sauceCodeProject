using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLoginBonus : GameSection
{
	private enum UI
	{
		TEX_LOGIN_BANNER,
		LBL_PERIOD,
		LBL_PICKUP,
		LBL_LOGIN_DAYS,
		GRD_BONUSLIST,
		SCR_BONUSLIST,
		SPR_FRAME,
		LBL_INFO_NUM,
		LBL_INFO_NAME,
		LBL_INFODETAIL_NUM,
		LBL_INFODETAIL_NAME,
		LBL_INFODETAIL_DESC,
		OBJ_ICON_ROOT,
		LBL_DAY,
		LBL_DAY_PICKUP,
		LBL_DAY_FINE,
		LBL_ITEMNUM,
		SPR_DAY_BASE,
		SPR_DAY_BASE_PICKUP,
		SPR_DAY_BASE_FINE,
		SPR_STAMP,
		SPR_STAMP_ANIM,
		TEX_MODEL,
		TEX_INNER_MODEL,
		OBJ_DETAIL_ROOT,
		SPR_FRAME_PILLER_L,
		SPR_FRAME_PILLER_R,
		SPR_FRAME_FOOTER_L,
		SPR_FRAME_FOOTER_R,
		SPR_FRAME_BG_1_L,
		SPR_FRAME_BG_1_R,
		SPR_FRAME_BG_2_L,
		SPR_FRAME_BG_2_R,
		SPR_FRAME_BG_3_L,
		SPR_FRAME_BG_3_R,
		SPR_FRAME_BG_4_L,
		SPR_FRAME_BG_4_R,
		SPR_FRAME_ICONS,
		BTN_CLOSE
	}

	private enum BG_COLOR
	{
		RED = 1,
		YELLOW,
		BLUE
	}

	private enum AUDIO
	{
		REQUEST_COMPLETE = 40000029
	}

	private static Color bgYellow = new Color(248f / 255f, 46f / 85f, 38f / 255f);

	private static Color bgRed = new Color(29f / 85f, 67f / 85f, 248f / 255f);

	private static Color bgBlue = new Color(248f / 255f, 29f / 85f, 0.3529412f);

	private static float iconHeight = 112f;

	private static float frameHeightMargin = 20f;

	private static Vector2 frameIconSizeBase = new Vector2(440f, 360f);

	private static Vector2 pillerSizeBase = new Vector2(11f, 755f);

	private static Vector2 bg3SizeBase = new Vector2(226f, 327f);

	private static float bg4Height = -360f;

	private static float footerHeight = -412.1f;

	private static float btnHeight = -369f;

	private static float pickUpPosX = -163f;

	private static float pickUpItemPosY = 150f;

	private static float scrollStartHeight = -38f;

	private const int BEGINNER_LOGIN_BONUS_ID = 6;

	private Transform texModel_;

	private UIModelRenderTexture texModelRenderTexture_;

	private UITexture texModelTexture_;

	private Transform texInnerModel_;

	private UIModelRenderTexture texInnerModelRenderTexture_;

	private UITexture texInnerModelTexture_;

	private Transform glowModel_;

	private bool isFirst;

	private bool isModel;

	private float startScrPos;

	private int arrayNow;

	private bool isDetail;

	private Transform info;

	private Transform infoDetail;

	private LoginBonus dummyLogBo;

	private LoginBonus lb;

	private LoginBonus.LoginBonusReward pickUpReward;

	private LoadObject topImageLoadObj;

	private bool showedBLBP;

	private List<Transform> touchAndReleaseList = new List<Transform>();

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		glowModel_ = Utility.Find(base._transform, "LIB_00000003");
		texModel_ = Utility.Find(base._transform, "TEX_MODEL");
		texModelRenderTexture_ = UIModelRenderTexture.Get(texModel_);
		texModelTexture_ = texModel_.GetComponent<UITexture>();
		texInnerModel_ = Utility.Find(base._transform, "TEX_INNER_MODEL");
		texInnerModelRenderTexture_ = UIModelRenderTexture.Get(texInnerModel_);
		texInnerModelTexture_ = texInnerModel_.GetComponent<UITexture>();
		info = SetPrefab(GetCtrl(UI.SPR_FRAME), "LimitedLoginBonusInfo");
		infoDetail = SetPrefab(GetCtrl(UI.SPR_FRAME), "LimitedLoginBonusInfoDetail");
		info.gameObject.SetActive(value: false);
		infoDetail.gameObject.SetActive(value: false);
		StartCoroutine(DoInitialize());
	}

	protected void OnQuery_RELEASE_ABILITY()
	{
		if (!(infoDetail == null) && !(info == null))
		{
			if (isDetail)
			{
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_NAME, "");
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_DESC, "");
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_NUM, "");
				infoDetail.gameObject.SetActive(value: false);
			}
			else
			{
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_NAME, "");
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_DESC, "");
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_NUM, "");
			}
			infoDetail.gameObject.SetActive(value: false);
			info.gameObject.SetActive(value: false);
			GameSection.StopEvent();
		}
	}

	protected void OnQuery_ABILITY_DATA_POPUP()
	{
		int index = (int)(GameSection.GetEventData() as object[])[0];
		string text = "";
		string text2 = null;
		string text3 = "";
		LoginBonus.LoginBonusReward loginBonusReward = lb.next[index].reward[0];
		if (Singleton<ItemTable>.I.IsExistItemData((uint)loginBonusReward.itemId))
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)loginBonusReward.itemId);
			if (itemData != null)
			{
				text = itemData.name;
				text2 = itemData.text;
			}
		}
		if (string.IsNullOrEmpty(text))
		{
			text = loginBonusReward.name;
		}
		text3 = "x" + loginBonusReward.itemNum.ToString();
		Vector3 localPosition = new Vector3(0f, 60f, 0f);
		if (text2 != null)
		{
			SetLabelText(infoDetail, UI.LBL_INFODETAIL_NAME, text);
			SetLabelText(infoDetail, UI.LBL_INFODETAIL_DESC, text2);
			SetLabelText(infoDetail, UI.LBL_INFODETAIL_NUM, text3);
			infoDetail.localPosition = localPosition;
			infoDetail.gameObject.SetActive(value: true);
			isDetail = true;
		}
		else
		{
			SetLabelText(info, UI.LBL_INFO_NAME, text);
			SetLabelText(info, UI.LBL_INFO_NUM, text3);
			info.localPosition = localPosition;
			info.gameObject.SetActive(value: true);
			isDetail = false;
		}
		GameSection.StopEvent();
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		base.OnNotify(flags);
		if ((flags & NOTIFY_FLAG.PRETREAT_SCENE) != (NOTIFY_FLAG)0L)
		{
			NoEventReleaseTouchAndReleases(touchAndReleaseList);
			OnQuery_RELEASE_ABILITY();
		}
	}

	private IEnumerator DoInitialize()
	{
		bool connect = false;
		lb = null;
		if (GameSection.GetEventData() != null)
		{
			LoginBonusConfirmModel.RequestSendForm requestSendForm = new LoginBonusConfirmModel.RequestSendForm();
			requestSendForm.loginBonusId = (int)GameSection.GetEventData();
			Protocol.Send(LoginBonusConfirmModel.URL, requestSendForm, delegate(LoginBonusConfirmModel ret)
			{
				if (ret.Error == Error.None)
				{
					if (ret != null && ret.result != null && ret.result.Count > 0)
					{
						lb = ret.result[0];
					}
					connect = true;
				}
			});
			while (!connect)
			{
				yield return null;
			}
		}
		if (!connect)
		{
			lb = MonoBehaviourSingleton<AccountManager>.I.logInBonus[0];
			MonoBehaviourSingleton<AccountManager>.I.logInBonus.Remove(lb);
		}
		if (lb == null)
		{
			base.Initialize();
			while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
			{
				yield return null;
			}
			GameSection.BackSection();
			yield break;
		}
		arrayNow = 0;
		int i = 0;
		for (int count = lb.next.Count; i < count; i++)
		{
			if (lb.next[i].count == lb.nowCount)
			{
				arrayNow = i;
				break;
			}
			if (lb.next[i].count > lb.nowCount)
			{
				arrayNow = i;
				break;
			}
		}
		int num = 1 + (lb.next.Count - 1) / 5;
		int num2 = 1 + arrayNow / 5;
		if (num > 3)
		{
			if (num2 > num - 2)
			{
				num2 = num - 2;
			}
			startScrPos = scrollStartHeight + iconHeight * (float)(num2 - 1);
			isFirst = true;
		}
		else
		{
			isFirst = false;
		}
		SetPickUp();
		float rotateSpeed = 35f;
		if (14 == pickUpReward.type)
		{
			SetRenderAccessoryModel(UI.TEX_MODEL, (uint)pickUpReward.itemId, pickUpReward.GetScale());
			isModel = true;
		}
		else if (5 == pickUpReward.type)
		{
			uint itemId = (uint)pickUpReward.itemId;
			texModelRenderTexture_.InitSkillItem(texModelTexture_, itemId, rotation: true, light_rotation: false, 45f);
			texInnerModelRenderTexture_.InitSkillItemSymbol(texInnerModelTexture_, itemId, rotation: true, 17f);
			isModel = true;
		}
		else if (4 == pickUpReward.type)
		{
			SetRenderEquipModel(UI.TEX_MODEL, (uint)pickUpReward.itemId, -1, -1, pickUpReward.GetScale());
			isModel = true;
		}
		else if (1 == pickUpReward.type || 2 == pickUpReward.type)
		{
			uint itemModelID = GetItemModelID((REWARD_TYPE)pickUpReward.type, pickUpReward.itemId);
			texModelRenderTexture_.InitItem(texModelTexture_, itemModelID);
			isModel = true;
		}
		else if (3 == pickUpReward.type && IsDispItem3D(pickUpReward.itemId))
		{
			uint itemModelID2 = GetItemModelID((REWARD_TYPE)pickUpReward.type, pickUpReward.itemId);
			texModelRenderTexture_.InitItem(texModelTexture_, itemModelID2);
			isModel = true;
		}
		texModelRenderTexture_.SetRotateSpeed(rotateSpeed);
		texInnerModelRenderTexture_.SetRotateSpeed(rotateSpeed);
		LoadingQueue loadingQueue = new LoadingQueue(this);
		string loginBonusTopImage = ResourceName.GetLoginBonusTopImage(lb.loginBonusId);
		topImageLoadObj = loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.LOGINBONUS_IMAGE, loginBonusTopImage);
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (lb == null)
		{
			SetLabelText(UI.LBL_PICKUP, "");
			SetLabelText(UI.LBL_PERIOD, "");
			return;
		}
		if (topImageLoadObj != null)
		{
			Texture2D texture2D = null;
			texture2D = (topImageLoadObj.loadedObject as Texture2D);
			if (texture2D != null)
			{
				Transform t2 = FindCtrl(base._transform, UI.TEX_LOGIN_BANNER);
				SetActive(t2, is_visible: true);
				SetTexture(t2, texture2D);
			}
		}
		if (!isModel)
		{
			FindCtrl(base._transform, UI.OBJ_DETAIL_ROOT).localPosition = new Vector3(pickUpPosX, pickUpItemPosY, 0f);
			LoginBonus.LoginBonusReward loginBonusReward = pickUpReward;
			ItemIcon.CreateRewardItemIcon((REWARD_TYPE)loginBonusReward.type, (uint)loginBonusReward.itemId, Utility.Find(base._transform, "OBJ_DETAIL_ROOT")).transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		}
		SetLabelText(UI.LBL_PERIOD, lb.period_announce);
		SetLabelText(UI.LBL_LOGIN_DAYS, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 7u), lb.nowCount.ToString()));
		FindCtrl(base._transform, UI.LBL_PICKUP).GetComponent<UILabel>().supportEncoding = true;
		SetLabelText(UI.LBL_PICKUP, pickUpReward.pickUpText);
		int count = lb.next.Count;
		SetFrame(1 + (lb.next.Count - 1) / 5, lb.boardType);
		touchAndReleaseList.Clear();
		SetGrid(UI.GRD_BONUSLIST, "LimitedLoginBonusItem", count, reset: false, delegate(int i, Transform t, bool b)
		{
			bool flag = false;
			LoginBonus.LoginBonusReward loginBonusReward2 = null;
			loginBonusReward2 = lb.next[i].reward[0];
			flag = loginBonusReward2.isGet;
			if (arrayNow == i && lb.reward.Count > 0)
			{
				GameObject gameObject = FindCtrl(t, UI.SPR_STAMP_ANIM).gameObject;
				gameObject.SetActive(value: true);
				EventDelegate.Set(gameObject.GetComponentInChildren<TweenScale>().onFinished, SetGetDialog);
				FindCtrl(t, UI.SPR_STAMP).gameObject.SetActive(value: false);
			}
			else if (flag)
			{
				FindCtrl(t, UI.SPR_STAMP_ANIM).gameObject.SetActive(value: false);
				FindCtrl(t, UI.SPR_STAMP).gameObject.SetActive(value: true);
			}
			else
			{
				FindCtrl(t, UI.SPR_STAMP).gameObject.SetActive(value: false);
				FindCtrl(t, UI.SPR_STAMP_ANIM).gameObject.SetActive(value: false);
			}
			if (loginBonusReward2.isPickUp)
			{
				FindCtrl(t, UI.SPR_DAY_BASE).gameObject.SetActive(value: false);
				FindCtrl(t, UI.SPR_DAY_BASE_PICKUP).gameObject.SetActive(value: true);
				FindCtrl(t, UI.SPR_DAY_BASE_FINE).gameObject.SetActive(value: false);
				FindCtrl(t, UI.LBL_DAY_PICKUP).gameObject.SetActive(value: true);
				FindCtrl(t, UI.LBL_DAY_FINE).gameObject.SetActive(value: false);
				FindCtrl(t, UI.LBL_DAY).gameObject.SetActive(value: false);
				SetLabelText(t, UI.LBL_DAY_PICKUP, loginBonusReward2.day);
			}
			else if (loginBonusReward2.frameType != 0)
			{
				FindCtrl(t, UI.SPR_DAY_BASE).gameObject.SetActive(value: false);
				FindCtrl(t, UI.SPR_DAY_BASE_PICKUP).gameObject.SetActive(value: false);
				FindCtrl(t, UI.SPR_DAY_BASE_FINE).gameObject.SetActive(value: true);
				FindCtrl(t, UI.LBL_DAY_PICKUP).gameObject.SetActive(value: false);
				FindCtrl(t, UI.LBL_DAY_FINE).gameObject.SetActive(value: true);
				FindCtrl(t, UI.LBL_DAY).gameObject.SetActive(value: false);
				SetLabelText(t, UI.LBL_DAY_FINE, loginBonusReward2.day);
			}
			else
			{
				FindCtrl(t, UI.SPR_DAY_BASE).gameObject.SetActive(value: true);
				FindCtrl(t, UI.SPR_DAY_BASE_PICKUP).gameObject.SetActive(value: false);
				FindCtrl(t, UI.SPR_DAY_BASE_FINE).gameObject.SetActive(value: false);
				FindCtrl(t, UI.LBL_DAY_PICKUP).gameObject.SetActive(value: false);
				FindCtrl(t, UI.LBL_DAY_FINE).gameObject.SetActive(value: false);
				FindCtrl(t, UI.LBL_DAY).gameObject.SetActive(value: true);
				SetLabelText(t, UI.LBL_DAY, loginBonusReward2.day);
			}
			SetLabelText(t, UI.LBL_ITEMNUM, "x" + loginBonusReward2.itemNum.ToString());
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)loginBonusReward2.type, (uint)loginBonusReward2.itemId, FindCtrl(t, UI.OBJ_ICON_ROOT));
			if (itemIcon != null)
			{
				itemIcon.SetEnableCollider(is_enable: false);
			}
			if (flag)
			{
				UITexture[] componentsInChildren = itemIcon.GetComponentsInChildren<UITexture>();
				int j = 0;
				for (int num = componentsInChildren.Length; j < num; j++)
				{
					componentsInChildren[j].color = Color.gray;
				}
				UISprite[] componentsInChildren2 = itemIcon.GetComponentsInChildren<UISprite>();
				int k = 0;
				for (int num2 = componentsInChildren2.Length; k < num2; k++)
				{
					componentsInChildren2[k].color = Color.gray;
				}
			}
			SetAbilityItemEvent(t, i, touchAndReleaseList);
		});
		if (isFirst)
		{
			Transform ctrl = GetCtrl(UI.SCR_BONUSLIST);
			UIPanel component = ctrl.GetComponent<UIPanel>();
			ctrl.transform.localPosition = new Vector3(0f, startScrPos, 0f);
			component.clipOffset = new Vector2(0f, 0f - startScrPos);
		}
	}

	private void SetFrame(int column_num, int board_type)
	{
		Color color;
		switch (board_type)
		{
		case 3:
			color = bgBlue;
			break;
		case 2:
			color = bgYellow;
			break;
		default:
			color = bgRed;
			break;
		}
		FindCtrl(base._transform, UI.SPR_FRAME_BG_1_L).GetComponent<UISprite>().color = color;
		FindCtrl(base._transform, UI.SPR_FRAME_BG_1_R).GetComponent<UISprite>().color = color;
		FindCtrl(base._transform, UI.SPR_FRAME_BG_2_L).GetComponent<UISprite>().color = color;
		FindCtrl(base._transform, UI.SPR_FRAME_BG_2_R).GetComponent<UISprite>().color = color;
		FindCtrl(base._transform, UI.SPR_FRAME_BG_3_L).GetComponent<UISprite>().color = color;
		FindCtrl(base._transform, UI.SPR_FRAME_BG_3_R).GetComponent<UISprite>().color = color;
		FindCtrl(base._transform, UI.SPR_FRAME_BG_4_L).GetComponent<UISprite>().color = color;
		FindCtrl(base._transform, UI.SPR_FRAME_BG_4_R).GetComponent<UISprite>().color = color;
		if (column_num < 4)
		{
			float num = (float)column_num * iconHeight + frameHeightMargin;
			float num2 = frameIconSizeBase.y - num;
			FindCtrl(base._transform, UI.SPR_FRAME_PILLER_L).GetComponent<UIWidget>().height = (int)(pillerSizeBase.y - num2);
			FindCtrl(base._transform, UI.SPR_FRAME_PILLER_R).GetComponent<UIWidget>().height = (int)(pillerSizeBase.y - num2);
			FindCtrl(base._transform, UI.SPR_FRAME_ICONS).GetComponent<UIWidget>().height = (int)num;
			FindCtrl(base._transform, UI.SPR_FRAME_BG_3_L).GetComponent<UIWidget>().height = (int)(bg3SizeBase.y - num2);
			FindCtrl(base._transform, UI.SPR_FRAME_BG_3_R).GetComponent<UIWidget>().height = (int)(bg3SizeBase.y - num2);
			FindCtrl(base._transform, UI.SPR_FRAME_BG_4_L).localPosition = new Vector3(0f, bg4Height + num2, 0f);
			FindCtrl(base._transform, UI.SPR_FRAME_BG_4_R).localPosition = new Vector3(0f, bg4Height + num2, 0f);
			FindCtrl(base._transform, UI.SPR_FRAME_FOOTER_L).localPosition = new Vector3(0f, footerHeight + num2, 0f);
			FindCtrl(base._transform, UI.SPR_FRAME_FOOTER_R).localPosition = new Vector3(0f, footerHeight + num2, 0f);
			FindCtrl(base._transform, UI.BTN_CLOSE).localPosition = new Vector3(0f, btnHeight + num2, 0f);
			base._transform.localPosition = new Vector3(0f, (0f - num2) / 2f, 0f);
		}
	}

	private void SetPickUp()
	{
		if (lb.usePickUp != 0)
		{
			int i = arrayNow + 1;
			for (int count = lb.next.Count; i < count; i++)
			{
				if (lb.next[i].reward[0].isPickUp)
				{
					pickUpReward = lb.next[i].reward[0];
					return;
				}
			}
			for (int num = arrayNow; num >= 0; num--)
			{
				if (lb.next[num].reward[0].isPickUp)
				{
					pickUpReward = lb.next[num].reward[0];
					return;
				}
			}
		}
		if (lb.next.Count - 1 > arrayNow)
		{
			pickUpReward = lb.next[arrayNow + 1].reward[0];
		}
		else
		{
			pickUpReward = lb.next[arrayNow].reward[0];
		}
	}

	private void SetGetDialog()
	{
		PlayAudio(AUDIO.REQUEST_COMPLETE);
		StartCoroutine("WaitGetDialog");
	}

	private IEnumerator WaitGetDialog()
	{
		yield return new WaitForSeconds(0.8f);
		DispatchEvent("LIMITED_LOGIN_GET", lb);
	}

	public void OnQuery_SELECT()
	{
	}

	public void OnQuery_CLOSE()
	{
		if (null != glowModel_)
		{
			glowModel_.gameObject.SetActive(value: false);
		}
		if (lb.type == 6 && lb.isBeginner2Pop && !showedBLBP)
		{
			showedBLBP = true;
			GameSection.StopEvent();
			DispatchEvent("BEGINNER_LOGIN_BONUS_POP");
		}
		else
		{
			GameSection.BackSection();
		}
	}

	private uint GetItemModelID(REWARD_TYPE type, int itemID)
	{
		uint result = uint.MaxValue;
		switch (type)
		{
		case REWARD_TYPE.CRYSTAL:
			result = 1u;
			break;
		case REWARD_TYPE.MONEY:
			result = 2u;
			break;
		case REWARD_TYPE.ITEM:
			result = (uint)itemID;
			break;
		}
		return result;
	}

	private bool IsDispItem3D(int itemID)
	{
		switch (itemID)
		{
		case 1200000:
		case 7000100:
		case 7000101:
		case 7000200:
		case 7000201:
		case 7000300:
		case 7000301:
			return true;
		default:
			return false;
		}
	}

	private void SetDummyLogbo()
	{
		dummyLogBo = new LoginBonus();
		dummyLogBo.next = new List<LoginBonus.NextReward>
		{
			new LoginBonus.NextReward
			{
				count = 1,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 6,
						itemId = 302610650,
						itemNum = 10,
						isGet = true,
						isPickUp = false,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u))
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 2,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 5,
						itemId = 100100104,
						itemNum = 1,
						isGet = true,
						isPickUp = false,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 2)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 3,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 1,
						itemId = 1,
						itemNum = 5,
						isGet = false,
						isPickUp = false,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 5)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 4,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 1,
						itemId = 1,
						itemNum = 5,
						isGet = false,
						isPickUp = false,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 5)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 5,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 1,
						itemId = 1,
						itemNum = 5,
						isGet = false,
						isPickUp = true,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 5)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 6,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 4,
						itemId = 20260130,
						itemNum = 1,
						isGet = true,
						isPickUp = false,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 3)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 7,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 2,
						itemId = 1,
						itemNum = 1000000,
						isGet = false,
						isPickUp = false,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 5)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 8,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 5,
						itemId = 100100104,
						itemNum = 1,
						isGet = false,
						isPickUp = false,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 2)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 9,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 6,
						itemId = 302610650,
						itemNum = 10,
						isGet = false,
						isPickUp = false,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 6)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 10,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 3,
						itemId = 7000400,
						itemNum = 1,
						isGet = false,
						isPickUp = true,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 7)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 11,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 2,
						itemId = 1,
						itemNum = 100000,
						isGet = false,
						isPickUp = true,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 5)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 12,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 6,
						itemId = 302610650,
						itemNum = 10,
						isGet = false,
						isPickUp = false,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 6)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 13,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 4,
						itemId = 20060100,
						itemNum = 1,
						isGet = false,
						isPickUp = true,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 7)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 14,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 1,
						itemId = 1,
						itemNum = 5,
						isGet = false,
						isPickUp = false,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 5)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 15,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 6,
						itemId = 302610650,
						itemNum = 10,
						isGet = false,
						isPickUp = false,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 6)
					}
				}
			},
			new LoginBonus.NextReward
			{
				count = 16,
				reward = new List<LoginBonus.LoginBonusReward>
				{
					new LoginBonus.LoginBonusReward
					{
						name = "アイテム名1",
						type = 4,
						itemId = 20060100,
						itemNum = 1,
						isGet = false,
						isPickUp = true,
						pickUpText = "",
						day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 7)
					}
				}
			}
		};
		dummyLogBo.reward = new List<LoginBonus.LoginBonusReward>
		{
			new LoginBonus.LoginBonusReward
			{
				name = "リワ\u30fcド",
				type = 1,
				itemId = 1,
				itemNum = 1,
				isGet = true,
				isPickUp = false,
				pickUpText = "",
				day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8u), 4)
			}
		};
		dummyLogBo.name = "スペシャル";
		dummyLogBo.type = 1;
		dummyLogBo.total = 6;
		dummyLogBo.rotate = 0;
		dummyLogBo.nowCount = 6;
		dummyLogBo.priority = 1;
		dummyLogBo.period_announce = "期間指定の文字列";
		dummyLogBo.boardType = 2;
		dummyLogBo.loginBonusId = 1;
	}
}
