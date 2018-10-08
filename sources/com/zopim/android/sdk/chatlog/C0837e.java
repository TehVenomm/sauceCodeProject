package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.attachment.FileExtension;

/* renamed from: com.zopim.android.sdk.chatlog.e */
/* synthetic */ class C0837e {
    /* renamed from: a */
    static final /* synthetic */ int[] f805a = new int[FileExtension.values().length];

    static {
        try {
            f805a[FileExtension.PDF.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f805a[FileExtension.TXT.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f805a[FileExtension.JPG.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f805a[FileExtension.JPEG.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
        try {
            f805a[FileExtension.PNG.ordinal()] = 5;
        } catch (NoSuchFieldError e5) {
        }
    }
}
