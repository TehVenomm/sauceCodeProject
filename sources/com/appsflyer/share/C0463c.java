package com.appsflyer.share;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import com.google.android.gms.drive.DriveFile;

/* renamed from: com.appsflyer.share.c */
final class C0463c {

    /* renamed from: ˎ */
    private String f340;

    C0463c() {
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˏ */
    public final void mo6632(String str) {
        this.f340 = str;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˏ */
    public final void mo6631(Context context) {
        if (this.f340 != null) {
            context.startActivity(new Intent("android.intent.action.VIEW", Uri.parse(this.f340)).setFlags(DriveFile.MODE_READ_ONLY));
        }
    }
}
