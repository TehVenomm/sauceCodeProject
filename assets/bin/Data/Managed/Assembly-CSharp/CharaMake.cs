using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMake : GameSection
{
	private enum UI
	{
		CharaLine,
		SPR_FRAME,
		SPR_PAGE_CURSOR,
		OBJ_PAGE_BUTTONS,
		GRD_PAGES,
		BTN_PREV,
		BTN_NEXT,
		BTN_NEXT_CENTER,
		BTN_DISABLE,
		TEX_MODEL,
		SPR_ON,
		SPR_OFF,
		OBJ_GENDERS_ON,
		OBJ_GENDERS_OFF,
		STR_CHANGEABLE_STATUS1,
		OBJ_LIST_FACETYPE,
		OBJ_SKIN_COLORS_ON,
		OBJ_SKIN_COLORS_OFF,
		SCR_SKIN_COLOR_LIST,
		GRD_SKIN_COLOR_LIST,
		OBJ_LIST_HAIRSTYLE,
		OBJ_HAIR_COLORS_ON,
		OBJ_HAIR_COLORS_OFF,
		SCR_HAIR_COLOR_LIST,
		GRD_HAIR_COLOR_LIST,
		OBJ_VOICES_ON,
		OBJ_VOICES_OFF,
		LBL_VOICE_NAME,
		IPT_NAME,
		TGL_INPUT_LIMITER,
		LBL_LIMIT_TIME_TEXT,
		LBL_CONFIRM_NAME,
		BTN_NO,
		BTN_YES,
		BTN_YES_OFF,
		STR_CHANGEABLE_STATUS2,
		TERMS_OF_SERVICE,
		SPR_CHECK,
		SPR_CHECK_OFF,
		GRD_LIST,
		BTN_LIST_PREV,
		BTN_LIST_NEXT,
		LBL_LISTITEM,
		OBJ_INPUT_NAME_GG,
		IPT_NAME_GG,
		SelectGender
	}

	private enum PAGE
	{
		SEX,
		FACE,
		HAIR,
		VOICE,
		NAME,
		CONFIRM,
		MAX
	}

	private enum LIST
	{
		FACETYPE,
		HAIRSTYLE,
		MAX
	}

	private class ListInfo
	{
		public Transform tansform;

		public int index;

		public int max;
	}

	private enum EDIT_TYPE
	{
		All,
		Name,
		Appearance
	}

	public const int STRING_TABLE_FACE_TYPE_NAME_ID_TOP = 10000;

	public const int STRING_TABLE_HAIR_STYLE_NAME_ID_TOP = 20000;

	public const int STRING_TABLE_VOICE_NAME_ID_TOP = 30000;

	public const int STRING_TABLE_PRESET_PLAYER_NAME_ID_TOP = 40000;

	public const int STRING_TABLE_SEX_OFFSET_ID = 1000;

	private const int MAX_SHOW_COLOR_ITEM_COUNT = 12;

	private Vector3 mainCameraPos;

	private Vector3 zoomCameraPos;

	private Vector3 playerPos;

	private float initPlayerRot;

	private float playerRot;

	private ListInfo[] lists = new ListInfo[2];

	private int sexID;

	private int skinColorID = 1;

	private int hairColorID;

	private int voiceTypeID;

	private PAGE page = PAGE.MAX;

	private GlobalSettingsManager.PlayerVisual playerVisual;

	private PlayerLoader playerLoader;

	private Transform shadow;

	private Vector3Interpolator cameraAnim = new Vector3Interpolator();

	private QuaternionInterpolator playerRotAnim = new QuaternionInterpolator();

	private LoadObject[][] voices;

	private AudioObject voiceAudioObject;

	private UINameInput inputName;

	private GameObject colorListItem;

	private UIScrollView skinColorScroll;

	private UIScrollView hairColorScroll;

	private bool nonFirstCharaMake;

	private string defaultUserName = string.Empty;

	private bool isTermsEnable;

	private EDIT_TYPE editType;

	private static readonly int[] VOICE_ID_CANDIDATE = new int[5]
	{
		1,
		94,
		2,
		4,
		14
	};

	public override string overrideBackKeyEvent
	{
		get
		{
			if (page == PAGE.SEX && !nonFirstCharaMake)
			{
				return "__NONE";
			}
			return "PAGE_PREV";
		}
	}

	public static void GetCameraPosRot(out Vector3 pos, out Vector3 rot, bool is_status_scene)
	{
		GetCameraPosRot(out pos, out Vector3 _, out rot, is_status_scene);
	}

	public static void GetCameraPosRot(out Vector3 cam_pos, out Vector3 cam_zoom_pos, out Vector3 cam_rot, bool is_non_title_scene)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		cam_pos = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.mainCameraPos;
		cam_rot = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.mainCameraRot;
		cam_zoom_pos = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.zoomCameraPos;
		if (is_non_title_scene)
		{
			float num = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerRot;
			float num2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.playerRot;
			float num3 = num2 - num;
			Vector3 val = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.playerPos;
			Vector3 val2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerPos;
			Quaternion val3 = Quaternion.AngleAxis(num3, Vector3.get_up());
			cam_pos = val3 * (cam_pos - val2) + val;
			cam_zoom_pos = val3 * (cam_zoom_pos - val2) + val;
			Quaternion val4 = Quaternion.AngleAxis(num3, Vector3.get_up()) * Quaternion.Euler(cam_rot);
			cam_rot = val4.get_eulerAngles();
		}
	}

	private void OnEnable()
	{
		InputManager.OnDrag = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDrag, new InputManager.OnTouchDelegate(OnDrag));
	}

	private void OnDisable()
	{
		InputManager.OnDrag = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDrag, new InputManager.OnTouchDelegate(OnDrag));
	}

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private void GetEditType(object[] event_data)
	{
		editType = EDIT_TYPE.All;
		if (event_data != null && event_data.Length > 2)
		{
			editType = (EDIT_TYPE)(int)event_data[2];
		}
	}

	private unsafe IEnumerator DoInitialize()
	{
		if (MonoBehaviourSingleton<PredownloadManager>.IsValid())
		{
			while (MonoBehaviourSingleton<PredownloadManager>.I.isLoading)
			{
				yield return (object)null;
			}
			Object.Destroy(MonoBehaviourSingleton<PredownloadManager>.I);
		}
		int m = 0;
		for (int l = lists.Length; m < l; m++)
		{
			lists[m] = new ListInfo();
		}
		object[] event_data = GameSection.GetEventData() as object[];
		if (event_data != null)
		{
			nonFirstCharaMake = true;
			isTermsEnable = true;
			UserInfo _user_info = event_data[0] as UserInfo;
			UserStatus _user_status = event_data[1] as UserStatus;
			GetEditType(event_data);
			sexID = _user_status.sex;
			lists[0].index = FaceTypeToIndex(_user_status.faceId);
			skinColorID = _user_status.skinId;
			lists[1].index = HeadToIndex(_user_status.hairId);
			hairColorID = _user_status.hairColorId;
			voiceTypeID = _user_status.voiceId;
			defaultUserName = _user_info.name;
		}
		GetCameraPosRot(out mainCameraPos, out zoomCameraPos, out Vector3 _, nonFirstCharaMake);
		if (!nonFirstCharaMake)
		{
			playerPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerPos;
			playerRot = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerRot;
		}
		else
		{
			playerPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.playerPos;
			playerRot = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.playerRot;
		}
		initPlayerRot = playerRot;
		if (MonoBehaviourSingleton<StageManager>.I.stageObject != null && shadow == null)
		{
			shadow = PlayerLoader.CreateShadow(MonoBehaviourSingleton<StageManager>.I.stageObject, false, -1, false);
			if (shadow != null)
			{
				shadow.set_position(playerPos + new Vector3(0f, 0.005f, 0f));
			}
		}
		LoadingQueue load_queue = new LoadingQueue(this);
		ResourceManager.enableCache = false;
		int voice_type_count = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount;
		voices = new LoadObject[2][];
		for (int k = 0; k < 2; k++)
		{
			voices[k] = new LoadObject[voice_type_count];
			for (int j = 0; j < voice_type_count; j++)
			{
				int VOICE_NUM = VOICE_ID_CANDIDATE.Length;
				string[] names = new string[VOICE_NUM];
				for (int i = 0; i < VOICE_NUM; i++)
				{
					names[i] = ResourceName.GetActionVoiceName(k, j, VOICE_ID_CANDIDATE[i]);
				}
				voices[k][j] = load_queue.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageName(k, j), names, false);
			}
		}
		ResourceManager.enableCache = true;
		LoadObject lo_color_list_item = load_queue.Load(RESOURCE_CATEGORY.UI, "CharaMakeColorListItem", false);
		yield return (object)load_queue.Wait();
		GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
		colorListItem = (lo_color_list_item.loadedObject as GameObject);
		SetGrid(item_num: hasVisuals.hasSkinColorIndexes.Length, create_item_func: new Func<int, Transform, Transform>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), grid_ctrl_enum: UI.GRD_SKIN_COLOR_LIST, item_prefab_name: null, reset: true, item_init_func: new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		SelectSkinColor(skinColorID);
		skinColorScroll = GetCtrl(UI.SCR_SKIN_COLOR_LIST).GetComponent<UIScrollView>();
		if (IsNeedScrollSkinColor())
		{
			UIGrid grid2 = GetCtrl(UI.GRD_SKIN_COLOR_LIST).GetComponent<UIGrid>();
			grid2.pivot = UIWidget.Pivot.Left;
		}
		SetGrid(item_num: hasVisuals.hasHairColorIndexes.Length, create_item_func: new Func<int, Transform, Transform>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), grid_ctrl_enum: UI.GRD_HAIR_COLOR_LIST, item_prefab_name: null, reset: true, item_init_func: new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		SelectHairColor(hairColorID);
		hairColorScroll = GetCtrl(UI.SCR_HAIR_COLOR_LIST).GetComponent<UIScrollView>();
		if (IsNeedScrollHairColor())
		{
			UIGrid grid = GetCtrl(UI.GRD_HAIR_COLOR_LIST).GetComponent<UIGrid>();
			grid.pivot = UIWidget.Pivot.Left;
		}
		LoadModel();
		while ((playerLoader != null && playerLoader.isLoading) || load_queue.IsLoading())
		{
			yield return (object)null;
		}
		cameraAnim.endValue = mainCameraPos;
		MovePage(0);
		if (!TutorialStep.HasAllTutorialCompleted() && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep < 1)
		{
			bool hasSentTutorialStep = false;
			MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep(delegate
			{
				((_003CDoInitialize_003Ec__Iterator16B)/*Error near IL_0728: stateMachine*/)._003ChasSentTutorialStep_003E__19 = true;
			});
			while (!hasSentTutorialStep)
			{
				yield return (object)null;
			}
		}
		ResetLayout();
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 1)
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_charactercreate_start, "Tutorial");
		}
		base.Initialize();
	}

	private void ResetLayout()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		if (editType == EDIT_TYPE.Name)
		{
			SetActive((Enum)UI.OBJ_INPUT_NAME_GG, false);
			GetCtrl(UI.OBJ_PAGE_BUTTONS).get_gameObject().SetActive(false);
			GetCtrl(UI.SPR_PAGE_CURSOR).get_gameObject().SetActive(false);
			GetCtrl(UI.CharaLine).get_gameObject().SetActive(false);
			MovePage(4);
		}
		else if (editType == EDIT_TYPE.Appearance)
		{
			SetActive((Enum)UI.OBJ_INPUT_NAME_GG, false);
			Transform ctrl = GetCtrl(UI.SelectGender);
			Vector3 localPosition = ctrl.get_localPosition();
			float x = localPosition.x;
			Vector3 localPosition2 = ctrl.get_localPosition();
			float num = localPosition2.y - 90f;
			Vector3 localPosition3 = ctrl.get_localPosition();
			Vector3 localPosition4 = default(Vector3);
			localPosition4._002Ector(x, num, localPosition3.z);
			ctrl.set_localPosition(localPosition4);
			Transform ctrl2 = GetCtrl(UI.OBJ_GENDERS_ON);
			Vector3 localPosition5 = ctrl2.get_localPosition();
			float x2 = localPosition5.x;
			Vector3 localPosition6 = ctrl2.get_localPosition();
			float num2 = localPosition6.y - 90f;
			Vector3 localPosition7 = ctrl2.get_localPosition();
			Vector3 localPosition8 = default(Vector3);
			localPosition8._002Ector(x2, num2, localPosition7.z);
			ctrl2.set_localPosition(localPosition8);
			Transform ctrl3 = GetCtrl(UI.OBJ_GENDERS_OFF);
			Vector3 localPosition9 = ctrl3.get_localPosition();
			float x3 = localPosition9.x;
			Vector3 localPosition10 = ctrl3.get_localPosition();
			float num3 = localPosition10.y - 90f;
			Vector3 localPosition11 = ctrl3.get_localPosition();
			Vector3 localPosition12 = default(Vector3);
			localPosition12._002Ector(x3, num3, localPosition11.z);
			ctrl3.set_localPosition(localPosition12);
		}
	}

	protected override void OnDestroy()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		base.OnDestroy();
		if (shadow != null)
		{
			Object.DestroyImmediate(shadow.get_gameObject());
			shadow = null;
		}
	}

	public override void UpdateUI()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		if (nonFirstCharaMake)
		{
			SetSprite((Enum)UI.SPR_FRAME, "CharacterEdit");
		}
		Transform ctrl = GetCtrl(UI.OBJ_PAGE_BUTTONS);
		int i = 0;
		for (int childCount = ctrl.get_childCount(); i < childCount; i++)
		{
			Transform val = ctrl.GetChild(i);
			SetEvent(val, "MOVE_PAGE", i);
			SetActive(val, UI.SPR_ON, i == (int)page);
			SetActive(val, UI.SPR_OFF, i != (int)page);
		}
		SetCellWidth(UI.GRD_PAGES, MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth, true);
		SetCenter((Enum)UI.GRD_PAGES, (int)page, false);
		UpdateLists();
		SetToggleGroup(UI.OBJ_GENDERS_ON, UI.OBJ_GENDERS_OFF, sexID, "SEX");
		SetToggleGroup(UI.OBJ_SKIN_COLORS_ON, UI.OBJ_SKIN_COLORS_OFF, skinColorID, "SKIN_COLOR");
		SetToggleGroup(UI.OBJ_HAIR_COLORS_ON, UI.OBJ_HAIR_COLORS_OFF, hairColorID, "HAIR_COLOR");
		SetToggleGroup(UI.OBJ_VOICES_ON, UI.OBJ_VOICES_OFF, voiceTypeID, "VOICE");
		UpdateVoiceNames();
		if (editType == EDIT_TYPE.Name)
		{
			SetInput((Enum)UI.IPT_NAME, base.sectionData.GetText("DEFAULT_NAME_TEXT"), 12, (EventDelegate.Callback)OnChangeName);
			inputName = base.GetComponent<UINameInput>((Enum)UI.IPT_NAME);
		}
		else
		{
			SetInput((Enum)UI.IPT_NAME_GG, base.sectionData.GetText("DEFAULT_NAME_TEXT"), 12, (EventDelegate.Callback)OnChangeName);
			inputName = base.GetComponent<UINameInput>((Enum)UI.IPT_NAME_GG);
		}
		inputName.CreateCaret(true);
		bool value = false;
		if (!string.IsNullOrEmpty(defaultUserName))
		{
			inputName.SetName(defaultUserName);
			OnChangeName();
			if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
			{
				DateTime now = TimeManager.GetNow();
				if (DateTime.TryParse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.editNameAt.date, out DateTime result) && result.CompareTo(now) > 0)
				{
					value = true;
					inputName.SetInActiveNameColor();
					inputName.set_enabled(false);
					BoxCollider component = inputName.GetComponent<BoxCollider>();
					if (component != null)
					{
						component.set_enabled(false);
					}
					SetLabelText((Enum)UI.LBL_LIMIT_TIME_TEXT, string.Format(base.sectionData.GetText("LIMIT_INPUT_TEXT"), MonoBehaviourSingleton<UserInfoManager>.I.userInfo.editNameAt.date));
				}
			}
			defaultUserName = string.Empty;
		}
		else if (!TutorialStep.HasAllTutorialCompleted())
		{
			inputName.SetName("Colopl");
			OnChangeName();
		}
		SetToggle((Enum)UI.TGL_INPUT_LIMITER, value);
		SetLabelText((Enum)UI.STR_CHANGEABLE_STATUS2, base.sectionData.GetText("STR_CHANGEABLE_STATUS"));
		SetActive((Enum)UI.TERMS_OF_SERVICE, !nonFirstCharaMake);
		SetActive((Enum)UI.SPR_CHECK, isTermsEnable);
		SetActive((Enum)UI.SPR_CHECK_OFF, !isTermsEnable);
		LoadModel();
	}

	private void LateUpdate()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (cameraAnim.IsPlaying())
		{
			MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.set_position(cameraAnim.Update());
		}
		if (playerRotAnim.IsPlaying() && playerLoader != null)
		{
			playerLoader.get_transform().set_rotation(playerRotAnim.Update());
		}
	}

	private void SetSpriteColors(UI ctrl, Color[] colors)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl2 = GetCtrl(ctrl);
		if (!(ctrl2 == null))
		{
			int i = 0;
			for (int childCount = ctrl2.get_childCount(); i < childCount; i++)
			{
				Color val = colors[i];
				Transform t = ctrl2.GetChild(i);
				SetColor(ctrl2.GetChild(i), val);
				UIButton component = base.GetComponent<UIButton>(t);
				if (component != null)
				{
					component.hover = val;
					component.pressed = val;
					component.disabledColor = val;
				}
			}
		}
	}

	private unsafe void SetList(LIST list, UI ui, int item_num, Action<int, Transform> cb)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		ListInfo listInfo = lists[(int)list];
		Transform ctrl = GetCtrl(ui);
		if (ctrl != null)
		{
			Transform val = ctrl.Find("CharaMakeList");
			if (val != null)
			{
				val.get_gameObject().set_name("CharaMakeList_Destroy");
				Object.Destroy(val.get_gameObject());
			}
		}
		Transform val2 = SetPrefab((Enum)ui, "CharaMakeList");
		SetEvent(val2, UI.BTN_LIST_PREV, "LIST_PREV", (int)ui);
		SetEvent(val2, UI.BTN_LIST_NEXT, "LIST_NEXT", (int)ui);
		_003CSetList_003Ec__AnonStorey475 _003CSetList_003Ec__AnonStorey;
		SetGrid(val2, UI.GRD_LIST, "CharaMakeListItem", item_num, false, new Action<int, Transform, bool>((object)_003CSetList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		SetCenterOnChildFunc(val2, UI.GRD_LIST, OnCenterListItem);
		SetCenter(val2, UI.GRD_LIST, listInfo.index, false);
		listInfo.tansform = val2.get_parent();
		listInfo.max = item_num;
	}

	private unsafe void UpdateLists()
	{
		GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
		int item_num = (!IsWoman()) ? hasVisuals.hasManFaceIndexes.Length : hasVisuals.hasWomanFaceIndexes.Length;
		_003CUpdateLists_003Ec__AnonStorey476 _003CUpdateLists_003Ec__AnonStorey;
		SetList(LIST.FACETYPE, UI.OBJ_LIST_FACETYPE, item_num, new Action<int, Transform>((object)_003CUpdateLists_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		int item_num2 = (!IsWoman()) ? hasVisuals.hasManHeadIndexes.Length : hasVisuals.hasWomanHeadIndexes.Length;
		SetList(LIST.HAIRSTYLE, UI.OBJ_LIST_HAIRSTYLE, item_num2, new Action<int, Transform>((object)_003CUpdateLists_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void UpdateVoiceNames()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		Transform ctrl = GetCtrl(UI.OBJ_VOICES_ON);
		Transform ctrl2 = GetCtrl(UI.OBJ_VOICES_OFF);
		if (!(ctrl == null) && !(ctrl2 == null))
		{
			int i = 0;
			for (int childCount = ctrl.get_childCount(); i < childCount; i++)
			{
				string text = StringTable.Get(STRING_CATEGORY.CHARA_MAKE, (uint)(30000 + 1000 * sexID + i));
				Transform root = ctrl.GetChild(i);
				SetLabelText(root, UI.LBL_VOICE_NAME, text);
				Transform root2 = ctrl2.GetChild(i);
				SetLabelText(root2, UI.LBL_VOICE_NAME, text);
			}
		}
	}

	private void SetToggleGroup(UI ui_on, UI ui_off, int value, string event_name = null)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(ui_on);
		Transform ctrl2 = GetCtrl(ui_off);
		if (!(ctrl == null) && !(ctrl2 == null))
		{
			int i = 0;
			for (int childCount = ctrl.get_childCount(); i < childCount; i++)
			{
				ctrl.GetChild(i).get_gameObject().SetActive(i == value);
				Transform val = ctrl2.GetChild(i);
				if (event_name != null)
				{
					SetEvent(val, event_name, i);
				}
				val.get_gameObject().SetActive(i != value);
			}
		}
	}

	private void LoadModel()
	{
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		DeleteRenderTexture((Enum)UI.TEX_MODEL);
		int sex = sexID;
		int face_type_id = IndexToFaceType(lists[0].index);
		int skin_color_id = skinColorID;
		int hair_style_id = IndexToHead(lists[1].index);
		int num = hairColorID;
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		playerLoadInfo.SetFace(sex, face_type_id, skin_color_id);
		playerLoadInfo.SetHair(sex, hair_style_id, num);
		playerLoadInfo.SetEquipBody(sex, (uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerBodyEquipItemID);
		playerLoadInfo.SetEquipHead(sex, (uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerHeadEquipItemID);
		playerLoadInfo.SetEquipArm(sex, (uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerArmEquipItemID);
		playerLoadInfo.SetEquipLeg(sex, (uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerLegEquipItemID);
		int num2 = -1;
		if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
		{
			num2 = ((!MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.isChangeHairShader) ? (-1) : num);
		}
		InitRenderTexture(UI.TEX_MODEL, 0f, true);
		playerLoader = GetRenderTextureModelTransform(UI.TEX_MODEL).get_gameObject().AddComponent<PlayerLoader>();
		PlayerLoader obj = playerLoader;
		int use_hair_overlay = num2;
		obj.StartLoad(playerLoadInfo, playerLoader.get_gameObject().get_layer(), 98, false, false, false, !nonFirstCharaMake, false, false, false, true, SHADER_TYPE.NORMAL, delegate
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			if (!(playerLoader.animator == null))
			{
				playerLoader.get_transform().set_position(playerPos);
				playerLoader.get_transform().set_eulerAngles(new Vector3(0f, playerRot, 0f));
				PlayerAnimCtrl.Get(playerLoader.animator, (sex != 0) ? PLCA.IDLE_01_F : PLCA.IDLE_01, null, null, null);
				EnableRenderTexture(UI.TEX_MODEL);
			}
		}, true, use_hair_overlay);
	}

	private void OnDrag(InputManager.TouchInfo touch_info)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		if (!(playerLoader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && page != PAGE.CONFIRM)
		{
			playerLoader.get_transform().Rotate(GameDefine.GetCharaRotateVector(touch_info));
			Vector3 eulerAngles = playerLoader.get_transform().get_eulerAngles();
			playerRot = eulerAngles.y;
		}
	}

	private void OnCenterListItem(GameObject go)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		int n = int.Parse(go.get_name());
		Transform t = go.get_transform().get_parent().get_parent()
			.get_parent()
			.get_parent();
		ListInfo listInfo = Array.Find(lists, (ListInfo o) => o.tansform == t);
		ChangeValue(ref listInfo.index, n);
	}

	private void ChangeValue(ref int v, int n)
	{
		if (v != n)
		{
			v = n;
			if (page == PAGE.VOICE)
			{
				PlayDefaultVoice();
			}
			else
			{
				LoadModel();
			}
		}
	}

	private void OnChangeName()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		string inputNameGG = GetInputNameGG();
		inputNameGG = inputNameGG.Replace(" ", string.Empty);
		inputNameGG = inputNameGG.Replace("\u3000", string.Empty);
		if (inputNameGG.Length == 0)
		{
			if (inputName != null)
			{
				inputName.InActiveName();
			}
			SetColor((Enum)UI.LBL_CONFIRM_NAME, Color.get_red());
			SetLabelText((Enum)UI.LBL_CONFIRM_NAME, base.sectionData.GetText("NO_NAME"));
			SetActive((Enum)UI.BTN_YES, false);
			SetActive((Enum)UI.BTN_YES_OFF, true);
		}
		else
		{
			if (inputName != null)
			{
				inputName.ActiveName();
				inputName.SetName(inputNameGG);
			}
			SetColor((Enum)UI.LBL_CONFIRM_NAME, Color.get_white());
			SetLabelText((Enum)UI.LBL_CONFIRM_NAME, inputNameGG);
			if (isTermsEnable)
			{
				SetActive((Enum)UI.BTN_YES, true);
				SetActive((Enum)UI.BTN_YES_OFF, false);
			}
			else
			{
				SetActive((Enum)UI.BTN_YES, false);
				SetActive((Enum)UI.BTN_YES_OFF, true);
			}
		}
	}

	private void OnQuery_LIST_PREV()
	{
		AddValue((UI)(int)GameSection.GetEventData(), -1);
	}

	private void OnQuery_LIST_NEXT()
	{
		AddValue((UI)(int)GameSection.GetEventData(), 1);
	}

	private void AddValue(UI ui, int add)
	{
		Transform t = GetCtrl(ui);
		ListInfo listInfo = Array.Find(lists, (ListInfo o) => o.tansform == t);
		int num = listInfo.index + add;
		if (num < 0)
		{
			num = listInfo.max - 1;
		}
		else if (num >= listInfo.max)
		{
			num = 0;
		}
		if (listInfo.index != num)
		{
			SetCenter(listInfo.tansform, UI.GRD_LIST, num, false);
		}
	}

	private void OnQuery_CONFIRM()
	{
		string inputNameGG = GetInputNameGG();
		if (inputNameGG.Length == 0)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your character name.", null, null, null, null), delegate
			{
			}, false, 0);
		}
		else if (!isTermsEnable)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please accept the term", null, null, null, null), delegate
			{
			}, false, 0);
		}
		else
		{
			OnQuery_OK();
		}
	}

	private void OnQuery_CANCEL()
	{
		GameSection.BackSection();
	}

	private void OnQuery_MOVE_PAGE()
	{
		int num = (int)GameSection.GetEventData();
		if (page == PAGE.SEX)
		{
			CheckUniqueName(num);
		}
		else
		{
			MovePage(num);
		}
	}

	private void OnQuery_PAGE_PREV()
	{
		if (page == PAGE.SEX || editType == EDIT_TYPE.Name)
		{
			if (nonFirstCharaMake)
			{
				BackBeforeSceneOrStatus();
			}
			else
			{
				GameSection.BackSection();
			}
		}
		else
		{
			AddPage(-1);
		}
	}

	private void OnQuery_PAGE_NEXT()
	{
		EDIT_TYPE eDIT_TYPE = editType;
		if (eDIT_TYPE == EDIT_TYPE.Name)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name.Equals(GetInputNameGG()))
			{
				BackBeforeSceneOrStatus();
			}
			else if (GameSection.CheckCrystal(MonoBehaviourSingleton<UserInfoManager>.I.crystalChangeName, 0, true))
			{
				GameSection.ChangeEvent("SPEND_CRYSTAL_CONFIRM", new object[1]
				{
					MonoBehaviourSingleton<UserInfoManager>.I.crystalChangeName
				});
			}
		}
		else
		{
			AddPage(1);
		}
	}

	private void OnQuery_SpendCrystalToChangeNameConfirm_YES()
	{
		GameSection.StayEvent();
		SendEditFigure(delegate(bool is_success)
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			GameSection.ResumeEvent(false, null);
			if (is_success)
			{
				RequestEvent("BACK_TO_HOME", null);
				GameSection.BackSection();
			}
			else
			{
				this.StartCoroutine_Auto(IECloseDialog());
			}
		});
	}

	private IEnumerator IECloseDialog()
	{
		yield return (object)new WaitForEndOfFrame();
		GameSection.BackSection();
	}

	private void AddPage(int add)
	{
		int num = (int)(page + add);
		if (num < 0)
		{
			num = 0;
		}
		else if (num >= 6)
		{
			num = 5;
		}
		if (num == 5 || page == PAGE.SEX)
		{
			CheckUniqueName(num);
		}
		else
		{
			if (editType == EDIT_TYPE.Appearance && num == 4)
			{
				num += add;
			}
			MovePage(num);
		}
	}

	private void MovePage(int n)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (page != (PAGE)n)
		{
			this.StartCoroutine(WaitForTrack(n));
		}
	}

	private IEnumerator WaitForTrack(int n)
	{
		yield return (object)null;
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 1)
		{
			switch (page)
			{
			case PAGE.FACE:
				MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_charactercreate_face, "Tutorial");
				break;
			case PAGE.HAIR:
				MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_charactercreate_hair, "Tutorial");
				break;
			case PAGE.VOICE:
				MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_charactercreate_voice, "Tutorial");
				break;
			case PAGE.NAME:
				MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_charactercreate_name, "Tutorial");
				break;
			}
		}
		yield return (object)null;
		yield return (object)null;
		MovePageOld(n);
	}

	private void MovePageOld(int n)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		if (page != (PAGE)n)
		{
			PAGE pAGE = page;
			page = (PAGE)n;
			SetCenter((Enum)UI.GRD_PAGES, n, false);
			Vector3 val;
			switch (page)
			{
			default:
				val = mainCameraPos;
				break;
			case PAGE.FACE:
			case PAGE.HAIR:
			case PAGE.VOICE:
				val = zoomCameraPos;
				break;
			}
			if (cameraAnim.endValue != val)
			{
				cameraAnim.Set(0.5f, MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.get_position(), val, null, default(Vector3), null);
				cameraAnim.Play();
			}
			Transform val2 = GetCtrl(UI.OBJ_PAGE_BUTTONS).GetChild(n);
			GetCtrl(UI.SPR_PAGE_CURSOR).set_position(val2.get_position());
			if (pAGE != PAGE.MAX)
			{
				Transform root = GetCtrl(UI.OBJ_PAGE_BUTTONS).GetChild((int)pAGE);
				SetActive(root, UI.SPR_ON, false);
				SetActive(root, UI.SPR_OFF, true);
			}
			SetActive(val2, UI.SPR_ON, true);
			SetActive(val2, UI.SPR_OFF, false);
			switch (page)
			{
			case PAGE.FACE:
				skinColorScroll.set_enabled(IsNeedScrollSkinColor());
				hairColorScroll.set_enabled(false);
				break;
			case PAGE.HAIR:
				skinColorScroll.set_enabled(false);
				hairColorScroll.set_enabled(IsNeedScrollHairColor());
				break;
			}
			switch (page)
			{
			case PAGE.SEX:
				break;
			case PAGE.NAME:
				break;
			default:
				if (page == PAGE.VOICE)
				{
					this.StartCoroutine(DoVoiceChangePlayVoice());
				}
				break;
			case PAGE.CONFIRM:
			{
				string inputNameGG = GetInputNameGG();
				if (inputNameGG.Length == 0)
				{
					int num = Random.Range(0, MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.presetPlayerNameCount);
					inputNameGG = StringTable.Get(STRING_CATEGORY.CHARA_MAKE, (uint)(40000 + 1000 * sexID + num));
					if (inputName != null)
					{
						inputName.SetName(inputNameGG);
						OnChangeName();
					}
				}
				playerRotAnim.Set(0.5f, playerLoader.get_transform().get_rotation(), Quaternion.AngleAxis(initPlayerRot, Vector3.get_up()), null, default(Quaternion), null);
				playerRot = initPlayerRot;
				playerRotAnim.Play();
				break;
			}
			}
		}
	}

	private IEnumerator DoVoiceChangePlayVoice()
	{
		yield return (object)new WaitForSeconds(0.25f);
		PlayDefaultVoice();
	}

	private void PlayVoice(int id)
	{
		if (voiceAudioObject != null)
		{
			voiceAudioObject.Stop(0);
			voiceAudioObject = null;
		}
		int num = sexID;
		int num2 = voiceTypeID;
		int num3 = num2;
		if (num3 >= voices[num].Length)
		{
			num3 = 0;
		}
		LoadObject loadObject = voices[num][num3];
		if (loadObject != null && loadObject.loadedObjects[id] != null)
		{
			AudioClip val = loadObject.loadedObjects[id].obj as AudioClip;
			if (val != null)
			{
				voiceAudioObject = SoundManager.PlayUISE(val, 1f, false, null, 0);
			}
		}
	}

	private void PlayDefaultVoice()
	{
		PlayVoice(0);
	}

	private void PlayVoiceRandom()
	{
		int id = Random.Range(0, VOICE_ID_CANDIDATE.Length);
		PlayVoice(id);
	}

	private void OnQuery_PLAY_VOICE()
	{
		PlayVoiceRandom();
	}

	private void OnQuery_SEX()
	{
		sexID = (int)GameSection.GetEventData();
		ChangeSex();
		SetToggleGroup(UI.OBJ_GENDERS_ON, UI.OBJ_GENDERS_OFF, sexID, null);
		LoadModel();
		UpdateLists();
		UpdateVoiceNames();
	}

	private void OnQuery_SKIN_COLOR()
	{
		skinColorID = (int)GameSection.GetEventData();
		SelectSkinColor(skinColorID);
		LoadModel();
	}

	private void OnQuery_HAIR_COLOR()
	{
		hairColorID = (int)GameSection.GetEventData();
		SelectHairColor(hairColorID);
		LoadModel();
	}

	private void OnQuery_VOICE()
	{
		voiceTypeID = (int)GameSection.GetEventData();
		SetToggleGroup(UI.OBJ_VOICES_ON, UI.OBJ_VOICES_OFF, voiceTypeID, null);
		PlayDefaultVoice();
	}

	protected void OnQuery_OK()
	{
		int num = voiceTypeID;
		if (nonFirstCharaMake)
		{
			BackBeforeSceneOrStatus();
		}
		if (num >= MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount)
		{
			num = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount - 1;
		}
		GameSection.StayEvent();
		SendEditFigure(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
			if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 2)
			{
				MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_charactercreate_end, "Tutorial");
			}
			if (!nonFirstCharaMake)
			{
				int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
				Native.TrackUserRegEventAppsFlyer(id.ToString());
			}
			DispatchEvent("MAIN_MENU_HOME", null);
		});
	}

	public void CheckUniqueName(int nextPage)
	{
		string inputNameGG = GetInputNameGG();
		if (inputNameGG.Length == 0)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your character name.", null, null, null, null), delegate
			{
			}, false, 0);
		}
		else
		{
			GameSection.StayEvent();
			OptionCheckUniqueNameModel.RequestSendForm requestSendForm = new OptionCheckUniqueNameModel.RequestSendForm();
			requestSendForm.name = inputNameGG;
			Protocol.Send(OptionCheckUniqueNameModel.URL, requestSendForm, delegate(OptionEditFigureModel ret)
			{
				bool flag = ret.Error == Error.None;
				GameSection.ResumeEvent(flag, null);
				if (flag)
				{
					MovePage(nextPage);
				}
			}, string.Empty);
		}
	}

	public void SendEditFigure(Action<bool> call_back)
	{
		OptionEditFigureModel.RequestSendForm requestSendForm = new OptionEditFigureModel.RequestSendForm();
		requestSendForm.sex = sexID;
		requestSendForm.face = IndexToFaceType(lists[0].index);
		requestSendForm.hair = IndexToHead(lists[1].index);
		requestSendForm.color = hairColorID;
		requestSendForm.skin = skinColorID;
		requestSendForm.voice = voiceTypeID;
		requestSendForm.name = GetInputNameGG();
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		Protocol.Send(OptionEditFigureModel.URL, requestSendForm, delegate(OptionEditFigureModel ret)
		{
			bool obj = false;
			switch (ret.Error)
			{
			case Error.None:
				obj = true;
				break;
			case Error.ERR_OPTION_NOT_UNIQUE_NAME:
				MovePage(4);
				break;
			case Error.ERR_CRYSTAL_NOT_ENOUGH:
				GameSection.ChangeStayEvent("NOT_ENOUGTH", null);
				obj = true;
				break;
			}
			call_back(obj);
		}, string.Empty);
	}

	private void OnQuery_TERMS()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 1)
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_charactercreate_agreement, "Tutorial");
		}
		isTermsEnable = !isTermsEnable;
		SetActive((Enum)UI.SPR_CHECK, isTermsEnable);
		SetActive((Enum)UI.SPR_CHECK_OFF, !isTermsEnable);
		OnChangeName();
	}

	private void ResetAnim(Enum ctrl_enum)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(ctrl_enum);
		if (null != ctrl)
		{
			UIButtonEffect component = ctrl.get_gameObject().GetComponent<UIButtonEffect>();
			if (null != component)
			{
				component.ResetAnim();
			}
		}
	}

	private void BackBeforeSceneOrStatus()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("ProfileTop"))
		{
			List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
			GameSectionHistory.HistoryData historyData = historyList[0];
			if (historyData.sceneName == "Smith")
			{
				GameSection.ChangeEvent("TO_STATUS", null);
			}
			else
			{
				GameSectionHistory.HistoryData item = historyList[historyList.Count - 1];
				historyList.Clear();
				historyList.Add(historyData);
				historyList.Add(item);
				GameSection.ChangeEvent("[BACK]", null);
			}
		}
		else
		{
			GameSection.ChangeEvent("TO_STATUS", null);
		}
	}

	private Transform CreateColorItem(int index, Transform parent)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ResourceUtility.Realizes(colorListItem, 5);
		val.set_parent(parent);
		val.set_localScale(Vector3.get_one());
		return val;
	}

	private void InitSkinColorItem(int index, Transform iTransform, bool isRecycle)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		UIScrollView component = GetCtrl(UI.SCR_HAIR_COLOR_LIST).GetComponent<UIScrollView>();
		int num = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.hasSkinColorIndexes[index];
		Color skinColor = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetSkinColor(num);
		CharaMakeColorListItem component2 = iTransform.GetComponent<CharaMakeColorListItem>();
		component2.Init(skinColor, num, component);
		SetEvent(component2.uiEventSender, "SKIN_COLOR", num);
	}

	private void SelectSkinColor(int id)
	{
		Transform ctrl = GetCtrl(UI.GRD_SKIN_COLOR_LIST);
		CharaMakeColorListItem[] componentsInChildren = ctrl.GetComponentsInChildren<CharaMakeColorListItem>();
		CharaMakeColorListItem[] array = componentsInChildren;
		foreach (CharaMakeColorListItem charaMakeColorListItem in array)
		{
			if (id == charaMakeColorListItem.id)
			{
				charaMakeColorListItem.On();
			}
			else
			{
				charaMakeColorListItem.Off();
			}
		}
	}

	private void InitHairColorItem(int index, Transform iTransform, bool isRecycle)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		UIScrollView component = GetCtrl(UI.SCR_HAIR_COLOR_LIST).GetComponent<UIScrollView>();
		int num = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.hasHairColorIndexes[index];
		Color hairColor = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetHairColor(num);
		CharaMakeColorListItem component2 = iTransform.GetComponent<CharaMakeColorListItem>();
		component2.Init(hairColor, num, component);
		SetEvent(component2.uiEventSender, "HAIR_COLOR", num);
	}

	private void SelectHairColor(int id)
	{
		Transform ctrl = GetCtrl(UI.GRD_HAIR_COLOR_LIST);
		CharaMakeColorListItem[] componentsInChildren = ctrl.GetComponentsInChildren<CharaMakeColorListItem>();
		CharaMakeColorListItem[] array = componentsInChildren;
		foreach (CharaMakeColorListItem charaMakeColorListItem in array)
		{
			if (id == charaMakeColorListItem.id)
			{
				charaMakeColorListItem.On();
			}
			else
			{
				charaMakeColorListItem.Off();
			}
		}
	}

	private bool IsNeedScrollSkinColor()
	{
		int num = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.hasSkinColorIndexes.Length;
		return num > 12;
	}

	private bool IsNeedScrollHairColor()
	{
		int num = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.hasHairColorIndexes.Length;
		return num > 12;
	}

	private bool IsWoman()
	{
		return sexID != 0;
	}

	private void ChangeSex()
	{
		GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
		if (IsWoman())
		{
			int id = IndexToFaceType(lists[0].index);
			if (!HasVisual(hasVisuals.hasWomanFaceIndexes, id))
			{
				lists[0].index = 0;
			}
			int id2 = IndexToHead(lists[1].index);
			if (!HasVisual(hasVisuals.hasWomanHeadIndexes, id2))
			{
				lists[1].index = 0;
			}
		}
		else
		{
			int id3 = IndexToFaceType(lists[0].index);
			if (!HasVisual(hasVisuals.hasManFaceIndexes, id3))
			{
				lists[0].index = 0;
			}
			int id4 = IndexToHead(lists[1].index);
			if (!HasVisual(hasVisuals.hasManHeadIndexes, id4))
			{
				lists[1].index = 0;
			}
		}
	}

	private bool HasVisual(int[] array, int id)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == id)
			{
				return true;
			}
		}
		return false;
	}

	private int FaceTypeToIndex(int faceType)
	{
		GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
		int[] array = (!IsWoman()) ? hasVisuals.hasManFaceIndexes : hasVisuals.hasWomanFaceIndexes;
		for (int i = 0; i < array.Length; i++)
		{
			int num = array[i];
			if (num == faceType)
			{
				return i;
			}
		}
		return -1;
	}

	private int HeadToIndex(int head)
	{
		GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
		int[] array = (!IsWoman()) ? hasVisuals.hasManHeadIndexes : hasVisuals.hasWomanHeadIndexes;
		for (int i = 0; i < array.Length; i++)
		{
			int num = array[i];
			if (num == head)
			{
				return i;
			}
		}
		return -1;
	}

	private int IndexToFaceType(int index)
	{
		GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
		return hasVisuals.GetFaceIndex(IsWoman(), index);
	}

	private int IndexToHead(int index)
	{
		GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
		return hasVisuals.GetHeadIndex(IsWoman(), index);
	}

	private string GetInputNameGG()
	{
		if (editType == EDIT_TYPE.Name)
		{
			return GetInputValue((Enum)UI.IPT_NAME);
		}
		if (editType == EDIT_TYPE.Appearance)
		{
			return MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
		}
		return GetInputValue((Enum)UI.IPT_NAME_GG);
	}

	private void SetInputNameGG(string name)
	{
		if (editType == EDIT_TYPE.Name)
		{
			SetInputValue((Enum)UI.IPT_NAME, name);
		}
		else
		{
			SetInputValue((Enum)UI.IPT_NAME_GG, name);
		}
	}
}
