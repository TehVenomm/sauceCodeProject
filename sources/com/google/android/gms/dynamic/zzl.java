package com.google.android.gms.dynamic;

import android.content.Intent;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import android.support.v4.view.MotionEventCompat;
import com.google.android.gms.internal.zzef;
import com.google.android.gms.internal.zzeg;

public abstract class zzl extends zzef implements zzk {
    public zzl() {
        attachInterface(this, "com.google.android.gms.dynamic.IFragmentWrapper");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        IObjectWrapper iObjectWrapper = null;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        IInterface zzaob;
        int id;
        boolean retainInstance;
        IBinder readStrongBinder;
        switch (i) {
            case 2:
                zzaob = zzaob();
                parcel2.writeNoException();
                zzeg.zza(parcel2, zzaob);
                break;
            case 3:
                Parcelable arguments = getArguments();
                parcel2.writeNoException();
                zzeg.zzb(parcel2, arguments);
                break;
            case 4:
                id = getId();
                parcel2.writeNoException();
                parcel2.writeInt(id);
                break;
            case 5:
                zzaob = zzaoc();
                parcel2.writeNoException();
                zzeg.zza(parcel2, zzaob);
                break;
            case 6:
                zzaob = zzaod();
                parcel2.writeNoException();
                zzeg.zza(parcel2, zzaob);
                break;
            case 7:
                retainInstance = getRetainInstance();
                parcel2.writeNoException();
                zzeg.zza(parcel2, retainInstance);
                break;
            case 8:
                String tag = getTag();
                parcel2.writeNoException();
                parcel2.writeString(tag);
                break;
            case 9:
                zzaob = zzaoe();
                parcel2.writeNoException();
                zzeg.zza(parcel2, zzaob);
                break;
            case 10:
                id = getTargetRequestCode();
                parcel2.writeNoException();
                parcel2.writeInt(id);
                break;
            case 11:
                retainInstance = getUserVisibleHint();
                parcel2.writeNoException();
                zzeg.zza(parcel2, retainInstance);
                break;
            case 12:
                zzaob = getView();
                parcel2.writeNoException();
                zzeg.zza(parcel2, zzaob);
                break;
            case 13:
                retainInstance = isAdded();
                parcel2.writeNoException();
                zzeg.zza(parcel2, retainInstance);
                break;
            case 14:
                retainInstance = isDetached();
                parcel2.writeNoException();
                zzeg.zza(parcel2, retainInstance);
                break;
            case 15:
                retainInstance = isHidden();
                parcel2.writeNoException();
                zzeg.zza(parcel2, retainInstance);
                break;
            case 16:
                retainInstance = isInLayout();
                parcel2.writeNoException();
                zzeg.zza(parcel2, retainInstance);
                break;
            case 17:
                retainInstance = isRemoving();
                parcel2.writeNoException();
                zzeg.zza(parcel2, retainInstance);
                break;
            case 18:
                retainInstance = isResumed();
                parcel2.writeNoException();
                zzeg.zza(parcel2, retainInstance);
                break;
            case 19:
                retainInstance = isVisible();
                parcel2.writeNoException();
                zzeg.zza(parcel2, retainInstance);
                break;
            case 20:
                readStrongBinder = parcel.readStrongBinder();
                if (readStrongBinder != null) {
                    zzaob = readStrongBinder.queryLocalInterface("com.google.android.gms.dynamic.IObjectWrapper");
                    iObjectWrapper = zzaob instanceof IObjectWrapper ? (IObjectWrapper) zzaob : new zzm(readStrongBinder);
                }
                zzz(iObjectWrapper);
                parcel2.writeNoException();
                break;
            case 21:
                setHasOptionsMenu(zzeg.zza(parcel));
                parcel2.writeNoException();
                break;
            case 22:
                setMenuVisibility(zzeg.zza(parcel));
                parcel2.writeNoException();
                break;
            case 23:
                setRetainInstance(zzeg.zza(parcel));
                parcel2.writeNoException();
                break;
            case MotionEventCompat.AXIS_DISTANCE /*24*/:
                setUserVisibleHint(zzeg.zza(parcel));
                parcel2.writeNoException();
                break;
            case 25:
                startActivity((Intent) zzeg.zza(parcel, Intent.CREATOR));
                parcel2.writeNoException();
                break;
            case 26:
                startActivityForResult((Intent) zzeg.zza(parcel, Intent.CREATOR), parcel.readInt());
                parcel2.writeNoException();
                break;
            case MotionEventCompat.AXIS_RELATIVE_X /*27*/:
                readStrongBinder = parcel.readStrongBinder();
                if (readStrongBinder != null) {
                    zzaob = readStrongBinder.queryLocalInterface("com.google.android.gms.dynamic.IObjectWrapper");
                    iObjectWrapper = zzaob instanceof IObjectWrapper ? (IObjectWrapper) zzaob : new zzm(readStrongBinder);
                }
                zzaa(iObjectWrapper);
                parcel2.writeNoException();
                break;
            default:
                return false;
        }
        return true;
    }
}
