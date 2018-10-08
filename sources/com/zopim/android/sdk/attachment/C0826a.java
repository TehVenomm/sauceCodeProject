package com.zopim.android.sdk.attachment;

import java.io.File;
import java.util.ArrayList;

/* renamed from: com.zopim.android.sdk.attachment.a */
class C0826a extends ArrayList<File> {
    /* renamed from: a */
    final /* synthetic */ File f689a;
    /* renamed from: b */
    final /* synthetic */ ImagePicker f690b;

    C0826a(ImagePicker imagePicker, File file) {
        this.f690b = imagePicker;
        this.f689a = file;
        add(this.f689a);
    }
}
