using MsgPack.Serialization;
using System;
using System.IO;
using UnityEngine;

public class PacketTest
{
	private SerializationContext context = SerializationContext.Default;

	public PacketTest()
		: this()
	{
	}

	private void Awake()
	{
		CoopPacketMsgpackSerializer.RegisterOverrideCommon(context);
	}

	private void Start()
	{
		Execute();
	}

	private void Update()
	{
	}

	private void Log(string log)
	{
		Naka.Log(log);
	}

	private void LogError(string log)
	{
		Naka.LogError(log);
	}

	private void Execute()
	{
		try
		{
			Test1();
		}
		catch (Exception arg)
		{
			LogError("Test1 Error:" + arg);
		}
		try
		{
			Test2();
		}
		catch (Exception arg2)
		{
			LogError("Test2 Error:" + arg2);
		}
		try
		{
			Test3();
		}
		catch (Exception arg3)
		{
			LogError("Test3 Error:" + arg3);
		}
		try
		{
			Test4();
		}
		catch (Exception arg4)
		{
			LogError("Test4 Error:" + arg4);
		}
		for (int i = 0; i <= 261; i++)
		{
			Type modelType = ((PACKET_TYPE)i).GetModelType();
			try
			{
				MsgPack(modelType);
			}
			catch (Exception arg5)
			{
				Naka.LogError("MsgPack Error:" + arg5);
			}
			try
			{
				Json(modelType);
			}
			catch (Exception arg6)
			{
				Naka.LogError("Json Error:" + arg6);
			}
		}
	}

	private void Test1()
	{
	}

	private void Test2()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		MemoryStream memoryStream = new MemoryStream();
		Coop_Model_ObjectAttackedHitFix coop_Model_ObjectAttackedHitFix = new Coop_Model_ObjectAttackedHitFix();
		coop_Model_ObjectAttackedHitFix.hitPos.x = 1f;
		coop_Model_ObjectAttackedHitFix.hitPos.y = 2.3f;
		coop_Model_ObjectAttackedHitFix.hitPos.z = 3.45f;
		Log("before pos=" + coop_Model_ObjectAttackedHitFix.hitPos);
		Type typeFromHandle = typeof(Coop_Model_ObjectAttackedHitFix);
		IMessagePackSingleObjectSerializer serializer = context.GetSerializer(typeFromHandle);
		serializer.Pack(memoryStream, coop_Model_ObjectAttackedHitFix);
		memoryStream.Position = 0L;
		Coop_Model_ObjectAttackedHitFix coop_Model_ObjectAttackedHitFix2 = (Coop_Model_ObjectAttackedHitFix)serializer.Unpack(memoryStream);
		Log("after pos=" + coop_Model_ObjectAttackedHitFix2.hitPos);
		string str = JSONSerializer.Serialize(coop_Model_ObjectAttackedHitFix, typeFromHandle);
		Log("json stream:" + str);
	}

	private void Test3()
	{
	}

	private void Test4()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		MemoryStream memoryStream = new MemoryStream();
		Coop_Model_EnemyTargetShotEvent coop_Model_EnemyTargetShotEvent = new Coop_Model_EnemyTargetShotEvent();
		int[] array = new int[3]
		{
			1,
			12,
			123
		};
		int[] array2 = array;
		foreach (int num in array2)
		{
			Enemy.RandomShotInfo.TargetInfo targetInfo = new Enemy.RandomShotInfo.TargetInfo();
			targetInfo.rot = new Quaternion((float)num, (float)(num * 2), (float)(num * 3), (float)(num * 4));
			targetInfo.targetId = num;
			coop_Model_EnemyTargetShotEvent.targets.Add(targetInfo);
		}
		string log = string.Empty;
		log = string.Empty;
		coop_Model_EnemyTargetShotEvent.targets.ForEach(delegate(Enemy.RandomShotInfo.TargetInfo r)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			string text2 = log;
			log = text2 + "[" + r.rot + "," + r.targetId + "],";
		});
		Log("before target:" + log);
		Type typeFromHandle = typeof(Coop_Model_EnemyTargetShotEvent);
		IMessagePackSingleObjectSerializer serializer = context.GetSerializer(typeFromHandle);
		serializer.Pack(memoryStream, coop_Model_EnemyTargetShotEvent);
		memoryStream.Position = 0L;
		Coop_Model_EnemyTargetShotEvent coop_Model_EnemyTargetShotEvent2 = (Coop_Model_EnemyTargetShotEvent)serializer.Unpack(memoryStream);
		log = string.Empty;
		coop_Model_EnemyTargetShotEvent2.targets.ForEach(delegate(Enemy.RandomShotInfo.TargetInfo r)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			string text = log;
			log = text + "[" + r.rot + "," + r.targetId + "],";
		});
		Log("after target:" + log);
		string str = JSONSerializer.Serialize(coop_Model_EnemyTargetShotEvent, typeFromHandle);
		Log("json stream:" + str);
	}

	private void MsgPack(Type type)
	{
		MemoryStream memoryStream = new MemoryStream();
		object obj = Activator.CreateInstance(type);
		Log($"MsgPack:     {obj.GetType().FullName}\nValue:    {obj.ToString()}\nHashCode: {obj.GetHashCode()}\n");
		IMessagePackSingleObjectSerializer serializer = context.GetSerializer(type);
		serializer.Pack(memoryStream, obj);
		Log($"MsgPacked:     {obj.GetType().FullName}\nValue:    {obj.ToString()}\nHashCode: {obj.GetHashCode()}\n");
		memoryStream.Position = 0L;
		object obj2 = serializer.Unpack(memoryStream);
		Log($"MsgUnpack:     {obj2.GetType().FullName}\nValue:    {obj2.ToString()}\nHashCode: {obj2.GetHashCode()}\n");
	}

	private void Json(Type type)
	{
		object obj = Activator.CreateInstance(type);
		Log($"JsonPack:     {obj.GetType().FullName}\nValue:    {obj.ToString()}\nHashCode: {obj.GetHashCode()}\n");
		string text = JSONSerializer.Serialize(obj, type);
		Log($"JsonPacked:     {obj.GetType().FullName}\nValue:    {obj.ToString()}\nHashCode: {obj.GetHashCode()}\nStream: {text}");
		Coop_Model_Base coop_Model_Base = JSONSerializer.Deserialize<Coop_Model_Base>(text, type);
		Log($"JsonUnpack:     {coop_Model_Base.GetType().FullName}\nValue:    {coop_Model_Base.ToString()}\nHashCode: {coop_Model_Base.GetHashCode()}\n");
	}
}
