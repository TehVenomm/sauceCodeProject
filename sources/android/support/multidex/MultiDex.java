package android.support.multidex;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Build.VERSION;
import android.util.Log;
import dalvik.system.DexFile;
import java.io.File;
import java.io.IOException;
import java.lang.reflect.Array;
import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;
import java.util.Set;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.zip.ZipFile;

public final class MultiDex {
    private static final boolean IS_VM_MULTIDEX_CAPABLE = isVMMultidexCapable(System.getProperty("java.vm.version"));
    private static final int MAX_SUPPORTED_SDK_VERSION = 20;
    private static final int MIN_SDK_VERSION = 4;
    private static final String OLD_SECONDARY_FOLDER_NAME = "secondary-dexes";
    private static final String SECONDARY_FOLDER_NAME = ("code_cache" + File.separator + OLD_SECONDARY_FOLDER_NAME);
    static final String TAG = "MultiDex";
    private static final int VM_WITH_MULTIDEX_VERSION_MAJOR = 2;
    private static final int VM_WITH_MULTIDEX_VERSION_MINOR = 1;
    private static final Set<String> installedApk = new HashSet();

    private static final class V14 {
        private V14() {
        }

        /* access modifiers changed from: private */
        public static void install(ClassLoader classLoader, List<File> list, File file) throws IllegalArgumentException, IllegalAccessException, NoSuchFieldException, InvocationTargetException, NoSuchMethodException {
            Object obj = MultiDex.findField(classLoader, "pathList").get(classLoader);
            MultiDex.expandFieldArray(obj, "dexElements", makeDexElements(obj, new ArrayList(list), file));
        }

        private static Object[] makeDexElements(Object obj, ArrayList<File> arrayList, File file) throws IllegalAccessException, InvocationTargetException, NoSuchMethodException {
            return (Object[]) MultiDex.findMethod(obj, "makeDexElements", ArrayList.class, File.class).invoke(obj, new Object[]{arrayList, file});
        }
    }

    private static final class V19 {
        private V19() {
        }

        /* access modifiers changed from: private */
        public static void install(ClassLoader classLoader, List<File> list, File file) throws IllegalArgumentException, IllegalAccessException, NoSuchFieldException, InvocationTargetException, NoSuchMethodException {
            IOException[] iOExceptionArr;
            Object obj = MultiDex.findField(classLoader, "pathList").get(classLoader);
            ArrayList arrayList = new ArrayList();
            MultiDex.expandFieldArray(obj, "dexElements", makeDexElements(obj, new ArrayList(list), file, arrayList));
            if (arrayList.size() > 0) {
                Iterator it = arrayList.iterator();
                while (it.hasNext()) {
                    Log.w(MultiDex.TAG, "Exception in makeDexElement", (IOException) it.next());
                }
                Field access$300 = MultiDex.findField(classLoader, "dexElementsSuppressedExceptions");
                IOException[] iOExceptionArr2 = (IOException[]) access$300.get(classLoader);
                if (iOExceptionArr2 == null) {
                    iOExceptionArr = (IOException[]) arrayList.toArray(new IOException[arrayList.size()]);
                } else {
                    IOException[] iOExceptionArr3 = new IOException[(arrayList.size() + iOExceptionArr2.length)];
                    arrayList.toArray(iOExceptionArr3);
                    System.arraycopy(iOExceptionArr2, 0, iOExceptionArr3, arrayList.size(), iOExceptionArr2.length);
                    iOExceptionArr = iOExceptionArr3;
                }
                access$300.set(classLoader, iOExceptionArr);
            }
        }

        private static Object[] makeDexElements(Object obj, ArrayList<File> arrayList, File file, ArrayList<IOException> arrayList2) throws IllegalAccessException, InvocationTargetException, NoSuchMethodException {
            return (Object[]) MultiDex.findMethod(obj, "makeDexElements", ArrayList.class, File.class, ArrayList.class).invoke(obj, new Object[]{arrayList, file, arrayList2});
        }
    }

    /* renamed from: android.support.multidex.MultiDex$V4 */
    private static final class C0052V4 {
        private C0052V4() {
        }

