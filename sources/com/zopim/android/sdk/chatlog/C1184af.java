package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.attachment.FileExtension;

/* renamed from: com.zopim.android.sdk.chatlog.af */
/* synthetic */ class C1184af {

    /* renamed from: a */
    static final /* synthetic */ int[] f818a = new int[FileExtension.values().length];

    static {
        try {
            f818a[FileExtension.PDF.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f818a[FileExtension.TXT.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f818a[FileExtension.JPG.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f818a[FileExtension.JPEG.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
        try {
            f818a[FileExtension.PNG.ordinal()] = 5;
        } catch (NoSuchFieldError e5) {
        }
    }
}
