using UnityEngine;

public class AttackShotNodeLink : MonoBehaviour
{
	private BulletData bulletData;

	private GameObject bulletObj;

	private Transform bulletTrans;

	private StageObject attacker;

	private string atkInfoName = string.Empty;

	private Transform parentTrans;

	private bool isRequestDelete;

	private bool isInit;

	private Vector3 defaultPos;

	private Vector3 defaultOffsetPos;

	private Vector3 defaultOffsetRot;

	private float rotSpd;

	private float moveSpd;

	private float moveDis;

	private int moveDir;

	private bool isChaseXPos = true;

	private bool isChaseYPos = true;

	private bool isChaseZPos = true;

	private bool isUseChasePos;

	private bool isChaseXRot = true;

	private bool isChaseYRot = true;

	private bool isChaseZRot = true;

	private bool isUseChaseRot;

	private bool isRot;

	private bool isMove;

	private bool isMinusMove;

	private float currentRotAngle;

	private float currentMoveDis;

	private Transform _transform;

	public string AttackInfoName => atkInfoName;

	public AttackShotNodeLink()
		: this()
	{
	}

	public void RequestDestroy()
	{
		isRequestDelete = true;
	}

	public void Initialize(StageObject attacker, Transform parentTrans, AnimEventData.EventData data, AttackInfo atkInfo, AnimEventShot childEventShot)
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		bulletData = atkInfo.bulletData;
		if (bulletData == null)
		{
			return;
		}
		BulletData.BulletBase data2 = bulletData.data;
		if (data2 != null)
		{
			this.attacker = attacker;
			this.parentTrans = parentTrans;
			int num = (!(attacker is Player)) ? 15 : 14;
			AttackHitInfo attackHitInfo = atkInfo as AttackHitInfo;
			if (attackHitInfo != null)
			{
				attackHitInfo.enableIdentityCheck = false;
			}
			atkInfoName = atkInfo.name;
			_transform = this.get_transform();
			_transform.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
			_transform.set_position(parentTrans.get_position());
			switch (data.intArgs[0])
			{
			case 0:
				_transform.set_rotation(attacker._transform.get_rotation());
				break;
			case 1:
				_transform.set_rotation(parentTrans.get_rotation());
				break;
			case 2:
				_transform.set_rotation(Quaternion.get_identity());
				break;
			}
			defaultPos = _transform.get_position();
			childEventShot.get_transform().set_parent(_transform);
			bulletObj = childEventShot.get_gameObject();
			bulletTrans = bulletObj.get_transform();
			Vector3 localPosition = default(Vector3);
			localPosition._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			bulletTrans.set_localEulerAngles(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
			bulletTrans.set_localPosition(localPosition);
			rotSpd = data.floatArgs[6];
			moveSpd = data.floatArgs[7];
			moveDis = data.floatArgs[8];
			isChaseXPos = ((data.intArgs[2] != 0) ? true : false);
			isChaseYPos = ((data.intArgs[3] != 0) ? true : false);
			isChaseZPos = ((data.intArgs[4] != 0) ? true : false);
			isChaseXRot = ((data.intArgs[5] != 0) ? true : false);
			isChaseYRot = ((data.intArgs[6] != 0) ? true : false);
			isChaseZRot = ((data.intArgs[7] != 0) ? true : false);
			isRot = ((data.intArgs[8] != 0) ? true : false);
			isMove = ((data.intArgs[9] != 0) ? true : false);
			moveDir = data.intArgs[10];
			isMinusMove = ((data.intArgs[11] != 0) ? true : false);
			if (parentTrans != null)
			{
				defaultOffsetPos = Vector3.get_zero();
				defaultOffsetRot = _transform.get_eulerAngles() - parentTrans.get_eulerAngles();
			}
			else
			{
				defaultOffsetPos = Vector3.get_zero();
				defaultOffsetRot = Vector3.get_zero();
			}
			if (isChaseXPos || isChaseYPos || isChaseZPos)
			{
				isUseChasePos = true;
			}
			if (isChaseXRot || isChaseYRot || isChaseZRot)
			{
				isUseChaseRot = true;
			}
			currentRotAngle = 0f;
			isInit = true;
		}
	}

	private void LateUpdate()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		if (!isInit)
		{
			return;
		}
		if (bulletObj == null)
		{
			Object.Destroy(this.get_gameObject());
			return;
		}
		if (isRequestDelete)
		{
			Object.Destroy(bulletObj);
			return;
		}
		if (parentTrans == null)
		{
			Object.Destroy(bulletObj);
			return;
		}
		Vector3 position = _transform.get_position();
		Vector3 position2 = parentTrans.get_position();
		Vector3 eulerAngles = _transform.get_eulerAngles();
		Vector3 eulerAngles2 = parentTrans.get_eulerAngles();
		if (isUseChaseRot)
		{
			if (isChaseXRot)
			{
				eulerAngles.x = eulerAngles2.x + defaultOffsetRot.x;
			}
			if (isChaseYRot)
			{
				eulerAngles.y = eulerAngles2.y + defaultOffsetRot.y;
			}
			if (isChaseZRot)
			{
				eulerAngles.z = eulerAngles2.z + defaultOffsetRot.z;
			}
		}
		if (isRot)
		{
			if (isChaseYRot)
			{
				currentRotAngle += rotSpd * Time.get_deltaTime();
			}
			else
			{
				currentRotAngle = rotSpd * Time.get_deltaTime();
			}
			if (currentRotAngle > 360f)
			{
				currentRotAngle -= 360f;
			}
			else if (currentRotAngle < 0f)
			{
				currentRotAngle += 360f;
			}
			eulerAngles.y += currentRotAngle;
		}
		_transform.set_eulerAngles(eulerAngles);
		if (isUseChasePos)
		{
			if (isChaseXPos)
			{
				position.x = position2.x + defaultOffsetPos.x;
			}
			if (isChaseYPos)
			{
				position.y = position2.y + defaultOffsetPos.y;
			}
			if (isChaseZPos)
			{
				position.z = position2.z + defaultOffsetPos.z;
			}
		}
		if (isMove)
		{
			currentMoveDis += moveSpd * Time.get_deltaTime();
			if (currentMoveDis >= moveDis)
			{
				currentMoveDis = moveDis;
				moveSpd *= -1f;
			}
			else
			{
				float num = 0f;
				if (isMinusMove)
				{
					num = 0f - moveDis;
				}
				if (currentMoveDis <= num)
				{
					currentMoveDis = num;
					moveSpd *= -1f;
				}
			}
			Vector3 zero = Vector3.get_zero();
			zero = ((moveDir != 0) ? (bulletTrans.get_right() * currentMoveDis) : (bulletTrans.get_forward() * currentMoveDis));
			if (isChaseXPos)
			{
				position.x += zero.x;
			}
			else
			{
				position.x = defaultPos.x + zero.x;
			}
			if (isChaseZPos)
			{
				position.z += zero.z;
			}
			else
			{
				position.z = defaultPos.z + zero.z;
			}
		}
		_transform.set_position(position);
	}

	public void Destroy()
	{
		if (attacker != null)
		{
			Enemy enemy = attacker as Enemy;
			if (enemy != null)
			{
				enemy.OnDestroyObstacle(this);
			}
		}
	}
}
