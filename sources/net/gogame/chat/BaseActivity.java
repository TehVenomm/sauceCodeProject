package net.gogame.chat;

import android.support.p000v4.app.Fragment;
import android.support.p000v4.app.FragmentActivity;

public abstract class BaseActivity extends FragmentActivity {
    public void addFragment(Fragment fragment, int i, String str) {
        getSupportFragmentManager().beginTransaction().add(i, fragment, str).commit();
    }

    public void replaceFragment(Fragment fragment, int i, String str) {
        getSupportFragmentManager().beginTransaction().replace(i, fragment, str).addToBackStack(null).commit();
    }
}
