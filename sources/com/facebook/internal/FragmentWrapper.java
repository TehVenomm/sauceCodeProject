package com.facebook.internal;

import android.app.Activity;
import android.app.Fragment;
import android.content.Intent;

public class FragmentWrapper {
    private Fragment nativeFragment;
    private android.support.p000v4.app.Fragment supportFragment;

    public FragmentWrapper(Fragment fragment) {
        Validate.notNull(fragment, "fragment");
        this.nativeFragment = fragment;
    }

    public FragmentWrapper(android.support.p000v4.app.Fragment fragment) {
        Validate.notNull(fragment, "fragment");
        this.supportFragment = fragment;
    }

    public final Activity getActivity() {
        return this.supportFragment != null ? this.supportFragment.getActivity() : this.nativeFragment.getActivity();
    }

    public Fragment getNativeFragment() {
        return this.nativeFragment;
    }

    public android.support.p000v4.app.Fragment getSupportFragment() {
        return this.supportFragment;
    }

    public void startActivityForResult(Intent intent, int i) {
        if (this.supportFragment != null) {
            this.supportFragment.startActivityForResult(intent, i);
        } else {
            this.nativeFragment.startActivityForResult(intent, i);
        }
    }
}
