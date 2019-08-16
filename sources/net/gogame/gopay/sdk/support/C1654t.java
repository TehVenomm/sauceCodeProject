package net.gogame.gopay.sdk.support;

import android.os.AsyncTask;
import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.net.URL;

/* renamed from: net.gogame.gopay.sdk.support.t */
public final class C1654t extends AsyncTask {

    /* renamed from: a */
    String f1307a;

    /* renamed from: b */
    C1655u f1308b;

    public C1654t(String str, C1655u uVar) {
        this.f1307a = str;
        this.f1308b = uVar;
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public Boolean doInBackground(String... strArr) {
        try {
            String str = this.f1307a + "/data.zip";
            URL url = new URL(strArr[0]);
            url.openConnection().connect();
            BufferedInputStream bufferedInputStream = new BufferedInputStream(url.openStream());
            FileOutputStream fileOutputStream = new FileOutputStream(str);
            byte[] bArr = new byte[1024];
            while (true) {
                int read = bufferedInputStream.read(bArr);
                if (read != -1) {
                    fileOutputStream.write(bArr, 0, read);
                } else {
                    fileOutputStream.flush();
                    fileOutputStream.close();
                    bufferedInputStream.close();
                    C1656v.m965a(str, this.f1307a);
                    new File(str).delete();
                    return Boolean.valueOf(true);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            return Boolean.valueOf(false);
        }
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ void onPostExecute(Object obj) {
        Boolean bool = (Boolean) obj;
        super.onPostExecute(bool);
        if (this.f1308b != null) {
            C1655u uVar = this.f1308b;
            bool.booleanValue();
            uVar.mo22683a();
        }
    }

    /* access modifiers changed from: protected */
    public final void onPreExecute() {
        super.onPreExecute();
    }
}
