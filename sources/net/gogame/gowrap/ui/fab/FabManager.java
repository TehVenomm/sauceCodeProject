package net.gogame.gowrap.ui.fab;

import android.app.Activity;
import android.view.MotionEvent;
import java.util.HashMap;
import java.util.Map;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.ui.ActivityHelper;
import net.gogame.gowrap.ui.DialogStartMenuManager;
import net.gogame.gowrap.ui.fab.Fab.ClickListener;

public class FabManager {
    private static final Map<Activity, Fab> fabMap = new HashMap();

    public static void showMenu(Activity activity) {
        DialogStartMenuManager.INSTANCE.showMenu(activity);
    }

    public static void onCreate(final Activity activity) {
        if (((Fab) fabMap.get(activity)) == null) {
            Fab popupWindowFab = new PopupWindowFab(false, false);
            popupWindowFab.setClickListener(new ClickListener() {
                public void onClick(Fab fab, MotionEvent motionEvent) {
                    FabManager.showMenu(activity);
                }
            });
            fabMap.put(activity, popupWindowFab);
        }
    }

    public static void showFab(Activity activity) {
        Fab fab = (Fab) fabMap.get(activity);
        if (fab != null) {
            fab.show(activity);
            if (Wrapper.INSTANCE.isServerDown()) {
                fab.update(activity);
            }
        }
    }

    public static void update(Activity activity) {
        Fab fab = (Fab) fabMap.get(activity);
        if (fab != null) {
            fab.update(activity);
        }
    }

    public static void hideFab(Activity activity) {
        Fab fab = (Fab) fabMap.get(activity);
        if (fab != null) {
            fab.hide(activity);
        }
    }

    public static void onResume(Activity activity) {
        showFab(activity);
    }

    public static void onPause(Activity activity) {
        hideFab(activity);
    }

    public static void onDestroy(Activity activity) {
        Fab fab = (Fab) fabMap.get(activity);
        if (fab != null) {
            fab.destroy(activity);
            fabMap.remove(activity);
        }
    }

    public static boolean handleTouchEvent(Activity activity, MotionEvent motionEvent) {
        if (activity == null) {
            activity = ActivityHelper.INSTANCE.getCurrentActivity();
        }
        Fab fab = (Fab) fabMap.get(activity);
        if (fab != null && fab.handleTouchEvent(motionEvent)) {
            return true;
        }
        if (motionEvent.getActionMasked() != 5) {
            return false;
        }
        if (motionEvent.getActionIndex() < 2) {
            return false;
        }
        showMenu(activity);
        return true;
    }
}
