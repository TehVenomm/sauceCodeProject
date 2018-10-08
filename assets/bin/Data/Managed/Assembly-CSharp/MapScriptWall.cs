using System.Collections.Generic;
using UnityEngine;

public class MapScriptWall
{
	[Tooltip("壁半径")]
	public float wallRadius = 40f;

	public MapScriptWall()
		: this()
	{
	}

	private void LateUpdate()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Vector3 zero = Vector3.get_zero();
			List<StageObject> characterList = MonoBehaviourSingleton<StageObjectManager>.I.characterList;
			List<StageObject>.Enumerator enumerator = characterList.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Vector3 val = enumerator.Current._transform.get_position() - zero;
				if (val.get_magnitude() > wallRadius)
				{
					enumerator.Current._transform.set_position(zero + val.get_normalized() * wallRadius);
				}
			}
		}
	}
}
