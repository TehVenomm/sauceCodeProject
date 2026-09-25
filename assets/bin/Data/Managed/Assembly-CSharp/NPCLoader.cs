// Decompiled with JetBrains decompiler
// Type: NPCLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class NPCLoader : ModelLoaderBase
{
  public static readonly Bounds BOUNDS = new Bounds(Vector3.zero, new Vector3(2f, 2f, 2f));
  private IEnumerator coroutine;
  private LoadingQueue loadingQueue;
  private System.Action callback;

  public override bool IsLoading() => this.isLoading;

  public override Animator GetAnimator() => this.animator;

  public override Transform GetHead() => this.head;

  public override void SetEnabled(bool is_enable)
  {
    if (Object.op_Inequality((Object) this.animator, (Object) null))
      ((Behaviour) this.animator).enabled = is_enable;
    if (Object.op_Inequality((Object) this.shadow, (Object) null))
      ((Component) this.shadow).gameObject.SetActive(is_enable);
    ModelLoaderBase.SetEnabled(this.renderers, is_enable);
  }

  public Transform model { get; private set; }

  public Transform head { get; private set; }

  public NPCFacial facial { get; private set; }

  public Animator animator { get; private set; }

  public Renderer[] renderers { get; private set; }

  public Transform shadow { get; private set; }

  public bool isLoading => this.coroutine != null;

  private void Update() => Object.op_Inequality((Object) this.animator, (Object) null);

  public void Load(
    int npc_model_id,
    int layer,
    bool need_shadow,
    bool enable_light_probes,
    SHADER_TYPE shader_type,
    System.Action callback,
    bool clearAll = false)
  {
    this.Clear();
    this.StartCoroutine(this.coroutine = this.DoLoad(npc_model_id, layer, need_shadow, enable_light_probes, shader_type, callback));
  }

  private IEnumerator DoLoad(
    int npc_model_id,
    int layer,
    bool need_shadow,
    bool enable_light_probes,
    SHADER_TYPE shader_type,
    System.Action callback)
  {
    this.loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_model = (LoadObject) this.loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.NPC_MODEL, ResourceName.GetNPCModel(npc_model_id));
    string npcAnim = ResourceName.GetNPCAnim(npc_model_id);
    LoadObject lo_anim = this.loadingQueue.Load(RESOURCE_CATEGORY.NPC_ANIM, npcAnim, new string[1]
    {
      npcAnim + "Ctrl"
    });
    if (this.loadingQueue.IsLoading())
      yield return (object) this.loadingQueue.Wait();
    this.model = lo_model.Realizes(((Component) this).transform, layer);
    if (Object.op_Inequality((Object) this.model, (Object) null))
    {
      this.head = Utility.Find(this.model, "Head");
      this.facial = ((Component) this.model).GetComponentInChildren<NPCFacial>();
      if (Object.op_Inequality((Object) this.facial, (Object) null))
        this.facial.animNode = Utility.Find(this.model, "Face");
      this.animator = ((Component) this.model).GetComponentInChildren<Animator>();
      if (lo_anim != null && Object.op_Inequality((Object) this.animator, (Object) null))
        this.animator.runtimeAnimatorController = lo_anim.loadedObjects[0].obj as RuntimeAnimatorController;
    }
    PlayerLoader.SetLightProbes(this.model, enable_light_probes);
    this.renderers = ((Component) this.model).GetComponentsInChildren<Renderer>();
    int index = 0;
    for (int length = this.renderers.Length; index < length; ++index)
    {
      if (this.renderers[index] is SkinnedMeshRenderer)
        (this.renderers[index] as SkinnedMeshRenderer).localBounds = NPCLoader.BOUNDS;
    }
    switch (shader_type)
    {
      case SHADER_TYPE.LIGHTWEIGHT:
        ShaderGlobal.ChangeWantLightweightShader(this.renderers);
        break;
      case SHADER_TYPE.UI:
        ShaderGlobal.ChangeWantUIShader(this.renderers);
        break;
    }
    if (need_shadow)
      this.shadow = PlayerLoader.CreateShadow(((Component) this).transform, is_lightweight: shader_type == SHADER_TYPE.LIGHTWEIGHT);
    this.coroutine = (IEnumerator) null;
    if (callback != null)
      callback();
  }

  public void Clear()
  {
    if (this.coroutine != null)
    {
      this.StopCoroutine(this.coroutine);
      this.coroutine = (IEnumerator) null;
    }
    if (Object.op_Inequality((Object) this.model, (Object) null))
    {
      Object.DestroyImmediate((Object) ((Component) this.model).gameObject);
      this.model = (Transform) null;
      this.head = (Transform) null;
    }
    this.loadingQueue = (LoadingQueue) null;
    this.animator = (Animator) null;
  }
}
