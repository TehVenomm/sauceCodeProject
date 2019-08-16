package net.gogame.chat;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Matrix;
import android.graphics.PointF;
import android.util.AttributeSet;
import android.view.MotionEvent;
import android.view.ScaleGestureDetector;
import android.view.ScaleGestureDetector.SimpleOnScaleGestureListener;
import android.view.View;
import android.view.View.MeasureSpec;
import android.view.View.OnTouchListener;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;

public class ZoomableImageView extends ImageView {
    private static final int CLICK = 3;
    private static final int DRAG = 1;
    private static final int NONE = 0;
    private static final int ZOOM = 2;
    private float bmHeight;
    private float bmWidth;
    /* access modifiers changed from: private */
    public float bottom;
    private Context context;
    /* access modifiers changed from: private */
    public float height;
    /* access modifiers changed from: private */
    public PointF last = new PointF();
    /* access modifiers changed from: private */

    /* renamed from: m */
    public float[] f1205m;
    /* access modifiers changed from: private */
    public ScaleGestureDetector mScaleDetector;
    /* access modifiers changed from: private */
    public Matrix matrix = new Matrix();
    /* access modifiers changed from: private */
    public float maxScale = 4.0f;
    /* access modifiers changed from: private */
    public float minScale = 1.0f;
    /* access modifiers changed from: private */
    public int mode = 0;
    /* access modifiers changed from: private */
    public float origHeight;
    /* access modifiers changed from: private */
    public float origWidth;
    /* access modifiers changed from: private */
    public float redundantXSpace;
    /* access modifiers changed from: private */
    public float redundantYSpace;
    /* access modifiers changed from: private */
    public float right;
    /* access modifiers changed from: private */
    public float saveScale = 1.0f;
    /* access modifiers changed from: private */
    public PointF start = new PointF();
    /* access modifiers changed from: private */
    public float width;

    private class ScaleListener extends SimpleOnScaleGestureListener {
        private ScaleListener() {
        }

        public boolean onScaleBegin(ScaleGestureDetector scaleGestureDetector) {
            ZoomableImageView.this.mode = 2;
            return true;
        }

