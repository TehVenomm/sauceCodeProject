package im.getsocial.p015a.p016a.p017a;

import android.support.v4.view.MotionEventCompat;
import java.io.Reader;
import org.apache.commons.lang3.CharUtils;

/* renamed from: im.getsocial.a.a.a.XdbacJlTDQ */
class XdbacJlTDQ {
    /* renamed from: a */
    private static final int[] f942a = new int[]{0, 0, 1, 1};
    /* renamed from: b */
    private static final char[] f943b;
    /* renamed from: c */
    private static final int[] f944c;
    /* renamed from: d */
    private static final int[] f945d;
    /* renamed from: e */
    private static final int[] f946e = new int[]{2, 2, 3, 4, 2, 2, 2, 5, 2, 6, 2, 2, 7, 8, 2, 9, 2, 2, 2, 2, 2, 10, 11, 12, 13, 14, 15, 16, 16, 16, 16, 16, 16, 16, 16, 17, 18, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 4, 19, 20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 21, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 22, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 23, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 16, 16, 16, 16, 16, 16, 16, 16, -1, -1, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, -1, -1, -1, -1, -1, -1, -1, -1, 24, 25, 26, 27, 28, 29, 30, 31, 32, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 33, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 34, 35, -1, -1, 34, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 36, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 37, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 38, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 39, -1, 39, -1, 39, -1, -1, -1, -1, -1, 39, 39, -1, -1, -1, -1, 39, 39, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 33, -1, 20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 35, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 38, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 40, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 41, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 42, -1, 42, -1, 42, -1, -1, -1, -1, -1, 42, 42, -1, -1, -1, -1, 42, 42, -1, -1, -1, -1, -1, -1, -1, -1, -1, 43, -1, 43, -1, 43, -1, -1, -1, -1, -1, 43, 43, -1, -1, -1, -1, 43, 43, -1, -1, -1, -1, -1, -1, -1, -1, -1, 44, -1, 44, -1, 44, -1, -1, -1, -1, -1, 44, 44, -1, -1, -1, -1, 44, 44, -1, -1, -1, -1, -1, -1, -1, -1};
    /* renamed from: f */
    private static final String[] f947f = new String[]{"Unkown internal scanner error", "Error: could not match input", "Error: pushback value was too large"};
    /* renamed from: g */
    private static final int[] f948g;
    /* renamed from: h */
    private Reader f949h = null;
    /* renamed from: i */
    private int f950i;
    /* renamed from: j */
    private int f951j = 0;
    /* renamed from: k */
    private char[] f952k = new char[16384];
    /* renamed from: l */
    private int f953l;
    /* renamed from: m */
    private int f954m;
    /* renamed from: n */
    private int f955n;
    /* renamed from: o */
    private int f956o;
    /* renamed from: p */
    private int f957p;
    /* renamed from: q */
    private int f958q;
    /* renamed from: r */
    private int f959r;
    /* renamed from: s */
    private boolean f960s = true;
    /* renamed from: t */
    private boolean f961t;
    /* renamed from: u */
    private StringBuffer f962u = new StringBuffer();

