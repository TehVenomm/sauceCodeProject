package im.getsocial.p015a.p016a;

import java.io.StringWriter;
import java.io.Writer;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: im.getsocial.a.a.pdwpUtZXDT */
public class pdwpUtZXDT<K, V> extends HashMap<K, V> implements XdbacJlTDQ, cjrhisSQCL, Map<K, V> {
    public pdwpUtZXDT(Map map) {
        super(map);
    }

    /* renamed from: a */
    public static String m725a(Map map) {
        Writer stringWriter = new StringWriter();
        try {
            pdwpUtZXDT.m726a(map, stringWriter);
            return stringWriter.toString();
        } catch (Throwable e) {
            throw new RuntimeException(e);
        }
    }

    /* renamed from: a */
    public static void m726a(Map map, Writer writer) {
        if (map == null) {
            writer.write("null");
            return;
        }
        Object obj = 1;
        writer.write(123);
        for (Entry entry : map.entrySet()) {
            Object obj2;
            if (obj != null) {
                obj2 = null;
            } else {
                writer.write(44);
                obj2 = obj;
            }
            writer.write(34);
            writer.write(zoToeBNOjF.m742a(String.valueOf(entry.getKey())));
            writer.write(34);
            writer.write(58);
            zoToeBNOjF.m743a(entry.getValue(), writer);
            obj = obj2;
        }
        writer.write(125);
    }

    /* renamed from: a */
    public final String mo4291a() {
        return pdwpUtZXDT.m725a((Map) this);
    }

    /* renamed from: a */
    public final void mo4292a(Writer writer) {
        pdwpUtZXDT.m726a(this, writer);
    }

    public String toString() {
        return pdwpUtZXDT.m725a((Map) this);
    }
}
