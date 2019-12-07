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

	public void RequestDestroy()
	{
		isRequestDelete = true;
	}

	public void Initialize(StageObject attacker, Transform parentTrans, AnimEventData.EventData data, AttackInfo atkInfo, AnimEventShot childEventShot)
	{
		bulletData = atkInfo.bulletData;
		if (!(bulletData == null) && bulletData.data != null)
		{
			this.attacker = attacker;
			this.parentTrans = parentTrans;
			_ = (attacker is Player);
			AttackHitInfo attackHitInfo = atkInfo as AttackHitInfo;
			if (attackHitInfo != null)
			{
				attackHitInfo.enableIdentityCheck = false;
			}
			atkInfoName = atkInfo.name;
			_transform = base.transform;
			_transform.parent = (MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform);
			_transform.position = parentTrans.position;
			switch (data.intArgs[0])
			{
			case 0:
				_transform.rotation = attacker._transform.rotation;
				break;
			case 1:
				_transform.rotation = parentTrans.rotation;
				break;
			case 2:
				_transform.rotation = Quaternion.identity;
				break;
			}
			defaultPos = _transform.position;
			childEventShot.transform.parent = _transform;
			bulletObj = childEventShot.gameObject;
			bulletTrans = bulletObj.transform;
			Vector3 localPosition = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			bulletTrans.localEulerAngles = new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
			bulletTrans.localPosition = localPosition;
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
				defaultOffsetPos = Vector3.zero;
				defaultOffsetRot = _transform.eulerAngles - parentTrans.eulerAngles;
			}
			else
			{
				defaultOffsetPos = Vector3.zero;
				defaultOffsetRot = Vector3.zero;
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
		if (!isInit)
		{
			return;
		}
		if (bulletObj == null)
		{
			Object.Destroy(base.gameObject);
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
		Vector3 position = _transform.position;
		Vector3 position2 = parentTrans.position;
		Vector3 eulerAngles = _transform.eulerAngles;
		Vector3 eulerAngles2 = parentTrans.eulerAngles;
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
				currentRotAngle += rotSpd * Time.deltaTime;
			}
			else
			{
				currentRotAngle = rotSpd * Time.deltaTime;
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
		_transform.eulerAngles = eulerAngles;
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
			currentMoveDis += moveSpd * Time.deltaTime;
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
			Vector3 zero = Vector3.zero;
			zero = ((moveDir != 0) ? (bulletTrans.right * currentMoveDis) : (bulletTrans.forward * currentMoveDis));
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
		_transform.position = position;
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
