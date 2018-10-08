package im.getsocial.sdk.ui.internal.views;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Bitmap.Config;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.PorterDuff.Mode;
import android.graphics.PorterDuffColorFilter;
import android.graphics.drawable.GradientDrawable;
import android.util.AttributeSet;
import android.widget.ImageView;
import im.getsocial.sdk.ui.C1067R;

public class RoundedCornerMaskView extends ImageView {
    /* renamed from: a */
    private float f3224a;
    /* renamed from: b */
    private int f3225b;
    /* renamed from: c */
    private Bitmap f3226c;
    /* renamed from: d */
    private final Paint f3227d = new Paint();

    public RoundedCornerMaskView(Context context) {
        super(context);
    }

    public RoundedCornerMaskView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
    }

    public RoundedCornerMaskView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
    }

    /* renamed from: a */
    public final void m3593a(float f) {
        this.f3224a = f;
    }

    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        if (this.f3226c == null) {
            int width = getWidth();
            int height = getHeight();
            Canvas canvas2 = new Canvas();
            GradientDrawable gradientDrawable = (GradientDrawable) getResources().getDrawable(C1067R.drawable.rounded_corners_drawable);
            gradientDrawable.setCornerRadius(this.f3224a);
            this.f3226c = Bitmap.createBitmap(width, height, Config.ARGB_8888);
            canvas2.setBitmap(this.f3226c);
            gradientDrawable.setBounds(0, 0, width, height);
            gradientDrawable.draw(canvas2);
        }
        canvas.drawBitmap(this.f3226c, 0.0f, 0.0f, this.f3227d);
        setColorFilter(this.f3225b, Mode.SRC_OUT);
    }

    public void setBackgroundColor(int i) {
        this.f3225b = i;
        this.f3227d.setColorFilter(new PorterDuffColorFilter(this.f3225b, Mode.SRC_OUT));
    }
}
