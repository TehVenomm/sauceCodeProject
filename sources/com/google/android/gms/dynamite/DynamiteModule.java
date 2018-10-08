package com.google.android.gms.dynamite;

import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;
import android.util.Log;
import com.appsflyer.share.Constants;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.DynamiteApi;
import com.google.android.gms.common.zze;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.dynamic.zzn;
import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

public final class DynamiteModule {
    private static Boolean zzgoz;
    private static zzk zzgpa;
    private static zzm zzgpb;
    private static String zzgpc;
    private static final ThreadLocal<zza> zzgpd = new ThreadLocal();
    private static final zzi zzgpe = new zza();
    public static final zzd zzgpf = new zzb();
    private static zzd zzgpg = new zzc();
    public static final zzd zzgph = new zzd();
    public static final zzd zzgpi = new zze();
    public static final zzd zzgpj = new zzf();
    public static final zzd zzgpk = new zzg();
    private final Context zzgpl;

    @DynamiteApi
    public static class DynamiteLoaderClassLoader {
        public static ClassLoader sClassLoader;
    }

    static final class zza {
        public Cursor zzgpm;

        private zza() {
        }
    }

    static final class zzb implements zzi {
        private final int zzgpn;
        private final int zzgpo = 0;

        public zzb(int i, int i2) {
            this.zzgpn = i;
        }

        public final int zzae(Context context, String str) {
            return this.zzgpn;
        }

        public final int zzb(Context context, String str, boolean z) {
            return 0;
        }
    }

    public static final class zzc extends Exception {
        private zzc(String str) {
            super(str);
        }

        private zzc(String str, Throwable th) {
            super(str, th);
        }
    }

    public interface zzd {
        zzj zza(Context context, String str, zzi zzi) throws zzc;
    }

    private DynamiteModule(Context context) {
        this.zzgpl = (Context) zzbp.zzu(context);
    }

    private static Context zza(Context context, String str, int i, Cursor cursor, zzm zzm) {
        try {
            return (Context) zzn.zzab(zzm.zza(zzn.zzw(context), str, i, zzn.zzw(cursor)));
        } catch (Exception e) {
            String valueOf = String.valueOf(e.toString());
            Log.e("DynamiteModule", valueOf.length() != 0 ? "Failed to load DynamiteLoader: ".concat(valueOf) : new String("Failed to load DynamiteLoader: "));
            return null;
        }
    }

