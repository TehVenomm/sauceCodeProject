package com.google.android.gms.internal.measurement;

import android.os.IBinder;
import android.os.IInterface;

public abstract class zzn extends zza implements zzk {
    public zzn() {
        super("com.google.android.gms.measurement.api.internal.IAppMeasurementDynamiteService");
    }

    public static zzk asInterface(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.measurement.api.internal.IAppMeasurementDynamiteService");
        return queryLocalInterface instanceof zzk ? (zzk) queryLocalInterface : new zzm(iBinder);
    }

    /* JADX WARNING: type inference failed for: r5v0 */
    /* JADX WARNING: type inference failed for: r5v1, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v2, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v2 */
    /* JADX WARNING: type inference failed for: r5v3, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v4, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v5, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v5 */
    /* JADX WARNING: type inference failed for: r5v6, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v7, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v9, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v8 */
    /* JADX WARNING: type inference failed for: r5v9, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v10, types: [com.google.android.gms.internal.measurement.zzs] */
    /* JADX WARNING: type inference failed for: r0v14, types: [com.google.android.gms.internal.measurement.zzq] */
    /* JADX WARNING: type inference failed for: r5v11 */
    /* JADX WARNING: type inference failed for: r5v12, types: [com.google.android.gms.internal.measurement.zzq] */
    /* JADX WARNING: type inference failed for: r5v13, types: [com.google.android.gms.internal.measurement.zzs] */
    /* JADX WARNING: type inference failed for: r0v17, types: [com.google.android.gms.internal.measurement.zzq] */
    /* JADX WARNING: type inference failed for: r5v14 */
    /* JADX WARNING: type inference failed for: r5v15, types: [com.google.android.gms.internal.measurement.zzq] */
    /* JADX WARNING: type inference failed for: r5v16, types: [com.google.android.gms.internal.measurement.zzs] */
    /* JADX WARNING: type inference failed for: r0v20, types: [com.google.android.gms.internal.measurement.zzq] */
    /* JADX WARNING: type inference failed for: r5v17 */
    /* JADX WARNING: type inference failed for: r5v18, types: [com.google.android.gms.internal.measurement.zzq] */
    /* JADX WARNING: type inference failed for: r5v20, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r1v9, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v21 */
    /* JADX WARNING: type inference failed for: r5v22, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v23, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v31, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v24 */
    /* JADX WARNING: type inference failed for: r5v25, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v26, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v50, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v27 */
    /* JADX WARNING: type inference failed for: r5v28, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v29, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v53, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v30 */
    /* JADX WARNING: type inference failed for: r5v31, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v32, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v56, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v33 */
    /* JADX WARNING: type inference failed for: r5v34, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v35, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v59, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v36 */
    /* JADX WARNING: type inference failed for: r5v37, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v38, types: [com.google.android.gms.internal.measurement.zzu] */
    /* JADX WARNING: type inference failed for: r0v62, types: [com.google.android.gms.internal.measurement.zzv] */
    /* JADX WARNING: type inference failed for: r5v39 */
    /* JADX WARNING: type inference failed for: r5v40, types: [com.google.android.gms.internal.measurement.zzv] */
    /* JADX WARNING: type inference failed for: r5v41, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v65, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v42 */
    /* JADX WARNING: type inference failed for: r5v43, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v44, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v68, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v45 */
    /* JADX WARNING: type inference failed for: r5v46, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v47, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v77, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v48 */
    /* JADX WARNING: type inference failed for: r5v49, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v50, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v87, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v51 */
    /* JADX WARNING: type inference failed for: r5v52, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v54, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v90, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v55 */
    /* JADX WARNING: type inference failed for: r5v56, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v59, types: [com.google.android.gms.internal.measurement.zzr] */
    /* JADX WARNING: type inference failed for: r0v95, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v60 */
    /* JADX WARNING: type inference failed for: r5v61, types: [com.google.android.gms.internal.measurement.zzp] */
    /* JADX WARNING: type inference failed for: r5v63 */
    /* JADX WARNING: type inference failed for: r5v64 */
    /* JADX WARNING: type inference failed for: r5v65 */
    /* JADX WARNING: type inference failed for: r5v66 */
    /* JADX WARNING: type inference failed for: r5v67 */
    /* JADX WARNING: type inference failed for: r5v68 */
    /* JADX WARNING: type inference failed for: r5v69 */
    /* JADX WARNING: type inference failed for: r5v70 */
    /* JADX WARNING: type inference failed for: r5v71 */
    /* JADX WARNING: type inference failed for: r5v72 */
    /* JADX WARNING: type inference failed for: r5v73 */
    /* JADX WARNING: type inference failed for: r5v74 */
    /* JADX WARNING: type inference failed for: r5v75 */
    /* JADX WARNING: type inference failed for: r5v76 */
    /* JADX WARNING: type inference failed for: r5v77 */
    /* JADX WARNING: type inference failed for: r5v78 */
    /* JADX WARNING: type inference failed for: r5v79 */
    /* JADX WARNING: type inference failed for: r5v80 */
    /* JADX WARNING: type inference failed for: r5v81 */
    /* access modifiers changed from: protected */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r5v0
      assigns: [?[int, float, boolean, short, byte, char, OBJECT, ARRAY], ?[OBJECT, ARRAY], com.google.android.gms.internal.measurement.zzr, com.google.android.gms.internal.measurement.zzs, com.google.android.gms.internal.measurement.zzu]
      uses: [com.google.android.gms.internal.measurement.zzp, com.google.android.gms.internal.measurement.zzq, com.google.android.gms.internal.measurement.zzv]
      mth insns count: 337
    	at jadx.core.dex.visitors.typeinference.TypeSearch.fillTypeCandidates(TypeSearch.java:237)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.typeinference.TypeSearch.run(TypeSearch.java:53)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.runMultiVariableSearch(TypeInferenceVisitor.java:99)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.visit(TypeInferenceVisitor.java:92)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:27)
    	at jadx.core.dex.visitors.DepthTraversal.lambda$visit$1(DepthTraversal.java:14)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
    	at jadx.core.ProcessClass.process(ProcessClass.java:30)
    	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
    	at jadx.api.JavaClass.decompile(JavaClass.java:62)
    	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
     */
    /* JADX WARNING: Unknown variable types count: 39 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final boolean zza(int r9, android.os.Parcel r10, android.os.Parcel r11, int r12) throws android.os.RemoteException {
        /*
            r8 = this;
            r5 = 0
            switch(r9) {
                case 1: goto L_0x0006;
                case 2: goto L_0x0022;
                case 3: goto L_0x0043;
                case 4: goto L_0x0076;
                case 5: goto L_0x0093;
                case 6: goto L_0x00be;
                case 7: goto L_0x00e1;
                case 8: goto L_0x00ee;
                case 9: goto L_0x00ff;
                case 10: goto L_0x0114;
                case 11: goto L_0x013b;
                case 12: goto L_0x0148;
                case 13: goto L_0x0151;
                case 14: goto L_0x015a;
                case 15: goto L_0x0163;
                case 16: goto L_0x017d;
                case 17: goto L_0x019c;
                case 18: goto L_0x01bb;
                case 19: goto L_0x01da;
                case 20: goto L_0x01f9;
                case 21: goto L_0x0218;
                case 22: goto L_0x0237;
                case 23: goto L_0x0256;
                case 24: goto L_0x0263;
                case 25: goto L_0x0270;
                case 26: goto L_0x0281;
                case 27: goto L_0x0292;
                case 28: goto L_0x02ab;
                case 29: goto L_0x02bc;
                case 30: goto L_0x02cd;
                case 31: goto L_0x02de;
                case 32: goto L_0x0309;
                case 33: goto L_0x0334;
                case 34: goto L_0x035a;
                case 35: goto L_0x0379;
                case 36: goto L_0x0398;
                case 37: goto L_0x03b7;
                case 38: goto L_0x03c0;
                case 39: goto L_0x03e3;
                case 40: goto L_0x03ec;
                case 41: goto L_0x040b;
                default: goto L_0x0004;
            }
        L_0x0004:
            r0 = 0
        L_0x0005:
            return r0
        L_0x0006:
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r1 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            android.os.Parcelable$Creator<com.google.android.gms.internal.measurement.zzx> r0 = com.google.android.gms.internal.measurement.zzx.CREATOR
            android.os.Parcelable r0 = com.google.android.gms.internal.measurement.zzd.zza(r10, r0)
            com.google.android.gms.internal.measurement.zzx r0 = (com.google.android.gms.internal.measurement.zzx) r0
            long r2 = r10.readLong()
            r8.initialize(r1, r0, r2)
        L_0x001d:
            r11.writeNoException()
            r0 = 1
            goto L_0x0005
        L_0x0022:
            java.lang.String r1 = r10.readString()
            java.lang.String r2 = r10.readString()
            android.os.Parcelable$Creator r0 = android.os.Bundle.CREATOR
            android.os.Parcelable r3 = com.google.android.gms.internal.measurement.zzd.zza(r10, r0)
            android.os.Bundle r3 = (android.os.Bundle) r3
            boolean r4 = com.google.android.gms.internal.measurement.zzd.zza(r10)
            boolean r5 = com.google.android.gms.internal.measurement.zzd.zza(r10)
            long r6 = r10.readLong()
            r0 = r8
            r0.logEvent(r1, r2, r3, r4, r5, r6)
            goto L_0x001d
        L_0x0043:
            java.lang.String r2 = r10.readString()
            java.lang.String r3 = r10.readString()
            android.os.Parcelable$Creator r0 = android.os.Bundle.CREATOR
            android.os.Parcelable r4 = com.google.android.gms.internal.measurement.zzd.zza(r10, r0)
            android.os.Bundle r4 = (android.os.Bundle) r4
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x0062
        L_0x0059:
            long r6 = r10.readLong()
            r1 = r8
            r1.logEventAndBundle(r2, r3, r4, r5, r6)
            goto L_0x001d
        L_0x0062:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r5 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r5 == 0) goto L_0x0070
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x0059
        L_0x0070:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r1)
            goto L_0x0059
        L_0x0076:
            java.lang.String r2 = r10.readString()
            java.lang.String r3 = r10.readString()
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r4 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            boolean r5 = com.google.android.gms.internal.measurement.zzd.zza(r10)
            long r6 = r10.readLong()
            r1 = r8
            r1.setUserProperty(r2, r3, r4, r5, r6)
            goto L_0x001d
        L_0x0093:
            java.lang.String r1 = r10.readString()
            java.lang.String r2 = r10.readString()
            boolean r3 = com.google.android.gms.internal.measurement.zzd.zza(r10)
            android.os.IBinder r4 = r10.readStrongBinder()
            if (r4 != 0) goto L_0x00aa
        L_0x00a5:
            r8.getUserProperties(r1, r2, r3, r5)
            goto L_0x001d
        L_0x00aa:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r4.queryLocalInterface(r0)
            boolean r5 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r5 == 0) goto L_0x00b8
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x00a5
        L_0x00b8:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r4)
            goto L_0x00a5
        L_0x00be:
            java.lang.String r1 = r10.readString()
            android.os.IBinder r2 = r10.readStrongBinder()
            if (r2 != 0) goto L_0x00cd
        L_0x00c8:
            r8.getMaxUserProperties(r1, r5)
            goto L_0x001d
        L_0x00cd:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r2.queryLocalInterface(r0)
            boolean r3 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r3 == 0) goto L_0x00db
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x00c8
        L_0x00db:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r2)
            goto L_0x00c8
        L_0x00e1:
            java.lang.String r0 = r10.readString()
            long r2 = r10.readLong()
            r8.setUserId(r0, r2)
            goto L_0x001d
        L_0x00ee:
            android.os.Parcelable$Creator r0 = android.os.Bundle.CREATOR
            android.os.Parcelable r0 = com.google.android.gms.internal.measurement.zzd.zza(r10, r0)
            android.os.Bundle r0 = (android.os.Bundle) r0
            long r2 = r10.readLong()
            r8.setConditionalUserProperty(r0, r2)
            goto L_0x001d
        L_0x00ff:
            java.lang.String r1 = r10.readString()
            java.lang.String r2 = r10.readString()
            android.os.Parcelable$Creator r0 = android.os.Bundle.CREATOR
            android.os.Parcelable r0 = com.google.android.gms.internal.measurement.zzd.zza(r10, r0)
            android.os.Bundle r0 = (android.os.Bundle) r0
            r8.clearConditionalUserProperty(r1, r2, r0)
            goto L_0x001d
        L_0x0114:
            java.lang.String r1 = r10.readString()
            java.lang.String r2 = r10.readString()
            android.os.IBinder r3 = r10.readStrongBinder()
            if (r3 != 0) goto L_0x0127
        L_0x0122:
            r8.getConditionalUserProperties(r1, r2, r5)
            goto L_0x001d
        L_0x0127:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r3.queryLocalInterface(r0)
            boolean r4 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r4 == 0) goto L_0x0135
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x0122
        L_0x0135:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r3)
            goto L_0x0122
        L_0x013b:
            boolean r0 = com.google.android.gms.internal.measurement.zzd.zza(r10)
            long r2 = r10.readLong()
            r8.setMeasurementEnabled(r0, r2)
            goto L_0x001d
        L_0x0148:
            long r0 = r10.readLong()
            r8.resetAnalyticsData(r0)
            goto L_0x001d
        L_0x0151:
            long r0 = r10.readLong()
            r8.setMinimumSessionDuration(r0)
            goto L_0x001d
        L_0x015a:
            long r0 = r10.readLong()
            r8.setSessionTimeoutDuration(r0)
            goto L_0x001d
        L_0x0163:
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r1 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            java.lang.String r2 = r10.readString()
            java.lang.String r3 = r10.readString()
            long r4 = r10.readLong()
            r0 = r8
            r0.setCurrentScreen(r1, r2, r3, r4)
            goto L_0x001d
        L_0x017d:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x0188
        L_0x0183:
            r8.getCurrentScreenName(r5)
            goto L_0x001d
        L_0x0188:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r2 == 0) goto L_0x0196
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x0183
        L_0x0196:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r1)
            goto L_0x0183
        L_0x019c:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x01a7
        L_0x01a2:
            r8.getCurrentScreenClass(r5)
            goto L_0x001d
        L_0x01a7:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r2 == 0) goto L_0x01b5
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x01a2
        L_0x01b5:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r1)
            goto L_0x01a2
        L_0x01bb:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x01c6
        L_0x01c1:
            r8.setInstanceIdProvider(r5)
            goto L_0x001d
        L_0x01c6:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IStringProvider"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzv
            if (r2 == 0) goto L_0x01d4
            com.google.android.gms.internal.measurement.zzv r0 = (com.google.android.gms.internal.measurement.zzv) r0
            r5 = r0
            goto L_0x01c1
        L_0x01d4:
            com.google.android.gms.internal.measurement.zzu r5 = new com.google.android.gms.internal.measurement.zzu
            r5.<init>(r1)
            goto L_0x01c1
        L_0x01da:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x01e5
        L_0x01e0:
            r8.getCachedAppInstanceId(r5)
            goto L_0x001d
        L_0x01e5:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r2 == 0) goto L_0x01f3
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x01e0
        L_0x01f3:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r1)
            goto L_0x01e0
        L_0x01f9:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x0204
        L_0x01ff:
            r8.getAppInstanceId(r5)
            goto L_0x001d
        L_0x0204:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r2 == 0) goto L_0x0212
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x01ff
        L_0x0212:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r1)
            goto L_0x01ff
        L_0x0218:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x0223
        L_0x021e:
            r8.getGmpAppId(r5)
            goto L_0x001d
        L_0x0223:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r2 == 0) goto L_0x0231
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x021e
        L_0x0231:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r1)
            goto L_0x021e
        L_0x0237:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x0242
        L_0x023d:
            r8.generateEventId(r5)
            goto L_0x001d
        L_0x0242:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r2 == 0) goto L_0x0250
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x023d
        L_0x0250:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r1)
            goto L_0x023d
        L_0x0256:
            java.lang.String r0 = r10.readString()
            long r2 = r10.readLong()
            r8.beginAdUnitExposure(r0, r2)
            goto L_0x001d
        L_0x0263:
            java.lang.String r0 = r10.readString()
            long r2 = r10.readLong()
            r8.endAdUnitExposure(r0, r2)
            goto L_0x001d
        L_0x0270:
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r0 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            long r2 = r10.readLong()
            r8.onActivityStarted(r0, r2)
            goto L_0x001d
        L_0x0281:
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r0 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            long r2 = r10.readLong()
            r8.onActivityStopped(r0, r2)
            goto L_0x001d
        L_0x0292:
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r1 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            android.os.Parcelable$Creator r0 = android.os.Bundle.CREATOR
            android.os.Parcelable r0 = com.google.android.gms.internal.measurement.zzd.zza(r10, r0)
            android.os.Bundle r0 = (android.os.Bundle) r0
            long r2 = r10.readLong()
            r8.onActivityCreated(r1, r0, r2)
            goto L_0x001d
        L_0x02ab:
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r0 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            long r2 = r10.readLong()
            r8.onActivityDestroyed(r0, r2)
            goto L_0x001d
        L_0x02bc:
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r0 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            long r2 = r10.readLong()
            r8.onActivityPaused(r0, r2)
            goto L_0x001d
        L_0x02cd:
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r0 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            long r2 = r10.readLong()
            r8.onActivityResumed(r0, r2)
            goto L_0x001d
        L_0x02de:
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r1 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            android.os.IBinder r2 = r10.readStrongBinder()
            if (r2 != 0) goto L_0x02f5
        L_0x02ec:
            long r2 = r10.readLong()
            r8.onActivitySaveInstanceState(r1, r5, r2)
            goto L_0x001d
        L_0x02f5:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r2.queryLocalInterface(r0)
            boolean r3 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r3 == 0) goto L_0x0303
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x02ec
        L_0x0303:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r2)
            goto L_0x02ec
        L_0x0309:
            android.os.Parcelable$Creator r0 = android.os.Bundle.CREATOR
            android.os.Parcelable r0 = com.google.android.gms.internal.measurement.zzd.zza(r10, r0)
            android.os.Bundle r0 = (android.os.Bundle) r0
            android.os.IBinder r2 = r10.readStrongBinder()
            if (r2 != 0) goto L_0x0320
        L_0x0317:
            long r2 = r10.readLong()
            r8.performAction(r0, r5, r2)
            goto L_0x001d
        L_0x0320:
            java.lang.String r1 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r1 = r2.queryLocalInterface(r1)
            boolean r3 = r1 instanceof com.google.android.gms.internal.measurement.zzp
            if (r3 == 0) goto L_0x032e
            com.google.android.gms.internal.measurement.zzp r1 = (com.google.android.gms.internal.measurement.zzp) r1
            r5 = r1
            goto L_0x0317
        L_0x032e:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r2)
            goto L_0x0317
        L_0x0334:
            int r1 = r10.readInt()
            java.lang.String r2 = r10.readString()
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r3 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r4 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            android.os.IBinder r0 = r10.readStrongBinder()
            com.google.android.gms.dynamic.IObjectWrapper r5 = com.google.android.gms.dynamic.IObjectWrapper.Stub.asInterface(r0)
            r0 = r8
            r0.logHealthData(r1, r2, r3, r4, r5)
            goto L_0x001d
        L_0x035a:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x0365
        L_0x0360:
            r8.setEventInterceptor(r5)
            goto L_0x001d
        L_0x0365:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IEventHandlerProxy"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzq
            if (r2 == 0) goto L_0x0373
            com.google.android.gms.internal.measurement.zzq r0 = (com.google.android.gms.internal.measurement.zzq) r0
            r5 = r0
            goto L_0x0360
        L_0x0373:
            com.google.android.gms.internal.measurement.zzs r5 = new com.google.android.gms.internal.measurement.zzs
            r5.<init>(r1)
            goto L_0x0360
        L_0x0379:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x0384
        L_0x037f:
            r8.registerOnMeasurementEventListener(r5)
            goto L_0x001d
        L_0x0384:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IEventHandlerProxy"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzq
            if (r2 == 0) goto L_0x0392
            com.google.android.gms.internal.measurement.zzq r0 = (com.google.android.gms.internal.measurement.zzq) r0
            r5 = r0
            goto L_0x037f
        L_0x0392:
            com.google.android.gms.internal.measurement.zzs r5 = new com.google.android.gms.internal.measurement.zzs
            r5.<init>(r1)
            goto L_0x037f
        L_0x0398:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x03a3
        L_0x039e:
            r8.unregisterOnMeasurementEventListener(r5)
            goto L_0x001d
        L_0x03a3:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IEventHandlerProxy"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzq
            if (r2 == 0) goto L_0x03b1
            com.google.android.gms.internal.measurement.zzq r0 = (com.google.android.gms.internal.measurement.zzq) r0
            r5 = r0
            goto L_0x039e
        L_0x03b1:
            com.google.android.gms.internal.measurement.zzs r5 = new com.google.android.gms.internal.measurement.zzs
            r5.<init>(r1)
            goto L_0x039e
        L_0x03b7:
            java.util.HashMap r0 = com.google.android.gms.internal.measurement.zzd.zzb(r10)
            r8.initForTests(r0)
            goto L_0x001d
        L_0x03c0:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x03cf
        L_0x03c6:
            int r0 = r10.readInt()
            r8.getTestFlag(r5, r0)
            goto L_0x001d
        L_0x03cf:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r2 == 0) goto L_0x03dd
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x03c6
        L_0x03dd:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r1)
            goto L_0x03c6
        L_0x03e3:
            boolean r0 = com.google.android.gms.internal.measurement.zzd.zza(r10)
            r8.setDataCollectionEnabled(r0)
            goto L_0x001d
        L_0x03ec:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x03f7
        L_0x03f2:
            r8.isDataCollectionEnabled(r5)
            goto L_0x001d
        L_0x03f7:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r2 == 0) goto L_0x0405
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x03f2
        L_0x0405:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r1)
            goto L_0x03f2
        L_0x040b:
            android.os.IBinder r1 = r10.readStrongBinder()
            if (r1 != 0) goto L_0x0416
        L_0x0411:
            r8.getDeepLink(r5)
            goto L_0x001d
        L_0x0416:
            java.lang.String r0 = "com.google.android.gms.measurement.api.internal.IBundleReceiver"
            android.os.IInterface r0 = r1.queryLocalInterface(r0)
            boolean r2 = r0 instanceof com.google.android.gms.internal.measurement.zzp
            if (r2 == 0) goto L_0x0424
            com.google.android.gms.internal.measurement.zzp r0 = (com.google.android.gms.internal.measurement.zzp) r0
            r5 = r0
            goto L_0x0411
        L_0x0424:
            com.google.android.gms.internal.measurement.zzr r5 = new com.google.android.gms.internal.measurement.zzr
            r5.<init>(r1)
            goto L_0x0411
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzn.zza(int, android.os.Parcel, android.os.Parcel, int):boolean");
    }
}
