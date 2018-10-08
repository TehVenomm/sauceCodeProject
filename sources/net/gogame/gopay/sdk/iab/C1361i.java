package net.gogame.gopay.sdk.iab;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Build.VERSION;
import android.support.v4.view.ViewCompat;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView.LayoutParams;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;
import net.gogame.gopay.sdk.C1342a;
import net.gogame.gopay.sdk.support.C1400m;

/* renamed from: net.gogame.gopay.sdk.iab.i */
public final class C1361i extends C1342a {
    /* renamed from: d */
    int f3531d = 1;
    /* renamed from: e */
    int f3532e = -1;
    /* renamed from: f */
    View f3533f;

    public C1361i(Context context) {
        super(context);
    }

    /* renamed from: a */
    private View m3879a(int i, View view) {
        View imageView;
        int i2 = 0;
        if (view == null) {
            view = new LinearLayout(this.f3350a);
            view.setBackgroundColor(0);
            view.setOrientation(0);
            view.setLayoutParams(new LayoutParams(-1, m3780a(60)));
            view.setPadding(m3780a(5), m3780a(5), m3780a(5), m3780a(5));
            imageView = new ImageView(this.f3350a);
            imageView.setTag(Integer.valueOf(1));
            imageView.setScaleType(ScaleType.FIT_CENTER);
            ViewGroup.LayoutParams layoutParams = new LinearLayout.LayoutParams(m3780a(100), -1);
            layoutParams.setMargins(m3780a(10), 0, 0, 0);
            view.addView(imageView, layoutParams);
            View textView = new TextView(this.f3350a);
            textView.setBackgroundColor(0);
            textView.setTextColor(ViewCompat.MEASURED_STATE_MASK);
            textView.setTypeface(Typeface.DEFAULT);
            textView.setGravity(17);
            textView.setTextSize(3, 8.0f);
            textView.setMaxLines(2);
            textView.setTag(Integer.valueOf(2));
            if (VERSION.SDK_INT >= 17) {
                textView.setTextAlignment(5);
            } else {
                textView.setGravity(3);
            }
            ViewGroup.LayoutParams layoutParams2 = new LinearLayout.LayoutParams(-1, -1);
            layoutParams2.setMargins(m3780a(12), 0, m3780a(1), 0);
            layoutParams2.weight = 1.0f;
            view.addView(textView, layoutParams2);
            View imageView2 = new ImageView(this.f3350a);
            Bitmap c = C1400m.m3956c();
            if (c != null) {
                imageView2.setImageBitmap(c);
            }
            imageView2.setScaleType(ScaleType.FIT_CENTER);
            ViewGroup.LayoutParams layoutParams3 = new LinearLayout.LayoutParams(m3780a(20), m3780a(20));
            layoutParams3.gravity = 16;
            layoutParams3.setMargins(0, 0, m3780a(8), 0);
            view.addView(imageView2, layoutParams3);
            imageView2.setTag(Integer.valueOf(3));
            view.setTag(new bv(imageView, textView, i));
        }
        imageView = view.findViewWithTag(Integer.valueOf(3));
        if (i != this.f3532e) {
            i2 = 4;
        }
        imageView.setVisibility(i2);
        bv bvVar = (bv) view.getTag();
        bvVar.f3516c = i;
        bvVar.f3515b.setText(((C1341a) getItem(bvVar.f3516c)).getDisplayName());
        bvVar.f3514a.setVisibility(4);
        C1400m.m3955b(m3781a(), ((C1341a) getItem(bvVar.f3516c)).getDisplayIcon(), new C1362j(this, bvVar, i));
        return view;
    }

    /* renamed from: b */
    private View m3880b(int i, View view) {
        if (view == null) {
            view = new RelativeLayout(this.f3350a);
            view.setLayoutParams(new LayoutParams(-1, m3780a(60)));
            View imageView = new ImageView(this.f3350a);
            imageView.setTag(Integer.valueOf(1));
            imageView.setScaleType(ScaleType.FIT_CENTER);
            ViewGroup.LayoutParams layoutParams = new RelativeLayout.LayoutParams(-2, -2);
            layoutParams.addRule(13);
            view.addView(imageView, layoutParams);
            View textView = new TextView(this.f3350a);
            textView.setBackgroundColor(0);
            textView.setTextColor(ViewCompat.MEASURED_STATE_MASK);
            textView.setGravity(17);
            textView.setTextSize(3, 6.0f);
            textView.setTag(Integer.valueOf(2));
            ViewGroup.LayoutParams layoutParams2 = new RelativeLayout.LayoutParams(-1, -1);
            layoutParams2.addRule(13);
            layoutParams2.setMargins(m3780a(1), 0, m3780a(1), 0);
            view.addView(textView, layoutParams2);
            view.setTag(new bv(imageView, textView, i));
        }
        if (i == this.f3532e) {
            this.f3533f = view;
            view.setBackgroundColor(-1);
        } else {
            view.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        bv bvVar = (bv) view.getTag();
        bvVar.f3516c = i;
        bvVar.f3515b.setVisibility(0);
        bvVar.f3515b.setText(((C1341a) getItem(bvVar.f3516c)).getDisplayName());
        bvVar.f3514a.setVisibility(4);
        C1400m.m3955b(m3781a(), ((C1341a) getItem(bvVar.f3516c)).getDisplayIcon(), new C1363k(this, bvVar, i));
        return view;
    }

    public final View getDropDownView(int i, View view, ViewGroup viewGroup) {
        return this.f3531d == 1 ? m3879a(i, view) : m3880b(i, view);
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        return this.f3531d == 1 ? m3879a(i, view) : m3880b(i, view);
    }
}
