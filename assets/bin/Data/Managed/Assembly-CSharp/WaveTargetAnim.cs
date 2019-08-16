using UnityEngine;

public class WaveTargetAnim : MonoBehaviour
{
	public WaveTargetAnim()
		: this()
	{
	}

	private void PlayChangeEffect()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			string targetChangeAnimEffect = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam().targetChangeAnimEffect;
			if (!targetChangeAnimEffect.IsNullOrWhiteSpace())
			{
				EffectManager.OneShot(targetChangeAnimEffect, this.get_transform().get_position(), Quaternion.get_identity());
			}
		}
	}
}
