package org.onepf.oms;

import android.content.Intent;
import android.os.Binder;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

public interface IOpenAppstore extends IInterface {

    public static abstract class Stub extends Binder implements IOpenAppstore {
        private static final String DESCRIPTOR = "org.onepf.oms.IOpenAppstore";
        static final int TRANSACTION_areOutsideLinksAllowed = 9;
        static final int TRANSACTION_getAppstoreName = 1;
        static final int TRANSACTION_getBillingServiceIntent = 5;
        static final int TRANSACTION_getPackageVersion = 4;
        static final int TRANSACTION_getProductPageIntent = 6;
        static final int TRANSACTION_getRateItPageIntent = 7;
        static final int TRANSACTION_getSameDeveloperPageIntent = 8;
        static final int TRANSACTION_isBillingAvailable = 3;
        static final int TRANSACTION_isPackageInstaller = 2;

        private static class Proxy implements IOpenAppstore {
            private IBinder mRemote;

            Proxy(IBinder iBinder) {
                this.mRemote = iBinder;
            }

            public boolean areOutsideLinksAllowed() throws RemoteException {
                boolean z = false;
                Parcel obtain = Parcel.obtain();
                Parcel obtain2 = Parcel.obtain();
                try {
                    obtain.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(9, obtain, obtain2, 0);
                    obtain2.readException();
                    if (obtain2.readInt() != 0) {
                        z = true;
                    }
                    return z;
                } finally {
                    obtain2.recycle();
                    obtain.recycle();
                }
            }

            public IBinder asBinder() {
                return this.mRemote;
            }

            public String getAppstoreName() throws RemoteException {
                Parcel obtain = Parcel.obtain();
                Parcel obtain2 = Parcel.obtain();
                try {
                    obtain.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(1, obtain, obtain2, 0);
                    obtain2.readException();
                    return obtain2.readString();
                } finally {
                    obtain2.recycle();
                    obtain.recycle();
                }
            }

            public Intent getBillingServiceIntent() throws RemoteException {
                Parcel obtain = Parcel.obtain();
                Parcel obtain2 = Parcel.obtain();
                try {
                    obtain.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(5, obtain, obtain2, 0);
                    obtain2.readException();
                    return obtain2.readInt() != 0 ? (Intent) Intent.CREATOR.createFromParcel(obtain2) : null;
                } finally {
                    obtain2.recycle();
                    obtain.recycle();
                }
            }

            public String getInterfaceDescriptor() {
                return Stub.DESCRIPTOR;
            }

            public int getPackageVersion(String str) throws RemoteException {
                Parcel obtain = Parcel.obtain();
                Parcel obtain2 = Parcel.obtain();
                try {
                    obtain.writeInterfaceToken(Stub.DESCRIPTOR);
                    obtain.writeString(str);
                    this.mRemote.transact(4, obtain, obtain2, 0);
                    obtain2.readException();
                    return obtain2.readInt();
                } finally {
                    obtain2.recycle();
                    obtain.recycle();
                }
            }

            public Intent getProductPageIntent(String str) throws RemoteException {
                Parcel obtain = Parcel.obtain();
                Parcel obtain2 = Parcel.obtain();
                try {
                    obtain.writeInterfaceToken(Stub.DESCRIPTOR);
                    obtain.writeString(str);
                    this.mRemote.transact(6, obtain, obtain2, 0);
                    obtain2.readException();
                    return obtain2.readInt() != 0 ? (Intent) Intent.CREATOR.createFromParcel(obtain2) : null;
                } finally {
                    obtain2.recycle();
                    obtain.recycle();
                }
            }

            public Intent getRateItPageIntent(String str) throws RemoteException {
                Parcel obtain = Parcel.obtain();
                Parcel obtain2 = Parcel.obtain();
                try {
                    obtain.writeInterfaceToken(Stub.DESCRIPTOR);
                    obtain.writeString(str);
                    this.mRemote.transact(7, obtain, obtain2, 0);
                    obtain2.readException();
                    return obtain2.readInt() != 0 ? (Intent) Intent.CREATOR.createFromParcel(obtain2) : null;
                } finally {
                    obtain2.recycle();
                    obtain.recycle();
                }
            }

            public Intent getSameDeveloperPageIntent(String str) throws RemoteException {
                Parcel obtain = Parcel.obtain();
                Parcel obtain2 = Parcel.obtain();
                try {
                    obtain.writeInterfaceToken(Stub.DESCRIPTOR);
                    obtain.writeString(str);
                    this.mRemote.transact(8, obtain, obtain2, 0);
                    obtain2.readException();
                    return obtain2.readInt() != 0 ? (Intent) Intent.CREATOR.createFromParcel(obtain2) : null;
                } finally {
                    obtain2.recycle();
                    obtain.recycle();
                }
            }

            public boolean isBillingAvailable(String str) throws RemoteException {
                boolean z = false;
                Parcel obtain = Parcel.obtain();
                Parcel obtain2 = Parcel.obtain();
                try {
                    obtain.writeInterfaceToken(Stub.DESCRIPTOR);
                    obtain.writeString(str);
                    this.mRemote.transact(3, obtain, obtain2, 0);
                    obtain2.readException();
                    if (obtain2.readInt() != 0) {
                        z = true;
                    }
                    return z;
                } finally {
                    obtain2.recycle();
                    obtain.recycle();
                }
            }

