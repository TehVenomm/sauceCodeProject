package net.gogame.gopay.sdk.iab;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.GradientDrawable;
import android.graphics.drawable.LayerDrawable;
import android.os.Build.VERSION;
import android.support.p000v4.view.ViewCompat;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView.LayoutParams;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.RelativeLayout;
import android.widget.TextView;
import java.lang.ref.WeakReference;
import net.gogame.gopay.sdk.C1359a;
import net.gogame.gopay.sdk.support.C1415m;

/* renamed from: net.gogame.gopay.sdk.iab.bs */
public final class C1394bs extends C1359a {

    /* renamed from: d */
    WeakReference f1101d;

    /* renamed from: e */
    int f1102e = 0;

    /* renamed from: f */
    C1365a f1103f;

    public C1394bs(Context context, C1398i iVar) {
        super(context);
        this.f1101d = new WeakReference(iVar);
    }

    /* renamed from: c */
    private C1365a m851c(int i) {
        return i == 0 ? this.f1103f : (C1365a) ((C1398i) this.f1101d.get()).getItem(getItem(i).intValue());
    }

    /* renamed from: a */
    public final void mo21548a(C1365a aVar) {
        this.f1103f = aVar;
        notifyDataSetChanged();
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: a */
    public final void mo21549a(C1623bv bvVar, Bitmap bitmap) {
        if (bitmap == null) {
            bvVar.f1272a.setVisibility(4);
            bvVar.f1273b.setVisibility(0);
            return;
        }
        int width = bitmap.getWidth();
        int height = bitmap.getHeight();
        BitmapDrawable bitmapDrawable = new BitmapDrawable(this.f993a.getResources(), bitmap);
        bitmapDrawable.setBounds(0, 0, width, height);
        bvVar.f1272a.setImageDrawable(bitmapDrawable);
        bvVar.f1272a.setVisibility(0);
        bvVar.f1273b.setVisibility(4);
    }

    /* renamed from: b */
    public final Integer getItem(int i) {
        return i == 0 ? (Integer) super.getItem(0) : (Integer) super.getItem(i - 1);
    }

    public final int getCount() {
        return (this.f1103f != null ? 1 : 0) + super.getCount();
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        if (view == 0) {
            r11 = new RelativeLayout(this.f993a);
            r11.setLayoutParams(new LayoutParams(mo21497a(80), -1));
            ImageView imageView = new ImageView(this.f993a);
            imageView.setTag(Integer.valueOf(1));
            imageView.setScaleType(ScaleType.FIT_CENTER);
            RelativeLayout.LayoutParams layoutParams = new RelativeLayout.LayoutParams(mo21497a(77), mo21497a(49));
            layoutParams.addRule(13);
            r11.addView(imageView, layoutParams);
            TextView textView = new TextView(this.f993a);
            textView.setBackgroundColor(0);
            textView.setTextColor(ViewCompat.MEASURED_STATE_MASK);
            textView.setGravity(17);
            textView.setTextSize(3, 6.0f);
            textView.setTag(Integer.valueOf(2));
            RelativeLayout.LayoutParams layoutParams2 = new RelativeLayout.LayoutParams(-1, -1);
            layoutParams2.addRule(13);
            layoutParams2.setMargins(mo21497a(1), 0, mo21497a(1), 0);
            r11.addView(textView, layoutParams2);
            r11.setTag(new C1623bv(imageView, textView, i));
            view = r11;
        }
        if (i == this.f1102e) {
            int a = mo21497a(2);
            GradientDrawable gradientDrawable = new GradientDrawable();
            gradientDrawable.setStroke(a, Color.rgb(92, 176, 59));
            gradientDrawable.setColor(-1);
            gradientDrawable.setGradientType(2);
            LayerDrawable layerDrawable = new LayerDrawable(new Drawable[]{gradientDrawable});
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
        C1623bv bvVar = (C1623bv) view.getTag();
        bvVar.f1274c = i;
        bvVar.f1273b.setText(m851c(bvVar.f1274c).getDisplayName());
        bvVar.f1272a.setVisibility(4);
        if (mo21498a() == null) {
            bvVar.f1273b.setVisibility(0);
        }
        C1365a c = m851c(bvVar.f1274c);
        if (c instanceof C1395f) {
            mo21549a(bvVar, C1415m.m935i());
        } else {
            C1415m.m927b(mo21498a(), c.getDisplayIcon(), new C1621bt(this, bvVar, i));
        }
        return view;
    }
}
