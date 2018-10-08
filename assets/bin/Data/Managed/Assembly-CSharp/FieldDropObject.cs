using rhyme;
using System.Collections.Generic;
using UnityEngine;

public class FieldDropObject : IAnimEvent
{
	protected enum AnimationStep
	{
		NONE,
		MOVE_TO_TARGET_POS,
		DROP_TO_GROUND,
		OPEN,
		GET
	}

	private static int startAnimHash = -1;

	private static int endAnimHash = -1;

	private static int openAnimHash = -1;

	protected InGameSettingsManager.FieldDropItem parameter;

	protected List<UIDropAnnounce.DropAnnounceInfo> announceInfo = new List<UIDropAnnounce.DropAnnounceInfo>();

	protected List<InGameManager.DropItemInfo> itemInfo;

	protected List<InGameManager.DropDeliveryInfo> deliveryInfo;

	protected StageObject targetObject;

	protected FloatInterpolator distanceAnim = new FloatInterpolator();

	protected FloatInterpolator speedAnim = new FloatInterpolator();

	protected FloatInterpolator scaleAnim = new FloatInterpolator();

	protected AnimationStep animationStep;

	protected float moveTime;

	protected float distance;

	protected bool isGet;

	protected bool isDelete;

	protected bool isRare;

	protected AnimEventProcessor animEventProcessor;

	private bool isOpend;

	private Transform effect;

	private float animationTimer;

	private static readonly float MOVE_TO_TARGET_TIME = 0.5f;

	private Vector3 dropPos;

	private Vector3 targetPos;

	private Vector3 prefabDefaultScale = Vector3.get_one();

	public Transform _transform
	{
		get;
		protected set;
	}

	public Animator animator
	{
		get;
		protected set;
	}

	public int rewardId
	{
		get;
		protected set;
	}

	public TargetPoint targetPoint
	{
		get;
		set;
	}

	public FieldDropObject()
		: this()
	{
	}//IL_002d: Unknown result type (might be due to invalid IL or missing references)
	//IL_0032: Unknown result type (might be due to invalid IL or missing references)


	public static FieldDropObject Create(Coop_Model_EnemyDefeat model, List<InGameManager.DropDeliveryInfo> deliveryList, List<InGameManager.DropItemInfo> itemList)
	{
		if (itemList.Count <= 0 && deliveryList.Count <= 0)
		{
			return null;
		}
		UIDropAnnounce.COLOR color = GetColor(model, deliveryList);
		return CreateTreasureBox(model, deliveryList, itemList, color);
	}

