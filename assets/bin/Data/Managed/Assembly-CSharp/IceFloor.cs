// Decompiled with JetBrains decompiler
// Type: IceFloor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (Rigidbody))]
public class IceFloor : MonoBehaviour
{
  private Rigidbody _rigidbody;
  private Collider _collider;
  private EffectCtrl _effect;
  private float timer;
  private List<Player> hittingPlayerList = new List<Player>(4);
  private IceFloor.STATE state;

  public float duration { set; get; }

  private void Awake()
  {
    this._rigidbody = ((Component) this).GetComponent<Rigidbody>();
    this._rigidbody.useGravity = false;
    this._rigidbody.isKinematic = true;
    this._collider = ((Component) this).GetComponentInChildren<Collider>();
    if (!Object.op_Equality((Object) this._collider, (Object) null))
      return;
    CapsuleCollider capsuleCollider = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
    capsuleCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
    capsuleCollider.direction = 2;
    ((Collider) capsuleCollider).isTrigger = true;
    this._collider = (Collider) capsuleCollider;
  }

  public void SetCollider(float radius, float height = 2f)
  {
    CapsuleCollider capsuleCollider = this._collider as CapsuleCollider;
    if (Object.op_Equality((Object) capsuleCollider, (Object) null))
    {
      capsuleCollider = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
      this._collider = (Collider) capsuleCollider;
    }
    capsuleCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
    capsuleCollider.direction = 2;
    capsuleCollider.radius = radius;
    capsuleCollider.height = height;
    ((Collider) capsuleCollider).isTrigger = true;
  }

  public void SetEffect(Transform eff)
  {
    this._effect = ((Component) eff).GetComponent<EffectCtrl>();
  }

  private void Update()
  {
    switch (this.state)
    {
      case IceFloor.STATE.UPDATE:
        this.timer += Time.deltaTime;
        if ((double) this.timer <= (double) this.duration)
          break;
        if (Object.op_Inequality((Object) this._effect, (Object) null))
          EffectManager.ReleaseEffect(((Component) this._effect).gameObject);
        this.state = IceFloor.STATE.WAIT_FOR_END;
        break;
      case IceFloor.STATE.WAIT_FOR_END:
        if (!Object.op_Equality((Object) this._effect, (Object) null))
          break;
        Object.Destroy((Object) ((Component) this).gameObject);
        break;
    }
  }

  private void OnDestroy()
  {
    for (int index = 0; index < this.hittingPlayerList.Count; ++index)
      this.hittingPlayerList[index].OnHitExitIceFloor(((Component) this).gameObject);
  }

  private void OnTriggerEnter(Collider collider)
  {
    StageObject componentInParent = ((Component) collider).gameObject.GetComponentInParent<StageObject>();
    if (Object.op_Equality((Object) componentInParent, (Object) null))
      return;
    Player player = componentInParent as Player;
    if (Object.op_Equality((Object) player, (Object) null))
      return;
    this.hittingPlayerList.Add(player);
    player.OnHitEnterIceFloor(((Component) this).gameObject);
  }

  private void OnTriggerExit(Collider collider)
  {
    StageObject componentInParent = ((Component) collider).gameObject.GetComponentInParent<StageObject>();
    if (Object.op_Equality((Object) componentInParent, (Object) null))
      return;
    Player player = componentInParent as Player;
    if (Object.op_Equality((Object) player, (Object) null))
      return;
    this.hittingPlayerList.Remove(player);
    player.OnHitExitIceFloor(((Component) this).gameObject);
  }

  private enum STATE
  {
    UPDATE,
    WAIT_FOR_END,
  }
}
