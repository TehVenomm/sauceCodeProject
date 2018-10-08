package im.getsocial.sdk.invites;

import android.content.ContentProvider;
import android.content.ContentValues;
import android.content.Context;
import android.content.UriMatcher;
import android.database.Cursor;
import android.net.Uri;
import android.os.ParcelFileDescriptor;
import com.google.android.gms.drive.DriveFile;
import im.getsocial.sdk.internal.p089m.KSZKMmRWhZ;
import java.io.File;

public class ImageContentProvider extends ContentProvider {
    /* renamed from: a */
    private static final UriMatcher f2256a = new UriMatcher(-1);

    /* renamed from: a */
    private ParcelFileDescriptor m2220a(String str) {
        Context context = getContext();
        return context == null ? null : ParcelFileDescriptor.open(new File(context.getCacheDir(), str), DriveFile.MODE_READ_ONLY);
    }

    public int delete(Uri uri, String str, String[] strArr) {
        return 0;
    }

    public String[] getStreamTypes(Uri uri, String str) {
        if (uri.toString().endsWith("jpg")) {
            return new String[]{"image/jpg", "image/jpeg"};
        } else if (uri.toString().endsWith("mp4")) {
            return new String[]{"video/mp4"};
        } else if (!uri.toString().endsWith("gif")) {
            return new String[0];
        } else {
            return new String[]{"image/gif"};
        }
    }

    public String getType(Uri uri) {
        return null;
    }

    public Uri insert(Uri uri, ContentValues contentValues) {
        return null;
    }

    public boolean onCreate() {
        f2256a.addURI(KSZKMmRWhZ.m2108a(getContext()), "smart-invite.jpg", 1);
        f2256a.addURI(KSZKMmRWhZ.m2108a(getContext()), "smart-invite-video.mp4", 2);
        f2256a.addURI(KSZKMmRWhZ.m2108a(getContext()), "smart-invite-gif.gif", 3);
        return false;
    }

    public ParcelFileDescriptor openFile(Uri uri, String str) {
        switch (f2256a.match(uri)) {
            case 1:
                return m2220a("getsocial-smartinvite-tempimage.jpg");
            case 2:
                return m2220a("getsocial-smartinvite-tempvideo.mp4");
            case 3:
                return m2220a("getsocial-smartinvite-tempgif.gif");
            default:
                return null;
        }
    }

    public Cursor query(Uri uri, String[] strArr, String str, String[] strArr2, String str2) {
        return null;
    }

    public int update(Uri uri, ContentValues contentValues, String str, String[] strArr) {
        return 0;
    }
}
