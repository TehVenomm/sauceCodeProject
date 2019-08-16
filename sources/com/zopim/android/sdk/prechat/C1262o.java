package com.zopim.android.sdk.prechat;

import com.zopim.android.sdk.data.observers.FormsObserver;
import com.zopim.android.sdk.model.Forms;
import com.zopim.android.sdk.model.Forms.OfflineForm;

/* renamed from: com.zopim.android.sdk.prechat.o */
class C1262o extends FormsObserver {

    /* renamed from: a */
    final /* synthetic */ ZopimOfflineFragment f954a;

    C1262o(ZopimOfflineFragment zopimOfflineFragment) {
        this.f954a = zopimOfflineFragment;
    }

    public void update(Forms forms) {
        OfflineForm offlineForm = forms.getOfflineForm();
        if (offlineForm != null && offlineForm.getFormSubmitted() == null) {
            this.f954a.mHandler.post(new C1263p(this));
        }
    }
}
