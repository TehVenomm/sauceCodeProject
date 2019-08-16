package com.google.flatbuffers;

import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.CharBuffer;
import java.nio.charset.CharacterCodingException;
import java.nio.charset.Charset;
import java.nio.charset.CharsetDecoder;
import java.nio.charset.CoderResult;
import java.util.Arrays;
import java.util.Comparator;

public class Table {
    private static final ThreadLocal<CharBuffer> CHAR_BUFFER = new ThreadLocal<>();
    public static final ThreadLocal<Charset> UTF8_CHARSET = new ThreadLocal<Charset>() {
        /* access modifiers changed from: protected */
        public Charset initialValue() {
            return Charset.forName("UTF-8");
        }
    };
    private static final ThreadLocal<CharsetDecoder> UTF8_DECODER = new ThreadLocal<CharsetDecoder>() {
        /* access modifiers changed from: protected */
        public CharsetDecoder initialValue() {
            return Charset.forName("UTF-8").newDecoder();
        }
    };

    /* renamed from: bb */
    protected ByteBuffer f441bb;
    protected int bb_pos;

    protected static boolean __has_identifier(ByteBuffer byteBuffer, String str) {
        if (str.length() != 4) {
            throw new AssertionError("FlatBuffers: file identifier must be length 4");
        }
        for (int i = 0; i < 4; i++) {
            if (str.charAt(i) != ((char) byteBuffer.get(byteBuffer.position() + 4 + i))) {
                return false;
            }
        }
        return true;
    }

    protected static int __indirect(int i, ByteBuffer byteBuffer) {
        return byteBuffer.getInt(i) + i;
    }

    protected static int __offset(int i, int i2, ByteBuffer byteBuffer) {
        int capacity = byteBuffer.capacity() - i2;
        return capacity + byteBuffer.getShort((capacity + i) - byteBuffer.getInt(capacity));
    }

    protected static int compareStrings(int i, int i2, ByteBuffer byteBuffer) {
        int i3 = byteBuffer.getInt(i) + i;
        int i4 = byteBuffer.getInt(i2) + i2;
        int i5 = byteBuffer.getInt(i3);
        int i6 = byteBuffer.getInt(i4);
        int i7 = i3 + 4;
        int i8 = i4 + 4;
        int min = Math.min(i5, i6);
        for (int i9 = 0; i9 < min; i9++) {
            if (byteBuffer.get(i9 + i7) != byteBuffer.get(i9 + i8)) {
                return byteBuffer.get(i9 + i7) - byteBuffer.get(i9 + i8);
            }
        }
        return i5 - i6;
    }

    protected static int compareStrings(int i, byte[] bArr, ByteBuffer byteBuffer) {
        int i2 = byteBuffer.getInt(i) + i;
        int i3 = byteBuffer.getInt(i2);
        int length = bArr.length;
        int i4 = i2 + 4;
        int min = Math.min(i3, length);
        for (int i5 = 0; i5 < min; i5++) {
            if (byteBuffer.get(i5 + i4) != bArr[i5]) {
                return byteBuffer.get(i5 + i4) - bArr[i5];
            }
        }
        return i3 - length;
    }

    /* access modifiers changed from: protected */
    public int __indirect(int i) {
        return this.f441bb.getInt(i) + i;
    }

    /* access modifiers changed from: protected */
    public int __offset(int i) {
        int i2 = this.bb_pos - this.f441bb.getInt(this.bb_pos);
        if (i < this.f441bb.getShort(i2)) {
            return this.f441bb.getShort(i2 + i);
        }
        return 0;
    }

    public void __reset() {
        this.f441bb = null;
        this.bb_pos = 0;
    }

    /* access modifiers changed from: protected */
    public String __string(int i) {
        CharsetDecoder charsetDecoder = (CharsetDecoder) UTF8_DECODER.get();
        charsetDecoder.reset();
        int i2 = this.f441bb.getInt(i) + i;
        ByteBuffer order = this.f441bb.duplicate().order(ByteOrder.LITTLE_ENDIAN);
        int i3 = order.getInt(i2);
        order.position(i2 + 4);
        order.limit(i2 + 4 + i3);
        int maxCharsPerByte = (int) (((float) i3) * charsetDecoder.maxCharsPerByte());
        CharBuffer charBuffer = (CharBuffer) CHAR_BUFFER.get();
        if (charBuffer == null || charBuffer.capacity() < maxCharsPerByte) {
            charBuffer = CharBuffer.allocate(maxCharsPerByte);
            CHAR_BUFFER.set(charBuffer);
        }
        charBuffer.clear();
        try {
            CoderResult decode = charsetDecoder.decode(order, charBuffer, true);
            if (!decode.isUnderflow()) {
                decode.throwException();
            }
            return charBuffer.flip().toString();
        } catch (CharacterCodingException e) {
            throw new RuntimeException(e);
        }
    }

    /* access modifiers changed from: protected */
    public Table __union(Table table, int i) {
        int i2 = this.bb_pos + i;
        table.bb_pos = i2 + this.f441bb.getInt(i2);
        table.f441bb = this.f441bb;
        return table;
    }

    /* access modifiers changed from: protected */
    public int __vector(int i) {
        int i2 = this.bb_pos + i;
        return i2 + this.f441bb.getInt(i2) + 4;
    }

    /* access modifiers changed from: protected */
    public ByteBuffer __vector_as_bytebuffer(int i, int i2) {
        int __offset = __offset(i);
        if (__offset == 0) {
            return null;
        }
        ByteBuffer order = this.f441bb.duplicate().order(ByteOrder.LITTLE_ENDIAN);
        int __vector = __vector(__offset);
        order.position(__vector);
        order.limit((__vector_len(__offset) * i2) + __vector);
        return order;
    }

    /* access modifiers changed from: protected */
    public ByteBuffer __vector_in_bytebuffer(ByteBuffer byteBuffer, int i, int i2) {
        int __offset = __offset(i);
        if (__offset == 0) {
            return null;
        }
        int __vector = __vector(__offset);
        byteBuffer.rewind();
        byteBuffer.limit((__vector_len(__offset) * i2) + __vector);
        byteBuffer.position(__vector);
        return byteBuffer;
    }

    /* access modifiers changed from: protected */
    public int __vector_len(int i) {
        int i2 = this.bb_pos + i;
        return this.f441bb.getInt(i2 + this.f441bb.getInt(i2));
    }

    public ByteBuffer getByteBuffer() {
        return this.f441bb;
    }

    /* access modifiers changed from: protected */
    public int keysCompare(Integer num, Integer num2, ByteBuffer byteBuffer) {
        return 0;
    }

    /* access modifiers changed from: protected */
    public void sortTables(int[] iArr, final ByteBuffer byteBuffer) {
        Integer[] numArr = new Integer[iArr.length];
        for (int i = 0; i < iArr.length; i++) {
            numArr[i] = Integer.valueOf(iArr[i]);
        }
        Arrays.sort(numArr, new Comparator<Integer>() {
            public int compare(Integer num, Integer num2) {
                return Table.this.keysCompare(num, num2, byteBuffer);
            }
        });
        for (int i2 = 0; i2 < iArr.length; i2++) {
            iArr[i2] = numArr[i2].intValue();
        }
    }
}
