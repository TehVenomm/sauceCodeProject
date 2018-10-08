package net.gogame.gopay.sdk.iab;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.GradientDrawable;
import android.graphics.drawable.LayerDrawable;
import android.os.Build.VERSION;
import android.support.v4.view.ViewCompat;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView.LayoutParams;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.RelativeLayout;
import android.widget.TextView;
import java.lang.ref.WeakReference;
import net.gogame.gopay.sdk.C1026a;
import net.gogame.gopay.sdk.support.C1084m;

public final class bs extends C1026a {
    /* renamed from: d */
    WeakReference f1116d;
    /* renamed from: e */
    int f1117e = 0;
    /* renamed from: f */
    C1025a f1118f;

    public bs(Context context, C1045i c1045i) {
        super(context);
        this.f1116d = new WeakReference(c1045i);
    }

    /* renamed from: c */
    private C1025a m848c(int i) {
        return i == 0 ? this.f1118f : (C1025a) ((C1045i) this.f1116d.get()).getItem(m851b(i).intValue());
    }

    /* renamed from: a */
    public final void m849a(C1025a c1025a) {
        this.f1118f = c1025a;
        notifyDataSetChanged();
    }

    /* renamed from: a */
    final void m850a(bv bvVar, Bitmap bitmap) {
        if (bitmap == null) {
            bvVar.f1126a.setVisibility(4);
            bvVar.f1127b.setVisibility(0);
            return;
        }
        int width = bitmap.getWidth();
        int height = bitmap.getHeight();
        Drawable bitmapDrawable = new BitmapDrawable(this.f962a.getResources(), bitmap);
        bitmapDrawable.setBounds(0, 0, width, height);
        bvVar.f1126a.setImageDrawable(bitmapDrawable);
        bvVar.f1126a.setVisibility(0);
        bvVar.f1127b.setVisibility(4);
    }

    /* renamed from: b */
    public final Integer m851b(int i) {
        return i == 0 ? (Integer) super.getItem(0) : (Integer) super.getItem(i - 1);
    }

    public final int getCount() {
        return (this.f1118f != null ? 1 : 0) + super.getCount();
    }

    public final /* synthetic */ Object getItem(int i) {
        return m851b(i);
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        if (view == null) {
            view = new RelativeLayout(this.f962a);
            view.setLayoutParams(new LayoutParams(m755a(80), -1));
            View imageView = new ImageView(this.f962a);
            imageView.setTag(Integer.valueOf(1));
            imageView.setScaleType(ScaleType.FIT_CENTER);
            ViewGroup.LayoutParams layoutParams = new RelativeLayout.LayoutParams(m755a(77), m755a(49));
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
        if (i == this.f1117e) {
            int a = m755a(2);
            GradientDrawable gradientDrawable = new GradientDrawable();
            gradientDrawable.setStroke(a, Color.rgb(92, 176, 59));
            gradientDrawable.setColor(-1);
            gradientDrawable.setGradientType(2);
            Drawable layerDrawable = new LayerDrawable(new Drawable[]{gradientDrawable});
            layerDrawable.setLayerInset(0, -a, -a, -a, 0);
            view.setAlpha(1.0f);
            if (VERSION.SDK_INT >= 16) {
                view.setBackground(layerDrawable);
            } else {
                view.setBackgroundDrawable(layerDrawable);
            }
        } else {
            view.setAlpha(0.5f);
            view.setBackgroundColor(0);
        }
        bv bvVar = (bv) view.getTag();
        bvVar.f1128c = i;
        bvVar.f1127b.setText(m848c(bvVar.f1128c).getDisplayName());
        bvVar.f1126a.setVisibility(4);
        if (m756a() == null) {
            bvVar.f1127b.setVisibility(0);
        }
        C1025a c = m848c(bvVar.f1128c);
        if (c instanceof C1043f) {
            m850a(bvVar, C1084m.m938i());
        } else {
            C1084m.m930b(m756a(), c.getDisplayIcon(), new bt(this, bvVar, i));
        }
        return view;
    }
}