    public static DynamiteModule zza(Context context, zzd zzd, String str) throws zzc {
        zzj zza;
        zza zza2 = (zza) zzgpd.get();
        zza zza3 = new zza();
        zzgpd.set(zza3);
        DynamiteModule zzag;
        try {
            zza = zzd.zza(context, str, zzgpe);
            Log.i("DynamiteModule", new StringBuilder((String.valueOf(str).length() + 68) + String.valueOf(str).length()).append("Considering local module ").append(str).append(":").append(zza.zzgpp).append(" and remote module ").append(str).append(":").append(zza.zzgpq).toString());
            if (zza.zzgpr == 0 || ((zza.zzgpr == -1 && zza.zzgpp == 0) || (zza.zzgpr == 1 && zza.zzgpq == 0))) {
                throw new zzc("No acceptable module found. Local version is " + zza.zzgpp + " and remote version is " + zza.zzgpq + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
            } else if (zza.zzgpr == -1) {
                zzag = zzag(context, str);
                if (zza3.zzgpm != null) {
                    zza3.zzgpm.close();
                }
                zzgpd.set(zza2);
                return zzag;
            } else if (zza.zzgpr == 1) {
                zzag = zza(context, str, zza.zzgpq);
                if (zza3.zzgpm != null) {
                    zza3.zzgpm.close();
                }
                zzgpd.set(zza2);
                return zzag;
            } else {
                throw new zzc("VersionPolicy returned invalid code:" + zza.zzgpr);
            }
        } catch (Throwable e) {
            String valueOf = String.valueOf(e.getMessage());
            Log.w("DynamiteModule", valueOf.length() != 0 ? "Failed to load remote module: ".concat(valueOf) : new String("Failed to load remote module: "));
            if (zza.zzgpp == 0 || zzd.zza(context, str, new zzb(zza.zzgpp, 0)).zzgpr != -1) {
                throw new zzc("Remote load failed. No local fallback found.", e);
            }
            zzag = zzag(context, str);
            if (zza3.zzgpm != null) {
                zza3.zzgpm.close();
            }
            zzgpd.set(zza2);
            return zzag;
        } catch (Throwable th) {
            if (zza3.zzgpm != null) {
                zza3.zzgpm.close();
            }
            zzgpd.set(zza2);
        }
    }

    private static DynamiteModule zza(Context context, String str, int i) throws zzc {
        synchronized (DynamiteModule.class) {
            try {
                Boolean bool = zzgoz;
            } catch (Throwable th) {
                while (true) {
                    Class cls = DynamiteModule.class;
                }
            }
        }
        if (bool != null) {
            return bool.booleanValue() ? zzc(context, str, i) : zzb(context, str, i);
        } else {
            throw new zzc("Failed to determine which loading route to use.");
        }
    }

    private static void zza(ClassLoader classLoader) throws zzc {
        Throwable e;
        try {
            zzm zzm;
            IBinder iBinder = (IBinder) classLoader.loadClass("com.google.android.gms.dynamiteloader.DynamiteLoaderV2").getConstructor(new Class[0]).newInstance(new Object[0]);
            if (iBinder == null) {
                zzm = null;
            } else {
                IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.dynamite.IDynamiteLoaderV2");
                if (queryLocalInterface instanceof zzm) {
                    zzm = (zzm) queryLocalInterface;
                } else {
                    Object zzn = new zzn(iBinder);
                }
            }
            zzgpb = zzm;
        } catch (ClassNotFoundException e2) {
            e = e2;
            throw new zzc("Failed to instantiate dynamite loader", e);
        } catch (IllegalAccessException e3) {
            e = e3;
            throw new zzc("Failed to instantiate dynamite loader", e);
        } catch (InstantiationException e4) {
            e = e4;
            throw new zzc("Failed to instantiate dynamite loader", e);
        } catch (InvocationTargetException e5) {
            e = e5;
            throw new zzc("Failed to instantiate dynamite loader", e);
        } catch (NoSuchMethodException e6) {
            e = e6;
            throw new zzc("Failed to instantiate dynamite loader", e);
        }
    }

    public static int zzae(Context context, String str) {
        int i = 0;
        try {
            Class loadClass = context.getApplicationContext().getClassLoader().loadClass(new StringBuilder(((String.valueOf("com.google.android.gms.dynamite.descriptors.").length() + 1) + String.valueOf(str).length()) + String.valueOf("ModuleDescriptor").length()).append("com.google.android.gms.dynamite.descriptors.").append(str).append(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER).append("ModuleDescriptor").toString());
            Field declaredField = loadClass.getDeclaredField("MODULE_ID");
            Field declaredField2 = loadClass.getDeclaredField("MODULE_VERSION");
            if (declaredField.get(null).equals(str)) {
                i = declaredField2.getInt(null);
            } else {
                String valueOf = String.valueOf(declaredField.get(null));
                Log.e("DynamiteModule", new StringBuilder((String.valueOf(valueOf).length() + 51) + String.valueOf(str).length()).append("Module descriptor id '").append(valueOf).append("' didn't match expected id '").append(str).append("'").toString());
            }
        } catch (ClassNotFoundException e) {
            Log.w("DynamiteModule", new StringBuilder(String.valueOf(str).length() + 45).append("Local module descriptor class for ").append(str).append(" not found.").toString());
        } catch (Exception e2) {
            valueOf = String.valueOf(e2.getMessage());
            Log.e("DynamiteModule", valueOf.length() != 0 ? "Failed to load module descriptor class: ".concat(valueOf) : new String("Failed to load module descriptor class: "));
        }
        return i;
    }

    public static int zzaf(Context context, String str) {
        return zzb(context, str, false);
    }

    private static DynamiteModule zzag(Context context, String str) {
        String valueOf = String.valueOf(str);
        Log.i("DynamiteModule", valueOf.length() != 0 ? "Selected local version of ".concat(valueOf) : new String("Selected local version of "));
        return new DynamiteModule(context.getApplicationContext());
    }

    public static int zzb(Context context, String str, boolean z) {
        Object e;
        synchronized (DynamiteModule.class) {
            Boolean bool = zzgoz;
            if (bool == null) {
                try {
                    Class loadClass = context.getApplicationContext().getClassLoader().loadClass(DynamiteLoaderClassLoader.class.getName());
                    Field declaredField = loadClass.getDeclaredField("sClassLoader");
                    synchronized (loadClass) {
                        ClassLoader classLoader = (ClassLoader) declaredField.get(null);
                        if (classLoader != null) {
                            if (classLoader == ClassLoader.getSystemClassLoader()) {
                                bool = Boolean.FALSE;
                            } else {
                                try {
                                    zza(classLoader);
                                } catch (zzc e2) {
                                }
                                bool = Boolean.TRUE;
                            }
                        } else if ("com.google.android.gms".equals(context.getApplicationContext().getPackageName())) {
                            declaredField.set(null, ClassLoader.getSystemClassLoader());
                            bool = Boolean.FALSE;
                        } else {
                            try {
                                int zzd = zzd(context, str, z);
                                if (zzgpc == null || zzgpc.isEmpty()) {
                                    return zzd;
                                }
                                ClassLoader zzh = new zzh(zzgpc, ClassLoader.getSystemClassLoader());
                                zza(zzh);
                                declaredField.set(null, zzh);
                                zzgoz = Boolean.TRUE;
                                return zzd;
                            } catch (zzc e3) {
                                declaredField.set(null, ClassLoader.getSystemClassLoader());
                                bool = Boolean.FALSE;
                                zzgoz = bool;
                                if (!bool.booleanValue()) {
                                    try {
                                    } catch (zzc e4) {
                                        String valueOf = String.valueOf(e4.getMessage());
                                        Log.w("DynamiteModule", valueOf.length() != 0 ? "Failed to retrieve remote module version: ".concat(valueOf) : new String("Failed to retrieve remote module version: "));
                                        return 0;
                                    }
                                }
                            }
                        }
                    }
                } catch (ClassNotFoundException e5) {
                    e = e5;
                } catch (IllegalAccessException e6) {
                    e = e6;
                } catch (NoSuchFieldException e7) {
                    e = e7;
                }
            }
        }
        try {
            valueOf = String.valueOf(e);
            Log.w("DynamiteModule", new StringBuilder(String.valueOf(valueOf).length() + 30).append("Failed to load module via V2: ").append(valueOf).toString());
            bool = Boolean.FALSE;
            zzgoz = bool;
            return !bool.booleanValue() ? zzc(context, str, z) : zzd(context, str, z);
        } catch (Throwable th) {
            loadClass = DynamiteModule.class;
        }
    }

    private static DynamiteModule zzb(Context context, String str, int i) throws zzc {
        Log.i("DynamiteModule", new StringBuilder(String.valueOf(str).length() + 51).append("Selected remote version of ").append(str).append(", version >= ").append(i).toString());
        zzk zzcw = zzcw(context);
        if (zzcw == null) {
            throw new zzc("Failed to create IDynamiteLoader.");
        }
        try {
            IObjectWrapper zza = zzcw.zza(zzn.zzw(context), str, i);
            if (zzn.zzab(zza) != null) {
                return new DynamiteModule((Context) zzn.zzab(zza));
            }
            throw new zzc("Failed to load remote module.");
        } catch (Throwable e) {
            throw new zzc("Failed to load remote module.", e);
        }
    }

    private static int zzc(Context context, String str, boolean z) {
        zzk zzcw = zzcw(context);
        if (zzcw == null) {
            return 0;
        }
        try {
            return zzcw.zza(zzn.zzw(context), str, z);
        } catch (RemoteException e) {
            String valueOf = String.valueOf(e.getMessage());
            Log.w("DynamiteModule", valueOf.length() != 0 ? "Failed to retrieve remote module version: ".concat(valueOf) : new String("Failed to retrieve remote module version: "));
            return 0;
        }
    }

    private static DynamiteModule zzc(Context context, String str, int i) throws zzc {
        Log.i("DynamiteModule", new StringBuilder(String.valueOf(str).length() + 51).append("Selected remote version of ").append(str).append(", version >= ").append(i).toString());
        synchronized (DynamiteModule.class) {
            try {
                zzm zzm = zzgpb;
            } catch (Throwable th) {
                while (true) {
                    Class cls = DynamiteModule.class;
                }
            }
        }
        if (zzm == null) {
            throw new zzc("DynamiteLoaderV2 was not cached.");
        }
        zza zza = (zza) zzgpd.get();
        if (zza == null || zza.zzgpm == null) {
            throw new zzc("No result cursor");
        }
        Context zza2 = zza(context.getApplicationContext(), str, i, zza.zzgpm, zzm);
        if (zza2 != null) {
            return new DynamiteModule(zza2);
        }
        throw new zzc("Failed to get module context");
    }

    private static zzk zzcw(Context context) {
        synchronized (DynamiteModule.class) {
            try {
                zzk zzk;
                if (zzgpa != null) {
                    zzk = zzgpa;
                    return zzk;
                } else if (zze.zzaew().isGooglePlayServicesAvailable(context) != 0) {
                    return null;
                } else {
                    IBinder iBinder = (IBinder) context.createPackageContext("com.google.android.gms", 3).getClassLoader().loadClass("com.google.android.gms.chimera.container.DynamiteLoaderImpl").newInstance();
                    if (iBinder == null) {
                        zzk = null;
                    } else {
                        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.dynamite.IDynamiteLoader");
                        if (queryLocalInterface instanceof zzk) {
                            zzk = (zzk) queryLocalInterface;
                        } else {
                            Object zzl = new zzl(iBinder);
                        }
                    }
                    if (zzk != null) {
                        zzgpa = zzk;
                        return zzk;
                    }
                    return null;
                }
            } catch (Exception e) {
                String valueOf = String.valueOf(e.getMessage());
                Log.e("DynamiteModule", valueOf.length() != 0 ? "Failed to load IDynamiteLoader from GmsCore: ".concat(valueOf) : new String("Failed to load IDynamiteLoader from GmsCore: "));
            } catch (Throwable th) {
                Class cls = DynamiteModule.class;
            }
        }
    }

    private static int zzd(Context context, String str, boolean z) throws zzc {
        Throwable e;
        String str2 = z ? "api_force_staging" : "api";
        Cursor query;
        try {
            query = context.getContentResolver().query(Uri.parse(new StringBuilder(((String.valueOf("content://com.google.android.gms.chimera/").length() + 1) + String.valueOf(str2).length()) + String.valueOf(str).length()).append("content://com.google.android.gms.chimera/").append(str2).append(Constants.URL_PATH_DELIMITER).append(str).toString()), null, null, null, null);
            if (query != null) {
                try {
                    if (query.moveToFirst()) {
                        int i = query.getInt(0);
                        if (i > 0) {
                            synchronized (DynamiteModule.class) {
                                zzgpc = query.getString(2);
                            }
                            zza zza = (zza) zzgpd.get();
                            if (zza != null && zza.zzgpm == null) {
                                zza.zzgpm = query;
                                query = null;
                            }
                        }
                        if (query != null) {
                            query.close();
                        }
                        return i;
                    }
                } catch (Exception e2) {
                    e = e2;
                } catch (Throwable th) {
                    while (true) {
                        break;
                    }
                    Class cls = DynamiteModule.class;
                }
            }
            Log.w("DynamiteModule", "Failed to retrieve remote module version.");
            throw new zzc("Failed to connect to dynamite module ContentResolver.");
        } catch (Exception e3) {
            e = e3;
            query = null;
            try {
                if (e instanceof zzc) {
                    throw e;
                }
                throw new zzc("V2 version check failed", e);
            } catch (Throwable th2) {
                e = th2;
                if (query != null) {
                    query.close();
                }
                throw e;
            }
        } catch (Throwable th3) {
            e = th3;
            query = null;
            if (query != null) {
                query.close();
            }
            throw e;
        }
    }

    public final Context zzaof() {
        return this.zzgpl;
    }

    public final IBinder zzgv(String str) throws zzc {
        Throwable e;
        String valueOf;
        try {
            return (IBinder) this.zzgpl.getClassLoader().loadClass(str).newInstance();
        } catch (ClassNotFoundException e2) {
            e = e2;
            valueOf = String.valueOf(str);
            throw new zzc(valueOf.length() != 0 ? "Failed to instantiate module class: ".concat(valueOf) : new String("Failed to instantiate module class: "), e);
        } catch (InstantiationException e3) {
            e = e3;
            valueOf = String.valueOf(str);
            if (valueOf.length() != 0) {
            }
            throw new zzc(valueOf.length() != 0 ? "Failed to instantiate module class: ".concat(valueOf) : new String("Failed to instantiate module class: "), e);
        } catch (IllegalAccessException e4) {
            e = e4;
            valueOf = String.valueOf(str);
            if (valueOf.length() != 0) {
            }
            throw new zzc(valueOf.length() != 0 ? "Failed to instantiate module class: ".concat(valueOf) : new String("Failed to instantiate module class: "), e);
        }
    }
}
