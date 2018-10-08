package im.getsocial.p015a.p016a;

import java.io.StringWriter;
import java.io.Writer;
import java.util.ArrayList;
import java.util.Collection;

/* renamed from: im.getsocial.a.a.upgqDBbsrL */
public class upgqDBbsrL<V> extends ArrayList<V> implements XdbacJlTDQ, cjrhisSQCL {
    /* renamed from: a */
    private static String m729a(Collection collection) {
        Writer stringWriter = new StringWriter();
        try {
            upgqDBbsrL.m730a(collection, stringWriter);
            return stringWriter.toString();
        } catch (Throwable e) {
            throw new RuntimeException(e);
        }
    }

    /* renamed from: a */
    public static void m730a(Collection collection, Writer writer) {
        if (collection == null) {
            writer.write("null");
            return;
        }
        Object obj = 1;
        writer.write(91);
        for (Object next : collection) {
            if (obj != null) {
                obj = null;
            } else {
                writer.write(44);
            }
            if (next == null) {
                writer.write("null");
            } else {
                zoToeBNOjF.m743a(next, writer);
            }
        }
        writer.write(93);
    }

    /* renamed from: a */
    public static void m731a(byte[] bArr, Writer writer) {
        if (bArr == null) {
            writer.write("null");
        } else if (bArr.length == 0) {
            writer.write("[]");
        } else {
            writer.write("[");
            writer.write(String.valueOf(bArr[0]));
            for (int i = 1; i < bArr.length; i++) {
                writer.write(",");
                writer.write(String.valueOf(bArr[i]));
            }
            writer.write("]");
        }
    }

    /* renamed from: a */
    public static void m732a(char[] cArr, Writer writer) {
        if (cArr == null) {
            writer.write("null");
        } else if (cArr.length == 0) {
            writer.write("[]");
        } else {
            writer.write("[\"");
            writer.write(String.valueOf(cArr[0]));
            for (int i = 1; i < cArr.length; i++) {
                writer.write("\",\"");
                writer.write(String.valueOf(cArr[i]));
            }
            writer.write("\"]");
        }
    }

    /* renamed from: a */
    public static void m733a(double[] dArr, Writer writer) {
        if (dArr == null) {
            writer.write("null");
        } else if (dArr.length == 0) {
            writer.write("[]");
        } else {
            writer.write("[");
            writer.write(String.valueOf(dArr[0]));
            for (int i = 1; i < dArr.length; i++) {
                writer.write(",");
                writer.write(String.valueOf(dArr[i]));
            }
            writer.write("]");
        }
    }

    /* renamed from: a */
    public static void m734a(float[] fArr, Writer writer) {
        if (fArr == null) {
            writer.write("null");
        } else if (fArr.length == 0) {
            writer.write("[]");
        } else {
            writer.write("[");
            writer.write(String.valueOf(fArr[0]));
            for (int i = 1; i < fArr.length; i++) {
                writer.write(",");
                writer.write(String.valueOf(fArr[i]));
            }
            writer.write("]");
        }
    }

    /* renamed from: a */
    public static void m735a(int[] iArr, Writer writer) {
        if (iArr == null) {
            writer.write("null");
        } else if (iArr.length == 0) {
            writer.write("[]");
        } else {
            writer.write("[");
            writer.write(String.valueOf(iArr[0]));
            for (int i = 1; i < iArr.length; i++) {
                writer.write(",");
                writer.write(String.valueOf(iArr[i]));
            }
            writer.write("]");
        }
    }

    /* renamed from: a */
    public static void m736a(long[] jArr, Writer writer) {
        if (jArr == null) {
            writer.write("null");
        } else if (jArr.length == 0) {
            writer.write("[]");
        } else {
            writer.write("[");
            writer.write(String.valueOf(jArr[0]));
            for (int i = 1; i < jArr.length; i++) {
                writer.write(",");
                writer.write(String.valueOf(jArr[i]));
            }
            writer.write("]");
        }
    }

    /* renamed from: a */
    public static void m737a(Object[] objArr, Writer writer) {
        if (objArr == null) {
            writer.write("null");
        } else if (objArr.length == 0) {
            writer.write("[]");
        } else {
            writer.write("[");
            zoToeBNOjF.m743a(objArr[0], writer);
            for (int i = 1; i < objArr.length; i++) {
                writer.write(",");
                zoToeBNOjF.m743a(objArr[i], writer);
            }
            writer.write("]");
        }
    }

    /* renamed from: a */
    public static void m738a(short[] sArr, Writer writer) {
        if (sArr == null) {
            writer.write("null");
        } else if (sArr.length == 0) {
            writer.write("[]");
        } else {
            writer.write("[");
            writer.write(String.valueOf(sArr[0]));
            for (int i = 1; i < sArr.length; i++) {
                writer.write(",");
                writer.write(String.valueOf(sArr[i]));
            }
            writer.write("]");
        }
    }

    /* renamed from: a */
    public static void m739a(boolean[] zArr, Writer writer) {
        if (zArr == null) {
            writer.write("null");
        } else if (zArr.length == 0) {
            writer.write("[]");
        } else {
            writer.write("[");
            writer.write(String.valueOf(zArr[0]));
            for (int i = 1; i < zArr.length; i++) {
                writer.write(",");
                writer.write(String.valueOf(zArr[i]));
            }
            writer.write("]");
        }
    }

    /* renamed from: a */
    public final String mo4291a() {
        return upgqDBbsrL.m729a((Collection) this);
    }

    /* renamed from: a */
    public final void mo4292a(Writer writer) {
        upgqDBbsrL.m730a((Collection) this, writer);
    }

    public String toString() {
        return upgqDBbsrL.m729a((Collection) this);
    }
}
