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
		BTN_VISIBLE_EQUIP,
		BTN_INVISIBLE_EQUIP,
		TGL_VISIBLE_EQUIP_BUTTON,
		OBJ_INPUT_NAME_GG,
		IPT_NAME_GG,
		SelectGender,
		BTN_BACK
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

	private const int MAX_SHOW_COLOR_ITEM_COUNT = 12;

	private bool isShowMyEquip;

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
		cam_pos = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.mainCameraPos;
		cam_rot = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.mainCameraRot;
		cam_zoom_pos = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.zoomCameraPos;
		if (is_non_title_scene)
		{
			float num = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerRot;
			float angle = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.playerRot - num;
			Vector3 b = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.playerPos;
			Vector3 b2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerPos;
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
			cam_pos = rotation * (cam_pos - b2) + b;
			cam_zoom_pos = rotation * (cam_zoom_pos - b2) + b;
			cam_rot = (Quaternion.AngleAxis(angle, Vector3.up) * Quaternion.Euler(cam_rot)).eulerAngles;
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
		StartCoroutine(DoInitialize());
	}

	private void GetEditType(object[] event_data)
	{
		editType = EDIT_TYPE.All;
		if (event_data != null && event_data.Length > 2)
		{
			editType = (EDIT_TYPE)event_data[2];
		}
	}

	private IEnumerator DoInitialize()
	{
		if (MonoBehaviourSingleton<PredownloadManager>.IsValid())
		{
			while (MonoBehaviourSingleton<PredownloadManager>.I.isLoading)
			{
				yield return null;
			}
			UnityEngine.Object.Destroy(MonoBehaviourSingleton<PredownloadManager>.I);
		}
		int i = 0;
		for (int num = lists.Length; i < num; i++)
		{
			lists[i] = new ListInfo();
		}
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedCharaMakeModelAnchor)
		{
			UIWidget component = GetComponent<UIWidget>(UI.TEX_MODEL);
			if (component != null)
			{
				DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
				component.leftAnchor.absolute = specialDeviceInfo.CharaMakeTexModelAnchor.left;
				component.rightAnchor.absolute = specialDeviceInfo.CharaMakeTexModelAnchor.right;
				component.bottomAnchor.absolute = specialDeviceInfo.CharaMakeTexModelAnchor.bottom;
				component.topAnchor.absolute = specialDeviceInfo.CharaMakeTexModelAnchor.top;
				component.UpdateAnchors();
			}
		}
		object[] array = GameSection.GetEventData() as object[];
		if (array != null)
		{
			nonFirstCharaMake = true;
			isTermsEnable = true;
			UserInfo userInfo = array[0] as UserInfo;
			UserStatus userStatus = array[1] as UserStatus;
			GetEditType(array);
			sexID = userStatus.sex;
			lists[0].index = FaceTypeToIndex(userStatus.faceId);
			skinColorID = userStatus.skinId;
			lists[1].index = HeadToIndex(userStatus.hairId);
			hairColorID = userStatus.hairColorId;
			voiceTypeID = userStatus.voiceId;
			defaultUserName = userInfo.name;
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
			shadow = PlayerLoader.CreateShadow(MonoBehaviourSingleton<StageManager>.I.stageObject, fixedY0: false);
			if (shadow != null)
			{
				shadow.position = playerPos + new Vector3(0f, 0.005f, 0f);
			}
		}
		LoadingQueue load_queue = new LoadingQueue(this);
		ResourceManager.enableCache = false;
		int playerVoiceTypeCount = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount;
		voices = new LoadObject[2][];
		for (int j = 0; j < 2; j++)
		{
			voices[j] = new LoadObject[playerVoiceTypeCount];
			for (int k = 0; k < playerVoiceTypeCount; k++)
			{
				int num2 = VOICE_ID_CANDIDATE.Length;
				string[] array2 = new string[num2];
				for (int l = 0; l < num2; l++)
				{
					array2[l] = ResourceName.GetActionVoiceName(j, k, VOICE_ID_CANDIDATE[l]);
				}
				voices[j][k] = load_queue.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageName(j, k), array2);
			}
		}
		ResourceManager.enableCache = true;
		LoadObject lo_color_list_item = load_queue.Load(RESOURCE_CATEGORY.UI, "CharaMakeColorListItem");
		yield return load_queue.Wait();
		GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
		colorListItem = (lo_color_list_item.loadedObject as GameObject);
		SetGrid(item_num: hasVisuals.hasSkinColorIndexes.Length, grid_ctrl_enum: UI.GRD_SKIN_COLOR_LIST, item_prefab_name: null, reset: true, create_item_func: CreateColorItem, item_init_func: InitSkinColorItem);
		SelectSkinColor(skinColorID);
		skinColorScroll = GetCtrl(UI.SCR_SKIN_COLOR_LIST).GetComponent<UIScrollView>();
		if (IsNeedScrollSkinColor())
		{
			GetCtrl(UI.GRD_SKIN_COLOR_LIST).GetComponent<UIGrid>().pivot = UIWidget.Pivot.Left;
		}
		int item_num2 = hasVisuals.hasHairColorIndexes.Length;
		SetGrid(UI.GRD_HAIR_COLOR_LIST, null, item_num2, reset: true, CreateColorItem, InitHairColorItem);
		SelectHairColor(hairColorID);
		hairColorScroll = GetCtrl(UI.SCR_HAIR_COLOR_LIST).GetComponent<UIScrollView>();
		if (IsNeedScrollHairColor())
		{
			GetCtrl(UI.GRD_HAIR_COLOR_LIST).GetComponent<UIGrid>().pivot = UIWidget.Pivot.Left;
		}
		LoadModel();
		while ((playerLoader != null && playerLoader.isLoading) || load_queue.IsLoading())
		{
			yield return null;
		}
		cameraAnim.endValue = mainCameraPos;
		MovePage(0);
		if (!TutorialStep.HasAllTutorialCompleted() && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep < 1)
		{
			bool hasSentTutorialStep = false;
			MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep(delegate
			{
				hasSentTutorialStep = true;
			});
			while (!hasSentTutorialStep)
			{
				yield return null;
			}
		}
		ResetLayout();
		_ = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
		_ = 1;
		base.Initialize();
	}

	private void ResetLayout()
	{
		if (editType == EDIT_TYPE.Name)
		{
			SetActive(UI.OBJ_INPUT_NAME_GG, is_visible: false);
			GetCtrl(UI.OBJ_PAGE_BUTTONS).gameObject.SetActive(value: false);
			GetCtrl(UI.SPR_PAGE_CURSOR).gameObject.SetActive(value: false);
			GetCtrl(UI.CharaLine).gameObject.SetActive(value: false);
			MovePage(4);
		}
		else if (editType == EDIT_TYPE.Appearance)
		{
			SetActive(UI.OBJ_INPUT_NAME_GG, is_visible: false);
			Transform ctrl = GetCtrl(UI.SelectGender);
			Vector3 vector2 = ctrl.localPosition = new Vector3(ctrl.localPosition.x, ctrl.localPosition.y - 90f, ctrl.localPosition.z);
			Transform ctrl2 = GetCtrl(UI.OBJ_GENDERS_ON);
			Vector3 vector4 = ctrl2.localPosition = new Vector3(ctrl2.localPosition.x, ctrl2.localPosition.y - 90f, ctrl2.localPosition.z);
			Transform ctrl3 = GetCtrl(UI.OBJ_GENDERS_OFF);
			Vector3 vector6 = ctrl3.localPosition = new Vector3(ctrl3.localPosition.x, ctrl3.localPosition.y - 90f, ctrl3.localPosition.z);
		}
		GetCtrl(UI.BTN_BACK).gameObject.SetActive(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep > 2);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (shadow != null)
		{
			UnityEngine.Object.DestroyImmediate(shadow.gameObject);
			shadow = null;
		}
	}

	public override void UpdateUI()
	{
		if (nonFirstCharaMake)
		{
			SetSprite(UI.SPR_FRAME, "CharacterEdit");
		}
		Transform ctrl = GetCtrl(UI.OBJ_PAGE_BUTTONS);
		int i = 0;
		for (int childCount = ctrl.childCount; i < childCount; i++)
		{
			Transform child = ctrl.GetChild(i);
			SetEvent(child, "MOVE_PAGE", i);
			SetActive(child, UI.SPR_ON, i == (int)page);
			SetActive(child, UI.SPR_OFF, i != (int)page);
		}
		SetCellWidth(UI.GRD_PAGES, MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth, reposition: true);
		SetCenter(UI.GRD_PAGES, (int)page);
		UpdateLists();
		SetToggleGroup(UI.OBJ_GENDERS_ON, UI.OBJ_GENDERS_OFF, sexID, "SEX");
		SetToggleGroup(UI.OBJ_SKIN_COLORS_ON, UI.OBJ_SKIN_COLORS_OFF, skinColorID, "SKIN_COLOR");
		SetToggleGroup(UI.OBJ_HAIR_COLORS_ON, UI.OBJ_HAIR_COLORS_OFF, hairColorID, "HAIR_COLOR");
		SetToggleGroup(UI.OBJ_VOICES_ON, UI.OBJ_VOICES_OFF, voiceTypeID, "VOICE");
		UpdateVoiceNames();
		if (editType == EDIT_TYPE.Name)
		{
			SetInput(UI.IPT_NAME, base.sectionData.GetText("DEFAULT_NAME_TEXT"), 14, OnChangeName);
			inputName = GetComponent<UINameInput>(UI.IPT_NAME);
		}
		else
		{
			SetInput(UI.IPT_NAME_GG, base.sectionData.GetText("DEFAULT_NAME_TEXT"), 14, OnChangeName);
			inputName = GetComponent<UINameInput>(UI.IPT_NAME_GG);
		}
		inputName.CreateCaret(is_enable: true);
		bool value = false;
		if (!string.IsNullOrEmpty(defaultUserName))
		{
			inputName.SetName(defaultUserName);
			OnChangeName();
			if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
			{
				TimeManager.GetNow();
				DateTime.TryParse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.editNameAt.date, out DateTime _);
			}
			defaultUserName = string.Empty;
		}
		else if (!TutorialStep.HasAllTutorialCompleted())
		{
			inputName.SetName("Colopl");
			OnChangeName();
		}
		SetToggle(UI.TGL_INPUT_LIMITER, value);
		SetLabelText(UI.STR_CHANGEABLE_STATUS2, base.sectionData.GetText("STR_CHANGEABLE_STATUS"));
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep < 2)
		{
			SetActive(UI.TGL_VISIBLE_EQUIP_BUTTON, is_visible: false);
		}
		else
		{
			SetActive(UI.TGL_VISIBLE_EQUIP_BUTTON, is_visible: true);
			SetToggleButton(UI.TGL_VISIBLE_EQUIP_BUTTON, isShowMyEquip, delegate(bool is_active)
			{
				isShowMyEquip = is_active;
				LoadModel();
			});
		}
		SetActive(UI.TERMS_OF_SERVICE, !nonFirstCharaMake);
		SetActive(UI.SPR_CHECK, isTermsEnable);
		SetActive(UI.SPR_CHECK_OFF, !isTermsEnable);
		LoadModel();
	}

	private void LateUpdate()
	{
		if (cameraAnim.IsPlaying())
		{
			MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = cameraAnim.Update();
		}
		if (playerRotAnim.IsPlaying() && playerLoader != null)
		{
			playerLoader.transform.rotation = playerRotAnim.Update();
		}
	}

	private void SetSpriteColors(UI ctrl, Color[] colors)
	{
		Transform ctrl2 = GetCtrl(ctrl);
		if (ctrl2 == null)
		{
			return;
		}
		int i = 0;
		for (int childCount = ctrl2.childCount; i < childCount; i++)
		{
			Color color = colors[i];
			Transform child = ctrl2.GetChild(i);
			SetColor(ctrl2.GetChild(i), color);
			UIButton component = GetComponent<UIButton>(child);
			if (component != null)
			{
				component.hover = color;
				component.pressed = color;
				component.disabledColor = color;
			}
		}
	}

	private void SetList(LIST list, UI ui, int item_num, Action<int, Transform> cb)
	{
		ListInfo listInfo = lists[(int)list];
		Transform ctrl = GetCtrl(ui);
		if (ctrl != null)
		{
			Transform transform = ctrl.Find("CharaMakeList");
			if (transform != null)
			{
				transform.gameObject.name = "CharaMakeList_Destroy";
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		Transform transform2 = SetPrefab(ui, "CharaMakeList");
		SetEvent(transform2, UI.BTN_LIST_PREV, "LIST_PREV", (int)ui);
		SetEvent(transform2, UI.BTN_LIST_NEXT, "LIST_NEXT", (int)ui);
		SetGrid(transform2, UI.GRD_LIST, "CharaMakeListItem", item_num, reset: false, delegate(int i, Transform c, bool b)
		{
			cb(i, c);
		});
		SetCenterOnChildFunc(transform2, UI.GRD_LIST, OnCenterListItem);
		SetCenter(transform2, UI.GRD_LIST, listInfo.index);
		listInfo.tansform = transform2.parent;
		listInfo.max = item_num;
	}

	private void UpdateLists()
	{
		GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
		int item_num = IsWoman() ? hasVisuals.hasWomanFaceIndexes.Length : hasVisuals.hasManFaceIndexes.Length;
		SetList(LIST.FACETYPE, UI.OBJ_LIST_FACETYPE, item_num, delegate(int i, Transform c)
		{
			int faceIndex = hasVisuals.GetFaceIndex(IsWoman(), i);
			string faceName = Singleton<AvatarTable>.I.GetFaceName(IsWoman(), faceIndex);
			SetLabelText(c, UI.LBL_LISTITEM, faceName);
		});
		int item_num2 = IsWoman() ? hasVisuals.hasWomanHeadIndexes.Length : hasVisuals.hasManHeadIndexes.Length;
		SetList(LIST.HAIRSTYLE, UI.OBJ_LIST_HAIRSTYLE, item_num2, delegate(int i, Transform c)
		{
			int headIndex = hasVisuals.GetHeadIndex(IsWoman(), i);
			string headName = Singleton<AvatarTable>.I.GetHeadName(IsWoman(), headIndex);
			SetLabelText(c, UI.LBL_LISTITEM, headName);
		});
	}

	private void UpdateVoiceNames()
	{
		Transform ctrl = GetCtrl(UI.OBJ_VOICES_ON);
		Transform ctrl2 = GetCtrl(UI.OBJ_VOICES_OFF);
		if (!(ctrl == null) && !(ctrl2 == null))
		{
			int i = 0;
			for (int childCount = ctrl.childCount; i < childCount; i++)
			{
				string text = StringTable.Get(STRING_CATEGORY.CHARA_MAKE, (uint)(30000 + 1000 * sexID + i));
				Transform child = ctrl.GetChild(i);
				SetLabelText(child, UI.LBL_VOICE_NAME, text);
				Transform child2 = ctrl2.GetChild(i);
				SetLabelText(child2, UI.LBL_VOICE_NAME, text);
			}
		}
	}

	private void SetToggleGroup(UI ui_on, UI ui_off, int value, string event_name = null)
	{
		Transform ctrl = GetCtrl(ui_on);
		Transform ctrl2 = GetCtrl(ui_off);
		if (ctrl == null || ctrl2 == null)
		{
			return;
		}
		int i = 0;
		for (int childCount = ctrl.childCount; i < childCount; i++)
		{
			ctrl.GetChild(i).gameObject.SetActive(i == value);
			Transform child = ctrl2.GetChild(i);
			if (event_name != null)
			{
				SetEvent(child, event_name, i);
			}
			child.gameObject.SetActive(i != value);
		}
	}

	private void LoadModel()
	{
		DeleteRenderTexture(UI.TEX_MODEL);
		int sex = sexID;
		int face_type_id = IndexToFaceType(lists[0].index);
		int skin_color_id = skinColorID;
		int hair_style_id = IndexToHead(lists[1].index);
		int num = hairColorID;
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		playerLoadInfo.SetFace(sex, face_type_id, skin_color_id);
		playerLoadInfo.SetHair(sex, hair_style_id, num);
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep < 2)
		{
			SetDefultEquip(playerLoadInfo, sex);
		}
		else if (isShowMyEquip)
		{
			UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
			EquipSetInfo equipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(userStatus.eSetNo);
			playerLoadInfo.SetUpCharaMakeLoadInfo(equipSet, uint.Parse(userStatus.armorUniqId), uint.Parse(userStatus.helmUniqId), uint.Parse(userStatus.armUniqId), uint.Parse(userStatus.legUniqId), sex, userStatus.showHelm == 1);
		}
		else
		{
			SetDefultEquip(playerLoadInfo, sex);
		}
		int use_hair_overlay = -1;
		if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
		{
			use_hair_overlay = (MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.isChangeHairShader ? num : (-1));
		}
		InitRenderTexture(UI.TEX_MODEL, 0f, link_main_camera: true);
		playerLoader = GetRenderTextureModelTransform(UI.TEX_MODEL).gameObject.AddComponent<PlayerLoader>();
		playerLoader.StartLoad(playerLoadInfo, playerLoader.gameObject.layer, 98, need_anim_event: false, need_foot_stamp: false, need_shadow: false, !nonFirstCharaMake, need_action_voice: false, need_high_reso_tex: false, need_res_ref_count: false, need_dev_frame_instantiate: true, SHADER_TYPE.NORMAL, delegate
		{
			if (!(playerLoader.animator == null))
			{
				playerLoader.transform.position = playerPos;
				playerLoader.transform.eulerAngles = new Vector3(0f, playerRot, 0f);
				PlayerAnimCtrl.Get(playerLoader.animator, (sex == 0) ? PLCA.IDLE_01 : PLCA.IDLE_01_F);
				EnableRenderTexture(UI.TEX_MODEL);
			}
		}, enable_eye_blick: true, use_hair_overlay);
	}

	private void SetDefultEquip(PlayerLoadInfo load_info, int sex)
	{
		load_info.SetEquipBody(sex, (uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerBodyEquipItemID);
		load_info.SetEquipHead(sex, (uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerHeadEquipItemID);
		load_info.SetEquipArm(sex, (uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerArmEquipItemID);
		load_info.SetEquipLeg(sex, (uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerLegEquipItemID);
	}

	private void OnDrag(InputManager.TouchInfo touch_info)
	{
		if (!(playerLoader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && page != PAGE.CONFIRM)
		{
			playerLoader.transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
			playerRot = playerLoader.transform.eulerAngles.y;
		}
	}

	private void OnCenterListItem(GameObject go)
	{
		int n = int.Parse(go.name);
		Transform t = go.transform.parent.parent.parent.parent;
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
		string inputNameGG = GetInputNameGG();
		inputNameGG = inputNameGG.Replace(" ", "");
		inputNameGG = inputNameGG.Replace("\u3000", "");
		if (inputNameGG.Length == 0)
		{
			if (inputName != null)
			{
				inputName.InActiveName();
			}
			SetColor(UI.LBL_CONFIRM_NAME, Color.red);
			SetLabelText(UI.LBL_CONFIRM_NAME, base.sectionData.GetText("NO_NAME"));
			SetActive(UI.BTN_YES, is_visible: false);
			SetActive(UI.BTN_YES_OFF, is_visible: true);
			return;
		}
		if (inputName != null)
		{
			inputName.ActiveName();
			inputName.SetName(inputNameGG);
		}
		SetColor(UI.LBL_CONFIRM_NAME, Color.white);
		SetLabelText(UI.LBL_CONFIRM_NAME, inputNameGG);
		if (isTermsEnable)
		{
			SetActive(UI.BTN_YES, is_visible: true);
			SetActive(UI.BTN_YES_OFF, is_visible: false);
		}
		else
		{
			SetActive(UI.BTN_YES, is_visible: false);
			SetActive(UI.BTN_YES_OFF, is_visible: true);
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
			SetCenter(listInfo.tansform, UI.GRD_LIST, num);
		}
	}

	private void OnQuery_CONFIRM()
	{
		if (GetInputNameGG().Length == 0)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your character name."), delegate
			{
			});
		}
		else if (!isTermsEnable)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please accept the term"), delegate
			{
			});
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name.Equals(GetInputNameGG()))
		{
			OnQuery_OK();
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.crystalChangeName > 0)
		{
			if (GameSection.CheckCrystal(MonoBehaviourSingleton<UserInfoManager>.I.crystalChangeName))
			{
				GameSection.ChangeEvent("SPEND_CRYSTAL_CONFIRM", new object[1]
				{
					MonoBehaviourSingleton<UserInfoManager>.I.crystalChangeName
				});
			}
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
			else if (GameSection.CheckCrystal(MonoBehaviourSingleton<UserInfoManager>.I.crystalChangeName))
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
			GameSection.ResumeEvent(is_resume: false);
			if (is_success)
			{
				RequestEvent("BACK_TO_HOME");
				GameSection.BackSection();
			}
			else
			{
				StartCoroutine(IECloseDialog());
			}
		});
	}

	private IEnumerator IECloseDialog()
	{
		yield return new WaitForEndOfFrame();
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
			return;
		}
		if (editType == EDIT_TYPE.Appearance && num == 4)
		{
			num += add;
		}
		MovePage(num);
	}

	private void MovePage(int n)
	{
		if (page != (PAGE)n)
		{
			StartCoroutine(WaitForTrack(n));
		}
	}

	private IEnumerator WaitForTrack(int n)
	{
		yield return null;
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 1)
		{
			switch (page)
			{
			}
		}
		yield return null;
		yield return null;
		MovePageOld(n);
	}

	private void MovePageOld(int n)
	{
		if (page == (PAGE)n)
		{
			return;
		}
		PAGE pAGE = page;
		page = (PAGE)n;
		SetCenter(UI.GRD_PAGES, n);
		PAGE pAGE2 = page;
		Vector3 vector = ((uint)(pAGE2 - 1) <= 2u) ? zoomCameraPos : mainCameraPos;
		if (cameraAnim.endValue != vector)
		{
			cameraAnim.Set(0.5f, MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position, vector);
			cameraAnim.Play();
		}
		Transform child = GetCtrl(UI.OBJ_PAGE_BUTTONS).GetChild(n);
		GetCtrl(UI.SPR_PAGE_CURSOR).position = child.position;
		if (pAGE != PAGE.MAX)
		{
			Transform child2 = GetCtrl(UI.OBJ_PAGE_BUTTONS).GetChild((int)pAGE);
			SetActive(child2, UI.SPR_ON, is_visible: false);
			SetActive(child2, UI.SPR_OFF, is_visible: true);
		}
		SetActive(child, UI.SPR_ON, is_visible: true);
		SetActive(child, UI.SPR_OFF, is_visible: false);
		switch (page)
		{
		case PAGE.FACE:
			skinColorScroll.enabled = IsNeedScrollSkinColor();
			hairColorScroll.enabled = false;
			break;
		case PAGE.HAIR:
			skinColorScroll.enabled = false;
			hairColorScroll.enabled = IsNeedScrollHairColor();
			break;
		}
		switch (page)
		{
		case PAGE.SEX:
		case PAGE.NAME:
			break;
		default:
			if (page == PAGE.VOICE)
			{
				StartCoroutine(DoVoiceChangePlayVoice());
			}
			break;
		case PAGE.CONFIRM:
		{
			string inputNameGG = GetInputNameGG();
			if (inputNameGG.Length == 0)
			{
				int num = UnityEngine.Random.Range(0, MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.presetPlayerNameCount);
				inputNameGG = StringTable.Get(STRING_CATEGORY.CHARA_MAKE, (uint)(40000 + 1000 * sexID + num));
				if (inputName != null)
				{
					inputName.SetName(inputNameGG);
					OnChangeName();
				}
			}
			playerRotAnim.Set(0.5f, playerLoader.transform.rotation, Quaternion.AngleAxis(initPlayerRot, Vector3.up));
			playerRot = initPlayerRot;
			playerRotAnim.Play();
			break;
		}
		}
	}

	private IEnumerator DoVoiceChangePlayVoice()
	{
		yield return new WaitForSeconds(0.25f);
		PlayDefaultVoice();
	}

	private void PlayVoice(int id)
	{
		if (voiceAudioObject != null)
		{
			voiceAudioObject.Stop();
			voiceAudioObject = null;
		}
		int num = sexID;
		int num2 = voiceTypeID;
		if (num2 >= voices[num].Length)
		{
			num2 = 0;
		}
		LoadObject loadObject = voices[num][num2];
		if (loadObject != null && loadObject.loadedObjects[id] != null)
		{
			AudioClip audioClip = loadObject.loadedObjects[id].obj as AudioClip;
			if (audioClip != null)
			{
				voiceAudioObject = SoundManager.PlayUISE(audioClip, 1f, loop: false, null);
			}
		}
	}

	private void PlayDefaultVoice()
	{
		PlayVoice(0);
	}

	private void PlayVoiceRandom()
	{
		int id = UnityEngine.Random.Range(0, VOICE_ID_CANDIDATE.Length);
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
		SetToggleGroup(UI.OBJ_GENDERS_ON, UI.OBJ_GENDERS_OFF, sexID);
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
		SetToggleGroup(UI.OBJ_VOICES_ON, UI.OBJ_VOICES_OFF, voiceTypeID);
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
			_ = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount;
		}
		GameSection.StayEvent();
		SendEditFigure(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
			_ = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
			_ = 2;
			if (!nonFirstCharaMake)
			{
				int id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
				Native.TrackUserRegEventAppsFlyer(id.ToString());
			}
			DispatchEvent("MAIN_MENU_HOME");
		});
	}

	public void CheckUniqueName(int nextPage)
	{
		string inputNameGG = GetInputNameGG();
		if (inputNameGG.Length == 0)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your character name."), delegate
			{
			});
			return;
		}
		GameSection.StayEvent();
		OptionCheckUniqueNameModel.RequestSendForm requestSendForm = new OptionCheckUniqueNameModel.RequestSendForm();
		requestSendForm.name = inputNameGG;
		Protocol.Send(OptionCheckUniqueNameModel.URL, requestSendForm, delegate(OptionEditFigureModel ret)
		{
			bool num = ret.Error == Error.None;
			GameSection.ResumeEvent(num);
			if (num)
			{
				MovePage(nextPage);
			}
		});
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
				GameSection.ChangeStayEvent("NOT_ENOUGTH");
				obj = true;
				break;
			}
			call_back(obj);
		});
	}

	private void OnQuery_TERMS()
	{
		_ = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
		_ = 1;
		isTermsEnable = !isTermsEnable;
		SetActive(UI.SPR_CHECK, isTermsEnable);
		SetActive(UI.SPR_CHECK_OFF, !isTermsEnable);
		OnChangeName();
	}

	private void ResetAnim(Enum ctrl_enum)
	{
		Transform ctrl = GetCtrl(ctrl_enum);
		if (null != ctrl)
		{
			UIButtonEffect component = ctrl.gameObject.GetComponent<UIButtonEffect>();
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
			for (int i = 0; i < historyList.Count; i++)
			{
				Debug.LogWarning(historyList[i].sceneName);
			}
			if (historyData.sceneName == "Smith")
			{
				if (StatusManager.IsUnique())
				{
					GameSection.ChangeEvent("TO_UNIQUE_STATUS");
				}
				else
				{
					GameSection.ChangeEvent("TO_STATUS");
				}
			}
			else
			{
				GameSectionHistory.HistoryData item = historyList[historyList.Count - 1];
				historyList.Clear();
				historyList.Add(historyData);
				historyList.Add(item);
				GameSection.ChangeEvent("[BACK]");
			}
		}
		else
		{
			GameSection.ChangeEvent("TO_STATUS");
		}
	}

	private Transform CreateColorItem(int index, Transform parent)
	{
		Transform transform = ResourceUtility.Realizes(colorListItem, 5);
		transform.parent = parent;
		transform.localScale = Vector3.one;
		return transform;
	}

	private void InitSkinColorItem(int index, Transform iTransform, bool isRecycle)
	{
		UIScrollView component = GetCtrl(UI.SCR_HAIR_COLOR_LIST).GetComponent<UIScrollView>();
		int num = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.hasSkinColorIndexes[index];
		Color skinColor = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetSkinColor(num);
		CharaMakeColorListItem component2 = iTransform.GetComponent<CharaMakeColorListItem>();
		component2.Init(skinColor, num, component);
		SetEvent(component2.uiEventSender, "SKIN_COLOR", num);
	}

	private void SelectSkinColor(int id)
	{
		CharaMakeColorListItem[] componentsInChildren = GetCtrl(UI.GRD_SKIN_COLOR_LIST).GetComponentsInChildren<CharaMakeColorListItem>();
		foreach (CharaMakeColorListItem charaMakeColorListItem in componentsInChildren)
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
		UIScrollView component = GetCtrl(UI.SCR_HAIR_COLOR_LIST).GetComponent<UIScrollView>();
		int num = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.hasHairColorIndexes[index];
		Color hairColor = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetHairColor(num);
		CharaMakeColorListItem component2 = iTransform.GetComponent<CharaMakeColorListItem>();
		component2.Init(hairColor, num, component);
		SetEvent(component2.uiEventSender, "HAIR_COLOR", num);
	}

	private void SelectHairColor(int id)
	{
		CharaMakeColorListItem[] componentsInChildren = GetCtrl(UI.GRD_HAIR_COLOR_LIST).GetComponentsInChildren<CharaMakeColorListItem>();
		foreach (CharaMakeColorListItem charaMakeColorListItem in componentsInChildren)
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
		return MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.hasSkinColorIndexes.Length > 12;
	}

	private bool IsNeedScrollHairColor()
	{
		return MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.hasHairColorIndexes.Length > 12;
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
		int[] array = IsWoman() ? hasVisuals.hasWomanFaceIndexes : hasVisuals.hasManFaceIndexes;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == faceType)
			{
				return i;
			}
		}
		return -1;
	}

	private int HeadToIndex(int head)
	{
		GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
		int[] array = IsWoman() ? hasVisuals.hasWomanHeadIndexes : hasVisuals.hasManHeadIndexes;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == head)
			{
				return i;
			}
		}
		return -1;
	}

	private int IndexToFaceType(int index)
	{
		return MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.GetFaceIndex(IsWoman(), index);
	}

	private int IndexToHead(int index)
	{
		return MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.GetHeadIndex(IsWoman(), index);
	}

	private string GetInputNameGG()
	{
		if (editType == EDIT_TYPE.Name)
		{
			return GetInputValue(UI.IPT_NAME);
		}
		if (editType == EDIT_TYPE.Appearance)
		{
			return MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
		}
		return GetInputValue(UI.IPT_NAME_GG);
	}

	private void SetInputNameGG(string name)
	{
		if (editType == EDIT_TYPE.Name)
		{
			SetInputValue(UI.IPT_NAME, name);
		}
		else
		{
			SetInputValue(UI.IPT_NAME_GG, name);
		}
	}
}
