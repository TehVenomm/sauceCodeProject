package com.google.android.gms.common.api.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api.Client;
import com.google.android.gms.common.internal.BaseGmsClient.ConnectionProgressReportCallbacks;
import com.google.android.gms.common.internal.GoogleApiAvailabilityCache;
import java.util.ArrayList;
import java.util.Map;

final class zaan extends zaau {
    final /* synthetic */ zaak zagi;
    private final Map<Client, zaam> zagk;

    public zaan(zaak zaak, Map<Client, zaam> map) {
        this.zagi = zaak;
        super(zaak, null);
        this.zagk = map;
    }

    @WorkerThread
    public final void zaan() {
        int i = 0;
        GoogleApiAvailabilityCache googleApiAvailabilityCache = new GoogleApiAvailabilityCache(this.zagi.zaex);
        ArrayList arrayList = new ArrayList();
        ArrayList arrayList2 = new ArrayList();
        for (Client client : this.zagk.keySet()) {
            if (!client.requiresGooglePlayServices() || ((zaam) this.zagk.get(client)).zaeb) {
                arrayList2.add(client);
            } else {
                arrayList.add(client);
            }
        }
        int i2 = -1;
        if (!arrayList.isEmpty()) {
            ArrayList arrayList3 = arrayList;
            int size = arrayList3.size();
            int i3 = 0;
            while (i3 < size) {
                Object obj = arrayList3.get(i3);
                i3++;
                i2 = googleApiAvailabilityCache.getClientAvailability(this.zagi.mContext, (Client) obj);
                if (i2 != 0) {
                    break;
                }
            }
        } else {
            ArrayList arrayList4 = arrayList2;
            int size2 = arrayList4.size();
            while (i < size2) {
                Object obj2 = arrayList4.get(i);
                i++;
                i2 = googleApiAvailabilityCache.getClientAvailability(this.zagi.mContext, (Client) obj2);
                if (i2 == 0) {
                    break;
                }
            }
        }
        if (i2 != 0) {
            this.zagi.zafs.zaa((zabf) new zaao(this, this.zagi, new ConnectionResult(i2, null)));
            return;
        }
        if (this.zagi.zagc) {
            this.zagi.zaga.connect();
        }
        for (Client client2 : this.zagk.keySet()) {
            ConnectionProgressReportCallbacks connectionProgressReportCallbacks = (ConnectionProgressReportCallbacks) this.zagk.get(client2);
            if (!client2.requiresGooglePlayServices() || googleApiAvailabilityCache.getClientAvailability(this.zagi.mContext, client2) == 0) {
                client2.connect(connectionProgressReportCallbacks);
            } else {
                this.zagi.zafs.zaa((zabf) new zaap(this, this.zagi, connectionProgressReportCallbacks));
            }
        }
    }
}
