package com.unity3d.player;

import android.content.Context;
import android.graphics.Bitmap;
import android.view.View;

/* renamed from: com.unity3d.player.j */
public final class C1112j extends View {

    /* renamed from: a */
    final int f593a;

    /* renamed from: b */
    final int f594b = getResources().getIdentifier("unity_static_splash", "drawable", getContext().getPackageName());

    /* renamed from: c */
    Bitmap f595c;

    /* renamed from: d */
    Bitmap f596d;

    /* renamed from: com.unity3d.player.j$1 */
    static final /* synthetic */ class C11131 {

        /* renamed from: a */
        static final /* synthetic */ int[] f597a = new int[C1114a.m546a().length];

        static {
            try {
                f597a[C1114a.f598a - 1] = 1;
            } catch (NoSuchFieldError e) {
            }
            try {
                f597a[C1114a.f599b - 1] = 2;
            } catch (NoSuchFieldError e2) {
            }
            try {
                f597a[C1114a.f600c - 1] = 3;
            } catch (NoSuchFieldError e3) {
            }
        }
    }

    /* renamed from: com.unity3d.player.j$a */
    enum C1114a {
        ;

        static {
            f598a = 1;
            f599b = 2;
            f600c = 3;
            f601d = new int[]{f598a, f599b, f600c};
        }

        /* renamed from: a */
        public static int[] m546a() {
            return (int[]) f601d.clone();
        }
    }

    public C1112j(Context context, int i) {
        super(context);
        this.f593a = i;
        if (this.f594b != 0) {
            forceLayout();
        }
    }

    public final void onDetachedFromWindow() {
        super.onDetachedFromWindow();
        if (this.f595c != null) {
            this.f595c.recycle();
            this.f595c = null;
        }
        if (this.f596d != null) {
            this.f596d.recycle();
            this.f596d = null;
        }
    }

    /* JADX WARNING: Code restructure failed: missing block: B:27:0x00b7, code lost:
        if (r6 < r5) goto L_0x00b9;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:28:0x00b9, code lost:
        r0 = (int) (((float) r6) * r7);
        r5 = r6;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void onLayout(boolean r11, int r12, int r13, int r14, int r15) {
        /*
            r10 = this;
            r1 = 1
            r2 = 0
            int r0 = r10.f594b
            if (r0 != 0) goto L_0x0007
        L_0x0006:
            return
        L_0x0007:
            android.graphics.Bitmap r0 = r10.f595c
            if (r0 != 0) goto L_0x001e
            android.graphics.BitmapFactory$Options r0 = new android.graphics.BitmapFactory$Options
            r0.<init>()
            r0.inScaled = r2
            android.content.res.Resources r3 = r10.getResources()
            int r4 = r10.f594b
            android.graphics.Bitmap r0 = android.graphics.BitmapFactory.decodeResource(r3, r4, r0)
            r10.f595c = r0
        L_0x001e:
            android.graphics.Bitmap r0 = r10.f595c
            int r3 = r0.getWidth()
            android.graphics.Bitmap r0 = r10.f595c
            int r5 = r0.getHeight()
            int r4 = r10.getWidth()
            int r6 = r10.getHeight()
            if (r4 == 0) goto L_0x0006
            if (r6 == 0) goto L_0x0006
            float r0 = (float) r3
            float r7 = (float) r5
            float r7 = r0 / r7
            float r0 = (float) r4
            float r8 = (float) r6
            float r0 = r0 / r8
            int r0 = (r0 > r7 ? 1 : (r0 == r7 ? 0 : -1))
            if (r0 > 0) goto L_0x00af
            r0 = r1
        L_0x0042:
            int[] r8 = com.unity3d.player.C1112j.C11131.f597a
            int r9 = r10.f593a
            int r9 = r9 + -1
            r8 = r8[r9]
            switch(r8) {
                case 1: goto L_0x00b1;
                case 2: goto L_0x00be;
                case 3: goto L_0x00be;
                default: goto L_0x004d;
            }
        L_0x004d:
            r0 = r3
        L_0x004e:
            android.graphics.Bitmap r3 = r10.f596d
            if (r3 == 0) goto L_0x0070
            android.graphics.Bitmap r3 = r10.f596d
            int r3 = r3.getWidth()
            if (r3 != r0) goto L_0x0062
            android.graphics.Bitmap r3 = r10.f596d
            int r3 = r3.getHeight()
            if (r3 == r5) goto L_0x0006
        L_0x0062:
            android.graphics.Bitmap r3 = r10.f596d
            android.graphics.Bitmap r4 = r10.f595c
            if (r3 == r4) goto L_0x0070
            android.graphics.Bitmap r3 = r10.f596d
            r3.recycle()
            r3 = 0
            r10.f596d = r3
        L_0x0070:
            android.graphics.Bitmap r3 = r10.f595c
            android.graphics.Bitmap r0 = android.graphics.Bitmap.createScaledBitmap(r3, r0, r5, r1)
            r10.f596d = r0
            android.graphics.Bitmap r0 = r10.f596d
            android.content.res.Resources r3 = r10.getResources()
            android.util.DisplayMetrics r3 = r3.getDisplayMetrics()
            int r3 = r3.densityDpi
            r0.setDensity(r3)
            android.graphics.drawable.ColorDrawable r0 = new android.graphics.drawable.ColorDrawable
            r3 = -16777216(0xffffffffff000000, float:-1.7014118E38)
            r0.<init>(r3)
            android.graphics.drawable.BitmapDrawable r3 = new android.graphics.drawable.BitmapDrawable
            android.content.res.Resources r4 = r10.getResources()
            android.graphics.Bitmap r5 = r10.f596d
            r3.<init>(r4, r5)
            r4 = 17
            r3.setGravity(r4)
            android.graphics.drawable.LayerDrawable r4 = new android.graphics.drawable.LayerDrawable
            r5 = 2
            android.graphics.drawable.Drawable[] r5 = new android.graphics.drawable.Drawable[r5]
            r5[r2] = r0
            r5[r1] = r3
            r4.<init>(r5)
            r10.setBackground(r4)
            goto L_0x0006
        L_0x00af:
            r0 = r2
            goto L_0x0042
        L_0x00b1:
            if (r4 >= r3) goto L_0x00d0
            float r0 = (float) r4
            float r0 = r0 / r7
            int r5 = (int) r0
            r0 = r4
        L_0x00b7:
            if (r6 >= r5) goto L_0x004e
        L_0x00b9:
            float r0 = (float) r6
            float r0 = r0 * r7
            int r0 = (int) r0
            r5 = r6
            goto L_0x004e
        L_0x00be:
            int r3 = r10.f593a
            int r5 = com.unity3d.player.C1112j.C1114a.f600c
            if (r3 != r5) goto L_0x00ce
            r3 = r1
        L_0x00c5:
            r0 = r0 ^ r3
            if (r0 == 0) goto L_0x00b9
            float r0 = (float) r4
            float r0 = r0 / r7
            int r3 = (int) r0
            r0 = r4
            r5 = r3
            goto L_0x004e
        L_0x00ce:
            r3 = r2
            goto L_0x00c5
        L_0x00d0:
            r0 = r3
            goto L_0x00b7
        */
        throw new UnsupportedOperationException("Method not decompiled: com.unity3d.player.C1112j.onLayout(boolean, int, int, int, int):void");
    }
}