            public boolean isPackageInstaller(String str) throws RemoteException {
                boolean z = false;
                Parcel obtain = Parcel.obtain();
                Parcel obtain2 = Parcel.obtain();
                try {
                    obtain.writeInterfaceToken(Stub.DESCRIPTOR);
                    obtain.writeString(str);
                    this.mRemote.transact(2, obtain, obtain2, 0);
                    obtain2.readException();
                    if (obtain2.readInt() != 0) {
                        z = true;
                    }
                    return z;
                } finally {
                    obtain2.recycle();
                    obtain.recycle();
                }
            }
        }

        public Stub() {
            attachInterface(this, DESCRIPTOR);
        }

        public static IOpenAppstore asInterface(IBinder iBinder) {
            if (iBinder == null) {
                return null;
            }
            IInterface queryLocalInterface = iBinder.queryLocalInterface(DESCRIPTOR);
            return (queryLocalInterface == null || !(queryLocalInterface instanceof IOpenAppstore)) ? new Proxy(iBinder) : (IOpenAppstore) queryLocalInterface;
        }

        public IBinder asBinder() {
            return this;
        }

        public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
            int i3 = 0;
            switch (i) {
                case 1:
                    parcel.enforceInterface(DESCRIPTOR);
                    String appstoreName = getAppstoreName();
                    parcel2.writeNoException();
                    parcel2.writeString(appstoreName);
                    return true;
                case 2:
                    parcel.enforceInterface(DESCRIPTOR);
                    boolean isPackageInstaller = isPackageInstaller(parcel.readString());
                    parcel2.writeNoException();
                    if (isPackageInstaller) {
                        i3 = 1;
                    }
                    parcel2.writeInt(i3);
                    return true;
                case 3:
                    parcel.enforceInterface(DESCRIPTOR);
                    boolean isBillingAvailable = isBillingAvailable(parcel.readString());
                    parcel2.writeNoException();
                    if (isBillingAvailable) {
                        i3 = 1;
                    }
                    parcel2.writeInt(i3);
                    return true;
                case 4:
                    parcel.enforceInterface(DESCRIPTOR);
                    int packageVersion = getPackageVersion(parcel.readString());
                    parcel2.writeNoException();
                    parcel2.writeInt(packageVersion);
                    return true;
                case 5:
                    parcel.enforceInterface(DESCRIPTOR);
                    Intent billingServiceIntent = getBillingServiceIntent();
                    parcel2.writeNoException();
                    if (billingServiceIntent != null) {
                        parcel2.writeInt(1);
                        billingServiceIntent.writeToParcel(parcel2, 1);
                        return true;
                    }
                    parcel2.writeInt(0);
                    return true;
                case 6:
                    parcel.enforceInterface(DESCRIPTOR);
                    Intent productPageIntent = getProductPageIntent(parcel.readString());
                    parcel2.writeNoException();
                    if (productPageIntent != null) {
                        parcel2.writeInt(1);
                        productPageIntent.writeToParcel(parcel2, 1);
                        return true;
                    }
                    parcel2.writeInt(0);
                    return true;
                case 7:
                    parcel.enforceInterface(DESCRIPTOR);
                    Intent rateItPageIntent = getRateItPageIntent(parcel.readString());
                    parcel2.writeNoException();
                    if (rateItPageIntent != null) {
                        parcel2.writeInt(1);
                        rateItPageIntent.writeToParcel(parcel2, 1);
                        return true;
                    }
                    parcel2.writeInt(0);
                    return true;
                case 8:
                    parcel.enforceInterface(DESCRIPTOR);
                    Intent sameDeveloperPageIntent = getSameDeveloperPageIntent(parcel.readString());
                    parcel2.writeNoException();
                    if (sameDeveloperPageIntent != null) {
                        parcel2.writeInt(1);
                        sameDeveloperPageIntent.writeToParcel(parcel2, 1);
                        return true;
                    }
                    parcel2.writeInt(0);
                    return true;
                case 9:
                    parcel.enforceInterface(DESCRIPTOR);
                    boolean areOutsideLinksAllowed = areOutsideLinksAllowed();
                    parcel2.writeNoException();
                    if (areOutsideLinksAllowed) {
                        i3 = 1;
                    }
                    parcel2.writeInt(i3);
                    return true;
                case 1598968902:
                    parcel2.writeString(DESCRIPTOR);
                    return true;
                default:
                    return super.onTransact(i, parcel, parcel2, i2);
            }
        }
    }

    boolean areOutsideLinksAllowed() throws RemoteException;

    String getAppstoreName() throws RemoteException;

    Intent getBillingServiceIntent() throws RemoteException;

    int getPackageVersion(String str) throws RemoteException;

    Intent getProductPageIntent(String str) throws RemoteException;

    Intent getRateItPageIntent(String str) throws RemoteException;

    Intent getSameDeveloperPageIntent(String str) throws RemoteException;

    boolean isBillingAvailable(String str) throws RemoteException;

    boolean isPackageInstaller(String str) throws RemoteException;
}