        /* access modifiers changed from: private */
        public static void install(ClassLoader classLoader, List<File> list) throws IllegalArgumentException, IllegalAccessException, NoSuchFieldException, IOException {
            int size = list.size();
            Field access$300 = MultiDex.findField(classLoader, "path");
            StringBuilder sb = new StringBuilder((String) access$300.get(classLoader));
            String[] strArr = new String[size];
            File[] fileArr = new File[size];
            ZipFile[] zipFileArr = new ZipFile[size];
            DexFile[] dexFileArr = new DexFile[size];
            ListIterator listIterator = list.listIterator();
            while (listIterator.hasNext()) {
                File file = (File) listIterator.next();
                String absolutePath = file.getAbsolutePath();
                sb.append(':').append(absolutePath);
                int previousIndex = listIterator.previousIndex();
                strArr[previousIndex] = absolutePath;
                fileArr[previousIndex] = file;
                zipFileArr[previousIndex] = new ZipFile(file);
                dexFileArr[previousIndex] = DexFile.loadDex(absolutePath, absolutePath + ".dex", 0);
            }
            access$300.set(classLoader, sb.toString());
            MultiDex.expandFieldArray(classLoader, "mPaths", strArr);
            MultiDex.expandFieldArray(classLoader, "mFiles", fileArr);
            MultiDex.expandFieldArray(classLoader, "mZips", zipFileArr);
            MultiDex.expandFieldArray(classLoader, "mDexs", dexFileArr);
        }
    }

    private MultiDex() {
    }

