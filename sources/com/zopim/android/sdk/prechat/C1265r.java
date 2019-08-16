package com.zopim.android.sdk.prechat;

import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.TextView;
import com.zopim.android.sdk.C1122R;

/* renamed from: com.zopim.android.sdk.prechat.r */
class C1265r implements OnItemSelectedListener {

    /* renamed from: a */
    final /* synthetic */ ZopimPreChatFragment f957a;

    C1265r(ZopimPreChatFragment zopimPreChatFragment) {
        this.f957a = zopimPreChatFragment;
    }

    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long j) {
        if (i <= this.f957a.mDepartmentSpinner.getCount() - 1 && (view instanceof TextView)) {
            ((TextView) view).setTextAppearance(this.f957a.getActivity(), C1122R.style.pre_chat_form_selected_item);
        }
    }

    public void onNothingSelected(AdapterView<?> adapterView) {
    }
}
