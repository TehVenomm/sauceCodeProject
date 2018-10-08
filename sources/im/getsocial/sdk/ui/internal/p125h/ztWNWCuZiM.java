package im.getsocial.sdk.ui.internal.p125h;

import android.content.Context;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.Bitmap.Config;
import android.graphics.Canvas;
import android.graphics.Rect;
import android.graphics.drawable.NinePatchDrawable;
import im.getsocial.sdk.ui.internal.p131d.p132a.QWVUXapsSm;
import im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

/* renamed from: im.getsocial.sdk.ui.internal.h.ztWNWCuZiM */
public final class ztWNWCuZiM extends NinePatchDrawable {
    /* renamed from: a */
    private int f3005a;
    /* renamed from: b */
    private int f3006b;
    /* renamed from: c */
    private Bitmap f3007c;
    /* renamed from: d */
    private Canvas f3008d;
    /* renamed from: e */
    private final Rect f3009e = new Rect();
    /* renamed from: f */
    private final Rect f3010f = new Rect();

    private ztWNWCuZiM(Resources resources, Bitmap bitmap, byte[] bArr) {
        super(resources, bitmap, bArr, new Rect(), null);
    }

    /* renamed from: a */
    public static ztWNWCuZiM m3368a(Context context, Resources resources, String str, QWVUXapsSm qWVUXapsSm) {
        Rect rect = new Rect(upgqDBbsrL.m3237a().m3243a((float) qWVUXapsSm.m3110c().m3174a()), upgqDBbsrL.m3237a().m3243a((float) qWVUXapsSm.m3108a().m3174a()), upgqDBbsrL.m3237a().m3243a((float) qWVUXapsSm.m3111d().m3174a()), upgqDBbsrL.m3237a().m3243a((float) qWVUXapsSm.m3109b().m3174a()));
        Bitmap a = upgqDBbsrL.m3237a().m3247a(context, str, null);
        if (a == null) {
            return null;
        }
        a = Bitmap.createScaledBitmap(a, upgqDBbsrL.m3237a().m3243a((float) a.getWidth()), upgqDBbsrL.m3237a().m3243a((float) a.getHeight()), true);
        Rect rect2 = new Rect(rect.left, rect.top, rect.right, rect.bottom);
        ByteBuffer order = ByteBuffer.allocate(84).order(ByteOrder.nativeOrder());
        order.put((byte) 1);
        order.put((byte) 2);
        order.put((byte) 2);
        order.put((byte) 9);
        order.putInt(0);
        order.putInt(0);
        order.putInt(0);
        order.putInt(0);
        order.putInt(0);
        order.putInt(0);
        order.putInt(0);
        order.putInt(rect2.left);
        if (a.getWidth() > rect2.left + rect2.right) {
            order.putInt(a.getWidth() - rect2.right);
        } else {
            order.putInt(rect2.left - 1);
        }
        order.putInt(rect2.top);
        if (a.getHeight() > rect2.top + rect2.bottom) {
            order.putInt(a.getHeight() - rect2.bottom);
        } else {
            order.putInt(rect2.top - 1);
        }
        order.putInt(1);
        order.putInt(1);
        order.putInt(1);
        order.putInt(1);
        order.putInt(1);
        order.putInt(1);
        order.putInt(1);
        order.putInt(1);
        order.putInt(1);
        ztWNWCuZiM ztwnwcuzim = new ztWNWCuZiM(resources, a, order.array());
        ztwnwcuzim.setFilterBitmap(true);
        return ztwnwcuzim;
    }

    public final void draw(Canvas canvas) {
        this.f3010f.set(getBounds());
        int i = this.f3010f.right - this.f3010f.left;
        int i2 = this.f3010f.bottom - this.f3010f.top;
        if (!(i == this.f3005a && i2 == this.f3006b)) {
            this.f3005a = i;
            this.f3006b = i2;
            this.f3007c = Bitmap.createBitmap(i, i2, Config.ARGB_8888);
            this.f3008d = new Canvas(this.f3007c);
            this.f3009e.set(0, 0, i, i2);
        }
        setBounds(this.f3009e);
        super.draw(this.f3008d);
        setBounds(this.f3010f);
        canvas.drawBitmap(this.f3007c, this.f3009e, this.f3010f, getPaint());
    }
}
