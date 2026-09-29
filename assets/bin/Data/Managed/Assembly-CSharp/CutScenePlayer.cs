// Decompiled with JetBrains decompiler
// Type: CutScenePlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CutScenePlayer : MonoBehaviour
{
  private Transform _transform;
  [SerializeField]
  private CutSceneData cutSceneData;
  private bool isInitialized;
  private System.Action onComplete;
  private const int MAX_CAMERA_NUM = 2;
  private CutScenePlayer.CutSceneCamera[] cameras = new CutScenePlayer.CutSceneCamera[2];
  private Animator cameraAnimator;
  private Transform cameraAnimatorTransform;
  private const int MAX_PLAYER_NUM = 4;
  private CutScenePlayer.PlayerInfo[] playerInfo;
  private CutScenePlayer.EnemyInfo enemyInfo;
  private CutScenePlayer.ActorInfo[] actorInfo;
  private CutScenePlayer.EffectInfo[] effectInfo;
  private CutScenePlayer.SeInfo[] seInfo;
  [SerializeField]
  private int cutNo;
  [SerializeField]
  private float playingTime;
  private float oldTime;
  private const int MAX_CUT_SCENE_ANIMATOR_STATE_NUM = 30;
  private int[] CUT_STATE_HASH = new int[30];
  private int ENDING_STATE_ID;

  public bool hasStory
  {
    get
    {
      return !Object.op_Equality((Object) this.cutSceneData, (Object) null) && this.cutSceneData.storyId != 0;
    }
  }

  public bool isPlaying { private set; get; }

  private void Awake()
  {
    this._transform = ((Component) this).transform;
    this.cameraAnimator = new GameObject("CameraController")
    {
      transform = {
        parent = this._transform
      }
    }.AddComponent<Animator>();
    this.cameraAnimatorTransform = ((Component) this.cameraAnimator).transform;
    for (int index = 0; index < 30; ++index)
      this.CUT_STATE_HASH[index] = Animator.StringToHash("Cut_" + index.ToString("D3"));
    this.ENDING_STATE_ID = Animator.StringToHash("END");
  }

  public void Init(string cutSceneDataPath, Action<bool> _onComplete)
  {
    this.StartCoroutine(this.InitImpl(cutSceneDataPath, _onComplete));
  }

  private IEnumerator InitImpl(string cutSceneDataPath, Action<bool> _onComplete)
  {
    LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
    if (!string.IsNullOrEmpty(cutSceneDataPath))
    {
      LoadObject loadedCutSceneObj = loadQueue.Load(RESOURCE_CATEGORY.CUTSCENE, cutSceneDataPath);
      if (loadQueue.IsLoading())
        yield return (object) loadQueue.Wait();
      this.cutSceneData = loadedCutSceneObj.loadedObject as CutSceneData;
      loadedCutSceneObj = (LoadObject) null;
    }
    if (Object.op_Equality((Object) this.cutSceneData, (Object) null))
    {
      if (_onComplete != null)
        _onComplete(false);
    }
    else
    {
      for (int index = 0; index < this.cutSceneData.seDataList.Count; ++index)
        loadQueue.CacheSE(this.cutSceneData.seDataList[index].seId);
      for (int index = 0; index < this.cutSceneData.effectKeyData.Count; ++index)
        loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, this.cutSceneData.effectKeyData[index].effectId);
      for (int index = 0; index < 2; ++index)
      {
        if (this.cameras[index] == null)
        {
          CutScenePlayer.CutSceneCamera cutSceneCamera = new CutScenePlayer.CutSceneCamera();
          GameObject gameObject = new GameObject("cut_scene_camera_ " + index.ToString());
          cutSceneCamera.camera = gameObject.AddComponent<Camera>();
          cutSceneCamera.transform = gameObject.transform;
          cutSceneCamera.transform.parent = this._transform;
          gameObject.SetActive(false);
          this.cameras[index] = cutSceneCamera;
        }
      }
      this.playerInfo = new CutScenePlayer.PlayerInfo[4];
      this.enemyInfo = new CutScenePlayer.EnemyInfo();
      this.actorInfo = new CutScenePlayer.ActorInfo[this.cutSceneData.actorData.Length];
      this.effectInfo = new CutScenePlayer.EffectInfo[this.cutSceneData.effectKeyData.Count];
      this.seInfo = new CutScenePlayer.SeInfo[this.cutSceneData.seDataList.Count];
      for (int index = 0; index < this.cutSceneData.actorData.Length; ++index)
      {
        CutSceneData.ActorData actorData = this.cutSceneData.actorData[index];
        this.actorInfo[index] = new CutScenePlayer.ActorInfo();
        this.actorInfo[index].keyData = actorData;
        this.actorInfo[index].obj = Object.Instantiate<GameObject>(actorData.prefab);
        this.actorInfo[index].obj.transform.parent = this._transform;
        this.actorInfo[index].animator = this.actorInfo[index].obj.GetComponent<Animator>();
        this.actorInfo[index].animator.cullingMode = (AnimatorCullingMode) 0;
        this.actorInfo[index].animator.runtimeAnimatorController = actorData.animatorController;
        this.actorInfo[index].animator.Rebind();
        this.actorInfo[index].obj.SetActive(false);
      }
      for (int index = 0; index < this.cutSceneData.effectKeyData.Count; ++index)
        this.effectInfo[index] = new CutScenePlayer.EffectInfo()
        {
          keyData = this.cutSceneData.effectKeyData[index]
        };
      for (int index = 0; index < this.cutSceneData.seDataList.Count; ++index)
        this.seInfo[index] = new CutScenePlayer.SeInfo()
        {
          keyData = this.cutSceneData.seDataList[index]
        };
      if (loadQueue.IsLoading())
        yield return (object) loadQueue.Wait();
      if (_onComplete != null)
        _onComplete(true);
    }
  }

  private void OnDestroy()
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, (Object) null))
      return;
    ((Component) MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform).gameObject.SetActive(true);
  }

  public void Play(System.Action _onComplete = null)
  {
    if (Object.op_Equality((Object) this.cutSceneData, (Object) null))
    {
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameMain", ((Component) this).gameObject, "HOME");
      if (_onComplete == null)
        return;
      _onComplete();
    }
    else
    {
      this.onComplete = _onComplete;
      this.isPlaying = true;
      this.cutNo = 0;
      this.playingTime = 0.0f;
      this.oldTime = 0.0f;
      MonoBehaviourSingleton<SoundManager>.I.TransitionTo(this.cutSceneData.mixerName);
      if (this.cutSceneData.bgm != 0)
        SoundManager.RequestBGM(this.cutSceneData.bgm);
      this.cameraAnimator.cullingMode = (AnimatorCullingMode) 0;
      this.cameraAnimator.runtimeAnimatorController = this.cutSceneData.cameraController;
      this.cameraAnimator.Play(this.CUT_STATE_HASH[this.cutNo]);
      this.UpdateCamera();
      ((Component) MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform).gameObject.SetActive(false);
      for (int index = 0; index < this.playerInfo.Length; ++index)
        this.playerInfo[index] = (CutScenePlayer.PlayerInfo) null;
      for (int index1 = 0; index1 < this.cutSceneData.playerData.Length; ++index1)
      {
        CutSceneData.PlayerData playerData = this.cutSceneData.playerData[index1];
        if (playerData != null)
        {
          if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
          {
            StageObjectManager i = MonoBehaviourSingleton<StageObjectManager>.I;
            Player player = (Player) null;
            if (playerData.type == CutSceneData.PlayerData.TYPE.MY_CHARACTER)
              player = (Player) i.self;
            else if ((CutSceneData.PlayerData.TYPE) i.playerList.Count > playerData.type)
            {
              int num = 0;
              for (int index2 = 0; index2 < i.playerList.Count; ++index2)
              {
                if (!(i.playerList[index2] is Self))
                {
                  if (playerData.type <= (CutSceneData.PlayerData.TYPE) num)
                  {
                    player = i.playerList[index2] as Player;
                    break;
                  }
                  ++num;
                }
              }
              if (Object.op_Equality((Object) player, (Object) null))
              {
                for (int index3 = 0; index3 < i.playerList.Count; ++index3)
                {
                  if (playerData.type <= (CutSceneData.PlayerData.TYPE) num)
                  {
                    player = i.nonplayerList[index3] as Player;
                    break;
                  }
                  ++num;
                }
              }
              if (Object.op_Equality((Object) player, (Object) null))
                continue;
            }
            else
              continue;
            CutScenePlayer.PlayerInfo playerInfo = new CutScenePlayer.PlayerInfo();
            playerInfo.obj = ((Component) player).gameObject;
            playerInfo.animator = player.animator;
            playerInfo.originalController = player.animator.runtimeAnimatorController;
            playerInfo.keyData = playerData;
            player.ActIdle(false, -1f);
            player._collider.enabled = false;
            player._rigidbody.constraints = (RigidbodyConstraints) 126;
            player._transform.position = playerData.startPos;
            player._transform.rotation = Quaternion.AngleAxis(playerData.startAngleY, Vector3.up);
            player.animator.cullingMode = (AnimatorCullingMode) 0;
            player.animator.runtimeAnimatorController = playerData.controller;
            player.animator.Rebind();
            this.playerInfo[index1] = playerInfo;
          }
          else
            break;
        }
      }
      for (int index = 0; index < this.playerInfo.Length; ++index)
      {
        if (this.playerInfo[index] != null && !Object.op_Equality((Object) this.playerInfo[index].obj, (Object) null))
        {
          List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
          for (int j = 0; j < playerList.Count; ++j)
          {
            if (Array.Find<CutScenePlayer.PlayerInfo>(this.playerInfo, (Predicate<CutScenePlayer.PlayerInfo>) (info => info != null && Object.op_Equality((Object) info.obj, (Object) ((Component) playerList[j]).gameObject))) == null)
              ((Component) playerList[j]).gameObject.SetActive(false);
          }
          List<StageObject> npcList = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList;
          for (int j = 0; j < npcList.Count; ++j)
          {
            if (Array.Find<CutScenePlayer.PlayerInfo>(this.playerInfo, (Predicate<CutScenePlayer.PlayerInfo>) (info => info != null && Object.op_Equality((Object) info.obj, (Object) ((Component) npcList[j]).gameObject))) == null)
              ((Component) npcList[j]).gameObject.SetActive(false);
          }
        }
      }
      if (this.cutSceneData.enemyData != null && MonoBehaviourSingleton<StageObjectManager>.IsValid())
      {
        CutSceneData.EnemyData enemyData = this.cutSceneData.enemyData;
        CutScenePlayer.EnemyInfo enemyInfo = new CutScenePlayer.EnemyInfo();
        Debug.Log((object) enemyData.startPos.y);
        Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
        enemyInfo.obj = ((Component) boss).gameObject;
        enemyInfo.animator = boss.animator;
        enemyInfo.originalController = boss.animator.runtimeAnimatorController;
        enemyInfo.keyData = enemyData;
        this.enemyInfo = enemyInfo;
        if (Object.op_Inequality((Object) boss.controller, (Object) null))
        {
          ((Behaviour) boss.controller).enabled = false;
          if (Object.op_Inequality((Object) boss._rigidbody, (Object) null))
            boss._rigidbody.constraints = (RigidbodyConstraints) 126;
        }
        boss.ActIdle();
        boss.animator.cullingMode = (AnimatorCullingMode) 0;
        boss.animator.runtimeAnimatorController = enemyData.controller;
        boss.animator.Rebind();
        boss._transform.position = enemyData.startPos;
        boss._transform.rotation = Quaternion.AngleAxis(enemyData.startAngleY, Vector3.up);
        boss.animator.Play(this.CUT_STATE_HASH[this.cutNo]);
      }
      for (int index = 0; index < this.actorInfo.Length; ++index)
      {
        if (this.actorInfo[index] != null)
        {
          this.actorInfo[index].obj.SetActive(true);
          bool flag = this.actorInfo[index].animator.HasState(0, this.CUT_STATE_HASH[this.cutNo]);
          if (flag)
          {
            this.actorInfo[index].obj.SetActive(flag);
            CutSceneData.ActorData keyData = this.actorInfo[index].keyData;
            Transform parentNode = this.GetParentNode(keyData.attachmentType, keyData.nodeName);
            if (Object.op_Inequality((Object) parentNode, (Object) null))
              this.actorInfo[index].obj.transform.parent = parentNode;
            this.actorInfo[index].obj.transform.localPosition = keyData.position;
            this.actorInfo[index].obj.transform.localRotation = Quaternion.Euler(keyData.rotation);
            foreach (SkinnedMeshRenderer componentsInChild in this.actorInfo[index].obj.GetComponentsInChildren<SkinnedMeshRenderer>())
              componentsInChild.localBounds = new Bounds(Vector3.zero, Vector3.op_Multiply(Vector3.one, 10000f));
            this.actorInfo[index].animator.Play(this.CUT_STATE_HASH[this.cutNo]);
          }
        }
      }
    }
  }

  private void Update()
  {
    if (!this.isPlaying)
      return;
    this.UpdateCamera();
    this.oldTime = this.playingTime;
    this.playingTime += Time.deltaTime;
    this.UpdatePlayer();
    this.UpdateEnemy();
    this.UpdateActor();
    this.UpdateSound();
    this.UpdateEffect();
  }

  private CutScenePlayer.CutSceneCamera GetActiveCamera() => this.cameras[this.cutNo % 2];

  private void UpdateCamera()
  {
    AnimatorStateInfo animatorStateInfo = this.cameraAnimator.GetCurrentAnimatorStateInfo(0);
    if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime >= 1.0)
    {
      ++this.cutNo;
      if (this.cameraAnimator.HasState(0, this.CUT_STATE_HASH[this.cutNo]))
        this.cameraAnimator.Play(this.CUT_STATE_HASH[this.cutNo]);
      else if (this.cameraAnimator.HasState(0, this.ENDING_STATE_ID))
      {
        this.cameraAnimator.Play(this.ENDING_STATE_ID);
        this.EndCutScene();
      }
      else
        this.EndCutScene();
    }
    int num = this.cutNo % 2;
    for (int index = 0; index < this.cameras.Length; ++index)
      ((Component) this.cameras[index].camera).gameObject.SetActive(num == index);
    CutScenePlayer.CutSceneCamera activeCamera = this.GetActiveCamera();
    activeCamera.transform.position = this.cameraAnimatorTransform.position;
    activeCamera.transform.rotation = this.cameraAnimatorTransform.rotation;
    activeCamera.camera.fieldOfView = this.cameraAnimatorTransform.localScale.x;
  }

  private void EndCutScene()
  {
    if (this.onComplete != null)
      this.onComplete();
    this.isPlaying = false;
    if (!MonoBehaviourSingleton<GameSceneManager>.IsValid() || !this.hasStory)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameMain", ((Component) this).gameObject, "STORY", (object) new object[4]
    {
      (object) this.cutSceneData.storyId,
      (object) 0,
      (object) 0,
      (object) GameSection.GetGoingHomeEvent()
    });
  }

  private void UpdatePlayer()
  {
    for (int index = 0; index < this.playerInfo.Length; ++index)
    {
      if (this.playerInfo[index] != null)
      {
        AnimatorStateInfo animatorStateInfo = this.playerInfo[index].animator.GetCurrentAnimatorStateInfo(0);
        if (((AnimatorStateInfo) ref animatorStateInfo).shortNameHash != this.CUT_STATE_HASH[this.cutNo])
          this.playerInfo[index].animator.Play(this.CUT_STATE_HASH[this.cutNo]);
      }
    }
  }

  private void UpdateEnemy()
  {
    AnimatorStateInfo animatorStateInfo = this.enemyInfo.animator.GetCurrentAnimatorStateInfo(0);
    if (((AnimatorStateInfo) ref animatorStateInfo).shortNameHash == this.CUT_STATE_HASH[this.cutNo])
      return;
    this.enemyInfo.animator.Play(this.CUT_STATE_HASH[this.cutNo]);
  }

  private void UpdateActor()
  {
    for (int index = 0; index < this.actorInfo.Length; ++index)
    {
      CutScenePlayer.ActorInfo actorInfo = this.actorInfo[index];
      if (actorInfo != null)
      {
        if (actorInfo.animator.HasState(0, this.CUT_STATE_HASH[this.cutNo]))
        {
          actorInfo.obj.SetActive(true);
          AnimatorStateInfo animatorStateInfo = actorInfo.animator.GetCurrentAnimatorStateInfo(0);
          if (((AnimatorStateInfo) ref animatorStateInfo).shortNameHash != this.CUT_STATE_HASH[this.cutNo])
          {
            actorInfo.animator.Play(this.CUT_STATE_HASH[this.cutNo]);
            CutSceneData.ActorData keyData = actorInfo.keyData;
            Transform parentNode = this.GetParentNode(keyData.attachmentType, keyData.nodeName);
            if (Object.op_Inequality((Object) parentNode, (Object) null))
              actorInfo.obj.transform.parent = parentNode;
            actorInfo.obj.transform.localPosition = keyData.position;
            actorInfo.obj.transform.localRotation = Quaternion.Euler(keyData.rotation);
          }
        }
        else
          actorInfo.obj.SetActive(false);
      }
    }
  }

  private void UpdateSound()
  {
    for (int index = 0; index < this.seInfo.Length; ++index)
    {
      CutScenePlayer.SeInfo seInfo = this.seInfo[index];
      if (seInfo != null && !seInfo.isPlayed && (double) this.oldTime <= (double) seInfo.keyData.time && (double) seInfo.keyData.time <= (double) this.playingTime)
      {
        SoundManager.PlayOneShotUISE(seInfo.keyData.seId);
        seInfo.isPlayed = true;
      }
    }
  }

  private Transform GetPlayerNode(CutSceneData.ATTACHMENT_TYPE attachmentType)
  {
    CutSceneData.PlayerData.TYPE type;
    switch (attachmentType)
    {
      case CutSceneData.ATTACHMENT_TYPE.MY_CHARACTER:
        type = CutSceneData.PlayerData.TYPE.MY_CHARACTER;
        break;
      case CutSceneData.ATTACHMENT_TYPE.PLAYER_1:
        type = CutSceneData.PlayerData.TYPE.PLAYER_1;
        break;
      case CutSceneData.ATTACHMENT_TYPE.PLAYER_2:
        type = CutSceneData.PlayerData.TYPE.PLAYER_2;
        break;
      case CutSceneData.ATTACHMENT_TYPE.PLAYER_3:
        type = CutSceneData.PlayerData.TYPE.PLAYER_3;
        break;
      default:
        return (Transform) null;
    }
    if (this.playerInfo != null)
    {
      for (int index = 0; index < this.playerInfo.Length; ++index)
      {
        if (this.playerInfo[index] != null && this.playerInfo[index].keyData.type == type)
          return this.playerInfo[index].obj.transform;
      }
    }
    return (Transform) null;
  }

  private Transform GetActorTransform(int index)
  {
    return this.actorInfo != null && index < this.actorInfo.Length && this.actorInfo[index] != null && Object.op_Inequality((Object) this.actorInfo[index].obj, (Object) null) ? this.actorInfo[index].obj.transform : (Transform) null;
  }

  private Transform FindChildTransform(Transform root, string name)
  {
    if (Object.op_Equality((Object) root, (Object) null))
      return (Transform) null;
    int childCount = root.childCount;
    for (int index = 0; index < childCount; ++index)
    {
      Transform child = root.GetChild(index);
      if (((Object) child).name == name)
        return child;
      Transform childTransform = this.FindChildTransform(child, name);
      if (Object.op_Inequality((Object) childTransform, (Object) null))
        return childTransform;
    }
    return (Transform) null;
  }

  private Transform GetParentNode(CutSceneData.ATTACHMENT_TYPE attachmentType, string nodeName)
  {
    if (attachmentType == CutSceneData.ATTACHMENT_TYPE.NONE)
      return (Transform) null;
    Transform root = (Transform) null;
    switch (attachmentType - 1)
    {
      case CutSceneData.ATTACHMENT_TYPE.NONE:
        root = this.GetActiveCamera().transform;
        break;
      case CutSceneData.ATTACHMENT_TYPE.CAMERA:
      case CutSceneData.ATTACHMENT_TYPE.MY_CHARACTER:
      case CutSceneData.ATTACHMENT_TYPE.PLAYER_1:
      case CutSceneData.ATTACHMENT_TYPE.PLAYER_2:
        root = this.GetPlayerNode(attachmentType);
        break;
      case CutSceneData.ATTACHMENT_TYPE.PLAYER_3:
        if (this.enemyInfo != null && Object.op_Inequality((Object) this.enemyInfo.obj, (Object) null))
        {
          root = this.enemyInfo.obj.transform;
          break;
        }
        break;
      case CutSceneData.ATTACHMENT_TYPE.ENEMY:
        root = this.GetActorTransform(0);
        break;
      case CutSceneData.ATTACHMENT_TYPE.ACTOR_1:
        root = this.GetActorTransform(1);
        break;
      case CutSceneData.ATTACHMENT_TYPE.ACTOR_2:
        root = this.GetActorTransform(2);
        break;
      case CutSceneData.ATTACHMENT_TYPE.ACTOR_3:
        root = this.GetActorTransform(3);
        break;
    }
    if (!Object.op_Inequality((Object) root, (Object) null))
      return (Transform) null;
    return string.IsNullOrEmpty(nodeName) ? root : this.FindChildTransform(root, nodeName);
  }

  private void UpdateEffect()
  {
    for (int index = 0; index < this.effectInfo.Length; ++index)
    {
      CutScenePlayer.EffectInfo effectInfo = this.effectInfo[index];
      if (effectInfo != null && !effectInfo.isPlayed && (double) this.oldTime <= (double) effectInfo.keyData.time && (double) effectInfo.keyData.time <= (double) this.playingTime)
      {
        Transform parentNode = this.GetParentNode(effectInfo.keyData.attachmentType, effectInfo.keyData.nodeName);
        Transform effect = EffectManager.GetEffect(effectInfo.keyData.effectId, parentNode);
        if (Object.op_Inequality((Object) effect, (Object) null))
        {
          effect.localPosition = effectInfo.keyData.position;
          effect.localRotation = Quaternion.Euler(effectInfo.keyData.rotation);
        }
        effectInfo.isPlayed = true;
      }
    }
  }

  public class CutSceneCamera
  {
    public Camera camera;
    public Transform transform;
  }

  public class PlayerInfo
  {
    public GameObject obj;
    public Animator animator;
    public RuntimeAnimatorController originalController;
    public CutSceneData.PlayerData keyData;
  }

  public class EnemyInfo
  {
    public GameObject obj;
    public Animator animator;
    public RuntimeAnimatorController originalController;
    public CutSceneData.EnemyData keyData;
  }

  public class ActorInfo
  {
    public GameObject obj;
    public Animator animator;
    public RuntimeAnimatorController originalController;
    public CutSceneData.ActorData keyData;
  }

  public class EffectInfo
  {
    public CutSceneData.EffectKeyData keyData;
    public bool isPlayed;
  }

  public class SeInfo
  {
    public CutSceneData.SEKeyData keyData;
    public bool isPlayed;
  }
}
