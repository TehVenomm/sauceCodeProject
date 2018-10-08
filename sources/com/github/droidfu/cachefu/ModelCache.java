package com.github.droidfu.cachefu;

import android.os.Parcel;
import android.os.Parcelable;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FilenameFilter;
import java.io.IOException;

public class ModelCache extends AbstractCache<String, CachedModel> {
    private long transactionCount = -9223372036854775807L;

    static class DescribedCachedModel implements Parcelable {
        private CachedModel cachedModel;

        DescribedCachedModel() {
        }

        public int describeContents() {
            return 0;
        }

        public CachedModel getCachedModel() {
            return this.cachedModel;
        }

        public void readFromParcel(Parcel parcel) throws IOException {
            try {
                this.cachedModel = (CachedModel) parcel.readParcelable(Class.forName(parcel.readString()).getClassLoader());
            } catch (ClassNotFoundException e) {
                throw new IOException(e.getMessage());
            }
        }

        public void setCachedModel(CachedModel cachedModel) {
            this.cachedModel = cachedModel;
        }

        public void writeToParcel(Parcel parcel, int i) {
            parcel.writeString(this.cachedModel.getClass().getCanonicalName());
            parcel.writeParcelable(this.cachedModel, i);
            this.cachedModel.writeToParcel(parcel, i);
        }
    }

    public ModelCache(int i, long j, int i2) {
        super("ModelCache", i, j, i2);
    }

    private void removeExpiredCache(final String str) {
        final File file = new File(this.diskCacheDirectory);
        if (file.exists()) {
            File[] listFiles = file.listFiles(new FilenameFilter() {
                public boolean accept(File file, String str) {
                    return file.equals(file) && str.startsWith(ModelCache.this.getFileNameForKey(str));
                }
            });
            if (listFiles != null && listFiles.length != 0) {
                for (File delete : listFiles) {
                    delete.delete();
                }
            }
        }
    }

    public String getFileNameForKey(String str) {
        return CacheHelper.getFileNameFromUrl(str);
    }

    public CachedModel put(String str, CachedModel cachedModel) {
        CachedModel cachedModel2;
        synchronized (this) {
            long j = this.transactionCount;
            this.transactionCount = 1 + j;
            cachedModel.setTransactionId(j);
            cachedModel2 = (CachedModel) super.put(str, cachedModel);
        }
        return cachedModel2;
    }

    protected CachedModel readValueFromDisk(File file) throws IOException {
        byte[] bArr = new byte[((int) file.length())];
        BufferedInputStream bufferedInputStream = new BufferedInputStream(new FileInputStream(file));
        bufferedInputStream.read(bArr);
        bufferedInputStream.close();
        Parcel obtain = Parcel.obtain();
        obtain.unmarshall(bArr, 0, bArr.length);
        obtain.setDataPosition(0);
        DescribedCachedModel describedCachedModel = new DescribedCachedModel();
        describedCachedModel.readFromParcel(obtain);
        return describedCachedModel.getCachedModel();
    }

    public void removeAllWithPrefix(String str) {
        synchronized (this) {
            for (String str2 : keySet()) {
                if (str2.startsWith(str)) {
                    remove(str2);
                }
            }
            if (isDiskCacheEnabled()) {
                removeExpiredCache(str);
            }
        }
    }

    protected void writeValueToDisk(File file, CachedModel cachedModel) throws IOException {
        DescribedCachedModel describedCachedModel = new DescribedCachedModel();
        describedCachedModel.setCachedModel(cachedModel);
        Parcel obtain = Parcel.obtain();
        describedCachedModel.writeToParcel(obtain, 0);
        new BufferedOutputStream(new FileOutputStream(file)).write(obtain.marshall());
    }
}
