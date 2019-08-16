package net.gogame.gowrap.wrapper;

import android.graphics.Bitmap;
import android.os.AsyncTask;
import android.widget.ImageView;

public class DownloadImageTask extends AsyncTask<String, Void, Bitmap> {
    private ImageView imageView;

    public DownloadImageTask(ImageView imageView2) {
        this.imageView = imageView2;
    }

    /* JADX WARNING: type inference failed for: r2v0, types: [java.io.InputStream] */
    /* JADX WARNING: type inference failed for: r2v1 */
    /* JADX WARNING: type inference failed for: r2v2, types: [java.io.InputStream] */
    /* JADX WARNING: type inference failed for: r2v3 */
    /* JADX WARNING: type inference failed for: r2v6 */
    /* JADX WARNING: type inference failed for: r2v7 */
    /* access modifiers changed from: protected */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Removed duplicated region for block: B:21:0x002c A[SYNTHETIC, Splitter:B:21:0x002c] */
    /* JADX WARNING: Unknown variable types count: 2 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public android.graphics.Bitmap doInBackground(java.lang.String... r6) {
        /*
            r5 = this;
            r0 = 0
            r1 = 0
            r1 = r6[r1]
            java.net.URL r2 = new java.net.URL     // Catch:{ Exception -> 0x0017, all -> 0x0028 }
            r2.<init>(r1)     // Catch:{ Exception -> 0x0017, all -> 0x0028 }
            java.io.InputStream r2 = r2.openStream()     // Catch:{ Exception -> 0x0017, all -> 0x0028 }
            android.graphics.Bitmap r0 = android.graphics.BitmapFactory.decodeStream(r2)     // Catch:{ Exception -> 0x0037 }
            if (r2 == 0) goto L_0x0016
            r2.close()     // Catch:{ Exception -> 0x0030 }
        L_0x0016:
            return r0
        L_0x0017:
            r1 = move-exception
            r2 = r0
        L_0x0019:
            java.lang.String r3 = "goWrap"
            java.lang.String r4 = "Error"
            android.util.Log.e(r3, r4, r1)     // Catch:{ all -> 0x0034 }
            if (r2 == 0) goto L_0x0016
            r2.close()     // Catch:{ Exception -> 0x0026 }
            goto L_0x0016
        L_0x0026:
            r1 = move-exception
            goto L_0x0016
        L_0x0028:
            r1 = move-exception
            r2 = r0
        L_0x002a:
            if (r2 == 0) goto L_0x002f
            r2.close()     // Catch:{ Exception -> 0x0032 }
        L_0x002f:
            throw r1
        L_0x0030:
            r1 = move-exception
            goto L_0x0016
        L_0x0032:
            r0 = move-exception
            goto L_0x002f
        L_0x0034:
            r0 = move-exception
            r1 = r0
            goto L_0x002a
        L_0x0037:
            r1 = move-exception
            goto L_0x0019
        */
        throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.wrapper.DownloadImageTask.doInBackground(java.lang.String[]):android.graphics.Bitmap");
    }

    /* access modifiers changed from: protected */
    public void onPostExecute(Bitmap bitmap) {
        this.imageView.setImageBitmap(bitmap);
    }
}
