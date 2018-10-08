using UnityEngine;

public class WaveTargetAnim : MonoBehaviour
{
	private void PlayChangeEffect()
	{
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			string targetChangeAnimEffect = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam().targetChangeAnimEffect;
			if (!targetChangeAnimEffect.IsNullOrWhiteSpace())
			{
				EffectManager.OneShot(targetChangeAnimEffect, base.transform.position, Quaternion.identity, false);
			}
		}
	}
}
