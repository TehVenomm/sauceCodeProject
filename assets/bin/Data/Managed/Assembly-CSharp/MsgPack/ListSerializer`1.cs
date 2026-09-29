// Decompiled with JetBrains decompiler
// Type: MsgPack.ListSerializer`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using MsgPack.Serialization;
using System.Collections.Generic;

#nullable disable
namespace MsgPack;

public class ListSerializer<T>(SerializationContext ownerContext) : MessagePackSerializer<List<T>>(ownerContext)
{
  protected override void PackToCore(Packer packer, List<T> objectTree)
  {
    MessagePackSerializer<T> serializer = this.OwnerContext.GetSerializer<T>();
    T[] array = objectTree.ToArray();
    packer.PackArrayHeader(array.Length);
    foreach (T obj in array)
      serializer.PackTo(packer, obj);
  }

  protected override List<T> UnpackFromCore(Unpacker unpacker)
  {
    MessagePackSerializer<T> serializer = this.OwnerContext.GetSerializer<T>();
    int num = unpacker.IsArrayHeader ? UnpackHelpers.GetItemsCount(unpacker) : throw SerializationExceptions.NewIsNotArrayHeader();
    List<T> objList = new List<T>();
    if (!unpacker.IsArrayHeader)
      throw SerializationExceptions.NewIsNotArrayHeader();
    for (int index = 0; index < num; ++index)
    {
      if (!unpacker.Read())
        throw SerializationExceptions.NewMissingItem(index);
      T obj;
      if (!unpacker.IsArrayHeader && !unpacker.IsMapHeader)
      {
        obj = serializer.UnpackFrom(unpacker);
      }
      else
      {
        using (Unpacker unpacker1 = unpacker.ReadSubtree())
          obj = serializer.UnpackFrom(unpacker1);
      }
      objList.Add(obj);
    }
    return objList;
  }
}
