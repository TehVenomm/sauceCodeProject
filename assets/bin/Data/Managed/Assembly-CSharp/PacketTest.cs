// Decompiled with JetBrains decompiler
// Type: PacketTest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using MsgPack.Serialization;
using System;
using System.IO;
using UnityEngine;

#nullable disable
public class PacketTest : MonoBehaviour
{
  private SerializationContext context = SerializationContext.Default;

  private void Awake() => CoopPacketMsgpackSerializer.RegisterOverrideCommon(this.context);

  private void Start() => this.Execute();

  private void Update()
  {
  }

  private void Log(string log) => Naka.Log(log);

  private void LogError(string log) => Naka.LogError(log);

  private void Execute()
  {
    try
    {
      this.Test1();
    }
    catch (Exception ex)
    {
      this.LogError("Test1 Error:" + (object) ex);
    }
    try
    {
      this.Test2();
    }
    catch (Exception ex)
    {
      this.LogError("Test2 Error:" + (object) ex);
    }
    try
    {
      this.Test3();
    }
    catch (Exception ex)
    {
      this.LogError("Test3 Error:" + (object) ex);
    }
    try
    {
      this.Test4();
    }
    catch (Exception ex)
    {
      this.LogError("Test4 Error:" + (object) ex);
    }
    for (int type = 0; type <= 263; ++type)
    {
      System.Type modelType = ((PACKET_TYPE) type).GetModelType();
      try
      {
        this.MsgPack(modelType);
      }
      catch (Exception ex)
      {
        Naka.LogError("MsgPack Error:" + (object) ex);
      }
      try
      {
        this.Json(modelType);
      }
      catch (Exception ex)
      {
        Naka.LogError("Json Error:" + (object) ex);
      }
    }
  }

  private void Test1()
  {
  }

  private void Test2()
  {
    MemoryStream memoryStream = new MemoryStream();
    Coop_Model_ObjectAttackedHitFix message = new Coop_Model_ObjectAttackedHitFix();
    message.hitPos.x = 1f;
    message.hitPos.y = 2.3f;
    message.hitPos.z = 3.45f;
    this.Log("before pos=" + (object) message.hitPos);
    System.Type type = typeof (Coop_Model_ObjectAttackedHitFix);
    IMessagePackSingleObjectSerializer serializer = this.context.GetSerializer(type);
    MessagePackSerializerExtensions.Pack((IMessagePackSerializer) serializer, (Stream) memoryStream, (object) message);
    memoryStream.Position = 0L;
    this.Log("after pos=" + (object) ((Coop_Model_ObjectAttackedHitFix) MessagePackSerializerExtensions.Unpack((IMessagePackSerializer) serializer, (Stream) memoryStream)).hitPos);
    this.Log("json stream:" + JSONSerializer.Serialize((object) message, type));
  }

  private void Test3()
  {
  }

  private void Test4()
  {
    MemoryStream memoryStream = new MemoryStream();
    Coop_Model_EnemyTargetShotEvent message = new Coop_Model_EnemyTargetShotEvent();
    int[] numArray = new int[3]{ 1, 12, 123 };
    foreach (int num in numArray)
      message.targets.Add(new Enemy.RandomShotInfo.TargetInfo()
      {
        rot = new Quaternion((float) num, (float) (num * 2), (float) (num * 3), (float) (num * 4)),
        targetId = num
      });
    string log = "";
    log = string.Empty;
    message.targets.ForEach((Action<Enemy.RandomShotInfo.TargetInfo>) (r => log = $"{log}[{(object) r.rot},{(object) r.targetId}],"));
    this.Log("before target:" + log);
    System.Type type = typeof (Coop_Model_EnemyTargetShotEvent);
    IMessagePackSingleObjectSerializer serializer = this.context.GetSerializer(type);
    MessagePackSerializerExtensions.Pack((IMessagePackSerializer) serializer, (Stream) memoryStream, (object) message);
    memoryStream.Position = 0L;
    Coop_Model_EnemyTargetShotEvent enemyTargetShotEvent = (Coop_Model_EnemyTargetShotEvent) MessagePackSerializerExtensions.Unpack((IMessagePackSerializer) serializer, (Stream) memoryStream);
    log = string.Empty;
    enemyTargetShotEvent.targets.ForEach((Action<Enemy.RandomShotInfo.TargetInfo>) (r => log = $"{log}[{(object) r.rot},{(object) r.targetId}],"));
    this.Log("after target:" + log);
    this.Log("json stream:" + JSONSerializer.Serialize((object) message, type));
  }

  private void MsgPack(System.Type type)
  {
    MemoryStream memoryStream = new MemoryStream();
    object instance = Activator.CreateInstance(type);
    this.Log($"MsgPack:     {instance.GetType().FullName}\nValue:    {instance.ToString()}\nHashCode: {instance.GetHashCode()}\n");
    IMessagePackSingleObjectSerializer serializer = this.context.GetSerializer(type);
    MessagePackSerializerExtensions.Pack((IMessagePackSerializer) serializer, (Stream) memoryStream, instance);
    this.Log($"MsgPacked:     {instance.GetType().FullName}\nValue:    {instance.ToString()}\nHashCode: {instance.GetHashCode()}\n");
    memoryStream.Position = 0L;
    object obj = MessagePackSerializerExtensions.Unpack((IMessagePackSerializer) serializer, (Stream) memoryStream);
    this.Log($"MsgUnpack:     {obj.GetType().FullName}\nValue:    {obj.ToString()}\nHashCode: {obj.GetHashCode()}\n");
  }

  private void Json(System.Type type)
  {
    object instance = Activator.CreateInstance(type);
    this.Log($"JsonPack:     {instance.GetType().FullName}\nValue:    {instance.ToString()}\nHashCode: {instance.GetHashCode()}\n");
    string message = JSONSerializer.Serialize(instance, type);
    this.Log($"JsonPacked:     {instance.GetType().FullName}\nValue:    {instance.ToString()}\nHashCode: {instance.GetHashCode()}\nStream: {message}");
    Coop_Model_Base coopModelBase = JSONSerializer.Deserialize<Coop_Model_Base>(message, type);
    this.Log($"JsonUnpack:     {coopModelBase.GetType().FullName}\nValue:    {coopModelBase.ToString()}\nHashCode: {coopModelBase.GetHashCode()}\n");
  }
}
