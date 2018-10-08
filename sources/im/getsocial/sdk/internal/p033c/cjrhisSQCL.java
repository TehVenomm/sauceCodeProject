package im.getsocial.sdk.internal.p033c;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import com.google.android.gms.drive.DriveFile;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.c.cjrhisSQCL */
public class cjrhisSQCL implements ruWsnwUPKh {
    /* renamed from: a */
    private final Context f1256a;
    /* renamed from: b */
    private final jjbQypPegg f1257b;

    @XdbacJlTDQ
    cjrhisSQCL(Context context, jjbQypPegg jjbqyppegg) {
        this.f1256a = context;
        this.f1257b = jjbqyppegg;
    }

    /* renamed from: a */
    public final void mo4381a(final String str) {
        this.f1257b.m1244a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ cjrhisSQCL f1255b;

            public void run() {
                Intent intent = new Intent("android.intent.action.VIEW", Uri.parse(str));
                intent.addFlags(DriveFile.MODE_READ_ONLY);
                this.f1255b.f1256a.startActivity(intent);
            }
        });
    }
}
