using MsgPack.Serialization;
using System;

namespace MsgPack
{
	public class ArraySerializer<T> : MessagePackSerializer<T[]>
	{
		public ArraySerializer(SerializationContext ownerContext)
			: base(ownerContext)
		{
		}

		protected override void PackToCore(Packer packer, T[] objectTree)
		{
			MessagePackSerializer<T> serializer = base.get_OwnerContext().GetSerializer<T>();
			packer.PackArrayHeader(objectTree.Length);
			foreach (T val in objectTree)
			{
				serializer.PackTo(packer, val);
			}
		}

		protected override T[] UnpackFromCore(Unpacker unpacker)
		{
			MessagePackSerializer<T> serializer = base.get_OwnerContext().GetSerializer<T>();
			if (!unpacker.get_IsArrayHeader())
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			int itemsCount = UnpackHelpers.GetItemsCount(unpacker);
			T[] array = new T[itemsCount];
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
				T val;
				if (!unpacker.get_IsArrayHeader() && !unpacker.get_IsMapHeader())
				{
					val = serializer.UnpackFrom(unpacker);
				}
				else
				{
					Unpacker val2 = unpacker.ReadSubtree();
					try
					{
						val = serializer.UnpackFrom(val2);
					}
					finally
					{
						((IDisposable)val2)?.Dispose();
					}
				}
				array[i] = val;
			}
			return array;
		}
	}
}
