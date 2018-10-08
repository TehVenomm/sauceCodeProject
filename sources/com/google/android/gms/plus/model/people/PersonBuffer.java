package com.google.android.gms.plus.model.people;

import com.google.android.gms.common.data.AbstractDataBuffer;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzd;
import com.google.android.gms.plus.internal.model.people.PersonEntity;
import com.google.android.gms.plus.internal.model.people.zzk;

public final class PersonBuffer extends AbstractDataBuffer<Person> {
    private final zzd<PersonEntity> zzaBj;

    public PersonBuffer(DataHolder dataHolder) {
        super(dataHolder);
        if (dataHolder.zzlm() == null || !dataHolder.zzlm().getBoolean("com.google.android.gms.plus.IsSafeParcelable", false)) {
            this.zzaBj = null;
        } else {
            this.zzaBj = new zzd(dataHolder, PersonEntity.CREATOR);
        }
    }

    public Person get(int i) {
        return this.zzaBj != null ? (Person) this.zzaBj.zzaC(i) : new zzk(this.zzPy, i);
    }
}
