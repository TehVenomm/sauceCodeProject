using System;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneData : ScriptableObject
{
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
		MAX
	}

	[Serializable]
	public class PlayerData
	{
		public enum TYPE
		{
			MY_CHARACTER,
			PLAYER_1,
			PLAYER_2,
			PLAYER_3,
			MAX_NUM
		}

		public TYPE type;

		public Vector3 startPos;

		public float startAngleY;

		public RuntimeAnimatorController controller;
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

		public ATTACHMENT_TYPE attachmentType;

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

		public ATTACHMENT_TYPE attachmentType;

		public string nodeName;
	}

	public PlayerData[] playerData;

	public EnemyData enemyData;

	public ActorData[] actorData;

	public RuntimeAnimatorController cameraController;

	public List<SEKeyData> seDataList = new List<SEKeyData>();

	public List<EffectKeyData> effectKeyData = new List<EffectKeyData>();

	public const int NOT_REQUEST_BGM = 0;

	public int bgm;

	public string mixerName;

	public const int NOT_REQUEST_STORY = 0;

	public int storyId;
}
