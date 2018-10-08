package im.getsocial.sdk;

import android.app.Application;
import android.content.ContentProvider;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p069d.jjbQypPegg;

public class AutoInitSdkContentProvider extends ContentProvider {
    /* renamed from: a */
    private static final cjrhisSQCL f1098a = upgqDBbsrL.m1274a(AutoInitSdkContentProvider.class);

    public int delete(Uri uri, String str, String[] strArr) {
        return 0;
    }

    public String getType(Uri uri) {
        return null;
    }

    public Uri insert(Uri uri, ContentValues contentValues) {
        return null;
    }

    public boolean onCreate() {
        Context context = getContext();
        if (context == null) {
            f1098a.mo4396d("Context is null! Android OS bug. Report the OS version and device model.");
        } else {
            new jjbQypPegg().m1576a((Application) context.getApplicationContext());
        }
        return false;
    }

    public Cursor query(Uri uri, String[] strArr, String str, String[] strArr2, String str2) {
        return null;
    }

    public int update(Uri uri, ContentValues contentValues, String str, String[] strArr) {
        return 0;
    }
}
