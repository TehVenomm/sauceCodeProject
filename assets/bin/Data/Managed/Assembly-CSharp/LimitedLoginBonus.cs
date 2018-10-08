using Network;
using System;
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

	private const int BEGINNER_LOGIN_BONUS_ID = 6;

	private static Color bgYellow = new Color(0.972549f, 0.5411765f, 0.149019614f);

	private static Color bgRed = new Color(0.34117648f, 0.7882353f, 0.972549f);

	private static Color bgBlue = new Color(0.972549f, 0.34117648f, 0.3529412f);

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
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		glowModel_ = Utility.Find(base._transform, "LIB_00000003");
		texModel_ = Utility.Find(base._transform, "TEX_MODEL");
		texModelRenderTexture_ = UIModelRenderTexture.Get(texModel_);
		texModelTexture_ = texModel_.GetComponent<UITexture>();
		texInnerModel_ = Utility.Find(base._transform, "TEX_INNER_MODEL");
		texInnerModelRenderTexture_ = UIModelRenderTexture.Get(texInnerModel_);
		texInnerModelTexture_ = texInnerModel_.GetComponent<UITexture>();
		info = SetPrefab(GetCtrl(UI.SPR_FRAME), "LimitedLoginBonusInfo", true);
		infoDetail = SetPrefab(GetCtrl(UI.SPR_FRAME), "LimitedLoginBonusInfoDetail", true);
		info.get_gameObject().SetActive(false);
		infoDetail.get_gameObject().SetActive(false);
		this.StartCoroutine(DoInitialize());
	}

	protected void OnQuery_RELEASE_ABILITY()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		if (!(infoDetail == null) && !(info == null))
		{
			if (isDetail)
			{
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_NAME, string.Empty);
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_DESC, string.Empty);
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_NUM, string.Empty);
				infoDetail.get_gameObject().SetActive(false);
			}
			else
			{
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_NAME, string.Empty);
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_DESC, string.Empty);
				SetLabelText(infoDetail, UI.LBL_INFODETAIL_NUM, string.Empty);
			}
			infoDetail.get_gameObject().SetActive(false);
			info.get_gameObject().SetActive(false);
			GameSection.StopEvent();
		}
	}

	protected void OnQuery_ABILITY_DATA_POPUP()
	{
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		object[] array = GameSection.GetEventData() as object[];
		int index = (int)array[0];
		string text = string.Empty;
		string text2 = null;
		string empty = string.Empty;
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
		empty = "x" + loginBonusReward.itemNum.ToString();
		Vector3 localPosition = default(Vector3);
		localPosition._002Ector(0f, 60f, 0f);
		if (text2 != null)
		{
			SetLabelText(infoDetail, UI.LBL_INFODETAIL_NAME, text);
			SetLabelText(infoDetail, UI.LBL_INFODETAIL_DESC, text2);
			SetLabelText(infoDetail, UI.LBL_INFODETAIL_NUM, empty);
			infoDetail.set_localPosition(localPosition);
			infoDetail.get_gameObject().SetActive(true);
			isDetail = true;
		}
		else
		{
			SetLabelText(info, UI.LBL_INFO_NAME, text);
			SetLabelText(info, UI.LBL_INFO_NUM, empty);
			info.set_localPosition(localPosition);
			info.get_gameObject().SetActive(true);
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
			Protocol.Send<LoginBonusConfirmModel.RequestSendForm, LoginBonusConfirmModel>(post_data: new LoginBonusConfirmModel.RequestSendForm
			{
				loginBonusId = (int)GameSection.GetEventData()
			}, url: LoginBonusConfirmModel.URL, call_back: (Action<LoginBonusConfirmModel>)delegate(LoginBonusConfirmModel ret)
			{
				if (ret.Error == Error.None)
				{
					if (ret != null && ret.result != null && ret.result.Count > 0)
					{
						((_003CDoInitialize_003Ec__Iterator9E)/*Error near IL_0071: stateMachine*/)._003C_003Ef__this.lb = ret.result[0];
					}
					((_003CDoInitialize_003Ec__Iterator9E)/*Error near IL_0071: stateMachine*/)._003Cconnect_003E__0 = true;
				}
			}, get_param: string.Empty);
			while (!connect)
			{
				yield return (object)null;
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
				yield return (object)null;
			}
			GameSection.BackSection();
		}
		else
		{
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
			int logbo_column_num = 1 + (lb.next.Count - 1) / 5;
			int logbo_now_column = 1 + arrayNow / 5;
			if (logbo_column_num > 3)
			{
				if (logbo_now_column > logbo_column_num - 2)
				{
					logbo_now_column = logbo_column_num - 2;
				}
				startScrPos = scrollStartHeight + iconHeight * (float)(logbo_now_column - 1);
				isFirst = true;
			}
			else
			{
				isFirst = false;
			}
			SetPickUp();
			float rotateSpeed = 35f;
			if (pickUpReward.type == 14)
			{
				SetRenderAccessoryModel((Enum)UI.TEX_MODEL, (uint)pickUpReward.itemId, pickUpReward.GetScale(), true, false);
				isModel = true;
			}
			else if (pickUpReward.type == 5)
			{
				uint modelID3 = (uint)pickUpReward.itemId;
				texModelRenderTexture_.InitSkillItem(texModelTexture_, modelID3, true, false, 45f);
				texInnerModelRenderTexture_.InitSkillItemSymbol(texInnerModelTexture_, modelID3, true, 17f);
				isModel = true;
			}
			else if (pickUpReward.type == 4)
			{
				SetRenderEquipModel((Enum)UI.TEX_MODEL, (uint)pickUpReward.itemId, -1, -1, pickUpReward.GetScale());
				isModel = true;
			}
			else if (pickUpReward.type == 1 || pickUpReward.type == 2)
			{
				uint modelID = GetItemModelID((REWARD_TYPE)pickUpReward.type, pickUpReward.itemId);
				texModelRenderTexture_.InitItem(texModelTexture_, modelID, true);
				isModel = true;
			}
			else if (pickUpReward.type == 3 && IsDispItem3D(pickUpReward.itemId))
			{
				uint modelID2 = GetItemModelID((REWARD_TYPE)pickUpReward.type, pickUpReward.itemId);
				texModelRenderTexture_.InitItem(texModelTexture_, modelID2, true);
				isModel = true;
			}
			texModelRenderTexture_.SetRotateSpeed(rotateSpeed);
			texInnerModelRenderTexture_.SetRotateSpeed(rotateSpeed);
			LoadingQueue loadingQueue = new LoadingQueue(this);
			string topImgName = ResourceName.GetLoginBonusTopImage(lb.loginBonusId);
			topImageLoadObj = loadingQueue.Load(RESOURCE_CATEGORY.LOGINBONUS_IMAGE, topImgName, false);
			if (loadingQueue.IsLoading())
			{
				yield return (object)loadingQueue.Wait();
			}
			base.Initialize();
		}
	}

	public unsafe override void UpdateUI()
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		if (lb == null)
		{
			SetLabelText((Enum)UI.LBL_PICKUP, string.Empty);
			SetLabelText((Enum)UI.LBL_PERIOD, string.Empty);
		}
		else
		{
			if (topImageLoadObj != null)
			{
				Texture2D val = null;
				val = (topImageLoadObj.loadedObject as Texture2D);
				if (val != null)
				{
					Transform t = FindCtrl(base._transform, UI.TEX_LOGIN_BANNER);
					SetActive(t, true);
					SetTexture(t, val);
				}
			}
			if (!isModel)
			{
				FindCtrl(base._transform, UI.OBJ_DETAIL_ROOT).set_localPosition(new Vector3(pickUpPosX, pickUpItemPosY, 0f));
				LoginBonus.LoginBonusReward loginBonusReward = pickUpReward;
				ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)loginBonusReward.type, (uint)loginBonusReward.itemId, Utility.Find(base._transform, "OBJ_DETAIL_ROOT"), -1, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
				itemIcon.transform.set_localScale(new Vector3(1.5f, 1.5f, 1.5f));
			}
			SetLabelText((Enum)UI.LBL_PERIOD, lb.period_announce);
			SetLabelText((Enum)UI.LBL_LOGIN_DAYS, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 7u), lb.nowCount.ToString()));
			FindCtrl(base._transform, UI.LBL_PICKUP).GetComponent<UILabel>().supportEncoding = true;
			SetLabelText((Enum)UI.LBL_PICKUP, pickUpReward.pickUpText);
			int count = lb.next.Count;
			SetFrame(1 + (lb.next.Count - 1) / 5, lb.boardType);
			touchAndReleaseList.Clear();
			SetGrid(UI.GRD_BONUSLIST, "LimitedLoginBonusItem", count, false, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			if (isFirst)
			{
				Transform ctrl = GetCtrl(UI.SCR_BONUSLIST);
				UIPanel component = ctrl.GetComponent<UIPanel>();
				ctrl.get_transform().set_localPosition(new Vector3(0f, startScrPos, 0f));
				component.clipOffset = new Vector2(0f, 0f - startScrPos);
			}
		}
	}

	private void SetFrame(int column_num, int board_type)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
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
			FindCtrl(base._transform, UI.SPR_FRAME_BG_4_L).set_localPosition(new Vector3(0f, bg4Height + num2, 0f));
			FindCtrl(base._transform, UI.SPR_FRAME_BG_4_R).set_localPosition(new Vector3(0f, bg4Height + num2, 0f));
			FindCtrl(base._transform, UI.SPR_FRAME_FOOTER_L).set_localPosition(new Vector3(0f, footerHeight + num2, 0f));
			FindCtrl(base._transform, UI.SPR_FRAME_FOOTER_R).set_localPosition(new Vector3(0f, footerHeight + num2, 0f));
			FindCtrl(base._transform, UI.BTN_CLOSE).set_localPosition(new Vector3(0f, btnHeight + num2, 0f));
			base._transform.set_localPosition(new Vector3(0f, (0f - num2) / 2f, 0f));
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		PlayAudio(AUDIO.REQUEST_COMPLETE, 1f, false);
		this.StartCoroutine("WaitGetDialog");
	}

	private IEnumerator WaitGetDialog()
	{
		yield return (object)new WaitForSeconds(0.8f);
		DispatchEvent("LIMITED_LOGIN_GET", lb);
	}

	public void OnQuery_SELECT()
	{
	}

	public void OnQuery_CLOSE()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (null != glowModel_)
		{
			glowModel_.get_gameObject().SetActive(false);
		}
		if (lb.type == 6 && lb.isBeginner2Pop && !showedBLBP)
		{
			showedBLBP = true;
			GameSection.StopEvent();
			DispatchEvent("BEGINNER_LOGIN_BONUS_POP", null);
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
		if (itemID == 7000100 || itemID == 7000101 || itemID == 7000200 || itemID == 7000201 || itemID == 7000300 || itemID == 7000301 || itemID == 1200000)
		{
			return true;
		}
		return false;
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
						pickUpText = string.Empty,
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
				pickUpText = string.Empty,
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
