using UnityEngine;

public class DropObject : MonoBehaviour
{
	protected int rarity;

	protected int animStep = -1;

	protected FloatInterpolator anim = new FloatInterpolator();

	protected Vector3 dropPos;

	protected Vector3 targetPos;

	protected float animTime;

	protected bool isRight = true;

	protected InGameSettingsManager.DropItem parameter;

	public Transform _transform
	{
		get;
		protected set;
	}

	public DropObject()
		: this()
	{
	}

	public static DropObject Create(int rarity, bool is_region_break, Vector3 pos)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			return null;
		}
		GameObject val = MonoBehaviourSingleton<InGameManager>.I.CreateBossDropObject((!is_region_break) ? rarity : 2);
		if (val == null)
		{
			return null;
		}
		DropObject dropObject = val.GetComponent<DropObject>();
		if (dropObject == null)
		{
			dropObject = val.AddComponent<DropObject>();
		}
		dropObject.Drop(rarity, pos);
		return dropObject;
	}

	protected virtual void Awake()
	{
		_transform = this.get_transform();
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.dropItem;
	}

	private void OnDisable()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.AddItemNum(_transform.get_position(), rarity, isRight);
		}
	}

	protected void Update()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		if (animStep != 0)
		{
			return;
		}
		if (anim.IsPlaying())
		{
			animTime += Time.get_deltaTime();
			_transform.set_localRotation(Quaternion.AngleAxis(animTime * parameter.rotationSpeed, Vector3.get_up()));
			float num = animTime / parameter.popAnimTime;
			if (num > 1f)
			{
				num = 1f;
			}
			Vector3 position = Vector3.Lerp(dropPos, targetPos, num);
			position.y = anim.Update() + parameter.defHeight;
			if (anim.IsPlaying())
			{
				_transform.set_position(position);
			}
		}
		else
		{
			this.get_gameObject().SetActive(false);
			animStep++;
		}
	}

	protected void Drop(int _rarity, Vector3 pos)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		rarity = _rarity;
		animStep = 0;
		animTime = 0f;
		isRight = true;
		anim.Set(parameter.popAnimTime, 0f, parameter.popHeight, parameter.popAnim, 0f);
		anim.Play();
		anim.Update(0f);
		pos.y = anim.Get() + parameter.defHeight;
		_transform.set_position(pos);
		Vector3 val = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_right();
		val.y = 0f;
		val.Normalize();
		if (Random.Range(0, 10) > 5)
		{
			val = Quaternion.AngleAxis(180f, Vector3.get_up()) * val;
			isRight = false;
		}
		val *= parameter.popSpeed;
		targetPos = pos + val;
		dropPos = pos;
	}
}
