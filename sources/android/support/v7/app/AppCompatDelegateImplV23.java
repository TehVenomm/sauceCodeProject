package android.support.p003v7.app;

import android.app.UiModeManager;
import android.content.Context;
import android.support.annotation.RequiresApi;
import android.view.ActionMode;
import android.view.Window;
import android.view.Window.Callback;

@RequiresApi(23)
/* renamed from: android.support.v7.app.AppCompatDelegateImplV23 */
class AppCompatDelegateImplV23 extends AppCompatDelegateImplV14 {
    private final UiModeManager mUiModeManager;

    /* renamed from: android.support.v7.app.AppCompatDelegateImplV23$AppCompatWindowCallbackV23 */
    class AppCompatWindowCallbackV23 extends AppCompatWindowCallbackV14 {
        AppCompatWindowCallbackV23(Callback callback) {
            super(callback);
        }

        public ActionMode onWindowStartingActionMode(ActionMode.Callback callback) {
            return null;
        }

        public ActionMode onWindowStartingActionMode(ActionMode.Callback callback, int i) {
            if (AppCompatDelegateImplV23.this.isHandleNativeActionModesEnabled()) {
                switch (i) {
                    case 0:
                        return startAsSupportActionMode(callback);
                }
            }
            return super.onWindowStartingActionMode(callback, i);
        }
    }

    AppCompatDelegateImplV23(Context context, Window window, AppCompatCallback appCompatCallback) {
        super(context, window, appCompatCallback);
        this.mUiModeManager = (UiModeManager) context.getSystemService("uimode");
    }

    /* access modifiers changed from: 0000 */
    public int mapNightMode(int i) {
        if (i == 0 && this.mUiModeManager.getNightMode() == 0) {
            return -1;
        }
        return super.mapNightMode(i);
    }

    /* access modifiers changed from: 0000 */
    public Callback wrapWindowCallback(Callback callback) {
        return new AppCompatWindowCallbackV23(callback);
    }
}
