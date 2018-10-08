package com.unity3d.player;

import android.app.Activity;
import android.content.ContextWrapper;
import android.view.MotionEvent;
import android.view.MotionEvent.PointerCoords;
import android.view.View;
import java.util.Queue;
import java.util.concurrent.ConcurrentLinkedQueue;

/* renamed from: com.unity3d.player.p */
public final class C0773p implements C0760j {
    /* renamed from: a */
    private final Queue f532a = new ConcurrentLinkedQueue();
    /* renamed from: b */
    private final Activity f533b;
    /* renamed from: c */
    private Runnable f534c = new C07721(this);

    /* renamed from: com.unity3d.player.p$1 */
    final class C07721 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ C0773p f531a;

        C07721(C0773p c0773p) {
            this.f531a = c0773p;
        }

        /* renamed from: a */
        private static void m510a(View view, MotionEvent motionEvent) {
            if (C0774q.f536b) {
                C0774q.f544j.mo4193a(view, motionEvent);
            }
        }

        public final void run() {
            while (true) {
                MotionEvent motionEvent = (MotionEvent) this.f531a.f532a.poll();
                if (motionEvent != null) {
                    View decorView = this.f531a.f533b.getWindow().getDecorView();
                    int source = motionEvent.getSource();
                    if ((source & 2) != 0) {
                        switch (motionEvent.getAction() & 255) {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                decorView.dispatchTouchEvent(motionEvent);
                                break;
                            default:
                                C07721.m510a(decorView, motionEvent);
                                break;
                        }
                    } else if ((source & 4) != 0) {
                        decorView.dispatchTrackballEvent(motionEvent);
                    } else {
                        C07721.m510a(decorView, motionEvent);
                    }
                } else {
                    return;
                }
            }
        }
    }

    public C0773p(ContextWrapper contextWrapper) {
        this.f533b = (Activity) contextWrapper;
    }

    /* renamed from: a */
    private static int m511a(PointerCoords[] pointerCoordsArr, float[] fArr, int i) {
        for (int i2 = 0; i2 < pointerCoordsArr.length; i2++) {
            PointerCoords pointerCoords = new PointerCoords();
            pointerCoordsArr[i2] = pointerCoords;
            int i3 = i + 1;
            pointerCoords.orientation = fArr[i];
            int i4 = i3 + 1;
            pointerCoords.pressure = fArr[i3];
            i3 = i4 + 1;
            pointerCoords.size = fArr[i4];
            i4 = i3 + 1;
            pointerCoords.toolMajor = fArr[i3];
            i3 = i4 + 1;
            pointerCoords.toolMinor = fArr[i4];
            i4 = i3 + 1;
            pointerCoords.touchMajor = fArr[i3];
            i3 = i4 + 1;
            pointerCoords.touchMinor = fArr[i4];
            i4 = i3 + 1;
            pointerCoords.x = fArr[i3];
            i = i4 + 1;
            pointerCoords.y = fArr[i4];
        }
        return i;
    }

    /* renamed from: a */
    private static PointerCoords[] m513a(int i, float[] fArr) {
        PointerCoords[] pointerCoordsArr = new PointerCoords[i];
        C0773p.m511a(pointerCoordsArr, fArr, 0);
        return pointerCoordsArr;
    }

    /* renamed from: a */
    public final void mo4204a(long j, long j2, int i, int i2, int[] iArr, float[] fArr, int i3, float f, float f2, int i4, int i5, int i6, int i7, int i8, long[] jArr, float[] fArr2) {
        if (this.f533b != null) {
            MotionEvent obtain = MotionEvent.obtain(j, j2, i, i2, iArr, C0773p.m513a(i2, fArr), i3, f, f2, i4, i5, i6, i7);
            int i9 = 0;
            for (int i10 = 0; i10 < i8; i10++) {
                PointerCoords[] pointerCoordsArr = new PointerCoords[i2];
                i9 = C0773p.m511a(pointerCoordsArr, fArr2, i9);
                obtain.addBatch(jArr[i10], pointerCoordsArr, i3);
            }
            this.f532a.add(obtain);
            this.f533b.runOnUiThread(this.f534c);
        }
    }
}
