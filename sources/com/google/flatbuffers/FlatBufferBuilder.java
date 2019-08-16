package com.google.flatbuffers;

import java.io.IOException;
import java.io.InputStream;
import java.nio.BufferUnderflowException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.CharBuffer;
import java.nio.charset.CharacterCodingException;
import java.nio.charset.Charset;
import java.nio.charset.CharsetEncoder;
import java.nio.charset.CoderResult;
import java.util.Arrays;

public class FlatBufferBuilder {
    static final /* synthetic */ boolean $assertionsDisabled = (!FlatBufferBuilder.class.desiredAssertionStatus());
    static final Charset utf8charset = Charset.forName("UTF-8");

    /* renamed from: bb */
    ByteBuffer f439bb;
    ByteBufferFactory bb_factory;
    ByteBuffer dst;
    CharsetEncoder encoder;
    boolean finished;
    boolean force_defaults;
    int minalign;
    boolean nested;
    int num_vtables;
    int object_start;
    int space;
    int vector_num_elems;
    int[] vtable;
    int vtable_in_use;
    int[] vtables;

    static class ByteBufferBackedInputStream extends InputStream {
        ByteBuffer buf;

        public ByteBufferBackedInputStream(ByteBuffer byteBuffer) {
            this.buf = byteBuffer;
        }

        public int read() throws IOException {
            try {
                return this.buf.get() & 255;
            } catch (BufferUnderflowException e) {
                return -1;
            }
        }
    }

    public static abstract class ByteBufferFactory {
        public abstract ByteBuffer newByteBuffer(int i);

        public void releaseByteBuffer(ByteBuffer byteBuffer) {
        }
    }

    public static final class HeapByteBufferFactory extends ByteBufferFactory {
        public ByteBuffer newByteBuffer(int i) {
            return ByteBuffer.allocate(i).order(ByteOrder.LITTLE_ENDIAN);
        }
    }

    public FlatBufferBuilder() {
        this(1024);
    }

    public FlatBufferBuilder(int i) {
        this(i, (ByteBufferFactory) new HeapByteBufferFactory());
    }

    public FlatBufferBuilder(int i, ByteBufferFactory byteBufferFactory) {
        this.minalign = 1;
        this.vtable = null;
        this.vtable_in_use = 0;
        this.nested = false;
        this.finished = false;
        this.vtables = new int[16];
        this.num_vtables = 0;
        this.vector_num_elems = 0;
        this.force_defaults = false;
        this.encoder = utf8charset.newEncoder();
        if (i <= 0) {
            i = 1;
        }
        this.space = i;
        this.bb_factory = byteBufferFactory;
        this.f439bb = byteBufferFactory.newByteBuffer(i);
    }

    public FlatBufferBuilder(ByteBuffer byteBuffer) {
        this.minalign = 1;
        this.vtable = null;
        this.vtable_in_use = 0;
        this.nested = false;
        this.finished = false;
        this.vtables = new int[16];
        this.num_vtables = 0;
        this.vector_num_elems = 0;
        this.force_defaults = false;
        this.encoder = utf8charset.newEncoder();
        init(byteBuffer, new HeapByteBufferFactory());
    }

    public FlatBufferBuilder(ByteBuffer byteBuffer, ByteBufferFactory byteBufferFactory) {
        this.minalign = 1;
        this.vtable = null;
        this.vtable_in_use = 0;
        this.nested = false;
        this.finished = false;
        this.vtables = new int[16];
        this.num_vtables = 0;
        this.vector_num_elems = 0;
        this.force_defaults = false;
        this.encoder = utf8charset.newEncoder();
        init(byteBuffer, byteBufferFactory);
    }

    @Deprecated
    private int dataStart() {
        finished();
        return this.space;
    }

    static ByteBuffer growByteBuffer(ByteBuffer byteBuffer, ByteBufferFactory byteBufferFactory) {
        int capacity = byteBuffer.capacity();
        if ((-1073741824 & capacity) != 0) {
            throw new AssertionError("FlatBuffers: cannot grow buffer beyond 2 gigabytes.");
        }
        int i = capacity == 0 ? 1 : capacity << 1;
        byteBuffer.position(0);
        ByteBuffer newByteBuffer = byteBufferFactory.newByteBuffer(i);
        newByteBuffer.position(i - capacity);
        newByteBuffer.put(byteBuffer);
        return newByteBuffer;
    }

