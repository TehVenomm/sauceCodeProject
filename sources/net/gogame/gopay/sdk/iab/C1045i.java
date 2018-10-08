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
import net.gogame.gopay.sdk.C1026a;
import net.gogame.gopay.sdk.support.C1084m;

/* renamed from: net.gogame.gopay.sdk.iab.i */
public final class C1045i extends C1026a {
    /* renamed from: d */
    int f1143d = 1;
    /* renamed from: e */
    int f1144e = -1;
    /* renamed from: f */
    View f1145f;

    public C1045i(Context context) {
        super(context);
    }

    /* renamed from: a */
    private View m854a(int i, View view) {
        View imageView;
        int i2 = 0;
        if (view == null) {
            view = new LinearLayout(this.f962a);
            view.setBackgroundColor(0);
            view.setOrientation(0);
            view.setLayoutParams(new LayoutParams(-1, m755a(60)));
            view.setPadding(m755a(5), m755a(5), m755a(5), m755a(5));
            imageView = new ImageView(this.f962a);
            imageView.setTag(Integer.valueOf(1));
            imageView.setScaleType(ScaleType.FIT_CENTER);
            ViewGroup.LayoutParams layoutParams = new LinearLayout.LayoutParams(m755a(100), -1);
            layoutParams.setMargins(m755a(10), 0, 0, 0);
            view.addView(imageView, layoutParams);
            View textView = new TextView(this.f962a);
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
            layoutParams2.setMargins(m755a(12), 0, m755a(1), 0);
            layoutParams2.weight = 1.0f;
            view.addView(textView, layoutParams2);
            View imageView2 = new ImageView(this.f962a);
            Bitmap c = C1084m.m931c();
            if (c != null) {
                imageView2.setImageBitmap(c);
            }
            imageView2.setScaleType(ScaleType.FIT_CENTER);
            ViewGroup.LayoutParams layoutParams3 = new LinearLayout.LayoutParams(m755a(20), m755a(20));
            layoutParams3.gravity = 16;
            layoutParams3.setMargins(0, 0, m755a(8), 0);
            view.addView(imageView2, layoutParams3);
            imageView2.setTag(Integer.valueOf(3));
            view.setTag(new bv(imageView, textView, i));
        }
        imageView = view.findViewWithTag(Integer.valueOf(3));
        if (i != this.f1144e) {
            i2 = 4;
        }
        imageView.setVisibility(i2);
        bv bvVar = (bv) view.getTag();
        bvVar.f1128c = i;
        bvVar.f1127b.setText(((C1025a) getItem(bvVar.f1128c)).getDisplayName());
        bvVar.f1126a.setVisibility(4);
        C1084m.m930b(m756a(), ((C1025a) getItem(bvVar.f1128c)).getDisplayIcon(), new C1046j(this, bvVar, i));
        return view;
    }

    /* renamed from: b */
    private View m855b(int i, View view) {
        if (view == null) {
            view = new RelativeLayout(this.f962a);
            view.setLayoutParams(new LayoutParams(-1, m755a(60)));
            View imageView = new ImageView(this.f962a);
            imageView.setTag(Integer.valueOf(1));
            imageView.setScaleType(ScaleType.FIT_CENTER);
            ViewGroup.LayoutParams layoutParams = new RelativeLayout.LayoutParams(-2, -2);
            layoutParams.addRule(13);
            view.addView(imageView, layoutParams);
            View textView = new TextView(this.f962a);
            textView.setBackgroundColor(0);
            textView.setTextColor(ViewCompat.MEASURED_STATE_MASK);
            textView.setGravity(17);
            textView.setTextSize(3, 6.0f);
            textView.setTag(Integer.valueOf(2));
            ViewGroup.LayoutParams layoutParams2 = new RelativeLayout.LayoutParams(-1, -1);
            layoutParams2.addRule(13);
            layoutParams2.setMargins(m755a(1), 0, m755a(1), 0);
            view.addView(textView, layoutParams2);
            view.setTag(new bv(imageView, textView, i));
        }
        if (i == this.f1144e) {
            this.f1145f = view;
            view.setBackgroundColor(-1);
        } else {
            view.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        bv bvVar = (bv) view.getTag();
        bvVar.f1128c = i;
        bvVar.f1127b.setVisibility(0);
        bvVar.f1127b.setText(((C1025a) getItem(bvVar.f1128c)).getDisplayName());
        bvVar.f1126a.setVisibility(4);
        C1084m.m930b(m756a(), ((C1025a) getItem(bvVar.f1128c)).getDisplayIcon(), new C1047k(this, bvVar, i));
        return view;
    }

    public final View getDropDownView(int i, View view, ViewGroup viewGroup) {
        return this.f1143d == 1 ? m854a(i, view) : m855b(i, view);
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        return this.f1143d == 1 ? m854a(i, view) : m855b(i, view);
    }
}
