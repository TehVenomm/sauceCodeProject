using MsgPack.Serialization;
using UnityEngine;

namespace MsgPack
{
	public class QuaternionSerializer : MessagePackSerializer<Quaternion>
	{
		public QuaternionSerializer(SerializationContext ownerContext)
			: base(ownerContext)
		{
		}

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
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			if (UnpackHelpers.GetItemsCount(unpacker) != 4)
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			if (!unpacker.IsArrayHeader)
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			if (!unpacker.ReadSingle(out float result))
			{
				throw SerializationExceptions.NewMissingItem(0);
			}
			if (!unpacker.ReadSingle(out float result2))
			{
				throw SerializationExceptions.NewMissingItem(1);
			}
			if (!unpacker.ReadSingle(out float result3))
			{
				throw SerializationExceptions.NewMissingItem(2);
			}
			if (!unpacker.ReadSingle(out float result4))
			{
				throw SerializationExceptions.NewMissingItem(2);
			}
			return new Quaternion(result, result2, result3, result4);
		}
	}
}
