package com.zopim.android.sdk.attachment.ui;

import android.support.v4.app.Fragment;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import com.zopim.android.sdk.attachment.ImagePicker;
import com.zopim.android.sdk.attachment.ui.AttachmentSourceSelectorDialog.C0829c;

/* renamed from: com.zopim.android.sdk.attachment.ui.a */
class C0830a implements OnItemClickListener {
    /* renamed from: a */
    final /* synthetic */ Fragment f700a;
    /* renamed from: b */
    final /* synthetic */ AttachmentSourceSelectorDialog f701b;

    C0830a(AttachmentSourceSelectorDialog attachmentSourceSelectorDialog, Fragment fragment) {
        this.f701b = attachmentSourceSelectorDialog;
        this.f700a = fragment;
    }

    public void onItemClick(AdapterView<?> adapterView, View view, int i, long j) {
        switch (C0831b.f702a[((C0829c) adapterView.getAdapter().getItem(i)).m646c().ordinal()]) {
            case 1:
                ImagePicker.INSTANCE.pickImagesFromGallery(this.f700a);
                this.f701b.dismiss();
                return;
            case 2:
                ImagePicker.INSTANCE.pickImageFromCamera(this.f700a);
                this.f701b.dismiss();
                return;
            default:
                return;
        }
    }
}
