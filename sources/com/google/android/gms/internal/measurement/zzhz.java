package com.google.android.gms.internal.measurement;

import com.github.droidfu.support.DisplaySupport;
import java.nio.ByteBuffer;

abstract class zzhz {
    zzhz() {
    }

    static void zzc(CharSequence charSequence, ByteBuffer byteBuffer) {
        int i;
        int i2;
        int length = charSequence.length();
        int position = byteBuffer.position();
        int i3 = 0;
        while (i3 < length) {
            try {
                char charAt = charSequence.charAt(i3);
                if (charAt >= 128) {
                    break;
                }
                byteBuffer.put(position + i3, (byte) charAt);
                i3++;
            } catch (IndexOutOfBoundsException e) {
                int position2 = byteBuffer.position();
                int max = Math.max(i3, (i - byteBuffer.position()) + 1);
                throw new ArrayIndexOutOfBoundsException("Failed writing " + charSequence.charAt(i3) + " at index " + (max + position2));
            }
        }
        if (i3 == length) {
            byteBuffer.position(position + i3);
            return;
        }
        position += i3;
        int i4 = i3;
        while (i4 < length) {
            try {
                char charAt2 = charSequence.charAt(i4);
                if (charAt2 < 128) {
                    byteBuffer.put(position, (byte) charAt2);
                } else if (charAt2 < 2048) {
                    int i5 = position + 1;
                    try {
                        byteBuffer.put(position, (byte) ((charAt2 >>> 6) | 192));
                        byteBuffer.put(i5, (byte) ((charAt2 & '?') | 128));
                        position = i5;
                    } catch (IndexOutOfBoundsException e2) {
                        i = i5;
                        i3 = i4;
                        int position22 = byteBuffer.position();
                        int max2 = Math.max(i3, (i - byteBuffer.position()) + 1);
                        throw new ArrayIndexOutOfBoundsException("Failed writing " + charSequence.charAt(i3) + " at index " + (max2 + position22));
                    }
                } else if (charAt2 < 55296 || 57343 < charAt2) {
                    int i6 = position + 1;
                    byteBuffer.put(position, (byte) ((charAt2 >>> 12) | 224));
                    position = i6 + 1;
                    byteBuffer.put(i6, (byte) (((charAt2 >>> 6) & 63) | 128));
                    byteBuffer.put(position, (byte) ((charAt2 & '?') | 128));
                } else {
                    if (i4 + 1 != length) {
                        i3 = i4 + 1;
                        char charAt3 = charSequence.charAt(i3);
                        if (Character.isSurrogatePair(charAt2, charAt3)) {
                            int codePoint = Character.toCodePoint(charAt2, charAt3);
                            int i7 = position + 1;
                            try {
                                byteBuffer.put(position, (byte) ((codePoint >>> 18) | DisplaySupport.SCREEN_DENSITY_HIGH));
                                i2 = i7 + 1;
                            } catch (IndexOutOfBoundsException e3) {
                                i = i7;
                                int position222 = byteBuffer.position();
                                int max22 = Math.max(i3, (i - byteBuffer.position()) + 1);
                                throw new ArrayIndexOutOfBoundsException("Failed writing " + charSequence.charAt(i3) + " at index " + (max22 + position222));
                            }
                            try {
                                byteBuffer.put(i7, (byte) (((codePoint >>> 12) & 63) | 128));
                                position = i2 + 1;
                                byteBuffer.put(i2, (byte) (((codePoint >>> 6) & 63) | 128));
                                byteBuffer.put(position, (byte) ((codePoint & 63) | 128));
                                i4 = i3;
                            } catch (IndexOutOfBoundsException e4) {
                                i4 = i3;
                                i = i2;
                                i3 = i4;
                                int position2222 = byteBuffer.position();
                                int max222 = Math.max(i3, (i - byteBuffer.position()) + 1);
                                throw new ArrayIndexOutOfBoundsException("Failed writing " + charSequence.charAt(i3) + " at index " + (max222 + position2222));
                            }
                        }
                    } else {
                        i3 = i4;
                    }
                    throw new zzib(i3, length);
                }
                i4++;
                position++;
            } catch (IndexOutOfBoundsException e5) {
                i3 = i4;
            }
        }
        byteBuffer.position(position);
    }

    /* access modifiers changed from: 0000 */
    public abstract int zzb(int i, byte[] bArr, int i2, int i3);

    /* access modifiers changed from: 0000 */
    public abstract int zzb(CharSequence charSequence, byte[] bArr, int i, int i2);

    /* access modifiers changed from: 0000 */
    public abstract void zzb(CharSequence charSequence, ByteBuffer byteBuffer);

    /* access modifiers changed from: 0000 */
    public final boolean zzf(byte[] bArr, int i, int i2) {
        return zzb(0, bArr, i, i2) == 0;
    }

    /* access modifiers changed from: 0000 */
    public abstract String zzh(byte[] bArr, int i, int i2) throws zzfi;
}
