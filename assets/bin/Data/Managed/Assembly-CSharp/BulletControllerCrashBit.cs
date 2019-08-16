using UnityEngine;

public class BulletControllerCrashBit : BulletControllerBase
{
	private Character character;

	private bool isWarping;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		if (bulletObject == null)
		{
			return;
		}
		if (bulletObject.stageObject == null)
		{
			bulletObject.OnDestroy();
			return;
		}
		base.timeCount += Time.get_deltaTime();
		if (character != null)
		{
			if (character.actionID == (Character.ACTION_ID)36 && !isWarping)
			{
				bulletObject.bulletEffect.get_gameObject().SetActive(false);
				bulletObject._collider.set_enabled(false);
				isWarping = true;
			}
			if (character.actionID != (Character.ACTION_ID)36 && isWarping)
			{
				bulletObject.bulletEffect.get_gameObject().SetActive(true);
				bulletObject._collider.set_enabled(true);
				isWarping = false;
			}
		}
		base._transform.set_position(bulletObject.stageObject._transform.get_position());
	}
}
