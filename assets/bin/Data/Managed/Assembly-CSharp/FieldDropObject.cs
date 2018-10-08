using rhyme;
using System.Collections.Generic;
using UnityEngine;

public class FieldDropObject : MonoBehaviour, IAnimEvent
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

	private Vector3 prefabDefaultScale = Vector3.one;

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
			case 5:
				return UIDropAnnounce.COLOR.ESP_N;
			case 6:
				return UIDropAnnounce.COLOR.ESP_HN;
			case 7:
				return UIDropAnnounce.COLOR.ESP_R;
			case 8:
				return UIDropAnnounce.COLOR.SEASONAL;
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
						case 14:
						{
							AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData((uint)model.dropItemIds[i]);
							if (data != null && GameDefine.IsRare(data.rarity))
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
		GameObject gameObject = MonoBehaviourSingleton<InGameManager>.I.CreateTreasureBox(color);
		FieldDropObject fieldDropObject = gameObject.GetComponent<FieldDropObject>();
		if ((Object)fieldDropObject == (Object)null)
		{
			fieldDropObject = gameObject.AddComponent<FieldDropObject>();
		}
		fieldDropObject.itemInfo = itemList;
		fieldDropObject.deliveryInfo = deliveryList;
		fieldDropObject.rewardId = model.rewardId;
		fieldDropObject.isRare = (color == UIDropAnnounce.COLOR.RARE);
		Vector3 b = Vector3.zero;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			InGameSettingsManager.FieldDropItem fieldDrop = MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop;
			float value = Random.value;
			float value2 = Random.value;
			float value3 = Random.value;
			float num = (!(Random.value > 0.5f)) ? 1f : (-1f);
			float num2 = (!(Random.value > 0.5f)) ? 1f : (-1f);
			b = new Vector3(Mathf.Lerp(fieldDrop.offsetMin.x, fieldDrop.offsetMax.x, value) * num, Mathf.Lerp(fieldDrop.offsetMin.y, fieldDrop.offsetMax.y, value2), Mathf.Lerp(fieldDrop.offsetMin.z, fieldDrop.offsetMax.z, value3) * num2);
		}
		Vector3 vector = new Vector3((float)model.x, 0f, (float)model.z);
		Vector3 target = vector + b;
		int obstacleMask = AIUtility.GetObstacleMask();
		RaycastHit hit = default(RaycastHit);
		if (AIUtility.RaycastForTargetPos(vector, target, obstacleMask, out hit))
		{
			Vector3 point = hit.point;
			float x = point.x;
			float y = target.y;
			Vector3 point2 = hit.point;
			target = new Vector3(x, y, point2.z);
		}
		fieldDropObject.Drop(vector, target);
		return fieldDropObject;
	}

	private void Awake()
	{
		_transform = base.transform;
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop;
		animator = base.gameObject.GetComponentInChildren<Animator>();
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
		if ((Object)parameter.animEventData != (Object)null && (Object)animator != (Object)null)
		{
			animEventProcessor = new AnimEventProcessor(parameter.animEventData, animator, this);
		}
	}

	private void OnEnable()
	{
		if (!isDelete)
		{
			prefabDefaultScale = _transform.localScale;
		}
		_transform.localScale = prefabDefaultScale;
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
		if (targetObject.isInitialized && !isOpend && animationStep != AnimationStep.MOVE_TO_TARGET_POS && animationStep != AnimationStep.OPEN && IsSelfAttack(collider.gameObject))
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
		if (!isOpend)
		{
			isOpend = true;
			SoundManager.PlayOneShotUISE(10000071);
			if (animEventProcessor != null)
			{
				animEventProcessor.CrossFade(openAnimHash, 0f);
			}
			effect = EffectManager.GetEffect("ef_btl_treasurebox_01", null);
			effect.position = _transform.position;
			rymFX component = effect.GetComponent<rymFX>();
			if ((Object)component != (Object)null)
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
		if ((Object)targetObject == (Object)null)
		{
			base.gameObject.SetActive(false);
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
				animationTimer += Time.deltaTime;
				Vector3 position2 = Vector3.Lerp(dropPos, targetPos, animationTimer / MOVE_TO_TARGET_TIME);
				float x = position2.x;
				Vector3 position3 = _transform.position;
				position2 = new Vector3(x, position3.y, position2.z);
				_transform.position = position2;
				if (animationTimer >= MOVE_TO_TARGET_TIME)
				{
					animationStep = AnimationStep.DROP_TO_GROUND;
				}
				break;
			}
			case AnimationStep.DROP_TO_GROUND:
				if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == endAnimHash)
				{
					targetPoint = GetComponent<TargetPoint>();
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
			case AnimationStep.OPEN:
			{
				AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
				if (currentAnimatorStateInfo.fullPathHash == openAnimHash && currentAnimatorStateInfo.normalizedTime > 0.99f)
				{
					animationStep = AnimationStep.NONE;
					if ((Object)effect != (Object)null)
					{
						EffectManager.ReleaseEffect(effect.gameObject, true, false);
					}
					base.gameObject.SetActive(false);
				}
				break;
			}
			case AnimationStep.GET:
				if (distanceAnim.IsPlaying())
				{
					moveTime += Time.deltaTime;
					Vector3 center = targetObject._collider.bounds.center;
					Vector3 point = _transform.position - center;
					float magnitude = point.magnitude;
					if (distance < magnitude)
					{
						distance = magnitude;
					}
					point = point.normalized * distance * (1f - distanceAnim.Update());
					Vector3 b = Quaternion.AngleAxis(moveTime * speedAnim.Update(), Vector3.up) * point;
					Vector3 position = center + b;
					_transform.position = position;
					Vector3 localScale = Vector3.one * scaleAnim.Update();
					if (distanceAnim.IsPlaying())
					{
						_transform.localScale = localScale;
					}
				}
				else
				{
					Transform transform = EffectManager.GetEffect("ef_btl_mpdrop_01", null);
					transform.position = _transform.position;
					rymFX component = transform.GetComponent<rymFX>();
					if ((Object)component != (Object)null)
					{
						component.AutoDelete = true;
						component.LoopEnd = true;
					}
					base.gameObject.SetActive(false);
				}
				break;
			}
		}
	}

	public virtual void Drop(Vector3 _dropPos, Vector3 _targetPos)
	{
		isOpend = false;
		animationTimer = 0f;
		animationStep = AnimationStep.MOVE_TO_TARGET_POS;
		targetObject = MonoBehaviourSingleton<StageObjectManager>.I.self;
		dropPos = _dropPos;
		_transform.position = _dropPos;
		targetPos = _targetPos;
		if (animEventProcessor != null)
		{
			animEventProcessor.CrossFade(startAnimHash, 0f);
		}
	}

	public void Delete(bool is_get)
	{
		if (!isDelete)
		{
			isDelete = true;
			isGet = is_get;
			if (is_get)
			{
				targetObject = MonoBehaviourSingleton<StageObjectManager>.I.self;
				distance = Vector3.Distance(targetObject._collider.bounds.center, _transform.position);
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
				base.gameObject.SetActive(false);
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
