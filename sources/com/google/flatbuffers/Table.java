package com.google.flatbuffers;

import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.CharBuffer;
import java.nio.charset.Charset;
import java.nio.charset.CharsetDecoder;
import java.nio.charset.CoderResult;
import java.util.Arrays;
import java.util.Comparator;

public class Table {
    private static final ThreadLocal<CharBuffer> CHAR_BUFFER = new ThreadLocal();
    public static final ThreadLocal<Charset> UTF8_CHARSET = new C06532();
    private static final ThreadLocal<CharsetDecoder> UTF8_DECODER = new C06521();
    protected ByteBuffer bb;
    protected int bb_pos;

    /* renamed from: com.google.flatbuffers.Table$1 */
    class C06521 extends ThreadLocal<CharsetDecoder> {
        C06521() {
        }

        protected CharsetDecoder initialValue() {
            return Charset.forName("UTF-8").newDecoder();
        }
    }

    /* renamed from: com.google.flatbuffers.Table$2 */
    class C06532 extends ThreadLocal<Charset> {
        C06532() {
        }

        protected Charset initialValue() {
            return Charset.forName("UTF-8");
        }
    }

    protected static boolean __has_identifier(ByteBuffer byteBuffer, String str) {
        if (str.length() != 4) {
            throw new AssertionError("FlatBuffers: file identifier must be length 4");
        }
        for (int i = 0; i < 4; i++) {
            if (str.charAt(i) != ((char) byteBuffer.get((byteBuffer.position() + 4) + i))) {
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
        i4 += 4;
        int min = Math.min(i5, i6);
        for (i3 = 0; i3 < min; i3++) {
            if (byteBuffer.get(i3 + i7) != byteBuffer.get(i3 + i4)) {
                return byteBuffer.get(i3 + i7) - byteBuffer.get(i3 + i4);
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
        for (i2 = 0; i2 < min; i2++) {
            if (byteBuffer.get(i2 + i4) != bArr[i2]) {
                return byteBuffer.get(i2 + i4) - bArr[i2];
            }
        }
        return i3 - length;
    }

    protected int __indirect(int i) {
        return this.bb.getInt(i) + i;
    }

    protected int __offset(int i) {
        int i2 = this.bb_pos - this.bb.getInt(this.bb_pos);
        return i < this.bb.getShort(i2) ? this.bb.getShort(i2 + i) : 0;
    }

    protected String __string(int i) {
        CharsetDecoder charsetDecoder = (CharsetDecoder) UTF8_DECODER.get();
        charsetDecoder.reset();
        int i2 = this.bb.getInt(i) + i;
        ByteBuffer order = this.bb.duplicate().order(ByteOrder.LITTLE_ENDIAN);
        int i3 = order.getInt(i2);
        order.position(i2 + 4);
        order.limit((i2 + 4) + i3);
        i3 = (int) (((float) i3) * charsetDecoder.maxCharsPerByte());
        CharBuffer charBuffer = (CharBuffer) CHAR_BUFFER.get();
        if (charBuffer == null || charBuffer.capacity() < i3) {
            charBuffer = CharBuffer.allocate(i3);
            CHAR_BUFFER.set(charBuffer);
        }
        charBuffer.clear();
        try {
            CoderResult decode = charsetDecoder.decode(order, charBuffer, true);
            if (!decode.isUnderflow()) {
                decode.throwException();
            }
            return charBuffer.flip().toString();
        } catch (Throwable e) {
            throw new Error(e);
        }
    }

    protected Table __union(Table table, int i) {
        int i2 = this.bb_pos + i;
        table.bb_pos = i2 + this.bb.getInt(i2);
        table.bb = this.bb;
        return table;
    }

    protected int __vector(int i) {
        int i2 = this.bb_pos + i;
        return (i2 + this.bb.getInt(i2)) + 4;
    }

    protected ByteBuffer __vector_as_bytebuffer(int i, int i2) {
        int __offset = __offset(i);
        if (__offset == 0) {
            return null;
        }
        ByteBuffer order = this.bb.duplicate().order(ByteOrder.LITTLE_ENDIAN);
        int __vector = __vector(__offset);
        order.position(__vector);
        order.limit((__vector_len(__offset) * i2) + __vector);
        return order;
    }

    protected int __vector_len(int i) {
        int i2 = this.bb_pos + i;
        return this.bb.getInt(i2 + this.bb.getInt(i2));
    }

    public ByteBuffer getByteBuffer() {
        return this.bb;
    }

    protected int keysCompare(Integer num, Integer num2, ByteBuffer byteBuffer) {
        return 0;
    }

    protected void sortTables(int[] iArr, final ByteBuffer byteBuffer) {
        int i = 0;
        Integer[] numArr = new Integer[iArr.length];
        for (int i2 = 0; i2 < iArr.length; i2++) {
            numArr[i2] = Integer.valueOf(iArr[i2]);
        }
        Arrays.sort(numArr, new Comparator<Integer>() {
            public int compare(Integer num, Integer num2) {
                return Table.this.keysCompare(num, num2, byteBuffer);
            }
        });
        while (i < iArr.length) {
            iArr[i] = numArr[i].intValue();
            i++;
        }
    }
}
