package im.getsocial.sdk.ui.internal.p125h;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Paint.Style;
import android.graphics.PorterDuff.Mode;
import android.graphics.PorterDuffXfermode;
import android.graphics.Rect;
import android.graphics.RectF;
import im.getsocial.sdk.internal.p072g.cjrhisSQCL;

/* renamed from: im.getsocial.sdk.ui.internal.h.HptYHntaqF */
public class HptYHntaqF implements cjrhisSQCL {
    /* renamed from: a */
    private final Paint f2953a;
    /* renamed from: b */
    private final Paint f2954b;
    /* renamed from: c */
    private final int f2955c;
    /* renamed from: d */
    private final int f2956d;
    /* renamed from: e */
    private final Rect f2957e = new Rect();
    /* renamed from: f */
    private RectF f2958f = new RectF();
    /* renamed from: g */
    private Paint f2959g;

    public HptYHntaqF(int i, int i2, int i3) {
        this.f2956d = i;
        this.f2955c = i2;
        if (i2 > 0) {
            this.f2959g = new Paint();
            this.f2959g.setStyle(Style.STROKE);
            this.f2959g.setAntiAlias(true);
            this.f2959g.setColor(i3);
            this.f2959g.setStrokeWidth((float) i2);
        }
        this.f2953a = new Paint(1);
        this.f2954b = new Paint(1);
        this.f2954b.setXfermode(new PorterDuffXfermode(Mode.SRC_IN));
    }

    /* renamed from: a */
    public final Bitmap mo4552a(Bitmap bitmap) {
        int width = bitmap.getWidth();
        int height = bitmap.getHeight();
        this.f2957e.set(0, 0, width, height);
        this.f2958f.set(0.0f, 0.0f, (float) width, (float) height);
        Bitmap createBitmap = Bitmap.createBitmap(width, height, bitmap.getConfig());
        Canvas canvas = new Canvas(createBitmap);
        canvas.drawARGB(0, 0, 0, 0);
        if (this.f2956d == 0) {
            canvas.drawRect(this.f2957e, this.f2953a);
        } else {
            canvas.drawRoundRect(new RectF(this.f2957e), (float) this.f2956d, (float) this.f2956d, this.f2953a);
        }
        canvas.drawBitmap(bitmap, this.f2957e, this.f2957e, this.f2954b);
        if (this.f2955c > 0) {
            RectF rectF = this.f2958f;
            rectF.left += this.f2959g.getStrokeWidth() / 2.0f;
            rectF = this.f2958f;
            rectF.top += this.f2959g.getStrokeWidth() / 2.0f;
            rectF = this.f2958f;
            rectF.right -= this.f2959g.getStrokeWidth() / 2.0f;
            rectF = this.f2958f;
            rectF.bottom -= this.f2959g.getStrokeWidth() / 2.0f;
            canvas.drawRoundRect(this.f2958f, (float) this.f2956d, (float) this.f2956d, this.f2959g);
        }
        return createBitmap;
    }
}
