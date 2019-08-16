package net.gogame.gopay.sdk.iab;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Build.VERSION;
import android.support.p000v4.view.ViewCompat;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView.LayoutParams;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;
import net.gogame.gopay.sdk.C1359a;
import net.gogame.gopay.sdk.support.C1415m;

/* renamed from: net.gogame.gopay.sdk.iab.i */
public final class C1398i extends C1359a {

    /* renamed from: d */
    int f1104d = 1;

    /* renamed from: e */
    int f1105e = -1;

    /* renamed from: f */
    View f1106f;

    public C1398i(Context context) {
        super(context);
    }

    /* renamed from: a */
    private View m856a(int i, View view) {
        int i2 = 0;
        if (view == 0) {
            r11 = new LinearLayout(this.f987a);
            r11.setBackgroundColor(0);
            r11.setOrientation(0);
            r11.setLayoutParams(new LayoutParams(-1, mo21497a(60)));
            r11.setPadding(mo21497a(5), mo21497a(5), mo21497a(5), mo21497a(5));
            ImageView imageView = new ImageView(this.f987a);
            imageView.setTag(Integer.valueOf(1));
            imageView.setScaleType(ScaleType.FIT_CENTER);
            LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(mo21497a(100), -1);
            layoutParams.setMargins(mo21497a(10), 0, 0, 0);
            r11.addView(imageView, layoutParams);
            TextView textView = new TextView(this.f987a);
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
            LinearLayout.LayoutParams layoutParams2 = new LinearLayout.LayoutParams(-1, -1);
            layoutParams2.setMargins(mo21497a(12), 0, mo21497a(1), 0);
            layoutParams2.weight = 1.0f;
            r11.addView(textView, layoutParams2);
            ImageView imageView2 = new ImageView(this.f987a);
            Bitmap c = C1415m.m928c();
            if (c != null) {
                imageView2.setImageBitmap(c);
            }
            imageView2.setScaleType(ScaleType.FIT_CENTER);
            LinearLayout.LayoutParams layoutParams3 = new LinearLayout.LayoutParams(mo21497a(20), mo21497a(20));
            layoutParams3.gravity = 16;
            layoutParams3.setMargins(0, 0, mo21497a(8), 0);
            r11.addView(imageView2, layoutParams3);
            imageView2.setTag(Integer.valueOf(3));
            r11.setTag(new C1623bv(imageView, textView, i));
            view = r11;
        }
        View findViewWithTag = view.findViewWithTag(Integer.valueOf(3));
        if (i != this.f1105e) {
            i2 = 4;
        }
        findViewWithTag.setVisibility(i2);
        C1623bv bvVar = (C1623bv) view.getTag();
        bvVar.f1262c = i;
        bvVar.f1261b.setText(((C1365a) getItem(bvVar.f1262c)).getDisplayName());
        bvVar.f1260a.setVisibility(4);
        C1415m.m927b(mo21498a(), ((C1365a) getItem(bvVar.f1262c)).getDisplayIcon(), new C1627j(this, bvVar, i));
        return view;
    }

    /* renamed from: b */
    private View m857b(int i, View view) {
        if (view == 0) {
            r10 = new RelativeLayout(this.f987a);
            r10.setLayoutParams(new LayoutParams(-1, mo21497a(60)));
            ImageView imageView = new ImageView(this.f987a);
            imageView.setTag(Integer.valueOf(1));
            imageView.setScaleType(ScaleType.FIT_CENTER);
            RelativeLayout.LayoutParams layoutParams = new RelativeLayout.LayoutParams(-2, -2);
            layoutParams.addRule(13);
            r10.addView(imageView, layoutParams);
            TextView textView = new TextView(this.f987a);
            textView.setBackgroundColor(0);
            textView.setTextColor(ViewCompat.MEASURED_STATE_MASK);
            textView.setGravity(17);
            textView.setTextSize(3, 6.0f);
            textView.setTag(Integer.valueOf(2));
            RelativeLayout.LayoutParams layoutParams2 = new RelativeLayout.LayoutParams(-1, -1);
            layoutParams2.addRule(13);
            layoutParams2.setMargins(mo21497a(1), 0, mo21497a(1), 0);
            r10.addView(textView, layoutParams2);
            r10.setTag(new C1623bv(imageView, textView, i));
            view = r10;
        }
        if (i == this.f1105e) {
            this.f1106f = view;
            view.setBackgroundColor(-1);
        } else {
            view.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        C1623bv bvVar = (C1623bv) view.getTag();
        bvVar.f1262c = i;
        bvVar.f1261b.setVisibility(0);
        bvVar.f1261b.setText(((C1365a) getItem(bvVar.f1262c)).getDisplayName());
        bvVar.f1260a.setVisibility(4);
        C1415m.m927b(mo21498a(), ((C1365a) getItem(bvVar.f1262c)).getDisplayIcon(), new C1628k(this, bvVar, i));
        return view;
    }

    public final View getDropDownView(int i, View view, ViewGroup viewGroup) {
        return this.f1104d == 1 ? m856a(i, view) : m857b(i, view);
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        return this.f1104d == 1 ? m856a(i, view) : m857b(i, view);
    }
}
