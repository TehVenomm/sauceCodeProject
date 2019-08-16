package p018jp.colopl.api.docomo;

import android.content.Context;
import com.github.droidfu.concurrent.BetterAsyncTask;
import p018jp.colopl.util.Util;

/* renamed from: jp.colopl.api.docomo.DoCoMoAsyncTask */
public class DoCoMoAsyncTask extends BetterAsyncTask<Void, Void, DoCoMoLocationInfo> {
    private DoCoMoAsyncTaskDelegate delegate;

    public DoCoMoAsyncTask(Context context) {
        super(context);
    }

    /* access modifiers changed from: protected */
    public void after(Context context, DoCoMoLocationInfo doCoMoLocationInfo) {
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

    /* access modifiers changed from: protected */
    public DoCoMoLocationInfo doCheckedInBackground(Context context, Void... voidArr) throws Exception {
        return new DoCoMoAPI().getLocationInfo();
    }

    public DoCoMoAsyncTaskDelegate getDelegate() {
        return this.delegate;
    }

    /* access modifiers changed from: protected */
    public void handleError(Context context, Exception exc) {
    }

    public void setDelegate(DoCoMoAsyncTaskDelegate doCoMoAsyncTaskDelegate) {
        this.delegate = doCoMoAsyncTaskDelegate;
    }
}
