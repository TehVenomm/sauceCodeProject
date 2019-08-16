using UnityEngine;

public class BulletControllerFollow : BulletControllerBase
{
	protected int followObjId;

	protected Character followObj;

	protected Vector3 followOffset = Vector3.get_zero();

	protected float attenuation = 0.5f;

	private Transform rootNode;

	private Vector3 velocity = Vector3.get_zero();

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, skillParam, pos, rot);
		base._rigidbody.set_isKinematic(true);
		followOffset = bullet.dataFollow.followOffset;
		attenuation = bullet.dataFollow.attenuation;
	}

	public override void RegisterFromObject(StageObject obj)
	{
		base.RegisterFromObject(obj);
		followObj = (obj as Character);
		rootNode = followObj.rootNode;
		followObjId = followObj.id;
	}

	public virtual void CheckFromObject()
	{
		if (followObjId != 0)
		{
			StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Find((StageObject obj) => obj.id == followObjId);
			if (stageObject != null)
			{
				RegisterFromObject(stageObject as Character);
			}
		}
	}

	public override void Update()
	{
		if (followObj == null || rootNode == null)
		{
			CheckFromObject();
		}
		else
		{
			UpdateFollowPosition();
		}
	}

	protected void UpdateFollowPosition()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		base._transform.set_position(base._transform.get_position() + (velocity + (rootNode.get_position() + followObj._rotation * followOffset - base._transform.get_position()) * base.speed) * attenuation * Time.get_deltaTime());
	}
}