	private static UIDropAnnounce.COLOR GetColor(Coop_Model_EnemyDefeat model, List<InGameManager.DropDeliveryInfo> deliveryList)
	{
		UIDropAnnounce.COLOR cOLOR = UIDropAnnounce.COLOR.NORMAL;
		if (!model.dropLoungeShare)
		{
			switch (model.boxType)
			{
			case 1:
				return UIDropAnnounce.COLOR.SP_N;
			case 2:
				return UIDropAnnounce.COLOR.SP_HN;
			case 3:
				return UIDropAnnounce.COLOR.SP_R;
			case 4:
				return UIDropAnnounce.COLOR.HALLOWEEN;
			default:
			{
				int i = 0;
				for (int count = model.dropIds.Count; i < count; i++)
				{
					if (cOLOR == UIDropAnnounce.COLOR.NORMAL)
					{
						switch (model.dropTypes[i])
						{
						case 5:
							cOLOR = UIDropAnnounce.COLOR.RARE;
							break;
						case 4:
						{
							EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)model.dropItemIds[i]);
							if (equipItemData != null && GameDefine.IsRare(equipItemData.rarity))
							{
								cOLOR = UIDropAnnounce.COLOR.RARE;
							}
							break;
						}
						default:
						{
							ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)model.dropItemIds[i]);
							if (itemData != null && GameDefine.IsRare(itemData.rarity))
							{
								cOLOR = UIDropAnnounce.COLOR.RARE;
							}
							break;
						}
						}
					}
				}
				if (deliveryList.Count > 0 && cOLOR == UIDropAnnounce.COLOR.NORMAL)
				{
					cOLOR = UIDropAnnounce.COLOR.DELIVERY;
				}
				return cOLOR;
			}
			}
		}
		return UIDropAnnounce.COLOR.LOUNGE;
	}

	public static FieldDropObject CreateTreasureBox(Coop_Model_EnemyDefeat model, List<InGameManager.DropDeliveryInfo> deliveryList, List<InGameManager.DropItemInfo> itemList, UIDropAnnounce.COLOR color)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = MonoBehaviourSingleton<InGameManager>.I.CreateTreasureBox(color);
		FieldDropObject fieldDropObject = val.GetComponent<FieldDropObject>();
		if (fieldDropObject == null)
		{
			fieldDropObject = val.AddComponent<FieldDropObject>();
		}
		fieldDropObject.itemInfo = itemList;
		fieldDropObject.deliveryInfo = deliveryList;
		fieldDropObject.rewardId = model.rewardId;
		fieldDropObject.isRare = (color == UIDropAnnounce.COLOR.RARE);
		Vector3 zero = Vector3.get_zero();
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			InGameSettingsManager.FieldDropItem fieldDrop = MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop;
			float value = Random.get_value();
			float value2 = Random.get_value();
			float value3 = Random.get_value();
			float num = (!(Random.get_value() > 0.5f)) ? 1f : (-1f);
			float num2 = (!(Random.get_value() > 0.5f)) ? 1f : (-1f);
			zero._002Ector(Mathf.Lerp(fieldDrop.offsetMin.x, fieldDrop.offsetMax.x, value) * num, Mathf.Lerp(fieldDrop.offsetMin.y, fieldDrop.offsetMax.y, value2), Mathf.Lerp(fieldDrop.offsetMin.z, fieldDrop.offsetMax.z, value3) * num2);
		}
		Vector3 val2 = default(Vector3);
		val2._002Ector((float)model.x, 0f, (float)model.z);
		Vector3 target = val2 + zero;
		int obstacleMask = AIUtility.GetObstacleMask();
		RaycastHit hit = default(RaycastHit);
		if (AIUtility.RaycastForTargetPos(val2, target, obstacleMask, out hit))
		{
			Vector3 point = hit.get_point();
			float x = point.x;
			float y = target.y;
			Vector3 point2 = hit.get_point();
			target._002Ector(x, y, point2.z);
		}
		fieldDropObject.Drop(val2, target);
		return fieldDropObject;
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		_transform = this.get_transform();
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop;
		animator = this.get_gameObject().GetComponentInChildren<Animator>();
		if (startAnimHash == -1)
		{
			startAnimHash = Animator.StringToHash("Base Layer.Pop");
		}
		if (endAnimHash == -1)
		{
			endAnimHash = Animator.StringToHash("Base Layer.Idle");
		}
		if (openAnimHash == -1)
		{
			openAnimHash = Animator.StringToHash("Base Layer.Open");
		}
		if (parameter.animEventData != null && animator != null)
		{
			animEventProcessor = new AnimEventProcessor(parameter.animEventData, animator, this);
		}
	}

	private void OnEnable()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (!isDelete)
		{
			prefabDefaultScale = _transform.get_localScale();
		}
		_transform.set_localScale(prefabDefaultScale);
		isDelete = false;
	}

	private void OnDisable()
	{
		if (isGet && MonoBehaviourSingleton<UIDropAnnounce>.IsValid())
		{
			int i = 0;
			for (int count = announceInfo.Count; i < count; i++)
			{
				MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(announceInfo[i]);
			}
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.DeleteDropObject(this);
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		if (targetObject.isInitialized && !isOpend && animationStep != AnimationStep.MOVE_TO_TARGET_POS && animationStep != AnimationStep.OPEN && IsSelfAttack(collider.get_gameObject()))
		{
			OpenDropObject();
		}
	}

	public bool IsSelfAttack(GameObject obj)
	{
		IAttackCollider component = obj.GetComponent<IAttackCollider>();
		if (object.ReferenceEquals(component, null))
		{
			return false;
		}
		if (component is HealAttackObject)
		{
			return false;
		}
		StageObject fromObject = component.GetFromObject();
		if (object.ReferenceEquals(fromObject, null))
		{
			return false;
		}
		return fromObject is Self;
	}

	public void OpenDropObject()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (!isOpend)
		{
			isOpend = true;
			SoundManager.PlayOneShotUISE(10000071);
			if (animEventProcessor != null)
			{
				animEventProcessor.CrossFade(openAnimHash, 0f);
			}
			effect = EffectManager.GetEffect("ef_btl_treasurebox_01", null);
			effect.set_position(_transform.get_position());
			rymFX component = effect.GetComponent<rymFX>();
			if (component != null)
			{
				component.AutoDelete = true;
				component.LoopEnd = true;
			}
			MonoBehaviourSingleton<CoopNetworkManager>.I.RewardGet(rewardId);
			if (deliveryInfo != null && deliveryInfo.Count > 0)
			{
				SoundManager.PlayOneShotUISE(40000155);
			}
			else if (isRare)
			{
				SoundManager.PlayOneShotUISE(40000154);
			}
			else
			{
				SoundManager.PlayOneShotUISE(10000064);
			}
			animationStep = AnimationStep.OPEN;
		}
	}

	private void LateUpdate()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		if (targetObject == null)
		{
			this.get_gameObject().SetActive(false);
		}
		else
		{
			if (animEventProcessor != null)
			{
				animEventProcessor.Update();
			}
			switch (animationStep)
			{
			case AnimationStep.MOVE_TO_TARGET_POS:
			{
				animationTimer += Time.get_deltaTime();
				Vector3 position2 = Vector3.Lerp(dropPos, targetPos, animationTimer / MOVE_TO_TARGET_TIME);
				float x = position2.x;
				Vector3 position3 = _transform.get_position();
				position2._002Ector(x, position3.y, position2.z);
				_transform.set_position(position2);
				if (animationTimer >= MOVE_TO_TARGET_TIME)
				{
					animationStep = AnimationStep.DROP_TO_GROUND;
				}
				break;
			}
			case AnimationStep.DROP_TO_GROUND:
			{
				AnimatorStateInfo currentAnimatorStateInfo2 = animator.GetCurrentAnimatorStateInfo(0);
				if (currentAnimatorStateInfo2.get_fullPathHash() == endAnimHash)
				{
					targetPoint = this.GetComponent<TargetPoint>();
					animationStep = AnimationStep.NONE;
				}
				if (isRare)
				{
					SoundManager.PlayOneShotUISE(10000061);
				}
				else
				{
					SoundManager.PlayOneShotUISE(10000062);
				}
				break;
			}
			case AnimationStep.OPEN:
			{
				AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
				if (currentAnimatorStateInfo.get_fullPathHash() == openAnimHash && currentAnimatorStateInfo.get_normalizedTime() > 0.99f)
				{
					animationStep = AnimationStep.NONE;
					if (effect != null)
					{
						EffectManager.ReleaseEffect(effect.get_gameObject(), true, false);
					}
					this.get_gameObject().SetActive(false);
				}
				break;
			}
			case AnimationStep.GET:
				if (distanceAnim.IsPlaying())
				{
					moveTime += Time.get_deltaTime();
					Bounds bounds = targetObject._collider.get_bounds();
					Vector3 center = bounds.get_center();
					Vector3 val = _transform.get_position() - center;
					float magnitude = val.get_magnitude();
					if (distance < magnitude)
					{
						distance = magnitude;
					}
					val = val.get_normalized() * distance * (1f - distanceAnim.Update());
					Vector3 val2 = Quaternion.AngleAxis(moveTime * speedAnim.Update(), Vector3.get_up()) * val;
					Vector3 position = center + val2;
					_transform.set_position(position);
					Vector3 localScale = Vector3.get_one() * scaleAnim.Update();
					if (distanceAnim.IsPlaying())
					{
						_transform.set_localScale(localScale);
					}
				}
				else
				{
					Transform val3 = EffectManager.GetEffect("ef_btl_mpdrop_01", null);
					val3.set_position(_transform.get_position());
					rymFX component = val3.GetComponent<rymFX>();
					if (component != null)
					{
						component.AutoDelete = true;
						component.LoopEnd = true;
					}
					this.get_gameObject().SetActive(false);
				}
				break;
			}
		}
	}

	public virtual void Drop(Vector3 _dropPos, Vector3 _targetPos)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		isOpend = false;
		animationTimer = 0f;
		animationStep = AnimationStep.MOVE_TO_TARGET_POS;
		targetObject = MonoBehaviourSingleton<StageObjectManager>.I.self;
		dropPos = _dropPos;
		_transform.set_position(_dropPos);
		targetPos = _targetPos;
		if (animEventProcessor != null)
		{
			animEventProcessor.CrossFade(startAnimHash, 0f);
		}
	}

	public void Delete(bool is_get)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		if (!isDelete)
		{
			isDelete = true;
			isGet = is_get;
			if (is_get)
			{
				targetObject = MonoBehaviourSingleton<StageObjectManager>.I.self;
				Bounds bounds = targetObject._collider.get_bounds();
				distance = Vector3.Distance(bounds.get_center(), _transform.get_position());
				distanceAnim.Set(parameter.getAnimTime, 0f, 1f, parameter.distanceAnim, 0f, null);
				distanceAnim.Play();
				speedAnim.Set(parameter.getAnimTime, 0f, parameter.rotateSpeed, parameter.rotateSpeedAnim, 0f, null);
				speedAnim.Play();
				scaleAnim.Set(parameter.getAnimTime, 0f, 1f, parameter.scaleAnim, 0f, null);
				scaleAnim.Play();
				moveTime = 0f;
				animationStep = AnimationStep.OPEN;
				announceInfo = MonoBehaviourSingleton<InGameManager>.I.CreateDropAnnounceInfoList(deliveryInfo, itemInfo, true);
			}
			else
			{
				this.get_gameObject().SetActive(false);
			}
		}
	}

	public void OnAnimEvent(AnimEventData.EventData data)
	{
		AnimEventFormat.ID id = data.id;
		if (id == AnimEventFormat.ID.SE_ONESHOT)
		{
			int num = data.intArgs[0];
			if (num != 0)
			{
				SoundManager.PlayOneShotUISE(num);
			}
		}
	}
}
