// Decompiled with JetBrains decompiler
// Type: DropObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class DropObject : MonoBehaviour
{
  protected int rarity;
  protected int animStep = -1;
  protected FloatInterpolator anim = new FloatInterpolator();
  protected Vector3 dropPos;
  protected Vector3 targetPos;
  protected float animTime;
  protected bool isRight = true;
  protected InGameSettingsManager.DropItem parameter;

  public Transform _transform { get; protected set; }

  public static DropObject Create(int rarity, bool is_region_break, Vector3 pos)
  {
    if (!MonoBehaviourSingleton<InGameManager>.IsValid())
      return (DropObject) null;
    GameObject bossDropObject = MonoBehaviourSingleton<InGameManager>.I.CreateBossDropObject(is_region_break ? 2 : rarity);
    if (Object.op_Equality((Object) bossDropObject, (Object) null))
      return (DropObject) null;
    DropObject dropObject = bossDropObject.GetComponent<DropObject>();
    if (Object.op_Equality((Object) dropObject, (Object) null))
      dropObject = bossDropObject.AddComponent<DropObject>();
    dropObject.Drop(rarity, pos);
    return dropObject;
  }

  protected virtual void Awake()
  {
    this._transform = ((Component) this).transform;
    this.parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.dropItem;
  }

  private void OnDisable()
  {
    if (!MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIPlayerStatus>.I.AddItemNum(this._transform.position, this.rarity, this.isRight);
  }

  protected void Update()
  {
    if (this.animStep != 0)
      return;
    if (this.anim.IsPlaying())
    {
      this.animTime += Time.deltaTime;
      this._transform.localRotation = Quaternion.AngleAxis(this.animTime * this.parameter.rotationSpeed, Vector3.up);
      float num = this.animTime / this.parameter.popAnimTime;
      if ((double) num > 1.0)
        num = 1f;
      Vector3 vector3 = Vector3.Lerp(this.dropPos, this.targetPos, num);
      vector3.y = this.anim.Update() + this.parameter.defHeight;
      if (!this.anim.IsPlaying())
        return;
      this._transform.position = vector3;
    }
    else
    {
      ((Component) this).gameObject.SetActive(false);
      ++this.animStep;
    }
  }

  protected void Drop(int _rarity, Vector3 pos)
  {
    this.rarity = _rarity;
    this.animStep = 0;
    this.animTime = 0.0f;
    this.isRight = true;
    this.anim.Set(this.parameter.popAnimTime, 0.0f, this.parameter.popHeight, this.parameter.popAnim, 0.0f, (AnimationCurve) null);
    this.anim.Play();
    this.anim.Update(0.0f);
    pos.y = this.anim.Get() + this.parameter.defHeight;
    this._transform.position = pos;
    Vector3 vector3_1 = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.right;
    vector3_1.y = 0.0f;
    ((Vector3) ref vector3_1).Normalize();
    if (Random.Range(0, 10) > 5)
    {
      vector3_1 = Quaternion.op_Multiply(Quaternion.AngleAxis(180f, Vector3.up), vector3_1);
      this.isRight = false;
    }
    Vector3 vector3_2 = Vector3.op_Multiply(vector3_1, this.parameter.popSpeed);
    this.targetPos = Vector3.op_Addition(pos, vector3_2);
    this.dropPos = pos;
  }
}
