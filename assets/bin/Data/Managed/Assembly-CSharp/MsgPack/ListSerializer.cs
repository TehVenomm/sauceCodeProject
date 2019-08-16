using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace MsgPack
{
	public class ListSerializer<T> : MessagePackSerializer<List<T>>
	{
		public ListSerializer(SerializationContext ownerContext)
			: base(ownerContext)
		{
		}

		protected override void PackToCore(Packer packer, List<T> objectTree)
		{
			MessagePackSerializer<T> serializer = base.get_OwnerContext().GetSerializer<T>();
			T[] array = objectTree.ToArray();
			packer.PackArrayHeader(array.Length);
			T[] array2 = array;
			foreach (T val in array2)
			{
				serializer.PackTo(packer, val);
			}
		}

		protected override List<T> UnpackFromCore(Unpacker unpacker)
		{
			MessagePackSerializer<T> serializer = base.get_OwnerContext().GetSerializer<T>();
			if (!unpacker.get_IsArrayHeader())
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			int itemsCount = UnpackHelpers.GetItemsCount(unpacker);
			List<T> list = new List<T>();
			if (!unpacker.get_IsArrayHeader())
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			for (int i = 0; i < itemsCount; i++)
			{
				if (!unpacker.Read())
				{
					throw SerializationExceptions.NewMissingItem(i);
				}
				T item;
				if (!unpacker.get_IsArrayHeader() && !unpacker.get_IsMapHeader())
				{
					item = serializer.UnpackFrom(unpacker);
				}
				else
				{
					Unpacker val = unpacker.ReadSubtree();
					try
					{
						item = serializer.UnpackFrom(val);
					}
					finally
					{
						((IDisposable)val)?.Dispose();
					}
				}
				list.Add(item);
			}
			return list;
		}
	}
}
