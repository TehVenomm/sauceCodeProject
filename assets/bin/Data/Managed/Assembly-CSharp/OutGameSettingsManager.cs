// Decompiled with JetBrains decompiler
// Type: OutGameSettingsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class OutGameSettingsManager : MonoBehaviourSingleton<OutGameSettingsManager>
{
  public OutGameSettingsManager.CharaMakeScene charaMakeScene;
  public OutGameSettingsManager.CharaEditScene charaEditScene;
  public OutGameSettingsManager.HomeScene homeScene;
  public OutGameSettingsManager.LoungeScene loungeScene;
  public OutGameSettingsManager.ClanScene clanScene;
  public OutGameSettingsManager.QuestMap questMap;
  public OutGameSettingsManager.QuestResult questResult;
  public OutGameSettingsManager.StatusScene statusScene;
  public OutGameSettingsManager.SmithScene smithScene;
  public OutGameSettingsManager.GatherScene gatherScene;
  public OutGameSettingsManager.QuestSelect questSelect;
  public OutGameSettingsManager.ShopScene shopScene;
  public OutGameSettingsManager.StoryScene storyScene;
  public OutGameSettingsManager.GachaScene gachaScene;
  public OutGameSettingsManager.ProfileScene profileScene;
  public OutGameSettingsManager.LoginBonusScene loginBonusScene;
  public GuildScene guildScene;

  public OutGameSettingsManager.EnemyDisplayInfo SearchEnemyDisplayInfoForGacha(
    EnemyTable.EnemyData enemyData)
  {
    return this.GetEnemyDisplayInfo(this.gachaScene.enemyDisplayInfos, enemyData);
  }

  public OutGameSettingsManager.EnemyDisplayInfo SearchEnemyDisplayInfoForQuestSelect(
    EnemyTable.EnemyData enemyData)
  {
    return this.GetEnemyDisplayInfo(this.questSelect.enemyDisplayInfos, enemyData);
  }

  private OutGameSettingsManager.EnemyDisplayInfo GetEnemyDisplayInfo(
    OutGameSettingsManager.EnemyDisplayInfo[] displayInfos,
    EnemyTable.EnemyData enemyData)
  {
    OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = Array.Find<OutGameSettingsManager.EnemyDisplayInfo>(displayInfos, (Predicate<OutGameSettingsManager.EnemyDisplayInfo>) (o => o.modelID == enemyData.modelId));
    if (enemyDisplayInfo == null)
    {
      string typeName = enemyData.type.ToString();
      enemyDisplayInfo = Array.Find<OutGameSettingsManager.EnemyDisplayInfo>(displayInfos, (Predicate<OutGameSettingsManager.EnemyDisplayInfo>) (o => o.typeName == typeName));
    }
    return enemyDisplayInfo;
  }

  [Serializable]
  public class CharaMakeScene
  {
    public string stage;
    public float cameraFieldOfView;
    public Vector3 mainCameraPos;
    public Vector3 mainCameraRot;
    public Vector3 zoomCameraPos;
    public Vector3 playerPos;
    public float playerRot;
    public int playerBodyEquipItemID;
    public int playerHeadEquipItemID;
    public int playerArmEquipItemID;
    public int playerLegEquipItemID;
    public int presetPlayerNameCount;
    public bool isChangeHairShader;
  }

  [Serializable]
  public class CharaEditScene
  {
    public string stage;
    public Vector3 playerPos;
    public float playerRot;
  }

  [Serializable]
  public class HomeScene
  {
    public string mainStage;
    public Vector3 selfInitPos;
    public float selfInitRot;
    public Vector3 selfInitStoryEndPos;
    public float selfInitStoryEndRot;
    public float selfCameraAngleY;
    public float selfCameraTagetHeight;
    public float selfCameraHeightMin;
    public float selfCameraHeightMax;
    public float selfCameraDistanceMin;
    public float selfCameraDistanceMax;
    public float selfCameraZoomRate;
    public float selfCameraZoomCoef;
    public Vector3 questCenterNPCPos;
    public Vector3 questCenterNPCRot;
    public float questCenterNPCFOV;
    public Vector3 orderCenterNPCPos;
    public Vector3 orderCenterNPCRot;
    public float orderCenterNPCFOV;
    public float npc00StunCapability;
    public OutGameSettingsManager.HomeScene.NPC[] npcs;
    public OutGameSettingsManager.HomeScene.RandomEquip randomEquip;
    public int linkFieldPortalID;
    public AnimationCurve loginBonusMoveCureve;
    public AnimationCurve loginBonusScaleCureve;
    public string gachaDecoNewEffectName;
    public string[] gachaDecoIconEffectNames;
    public float gachaDecoIntervalTime;
    public Vector3 defaultTargetPos = new Vector3(0.0f, 0.0f, 7f);
    public Vector3 defaultCameraPos = Vector3.zero;

    public float GetSelfCameraHeight()
    {
      return Mathf.Lerp(this.selfCameraHeightMin, this.selfCameraHeightMax, this.selfCameraZoomRate);
    }

    public float GetSelfCameraDistance()
    {
      return Mathf.Lerp(this.selfCameraDistanceMin, this.selfCameraDistanceMax, this.selfCameraZoomRate);
    }

    public void SetupNPCSituations()
    {
      int index1 = 0;
      for (int length1 = this.npcs.Length; index1 < length1; ++index1)
      {
        OutGameSettingsManager.HomeScene.NPC npc = this.npcs[index1];
        npc.selectSituationID = -1;
        int num1 = Random.Range(0, 100);
        int num2 = 0;
        if (npc.enabled)
        {
          int index2 = 0;
          for (int length2 = npc.situations.Length; index2 < length2; ++index2)
          {
            OutGameSettingsManager.HomeScene.NPC.Situation situation = npc.situations[index2];
            if (situation.enabled && situation.percent != 0)
            {
              num2 += situation.percent;
              if (num1 <= num2)
              {
                npc.selectSituationID = index2;
                break;
              }
            }
          }
        }
      }
      int index3 = 0;
      for (int length3 = this.npcs.Length; index3 < length3; ++index3)
      {
        OutGameSettingsManager.HomeScene.NPC npc = this.npcs[index3];
        if (npc.enabled)
        {
          int index4 = 0;
          for (int length4 = npc.situations.Length; index4 < length4; ++index4)
          {
            OutGameSettingsManager.HomeScene.NPC.Situation situation1 = npc.situations[index4];
            if (situation1.enabled && situation1.percent == 0 && !string.IsNullOrEmpty(situation1.name))
            {
              for (int index5 = 0; index5 < length3; ++index5)
              {
                if (index5 != index3 && this.npcs[index5].enabled)
                {
                  OutGameSettingsManager.HomeScene.NPC.Situation situation2 = this.npcs[index5].GetSituation();
                  if (situation2 != null && situation2.enabled && !string.IsNullOrEmpty(situation2.name) && situation2.name == situation1.name)
                  {
                    npc.selectSituationID = index4;
                    break;
                  }
                }
              }
            }
          }
        }
      }
    }

    [Serializable]
    public class RandomEquip
    {
      public int[] bodys;
      public int[] helms;
      public int[] arms;
      public int[] legs;
    }

    [Serializable]
    public class NPC
    {
      public string comment;
      public bool enabled = true;
      public int npcID;
      public float scaleX;
      public string eventName;
      public OutGameSettingsManager.HomeScene.NPC.Situation[] situations;
      [NonSerialized]
      public int selectSituationID;
      public string overrideComponentName;
      public string wayPointName;

      public OutGameSettingsManager.HomeScene.NPC.Situation GetSituation()
      {
        return this.selectSituationID < 0 ? (OutGameSettingsManager.HomeScene.NPC.Situation) null : this.situations[this.selectSituationID];
      }

      public string GetLoopAnim() => this.GetSituation().loopAnim;

      public string GetNearAnim() => this.GetSituation().nearAnim;

      [Serializable]
      public class Situation
      {
        [Tooltip("シチュエーション名。他のNPCとリンクしたい場合に同じ名前を入力")]
        public string name;
        public bool enabled = true;
        [Tooltip("出現確率(0-100)。他のNPCのシチュエーションに合わせる場合は0を入力")]
        public int percent = 100;
        public Vector3 pos;
        public float rot;
        public string loopAnim;
        public string nearAnim;
      }
    }
  }

  [Serializable]
  public class LoungeScene : OutGameSettingsManager.HomeScene
  {
    public float moveSpeedRate = 1f;
    public float sittingCameraEaseCoef;
    public Vector3 waveSoundPoint;
    public Vector3 boardCenterNPCPos;
    public Vector3 boardCenterNPCRot;
    public float boardCenterNPCFOV;
  }

  [Serializable]
  public class ClanScene : OutGameSettingsManager.LoungeScene
  {
  }

  [Serializable]
  public class QuestMap
  {
    public Color monsterAmbientColor = Color.white;
    public float cameraFieldOfViwe = 40f;
    public float cameraMoveTime = 0.5f;
    public Vector3 cameraManualAngle = new Vector3(50f, 0.0f, 0.0f);
    public float cameraManualDistanceDef = 5f;
    public float cameraManualDistanceMin = 2f;
    public float cameraManualDistanceMax = 8f;
    public Vector3 cameraZoomAngle = new Vector3(50f, 0.0f, 0.0f);
    public float cameraZoomDistance = 2f;
    public float cameraBlurStrength = 0.25f;
    public float cameraBlurTime = 1f;
  }

  [Serializable]
  public class QuestResult
  {
    public Vector3[] playerPoss;
    public float[] playerRots;
    public float cameraFieldOfView;
    public float cameraBlurStrength;
    public float loseCameraHeight;
    public float loseCameraDistance;
    public float loseCameraRotateSpeed;
  }

  [Serializable]
  public class StatusScene
  {
    public bool isPlaySpAttackTypeMotion = true;
    public Vector3 playerPos;
    public float playerRot;
    public float playerScaleMale = 1f;
    public float playerScaleFemale = 0.97f;
    public float playerFemaleCameraOffsetY = -20f;
    public Vector3 smithNPCPos;
    public Vector3 smithNPCRot;
    public float smithSize;
    public float avatarPlayerRot;
    public float cameraHeight;
    public float cameraTargetHeight;
    public float cameraTargetDistance;
    public float cameraFieldOfView;
    public float cameraMoveTime;
    public Vector3 dirLightOffset;
    public float equipSectionStartBlurTime;
    public float equipSectionEndTime;
    public float equipSectionBlurStrength;
    public float equipSectionBlurDelay;
    public OutGameSettingsManager.StatusScene.EquipViewInfo[] equipViewInfos;
    public float renderTextureNearClip = 1f;
    public bool isChangeHairShader;

    public OutGameSettingsManager.StatusScene.EquipViewInfo GetEquipViewInfo(string find_name)
    {
      find_name = find_name.Replace("VISUAL_", "");
      int length = this.equipViewInfos.Length;
      for (int index = 0; index < length; ++index)
      {
        if (this.equipViewInfos[index].equipTypeName == find_name)
          return this.equipViewInfos[index];
      }
      return (OutGameSettingsManager.StatusScene.EquipViewInfo) null;
    }

    [Serializable]
    public class EquipViewInfo
    {
      public string equipTypeName;
      public Vector3 cameraTargetPos;
      public float cameraDistance;
      public float cameraYAngle;
      public float cameraXAngle;
    }
  }

  [Serializable]
  public class SmithScene
  {
    public string createStage;
    public string createUniqueStage;
    public float createCameraFieldOfView;
    public Vector3 createCameraPos;
    public Vector3 createCameraRot;
    public string glowSkillStage;
    public float glowSkillCameraFieldOfView;
    public Vector3 glowSkillCameraPos;
    public Vector3 glowSkillCameraRot;
  }

  [Serializable]
  public class GatherScene
  {
    public string mainStage;
    public AnimationClip cameraRailAnim;
    public float cameraDragRailRateCoef = -6f;
    public float cameraRailRateEaseCoef = 0.9f;
    public float cameraFieldOfView = 55f;
    public AnimationClip objectAppearAnim;
  }

  [Serializable]
  public class QuestSelect
  {
    [CustomArray("typeName")]
    public OutGameSettingsManager.EnemyDisplayInfo[] enemyDisplayInfos;
    [Tooltip("出発前キャラの描画順を右側を手前とする")]
    public bool isRightDepthForward = true;
  }

  [Serializable]
  public class ShopScene
  {
    public string mainStage;
    public Vector3 cameraPos;
    public Vector3 cameraRot;
    public Vector3 skillNPCPos;
    public Vector3 skillNPCRot;
    public Vector3 skillCatNPCPos;
    public Vector3 skillCatNPCRot;
    public float skillNPCFOV;
  }

  [Serializable]
  public class StoryScene
  {
    public float cameraHeight;
    public float cameraFieldOfView;
    public float cameraPanNormalZ = 0.75f;
    public float cameraPanNearZ = 1.5f;
    public float cameraPanFarZ = 0.25f;
    public Vector3 leftStandPos;
    public float leftStandRot;
    public float leftStandUpOffset;
    public float charaFadeTime = 0.25f;
    public float charaFadeMoveX = 0.1f;
    public Vector3 duoCameraPos = new Vector3(0.0f, 1.25f, 0.0f);
    public Vector3 trioCameraPos = new Vector3(0.0f, 1.25f, -0.95f);
  }

  [Serializable]
  public class GachaScene
  {
    public string SkillGachaStage;
    public string QuestSingleGachaStage;
    public string QuestReamGachaStage;
    public string QuestFeverGachaStage = "GA001D_03";
    [CustomArray("typeName")]
    public OutGameSettingsManager.EnemyDisplayInfo[] enemyDisplayInfos;
  }

  [Serializable]
  public class ProfileScene
  {
    public Vector3 playerPos = new Vector3(-12.65f, 0.63f, 1.8f);
    public float playerRot = 145f;
    public float cameraFieldOfView = 35f;
    public float nearClip = 1f;
  }

  [Serializable]
  public class EnemyDisplayInfo
  {
    public int modelID;
    public string typeName;
    public int animID;
    public Vector3 pos;
    public float angleY = 180f;
    public float scale = 1f;
    public float gachaScale = 1f;
    public int seIdhowl;
    public int seIdGachaShort;
    public int seIdGachaLong;

    public enum SCENE
    {
      QUEST,
      GACHA,
    }
  }

  [Serializable]
  public class LoginBonusScene
  {
    public Vector3 npc00Pos = new Vector3(-0.42f, 0.0f, 7.72f);
    public Vector3 npc00Rot = new Vector3(0.0f, 170f, 0.0f);
    public Vector3 npc06Pos = new Vector3(0.2f, 0.0f, 8f);
    public Vector3 npc06Rot = new Vector3(0.0f, 180f, 0.0f);
    public Vector3 npc00CameraPos = new Vector3(0.0f, 1.3f, 4f);
    public Vector3 npc00CameraRot = new Vector3(3.7f, -7f, 0.0f);
    public Vector3 cameraPos = new Vector3(0.0f, 1.3f, 0.3f);
    public Vector3 cameraRot = new Vector3(0.0f, 0.0f, 0.0f);
    public float cameraFov = 19f;
  }
}