        public boolean onScale(ScaleGestureDetector scaleGestureDetector) {
            float scaleFactor = scaleGestureDetector.getScaleFactor();
            float access$700 = ZoomableImageView.this.saveScale;
            ZoomableImageView.this.saveScale = ZoomableImageView.this.saveScale * scaleFactor;
            if (ZoomableImageView.this.saveScale > ZoomableImageView.this.maxScale) {
                ZoomableImageView.this.saveScale = ZoomableImageView.this.maxScale;
                scaleFactor = ZoomableImageView.this.maxScale / access$700;
            } else if (ZoomableImageView.this.saveScale < ZoomableImageView.this.minScale) {
                ZoomableImageView.this.saveScale = ZoomableImageView.this.minScale;
                scaleFactor = ZoomableImageView.this.minScale / access$700;
            }
            ZoomableImageView.this.right = ((ZoomableImageView.this.width * ZoomableImageView.this.saveScale) - ZoomableImageView.this.width) - ((ZoomableImageView.this.redundantXSpace * 2.0f) * ZoomableImageView.this.saveScale);
            ZoomableImageView.this.bottom = ((ZoomableImageView.this.height * ZoomableImageView.this.saveScale) - ZoomableImageView.this.height) - ((ZoomableImageView.this.redundantYSpace * 2.0f) * ZoomableImageView.this.saveScale);
            if (ZoomableImageView.this.origWidth * ZoomableImageView.this.saveScale <= ZoomableImageView.this.width || ZoomableImageView.this.origHeight * ZoomableImageView.this.saveScale <= ZoomableImageView.this.height) {
                ZoomableImageView.this.matrix.postScale(scaleFactor, scaleFactor, ZoomableImageView.this.width / 2.0f, ZoomableImageView.this.height / 2.0f);
                if (scaleFactor < 1.0f) {
                    ZoomableImageView.this.matrix.getValues(ZoomableImageView.this.f1205m);
                    float f = ZoomableImageView.this.f1205m[2];
                    float f2 = ZoomableImageView.this.f1205m[5];
                    if (scaleFactor < 1.0f) {
                        if (((float) Math.round(ZoomableImageView.this.origWidth * ZoomableImageView.this.saveScale)) < ZoomableImageView.this.width) {
                            if (f2 < (-ZoomableImageView.this.bottom)) {
                                ZoomableImageView.this.matrix.postTranslate(0.0f, -(ZoomableImageView.this.bottom + f2));
                            } else if (f2 > 0.0f) {
                                ZoomableImageView.this.matrix.postTranslate(0.0f, -f2);
                            }
                        } else if (f < (-ZoomableImageView.this.right)) {
                            ZoomableImageView.this.matrix.postTranslate(-(f + ZoomableImageView.this.right), 0.0f);
                        } else if (f > 0.0f) {
                            ZoomableImageView.this.matrix.postTranslate(-f, 0.0f);
                        }
                    }
                }
            } else {
                ZoomableImageView.this.matrix.postScale(scaleFactor, scaleFactor, scaleGestureDetector.getFocusX(), scaleGestureDetector.getFocusY());
                ZoomableImageView.this.matrix.getValues(ZoomableImageView.this.f1205m);
                float f3 = ZoomableImageView.this.f1205m[2];
                float f4 = ZoomableImageView.this.f1205m[5];
                if (scaleFactor < 1.0f) {
                    if (f3 < (-ZoomableImageView.this.right)) {
                        ZoomableImageView.this.matrix.postTranslate(-(f3 + ZoomableImageView.this.right), 0.0f);
                    } else if (f3 > 0.0f) {
                        ZoomableImageView.this.matrix.postTranslate(-f3, 0.0f);
                    }
                    if (f4 < (-ZoomableImageView.this.bottom)) {
                        ZoomableImageView.this.matrix.postTranslate(0.0f, -(ZoomableImageView.this.bottom + f4));
                    } else if (f4 > 0.0f) {
                        ZoomableImageView.this.matrix.postTranslate(0.0f, -f4);
                    }
                }
            }
            return true;
        }
    }

