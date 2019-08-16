package com.zopim.android.sdk.chatlog;

import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import com.zopim.android.sdk.C1122R;

/* renamed from: com.zopim.android.sdk.chatlog.h */
final class C1210h extends ViewHolder {

    /* renamed from: a */
    private static final String f856a = C1210h.class.getSimpleName();

    /* renamed from: b */
    private TextView f857b;

    public C1210h(View view) {
        super(view);
        this.f857b = (TextView) view.findViewById(C1122R.C1125id.message_text);
    }

    /* renamed from: a */
    public void mo20755a(C1178aa aaVar) {
        if (aaVar == null) {
            Log.e(f856a, "Item must not be null");
        } else {
            this.f857b.setText(aaVar.f795i);
        }
    }
}
