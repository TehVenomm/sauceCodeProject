package im.getsocial.sdk.ui.internal.views;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Paint.Align;
import android.graphics.Paint.Style;
import android.graphics.Rect;
import android.text.TextPaint;
import android.text.TextUtils;
import android.text.TextUtils.TruncateAt;
import android.util.AttributeSet;
import android.view.View;
import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;
import im.getsocial.sdk.ui.internal.p125h.fOrCGNYyfk;
import im.getsocial.sdk.ui.internal.p125h.ztWNWCuZiM;
import im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme;
import im.getsocial.sdk.ui.internal.p131d.p132a.QWVUXapsSm;
import im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL;

public class AssetButton extends View {
    /* renamed from: a */
    private Bitmap f3119a;
    /* renamed from: b */
    private Bitmap f3120b;
    /* renamed from: c */
    private ztWNWCuZiM f3121c;
    /* renamed from: d */
    private ztWNWCuZiM f3122d;
    /* renamed from: e */
    private int f3123e;
    /* renamed from: f */
    private String f3124f = "";
    /* renamed from: g */
    private int f3125g;
    /* renamed from: h */
    private int f3126h;
    /* renamed from: i */
    private TextPaint f3127i;
    /* renamed from: j */
    private Paint f3128j;
    /* renamed from: k */
    private Rect f3129k;
    /* renamed from: l */
    private Rect f3130l;
    /* renamed from: m */
    private final Rect f3131m = new Rect();
    /* renamed from: n */
    private final Rect f3132n = new Rect();

    public AssetButton(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        m3499a();
    }

    /* renamed from: a */
    private void m3499a() {
        this.f3127i = new TextPaint(1);
        this.f3127i.setTextAlign(Align.CENTER);
        this.f3127i.setFilterBitmap(true);
        this.f3128j = new Paint(1);
        this.f3128j.setTextAlign(Align.CENTER);
        this.f3128j.setStyle(Style.STROKE);
        setEnabled(true);
        setClickable(true);
    }

    /* renamed from: b */
    private void m3500b() {
        this.f3131m.left = getPaddingLeft();
        this.f3131m.top = getPaddingTop();
        this.f3131m.right = getWidth() - getPaddingRight();
        this.f3131m.bottom = getHeight() - getPaddingBottom();
        invalidate();
    }

    /* renamed from: a */
    public final void m3501a(int i, int i2) {
        this.f3125g = i;
        this.f3126h = i2;
    }

    /* renamed from: a */
    public final void m3502a(KluUZYuxme kluUZYuxme) {
        this.f3127i.setTypeface(fOrCGNYyfk.m3331b(getContext(), kluUZYuxme));
        this.f3127i.setTextSize((float) upgqDBbsrL.m3237a().m3244a(kluUZYuxme.m3157d()));
        this.f3127i.getTextBounds(this.f3124f, 0, this.f3124f.length(), this.f3132n);
        this.f3127i.setColor(kluUZYuxme.m3156c().m3215a());
        this.f3128j.setTypeface(fOrCGNYyfk.m3331b(getContext(), kluUZYuxme));
        this.f3128j.setTextSize((float) upgqDBbsrL.m3237a().m3244a(kluUZYuxme.m3157d()));
        this.f3128j.setStrokeWidth((float) kluUZYuxme.m3158e());
        this.f3128j.setColor(kluUZYuxme.m3154a().m3215a());
        invalidate();
    }

    /* renamed from: a */
    public final void m3503a(String str) {
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Text can not be null");
        this.f3124f = str;
        this.f3127i.getTextBounds(this.f3124f, 0, this.f3124f.length(), this.f3132n);
        invalidate();
    }

    /* renamed from: a */
    public final void m3504a(String str, QWVUXapsSm qWVUXapsSm, String str2, QWVUXapsSm qWVUXapsSm2) {
        m3499a();
        this.f3121c = ztWNWCuZiM.m3368a(getContext(), getResources(), str, qWVUXapsSm);
        this.f3122d = ztWNWCuZiM.m3368a(getContext(), getResources(), str2, qWVUXapsSm2);
    }

    /* renamed from: a */
    public final void m3505a(String str, String str2) {
        upgqDBbsrL a = upgqDBbsrL.m3237a();
        this.f3119a = a.m3258c(getContext(), str);
        this.f3120b = a.m3258c(getContext(), str2);
        this.f3129k = new Rect(0, 0, this.f3119a.getWidth(), this.f3119a.getHeight());
        this.f3130l = new Rect(0, 0, this.f3120b.getWidth(), this.f3120b.getHeight());
    }

    protected void onDraw(Canvas canvas) {
        canvas.drawColor(this.f3123e);
        this.f3127i.setAlpha(isEnabled() ? 255 : 128);
        if (isEnabled() && isPressed()) {
            if (this.f3122d == null) {
                canvas.drawBitmap(this.f3120b, this.f3130l, this.f3131m, this.f3127i);
            } else {
                this.f3122d.setBounds(this.f3131m);
                this.f3122d.draw(canvas);
            }
            String charSequence = TextUtils.ellipsize(this.f3124f, this.f3127i, (float) (this.f3131m.width() - 20), TruncateAt.END).toString();
            canvas.drawText(charSequence, (float) (this.f3131m.left + (this.f3131m.width() / 2)), (float) ((((this.f3131m.top + (this.f3131m.height() / 2)) - this.f3132n.top) - (this.f3132n.height() / 2)) + this.f3126h), this.f3128j);
            canvas.drawText(charSequence, (float) (this.f3131m.left + (this.f3131m.width() / 2)), (float) ((((this.f3131m.top + (this.f3131m.height() / 2)) - this.f3132n.top) - (this.f3132n.height() / 2)) + this.f3126h), this.f3127i);
            return;
        }
        if (this.f3121c != null) {
            this.f3121c.setBounds(this.f3131m);
            this.f3121c.draw(canvas);
        } else if (this.f3119a != null) {
            canvas.drawBitmap(this.f3119a, this.f3129k, this.f3131m, this.f3127i);
        }
        charSequence = TextUtils.ellipsize(this.f3124f, this.f3127i, (float) (this.f3131m.width() - 20), TruncateAt.END).toString();
        canvas.drawText(charSequence, (float) (this.f3131m.left + (this.f3131m.width() / 2)), (float) ((((this.f3131m.top + (this.f3131m.height() / 2)) - this.f3132n.top) - (this.f3132n.height() / 2)) + this.f3125g), this.f3128j);
        canvas.drawText(charSequence, (float) (this.f3131m.left + (this.f3131m.width() / 2)), (float) ((((this.f3131m.top + (this.f3131m.height() / 2)) - this.f3132n.top) - (this.f3132n.height() / 2)) + this.f3125g), this.f3127i);
    }

    protected void onSizeChanged(int i, int i2, int i3, int i4) {
        m3500b();
    }

    public void setBackgroundColor(int i) {
        this.f3123e = i;
        invalidate();
    }

    public final void setEnabled(boolean z) {
        super.setEnabled(z);
        invalidate();
    }

    public void setPadding(int i, int i2, int i3, int i4) {
        super.setPadding(i, i2, i3, i4);
        m3500b();
    }

    public final void setPressed(boolean z) {
        super.setPressed(z);
        invalidate();
    }
}
