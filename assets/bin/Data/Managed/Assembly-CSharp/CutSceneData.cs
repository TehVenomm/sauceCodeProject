// Decompiled with JetBrains decompiler
// Type: CutSceneData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CutSceneData : ScriptableObject
{
  public CutSceneData.PlayerData[] playerData;
  public CutSceneData.EnemyData enemyData;
  public CutSceneData.ActorData[] actorData;
  public RuntimeAnimatorController cameraController;
  public List<CutSceneData.SEKeyData> seDataList = new List<CutSceneData.SEKeyData>();
  public List<CutSceneData.EffectKeyData> effectKeyData = new List<CutSceneData.EffectKeyData>();
  public const int NOT_REQUEST_BGM = 0;
  public int bgm;
  public string mixerName;
  public const int NOT_REQUEST_STORY = 0;
  public int storyId;

  public enum ATTACHMENT_TYPE
  {
    NONE,
    CAMERA,
    MY_CHARACTER,
    PLAYER_1,
    PLAYER_2,
    PLAYER_3,
    ENEMY,
    ACTOR_1,
    ACTOR_2,
    ACTOR_3,
    ACTOR_4,
    MAX,
  }

  [Serializable]
  public class PlayerData
  {
    public CutSceneData.PlayerData.TYPE type;
    public Vector3 startPos;
    public float startAngleY;
    public RuntimeAnimatorController controller;

    public enum TYPE
    {
      MY_CHARACTER,
      PLAYER_1,
      PLAYER_2,
      PLAYER_3,
      MAX_NUM,
    }
  }

  [Serializable]
  public class EnemyData
  {
    public Vector3 startPos;
    public float startAngleY;
    public RuntimeAnimatorController controller;
  }

  [Serializable]
  public class ActorData
  {
    public GameObject prefab;
    public RuntimeAnimatorController animatorController;
    public Vector3 position;
    public Vector3 rotation;
    public CutSceneData.ATTACHMENT_TYPE attachmentType;
    public string nodeName;
  }

  [Serializable]
  public class SEKeyData
  {
    public float time;
    public int seId;
  }

  [Serializable]
  public class EffectKeyData
  {
    public float time;
    public string effectId;
    public Vector3 position;
    public Vector3 rotation;
    public CutSceneData.ATTACHMENT_TYPE attachmentType;
    public string nodeName;
  }
}
