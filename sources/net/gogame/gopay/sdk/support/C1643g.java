package net.gogame.gopay.sdk.support;

import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.MotionEvent;
import android.view.View;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView.OnItemLongClickListener;

/* renamed from: net.gogame.gopay.sdk.support.g */
final class C1643g extends SimpleOnGestureListener {

    /* renamed from: a */
    final /* synthetic */ C1414c f1296a;

    private C1643g(C1414c cVar) {
        this.f1296a = cVar;
    }

    /* synthetic */ C1643g(C1414c cVar, byte b) {
        this(cVar);
    }

    public final boolean onDown(MotionEvent motionEvent) {
        return this.f1296a.mo21579a(motionEvent);
    }

    public final boolean onFling(MotionEvent motionEvent, MotionEvent motionEvent2, float f, float f2) {
        return this.f1296a.mo21578a(f);
    }

    public final void onLongPress(MotionEvent motionEvent) {
        this.f1296a.m908d();
        int a = this.f1296a.m889a((int) motionEvent.getX(), (int) motionEvent.getY());
        if (a >= 0 && !this.f1296a.f1134A) {
            View childAt = this.f1296a.getChildAt(a);
            OnItemLongClickListener onItemLongClickListener = this.f1296a.getOnItemLongClickListener();
            if (onItemLongClickListener != null) {
                int g = this.f1296a.f1155p + a;
                if (onItemLongClickListener.onItemLongClick(this.f1296a, childAt, g, this.f1296a.f1141b.getItemId(g))) {
                    this.f1296a.performHapticFeedback(0);
                }
            }
        }
    }

    public final boolean onScroll(MotionEvent motionEvent, MotionEvent motionEvent2, float f, float f2) {
        this.f1296a.m898a(Boolean.valueOf(true));
        this.f1296a.setCurrentScrollState$6c40596b(C1647k.f1298b);
        this.f1296a.m908d();
        this.f1296a.f1143d += (int) f;
        C1414c.m902b(this.f1296a, Math.round(f));
        this.f1296a.requestLayout();
        return true;
    }

    public final boolean onSingleTapConfirmed(MotionEvent motionEvent) {
        this.f1296a.m908d();
        OnItemClickListener onItemClickListener = this.f1296a.getOnItemClickListener();
        int a = this.f1296a.m889a((int) motionEvent.getX(), (int) motionEvent.getY());
        if (a >= 0 && !this.f1296a.f1134A) {
            View childAt = this.f1296a.getChildAt(a);
            int g = this.f1296a.f1155p + a;
            if (onItemClickListener != null) {
                onItemClickListener.onItemClick(this.f1296a, childAt, g, this.f1296a.f1141b.getItemId(g));
                return true;
            }
        }
        if (this.f1296a.f1136C != null && !this.f1296a.f1134A) {
            this.f1296a.f1136C.onClick(this.f1296a);
        }
        return false;
    }
}
