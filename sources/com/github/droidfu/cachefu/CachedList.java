package com.github.droidfu.cachefu;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Iterator;

public class CachedList<CO extends CachedModel> extends CachedModel {
    public static final Creator<CachedList<CachedModel>> CREATOR = new C05951();
    protected Class<? extends CachedModel> clazz;
    protected ArrayList<CO> list;

    /* renamed from: com.github.droidfu.cachefu.CachedList$1 */
    static final class C05951 implements Creator<CachedList<CachedModel>> {
        C05951() {
        }

        public CachedList<CachedModel> createFromParcel(Parcel parcel) {
            try {
                return new CachedList(parcel);
            } catch (IOException e) {
                e.printStackTrace();
                return null;
            }
        }

        public CachedList<CachedModel>[] newArray(int i) {
            return new CachedList[i];
        }
    }

    public CachedList() {
        this.list = new ArrayList();
    }

    public CachedList(Parcel parcel) throws IOException {
        super(parcel);
    }

    public CachedList(Class<? extends CachedModel> cls) {
        initList(cls);
        this.list = new ArrayList();
    }

    public CachedList(Class<? extends CachedModel> cls, int i) {
        initList(cls);
        this.list = new ArrayList(i);
    }

    public CachedList(Class<? extends CachedModel> cls, String str) {
        super(str);
        initList(cls);
        this.list = new ArrayList();
    }

    private void initList(Class<? extends CachedModel> cls) {
        this.clazz = cls;
    }

    public String createKey(String str) {
        return "list_" + str;
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof CachedList)) {
            return false;
        }
        CachedList cachedList = (CachedList) obj;
        return this.clazz.equals(cachedList.clazz) && this.list.equals(cachedList.list);
    }

    public ArrayList<CO> getList() {
        return this.list;
    }

    public void readFromParcel(Parcel parcel) throws IOException {
        super.readFromParcel(parcel);
        try {
            this.clazz = Class.forName(parcel.readString());
            this.list = parcel.createTypedArrayList((Creator) this.clazz.getField("CREATOR").get(this));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public boolean reloadAll(ModelCache modelCache) {
        boolean reload = reload(modelCache);
        Iterator it = this.list.iterator();
        boolean z = reload;
        while (it.hasNext()) {
            if (((CachedModel) it.next()).reload(modelCache)) {
                z = true;
            }
        }
        return z;
    }

    public boolean reloadFromCachedModel(ModelCache modelCache, CachedModel cachedModel) {
        CachedList cachedList = (CachedList) cachedModel;
        this.clazz = cachedList.clazz;
        this.list = cachedList.list;
        return false;
    }

    public void writeToParcel(Parcel parcel, int i) {
        super.writeToParcel(parcel, i);
        parcel.writeString(this.clazz.getCanonicalName());
        parcel.writeTypedList(this.list);
    }
}
