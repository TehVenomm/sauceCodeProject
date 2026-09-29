// Decompiled with JetBrains decompiler
// Type: HomeLoginBonusTheater
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class HomeLoginBonusTheater : GameSection
{
  public static readonly HomeLoginBonusTheater.VOICE[] voiceGreetings = new HomeLoginBonusTheater.VOICE[4]
  {
    HomeLoginBonusTheater.VOICE.PAMERA_GREET_0,
    HomeLoginBonusTheater.VOICE.PAMERA_GREET_1,
    HomeLoginBonusTheater.VOICE.PAMERA_GREET_2,
    HomeLoginBonusTheater.VOICE.PAMERA_GREET_3
  };
  public static readonly HomeLoginBonusTheater.VOICE[] voiceCheers = new HomeLoginBonusTheater.VOICE[3]
  {
    HomeLoginBonusTheater.VOICE.PAMERA_CHEER_0,
    HomeLoginBonusTheater.VOICE.PAMERA_CHEER_1,
    HomeLoginBonusTheater.VOICE.PAMERA_CHEER_2
  };
  private List<HomeLoginBonusTheater.FSM> fsmList_ = new List<HomeLoginBonusTheater.FSM>();
  private HomeLoginBonusTheater.FSMInfo fsmInfo_ = new HomeLoginBonusTheater.FSMInfo();
  private System.Action mainAction_;
  private float waitTime_;
  private Camera homeCamera_;
  private bool isMoveEndCamera_;
  private Transform board_;
  private Transform light_;
  private Transform fireball_;
  private Transform itemModel_;
  private Transform itemLoader_;
  private Transform fireEffect1_;
  private Transform fireEffect2_;
  private TransformInterpolator interpolator_;
  private float homeFieldOfView_;
  private float fovSpeed_ = 1.5f;
  private Vector3 previousCameraPosition = Vector3.zero;
  private Quaternion previousCameraRotation = Quaternion.identity;
  private Vector3 previousNPC00Position = Vector3.zero;
  private Quaternion previousNPC00Rotation = Quaternion.identity;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "ItemTable";
    }
  }

  public void PreInitialize()
  {
    List<HomeCharacterBase> homeCharacterBaseList = new List<HomeCharacterBase>();
    IHomeManager currentIhomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
    if (currentIhomeManager == null)
      return;
    HomeCamera homeCamera = currentIhomeManager.HomeCamera;
    HomeNPCCharacter homeNpcCharacter1 = currentIhomeManager.IHomePeople.GetHomeNPCCharacter(0);
    HomeNPCCharacter homeNpcCharacter2 = currentIhomeManager.IHomePeople.GetHomeNPCCharacter(6);
    HomeSelfCharacter selfChara = currentIhomeManager.IHomePeople.selfChara;
    List<HomeCharacterBase> charas = currentIhomeManager.IHomePeople.charas;
    HomeSelfCharacter.CTRL = false;
    OutGameSettingsManager.LoginBonusScene loginBonusScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.loginBonusScene;
    Vector3 npc00CameraPos = loginBonusScene.npc00CameraPos;
    Quaternion quaternion1 = Quaternion.Euler(loginBonusScene.npc00CameraRot);
    this.previousCameraPosition = ((Component) homeCamera.targetCamera).transform.position;
    this.previousCameraRotation = ((Component) homeCamera.targetCamera).transform.rotation;
    ((Component) homeCamera.targetCamera).transform.localPosition = npc00CameraPos;
    ((Component) homeCamera.targetCamera).transform.localRotation = quaternion1;
    homeCamera.targetCamera.fieldOfView = MonoBehaviourSingleton<OutGameSettingsManager>.I.loginBonusScene.cameraFov;
    if (Object.op_Inequality((Object) null, (Object) homeNpcCharacter1))
    {
      Transform transform = ((Component) homeNpcCharacter1).transform;
      this.previousNPC00Position = transform.position;
      this.previousNPC00Rotation = transform.rotation;
      Vector3 npc00Pos = loginBonusScene.npc00Pos;
      Quaternion quaternion2 = Quaternion.Euler(loginBonusScene.npc00Rot);
      transform.position = npc00Pos;
      transform.rotation = quaternion2;
      homeNpcCharacter1.PushOutControll();
    }
    if (Object.op_Inequality((Object) null, (Object) homeNpcCharacter2))
    {
      Transform transform = ((Component) homeNpcCharacter2).transform;
      Vector3 npc06Pos = loginBonusScene.npc06Pos;
      Quaternion quaternion3 = Quaternion.Euler(loginBonusScene.npc06Rot);
      transform.position = npc06Pos;
      transform.rotation = quaternion3;
      homeNpcCharacter2.PushOutControll();
      ((Component) homeNpcCharacter2).gameObject.GetComponentInChildren<PlayerAnimCtrl>().Play(PLCA.LOGIN_IDLE);
      homeNpcCharacter2.HideShadow();
    }
    if (Object.op_Inequality((Object) null, (Object) selfChara))
      ((Component) selfChara).gameObject.SetActive(false);
    charas.ForEach((Action<HomeCharacterBase>) (o =>
    {
      switch (o)
      {
        case HomePlayerCharacter _:
        case LoungePlayer _:
          ((Component) o).gameObject.SetActive(false);
          if (!Object.op_Inequality((Object) null, (Object) o.GetNamePlate()))
            break;
          ((Component) o.GetNamePlate()).gameObject.SetActive(false);
          break;
      }
    }));
  }

  public override void Initialize()
  {
    this.PreInitialize();
    this.StartCoroutine("DoInitialize");
  }

  private IEnumerator DoInitialize()
  {
    HomeCamera homeCamera = (HomeCamera) null;
    HomeNPCCharacter npc00 = (HomeNPCCharacter) null;
    HomeNPCCharacter npc06 = (HomeNPCCharacter) null;
    IHomeManager currentIhomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
    homeCamera = currentIhomeManager.HomeCamera;
    npc00 = currentIhomeManager.IHomePeople.GetHomeNPCCharacter(0);
    npc06 = currentIhomeManager.IHomePeople.GetHomeNPCCharacter(6);
    MonoBehaviourSingleton<AccountManager>.I.DisplayLogInBonusSection();
    LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject boardLO = loadQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, "LIB_00000001");
    LoadObject lightLO = loadQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, "LIB_00000002");
    LoadObject fireballLO = loadQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_dragon_breath_01");
    LoadObject fireEffect1LO = loadQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_damage_slash_fire_01");
    LoadObject fireEffect2LO = loadQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_damage_fire_01");
    foreach (int voice_id in (int[]) Enum.GetValues(typeof (HomeLoginBonusTheater.VOICE)))
      loadQueue.CacheVoice(voice_id);
    foreach (int se_id in (int[]) Enum.GetValues(typeof (HomeLoginBonusTheater.AUDIO)))
      loadQueue.CacheSE(se_id);
    LoginBonus bonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus.Find((Predicate<LoginBonus>) (obj => obj.type == 0));
    if (bonus != null)
    {
      List<LoginBonus.NextReward> next = bonus.next;
      if (next != null)
      {
        LoadObject[] itemIconLOs = new LoadObject[9];
        LoadObject[] itemBGIconLOs = new LoadObject[9];
        string iconName = "";
        string iconBGName = "";
        HomeLoginBonusTheater.GetIconName(bonus.reward[0], out iconName, out iconBGName);
        itemIconLOs[bonus.nowCount - 1] = loadQueue.LoadItemIcon(iconName);
        if (string.Empty != iconBGName)
          itemBGIconLOs[bonus.nowCount - 1] = loadQueue.LoadItemIcon(iconBGName);
        next.ForEach((Action<LoginBonus.NextReward>) (o =>
        {
          if (0 >= o.reward.Count || 0 >= o.count || 9 < o.count)
            return;
          HomeLoginBonusTheater.GetIconName(o.reward[0], out iconName, out iconBGName);
          itemIconLOs[o.count - 1] = loadQueue.LoadItemIcon(iconName);
          if (!(string.Empty != iconBGName))
            return;
          itemBGIconLOs[o.count - 1] = loadQueue.LoadItemIcon(iconBGName);
        }));
        this.itemLoader_ = Utility.CreateGameObject("ItemLoader", MonoBehaviourSingleton<AppMain>.I._transform);
        ItemLoader loader = ((Component) this.itemLoader_).gameObject.AddComponent<ItemLoader>();
        loader.LoadItem(HomeLoginBonusTheater.GetItemModelID((REWARD_TYPE) bonus.reward[0].type, bonus.reward[0].itemId), this.itemModel_, 0);
        while (loader.isLoading)
          yield return (object) null;
        this.itemModel_ = Utility.CreateGameObject("ItemModel", MonoBehaviourSingleton<AppMain>.I._transform);
        loader.nodeMain.SetParent(this.itemModel_);
        ((Component) this.itemModel_).gameObject.SetActive(false);
        float num = 0.16f;
        this.itemModel_.localScale = new Vector3(num, num, num);
        this.homeCamera_ = homeCamera.targetCamera;
        this.interpolator_ = ((Component) this.homeCamera_).gameObject.GetComponent<TransformInterpolator>();
        if (Object.op_Equality((Object) null, (Object) this.interpolator_))
          this.interpolator_ = ((Component) this.homeCamera_).gameObject.AddComponent<TransformInterpolator>();
        this.homeCamera_.fieldOfView = MonoBehaviourSingleton<OutGameSettingsManager>.I.loginBonusScene.cameraFov;
        this.homeFieldOfView_ = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.outGameFieldOfView;
        if (loadQueue.IsLoading())
          yield return (object) loadQueue.Wait();
        Transform parent = Utility.Find(npc06._transform, "Move");
        this.board_ = ResourceUtility.Realizes(boardLO.loadedObject, parent);
        this.light_ = ResourceUtility.Realizes(lightLO.loadedObject, npc06._transform);
        ((Component) this.light_).gameObject.SetActive(false);
        this.fireball_ = ResourceUtility.Realizes(fireballLO.loadedObject, npc06._transform);
        this.fireball_.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        ((Component) this.fireball_).gameObject.SetActive(false);
        this.fireEffect1_ = ResourceUtility.Realizes(fireEffect1LO.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform);
        this.fireEffect1_.localScale = new Vector3(0.18f, 0.18f, 0.18f);
        ((Component) this.fireEffect1_).gameObject.SetActive(false);
        this.fireEffect2_ = ResourceUtility.Realizes(fireEffect2LO.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform);
        this.fireEffect2_.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        ((Component) this.fireEffect2_).gameObject.SetActive(false);
        Material[] materialArray1 = new Material[9];
        Transform[] transformArray1 = new Transform[9];
        Material[] materialArray2 = new Material[9];
        Transform[] transformArray2 = new Transform[9];
        Renderer[] rendererArray = new Renderer[9];
        Transform transform1 = this.board_.Find("Day_set");
        if (Object.op_Inequality((Object) null, (Object) transform1))
        {
          for (int index = 0; index <= 8; ++index)
          {
            string str = "Day" + (index + 1).ToString();
            Transform transform2 = transform1.Find($"{str}/{str}_panel");
            if (!Object.op_Equality((Object) null, (Object) transform2))
            {
              Renderer component1 = ((Component) transform2).GetComponent<Renderer>();
              if (Object.op_Inequality((Object) null, (Object) component1))
                materialArray1[index] = component1.material;
              transformArray1[index] = transform2;
              Transform transform3 = transform1.Find($"{str}/{str}_panel2");
              if (!Object.op_Equality((Object) null, (Object) transform2))
              {
                Renderer component2 = ((Component) transform3).GetComponent<Renderer>();
                if (Object.op_Inequality((Object) null, (Object) component1))
                  materialArray2[index] = component2.material;
                transformArray2[index] = transform3;
                Transform transform4 = transform1.Find($"{str}/{str}_paper");
                if (!Object.op_Equality((Object) null, (Object) transform4))
                  rendererArray[index] = ((Component) transform4).gameObject.GetComponent<Renderer>();
              }
            }
          }
        }
        Texture[] textureArray1 = new Texture[9];
        for (int index = 0; index < itemIconLOs.Length; ++index)
        {
          if (itemIconLOs[index] != null)
            textureArray1[index] = itemIconLOs[index].loadedObject as Texture;
        }
        Texture[] textureArray2 = new Texture[9];
        for (int index = 0; index < itemBGIconLOs.Length; ++index)
        {
          if (itemBGIconLOs[index] != null)
            textureArray2[index] = itemBGIconLOs[index].loadedObject as Texture;
        }
        int index1;
        for (index1 = 0; index1 < bonus.nowCount - 1; ++index1)
        {
          ((Component) transformArray1[index1]).gameObject.SetActive(false);
          ((Component) transformArray2[index1]).gameObject.SetActive(false);
          rendererArray[index1].material.SetFloat("_Offset", 1f);
        }
        for (; index1 < 9; ++index1)
        {
          if (Object.op_Inequality((Object) null, (Object) textureArray2[index1]))
          {
            materialArray1[index1].mainTexture = textureArray2[index1];
            materialArray2[index1].mainTexture = textureArray1[index1];
          }
          else
            materialArray1[index1].mainTexture = textureArray1[index1];
        }
        this.fsmInfo_.npc00 = npc00;
        this.fsmInfo_.npc06 = npc06;
        this.fsmInfo_.light = this.light_;
        this.fsmInfo_.fireball = this.fireball_;
        this.fsmInfo_.fireEffect1 = this.fireEffect1_;
        this.fsmInfo_.fireEffect2 = this.fireEffect2_;
        this.fsmInfo_.itemModel = this.itemModel_;
        this.fsmInfo_.fireballEndPos = transformArray1[bonus.nowCount - 1].position;
        this.fsmInfo_.fireballEndPos.z -= 0.08f;
        this.fsmInfo_.dayIndex = bonus.nowCount - 1;
        this.fsmInfo_.todayPanel = materialArray1[bonus.nowCount - 1];
        this.fsmInfo_.todayPanel2 = materialArray2[bonus.nowCount - 1];
        this.fsmInfo_.todayPaper = rendererArray[bonus.nowCount - 1].material;
        this.fsmInfo_.interpolator = this.interpolator_;
        this.fsmInfo_.moveCurve = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.loginBonusMoveCureve;
        this.fsmInfo_.scaleCurve = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.loginBonusScaleCureve;
        this.fsmInfo_.previousNPC00Position = this.previousNPC00Position;
        this.fsmInfo_.previousNPC00Rotation = this.previousNPC00Rotation;
        this.fsmList_.Add((HomeLoginBonusTheater.FSM) new HomeLoginBonusTheater.FSMNpc00());
        this.fsmList_.Add((HomeLoginBonusTheater.FSM) new HomeLoginBonusTheater.FSMNpc00Facial());
        this.fsmList_.Add((HomeLoginBonusTheater.FSM) new HomeLoginBonusTheater.FSMNpc06());
        this.fsmList_.Add((HomeLoginBonusTheater.FSM) new HomeLoginBonusTheater.FSMBoard());
        this.fsmList_.Add((HomeLoginBonusTheater.FSM) new HomeLoginBonusTheater.FSMFireball());
        this.fsmList_.Add((HomeLoginBonusTheater.FSM) new HomeLoginBonusTheater.FSMCamera());
        this.fsmList_.ForEach((Action<HomeLoginBonusTheater.FSM>) (o => o.Init(this.fsmInfo_)));
        this.mainAction_ = new System.Action(this.Phase00);
        base.Initialize();
      }
    }
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    if (Object.op_Inequality((Object) null, (Object) this.light_))
    {
      Object.Destroy((Object) ((Component) this.light_).gameObject);
      this.light_ = (Transform) null;
    }
    if (Object.op_Inequality((Object) null, (Object) this.fireball_))
    {
      Object.Destroy((Object) ((Component) this.fireball_).gameObject);
      this.fireball_ = (Transform) null;
    }
    if (Object.op_Inequality((Object) null, (Object) this.itemModel_))
    {
      Object.Destroy((Object) ((Component) this.itemModel_).gameObject);
      this.itemModel_ = (Transform) null;
    }
    if (!Object.op_Inequality((Object) null, (Object) this.itemLoader_))
      return;
    Object.Destroy((Object) ((Component) this.itemLoader_).gameObject);
    this.itemLoader_ = (Transform) null;
  }

  private void Update()
  {
    this.fsmInfo_.deltaTime = Time.deltaTime;
    this.waitTime_ -= this.fsmInfo_.deltaTime;
    List<HomeLoginBonusTheater.FSM> removeList = new List<HomeLoginBonusTheater.FSM>();
    this.fsmList_.ForEach((Action<HomeLoginBonusTheater.FSM>) (o =>
    {
      if (o.DoAction())
        return;
      removeList.Add(o);
    }));
    removeList.ForEach((Action<HomeLoginBonusTheater.FSM>) (o => this.fsmList_.Remove(o)));
    if (this.mainAction_ == null)
      return;
    this.mainAction_();
  }

  private void Phase00()
  {
    if (!this.fsmInfo_.goNextMain)
      return;
    this.fsmInfo_.goNextMain = false;
    this.DispatchEvent("NOTICE");
    this.mainAction_ = new System.Action(this.Phase01);
    this.StartTimer(0.5f);
  }

  private void Phase01()
  {
    if (!this.isWaitComplete())
      return;
    this.fsmInfo_.goNextBoard = true;
    this.mainAction_ = new System.Action(this.Phase02);
  }

  private void Phase02()
  {
    if (!this.fsmInfo_.goNextMain)
      return;
    this.fsmInfo_.goNextMain = false;
    if (Object.op_Inequality((Object) null, (Object) this.interpolator_))
    {
      this.interpolator_.Translate(1.3f, this.previousCameraPosition, add_value: new Vector3());
      this.interpolator_.Rotate(1.3f, ((Quaternion) ref this.previousCameraRotation).eulerAngles, add_value: new Vector3());
    }
    if (Object.op_Inequality((Object) null, (Object) this.light_))
      ((Component) this.light_).gameObject.SetActive(false);
    this.mainAction_ = new System.Action(this.Phase03);
    this.StartTimer(0.3f);
    this.fsmInfo_.goNextNpc00 = true;
    this.fsmInfo_.goNextNpc06 = true;
  }

  private void Phase03()
  {
    float num = this.homeCamera_.fieldOfView + this.fovSpeed_;
    if ((double) num >= (double) this.homeFieldOfView_)
      num = this.homeFieldOfView_;
    this.homeCamera_.fieldOfView = num;
    if (!this.isWaitComplete())
      return;
    HomeSelfCharacter selfChara = GameSceneGlobalSettings.GetCurrentIHomeManager().IHomePeople.selfChara;
    if (Object.op_Inequality((Object) null, (Object) selfChara))
      ((Component) selfChara).gameObject.SetActive(true);
    this.mainAction_ = new System.Action(this.Phase04);
  }

  private void Phase04()
  {
    float num = this.homeCamera_.fieldOfView + this.fovSpeed_;
    if ((double) num >= (double) this.homeFieldOfView_)
      num = this.homeFieldOfView_;
    this.homeCamera_.fieldOfView = num;
    if (Object.op_Inequality((Object) null, (Object) this.homeCamera_) && !this.isMoveEndCamera_ && !this.interpolator_.IsPlaying())
      this.isMoveEndCamera_ = true;
    if (!this.isMoveEndCamera_ || !this.CanChangeScene())
      return;
    GameSection.BackSection();
    HomeSelfCharacter.CTRL = true;
    List<HomeCharacterBase> homeCharacterBaseList = new List<HomeCharacterBase>();
    GameSceneGlobalSettings.GetCurrentIHomeManager().IHomePeople.charas.ForEach((Action<HomeCharacterBase>) (o =>
    {
      switch (o)
      {
        case HomePlayerCharacter _:
        case LoungePlayer _:
          ((Component) o).gameObject.SetActive(true);
          if (!Object.op_Inequality((Object) null, (Object) o.GetNamePlate()))
            break;
          ((Component) o.GetNamePlate()).gameObject.SetActive(true);
          break;
      }
    }));
    this.mainAction_ = (System.Action) null;
  }

  private void StartTimer(float t) => this.waitTime_ = t;

  private bool isWaitComplete() => 0.0 > (double) this.waitTime_;

  private void OnCloseDialog(string close_section_name)
  {
    if (MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count > 0)
      return;
    this.fsmInfo_.goNextMain = true;
  }

  private bool CanChangeScene()
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing;
  }

  private static int GetIconBGID(ITEM_ICON_TYPE icon_type, int icon_id)
  {
    int iconBgid = -1;
    switch (icon_type)
    {
      case ITEM_ICON_TYPE.SKILL_ATTACK:
      case ITEM_ICON_TYPE.SKILL_SUPPORT:
      case ITEM_ICON_TYPE.SKILL_HEAL:
      case ITEM_ICON_TYPE.SKILL_PASSIVE:
      case ITEM_ICON_TYPE.SKILL_GROW:
        iconBgid = ItemIcon.GetIconBGID(icon_type, icon_id, new RARITY_TYPE?());
        break;
    }
    return iconBgid;
  }

  private static uint GetItemModelID(REWARD_TYPE type, int itemID)
  {
    uint itemModelId = uint.MaxValue;
    switch (type)
    {
      case REWARD_TYPE.CRYSTAL:
        itemModelId = 1U;
        break;
      case REWARD_TYPE.MONEY:
        itemModelId = 2U;
        break;
      case REWARD_TYPE.ITEM:
        itemModelId = (uint) itemID;
        break;
    }
    return itemModelId;
  }

  private static void GetIconName(
    LoginBonus.LoginBonusReward reward,
    out string iconName,
    out string iconBGName)
  {
    ITEM_ICON_TYPE icon_type = ITEM_ICON_TYPE.NONE;
    RARITY_TYPE? rarity = new RARITY_TYPE?();
    ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
    ELEMENT_TYPE element2 = ELEMENT_TYPE.MAX;
    int icon_id;
    ItemIcon.GetIconShowData((REWARD_TYPE) reward.type, (uint) reward.itemId, out icon_id, out icon_type, out rarity, out element, out element2, out EQUIPMENT_TYPE? _, out int _, out int _, out GET_TYPE _);
    iconName = icon_type != ITEM_ICON_TYPE.ACCESSORY ? ResourceName.GetItemIcon(icon_id) : ResourceName.GetAccessoryIcon(icon_id);
    int iconBgid = HomeLoginBonusTheater.GetIconBGID(icon_type, icon_id);
    iconBGName = ResourceName.GetItemIcon(iconBgid);
  }

  public static void PlayAudio(HomeLoginBonusTheater.AUDIO audio)
  {
    SoundManager.PlayOneShotSE((int) audio);
  }

  public static void PlayRandomVoice(HomeLoginBonusTheater.VOICE[] voiceList)
  {
    int length = voiceList.Length;
    if (length < 1)
      return;
    int index = Utility.Random(length);
    SoundManager.PlayVoice((int) voiceList[index]);
  }

  public enum AUDIO
  {
    SE_TOP = 40000040, // 0x02625A28
    SE_FIRE = 40000041, // 0x02625A29
  }

  public enum VOICE
  {
    PAMERA_GREET_0 = 14, // 0x0000000E
    PAMERA_GREET_1 = 15, // 0x0000000F
    PAMERA_GREET_2 = 16, // 0x00000010
    PAMERA_GREET_3 = 19, // 0x00000013
    PAMERA_CHEER_0 = 301, // 0x0000012D
    PAMERA_CHEER_1 = 302, // 0x0000012E
    PAMERA_CHEER_2 = 303, // 0x0000012F
  }

  private class FSMInfo
  {
    public float deltaTime;
    public HomeNPCCharacter npc00;
    public HomeNPCCharacter npc06;
    public Transform light;
    public Transform dragonJaw;
    public Transform fireball;
    public Transform fireEffect1;
    public Transform fireEffect2;
    public Transform itemModel;
    public Vector3 fireballStartPos = Vector3.zero;
    public Vector3 fireballEndPos = Vector3.zero;
    public int dayIndex;
    public Material todayPaper;
    public Material todayPanel;
    public Material todayPanel2;
    public TransformInterpolator interpolator;
    public AnimationCurve moveCurve;
    public AnimationCurve scaleCurve;
    public bool goNextMain;
    public bool goNextNpc00;
    public bool goNextNpc00Facial;
    public bool goNextBoard;
    public bool goNextNpc06;
    public bool goNextFireball;
    public bool goNextCamera;
    public Vector3 previousNPC00Position;
    public Quaternion previousNPC00Rotation;
  }

  private abstract class FSM
  {
    protected HomeLoginBonusTheater.FSM.Act act_;
    protected float waitTime_;
    protected HomeLoginBonusTheater.FSMInfo info_;

    public virtual void Init(HomeLoginBonusTheater.FSMInfo info) => this.info_ = info;

    public bool DoAction()
    {
      this.waitTime_ -= this.info_.deltaTime;
      return this.act_ != null && this.act_();
    }

    protected bool IsWaitComplete() => 0.0 >= (double) this.waitTime_;

    protected void StartTimer(float time) => this.waitTime_ = time;

    protected virtual bool PhaseExit() => false;

    protected void ToExit() => this.act_ = new HomeLoginBonusTheater.FSM.Act(this.PhaseExit);

    protected delegate bool Act();
  }

  private class FSMNpc00 : HomeLoginBonusTheater.FSM
  {
    private HomeNPCCharacter npc00_;
    private PlayerAnimCtrl npc00AnimCtrl_;
    private Action<PlayerAnimCtrl, PLCA> tempAction_;
    private Vector3 previousNPC00Position_;
    private Quaternion previousNPC00Rotation_;

    public override void Init(HomeLoginBonusTheater.FSMInfo info)
    {
      base.Init(info);
      this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase00);
      this.npc00_ = info.npc00;
      this.npc00AnimCtrl_ = ((Component) this.npc00_).gameObject.GetComponentInChildren<PlayerAnimCtrl>();
      this.tempAction_ = this.npc00AnimCtrl_.onEnd;
      this.npc00AnimCtrl_.onEnd = (Action<PlayerAnimCtrl, PLCA>) null;
      this.npc00AnimCtrl_.Play(PLCA.BOW);
      this.previousNPC00Position_ = info.previousNPC00Position;
      this.previousNPC00Rotation_ = info.previousNPC00Rotation;
      HomeLoginBonusTheater.PlayRandomVoice(HomeLoginBonusTheater.voiceGreetings);
    }

    private bool Phase00()
    {
      if (this.npc00AnimCtrl_.IsPlayingIdleAnims(0))
      {
        this.info_.goNextCamera = true;
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase01);
      }
      return true;
    }

    private bool Phase01()
    {
      if (this.info_.goNextNpc00)
      {
        this.info_.goNextNpc00 = false;
        this.StartTimer(0.15f);
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase02);
      }
      return true;
    }

    private bool Phase02()
    {
      if (this.IsWaitComplete())
      {
        this.npc00AnimCtrl_.Play(PLCA.HAPPY);
        this.StartTimer(3.5f);
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase03);
      }
      return true;
    }

    private bool Phase03()
    {
      if (this.npc00AnimCtrl_.IsCurrentState(PLCA.HAPPY) || this.IsWaitComplete())
      {
        this.npc00AnimCtrl_.onEnd = this.tempAction_;
        this.info_.goNextNpc00Facial = true;
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase04);
        this.StartTimer(2f);
      }
      return true;
    }

    private bool Phase04()
    {
      if (this.IsWaitComplete())
      {
        this.info_.goNextMain = true;
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase99);
      }
      return true;
    }

    private bool Phase99()
    {
      if (this.info_.goNextNpc00)
      {
        this.info_.goNextNpc00 = false;
        this.npc00_.defaultPosition = this.previousNPC00Position_;
        this.npc00_.defaultRotation = this.previousNPC00Rotation_;
        this.npc00_.PopState();
        this.npc00_.PushBackPosition();
        this.ToExit();
      }
      return true;
    }
  }

  private class FSMNpc00Facial : HomeLoginBonusTheater.FSM
  {
    private NPCFacial facial_;

    public override void Init(HomeLoginBonusTheater.FSMInfo info)
    {
      base.Init(info);
      this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase00);
      this.facial_ = ((Component) info.npc00).gameObject.GetComponentInChildren<NPCFacial>();
    }

    private bool Phase00()
    {
      if (this.info_.goNextNpc00Facial)
      {
        this.info_.goNextNpc00Facial = false;
        this.facial_.eyeType = NPCFacial.TYPE.JOY;
        this.facial_.mouthType = NPCFacial.TYPE.JOY;
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase01);
        this.StartTimer(2f);
      }
      return true;
    }

    private bool Phase01()
    {
      if (this.IsWaitComplete())
      {
        this.facial_.eyeType = NPCFacial.TYPE.NORMAL;
        this.facial_.mouthType = NPCFacial.TYPE.NORMAL;
        this.ToExit();
      }
      return true;
    }
  }

  private class FSMNpc06 : HomeLoginBonusTheater.FSM
  {
    private HomeNPCCharacter npc06_;
    private PlayerAnimCtrl npc06AnimCtrl_;
    private Transform jaw_;
    private PLCA tempDefaultAnim_;
    private Action<PlayerAnimCtrl, PLCA> tempAction_;

    public override void Init(HomeLoginBonusTheater.FSMInfo info)
    {
      base.Init(info);
      this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase00);
      this.npc06_ = info.npc06;
      this.npc06AnimCtrl_ = ((Component) this.npc06_).gameObject.GetComponentInChildren<PlayerAnimCtrl>();
      this.jaw_ = Utility.Find(this.npc06_._transform, "Jaw");
      this.info_.dragonJaw = this.jaw_;
      this.tempDefaultAnim_ = this.npc06AnimCtrl_.defaultAnim;
      this.npc06AnimCtrl_.defaultAnim = PLCA.LOGIN_IDLE;
      this.tempAction_ = this.npc06AnimCtrl_.onEnd;
      this.npc06AnimCtrl_.onEnd = (Action<PlayerAnimCtrl, PLCA>) null;
    }

    private bool Phase00()
    {
      if (this.info_.goNextNpc06)
      {
        this.info_.goNextNpc06 = false;
        this.npc06AnimCtrl_.Play(PLCA.LOGIN_FIRE);
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase01);
        this.info_.goNextFireball = true;
      }
      return true;
    }

    private bool Phase01()
    {
      if (this.info_.goNextNpc06)
      {
        this.info_.goNextNpc06 = false;
        this.npc06AnimCtrl_.defaultAnim = this.tempDefaultAnim_;
        this.npc06AnimCtrl_.onEnd = this.tempAction_;
        this.npc06AnimCtrl_.moveAnim = PLCA.LOGIN_FLY;
        this.npc06AnimCtrl_.Play(PLCA.LOGIN_FLY);
        this.npc06_.PopState();
        this.npc06_.PushLeave();
        this.ToExit();
      }
      return true;
    }
  }

  private class FSMFireball : HomeLoginBonusTheater.FSM
  {
    private Transform fireball_;
    private Transform fireEffect1_;
    private Transform fireEffect2_;
    private Vector3 endPos_ = Vector3.zero;
    private Vector3 dir_ = Vector3.up;
    private float velocity_ = 0.098f;
    private Vector3 position_;

    public override void Init(HomeLoginBonusTheater.FSMInfo info)
    {
      base.Init(info);
      this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase00);
      this.fireball_ = info.fireball;
      this.fireEffect1_ = info.fireEffect1;
      this.fireEffect2_ = info.fireEffect2;
      this.endPos_ = info.fireballEndPos;
      this.fireEffect1_.position = this.endPos_;
      this.fireEffect2_.position = this.endPos_;
    }

    private bool Phase00()
    {
      if (this.info_.goNextFireball)
      {
        this.info_.goNextFireball = false;
        float time = 1.06f;
        if (this.info_.dayIndex % 3 == 1)
          time = 0.99f;
        else if (this.info_.dayIndex % 3 == 2)
          time = 0.9f;
        this.StartTimer(time);
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase01);
      }
      return true;
    }

    private bool Phase01()
    {
      if (this.IsWaitComplete())
      {
        ((Component) this.fireball_).gameObject.SetActive(true);
        this.position_ = this.info_.dragonJaw.TransformPoint(new Vector3(-0.12f, 0.0f, 0.01f));
        this.dir_ = Vector3.op_Subtraction(this.endPos_, this.position_);
        ((Vector3) ref this.dir_).Normalize();
        this.fireball_.position = this.position_;
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase02);
        HomeLoginBonusTheater.PlayAudio(HomeLoginBonusTheater.AUDIO.SE_FIRE);
      }
      return true;
    }

    private bool Phase02()
    {
      this.position_ = Vector3.op_Addition(this.position_, Vector3.op_Multiply(this.dir_, this.velocity_));
      this.fireball_.position = this.position_;
      Vector3 vector3 = Vector3.op_Subtraction(this.endPos_, this.position_);
      ((Vector3) ref vector3).Normalize();
      if (0.800000011920929 >= (double) Vector3.Dot(vector3, this.dir_))
      {
        this.position_ = this.endPos_;
        this.fireball_.position = this.endPos_;
        rymFX component = ((Component) this.fireball_).gameObject.GetComponent<rymFX>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.AutoDelete = true;
          component.LoopEnd = true;
        }
        ((Component) this.fireEffect1_).gameObject.SetActive(true);
        ((Component) this.fireEffect2_).gameObject.SetActive(true);
        this.info_.goNextBoard = true;
        this.info_.goNextNpc00 = true;
        this.info_.goNextBoard = true;
        this.StartTimer(0.5f);
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase03);
      }
      return true;
    }

    private bool Phase03()
    {
      if (this.IsWaitComplete())
      {
        rymFX component = ((Component) this.fireEffect2_).gameObject.GetComponent<rymFX>();
        if (Object.op_Inequality((Object) null, (Object) component))
        {
          component.AutoDelete = true;
          component.LoopEnd = true;
        }
        this.ToExit();
      }
      return true;
    }
  }

  private class FSMBoard : HomeLoginBonusTheater.FSM
  {
    private Material paperMat_;
    private Material panelMat_;
    private Material panel2Mat_;
    private Transform light_;
    private float offset_;
    private float speed_;
    private Vector3 itemEndPos_ = new Vector3(-0.004f, 1.431f, 7.3f);
    private float itemEndScale_ = 0.4128781f;
    private Vector3 moveDir_ = Vector3.up;
    private Vector3 movePos_ = Vector3.zero;
    private Vector3 lightPosOffset_ = new Vector3(0.0f, 0.0f, 0.2f);
    private float moveScale_ = 1f;
    private Transform itemModel_;
    private int animFrame_ = 17;
    private float scaleVelocity_;
    private float itemMoveTime = 0.84f;
    private Vector3 itemInitPos;
    private float itemStartScale;
    private float phase3Time;
    private float endRotation = 720f;
    private float rotation;

    public override void Init(HomeLoginBonusTheater.FSMInfo info)
    {
      base.Init(info);
      this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase00);
      this.paperMat_ = info.todayPaper;
      this.panelMat_ = info.todayPanel;
      this.panel2Mat_ = info.todayPanel2;
      this.light_ = info.light;
      this.itemModel_ = info.itemModel;
    }

    private bool Phase00()
    {
      if (this.info_.goNextBoard)
      {
        this.info_.goNextBoard = false;
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase01);
        this.offset_ = 0.0f;
        this.speed_ = 0.2f;
        ((Component) this.itemModel_).gameObject.SetActive(true);
        this.itemModel_.position = this.info_.fireballEndPos;
        HomeLoginBonusTheater.PlayAudio(HomeLoginBonusTheater.AUDIO.SE_TOP);
        this.movePos_ = this.info_.fireballEndPos;
        this.light_.position = Vector3.op_Addition(this.movePos_, this.lightPosOffset_);
        this.itemInitPos = this.itemModel_.position;
        this.itemStartScale = this.itemModel_.localScale.x;
      }
      return true;
    }

    private bool Phase01()
    {
      this.offset_ += this.speed_;
      this.paperMat_.SetFloat("_Offset", this.offset_);
      this.panelMat_.SetFloat("_Offset", this.offset_);
      this.panel2Mat_.SetFloat("_Offset", this.offset_);
      if ((double) this.offset_ > 0.5)
        this.Phase03();
      if (1.0 < (double) this.offset_)
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase03);
      return true;
    }

    private bool Phase02()
    {
      if (this.IsWaitComplete())
      {
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase03);
        this.scaleVelocity_ = (this.itemEndScale_ - this.itemModel_.localScale.x) / (float) this.animFrame_;
        this.movePos_ = this.info_.fireballEndPos;
        this.moveScale_ = this.itemModel_.localScale.x;
        this.itemModel_.position = this.movePos_;
        this.light_.position = Vector3.op_Addition(this.movePos_, this.lightPosOffset_);
        this.moveDir_ = Vector3.op_Subtraction(this.itemEndPos_, this.movePos_);
        ((Vector3) ref this.moveDir_).Normalize();
        ((Component) this.light_).gameObject.SetActive(true);
        this.itemInitPos = this.itemModel_.position;
        this.itemStartScale = this.itemModel_.localScale.x;
      }
      return true;
    }

    private bool Phase03()
    {
      this.phase3Time += Time.deltaTime;
      float num1 = this.phase3Time / this.itemMoveTime;
      float num2 = this.info_.moveCurve.Evaluate(num1);
      float num3 = this.info_.scaleCurve.Evaluate(num1);
      this.movePos_ = Vector3.Lerp(this.itemInitPos, this.itemEndPos_, num2);
      this.moveScale_ = (this.itemEndScale_ - this.itemStartScale) * num3 + this.itemStartScale;
      this.rotation = this.endRotation * num1;
      if ((double) this.phase3Time > (double) this.itemMoveTime)
      {
        ((Component) this.light_).gameObject.SetActive(true);
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase05);
        this.movePos_ = this.itemEndPos_;
        this.moveScale_ = this.itemEndScale_;
        this.rotation = this.endRotation;
      }
      this.itemModel_.position = this.movePos_;
      this.itemModel_.localScale = new Vector3(this.moveScale_, this.moveScale_, this.moveScale_);
      this.itemModel_.localRotation = Quaternion.AngleAxis(this.rotation, Vector3.up);
      this.light_.position = Vector3.op_Addition(this.movePos_, this.lightPosOffset_);
      return true;
    }

    private bool Phase04()
    {
      this.moveScale_ += this.scaleVelocity_;
      if ((double) this.moveScale_ >= (double) this.itemEndScale_)
      {
        this.moveScale_ = this.itemEndScale_;
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase05);
      }
      this.itemModel_.localScale = new Vector3(this.moveScale_, this.moveScale_, this.moveScale_);
      return true;
    }

    private bool Phase05()
    {
      if (this.info_.goNextBoard)
      {
        ((Component) this.itemModel_).gameObject.SetActive(false);
        ((Component) this.light_).gameObject.SetActive(false);
        this.ToExit();
      }
      return true;
    }
  }

  private class FSMCamera : HomeLoginBonusTheater.FSM
  {
    public override void Init(HomeLoginBonusTheater.FSMInfo info)
    {
      base.Init(info);
      this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase00);
    }

    private bool Phase00()
    {
      if (this.info_.goNextCamera)
      {
        this.info_.goNextCamera = false;
        this.act_ = new HomeLoginBonusTheater.FSM.Act(this.Phase01);
        OutGameSettingsManager.LoginBonusScene loginBonusScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.loginBonusScene;
        Vector3 cameraPos = loginBonusScene.cameraPos;
        Vector3 cameraRot = loginBonusScene.cameraRot;
        this.info_.interpolator.Translate(0.7f, cameraPos, add_value: new Vector3());
        this.info_.interpolator.Rotate(0.7f, cameraRot, add_value: new Vector3());
      }
      return true;
    }

    private bool Phase01()
    {
      if (!this.info_.interpolator.IsPlaying())
      {
        this.info_.goNextNpc06 = true;
        this.ToExit();
      }
      return true;
    }
  }
}
