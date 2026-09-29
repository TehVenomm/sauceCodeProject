// Decompiled with JetBrains decompiler
// Type: MsgPack.QuaternionSerializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using MsgPack.Serialization;
using UnityEngine;

#nullable disable
namespace MsgPack;

public class QuaternionSerializer(SerializationContext ownerContext) : 
  MessagePackSerializer<Quaternion>(ownerContext)
{
  protected override void PackToCore(Packer packer, Quaternion objectTree)
  {
    packer.PackArrayHeader(4);
    packer.Pack(objectTree.x);
    packer.Pack(objectTree.y);
    packer.Pack(objectTree.z);
    packer.Pack(objectTree.w);
  }

  protected override Quaternion UnpackFromCore(Unpacker unpacker)
  {
    if (!unpacker.IsArrayHeader)
      throw SerializationExceptions.NewIsNotArrayHeader();
    if (UnpackHelpers.GetItemsCount(unpacker) != 4)
      throw SerializationExceptions.NewIsNotArrayHeader();
    if (!unpacker.IsArrayHeader)
      throw SerializationExceptions.NewIsNotArrayHeader();
    float num1;
    if (!unpacker.ReadSingle(ref num1))
      throw SerializationExceptions.NewMissingItem(0);
    float num2;
    if (!unpacker.ReadSingle(ref num2))
      throw SerializationExceptions.NewMissingItem(1);
    float num3;
    if (!unpacker.ReadSingle(ref num3))
      throw SerializationExceptions.NewMissingItem(2);
    float num4;
    if (!unpacker.ReadSingle(ref num4))
      throw SerializationExceptions.NewMissingItem(2);
    return new Quaternion(num1, num2, num3, num4);
  }
}