    /* JADX WARNING: Code restructure failed: missing block: B:46:0x010f, code lost:
        android.util.Log.i(TAG, "install done");
     */
    /* JADX WARNING: Code restructure failed: missing block: B:60:?, code lost:
        return;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static void install(android.content.Context r6) {
        /*
            r4 = 20
            r3 = 4
            java.lang.String r0 = "MultiDex"
            java.lang.String r1 = "install"
            android.util.Log.i(r0, r1)
            boolean r0 = IS_VM_MULTIDEX_CAPABLE
            if (r0 == 0) goto L_0x0016
            java.lang.String r0 = "MultiDex"
            java.lang.String r1 = "VM has multidex support, MultiDex support library is disabled."
            android.util.Log.i(r0, r1)
        L_0x0015:
            return
        L_0x0016:
            int r0 = android.os.Build.VERSION.SDK_INT
            if (r0 >= r3) goto L_0x0045
            java.lang.RuntimeException r0 = new java.lang.RuntimeException
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = "Multi dex installation failed. SDK "
            java.lang.StringBuilder r1 = r1.append(r2)
            int r2 = android.os.Build.VERSION.SDK_INT
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r2 = " is unsupported. Min SDK version is "
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r1 = r1.append(r3)
            java.lang.String r2 = "."
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r1 = r1.toString()
            r0.<init>(r1)
            throw r0
        L_0x0045:
            android.content.pm.ApplicationInfo r0 = getApplicationInfo(r6)     // Catch:{ Exception -> 0x005d }
            if (r0 == 0) goto L_0x0015
            java.util.Set<java.lang.String> r1 = installedApk     // Catch:{ Exception -> 0x005d }
            monitor-enter(r1)     // Catch:{ Exception -> 0x005d }
            java.lang.String r2 = r0.sourceDir     // Catch:{ all -> 0x005a }
            java.util.Set<java.lang.String> r3 = installedApk     // Catch:{ all -> 0x005a }
            boolean r3 = r3.contains(r2)     // Catch:{ all -> 0x005a }
            if (r3 == 0) goto L_0x0088
            monitor-exit(r1)     // Catch:{ all -> 0x005a }
            goto L_0x0015
        L_0x005a:
            r0 = move-exception
            monitor-exit(r1)     // Catch:{ all -> 0x005a }
            throw r0     // Catch:{ Exception -> 0x005d }
        L_0x005d:
            r0 = move-exception
            java.lang.String r1 = "MultiDex"
            java.lang.String r2 = "Multidex installation failure"
            android.util.Log.e(r1, r2, r0)
            java.lang.RuntimeException r1 = new java.lang.RuntimeException
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r3 = "Multi dex installation failed ("
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r0 = r0.getMessage()
            java.lang.StringBuilder r0 = r2.append(r0)
            java.lang.String r2 = ")."
            java.lang.StringBuilder r0 = r0.append(r2)
            java.lang.String r0 = r0.toString()
            r1.<init>(r0)
            throw r1
        L_0x0088:
            java.util.Set<java.lang.String> r3 = installedApk     // Catch:{ all -> 0x005a }
            r3.add(r2)     // Catch:{ all -> 0x005a }
            int r2 = android.os.Build.VERSION.SDK_INT     // Catch:{ all -> 0x005a }
            if (r2 <= r4) goto L_0x00d9
            java.lang.String r2 = "MultiDex"
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ all -> 0x005a }
            r3.<init>()     // Catch:{ all -> 0x005a }
            java.lang.String r4 = "MultiDex is not guaranteed to work in SDK version "
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ all -> 0x005a }
            int r4 = android.os.Build.VERSION.SDK_INT     // Catch:{ all -> 0x005a }
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ all -> 0x005a }
            java.lang.String r4 = ": SDK version higher than "
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ all -> 0x005a }
            r4 = 20
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ all -> 0x005a }
            java.lang.String r4 = " should be backed by "
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ all -> 0x005a }
            java.lang.String r4 = "runtime with built-in multidex capabilty but it's not the "
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ all -> 0x005a }
            java.lang.String r4 = "case here: java.vm.version=\""
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ all -> 0x005a }
            java.lang.String r4 = "java.vm.version"
            java.lang.String r4 = java.lang.System.getProperty(r4)     // Catch:{ all -> 0x005a }
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ all -> 0x005a }
            java.lang.String r4 = "\""
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ all -> 0x005a }
            java.lang.String r3 = r3.toString()     // Catch:{ all -> 0x005a }
            android.util.Log.w(r2, r3)     // Catch:{ all -> 0x005a }
        L_0x00d9:
            java.lang.ClassLoader r2 = r6.getClassLoader()     // Catch:{ RuntimeException -> 0x00e9 }
            if (r2 != 0) goto L_0x00f4
            java.lang.String r0 = "MultiDex"
            java.lang.String r2 = "Context class loader is null. Must be running in test mode. Skip patching."
            android.util.Log.e(r0, r2)     // Catch:{ all -> 0x005a }
            monitor-exit(r1)     // Catch:{ all -> 0x005a }
            goto L_0x0015
        L_0x00e9:
            r0 = move-exception
            java.lang.String r2 = "MultiDex"
            java.lang.String r3 = "Failure while trying to obtain Context class loader. Must be running in test mode. Skip patching."
            android.util.Log.w(r2, r3, r0)     // Catch:{ all -> 0x005a }
            monitor-exit(r1)     // Catch:{ all -> 0x005a }
            goto L_0x0015
        L_0x00f4:
            clearOldDexDir(r6)     // Catch:{ Throwable -> 0x0118 }
        L_0x00f7:
            java.io.File r3 = new java.io.File     // Catch:{ all -> 0x005a }
            java.lang.String r4 = r0.dataDir     // Catch:{ all -> 0x005a }
            java.lang.String r5 = SECONDARY_FOLDER_NAME     // Catch:{ all -> 0x005a }
            r3.<init>(r4, r5)     // Catch:{ all -> 0x005a }
            r4 = 0
            java.util.List r4 = android.support.multidex.MultiDexExtractor.load(r6, r0, r3, r4)     // Catch:{ all -> 0x005a }
            boolean r5 = checkValidZipFiles(r4)     // Catch:{ all -> 0x005a }
            if (r5 == 0) goto L_0x0121
            installSecondaryDexes(r2, r3, r4)     // Catch:{ all -> 0x005a }
        L_0x010e:
            monitor-exit(r1)     // Catch:{ all -> 0x005a }
            java.lang.String r0 = "MultiDex"
            java.lang.String r1 = "install done"
            android.util.Log.i(r0, r1)
            goto L_0x0015
        L_0x0118:
            r3 = move-exception
            java.lang.String r4 = "MultiDex"
            java.lang.String r5 = "Something went wrong when trying to clear old MultiDex extraction, continuing without cleaning."
            android.util.Log.w(r4, r5, r3)     // Catch:{ all -> 0x005a }
            goto L_0x00f7
        L_0x0121:
            java.lang.String r4 = "MultiDex"
            java.lang.String r5 = "Files were not valid zip files.  Forcing a reload."
            android.util.Log.w(r4, r5)     // Catch:{ all -> 0x005a }
            r4 = 1
            java.util.List r0 = android.support.multidex.MultiDexExtractor.load(r6, r0, r3, r4)     // Catch:{ all -> 0x005a }
            boolean r4 = checkValidZipFiles(r0)     // Catch:{ all -> 0x005a }
            if (r4 == 0) goto L_0x0137
            installSecondaryDexes(r2, r3, r0)     // Catch:{ all -> 0x005a }
            goto L_0x010e
        L_0x0137:
            java.lang.RuntimeException r0 = new java.lang.RuntimeException     // Catch:{ all -> 0x005a }
            java.lang.String r2 = "Zip files were not valid."
            r0.<init>(r2)     // Catch:{ all -> 0x005a }
            throw r0     // Catch:{ all -> 0x005a }
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.multidex.MultiDex.install(android.content.Context):void");
    }

    private static ApplicationInfo getApplicationInfo(Context context) throws NameNotFoundException {
        try {
            PackageManager packageManager = context.getPackageManager();
            String packageName = context.getPackageName();
            if (packageManager == null || packageName == null) {
                return null;
            }
            return packageManager.getApplicationInfo(packageName, 128);
        } catch (RuntimeException e) {
            Log.w(TAG, "Failure while trying to obtain ApplicationInfo from Context. Must be running in test mode. Skip patching.", e);
            return null;
        }
    }

    static boolean isVMMultidexCapable(String str) {
        boolean z = false;
        if (str != null) {
            Matcher matcher = Pattern.compile("(\\d+)\\.(\\d+)(\\.\\d+)?").matcher(str);
            if (matcher.matches()) {
                try {
                    int parseInt = Integer.parseInt(matcher.group(1));
                    int parseInt2 = Integer.parseInt(matcher.group(2));
                    if (parseInt > 2 || (parseInt == 2 && parseInt2 >= 1)) {
                        z = true;
                    }
                } catch (NumberFormatException e) {
                }
            }
        }
        Log.i(TAG, "VM with version " + str + (z ? " has multidex support" : " does not have multidex support"));
        return z;
    }

    private static void installSecondaryDexes(ClassLoader classLoader, File file, List<File> list) throws IllegalArgumentException, IllegalAccessException, NoSuchFieldException, InvocationTargetException, NoSuchMethodException, IOException {
        if (list.isEmpty()) {
            return;
        }
        if (VERSION.SDK_INT >= 19) {
            V19.install(classLoader, list, file);
        } else if (VERSION.SDK_INT >= 14) {
            V14.install(classLoader, list, file);
        } else {
            C0052V4.install(classLoader, list);
        }
    }

    private static boolean checkValidZipFiles(List<File> list) {
        for (File verifyZipFile : list) {
            if (!MultiDexExtractor.verifyZipFile(verifyZipFile)) {
                return false;
            }
        }
        return true;
    }

    /* access modifiers changed from: private */
    public static Field findField(Object obj, String str) throws NoSuchFieldException {
        Class cls = obj.getClass();
        while (cls != null) {
            try {
                Field declaredField = cls.getDeclaredField(str);
                if (!declaredField.isAccessible()) {
                    declaredField.setAccessible(true);
                }
                return declaredField;
            } catch (NoSuchFieldException e) {
                cls = cls.getSuperclass();
            }
        }
        throw new NoSuchFieldException("Field " + str + " not found in " + obj.getClass());
    }

