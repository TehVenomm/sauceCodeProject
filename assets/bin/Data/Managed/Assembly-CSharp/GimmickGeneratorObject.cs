using System.Collections.Generic;
using UnityEngine;

public class GimmickGeneratorObject : StageObject, IFieldGimmickObject
{
	private enum GENERATE_TYPE
	{
		NONE,
		LINEAR_MOVE,
		MAX
	}

	private class GeneratedObject
	{
		private Transform trans;

		private bool enable;

		private float duration;

		private float timer;

		public GeneratedObject(Transform trans, float duration)
		{
			this.trans = trans;
			this.duration = duration;
			enable = true;
			timer = 0f;
		}

		public void Update()
		{
			timer += Time.get_deltaTime();
			if (timer >= duration)
			{
				Destroy();
			}
		}

		public void Destroy()
		{
			if (trans != null)
			{
				Object.Destroy(trans.get_gameObject());
				trans = null;
			}
			enable = false;
		}

		public bool IsEnable()
		{
			return enable;
		}

		public Vector3 GetPosition()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return trans.get_position();
		}
	}

	private const string NAME_NODE_ATK_COLLIDER_R = "attack_R";

	private const string NAME_NODE_ATK_COLLIDER_L = "attack_L";

	private int generateType;

	private float interval;

	private float duration;

	private float startX;

	private float startZ;

	private float endX;

	private float endZ;

	private string effectName = string.Empty;

	private float normalAtk;

	private int colliderDirection = 2;

	private float colliderRadius;

	private float colliderHeight;

	private float colliderCenterX;

	private float colliderCenterY;

	private float colliderCenterZ;

	private Vector3 startPos = Vector3.get_zero();

	private Vector3 endPos = Vector3.get_zero();

	private Vector3 center = Vector3.get_zero();

	private float generateTimer;

	private AttackHitChecker attackHitChecker = new AttackHitChecker();

	private bool referenceCheckerFlag;

	private List<GeneratedObject> generatedObjectList = new List<GeneratedObject>();

	public void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		if (pointData != null)
		{
			id = (int)pointData.pointID;
			ParseParam(pointData.value2);
			SetAppearPoints();
			SetCenter();
		}
	}

	public int GetId()
	{
		return id;
	}

	public void RequestDestroy()
	{
		DestroyObject();
	}

	public void SetTransform(Transform trans)
	{
	}

	public Transform GetTransform()
	{
		return base._transform;
	}

	public string GetObjectName()
	{
		return "GimmickGeneratorObject";
	}

	public float GetTargetRadius()
	{
		return 0f;
	}

	public float GetTargetSqrRadius()
	{
		return 0f;
	}

	public void UpdateTargetMarker(bool isNear)
	{
	}

	public bool IsSearchableNearest()
	{
		return true;
	}

	protected override void Update()
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
		int i = 0;
		for (int count = generatedObjectList.Count; i < count; i++)
		{
			generatedObjectList[i].Update();
		}
		generatedObjectList.RemoveAll((GeneratedObject o) => !o.IsEnable());
		if (IsCoopNone() || IsOriginal())
		{
			generateTimer += Time.get_deltaTime();
			GENERATE_TYPE gENERATE_TYPE = (GENERATE_TYPE)generateType;
			if (gENERATE_TYPE == GENERATE_TYPE.LINEAR_MOVE && generateTimer > interval)
			{
				OnGenerateForLinearMove(startPos);
				generateTimer = 0f;
			}
		}
	}

	public void OnGenerateForLinearMove(Vector3 pos)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		if (generatedObjectList != null && ((!IsCoopNone() && !IsOriginal()) || generatedObjectList.Count <= 0))
		{
			Transform val = Utility.CreateGameObject("GimmickShot", base._transform);
			val.set_position(pos);
			Transform obj = val;
			Vector3 val2 = endPos - startPos;
			obj.set_rotation(Quaternion.LookRotation(val2.get_normalized()));
			Transform val3 = null;
			if (!effectName.IsNullOrWhiteSpace())
			{
				val3 = EffectManager.GetEffect(effectName, val);
				val3.set_localPosition(Vector3.get_zero());
				val3.set_localRotation(Quaternion.get_identity());
			}
			if (val3 != null)
			{
				Transform val4 = Utility.Find(val3, "attack_R");
				Transform val5 = Utility.Find(val3, "attack_L");
				GeneratedAttackObject generatedAttackObject = val4.get_gameObject().AddComponent<GeneratedAttackObject>();
				GeneratedAttackObject generatedAttackObject2 = val5.get_gameObject().AddComponent<GeneratedAttackObject>();
				GeneratedAttackObject generatedAttackObject3 = val4.get_gameObject().AddComponent<GeneratedAttackObject>();
				GeneratedAttackObject generatedAttackObject4 = val5.get_gameObject().AddComponent<GeneratedAttackObject>();
				AttackHitInfo attackHitInfo = new AttackHitInfo();
				attackHitInfo.name = "generatedAttack";
				attackHitInfo.attackType = AttackHitInfo.ATTACK_TYPE.GIMMICK_GENERATED;
				attackHitInfo.toPlayer.reactionType = AttackHitInfo.ToPlayer.REACTION_TYPE.BLOW;
				attackHitInfo.toPlayer.reactionBlowForce = 100f;
				attackHitInfo.toPlayer.reactionBlowAngle = 20f;
				attackHitInfo.atk.normal = normalAtk;
				generatedAttackObject.Initialize(this, val4.get_parent(), attackHitInfo, Vector3.get_zero(), Vector3.get_zero(), colliderRadius, colliderHeight, colliderDirection, center, 31);
				generatedAttackObject2.Initialize(this, val5.get_parent(), attackHitInfo, Vector3.get_zero(), Vector3.get_zero(), colliderRadius, colliderHeight, colliderDirection, -center, 31);
				generatedAttackObject3.Initialize(this, val4.get_parent(), attackHitInfo, Vector3.get_zero(), Vector3.get_zero(), colliderRadius, colliderHeight, colliderDirection, center, 31);
				generatedAttackObject4.Initialize(this, val5.get_parent(), attackHitInfo, Vector3.get_zero(), Vector3.get_zero(), colliderRadius, colliderHeight, colliderDirection, -center, 31);
			}
			GeneratedObject item = new GeneratedObject(val, 10f);
			generatedObjectList.Add(item);
			if (referenceCheckerFlag)
			{
				attackHitChecker = new AttackHitChecker();
				referenceCheckerFlag = false;
			}
			if (base.packetSender != null)
			{
				base.packetSender.OnShotGimmickGenerator(pos);
			}
		}
	}

	private void SetAppearPoints()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		startPos = new Vector3(startX, 0f, startZ);
		endPos = new Vector3(endX, 0f, endZ);
	}

	private void SetCenter()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		center = new Vector3(colliderCenterX, colliderCenterY, colliderCenterZ);
	}

	private void ParseParam(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return;
		}
		string[] array = value2.Split(',');
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2)
			{
				switch (array2[0])
				{
				case "type":
					int.TryParse(array2[1], out generateType);
					break;
				case "in":
					float.TryParse(array2[1], out interval);
					break;
				case "du":
					float.TryParse(array2[1], out duration);
					break;
				case "sX":
					float.TryParse(array2[1], out startX);
					break;
				case "sZ":
					float.TryParse(array2[1], out startZ);
					break;
				case "eX":
					float.TryParse(array2[1], out endX);
					break;
				case "eZ":
					float.TryParse(array2[1], out endZ);
					break;
				case "eff":
					effectName = array2[1];
					break;
				case "atk":
					float.TryParse(array2[1], out normalAtk);
					break;
				case "dir":
					int.TryParse(array2[1], out colliderDirection);
					break;
				case "r":
					float.TryParse(array2[1], out colliderRadius);
					break;
				case "h":
					float.TryParse(array2[1], out colliderHeight);
					break;
				case "cX":
					float.TryParse(array2[1], out colliderCenterX);
					break;
				case "cY":
					float.TryParse(array2[1], out colliderCenterY);
					break;
				case "cZ":
					float.TryParse(array2[1], out colliderCenterZ);
					break;
				}
			}
		}
	}

	public static string[] GetEffectNames(string value2)
	{
		List<string> list = new List<string>();
		if (value2.IsNullOrWhiteSpace())
		{
			return new string[0];
		}
		string[] array = value2.Split(',');
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2)
			{
				string text = array2[0];
				if (text != null && text == "eff")
				{
					list.Add(array2[1]);
				}
			}
		}
		return list.ToArray();
	}

	protected override bool IsValidAttackedHit(StageObject from_object)
	{
		return false;
	}

	public override AttackHitChecker ReferenceAttackHitChecker()
	{
		referenceCheckerFlag = true;
		return attackHitChecker;
	}
}
