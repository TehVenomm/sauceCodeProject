// Decompiled with JetBrains decompiler
// Type: TutorialBossDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TutorialBossDirector : MonoBehaviour
{
  [SerializeField]
  private Animation cameraAnim;
  private readonly string BATTLE_ENTER_CAMERA_CLIP_NAME = "CAM_Tutorial";
  private readonly string BATTLE_EXIT_CAMERA_CLIP_NAME = "CAM_TutorialEnd_001";
  [SerializeField]
  private RuntimeAnimatorController playerAnimatorController;
  private RuntimeAnimatorController originalPlayerAnimatorController;
  private Character player;
  private readonly string PLAYER_ANIM_ENTER_CUT_SCENE_START_NAME = "PLC00_1001_tutorial";
  private readonly string PLAYER_ANIM_EXIT_CUT_SCENE_START_NAME = "PLC00_1002_TutorialEnd";
  private Enemy boss;
  private CircleShadow bossShadow;
  private Material bossShadowMaterial;
  private EnemyController enemyController;
  private readonly string BOSS_ANIM_ENTER_CUT_SCENE_STATE_NAME = "ENM011_1001_Tutorial";
  private readonly string BOSS_ANIM_EXIT_CUT_SCENE_STATE_NAME = "ENM011_1002_TutorialEnd";
  private RadialBlurFilter radialBlurFilter;
  [SerializeField]
  private GameObject[] titleEffectPrefab;
  public bool replaceCameraRoationWithCutSceneRotation;
  [SerializeField]
  private RuntimeAnimatorController legendDragonAnimController;
  private GameObject legendDragon;
  private GameObject titleUIPrefab;
  private readonly string LEGEND_DRAGON_ANIM_STATE = "ENM011_1003_TutorialEnd";
  private Vector3 cutChangePosition = Vector3.zero;
  private Quaternion cutChangeRotation = Quaternion.identity;
  [SerializeField]
  public TutorialBossDirector.Logo logo;
  public static readonly Vector3 CAMERA_END_POSITION = new Vector3(-0.24f, 2.67f, 31.75705f);
  public static readonly Quaternion CAMERA_END_ROTAION = new Quaternion(3f / 1000f, 0.9898f, -0.1407118f, 0.02110636f);
  private readonly float DURATION_TO_BATTLE_START = 0.4f;
  private GameObject[] effects;

  public Camera logoCamera => this.logo != null ? this.logo.camera : (Camera) null;

  public float originalFov { set; get; }

  public void StartBattleStartDirection(Enemy enemy, Character character, System.Action onComplete)
  {
    this.boss = enemy;
    this.bossShadow = ((Component) this.boss).GetComponentInChildren<CircleShadow>();
    this.bossShadowMaterial = ((Renderer) ((Component) this.bossShadow).GetComponent<MeshRenderer>()).material;
    this.bossShadow.setAnimTransform(this.boss.hip);
    this.player = character;
    this.radialBlurFilter = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<RadialBlurFilter>();
    MonoBehaviourSingleton<SoundManager>.I.requestBGMID = 114;
    MonoBehaviourSingleton<SoundManager>.I.TransitionTo("EventBattle1");
    this.originalPlayerAnimatorController = this.player.animator.runtimeAnimatorController;
    this.player.animator.runtimeAnimatorController = this.playerAnimatorController;
    this.player.animator.cullingMode = (AnimatorCullingMode) 0;
    this.player.animator.Rebind();
    this.player._position = new Vector3(0.0f, 0.0f, 26f);
    this.player.PlayMotion(this.PLAYER_ANIM_ENTER_CUT_SCENE_START_NAME);
    enemy.animator.cullingMode = (AnimatorCullingMode) 0;
    enemy.animator.Rebind();
    enemy.PlayMotion(this.BOSS_ANIM_ENTER_CUT_SCENE_STATE_NAME);
    this.enemyController = ((Component) enemy).GetComponent<EnemyController>();
    ((Behaviour) this.enemyController).enabled = false;
    this.originalFov = MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView;
    this.cameraAnim.cullingType = (AnimationCullingType) 0;
    this.cameraAnim.Play(this.BATTLE_ENTER_CAMERA_CLIP_NAME);
    ((Behaviour) MonoBehaviourSingleton<InGameCameraManager>.I).enabled = false;
    this.StartCoroutine(this.DoBattleStartDirection(onComplete));
  }

  private IEnumerator WaitAndPlaySounds(
    List<TutorialBossDirector.PlaySoundParam> playSoundParams)
  {
    float timer = 0.0f;
    while (0 < playSoundParams.Count)
    {
      TutorialBossDirector.PlaySoundParam playSoundParam = playSoundParams[0];
      if ((double) timer >= (double) playSoundParam.time)
      {
        if (playSoundParam.func != null)
        {
          Vector3 pos = playSoundParam.func();
          SoundManager.PlayOneShotSE(playSoundParam.id, pos);
        }
        else
          SoundManager.PlayOneShotUISE(playSoundParam.id);
        playSoundParams.Remove(playSoundParam);
      }
      timer += Time.deltaTime;
      yield return (object) null;
    }
  }

  private IEnumerator DoBattleStartDirection(System.Action onComplete)
  {
    Transform t = ((Component) this.cameraAnim).transform;
    Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    Transform cameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
    this.StartCoroutine(this.WaitAndPlaySounds(new List<TutorialBossDirector.PlaySoundParam>()
    {
      new TutorialBossDirector.PlaySoundParam(0.0f, UITutorialOperationHelper.SE_ID_THUNDERSTORM_01),
      new TutorialBossDirector.PlaySoundParam(5.53f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(6.53f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(7.56f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(8.56f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(9.56f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(11.3f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(11.93f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(13f, UITutorialOperationHelper.SE_ID_DRAGON_LANDING),
      new TutorialBossDirector.PlaySoundParam(14.7f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_01, (TutorialBossDirector.PlaySoundParam.GetPosFunc) (() => this.boss.head.position))
    }));
    this.StartCoroutine(this.WaitForTime(14.7f, (System.Action) (() => this.StartCoroutine(this.DoRadialBlur(0.6f, 0.3f, 1f)))));
    while (this.cameraAnim.isPlaying)
    {
      this.boss._rigidbody.Sleep();
      if (5.0 < (double) this.boss.head.position.y)
        this.bossShadowMaterial.SetFloat("_AlphaPower", 2.5f / this.boss.head.position.y);
      cameraTransform.position = t.position;
      cameraTransform.rotation = t.rotation;
      mainCamera.fieldOfView = t.localScale.x;
      yield return (object) null;
    }
    this.bossShadowMaterial.SetFloat("_AlphaPower", 0.5f);
    ((Component) this.boss).transform.position = Vector3.zero;
    this.player.PlayMotion("idle");
    this.player.animator.runtimeAnimatorController = this.originalPlayerAnimatorController;
    Vector3 startCameraPos = cameraTransform.position;
    Quaternion startCameraRotation = cameraTransform.rotation;
    float startFieldOfView = mainCamera.fieldOfView;
    float timer = 0.0f;
    while ((double) timer < (double) this.DURATION_TO_BATTLE_START)
    {
      timer += Time.deltaTime;
      float num = timer / this.DURATION_TO_BATTLE_START;
      cameraTransform.position = Vector3.Lerp(startCameraPos, TutorialBossDirector.CAMERA_END_POSITION, num);
      cameraTransform.rotation = Quaternion.Slerp(startCameraRotation, TutorialBossDirector.CAMERA_END_ROTAION, num);
      mainCamera.fieldOfView = Mathf.Lerp(startFieldOfView, this.originalFov, num);
      yield return (object) null;
    }
    this.boss.PlayMotion("idle");
    MonoBehaviourSingleton<InGameCameraManager>.I.ResetMovePositionAndRotaion();
    ((Behaviour) MonoBehaviourSingleton<InGameCameraManager>.I).enabled = true;
    this.bossShadow.setAnimTransform((Transform) null);
    ((Component) this.bossShadow).transform.position = ((Component) this.boss).transform.position;
    if (onComplete != null)
      onComplete();
  }

  public void StartBattleEndDirection(
    Enemy _boss,
    Character _player,
    GameObject legend,
    GameObject title_ui,
    System.Action onComplete)
  {
    this.boss = _boss;
    this.player = _player;
    this.StartBattleEndDirection(legend, title_ui, onComplete);
  }

  public void StartBattleEndDirection(GameObject legend, GameObject title_ui, System.Action onComplete)
  {
    this.titleUIPrefab = title_ui;
    this.originalPlayerAnimatorController = this.player.animator.runtimeAnimatorController;
    this.player._collider.enabled = false;
    this.player.animator.runtimeAnimatorController = this.playerAnimatorController;
    this.player.animator.cullingMode = (AnimatorCullingMode) 0;
    this.player.animator.Rebind();
    this.player._transform.position = new Vector3(0.0f, 0.0f, 26f);
    this.player._transform.eulerAngles = new Vector3(0.0f, 180f, 0.0f);
    this.player._rigidbody.constraints = (RigidbodyConstraints) 126;
    this.player.ActIdle();
    this.player.PlayMotion(this.PLAYER_ANIM_EXIT_CUT_SCENE_START_NAME);
    legend.SetActive(true);
    this.legendDragon = legend;
    Animator component = legend.GetComponent<Animator>();
    component.runtimeAnimatorController = this.legendDragonAnimController;
    component.Play(this.LEGEND_DRAGON_ANIM_STATE);
    if (Object.op_Inequality((Object) this.boss, (Object) null))
    {
      if (this.boss.colliders != null && this.boss.colliders.Length != 0)
      {
        int index = 0;
        for (int length = this.boss.colliders.Length; index < length; ++index)
        {
          if (Object.op_Inequality((Object) this.boss.colliders[index], (Object) null))
            this.boss.colliders[index].enabled = false;
        }
        this.boss._transform.position = Vector3.zero;
        this.boss._transform.eulerAngles = Vector3.zero;
        this.boss._rigidbody.constraints = (RigidbodyConstraints) 126;
        this.boss.ActIdle();
        this.boss.animator.cullingMode = (AnimatorCullingMode) 0;
        this.boss.animator.Rebind();
        this.boss.PlayMotion(this.BOSS_ANIM_EXIT_CUT_SCENE_STATE_NAME);
      }
      if (Object.op_Inequality((Object) this.boss.hip, (Object) null))
        this.bossShadow.setAnimTransform(this.boss.hip);
    }
    if (Object.op_Equality((Object) this.enemyController, (Object) null))
      this.enemyController = this.boss.controller as EnemyController;
    if (Object.op_Inequality((Object) this.enemyController, (Object) null))
      ((Behaviour) this.enemyController).enabled = false;
    this.originalFov = MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView;
    ((Component) this.cameraAnim).transform.position = Vector3.zero;
    ((Component) this.cameraAnim).transform.rotation = Quaternion.identity;
    ((Component) this.cameraAnim).transform.localScale = Vector3.zero;
    this.cameraAnim.cullingType = (AnimationCullingType) 0;
    this.cameraAnim.Play(this.BATTLE_EXIT_CAMERA_CLIP_NAME);
    this.cameraAnim.Sample();
    MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = TutorialBossDirector.CAMERA_END_POSITION;
    MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation = TutorialBossDirector.CAMERA_END_ROTAION;
    ((Behaviour) MonoBehaviourSingleton<InGameCameraManager>.I).enabled = false;
    this.StartCoroutine(this.DoBattleEndDirection(onComplete));
  }

  private IEnumerator DoBattleEndDirection(System.Action onComplete)
  {
    Transform t = ((Component) this.cameraAnim).transform;
    Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    Transform cameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
    this.StartCoroutine(this.WaitAndPlaySounds(new List<TutorialBossDirector.PlaySoundParam>()
    {
      new TutorialBossDirector.PlaySoundParam(0.0f, UITutorialOperationHelper.SE_ID_THUNDERSTORM_02),
      new TutorialBossDirector.PlaySoundParam(0.0f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_02),
      new TutorialBossDirector.PlaySoundParam(2.26f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(3.2f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(4.16f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(5.16f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(6.13f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01),
      new TutorialBossDirector.PlaySoundParam(8.65f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_03),
      new TutorialBossDirector.PlaySoundParam(11.2f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_02),
      new TutorialBossDirector.PlaySoundParam(13.2f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_04)
    }));
    bool isRequestedLogoAnimation = false;
    while (this.cameraAnim.isPlaying)
    {
      if (Object.op_Inequality((Object) this.boss, (Object) null))
      {
        if (Object.op_Inequality((Object) this.boss._rigidbody, (Object) null))
          this.boss._rigidbody.Sleep();
        if (Object.op_Inequality((Object) this.boss.head, (Object) null) && 2.5 < (double) this.boss.head.position.y)
          this.bossShadowMaterial.SetFloat("_AlphaPower", 1.25f / this.boss.head.position.y);
      }
      cameraTransform.position = t.position;
      cameraTransform.rotation = t.rotation;
      float x = t.localScale.x;
      if (1.0 < (double) x)
        mainCamera.fieldOfView = x;
      if ((double) t.localScale.z > 0.5 && !isRequestedLogoAnimation)
      {
        isRequestedLogoAnimation = true;
        this.StartLogoAnimation(true, onComplete);
      }
      yield return (object) null;
    }
    if (Object.op_Inequality((Object) this.bossShadow, (Object) null))
    {
      this.bossShadow.setAnimTransform((Transform) null);
      ((Component) this.bossShadow).transform.position = ((Component) this.boss).transform.position;
    }
    if (!isRequestedLogoAnimation)
      this.StartLogoAnimation(true, onComplete);
  }

  public void StartLogoAnimation(bool tutorial_flag, System.Action onComplete, System.Action onLoop = null)
  {
    this.StartCoroutine(this.DoStartLogoAnimation(tutorial_flag, onComplete, onLoop));
  }

  public void InitLogo()
  {
    ((Component) this.logo.camera).gameObject.SetActive(false);
    ((Component) this.logo.eye).transform.localScale = Vector3.zero;
    Material material1 = this.logo.fader.material;
    Material material2 = this.logo.logo.material;
    Color color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    material1.SetColor("_Color", color);
    material2.SetFloat("_AlphaRate", -1f);
    material2.SetFloat("_BlendRate", 0.0f);
    this.logo.effect1.SetActive(false);
    this.logo.bg.SetActive(false);
    if (this.effects == null)
      return;
    for (int index = 0; index < this.effects.Length; ++index)
    {
      if (Object.op_Inequality((Object) this.effects[index], (Object) null))
      {
        Object.Destroy((Object) this.effects[index]);
        this.effects[index] = (GameObject) null;
      }
    }
  }

  private IEnumerator DoFadeOut()
  {
    Material faderMat = this.logo.fader.material;
    float timer = 0.0f;
    while ((double) timer < 0.30000001192092896)
    {
      timer += Time.deltaTime;
      faderMat.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, Mathf.Clamp01(timer / 0.3f)));
      yield return (object) null;
    }
    if (Object.op_Inequality((Object) this.legendDragon, (Object) null))
      this.legendDragon.SetActive(false);
  }

  private IEnumerator DoStartLogoAnimation(bool tutorial_flag, System.Action onComplete, System.Action onLoop)
  {
    this.logo.root.position = Vector3.op_Multiply(Vector3.up, 1000f);
    this.logo.root.rotation = Quaternion.identity;
    this.logo.root.localScale = Vector3.one;
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyTitleTop)
    {
      DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
      this.logo.camera.orthographicSize = specialDeviceInfo.TitleTopCameraSize;
      this.logo.bg.transform.localScale = specialDeviceInfo.TitleTopBGScale;
    }
    ((Component) this.logo.camera).gameObject.SetActive(true);
    Material faderMat = this.logo.fader.material;
    Material logoMat = this.logo.logo.material;
    this.logo.camera.depth = -1f;
    this.logo.dragonRoot.SetActive(false);
    Color dragonPlaneColor = new Color(1f, 1f, 1f, 0.0f);
    this.logo.dragonPlane.sharedMaterial.SetColor("Color", dragonPlaneColor);
    float timer = 0.0f;
    if (tutorial_flag)
    {
      this.StartCoroutine(this.DoFadeOut());
      SoundManager.RequestBGM(11, false);
      while (MonoBehaviourSingleton<SoundManager>.I.playingBGMID != 11 || MonoBehaviourSingleton<SoundManager>.I.changingBGM)
        yield return (object) null;
      yield return (object) new WaitForSeconds(2.3f);
    }
    else
      faderMat.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, 0.0f));
    this.effects = new GameObject[this.titleEffectPrefab.Length];
    for (int index = 0; index < this.titleEffectPrefab.Length; ++index)
    {
      rymFX component = ((Component) ResourceUtility.Realizes((Object) this.titleEffectPrefab[index])).GetComponent<rymFX>();
      component.Cameras = new Camera[1]{ this.logo.camera };
      component._transform.localScale = Vector3.op_Multiply(component._transform.localScale, 10f);
      component._transform.position = index != 1 ? ((Component) this.logo.eye).transform.position : new Vector3(0.568f, 999.946f, 0.1f);
      this.effects[index] = ((Component) component).gameObject;
    }
    yield return (object) new WaitForSeconds(1f);
    timer = 0.0f;
    while ((double) timer < 0.17000000178813934)
    {
      timer += Time.deltaTime;
      ((Component) this.logo.eye).transform.localScale = Vector3.op_Multiply(Vector3.op_Multiply(Vector3.one, Mathf.Clamp01(timer / 0.17f)), 10f);
      logoMat.SetFloat("_AlphaRate", (float) ((double) timer * 2.0 - 1.0));
      yield return (object) null;
    }
    this.logo.dragonRoot.SetActive(true);
    while ((double) timer < 1.0)
    {
      timer += Time.deltaTime;
      logoMat.SetFloat("_AlphaRate", (float) ((double) timer * 2.0 - 1.0));
      dragonPlaneColor.a = timer;
      this.logo.dragonPlane.sharedMaterial.SetColor("Color", dragonPlaneColor);
      yield return (object) null;
    }
    dragonPlaneColor.a = 1f;
    this.logo.dragonPlane.sharedMaterial.SetColor("Color", dragonPlaneColor);
    timer = 0.0f;
    while ((double) timer < 0.5)
    {
      timer += Time.deltaTime;
      logoMat.SetFloat("_BlendRate", timer * 2f);
      yield return (object) null;
    }
    this.logo.bg.SetActive(true);
    this.logo.effect1.SetActive(true);
    timer = 0.0f;
    Material bgMaterial = this.logo.bgFader.material;
    while ((double) timer < 0.699999988079071)
    {
      timer += Time.deltaTime;
      bgMaterial.color = new Color(1f, 1f, 1f, (float) (1.0 - (double) timer / 0.699999988079071));
      yield return (object) null;
    }
    if (!tutorial_flag)
    {
      if (onLoop != null)
        onLoop();
      while (!tutorial_flag)
        yield return (object) null;
    }
    yield return (object) new WaitForSeconds(0.3f);
    if (Object.op_Inequality((Object) this.titleUIPrefab, (Object) null))
    {
      Transform transform1 = ResourceUtility.Realizes((Object) this.titleUIPrefab, MonoBehaviourSingleton<UIManager>.I.uiRootTransform, 5);
      if (Object.op_Inequality((Object) transform1, (Object) null))
      {
        Transform transform2 = Utility.Find(transform1, "BTN_START");
        if (Object.op_Inequality((Object) transform2, (Object) null))
          ((Component) transform2).GetComponent<Collider>().enabled = false;
        Transform transform3 = Utility.Find(transform1, "BTN_ADVANCED_LOGIN");
        if (Object.op_Inequality((Object) transform3, (Object) null))
          ((Component) transform3).gameObject.SetActive(false);
        Transform transform4 = Utility.Find(transform1, "BTN_CLEARCACHE");
        if (Object.op_Inequality((Object) transform4, (Object) null))
          ((Component) transform4).gameObject.SetActive(false);
      }
    }
    yield return (object) new WaitForSeconds(6f);
    timer = 0.0f;
    while ((double) timer < 0.30000001192092896)
    {
      timer += Time.deltaTime;
      faderMat.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, timer / 0.3f));
      yield return (object) null;
    }
    MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
    for (int index = 0; index < this.effects.Length; ++index)
      EffectManager.ReleaseEffect(this.effects[index]);
    if (onComplete != null)
      onComplete();
  }

  private void LateUpdate()
  {
    Transform transform = ((Component) this.cameraAnim).transform;
    if (!MonoBehaviourSingleton<AppMain>.IsValid())
      return;
    if (!this.replaceCameraRoationWithCutSceneRotation)
    {
      if ((double) transform.localScale.y <= 0.10000000149011612)
        return;
      this.replaceCameraRoationWithCutSceneRotation = true;
      this.cutChangePosition = transform.position;
      this.cutChangeRotation = transform.rotation;
    }
    else
    {
      if (!this.replaceCameraRoationWithCutSceneRotation)
        return;
      if ((double) transform.localScale.y > 0.10000000149011612)
      {
        if (!MonoBehaviourSingleton<AppMain>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<AppMain>.I.mainCameraTransform, (Object) null))
          return;
        MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = this.cutChangePosition;
        MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation = this.cutChangeRotation;
      }
      else
        this.replaceCameraRoationWithCutSceneRotation = false;
    }
  }

  private IEnumerator DoRadialBlur(float startDuration, float endDuration, float maxStrength)
  {
    this.radialBlurFilter.StartFilter();
    float timer = 0.0f;
    Camera camera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    while ((double) timer < (double) startDuration)
    {
      timer += Time.deltaTime;
      this.radialBlurFilter.strength = Mathf.Lerp(0.0f, maxStrength, timer / 0.6f);
      this.radialBlurFilter.SetCenter(Vector2.op_Implicit(camera.WorldToViewportPoint(this.boss.head.position)));
      yield return (object) null;
    }
    timer = 0.0f;
    while ((double) timer < (double) endDuration)
    {
      timer += Time.deltaTime;
      this.radialBlurFilter.strength = Mathf.Lerp(maxStrength, 0.0f, timer / 0.3f);
      this.radialBlurFilter.SetCenter(Vector2.op_Implicit(camera.WorldToViewportPoint(this.boss.head.position)));
      yield return (object) null;
    }
    this.radialBlurFilter.StopFilter();
  }

  private IEnumerator WaitForTime(float waitTime, System.Action action)
  {
    yield return (object) new WaitForSeconds(waitTime);
    action();
  }

  [Serializable]
  public class Logo
  {
    public Transform root;
    public Camera camera;
    public Renderer logo;
    public Renderer eye;
    public Renderer fader;
    public GameObject bg;
    public Renderer bgFader;
    public GameObject effect1;
    public GameObject dragonRoot;
    public Renderer dragonPlane;
  }

  private class PlaySoundParam
  {
    public TutorialBossDirector.PlaySoundParam.GetPosFunc func;
    public float time;
    public int id = -1;

    public PlaySoundParam(
      float _time,
      int _id,
      TutorialBossDirector.PlaySoundParam.GetPosFunc _func = null)
    {
      this.time = _time;
      this.id = _id;
      this.func = _func;
    }

    public delegate Vector3 GetPosFunc();
  }
}
