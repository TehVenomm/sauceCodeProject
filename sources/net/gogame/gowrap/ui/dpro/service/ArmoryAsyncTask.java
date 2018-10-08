package net.gogame.gowrap.ui.dpro.service;

import android.os.AsyncTask;
import android.util.Log;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.ui.dpro.model.armory.ArmoryResponse;

public class ArmoryAsyncTask extends AsyncTask<String, Void, ArmoryResponse> {
    private Exception exceptionToBeThrown;
    private final ArmoryService service;

    public ArmoryAsyncTask() {
        this(DefaultArmoryService.INSTANCE);
    }

    public ArmoryAsyncTask(ArmoryService armoryService) {
        this.service = armoryService;
    }

    protected ArmoryResponse doInBackground(String... strArr) {
        ArmoryResponse armoryResponse = null;
        if (!(strArr == null || strArr.length == 0)) {
            String str = strArr[0];
            if (str != null) {
                try {
                    armoryResponse = this.service.getArmory(str);
                } catch (Throwable e) {
                    Log.e(Constants.TAG, "Exception", e);
                    this.exceptionToBeThrown = e;
                }
            }
        }
        return armoryResponse;
    }

    public Exception getExceptionToBeThrown() {
        return this.exceptionToBeThrown;
    }
}