    /* access modifiers changed from: private */
    public static Method findMethod(Object obj, String str, Class<?>... clsArr) throws NoSuchMethodException {
        Class cls = obj.getClass();
        while (cls != null) {
            try {
                Method declaredMethod = cls.getDeclaredMethod(str, clsArr);
                if (!declaredMethod.isAccessible()) {
                    declaredMethod.setAccessible(true);
                }
                return declaredMethod;
            } catch (NoSuchMethodException e) {
                cls = cls.getSuperclass();
            }
        }
        throw new NoSuchMethodException("Method " + str + " with parameters " + Arrays.asList(clsArr) + " not found in " + obj.getClass());
    }

    /* access modifiers changed from: private */
    public static void expandFieldArray(Object obj, String str, Object[] objArr) throws NoSuchFieldException, IllegalArgumentException, IllegalAccessException {
        Field findField = findField(obj, str);
        Object[] objArr2 = (Object[]) findField.get(obj);
        Object[] objArr3 = (Object[]) Array.newInstance(objArr2.getClass().getComponentType(), objArr2.length + objArr.length);
        System.arraycopy(objArr2, 0, objArr3, 0, objArr2.length);
        System.arraycopy(objArr, 0, objArr3, objArr2.length, objArr.length);
        findField.set(obj, objArr3);
    }

    private static void clearOldDexDir(Context context) throws Exception {
        File file = new File(context.getFilesDir(), OLD_SECONDARY_FOLDER_NAME);
        if (file.isDirectory()) {
            Log.i(TAG, "Clearing old secondary dex dir (" + file.getPath() + ").");
            File[] listFiles = file.listFiles();
            if (listFiles == null) {
                Log.w(TAG, "Failed to list secondary dex dir content (" + file.getPath() + ").");
                return;
            }
            for (File file2 : listFiles) {
                Log.i(TAG, "Trying to delete old file " + file2.getPath() + " of size " + file2.length());
                if (!file2.delete()) {
                    Log.w(TAG, "Failed to delete old file " + file2.getPath());
                } else {
                    Log.i(TAG, "Deleted old file " + file2.getPath());
                }
            }
            if (!file.delete()) {
                Log.w(TAG, "Failed to delete secondary dex dir " + file.getPath());
            } else {
                Log.i(TAG, "Deleted old secondary dex dir " + file.getPath());
            }
        }
    }
}
