package com.appsflyer;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.Parcel;
import android.os.RemoteException;
import java.io.IOException;
import java.util.concurrent.LinkedBlockingQueue;

/* renamed from: com.appsflyer.m */
final class C0284m {

    /* renamed from: com.appsflyer.m$a */
    static final class C0281a {
        /* renamed from: ˎ */
        private final boolean f282;
        /* renamed from: ˏ */
        private final String f283;

        C0281a(String str, boolean z) {
            this.f283 = str;
            this.f282 = z;
        }

        /* renamed from: ॱ */
        public final String m326() {
            return this.f283;
        }

        /* renamed from: ˎ */
        final boolean m325() {
            return this.f282;
        }
    }

    /* renamed from: com.appsflyer.m$d */
    static final class C0282d implements IInterface {
        /* renamed from: ॱ */
        private IBinder f284;

        C0282d(IBinder iBinder) {
            this.f284 = iBinder;
        }

        public final IBinder asBinder() {
            return this.f284;
        }

        /* renamed from: ˏ */
        public final String m328() throws RemoteException {
            Parcel obtain = Parcel.obtain();
            Parcel obtain2 = Parcel.obtain();
            try {
                obtain.writeInterfaceToken(AdvertisingInterface.ADVERTISING_ID_SERVICE_INTERFACE_TOKEN);
                this.f284.transact(1, obtain, obtain2, 0);
                obtain2.readException();
                String readString = obtain2.readString();
                return readString;
            } finally {
                obtain2.recycle();
                obtain.recycle();
            }
        }

        /* renamed from: ˊ */
        final boolean m327() throws RemoteException {
            boolean z = true;
            Parcel obtain = Parcel.obtain();
            Parcel obtain2 = Parcel.obtain();
            try {
                obtain.writeInterfaceToken(AdvertisingInterface.ADVERTISING_ID_SERVICE_INTERFACE_TOKEN);
                obtain.writeInt(1);
                this.f284.transact(2, obtain, obtain2, 0);
                obtain2.readException();
                if (obtain2.readInt() == 0) {
                    z = false;
                }
                obtain2.recycle();
                obtain.recycle();
                return z;
            } catch (Throwable th) {
                obtain2.recycle();
                obtain.recycle();
            }
        }
    }

    /* renamed from: com.appsflyer.m$e */
    static final class C0283e implements ServiceConnection {
        /* renamed from: ˊ */
        private boolean f285;
        /* renamed from: ॱ */
        private final LinkedBlockingQueue<IBinder> f286;

        private C0283e() {
            this.f285 = false;
            this.f286 = new LinkedBlockingQueue(1);
        }

        public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
            try {
                this.f286.put(iBinder);
            } catch (InterruptedException e) {
            }
        }

        public final void onServiceDisconnected(ComponentName componentName) {
        }

        /* renamed from: ॱ */
        public final IBinder m329() throws InterruptedException {
            if (this.f285) {
                throw new IllegalStateException();
            }
            this.f285 = true;
            return (IBinder) this.f286.take();
        }
    }

    C0284m() {
    }

    /* renamed from: ˏ */
    static C0281a m330(Context context) throws Exception {
        if (Looper.myLooper() == Looper.getMainLooper()) {
            throw new IllegalStateException("Cannot be called from the main thread");
        }
        try {
            context.getPackageManager().getPackageInfo("com.android.vending", 0);
            ServiceConnection c0283e = new C0283e();
            Intent intent = new Intent(AdvertisingInfoServiceStrategy.GOOGLE_PLAY_SERVICES_INTENT);
            intent.setPackage("com.google.android.gms");
            try {
                if (context.bindService(intent, c0283e, 1)) {
                    C0282d c0282d = new C0282d(c0283e.m329());
                    C0281a c0281a = new C0281a(c0282d.m328(), c0282d.m327());
                    if (context != null) {
                        context.unbindService(c0283e);
                    }
                    return c0281a;
                }
                if (context != null) {
                    context.unbindService(c0283e);
                }
                throw new IOException("Google Play connection failed");
            } catch (Exception e) {
                throw e;
            } catch (Throwable th) {
                if (context != null) {
                    context.unbindService(c0283e);
                }
            }
        } catch (Exception e2) {
            throw e2;
        }
    }
}
