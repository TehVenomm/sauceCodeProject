package com.unity3d.player;

import android.app.Presentation;
import android.content.Context;
import android.hardware.display.DisplayManager;
import android.hardware.display.DisplayManager.DisplayListener;
import android.os.Bundle;
import android.view.Display;
import android.view.SurfaceHolder;
import android.view.SurfaceHolder.Callback;
import android.view.SurfaceView;
import android.view.View;
import com.facebook.internal.ServerProtocol;

/* renamed from: com.unity3d.player.k */
public final class C0764k implements C0756g {
    /* renamed from: a */
    private Object f516a = new Object[0];
    /* renamed from: b */
    private Presentation f517b;
    /* renamed from: c */
    private DisplayListener f518c;

    /* renamed from: a */
    public final void mo4198a(Context context) {
        if (this.f518c != null) {
            DisplayManager displayManager = (DisplayManager) context.getSystemService(ServerProtocol.DIALOG_PARAM_DISPLAY);
            if (displayManager != null) {
                displayManager.unregisterDisplayListener(this.f518c);
            }
        }
    }

    /* renamed from: a */
    public final void mo4199a(final UnityPlayer unityPlayer, Context context) {
        DisplayManager displayManager = (DisplayManager) context.getSystemService(ServerProtocol.DIALOG_PARAM_DISPLAY);
        if (displayManager != null) {
            displayManager.registerDisplayListener(new DisplayListener(this) {
                /* renamed from: b */
                final /* synthetic */ C0764k f509b;

                public final void onDisplayAdded(int i) {
                    unityPlayer.displayChanged(-1, null);
                }

                public final void onDisplayChanged(int i) {
                    unityPlayer.displayChanged(-1, null);
                }

                public final void onDisplayRemoved(int i) {
                    unityPlayer.displayChanged(-1, null);
                }
            }, null);
        }
    }

    /* renamed from: a */
    public final boolean mo4200a(final UnityPlayer unityPlayer, final Context context, int i) {
        synchronized (this.f516a) {
            Display display;
            if (this.f517b != null && this.f517b.isShowing()) {
                display = this.f517b.getDisplay();
                if (display != null && display.getDisplayId() == i) {
                    return true;
                }
            }
            DisplayManager displayManager = (DisplayManager) context.getSystemService(ServerProtocol.DIALOG_PARAM_DISPLAY);
            if (displayManager == null) {
                return false;
            }
            display = displayManager.getDisplay(i);
            if (display == null) {
                return false;
            }
            unityPlayer.m457b(new Runnable(this) {
                /* renamed from: d */
                final /* synthetic */ C0764k f515d;

                public final void run() {
                    synchronized (this.f515d.f516a) {
                        if (this.f515d.f517b != null) {
                            this.f515d.f517b.dismiss();
                        }
                        this.f515d.f517b = new Presentation(this, context, display) {
                            /* renamed from: a */
                            final /* synthetic */ C07632 f511a;

                            /* renamed from: com.unity3d.player.k$2$1$1 */
                            final class C07611 implements Callback {
                                /* renamed from: a */
                                final /* synthetic */ C07621 f510a;

                                C07611(C07621 c07621) {
                                    this.f510a = c07621;
                                }

                                public final void surfaceChanged(SurfaceHolder surfaceHolder, int i, int i2, int i3) {
                                    unityPlayer.displayChanged(1, surfaceHolder.getSurface());
                                }

                                public final void surfaceCreated(SurfaceHolder surfaceHolder) {
                                    unityPlayer.displayChanged(1, surfaceHolder.getSurface());
                                }

                                public final void surfaceDestroyed(SurfaceHolder surfaceHolder) {
                                    unityPlayer.displayChanged(1, null);
                                }
                            }

                            protected final void onCreate(Bundle bundle) {
                                View surfaceView = new SurfaceView(context);
                                surfaceView.getHolder().addCallback(new C07611(this));
                                setContentView(surfaceView);
                            }

                            public final void onDisplayRemoved() {
                                dismiss();
                                synchronized (this.f511a.f515d.f516a) {
                                    this.f511a.f515d.f517b = null;
                                }
                            }
                        };
                        this.f515d.f517b.show();
                    }
                }
            });
            return true;
        }
    }
}
