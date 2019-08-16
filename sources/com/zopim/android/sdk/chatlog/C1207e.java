package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.attachment.FileExtension;

/* renamed from: com.zopim.android.sdk.chatlog.e */
/* synthetic */ class C1207e {

    /* renamed from: a */
    static final /* synthetic */ int[] f849a = new int[FileExtension.values().length];

    static {
        try {
            f849a[FileExtension.PDF.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f849a[FileExtension.TXT.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f849a[FileExtension.JPG.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f849a[FileExtension.JPEG.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
        try {
            f849a[FileExtension.PNG.ordinal()] = 5;
        } catch (NoSuchFieldError e5) {
        }
    }
}