    static {
        int i;
        char[] cArr = new char[65536];
        int i2 = 0;
        int i3 = 0;
        while (i2 < 90) {
            i = i2 + 1;
            char charAt = "\t\u0000\u0001\u0007\u0001\u0007\u0002\u0000\u0001\u0007\u0012\u0000\u0001\u0007\u0001\u0000\u0001\t\b\u0000\u0001\u0006\u0001\u0019\u0001\u0002\u0001\u0004\u0001\n\n\u0003\u0001\u001a\u0006\u0000\u0004\u0001\u0001\u0005\u0001\u0001\u0014\u0000\u0001\u0017\u0001\b\u0001\u0018\u0003\u0000\u0001\u0012\u0001\u000b\u0002\u0001\u0001\u0011\u0001\f\u0005\u0000\u0001\u0013\u0001\u0000\u0001\r\u0003\u0000\u0001\u000e\u0001\u0014\u0001\u000f\u0001\u0010\u0005\u0000\u0001\u0015\u0001\u0000\u0001\u0016ﾂ\u0000".charAt(i2);
            char charAt2 = "\t\u0000\u0001\u0007\u0001\u0007\u0002\u0000\u0001\u0007\u0012\u0000\u0001\u0007\u0001\u0000\u0001\t\b\u0000\u0001\u0006\u0001\u0019\u0001\u0002\u0001\u0004\u0001\n\n\u0003\u0001\u001a\u0006\u0000\u0004\u0001\u0001\u0005\u0001\u0001\u0014\u0000\u0001\u0017\u0001\b\u0001\u0018\u0003\u0000\u0001\u0012\u0001\u000b\u0002\u0001\u0001\u0011\u0001\f\u0005\u0000\u0001\u0013\u0001\u0000\u0001\r\u0003\u0000\u0001\u000e\u0001\u0014\u0001\u000f\u0001\u0010\u0005\u0000\u0001\u0015\u0001\u0000\u0001\u0016ﾂ\u0000".charAt(i);
            int i4 = charAt;
            while (true) {
                i2 = i3 + 1;
                cArr[i3] = (char) charAt2;
                i3 = i4 - 1;
                if (i3 <= 0) {
                    break;
                }
                i4 = i3;
                i3 = i2;
            }
            i3 = i2;
            i2 = i + 1;
        }
        f943b = cArr;
        int[] iArr = new int[45];
        i = "\u0002\u0000\u0002\u0001\u0001\u0002\u0001\u0003\u0001\u0004\u0003\u0001\u0001\u0005\u0001\u0006\u0001\u0007\u0001\b\u0001\t\u0001\n\u0001\u000b\u0001\f\u0001\r\u0005\u0000\u0001\f\u0001\u000e\u0001\u000f\u0001\u0010\u0001\u0011\u0001\u0012\u0001\u0013\u0001\u0014\u0001\u0000\u0001\u0015\u0001\u0000\u0001\u0015\u0004\u0000\u0001\u0016\u0001\u0017\u0002\u0000\u0001\u0018".length();
        i3 = 0;
        i2 = 0;
        while (i3 < i) {
            int i5 = i3 + 1;
            i3 = "\u0002\u0000\u0002\u0001\u0001\u0002\u0001\u0003\u0001\u0004\u0003\u0001\u0001\u0005\u0001\u0006\u0001\u0007\u0001\b\u0001\t\u0001\n\u0001\u000b\u0001\f\u0001\r\u0005\u0000\u0001\f\u0001\u000e\u0001\u000f\u0001\u0010\u0001\u0011\u0001\u0012\u0001\u0013\u0001\u0014\u0001\u0000\u0001\u0015\u0001\u0000\u0001\u0015\u0004\u0000\u0001\u0016\u0001\u0017\u0002\u0000\u0001\u0018".charAt(i3);
            char charAt3 = "\u0002\u0000\u0002\u0001\u0001\u0002\u0001\u0003\u0001\u0004\u0003\u0001\u0001\u0005\u0001\u0006\u0001\u0007\u0001\b\u0001\t\u0001\n\u0001\u000b\u0001\f\u0001\r\u0005\u0000\u0001\f\u0001\u000e\u0001\u000f\u0001\u0010\u0001\u0011\u0001\u0012\u0001\u0013\u0001\u0014\u0001\u0000\u0001\u0015\u0001\u0000\u0001\u0015\u0004\u0000\u0001\u0016\u0001\u0017\u0002\u0000\u0001\u0018".charAt(i5);
            while (true) {
                i4 = i2 + 1;
                iArr[i2] = charAt3;
                i3--;
                if (i3 <= 0) {
                    break;
                }
                i2 = i4;
            }
            i3 = i5 + 1;
            i2 = i4;
        }
        f944c = iArr;
        int[] iArr2 = new int[45];
        int length = "\u0000\u0000\u0000\u001b\u00006\u0000Q\u0000l\u0000\u00006\u0000¢\u0000½\u0000Ø\u00006\u00006\u00006\u00006\u00006\u00006\u0000ó\u0000Ď\u00006\u0000ĩ\u0000ń\u0000ş\u0000ź\u0000ƕ\u00006\u00006\u00006\u00006\u00006\u00006\u00006\u00006\u0000ư\u0000ǋ\u0000Ǧ\u0000Ǧ\u0000ȁ\u0000Ȝ\u0000ȷ\u0000ɒ\u00006\u00006\u0000ɭ\u0000ʈ\u00006".length();
        i3 = 0;
        i2 = 0;
        while (i3 < length) {
            i = i3 + 1;
            charAt2 = "\u0000\u0000\u0000\u001b\u00006\u0000Q\u0000l\u0000\u00006\u0000¢\u0000½\u0000Ø\u00006\u00006\u00006\u00006\u00006\u00006\u0000ó\u0000Ď\u00006\u0000ĩ\u0000ń\u0000ş\u0000ź\u0000ƕ\u00006\u00006\u00006\u00006\u00006\u00006\u00006\u00006\u0000ư\u0000ǋ\u0000Ǧ\u0000Ǧ\u0000ȁ\u0000Ȝ\u0000ȷ\u0000ɒ\u00006\u00006\u0000ɭ\u0000ʈ\u00006".charAt(i3);
            i3 = i + 1;
            iArr2[i2] = "\u0000\u0000\u0000\u001b\u00006\u0000Q\u0000l\u0000\u00006\u0000¢\u0000½\u0000Ø\u00006\u00006\u00006\u00006\u00006\u00006\u0000ó\u0000Ď\u00006\u0000ĩ\u0000ń\u0000ş\u0000ź\u0000ƕ\u00006\u00006\u00006\u00006\u00006\u00006\u00006\u00006\u0000ư\u0000ǋ\u0000Ǧ\u0000Ǧ\u0000ȁ\u0000Ȝ\u0000ȷ\u0000ɒ\u00006\u00006\u0000ɭ\u0000ʈ\u00006".charAt(i) | (charAt2 << 16);
            i2++;
        }
        f945d = iArr2;
        iArr = new int[45];
        i = "\u0002\u0000\u0001\t\u0003\u0001\u0001\t\u0003\u0001\u0006\t\u0002\u0001\u0001\t\u0005\u0000\b\t\u0001\u0000\u0001\u0001\u0001\u0000\u0001\u0001\u0004\u0000\u0002\t\u0002\u0000\u0001\t".length();
        i2 = 0;
        i3 = 0;
        while (i2 < i) {
            int i6 = i2 + 1;
            charAt = "\u0002\u0000\u0001\t\u0003\u0001\u0001\t\u0003\u0001\u0006\t\u0002\u0001\u0001\t\u0005\u0000\b\t\u0001\u0000\u0001\u0001\u0001\u0000\u0001\u0001\u0004\u0000\u0002\t\u0002\u0000\u0001\t".charAt(i2);
            charAt2 = "\u0002\u0000\u0001\t\u0003\u0001\u0001\t\u0003\u0001\u0006\t\u0002\u0001\u0001\t\u0005\u0000\b\t\u0001\u0000\u0001\u0001\u0001\u0000\u0001\u0001\u0004\u0000\u0002\t\u0002\u0000\u0001\t".charAt(i6);
            i4 = charAt;
            while (true) {
                i2 = i3 + 1;
                iArr[i3] = charAt2;
                i3 = i4 - 1;
                if (i3 <= 0) {
                    break;
                }
                i4 = i3;
                i3 = i2;
            }
            i3 = i2;
            i2 = i6 + 1;
        }
        f948g = iArr;
    }

