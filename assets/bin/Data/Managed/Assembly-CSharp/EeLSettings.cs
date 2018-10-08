using rhyme;
using System;
using System.IO;
using System.Text;
using UnityEngine;

public static class EeLSettings
{
	public unsafe static void Startup()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		rymFXManager.ResourceLoadDelegate = new ResourceLoadFunc((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		rymFXManager.PlaySoundDelegate = new SoundFunc((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		rymFXManager.InitFxDelegate = new InitFxFunc((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		rymFXManager.QueryDestroyFxDelegate = new QueryDestroyFxFunc((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		rymFXManager.MeshBounds = new Bounds(Vector3.get_zero(), new Vector3(100000f, 100000f, 100000f));
		rymFXManager.EnableLog = false;
		rymFXManager.GetShaderDelegate = new GetShaderFunc((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		rymFXTrail.ComplementMode = 0;
		rymFXManager.ClearPoolObjects();
		rymTPool<rymXorShift>.Precreate(32);
		rymTPool<EmitParam>.Precreate(16);
		rymTPool<PtclWorkBlock>.Precreate(32);
		rymTPool<rymList<PtclWorkBlock>>.Precreate(32);
		rymTPool<PtclWork>.Precreate(512);
		rymTPool<WorkAccess>.Precreate(8);
		rymTPool<ApplyParam>.Precreate(8);
		rymTPool<rymFXParticle2ChildPlug>.Precreate(32);
		rymTPool<rymFXTrailPoint>.Precreate(64);
		rymTPool<rymList<rymFXTrailPoint>>.Precreate(32);
		rymTPool<rymFXTrailParam>.Precreate(8);
		rymTPool<rymFX4KeyAnimValue>.Precreate(32);
		rymTPool<Param>.Precreate(128);
		rymTPool<rymMemReader>.Precreate(1);
		rymTPool<StringBuilder>.Precreate(1);
		rymTPool<rymList<string>>.Precreate(16);
		rymTPool<rymList<float>>.Precreate(512);
		rymTPool<rymList<int>>.Precreate(16);
		rymTPool<rymFXWorkFixPos>.Precreate(8);
		rymTPool<rymFXWorkFixRot>.Precreate(8);
		rymTPool<rymFXWorkFixScale>.Precreate(8);
		rymTPool<rymFXWorkFixColor>.Precreate(8);
		rymTPool<rymFXWorkLinearPos>.Precreate(8);
		rymTPool<rymFXWorkLinearRot>.Precreate(8);
		rymTPool<rymFXWorkLinearScale>.Precreate(8);
		rymTPool<rymFXWorkLinearColor>.Precreate(8);
		rymTPool<rymList<rymFXWork>>.Precreate(32);
		rymTPool<rymFXSpeedForce>.Precreate(8);
		rymTPool<rymFXAccelForce>.Precreate(8);
		rymTPool<rymFXShakeForce>.Precreate(8);
		rymTPool<rymFXScrewForce>.Precreate(8);
		rymTPool<rymFXAbsorbForce>.Precreate(8);
		rymTPool<rymList<rymFXParticleForceBase>>.Precreate(32);
		rymTPool<rymFXSoundTrigger>.Precreate(8);
		rymTPool<rymList<rymFXTrigger>>.Precreate(8);
		rymTPool<rymFXPlug>.Precreate(32);
		rymTPool<rymList<rymFXPlug>>.Precreate(16);
		rymTPool<rymList<int>>.Precreate(16);
		rymTPool<rymFXSoundInfo>.Precreate(4);
		rymTPool<rymList<rymFXSoundInfo>>.Precreate(4);
		rymTPool<rymFXParticle2>.Precreate(16);
		rymTPool<rymFXSprite>.Precreate(16);
		rymTPool<rymFXTrail>.Precreate(8);
		rymTPool<rymList<rymFXObject>>.Precreate(16);
		rymTPool<rymFXParticle2ChildWork>.Precreate(16);
		rymTPool<rymFXParticle2ChildParam>.Precreate(16);
		rymTPool<rymList<rymFXObjectBase>>.Precreate(32);
		rymTPool<Param>.Precreate(16);
		object[] array = new object[96];
		int i = 0;
		int num = 0;
		for (; i < 32; i++)
		{
			rymList<Vector2> val = rymTPool<rymList<Vector2>>.Get();
			val.set_Capacity(400);
			rymList<Vector3> val2 = rymTPool<rymList<Vector3>>.Get();
			val2.set_Capacity(400);
			rymList<Color> val3 = rymTPool<rymList<Color>>.Get();
			val3.set_Capacity(400);
			array[num++] = val;
			array[num++] = val2;
			array[num++] = val3;
		}
		int j = 0;
		int num5 = 0;
		for (; j < 32; j++)
		{
			rymList<Vector2> val4 = array[num5++] as rymList<Vector2>;
			rymList<Vector3> val5 = array[num5++] as rymList<Vector3>;
			rymList<Color> val6 = array[num5++] as rymList<Color>;
			rymTPool<rymList<Vector2>>.Release(ref val4);
			rymTPool<rymList<Vector3>>.Release(ref val5);
			rymTPool<rymList<Color>>.Release(ref val6);
		}
		rymTPool<rymList<int>>.poolCountLimit = 32;
		rymFXManager.EnableMaterialCache = false;
	}

	private unsafe static void OnResourceLoad(ResourceLoadWork work)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		if (!(((IntPtr)(void*)work).fx == null))
		{
			ResourceLink component = ((IntPtr)(void*)work).fx.GetComponent<ResourceLink>();
			if (component != null)
			{
				rymList<string> textureNameList = ((IntPtr)(void*)work).fx.GetTextureNameList();
				if (textureNameList != null)
				{
					int i = 0;
					for (int size = textureNameList.size; i < size; i++)
					{
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(textureNameList.get_Item(i));
						if (!string.IsNullOrEmpty(fileNameWithoutExtension))
						{
							((IntPtr)(void*)work).textures[i] = component.Get<Texture>(fileNameWithoutExtension);
						}
					}
				}
				else
				{
					Debug.LogWarning((object)((IntPtr)(void*)work).fx.get_name());
				}
			}
			((IntPtr)(void*)work).fx.ResourceLoadComplete();
		}
	}

	private static void PlaySound(rymFX fx, rymFXSoundInfo info)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		if (fx.get_enabled() && (!info.loop || !(info.audio_source != null)) && !string.IsNullOrEmpty(info.clip_name))
		{
			ResourceLink component = fx.GetComponent<ResourceLink>();
			if (component != null)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(info.clip_name);
				AudioClip val = component.Get<AudioClip>(fileNameWithoutExtension);
				if (val != null)
				{
					AudioObject audioObject = SoundManager.PlaySE(val, info.loop, fx.get__transform());
					if (audioObject != null && info.loop)
					{
						EffectInfoComponent component2 = fx.GetComponent<EffectInfoComponent>();
						if (component2 != null)
						{
							component2.SetLoopAudioObject(audioObject);
						}
					}
				}
				else
				{
					Log.Error(LOG.RESOURCE, "{0} is not found. ({1})", fileNameWithoutExtension, fx.get_name());
				}
			}
		}
	}

	private static void InitFx(rymFX fx, bool binary)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (fx.get_gameObject().get_layer() != 5)
		{
			SceneSettingsManager.ApplyEffect(fx, false);
		}
	}

	private static Shader GetShader(string name)
	{
		return ResourceUtility.FindShader(name);
	}

	private static bool OnQueryDestroyFx(rymFX fx)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && MonoBehaviourSingleton<EffectManager>.I.StockOrDestroy(fx.get_gameObject(), false))
		{
			return false;
		}
		return true;
	}
}
