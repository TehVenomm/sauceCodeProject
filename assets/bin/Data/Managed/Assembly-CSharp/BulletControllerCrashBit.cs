// Decompiled with JetBrains decompiler
// Type: BulletControllerCrashBit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerCrashBit : BulletControllerBase
{
  private Character character;
  private bool isWarping;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam skillParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, skillParam, pos, rot);
  }

  public override void RegisterFromObject(StageObject obj)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return;
    this.character = obj as Character;
  }

  public override void Update()
  {
    if (Object.op_Equality((Object) this.bulletObject, (Object) null))
      return;
    if (Object.op_Equality((Object) this.bulletObject.stageObject, (Object) null))
    {
      this.bulletObject.OnDestroy();
    }
    else
    {
      this.timeCount += Time.deltaTime;
      if (Object.op_Inequality((Object) this.character, (Object) null))
      {
        if (this.character.actionID == (Character.ACTION_ID) 36 && !this.isWarping)
        {
          ((Component) this.bulletObject.bulletEffect).gameObject.SetActive(false);
          this.bulletObject._collider.enabled = false;
          this.isWarping = true;
        }
        if (this.character.actionID != (Character.ACTION_ID) 36 && this.isWarping)
        {
          ((Component) this.bulletObject.bulletEffect).gameObject.SetActive(true);
          this.bulletObject._collider.enabled = true;
          this.isWarping = false;
        }
      }
      this._transform.position = this.bulletObject.stageObject._transform.position;
    }
  }
}
