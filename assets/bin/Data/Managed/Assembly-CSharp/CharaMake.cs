// Decompiled with JetBrains decompiler
// Type: CharaMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CharaMake : GameSection
{
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
  private CharaMake.ListInfo[] lists = new CharaMake.ListInfo[2];
  private int sexID;
  private int skinColorID = 1;
  private int hairColorID;
  private int voiceTypeID;
  private CharaMake.PAGE page = CharaMake.PAGE.MAX;
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
  private CharaMake.EDIT_TYPE editType;
  private static readonly int[] VOICE_ID_CANDIDATE = new int[5]
  {
    1,
    94,
    2,
    4,
    14
  };

  public static void GetCameraPosRot(out Vector3 pos, out Vector3 rot, bool is_status_scene)
  {
    CharaMake.GetCameraPosRot(out pos, out Vector3 _, out rot, is_status_scene);
  }

  public static void GetCameraPosRot(
    out Vector3 cam_pos,
    out Vector3 cam_zoom_pos,
    out Vector3 cam_rot,
    bool is_non_title_scene)
  {
    cam_pos = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.mainCameraPos;
    cam_rot = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.mainCameraRot;
    cam_zoom_pos = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.zoomCameraPos;
    if (!is_non_title_scene)
      return;
    float num = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.playerRot - MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerRot;
    Vector3 playerPos1 = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.playerPos;
    Vector3 playerPos2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerPos;
    Quaternion quaternion1 = Quaternion.AngleAxis(num, Vector3.up);
    cam_pos = Vector3.op_Addition(Quaternion.op_Multiply(quaternion1, Vector3.op_Subtraction(cam_pos, playerPos2)), playerPos1);
    cam_zoom_pos = Vector3.op_Addition(Quaternion.op_Multiply(quaternion1, Vector3.op_Subtraction(cam_zoom_pos, playerPos2)), playerPos1);
    ref Vector3 local = ref cam_rot;
    Quaternion quaternion2 = Quaternion.op_Multiply(Quaternion.AngleAxis(num, Vector3.up), Quaternion.Euler(cam_rot));
    Vector3 eulerAngles = ((Quaternion) ref quaternion2).eulerAngles;
    local = eulerAngles;
  }

  public override string overrideBackKeyEvent
  {
    get => this.page == CharaMake.PAGE.SEX && !this.nonFirstCharaMake ? "__NONE" : "PAGE_PREV";
  }

  private void OnEnable() => InputManager.OnDrag += new InputManager.OnTouchDelegate(this.OnDrag);

  private void OnDisable() => InputManager.OnDrag -= new InputManager.OnTouchDelegate(this.OnDrag);

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private void GetEditType(object[] event_data)
  {
    this.editType = CharaMake.EDIT_TYPE.All;
    if (event_data == null || event_data.Length <= 2)
      return;
    this.editType = (CharaMake.EDIT_TYPE) event_data[2];
  }

  private IEnumerator DoInitialize()
  {
    if (MonoBehaviourSingleton<PredownloadManager>.IsValid())
    {
      while (MonoBehaviourSingleton<PredownloadManager>.I.isLoading)
        yield return (object) null;
      Object.Destroy((Object) MonoBehaviourSingleton<PredownloadManager>.I);
    }
    int index1 = 0;
    for (int length = this.lists.Length; index1 < length; ++index1)
      this.lists[index1] = new CharaMake.ListInfo();
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedCharaMakeModelAnchor)
    {
      UIWidget component = this.GetComponent<UIWidget>((Enum) CharaMake.UI.TEX_MODEL);
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
        component.leftAnchor.absolute = specialDeviceInfo.CharaMakeTexModelAnchor.left;
        component.rightAnchor.absolute = specialDeviceInfo.CharaMakeTexModelAnchor.right;
        component.bottomAnchor.absolute = specialDeviceInfo.CharaMakeTexModelAnchor.bottom;
        component.topAnchor.absolute = specialDeviceInfo.CharaMakeTexModelAnchor.top;
        component.UpdateAnchors();
      }
    }
    if (GameSection.GetEventData() is object[] eventData)
    {
      this.nonFirstCharaMake = true;
      this.isTermsEnable = true;
      Network.UserInfo userInfo = eventData[0] as Network.UserInfo;
      UserStatus userStatus = eventData[1] as UserStatus;
      this.GetEditType(eventData);
      this.sexID = userStatus.sex;
      this.lists[0].index = this.FaceTypeToIndex(userStatus.faceId);
      this.skinColorID = userStatus.skinId;
      this.lists[1].index = this.HeadToIndex(userStatus.hairId);
      this.hairColorID = userStatus.hairColorId;
      this.voiceTypeID = userStatus.voiceId;
      this.defaultUserName = userInfo.name;
    }
    CharaMake.GetCameraPosRot(out this.mainCameraPos, out this.zoomCameraPos, out Vector3 _, this.nonFirstCharaMake);
    if (!this.nonFirstCharaMake)
    {
      this.playerPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerPos;
      this.playerRot = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerRot;
    }
    else
    {
      this.playerPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.playerPos;
      this.playerRot = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.playerRot;
    }
    this.initPlayerRot = this.playerRot;
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.stageObject, (Object) null) && Object.op_Equality((Object) this.shadow, (Object) null))
    {
      this.shadow = PlayerLoader.CreateShadow(MonoBehaviourSingleton<StageManager>.I.stageObject, false);
      if (Object.op_Inequality((Object) this.shadow, (Object) null))
        this.shadow.position = Vector3.op_Addition(this.playerPos, new Vector3(0.0f, 0.005f, 0.0f));
    }
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    ResourceManager.enableCache = false;
    int playerVoiceTypeCount = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount;
    this.voices = new LoadObject[2][];
    for (int sex = 0; sex < 2; ++sex)
    {
      this.voices[sex] = new LoadObject[playerVoiceTypeCount];
      for (int voice_type_id = 0; voice_type_id < playerVoiceTypeCount; ++voice_type_id)
      {
        int length = CharaMake.VOICE_ID_CANDIDATE.Length;
        string[] resource_names = new string[length];
        for (int index2 = 0; index2 < length; ++index2)
          resource_names[index2] = ResourceName.GetActionVoiceName(sex, voice_type_id, CharaMake.VOICE_ID_CANDIDATE[index2]);
        this.voices[sex][voice_type_id] = load_queue.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageName(sex, voice_type_id), resource_names);
      }
    }
    ResourceManager.enableCache = true;
    LoadObject lo_color_list_item = load_queue.Load(RESOURCE_CATEGORY.UI, "CharaMakeColorListItem");
    yield return (object) load_queue.Wait();
    GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
    this.colorListItem = lo_color_list_item.loadedObject as GameObject;
    this.SetGrid((Enum) CharaMake.UI.GRD_SKIN_COLOR_LIST, (string) null, hasVisuals.hasSkinColorIndexes.Length, true, new Func<int, Transform, Transform>(this.CreateColorItem), new Action<int, Transform, bool>(this.InitSkinColorItem));
    this.SelectSkinColor(this.skinColorID);
    this.skinColorScroll = ((Component) this.GetCtrl((Enum) CharaMake.UI.SCR_SKIN_COLOR_LIST)).GetComponent<UIScrollView>();
    if (this.IsNeedScrollSkinColor())
      ((Component) this.GetCtrl((Enum) CharaMake.UI.GRD_SKIN_COLOR_LIST)).GetComponent<UIGrid>().pivot = UIWidget.Pivot.Left;
    this.SetGrid((Enum) CharaMake.UI.GRD_HAIR_COLOR_LIST, (string) null, hasVisuals.hasHairColorIndexes.Length, true, new Func<int, Transform, Transform>(this.CreateColorItem), new Action<int, Transform, bool>(this.InitHairColorItem));
    this.SelectHairColor(this.hairColorID);
    this.hairColorScroll = ((Component) this.GetCtrl((Enum) CharaMake.UI.SCR_HAIR_COLOR_LIST)).GetComponent<UIScrollView>();
    if (this.IsNeedScrollHairColor())
      ((Component) this.GetCtrl((Enum) CharaMake.UI.GRD_HAIR_COLOR_LIST)).GetComponent<UIGrid>().pivot = UIWidget.Pivot.Left;
    this.LoadModel();
    while (Object.op_Inequality((Object) this.playerLoader, (Object) null) && this.playerLoader.isLoading || load_queue.IsLoading())
      yield return (object) null;
    this.cameraAnim.endValue = this.mainCameraPos;
    this.MovePage(0);
    if (!TutorialStep.HasAllTutorialCompleted() && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep < 1)
    {
      bool hasSentTutorialStep = false;
      MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep((Action<bool>) (has_sent => hasSentTutorialStep = true));
      while (!hasSentTutorialStep)
        yield return (object) null;
    }
    this.ResetLayout();
    int tutorialStep = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
    base.Initialize();
  }

  private void ResetLayout()
  {
    if (this.editType == CharaMake.EDIT_TYPE.Name)
    {
      this.SetActive((Enum) CharaMake.UI.OBJ_INPUT_NAME_GG, false);
      ((Component) this.GetCtrl((Enum) CharaMake.UI.OBJ_PAGE_BUTTONS)).gameObject.SetActive(false);
      ((Component) this.GetCtrl((Enum) CharaMake.UI.SPR_PAGE_CURSOR)).gameObject.SetActive(false);
      ((Component) this.GetCtrl((Enum) CharaMake.UI.CharaLine)).gameObject.SetActive(false);
      this.MovePage(4);
    }
    else if (this.editType == CharaMake.EDIT_TYPE.Appearance)
    {
      this.SetActive((Enum) CharaMake.UI.OBJ_INPUT_NAME_GG, false);
      Transform ctrl1 = this.GetCtrl((Enum) CharaMake.UI.SelectGender);
      Vector3 vector3_1;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_1).\u002Ector(ctrl1.localPosition.x, ctrl1.localPosition.y - 90f, ctrl1.localPosition.z);
      ctrl1.localPosition = vector3_1;
      Transform ctrl2 = this.GetCtrl((Enum) CharaMake.UI.OBJ_GENDERS_ON);
      Vector3 vector3_2;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_2).\u002Ector(ctrl2.localPosition.x, ctrl2.localPosition.y - 90f, ctrl2.localPosition.z);
      ctrl2.localPosition = vector3_2;
      Transform ctrl3 = this.GetCtrl((Enum) CharaMake.UI.OBJ_GENDERS_OFF);
      Vector3 vector3_3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_3).\u002Ector(ctrl3.localPosition.x, ctrl3.localPosition.y - 90f, ctrl3.localPosition.z);
      ctrl3.localPosition = vector3_3;
    }
    ((Component) this.GetCtrl((Enum) CharaMake.UI.BTN_BACK)).gameObject.SetActive(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep > 2);
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    if (!Object.op_Inequality((Object) this.shadow, (Object) null))
      return;
    Object.DestroyImmediate((Object) ((Component) this.shadow).gameObject);
    this.shadow = (Transform) null;
  }

  public override void UpdateUI()
  {
    if (this.nonFirstCharaMake)
      this.SetSprite((Enum) CharaMake.UI.SPR_FRAME, "CharacterEdit");
    Transform ctrl = this.GetCtrl((Enum) CharaMake.UI.OBJ_PAGE_BUTTONS);
    int event_data = 0;
    for (int childCount = ctrl.childCount; event_data < childCount; ++event_data)
    {
      Transform child = ctrl.GetChild(event_data);
      this.SetEvent(child, "MOVE_PAGE", event_data);
      this.SetActive(child, (Enum) CharaMake.UI.SPR_ON, (CharaMake.PAGE) event_data == this.page);
      this.SetActive(child, (Enum) CharaMake.UI.SPR_OFF, (CharaMake.PAGE) event_data != this.page);
    }
    this.SetCellWidth((Enum) CharaMake.UI.GRD_PAGES, MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth, true);
    this.SetCenter((Enum) CharaMake.UI.GRD_PAGES, (int) this.page);
    this.UpdateLists();
    this.SetToggleGroup(CharaMake.UI.OBJ_GENDERS_ON, CharaMake.UI.OBJ_GENDERS_OFF, this.sexID, "SEX");
    this.SetToggleGroup(CharaMake.UI.OBJ_SKIN_COLORS_ON, CharaMake.UI.OBJ_SKIN_COLORS_OFF, this.skinColorID, "SKIN_COLOR");
    this.SetToggleGroup(CharaMake.UI.OBJ_HAIR_COLORS_ON, CharaMake.UI.OBJ_HAIR_COLORS_OFF, this.hairColorID, "HAIR_COLOR");
    this.SetToggleGroup(CharaMake.UI.OBJ_VOICES_ON, CharaMake.UI.OBJ_VOICES_OFF, this.voiceTypeID, "VOICE");
    this.UpdateVoiceNames();
    if (this.editType == CharaMake.EDIT_TYPE.Name)
    {
      this.SetInput((Enum) CharaMake.UI.IPT_NAME, this.sectionData.GetText("DEFAULT_NAME_TEXT"), 14, new EventDelegate.Callback(this.OnChangeName));
      this.inputName = this.GetComponent<UINameInput>((Enum) CharaMake.UI.IPT_NAME);
    }
    else
    {
      this.SetInput((Enum) CharaMake.UI.IPT_NAME_GG, this.sectionData.GetText("DEFAULT_NAME_TEXT"), 14, new EventDelegate.Callback(this.OnChangeName));
      this.inputName = this.GetComponent<UINameInput>((Enum) CharaMake.UI.IPT_NAME_GG);
    }
    this.inputName.CreateCaret(true);
    bool flag = false;
    if (!string.IsNullOrEmpty(this.defaultUserName))
    {
      this.inputName.SetName(this.defaultUserName);
      this.OnChangeName();
      if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
      {
        TimeManager.GetNow();
        DateTime.TryParse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.editNameAt.date, out DateTime _);
      }
      this.defaultUserName = string.Empty;
    }
    else if (!TutorialStep.HasAllTutorialCompleted())
    {
      this.inputName.SetName("Colopl");
      this.OnChangeName();
    }
    this.SetToggle((Enum) CharaMake.UI.TGL_INPUT_LIMITER, flag);
    this.SetLabelText((Enum) CharaMake.UI.STR_CHANGEABLE_STATUS2, this.sectionData.GetText("STR_CHANGEABLE_STATUS"));
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep < 2)
    {
      this.SetActive((Enum) CharaMake.UI.TGL_VISIBLE_EQUIP_BUTTON, false);
    }
    else
    {
      this.SetActive((Enum) CharaMake.UI.TGL_VISIBLE_EQUIP_BUTTON, true);
      this.SetToggleButton((Enum) CharaMake.UI.TGL_VISIBLE_EQUIP_BUTTON, this.isShowMyEquip, (Action<bool>) (is_active =>
      {
        this.isShowMyEquip = is_active;
        this.LoadModel();
      }));
    }
    this.SetActive((Enum) CharaMake.UI.TERMS_OF_SERVICE, !this.nonFirstCharaMake);
    this.SetActive((Enum) CharaMake.UI.SPR_CHECK, this.isTermsEnable);
    this.SetActive((Enum) CharaMake.UI.SPR_CHECK_OFF, !this.isTermsEnable);
    this.LoadModel();
  }

  private void LateUpdate()
  {
    if (this.cameraAnim.IsPlaying())
      MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = this.cameraAnim.Update();
    if (!this.playerRotAnim.IsPlaying() || !Object.op_Inequality((Object) this.playerLoader, (Object) null))
      return;
    ((Component) this.playerLoader).transform.rotation = this.playerRotAnim.Update();
  }

  private void SetSpriteColors(CharaMake.UI ctrl, Color[] colors)
  {
    Transform ctrl1 = this.GetCtrl((Enum) ctrl);
    if (Object.op_Equality((Object) ctrl1, (Object) null))
      return;
    int index = 0;
    for (int childCount = ctrl1.childCount; index < childCount; ++index)
    {
      Color color = colors[index];
      Transform child = ctrl1.GetChild(index);
      this.SetColor(ctrl1.GetChild(index), color);
      UIButton component = this.GetComponent<UIButton>(child);
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.hover = color;
        component.pressed = color;
        component.disabledColor = color;
      }
    }
  }

  private void SetList(
    CharaMake.LIST list,
    CharaMake.UI ui,
    int item_num,
    Action<int, Transform> cb)
  {
    CharaMake.ListInfo list1 = this.lists[(int) list];
    Transform ctrl = this.GetCtrl((Enum) ui);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
    {
      Transform transform = ctrl.Find("CharaMakeList");
      if (Object.op_Inequality((Object) transform, (Object) null))
      {
        ((Object) ((Component) transform).gameObject).name = "CharaMakeList_Destroy";
        Object.Destroy((Object) ((Component) transform).gameObject);
      }
    }
    Transform root = this.SetPrefab((Enum) ui, "CharaMakeList");
    this.SetEvent(root, (Enum) CharaMake.UI.BTN_LIST_PREV, "LIST_PREV", (int) ui);
    this.SetEvent(root, (Enum) CharaMake.UI.BTN_LIST_NEXT, "LIST_NEXT", (int) ui);
    this.SetGrid(root, (Enum) CharaMake.UI.GRD_LIST, "CharaMakeListItem", item_num, false, (Action<int, Transform, bool>) ((i, c, b) => cb(i, c)));
    this.SetCenterOnChildFunc(root, (Enum) CharaMake.UI.GRD_LIST, new UICenterOnChild.OnCenterCallback(this.OnCenterListItem));
    this.SetCenter(root, (Enum) CharaMake.UI.GRD_LIST, list1.index);
    list1.tansform = root.parent;
    list1.max = item_num;
  }

  private void UpdateLists()
  {
    GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
    this.SetList(CharaMake.LIST.FACETYPE, CharaMake.UI.OBJ_LIST_FACETYPE, this.IsWoman() ? hasVisuals.hasWomanFaceIndexes.Length : hasVisuals.hasManFaceIndexes.Length, (Action<int, Transform>) ((i, c) =>
    {
      int faceIndex = hasVisuals.GetFaceIndex(this.IsWoman(), i);
      string faceName = Singleton<AvatarTable>.I.GetFaceName(this.IsWoman(), faceIndex);
      this.SetLabelText(c, (Enum) CharaMake.UI.LBL_LISTITEM, faceName);
    }));
    this.SetList(CharaMake.LIST.HAIRSTYLE, CharaMake.UI.OBJ_LIST_HAIRSTYLE, this.IsWoman() ? hasVisuals.hasWomanHeadIndexes.Length : hasVisuals.hasManHeadIndexes.Length, (Action<int, Transform>) ((i, c) =>
    {
      int headIndex = hasVisuals.GetHeadIndex(this.IsWoman(), i);
      string headName = Singleton<AvatarTable>.I.GetHeadName(this.IsWoman(), headIndex);
      this.SetLabelText(c, (Enum) CharaMake.UI.LBL_LISTITEM, headName);
    }));
  }

  private void UpdateVoiceNames()
  {
    Transform ctrl1 = this.GetCtrl((Enum) CharaMake.UI.OBJ_VOICES_ON);
    Transform ctrl2 = this.GetCtrl((Enum) CharaMake.UI.OBJ_VOICES_OFF);
    if (Object.op_Equality((Object) ctrl1, (Object) null) || Object.op_Equality((Object) ctrl2, (Object) null))
      return;
    int num = 0;
    for (int childCount = ctrl1.childCount; num < childCount; ++num)
    {
      string text = StringTable.Get(STRING_CATEGORY.CHARA_MAKE, (uint) (30000 + 1000 * this.sexID + num));
      this.SetLabelText(ctrl1.GetChild(num), (Enum) CharaMake.UI.LBL_VOICE_NAME, text);
      this.SetLabelText(ctrl2.GetChild(num), (Enum) CharaMake.UI.LBL_VOICE_NAME, text);
    }
  }

  private void SetToggleGroup(
    CharaMake.UI ui_on,
    CharaMake.UI ui_off,
    int value,
    string event_name = null)
  {
    Transform ctrl1 = this.GetCtrl((Enum) ui_on);
    Transform ctrl2 = this.GetCtrl((Enum) ui_off);
    if (Object.op_Equality((Object) ctrl1, (Object) null) || Object.op_Equality((Object) ctrl2, (Object) null))
      return;
    int event_data = 0;
    for (int childCount = ctrl1.childCount; event_data < childCount; ++event_data)
    {
      ((Component) ctrl1.GetChild(event_data)).gameObject.SetActive(event_data == value);
      Transform child = ctrl2.GetChild(event_data);
      if (event_name != null)
        this.SetEvent(child, event_name, event_data);
      ((Component) child).gameObject.SetActive(event_data != value);
    }
  }

  private void LoadModel()
  {
    this.DeleteRenderTexture((Enum) CharaMake.UI.TEX_MODEL);
    int sex = this.sexID;
    int faceType = this.IndexToFaceType(this.lists[0].index);
    int skinColorId = this.skinColorID;
    int head = this.IndexToHead(this.lists[1].index);
    int hairColorId = this.hairColorID;
    PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
    playerLoadInfo.SetFace(sex, faceType, skinColorId);
    playerLoadInfo.SetHair(sex, head, hairColorId);
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep < 2)
      this.SetDefultEquip(playerLoadInfo, sex);
    else if (this.isShowMyEquip)
    {
      UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
      EquipSetInfo equipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(userStatus.eSetNo);
      playerLoadInfo.SetUpCharaMakeLoadInfo(equipSet, (ulong) uint.Parse(userStatus.armorUniqId), (ulong) uint.Parse(userStatus.helmUniqId), (ulong) uint.Parse(userStatus.armUniqId), (ulong) uint.Parse(userStatus.legUniqId), sex, userStatus.showHelm == 1);
    }
    else
      this.SetDefultEquip(playerLoadInfo, sex);
    int use_hair_overlay = -1;
    if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
      use_hair_overlay = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.isChangeHairShader ? hairColorId : -1;
    this.InitRenderTexture((Enum) CharaMake.UI.TEX_MODEL, 0.0f, true);
    this.playerLoader = ((Component) this.GetRenderTextureModelTransform((Enum) CharaMake.UI.TEX_MODEL)).gameObject.AddComponent<PlayerLoader>();
    this.playerLoader.StartLoad(playerLoadInfo, ((Component) this.playerLoader).gameObject.layer, 98, false, false, false, !this.nonFirstCharaMake, false, false, false, true, SHADER_TYPE.NORMAL, (PlayerLoader.OnCompleteLoad) (o =>
    {
      if (Object.op_Equality((Object) this.playerLoader.animator, (Object) null))
        return;
      ((Component) this.playerLoader).transform.position = this.playerPos;
      ((Component) this.playerLoader).transform.eulerAngles = new Vector3(0.0f, this.playerRot, 0.0f);
      PlayerAnimCtrl.Get(this.playerLoader.animator, sex == 0 ? PLCA.IDLE_01 : PLCA.IDLE_01_F);
      this.EnableRenderTexture((Enum) CharaMake.UI.TEX_MODEL);
    }), use_hair_overlay: use_hair_overlay);
  }

  private void SetDefultEquip(PlayerLoadInfo load_info, int sex)
  {
    load_info.SetEquipBody(sex, (uint) MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerBodyEquipItemID);
    load_info.SetEquipHead(sex, (uint) MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerHeadEquipItemID);
    load_info.SetEquipArm(sex, (uint) MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerArmEquipItemID);
    load_info.SetEquipLeg(sex, (uint) MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerLegEquipItemID);
  }

  private void OnDrag(InputManager.TouchInfo touch_info)
  {
    if (Object.op_Equality((Object) this.playerLoader, (Object) null) || MonoBehaviourSingleton<UIManager>.I.IsDisable() || this.page == CharaMake.PAGE.CONFIRM)
      return;
    ((Component) this.playerLoader).transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
    this.playerRot = ((Component) this.playerLoader).transform.eulerAngles.y;
  }

  private void OnCenterListItem(GameObject go)
  {
    int n = int.Parse(((Object) go).name);
    Transform t = go.transform.parent.parent.parent.parent;
    this.ChangeValue(ref Array.Find<CharaMake.ListInfo>(this.lists, (Predicate<CharaMake.ListInfo>) (o => Object.op_Equality((Object) o.tansform, (Object) t))).index, n);
  }

  private void ChangeValue(ref int v, int n)
  {
    if (v == n)
      return;
    v = n;
    if (this.page == CharaMake.PAGE.VOICE)
      this.PlayDefaultVoice();
    else
      this.LoadModel();
  }

  private void OnChangeName()
  {
    string str = this.GetInputNameGG().Replace(" ", "").Replace("　", "");
    if (str.Length == 0)
    {
      if (Object.op_Inequality((Object) this.inputName, (Object) null))
        this.inputName.InActiveName();
      this.SetColor((Enum) CharaMake.UI.LBL_CONFIRM_NAME, Color.red);
      this.SetLabelText((Enum) CharaMake.UI.LBL_CONFIRM_NAME, this.sectionData.GetText("NO_NAME"));
      this.SetActive((Enum) CharaMake.UI.BTN_YES, false);
      this.SetActive((Enum) CharaMake.UI.BTN_YES_OFF, true);
    }
    else
    {
      if (Object.op_Inequality((Object) this.inputName, (Object) null))
      {
        this.inputName.ActiveName();
        this.inputName.SetName(str);
      }
      this.SetColor((Enum) CharaMake.UI.LBL_CONFIRM_NAME, Color.white);
      this.SetLabelText((Enum) CharaMake.UI.LBL_CONFIRM_NAME, str);
      if (this.isTermsEnable)
      {
        this.SetActive((Enum) CharaMake.UI.BTN_YES, true);
        this.SetActive((Enum) CharaMake.UI.BTN_YES_OFF, false);
      }
      else
      {
        this.SetActive((Enum) CharaMake.UI.BTN_YES, false);
        this.SetActive((Enum) CharaMake.UI.BTN_YES_OFF, true);
      }
    }
  }

  private void OnQuery_LIST_PREV() => this.AddValue((CharaMake.UI) GameSection.GetEventData(), -1);

  private void OnQuery_LIST_NEXT() => this.AddValue((CharaMake.UI) GameSection.GetEventData(), 1);

  private void AddValue(CharaMake.UI ui, int add)
  {
    Transform t = this.GetCtrl((Enum) ui);
    CharaMake.ListInfo listInfo = Array.Find<CharaMake.ListInfo>(this.lists, (Predicate<CharaMake.ListInfo>) (o => Object.op_Equality((Object) o.tansform, (Object) t)));
    int index = listInfo.index + add;
    if (index < 0)
      index = listInfo.max - 1;
    else if (index >= listInfo.max)
      index = 0;
    if (listInfo.index == index)
      return;
    this.SetCenter(listInfo.tansform, (Enum) CharaMake.UI.GRD_LIST, index);
  }

  private void OnQuery_CONFIRM()
  {
    if (this.GetInputNameGG().Length == 0)
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your character name."), (Action<string>) (ret => { }));
    else if (!this.isTermsEnable)
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please accept the term"), (Action<string>) (ret => { }));
    else if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name.Equals(this.GetInputNameGG()))
      this.OnQuery_OK();
    else if (MonoBehaviourSingleton<UserInfoManager>.I.crystalChangeName > 0)
    {
      if (!GameSection.CheckCrystal(MonoBehaviourSingleton<UserInfoManager>.I.crystalChangeName))
        return;
      GameSection.ChangeEvent("SPEND_CRYSTAL_CONFIRM", (object) new object[1]
      {
        (object) MonoBehaviourSingleton<UserInfoManager>.I.crystalChangeName
      });
    }
    else
      this.OnQuery_OK();
  }

  private void OnQuery_CANCEL() => GameSection.BackSection();

  private void OnQuery_MOVE_PAGE()
  {
    int eventData = (int) GameSection.GetEventData();
    if (this.page == CharaMake.PAGE.SEX)
      this.CheckUniqueName(eventData);
    else
      this.MovePage(eventData);
  }

  private void OnQuery_PAGE_PREV()
  {
    if (this.page == CharaMake.PAGE.SEX || this.editType == CharaMake.EDIT_TYPE.Name)
    {
      if (this.nonFirstCharaMake)
        this.BackBeforeSceneOrStatus();
      else
        GameSection.BackSection();
    }
    else
      this.AddPage(-1);
  }

  private void OnQuery_PAGE_NEXT()
  {
    if (this.editType == CharaMake.EDIT_TYPE.Name)
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name.Equals(this.GetInputNameGG()))
      {
        this.BackBeforeSceneOrStatus();
      }
      else
      {
        if (!GameSection.CheckCrystal(MonoBehaviourSingleton<UserInfoManager>.I.crystalChangeName))
          return;
        GameSection.ChangeEvent("SPEND_CRYSTAL_CONFIRM", (object) new object[1]
        {
          (object) MonoBehaviourSingleton<UserInfoManager>.I.crystalChangeName
        });
      }
    }
    else
      this.AddPage(1);
  }

  private void OnQuery_SpendCrystalToChangeNameConfirm_YES()
  {
    GameSection.StayEvent();
    this.SendEditFigure((Action<bool>) (is_success =>
    {
      GameSection.ResumeEvent(false);
      if (is_success)
      {
        this.RequestEvent("BACK_TO_HOME");
        GameSection.BackSection();
      }
      else
        this.StartCoroutine(this.IECloseDialog());
    }));
  }

  private IEnumerator IECloseDialog()
  {
    yield return (object) new WaitForEndOfFrame();
    GameSection.BackSection();
  }

  private void AddPage(int add)
  {
    int num = (int) (this.page + add);
    if (num < 0)
      num = 0;
    else if (num >= 6)
      num = 5;
    if (num == 5 || this.page == CharaMake.PAGE.SEX)
    {
      this.CheckUniqueName(num);
    }
    else
    {
      if (this.editType == CharaMake.EDIT_TYPE.Appearance && num == 4)
        num += add;
      this.MovePage(num);
    }
  }

  private void MovePage(int n)
  {
    if (this.page == (CharaMake.PAGE) n)
      return;
    this.StartCoroutine(this.WaitForTrack(n));
  }

  private IEnumerator WaitForTrack(int n)
  {
    yield return (object) null;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 1)
    {
      switch (this.page)
      {
      }
    }
    yield return (object) null;
    yield return (object) null;
    this.MovePageOld(n);
  }

  private void MovePageOld(int n)
  {
    if (this.page == (CharaMake.PAGE) n)
      return;
    CharaMake.PAGE page = this.page;
    this.page = (CharaMake.PAGE) n;
    this.SetCenter((Enum) CharaMake.UI.GRD_PAGES, n);
    Vector3 end_value;
    switch (this.page)
    {
      case CharaMake.PAGE.FACE:
      case CharaMake.PAGE.HAIR:
      case CharaMake.PAGE.VOICE:
        end_value = this.zoomCameraPos;
        break;
      default:
        end_value = this.mainCameraPos;
        break;
    }
    if (Vector3.op_Inequality(this.cameraAnim.endValue, end_value))
    {
      this.cameraAnim.Set(0.5f, MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position, end_value, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
      this.cameraAnim.Play();
    }
    Transform child1 = this.GetCtrl((Enum) CharaMake.UI.OBJ_PAGE_BUTTONS).GetChild(n);
    this.GetCtrl((Enum) CharaMake.UI.SPR_PAGE_CURSOR).position = child1.position;
    if (page != CharaMake.PAGE.MAX)
    {
      Transform child2 = this.GetCtrl((Enum) CharaMake.UI.OBJ_PAGE_BUTTONS).GetChild((int) page);
      this.SetActive(child2, (Enum) CharaMake.UI.SPR_ON, false);
      this.SetActive(child2, (Enum) CharaMake.UI.SPR_OFF, true);
    }
    this.SetActive(child1, (Enum) CharaMake.UI.SPR_ON, true);
    this.SetActive(child1, (Enum) CharaMake.UI.SPR_OFF, false);
    switch (this.page)
    {
      case CharaMake.PAGE.FACE:
        ((Behaviour) this.skinColorScroll).enabled = this.IsNeedScrollSkinColor();
        ((Behaviour) this.hairColorScroll).enabled = false;
        break;
      case CharaMake.PAGE.HAIR:
        ((Behaviour) this.skinColorScroll).enabled = false;
        ((Behaviour) this.hairColorScroll).enabled = this.IsNeedScrollHairColor();
        break;
    }
    switch (this.page)
    {
      case CharaMake.PAGE.SEX:
        break;
      case CharaMake.PAGE.NAME:
        break;
      case CharaMake.PAGE.CONFIRM:
        if (this.GetInputNameGG().Length == 0)
        {
          string _name = StringTable.Get(STRING_CATEGORY.CHARA_MAKE, (uint) (40000 + 1000 * this.sexID + Random.Range(0, MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.presetPlayerNameCount)));
          if (Object.op_Inequality((Object) this.inputName, (Object) null))
          {
            this.inputName.SetName(_name);
            this.OnChangeName();
          }
        }
        this.playerRotAnim.Set(0.5f, ((Component) this.playerLoader).transform.rotation, Quaternion.AngleAxis(this.initPlayerRot, Vector3.up), (AnimationCurve) null, new Quaternion(), (AnimationCurve) null);
        this.playerRot = this.initPlayerRot;
        this.playerRotAnim.Play();
        break;
      default:
        if (this.page != CharaMake.PAGE.VOICE)
          break;
        this.StartCoroutine(this.DoVoiceChangePlayVoice());
        break;
    }
  }

  private IEnumerator DoVoiceChangePlayVoice()
  {
    yield return (object) new WaitForSeconds(0.25f);
    this.PlayDefaultVoice();
  }

  private void PlayVoice(int id)
  {
    if (Object.op_Inequality((Object) this.voiceAudioObject, (Object) null))
    {
      this.voiceAudioObject.Stop();
      this.voiceAudioObject = (AudioObject) null;
    }
    int sexId = this.sexID;
    int index = this.voiceTypeID;
    if (index >= this.voices[sexId].Length)
      index = 0;
    LoadObject loadObject = this.voices[sexId][index];
    if (loadObject == null || loadObject.loadedObjects[id] == null)
      return;
    AudioClip clip = loadObject.loadedObjects[id].obj as AudioClip;
    if (!Object.op_Inequality((Object) clip, (Object) null))
      return;
    this.voiceAudioObject = SoundManager.PlayUISE(clip, 1f, false, (Transform) null);
  }

  private void PlayDefaultVoice() => this.PlayVoice(0);

  private void PlayVoiceRandom()
  {
    this.PlayVoice(Random.Range(0, CharaMake.VOICE_ID_CANDIDATE.Length));
  }

  private void OnQuery_PLAY_VOICE() => this.PlayVoiceRandom();

  private void OnQuery_SEX()
  {
    this.sexID = (int) GameSection.GetEventData();
    this.ChangeSex();
    this.SetToggleGroup(CharaMake.UI.OBJ_GENDERS_ON, CharaMake.UI.OBJ_GENDERS_OFF, this.sexID);
    this.LoadModel();
    this.UpdateLists();
    this.UpdateVoiceNames();
  }

  private void OnQuery_SKIN_COLOR()
  {
    this.skinColorID = (int) GameSection.GetEventData();
    this.SelectSkinColor(this.skinColorID);
    this.LoadModel();
  }

  private void OnQuery_HAIR_COLOR()
  {
    this.hairColorID = (int) GameSection.GetEventData();
    this.SelectHairColor(this.hairColorID);
    this.LoadModel();
  }

  private void OnQuery_VOICE()
  {
    this.voiceTypeID = (int) GameSection.GetEventData();
    this.SetToggleGroup(CharaMake.UI.OBJ_VOICES_ON, CharaMake.UI.OBJ_VOICES_OFF, this.voiceTypeID);
    this.PlayDefaultVoice();
  }

  protected void OnQuery_OK()
  {
    int voiceTypeId = this.voiceTypeID;
    if (this.nonFirstCharaMake)
      this.BackBeforeSceneOrStatus();
    int playerVoiceTypeCount1 = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount;
    if (voiceTypeId >= playerVoiceTypeCount1)
    {
      int playerVoiceTypeCount2 = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVoiceTypeCount;
    }
    GameSection.StayEvent();
    this.SendEditFigure((Action<bool>) (is_success =>
    {
      GameSection.ResumeEvent(is_success);
      int tutorialStep = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
      if (!this.nonFirstCharaMake)
        Native.TrackUserRegEventAppsFlyer(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString());
      this.DispatchEvent("MAIN_MENU_HOME");
    }));
  }

  public void CheckUniqueName(int nextPage)
  {
    string inputNameGg = this.GetInputNameGG();
    if (inputNameGg.Length == 0)
    {
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "Please enter your character name."), (Action<string>) (ret => { }));
    }
    else
    {
      GameSection.StayEvent();
      Protocol.Send<OptionCheckUniqueNameModel.RequestSendForm, OptionEditFigureModel>(OptionCheckUniqueNameModel.URL, new OptionCheckUniqueNameModel.RequestSendForm()
      {
        name = inputNameGg
      }, (Action<OptionEditFigureModel>) (ret =>
      {
        int num = ret.Error == Error.None ? 1 : 0;
        GameSection.ResumeEvent(num != 0);
        if (num == 0)
          return;
        this.MovePage(nextPage);
      }));
    }
  }

  public void SendEditFigure(Action<bool> call_back)
  {
    Protocol.Send<OptionEditFigureModel.RequestSendForm, OptionEditFigureModel>(OptionEditFigureModel.URL, new OptionEditFigureModel.RequestSendForm()
    {
      sex = this.sexID,
      face = this.IndexToFaceType(this.lists[0].index),
      hair = this.IndexToHead(this.lists[1].index),
      color = this.hairColorID,
      skin = this.skinColorID,
      voice = this.voiceTypeID,
      name = this.GetInputNameGG(),
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal
    }, (Action<OptionEditFigureModel>) (ret =>
    {
      bool flag = false;
      switch (ret.Error)
      {
        case Error.None:
          flag = true;
          break;
        case Error.ERR_CRYSTAL_NOT_ENOUGH:
          GameSection.ChangeStayEvent("NOT_ENOUGTH");
          flag = true;
          break;
        case Error.ERR_OPTION_NOT_UNIQUE_NAME:
          this.MovePage(4);
          break;
      }
      call_back(flag);
    }));
  }

  private void OnQuery_TERMS()
  {
    int tutorialStep = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
    this.isTermsEnable = !this.isTermsEnable;
    this.SetActive((Enum) CharaMake.UI.SPR_CHECK, this.isTermsEnable);
    this.SetActive((Enum) CharaMake.UI.SPR_CHECK_OFF, !this.isTermsEnable);
    this.OnChangeName();
  }

  private void ResetAnim(Enum ctrl_enum)
  {
    Transform ctrl = this.GetCtrl(ctrl_enum);
    if (!Object.op_Inequality((Object) null, (Object) ctrl))
      return;
    UIButtonEffect component = ((Component) ctrl).gameObject.GetComponent<UIButtonEffect>();
    if (!Object.op_Inequality((Object) null, (Object) component))
      return;
    component.ResetAnim();
  }

  private void BackBeforeSceneOrStatus()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("ProfileTop"))
    {
      List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
      GameSectionHistory.HistoryData historyData1 = historyList[0];
      for (int index = 0; index < historyList.Count; ++index)
        Debug.LogWarning((object) historyList[index].sceneName);
      if (historyData1.sceneName == "Smith")
      {
        if (StatusManager.IsUnique())
          GameSection.ChangeEvent("TO_UNIQUE_STATUS");
        else
          GameSection.ChangeEvent("TO_STATUS");
      }
      else
      {
        GameSectionHistory.HistoryData historyData2 = historyList[historyList.Count - 1];
        historyList.Clear();
        historyList.Add(historyData1);
        historyList.Add(historyData2);
        GameSection.ChangeEvent("[BACK]");
      }
    }
    else
      GameSection.ChangeEvent("TO_STATUS");
  }

  private Transform CreateColorItem(int index, Transform parent)
  {
    Transform colorItem = ResourceUtility.Realizes((Object) this.colorListItem, 5);
    colorItem.parent = parent;
    colorItem.localScale = Vector3.one;
    return colorItem;
  }

  private void InitSkinColorItem(int index, Transform iTransform, bool isRecycle)
  {
    UIScrollView component1 = ((Component) this.GetCtrl((Enum) CharaMake.UI.SCR_HAIR_COLOR_LIST)).GetComponent<UIScrollView>();
    int hasSkinColorIndex = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.hasSkinColorIndexes[index];
    Color skinColor = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetSkinColor(hasSkinColorIndex);
    CharaMakeColorListItem component2 = ((Component) iTransform).GetComponent<CharaMakeColorListItem>();
    component2.Init(skinColor, hasSkinColorIndex, component1);
    this.SetEvent(component2.uiEventSender, "SKIN_COLOR", hasSkinColorIndex);
  }

  private void SelectSkinColor(int id)
  {
    foreach (CharaMakeColorListItem componentsInChild in ((Component) this.GetCtrl((Enum) CharaMake.UI.GRD_SKIN_COLOR_LIST)).GetComponentsInChildren<CharaMakeColorListItem>())
    {
      if (id == componentsInChild.id)
        componentsInChild.On();
      else
        componentsInChild.Off();
    }
  }

  private void InitHairColorItem(int index, Transform iTransform, bool isRecycle)
  {
    UIScrollView component1 = ((Component) this.GetCtrl((Enum) CharaMake.UI.SCR_HAIR_COLOR_LIST)).GetComponent<UIScrollView>();
    int hasHairColorIndex = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.hasHairColorIndexes[index];
    Color hairColor = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetHairColor(hasHairColorIndex);
    CharaMakeColorListItem component2 = ((Component) iTransform).GetComponent<CharaMakeColorListItem>();
    component2.Init(hairColor, hasHairColorIndex, component1);
    this.SetEvent(component2.uiEventSender, "HAIR_COLOR", hasHairColorIndex);
  }

  private void SelectHairColor(int id)
  {
    foreach (CharaMakeColorListItem componentsInChild in ((Component) this.GetCtrl((Enum) CharaMake.UI.GRD_HAIR_COLOR_LIST)).GetComponentsInChildren<CharaMakeColorListItem>())
    {
      if (id == componentsInChild.id)
        componentsInChild.On();
      else
        componentsInChild.Off();
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

  private bool IsWoman() => this.sexID != 0;

  private void ChangeSex()
  {
    GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
    if (this.IsWoman())
    {
      int faceType = this.IndexToFaceType(this.lists[0].index);
      if (!this.HasVisual(hasVisuals.hasWomanFaceIndexes, faceType))
        this.lists[0].index = 0;
      int head = this.IndexToHead(this.lists[1].index);
      if (this.HasVisual(hasVisuals.hasWomanHeadIndexes, head))
        return;
      this.lists[1].index = 0;
    }
    else
    {
      int faceType = this.IndexToFaceType(this.lists[0].index);
      if (!this.HasVisual(hasVisuals.hasManFaceIndexes, faceType))
        this.lists[0].index = 0;
      int head = this.IndexToHead(this.lists[1].index);
      if (this.HasVisual(hasVisuals.hasManHeadIndexes, head))
        return;
      this.lists[1].index = 0;
    }
  }

  private bool HasVisual(int[] array, int id)
  {
    for (int index = 0; index < array.Length; ++index)
    {
      if (array[index] == id)
        return true;
    }
    return false;
  }

  private int FaceTypeToIndex(int faceType)
  {
    GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
    int[] numArray = this.IsWoman() ? hasVisuals.hasWomanFaceIndexes : hasVisuals.hasManFaceIndexes;
    for (int index = 0; index < numArray.Length; ++index)
    {
      if (numArray[index] == faceType)
        return index;
    }
    return -1;
  }

  private int HeadToIndex(int head)
  {
    GlobalSettingsManager.HasVisuals hasVisuals = MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals;
    int[] numArray = this.IsWoman() ? hasVisuals.hasWomanHeadIndexes : hasVisuals.hasManHeadIndexes;
    for (int index = 0; index < numArray.Length; ++index)
    {
      if (numArray[index] == head)
        return index;
    }
    return -1;
  }

  private int IndexToFaceType(int index)
  {
    return MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.GetFaceIndex(this.IsWoman(), index);
  }

  private int IndexToHead(int index)
  {
    return MonoBehaviourSingleton<GlobalSettingsManager>.I.hasVisuals.GetHeadIndex(this.IsWoman(), index);
  }

  private string GetInputNameGG()
  {
    if (this.editType == CharaMake.EDIT_TYPE.Name)
      return this.GetInputValue((Enum) CharaMake.UI.IPT_NAME);
    return this.editType == CharaMake.EDIT_TYPE.Appearance ? MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name : this.GetInputValue((Enum) CharaMake.UI.IPT_NAME_GG);
  }

  private void SetInputNameGG(string name)
  {
    if (this.editType == CharaMake.EDIT_TYPE.Name)
      this.SetInputValue((Enum) CharaMake.UI.IPT_NAME, name);
    else
      this.SetInputValue((Enum) CharaMake.UI.IPT_NAME_GG, name);
  }

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
    BTN_BACK,
  }

  private enum PAGE
  {
    SEX,
    FACE,
    HAIR,
    VOICE,
    NAME,
    CONFIRM,
    MAX,
  }

  private enum LIST
  {
    FACETYPE,
    HAIRSTYLE,
    MAX,
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
    Appearance,
  }
}
