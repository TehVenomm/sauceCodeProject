// Decompiled with JetBrains decompiler
// Type: MsgPack.Vector3Serializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using MsgPack.Serialization;
using UnityEngine;

#nullable disable
namespace MsgPack;

public class Vector3Serializer(SerializationContext ownerContext) : MessagePackSerializer<Vector3>(ownerContext)
{
  protected override void PackToCore(Packer packer, Vector3 objectTree)
  {
    packer.PackArrayHeader(3);
    packer.Pack(objectTree.x);
    packer.Pack(objectTree.y);
    packer.Pack(objectTree.z);
  }

  protected override Vector3 UnpackFromCore(Unpacker unpacker)
  {
    if (!unpacker.IsArrayHeader)
      throw SerializationExceptions.NewIsNotArrayHeader();
    if (UnpackHelpers.GetItemsCount(unpacker) != 3)
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
    return new Vector3(num1, num2, num3);
  }
}
