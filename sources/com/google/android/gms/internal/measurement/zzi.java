package com.google.android.gms.internal.measurement;

import android.annotation.TargetApi;
import android.app.job.JobInfo;
import android.app.job.JobScheduler;
import android.content.Context;
import android.os.Build.VERSION;
import android.os.UserHandle;
import android.support.annotation.Nullable;
import android.util.Log;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

@TargetApi(24)
public final class zzi {
    @Nullable
    private static final Method zzg = zza();
    @Nullable
    private static final Method zzh = zzb();
    private final JobScheduler zzf;

    private zzi(JobScheduler jobScheduler) {
        this.zzf = jobScheduler;
    }

    private final int zza(JobInfo jobInfo, String str, int i, String str2) {
        if (zzg != null) {
            try {
                return ((Integer) zzg.invoke(this.zzf, new Object[]{jobInfo, str, Integer.valueOf(i), str2})).intValue();
            } catch (IllegalAccessException | InvocationTargetException e) {
                Log.e(str2, "error calling scheduleAsPackage", e);
            }
        }
        return this.zzf.schedule(jobInfo);
    }

    public static int zza(Context context, JobInfo jobInfo, String str, String str2) {
        JobScheduler jobScheduler = (JobScheduler) context.getSystemService("jobscheduler");
        return (zzg == null || context.checkSelfPermission("android.permission.UPDATE_DEVICE_STATS") != 0) ? jobScheduler.schedule(jobInfo) : new zzi(jobScheduler).zza(jobInfo, str, zzc(), str2);
    }

    @Nullable
    private static Method zza() {
        if (VERSION.SDK_INT >= 24) {
            try {
                return JobScheduler.class.getDeclaredMethod("scheduleAsPackage", new Class[]{JobInfo.class, String.class, Integer.TYPE, String.class});
            } catch (NoSuchMethodException e) {
                if (Log.isLoggable("JobSchedulerCompat", 6)) {
                    Log.e("JobSchedulerCompat", "No scheduleAsPackage method available, falling back to schedule");
                }
            }
        }
        return null;
    }

    @Nullable
    private static Method zzb() {
        Method method = null;
        if (VERSION.SDK_INT < 24) {
            return method;
        }
        try {
            return UserHandle.class.getDeclaredMethod("myUserId", null);
        } catch (NoSuchMethodException e) {
            if (!Log.isLoggable("JobSchedulerCompat", 6)) {
                return method;
            }
            Log.e("JobSchedulerCompat", "No myUserId method available");
            return method;
        }
    }

    private static int zzc() {
        if (zzh != null) {
            try {
                return ((Integer) zzh.invoke(null, new Object[0])).intValue();
            } catch (IllegalAccessException | InvocationTargetException e) {
                if (Log.isLoggable("JobSchedulerCompat", 6)) {
                    Log.e("JobSchedulerCompat", "myUserId invocation illegal", e);
                }
            }
        }
        return 0;
    }
}
