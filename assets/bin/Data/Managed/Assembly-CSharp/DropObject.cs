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

	public static DropObject Create(int rarity, bool is_region_break, Vector3 pos)
	{
		if (!MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			return null;
		}
		GameObject gameObject = MonoBehaviourSingleton<InGameManager>.I.CreateBossDropObject((!is_region_break) ? rarity : 2);
		if ((Object)gameObject == (Object)null)
		{
			return null;
		}
		DropObject dropObject = gameObject.GetComponent<DropObject>();
		if ((Object)dropObject == (Object)null)
		{
			dropObject = gameObject.AddComponent<DropObject>();
		}
		dropObject.Drop(rarity, pos);
		return dropObject;
	}

	protected virtual void Awake()
	{
		_transform = base.transform;
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.dropItem;
	}

	private void OnDisable()
	{
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.AddItemNum(_transform.position, rarity, isRight);
		}
	}

	protected void Update()
	{
		if (animStep == 0)
		{
			if (anim.IsPlaying())
			{
				animTime += Time.deltaTime;
				_transform.localRotation = Quaternion.AngleAxis(animTime * parameter.rotationSpeed, Vector3.up);
				float num = animTime / parameter.popAnimTime;
				if (num > 1f)
				{
					num = 1f;
				}
				Vector3 position = Vector3.Lerp(dropPos, targetPos, num);
				position.y = anim.Update() + parameter.defHeight;
				if (anim.IsPlaying())
				{
					_transform.position = position;
				}
			}
			else
			{
				base.gameObject.SetActive(false);
				animStep++;
			}
		}
	}

	protected void Drop(int _rarity, Vector3 pos)
	{
		rarity = _rarity;
		animStep = 0;
		animTime = 0f;
		isRight = true;
		anim.Set(parameter.popAnimTime, 0f, parameter.popHeight, parameter.popAnim, 0f, null);
		anim.Play();
		anim.Update(0f);
		pos.y = anim.Get() + parameter.defHeight;
		_transform.position = pos;
		Vector3 vector = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.right;
		vector.y = 0f;
		vector.Normalize();
		if (Random.Range(0, 10) > 5)
		{
			vector = Quaternion.AngleAxis(180f, Vector3.up) * vector;
			isRight = false;
		}
		vector *= parameter.popSpeed;
		targetPos = pos + vector;
		dropPos = pos;
	}
}
