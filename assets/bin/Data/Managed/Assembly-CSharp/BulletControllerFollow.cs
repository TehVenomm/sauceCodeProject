using UnityEngine;

public class BulletControllerFollow : BulletControllerBase
{
	protected int followObjId;

	protected Character followObj;

	protected Vector3 followOffset = Vector3.zero;

	protected float attenuation = 0.5f;

	private Transform rootNode;

	private Vector3 velocity = Vector3.zero;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, skillParam, pos, rot);
		base._rigidbody.isKinematic = true;
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
		base._transform.position = base._transform.position + (velocity + (rootNode.position + followObj._rotation * followOffset - base._transform.position) * base.speed) * attenuation * Time.deltaTime;
	}
}