    public void Nested(int i) {
        if (i != offset()) {
            throw new AssertionError("FlatBuffers: struct must be serialized inline.");
        }
    }

    public void addBoolean(int i, boolean z, boolean z2) {
        if (this.force_defaults || z != z2) {
            addBoolean(z);
            slot(i);
        }
    }

    public void addBoolean(boolean z) {
        prep(1, 0);
        putBoolean(z);
    }

    public void addByte(byte b) {
        prep(1, 0);
        putByte(b);
    }

    public void addByte(int i, byte b, int i2) {
        if (this.force_defaults || b != i2) {
            addByte(b);
            slot(i);
        }
    }

    public void addDouble(double d) {
        prep(8, 0);
        putDouble(d);
    }

    public void addDouble(int i, double d, double d2) {
        if (this.force_defaults || d != d2) {
            addDouble(d);
            slot(i);
        }
    }

    public void addFloat(float f) {
        prep(4, 0);
        putFloat(f);
    }

    public void addFloat(int i, float f, double d) {
        if (this.force_defaults || ((double) f) != d) {
            addFloat(f);
            slot(i);
        }
    }

    public void addInt(int i) {
        prep(4, 0);
        putInt(i);
    }

    public void addInt(int i, int i2, int i3) {
        if (this.force_defaults || i2 != i3) {
            addInt(i2);
            slot(i);
        }
    }

    public void addLong(int i, long j, long j2) {
        if (this.force_defaults || j != j2) {
            addLong(j);
            slot(i);
        }
    }

    public void addLong(long j) {
        prep(8, 0);
        putLong(j);
    }

    public void addOffset(int i) {
        prep(4, 0);
        if ($assertionsDisabled || i <= offset()) {
            putInt((offset() - i) + 4);
            return;
        }
        throw new AssertionError();
    }

    public void addOffset(int i, int i2, int i3) {
        if (this.force_defaults || i2 != i3) {
            addOffset(i2);
            slot(i);
        }
    }

    public void addShort(int i, short s, int i2) {
        if (this.force_defaults || s != i2) {
            addShort(s);
            slot(i);
        }
    }

    public void addShort(short s) {
        prep(2, 0);
        putShort(s);
    }

    public void addStruct(int i, int i2, int i3) {
        if (i2 != i3) {
            Nested(i2);
            slot(i);
        }
    }

    public void clear() {
        this.space = this.f439bb.capacity();
        this.f439bb.clear();
        this.minalign = 1;
        while (this.vtable_in_use > 0) {
            int[] iArr = this.vtable;
            int i = this.vtable_in_use - 1;
            this.vtable_in_use = i;
            iArr[i] = 0;
        }
        this.vtable_in_use = 0;
        this.nested = false;
        this.finished = false;
        this.object_start = 0;
        this.num_vtables = 0;
        this.vector_num_elems = 0;
    }

    public int createByteVector(byte[] bArr) {
        int length = bArr.length;
        startVector(1, length, 1);
        ByteBuffer byteBuffer = this.f439bb;
        int i = this.space - length;
        this.space = i;
        byteBuffer.position(i);
        this.f439bb.put(bArr);
        return endVector();
    }

    public <T extends Table> int createSortedVectorOfTables(T t, int[] iArr) {
        t.sortTables(iArr, this.f439bb);
        return createVectorOfTables(iArr);
    }

    public int createString(CharSequence charSequence) {
        int length = (int) (((float) charSequence.length()) * this.encoder.maxBytesPerChar());
        if (this.dst == null || this.dst.capacity() < length) {
            this.dst = ByteBuffer.allocate(Math.max(128, length));
        }
        this.dst.clear();
        CoderResult encode = this.encoder.encode(charSequence instanceof CharBuffer ? (CharBuffer) charSequence : CharBuffer.wrap(charSequence), this.dst, true);
        if (encode.isError()) {
            try {
                encode.throwException();
            } catch (CharacterCodingException e) {
                throw new Error(e);
            }
        }
        this.dst.flip();
        return createString(this.dst);
    }

    public int createString(ByteBuffer byteBuffer) {
        int remaining = byteBuffer.remaining();
        addByte(0);
        startVector(1, remaining, 1);
        ByteBuffer byteBuffer2 = this.f439bb;
        int i = this.space - remaining;
        this.space = i;
        byteBuffer2.position(i);
        this.f439bb.put(byteBuffer);
        return endVector();
    }

