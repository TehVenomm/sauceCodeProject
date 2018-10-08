package im.getsocial.p018b.p020b;

import java.nio.charset.Charset;

/* renamed from: im.getsocial.b.b.rWfbqYooCV */
final class rWfbqYooCV {
    /* renamed from: a */
    public static final Charset f1022a = Charset.forName("UTF-8");

    private rWfbqYooCV() {
    }

    /* renamed from: a */
    public static void m783a(long j, long j2, long j3) {
        if ((j2 | j3) < 0 || j2 > j || j - j2 < j3) {
            throw new ArrayIndexOutOfBoundsException(String.format("size=%s offset=%s byteCount=%s", new Object[]{Long.valueOf(j), Long.valueOf(j2), Long.valueOf(j3)}));
        }
    }

    /* renamed from: a */
    public static void m784a(Throwable th) {
        throw th;
    }

    /* renamed from: a */
    public static boolean m785a(byte[] bArr, int i, byte[] bArr2, int i2, int i3) {
        for (int i4 = 0; i4 < i3; i4++) {
            if (bArr[i4 + i] != bArr2[i4 + i2]) {
                return false;
            }
        }
        return true;
    }
}
