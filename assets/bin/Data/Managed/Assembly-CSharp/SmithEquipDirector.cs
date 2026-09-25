// Decompiled with JetBrains decompiler
// Type: SmithEquipDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithEquipDirector : AnimationDirector
{
  [SerializeField]
  private Transform npcParent;
  [SerializeField]
  private Transform npc003Parent;
  [SerializeField]
  private Transform effectParent;
  [SerializeField]
  private Animator cameraAnimator;
  [SerializeField]
  private GameObject[] createEffectPrefabs;
  [SerializeField]
  private GameObject[] growEffectPrefabs;
  [SerializeField]
  private GameObject[] evolveEffectPrefabs;
  [SerializeField]
  private GameObject[] exceedEffectPrefabs;
  [SerializeField]
  private GameObject[] abilityChangeEffectPrefabs;
  private GameObject[] effects;
  private List<Animator> animators = new List<Animator>();
  private Transform[] npc004TransformList;
  private Animator npc004Animator;
  private Transform[] npc003TransformList;
  private Animator npc003Animator;
  private Transform effectBindTarget;
  private bool updateFOV;
  private float time;
  private const string STATE_NAME_CREATE = "CREATE";
  private const string STATE_NAME_GROW = "GROW";
  private const string STATE_NAME_EVOLVE = "EVOLVE";
  private const string STATE_NAME_EXCEED = "EXCEED";
  private const string STATE_NAME_ABILITY_CHANGE = "ABILITY_CHANGE";

  private void Start()
  {
  }

  public void SetNPC003(GameObject npcObject)
  {
    if (Object.op_Equality((Object) npcObject, (Object) null))
      return;
    Utility.Attach(this.npc003Parent, npcObject.transform);
    Utility.SetLayerWithChildren(npcObject.transform, ((Component) this.npc003Parent).gameObject.layer);
    this.npc003Animator = npcObject.GetComponentInChildren<Animator>();
    this.npc003Animator.cullingMode = (AnimatorCullingMode) 0;
    this.npc003TransformList = npcObject.GetComponentsInChildren<Transform>();
  }

  public void SetNPC004(GameObject npcObject)
  {
    Utility.Attach(this.npcParent, npcObject.transform);
    Utility.SetLayerWithChildren(npcObject.transform, ((Component) this.npcParent).gameObject.layer);
    this.npc004Animator = npcObject.GetComponentInChildren<Animator>();
    this.npc004Animator.cullingMode = (AnimatorCullingMode) 0;
    this.npc004TransformList = npcObject.GetComponentsInChildren<Transform>();
  }

  private Transform SearchNpc004Transform(string targetName)
  {
    if (this.npc004TransformList == null)
      return (Transform) null;
    foreach (Transform npc004Transform in this.npc004TransformList)
    {
      if (((Object) npc004Transform).name == targetName)
        return npc004Transform;
    }
    return (Transform) null;
  }

  private Transform SearchNpc003Transform(string targetName)
  {
    if (this.npc003TransformList == null)
      return (Transform) null;
    foreach (Transform npc003Transform in this.npc003TransformList)
    {
      if (((Object) npc003Transform).name == targetName)
        return npc003Transform;
    }
    return (Transform) null;
  }

  public void StartCreate(System.Action onEnd)
  {
    this.updateFOV = true;
    this.SetEffects(this.createEffectPrefabs);
    this.PlayDirection("CREATE", onEnd);
    this.PlayNPCAnimation("CREATE");
    this.PlayCameraAnimation("CREATE");
    this.PlayEffect("ef_studio_create_01");
  }

  public void StartGrow(System.Action onEnd)
  {
    this.updateFOV = true;
    $"{this.GetNpcRoot()}Tools/Hammer_L";
    this.effectBindTarget = Utility.FindChild(this.npcParent, "NPC004_003/");
    this.SetEffects(this.growEffectPrefabs);
    this.PlayDirection("GROW", onEnd);
    this.PlayNPCAnimation("GROW");
    this.PlayCameraAnimation("GROW");
  }

  public void StartEvolve(System.Action onEnd)
  {
    this.updateFOV = false;
    this.effectBindTarget = Utility.FindChild(this.npcParent, $"{this.GetNpcRoot()}Hip/Spine00/Spine01/L_Shoulder/L_Upperarm/L_Forearm/L_Hand/L_Wep");
    this.SetEffects(this.evolveEffectPrefabs);
    this.PlayDirection("EVOLVE", onEnd);
    this.PlayNPCAnimation("EVOLVE");
    this.PlayCameraAnimation("EVOLVE");
    this.time = 0.0f;
    this.StartCoroutine(this.DoEvolveEffect());
  }

  public void StartExceed(System.Action onEnd)
  {
    this.updateFOV = false;
    this.effectBindTarget = Utility.FindChild(this.npcParent, $"{this.GetNpcRoot()}Tools/Hammer_L");
    this.SetEffects(this.exceedEffectPrefabs);
    this.PlayDirection("EXCEED", onEnd);
    this.PlayNPCAnimation("EXCEED");
    this.PlayCameraAnimation("EXCEED");
    if (!Object.op_Inequality((Object) this.effectBindTarget, (Object) null))
      return;
    this.PlayBindEffect(0, this.effectBindTarget, new Vector3(0.0f, 0.0f, 0.522f));
  }

  public string GetNpcRoot()
  {
    return $"NPC004_{(StatusManager.IsUnique() ? 3 : 1):D3}/NPC004_Origin/Move/Root/";
  }

  public void StartAbilityChange(System.Action onEnd)
  {
    this.updateFOV = true;
    this.SetEffects(this.abilityChangeEffectPrefabs);
    this.PlayDirection("ABILITY_CHANGE", onEnd);
    this.PlayNPCAnimation("ABILITY_CHANGE");
    this.PlayCameraAnimation("ABILITY_CHANGE");
  }

  private void PlayDirection(string name, System.Action onEnd)
  {
    if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
      MonoBehaviourSingleton<StatusStageManager>.I.SetEnableSmithCharacterActivate(false);
    if (MonoBehaviourSingleton<AppMain>.IsValid())
      this.SetLinkCamera(true);
    this.Play(name, (System.Action) (() =>
    {
      this.ResetPlayRate();
      SoundManager.StopSEAll();
      this.PlayAudioOnEnd(name);
      onEnd();
    }));
  }

  private void PlayAudioOnEnd(string name)
  {
    switch (name)
    {
      case "CREATE":
        this.PlayAudio(SmithEquipDirector.AUDIO.CREATE_ONEND);
        break;
      case "GROW":
        break;
      case "EVOLVE":
        this.PlayAudio(SmithEquipDirector.AUDIO.GROW_ONEND);
        break;
      case "EXCEED":
        this.PlayAudio(SmithEquipDirector.AUDIO.GROW_ONEND);
        break;
      default:
        int num = name == "ABILITY_CHANGE" ? 1 : 0;
        break;
    }
  }

  private void SetEffects(GameObject[] prefabs)
  {
    if (prefabs == null || prefabs.Length == 0)
      return;
    Camera camera = MonoBehaviourSingleton<AppMain>.IsValid() ? MonoBehaviourSingleton<AppMain>.I.mainCamera : this.useCamera;
    List<Animator> collection = new List<Animator>();
    this.effects = new GameObject[prefabs.Length];
    for (int index = 0; index < prefabs.Length; ++index)
    {
      GameObject gameObject = ((Component) ResourceUtility.Realizes((Object) prefabs[index], this.effectParent, ((Component) this.effectParent).gameObject.layer)).gameObject;
      rymFX component = gameObject.GetComponent<rymFX>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.Cameras = new Camera[1]{ camera };
        gameObject.GetComponentsInChildren<Animator>(collection);
        this.animators.AddRange((IEnumerable<Animator>) collection);
      }
      gameObject.SetActive(false);
      this.effects[index] = gameObject;
    }
  }

  public override void Reset()
  {
    if (AppMain.isApplicationQuit)
      return;
    this.skip = false;
    if (this.effects != null)
    {
      for (int index = 0; index < this.effects.Length; ++index)
      {
        GameObject effect = this.effects[index];
        if (Object.op_Implicit((Object) effect))
          Object.Destroy((Object) effect);
      }
    }
    this.animators.Clear();
    base.Reset();
  }

  protected override void Update()
  {
    this.time += Time.deltaTime;
    if (this.skip)
      this.time = 10000f;
    if (Object.op_Inequality((Object) this.npc004Animator, (Object) null))
      this.npc004Animator.speed = this.skip ? 10000f : 1f;
    if (Object.op_Inequality((Object) this.npc003Animator, (Object) null))
      this.npc003Animator.speed = this.skip ? 10000f : 1f;
    if (Object.op_Implicit((Object) this.cameraAnimator))
      this.cameraAnimator.speed = this.skip ? 10000f : 1f;
    base.Update();
  }

  protected override void LateUpdate()
  {
    if (this.updateFOV && Object.op_Inequality((Object) this.useCamera, (Object) null))
    {
      float x = ((Component) this.cameraAnimator).transform.localScale.x;
      if ((double) x > 0.0)
        this.useCamera.fieldOfView = Utility.HorizontalToVerticalFOV(x);
    }
    base.LateUpdate();
  }

  public override void Skip()
  {
    if (this.skip)
      return;
    for (int index = 0; index < this.effects.Length; ++index)
    {
      GameObject effect = this.effects[index];
      if (Object.op_Inequality((Object) effect, (Object) null))
      {
        rymFX component = effect.GetComponent<rymFX>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          float num1 = component.GetEndFrame() - component.GetCurFrame();
          if ((double) num1 > 0.0)
          {
            float num2 = num1 / 30f;
            component.UpdateFx(num2, (Camera) null, component.IsLoop());
          }
        }
      }
    }
    if (this.animators != null && this.animators.Count > 0)
    {
      for (int index = 0; index < this.animators.Count; ++index)
        this.animators[index].speed = 10000f;
    }
    SoundManager.StopSEAll();
    base.Skip();
  }

  private void ResetPlayRate()
  {
    if (this.animators == null || this.animators.Count <= 0)
      return;
    for (int index = 0; index < this.animators.Count; ++index)
      this.animators[index].speed = 1f;
  }

  private void PlayNPCAnimation(string stateName)
  {
    this.npc004Animator.Play(stateName, 0, 0.0f);
    this.npc004Animator.Update(0.0f);
    if (Object.op_Inequality((Object) this.npc003Animator, (Object) null))
    {
      this.npc003Animator.Play(stateName, 0, 0.0f);
      this.npc003Animator.Update(0.0f);
    }
    switch (stateName)
    {
      case "CREATE":
        this.PlayAudio(SmithEquipDirector.AUDIO.CREATE_START);
        break;
      case "GROW":
        break;
      case "EVOLVE":
        this.PlayAudio(SmithEquipDirector.AUDIO.EVOLVE_START);
        break;
      case "EXCEED":
        this.PlayAudio(SmithEquipDirector.AUDIO.EXCEED_START);
        break;
      default:
        int num = stateName == "ABILITY_CHANGE" ? 1 : 0;
        break;
    }
  }

  private void PlayCameraAnimation(string stateName)
  {
    this.cameraAnimator.Play(stateName, 0, 0.0f);
    this.cameraAnimator.Update(0.0f);
  }

  private void PlayEffect(string name)
  {
    GameObject effect = this.FindEffect(name);
    if (!Object.op_Implicit((Object) effect))
      return;
    effect.SetActive(true);
  }

  private void PlayEffectWithParent(string name)
  {
    string[] strArray = name.Split('@');
    if (strArray.Length > 1)
    {
      GameObject effect = this.FindEffect(strArray[0]);
      if (Object.op_Equality((Object) effect, (Object) null))
        return;
      GameObject gameObject = Object.Instantiate<GameObject>(effect);
      Transform transform1 = this.SearchNpc004Transform(strArray[1]);
      if (!Object.op_Inequality((Object) gameObject, (Object) null) || !Object.op_Inequality((Object) transform1, (Object) null))
        return;
      Transform transform2 = gameObject.transform;
      transform2.parent = transform1;
      transform2.localPosition = Vector3.zero;
      transform2.localScale = Vector3.one;
      transform2.localRotation = Quaternion.identity;
      gameObject.SetActive(true);
    }
    else
      this.PlayEffect(name);
  }

  private void PlayEffectSilvy(string name)
  {
    string[] strArray = name.Split('@');
    if (strArray.Length > 1)
    {
      GameObject effect = this.FindEffect(strArray[0]);
      if (Object.op_Equality((Object) effect, (Object) null))
        return;
      GameObject gameObject = Object.Instantiate<GameObject>(effect);
      Transform transform1 = this.SearchNpc003Transform(strArray[1]);
      if (!Object.op_Inequality((Object) gameObject, (Object) null) || !Object.op_Inequality((Object) transform1, (Object) null))
        return;
      Transform transform2 = gameObject.transform;
      transform2.parent = transform1;
      transform2.localPosition = Vector3.zero;
      transform2.localScale = Vector3.one;
      transform2.localRotation = Quaternion.identity;
      gameObject.SetActive(true);
    }
    else
      this.PlayEffect(name);
  }

  private void PlayBindEffect(int effectIndex, Transform bindTarget, Vector3 offset)
  {
    GameObject effect = this.effects[effectIndex];
    effect.transform.parent = bindTarget;
    effect.transform.localPosition = offset;
    effect.SetActive(true);
  }

  private GameObject FindEffect(string name)
  {
    for (int index = 0; index < this.effects.Length; ++index)
    {
      GameObject effect = this.effects[index];
      if (((Object) effect).name == name)
        return effect;
    }
    return (GameObject) null;
  }

  private IEnumerator DoEvolveEffect()
  {
    if (Object.op_Implicit((Object) this.effectBindTarget))
      this.PlayBindEffect(1, this.effectBindTarget, new Vector3(-0.022f, -0.041f, 0.04f));
    GameObject e = this.FindEffect("ef_studio_evolve_01");
    for (float time = 0.3f; (double) time > 0.0; time -= Time.deltaTime)
      yield return (object) null;
    if (Object.op_Implicit((Object) e))
    {
      e.SetActive(true);
      e.transform.localPosition = new Vector3(2.846f, 0.968f, 2.001f);
    }
  }

  protected void PlayAudio(SmithEquipDirector.AUDIO audio)
  {
    if (this.skip)
      return;
    SoundManager.PlayOneShotUISE((int) audio);
  }

  public enum AUDIO
  {
    CREATE_START = 40000044, // 0x02625A2C
    ABILITY_CHANGE_ONEND = 40000045, // 0x02625A2D
    CREATE_ONEND = 40000045, // 0x02625A2D
    GROW_HIT_01 = 40000046, // 0x02625A2E
    GROW_HIT_02 = 40000047, // 0x02625A2F
    GROW_HIT_03 = 40000048, // 0x02625A30
    EXCEED_START = 40000050, // 0x02625A32
    EXCEED_ONEND = 40000051, // 0x02625A33
    GROW_ONEND = 40000051, // 0x02625A33
    EVOLVE_START = 40000055, // 0x02625A37
    ABILITY_CHANGE_01 = 40000058, // 0x02625A3A
    ABILITY_CHANGE_02 = 40000059, // 0x02625A3B
    ABILITY_CHANGE_03 = 40000060, // 0x02625A3C
  }
}
