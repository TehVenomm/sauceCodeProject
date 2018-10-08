using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Toggled Components")]
[ExecuteInEditMode]
[RequireComponent(typeof(UIToggle))]
public class UIToggledComponents
{
	public List<MonoBehaviour> activate;

	public List<MonoBehaviour> deactivate;

	[SerializeField]
	[HideInInspector]
	private MonoBehaviour target;

	[SerializeField]
	[HideInInspector]
	private bool inverse;

	public UIToggledComponents()
		: this()
	{
	}

	private void Awake()
	{
		if (target != null)
		{
			if (activate.Count == 0 && deactivate.Count == 0)
			{
				if (inverse)
				{
					deactivate.Add(target);
				}
				else
				{
					activate.Add(target);
				}
			}
			else
			{
				target = null;
			}
		}
		UIToggle component = this.GetComponent<UIToggle>();
		EventDelegate.Add(component.onChange, Toggle);
	}

	public void Toggle()
	{
		if (this.get_enabled())
		{
			for (int i = 0; i < activate.Count; i++)
			{
				MonoBehaviour val = activate[i];
				val.set_enabled(UIToggle.current.value);
			}
			for (int j = 0; j < deactivate.Count; j++)
			{
				MonoBehaviour val2 = deactivate[j];
				val2.set_enabled(!UIToggle.current.value);
			}
		}
	}
}
