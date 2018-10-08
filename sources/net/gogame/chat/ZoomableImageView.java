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
    private float bottom;
    private Context context;
    private float height;
    private PointF last = new PointF();
    /* renamed from: m */
    private float[] f945m;
    private ScaleGestureDetector mScaleDetector;
    private Matrix matrix = new Matrix();
    private float maxScale = 4.0f;
    private float minScale = 1.0f;
    private int mode = 0;
    private float origHeight;
    private float origWidth;
    private float redundantXSpace;
    private float redundantYSpace;
    private float right;
    private float saveScale = 1.0f;
    private PointF start = new PointF();
    private float width;

    /* renamed from: net.gogame.chat.ZoomableImageView$1 */
    class C10121 implements OnTouchListener {
        C10121() {
        }

        public boolean onTouch(View view, MotionEvent motionEvent) {
            float f = 0.0f;
            ZoomableImageView.this.mScaleDetector.onTouchEvent(motionEvent);
            ZoomableImageView.this.matrix.getValues(ZoomableImageView.this.f945m);
            float f2 = ZoomableImageView.this.f945m[2];
            float f3 = ZoomableImageView.this.f945m[5];
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
                        float f4 = pointF.x - ZoomableImageView.this.last.x;
                        float f5 = pointF.y - ZoomableImageView.this.last.y;
                        float round = (float) Math.round(ZoomableImageView.this.origHeight * ZoomableImageView.this.saveScale);
                        if (((float) Math.round(ZoomableImageView.this.origWidth * ZoomableImageView.this.saveScale)) < ZoomableImageView.this.width) {
                            if (f3 + f5 > 0.0f) {
                                f5 = -f3;
                            } else if (f3 + f5 < (-ZoomableImageView.this.bottom)) {
                                f5 = -(ZoomableImageView.this.bottom + f3);
                            }
                        } else if (round >= ZoomableImageView.this.height) {
                            if (f2 + f4 > 0.0f) {
                                f4 = -f2;
                            } else if (f2 + f4 < (-ZoomableImageView.this.right)) {
                                f4 = -(ZoomableImageView.this.right + f2);
                            }
                            if (f3 + f5 > 0.0f) {
                                f5 = -f3;
                                f = f4;
                            } else if (f3 + f5 < (-ZoomableImageView.this.bottom)) {
                                f5 = -(ZoomableImageView.this.bottom + f3);
                                f = f4;
                            } else {
                                f = f4;
                            }
                        } else if (f2 + f4 > 0.0f) {
                            f = -f2;
                            f5 = 0.0f;
                        } else if (f2 + f4 < (-ZoomableImageView.this.right)) {
                            f = -(ZoomableImageView.this.right + f2);
                            f5 = 0.0f;
                        } else {
                            f5 = 0.0f;
                            f = f4;
                        }
                        ZoomableImageView.this.matrix.postTranslate(f, f5);
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
    }

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
            float f;
            if (ZoomableImageView.this.origWidth * ZoomableImageView.this.saveScale <= ZoomableImageView.this.width || ZoomableImageView.this.origHeight * ZoomableImageView.this.saveScale <= ZoomableImageView.this.height) {
                ZoomableImageView.this.matrix.postScale(scaleFactor, scaleFactor, ZoomableImageView.this.width / 2.0f, ZoomableImageView.this.height / 2.0f);
                if (scaleFactor < 1.0f) {
                    ZoomableImageView.this.matrix.getValues(ZoomableImageView.this.f945m);
                    access$700 = ZoomableImageView.this.f945m[2];
                    f = ZoomableImageView.this.f945m[5];
                    if (scaleFactor < 1.0f) {
                        if (((float) Math.round(ZoomableImageView.this.origWidth * ZoomableImageView.this.saveScale)) < ZoomableImageView.this.width) {
                            if (f < (-ZoomableImageView.this.bottom)) {
                                ZoomableImageView.this.matrix.postTranslate(0.0f, -(ZoomableImageView.this.bottom + f));
                            } else if (f > 0.0f) {
                                ZoomableImageView.this.matrix.postTranslate(0.0f, -f);
                            }
                        } else if (access$700 < (-ZoomableImageView.this.right)) {
                            ZoomableImageView.this.matrix.postTranslate(-(access$700 + ZoomableImageView.this.right), 0.0f);
                        } else if (access$700 > 0.0f) {
                            ZoomableImageView.this.matrix.postTranslate(-access$700, 0.0f);
                        }
                    }
                }
            } else {
                ZoomableImageView.this.matrix.postScale(scaleFactor, scaleFactor, scaleGestureDetector.getFocusX(), scaleGestureDetector.getFocusY());
                ZoomableImageView.this.matrix.getValues(ZoomableImageView.this.f945m);
                access$700 = ZoomableImageView.this.f945m[2];
                f = ZoomableImageView.this.f945m[5];
                if (scaleFactor < 1.0f) {
                    if (access$700 < (-ZoomableImageView.this.right)) {
                        ZoomableImageView.this.matrix.postTranslate(-(access$700 + ZoomableImageView.this.right), 0.0f);
                    } else if (access$700 > 0.0f) {
                        ZoomableImageView.this.matrix.postTranslate(-access$700, 0.0f);
                    }
                    if (f < (-ZoomableImageView.this.bottom)) {
                        ZoomableImageView.this.matrix.postTranslate(0.0f, -(ZoomableImageView.this.bottom + f));
                    } else if (f > 0.0f) {
                        ZoomableImageView.this.matrix.postTranslate(0.0f, -f);
                    }
                }
            }
            return true;
        }
    }

    public ZoomableImageView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        super.setClickable(true);
        this.context = context;
        this.mScaleDetector = new ScaleGestureDetector(context, new ScaleListener());
        this.matrix.setTranslate(1.0f, 1.0f);
        this.f945m = new float[9];
        setImageMatrix(this.matrix);
        setScaleType(ScaleType.MATRIX);
        setOnTouchListener(new C10121());
    }

    public void setImageBitmap(Bitmap bitmap) {
        super.setImageBitmap(bitmap);
        this.bmWidth = (float) bitmap.getWidth();
        this.bmHeight = (float) bitmap.getHeight();
    }

    public void setMaxZoom(float f) {
        this.maxScale = f;
    }

    protected void onMeasure(int i, int i2) {
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
