// Decompiled with JetBrains decompiler
// Type: MapScriptWall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class MapScriptWall : MonoBehaviour
{
  [Tooltip("壁半径")]
  public float wallRadius = 40f;

  private void LateUpdate()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Vector3 zero = Vector3.zero;
    List<StageObject>.Enumerator enumerator = MonoBehaviourSingleton<StageObjectManager>.I.characterList.GetEnumerator();
    while (enumerator.MoveNext())
    {
      Vector3 vector3 = Vector3.op_Subtraction(enumerator.Current._transform.position, zero);
      if ((double) ((Vector3) ref vector3).magnitude > (double) this.wallRadius)
        enumerator.Current._transform.position = Vector3.op_Addition(zero, Vector3.op_Multiply(((Vector3) ref vector3).normalized, this.wallRadius));
    }
  }
}
