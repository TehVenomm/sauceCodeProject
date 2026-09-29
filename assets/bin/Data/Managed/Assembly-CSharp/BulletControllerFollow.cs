// Decompiled with JetBrains decompiler
// Type: BulletControllerFollow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class BulletControllerFollow : BulletControllerBase
{
  protected int followObjId;
  protected Character followObj;
  protected Vector3 followOffset = Vector3.zero;
  protected float attenuation = 0.5f;
  private Transform rootNode;
  private Vector3 velocity = Vector3.zero;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam skillParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, skillParam, pos, rot);
    this._rigidbody.isKinematic = true;
    this.followOffset = bullet.dataFollow.followOffset;
    this.attenuation = bullet.dataFollow.attenuation;
  }

  public override void RegisterFromObject(StageObject obj)
  {
    base.RegisterFromObject(obj);
    this.followObj = obj as Character;
    this.rootNode = this.followObj.rootNode;
    this.followObjId = this.followObj.id;
  }

  public virtual void CheckFromObject()
  {
    if (this.followObjId == 0)
      return;
    StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Find((Predicate<StageObject>) (obj => obj.id == this.followObjId));
    if (!Object.op_Inequality((Object) stageObject, (Object) null))
      return;
    this.RegisterFromObject((StageObject) (stageObject as Character));
  }

  public override void Update()
  {
    if (Object.op_Equality((Object) this.followObj, (Object) null) || Object.op_Equality((Object) this.rootNode, (Object) null))
      this.CheckFromObject();
    else
      this.UpdateFollowPosition();
  }

  protected void UpdateFollowPosition()
  {
    this._transform.position = Vector3.op_Addition(this._transform.position, Vector3.op_Multiply(Vector3.op_Multiply(Vector3.op_Addition(this.velocity, Vector3.op_Multiply(Vector3.op_Subtraction(Vector3.op_Addition(this.rootNode.position, Quaternion.op_Multiply(this.followObj._rotation, this.followOffset)), this._transform.position), this.speed)), this.attenuation), Time.deltaTime));
  }
}
