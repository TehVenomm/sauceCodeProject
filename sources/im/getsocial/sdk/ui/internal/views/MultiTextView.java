package im.getsocial.sdk.ui.internal.views;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Paint.Align;
import android.graphics.Paint.FontMetrics;
import android.graphics.Paint.Style;
import android.graphics.RectF;
import android.graphics.Typeface;
import android.support.v4.view.GravityCompat;
import android.text.TextUtils;
import android.util.AttributeSet;
import android.view.View;
import android.view.View.MeasureSpec;
import com.google.android.gms.nearby.messages.Strategy;
import im.getsocial.sdk.ui.internal.p125h.fOrCGNYyfk;
import im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme;
import im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Iterator;

public class MultiTextView extends View {
    /* renamed from: a */
    private final FontMetrics f3167a = new FontMetrics();
    /* renamed from: b */
    private final ArrayList<jjbQypPegg> f3168b = new ArrayList();
    /* renamed from: c */
    private int f3169c = GravityCompat.START;
    /* renamed from: d */
    private int f3170d = Strategy.TTL_SECONDS_INFINITE;
    /* renamed from: e */
    private final ArrayList<Integer> f3171e = new ArrayList();
    /* renamed from: f */
    private final ArrayList<Integer> f3172f = new ArrayList();
    /* renamed from: g */
    private final ArrayList<Integer> f3173g = new ArrayList();
    /* renamed from: h */
    private int f3174h;
    /* renamed from: i */
    private int f3175i;
    /* renamed from: j */
    private int f3176j;
    /* renamed from: k */
    private Paint f3177k;
    /* renamed from: l */
    private RectF f3178l;
    /* renamed from: m */
    private int f3179m;
    /* renamed from: n */
    private int f3180n;

    private class jjbQypPegg {
        /* renamed from: a */
        final /* synthetic */ MultiTextView f3156a;
        /* renamed from: b */
        private float f3157b;
        /* renamed from: c */
        private float f3158c;
        /* renamed from: d */
        private float f3159d;
        /* renamed from: e */
        private float f3160e;
        /* renamed from: f */
        private final Paint f3161f;
        /* renamed from: g */
        private Paint f3162g;
        /* renamed from: h */
        private final String[] f3163h;
        /* renamed from: i */
        private int[] f3164i;
        /* renamed from: j */
        private int f3165j;
        /* renamed from: k */
        private int f3166k = 0;

        public jjbQypPegg(MultiTextView multiTextView, String str, KluUZYuxme kluUZYuxme) {
            Typeface a;
            this.f3156a = multiTextView;
            try {
                a = fOrCGNYyfk.m3329a(multiTextView.getContext(), kluUZYuxme);
            } catch (IOException e) {
                a = Typeface.DEFAULT;
            }
            upgqDBbsrL a2 = upgqDBbsrL.m3237a();
            this.f3157b = (float) a2.m3245a(kluUZYuxme);
            this.f3158c = a2.m3252b(kluUZYuxme);
            this.f3159d = a2.m3257c(kluUZYuxme);
            this.f3160e = a2.m3260d(kluUZYuxme);
            this.f3161f = new Paint(1);
            this.f3161f.setTextAlign(Align.CENTER);
            this.f3161f.setTypeface(a);
            this.f3161f.setColor(kluUZYuxme.m3156c().m3215a());
            if (this.f3158c != 0.0f) {
                this.f3162g = new Paint(1);
                this.f3162g.setTextAlign(Align.CENTER);
                this.f3162g.setStyle(Style.STROKE);
                this.f3162g.setTypeface(a);
                this.f3162g.setColor(kluUZYuxme.m3154a().m3215a());
            }
            this.f3163h = str.split(" ");
            this.f3164i = new int[this.f3163h.length];
            m3541b();
        }

        /* renamed from: b */
        private void m3541b() {
            this.f3161f.setTextSize(this.f3157b);
            if (this.f3162g != null) {
                this.f3162g.setTextSize(this.f3157b);
                this.f3162g.setStrokeWidth(this.f3158c);
            }
            this.f3165j = (int) (this.f3161f.measureText(" ") - this.f3158c);
            for (int i = 0; i < this.f3163h.length; i++) {
                this.f3164i[i] = (int) (this.f3161f.measureText(this.f3163h[i]) + (this.f3158c * 2.0f));
            }
        }

