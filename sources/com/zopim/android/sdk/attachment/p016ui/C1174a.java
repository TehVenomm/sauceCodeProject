package com.zopim.android.sdk.attachment.p016ui;

import android.support.p000v4.app.Fragment;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import com.zopim.android.sdk.attachment.ImagePicker;

/* renamed from: com.zopim.android.sdk.attachment.ui.a */
class C1174a implements OnItemClickListener {

    /* renamed from: a */
    final /* synthetic */ Fragment f744a;

    /* renamed from: b */
    final /* synthetic */ AttachmentSourceSelectorDialog f745b;

    C1174a(AttachmentSourceSelectorDialog attachmentSourceSelectorDialog, Fragment fragment) {
        this.f745b = attachmentSourceSelectorDialog;
        this.f744a = fragment;
    }

    public void onItemClick(AdapterView<?> adapterView, View view, int i, long j) {
        switch (C1175b.f746a[((C1173c) adapterView.getAdapter().getItem(i)).mo20703c().ordinal()]) {
            case 1:
                ImagePicker.INSTANCE.pickImagesFromGallery(this.f744a);
                this.f745b.dismiss();
                return;
            case 2:
                ImagePicker.INSTANCE.pickImageFromCamera(this.f744a);
                this.f745b.dismiss();
                return;
            default:
                return;
        }
    }
}