    public ZoomableImageView(Context context2, AttributeSet attributeSet) {
        super(context2, attributeSet);
        super.setClickable(true);
        this.context = context2;
        this.mScaleDetector = new ScaleGestureDetector(context2, new ScaleListener());
        this.matrix.setTranslate(1.0f, 1.0f);
        this.f1205m = new float[9];
        setImageMatrix(this.matrix);
        setScaleType(ScaleType.MATRIX);
        setOnTouchListener(new OnTouchListener() {
            public boolean onTouch(View view, MotionEvent motionEvent) {
                ZoomableImageView.this.mScaleDetector.onTouchEvent(motionEvent);
                ZoomableImageView.this.matrix.getValues(ZoomableImageView.this.f1205m);
                float f = ZoomableImageView.this.f1205m[2];
                float f2 = ZoomableImageView.this.f1205m[5];
                PointF pointF = new PointF(motionEvent.getX(), motionEvent.getY());
                switch (motionEvent.getAction()) {
                    case 0:
                        ZoomableImageView.this.last.set(motionEvent.getX(), motionEvent.getY());
                        ZoomableImageView.this.start.set(ZoomableImageView.this.last);
                        ZoomableImageView.this.mode = 1;
                        break;
                    case 1:
                        ZoomableImageView.this.mode = 0;
                        int abs = (int) Math.abs(pointF.y - ZoomableImageView.this.start.y);
                        if (((int) Math.abs(pointF.x - ZoomableImageView.this.start.x)) < 3 && abs < 3) {
                            ZoomableImageView.this.performClick();
                            break;
                        }
                    case 2:
                        if (ZoomableImageView.this.mode == 2 || (ZoomableImageView.this.mode == 1 && ZoomableImageView.this.saveScale > ZoomableImageView.this.minScale)) {
                            float f3 = pointF.x - ZoomableImageView.this.last.x;
                            float f4 = pointF.y - ZoomableImageView.this.last.y;
                            float round = (float) Math.round(ZoomableImageView.this.origHeight * ZoomableImageView.this.saveScale);
                            if (((float) Math.round(ZoomableImageView.this.origWidth * ZoomableImageView.this.saveScale)) < ZoomableImageView.this.width) {
                                if (f2 + f4 > 0.0f) {
                                    f4 = -f2;
                                    f3 = 0.0f;
                                } else if (f2 + f4 < (-ZoomableImageView.this.bottom)) {
                                    f4 = -(ZoomableImageView.this.bottom + f2);
                                    f3 = 0.0f;
                                } else {
                                    f3 = 0.0f;
                                }
                            } else if (round >= ZoomableImageView.this.height) {
                                if (f + f3 > 0.0f) {
                                    f3 = -f;
                                } else if (f + f3 < (-ZoomableImageView.this.right)) {
                                    f3 = -(ZoomableImageView.this.right + f);
                                }
                                if (f2 + f4 > 0.0f) {
                                    f4 = -f2;
                                } else if (f2 + f4 < (-ZoomableImageView.this.bottom)) {
                                    f4 = -(ZoomableImageView.this.bottom + f2);
                                }
                            } else if (f + f3 > 0.0f) {
                                f3 = -f;
                                f4 = 0.0f;
                            } else if (f + f3 < (-ZoomableImageView.this.right)) {
                                f3 = -(ZoomableImageView.this.right + f);
                                f4 = 0.0f;
                            } else {
                                f4 = 0.0f;
                            }
                            ZoomableImageView.this.matrix.postTranslate(f3, f4);
                            ZoomableImageView.this.last.set(pointF.x, pointF.y);
                            break;
                        }
                    case 5:
                        ZoomableImageView.this.last.set(motionEvent.getX(), motionEvent.getY());
                        ZoomableImageView.this.start.set(ZoomableImageView.this.last);
                        ZoomableImageView.this.mode = 2;
                        break;
                    case 6:
                        ZoomableImageView.this.mode = 0;
                        break;
                }
                ZoomableImageView.this.setImageMatrix(ZoomableImageView.this.matrix);
                ZoomableImageView.this.invalidate();
                return true;
            }
        });
    }

    public void setImageBitmap(Bitmap bitmap) {
        super.setImageBitmap(bitmap);
        this.bmWidth = (float) bitmap.getWidth();
        this.bmHeight = (float) bitmap.getHeight();
    }

    public void setMaxZoom(float f) {
        this.maxScale = f;
    }

    /* access modifiers changed from: protected */
    public void onMeasure(int i, int i2) {
        super.onMeasure(i, i2);
        this.width = (float) MeasureSpec.getSize(i);
        this.height = (float) MeasureSpec.getSize(i2);
        float min = Math.min(this.width / this.bmWidth, this.height / this.bmHeight);
        this.matrix.setScale(min, min);
        setImageMatrix(this.matrix);
        this.saveScale = 1.0f;
        this.redundantYSpace = this.height - (this.bmHeight * min);
        this.redundantXSpace = this.width - (min * this.bmWidth);
        this.redundantYSpace /= 2.0f;
        this.redundantXSpace /= 2.0f;
        this.matrix.postTranslate(this.redundantXSpace, this.redundantYSpace);
        this.origWidth = this.width - (this.redundantXSpace * 2.0f);
        this.origHeight = this.height - (this.redundantYSpace * 2.0f);
        this.right = ((this.width * this.saveScale) - this.width) - ((this.redundantXSpace * 2.0f) * this.saveScale);
        this.bottom = ((this.height * this.saveScale) - this.height) - ((this.redundantYSpace * 2.0f) * this.saveScale);
        setImageMatrix(this.matrix);
    }
}
