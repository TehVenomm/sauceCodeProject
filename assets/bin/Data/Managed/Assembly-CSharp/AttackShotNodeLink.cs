// Decompiled with JetBrains decompiler
// Type: AttackShotNodeLink
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AttackShotNodeLink : MonoBehaviour
{
  private BulletData bulletData;
  private GameObject bulletObj;
  private Transform bulletTrans;
  private StageObject attacker;
  private string atkInfoName = string.Empty;
  private Transform parentTrans;
  private bool isRequestDelete;
  private bool isInit;
  private Vector3 defaultPos;
  private Vector3 defaultOffsetPos;
  private Vector3 defaultOffsetRot;
  private float rotSpd;
  private float moveSpd;
  private float moveDis;
  private int moveDir;
  private bool isChaseXPos = true;
  private bool isChaseYPos = true;
  private bool isChaseZPos = true;
  private bool isUseChasePos;
  private bool isChaseXRot = true;
  private bool isChaseYRot = true;
  private bool isChaseZRot = true;
  private bool isUseChaseRot;
  private bool isRot;
  private bool isMove;
  private bool isMinusMove;
  private float currentRotAngle;
  private float currentMoveDis;
  private Transform _transform;

  public string AttackInfoName => this.atkInfoName;

  public void RequestDestroy() => this.isRequestDelete = true;

  public void Initialize(
    StageObject attacker,
    Transform parentTrans,
    AnimEventData.EventData data,
    AttackInfo atkInfo,
    AnimEventShot childEventShot)
  {
    this.bulletData = atkInfo.bulletData;
    if (Object.op_Equality((Object) this.bulletData, (Object) null) || this.bulletData.data == null)
      return;
    this.attacker = attacker;
    this.parentTrans = parentTrans;
    Player player = attacker as Player;
    if (atkInfo is AttackHitInfo attackHitInfo)
      attackHitInfo.enableIdentityCheck = false;
    this.atkInfoName = atkInfo.name;
    this._transform = ((Component) this).transform;
    this._transform.parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
    this._transform.position = parentTrans.position;
    switch (data.intArgs[0])
    {
      case 0:
        this._transform.rotation = attacker._transform.rotation;
        break;
      case 1:
        this._transform.rotation = parentTrans.rotation;
        break;
      case 2:
        this._transform.rotation = Quaternion.identity;
        break;
    }
    this.defaultPos = this._transform.position;
    ((Component) childEventShot).transform.parent = this._transform;
    this.bulletObj = ((Component) childEventShot).gameObject;
    this.bulletTrans = this.bulletObj.transform;
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
    this.bulletTrans.localEulerAngles = new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
    this.bulletTrans.localPosition = vector3;
    this.rotSpd = data.floatArgs[6];
    this.moveSpd = data.floatArgs[7];
    this.moveDis = data.floatArgs[8];
    this.isChaseXPos = data.intArgs[2] != 0;
    this.isChaseYPos = data.intArgs[3] != 0;
    this.isChaseZPos = data.intArgs[4] != 0;
    this.isChaseXRot = data.intArgs[5] != 0;
    this.isChaseYRot = data.intArgs[6] != 0;
    this.isChaseZRot = data.intArgs[7] != 0;
    this.isRot = data.intArgs[8] != 0;
    this.isMove = data.intArgs[9] != 0;
    this.moveDir = data.intArgs[10];
    this.isMinusMove = data.intArgs[11] != 0;
    if (Object.op_Inequality((Object) parentTrans, (Object) null))
    {
      this.defaultOffsetPos = Vector3.zero;
      this.defaultOffsetRot = Vector3.op_Subtraction(this._transform.eulerAngles, parentTrans.eulerAngles);
    }
    else
    {
      this.defaultOffsetPos = Vector3.zero;
      this.defaultOffsetRot = Vector3.zero;
    }
    if (this.isChaseXPos || this.isChaseYPos || this.isChaseZPos)
      this.isUseChasePos = true;
    if (this.isChaseXRot || this.isChaseYRot || this.isChaseZRot)
      this.isUseChaseRot = true;
    this.currentRotAngle = 0.0f;
    this.isInit = true;
  }

  private void LateUpdate()
  {
    if (!this.isInit)
      return;
    if (Object.op_Equality((Object) this.bulletObj, (Object) null))
      Object.Destroy((Object) ((Component) this).gameObject);
    else if (this.isRequestDelete)
      Object.Destroy((Object) this.bulletObj);
    else if (Object.op_Equality((Object) this.parentTrans, (Object) null))
    {
      Object.Destroy((Object) this.bulletObj);
    }
    else
    {
      Vector3 position1 = this._transform.position;
      Vector3 position2 = this.parentTrans.position;
      Vector3 eulerAngles1 = this._transform.eulerAngles;
      Vector3 eulerAngles2 = this.parentTrans.eulerAngles;
      if (this.isUseChaseRot)
      {
        if (this.isChaseXRot)
          eulerAngles1.x = eulerAngles2.x + this.defaultOffsetRot.x;
        if (this.isChaseYRot)
          eulerAngles1.y = eulerAngles2.y + this.defaultOffsetRot.y;
        if (this.isChaseZRot)
          eulerAngles1.z = eulerAngles2.z + this.defaultOffsetRot.z;
      }
      if (this.isRot)
      {
        if (this.isChaseYRot)
          this.currentRotAngle += this.rotSpd * Time.deltaTime;
        else
          this.currentRotAngle = this.rotSpd * Time.deltaTime;
        if ((double) this.currentRotAngle > 360.0)
          this.currentRotAngle -= 360f;
        else if ((double) this.currentRotAngle < 0.0)
          this.currentRotAngle += 360f;
        eulerAngles1.y += this.currentRotAngle;
      }
      this._transform.eulerAngles = eulerAngles1;
      if (this.isUseChasePos)
      {
        if (this.isChaseXPos)
          position1.x = position2.x + this.defaultOffsetPos.x;
        if (this.isChaseYPos)
          position1.y = position2.y + this.defaultOffsetPos.y;
        if (this.isChaseZPos)
          position1.z = position2.z + this.defaultOffsetPos.z;
      }
      if (this.isMove)
      {
        this.currentMoveDis += this.moveSpd * Time.deltaTime;
        if ((double) this.currentMoveDis >= (double) this.moveDis)
        {
          this.currentMoveDis = this.moveDis;
          this.moveSpd *= -1f;
        }
        else
        {
          float num = 0.0f;
          if (this.isMinusMove)
            num = -this.moveDis;
          if ((double) this.currentMoveDis <= (double) num)
          {
            this.currentMoveDis = num;
            this.moveSpd *= -1f;
          }
        }
        Vector3 zero = Vector3.zero;
        Vector3 vector3 = this.moveDir != 0 ? Vector3.op_Multiply(this.bulletTrans.right, this.currentMoveDis) : Vector3.op_Multiply(this.bulletTrans.forward, this.currentMoveDis);
        if (this.isChaseXPos)
          position1.x += vector3.x;
        else
          position1.x = this.defaultPos.x + vector3.x;
        if (this.isChaseZPos)
          position1.z += vector3.z;
        else
          position1.z = this.defaultPos.z + vector3.z;
      }
      this._transform.position = position1;
    }
  }

  public void Destroy()
  {
    if (!Object.op_Inequality((Object) this.attacker, (Object) null))
      return;
    Enemy attacker = this.attacker as Enemy;
    if (!Object.op_Inequality((Object) attacker, (Object) null))
      return;
    attacker.OnDestroyObstacle(this);
  }
}
