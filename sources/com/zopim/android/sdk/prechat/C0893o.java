package com.zopim.android.sdk.prechat;

import com.zopim.android.sdk.data.observers.FormsObserver;
import com.zopim.android.sdk.model.Forms;
import com.zopim.android.sdk.model.Forms.OfflineForm;

/* renamed from: com.zopim.android.sdk.prechat.o */
class C0893o extends FormsObserver {
    /* renamed from: a */
    final /* synthetic */ ZopimOfflineFragment f910a;

    C0893o(ZopimOfflineFragment zopimOfflineFragment) {
        this.f910a = zopimOfflineFragment;
    }

    public void update(Forms forms) {
        OfflineForm offlineForm = forms.getOfflineForm();
        if (offlineForm != null && offlineForm.getFormSubmitted() == null) {
            this.f910a.mHandler.post(new C0894p(this));
        }
    }
}
