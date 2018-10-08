using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
	public BrainParam param = new BrainParam();

	public Transform _frontTransform;

	public Transform _backTransform;

	public bool canCheckAvoidAttack;

	public bool canAvoidAttack;

	public Character owner
	{
		get;
		private set;
	}

	public bool isInitialized
	{
		get;
		private set;
	}

	public OpponentMemory opponentMem
	{
		get;
		private set;
	}

	public TargetController targetCtrl
	{
		get;
		private set;
	}

	public MoveController moveCtrl
	{
		get;
		private set;
	}

	public WeaponController weaponCtrl
	{
		get;
		private set;
	}

	public StateMachine fsm
	{
		get;
		protected set;
	}

	public Goal_Think think
	{
		get;
		protected set;
	}

	public DangerRader dangerRader
	{
		get;
		protected set;
	}

	protected SpanTimer opponentMemSpanTimer
	{
		get;
		set;
	}

	protected SpanTimer targetUpdateSpanTimer
	{
		get;
		set;
	}

	public float rootInternalRedius
	{
		get;
		private set;
	}

	public float rootFrontDistance
	{
		get;
		private set;
	}

	public float rootBackDistance
	{
		get;
		private set;
	}

	public Vector2 frontPositionXZ
	{
		get
		{
			Vector3 position = _frontTransform.position;
			return new Vector2(position.x, position.z);
		}
	}

	public Vector2 frontForwardXZ
	{
		get
		{
			Vector3 forward = _frontTransform.forward;
			return new Vector2(forward.x, forward.z);
		}
	}

	public Vector2 backPositionXZ
	{
		get
		{
			Vector3 position = _backTransform.position;
			return new Vector2(position.x, position.z);
		}
	}

	public Vector2 backForwardXZ
	{
		get
		{
			Vector3 forward = _backTransform.forward;
			return new Vector2(forward.x, forward.z);
		}
	}

	public bool isNonActive => fsm != null && fsm.currentType == STATE_TYPE.NONACTIVE;

	protected virtual void Awake()
	{
		owner = GetComponentInParent<Character>();
		isInitialized = false;
	}

	protected virtual void OnEnable()
	{
		Initialize();
	}

	protected virtual void Start()
	{
	}

	protected virtual void Update()
	{
		if (!((Object)owner == (Object)null) && !owner.isDead && isInitialized)
		{
			if (opponentMemSpanTimer != null && opponentMemSpanTimer.IsReady())
			{
				opponentMem.Update();
			}
			if (targetUpdateSpanTimer != null && targetUpdateSpanTimer.IsReady())
			{
				targetCtrl.UpdateTarget();
			}
			if (fsm != null)
			{
				fsm.Update();
			}
			if (think != null)
			{
				think.Update(this);
			}
		}
	}

	protected virtual void OnDestroy()
	{
		if (think != null)
		{
			Goal.Free(think);
		}
	}

	public void Initialize()
	{
		if (!((Object)owner == (Object)null) && owner.isInitialized && !isInitialized)
		{
			OnInitialize();
			isInitialized = true;
		}
	}

	protected virtual void OnInitialize()
	{
		opponentMem = new OpponentMemory(this);
		targetCtrl = new TargetController(this);
		moveCtrl = new MoveController(this);
		weaponCtrl = new WeaponController(this);
		opponentMemSpanTimer = new SpanTimer(param.thinkParam.opponentMemorySpan);
		targetUpdateSpanTimer = new SpanTimer(param.thinkParam.targetUpdateSpan);
		_frontTransform = GetFront();
		_backTransform = GetBack();
		rootInternalRedius = param.sensorParam.internalRadius * GetScale();
		rootFrontDistance = (frontPositionXZ - owner.positionXZ).magnitude - rootInternalRedius;
		rootBackDistance = (backPositionXZ - owner.positionXZ).magnitude - rootInternalRedius;
	}

	public void ResetInitialized()
	{
		OnInitialize();
	}

	public virtual float GetScale()
	{
		return 1f;
	}

	public virtual Transform GetFront()
	{
		return owner._transform;
	}

	public virtual Transform GetBack()
	{
		return owner._transform;
	}

	public virtual List<StageObject> GetTargetObjectList()
	{
		return (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? new List<StageObject>() : MonoBehaviourSingleton<StageObjectManager>.I.objectList;
	}

	public virtual List<StageObject> GetAllyObjectList()
	{
		return (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? new List<StageObject>() : MonoBehaviourSingleton<StageObjectManager>.I.objectList;
	}

	public virtual void HandleEvent(BRAIN_EVENT ev, object param = null)
	{
		if (ev == BRAIN_EVENT.DESTROY_OBJECT)
		{
			StageObject stageObject = (StageObject)param;
			if (opponentMem != null)
			{
				opponentMem.Remove(stageObject);
			}
			if (targetCtrl != null && (Object)targetCtrl.GetCurrentTarget() == (Object)stageObject)
			{
				targetCtrl.MissCurrentTarget();
			}
			if (targetCtrl != null && (Object)targetCtrl.GetAllyTarget() == (Object)stageObject)
			{
				targetCtrl.SetAllyTarget(null);
			}
			if (targetUpdateSpanTimer != null)
			{
				targetUpdateSpanTimer.SetTempSpan(0.5f);
			}
		}
		if (fsm != null)
		{
			fsm.HandleEvent(ev, param);
		}
		if (think != null)
		{
			think.HandleEvent(this, ev, param);
		}
	}
}