    public ByteBuffer createUnintializedVector(int i, int i2, int i3) {
        int i4 = i * i2;
        startVector(i, i2, i3);
        ByteBuffer byteBuffer = this.f439bb;
        int i5 = this.space - i4;
        this.space = i5;
        byteBuffer.position(i5);
        ByteBuffer order = this.f439bb.slice().order(ByteOrder.LITTLE_ENDIAN);
        order.limit(i4);
        return order;
    }

    public int createVectorOfTables(int[] iArr) {
        notNested();
        startVector(4, iArr.length, 4);
        for (int length = iArr.length - 1; length >= 0; length--) {
            addOffset(iArr[length]);
        }
        return endVector();
    }

    public ByteBuffer dataBuffer() {
        finished();
        return this.f439bb;
    }

    public int endObject() {
        int i;
        if (this.vtable == null || !this.nested) {
            throw new AssertionError("FlatBuffers: endObject called without startObject");
        }
        addInt(0);
        int offset = offset();
        int i2 = this.vtable_in_use - 1;
        while (i2 >= 0 && this.vtable[i2] == 0) {
            i2--;
        }
        for (int i3 = i2; i3 >= 0; i3--) {
            addShort((short) (this.vtable[i3] != 0 ? offset - this.vtable[i3] : 0));
        }
        addShort((short) (offset - this.object_start));
        addShort((short) ((i2 + 1 + 2) * 2));
        int i4 = 0;
        loop2:
        while (true) {
            if (i4 >= this.num_vtables) {
                i = 0;
                break;
            }
            int capacity = this.f439bb.capacity() - this.vtables[i4];
            int i5 = this.space;
            short s = this.f439bb.getShort(capacity);
            if (s == this.f439bb.getShort(i5)) {
                int i6 = 2;
                while (i6 < s) {
                    if (this.f439bb.getShort(capacity + i6) == this.f439bb.getShort(i5 + i6)) {
                        i6 += 2;
                    }
                }
                i = this.vtables[i4];
                break loop2;
            }
            i4++;
        }
        if (i != 0) {
            this.space = this.f439bb.capacity() - offset;
            this.f439bb.putInt(this.space, i - offset);
        } else {
            if (this.num_vtables == this.vtables.length) {
                this.vtables = Arrays.copyOf(this.vtables, this.num_vtables * 2);
            }
            int[] iArr = this.vtables;
            int i7 = this.num_vtables;
            this.num_vtables = i7 + 1;
            iArr[i7] = offset();
            this.f439bb.putInt(this.f439bb.capacity() - offset, offset() - offset);
        }
        this.nested = false;
        return offset;
    }

    public int endVector() {
        if (!this.nested) {
            throw new AssertionError("FlatBuffers: endVector called without startVector");
        }
        this.nested = false;
        putInt(this.vector_num_elems);
        return offset();
    }

    public void finish(int i) {
        finish(i, false);
    }

    public void finish(int i, String str) {
        finish(i, str, false);
    }

    /* access modifiers changed from: protected */
    public void finish(int i, String str, boolean z) {
        prep(this.minalign, (z ? 4 : 0) + 8);
        if (str.length() != 4) {
            throw new AssertionError("FlatBuffers: file identifier must be length 4");
        }
        for (int i2 = 3; i2 >= 0; i2--) {
            addByte((byte) str.charAt(i2));
        }
        finish(i, z);
    }

    /* access modifiers changed from: protected */
    public void finish(int i, boolean z) {
        prep(this.minalign, (z ? 4 : 0) + 4);
        addOffset(i);
        if (z) {
            addInt(this.f439bb.capacity() - this.space);
        }
        this.f439bb.position(this.space);
        this.finished = true;
    }

    public void finishSizePrefixed(int i) {
        finish(i, true);
    }

    public void finishSizePrefixed(int i, String str) {
        finish(i, str, true);
    }

    public void finished() {
        if (!this.finished) {
            throw new AssertionError("FlatBuffers: you can only access the serialized buffer after it has been finished by FlatBufferBuilder.finish().");
        }
    }

    public FlatBufferBuilder forceDefaults(boolean z) {
        this.force_defaults = z;
        return this;
    }

