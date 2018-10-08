package com.zopim.android.sdk.chatlog;

import android.content.Intent;
import android.net.Uri;
import android.support.v4.widget.ContentLoadingProgressBar;
import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import com.squareup.picasso.Picasso;
import com.zopim.android.sdk.C0784R;
import com.zopim.android.sdk.attachment.FileExtension;
import com.zopim.android.sdk.util.CropSquareTransform;

final class VisitorMessageHolder extends ViewHolder {
    /* renamed from: k */
    private static final String f731k = VisitorMessageHolder.class.getSimpleName();
    /* renamed from: a */
    public View f732a;
    /* renamed from: b */
    public TextView f733b;
    /* renamed from: c */
    public ImageView f734c;
    /* renamed from: d */
    public TextView f735d;
    /* renamed from: e */
    public View f736e;
    /* renamed from: f */
    public ImageView f737f;
    /* renamed from: g */
    public ContentLoadingProgressBar f738g;
    /* renamed from: h */
    public OnClickListener f739h;
    /* renamed from: i */
    public Intent f740i = new Intent("android.intent.action.VIEW");
    /* renamed from: j */
    android.view.View.OnClickListener f741j = new ae(this);
    /* renamed from: l */
    private ab f742l;

    public interface OnClickListener {
        void onClick(int i);
    }

    public VisitorMessageHolder(View view, OnClickListener onClickListener) {
        super(view);
        this.f732a = view.findViewById(C0784R.id.message_container);
        this.f733b = (TextView) view.findViewById(C0784R.id.message_text);
        this.f735d = (TextView) view.findViewById(C0784R.id.send_failed_label);
        this.f734c = (ImageView) view.findViewById(C0784R.id.send_failed_icon);
        this.f736e = view.findViewById(C0784R.id.attachment_image_container);
        this.f737f = (ImageView) view.findViewById(C0784R.id.attachment_thumbnail);
        this.f738g = (ContentLoadingProgressBar) view.findViewById(C0784R.id.attachment_progress);
        this.f739h = onClickListener;
        this.f737f.setOnClickListener(this.f741j);
        this.f740i.setFlags(1073741824);
    }

    /* renamed from: a */
    private void m663a(int i) {
        switch (i) {
            case -1:
            case 0:
                this.f738g.setVisibility(4);
                return;
            case 100:
                this.f738g.setVisibility(4);
                return;
            default:
                this.f738g.setVisibility(0);
                return;
        }
    }

    /* renamed from: b */
    private void m665b(ab abVar) {
        if (abVar != null && abVar.f765a != null) {
            switch (af.f774a[FileExtension.getExtension(abVar.f765a).ordinal()]) {
                case 3:
                case 4:
                case 5:
                    Picasso.with(this.itemView.getContext()).load(abVar.f765a).error(C0784R.drawable.ic_chat_default_avatar).placeholder(C0784R.drawable.bg_picasso_placeholder).transform(new CropSquareTransform()).into(this.f737f, new ad(this, abVar));
                    this.f733b.setVisibility(8);
                    this.f736e.setVisibility(0);
                    this.f740i.setDataAndType(Uri.fromFile(abVar.f765a), "image/*");
                    return;
                default:
                    return;
            }
        }
    }

    /* renamed from: a */
    public void m666a(ab abVar) {
        if (abVar == null) {
            Log.e(f731k, "Item must not be null");
            return;
        }
        this.f742l = abVar;
        if (abVar.f769e) {
            this.f734c.setVisibility(0);
            this.f735d.setVisibility(0);
            this.itemView.setOnClickListener(new ac(this));
        } else {
            this.f735d.setVisibility(8);
            this.f734c.setVisibility(8);
            this.itemView.setOnClickListener(null);
        }
        if ((abVar.f765a != null ? 1 : 0) != 0) {
            m665b(abVar);
            return;
        }
        this.f733b.setText(abVar.i);
        this.f733b.setVisibility(0);
        this.f736e.setVisibility(8);
    }
}
