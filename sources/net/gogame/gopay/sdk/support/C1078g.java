package net.gogame.gopay.sdk.support;

import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.MotionEvent;
import android.view.View;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView.OnItemLongClickListener;

/* renamed from: net.gogame.gopay.sdk.support.g */
final class C1078g extends SimpleOnGestureListener {
    /* renamed from: a */
    final /* synthetic */ C1074c f1230a;

    private C1078g(C1074c c1074c) {
        this.f1230a = c1074c;
    }

    public final boolean onDown(MotionEvent motionEvent) {
        return this.f1230a.m918a(motionEvent);
    }

    public final boolean onFling(MotionEvent motionEvent, MotionEvent motionEvent2, float f, float f2) {
        return this.f1230a.m917a(f);
    }

    public final void onLongPress(MotionEvent motionEvent) {
        this.f1230a.m909d();
        int a = this.f1230a.m890a((int) motionEvent.getX(), (int) motionEvent.getY());
        if (a >= 0 && !this.f1230a.f1195A) {
            View childAt = this.f1230a.getChildAt(a);
            OnItemLongClickListener onItemLongClickListener = this.f1230a.getOnItemLongClickListener();
            if (onItemLongClickListener != null) {
                int g = this.f1230a.f1216p + a;
                if (onItemLongClickListener.onItemLongClick(this.f1230a, childAt, g, this.f1230a.f1202b.getItemId(g))) {
                    this.f1230a.performHapticFeedback(0);
                }
            }
        }
    }

    public final boolean onScroll(MotionEvent motionEvent, MotionEvent motionEvent2, float f, float f2) {
        this.f1230a.m899a(Boolean.valueOf(true));
        this.f1230a.setCurrentScrollState$6c40596b(C1082k.f1232b);
        this.f1230a.m909d();
        C1074c c1074c = this.f1230a;
        c1074c.f1204d += (int) f;
        C1074c.m903b(this.f1230a, Math.round(f));
        this.f1230a.requestLayout();
        return true;
    }

    public final boolean onSingleTapConfirmed(MotionEvent motionEvent) {
        this.f1230a.m909d();
        OnItemClickListener onItemClickListener = this.f1230a.getOnItemClickListener();
        int a = this.f1230a.m890a((int) motionEvent.getX(), (int) motionEvent.getY());
        if (a >= 0 && !this.f1230a.f1195A) {
            View childAt = this.f1230a.getChildAt(a);
            int g = this.f1230a.f1216p + a;
            if (onItemClickListener != null) {
                onItemClickListener.onItemClick(this.f1230a, childAt, g, this.f1230a.f1202b.getItemId(g));
                return true;
            }
        }
        if (!(this.f1230a.f1197C == null || this.f1230a.f1195A)) {
            this.f1230a.f1197C.onClick(this.f1230a);
        }
        return false;
    }
}
