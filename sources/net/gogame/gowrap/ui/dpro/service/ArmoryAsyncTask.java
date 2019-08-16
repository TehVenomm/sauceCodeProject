package net.gogame.gowrap.p019ui.dpro.service;

import android.os.AsyncTask;
import android.util.Log;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.p019ui.dpro.model.armory.ArmoryResponse;

/* renamed from: net.gogame.gowrap.ui.dpro.service.ArmoryAsyncTask */
public class ArmoryAsyncTask extends AsyncTask<String, Void, ArmoryResponse> {
    private Exception exceptionToBeThrown;
    private final ArmoryService service;

    public ArmoryAsyncTask() {
        this(DefaultArmoryService.INSTANCE);
    }

    public ArmoryAsyncTask(ArmoryService armoryService) {
        this.service = armoryService;
    }

    /* access modifiers changed from: protected */
    public ArmoryResponse doInBackground(String... strArr) {
        ArmoryResponse armoryResponse = null;
        if (strArr == null || strArr.length == 0) {
            return armoryResponse;
        }
        String str = strArr[0];
        if (str == null) {
            return armoryResponse;
        }
        try {
            return this.service.getArmory(str);
        } catch (Exception e) {
            Log.e(Constants.TAG, "Exception", e);
            this.exceptionToBeThrown = e;
            return armoryResponse;
        }
    }

    public Exception getExceptionToBeThrown() {
        return this.exceptionToBeThrown;
    }
}
