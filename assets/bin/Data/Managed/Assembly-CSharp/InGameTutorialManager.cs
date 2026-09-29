// Decompiled with JetBrains decompiler
// Type: InGameTutorialManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class InGameTutorialManager : MonoBehaviour
{
  public float TutorialMoveTime;
  private static readonly float SHOW_BOT_CAM_TIME = 8f;
  private static readonly float SHOW_BOT_CAM_TIME_RATIO_PHASE1 = 1.5f;
  private static readonly float SHOW_BOT_CAM_FOV_PHASE1 = 50f;
  private static readonly float SHOW_BOT_CAM_TIME_RATE = 0.1f;
  private static readonly float SHOW_BOT_CAM_Y = 4f;
  private static readonly float SHOW_BOT_CAM_TIME_RATIO_PHASE2 = 2f;
  private static readonly float SHOW_WELCOME_LOG_TIME = 3f;
  private static readonly float BOT_POSE_TIME = 4f;
  private static readonly float SECOND_SKILL_TIME = 20f;
  private static readonly float FIRST_SKILL_TIME_OFFSET = 10f;
  private static readonly float SHOW_BOT_SKILL_CAM_COORDINATE_OFFSET = 2.5f;
  private static readonly float SHOW_BOT_SKILL_TIME = 2f;
  public static readonly float DURATION_DISP_DIALOG = 2f;
  public static readonly float DURATION_DISP_COMPLETE = 2f;
  private static readonly float MAX_APEAR_POS_X = 23f;
  private static readonly float MAX_APEAR_POS_Z = 23f;
  private List<InGameTutorialManager.EnemyHolder> poppedEnemies = new List<InGameTutorialManager.EnemyHolder>(10);
  private InGameTutorialManager.State current;
  private Object dialogPrefab;
  private Object helperPrefab;
  private Object bossDirectorPrefab;
  private UITutorialDialog dialogWindow;
  private UITutorialOperationHelper helperUI;
  private TutorialBossDirector bossDirector;
  private Transform mdlArrow;
  public const int BATTLE_END_STORY_ID = 11000001;
  public const int CHARAMAKE_END_STORY_ID = 11000002;
  private Object legendDragonPrefab;
  private GameObject _legendDragon;
  private Object appearEffectPrefab;
  private GameObject _appearEffect;

  public Enemy boss { set; get; }

  public UITutorialDialog dialog
  {
    get
    {
      if (Object.op_Equality((Object) this.dialogWindow, (Object) null))
      {
        this.dialogWindow = ((Component) ResourceUtility.Realizes(this.dialogPrefab)).GetComponent<UITutorialDialog>();
        this.dialogWindow.Close();
      }
      return this.dialogWindow;
    }
  }

  public UITutorialOperationHelper helper
  {
    get
    {
      if (Object.op_Equality((Object) this.helperUI, (Object) null))
        this.helperUI = ((Component) ResourceUtility.Realizes(this.helperPrefab)).GetComponent<UITutorialOperationHelper>();
      return this.helperUI;
    }
  }

  public TutorialBossDirector director
  {
    get
    {
      if (Object.op_Equality((Object) this.bossDirector, (Object) null))
        this.bossDirector = ((Component) ResourceUtility.Realizes(this.bossDirectorPrefab)).GetComponent<TutorialBossDirector>();
      return this.bossDirector;
    }
  }

  public GameObject legendDragon
  {
    get
    {
      if (Object.op_Equality((Object) this._legendDragon, (Object) null))
        this._legendDragon = ((Component) ResourceUtility.Realizes(this.legendDragonPrefab)).gameObject;
      return this._legendDragon;
    }
  }

  public GameObject titleUIPrefab { get; set; }

  public Object targetAreaPrefab { get; set; }

  public GameObject appearEffect
  {
    get
    {
      if (Object.op_Equality((Object) this._appearEffect, (Object) null))
        this._appearEffect = ((Component) ResourceUtility.Realizes(this.appearEffectPrefab)).gameObject;
      return this._appearEffect;
    }
  }

  private IEnumerator Start()
  {
    PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, true);
    while (MonoBehaviourSingleton<LoadingProcess>.IsValid())
      yield return (object) null;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedDialog = loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialDialog");
    LoadObject loadedTargetArea = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_tutorial_area_01");
    LoadObject loadedHelper = loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialOperationHelper");
    LoadObject loadedDirector = loadingQueue.Load(RESOURCE_CATEGORY.CUTSCENE, "InGameTutorialDirector");
    LoadObject loadedDragon = loadingQueue.Load(RESOURCE_CATEGORY.ENEMY_MODEL, "ENM01_Legend");
    loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[1]
    {
      "mdl_arrow_01"
    });
    LoadObject loadedAppear = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_battle_start_01");
    loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_bow_01_01");
    MonoBehaviourSingleton<UIManager>.I.LoadUI(false, false, true);
    loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_COMPLETE);
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.dialogPrefab = loadedDialog.loadedObject;
    this.targetAreaPrefab = loadedTargetArea.loadedObject;
    this.helperPrefab = loadedHelper.loadedObject;
    this.bossDirectorPrefab = loadedDirector.loadedObject;
    this.legendDragonPrefab = loadedDragon.loadedObject;
    this.appearEffectPrefab = loadedAppear.loadedObject;
    while (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.portalObjectList == null)
      yield return (object) null;
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      ((Component) MonoBehaviourSingleton<UIPlayerStatus>.I).gameObject.SetActive(false);
    if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
      ((Component) MonoBehaviourSingleton<UIEnduranceStatus>.I).gameObject.SetActive(false);
    InGameMain currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain;
    if (Object.op_Inequality((Object) currentSection, (Object) null))
    {
      Transform transform1 = currentSection._transform.Find("InGameMain/PlayerStatus");
      if (Object.op_Inequality((Object) transform1, (Object) null))
        ((Component) transform1).gameObject.SetActive(false);
      Transform transform2 = currentSection._transform.Find("InGameMain/StaticSwitchPanel/NewMenuParent");
      if (Object.op_Inequality((Object) transform2, (Object) null))
        ((Component) transform2).gameObject.SetActive(false);
      Transform transform3 = currentSection._transform.Find("InGameMain/StaticSwitchPanel/ChatButtonParent");
      if (Object.op_Inequality((Object) transform3, (Object) null))
        ((Component) transform3).gameObject.SetActive(false);
    }
    List<PortalObject> portalObjectList = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList;
    int num = 0;
    while (num < portalObjectList.Count)
      ++num;
    this.Change((InGameTutorialManager.State) new InGameTutorialManager.TutorialBattle(this));
    this.isCompleteLoadSE = false;
    PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, false);
    ResourceManager.autoRetry = true;
  }

  public void UpdateArrowModel()
  {
    if (Object.op_Equality((Object) null, (Object) this.mdlArrow))
      return;
    float num1 = 2.5f;
    float num2 = Vector3.Distance(this.mdlArrow.position, ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).transform.position);
    float num3 = 10.0 <= (double) num2 ? num1 + (float) (0.05000000074505806 * ((double) num2 - (double) num1)) : num1;
    this.mdlArrow.localScale = new Vector3(num3, num3, num3);
  }

  private void Update()
  {
    if (this.current == null)
      return;
    this.current.Update();
  }

  private void FixedUpdate()
  {
    if (this.current == null)
      return;
    this.current.FixedUpdate();
  }

  private void OnDestroy()
  {
    ResourceManager.autoRetry = false;
    if (Object.op_Inequality((Object) this.helper, (Object) null))
      Object.Destroy((Object) ((Component) this.helper).gameObject);
    if (Object.op_Inequality((Object) this.dialog, (Object) null))
      Object.Destroy((Object) ((Component) this.dialog).gameObject);
    if (!Object.op_Inequality((Object) null, (Object) this.mdlArrow))
      return;
    Object.Destroy((Object) ((Component) this.mdlArrow).gameObject);
  }

  public void ClearedPoppedEnemiesInfo() => this.poppedEnemies.Clear();

  public Enemy PopEnemy(Vector3 pos, bool setAI = true, float xMax = -1f, float zMax = -1f)
  {
    FieldMapTable.EnemyPopTableData enemyPopData = Singleton<FieldMapTable>.I.GetEnemyPopData(MonoBehaviourSingleton<FieldManager>.I.currentMapID, 0);
    if (enemyPopData == null)
      return (Enemy) null;
    int enemy_id = (int) enemyPopData.enemyID;
    int enemy_lv = (int) enemyPopData.enemyLv;
    if (enemy_id == 0 && QuestManager.IsValidInGame())
    {
      enemy_id = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyID();
      enemy_lv = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyLv();
    }
    if (0.0 < (double) xMax)
      pos.x = Mathf.Clamp(pos.x, -xMax, xMax);
    if (0.0 < (double) zMax)
      pos.z = Mathf.Clamp(pos.z, -zMax, zMax);
    if ((double) pos.z < -29.5)
      pos.z = -29.5f;
    Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(0, pos, 0.0f, enemy_id, enemy_lv, false, false, setAI, callback: (EnemyLoader.OnCompleteLoad) (target =>
    {
      if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
        MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(target.id, target.hpMax);
      if (setAI)
        return;
      EnemyController component = ((Component) target).GetComponent<EnemyController>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      Object.Destroy((Object) component);
    }));
    this.poppedEnemies.Add(new InGameTutorialManager.EnemyHolder()
    {
      enemy = enemy
    });
    return enemy;
  }

  public void SetActiveAllEnemiesController(bool active)
  {
    for (int index = 0; index < this.poppedEnemies.Count; ++index)
    {
      if (((Component) this.poppedEnemies[index].enemy).gameObject.activeInHierarchy)
        ((Behaviour) this.poppedEnemies[index].enemy).enabled = active;
    }
  }

  public bool CheckAllEnemiesDead()
  {
    for (int index = 0; index < this.poppedEnemies.Count; ++index)
    {
      if (((Component) this.poppedEnemies[index].enemy).gameObject.activeInHierarchy)
        return false;
    }
    return true;
  }

  public void CheckAndCallOnDeadAll(Action<Enemy> onDead)
  {
    for (int index = 0; index < this.poppedEnemies.Count; ++index)
      this.poppedEnemies[index].CheckAndCallOndead(onDead);
  }

  public T GetState<T>() where T : class => this.current as T;

  public void Change(InGameTutorialManager.State next)
  {
    if (this.current != null)
      this.current.Final();
    next?.Init();
    this.current = next;
  }

  private IEnumerator WaitForTime(float waitTime, System.Action action)
  {
    yield return (object) new WaitForSeconds(waitTime);
    action();
  }

  public void LoadSE()
  {
    this.isLoadingSE = true;
    this.StartCoroutine("DoLoadSE");
  }

  private IEnumerator DoLoadSE()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_THUNDERSTORM_01);
    loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01);
    loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_LANDING);
    loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_01);
    loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_THUNDERSTORM_02);
    loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_02);
    loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_03);
    loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_02);
    loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_04);
    loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_TITLELOGO);
    while (loadingQueue.IsLoading())
      yield return (object) null;
    this.isLoadingSE = false;
    this.isCompleteLoadSE = true;
  }

  public bool isLoadingSE { get; private set; }

  public bool isCompleteLoadSE { get; private set; }

  public enum CHAT_VOICE
  {
    CH00 = 17, // 0x00000011
    CH02 = 10018, // 0x00002722
    CH01 = 200016, // 0x00030D50
  }

  public class EnemyHolder
  {
    public bool isCalledOndeadHandler;
    public Enemy enemy;

    public void CheckAndCallOndead(Action<Enemy> onDead)
    {
      if (this.enemy.actionID != Character.ACTION_ID.DEAD || this.isCalledOndeadHandler)
        return;
      this.isCalledOndeadHandler = true;
      if (onDead == null)
        return;
      onDead(this.enemy);
    }
  }

  public class State
  {
    protected InGameTutorialManager tutorialManager;
    private SelfController _selfController;
    private Character _character;

    protected SelfController selfController
    {
      get
      {
        if (Object.op_Equality((Object) this._selfController, (Object) null))
          this._selfController = MonoBehaviourSingleton<StageObjectManager>.I.self.controller as SelfController;
        return this._selfController;
      }
    }

    protected Character character
    {
      get
      {
        if (Object.op_Equality((Object) this._character, (Object) null))
          this._character = ((Component) this.selfController).GetComponent<Character>();
        return this._character;
      }
    }

    public State(InGameTutorialManager owner) => this.tutorialManager = owner;

    public virtual void Init()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Final()
    {
    }
  }

  public class TutorialMove(InGameTutorialManager owner) : InGameTutorialManager.State(owner)
  {
    private PuniConManager puniconManager;
    private InputManager.TouchInfo touchInfo = new InputManager.TouchInfo();
    private Transform targetAreaObject;
    private UISprite finger;
    private float timer;
    private readonly float TO_END_POINT_DURATION = 0.5f;
    private readonly float WAIT_AUTO_ROTATIN_START = 2f;
    private readonly float TOTAL_AUTO_CONTROL_TIME = 4f;
    private InGameTutorialManager.TutorialMove.Phase currentPhase;
    private readonly Vector3 TARGET_AREA_POSITION = new Vector3(0.0f, 0.0f, -3f);
    private readonly float WAIT_DRAG_DURATION = 1f;
    private readonly float HELP_TEXTURE_DISPLAY_TIME = 3.5f;
    private readonly float TARGET_AREA_RANGE = 1.2f;
    private readonly float MOVE_TRAINING_TIME = 1.5f;
    private readonly float ROLLING_TRAINING_WAIT_TIME = 1f;

    public override void Init()
    {
      Player character = this.character as Player;
      character.SetDiableAction(Character.ACTION_ID.MOVE, false);
      character.SetDiableAction(Character.ACTION_ID.ATTACK, true);
      character.SetDiableAction(Character.ACTION_ID.MAX, true);
      character.SetDiableAction((Character.ACTION_ID) 33, true);
      character.SetDiableAction((Character.ACTION_ID) 19, true);
      this.puniconManager = MonoBehaviourSingleton<PuniConManager>.I;
      this.targetAreaObject = ResourceUtility.Realizes(this.tutorialManager.targetAreaPrefab);
      this.targetAreaObject.position = this.TARGET_AREA_POSITION;
      ((Component) this.targetAreaObject).gameObject.SetActive(false);
      this.currentPhase = InGameTutorialManager.TutorialMove.Phase.WAIT_FOR_DISP_GREETING;
      this.tutorialManager.Update();
      MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_loading_end, "Tutorial");
    }

    public override void Update()
    {
      switch (this.currentPhase)
      {
        case InGameTutorialManager.TutorialMove.Phase.WAIT_FOR_DISP_GREETING:
          this.WaitForDispGreeting();
          break;
        case InGameTutorialManager.TutorialMove.Phase.GREETING_TO_THE_WORLD:
          this.GGGreetingToTheWorld();
          break;
        case InGameTutorialManager.TutorialMove.Phase.WAIT_AUCO_CONTROL_START:
          this.WaitAutoControlStart();
          break;
        case InGameTutorialManager.TutorialMove.Phase.AUTO_DRAGGING:
          this.AutoDragging();
          break;
        case InGameTutorialManager.TutorialMove.Phase.DISP_PLAYER_CONTROL_DIALOG:
          this.DispPlayerControlDialog();
          break;
        case InGameTutorialManager.TutorialMove.Phase.PLAYER_MOVE_TRAINING:
          this.PlayerMoveTraining();
          break;
        case InGameTutorialManager.TutorialMove.Phase.FINISH_AND_WAIT_DIALOG:
          this.FinishAndWaitDialog();
          break;
        case InGameTutorialManager.TutorialMove.Phase.SHOW_DIALOG_MOVE:
          this.ShowDialogMove();
          break;
        case InGameTutorialManager.TutorialMove.Phase.SHOW_DIALOG_ROLLING_WAIT:
          this.ShowDialogRollingWait();
          break;
        case InGameTutorialManager.TutorialMove.Phase.SHOW_DIALOG_ROLLING:
          this.ShowDialogRolling();
          break;
        case InGameTutorialManager.TutorialMove.Phase.GG_TUTORIAL_MOVE:
          this.GGTutorialMove();
          break;
        case InGameTutorialManager.TutorialMove.Phase.GG_DONE_TUTORIAL_MOVE:
          this.GGTutorialMoveDone();
          break;
      }
    }

    private void WaitForDispGreeting()
    {
      this.timer += Time.deltaTime;
      if ((double) this.timer < 1.0)
        return;
      this.timer = 0.0f;
      this.tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0001", 0, "Tutorial_Move_Text_0002");
      this.tutorialManager.dialog.OpenThreeLineLabel();
      this.currentPhase = InGameTutorialManager.TutorialMove.Phase.GREETING_TO_THE_WORLD;
    }

    private void GreetingToTheWorld()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG > (double) this.timer)
        return;
      this.currentPhase = InGameTutorialManager.TutorialMove.Phase.NONE;
      this.tutorialManager.dialog.Close(1, (System.Action) (() =>
      {
        this.timer = 0.0f;
        this.tutorialManager.helper.moveHelper.ShowHelpText();
        UITweenCtrl component = ((Component) this.tutorialManager.helper.fingerMove).GetComponent<UITweenCtrl>();
        if (Object.op_Inequality((Object) null, (Object) component))
        {
          ((Component) component).gameObject.SetActive(true);
          component.Play();
        }
        this.tutorialManager.StartCoroutine(this.tutorialManager.WaitForTime(2.5f, (System.Action) (() => this.tutorialManager.helper.moveHelper.HideHelpText((System.Action) (() =>
        {
          ((Component) this.tutorialManager.helper.fingerMove).gameObject.SetActive(false);
          this.tutorialManager.StartCoroutine(this.tutorialManager.WaitForTime(0.5f, (System.Action) (() =>
          {
            this.tutorialManager.helper.moveHelper.ShowHelpPicture();
            this.tutorialManager.StartCoroutine(this.tutorialManager.WaitForTime(InGameTutorialManager.DURATION_DISP_DIALOG, (System.Action) (() => this.tutorialManager.helper.moveHelper.HideHelpPicture((System.Action) (() => this.currentPhase = InGameTutorialManager.TutorialMove.Phase.SHOW_DIALOG_MOVE)))));
          })));
        })))));
      }));
    }

    private void GGGreetingToTheWorld()
    {
      this.timer += Time.deltaTime;
      if (3.5 > (double) this.timer)
        return;
      this.currentPhase = InGameTutorialManager.TutorialMove.Phase.NONE;
      this.tutorialManager.dialog.Close(1, (System.Action) (() =>
      {
        this.timer = 0.0f;
        this.currentPhase = InGameTutorialManager.TutorialMove.Phase.GG_TUTORIAL_MOVE;
        this.tutorialManager.dialog.HideThreeLineLabel();
      }));
    }

    private void GGTutorialMove()
    {
      if ((double) this.tutorialManager.TutorialMoveTime >= 2.5)
      {
        this.tutorialManager.helper.moveHelper.HideHelpText();
        UITweenCtrl component = ((Component) this.tutorialManager.helper.fingerMove).GetComponent<UITweenCtrl>();
        if (Object.op_Inequality((Object) null, (Object) component))
          ((Component) component).gameObject.SetActive(false);
        this.tutorialManager.helper.commonHelper.ShowGoodJob();
        this.currentPhase = InGameTutorialManager.TutorialMove.Phase.GG_DONE_TUTORIAL_MOVE;
        this.timer = 0.0f;
      }
      else
      {
        this.timer += Time.deltaTime;
        if ((double) this.timer < 1.0)
          return;
        this.currentPhase = InGameTutorialManager.TutorialMove.Phase.NONE;
        this.tutorialManager.helper.moveHelper.ShowHelpText();
        UITweenCtrl ctrl = ((Component) this.tutorialManager.helper.fingerMove).GetComponent<UITweenCtrl>();
        if (!Object.op_Inequality((Object) null, (Object) ctrl))
          return;
        ((Component) ctrl).gameObject.SetActive(true);
        ctrl.Reset();
        ctrl.Play(onFinished: (EventDelegate.Callback) (() => this.tutorialManager.StartCoroutine(this.tutorialManager.WaitForTime(1f, (System.Action) (() => this.tutorialManager.helper.moveHelper.HideHelpText((System.Action) (() =>
        {
          ((Component) ctrl).gameObject.SetActive(false);
          this.currentPhase = InGameTutorialManager.TutorialMove.Phase.GG_TUTORIAL_MOVE;
          this.timer = 0.0f;
        })))))));
      }
    }

    private void GGTutorialMoveDone()
    {
      this.timer += Time.deltaTime;
      if (1.0 >= (double) this.timer)
        return;
      this.tutorialManager.helper.commonHelper.HideGoodJob();
      this.tutorialManager.Change((InGameTutorialManager.State) new InGameTutorialManager.TutorialAttack(this.tutorialManager));
    }

    private void WaitAutoControlStart()
    {
      this.timer += Time.deltaTime;
      if ((double) this.WAIT_DRAG_DURATION < (double) this.timer)
      {
        this.timer = 0.0f;
        this.touchInfo.id = 1;
        this.touchInfo.beginPosition = new Vector2((float) (Screen.width / 2), (float) (Screen.height / 4));
        this.touchInfo.position = this.touchInfo.beginPosition;
        this.puniconManager.OnTouchOn(this.touchInfo);
        this.finger = this.tutorialManager.helper.commonHelper.ShowFinger();
        this.currentPhase = InGameTutorialManager.TutorialMove.Phase.AUTO_DRAGGING;
      }
      this.puniconManager.OnDrag(this.touchInfo);
    }

    private void AutoDragging()
    {
      this.timer += Time.deltaTime;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector((float) (-100.0 * ((double) Screen.width / 480.0)), (float) (100.0 * ((double) Screen.height / 640.0)));
      Vector2 v = Vector2.Lerp(Vector2.zero, vector2_1, this.timer / this.TO_END_POINT_DURATION);
      if ((double) this.timer >= (double) this.WAIT_AUTO_ROTATIN_START)
      {
        float degrees = Mathf.Lerp(0.0f, -90f, this.timer - this.WAIT_AUTO_ROTATIN_START);
        this.Rotate(ref v, degrees);
      }
      this.touchInfo.position = Vector2.op_Addition(this.touchInfo.beginPosition, v);
      this.finger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(new Vector3(this.touchInfo.position.x, this.touchInfo.position.y, MonoBehaviourSingleton<AppMain>.I.mainCamera.nearClipPlane));
      Vector2 vector2_2 = Vector2.op_Subtraction(this.touchInfo.position, this.touchInfo.beginPosition);
      this.touchInfo.axis = ((Vector2) ref vector2_2).normalized;
      this.puniconManager.OnDrag(this.touchInfo);
      this.PlayerMove();
      if ((double) this.TOTAL_AUTO_CONTROL_TIME > (double) this.timer)
        return;
      this.timer = 0.0f;
      this.character.ActIdle();
      this.puniconManager.OnTouchOff(this.touchInfo);
      this.tutorialManager.helper.commonHelper.HideFinger();
      this.tutorialManager.helper.commonHelper.HideAutoControlMark();
      this.tutorialManager.helper.moveHelper.HideHelpText((System.Action) (() =>
      {
        ((Component) this.targetAreaObject).gameObject.SetActive(true);
        this.tutorialManager.helper.moveHelper.ShowHelpPicture();
        MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
      }));
      this.currentPhase = InGameTutorialManager.TutorialMove.Phase.DISP_PLAYER_CONTROL_DIALOG;
    }

    private void DispPlayerControlDialog()
    {
      this.timer += Time.deltaTime;
      if ((double) this.HELP_TEXTURE_DISPLAY_TIME >= (double) this.timer)
        return;
      this.tutorialManager.helper.moveHelper.HideHelpPicture((System.Action) (() =>
      {
        this.tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0004", 0, "Tutorial_Move_Text_0005");
        ((Behaviour) this.selfController).enabled = true;
      }));
      this.currentPhase = InGameTutorialManager.TutorialMove.Phase.PLAYER_MOVE_TRAINING;
    }

    private void PlayerMoveTraining()
    {
      Vector3 vector3 = Vector3.op_Subtraction(this.character._transform.position, this.TARGET_AREA_POSITION);
      if ((double) ((Vector3) ref vector3).sqrMagnitude >= (double) this.TARGET_AREA_RANGE * (double) this.TARGET_AREA_RANGE)
        return;
      if (Object.op_Inequality((Object) this.targetAreaObject, (Object) null))
      {
        EffectManager.ReleaseEffect(((Component) this.targetAreaObject).gameObject);
        this.targetAreaObject = (Transform) null;
      }
      this.currentPhase = InGameTutorialManager.TutorialMove.Phase.NONE;
      ((Behaviour) this.selfController).enabled = false;
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
      this.tutorialManager.dialog.Close(1, (System.Action) (() =>
      {
        this.tutorialManager.helper.commonHelper.ShowComplete();
        this.currentPhase = InGameTutorialManager.TutorialMove.Phase.FINISH_AND_WAIT_DIALOG;
        this.timer = 0.0f;
      }));
    }

    private void FinishAndWaitDialog()
    {
      this.timer += Time.deltaTime;
      if (1.0 >= (double) this.timer)
        return;
      this.tutorialManager.helper.commonHelper.HideComplete();
      this.tutorialManager.Change((InGameTutorialManager.State) new InGameTutorialManager.TutorialAttack(this.tutorialManager));
    }

    private void ShowDialogMove()
    {
      this.timer += Time.deltaTime;
      if ((double) this.MOVE_TRAINING_TIME >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.tutorialManager.helper.moveHelper.HideHelpPicture();
      ((Component) this.tutorialManager.helper.fingerMove).gameObject.SetActive(false);
      this.tutorialManager.Change((InGameTutorialManager.State) new InGameTutorialManager.TutorialBattle(this.tutorialManager));
    }

    private void ShowDialogRollingWait()
    {
      this.timer += Time.deltaTime;
      if ((double) this.ROLLING_TRAINING_WAIT_TIME >= (double) this.timer)
        return;
      this.tutorialManager.helper.avoidHelper.ShowHelpText();
      this.currentPhase = InGameTutorialManager.TutorialMove.Phase.SHOW_DIALOG_ROLLING;
      this.timer = 0.0f;
      UITweenCtrl component = ((Component) this.tutorialManager.helper.fingerRolling).GetComponent<UITweenCtrl>();
      if (!Object.op_Inequality((Object) null, (Object) component))
        return;
      ((Component) component).gameObject.SetActive(true);
      component.Play();
    }

    private void ShowDialogRolling()
    {
      this.timer += Time.deltaTime;
      if ((double) this.MOVE_TRAINING_TIME >= (double) this.timer)
        return;
      this.tutorialManager.helper.avoidHelper.HideHelpText();
      this.currentPhase = InGameTutorialManager.TutorialMove.Phase.FINISH_AND_WAIT_DIALOG;
      this.tutorialManager.Change((InGameTutorialManager.State) new InGameTutorialManager.TutorialBattle(this.tutorialManager));
      this.timer = 0.0f;
      ((Component) this.tutorialManager.helper.fingerRolling).gameObject.SetActive(false);
    }

    public override void Final()
    {
      if (Object.op_Inequality((Object) this.targetAreaObject, (Object) null))
      {
        EffectManager.ReleaseEffect(((Component) this.targetAreaObject).gameObject);
        this.targetAreaObject = (Transform) null;
      }
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
    }

    private void PlayerMove()
    {
      InGameSettingsManager.SelfController parameter = this.selfController.parameter;
      Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
      Vector3 right = cameraTransform.right;
      Vector3 forward = cameraTransform.forward;
      forward.y = 0.0f;
      ((Vector3) ref forward).Normalize();
      Vector3 velocity = Vector3.op_Addition(Vector3.op_Multiply(Vector3.op_Multiply(right, this.touchInfo.axis.x), parameter.moveForwardSpeed), Vector3.op_Multiply(Vector3.op_Multiply(forward, this.touchInfo.axis.y), parameter.moveForwardSpeed));
      Character.MOTION_ID motion_id = Character.MOTION_ID.WALK;
      this.character.ActMoveVelocity(parameter.enableRootMotion ? Vector3.zero : velocity, parameter.moveForwardSpeed, motion_id);
      this.character.SetLerpRotation(velocity);
    }

    private void Rotate(ref Vector2 v, float degrees)
    {
      float num1 = Mathf.Sin(degrees * ((float) Math.PI / 180f));
      float num2 = Mathf.Cos(degrees * ((float) Math.PI / 180f));
      float x = v.x;
      float y = v.y;
      v.x = (float) ((double) num2 * (double) x - (double) num1 * (double) y);
      v.y = (float) ((double) num1 * (double) x + (double) num2 * (double) y);
    }

    private enum Phase
    {
      NONE,
      WAIT_FOR_DISP_GREETING,
      GREETING_TO_THE_WORLD,
      WAIT_AUCO_CONTROL_START,
      AUTO_DRAGGING,
      DISP_PLAYER_CONTROL_DIALOG,
      PLAYER_MOVE_TRAINING,
      FINISH_AND_WAIT_DIALOG,
      SHOW_DIALOG_MOVE,
      SHOW_DIALOG_ROLLING_WAIT,
      SHOW_DIALOG_ROLLING,
      GG_TUTORIAL_MOVE,
      GG_DONE_TUTORIAL_MOVE,
    }
  }

  public class TutorialRolling(InGameTutorialManager owner) : InGameTutorialManager.State(owner)
  {
    private InGameTutorialManager.TutorialRolling.Phase currentPhase = InGameTutorialManager.TutorialRolling.Phase.WAIT_EXPLAIN_WINDOW;
    private float timer;
    private UISprite finger;
    private readonly float DURATION_DISP_HELPER_TEXT = 1.5f;
    private readonly float AUTO_CONTROL_AVOID_TIME = 0.2f;
    private int playerRollingNum;
    private readonly int PLAYER_ROLLING_SUCCESS_NUM = 3;

    public void AddRollingCount() => ++this.playerRollingNum;

    public override void Init()
    {
      Player character = this.character as Player;
      character.SetDiableAction(Character.ACTION_ID.MOVE, true);
      character.SetDiableAction(Character.ACTION_ID.ATTACK, true);
      character.SetDiableAction(Character.ACTION_ID.MAX, false);
      character.SetDiableAction((Character.ACTION_ID) 33, true);
      character.SetDiableAction((Character.ACTION_ID) 19, true);
      this.tutorialManager.dialog.Open(0, "Tutorial_Avoidance_Text_0101", 0, "Tutorial_Avoidance_Text_0102");
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
      ((Behaviour) this.selfController).enabled = false;
    }

    public override void Update()
    {
      switch (this.currentPhase)
      {
        case InGameTutorialManager.TutorialRolling.Phase.WAIT_EXPLAIN_WINDOW:
          this.WaitExplainWindow();
          break;
        case InGameTutorialManager.TutorialRolling.Phase.WAIT_DISP_HELP_TEXT:
          this.WaitDispHelpText();
          break;
        case InGameTutorialManager.TutorialRolling.Phase.AUTO_CONTROL_ROLLING_RIGHT:
          this.AutoControllRollingRight();
          break;
        case InGameTutorialManager.TutorialRolling.Phase.WAIT_AUTO_ROLLING_RIGHT:
          this.WaitAutoRollingRight();
          break;
        case InGameTutorialManager.TutorialRolling.Phase.AUTO_CONTROL_ROLLING_LEFT:
          this.AutoControllRollingLeft();
          break;
        case InGameTutorialManager.TutorialRolling.Phase.WAIT_AUTO_ROLLING_LEFT:
          this.WaitAutoRollingLeft();
          break;
        case InGameTutorialManager.TutorialRolling.Phase.PLAYER_ROLLING_TRAINING:
          this.PlayerRollingTraining();
          break;
        case InGameTutorialManager.TutorialRolling.Phase.FISNIH_AND_WAIT_DIALOG:
          this.FinishAndWaitDialog();
          break;
      }
    }

    private void WaitExplainWindow()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.tutorialManager.dialog.Close(1);
      this.timer = 0.0f;
      this.tutorialManager.helper.avoidHelper.ShowHelpText();
      this.tutorialManager.helper.commonHelper.ShowAutoControlMark();
      this.currentPhase = InGameTutorialManager.TutorialRolling.Phase.WAIT_DISP_HELP_TEXT;
    }

    private void WaitDispHelpText()
    {
      this.timer += Time.deltaTime;
      if ((double) this.DURATION_DISP_HELPER_TEXT >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialRolling.Phase.AUTO_CONTROL_ROLLING_RIGHT;
      this.finger = this.tutorialManager.helper.commonHelper.ShowFinger(0.1f);
    }

    private void AutoControllRollingRight()
    {
      this.timer += Time.deltaTime;
      if (Object.op_Inequality((Object) this.finger, (Object) null))
      {
        Vector3 vector3_1;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_1).\u002Ector((float) Screen.width * 0.5f, (float) Screen.height * 0.25f, 0.0f);
        Vector3 vector3_2;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_2).\u002Ector((float) Screen.width * 0.75f, (float) Screen.height * 0.25f, 0.0f);
        this.finger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(Vector3.Lerp(vector3_1, vector3_2, this.timer / this.AUTO_CONTROL_AVOID_TIME));
      }
      if ((double) this.AUTO_CONTROL_AVOID_TIME >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.tutorialManager.helper.commonHelper.HideFinger();
      this.Rolling(new Vector2(1f, 0.0f));
      this.currentPhase = InGameTutorialManager.TutorialRolling.Phase.WAIT_AUTO_ROLLING_RIGHT;
    }

    private void WaitAutoRollingRight()
    {
      if (this.character.actionID == Character.ACTION_ID.MAX)
        return;
      this.finger = this.tutorialManager.helper.commonHelper.ShowFinger(0.1f);
      this.currentPhase = InGameTutorialManager.TutorialRolling.Phase.AUTO_CONTROL_ROLLING_LEFT;
    }

    private void AutoControllRollingLeft()
    {
      this.timer += Time.deltaTime;
      if (Object.op_Inequality((Object) this.finger, (Object) null))
      {
        Vector3 vector3_1;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_1).\u002Ector((float) Screen.width * 0.5f, (float) Screen.height * 0.25f, 0.0f);
        Vector3 vector3_2;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_2).\u002Ector((float) Screen.width * 0.25f, (float) Screen.height * 0.25f, 0.0f);
        this.finger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(Vector3.Lerp(vector3_1, vector3_2, this.timer / this.AUTO_CONTROL_AVOID_TIME));
      }
      if ((double) this.AUTO_CONTROL_AVOID_TIME >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.tutorialManager.helper.commonHelper.HideFinger();
      this.Rolling(new Vector2(-1f, 0.0f));
      this.currentPhase = InGameTutorialManager.TutorialRolling.Phase.WAIT_AUTO_ROLLING_LEFT;
    }

    private void WaitAutoRollingLeft()
    {
      if (this.character.actionID == Character.ACTION_ID.MAX)
        return;
      this.currentPhase = InGameTutorialManager.TutorialRolling.Phase.NONE;
      this.tutorialManager.helper.commonHelper.HideAutoControlMark();
      this.tutorialManager.helper.avoidHelper.HideHelpText((System.Action) (() =>
      {
        MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
        ((Behaviour) this.selfController).enabled = true;
        this.tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 0, "Tutorial_Avoidance_Text_0104");
        this.playerRollingNum = 0;
        this.currentPhase = InGameTutorialManager.TutorialRolling.Phase.PLAYER_ROLLING_TRAINING;
      }));
    }

    private void PlayerRollingTraining()
    {
      if (this.PLAYER_ROLLING_SUCCESS_NUM > this.playerRollingNum)
        return;
      this.currentPhase = InGameTutorialManager.TutorialRolling.Phase.NONE;
      this.tutorialManager.dialog.Close(1, (System.Action) (() =>
      {
        this.timer = 0.0f;
        this.tutorialManager.helper.commonHelper.ShowComplete();
        this.currentPhase = InGameTutorialManager.TutorialRolling.Phase.FISNIH_AND_WAIT_DIALOG;
      }));
    }

    private void FinishAndWaitDialog()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_COMPLETE >= (double) this.timer)
        return;
      this.tutorialManager.helper.commonHelper.HideComplete();
      this.tutorialManager.Change((InGameTutorialManager.State) new InGameTutorialManager.TutorialAttack(this.tutorialManager));
    }

    private void Rolling(Vector2 direction)
    {
      this.selfController.SetCommand(new SelfController.Command()
      {
        type = SelfController.COMMAND_TYPE.AVOID,
        inputVec = direction
      });
    }

    private enum Phase
    {
      NONE,
      WAIT_EXPLAIN_WINDOW,
      WAIT_DISP_HELP_TEXT,
      AUTO_CONTROL_ROLLING_RIGHT,
      WAIT_AUTO_ROLLING_RIGHT,
      AUTO_CONTROL_ROLLING_LEFT,
      WAIT_AUTO_ROLLING_LEFT,
      PLAYER_ROLLING_TRAINING,
      FISNIH_AND_WAIT_DIALOG,
    }
  }

  public class TutorialAttack(InGameTutorialManager owner) : InGameTutorialManager.State(owner)
  {
    private InGameTutorialManager.TutorialAttack.Phase currentPhase = InGameTutorialManager.TutorialAttack.Phase.WAIT_DISP_FIRST_DIALOG;
    private float timer;
    private UISprite tapFinger;
    private readonly float ENEMY_SPAWN_DISTANCE = 26f;
    private readonly int NUM_ENEMIES = 20;
    private int AttackCounter;
    private readonly float DURATION_DISP_HELP_TEXT = 1.5f;
    private bool isTutorialTextShowed;
    private readonly float WAIT_FOR_DISP_TAP_FINGER = 0.5f;
    private int attackCount;

    public override void Init()
    {
      Player character = this.character as Player;
      character.SetDiableAction(Character.ACTION_ID.MOVE, true);
      character.SetDiableAction(Character.ACTION_ID.ATTACK, false);
      character.SetDiableAction(Character.ACTION_ID.MAX, true);
      character.SetDiableAction((Character.ACTION_ID) 33, true);
      character.SetDiableAction((Character.ACTION_ID) 19, true);
      InputManager.OnTap += new InputManager.OnTouchDelegate(this.TutorialAttackOnTap);
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.WAIT_PLAYER_ATTACK;
    }

    private void TutorialAttackOnTap(InputManager.TouchInfo touch_info) => ++this.AttackCounter;

    public override void Update()
    {
      switch (this.currentPhase)
      {
        case InGameTutorialManager.TutorialAttack.Phase.WAIT_DISP_FIRST_DIALOG:
          this.WaitDispFirstDialog();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.WAIT_DISP_HELP_TEXT:
          this.WaitDispHelpText();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.AUTO_CONTROL_ATTACK:
          this.AutoControlAttack();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.WAIT_AUTO_CONTROL_ATTACK:
          this.WaitAutoControlAttack();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.PLAYER_ATTACK_TRAINING_WAIT:
          this.PlayerAttackTrainingWait();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.PLAYER_ATTACK_TRAINING:
          this.PlayerAttackTraining();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.WAIT_PLAYER_ATTACK:
          this.ShowAttackTutorial();
          this.WaitPlayerAttack();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.WAIT_DISP_COMPLETE_FOR_ATTACK:
          this.WaitDispCompleteForAttack();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.WAIT_DISP_COMBO_DIALOG:
          this.WaitDispComboDalog();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.WAIT_DISP_COMBO_HELP_TEXT:
          this.WaitDispComboHelpText();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.AUTO_CONTROL_COMBO:
          this.AutoControlCombo();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.WAIT_AUTO_CONTOROL_COMBO:
          this.WaitAutoControlCombo();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.PLAYER_COMBO_TRAINING:
          this.PlayerComboTraining();
          break;
        case InGameTutorialManager.TutorialAttack.Phase.WAIT_DISP_COMPLETE_FOR_COMBO:
          this.WaitDispCompleteForCombo();
          break;
      }
    }

    private void WaitDispFirstDialog()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.tutorialManager.dialog.Close(1);
      this.tutorialManager.helper.attackHelper.ShowHelpText();
      this.tutorialManager.helper.commonHelper.ShowAutoControlMark();
      this.tapFinger = this.tutorialManager.helper.commonHelper.ShowTapFinger();
      this.tapFinger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(new Vector3((float) Screen.width / 2f, (float) Screen.height / 4f, 0.0f));
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.WAIT_DISP_HELP_TEXT;
    }

    private void WaitDispHelpText()
    {
      this.timer += Time.deltaTime;
      if ((double) this.DURATION_DISP_HELP_TEXT >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.AUTO_CONTROL_ATTACK;
    }

    private void AutoControlAttack()
    {
      this.tutorialManager.helper.commonHelper.ShowTapIcon();
      this.selfController.SetCommand(new SelfController.Command()
      {
        type = SelfController.COMMAND_TYPE.ATTACK,
        isTouchOn = true
      });
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.WAIT_AUTO_CONTROL_ATTACK;
    }

    private void WaitAutoControlAttack()
    {
      if (this.character.actionID == Character.ACTION_ID.ATTACK)
        return;
      this.tutorialManager.helper.commonHelper.HideAutoControlMark();
      this.tutorialManager.helper.commonHelper.HideTapIcon();
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.NONE;
      this.tutorialManager.helper.commonHelper.HideTapFinger();
      this.tutorialManager.helper.attackHelper.HideHelpText((System.Action) (() =>
      {
        this.tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 0, "Tutorial_Attack_Text_0203");
        this.timer = 0.0f;
        MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
        ((Behaviour) this.selfController).enabled = true;
        this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.PLAYER_ATTACK_TRAINING;
      }));
    }

    private void PlayerAttackTrainingWait()
    {
      this.timer += Time.deltaTime;
      if ((double) this.DURATION_DISP_HELP_TEXT >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.PLAYER_ATTACK_TRAINING;
      this.tutorialManager.helper.attackHelper.ShowHelpText();
    }

    private void PlayerAttackTraining()
    {
      if (this.character.actionID != Character.ACTION_ID.ATTACK)
        return;
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
      ((Behaviour) this.selfController).enabled = false;
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.NONE;
      this.tutorialManager.dialog.Close(1, (System.Action) (() =>
      {
        this.tutorialManager.helper.commonHelper.ShowComplete();
        this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.WAIT_PLAYER_ATTACK;
      }));
    }

    private void WaitPlayerAttack()
    {
      if (this.AttackCounter < 3)
        return;
      InputManager.OnTap -= new InputManager.OnTouchDelegate(this.TutorialAttackOnTap);
      this.tutorialManager.helper.commonHelper.HideTapIcon();
      this.tutorialManager.helper.commonHelper.HideTapFinger();
      this.tutorialManager.helper.attackHelper.HideHelpText();
      this.tutorialManager.helper.commonHelper.ShowExcellent();
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.WAIT_DISP_COMPLETE_FOR_ATTACK;
    }

    private void ShowAttackTutorial()
    {
      this.timer += Time.deltaTime;
      if ((double) this.timer < 1.0 || this.isTutorialTextShowed)
        return;
      this.isTutorialTextShowed = true;
      this.tutorialManager.helper.attackHelper.ShowHelpText();
      this.tutorialManager.helper.commonHelper.ShowTapIcon();
      this.tutorialManager.helper.commonHelper.ShowTapIcon(1);
      this.tutorialManager.helper.commonHelper.ShowTapIcon(2);
      this.tapFinger = this.tutorialManager.helper.commonHelper.ShowTapFinger();
      this.tapFinger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(new Vector3((float) Screen.width / 2f, (float) Screen.height / 4f, 0.0f));
      this.tutorialManager.StartCoroutine(this.tutorialManager.WaitForTime(2f, (System.Action) (() => this.tutorialManager.helper.attackHelper.HideHelpText((System.Action) (() =>
      {
        this.tutorialManager.helper.commonHelper.HideTapIcon();
        this.tutorialManager.helper.commonHelper.HideTapFinger();
        this.timer = 0.0f;
        this.isTutorialTextShowed = false;
      })))));
    }

    private void WaitDispCompleteForAttack()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_COMPLETE >= (double) this.timer)
        return;
      this.tutorialManager.helper.commonHelper.HideExcellent();
      this.timer = 0.0f;
      this.tutorialManager.Change((InGameTutorialManager.State) new InGameTutorialManager.TutorialBattle(this.tutorialManager));
    }

    private void WaitDispComboDalog()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.tutorialManager.dialog.Close(1);
      this.tutorialManager.helper.attackHelper.ShowComboHelpText();
      this.tutorialManager.helper.commonHelper.ShowAutoControlMark();
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.WAIT_DISP_COMBO_HELP_TEXT;
    }

    private void WaitDispComboHelpText()
    {
      this.timer += Time.deltaTime;
      if ((double) this.WAIT_FOR_DISP_TAP_FINGER >= (double) this.timer)
        return;
      this.tapFinger = this.tutorialManager.helper.commonHelper.ShowTapFinger();
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.AUTO_CONTROL_COMBO;
    }

    private void AutoControlCombo()
    {
      Player character = this.character as Player;
      if (this.character.actionID != Character.ACTION_ID.ATTACK)
      {
        this.tutorialManager.helper.commonHelper.ShowTapIcon();
        this.selfController.SetCommand(new SelfController.Command()
        {
          type = SelfController.COMMAND_TYPE.ATTACK,
          isTouchOn = true
        });
        this.attackCount = 1;
      }
      else if (character.enableInputCombo)
      {
        this.tutorialManager.helper.commonHelper.ShowTapIcon(this.attackCount);
        ++this.attackCount;
        character.ActAttackCombo();
      }
      if (!this.character.IsPlayingMotion(17))
        return;
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.WAIT_AUTO_CONTOROL_COMBO;
    }

    private void WaitAutoControlCombo()
    {
      if (this.character.actionID == Character.ACTION_ID.ATTACK)
        return;
      this.tutorialManager.helper.commonHelper.HideTapFinger();
      this.tutorialManager.helper.commonHelper.HideTapIcon();
      this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.NONE;
      this.tutorialManager.helper.commonHelper.HideAutoControlMark();
      this.tutorialManager.helper.attackHelper.HideComboHelpText((System.Action) (() =>
      {
        this.tutorialManager.PopEnemy(Vector3.op_Addition(this.character._transform.position, Vector3.op_Multiply(this.character._transform.forward, this.ENEMY_SPAWN_DISTANCE)), false);
        this.tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 0, "Tutorial_Attack_Text_0208");
        this.timer = 0.0f;
        MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
        ((Behaviour) this.selfController).enabled = true;
        this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.PLAYER_COMBO_TRAINING;
      }));
    }

    private void PlayerComboTraining()
    {
      if (this.character.IsPlayingMotion(17))
      {
        this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.NONE;
        this.tutorialManager.dialog.Close(1, (System.Action) (() =>
        {
          this.tutorialManager.helper.commonHelper.ShowComplete();
          this.timer = 0.0f;
          this.currentPhase = InGameTutorialManager.TutorialAttack.Phase.WAIT_DISP_COMPLETE_FOR_COMBO;
        }));
      }
      else
      {
        if (!this.tutorialManager.CheckAllEnemiesDead())
          return;
        this.timer = 0.0f;
        this.tutorialManager.PopEnemy(Vector3.op_Addition(this.character._transform.position, Vector3.op_Multiply(this.character._transform.forward, this.ENEMY_SPAWN_DISTANCE)), false);
      }
    }

    private void WaitDispCompleteForCombo()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_COMPLETE >= (double) this.timer)
        return;
      this.tutorialManager.helper.commonHelper.HideComplete();
      this.tutorialManager.Change((InGameTutorialManager.State) new InGameTutorialManager.TutorialSpecialMove(this.tutorialManager));
    }

    public override void Final()
    {
    }

    private enum Phase
    {
      NONE,
      WAIT_DISP_FIRST_DIALOG,
      WAIT_DISP_HELP_TEXT,
      AUTO_CONTROL_ATTACK,
      WAIT_AUTO_CONTROL_ATTACK,
      PLAYER_ATTACK_TRAINING_WAIT,
      PLAYER_ATTACK_TRAINING,
      WAIT_PLAYER_ATTACK,
      WAIT_DISP_COMPLETE_FOR_ATTACK,
      WAIT_DISP_COMBO_DIALOG,
      WAIT_DISP_COMBO_HELP_TEXT,
      AUTO_CONTROL_COMBO,
      WAIT_AUTO_CONTOROL_COMBO,
      PLAYER_COMBO_TRAINING,
      WAIT_DISP_COMPLETE_FOR_COMBO,
    }
  }

  public class TutorialSpecialMove(InGameTutorialManager owner) : InGameTutorialManager.State(owner)
  {
    private InGameTutorialManager.TutorialSpecialMove.Phase currentPhase = InGameTutorialManager.TutorialSpecialMove.Phase.WAIT_DISP_FIRST_DIALOG;
    private float timer;
    private readonly float AUTO_GUARD_DURATION = 2f;
    private readonly float DURATION_DISP_DIALOG = 4f;
    private readonly float DURATION_WAIT_DISP_HELP_TEXT = 2.5f;
    private readonly float GUARD_SUCCESS_TIME = 2f;

    public override void Init()
    {
      Player character = this.character as Player;
      character.SetDiableAction(Character.ACTION_ID.MOVE, false);
      character.SetDiableAction(Character.ACTION_ID.ATTACK, true);
      character.SetDiableAction(Character.ACTION_ID.MAX, true);
      character.SetDiableAction((Character.ACTION_ID) 33, false);
      character.SetDiableAction((Character.ACTION_ID) 19, false);
      this.tutorialManager.dialog.Open(1, "Tutorial_Special_Text_0401", 1, "Tutorial_Special_Text_0402");
      ((Behaviour) this.selfController).enabled = false;
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
    }

    public override void Update()
    {
      switch (this.currentPhase)
      {
        case InGameTutorialManager.TutorialSpecialMove.Phase.WAIT_DISP_FIRST_DIALOG:
          this.WaitDispFirstDialog();
          break;
        case InGameTutorialManager.TutorialSpecialMove.Phase.WAIT_DISP_HELP_TEXT:
          this.WaitDispHelpText();
          break;
        case InGameTutorialManager.TutorialSpecialMove.Phase.AUTO_CONTROL_GUARD_START:
          this.AutoControlGuardStart();
          break;
        case InGameTutorialManager.TutorialSpecialMove.Phase.AUTO_CONTROL_GUARD:
          this.AutoControlGuard();
          break;
        case InGameTutorialManager.TutorialSpecialMove.Phase.PLAYER_GUAD_TRAINING:
          this.PlayerGuardTraining();
          break;
        case InGameTutorialManager.TutorialSpecialMove.Phase.WAIT_DISP_COMPLETE:
          this.WaitDispComplete();
          break;
      }
    }

    private void WaitDispFirstDialog()
    {
      this.timer += Time.deltaTime;
      if ((double) this.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.tutorialManager.dialog.Close(1, (System.Action) (() =>
      {
        this.tutorialManager.helper.commonHelper.ShowAutoControlMark();
        this.tutorialManager.helper.guardHelper.ShowHelpText();
        this.currentPhase = InGameTutorialManager.TutorialSpecialMove.Phase.WAIT_DISP_HELP_TEXT;
      }));
      this.currentPhase = InGameTutorialManager.TutorialSpecialMove.Phase.NONE;
    }

    private void WaitDispHelpText()
    {
      this.timer += Time.deltaTime;
      if ((double) this.DURATION_WAIT_DISP_HELP_TEXT >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.tutorialManager.helper.commonHelper.ShowLongTapFinger().cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(new Vector3((float) Screen.width / 2f, (float) Screen.height / 4f, 0.0f));
      this.currentPhase = InGameTutorialManager.TutorialSpecialMove.Phase.AUTO_CONTROL_GUARD_START;
    }

    private void AutoControlGuardStart()
    {
      this.selfController.SetCommand(new SelfController.Command()
      {
        type = SelfController.COMMAND_TYPE.SPECIAL_ACTION,
        isTouchOn = true
      });
      this.currentPhase = InGameTutorialManager.TutorialSpecialMove.Phase.AUTO_CONTROL_GUARD;
    }

    private void AutoControlGuard()
    {
      this.timer += Time.deltaTime;
      if ((double) this.AUTO_GUARD_DURATION > (double) this.timer)
        return;
      this.timer = 0.0f;
      this.character.ActIdle();
      this.tutorialManager.helper.commonHelper.HideLongTapFinger();
      this.tutorialManager.helper.commonHelper.HideAutoControlMark();
      this.currentPhase = InGameTutorialManager.TutorialSpecialMove.Phase.NONE;
      this.tutorialManager.helper.guardHelper.HideHelpText((System.Action) (() =>
      {
        this.tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 1, "Tutorial_Special_Text_0405", 1, "Tutorial_Special_Text_0404");
        MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
        ((Behaviour) this.selfController).enabled = true;
        this.currentPhase = InGameTutorialManager.TutorialSpecialMove.Phase.PLAYER_GUAD_TRAINING;
      }));
    }

    private void PlayerGuardTraining()
    {
      Player character = this.character as Player;
      if (character.isGuardWalk || this.character.actionID == (Character.ACTION_ID) 19)
      {
        character.SetDiableAction(Character.ACTION_ID.MOVE, false);
        this.timer += Time.deltaTime;
      }
      else
      {
        character.SetDiableAction(Character.ACTION_ID.MOVE, true);
        this.timer = 0.0f;
      }
      if ((double) this.GUARD_SUCCESS_TIME > (double) this.timer)
        return;
      this.currentPhase = InGameTutorialManager.TutorialSpecialMove.Phase.NONE;
      this.tutorialManager.dialog.Close(2, (System.Action) (() =>
      {
        this.timer = 0.0f;
        this.tutorialManager.helper.commonHelper.ShowComplete();
        this.currentPhase = InGameTutorialManager.TutorialSpecialMove.Phase.WAIT_DISP_COMPLETE;
      }));
    }

    private void WaitDispComplete()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_COMPLETE >= (double) this.timer)
        return;
      this.tutorialManager.helper.commonHelper.HideComplete();
      this.tutorialManager.Change((InGameTutorialManager.State) new InGameTutorialManager.TutorialBattle(this.tutorialManager));
    }

    private enum Phase
    {
      NONE,
      WAIT_DISP_FIRST_DIALOG,
      WAIT_DISP_HELP_TEXT,
      AUTO_CONTROL_GUARD_START,
      AUTO_CONTROL_GUARD,
      PLAYER_GUAD_TRAINING,
      WAIT_DISP_COMPLETE,
    }
  }

  public class TutorialBattle(InGameTutorialManager owner) : InGameTutorialManager.State(owner)
  {
    private InGameTutorialManager.TutorialBattle.Phase currentPhase = InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_FIRST_DIALOG;
    private float timer;
    private readonly float ENEMY_SPAWN_DISTANCE = 3f;
    private readonly float PLAYER_ATTACK_TRAINING_WAIT_TIME = 1.5f;
    private readonly float DISP_FINGER_ATTACK_TIME = 5f;
    private int currentPortalPoint;
    private bool createdPNC;
    private readonly float WAIT_PORTAL_EFFECT_TIME = 2f;
    private readonly float BATTLE_TRAINING_DIALOG_TIME = 2.5f;
    private bool battleTrainingDialog;
    private Vector3 portalPosition;
    private Vector3 targetCameraPos;
    private Vector3 cameraPos;

    public override void Init()
    {
      Player character = this.character as Player;
      character.SetDiableAction(Character.ACTION_ID.MOVE, true);
      character.SetDiableAction(Character.ACTION_ID.ATTACK, true);
      character.SetDiableAction(Character.ACTION_ID.MAX, true);
      character.SetDiableAction((Character.ACTION_ID) 33, true);
      character.SetDiableAction((Character.ACTION_ID) 19, true);
      this.tutorialManager.CheckAndCallOnDeadAll((Action<Enemy>) null);
      this.tutorialManager.helper.moveHelper.HideHelpText();
      this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_FIRST_DIALOG;
      this.timer = 0.0f;
    }

    public override void Update()
    {
      if (MonoBehaviourSingleton<FieldManager>.I.currentPortalID == 10000101U)
      {
        this.tutorialManager.dialog.Close(1);
        this.tutorialManager.Change((InGameTutorialManager.State) new InGameTutorialManager.TutorialBossBattle(this.tutorialManager));
      }
      else
      {
        if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
        {
          Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
          if (Object.op_Inequality((Object) self, (Object) null) && self.hp < self.hpMax / 2)
            self.hp = self.hpMax / 2;
        }
        switch (this.currentPhase)
        {
          case InGameTutorialManager.TutorialBattle.Phase.PLAYER_ATTACK_TRAINING_WAIT:
            this.PlayerAttackTrainingWait();
            break;
          case InGameTutorialManager.TutorialBattle.Phase.PLAYER_ATTACK_TRAINING:
            this.PlayerAttackTraining();
            break;
          case InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_FIRST_DIALOG:
            this.WaitDispFirstDialog();
            break;
          case InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_PORTAL_EXPLAIN_DIALOG:
            this.GGPlayerBattleTraining();
            break;
          case InGameTutorialManager.TutorialBattle.Phase.WAIT_PORTAL_EFFECT:
            this.WaitPortalEffect();
            break;
          case InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_HELP_PICTURE_0:
            this.WaitDispHelpPicture0();
            break;
          case InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_HELP_PICTURE_1:
            this.WaitDispHelpPicture1();
            break;
          case InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_PLAYER_TRAINING_DIALOG:
            this.WaitDispPlayerTrainingDialog();
            break;
          case InGameTutorialManager.TutorialBattle.Phase.PLAYER_BATTLE_TRAINING:
            this.PlayerBattleTraining();
            break;
          case InGameTutorialManager.TutorialBattle.Phase.WAIT_HIDE_PORTAL_OPEN_INFO:
            this.WaitHidePortalOpenInfo();
            break;
          case InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_LAST_DIALOG:
            this.WaitDispLastDialog();
            break;
        }
      }
    }

    private void PlayerAttackTrainingWait()
    {
      this.tutorialManager.CheckAndCallOnDeadAll(new Action<Enemy>(this.CreatePortalPoint));
      this.timer += Time.deltaTime;
      if ((double) this.PLAYER_ATTACK_TRAINING_WAIT_TIME >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.NONE;
      this.tutorialManager.helper.attackHelper.ShowHelpText();
      UITweenCtrl component = ((Component) this.tutorialManager.helper.fingerAttack).GetComponent<UITweenCtrl>();
      if (Object.op_Inequality((Object) null, (Object) component))
      {
        ((Component) component).gameObject.SetActive(true);
        component.Play();
      }
      this.tutorialManager.StartCoroutine(this.tutorialManager.WaitForTime(2.5f, (System.Action) (() =>
      {
        ((Component) this.tutorialManager.helper.fingerAttack).gameObject.SetActive(false);
        this.tutorialManager.helper.attackHelper.HideHelpText((System.Action) (() => this.tutorialManager.StartCoroutine(this.tutorialManager.WaitForTime(0.5f, (System.Action) (() =>
        {
          this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.PLAYER_ATTACK_TRAINING;
          this.tutorialManager.helper.attackHelper.ShowHelpPicture();
        })))));
      })));
    }

    private void PlayerAttackTraining()
    {
      this.timer += Time.deltaTime;
      if ((double) this.DISP_FINGER_ATTACK_TIME < (double) this.timer)
      {
        this.tutorialManager.helper.attackHelper.HideHelpPicture();
        if (((Component) this.tutorialManager.helper.fingerAttack).gameObject.activeSelf)
          ((Component) this.tutorialManager.helper.fingerAttack).gameObject.SetActive(false);
      }
      this.tutorialManager.CheckAndCallOnDeadAll(new Action<Enemy>(this.CreatePortalPoint));
      if (!this.tutorialManager.CheckAllEnemiesDead() || (double) this.DISP_FINGER_ATTACK_TIME >= (double) this.timer)
        return;
      this.tutorialManager.helper.attackHelper.HideHelpPicture();
      if (((Component) this.tutorialManager.helper.fingerAttack).gameObject.activeSelf)
        ((Component) this.tutorialManager.helper.fingerAttack).gameObject.SetActive(false);
      this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.WAIT_PORTAL_EFFECT;
      this.timer = 0.0f;
    }

    private void GGPlayerBattleTraining()
    {
      this.tutorialManager.CheckAndCallOnDeadAll(new Action<Enemy>(this.CreatePortalPoint));
      if (MonoBehaviourSingleton<InGameProgress>.I.portalObjectList == null || MonoBehaviourSingleton<InGameProgress>.I.portalObjectList.Count <= 0)
        return;
      PortalObject portalObject = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[0];
      if (!this.createdPNC)
      {
        this.createdPNC = true;
        Player character = this.character as Player;
        character.SetDiableAction(Character.ACTION_ID.MOVE, false);
        character.SetDiableAction(Character.ACTION_ID.ATTACK, false);
        character.SetDiableAction(Character.ACTION_ID.MAX, false);
        character.SetDiableAction((Character.ACTION_ID) 33, false);
        character.SetDiableAction((Character.ACTION_ID) 19, false);
        Vector3 vector3_1 = Vector3.zero;
        for (int index = 0; index < 1; ++index)
        {
          float num1 = Random.Range(0.0f, 360f);
          float num2 = 2.5f;
          this.tutorialManager.PopEnemy(Vector3.op_Addition(new Vector3(character.positionXZ.x, 0.0f, character.positionXZ.y), new Vector3(num2 * Mathf.Cos(num1 * ((float) Math.PI / 180f)), 0.0f, num2 * Mathf.Sin(num1 * ((float) Math.PI / 180f)))), false);
          vector3_1 = Vector3.op_Addition(new Vector3(character.positionXZ.x, 0.0f, character.positionXZ.y), new Vector3(num2 * Mathf.Cos(num1 * ((float) Math.PI / 180f)), 0.0f, num2 * Mathf.Sin(num1 * ((float) Math.PI / 180f))));
        }
        for (int index = 0; index < 1; ++index)
        {
          float num3 = Random.Range(0.0f, 360f);
          float num4 = 4f;
          this.tutorialManager.PopEnemy(Vector3.op_Addition(new Vector3(character.positionXZ.x, 0.0f, character.positionXZ.y), new Vector3(num4 * Mathf.Cos(num3 * ((float) Math.PI / 180f)), 0.0f, num4 * Mathf.Sin(num3 * ((float) Math.PI / 180f)))), false);
        }
        for (int index = 0; index < 1; ++index)
        {
          float num5 = Random.Range(0.0f, 360f);
          float num6 = 5f;
          this.tutorialManager.PopEnemy(Vector3.op_Addition(new Vector3(character.positionXZ.x, 0.0f, character.positionXZ.y), new Vector3(num6 * Mathf.Cos(num5 * ((float) Math.PI / 180f)), 0.0f, num6 * Mathf.Sin(num5 * ((float) Math.PI / 180f)))), false);
        }
        for (int index = 0; index < 10; ++index)
        {
          float num7 = Random.Range(0.0f, 360f);
          float num8 = Random.Range(4f, 26f);
          this.tutorialManager.PopEnemy(new Vector3(num8 * Mathf.Cos(num7 * ((float) Math.PI / 180f)), 0.0f, num8 * Mathf.Sin(num7 * ((float) Math.PI / 180f))), false);
        }
        Vector3 vector3_2;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_2).\u002Ector(0.0f, 1.6f, 0.0f);
        Vector3 vector3_3;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_3).\u002Ector(1.5f, 1.5f, 1.5f);
        this.tutorialManager.mdlArrow.localScale = vector3_3;
        this.tutorialManager.mdlArrow.position = Vector3.op_Addition(vector3_1, vector3_2);
        ((Component) this.tutorialManager.mdlArrow).gameObject.SetActive(true);
        this.tutorialManager.helper.commonHelper.ShowEnemyCount(portalObject.nowPoint);
      }
      if (this.currentPortalPoint != portalObject.nowPoint && !this.tutorialManager.dialog.isThreeLineLabel2Active())
      {
        this.currentPortalPoint = portalObject.nowPoint;
        this.tutorialManager.helper.commonHelper.ShowEnemyCount(portalObject.nowPoint);
      }
      else if (this.currentPortalPoint != portalObject.nowPoint && portalObject.isFull && this.tutorialManager.dialog.isThreeLineLabel2Active())
      {
        this.currentPortalPoint = portalObject.nowPoint;
        this.tutorialManager.helper.commonHelper.ShowEnemyCount(portalObject.nowPoint);
        this.tutorialManager.dialog.HideThreeLineLabel2();
      }
      if (portalObject.isFull && !this.tutorialManager.helper.commonHelper.OnShowEnemyCount && this.tutorialManager.helper.commonHelper.CurrentEnemyShow > 0)
      {
        Debug.Log((object) ("Current Enemy Show: " + (object) this.tutorialManager.helper.commonHelper.CurrentEnemyShow));
        this.tutorialManager.helper.commonHelper.HideEnemyCount();
        this.tutorialManager.helper.commonHelper.ShowSplendid();
        this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.WAIT_PORTAL_EFFECT;
        this.tutorialManager.dialog.HideThreeLineLabel2();
        this.timer = 0.0f;
      }
      else
      {
        if (!portalObject.isFull || this.tutorialManager.helper.commonHelper.OnShowEnemyCount || this.tutorialManager.helper.commonHelper.CurrentEnemyShow != 0)
          return;
        this.timer += Time.deltaTime;
        if ((double) this.timer <= 2.0)
          return;
        Debug.Log((object) ("Current Enemy Show: " + (object) this.tutorialManager.helper.commonHelper.CurrentEnemyShow));
        this.tutorialManager.helper.commonHelper.HideEnemyCount();
        this.tutorialManager.helper.commonHelper.ShowSplendid();
        this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.WAIT_PORTAL_EFFECT;
        this.tutorialManager.dialog.HideThreeLineLabel2();
        this.timer = 0.0f;
      }
    }

    private void WaitPortalEffect()
    {
      this.timer += Time.deltaTime;
      if ((double) this.WAIT_PORTAL_EFFECT_TIME >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.PLAYER_BATTLE_TRAINING;
      this.portalPosition = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[0]._transform.position;
      this.tutorialManager.ClearedPoppedEnemiesInfo();
      Vector3 position = this.character._transform.position;
      Vector3[] vector3Array = new Vector3[2]
      {
        Vector3.op_Addition(position, new Vector3(3.5f, 0.0f, -5f)),
        Vector3.op_Addition(position, new Vector3(-3.5f, 0.0f, -5f))
      };
      foreach (Vector3 pos in vector3Array)
        this.tutorialManager.PopEnemy(pos, xMax: InGameTutorialManager.MAX_APEAR_POS_X, zMax: InGameTutorialManager.MAX_APEAR_POS_Z);
      this.tutorialManager.dialog.Open(1, "Tutorial_Portal_Text_0507", 1, "Tutorial_Portal_Text_0505");
      this.battleTrainingDialog = true;
    }

    private void WaitDispFirstDialog()
    {
      this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_PORTAL_EXPLAIN_DIALOG;
    }

    private void WaitDispPortalExplainDialog()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.NONE;
      this.tutorialManager.dialog.Close(1, (System.Action) (() =>
      {
        this.tutorialManager.helper.battleHelper.ShowHelpPicture0();
        this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_HELP_PICTURE_0;
        List<PortalObject> portalObjectList = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList;
        ((Component) portalObjectList[0]).gameObject.SetActive(true);
        this.portalPosition = portalObjectList[0]._transform.position;
        this.tutorialManager.ClearedPoppedEnemiesInfo();
        Vector3 position = this.character._transform.position;
        Vector3[] vector3Array = new Vector3[3]
        {
          Vector3.op_Addition(position, new Vector3(3.5f, 0.0f, -5f)),
          Vector3.op_Addition(position, new Vector3(-3.5f, 0.0f, -5f)),
          Vector3.op_Addition(position, new Vector3(0.0f, 0.0f, -5f))
        };
        foreach (Vector3 pos in vector3Array)
          this.tutorialManager.PopEnemy(pos, xMax: InGameTutorialManager.MAX_APEAR_POS_X, zMax: InGameTutorialManager.MAX_APEAR_POS_Z);
        this.tutorialManager.SetActiveAllEnemiesController(false);
      }));
    }

    private void WaitDispHelpPicture0()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.NONE;
      this.tutorialManager.helper.battleHelper.HideHelpPicture0((System.Action) (() =>
      {
        this.tutorialManager.helper.battleHelper.ShowHelpPicture1();
        this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_HELP_PICTURE_1;
      }));
    }

    private void WaitDispHelpPicture1()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.NONE;
      this.tutorialManager.helper.battleHelper.HideHelpPicture1((System.Action) (() =>
      {
        ((Behaviour) this.selfController).enabled = true;
        MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
        this.tutorialManager.SetActiveAllEnemiesController(true);
        this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.PLAYER_BATTLE_TRAINING;
        this.portalPosition = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[0]._transform.position;
        this.tutorialManager.ClearedPoppedEnemiesInfo();
        Vector3 position = this.character._transform.position;
        Vector3[] vector3Array = new Vector3[2]
        {
          Vector3.op_Addition(position, new Vector3(3.5f, 0.0f, -5f)),
          Vector3.op_Addition(position, new Vector3(-3.5f, 0.0f, -5f))
        };
        foreach (Vector3 pos in vector3Array)
          this.tutorialManager.PopEnemy(pos, xMax: InGameTutorialManager.MAX_APEAR_POS_X, zMax: InGameTutorialManager.MAX_APEAR_POS_Z);
        this.tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 1, "Tutorial_Portal_Text_0505");
        this.battleTrainingDialog = true;
      }));
    }

    private void WaitDispPlayerTrainingDialog()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.NONE;
      this.tutorialManager.dialog.Close(1, (System.Action) (() => this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.PLAYER_BATTLE_TRAINING));
    }

    private void PlayerBattleTraining()
    {
      this.tutorialManager.CheckAndCallOnDeadAll(new Action<Enemy>(this.CreatePortalPoint));
      this.timer += Time.deltaTime;
      if ((double) this.BATTLE_TRAINING_DIALOG_TIME < (double) this.timer && this.battleTrainingDialog)
      {
        this.battleTrainingDialog = false;
        this.tutorialManager.dialog.Close(1);
      }
      if (!this.tutorialManager.CheckAllEnemiesDead())
        return;
      this.tutorialManager.dialog.Close(1);
      ((Behaviour) MonoBehaviourSingleton<InGameCameraManager>.I).enabled = false;
      this.cameraPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
      ((Behaviour) this.selfController).enabled = false;
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
      this.timer = 0.0f;
      this.targetCameraPos = Vector3.op_Addition(this.portalPosition, Vector3.op_Subtraction(this.cameraPos, this.character._position));
      this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.WAIT_HIDE_PORTAL_OPEN_INFO;
    }

    private void WaitHidePortalOpenInfo()
    {
      this.timer += Time.deltaTime;
      MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = Vector3.Lerp(this.cameraPos, this.targetCameraPos, this.timer / 1.2f);
      if ((double) this.timer < 2.7000000476837158)
        return;
      MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = this.cameraPos;
      ((Behaviour) MonoBehaviourSingleton<InGameCameraManager>.I).enabled = true;
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
      ((Behaviour) this.selfController).enabled = true;
      this.currentPhase = InGameTutorialManager.TutorialBattle.Phase.WAIT_DISP_LAST_DIALOG;
      Vector3 vector3_1;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_1).\u002Ector(2.5f, 2.5f, 2.5f);
      Vector3 vector3_2;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_2).\u002Ector(0.0f, 2.6f, 0.0f);
      this.tutorialManager.mdlArrow.localScale = vector3_1;
      this.tutorialManager.mdlArrow.position = Vector3.op_Addition(MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[0]._transform.position, vector3_2);
      ((Component) this.tutorialManager.mdlArrow).gameObject.SetActive(true);
    }

    private void WaitDispLastDialog()
    {
      this.tutorialManager.UpdateArrowModel();
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.tutorialManager.dialog.Close(1);
      this.tutorialManager.Change((InGameTutorialManager.State) new InGameTutorialManager.TutorialBossBattle(this.tutorialManager));
    }

    private void CreatePortalPoint(Enemy enemy)
    {
      if (!MonoBehaviourSingleton<FieldManager>.IsValid())
        return;
      Coop_Model_EnemyDefeat model = new Coop_Model_EnemyDefeat();
      model.ppt = 1;
      model.x = (int) enemy._transform.position.x;
      model.z = (int) enemy._transform.position.z;
      if ((double) ((Component) this.tutorialManager.mdlArrow).gameObject.transform.position.x == (double) enemy._transform.position.x && (double) ((Component) this.tutorialManager.mdlArrow).gameObject.transform.position.z == (double) enemy._transform.position.z)
        ((Component) this.tutorialManager.mdlArrow).gameObject.SetActive(false);
      FieldMapPortalInfo pointToPortalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalPointToPortalInfo();
      MonoBehaviourSingleton<FieldManager>.I.AddPortalPointToPortalInfo(model.ppt);
      if (pointToPortalInfo == null || !MonoBehaviourSingleton<InGameProgress>.IsValid())
        return;
      if (MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[0].nowPoint == 0)
      {
        this.tutorialManager.helper.commonHelper.HideEnemyCount();
        this.tutorialManager.dialog.OpenThreeLineLabel2();
      }
      MonoBehaviourSingleton<InGameProgress>.I.CreatePortalPoint(pointToPortalInfo, model);
    }

    public override void Final()
    {
    }

    private enum Phase
    {
      NONE,
      PLAYER_ATTACK_TRAINING_WAIT,
      PLAYER_ATTACK_TRAINING,
      WAIT_DISP_FIRST_DIALOG,
      WAIT_DISP_PORTAL_EXPLAIN_DIALOG,
      WAIT_PORTAL_EFFECT,
      WAIT_DISP_HELP_PICTURE_0,
      WAIT_DISP_HELP_PICTURE_1,
      WAIT_DISP_PLAYER_TRAINING_DIALOG,
      PLAYER_BATTLE_TRAINING,
      WAIT_HIDE_PORTAL_OPEN_INFO,
      WAIT_DISP_LAST_DIALOG,
    }
  }

  public class TutorialBossBattle : InGameTutorialManager.State
  {
    private readonly int ENEMY_ID = 110010911;
    private readonly int ENEMY_LV = 1;
    private readonly float SOLO_BATTLE_TIME_LIMIT = 30f;
    private readonly float SKILL_WAIT_LIMIT_TIME = 25f;
    private readonly float BATTLE_WITH_FRIEND_TIME = 35f;
    private readonly float BOSS_MIN_HP_RATE = 0.2f;
    private readonly float BOSS_ESCAPE_HP_RATE = 0.5f;
    private readonly float BOSS_REFILL_HP_RATE = 0.3f;
    private InGameTutorialManager.TutorialBossBattle.Phase currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.WAIT_SCENE_LOADING;
    private Enemy boss;
    private EnemyController bossController;
    private Player playerOwn;
    private float timer;
    private float newTipTimer;
    private bool isShowHelp;
    private float delayTimeNpcUseSkill;
    private int delayTimeNpcUseSkillOffset;
    private int bossMultiXHp;
    private Transform fakeCamera;
    private bool showPhase1BotCam;
    private bool initFakeCamera;
    private bool boolSkillNpc;
    private Transform playerStatusRoot;
    private Transform enemyStatusRoot;
    private readonly float DISP_ENEMY_WEAK_DIALOG_RADIUS_SQR = 100f;
    private const int NPC_PLAYER_ID_0 = 991;
    private const int NPC_PLAYER_ID_1 = 990;
    private const int NPC_PLAYER_ID_2 = 992;
    private readonly int TUTORIAL_STAMP_ID = 1;
    private Transform cursorTop;
    private float showBotTimer;
    private bool lockCamBot;
    private float camFov;
    private float showBotCamFov = 104f;
    private Vector3 camPos = Vector3.zero;
    private Vector3 camRot = Vector3.zero;
    private Vector3 showBotCamPos = new Vector3(0.0f, 1.15f, -29f);
    private Vector3 showBotCamRot = new Vector3(-20.6f, 180f, 0.0f);
    private float countTimerForReSkill;
    private readonly int NPC_LOAD_COMPLETE_BIT = 7;
    private int m_npcLoadCompleteBitFlag;
    private NpcController npcUseSkill1;
    private NpcController npcUseSkill2;
    private NpcController npcUseSkill0;
    private Player[] listNpc = new Player[3];
    private StageObjectManager.CreatePlayerInfo.ExtentionInfo[] listNpcInfo = new StageObjectManager.CreatePlayerInfo.ExtentionInfo[3];
    private Vector3[] initNPCPos = new Vector3[3];
    private static int ENTRY_VOICE_PATTERN_1 = 18;
    private static int ENTRY_VOICE_PATTERN_2 = 10014;
    private static int ENTRY_VOICE_PATTERN_3 = 14;
    private bool isTrackDragonFight;

    public TutorialBossBattle(InGameTutorialManager owner)
      : base(owner)
    {
      InGameSettingsManager.TutorialParam tutorialParam = MonoBehaviourSingleton<InGameSettingsManager>.I.tutorialParam;
      if (tutorialParam == null)
        return;
      this.ENEMY_ID = tutorialParam.enemyID;
      this.ENEMY_LV = tutorialParam.enemyLv;
      this.SOLO_BATTLE_TIME_LIMIT = tutorialParam.soloBattleTimeLimit;
      this.SKILL_WAIT_LIMIT_TIME = tutorialParam.skillWaitLimitTime;
      this.BATTLE_WITH_FRIEND_TIME = tutorialParam.battleWithFriendTime;
      this.BOSS_MIN_HP_RATE = tutorialParam.bossMinHpRate;
      this.BOSS_ESCAPE_HP_RATE = tutorialParam.bossEscapeHpRate;
      if ((double) this.BATTLE_WITH_FRIEND_TIME < 10.0)
        this.BATTLE_WITH_FRIEND_TIME = 10f;
      this.delayTimeNpcUseSkill = tutorialParam.deplayTimeNpcUseSkill;
      this.delayTimeNpcUseSkillOffset = tutorialParam.deplayTimeNpcUseSkillOffset;
      this.bossMultiXHp = tutorialParam.bossMultiXHp;
    }

    public override void Init() => this.fakeCamera = new GameObject().transform;

    public override void Update()
    {
      if (Object.op_Inequality((Object) this.boss, (Object) null))
        this.boss.hpMax = 500000;
      if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      {
        Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
        if (Object.op_Inequality((Object) self, (Object) null) && self.hp < self.hpMax / 2)
          self.hp = self.hpMax / 2;
      }
      if (Object.op_Inequality((Object) this.boss, (Object) null) && this.boss.hpMax > 0 && this.boss.hp <= (int) ((double) this.boss.hpMax * (double) this.BOSS_REFILL_HP_RATE))
      {
        this.boss.hp = this.boss.hpMax;
        if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
        {
          MonoBehaviourSingleton<UIEnemyStatus>.I.SetHpMultiX(--this.bossMultiXHp);
          MonoBehaviourSingleton<UIEnemyStatus>.I.PlayShakeHpMultiX();
        }
      }
      switch (this.currentPhase)
      {
        case InGameTutorialManager.TutorialBossBattle.Phase.WAIT_SCENE_LOADING:
          this.WaitSceneLoading();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.WALKING_TO_PLAYER:
          this.WalkingToPlayer();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.WAIT_DISP_WEAK_MARK:
          this.WaitDispWeakMark();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.WAIT_HELP_PICTURE_0:
          this.WaitHelpPicture0();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.WAIT_HELP_PICTURE_1:
          this.WaitHelpPicture1();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.SOLO_BATTLE:
          this.SoloBattle();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.WAIT_DOWN:
          this.WaitDown();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.BATTLE_WITH_FRIENDS:
          this.BattleWithFriends();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.WAIT_SKILL_HELP_PICTURE_0:
          this.WaitSkillHelpPicture0();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.WAIT_SKILL_HELP_PICTURE_1:
          this.WaitSkillHelpPicture1();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.PLAYER_CONTROL_SKILL:
          this.PlayerControlSkill();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.ENEMY_ESCAPE_BATTLE:
          this.EnemyEscapeBattle();
          break;
        case InGameTutorialManager.TutorialBossBattle.Phase.DISP_TIELE:
          this.DispTitle();
          break;
      }
      if (this.tutorialManager.dialog.isTwoLineGameObjectActive())
        this.newTipTimer += Time.deltaTime;
      if ((double) this.newTipTimer < (double) InGameTutorialManager.SHOW_WELCOME_LOG_TIME || this.isShowHelp)
        return;
      ((Component) this.tutorialManager.dialog).gameObject.SetActive(false);
      this.tutorialManager.helper.basicNewHelper.ShowHelpPicture((System.Action) (() =>
      {
        this.boss.setPauseWithAnim(false);
        ((Behaviour) this.bossController).enabled = true;
        InGameTutorialManager.TutorialBossBattle.SetDisablePlayerControl(this.playerOwn, false);
        this.timer = 0.0f;
      }));
      this.newTipTimer = 0.0f;
      this.isShowHelp = true;
    }

    private void DebugVector(string name, Vector3 v)
    {
    }

    public override void FixedUpdate()
    {
      if (this.lockCamBot)
      {
        if (!this.showPhase1BotCam)
        {
          this.showBotTimer += Time.deltaTime;
          if (!this.initFakeCamera)
          {
            this.camRot = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.eulerAngles;
            this.camPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
            this.fakeCamera.position = Vector3.LerpUnclamped(this.showBotCamPos, ((Component) this.playerOwn).transform.position, InGameTutorialManager.SHOW_BOT_CAM_TIME_RATE + 1f);
            this.fakeCamera.position = new Vector3(this.fakeCamera.position.x, Mathf.Lerp(this.camPos.y, InGameTutorialManager.SHOW_BOT_CAM_Y, InGameTutorialManager.SHOW_BOT_CAM_TIME_RATIO_PHASE1), this.fakeCamera.position.z);
            this.fakeCamera.LookAt(this.showBotCamPos);
            this.initFakeCamera = true;
          }
          MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = Vector3.Lerp(this.camPos, this.fakeCamera.position, this.showBotTimer / InGameTutorialManager.SHOW_BOT_CAM_TIME_RATIO_PHASE1);
          MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView = Mathf.Lerp(this.camFov, InGameTutorialManager.SHOW_BOT_CAM_FOV_PHASE1, this.showBotTimer / InGameTutorialManager.SHOW_BOT_CAM_TIME_RATIO_PHASE1);
          MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.eulerAngles = Vector3.op_Addition(this.camRot, this.learpAngle(this.camRot, this.fakeCamera.eulerAngles, this.showBotTimer / InGameTutorialManager.SHOW_BOT_CAM_TIME_RATIO_PHASE1));
          if ((double) this.showBotTimer > (double) InGameTutorialManager.SHOW_BOT_CAM_TIME_RATIO_PHASE1)
          {
            this.showPhase1BotCam = true;
            this.showBotTimer = 0.0f;
            this.camPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
            this.camRot = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.eulerAngles;
            this.camFov = MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView;
          }
        }
        if (this.showPhase1BotCam)
        {
          this.showBotTimer += Time.deltaTime;
          float deltaTime = this.showBotTimer / InGameTutorialManager.SHOW_BOT_CAM_TIME_RATIO_PHASE2;
          Vector3 vector3_1 = Vector3.Lerp(this.camPos, this.showBotCamPos, deltaTime);
          Vector3 vector3_2 = Vector3.op_Addition(this.camRot, this.learpAngle(this.camRot, this.showBotCamRot, deltaTime));
          float num = Mathf.Lerp(this.camFov, this.showBotCamFov, deltaTime);
          MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = vector3_1;
          MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.eulerAngles = vector3_2;
          MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView = num;
          if ((double) this.showBotTimer > (double) InGameTutorialManager.SHOW_BOT_CAM_TIME)
          {
            ((Behaviour) MonoBehaviourSingleton<InGameCameraManager>.I).enabled = true;
            this.lockCamBot = false;
            this.timer = 0.0f;
          }
        }
      }
      if (!this.boolSkillNpc)
        return;
      if ((double) this.showBotTimer <= (double) InGameTutorialManager.SHOW_BOT_SKILL_TIME)
      {
        this.showBotTimer += Time.deltaTime;
      }
      else
      {
        this.boolSkillNpc = false;
        this.showBotTimer = 0.0f;
        ((Behaviour) MonoBehaviourSingleton<InGameCameraManager>.I).enabled = true;
        ((Behaviour) this.bossController).enabled = true;
      }
    }

    private void UpdateCamNpcUseSkill(Transform npcTransform)
    {
      if (Object.op_Equality((Object) npcTransform, (Object) null))
        return;
      ((Behaviour) MonoBehaviourSingleton<InGameCameraManager>.I).enabled = false;
      Vector3 vector3_1 = Vector3.op_Subtraction(npcTransform.position, ((Component) this.boss).transform.position);
      Vector3 vector3_2 = Vector3.op_Addition(npcTransform.position, Vector3.op_Multiply(((Vector3) ref vector3_1).normalized, 3f));
      MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = new Vector3(vector3_2.x, MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position.y, vector3_2.z);
      MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.LookAt(npcTransform);
      this.showBotTimer = 0.0f;
      this.boolSkillNpc = true;
      ((Behaviour) this.bossController).enabled = false;
    }

    private Vector3 learpAngle(Vector3 source, Vector3 destination, float deltaTime)
    {
      float num1 = this.learpAngle(source.x, destination.x, deltaTime);
      float num2 = this.learpAngle(source.y, destination.y, deltaTime);
      float num3 = this.learpAngle(source.z, destination.z, deltaTime);
      this.DebugVector("learpAngle ", new Vector3(num1, num2, num3));
      return new Vector3(num1, num2, num3);
    }

    private float learpAngle(float source, float destination, float deltaTime)
    {
      return Mathf.Lerp(0.0f, Mathf.DeltaAngle(source, destination), deltaTime);
    }

    private void WaitSceneLoading()
    {
      if (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
        ResourceManager.autoRetry = false;
      this.tutorialManager.UpdateArrowModel();
      if (MonoBehaviourSingleton<FieldManager>.I.currentPortalID != 10000101U)
        return;
      if (Object.op_Inequality((Object) null, (Object) this.tutorialManager.mdlArrow))
        Object.Destroy((Object) ((Component) this.tutorialManager.mdlArrow).gameObject);
      if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "InGameMain")
        return;
      if (!this.tutorialManager.isLoadingSE && !this.tutorialManager.isCompleteLoadSE)
        this.tutorialManager.LoadSE();
      if (!this.tutorialManager.isCompleteLoadSE || !MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().isInitialized)
        return;
      ResourceManager.autoRetry = true;
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
      if (Object.op_Equality((Object) this.playerStatusRoot, (Object) null))
      {
        InGameMain currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain;
        if (Object.op_Inequality((Object) currentSection, (Object) null))
        {
          Transform transform1 = currentSection._transform.Find("InGameMain/StaticSwitchPanel/NewMenuParent");
          if (Object.op_Inequality((Object) transform1, (Object) null))
            ((Component) transform1).gameObject.SetActive(false);
          this.playerStatusRoot = currentSection._transform.Find("InGameMain/PlayerStatus");
          if (Object.op_Inequality((Object) this.playerStatusRoot, (Object) null))
            ((Component) this.playerStatusRoot).gameObject.SetActive(false);
          Transform transform2 = currentSection._transform.Find("InGameMain/StaticSwitchPanel/ChatButtonParent");
          if (Object.op_Inequality((Object) transform2, (Object) null))
            ((Component) transform2).gameObject.SetActive(false);
        }
      }
      if (this.character.actionID == (Character.ACTION_ID) 23)
        return;
      InGameMain currentSection1 = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain;
      if (Object.op_Inequality((Object) currentSection1, (Object) null) && Object.op_Equality((Object) this.enemyStatusRoot, (Object) null))
        this.enemyStatusRoot = currentSection1._transform.Find("InGameMain/StaticPanel/EnemyStatus");
      if (Object.op_Inequality((Object) this.enemyStatusRoot, (Object) null))
        ((Component) this.enemyStatusRoot).gameObject.SetActive(false);
      ((Behaviour) this.selfController).enabled = false;
      this.tutorialManager.legendDragon.SetActive(false);
      MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(0, new Vector3(0.0f, 0.0f, 0.0f), 0.0f, this.ENEMY_ID, this.ENEMY_LV, true, true, callback: (EnemyLoader.OnCompleteLoad) (e =>
      {
        this.tutorialManager.boss = e;
        this.boss = e;
        e.SetImmortal();
        this.bossController = ((Component) this.boss).GetComponent<EnemyController>();
        ((Behaviour) this.bossController).enabled = true;
        this.tutorialManager.director.StartBattleStartDirection(e, this.character, (System.Action) (() =>
        {
          this.playerOwn = this.character as Player;
          this.WaitForDispGreeting();
          this.playerOwn.skillInfo.GetSkillParam(0).useGaugeCounter = 0.0f;
          MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
          ((Behaviour) this.selfController).enabled = true;
          if (Object.op_Inequality((Object) this.playerStatusRoot, (Object) null))
          {
            ((Component) this.playerStatusRoot).gameObject.SetActive(true);
            ((Component) this.playerStatusRoot).GetComponent<UIPanel>().alpha = 0.0f;
            TweenAlpha tweenAlpha = TweenAlpha.Begin(((Component) this.playerStatusRoot).gameObject, 0.3f, 1f);
            UISkillButton skillButton = MonoBehaviourSingleton<UISkillButtonGroup>.I.GetUISkillButton(2);
            skillButton.ReleaseEffects();
            ((Component) skillButton).gameObject.SetActive(false);
            EventDelegate.Callback del = (EventDelegate.Callback) (() => ((Component) skillButton).gameObject.SetActive(true));
            tweenAlpha.AddOnFinished(del);
          }
          if (Object.op_Inequality((Object) this.enemyStatusRoot, (Object) null))
          {
            ((Component) this.enemyStatusRoot).gameObject.SetActive(true);
            ((Component) this.enemyStatusRoot).GetComponent<UIWidget>().alpha = 0.0f;
            TweenAlpha.Begin(((Component) this.enemyStatusRoot).gameObject, 0.3f, 1f);
          }
          this.boss.setPauseWithAnim(true);
          ((Behaviour) this.bossController).enabled = false;
          this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.SOLO_BATTLE;
          MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_3_battle_start, "Tutorial");
          Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_3_battle_start.ToString()));
          MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_3_battle_start, "Tutorial");
        }));
        if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
          return;
        MonoBehaviourSingleton<UIEnemyStatus>.I.SetActiveTutorialObj(true);
        MonoBehaviourSingleton<UIEnemyStatus>.I.SetHpMultiX(this.bossMultiXHp);
      }), isOverrideScale: true);
      this.LoadNpc();
      this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.NONE;
    }

    private static void SetDisablePlayerControl(Player player, bool isDisable)
    {
      if (!Object.op_Inequality((Object) player, (Object) null))
        return;
      player.SetDiableAction(Character.ACTION_ID.MOVE, isDisable);
      player.SetDiableAction(Character.ACTION_ID.ATTACK, isDisable);
      player.SetDiableAction(Character.ACTION_ID.MAX, isDisable);
      player.SetDiableAction((Character.ACTION_ID) 33, isDisable);
      player.SetDiableAction((Character.ACTION_ID) 19, isDisable);
    }

    private void WaitForDispGreeting()
    {
      this.tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0001", 0, "Tutorial_Move_Text_0002");
      this.tutorialManager.dialog.OpenThreeLineLabel();
    }

    private void WalkingToPlayer()
    {
      Vector3 vector3 = Vector3.op_Subtraction(this.character._position, this.boss._position);
      if ((double) ((Vector3) ref vector3).sqrMagnitude >= (double) this.DISP_ENEMY_WEAK_DIALOG_RADIUS_SQR)
        return;
      this.boss.setPause(true);
      ((Behaviour) this.bossController).enabled = false;
      this.boss.regionWorks[1].weakState = Enemy.WEAK_STATE.WEAK;
      ((Behaviour) this.selfController).enabled = false;
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.WAIT_DISP_WEAK_MARK;
    }

    private void WaitDispWeakMark()
    {
      this.timer += Time.deltaTime;
      if (1.3999999761581421 >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.tutorialManager.helper.bossHelper.ShowHelpPicture0();
      this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.WAIT_HELP_PICTURE_0;
    }

    private void WaitHelpPicture0()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.NONE;
      this.tutorialManager.helper.bossHelper.HideHelpPicture0((System.Action) (() =>
      {
        this.timer = 0.0f;
        this.tutorialManager.helper.bossHelper.ShowHelpPicture1();
        this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.WAIT_HELP_PICTURE_1;
      }));
    }

    private void WaitHelpPicture1()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.NONE;
      this.tutorialManager.helper.bossHelper.HideHelpPicture1((System.Action) (() =>
      {
        this.timer = 0.0f;
        this.boss.setPause(false);
        ((Behaviour) this.bossController).enabled = true;
        ((Behaviour) this.selfController).enabled = true;
        MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
        this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.SOLO_BATTLE;
      }));
    }

    private void SoloBattle()
    {
      this.timer += Time.deltaTime;
      this.boss._downMax = 1;
      if (Object.op_Inequality((Object) this.cursorTop, (Object) null))
      {
        SkillInfo.SkillParam skillParam = (this.character as Player).skillInfo.GetSkillParam(0);
        if ((double) skillParam.useGaugeCounter < (double) (int) skillParam.GetMaxGaugeValue())
          TutorialMessage.DetachCursor(this.cursorTop);
      }
      if ((double) this.timer > (double) this.SKILL_WAIT_LIMIT_TIME && Object.op_Equality((Object) this.cursorTop, (Object) null))
      {
        SkillInfo.SkillParam skillParam = (this.character as Player).skillInfo.GetSkillParam(0);
        skillParam.useGaugeCounter = (float) (int) skillParam.GetMaxGaugeValue();
        Transform child = Utility.FindChild(((Component) MonoBehaviourSingleton<UIManager>.I.uiCamera).transform, "Skill01");
        if (Object.op_Inequality((Object) null, (Object) child))
        {
          UIButton[] componentsInChildren = ((Component) child).gameObject.GetComponentsInChildren<UIButton>();
          if (componentsInChildren.Length != 0)
            this.cursorTop = TutorialMessage.AttachCursor(((Component) componentsInChildren[0]).transform);
        }
      }
      if ((double) this.timer <= (double) this.SOLO_BATTLE_TIME_LIMIT)
        return;
      this.timer = 0.0f;
      this.boss._downMax = 800;
      this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.PLAYER_CONTROL_SKILL;
    }

    private void WaitDown()
    {
      this.timer += Time.deltaTime;
      if ((double) this.timer < 1.0 || !MonoBehaviourSingleton<UIInGamePopupDialog>.IsValid() || MonoBehaviourSingleton<UIInGamePopupDialog>.I.isOpenDialog)
        return;
      this.timer = 0.0f;
      this.boss.setPause(true);
      ((Behaviour) this.bossController).enabled = false;
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
      ((Behaviour) this.selfController).enabled = false;
      this.tutorialManager.helper.bossHelper.ShowHelpPicture2();
      this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.WAIT_SKILL_HELP_PICTURE_0;
    }

    private void WaitSkillHelpPicture0()
    {
      this.timer += Time.deltaTime;
      if ((double) InGameTutorialManager.DURATION_DISP_DIALOG >= (double) this.timer)
        return;
      this.timer = 0.0f;
      this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.NONE;
      this.tutorialManager.helper.bossHelper.HideHelpPicture2((System.Action) (() =>
      {
        this.tutorialManager.helper.bossHelper.ShowHelpPicture3();
        this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.WAIT_SKILL_HELP_PICTURE_1;
      }));
    }

    private void WaitSkillHelpPicture1()
    {
    }

    private void PlayerControlSkill()
    {
      this.timer += Time.deltaTime;
      if (this.lockCamBot)
        this.showBotTimer += Time.deltaTime;
      if (Object.op_Inequality((Object) this.cursorTop, (Object) null))
      {
        SkillInfo.SkillParam skillParam = (this.character as Player).skillInfo.GetSkillParam(0);
        if ((double) skillParam.useGaugeCounter < (double) (int) skillParam.GetMaxGaugeValue())
          TutorialMessage.DetachCursor(this.cursorTop);
      }
      if ((double) this.timer <= (double) this.SKILL_WAIT_LIMIT_TIME)
        return;
      this.boss.enemyLevel = (XorInt) 0;
      Vector3 position = this.boss._transform.position;
      GameSceneTables.SectionData sectionData = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().sectionData;
      this.ShowNpc();
      if (Object.op_Inequality((Object) null, (Object) this.cursorTop))
      {
        TutorialMessage.DetachCursor(this.cursorTop);
        UISkillButton componentInChildren = ((Component) this.cursorTop).gameObject.GetComponentInChildren<UISkillButton>();
        if (Object.op_Inequality((Object) null, (Object) componentInChildren))
        {
          Transform transform = ((Component) componentInChildren.GetCoolTimeGauge()).transform;
          Vector3 localPosition = transform.localPosition;
          localPosition.z = 0.0f;
          transform.localPosition = localPosition;
        }
      }
      this.lockCamBot = true;
      ((Behaviour) MonoBehaviourSingleton<InGameCameraManager>.I).enabled = false;
      this.camPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
      this.camRot = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.eulerAngles;
      this.camFov = MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView;
      this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.BATTLE_WITH_FRIENDS;
      MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_4_battle_NPC_appear, "Tutorial");
      Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_4_battle_NPC_appear.ToString()));
      MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_4_battle_NPC_appear, "Tutorial");
      this.timer = 0.0f;
    }

    private void ShowNpc()
    {
      this.tutorialManager.StartCoroutine(this.IEShowNpc());
      this.tutorialManager.StartCoroutine(this.IEStopPose());
    }

    private IEnumerator IEShowNpc()
    {
      while (Object.op_Equality((Object) this.listNpc[0], (Object) null) || Object.op_Equality((Object) this.listNpc[1], (Object) null) || Object.op_Equality((Object) this.listNpc[2], (Object) null))
        yield return (object) null;
      this.tutorialManager.StartCoroutine(this.EnableNpc(3.5f, this.listNpc[0], this.listNpcInfo[0], this.initNPCPos[0]));
      this.tutorialManager.StartCoroutine(this.EnableNpc(2f, this.listNpc[1], this.listNpcInfo[1], this.initNPCPos[1]));
      this.tutorialManager.StartCoroutine(this.EnableNpc(2.5f, this.listNpc[2], this.listNpcInfo[2], this.initNPCPos[2]));
    }

    private void LoadNpc()
    {
      this.CreatePlayer(991, new StageObjectManager.CreatePlayerInfo.ExtentionInfo()
      {
        npcDataID = 991,
        npcLv = 0,
        npcLvIndex = 1
      }, new Vector3(2f, 0.0f, -36f), 2f, (string) null);
      this.CreatePlayer(990, new StageObjectManager.CreatePlayerInfo.ExtentionInfo()
      {
        npcDataID = 990,
        npcLv = 0,
        npcLvIndex = 0
      }, new Vector3(0.0f, 0.0f, -36f), 3.5f, (string) null);
      this.CreatePlayer(992, new StageObjectManager.CreatePlayerInfo.ExtentionInfo()
      {
        npcDataID = 992,
        npcLv = 0,
        npcLvIndex = 2
      }, new Vector3(-2f, 0.0f, -36f), 2.5f, (string) null);
    }

    private void BattleWithFriends()
    {
      if (this.IsCompleteLoadNPC)
      {
        int num = this.IsEscapeBossHp ? 1 : 0;
      }
      this.timer += Time.deltaTime;
      if ((double) this.BATTLE_WITH_FRIEND_TIME < (double) this.timer)
        this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.ENEMY_ESCAPE_BATTLE;
      SkillInfo.SkillParam skillParam = (this.character as Player).skillInfo.GetSkillParam(0);
      if ((double) skillParam.useGaugeCounter < (double) (int) skillParam.GetMaxGaugeValue())
      {
        this.countTimerForReSkill += Time.deltaTime;
        if ((double) this.countTimerForReSkill > 6.0)
        {
          this.countTimerForReSkill = 0.0f;
          skillParam.useGaugeCounter = (float) (int) skillParam.GetMaxGaugeValue();
        }
      }
      if ((double) this.timer > (double) InGameTutorialManager.FIRST_SKILL_TIME_OFFSET && Object.op_Inequality((Object) this.npcUseSkill1, (Object) null))
      {
        this.npcUseSkill1.UseSkill();
        this.npcUseSkill1 = (NpcController) null;
      }
      if ((double) this.timer <= (double) InGameTutorialManager.SECOND_SKILL_TIME || !Object.op_Inequality((Object) this.npcUseSkill0, (Object) null))
        return;
      this.npcUseSkill0.UseSkill();
      this.npcUseSkill0 = (NpcController) null;
    }

    private bool IsEscapeBossHp
    {
      get => this.boss.hp <= (int) ((double) this.boss.hpMax * (double) this.BOSS_ESCAPE_HP_RATE);
    }

    private void CreatePlayer(
      int npcId,
      StageObjectManager.CreatePlayerInfo.ExtentionInfo extention_info,
      Vector3 pos,
      float delay,
      string message,
      int stampId = 0)
    {
      this.CreatePlayerImpl(npcId, extention_info, pos, delay, message, stampId);
    }

    private bool IsCompleteLoadNPC => this.m_npcLoadCompleteBitFlag == this.NPC_LOAD_COMPLETE_BIT;

    private void CreatePlayerImpl(
      int npcId,
      StageObjectManager.CreatePlayerInfo.ExtentionInfo extention_info,
      Vector3 pos,
      float delay,
      string message,
      int stampId)
    {
      Player nonPlayer = MonoBehaviourSingleton<StageObjectManager>.I.CreateNonPlayer(npcId, extention_info, pos, 0.0f);
      nonPlayer.onTheGround = false;
      NpcController component = ((Component) nonPlayer).gameObject.GetComponent<NpcController>();
      nonPlayer.SetDiableAction(Character.ACTION_ID.MAX, true);
      nonPlayer.SetDiableAction(Character.ACTION_ID.MOVE, true);
      nonPlayer.SetDiableAction(Character.ACTION_ID.ATTACK, true);
      nonPlayer.SetDiableAction(Character.ACTION_ID.MOVE_LOOKAT, true);
      nonPlayer.SetDiableAction(Character.ACTION_ID.MOVE_POINT, true);
      nonPlayer.SetDiableAction(Character.ACTION_ID.MAX, true);
      ((Component) nonPlayer).transform.position = new Vector3(0.0f, (float) (10000 + Random.Range(0, 100)), 0.0f);
      nonPlayer.ActiveShadow(false);
      if (Object.op_Inequality((Object) component, (Object) null))
        component.SetPose(true);
      if (Object.op_Inequality((Object) nonPlayer._collider, (Object) null))
        nonPlayer._collider.enabled = false;
      if (Object.op_Inequality((Object) nonPlayer._rigidbody, (Object) null))
        nonPlayer._rigidbody.constraints = (RigidbodyConstraints) 126;
      nonPlayer.ActIdle(false, -1f);
      this.initNPCPos[extention_info.npcLvIndex] = pos;
      this.listNpc[extention_info.npcLvIndex] = nonPlayer;
      this.listNpcInfo[extention_info.npcLvIndex] = extention_info;
    }

    private IEnumerator EnableNpc(
      float delay,
      Player npc,
      StageObjectManager.CreatePlayerInfo.ExtentionInfo extention_info,
      Vector3 pos)
    {
      yield return (object) new WaitForSeconds(delay);
      NpcController npcController = ((Component) npc).gameObject.GetComponent<NpcController>();
      Debug.Log((object) $"Set enable npc {pos.x} {pos.z}");
      ((Component) npc).transform.position = pos;
      npc.onTheGround = true;
      npc.ActiveShadow(true);
      ((Component) npc).transform.eulerAngles = Vector3.zero;
      npc.SetDiableAction(Character.ACTION_ID.MAX, false);
      npc.SetDiableAction(Character.ACTION_ID.MOVE, false);
      npc.SetDiableAction(Character.ACTION_ID.MOVE_LOOKAT, false);
      npc.SetDiableAction(Character.ACTION_ID.MOVE_POINT, false);
      npc.SetDiableAction(Character.ACTION_ID.ATTACK, false);
      npc.SetDiableAction(Character.ACTION_ID.MAX, false);
      if (Object.op_Inequality((Object) npc._collider, (Object) null))
        npc._collider.enabled = true;
      if (Object.op_Inequality((Object) npc._rigidbody, (Object) null))
        npc._rigidbody.constraints = (RigidbodyConstraints) 116;
      this.tutorialManager.appearEffect.transform.position = ((Component) npc).transform.position;
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) npc);
      int voice_id = 0;
      switch (extention_info.npcDataID)
      {
        case 990:
          voice_id = InGameTutorialManager.TutorialBossBattle.ENTRY_VOICE_PATTERN_2;
          this.npcUseSkill1 = npcController;
          break;
        case 991:
          voice_id = InGameTutorialManager.TutorialBossBattle.ENTRY_VOICE_PATTERN_1;
          this.npcUseSkill0 = npcController;
          break;
        case 992:
          voice_id = InGameTutorialManager.TutorialBossBattle.ENTRY_VOICE_PATTERN_3;
          this.npcUseSkill2 = npcController;
          break;
      }
      loadingQueue.CacheActionVoice(voice_id);
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.tutorialManager.StartCoroutine(this.IEStartPose(npcController, extention_info.npcDataID));
      yield return (object) new WaitForSeconds(1f);
      this.m_npcLoadCompleteBitFlag |= 1 << extention_info.npcLvIndex;
    }

    private IEnumerator PlayVoice(int id)
    {
      switch (id)
      {
        case 990:
          yield return (object) new WaitForSeconds(1.5f);
          SoundManager.PlayActionVoice(InGameTutorialManager.TutorialBossBattle.ENTRY_VOICE_PATTERN_2);
          break;
        case 991:
          SoundManager.PlayActionVoice(InGameTutorialManager.TutorialBossBattle.ENTRY_VOICE_PATTERN_1);
          break;
        case 992:
          yield return (object) new WaitForSeconds(2f);
          SoundManager.PlayActionVoice(InGameTutorialManager.TutorialBossBattle.ENTRY_VOICE_PATTERN_3);
          break;
      }
    }

    private IEnumerator IEStartPose(NpcController controller, int npcId)
    {
      while (!controller.nonPlayer.isInitialized)
        yield return (object) null;
      this.tutorialManager.StartCoroutine(this.PlayVoice(npcId));
      controller.nonPlayer.ActPose();
    }

    private IEnumerator IEStopPose()
    {
      while (Object.op_Equality((Object) this.npcUseSkill0, (Object) null) || Object.op_Equality((Object) this.npcUseSkill1, (Object) null) || Object.op_Equality((Object) this.npcUseSkill2, (Object) null))
        yield return (object) null;
      yield return (object) new WaitForSeconds(InGameTutorialManager.BOT_POSE_TIME);
      this.npcUseSkill0.SetPose(false);
      this.npcUseSkill1.SetPose(false);
      this.npcUseSkill2.SetPose(false);
    }

    private int ChooseRandom(int[] items, float rejectRate = 0.0f)
    {
      if (items == null || (double) Random.Range(0.0f, 1f) < (double) rejectRate)
        return 0;
      int index = Random.Range(0, items.Length);
      return items[index];
    }

    private void EnemyEscapeBattle()
    {
      if (this.character.actionID == (Character.ACTION_ID) 22)
        return;
      if (!this.isTrackDragonFight)
      {
        this.isTrackDragonFight = true;
        MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_5_battle_end, "Tutorial");
        Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_5_battle_end.ToString()));
        MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_5_battle_end, "Tutorial");
      }
      this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.NONE;
      if (Object.op_Inequality((Object) this.playerStatusRoot, (Object) null))
        ((Component) this.playerStatusRoot).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.enemyStatusRoot, (Object) null))
        ((Component) this.enemyStatusRoot).gameObject.SetActive(false);
      MonoBehaviourSingleton<TargetMarkerManager>.I.showMarker = false;
      Transform transform = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain)._transform.Find("InGameMain/DynamicPanel");
      if (Object.op_Inequality((Object) transform, (Object) null))
        ((Component) transform).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.selfController, (Object) null))
        ((Behaviour) this.selfController).enabled = false;
      if (Object.op_Inequality((Object) this.bossController, (Object) null))
        ((Behaviour) this.bossController).enabled = false;
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
      this.tutorialManager.director.StartBattleEndDirection(this.tutorialManager.legendDragon, this.tutorialManager.titleUIPrefab, (System.Action) (() =>
      {
        this.currentPhase = InGameTutorialManager.TutorialBossBattle.Phase.DISP_TIELE;
        ResourceManager.autoRetry = true;
        MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
      }));
    }

    private void DispTitle()
    {
      this.tutorialManager.Change((InGameTutorialManager.State) null);
      Protocol.Force((System.Action) (() => MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep((Action<bool>) (is_success =>
      {
        if (!is_success)
          return;
        MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_6_pamela_greeting, "Tutorial");
        Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_6_pamela_greeting.ToString()));
        MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_6_pamela_greeting, "Tutorial");
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (InGameTutorialManager), ((Component) this.tutorialManager).gameObject, "STORY", (object) new object[4]
        {
          (object) 11000001,
          (object) 0,
          (object) 0,
          (object) "MAIN_MENU_HOME"
        });
        MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
      }))));
    }

    public override void Final()
    {
    }

    public enum Phase
    {
      NONE,
      WAIT_SCENE_LOADING,
      WALKING_TO_PLAYER,
      WAIT_DISP_WEAK_MARK,
      WAIT_HELP_PICTURE_0,
      WAIT_HELP_PICTURE_1,
      SOLO_BATTLE,
      WAIT_DOWN,
      BATTLE_WITH_FRIENDS,
      WAIT_SKILL_HELP_PICTURE_0,
      WAIT_SKILL_HELP_PICTURE_1,
      PLAYER_CONTROL_SKILL,
      ENEMY_ESCAPE_BATTLE,
      DISP_TIELE,
    }
  }
}
