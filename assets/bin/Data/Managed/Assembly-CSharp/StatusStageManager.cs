// Decompiled with JetBrains decompiler
// Type: StatusStageManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class StatusStageManager : MonoBehaviourSingleton<StatusStageManager>
{
  private StatusStageManager.VIEW_TYPE viewType;
  private StatusStageManager.VIEW_MODE viewMode;
  private UITexture uiTexture;
  private UIRenderTexture renderTexture;
  private PlayerLoader playerLoader;
  private Transform playerShadow;
  private Camera targetCamera;
  private Transform targetCameraTransform;
  private OutGameSettingsManager.StatusScene parameter;
  private Vector3Interpolator cameraPosAnim = new Vector3Interpolator();
  private QuaternionInterpolator cameraRotAnim = new QuaternionInterpolator();
  private QuaternionInterpolator playerRotAnim = new QuaternionInterpolator();
  private FloatInterpolator cameraFovAnim = new FloatInterpolator();
  private bool cameraTurningMode;
  private StatusEquip.LocalEquipSetData equipSetData;
  private EquipItemInfo equipInfo;
  private StatusSmithCharacter m_stSmithCharacter;
  private StatusSmithCharacter m_stUniqueSmithCharacter;

  public bool isBusy => this.cameraPosAnim.IsPlaying() || this.cameraRotAnim.IsPlaying();

  public PlayerLoader GetPlayerLoader() => this.playerLoader;

  public int GetPlayerLayer() => this.renderTexture.renderLayer;

  protected override void Awake()
  {
    base.Awake();
    this.targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    this.targetCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
    this.parameter = MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene;
    this.uiTexture = Utility.CreateGameObjectAndComponent("UITexture", MonoBehaviourSingleton<UIManager>.I.system._transform, 5) as UITexture;
    this.uiTexture.shader = ResourceUtility.FindShader("Unlit/ui_render_tex");
    this.uiTexture.SetAnchor(((Component) MonoBehaviourSingleton<UIManager>.I.system).gameObject, 0, 0, 0, 0);
    this.uiTexture.UpdateAnchors();
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
    {
      UIWidget component = ((Component) this.uiTexture).gameObject.GetComponent<UIWidget>();
      component.width = 480;
      component.height = 854;
    }
    this.m_stSmithCharacter = (StatusSmithCharacter) Utility.CreateGameObjectAndComponent("StatusSmithCharacter", this._transform);
    this.m_stSmithCharacter.isUnique = false;
    this.m_stUniqueSmithCharacter = (StatusSmithCharacter) Utility.CreateGameObjectAndComponent("StatusSmithCharacter", this._transform);
    this.m_stUniqueSmithCharacter.isUnique = true;
  }

  protected override void _OnDestroy()
  {
    if (Object.op_Inequality((Object) this.uiTexture, (Object) null))
      Object.Destroy((Object) ((Component) this.uiTexture).gameObject);
    if (!Object.op_Inequality((Object) this.playerShadow, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this.playerShadow).gameObject);
  }

  private void OnEnable() => InputManager.OnDrag += new InputManager.OnTouchDelegate(this.OnDrag);

  protected override void OnDisable()
  {
    base.OnDisable();
    InputManager.OnDrag -= new InputManager.OnTouchDelegate(this.OnDrag);
  }

  private void OnDrag(InputManager.TouchInfo touch_info)
  {
    if (Object.op_Equality((Object) this.playerLoader, (Object) null) || MonoBehaviourSingleton<UIManager>.I.IsDisable())
      return;
    string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
    if (currentSectionName != "StatusTop" && currentSectionName != "StatusAvatar" && currentSectionName != "StatusAccessory" && currentSectionName != "UniqueStatusTop")
      return;
    ((Component) this.playerLoader).transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
  }

  public void SetUITextureActive(bool active)
  {
    ((Component) this.uiTexture).gameObject.SetActive(active);
  }

  public void ClearPlayerLoaded(PlayerLoadInfo load_info)
  {
    if (Object.op_Inequality((Object) this.playerLoader, (Object) null) && this.playerLoader.loadInfo.Equals(load_info) || !Object.op_Inequality((Object) this.renderTexture, (Object) null))
      return;
    Object.DestroyImmediate((Object) this.renderTexture);
  }

  public void LoadPlayer(PlayerLoadInfo load_info, int anim_id = 0)
  {
    if (Object.op_Equality((Object) this.playerShadow, (Object) null))
    {
      this.playerShadow = PlayerLoader.CreateShadow(MonoBehaviourSingleton<StageManager>.I.stageObject, false);
      ((Component) this.playerShadow).transform.position = Vector3.op_Addition(this.parameter.playerPos, new Vector3(0.0f, 0.005f, 0.0f));
    }
    ShaderGlobal.lightProbe = false;
    if (Object.op_Inequality((Object) this.playerLoader, (Object) null) && this.playerLoader.loadInfo.Equals(load_info))
      return;
    if (Object.op_Inequality((Object) this.renderTexture, (Object) null))
      Object.DestroyImmediate((Object) this.renderTexture);
    this.renderTexture = UIRenderTexture.Get(this.uiTexture, link_main_camera: true);
    this.renderTexture.Disable();
    this.renderTexture.nearClipPlane = this.parameter.renderTextureNearClip;
    int use_hair_overlay = -1;
    if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
      use_hair_overlay = MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isChangeHairShader ? MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hairColorId : -1;
    int anim_id1 = anim_id;
    if (anim_id1 == 0)
      anim_id1 = PLAYER_ANIM_TYPE.GetStatus(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
    this.playerLoader = ((Component) this.renderTexture.modelTransform).gameObject.AddComponent<PlayerLoader>();
    this.playerLoader.StartLoad(load_info, this.renderTexture.renderLayer, anim_id1, false, false, false, false, false, true, true, true, SHADER_TYPE.NORMAL, (PlayerLoader.OnCompleteLoad) (o =>
    {
      ((Component) this.playerLoader).transform.position = this.parameter.playerPos;
      ((Component) this.playerLoader).transform.eulerAngles = new Vector3(0.0f, this.viewMode == StatusStageManager.VIEW_MODE.EQUIP ? this.parameter.playerRot : this.parameter.avatarPlayerRot, 0.0f);
      if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
      {
        float num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex == 0 ? this.parameter.playerScaleMale : this.parameter.playerScaleFemale;
        ((Component) this.playerLoader).transform.localScale = ((Component) this.playerLoader).transform.localScale.Mul(new Vector3(num, num, num));
      }
      this.renderTexture.Enable();
    }), use_hair_overlay: use_hair_overlay);
  }

  public void SetViewMode(StatusStageManager.VIEW_MODE view_mode)
  {
    if (this.viewMode == view_mode)
      return;
    this.equipSetData = (StatusEquip.LocalEquipSetData) null;
    if (this.viewType == StatusStageManager.VIEW_TYPE.STATUS)
      this.MoveCamera(this.viewType, this.viewType, this.viewMode, view_mode);
    this.viewMode = view_mode;
  }

  public void SetEquipSetData(StatusEquip.LocalEquipSetData equip_set_data)
  {
    if (this.equipSetData == equip_set_data)
      return;
    this.equipInfo = (EquipItemInfo) null;
    this.equipSetData = equip_set_data;
    this.MoveCamera(this.viewType, this.viewType, this.viewMode, this.viewMode);
  }

  public void SetEquipInfo(EquipItemInfo equip_info)
  {
    PlayerLoadInfo playerLoadInfo = this.playerLoader.loadInfo.Clone();
    if (equip_info != null)
      playerLoadInfo.SetEquip(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, equip_info.tableData);
    else if (this.viewMode == StatusStageManager.VIEW_MODE.AVATAR)
    {
      int equip = EQUIP_SLOT.AvatatToEquip(this.equipSetData.index);
      equip_info = this.equipSetData.equipSetInfo.item[equip];
      if (equip_info == null)
        playerLoadInfo.RemoveEquip(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, equip);
      else
        playerLoadInfo.SetEquip(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, equip_info.tableData);
    }
    else if (this.equipSetData != null)
      playerLoadInfo.RemoveEquip(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, this.equipSetData.index);
    if (this.equipInfo == equip_info)
      return;
    this.equipInfo = equip_info;
    this.MoveCamera(this.viewType, this.viewType, this.viewMode, this.viewMode);
  }

  public void UpdateCamera(
    string scene_name,
    string section_name,
    GameSceneTables.SectionData section_data)
  {
    if (section_data.type.IsDialog())
      return;
    StatusStageManager.VIEW_TYPE viewType = this.viewType;
    StatusStageManager.VIEW_TYPE type_to = !(scene_name == "StatusScene") && !(scene_name == "UniqueStatusScene") || !(section_name != "StatusToSmith") || !(section_name != "ItemStorageSell") || section_name.Contains("Exchange") || !(section_name != "UniqueStatusToSmith") ? StatusStageManager.VIEW_TYPE.SMITH : StatusStageManager.VIEW_TYPE.STATUS;
    if (this.viewType == type_to)
      return;
    this.MoveCamera(this.viewType, type_to, this.viewMode, this.viewMode);
    this.viewType = type_to;
  }

  private void MoveCamera(
    StatusStageManager.VIEW_TYPE type_from,
    StatusStageManager.VIEW_TYPE type_to,
    StatusStageManager.VIEW_MODE mode_from,
    StatusStageManager.VIEW_MODE mode_to)
  {
    Quaternion end_value1 = Quaternion.Euler(0.0f, this.parameter.playerRot, 0.0f);
    Vector3 end_value2;
    Quaternion end_value3;
    float cameraFieldOfView;
    if (type_to == StatusStageManager.VIEW_TYPE.STATUS)
    {
      Vector3 playerPos = this.parameter.playerPos;
      if (mode_to == StatusStageManager.VIEW_MODE.AVATAR)
        end_value1 = Quaternion.Euler(0.0f, this.parameter.avatarPlayerRot, 0.0f);
      Vector3 vector3_1;
      if (this.equipSetData != null)
      {
        OutGameSettingsManager.StatusScene.EquipViewInfo equipViewInfo = (OutGameSettingsManager.StatusScene.EquipViewInfo) null;
        if (this.equipInfo != null)
          equipViewInfo = this.parameter.GetEquipViewInfo(this.equipInfo.tableData.type.ToString());
        if (equipViewInfo == null)
          equipViewInfo = this.parameter.GetEquipViewInfo(EQUIP_SLOT.ToType(this.viewMode == StatusStageManager.VIEW_MODE.AVATAR ? EQUIP_SLOT.AvatatToEquip(this.equipSetData.index) : this.equipSetData.index).ToString());
        Vector3 cameraTargetPos = equipViewInfo.cameraTargetPos;
        if (mode_to == StatusStageManager.VIEW_MODE.AVATAR)
          cameraTargetPos.x = 0.0f;
        vector3_1 = Vector3.op_Addition(Quaternion.op_Multiply(end_value1, cameraTargetPos), playerPos);
        Vector3 vector3_2 = Quaternion.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(-equipViewInfo.cameraXAngle, Vector3.right), Quaternion.AngleAxis(-equipViewInfo.cameraYAngle, Vector3.up)), Vector3.forward);
        Vector3 vector3_3 = Quaternion.op_Multiply(end_value1, vector3_2);
        end_value2 = Vector3.op_Addition(vector3_1, Vector3.op_Multiply(vector3_3, equipViewInfo.cameraDistance));
      }
      else
      {
        Vector3 vector3_4 = Quaternion.op_Multiply(end_value1, Vector3.forward);
        end_value2 = Vector3.op_Addition(Vector3.op_Addition(playerPos, Vector3.op_Multiply(vector3_4, this.parameter.cameraTargetDistance)), new Vector3(0.0f, this.parameter.cameraHeight, 0.0f));
        vector3_1 = Vector3.op_Addition(playerPos, new Vector3(0.0f, this.parameter.cameraTargetHeight, 0.0f));
      }
      end_value3 = Quaternion.LookRotation(Vector3.op_Subtraction(vector3_1, end_value2));
      cameraFieldOfView = this.parameter.cameraFieldOfView;
    }
    else
    {
      OutGameSettingsManager.SmithScene smithScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene;
      end_value2 = smithScene.createCameraPos;
      end_value3 = Quaternion.Euler(smithScene.createCameraRot);
      cameraFieldOfView = smithScene.createCameraFieldOfView;
      end_value1 = Quaternion.Euler(0.0f, this.parameter.playerRot, 0.0f);
    }
    float _time = this.parameter.cameraMoveTime;
    if (MonoBehaviourSingleton<TransitionManager>.I.isTransing && !MonoBehaviourSingleton<TransitionManager>.I.isChanging)
      _time = 0.0f;
    this.cameraTurningMode = (double) _time > 0.0 && type_from == type_to && type_from == StatusStageManager.VIEW_TYPE.STATUS && mode_from != mode_to;
    this.cameraPosAnim.Set(_time, this.targetCameraTransform.position, end_value2, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    this.cameraPosAnim.Play();
    this.cameraRotAnim.Set(_time, this.targetCameraTransform.rotation, end_value3, (AnimationCurve) null, new Quaternion(), (AnimationCurve) null);
    this.cameraRotAnim.Play();
    this.cameraFovAnim.Set(_time, this.targetCamera.fieldOfView, cameraFieldOfView, (AnimationCurve) null, 0.0f, (AnimationCurve) null);
    this.cameraFovAnim.Play();
    if (!Object.op_Inequality((Object) this.playerLoader, (Object) null) || this.playerLoader.isLoading)
      return;
    this.playerRotAnim.Set(_time * 1.25f, ((Component) this.playerLoader).transform.rotation, end_value1, (AnimationCurve) null, new Quaternion(), (AnimationCurve) null);
    this.playerRotAnim.Play();
  }

  private void LateUpdate()
  {
    if (this.playerRotAnim.IsPlaying() && Object.op_Inequality((Object) this.playerLoader, (Object) null) && !this.playerLoader.isLoading)
      ((Component) this.playerLoader).transform.rotation = this.playerRotAnim.Update();
    if (!this.cameraPosAnim.IsPlaying())
      return;
    this.targetCamera.fieldOfView = this.cameraFovAnim.Update();
    this.targetCameraTransform.position = this.cameraPosAnim.Update();
    this.targetCameraTransform.rotation = this.cameraRotAnim.Update();
    if (!this.cameraTurningMode)
      return;
    Vector3 vector3 = Vector3.op_UnaryNegation(this.targetCameraTransform.forward);
    vector3.x *= this.parameter.cameraTargetDistance;
    vector3.y = this.parameter.cameraHeight;
    vector3.z *= this.parameter.cameraTargetDistance;
    this.targetCameraTransform.position = Vector3.op_Addition(vector3, this.parameter.playerPos);
  }

  public void SetSmithCharacterActivate(bool active)
  {
    if (!Object.op_Inequality((Object) this.m_stSmithCharacter, (Object) null))
      return;
    this.m_stSmithCharacter.SetActive(active);
  }

  public void SetUniqueSmithCharacterActivate(bool active)
  {
    if (!Object.op_Inequality((Object) this.m_stUniqueSmithCharacter, (Object) null))
      return;
    this.m_stUniqueSmithCharacter.SetActive(active);
  }

  public void SetEnableSmithCharacterActivate(bool active)
  {
    if (StatusManager.IsUnique())
      this.SetUniqueSmithCharacterActivate(active);
    else
      this.SetSmithCharacterActivate(active);
  }

  public void SetDisableSmithCharacterActivate(bool active)
  {
    if (!StatusManager.IsUnique())
      this.SetUniqueSmithCharacterActivate(active);
    else
      this.SetSmithCharacterActivate(active);
  }

  public enum VIEW_TYPE
  {
    INIT,
    STATUS,
    SMITH,
  }

  public enum VIEW_MODE
  {
    EQUIP,
    AVATAR,
  }
}
