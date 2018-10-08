package im.getsocial.p015a.p016a;

import android.support.v4.view.MotionEventCompat;
import java.io.Writer;
import java.util.Collection;
import java.util.Map;

/* renamed from: im.getsocial.a.a.zoToeBNOjF */
public class zoToeBNOjF {
    /* renamed from: a */
    public static String m742a(String str) {
        if (str == null) {
            return null;
        }
        StringBuffer stringBuffer = new StringBuffer();
        zoToeBNOjF.m744a(str, stringBuffer);
        return stringBuffer.toString();
    }

    /* renamed from: a */
    public static void m743a(Object obj, Writer writer) {
        if (obj == null) {
            writer.write("null");
        } else if (obj instanceof String) {
            writer.write(34);
            writer.write(zoToeBNOjF.m742a((String) obj));
            writer.write(34);
        } else if (obj instanceof Double) {
            if (((Double) obj).isInfinite() || ((Double) obj).isNaN()) {
                writer.write("null");
            } else {
                writer.write(obj.toString());
            }
        } else if (obj instanceof Float) {
            if (((Float) obj).isInfinite() || ((Float) obj).isNaN()) {
                writer.write("null");
            } else {
                writer.write(obj.toString());
            }
        } else if (obj instanceof Number) {
            writer.write(obj.toString());
        } else if (obj instanceof Boolean) {
            writer.write(obj.toString());
        } else if (obj instanceof XdbacJlTDQ) {
            ((XdbacJlTDQ) obj).mo4292a(writer);
        } else if (obj instanceof cjrhisSQCL) {
            writer.write(((cjrhisSQCL) obj).mo4291a());
        } else if (obj instanceof Map) {
            pdwpUtZXDT.m726a((Map) obj, writer);
        } else if (obj instanceof Collection) {
            upgqDBbsrL.m730a((Collection) obj, writer);
        } else if (obj instanceof byte[]) {
            upgqDBbsrL.m731a((byte[]) obj, writer);
        } else if (obj instanceof short[]) {
            upgqDBbsrL.m738a((short[]) obj, writer);
        } else if (obj instanceof int[]) {
            upgqDBbsrL.m735a((int[]) obj, writer);
        } else if (obj instanceof long[]) {
            upgqDBbsrL.m736a((long[]) obj, writer);
        } else if (obj instanceof float[]) {
            upgqDBbsrL.m734a((float[]) obj, writer);
        } else if (obj instanceof double[]) {
            upgqDBbsrL.m733a((double[]) obj, writer);
        } else if (obj instanceof boolean[]) {
            upgqDBbsrL.m739a((boolean[]) obj, writer);
        } else if (obj instanceof char[]) {
            upgqDBbsrL.m732a((char[]) obj, writer);
        } else if (obj instanceof Object[]) {
            upgqDBbsrL.m737a((Object[]) obj, writer);
        } else {
            writer.write(obj.toString());
        }
    }

    /* renamed from: a */
    private static void m744a(String str, StringBuffer stringBuffer) {
        int length = str.length();
        for (int i = 0; i < length; i++) {
            char charAt = str.charAt(i);
            switch (charAt) {
                case '\b':
                    stringBuffer.append("\\b");
                    break;
                case '\t':
                    stringBuffer.append("\\t");
                    break;
                case '\n':
                    stringBuffer.append("\\n");
                    break;
                case '\f':
                    stringBuffer.append("\\f");
                    break;
                case '\r':
                    stringBuffer.append("\\r");
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    stringBuffer.append("\\\"");
                    break;
                case MotionEventCompat.AXIS_GENERIC_16 /*47*/:
                    stringBuffer.append("\\/");
                    break;
                case '\\':
                    stringBuffer.append("\\\\");
                    break;
                default:
                    if ((charAt >= '\u0000' && charAt <= '\u001f') || ((charAt >= '' && charAt <= '') || (charAt >= ' ' && charAt <= '⃿'))) {
                        String toHexString = Integer.toHexString(charAt);
                        stringBuffer.append("\\u");
                        for (int i2 = 0; i2 < 4 - toHexString.length(); i2++) {
                            stringBuffer.append('0');
                        }
                        stringBuffer.append(toHexString.toUpperCase());
                        break;
                    }
                    stringBuffer.append(charAt);
                    break;
            }
        }
    }
}
