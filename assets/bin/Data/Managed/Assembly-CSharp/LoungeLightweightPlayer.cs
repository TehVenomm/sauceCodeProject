// Decompiled with JetBrains decompiler
// Type: LoungeLightweightPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections;
using UnityEngine;

#nullable disable
public class LoungeLightweightPlayer : LoungePlayer
{
  protected override bool IsValidMove() => !this.isMoving && !this.isPlayingSitAnimation;

  protected override void InitAnim()
  {
  }

  protected override IEnumerator Move()
  {
    this.isMoving = true;
    float t = 0.0f;
    Vector3 pos = this._transform.position;
    do
    {
      Vector3.op_Subtraction(this.moveTargetPos, pos);
      this._transform.position = Vector3.Lerp(pos, this.moveTargetPos, t);
      t += Time.deltaTime / 1f;
      yield return (object) null;
    }
    while ((double) t < 1.0);
    this._transform.position = this.moveTargetPos;
    this.isMoving = false;
  }

  protected override IEnumerator DoSit()
  {
    yield break;
  }

  protected override PlayerLoader Load(
    LoungePlayer chara,
    GameObject go,
    CharaInfo chara_info,
    PlayerLoader.OnCompleteLoad callback)
  {
    LightweightPlayerLoader lightweightPlayerLoader = go.AddComponent<LightweightPlayerLoader>();
    PlayerLoadInfo player_load_info = new PlayerLoadInfo();
    if (chara_info != null)
      player_load_info.Apply(chara_info, false, true, true, true);
    lightweightPlayerLoader.StartLoad(player_load_info, 8, 99, false, false, true, true, false, false, true, true, SHADER_TYPE.NORMAL, callback, true, -1);
    return (PlayerLoader) lightweightPlayerLoader;
  }

  public override void OnRecvSit()
  {
  }

  public override void OnRecvStandUp()
  {
  }

  public override bool DispatchEvent() => false;
}
