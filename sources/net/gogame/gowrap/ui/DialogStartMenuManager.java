package net.gogame.gowrap.ui;

import android.app.Activity;
import android.content.Intent;
import net.gogame.gowrap.GoWrapImpl;

public class DialogStartMenuManager implements StartMenuManager {
    private static final Class<? extends Activity> DEFAULT_ACTIVITY_CLASS = MainActivity.class;
    public static final DialogStartMenuManager INSTANCE = new DialogStartMenuManager();

    public void showMenu(Activity activity) {
        Class mainActivity = GoWrapImpl.INSTANCE.getMainActivity();
        if (mainActivity == null) {
            mainActivity = DEFAULT_ACTIVITY_CLASS;
        }
        Intent intent = new Intent(activity, mainActivity);
        intent.setFlags(131072);
        activity.startActivity(intent);
    }

    public void hideMenu() {
    }
}
