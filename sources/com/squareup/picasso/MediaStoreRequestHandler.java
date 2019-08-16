package com.squareup.picasso;

import android.content.ContentResolver;
import android.content.ContentUris;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory.Options;
import android.net.Uri;
import android.provider.MediaStore.Images;
import android.provider.MediaStore.Video.Thumbnails;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import com.squareup.picasso.Picasso.LoadedFrom;
import com.squareup.picasso.RequestHandler.Result;
import java.io.IOException;

class MediaStoreRequestHandler extends ContentStreamRequestHandler {
    private static final String[] CONTENT_ORIENTATION = {"orientation"};

    enum PicassoKind {
        MICRO(3, 96, 96),
        MINI(1, 512, 384),
        FULL(2, -1, -1);
        
        final int androidKind;
        final int height;
        final int width;

        private PicassoKind(int i, int i2, int i3) {
            this.androidKind = i;
            this.width = i2;
            this.height = i3;
        }
    }

    MediaStoreRequestHandler(Context context) {
        super(context);
    }

    public boolean canHandleRequest(Request request) {
        Uri uri = request.uri;
        return Param.CONTENT.equals(uri.getScheme()) && "media".equals(uri.getAuthority());
    }

    public Result load(Request request, int i) throws IOException {
        Bitmap thumbnail;
        ContentResolver contentResolver = this.context.getContentResolver();
        int exifOrientation = getExifOrientation(contentResolver, request.uri);
        String type = contentResolver.getType(request.uri);
        boolean z = type != null && type.startsWith("video/");
        if (request.hasSize()) {
            PicassoKind picassoKind = getPicassoKind(request.targetWidth, request.targetHeight);
            if (!z && picassoKind == PicassoKind.FULL) {
                return new Result(null, getInputStream(request), LoadedFrom.DISK, exifOrientation);
            }
            long parseId = ContentUris.parseId(request.uri);
            Options createBitmapOptions = createBitmapOptions(request);
            createBitmapOptions.inJustDecodeBounds = true;
            calculateInSampleSize(request.targetWidth, request.targetHeight, picassoKind.width, picassoKind.height, createBitmapOptions, request);
            if (z) {
                thumbnail = Thumbnails.getThumbnail(contentResolver, parseId, picassoKind == PicassoKind.FULL ? 1 : picassoKind.androidKind, createBitmapOptions);
            } else {
                thumbnail = Images.Thumbnails.getThumbnail(contentResolver, parseId, picassoKind.androidKind, createBitmapOptions);
            }
            if (thumbnail != null) {
                return new Result(thumbnail, null, LoadedFrom.DISK, exifOrientation);
            }
        }
        return new Result(null, getInputStream(request), LoadedFrom.DISK, exifOrientation);
    }

    static PicassoKind getPicassoKind(int i, int i2) {
        if (i <= PicassoKind.MICRO.width && i2 <= PicassoKind.MICRO.height) {
            return PicassoKind.MICRO;
        }
        if (i > PicassoKind.MINI.width || i2 > PicassoKind.MINI.height) {
            return PicassoKind.FULL;
        }
        return PicassoKind.MINI;
    }

    /* JADX WARNING: Removed duplicated region for block: B:23:0x0034  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static int getExifOrientation(android.content.ContentResolver r8, android.net.Uri r9) {
        /*
            r6 = 0
            r7 = 0
            java.lang.String[] r2 = CONTENT_ORIENTATION     // Catch:{ RuntimeException -> 0x0027, all -> 0x0030 }
            r3 = 0
            r4 = 0
            r5 = 0
            r0 = r8
            r1 = r9
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5)     // Catch:{ RuntimeException -> 0x0027, all -> 0x0030 }
            if (r1 == 0) goto L_0x0015
            boolean r0 = r1.moveToFirst()     // Catch:{ RuntimeException -> 0x003a, all -> 0x0038 }
            if (r0 != 0) goto L_0x001c
        L_0x0015:
            if (r1 == 0) goto L_0x001a
            r1.close()
        L_0x001a:
            r0 = r6
        L_0x001b:
            return r0
        L_0x001c:
            r0 = 0
            int r0 = r1.getInt(r0)     // Catch:{ RuntimeException -> 0x003a, all -> 0x0038 }
            if (r1 == 0) goto L_0x001b
            r1.close()
            goto L_0x001b
        L_0x0027:
            r0 = move-exception
            r0 = r7
        L_0x0029:
            if (r0 == 0) goto L_0x002e
            r0.close()
        L_0x002e:
            r0 = r6
            goto L_0x001b
        L_0x0030:
            r0 = move-exception
            r1 = r7
        L_0x0032:
            if (r1 == 0) goto L_0x0037
            r1.close()
        L_0x0037:
            throw r0
        L_0x0038:
            r0 = move-exception
            goto L_0x0032
        L_0x003a:
            r0 = move-exception
            r0 = r1
            goto L_0x0029
        */
        throw new UnsupportedOperationException("Method not decompiled: com.squareup.picasso.MediaStoreRequestHandler.getExifOrientation(android.content.ContentResolver, android.net.Uri):int");
    }
}
