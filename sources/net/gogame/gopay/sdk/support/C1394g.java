package net.gogame.gopay.sdk.support;

import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.MotionEvent;
import android.view.View;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView.OnItemLongClickListener;

/* renamed from: net.gogame.gopay.sdk.support.g */
final class C1394g extends SimpleOnGestureListener {
    /* renamed from: a */
    final /* synthetic */ C1390c f3618a;

    private C1394g(C1390c c1390c) {
        this.f3618a = c1390c;
    }

    public final boolean onDown(MotionEvent motionEvent) {
        return this.f3618a.m3943a(motionEvent);
    }

    public final boolean onFling(MotionEvent motionEvent, MotionEvent motionEvent2, float f, float f2) {
        return this.f3618a.m3942a(f);
    }

    public final void onLongPress(MotionEvent motionEvent) {
        this.f3618a.m3934d();
        int a = this.f3618a.m3915a((int) motionEvent.getX(), (int) motionEvent.getY());
        if (a >= 0 && !this.f3618a.f3583A) {
            View childAt = this.f3618a.getChildAt(a);
            OnItemLongClickListener onItemLongClickListener = this.f3618a.getOnItemLongClickListener();
            if (onItemLongClickListener != null) {
                int g = this.f3618a.f3604p + a;
                if (onItemLongClickListener.onItemLongClick(this.f3618a, childAt, g, this.f3618a.f3590b.getItemId(g))) {
                    this.f3618a.performHapticFeedback(0);
                }
            }
        }
    }

    public final boolean onScroll(MotionEvent motionEvent, MotionEvent motionEvent2, float f, float f2) {
        this.f3618a.m3924a(Boolean.valueOf(true));
        this.f3618a.setCurrentScrollState$6c40596b(C1398k.f3620b);
        this.f3618a.m3934d();
        C1390c c1390c = this.f3618a;
        c1390c.f3592d += (int) f;
        C1390c.m3928b(this.f3618a, Math.round(f));
        this.f3618a.requestLayout();
        return true;
    }

    public final boolean onSingleTapConfirmed(MotionEvent motionEvent) {
        this.f3618a.m3934d();
        OnItemClickListener onItemClickListener = this.f3618a.getOnItemClickListener();
        int a = this.f3618a.m3915a((int) motionEvent.getX(), (int) motionEvent.getY());
        if (a >= 0 && !this.f3618a.f3583A) {
            View childAt = this.f3618a.getChildAt(a);
            int g = this.f3618a.f3604p + a;
            if (onItemClickListener != null) {
                onItemClickListener.onItemClick(this.f3618a, childAt, g, this.f3618a.f3590b.getItemId(g));
                return true;
            }
        }
        if (!(this.f3618a.f3585C == null || this.f3618a.f3583A)) {
            this.f3618a.f3585C.onClick(this.f3618a);
        }
        return false;
    }
}
