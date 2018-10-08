package com.google.android.gms.dynamic;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.FrameLayout.LayoutParams;
import android.widget.LinearLayout;
import android.widget.TextView;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.internal.zzt;
import com.google.android.gms.common.zze;
import java.util.LinkedList;

public abstract class zza<T extends LifecycleDelegate> {
    private T zzgol;
    private Bundle zzgom;
    private LinkedList<zzi> zzgon;
    private final zzo<T> zzgoo = new zzb(this);

    private final void zza(Bundle bundle, zzi zzi) {
        if (this.zzgol != null) {
            zzi.zzb(this.zzgol);
            return;
        }
        if (this.zzgon == null) {
            this.zzgon = new LinkedList();
        }
        this.zzgon.add(zzi);
        if (bundle != null) {
            if (this.zzgom == null) {
                this.zzgom = (Bundle) bundle.clone();
            } else {
                this.zzgom.putAll(bundle);
            }
        }
        zza(this.zzgoo);
    }

    public static void zzb(FrameLayout frameLayout) {
        zze instance = GoogleApiAvailability.getInstance();
        Context context = frameLayout.getContext();
        int isGooglePlayServicesAvailable = instance.isGooglePlayServicesAvailable(context);
        CharSequence zzi = zzt.zzi(context, isGooglePlayServicesAvailable);
        CharSequence zzk = zzt.zzk(context, isGooglePlayServicesAvailable);
        View linearLayout = new LinearLayout(frameLayout.getContext());
        linearLayout.setOrientation(1);
        linearLayout.setLayoutParams(new LayoutParams(-2, -2));
        frameLayout.addView(linearLayout);
        View textView = new TextView(frameLayout.getContext());
        textView.setLayoutParams(new LayoutParams(-2, -2));
        textView.setText(zzi);
        linearLayout.addView(textView);
        Intent zza = zze.zza(context, isGooglePlayServicesAvailable, null);
        if (zza != null) {
            View button = new Button(context);
            button.setId(16908313);
            button.setLayoutParams(new LayoutParams(-2, -2));
            button.setText(zzk);
            linearLayout.addView(button);
            button.setOnClickListener(new zzf(context, zza));
        }
    }

    private final void zzcu(int i) {
        while (!this.zzgon.isEmpty() && ((zzi) this.zzgon.getLast()).getState() >= i) {
            this.zzgon.removeLast();
        }
    }

    public final void onCreate(Bundle bundle) {
        zza(bundle, new zzd(this, bundle));
    }

    public final View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        FrameLayout frameLayout = new FrameLayout(layoutInflater.getContext());
        zza(bundle, new zze(this, frameLayout, layoutInflater, viewGroup, bundle));
        if (this.zzgol == null) {
            zza(frameLayout);
        }
        return frameLayout;
    }

    public final void onDestroy() {
        if (this.zzgol != null) {
            this.zzgol.onDestroy();
        } else {
            zzcu(1);
        }
    }

    public final void onDestroyView() {
        if (this.zzgol != null) {
            this.zzgol.onDestroyView();
        } else {
            zzcu(2);
        }
    }

    public final void onInflate(Activity activity, Bundle bundle, Bundle bundle2) {
        zza(bundle2, new zzc(this, activity, bundle, bundle2));
    }

    public final void onLowMemory() {
        if (this.zzgol != null) {
            this.zzgol.onLowMemory();
        }
    }

    public final void onPause() {
        if (this.zzgol != null) {
            this.zzgol.onPause();
        } else {
            zzcu(5);
        }
    }

    public final void onResume() {
        zza(null, new zzh(this));
    }

    public final void onSaveInstanceState(Bundle bundle) {
        if (this.zzgol != null) {
            this.zzgol.onSaveInstanceState(bundle);
        } else if (this.zzgom != null) {
            bundle.putAll(this.zzgom);
        }
    }

    public final void onStart() {
        zza(null, new zzg(this));
    }

    public final void onStop() {
        if (this.zzgol != null) {
            this.zzgol.onStop();
        } else {
            zzcu(4);
        }
    }

    protected void zza(FrameLayout frameLayout) {
        zze instance = GoogleApiAvailability.getInstance();
        Context context = frameLayout.getContext();
        int isGooglePlayServicesAvailable = instance.isGooglePlayServicesAvailable(context);
        CharSequence zzi = zzt.zzi(context, isGooglePlayServicesAvailable);
        CharSequence zzk = zzt.zzk(context, isGooglePlayServicesAvailable);
        View linearLayout = new LinearLayout(frameLayout.getContext());
        linearLayout.setOrientation(1);
        linearLayout.setLayoutParams(new LayoutParams(-2, -2));
        frameLayout.addView(linearLayout);
        View textView = new TextView(frameLayout.getContext());
        textView.setLayoutParams(new LayoutParams(-2, -2));
        textView.setText(zzi);
        linearLayout.addView(textView);
        Intent zza = zze.zza(context, isGooglePlayServicesAvailable, null);
        if (zza != null) {
            View button = new Button(context);
            button.setId(16908313);
            button.setLayoutParams(new LayoutParams(-2, -2));
            button.setText(zzk);
            linearLayout.addView(button);
            button.setOnClickListener(new zzf(context, zza));
        }
    }

    protected abstract void zza(zzo<T> zzo);

    public final T zzaoa() {
        return this.zzgol;
    }
}