        /* renamed from: f */
        static /* synthetic */ boolean m3546f(jjbQypPegg jjbqyppegg) {
            int i = jjbqyppegg.f3166k;
            jjbqyppegg.f3166k = i + 1;
            if (i >= 10) {
                return false;
            }
            jjbqyppegg.f3157b *= 0.95f;
            jjbqyppegg.f3158c *= 0.95f;
            jjbqyppegg.f3159d *= 0.95f;
            jjbqyppegg.f3160e *= 0.95f;
            jjbqyppegg.m3541b();
            return true;
        }

        /* renamed from: a */
        public final String[] m3550a() {
            return this.f3163h;
        }
    }

    public MultiTextView(Context context) {
        super(context);
        m3552b();
    }

    public MultiTextView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        m3552b();
    }

    /* renamed from: b */
    private int m3551b(int i) {
        if (this.f3172f.isEmpty()) {
            return getPaddingLeft();
        }
        int intValue = ((Integer) this.f3172f.get(i)).intValue();
        switch (this.f3169c) {
            case 1:
            case 17:
                return (getWidth() - intValue) / 2;
            case 5:
            case GravityCompat.END /*8388613*/:
                return (getWidth() - getPaddingRight()) - intValue;
            default:
                return getPaddingLeft();
        }
    }

    /* renamed from: b */
    private void m3552b() {
        this.f3177k = new Paint(1);
        this.f3178l = new RectF();
    }

    /* renamed from: c */
    private int m3553c(int i) {
        if (this.f3173g.isEmpty()) {
            return getPaddingTop();
        }
        int intValue;
        int i2 = 0;
        int i3 = 0;
        while (i3 <= i) {
            intValue = ((Integer) this.f3173g.get(i3)).intValue() + i2;
            i3++;
            i2 = intValue;
        }
        intValue = i2 - (((Integer) this.f3171e.get(0)).intValue() / 2);
        switch (this.f3169c) {
            case 16:
            case 17:
                return intValue + (getPaddingTop() + ((((getMeasuredHeight() - getPaddingTop()) - getPaddingBottom()) - this.f3175i) / 2));
            case 80:
                return intValue + (getPaddingBottom() - this.f3175i);
            default:
                return intValue + getPaddingTop();
        }
    }

    /* renamed from: a */
    public final void m3554a() {
        this.f3168b.clear();
        requestLayout();
        invalidate();
    }

    /* renamed from: a */
    public final void m3555a(int i) {
        this.f3169c = 17;
    }

    /* renamed from: a */
    public final void m3556a(String str, KluUZYuxme kluUZYuxme) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Can not add null text");
        if (!str.isEmpty()) {
            this.f3168b.add(new jjbQypPegg(this, str, kluUZYuxme));
            StringBuilder stringBuilder = new StringBuilder();
            Iterator it = this.f3168b.iterator();
            while (it.hasNext()) {
                stringBuilder.append(TextUtils.join(" ", ((jjbQypPegg) it.next()).m3550a()));
            }
            setContentDescription(stringBuilder.toString());
            requestLayout();
            invalidate();
        }
    }

    protected void onDraw(Canvas canvas) {
        if (getBackground() != null) {
            getBackground().draw(canvas);
        }
        int b = m3551b(0);
        int c = m3553c(0);
        Iterator it = this.f3168b.iterator();
        int i = 0;
        int i2 = b;
        int i3 = c;
        c = 0;
        while (it.hasNext()) {
            jjbQypPegg jjbqyppegg = (jjbQypPegg) it.next();
            jjbqyppegg.f3161f.getFontMetrics(this.f3167a);
            this.f3176j = jjbqyppegg.f3165j;
            int i4 = i3;
            i3 = i2;
            i2 = i;
            i = c;
            for (int i5 = 0; i5 < jjbqyppegg.f3163h.length; i5++) {
                String str = jjbqyppegg.f3163h[i5];
                int i6 = jjbqyppegg.f3164i[i5];
                if (i2 + i6 > ((Integer) this.f3172f.get(i)).intValue() && this.f3172f.size() > i + 1) {
                    i++;
                    i3 = m3551b(i);
                    i4 = m3553c(i);
                    i2 = 0;
                }
                i2 += this.f3176j + i6;
                if (jjbqyppegg.f3162g != null) {
                    canvas.drawText(str, ((float) ((i3 + 0) + (i6 / 2))) + jjbqyppegg.f3159d, (((float) i4) - this.f3167a.bottom) + jjbqyppegg.f3160e, jjbqyppegg.f3162g);
                }
                canvas.drawText(str, (float) ((i3 + 0) + (i6 / 2)), ((float) i4) - this.f3167a.bottom, jjbqyppegg.f3161f);
                i3 += this.f3176j + i6;
            }
            c = i;
            i = i2;
            i2 = i3;
            i3 = i4;
        }
    }

    protected void onMeasure(int i, int i2) {
        upgqDBbsrL a = upgqDBbsrL.m3237a();
        this.f3179m = a.m3253b(8.0f);
        this.f3180n = a.m3253b(12.0f);
        int mode = MeasureSpec.getMode(i);
        int size = MeasureSpec.getSize(i);
        int mode2 = MeasureSpec.getMode(i2);
        int size2 = MeasureSpec.getSize(i2);
        int paddingLeft = getPaddingLeft();
        int paddingRight = getPaddingRight();
        Iterator it = this.f3168b.iterator();
        paddingRight = paddingLeft + paddingRight;
        while (it.hasNext()) {
            jjbQypPegg jjbqyppegg = (jjbQypPegg) it.next();
            this.f3176j = jjbqyppegg.f3165j;
            int i3 = this.f3176j;
            int length = jjbqyppegg.f3163h.length;
            int[] c = jjbqyppegg.f3164i;
            int length2 = c.length;
            paddingLeft = paddingRight + (i3 * length);
            paddingRight = 0;
            while (paddingRight < length2) {
                i3 = c[paddingRight];
                paddingRight++;
                paddingLeft += i3;
            }
            paddingRight = paddingLeft;
        }
        paddingLeft = paddingRight - this.f3176j;
        switch (mode) {
            case Integer.MIN_VALUE:
                paddingRight = Math.min(paddingLeft, size);
                break;
            case 1073741824:
                paddingRight = size;
                break;
            default:
                paddingRight = paddingLeft;
                break;
        }
        this.f3174h = (paddingRight - getPaddingLeft()) - getPaddingRight();
        while (true) {
            Iterator it2;
            this.f3171e.clear();
            this.f3172f.clear();
            this.f3173g.clear();
            this.f3176j = 0;
            Iterator it3 = this.f3168b.iterator();
            i3 = 0;
            int i4 = 0;
            mode = 0;
            size = 0;
            while (it3.hasNext()) {
                jjbqyppegg = (jjbQypPegg) it3.next();
                jjbqyppegg.f3161f.getFontMetrics(this.f3167a);
                this.f3176j = jjbqyppegg.f3165j;
                for (int i5 : jjbqyppegg.f3164i) {
                    if (i4 + i5 <= this.f3174h) {
                        i3 = Math.max(i3, (int) jjbqyppegg.f3158c);
                        size = this.f3176j;
                        i4 += i5 + size;
                        mode = Math.max(mode, ((int) ((-this.f3167a.top) + this.f3167a.bottom)) + i3);
                    } else {
                        this.f3171e.add(Integer.valueOf(i3));
                        this.f3172f.add(Integer.valueOf(i4 - size));
                        this.f3173g.add(Integer.valueOf(mode));
                        i3 = (int) jjbqyppegg.f3158c;
                        size = this.f3176j;
                        i4 = i5 + size;
                        mode = ((int) ((-this.f3167a.top) + this.f3167a.bottom)) + i3;
                    }
                }
            }
            this.f3171e.add(Integer.valueOf(i3));
            this.f3172f.add(Integer.valueOf(i4 - size));
            this.f3173g.add(Integer.valueOf(mode));
            if (this.f3172f.size() > this.f3170d) {
                it2 = this.f3168b.iterator();
                while (it2.hasNext()) {
                    if (!jjbQypPegg.m3546f((jjbQypPegg) it2.next())) {
                    }
                }
            }
            this.f3175i = 0;
            it2 = this.f3173g.iterator();
            while (it2.hasNext()) {
                this.f3175i = ((Integer) it2.next()).intValue() + this.f3175i;
            }
            paddingLeft = (this.f3175i + getPaddingTop()) + getPaddingBottom();
            switch (mode2) {
                case Integer.MIN_VALUE:
                    paddingLeft = Math.min(paddingLeft, size2);
                    break;
                case 1073741824:
                    paddingLeft = size2;
                    break;
            }
            setMeasuredDimension(paddingRight, paddingLeft);
            return;
        }
    }
}
