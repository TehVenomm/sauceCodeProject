package com.google.firebase.messaging.cpp;

import com.google.flatbuffers.FlatBufferBuilder;
import com.google.flatbuffers.Table;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public final class SerializedMessage extends Table {
    public static void addCollapseKey(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(2, i, 0);
    }

    public static void addData(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(3, i, 0);
    }

    public static void addError(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(9, i, 0);
    }

    public static void addErrorDescription(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(10, i, 0);
    }

    public static void addFrom(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(0, i, 0);
    }

    public static void addMessageId(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(5, i, 0);
    }

    public static void addMessageType(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(6, i, 0);
    }

    public static void addNotification(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(11, i, 0);
    }

    public static void addNotificationOpened(FlatBufferBuilder flatBufferBuilder, boolean z) {
        flatBufferBuilder.addBoolean(12, z, false);
    }

    public static void addPriority(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(7, i, 0);
    }

    public static void addRawData(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(4, i, 0);
    }

    public static void addTimeToLive(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addInt(8, i, 0);
    }

    public static void addTo(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(1, i, 0);
    }

    public static int createDataVector(FlatBufferBuilder flatBufferBuilder, int[] iArr) {
        flatBufferBuilder.startVector(4, iArr.length, 4);
        for (int length = iArr.length - 1; length >= 0; length--) {
            flatBufferBuilder.addOffset(iArr[length]);
        }
        return flatBufferBuilder.endVector();
    }

    public static int createSerializedMessage(FlatBufferBuilder flatBufferBuilder, int i, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10, int i11, int i12, boolean z) {
        flatBufferBuilder.startObject(13);
        addNotification(flatBufferBuilder, i12);
        addErrorDescription(flatBufferBuilder, i11);
        addError(flatBufferBuilder, i10);
        addTimeToLive(flatBufferBuilder, i9);
        addPriority(flatBufferBuilder, i8);
        addMessageType(flatBufferBuilder, i7);
        addMessageId(flatBufferBuilder, i6);
        addRawData(flatBufferBuilder, i5);
        addData(flatBufferBuilder, i4);
        addCollapseKey(flatBufferBuilder, i3);
        addTo(flatBufferBuilder, i2);
        addFrom(flatBufferBuilder, i);
        addNotificationOpened(flatBufferBuilder, z);
        return endSerializedMessage(flatBufferBuilder);
    }

    public static int endSerializedMessage(FlatBufferBuilder flatBufferBuilder) {
        return flatBufferBuilder.endObject();
    }

    public static SerializedMessage getRootAsSerializedMessage(ByteBuffer byteBuffer) {
        return getRootAsSerializedMessage(byteBuffer, new SerializedMessage());
    }

    public static SerializedMessage getRootAsSerializedMessage(ByteBuffer byteBuffer, SerializedMessage serializedMessage) {
        byteBuffer.order(ByteOrder.LITTLE_ENDIAN);
        return serializedMessage.__assign(byteBuffer.getInt(byteBuffer.position()) + byteBuffer.position(), byteBuffer);
    }

    public static void startDataVector(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.startVector(4, i, 4);
    }

    public static void startSerializedMessage(FlatBufferBuilder flatBufferBuilder) {
        flatBufferBuilder.startObject(13);
    }

    public SerializedMessage __assign(int i, ByteBuffer byteBuffer) {
        __init(i, byteBuffer);
        return this;
    }

    public void __init(int i, ByteBuffer byteBuffer) {
        this.bb_pos = i;
        this.bb = byteBuffer;
    }

    public String collapseKey() {
        int __offset = __offset(8);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer collapseKeyAsByteBuffer() {
        return __vector_as_bytebuffer(8, 1);
    }

    public DataPair data(int i) {
        return data(new DataPair(), i);
    }

    public DataPair data(DataPair dataPair, int i) {
        int __offset = __offset(10);
        return __offset != 0 ? dataPair.__assign(__indirect(__vector(__offset) + (i * 4)), this.bb) : null;
    }

    public int dataLength() {
        int __offset = __offset(10);
        return __offset != 0 ? __vector_len(__offset) : 0;
    }

    public String error() {
        int __offset = __offset(22);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer errorAsByteBuffer() {
        return __vector_as_bytebuffer(22, 1);
    }

    public String errorDescription() {
        int __offset = __offset(24);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer errorDescriptionAsByteBuffer() {
        return __vector_as_bytebuffer(24, 1);
    }

    public String from() {
        int __offset = __offset(4);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer fromAsByteBuffer() {
        return __vector_as_bytebuffer(4, 1);
    }

    public String messageId() {
        int __offset = __offset(14);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer messageIdAsByteBuffer() {
        return __vector_as_bytebuffer(14, 1);
    }

    public String messageType() {
        int __offset = __offset(16);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer messageTypeAsByteBuffer() {
        return __vector_as_bytebuffer(16, 1);
    }

    public SerializedNotification notification() {
        return notification(new SerializedNotification());
    }

    public SerializedNotification notification(SerializedNotification serializedNotification) {
        int __offset = __offset(26);
        return __offset != 0 ? serializedNotification.__assign(__indirect(__offset + this.bb_pos), this.bb) : null;
    }

    public boolean notificationOpened() {
        int __offset = __offset(28);
        return (__offset == 0 || this.bb.get(__offset + this.bb_pos) == (byte) 0) ? false : true;
    }

    public String priority() {
        int __offset = __offset(18);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer priorityAsByteBuffer() {
        return __vector_as_bytebuffer(18, 1);
    }

    public String rawData() {
        int __offset = __offset(12);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer rawDataAsByteBuffer() {
        return __vector_as_bytebuffer(12, 1);
    }

    public int timeToLive() {
        int __offset = __offset(20);
        return __offset != 0 ? this.bb.getInt(__offset + this.bb_pos) : 0;
    }

    public String to() {
        int __offset = __offset(6);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer toAsByteBuffer() {
        return __vector_as_bytebuffer(6, 1);
    }
}
