package im.getsocial.sdk.ui.internal.p125h;

import android.content.Context;
import android.graphics.Typeface;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.HashMap;
import java.util.Map;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

/* renamed from: im.getsocial.sdk.ui.internal.h.fOrCGNYyfk */
public final class fOrCGNYyfk {
    /* renamed from: a */
    private static final cjrhisSQCL f2974a = upgqDBbsrL.m1274a(fOrCGNYyfk.class);
    /* renamed from: b */
    private static final Map<String, Typeface> f2975b = new HashMap();

    private fOrCGNYyfk() {
    }

    /* renamed from: a */
    public static Typeface m3329a(Context context, KluUZYuxme kluUZYuxme) {
        Typeface typeface;
        int i = 1;
        String b = kluUZYuxme.m3155b();
        if (b == null) {
            throw new RuntimeException("Typeface fontname null");
        } else if (f2975b.containsKey(b)) {
            return (Typeface) f2975b.get(b);
        } else {
            if ((b.lastIndexOf(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER) != -1 ? 1 : 0) != 0) {
                typeface = null;
                InputStream a = XdbacJlTDQ.m3328a(context, b, im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3211b(), "getsocial-ui-internal");
                if (a != null) {
                    typeface = fOrCGNYyfk.m3330a(a);
                }
                f2975b.put(b, typeface);
                return typeface;
            }
            try {
                String[] split = b.split("-");
                String str = split[1];
                String str2 = split[0];
                if (!"bold".equalsIgnoreCase(str)) {
                    i = "italic".equalsIgnoreCase(str) ? 2 : "bold_italic".equalsIgnoreCase(str) ? 3 : 0;
                }
                return Typeface.create(str2, i);
            } catch (Exception e) {
                f2974a.mo4387a("Failed to load font " + b + ", exception: " + e.getMessage());
                typeface = Typeface.create(b, 0);
                f2975b.put(b, typeface);
                return typeface;
            }
        }
    }

    /* renamed from: a */
    private static Typeface m3330a(InputStream inputStream) {
        FileOutputStream fileOutputStream;
        Typeface createFromFile;
        Throwable th;
        Throwable th2;
        try {
            File createTempFile = File.createTempFile("temp", null);
            fileOutputStream = new FileOutputStream(createTempFile);
            try {
                byte[] bArr = new byte[1024];
                while (inputStream.read(bArr) > 0) {
                    fileOutputStream.write(bArr);
                }
                createFromFile = Typeface.createFromFile(createTempFile);
            } catch (Throwable th3) {
                th = th3;
                createFromFile = null;
                th2 = th;
                f2974a.mo4396d("Failed to load typeface, error: " + th2.getMessage());
                if (fileOutputStream != null) {
                    try {
                        fileOutputStream.close();
                    } catch (IOException e) {
                        f2974a.mo4396d("Failed to close stream: " + e.getMessage());
                    }
                }
                return createFromFile;
            }
            try {
                fileOutputStream.close();
                if (!createTempFile.delete()) {
                    f2974a.mo4387a("Failed to delete file");
                }
            } catch (Throwable th4) {
                th2 = th4;
                f2974a.mo4396d("Failed to load typeface, error: " + th2.getMessage());
                if (fileOutputStream != null) {
                    fileOutputStream.close();
                }
                return createFromFile;
            }
        } catch (Throwable th5) {
            createFromFile = null;
            th = th5;
            fileOutputStream = null;
            th2 = th;
            f2974a.mo4396d("Failed to load typeface, error: " + th2.getMessage());
            if (fileOutputStream != null) {
                fileOutputStream.close();
            }
            return createFromFile;
        }
        return createFromFile;
    }

    /* renamed from: b */
    public static Typeface m3331b(Context context, KluUZYuxme kluUZYuxme) {
        try {
            return fOrCGNYyfk.m3329a(context, kluUZYuxme);
        } catch (IOException e) {
            return Typeface.DEFAULT;
        }
    }
}
