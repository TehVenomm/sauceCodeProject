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
		if (!((Object)obj == (Object)null))
		{
			character = (obj as Character);
		}
	}

	public override void Update()
	{
		if (!((Object)bulletObject == (Object)null))
		{
			if ((Object)bulletObject.stageObject == (Object)null)
			{
				bulletObject.OnDestroy();
			}
			else
			{
				base.timeCount += Time.deltaTime;
				if ((Object)character != (Object)null)
				{
					if (character.actionID == (Character.ACTION_ID)35 && !isWarping)
					{
						bulletObject.bulletEffect.gameObject.SetActive(false);
						bulletObject._collider.enabled = false;
						isWarping = true;
					}
					if (character.actionID != (Character.ACTION_ID)35 && isWarping)
					{
						bulletObject.bulletEffect.gameObject.SetActive(true);
						bulletObject._collider.enabled = true;
						isWarping = false;
					}
				}
				base._transform.position = bulletObject.stageObject._transform.position;
			}
		}
	}
}
