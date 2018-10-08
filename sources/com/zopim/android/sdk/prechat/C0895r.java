package com.zopim.android.sdk.prechat;

import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.TextView;
import com.zopim.android.sdk.C0784R;

/* renamed from: com.zopim.android.sdk.prechat.r */
class C0895r implements OnItemSelectedListener {
    /* renamed from: a */
    final /* synthetic */ ZopimPreChatFragment f913a;

    C0895r(ZopimPreChatFragment zopimPreChatFragment) {
        this.f913a = zopimPreChatFragment;
    }

    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long j) {
        if (i <= this.f913a.mDepartmentSpinner.getCount() - 1 && (view instanceof TextView)) {
            ((TextView) view).setTextAppearance(this.f913a.getActivity(), C0784R.style.pre_chat_form_selected_item);
        }
    }

    public void onNothingSelected(AdapterView<?> adapterView) {
    }
}
