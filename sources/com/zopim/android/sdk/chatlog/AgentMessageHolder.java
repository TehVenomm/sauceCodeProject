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
import com.zopim.android.sdk.C0784R;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.attachment.FileExtension;
import com.zopim.android.sdk.attachment.SharedFileProvider;
import com.zopim.android.sdk.util.CircleTransform;
import com.zopim.android.sdk.util.CropSquareTransform;
import java.util.Locale;

final class AgentMessageHolder extends ViewHolder {
    /* renamed from: c */
    private static final String f703c = AgentMessageHolder.class.getSimpleName();
    /* renamed from: a */
    public LinearLayout f704a;
    /* renamed from: b */
    OnClickListener f705b = new C0836d(this);
    /* renamed from: d */
    private ImageView f706d;
    /* renamed from: e */
    private TextView f707e;
    /* renamed from: f */
    private TextView f708f;
    /* renamed from: g */
    private View f709g;
    /* renamed from: h */
    private TextView f710h;
    /* renamed from: i */
    private TextView f711i;
    /* renamed from: j */
    private ImageView f712j;
    /* renamed from: k */
    private View f713k;
    /* renamed from: l */
    private ImageView f714l;
    /* renamed from: m */
    private ProgressBar f715m;
    /* renamed from: n */
    private OptionClickListener f716n;
    /* renamed from: o */
    private Intent f717o = new Intent("android.intent.action.VIEW");

    public interface OptionClickListener {
        void onClick(String str);
    }

    public AgentMessageHolder(View view, OptionClickListener optionClickListener) {
        super(view);
        this.f706d = (ImageView) view.findViewById(C0784R.id.avatar_icon);
        this.f708f = (TextView) view.findViewById(C0784R.id.message_text);
        this.f707e = (TextView) view.findViewById(C0784R.id.agent_name);
        this.f704a = (LinearLayout) view.findViewById(C0784R.id.options_container);
        this.f709g = view.findViewById(C0784R.id.attachment_document);
        this.f710h = (TextView) view.findViewById(C0784R.id.attachment_name);
        this.f711i = (TextView) view.findViewById(C0784R.id.attachment_size);
        this.f712j = (ImageView) view.findViewById(C0784R.id.attachment_icon);
        this.f713k = view.findViewById(C0784R.id.attachment_image_container);
        this.f714l = (ImageView) view.findViewById(C0784R.id.attachment_thumbnail);
        this.f715m = (ProgressBar) view.findViewById(C0784R.id.attachment_progress);
        this.f717o.setFlags(1073741825);
        this.f713k.setOnClickListener(this.f705b);
        this.f709g.setOnClickListener(this.f705b);
        this.f716n = optionClickListener;
    }

    /* renamed from: a */
    private String m649a(long j, boolean z) {
        int i = z ? 1000 : 1024;
        if (j < ((long) i)) {
            return j + " B";
        }
        String str = (z ? "kMGTPE" : "KMGTPE").charAt(((int) (Math.log((double) j) / Math.log((double) i))) - 1) + (z ? "" : "i");
        return String.format(Locale.US, "%.1f %sB", new Object[]{Double.valueOf(((double) j) / Math.pow((double) i, (double) ((int) (Math.log((double) j) / Math.log((double) i))))), str});
    }

