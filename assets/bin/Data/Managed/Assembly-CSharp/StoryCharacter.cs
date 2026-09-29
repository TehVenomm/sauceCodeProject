// Decompiled with JetBrains decompiler
// Type: StoryCharacter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class StoryCharacter : MonoBehaviour
{
  private UIRenderTexture renderTex;
  private NPCLoader npcLoader;
  private Vector3 basePos;
  private Vector3 baseRot;
  private Vector3Interpolator animPos = new Vector3Interpolator();
  private QuaternionInterpolator animRot = new QuaternionInterpolator();
  private Transform lookTransform;
  private PLCA idleAnim;
  private UITweenCtrl[] tweenAnimations;
  private PlayerAnimCtrl playerAnimCtrl;

  public bool isLoading { get; private set; }

  public bool isMoving => this.animPos.IsPlaying() || this.animRot.IsPlaying();

  public int id { get; private set; }

  public string charaName { get; private set; }

  public string displayName
  {
    get => !string.IsNullOrEmpty(this.aliasName) ? this.aliasName : this.charaName;
  }

  public string aliasName { get; private set; }

  public StoryDirector.POS dir { get; private set; }

  public UITexture uiTex { get; private set; }

  public Transform model { get; private set; }

  public static StoryCharacter Initialize(
    int id,
    UITexture ui_tex,
    string _name,
    string _dir,
    string idle_anim,
    int layer = -1)
  {
    NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData(_name);
    if (npcData == null)
      return (StoryCharacter) null;
    UIRenderTexture uiRenderTexture = UIRenderTexture.Get(ui_tex, link_main_camera: true, layer: layer);
    uiRenderTexture.Disable();
    uiRenderTexture.nearClipPlane = 1f;
    uiRenderTexture.farClipPlane = 100f;
    Transform gameObject = Utility.CreateGameObject("StoryModel", uiRenderTexture.modelTransform, uiRenderTexture.renderLayer);
    StoryCharacter storyCharacter = ((Component) gameObject).gameObject.AddComponent<StoryCharacter>();
    storyCharacter.model = gameObject;
    storyCharacter.id = id;
    storyCharacter.renderTex = uiRenderTexture;
    storyCharacter.uiTex = ui_tex;
    storyCharacter.charaName = _name;
    storyCharacter.aliasName = string.Empty;
    storyCharacter.SetStandPosition(_dir);
    storyCharacter.idleAnim = !string.IsNullOrEmpty(idle_anim) ? PlayerAnimCtrl.StringToEnum(idle_anim) : PlayerAnimCtrl.StringToEnum(npcData.anim);
    storyCharacter.isLoading = true;
    ModelLoaderBase modelLoaderBase = npcData.LoadModel(((Component) gameObject).gameObject, false, false, new Action<Animator>(storyCharacter.OnModelLoadComplete), false);
    storyCharacter.npcLoader = modelLoaderBase as NPCLoader;
    storyCharacter.CollectTween(((Component) ui_tex).transform);
    return storyCharacter;
  }

  public void SetStandPosition(string _dir, bool doesSetImmidiate = false)
  {
    this.basePos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
    this.baseRot = new Vector3(0.0f, MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandRot, 0.0f);
    this.dir = StoryDirector.POS.LEFT;
    if (_dir == "R" || _dir == "UR")
    {
      this.basePos.x = -this.basePos.x;
      this.baseRot.y = -this.baseRot.y;
      this.dir = StoryDirector.POS.RIGHT;
    }
    else if (_dir == "C" || _dir == "UC")
    {
      this.basePos.x = 0.0f;
      this.baseRot.y = 180f;
      this.dir = StoryDirector.POS.CENTER;
    }
    if (_dir == "UR" || _dir == "UC" || _dir == "UL")
      this.basePos.y += MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandUpOffset;
    this.animPos.Set(this.basePos);
    if (doesSetImmidiate)
      this.model.localPosition = this.basePos;
    this.animRot.Set(Quaternion.Euler(this.baseRot));
  }

  public void SetPosition(float x, float y, float time)
  {
    this.basePos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
    Vector3 end_value;
    // ISSUE: explicit constructor call
    ((Vector3) ref end_value).\u002Ector(x, y, this.basePos.z);
    this.animPos.Set(time, end_value, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    this.animPos.Play();
  }

  public void SetModelScale(Vector3 scale) => this.model.localScale = scale;

  private void CollectTween(Transform t_ui_tex)
  {
    if (Object.op_Equality((Object) t_ui_tex, (Object) null))
      return;
    this.tweenAnimations = ((Component) t_ui_tex).GetComponentsInChildren<UITweenCtrl>();
  }

  public void PlayTween(StoryCharacter.EaseDir type, bool forward = true, EventDelegate.Callback callback = null)
  {
    if (this.tweenAnimations == null)
      return;
    UITweenCtrl uiTweenCtrl = Array.Find<UITweenCtrl>(this.tweenAnimations, (Predicate<UITweenCtrl>) (t => (StoryCharacter.EaseDir) t.id == type));
    if (!Object.op_Inequality((Object) uiTweenCtrl, (Object) null))
      return;
    uiTweenCtrl.Reset();
    uiTweenCtrl.Play(forward, callback);
  }

  private void OnModelLoadComplete(Animator animator)
  {
    if (Object.op_Inequality((Object) animator, (Object) null))
      this.playerAnimCtrl = PlayerAnimCtrl.Get(animator, this.idleAnim);
    this.isLoading = false;
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.model, (Object) null))
      return;
    this.model.localPosition = this.animPos.Update();
    this.model.localRotation = Quaternion.op_Multiply(Quaternion.Euler(new Vector3(-6f, 0.0f, 0.0f)), this.animRot.Update());
  }

  public void Show() => this.renderTex.Enable();

  public void Hide() => this.renderTex.Disable();

  public bool IsShow() => this.renderTex.enableTexture;

  public void FadeIn()
  {
    float charaFadeTime = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime;
    this.renderTex.Enable(charaFadeTime);
    float charaFadeMoveX = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeMoveX;
    Vector3 leftStandPos;
    Vector3 begin_value;
    if (StoryDirector.POS.LEFT == this.dir)
    {
      leftStandPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
      begin_value = leftStandPos;
      begin_value.x -= charaFadeMoveX;
    }
    else if (StoryDirector.POS.RIGHT == this.dir)
    {
      leftStandPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
      leftStandPos.x = -leftStandPos.x;
      begin_value = leftStandPos;
      begin_value.x += charaFadeMoveX;
    }
    else
    {
      leftStandPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
      leftStandPos.x = 0.0f;
      begin_value = leftStandPos;
    }
    this.animPos.Set(charaFadeTime, begin_value, leftStandPos, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    this.animPos.Play();
  }

  public void FadeOut()
  {
    float charaFadeTime = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime;
    this.renderTex.FadeOutDisable(charaFadeTime);
    float charaFadeMoveX = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeMoveX;
    Vector3 begin_value = Vector3.zero;
    Vector3 end_value = begin_value;
    bool flag = false;
    if (StoryDirector.POS.LEFT == this.dir)
    {
      begin_value = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
      end_value = begin_value;
      end_value.x -= charaFadeMoveX;
      flag = true;
    }
    else if (StoryDirector.POS.RIGHT == this.dir)
    {
      begin_value = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
      begin_value.x = -begin_value.x;
      end_value = begin_value;
      end_value.x += charaFadeMoveX;
      flag = true;
    }
    if (!flag)
      return;
    this.animPos.Set(charaFadeTime, begin_value, end_value, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    this.animPos.Play();
  }

  public void RotateFront(float time = 0.5f) => this.RotateAngle(0.0f, time);

  public void RotateDefault(float time = 0.5f)
  {
    this.animRot.Set(time, this.model.localRotation, Quaternion.Euler(this.baseRot), (AnimationCurve) null, new Quaternion(), (AnimationCurve) null);
    this.animRot.Play();
  }

  public void RotateAngle(float angle, float time = 0.5f)
  {
    this.animRot.Set(time, this.model.localRotation, Quaternion.Euler(new Vector3(0.0f, 180f + angle, 0.0f)), (AnimationCurve) null, new Quaternion(), (AnimationCurve) null);
    this.animRot.Play();
  }

  public void RequestPose(string pose_name)
  {
    if (Object.op_Equality((Object) this.playerAnimCtrl, (Object) null))
      return;
    try
    {
      this.playerAnimCtrl.Play((PLCA) Enum.Parse(typeof (PLCA), pose_name));
    }
    catch
    {
      Log.Error(LOG.GAMESCENE, "不正なモーションコマンド：" + pose_name);
    }
  }

  public void RequestFace(string eye_type, string mouth_type)
  {
    if (Object.op_Equality((Object) this.npcLoader, (Object) null) || Object.op_Equality((Object) this.npcLoader.facial, (Object) null))
      return;
    NPCFacial facial = this.npcLoader.facial;
    NPCFacial.TYPE eyeType = facial.eyeType;
    NPCFacial.TYPE mouthType = facial.mouthType;
    if (!string.IsNullOrEmpty(eye_type))
    {
      try
      {
        NPCFacial.TYPE type = (NPCFacial.TYPE) Enum.Parse(typeof (NPCFacial.TYPE), eye_type);
        facial.eyeType = type;
      }
      catch
      {
        Log.Error(LOG.GAMESCENE, "不正な表情(目)：" + eye_type);
      }
    }
    if (!string.IsNullOrEmpty(mouth_type))
    {
      try
      {
        NPCFacial.TYPE type = (NPCFacial.TYPE) Enum.Parse(typeof (NPCFacial.TYPE), mouth_type);
        facial.mouthType = type;
      }
      catch
      {
        Log.Error(LOG.GAMESCENE, "不正な表情(口)：" + mouth_type);
      }
    }
    if (eyeType == facial.eyeType && mouthType == facial.mouthType)
      return;
    if (facial.eyeType != NPCFacial.TYPE.NORMAL || facial.mouthType != NPCFacial.TYPE.NORMAL)
      facial.enableAnim = false;
    else
      facial.enableAnim = true;
  }

  public void SetAliasName(string _name) => this.aliasName = _name;

  public enum EaseDir
  {
    LEFT,
    RIGHT,
  }
}
