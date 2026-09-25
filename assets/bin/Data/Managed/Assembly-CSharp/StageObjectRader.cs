// Decompiled with JetBrains decompiler
// Type: StageObjectRader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StageObjectRader : MonoBehaviour
{
  public List<StageObjectRader.CatchStageObject> objects { get; protected set; }

  public Transform _transform { get; protected set; }

  public Rigidbody _rigidbody { get; protected set; }

  public Collider _collider { get; protected set; }

  public StageObject stageObject { get; protected set; }

  protected virtual void Awake()
  {
    this.objects = new List<StageObjectRader.CatchStageObject>();
    this._transform = ((Component) this).transform;
    this._rigidbody = ((Component) this).GetComponent<Rigidbody>();
    this._collider = ((Component) this).GetComponent<Collider>();
    if (Object.op_Equality((Object) this._collider, (Object) null))
    {
      SphereCollider sphereCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
      sphereCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
      this._collider = (Collider) sphereCollider;
    }
    if (!Object.op_Inequality((Object) this._collider, (Object) null))
      return;
    this._collider.isTrigger = true;
    if (Object.op_Equality((Object) this._rigidbody, (Object) null))
      this._rigidbody = ((Component) this).gameObject.AddComponent<Rigidbody>();
    this._rigidbody.isKinematic = true;
  }

  protected virtual void Start()
  {
    this.stageObject = ((Component) this).gameObject.GetComponentInParent<StageObject>();
  }

  public void SetRadius(float radius)
  {
    SphereCollider collider = this._collider as SphereCollider;
    if (!Object.op_Inequality((Object) collider, (Object) null))
      return;
    collider.radius = radius;
  }

  public StageObjectRader.CatchStageObject Find(Collider collider)
  {
    this.RemoveFromDestroyedCollider();
    return this.objects.Find((Predicate<StageObjectRader.CatchStageObject>) (o => Object.op_Equality((Object) o.collider, (Object) collider)));
  }

  public Enemy FindEnemy()
  {
    this.RemoveFromDestroyedCollider();
    StageObjectRader.CatchStageObject catchStageObject = this.objects.Find((Predicate<StageObjectRader.CatchStageObject>) (o => o.obj is Enemy && Object.op_Inequality((Object) o.collider, (Object) null) && Object.op_Equality((Object) o.bullet, (Object) null)));
    return catchStageObject == null ? (Enemy) null : catchStageObject.obj as Enemy;
  }

  public BulletObject FindEnemyBullet()
  {
    this.RemoveFromDestroyedCollider();
    return this.objects.Find((Predicate<StageObjectRader.CatchStageObject>) (o => o.obj is Enemy && Object.op_Inequality((Object) o.collider, (Object) null) && Object.op_Inequality((Object) o.bullet, (Object) null)))?.bullet;
  }

  protected virtual void RemoveFromDestroyedCollider()
  {
    this.objects.RemoveAll((Predicate<StageObjectRader.CatchStageObject>) (o => Object.op_Equality((Object) o.collider, (Object) null)));
  }

  protected virtual void Add(StageObject obj, Collider collider, BulletObject bullet)
  {
    if (this.Find(collider) != null)
      return;
    this.objects.Add(new StageObjectRader.CatchStageObject()
    {
      obj = obj,
      collider = collider,
      bullet = bullet
    });
  }

  protected virtual void Remove(Collider collider)
  {
    StageObjectRader.CatchStageObject catchStageObject = this.Find(collider);
    if (catchStageObject == null)
      return;
    this.objects.Remove(catchStageObject);
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (Object.op_Equality((Object) this._collider, (Object) null) || !this._collider.enabled || Object.op_Equality((Object) this.stageObject, (Object) null) || Object.op_Equality((Object) ((Component) collider).gameObject, (Object) ((Component) this).gameObject))
      return;
    BulletObject component = ((Component) collider).gameObject.GetComponent<BulletObject>();
    StageObject stageObject;
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      stageObject = component.stageObject;
    }
    else
    {
      if (collider.isTrigger)
        return;
      stageObject = ((Component) collider).gameObject.GetComponentInParent<StageObject>();
    }
    if (Object.op_Equality((Object) stageObject, (Object) null) || Object.op_Equality((Object) stageObject, (Object) this.stageObject))
      return;
    this.Add(stageObject, collider, component);
  }

  private void OnTriggerExit(Collider collider)
  {
    if (Object.op_Equality((Object) ((Component) collider).gameObject, (Object) ((Component) this).gameObject) || Object.op_Equality((Object) ((Component) collider).gameObject.GetComponentInParent<StageObject>(), (Object) null))
      return;
    this.Remove(collider);
  }

  public class CatchStageObject
  {
    public StageObject obj;
    public Collider collider;
    public BulletObject bullet;
  }
}
