package net.gogame.gopay.sdk.support;

import android.os.AsyncTask;
import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.URL;

/* renamed from: net.gogame.gopay.sdk.support.t */
public final class C1405t extends AsyncTask {
    /* renamed from: a */
    String f3638a;
    /* renamed from: b */
    C1354u f3639b;

    public C1405t(String str, C1354u c1354u) {
        this.f3638a = str;
        this.f3639b = c1354u;
    }

    /* renamed from: a */
    private Boolean m3975a(String... strArr) {
        try {
            String str = this.f3638a + "/data.zip";
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
                    C1406v.m3976a(str, this.f3638a);
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
        return m3975a((String[]) objArr);
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        Boolean bool = (Boolean) obj;
        super.onPostExecute(bool);
        if (this.f3639b != null) {
            C1354u c1354u = this.f3639b;
            bool.booleanValue();
            c1354u.mo4872a();
        }
    }

    protected final void onPreExecute() {
        super.onPreExecute();
    }
}
