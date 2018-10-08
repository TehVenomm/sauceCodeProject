package im.getsocial.p018b.p020b;

import java.io.Serializable;
import java.util.Arrays;
import org.apache.commons.lang3.StringUtils;

/* renamed from: im.getsocial.b.b.zoToeBNOjF */
public class zoToeBNOjF implements Serializable, Comparable<zoToeBNOjF> {
    /* renamed from: a */
    static final char[] f1028a = new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};
    /* renamed from: b */
    public static final zoToeBNOjF f1029b = new zoToeBNOjF((byte[]) new byte[0].clone());
    /* renamed from: c */
    final byte[] f1030c;
    /* renamed from: d */
    transient int f1031d;
    /* renamed from: e */
    transient String f1032e;

    zoToeBNOjF(byte[] bArr) {
        this.f1030c = bArr;
    }

    /* renamed from: a */
    public byte mo4306a(int i) {
        return this.f1030c[i];
    }

    /* renamed from: a */
    public zoToeBNOjF mo4307a(int i, int i2) {
        if (i < 0) {
            throw new IllegalArgumentException("beginIndex < 0");
        } else if (i2 > this.f1030c.length) {
            throw new IllegalArgumentException("endIndex > length(" + this.f1030c.length + ")");
        } else {
            int i3 = i2 - i;
            if (i3 < 0) {
                throw new IllegalArgumentException("endIndex < beginIndex");
            } else if (i == 0 && i2 == this.f1030c.length) {
                return this;
            } else {
                Object obj = new byte[i3];
                System.arraycopy(this.f1030c, i, obj, 0, i3);
                return new zoToeBNOjF(obj);
            }
        }
    }

    /* renamed from: a */
    public String mo4308a() {
        String str = this.f1032e;
        if (str != null) {
            return str;
        }
        str = new String(this.f1030c, rWfbqYooCV.f1022a);
        this.f1032e = str;
        return str;
    }

    /* renamed from: a */
    public boolean mo4309a(int i, byte[] bArr, int i2, int i3) {
        return i >= 0 && i <= this.f1030c.length - i3 && i2 >= 0 && i2 <= bArr.length - i3 && rWfbqYooCV.m785a(this.f1030c, i, bArr, i2, i3);
    }

    /* renamed from: b */
    public String mo4310b() {
        int i = 0;
        char[] cArr = new char[(this.f1030c.length << 1)];
        byte[] bArr = this.f1030c;
        int length = bArr.length;
        int i2 = 0;
        while (i < length) {
            byte b = bArr[i];
            int i3 = i2 + 1;
            cArr[i2] = (char) f1028a[(b >> 4) & 15];
            i2 = i3 + 1;
            cArr[i3] = (char) f1028a[b & 15];
            i++;
        }
        return new String(cArr);
    }

    /* renamed from: c */
    public int mo4311c() {
        return this.f1030c.length;
    }

    public /* synthetic */ int compareTo(Object obj) {
        zoToeBNOjF zotoebnojf = (zoToeBNOjF) obj;
        int c = mo4311c();
        int c2 = zotoebnojf.mo4311c();
        int min = Math.min(c, c2);
        int i = 0;
        while (i < min) {
            int a = mo4306a(i) & 255;
            int a2 = zotoebnojf.mo4306a(i) & 255;
            if (a != a2) {
                return a < a2 ? -1 : 1;
            } else {
                i++;
            }
        }
        return c == c2 ? 0 : c >= c2 ? 1 : -1;
    }

    /* renamed from: d */
    public byte[] mo4312d() {
        return (byte[]) this.f1030c.clone();
    }

    public boolean equals(Object obj) {
        return obj == this ? true : (obj instanceof zoToeBNOjF) && ((zoToeBNOjF) obj).mo4311c() == this.f1030c.length && ((zoToeBNOjF) obj).mo4309a(0, this.f1030c, 0, this.f1030c.length);
    }

    public int hashCode() {
        int i = this.f1031d;
        if (i != 0) {
            return i;
        }
        i = Arrays.hashCode(this.f1030c);
        this.f1031d = i;
        return i;
    }

    public String toString() {
        if (this.f1030c.length == 0) {
            return "[size=0]";
        }
        String a = mo4308a();
        int length = a.length();
        int i = 0;
        int i2 = 0;
        while (i2 < length) {
            if (i != 64) {
                int codePointAt = a.codePointAt(i2);
                if ((Character.isISOControl(codePointAt) && codePointAt != 10 && codePointAt != 13) || codePointAt == 65533) {
                    i2 = -1;
                    break;
                }
                i++;
                i2 += Character.charCount(codePointAt);
            } else {
                break;
            }
        }
        i2 = a.length();
        if (i2 == -1) {
            return this.f1030c.length <= 64 ? "[hex=" + mo4310b() + "]" : "[size=" + this.f1030c.length + " hex=" + mo4307a(0, 64).mo4310b() + "…]";
        } else {
            String replace = a.substring(0, i2).replace("\\", "\\\\").replace(StringUtils.LF, "\\n").replace(StringUtils.CR, "\\r");
            return i2 < a.length() ? "[size=" + this.f1030c.length + " text=" + replace + "…]" : "[text=" + replace + "]";
        }
    }
}
