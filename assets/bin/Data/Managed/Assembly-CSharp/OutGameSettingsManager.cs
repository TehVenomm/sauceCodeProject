using System;
using UnityEngine;

public class OutGameSettingsManager : MonoBehaviourSingleton<OutGameSettingsManager>
{
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
			[Serializable]
			public class Situation
			{
				[Tooltip("シチュエ\u30fcション名。他のNPCとリンクしたい場合に同じ名前を入力")]
				public string name;

				public bool enabled = true;

				[Tooltip("出現確率(0-100)。他のNPCのシチュエ\u30fcションに合わせる場合は0を入力")]
				public int percent = 100;

				public Vector3 pos;

				public float rot;

				public string loopAnim;

				public string nearAnim;
			}

			public string comment;

			public bool enabled = true;

			public int npcID;

			public float scaleX;

			public string eventName;

			public Situation[] situations;

			[NonSerialized]
			public int selectSituationID;

			public string overrideComponentName;

			public string wayPointName;

			public Situation GetSituation()
			{
				if (selectSituationID < 0)
				{
					return null;
				}
				return situations[selectSituationID];
			}

			public string GetLoopAnim()
			{
				return GetSituation().loopAnim;
			}

			public string GetNearAnim()
			{
				return GetSituation().nearAnim;
			}
		}

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

		public NPC[] npcs;

		public RandomEquip randomEquip;

		public int linkFieldPortalID;

		public AnimationCurve loginBonusMoveCureve;

		public AnimationCurve loginBonusScaleCureve;

		public string gachaDecoNewEffectName;

		public string[] gachaDecoIconEffectNames;

		public float gachaDecoIntervalTime;

		public Vector3 defaultTargetPos = new Vector3(0f, 0f, 7f);

		public Vector3 defaultCameraPos = Vector3.zero;

		public float GetSelfCameraHeight()
		{
			return Mathf.Lerp(selfCameraHeightMin, selfCameraHeightMax, selfCameraZoomRate);
		}

		public float GetSelfCameraDistance()
		{
			return Mathf.Lerp(selfCameraDistanceMin, selfCameraDistanceMax, selfCameraZoomRate);
		}

		public void SetupNPCSituations()
		{
			int i = 0;
			for (int num = npcs.Length; i < num; i++)
			{
				NPC nPC = npcs[i];
				nPC.selectSituationID = -1;
				int num2 = UnityEngine.Random.Range(0, 100);
				int num3 = 0;
				if (!nPC.enabled)
				{
					continue;
				}
				int j = 0;
				for (int num4 = nPC.situations.Length; j < num4; j++)
				{
					NPC.Situation situation = nPC.situations[j];
					if (situation.enabled && situation.percent != 0)
					{
						num3 += situation.percent;
						if (num2 <= num3)
						{
							nPC.selectSituationID = j;
							break;
						}
					}
				}
			}
			int k = 0;
			for (int num5 = npcs.Length; k < num5; k++)
			{
				NPC nPC2 = npcs[k];
				if (!nPC2.enabled)
				{
					continue;
				}
				int l = 0;
				for (int num6 = nPC2.situations.Length; l < num6; l++)
				{
					NPC.Situation situation2 = nPC2.situations[l];
					if (!situation2.enabled || situation2.percent != 0 || string.IsNullOrEmpty(situation2.name))
					{
						continue;
					}
					for (int m = 0; m < num5; m++)
					{
						if (m != k && npcs[m].enabled)
						{
							NPC.Situation situation3 = npcs[m].GetSituation();
							if (situation3 != null && situation3.enabled && !string.IsNullOrEmpty(situation3.name) && situation3.name == situation2.name)
							{
								nPC2.selectSituationID = l;
								break;
							}
						}
					}
				}
			}
		}
	}

	[Serializable]
	public class LoungeScene : HomeScene
	{
		public float moveSpeedRate = 1f;

		public float sittingCameraEaseCoef;

		public Vector3 waveSoundPoint;

		public Vector3 boardCenterNPCPos;

		public Vector3 boardCenterNPCRot;

		public float boardCenterNPCFOV;
	}

	[Serializable]
	public class ClanScene : LoungeScene
	{
	}

	[Serializable]
	public class QuestMap
	{
		public Color monsterAmbientColor = Color.white;

		public float cameraFieldOfViwe = 40f;

		public float cameraMoveTime = 0.5f;

		public Vector3 cameraManualAngle = new Vector3(50f, 0f, 0f);

		public float cameraManualDistanceDef = 5f;

		public float cameraManualDistanceMin = 2f;

		public float cameraManualDistanceMax = 8f;

		public Vector3 cameraZoomAngle = new Vector3(50f, 0f, 0f);

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
		[Serializable]
		public class EquipViewInfo
		{
			public string equipTypeName;

			public Vector3 cameraTargetPos;

			public float cameraDistance;

			public float cameraYAngle;

			public float cameraXAngle;
		}

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

		public EquipViewInfo[] equipViewInfos;

		public float renderTextureNearClip = 1f;

		public bool isChangeHairShader;

		public EquipViewInfo GetEquipViewInfo(string find_name)
		{
			find_name = find_name.Replace("VISUAL_", "");
			int num = equipViewInfos.Length;
			for (int i = 0; i < num; i++)
			{
				if (equipViewInfos[i].equipTypeName == find_name)
				{
					return equipViewInfos[i];
				}
			}
			return null;
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
		public EnemyDisplayInfo[] enemyDisplayInfos;

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

		public Vector3 duoCameraPos = new Vector3(0f, 1.25f, 0f);

		public Vector3 trioCameraPos = new Vector3(0f, 1.25f, -0.95f);
	}

	[Serializable]
	public class GachaScene
	{
		public string SkillGachaStage;

		public string QuestSingleGachaStage;

		public string QuestReamGachaStage;

		public string QuestFeverGachaStage = "GA001D_03";

		[CustomArray("typeName")]
		public EnemyDisplayInfo[] enemyDisplayInfos;
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
		public enum SCENE
		{
			QUEST,
			GACHA
		}

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
	}

	[Serializable]
	public class LoginBonusScene
	{
		public Vector3 npc00Pos = new Vector3(-0.42f, 0f, 7.72f);

		public Vector3 npc00Rot = new Vector3(0f, 170f, 0f);

		public Vector3 npc06Pos = new Vector3(0.2f, 0f, 8f);

		public Vector3 npc06Rot = new Vector3(0f, 180f, 0f);

		public Vector3 npc00CameraPos = new Vector3(0f, 1.3f, 4f);

		public Vector3 npc00CameraRot = new Vector3(3.7f, -7f, 0f);

		public Vector3 cameraPos = new Vector3(0f, 1.3f, 0.3f);

		public Vector3 cameraRot = new Vector3(0f, 0f, 0f);

		public float cameraFov = 19f;
	}

	public CharaMakeScene charaMakeScene;

	public CharaEditScene charaEditScene;

	public HomeScene homeScene;

	public LoungeScene loungeScene;

	public ClanScene clanScene;

	public QuestMap questMap;

	public QuestResult questResult;

	public StatusScene statusScene;

	public SmithScene smithScene;

	public GatherScene gatherScene;

	public QuestSelect questSelect;

	public ShopScene shopScene;

	public StoryScene storyScene;

	public GachaScene gachaScene;

	public ProfileScene profileScene;

	public LoginBonusScene loginBonusScene;

	public GuildScene guildScene;

	public EnemyDisplayInfo SearchEnemyDisplayInfoForGacha(EnemyTable.EnemyData enemyData)
	{
		return GetEnemyDisplayInfo(gachaScene.enemyDisplayInfos, enemyData);
	}

	public EnemyDisplayInfo SearchEnemyDisplayInfoForQuestSelect(EnemyTable.EnemyData enemyData)
	{
		return GetEnemyDisplayInfo(questSelect.enemyDisplayInfos, enemyData);
	}

	private EnemyDisplayInfo GetEnemyDisplayInfo(EnemyDisplayInfo[] displayInfos, EnemyTable.EnemyData enemyData)
	{
		EnemyDisplayInfo enemyDisplayInfo = Array.Find(displayInfos, (EnemyDisplayInfo o) => o.modelID == enemyData.modelId);
		if (enemyDisplayInfo == null)
		{
			string typeName = enemyData.type.ToString();
			enemyDisplayInfo = Array.Find(displayInfos, (EnemyDisplayInfo o) => o.typeName == typeName);
		}
		return enemyDisplayInfo;
	}
}
