// Decompiled with JetBrains decompiler
// Type: NodeObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class NodeObject : MonoBehaviour
{
  protected float timeCount;

  public Transform _transform { get; protected set; }

  public Rigidbody _rigidbody { get; protected set; }

  public Collider _collider { get; protected set; }

  public StageObject stageObject { get; protected set; }

  protected virtual bool triggerColliderIsRequired() => true;

  protected virtual void Awake()
  {
    this._transform = ((Component) this).transform;
    this._rigidbody = ((Component) this).GetComponent<Rigidbody>();
    this._collider = this.GetCollider();
    if (!Object.op_Inequality((Object) this._collider, (Object) null) || !this.triggerColliderIsRequired())
      return;
    if (Object.op_Equality((Object) this._rigidbody, (Object) null))
      this._rigidbody = ((Component) this).gameObject.AddComponent<Rigidbody>();
    this._rigidbody.isKinematic = true;
  }

  protected Collider GetCollider()
  {
    Collider component1 = ((Component) this).GetComponent<Collider>();
    if (Object.op_Equality((Object) component1, (Object) null))
      return (Collider) null;
    bool flag = this.triggerColliderIsRequired();
    if (component1.isTrigger == flag)
      return component1;
    foreach (Collider component2 in ((Component) this).gameObject.GetComponents<Collider>())
    {
      if (component2.isTrigger == flag)
        return component2;
    }
    return (Collider) null;
  }

  protected virtual void Start()
  {
    if (!this.triggerColliderIsRequired())
      return;
    this.stageObject = ((Component) this).gameObject.GetComponentInParent<StageObject>();
  }

  protected virtual void Update()
  {
    if (!Object.op_Inequality((Object) this._collider, (Object) null) || !this._collider.enabled)
      return;
    this.timeCount += Time.deltaTime;
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (Object.op_Equality((Object) this._collider, (Object) null) || !this._collider.enabled || collider.isTrigger || Object.op_Equality((Object) this.stageObject, (Object) null) || Object.op_Equality((Object) ((Component) collider).gameObject, (Object) ((Component) this).gameObject))
      return;
    StageObject componentInParent = ((Component) collider).gameObject.GetComponentInParent<StageObject>();
    if (Object.op_Equality((Object) componentInParent, (Object) null) || Object.op_Equality((Object) componentInParent, (Object) this.stageObject))
      return;
    this.OnHitTrigger(collider, componentInParent);
  }

  protected virtual void OnHitTrigger(Collider to_collider, StageObject to_object)
  {
  }

  public void SetEnableTrigger(bool enable, bool init = true)
  {
    if (Object.op_Inequality((Object) this._collider, (Object) null) && this._collider.isTrigger)
      this._collider.enabled = enable;
    if (!(enable & init))
      return;
    this.timeCount = 0.0f;
  }
}
