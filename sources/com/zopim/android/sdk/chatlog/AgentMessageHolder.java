package com.zopim.android.sdk.chatlog;

import android.content.Intent;
import android.net.Uri;
import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.webkit.MimeTypeMap;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;
import com.squareup.picasso.Picasso;
import com.squareup.picasso.Transformation;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.attachment.FileExtension;
import com.zopim.android.sdk.attachment.SharedFileProvider;
import com.zopim.android.sdk.util.CircleTransform;
import com.zopim.android.sdk.util.CropSquareTransform;
import java.util.Locale;

final class AgentMessageHolder extends ViewHolder {
    /* access modifiers changed from: private */

    /* renamed from: c */
    public static final String f747c = AgentMessageHolder.class.getSimpleName();

    /* renamed from: a */
    public LinearLayout f748a;

    /* renamed from: b */
    OnClickListener f749b = new C1206d(this);

    /* renamed from: d */
    private ImageView f750d;

    /* renamed from: e */
    private TextView f751e;

    /* renamed from: f */
    private TextView f752f;

    /* renamed from: g */
    private View f753g;

    /* renamed from: h */
    private TextView f754h;

    /* renamed from: i */
    private TextView f755i;

    /* renamed from: j */
    private ImageView f756j;

    /* renamed from: k */
    private View f757k;

    /* renamed from: l */
    private ImageView f758l;
    /* access modifiers changed from: private */

    /* renamed from: m */
    public ProgressBar f759m;
    /* access modifiers changed from: private */

    /* renamed from: n */
    public OptionClickListener f760n;
    /* access modifiers changed from: private */

    /* renamed from: o */
    public Intent f761o = new Intent("android.intent.action.VIEW");

    public interface OptionClickListener {
        void onClick(String str);
    }

    public AgentMessageHolder(View view, OptionClickListener optionClickListener) {
        super(view);
        this.f750d = (ImageView) view.findViewById(C1122R.C1125id.avatar_icon);
        this.f752f = (TextView) view.findViewById(C1122R.C1125id.message_text);
        this.f751e = (TextView) view.findViewById(C1122R.C1125id.agent_name);
        this.f748a = (LinearLayout) view.findViewById(C1122R.C1125id.options_container);
        this.f753g = view.findViewById(C1122R.C1125id.attachment_document);
        this.f754h = (TextView) view.findViewById(C1122R.C1125id.attachment_name);
        this.f755i = (TextView) view.findViewById(C1122R.C1125id.attachment_size);
        this.f756j = (ImageView) view.findViewById(C1122R.C1125id.attachment_icon);
        this.f757k = view.findViewById(C1122R.C1125id.attachment_image_container);
        this.f758l = (ImageView) view.findViewById(C1122R.C1125id.attachment_thumbnail);
        this.f759m = (ProgressBar) view.findViewById(C1122R.C1125id.attachment_progress);
        this.f761o.setFlags(1073741825);
        this.f757k.setOnClickListener(this.f749b);
        this.f753g.setOnClickListener(this.f749b);
        this.f760n = optionClickListener;
    }

    /* renamed from: a */
    private String m662a(long j, boolean z) {
        int i = z ? 1000 : 1024;
        if (j < ((long) i)) {
            return j + " B";
        }
        int log = (int) (Math.log((double) j) / Math.log((double) i));
        return String.format(Locale.US, "%.1f %sB", new Object[]{Double.valueOf(((double) j) / Math.pow((double) i, (double) log)), (z ? "kMGTPE" : "KMGTPE").charAt(log - 1) + (z ? "" : "i")});
    }

