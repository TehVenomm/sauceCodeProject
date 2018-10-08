package com.zopim.android.sdk.chatlog;

import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;
import com.zopim.android.sdk.C0785R;
import com.zopim.android.sdk.model.ChatLog.Rating;

final class ChatRatingHolder extends ViewHolder {
    /* renamed from: e */
    private static final String f718e = ChatRatingHolder.class.getSimpleName();
    /* renamed from: a */
    OnClickListener f719a = new C0848o(this);
    /* renamed from: b */
    OnClickListener f720b = new C0849p(this);
    /* renamed from: c */
    OnClickListener f721c = new C0850q(this);
    /* renamed from: d */
    OnClickListener f722d = new C0851r(this);
    /* renamed from: f */
    private RadioGroup f723f;
    /* renamed from: g */
    private RadioButton f724g;
    /* renamed from: h */
    private RadioButton f725h;
    /* renamed from: i */
    private View f726i;
    /* renamed from: j */
    private View f727j;
    /* renamed from: k */
    private Listener f728k;
    /* renamed from: l */
    private TextView f729l;
    /* renamed from: m */
    private C0853t f730m;

    public interface Listener {
        void onRating(Rating rating);
    }

    public ChatRatingHolder(View view, Listener listener) {
        super(view);
        this.f723f = (RadioGroup) view.findViewById(C0785R.id.rating_button_group);
        this.f724g = (RadioButton) view.findViewById(C0785R.id.positive_button);
        this.f725h = (RadioButton) view.findViewById(C0785R.id.negative_button);
        this.f726i = view.findViewById(C0785R.id.add_comment_button);
        this.f727j = view.findViewById(C0785R.id.edit_comment_button);
        this.f729l = (TextView) view.findViewById(C0785R.id.comment_message);
        this.f724g.setOnClickListener(this.f719a);
        this.f725h.setOnClickListener(this.f720b);
        this.f726i.setOnClickListener(this.f721c);
        this.f727j.setOnClickListener(this.f722d);
        this.f728k = listener;
    }

    /* renamed from: a */
    public void m661a(C0853t c0853t) {
        boolean z = true;
        if (c0853t == null) {
            Log.e(f718e, "Item must not be null");
            return;
        }
        this.f730m = c0853t;
        switch (C0852s.f830a[c0853t.f831a.ordinal()]) {
            case 1:
                this.f724g.setChecked(true);
                this.f725h.setChecked(false);
                this.f726i.setVisibility(0);
                break;
            case 2:
                this.f724g.setChecked(false);
                this.f725h.setChecked(true);
                this.f726i.setVisibility(0);
                break;
            default:
                this.f724g.setChecked(false);
                this.f725h.setChecked(false);
                this.f726i.setVisibility(4);
                break;
        }
        if (c0853t.f832b == null || c0853t.f832b.isEmpty()) {
            z = false;
        }
        if (z) {
            this.f726i.setVisibility(8);
            this.f727j.setVisibility(0);
            this.f729l.setVisibility(0);
            this.f729l.setText(c0853t.f832b);
            return;
        }
        this.f727j.setVisibility(8);
        this.f729l.setVisibility(8);
    }
}
