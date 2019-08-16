package net.gogame.gopay.sdk.support;

import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.MotionEvent;
import android.view.View;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView.OnItemLongClickListener;

/* renamed from: net.gogame.gopay.sdk.support.g */
final class C1643g extends SimpleOnGestureListener {

    /* renamed from: a */
    final /* synthetic */ C1414c f1308a;

    private C1643g(C1414c cVar) {
        this.f1308a = cVar;
    }

    /* synthetic */ C1643g(C1414c cVar, byte b) {
        this(cVar);
    }

    public final boolean onDown(MotionEvent motionEvent) {
        return this.f1308a.mo21579a(motionEvent);
    }

    public final boolean onFling(MotionEvent motionEvent, MotionEvent motionEvent2, float f, float f2) {
        return this.f1308a.mo21578a(f);
    }

    public final void onLongPress(MotionEvent motionEvent) {
        this.f1308a.m908d();
        int a = this.f1308a.m889a((int) motionEvent.getX(), (int) motionEvent.getY());
        if (a >= 0 && !this.f1308a.f1140A) {
            View childAt = this.f1308a.getChildAt(a);
            OnItemLongClickListener onItemLongClickListener = this.f1308a.getOnItemLongClickListener();
            if (onItemLongClickListener != null) {
                int g = this.f1308a.f1161p + a;
                if (onItemLongClickListener.onItemLongClick(this.f1308a, childAt, g, this.f1308a.f1147b.getItemId(g))) {
                    this.f1308a.performHapticFeedback(0);
                }
            }
        }
    }

    public final boolean onScroll(MotionEvent motionEvent, MotionEvent motionEvent2, float f, float f2) {
        this.f1308a.m898a(Boolean.valueOf(true));
        this.f1308a.setCurrentScrollState$6c40596b(C1647k.f1310b);
        this.f1308a.m908d();
        this.f1308a.f1149d += (int) f;
        C1414c.m902b(this.f1308a, Math.round(f));
        this.f1308a.requestLayout();
        return true;
    }

    public final boolean onSingleTapConfirmed(MotionEvent motionEvent) {
        this.f1308a.m908d();
        OnItemClickListener onItemClickListener = this.f1308a.getOnItemClickListener();
        int a = this.f1308a.m889a((int) motionEvent.getX(), (int) motionEvent.getY());
        if (a >= 0 && !this.f1308a.f1140A) {
            View childAt = this.f1308a.getChildAt(a);
            int g = this.f1308a.f1161p + a;
            if (onItemClickListener != null) {
                onItemClickListener.onItemClick(this.f1308a, childAt, g, this.f1308a.f1147b.getItemId(g));
                return true;
            }
        }
        if (this.f1308a.f1142C != null && !this.f1308a.f1140A) {
            this.f1308a.f1142C.onClick(this.f1308a);
        }
        return false;
    }
}
