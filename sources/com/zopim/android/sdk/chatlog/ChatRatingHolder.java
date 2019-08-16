package com.zopim.android.sdk.chatlog;

import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.model.ChatLog.Rating;

final class ChatRatingHolder extends ViewHolder {

    /* renamed from: e */
    private static final String f762e = ChatRatingHolder.class.getSimpleName();

    /* renamed from: a */
    OnClickListener f763a = new C1217o(this);

    /* renamed from: b */
    OnClickListener f764b = new C1218p(this);

    /* renamed from: c */
    OnClickListener f765c = new C1219q(this);

    /* renamed from: d */
    OnClickListener f766d = new C1220r(this);
    /* access modifiers changed from: private */

    /* renamed from: f */
    public RadioGroup f767f;

    /* renamed from: g */
    private RadioButton f768g;

    /* renamed from: h */
    private RadioButton f769h;

    /* renamed from: i */
    private View f770i;

    /* renamed from: j */
    private View f771j;
    /* access modifiers changed from: private */

    /* renamed from: k */
    public Listener f772k;
    /* access modifiers changed from: private */

    /* renamed from: l */
    public TextView f773l;
    /* access modifiers changed from: private */

    /* renamed from: m */
    public C1222t f774m;

    public interface Listener {
        void onRating(Rating rating);
    }

    public ChatRatingHolder(View view, Listener listener) {
        super(view);
        this.f767f = (RadioGroup) view.findViewById(C1122R.C1125id.rating_button_group);
        this.f768g = (RadioButton) view.findViewById(C1122R.C1125id.positive_button);
        this.f769h = (RadioButton) view.findViewById(C1122R.C1125id.negative_button);
        this.f770i = view.findViewById(C1122R.C1125id.add_comment_button);
        this.f771j = view.findViewById(C1122R.C1125id.edit_comment_button);
        this.f773l = (TextView) view.findViewById(C1122R.C1125id.comment_message);
        this.f768g.setOnClickListener(this.f763a);
        this.f769h.setOnClickListener(this.f764b);
        this.f770i.setOnClickListener(this.f765c);
        this.f771j.setOnClickListener(this.f766d);
        this.f772k = listener;
    }

    /* renamed from: a */
    public void mo20709a(C1222t tVar) {
        boolean z = true;
        if (tVar == null) {
            Log.e(f762e, "Item must not be null");
            return;
        }
        this.f774m = tVar;
        switch (C1221s.f874a[tVar.f875a.ordinal()]) {
            case 1:
                this.f768g.setChecked(true);
                this.f769h.setChecked(false);
                this.f770i.setVisibility(0);
                break;
            case 2:
                this.f768g.setChecked(false);
                this.f769h.setChecked(true);
                this.f770i.setVisibility(0);
                break;
            default:
                this.f768g.setChecked(false);
                this.f769h.setChecked(false);
                this.f770i.setVisibility(4);
                break;
        }
        if (tVar.f876b == null || tVar.f876b.isEmpty()) {
            z = false;
        }
        if (z) {
            this.f770i.setVisibility(8);
            this.f771j.setVisibility(0);
            this.f773l.setVisibility(0);
            this.f773l.setText(tVar.f876b);
            return;
        }
        this.f771j.setVisibility(8);
        this.f773l.setVisibility(8);
    }
}
