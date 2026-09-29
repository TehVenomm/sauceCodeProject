// Decompiled with JetBrains decompiler
// Type: AnimationDirectorProxy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AnimationDirectorProxy : MonoBehaviour
{
  private void __FUNCTION__InstantiatePrefab(string game_object_name)
  {
    if (!Object.op_Inequality((Object) AnimationDirector.I, (Object) null))
      return;
    AnimationDirector.I.__FUNCTION__InstantiatePrefab(game_object_name);
  }

  private void __FUNCTION__PlayAudio(string game_object_name)
  {
    if (!Object.op_Inequality((Object) AnimationDirector.I, (Object) null))
      return;
    AnimationDirector.I.__FUNCTION__PlayAudio(game_object_name);
  }

  private void __FUNCTION_Command(string command)
  {
    if (!Object.op_Inequality((Object) AnimationDirector.I, (Object) null))
      return;
    AnimationDirector.I.__FUNCTION_Command(command);
  }

  public void __FUNCTION__PlayCachedAudio(int se_id)
  {
    if (!Object.op_Inequality((Object) AnimationDirector.I, (Object) null))
      return;
    AnimationDirector.I.__FUNCTION__PlayCachedAudio(se_id);
  }
}