    /* renamed from: b */
    private void m651b(C0832a c0832a) {
        if (c0832a != null && c0832a.f749a != null) {
            String fileExtensionFromUrl = MimeTypeMap.getFileExtensionFromUrl(c0832a.f749a.toExternalForm());
            Uri parse = Uri.parse(c0832a.f749a.toExternalForm());
            Uri providerUri = SharedFileProvider.getProviderUri(this.itemView.getContext(), c0832a.f752d);
            switch (C0837e.f805a[FileExtension.valueOfExtension(fileExtensionFromUrl).ordinal()]) {
                case 1:
                    if (providerUri != null) {
                        this.f717o.setDataAndType(providerUri, "application/pdf");
                    } else {
                        this.f717o.setData(parse);
                    }
                    this.f712j.setImageResource(C0784R.drawable.ic_chat_attachment_pdf);
                    this.f710h.setText(c0832a.f751c);
                    if (c0832a.f750b != null) {
                        this.f711i.setVisibility(0);
                        this.f711i.setText(m649a(c0832a.f750b.longValue(), true));
                    } else {
                        this.f711i.setVisibility(8);
                    }
                    this.f709g.setVisibility(0);
                    this.f708f.setVisibility(8);
                    this.f713k.setVisibility(8);
                    return;
                case 2:
                    if (providerUri != null) {
                        this.f717o.setDataAndType(providerUri, "text/plain");
                    } else {
                        this.f717o.setData(parse);
                    }
                    this.f712j.setImageResource(C0784R.drawable.ic_chat_attachment_txt);
                    this.f710h.setText(c0832a.f751c);
                    if (providerUri != null) {
                        this.f711i.setVisibility(0);
                        this.f711i.setText(m649a(c0832a.f750b.longValue(), true));
                    } else {
                        this.f711i.setVisibility(8);
                    }
                    this.f709g.setVisibility(0);
                    this.f708f.setVisibility(8);
                    this.f713k.setVisibility(8);
                    return;
                case 3:
                case 4:
                case 5:
                    if (providerUri != null) {
                        this.f717o.setDataAndType(providerUri, "image/*");
                    } else {
                        this.f717o.setDataAndType(parse, "image/*");
                    }
                    this.f715m.setVisibility(0);
                    Picasso.with(this.itemView.getContext()).load(parse).placeholder(C0784R.drawable.bg_picasso_placeholder).error(C0784R.drawable.ic_chat_default_avatar).transform(new CropSquareTransform()).into(this.f714l, new C0834b(this));
                    this.f709g.setVisibility(8);
                    this.f708f.setVisibility(8);
                    this.f713k.setVisibility(0);
                    return;
                default:
                    return;
            }
        }
    }

    /* renamed from: c */
    private void m653c(C0832a c0832a) {
        int i = 0;
        if (c0832a.f754f.length != this.f704a.getChildCount()) {
            Logger.m566w(f703c, c0832a.f754f.length + " item options,  " + this.f704a.getChildCount() + " views.");
            Log.w(f703c, "Unexpected agent options length. Ignoring to avoid array index out bounds exception.");
        }
        switch (c0832a.f754f.length) {
            case 0:
                return;
            case 1:
                TextView textView = (TextView) this.f704a.getChildAt(0);
                textView.setText(c0832a.f754f[0]);
                textView.setBackgroundResource(C0784R.drawable.bg_chat_bubble_visitor);
                textView.setTextAppearance(this.itemView.getContext(), C0784R.style.chat_bubble_visitor);
                textView.setCompoundDrawablesWithIntrinsicBounds(0, 0, 0, 0);
                return;
        }
        while (i < c0832a.f754f.length) {
            textView = (TextView) this.f704a.getChildAt(i);
            textView.setText(c0832a.f754f[i]);
            textView.setClickable(true);
            textView.setOnClickListener(new C0835c(this));
            i++;
        }
    }

    /* renamed from: a */
    public void m654a(C0832a c0832a) {
        if (c0832a == null) {
            Log.e(f703c, "Item must not be null");
            return;
        }
        this.f707e.setText(c0832a.j);
        if (c0832a.f753e == null || c0832a.f753e.isEmpty()) {
            Picasso.with(this.itemView.getContext()).load(C0784R.drawable.ic_chat_default_avatar).transform(new CircleTransform()).into(this.f706d);
        } else {
            Picasso.with(this.itemView.getContext()).load(c0832a.f753e).error(C0784R.drawable.ic_chat_default_avatar).placeholder(C0784R.drawable.ic_chat_default_avatar).transform(new CircleTransform()).into(this.f706d);
        }
        if ((c0832a.f749a != null ? 1 : 0) != 0) {
            m651b(c0832a);
            return;
        }
        this.f708f.setText(c0832a.i);
        this.f713k.setVisibility(8);
        this.f709g.setVisibility(8);
        this.f708f.setVisibility(0);
        if (c0832a.f754f.length > 0) {
            m653c(c0832a);
        }
    }

    /* renamed from: a */
    public void m655a(boolean z) {
        if (z) {
            this.f706d.setVisibility(0);
        } else {
            this.f706d.setVisibility(4);
        }
    }

    /* renamed from: b */
    public void m656b(boolean z) {
        if (z) {
            this.f707e.setVisibility(0);
        } else {
            this.f707e.setVisibility(8);
        }
    }
}
