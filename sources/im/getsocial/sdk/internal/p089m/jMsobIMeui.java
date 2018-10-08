package im.getsocial.sdk.internal.p089m;

import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import java.io.BufferedReader;
import java.io.Closeable;
import java.io.IOException;
import java.io.InputStreamReader;
import java.nio.charset.Charset;

/* renamed from: im.getsocial.sdk.internal.m.jMsobIMeui */
public final class jMsobIMeui {
    /* renamed from: a */
    private static final cjrhisSQCL f2218a = upgqDBbsrL.m1274a(jMsobIMeui.class);

    private jMsobIMeui() {
    }

    /* renamed from: a */
    public static String m2116a(String str) {
        Closeable inputStreamReader;
        String readLine;
        Closeable closeable;
        Throwable th;
        Closeable closeable2 = null;
        Closeable bufferedReader;
        try {
            inputStreamReader = new InputStreamReader(Runtime.getRuntime().exec(new String[]{"getprop", str}).getInputStream(), Charset.defaultCharset());
            try {
                bufferedReader = new BufferedReader(inputStreamReader);
                try {
                    readLine = bufferedReader.readLine();
                    jMsobIMeui.m2117a(inputStreamReader);
                    jMsobIMeui.m2117a(bufferedReader);
                } catch (IOException e) {
                    jMsobIMeui.m2117a(inputStreamReader);
                    jMsobIMeui.m2117a(bufferedReader);
                    return readLine;
                } catch (Throwable th2) {
                    closeable = inputStreamReader;
                    inputStreamReader = bufferedReader;
                    th = th2;
                    closeable2 = closeable;
                    jMsobIMeui.m2117a(closeable2);
                    jMsobIMeui.m2117a(inputStreamReader);
                    throw th;
                }
            } catch (IOException e2) {
                bufferedReader = closeable2;
                jMsobIMeui.m2117a(inputStreamReader);
                jMsobIMeui.m2117a(bufferedReader);
                return readLine;
            } catch (Throwable th3) {
                th = th3;
                closeable = inputStreamReader;
                inputStreamReader = closeable2;
                closeable2 = closeable;
                jMsobIMeui.m2117a(closeable2);
                jMsobIMeui.m2117a(inputStreamReader);
                throw th;
            }
        } catch (IOException e3) {
            inputStreamReader = closeable2;
            bufferedReader = closeable2;
            jMsobIMeui.m2117a(inputStreamReader);
            jMsobIMeui.m2117a(bufferedReader);
            return readLine;
        } catch (Throwable th4) {
            th = th4;
            inputStreamReader = closeable2;
            jMsobIMeui.m2117a(closeable2);
            jMsobIMeui.m2117a(inputStreamReader);
            throw th;
        }
        return readLine;
    }

    /* renamed from: a */
    private static void m2117a(Closeable closeable) {
        if (closeable != null) {
            try {
                closeable.close();
            } catch (IOException e) {
                f2218a.mo4387a("Failed to close the stream: " + e.getMessage());
            }
        }
    }
}
