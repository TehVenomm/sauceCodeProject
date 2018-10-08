package im.getsocial.sdk.ui.internal.p128a;

import android.graphics.Rect;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewTreeObserver.OnGlobalLayoutListener;
import android.view.Window;
import android.widget.FrameLayout.LayoutParams;

/* renamed from: im.getsocial.sdk.ui.internal.a.jjbQypPegg */
public final class jjbQypPegg {
    /* renamed from: a */
    private final View f2771a;
    /* renamed from: b */
    private final LayoutParams f2772b = ((LayoutParams) this.f2771a.getLayoutParams());
    /* renamed from: c */
    private int f2773c;

    /* renamed from: im.getsocial.sdk.ui.internal.a.jjbQypPegg$1 */
    class C11321 implements OnGlobalLayoutListener {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2770a;

        C11321(jjbQypPegg jjbqyppegg) {
            this.f2770a = jjbqyppegg;
        }

        public void onGlobalLayout() {
            jjbQypPegg.m3093a(this.f2770a);
        }
    }

    private jjbQypPegg(View view) {
        this.f2771a = ((ViewGroup) view).getChildAt(0);
        this.f2771a.getViewTreeObserver().addOnGlobalLayoutListener(new C11321(this));
    }

    /* renamed from: a */
    public static void m3092a(Window window) {
        jjbQypPegg jjbqyppegg = new jjbQypPegg(window.getDecorView().findViewById(16908290));
    }

    /* renamed from: a */
    static /* synthetic */ void m3093a(jjbQypPegg jjbqyppegg) {
        Rect rect = new Rect();
        jjbqyppegg.f2771a.getWindowVisibleDisplayFrame(rect);
        int i = rect.bottom - rect.top;
        if (i != jjbqyppegg.f2773c) {
            jjbqyppegg.f2772b.height = i;
            jjbqyppegg.f2771a.requestLayout();
            jjbqyppegg.f2773c = i;
        }
    }
}
