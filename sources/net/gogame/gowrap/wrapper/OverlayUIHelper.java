package net.gogame.gowrap.wrapper;

import android.app.Activity;
import android.view.View;
import android.view.ViewGroup;

public final class OverlayUIHelper {
    private OverlayUIHelper() {
    }

    public static View getTopMostView(Activity activity) {
        return getLeafView(getRootView(activity));
    }

    public static View getRootView(Activity activity) {
        return activity.getWindow().getDecorView().findViewById(16908290);
    }

    public static View getLeafView(View view) {
        if (!(view instanceof ViewGroup)) {
            return view;
        }
        ViewGroup viewGroup = (ViewGroup) view;
        for (int i = 0; i < viewGroup.getChildCount(); i++) {
            View leafView = getLeafView(viewGroup.getChildAt(i));
            if (leafView != null) {
                return leafView;
            }
        }
        return null;
    }
}
