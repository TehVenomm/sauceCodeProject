package net.gogame.gowrap.ui.dpro.service;

import android.os.AsyncTask;
import android.util.Log;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.ui.dpro.model.equipmentcollection.EquipmentCollectionResponse;

public class EquipmentCollectionAsyncTask extends AsyncTask<String, Void, EquipmentCollectionResponse> {
    private Exception exceptionToBeThrown;
    private final EquipmentCollectionService service;

    public EquipmentCollectionAsyncTask() {
        this(DefaultEquipmentCollectionService.INSTANCE);
    }

    public EquipmentCollectionAsyncTask(EquipmentCollectionService equipmentCollectionService) {
        this.service = equipmentCollectionService;
    }

    protected EquipmentCollectionResponse doInBackground(String... strArr) {
        EquipmentCollectionResponse equipmentCollectionResponse = null;
        if (!(strArr == null || strArr.length == 0)) {
            String str = strArr[0];
            if (str != null) {
                try {
                    equipmentCollectionResponse = this.service.getEquipmentCollection(str);
                } catch (Throwable e) {
                    Log.e(Constants.TAG, "Exception", e);
                    this.exceptionToBeThrown = e;
                }
            }
        }
        return equipmentCollectionResponse;
    }

    public Exception getExceptionToBeThrown() {
        return this.exceptionToBeThrown;
    }
}
