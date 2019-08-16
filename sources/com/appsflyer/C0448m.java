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
final class C0448m {

    /* renamed from: com.appsflyer.m$a */
    static final class C0449a {

        /* renamed from: ˎ */
        private final boolean f303;

        /* renamed from: ˏ */
        private final String f304;

        C0449a(String str, boolean z) {
            this.f304 = str;
            this.f303 = z;
        }

        /* renamed from: ॱ */
        public final String mo6589() {
            return this.f304;
        }

        /* access modifiers changed from: 0000 */
        /* renamed from: ˎ */
        public final boolean mo6588() {
            return this.f303;
        }
    }

    /* renamed from: com.appsflyer.m$d */
    static final class C0450d implements IInterface {

        /* renamed from: ॱ */
        private IBinder f305;

        C0450d(IBinder iBinder) {
            this.f305 = iBinder;
        }

        public final IBinder asBinder() {
            return this.f305;
        }

        /* renamed from: ˏ */
        public final String mo6592() throws RemoteException {
            Parcel obtain = Parcel.obtain();
            Parcel obtain2 = Parcel.obtain();
            try {
                obtain.writeInterfaceToken(AdvertisingInterface.ADVERTISING_ID_SERVICE_INTERFACE_TOKEN);
                this.f305.transact(1, obtain, obtain2, 0);
                obtain2.readException();
                return obtain2.readString();
            } finally {
                obtain2.recycle();
                obtain.recycle();
            }
        }

        /* access modifiers changed from: 0000 */
        /* renamed from: ˊ */
        public final boolean mo6591() throws RemoteException {
            boolean z = true;
            Parcel obtain = Parcel.obtain();
            Parcel obtain2 = Parcel.obtain();
            try {
                obtain.writeInterfaceToken(AdvertisingInterface.ADVERTISING_ID_SERVICE_INTERFACE_TOKEN);
                obtain.writeInt(1);
                this.f305.transact(2, obtain, obtain2, 0);
                obtain2.readException();
                if (obtain2.readInt() == 0) {
                    z = false;
                }
                return z;
            } finally {
                obtain2.recycle();
                obtain.recycle();
            }
        }
    }

    /* renamed from: com.appsflyer.m$e */
    static final class C0451e implements ServiceConnection {

        /* renamed from: ˊ */
        private boolean f306;

        /* renamed from: ॱ */
        private final LinkedBlockingQueue<IBinder> f307;

        private C0451e() {
            this.f306 = false;
            this.f307 = new LinkedBlockingQueue<>(1);
        }

        /* synthetic */ C0451e(byte b) {
            this();
        }

        public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
            try {
                this.f307.put(iBinder);
            } catch (InterruptedException e) {
            }
        }

        public final void onServiceDisconnected(ComponentName componentName) {
        }

        /* renamed from: ॱ */
        public final IBinder mo6595() throws InterruptedException {
            if (this.f306) {
                throw new IllegalStateException();
            }
            this.f306 = true;
            return (IBinder) this.f307.take();
        }
    }

    C0448m() {
    }

    /* renamed from: ˏ */
    static C0449a m316(Context context) throws Exception {
        if (Looper.myLooper() == Looper.getMainLooper()) {
            throw new IllegalStateException("Cannot be called from the main thread");
        }
        try {
            context.getPackageManager().getPackageInfo("com.android.vending", 0);
            C0451e eVar = new C0451e(0);
            Intent intent = new Intent(AdvertisingInfoServiceStrategy.GOOGLE_PLAY_SERVICES_INTENT);
            intent.setPackage("com.google.android.gms");
            try {
                if (context.bindService(intent, eVar, 1)) {
                    C0450d dVar = new C0450d(eVar.mo6595());
                    C0449a aVar = new C0449a(dVar.mo6592(), dVar.mo6591());
                    if (context != null) {
                        context.unbindService(eVar);
                    }
                    return aVar;
                }
                if (context != null) {
                    context.unbindService(eVar);
                }
                throw new IOException("Google Play connection failed");
            } catch (Exception e) {
                throw e;
            } catch (Throwable th) {
                if (context != null) {
                    context.unbindService(eVar);
                }
                throw th;
            }
        } catch (Exception e2) {
            throw e2;
        }
    }
}