    public FlatBufferBuilder init(ByteBuffer byteBuffer, ByteBufferFactory byteBufferFactory) {
        this.bb_factory = byteBufferFactory;
        this.f439bb = byteBuffer;
        this.f439bb.clear();
        this.f439bb.order(ByteOrder.LITTLE_ENDIAN);
        this.minalign = 1;
        this.space = this.f439bb.capacity();
        this.vtable_in_use = 0;
        this.nested = false;
        this.finished = false;
        this.object_start = 0;
        this.num_vtables = 0;
        this.vector_num_elems = 0;
        return this;
    }

    public void notNested() {
        if (this.nested) {
            throw new AssertionError("FlatBuffers: object serialization must not be nested.");
        }
    }

    public int offset() {
        return this.f439bb.capacity() - this.space;
    }

    public void pad(int i) {
        for (int i2 = 0; i2 < i; i2++) {
            ByteBuffer byteBuffer = this.f439bb;
            int i3 = this.space - 1;
            this.space = i3;
            byteBuffer.put(i3, 0);
        }
    }

    public void prep(int i, int i2) {
        if (i > this.minalign) {
            this.minalign = i;
        }
        int capacity = ((((this.f439bb.capacity() - this.space) + i2) ^ -1) + 1) & (i - 1);
        while (this.space < capacity + i + i2) {
            int capacity2 = this.f439bb.capacity();
            ByteBuffer byteBuffer = this.f439bb;
            this.f439bb = growByteBuffer(byteBuffer, this.bb_factory);
            if (byteBuffer != this.f439bb) {
                this.bb_factory.releaseByteBuffer(byteBuffer);
            }
            this.space = (this.f439bb.capacity() - capacity2) + this.space;
        }
        pad(capacity);
    }

    public void putBoolean(boolean z) {
        ByteBuffer byteBuffer = this.f439bb;
        int i = this.space - 1;
        this.space = i;
        byteBuffer.put(i, (byte) (z ? 1 : 0));
    }

    public void putByte(byte b) {
        ByteBuffer byteBuffer = this.f439bb;
        int i = this.space - 1;
        this.space = i;
        byteBuffer.put(i, b);
    }

    public void putDouble(double d) {
        ByteBuffer byteBuffer = this.f439bb;
        int i = this.space - 8;
        this.space = i;
        byteBuffer.putDouble(i, d);
    }

    public void putFloat(float f) {
        ByteBuffer byteBuffer = this.f439bb;
        int i = this.space - 4;
        this.space = i;
        byteBuffer.putFloat(i, f);
    }

    public void putInt(int i) {
        ByteBuffer byteBuffer = this.f439bb;
        int i2 = this.space - 4;
        this.space = i2;
        byteBuffer.putInt(i2, i);
    }

    public void putLong(long j) {
        ByteBuffer byteBuffer = this.f439bb;
        int i = this.space - 8;
        this.space = i;
        byteBuffer.putLong(i, j);
    }

    public void putShort(short s) {
        ByteBuffer byteBuffer = this.f439bb;
        int i = this.space - 2;
        this.space = i;
        byteBuffer.putShort(i, s);
    }

    public void required(int i, int i2) {
        int capacity = this.f439bb.capacity() - i;
        if (!(this.f439bb.getShort((capacity - this.f439bb.getInt(capacity)) + i2) != 0)) {
            throw new AssertionError("FlatBuffers: field " + i2 + " must be set");
        }
    }

    public byte[] sizedByteArray() {
        return sizedByteArray(this.space, this.f439bb.capacity() - this.space);
    }

    public byte[] sizedByteArray(int i, int i2) {
        finished();
        byte[] bArr = new byte[i2];
        this.f439bb.position(i);
        this.f439bb.get(bArr);
        return bArr;
    }

    public InputStream sizedInputStream() {
        finished();
        ByteBuffer duplicate = this.f439bb.duplicate();
        duplicate.position(this.space);
        duplicate.limit(this.f439bb.capacity());
        return new ByteBufferBackedInputStream(duplicate);
    }

    public void slot(int i) {
        this.vtable[i] = offset();
    }

    public void startObject(int i) {
        notNested();
        if (this.vtable == null || this.vtable.length < i) {
            this.vtable = new int[i];
        }
        this.vtable_in_use = i;
        Arrays.fill(this.vtable, 0, this.vtable_in_use, 0);
        this.nested = true;
        this.object_start = offset();
    }

    public void startVector(int i, int i2, int i3) {
        notNested();
        this.vector_num_elems = i2;
        prep(4, i * i2);
        prep(i3, i * i2);
        this.nested = true;
    }
}
