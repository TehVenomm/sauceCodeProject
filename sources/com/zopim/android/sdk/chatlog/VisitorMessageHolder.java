package com.zopim.android.sdk.chatlog;

import android.content.Intent;
import android.net.Uri;
import android.support.p000v4.widget.ContentLoadingProgressBar;
import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import com.squareup.picasso.Picasso;
import com.squareup.picasso.Transformation;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.attachment.FileExtension;
import com.zopim.android.sdk.util.CropSquareTransform;

final class VisitorMessageHolder extends ViewHolder {
    /* access modifiers changed from: private */

    /* renamed from: k */
    public static final String f775k = VisitorMessageHolder.class.getSimpleName();

    /* renamed from: a */
    public View f776a;

    /* renamed from: b */
    public TextView f777b;

    /* renamed from: c */
    public ImageView f778c;

    /* renamed from: d */
    public TextView f779d;

    /* renamed from: e */
    public View f780e;

    /* renamed from: f */
    public ImageView f781f;

    /* renamed from: g */
    public ContentLoadingProgressBar f782g;

    /* renamed from: h */
    public OnClickListener f783h;

    /* renamed from: i */
    public Intent f784i = new Intent("android.intent.action.VIEW");

    /* renamed from: j */
    android.view.View.OnClickListener f785j = new C1183ae(this);

    /* renamed from: l */
    private C1180ab f786l;

    public interface OnClickListener {
        void onClick(int i);
    }

    public VisitorMessageHolder(View view, OnClickListener onClickListener) {
        super(view);
        this.f776a = view.findViewById(C1122R.C1125id.message_container);
        this.f777b = (TextView) view.findViewById(C1122R.C1125id.message_text);
        this.f779d = (TextView) view.findViewById(C1122R.C1125id.send_failed_label);
        this.f778c = (ImageView) view.findViewById(C1122R.C1125id.send_failed_icon);
        this.f780e = view.findViewById(C1122R.C1125id.attachment_image_container);
        this.f781f = (ImageView) view.findViewById(C1122R.C1125id.attachment_thumbnail);
        this.f782g = (ContentLoadingProgressBar) view.findViewById(C1122R.C1125id.attachment_progress);
        this.f783h = onClickListener;
        this.f781f.setOnClickListener(this.f785j);
        this.f784i.setFlags(1073741824);
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m676a(int i) {
        switch (i) {
            case -1:
            case 0:
                this.f782g.setVisibility(4);
                return;
            case 100:
                this.f782g.setVisibility(4);
                return;
            default:
                this.f782g.setVisibility(0);
                return;
        }
    }

    /* renamed from: b */
    private void m678b(C1180ab abVar) {
        if (abVar != null && abVar.f809a != null) {
            switch (C1184af.f818a[FileExtension.getExtension(abVar.f809a).ordinal()]) {
                case 3:
                case 4:
                case 5:
                    Picasso.with(this.itemView.getContext()).load(abVar.f809a).error(C1122R.C1124drawable.ic_chat_default_avatar).placeholder(C1122R.C1124drawable.bg_picasso_placeholder).transform((Transformation) new CropSquareTransform()).into(this.f781f, new C1182ad(this, abVar));
                    this.f777b.setVisibility(8);
                    this.f780e.setVisibility(0);
                    this.f784i.setDataAndType(Uri.fromFile(abVar.f809a), "image/*");
                    return;
                default:
                    return;
            }
        }
    }

    /* renamed from: a */
    public void mo20715a(C1180ab abVar) {
        if (abVar == null) {
            Log.e(f775k, "Item must not be null");
            return;
        }
        this.f786l = abVar;
        if (abVar.f813e) {
            this.f778c.setVisibility(0);
            this.f779d.setVisibility(0);
            this.itemView.setOnClickListener(new C1181ac(this));
        } else {
            this.f779d.setVisibility(8);
            this.f778c.setVisibility(8);
            this.itemView.setOnClickListener(null);
        }
        if (abVar.f809a != null) {
            m678b(abVar);
            return;
        }
        this.f777b.setText(abVar.f795i);
        this.f777b.setVisibility(0);
        this.f780e.setVisibility(8);
    }
}
