package com.google.firebase.messaging.cpp;

import com.google.flatbuffers.FlatBufferBuilder;
import com.google.flatbuffers.Table;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public final class SerializedEvent extends Table {
    public static void addEvent(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(1, i, 0);
    }

    public static void addEventType(FlatBufferBuilder flatBufferBuilder, byte b) {
        flatBufferBuilder.addByte(0, b, 0);
    }

    public static int createSerializedEvent(FlatBufferBuilder flatBufferBuilder, byte b, int i) {
        flatBufferBuilder.startObject(2);
        addEvent(flatBufferBuilder, i);
        addEventType(flatBufferBuilder, b);
        return endSerializedEvent(flatBufferBuilder);
    }

    public static int endSerializedEvent(FlatBufferBuilder flatBufferBuilder) {
        return flatBufferBuilder.endObject();
    }

    public static void finishSerializedEventBuffer(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.finish(i);
    }

    public static SerializedEvent getRootAsSerializedEvent(ByteBuffer byteBuffer) {
        return getRootAsSerializedEvent(byteBuffer, new SerializedEvent());
    }

    public static SerializedEvent getRootAsSerializedEvent(ByteBuffer byteBuffer, SerializedEvent serializedEvent) {
        byteBuffer.order(ByteOrder.LITTLE_ENDIAN);
        return serializedEvent.__assign(byteBuffer.getInt(byteBuffer.position()) + byteBuffer.position(), byteBuffer);
    }

    public static void startSerializedEvent(FlatBufferBuilder flatBufferBuilder) {
        flatBufferBuilder.startObject(2);
    }

    public SerializedEvent __assign(int i, ByteBuffer byteBuffer) {
        __init(i, byteBuffer);
        return this;
    }

    public void __init(int i, ByteBuffer byteBuffer) {
        this.bb_pos = i;
        this.bb = byteBuffer;
    }

    public Table event(Table table) {
        int __offset = __offset(6);
        return __offset != 0 ? __union(table, __offset) : null;
    }

    public byte eventType() {
        int __offset = __offset(4);
        return __offset != 0 ? this.bb.get(__offset + this.bb_pos) : (byte) 0;
    }
}
