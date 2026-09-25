// Decompiled with JetBrains decompiler
// Type: AnimationDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using UnityEngine;

#nullable disable
public class AnimationDirector : MonoBehaviour
{
  public const string TAG = "Direction";
  public Camera useCamera;
  public GameObject fader;
  public Component commandReceiver;
  protected bool skip;
  private bool linkCamera;
  private Transform saveCameraParamsObject;
  private Camera saveCameraParams;
  private Vector3 saveCameraPos;
  private Quaternion saveCameraRot;
  private bool isDestroy;
  private Animator _animator;
  private int playingStateHash;
  private System.Action endCallback;

  public static AnimationDirector I { get; private set; }

  public void __FUNCTION__InstantiatePrefab(string game_object_name)
  {
    GameObject gameObject = GameObject.Find(game_object_name);
    if (Object.op_Equality((Object) gameObject, (Object) null))
      return;
    DirectionPrefabObject component = gameObject.GetComponent<DirectionPrefabObject>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    this.CreateEffect(component.prefab, ((Component) component).transform);
  }

  public void __FUNCTION__PlayAudio(string game_object_name)
  {
    GameObject gameObject = GameObject.Find(game_object_name);
    if (Object.op_Equality((Object) gameObject, (Object) null))
      return;
    AudioSource component = gameObject.GetComponent<AudioSource>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    gameObject.tag = "Direction";
    component.Play();
  }

  public virtual void __FUNCTION__PlayCachedAudio(int se_id) => SoundManager.PlayOneShotUISE(se_id);

  public void __FUNCTION_Command(string command)
  {
    if (!Object.op_Inequality((Object) this.commandReceiver, (Object) null))
      return;
    this.commandReceiver.SendMessage("OnDirectionCommand", (object) command);
  }

  protected Transform CreateEffect(GameObject effect_prefab, Transform parent)
  {
    Transform effect = ResourceUtility.Realizes((Object) effect_prefab, parent);
    if (Object.op_Equality((Object) effect, (Object) null))
      return (Transform) null;
    ((Component) effect).gameObject.tag = "Direction";
    rymFX component = ((Component) effect).GetComponent<rymFX>();
    if (Object.op_Inequality((Object) component, (Object) null) && Object.op_Inequality((Object) this.useCamera, (Object) null))
      component.Cameras = new Camera[1]
      {
        MonoBehaviourSingleton<AppMain>.I.mainCamera
      };
    return effect;
  }

  protected virtual void OnEnable() => AnimationDirector.I = this;

  protected virtual void OnDisable()
  {
    if (!Object.op_Equality((Object) AnimationDirector.I, (Object) this))
      return;
    AnimationDirector.I = (AnimationDirector) null;
  }

  protected virtual void Awake()
  {
    AnimationDirector.I = this;
    this._animator = ((Component) this).GetComponent<Animator>();
    if (MonoBehaviourSingleton<AppMain>.IsValid() && Object.op_Inequality((Object) this.useCamera, (Object) null))
      ((Behaviour) this.useCamera).enabled = false;
    if (!Object.op_Inequality((Object) this.fader, (Object) null))
      return;
    this.fader.SetActive(false);
  }

  protected virtual void OnDestroy()
  {
    if (AppMain.isApplicationQuit)
      return;
    this.isDestroy = true;
    this.SetLinkCamera(false);
    if (!Object.op_Equality((Object) AnimationDirector.I, (Object) this))
      return;
    AnimationDirector.I = (AnimationDirector) null;
  }

  public void Play(string state_name, System.Action end_callback = null, float normalizedTime = 0.0f)
  {
    this.playingStateHash = Animator.StringToHash("Base Layer." + state_name);
    this.endCallback = end_callback;
    ((Behaviour) this._animator).enabled = true;
    this._animator.Play(this.playingStateHash, 0, normalizedTime);
    this._animator.Update(0.0f);
  }

  public void SetAnimatorInteger(string name, int value) => this._animator.SetInteger(name, value);

  public bool isPlaying => this.playingStateHash != 0;

  protected virtual void Update()
  {
    if (Object.op_Equality((Object) this._animator, (Object) null))
      return;
    this._animator.speed = this.skip ? 10000f : 1f;
    if (this.playingStateHash == 0)
      return;
    AnimatorStateInfo animatorStateInfo = this._animator.GetCurrentAnimatorStateInfo(0);
    if (((AnimatorStateInfo) ref animatorStateInfo).fullPathHash != this.playingStateHash || (double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
      return;
    this.playingStateHash = 0;
    if (this.endCallback == null)
      return;
    System.Action endCallback = this.endCallback;
    this.endCallback = (System.Action) null;
    endCallback();
  }

  protected virtual void LateUpdate()
  {
    if (!this.linkCamera)
      return;
    MonoBehaviourSingleton<AppMain>.I.mainCamera.CopyFrom(this.useCamera);
  }

  public void SetLinkCamera(bool is_link)
  {
    if (Object.op_Equality((Object) this.useCamera, (Object) null) || this.linkCamera == is_link)
      return;
    this.linkCamera = is_link;
    if (is_link)
    {
      if (Object.op_Equality((Object) this.saveCameraParams, (Object) null))
      {
        this.saveCameraParamsObject = Utility.CreateGameObject("saveCameraParams", ((Component) this).transform);
        this.saveCameraParams = ((Component) this.saveCameraParamsObject).gameObject.AddComponent<Camera>();
      }
      Transform transform = ((Component) this).transform;
      Vector3 position = transform.position;
      Quaternion rotation = transform.rotation;
      this.saveCameraParams.CopyFrom(MonoBehaviourSingleton<AppMain>.I.mainCamera);
      this.saveCameraPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
      this.saveCameraRot = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation;
      transform.position = position;
      transform.rotation = rotation;
      ((Behaviour) this.saveCameraParams).enabled = false;
      if (!Object.op_Inequality((Object) this.fader, (Object) null))
        return;
      this.fader.SetActive(true);
    }
    else
    {
      if (Object.op_Inequality((Object) this.saveCameraParams, (Object) null))
      {
        MonoBehaviourSingleton<AppMain>.I.mainCamera.CopyFrom(this.saveCameraParams);
        MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = this.saveCameraPos;
        MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation = this.saveCameraRot;
        if (!this.isDestroy)
          Object.DestroyImmediate((Object) ((Component) this.saveCameraParamsObject).gameObject);
        this.saveCameraParamsObject = (Transform) null;
        this.saveCameraParams = (Camera) null;
      }
      if (!Object.op_Inequality((Object) this.fader, (Object) null))
        return;
      this.fader.SetActive(false);
    }
  }

  public virtual void Skip() => this.skip = true;

  public bool IsSkip() => this.skip;

  public virtual void SkipAll()
  {
  }

  public virtual void Reset() => this.SetLinkCamera(false);
}
