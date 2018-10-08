package com.google.firebase.messaging.cpp;

import com.google.flatbuffers.FlatBufferBuilder;
import com.google.flatbuffers.Table;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public final class SerializedNotification extends Table {
    public static void addBadge(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(4, i, 0);
    }

    public static void addBody(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(1, i, 0);
    }

    public static void addBodyLocArgs(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(9, i, 0);
    }

    public static void addBodyLocKey(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(8, i, 0);
    }

    public static void addClickAction(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(7, i, 0);
    }

    public static void addColor(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(6, i, 0);
    }

    public static void addIcon(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(2, i, 0);
    }

    public static void addSound(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(3, i, 0);
    }

    public static void addTag(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(5, i, 0);
    }

    public static void addTitle(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(0, i, 0);
    }

    public static void addTitleLocArgs(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(11, i, 0);
    }

    public static void addTitleLocKey(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.addOffset(10, i, 0);
    }

    public static int createBodyLocArgsVector(FlatBufferBuilder flatBufferBuilder, int[] iArr) {
        flatBufferBuilder.startVector(4, iArr.length, 4);
        for (int length = iArr.length - 1; length >= 0; length--) {
            flatBufferBuilder.addOffset(iArr[length]);
        }
        return flatBufferBuilder.endVector();
    }

    public static int createSerializedNotification(FlatBufferBuilder flatBufferBuilder, int i, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10, int i11, int i12) {
        flatBufferBuilder.startObject(12);
        addTitleLocArgs(flatBufferBuilder, i12);
        addTitleLocKey(flatBufferBuilder, i11);
        addBodyLocArgs(flatBufferBuilder, i10);
        addBodyLocKey(flatBufferBuilder, i9);
        addClickAction(flatBufferBuilder, i8);
        addColor(flatBufferBuilder, i7);
        addTag(flatBufferBuilder, i6);
        addBadge(flatBufferBuilder, i5);
        addSound(flatBufferBuilder, i4);
        addIcon(flatBufferBuilder, i3);
        addBody(flatBufferBuilder, i2);
        addTitle(flatBufferBuilder, i);
        return endSerializedNotification(flatBufferBuilder);
    }

    public static int createTitleLocArgsVector(FlatBufferBuilder flatBufferBuilder, int[] iArr) {
        flatBufferBuilder.startVector(4, iArr.length, 4);
        for (int length = iArr.length - 1; length >= 0; length--) {
            flatBufferBuilder.addOffset(iArr[length]);
        }
        return flatBufferBuilder.endVector();
    }

    public static int endSerializedNotification(FlatBufferBuilder flatBufferBuilder) {
        return flatBufferBuilder.endObject();
    }

    public static SerializedNotification getRootAsSerializedNotification(ByteBuffer byteBuffer) {
        return getRootAsSerializedNotification(byteBuffer, new SerializedNotification());
    }

    public static SerializedNotification getRootAsSerializedNotification(ByteBuffer byteBuffer, SerializedNotification serializedNotification) {
        byteBuffer.order(ByteOrder.LITTLE_ENDIAN);
        return serializedNotification.__assign(byteBuffer.getInt(byteBuffer.position()) + byteBuffer.position(), byteBuffer);
    }

    public static void startBodyLocArgsVector(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.startVector(4, i, 4);
    }

    public static void startSerializedNotification(FlatBufferBuilder flatBufferBuilder) {
        flatBufferBuilder.startObject(12);
    }

    public static void startTitleLocArgsVector(FlatBufferBuilder flatBufferBuilder, int i) {
        flatBufferBuilder.startVector(4, i, 4);
    }

    public SerializedNotification __assign(int i, ByteBuffer byteBuffer) {
        __init(i, byteBuffer);
        return this;
    }

    public void __init(int i, ByteBuffer byteBuffer) {
        this.bb_pos = i;
        this.bb = byteBuffer;
    }

    public String badge() {
        int __offset = __offset(12);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer badgeAsByteBuffer() {
        return __vector_as_bytebuffer(12, 1);
    }

    public String body() {
        int __offset = __offset(6);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer bodyAsByteBuffer() {
        return __vector_as_bytebuffer(6, 1);
    }

    public String bodyLocArgs(int i) {
        int __offset = __offset(22);
        return __offset != 0 ? __string(__vector(__offset) + (i * 4)) : null;
    }

    public int bodyLocArgsLength() {
        int __offset = __offset(22);
        return __offset != 0 ? __vector_len(__offset) : 0;
    }

    public String bodyLocKey() {
        int __offset = __offset(20);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer bodyLocKeyAsByteBuffer() {
        return __vector_as_bytebuffer(20, 1);
    }

    public String clickAction() {
        int __offset = __offset(18);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer clickActionAsByteBuffer() {
        return __vector_as_bytebuffer(18, 1);
    }

    public String color() {
        int __offset = __offset(16);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer colorAsByteBuffer() {
        return __vector_as_bytebuffer(16, 1);
    }

    public String icon() {
        int __offset = __offset(8);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer iconAsByteBuffer() {
        return __vector_as_bytebuffer(8, 1);
    }

    public String sound() {
        int __offset = __offset(10);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer soundAsByteBuffer() {
        return __vector_as_bytebuffer(10, 1);
    }

    public String tag() {
        int __offset = __offset(14);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer tagAsByteBuffer() {
        return __vector_as_bytebuffer(14, 1);
    }

    public String title() {
        int __offset = __offset(4);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer titleAsByteBuffer() {
        return __vector_as_bytebuffer(4, 1);
    }

    public String titleLocArgs(int i) {
        int __offset = __offset(26);
        return __offset != 0 ? __string(__vector(__offset) + (i * 4)) : null;
    }

    public int titleLocArgsLength() {
        int __offset = __offset(26);
        return __offset != 0 ? __vector_len(__offset) : 0;
    }

    public String titleLocKey() {
        int __offset = __offset(24);
        return __offset != 0 ? __string(__offset + this.bb_pos) : null;
    }

    public ByteBuffer titleLocKeyAsByteBuffer() {
        return __vector_as_bytebuffer(24, 1);
    }
}
