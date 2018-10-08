package net.gogame.gopay.sdk.support;

import android.os.AsyncTask;
import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.URL;

/* renamed from: net.gogame.gopay.sdk.support.t */
public final class C1089t extends AsyncTask {
    /* renamed from: a */
    String f1250a;
    /* renamed from: b */
    C1038u f1251b;

    public C1089t(String str, C1038u c1038u) {
        this.f1250a = str;
        this.f1251b = c1038u;
    }

    /* renamed from: a */
    private Boolean m950a(String... strArr) {
        try {
            String str = this.f1250a + "/data.zip";
            URL url = new URL(strArr[0]);
            url.openConnection().connect();
            InputStream bufferedInputStream = new BufferedInputStream(url.openStream());
            OutputStream fileOutputStream = new FileOutputStream(str);
            byte[] bArr = new byte[1024];
            while (true) {
                int read = bufferedInputStream.read(bArr);
                if (read != -1) {
                    fileOutputStream.write(bArr, 0, read);
                } else {
                    fileOutputStream.flush();
                    fileOutputStream.close();
                    bufferedInputStream.close();
                    C1090v.m951a(str, this.f1250a);
                    new File(str).delete();
                    return Boolean.valueOf(true);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            return Boolean.valueOf(false);
        }
    }

    protected final /* synthetic */ Object doInBackground(Object[] objArr) {
        return m950a((String[]) objArr);
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        Boolean bool = (Boolean) obj;
        super.onPostExecute(bool);
        if (this.f1251b != null) {
            C1038u c1038u = this.f1251b;
            bool.booleanValue();
            c1038u.mo4424a();
        }
    }

    protected final void onPreExecute() {
        super.onPreExecute();
    }
}
