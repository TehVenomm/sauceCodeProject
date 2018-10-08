package net.gogame.gopay.sdk;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.support.v4.view.GravityCompat;
import android.text.TextUtils.TruncateAt;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.AbsListView;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.LinearLayout;
import android.widget.TextView;
import net.gogame.gopay.sdk.iab.bv;
import net.gogame.gopay.sdk.support.C1400m;
import net.gogame.gopay.sdk.support.DisplayUtils;

/* renamed from: net.gogame.gopay.sdk.d */
public final class C1345d extends C1342a {
    /* renamed from: d */
    private final boolean f3354d;

    public C1345d(Context context) {
        this(context, false);
    }

    public C1345d(Context context, boolean z) {
        super(context);
        this.f3354d = z;
    }

    /* renamed from: a */
    private View m3783a(int i, View view, boolean z) {
        if (view == null) {
            TextView textView = null;
            view = new LinearLayout(this.f3350a);
            view.setWeightSum(1.0f);
            view.setOrientation(0);
            view.setGravity(GravityCompat.START);
            View imageView = new ImageView(this.f3350a);
            imageView.setImageResource(17301514);
            imageView.setScaleType(ScaleType.FIT_CENTER);
            imageView.setPadding(m3780a(1), m3780a(1), m3780a(1), m3780a(1));
            imageView.setVisibility(4);
            imageView.setTag(Integer.valueOf(2));
            LayoutParams layoutParams = new LinearLayout.LayoutParams(-2, -1, 0.0f);
            layoutParams.gravity = 16;
            layoutParams.setMargins(m3780a(2), 0, m3780a(2), 0);
            view.addView(imageView, layoutParams);
            if (z) {
                textView = new TextView(this.f3350a);
                textView.setBackgroundColor(0);
                textView.setTextColor(Color.parseColor("#000000"));
                textView.setTextSize(2, 14.0f);
                textView.setGravity(8388627);
                textView.setTag(Integer.valueOf(1));
                textView.setMaxLines(1);
                textView.setEllipsize(TruncateAt.END);
                textView.setPadding(0, 0, 0, 0);
                layoutParams = new LinearLayout.LayoutParams(-1, -1, 1.0f);
                layoutParams.setMargins(m3780a(2), m3780a(0), m3780a(1), m3780a(0));
                layoutParams.gravity = 16;
                view.addView(textView, layoutParams);
            }
            View imageView2 = new ImageView(this.f3350a);
            Bitmap e = C1400m.m3959e();
            if (e != null) {
                imageView2.setImageBitmap(e);
            }
            imageView2.setScaleType(ScaleType.FIT_XY);
            imageView2.setPadding(m3780a(0), m3780a(1), m3780a(1), m3780a(1));
            imageView2.setVisibility(4);
            imageView2.setTag(Integer.valueOf(3));
            LayoutParams layoutParams2 = new LinearLayout.LayoutParams(DisplayUtils.pxFromDp(this.f3350a, 14.0f), DisplayUtils.pxFromDp(this.f3350a, 8.0f), 0.0f);
            layoutParams2.gravity = 16;
            layoutParams2.setMargins(m3780a(2), 0, m3780a(2), 0);
            view.addView(imageView2, layoutParams2);
            view.setTag(new bv(imageView, textView, i));
        }
        View findViewWithTag = view.findViewWithTag(Integer.valueOf(3));
        if (findViewWithTag != null) {
            findViewWithTag.setVisibility(4);
        }
        bv bvVar = (bv) view.getTag();
        bvVar.f3516c = i;
        if (bvVar.f3515b != null) {
            bvVar.f3515b.setText(((Country) getItem(bvVar.f3516c)).getDisplayName());
        }
        if (bvVar.f3514a != null) {
            bvVar.f3514a.setVisibility(4);
        }
        if (bvVar.f3514a != null) {
            C1400m.m3949a(m3781a(), ((Country) getItem(bvVar.f3516c)).getDisplayIcon(), new C1347e(this, bvVar, i));
        }
        return view;
    }

    public final View getDropDownView(int i, View view, ViewGroup viewGroup) {
        View a = m3783a(i, view, true);
        a.setBackgroundColor(0);
        a.setLayoutParams(new AbsListView.LayoutParams(-1, m3780a(30)));
        return a;
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        View a = m3783a(i, view, !this.f3354d);
        a.setBackgroundColor(0);
        a.setLayoutParams(new LayoutParams(-1, m3780a(30)));
        View findViewWithTag = a.findViewWithTag(Integer.valueOf(3));
        if (findViewWithTag != null && getCount() > 1) {
            findViewWithTag.setVisibility(0);
        }
        return a;
    }
}