    XdbacJlTDQ(Reader reader) {
    }

    /* renamed from: c */
    private String m713c() {
        return new String(this.f952k, this.f955n, this.f953l - this.f955n);
    }

    /* renamed from: a */
    final int m714a() {
        return this.f958q;
    }

    /* renamed from: a */
    public final void m715a(Reader reader) {
        this.f949h = reader;
        this.f960s = true;
        this.f961t = false;
        this.f955n = 0;
        this.f956o = 0;
        this.f953l = 0;
        this.f954m = 0;
        this.f959r = 0;
        this.f958q = 0;
        this.f957p = 0;
        this.f951j = 0;
    }

    /* renamed from: b */
    public final zoToeBNOjF m716b() {
        int i = this.f956o;
        char[] cArr = this.f952k;
        char[] cArr2 = f943b;
        int[] iArr = f946e;
        int[] iArr2 = f945d;
        int[] iArr3 = f948g;
        while (true) {
            int i2 = this.f953l;
            this.f958q += i2 - this.f955n;
            int i3 = -1;
            this.f955n = i2;
            this.f954m = i2;
            this.f950i = f942a[this.f951j];
            int i4 = i;
            i = i2;
            while (true) {
                char c;
                int i5;
                char c2;
                int i6;
                if (i < i4) {
                    i5 = i4;
                    i4 = i2;
                    i2 = i + 1;
                    c2 = cArr[i];
                    i6 = iArr[iArr2[this.f950i] + cArr2[c2]];
                    if (i6 == -1) {
                        c = c2;
                        i = i5;
                    } else {
                        this.f950i = i6;
                        i6 = iArr3[this.f950i];
                        if ((i6 & 1) == 1) {
                            i4 = this.f950i;
                            if ((i6 & 8) == 8) {
                                i3 = i4;
                                i4 = i2;
                                c = c2;
                                i = i5;
                            } else {
                                i3 = i4;
                                i4 = i2;
                            }
                        }
                        i = i2;
                        i2 = i4;
                        i4 = i5;
                    }
                } else if (this.f961t) {
                    i = i4;
                    i4 = i2;
                    c = '￿';
                } else {
                    Object obj;
                    this.f954m = i;
                    this.f953l = i2;
                    if (this.f955n > 0) {
                        System.arraycopy(this.f952k, this.f955n, this.f952k, 0, this.f956o - this.f955n);
                        this.f956o -= this.f955n;
                        this.f954m -= this.f955n;
                        this.f953l -= this.f955n;
                        this.f955n = 0;
                    }
                    if (this.f954m >= this.f952k.length) {
                        Object obj2 = new char[(this.f954m << 1)];
                        System.arraycopy(this.f952k, 0, obj2, 0, this.f952k.length);
                        this.f952k = obj2;
                    }
                    int read = this.f949h.read(this.f952k, this.f956o, this.f952k.length - this.f956o);
                    if (read > 0) {
                        this.f956o = read + this.f956o;
                        obj = null;
                    } else {
                        if (read == 0) {
                            read = this.f949h.read();
                            if (read != -1) {
                                char[] cArr3 = this.f952k;
                                i2 = this.f956o;
                                this.f956o = i2 + 1;
                                cArr3[i2] = (char) ((char) read);
                                obj = null;
                            }
                        }
                        i = 1;
                    }
                    i5 = this.f954m;
                    i2 = this.f953l;
                    cArr = this.f952k;
                    i4 = this.f956o;
                    if (obj != null) {
                        i = i4;
                        i4 = i2;
                        c = '￿';
                    } else {
                        i = i5;
                        i5 = i4;
                        i4 = i2;
                        i2 = i + 1;
                        c2 = cArr[i];
                        i6 = iArr[iArr2[this.f950i] + cArr2[c2]];
                        if (i6 == -1) {
                            this.f950i = i6;
                            i6 = iArr3[this.f950i];
                            if ((i6 & 1) == 1) {
                                i4 = this.f950i;
                                if ((i6 & 8) == 8) {
                                    i3 = i4;
                                    i4 = i2;
                                } else {
                                    i3 = i4;
                                    i4 = i2;
                                    c = c2;
                                    i = i5;
                                }
                            }
                            i = i2;
                            i2 = i4;
                            i4 = i5;
                        } else {
                            c = c2;
                            i = i5;
                        }
                    }
                }
                this.f953l = i4;
                if (i3 >= 0) {
                    i3 = f944c[i3];
                }
                switch (i3) {
                    case 1:
                        throw new pdwpUtZXDT(this.f958q, 0, new Character(this.f952k[this.f955n]));
                    case 2:
                        return new zoToeBNOjF(0, Long.valueOf(m713c()));
                    case 3:
                    case 25:
                    case 26:
                    case MotionEventCompat.AXIS_RELATIVE_X /*27*/:
                    case MotionEventCompat.AXIS_RELATIVE_Y /*28*/:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                    case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    case MotionEventCompat.AXIS_GENERIC_4 /*35*/:
                    case MotionEventCompat.AXIS_GENERIC_5 /*36*/:
                    case MotionEventCompat.AXIS_GENERIC_6 /*37*/:
                    case MotionEventCompat.AXIS_GENERIC_7 /*38*/:
                    case MotionEventCompat.AXIS_GENERIC_8 /*39*/:
                    case 40:
                    case MotionEventCompat.AXIS_GENERIC_10 /*41*/:
                    case 42:
                    case MotionEventCompat.AXIS_GENERIC_12 /*43*/:
                    case MotionEventCompat.AXIS_GENERIC_13 /*44*/:
                    case MotionEventCompat.AXIS_GENERIC_14 /*45*/:
                    case MotionEventCompat.AXIS_GENERIC_15 /*46*/:
                    case MotionEventCompat.AXIS_GENERIC_16 /*47*/:
                    case 48:
                        break;
                    case 4:
                        this.f962u = null;
                        this.f962u = new StringBuffer();
                        this.f951j = 2;
                        break;
                    case 5:
                        return new zoToeBNOjF(1, null);
                    case 6:
                        return new zoToeBNOjF(2, null);
                    case 7:
                        return new zoToeBNOjF(3, null);
                    case 8:
                        return new zoToeBNOjF(4, null);
                    case 9:
                        return new zoToeBNOjF(5, null);
                    case 10:
                        return new zoToeBNOjF(6, null);
                    case 11:
                        this.f962u.append(m713c());
                        break;
                    case 12:
                        this.f962u.append('\\');
                        break;
                    case 13:
                        this.f951j = 0;
                        return new zoToeBNOjF(0, this.f962u.toString());
                    case 14:
                        this.f962u.append('\"');
                        break;
                    case 15:
                        this.f962u.append('/');
                        break;
                    case 16:
                        this.f962u.append('\b');
                        break;
                    case 17:
                        this.f962u.append('\f');
                        break;
                    case 18:
                        this.f962u.append('\n');
                        break;
                    case 19:
                        this.f962u.append(CharUtils.CR);
                        break;
                    case 20:
                        this.f962u.append('\t');
                        break;
                    case 21:
                        return new zoToeBNOjF(0, Double.valueOf(m713c()));
                    case 22:
                        return new zoToeBNOjF(0, null);
                    case 23:
                        return new zoToeBNOjF(0, Boolean.valueOf(m713c()));
                    case MotionEventCompat.AXIS_DISTANCE /*24*/:
                        try {
                            this.f962u.append((char) Integer.parseInt(m713c().substring(2), 16));
                            break;
                        } catch (Exception e) {
                            throw new pdwpUtZXDT(this.f958q, 2, e);
                        }
                    default:
                        if (c == '￿' && this.f955n == this.f954m) {
                            this.f961t = true;
                            return null;
                        }
                        String str;
                        try {
                            str = f947f[1];
                        } catch (ArrayIndexOutOfBoundsException e2) {
                            str = f947f[0];
                        }
                        throw new Error(str);
                }
            }
        }
    }
}
