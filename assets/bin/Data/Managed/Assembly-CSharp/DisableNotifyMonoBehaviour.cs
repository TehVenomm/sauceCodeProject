using System.Collections.Generic;
using UnityEngine;

public class DisableNotifyMonoBehaviour : MonoBehaviour
{
	public Transform _transform
	{
		get;
		private set;
	}

	public DisableNotifyMonoBehaviour notifyMaster
	{
		get;
		private set;
	}

	public List<DisableNotifyMonoBehaviour> notifyServants
	{
		get;
		private set;
	}

	protected virtual void Awake()
	{
		_transform = base.transform;
	}

	protected virtual void OnDisable()
	{
		if (notifyServants != null)
		{
			notifyServants.ForEach(delegate(DisableNotifyMonoBehaviour o)
			{
				o.OnDisableMaster();
			});
			notifyServants.Clear();
			notifyServants = null;
		}
		ResetNotifyMaster();
	}

	private void OnApplicationQuit()
	{
		notifyMaster = null;
		if (notifyServants != null)
		{
			notifyServants.Clear();
			notifyServants = null;
		}
	}

	public virtual void SetNotifyMaster(DisableNotifyMonoBehaviour master)
	{
		ResetNotifyMaster();
		notifyMaster = master;
		if (master.notifyServants == null)
		{
			master.notifyServants = new List<DisableNotifyMonoBehaviour>();
		}
		master.notifyServants.Add(this);
		master.OnAttachServant(this);
	}

	public void ResetNotifyMaster()
	{
		if (notifyMaster != null)
		{
			if (notifyMaster.notifyServants != null)
			{
				notifyMaster.notifyServants.Remove(this);
			}
			notifyMaster.OnDetachServant(this);
			notifyMaster = null;
		}
	}

	protected virtual void OnDisableMaster()
	{
	}

	protected virtual void OnAttachServant(DisableNotifyMonoBehaviour servant)
	{
	}

	protected virtual void OnDetachServant(DisableNotifyMonoBehaviour servant)
	{
	}
}
