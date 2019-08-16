package net.gogame.gowrap.p019ui.dpro.service;

import android.os.AsyncTask;
import android.util.Log;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.p019ui.dpro.model.equipmentcollection.EquipmentCollectionResponse;

/* renamed from: net.gogame.gowrap.ui.dpro.service.EquipmentCollectionAsyncTask */
public class EquipmentCollectionAsyncTask extends AsyncTask<String, Void, EquipmentCollectionResponse> {
    private Exception exceptionToBeThrown;
    private final EquipmentCollectionService service;

    public EquipmentCollectionAsyncTask() {
        this(DefaultEquipmentCollectionService.INSTANCE);
    }

    public EquipmentCollectionAsyncTask(EquipmentCollectionService equipmentCollectionService) {
        this.service = equipmentCollectionService;
    }

    /* access modifiers changed from: protected */
    public EquipmentCollectionResponse doInBackground(String... strArr) {
        EquipmentCollectionResponse equipmentCollectionResponse = null;
        if (strArr == null || strArr.length == 0) {
            return equipmentCollectionResponse;
        }
        String str = strArr[0];
        if (str == null) {
            return equipmentCollectionResponse;
        }
        try {
            return this.service.getEquipmentCollection(str);
        } catch (Exception e) {
            Log.e(Constants.TAG, "Exception", e);
            this.exceptionToBeThrown = e;
            return equipmentCollectionResponse;
        }
    }

    public Exception getExceptionToBeThrown() {
        return this.exceptionToBeThrown;
    }
}
