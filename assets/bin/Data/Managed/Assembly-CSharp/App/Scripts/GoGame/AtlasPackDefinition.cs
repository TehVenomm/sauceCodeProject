// Decompiled with JetBrains decompiler
// Type: App.Scripts.GoGame.AtlasPackDefinition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace App.Scripts.GoGame;

public class AtlasPackDefinition : ScriptableObject
{
  public AtlasPackDefinition.GoOptDefinition[] Defines;

  [Serializable]
  public class GoOptDefinition
  {
    public string Name;
    public Shader Shader;
    public TextAsset[] Child;
  }
}