    /* renamed from: b */
    private void m664b(C1177a aVar) {
        if (aVar != null && aVar.f787a != null) {
            String fileExtensionFromUrl = MimeTypeMap.getFileExtensionFromUrl(aVar.f787a.toExternalForm());
            Uri parse = Uri.parse(aVar.f787a.toExternalForm());
            Uri providerUri = SharedFileProvider.getProviderUri(this.itemView.getContext(), aVar.f790d);
            switch (C1207e.f849a[FileExtension.valueOfExtension(fileExtensionFromUrl).ordinal()]) {
                case 1:
                    if (providerUri != null) {
                        this.f761o.setDataAndType(providerUri, "application/pdf");
                    } else {
                        this.f761o.setData(parse);
                    }
                    this.f756j.setImageResource(C1122R.C1124drawable.ic_chat_attachment_pdf);
                    this.f754h.setText(aVar.f789c);
                    if (aVar.f788b != null) {
                        this.f755i.setVisibility(0);
                        this.f755i.setText(m662a(aVar.f788b.longValue(), true));
                    } else {
                        this.f755i.setVisibility(8);
                    }
                    this.f753g.setVisibility(0);
                    this.f752f.setVisibility(8);
                    this.f757k.setVisibility(8);
                    return;
                case 2:
                    if (providerUri != null) {
                        this.f761o.setDataAndType(providerUri, "text/plain");
                    } else {
                        this.f761o.setData(parse);
                    }
                    this.f756j.setImageResource(C1122R.C1124drawable.ic_chat_attachment_txt);
                    this.f754h.setText(aVar.f789c);
                    if (providerUri != null) {
                        this.f755i.setVisibility(0);
                        this.f755i.setText(m662a(aVar.f788b.longValue(), true));
                    } else {
                        this.f755i.setVisibility(8);
                    }
                    this.f753g.setVisibility(0);
                    this.f752f.setVisibility(8);
                    this.f757k.setVisibility(8);
                    return;
                case 3:
                case 4:
                case 5:
                    if (providerUri != null) {
                        this.f761o.setDataAndType(providerUri, "image/*");
                    } else {
                        this.f761o.setDataAndType(parse, "image/*");
                    }
                    this.f759m.setVisibility(0);
                    Picasso.with(this.itemView.getContext()).load(parse).placeholder(C1122R.C1124drawable.bg_picasso_placeholder).error(C1122R.C1124drawable.ic_chat_default_avatar).transform((Transformation) new CropSquareTransform()).into(this.f758l, new C1204b(this));
                    this.f753g.setVisibility(8);
                    this.f752f.setVisibility(8);
                    this.f757k.setVisibility(0);
                    return;
                default:
                    return;
            }
        }
    }

    /* renamed from: c */
    private void m666c(C1177a aVar) {
        if (aVar.f792f.length != this.f748a.getChildCount()) {
            Logger.m579w(f747c, aVar.f792f.length + " item options,  " + this.f748a.getChildCount() + " views.");
            Log.w(f747c, "Unexpected agent options length. Ignoring to avoid array index out bounds exception.");
        }
        switch (aVar.f792f.length) {
            case 0:
                return;
            case 1:
                TextView textView = (TextView) this.f748a.getChildAt(0);
                textView.setText(aVar.f792f[0]);
                textView.setBackgroundResource(C1122R.C1124drawable.bg_chat_bubble_visitor);
                textView.setTextAppearance(this.itemView.getContext(), C1122R.style.chat_bubble_visitor);
                textView.setCompoundDrawablesWithIntrinsicBounds(0, 0, 0, 0);
                return;
        }
        for (int i = 0; i < aVar.f792f.length; i++) {
            TextView textView2 = (TextView) this.f748a.getChildAt(i);
            textView2.setText(aVar.f792f[i]);
            textView2.setClickable(true);
            textView2.setOnClickListener(new C1205c(this));
        }
    }

    /* renamed from: a */
    public void mo20705a(C1177a aVar) {
        if (aVar == null) {
            Log.e(f747c, "Item must not be null");
            return;
        }
        this.f751e.setText(aVar.f796j);
        if (aVar.f791e == null || aVar.f791e.isEmpty()) {
            Picasso.with(this.itemView.getContext()).load(C1122R.C1124drawable.ic_chat_default_avatar).transform((Transformation) new CircleTransform()).into(this.f750d);
        } else {
            Picasso.with(this.itemView.getContext()).load(aVar.f791e).error(C1122R.C1124drawable.ic_chat_default_avatar).placeholder(C1122R.C1124drawable.ic_chat_default_avatar).transform((Transformation) new CircleTransform()).into(this.f750d);
        }
        if (aVar.f787a != null) {
            m664b(aVar);
            return;
        }
        this.f752f.setText(aVar.f795i);
        this.f757k.setVisibility(8);
        this.f753g.setVisibility(8);
        this.f752f.setVisibility(0);
        if (aVar.f792f.length > 0) {
            m666c(aVar);
        }
    }

    /* renamed from: a */
    public void mo20706a(boolean z) {
        if (z) {
            this.f750d.setVisibility(0);
        } else {
            this.f750d.setVisibility(4);
        }
    }

    /* renamed from: b */
    public void mo20707b(boolean z) {
        if (z) {
            this.f751e.setVisibility(0);
        } else {
            this.f751e.setVisibility(8);
        }
    }
}
