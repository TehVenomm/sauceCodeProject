package jp.colopl.api.docomo;

import android.content.Context;
import com.github.droidfu.concurrent.BetterAsyncTask;
import jp.colopl.util.Util;

public class DoCoMoAsyncTask extends BetterAsyncTask<Void, Void, DoCoMoLocationInfo> {
    private DoCoMoAsyncTaskDelegate delegate;

    public DoCoMoAsyncTask(Context context) {
        super(context);
    }

    protected void after(Context context, DoCoMoLocationInfo doCoMoLocationInfo) {
        if (this.delegate != null) {
            if (doCoMoLocationInfo == null) {
                Util.eLog("DoCoMoAsyncTask", "doCoMoLocationInfo is null");
                return;
            }
            ResultInfo resultInfo = doCoMoLocationInfo.getResultInfo();
            if (resultInfo == null) {
                Util.dLog("DoCoMoAsyncTask", "resultInfo is null");
            } else if (resultInfo.isResultCodeOK()) {
                Util.dLog("DoCoMoAsyncTask", "resultCode is not 2000");
                this.delegate.receiveSuccessDoCoMoLocationInfo(doCoMoLocationInfo);
            } else {
                this.delegate.receiveErrorDoCoMoLocationInfo(doCoMoLocationInfo);
            }
        }
    }

    protected DoCoMoLocationInfo doCheckedInBackground(Context context, Void... voidArr) throws Exception {
        return new DoCoMoAPI().getLocationInfo();
    }

    public DoCoMoAsyncTaskDelegate getDelegate() {
        return this.delegate;
    }

    protected void handleError(Context context, Exception exception) {
    }

    public void setDelegate(DoCoMoAsyncTaskDelegate doCoMoAsyncTaskDelegate) {
        this.delegate = doCoMoAsyncTaskDelegate;
    }
}
