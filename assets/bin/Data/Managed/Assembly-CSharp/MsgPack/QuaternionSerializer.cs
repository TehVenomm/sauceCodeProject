using MsgPack.Serialization;
using UnityEngine;

namespace MsgPack
{
	public class QuaternionSerializer
	{
		public QuaternionSerializer(SerializationContext ownerContext)
			: base(ownerContext)
		{
		}

		protected override void PackToCore(Packer packer, Quaternion objectTree)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			packer.PackArrayHeader(4);
			packer.Pack(objectTree.x);
			packer.Pack(objectTree.y);
			packer.Pack(objectTree.z);
			packer.Pack(objectTree.w);
		}

		protected override Quaternion UnpackFromCore(Unpacker unpacker)
		{
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			if (!unpacker.get_IsArrayHeader())
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			int itemsCount = UnpackHelpers.GetItemsCount(unpacker);
			if (itemsCount != 4)
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			if (!unpacker.get_IsArrayHeader())
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			float num = default(float);
			if (!unpacker.ReadSingle(ref num))
			{
				throw SerializationExceptions.NewMissingItem(0);
			}
			float num2 = default(float);
			if (!unpacker.ReadSingle(ref num2))
			{
				throw SerializationExceptions.NewMissingItem(1);
			}
			float num3 = default(float);
			if (!unpacker.ReadSingle(ref num3))
			{
				throw SerializationExceptions.NewMissingItem(2);
			}
			float num4 = default(float);
			if (!unpacker.ReadSingle(ref num4))
			{
				throw SerializationExceptions.NewMissingItem(2);
			}
			return new Quaternion(num, num2, num3, num4);
		}
	}
}
