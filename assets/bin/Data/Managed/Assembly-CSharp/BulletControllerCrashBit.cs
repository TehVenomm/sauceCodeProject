using UnityEngine;

public class BulletControllerCrashBit : BulletControllerBase
{
	private Character character;

	private bool isWarping;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, skillParam, pos, rot);
	}

	public override void RegisterFromObject(StageObject obj)
	{
		if (!(obj == null))
		{
			character = (obj as Character);
		}
	}

	public override void Update()
	{
		if (bulletObject == null)
		{
			return;
		}
		if (bulletObject.stageObject == null)
		{
			bulletObject.OnDestroy();
			return;
		}
		base.timeCount += Time.deltaTime;
		if (character != null)
		{
			if (character.actionID == (Character.ACTION_ID)36 && !isWarping)
			{
				bulletObject.bulletEffect.gameObject.SetActive(value: false);
				bulletObject._collider.enabled = false;
				isWarping = true;
			}
			if (character.actionID != (Character.ACTION_ID)36 && isWarping)
			{
				bulletObject.bulletEffect.gameObject.SetActive(value: true);
				bulletObject._collider.enabled = true;
				isWarping = false;
			}
		}
		base._transform.position = bulletObject.stageObject._transform.position;
	}
}
