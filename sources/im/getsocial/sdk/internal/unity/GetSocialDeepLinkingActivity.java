package im.getsocial.sdk.internal.unity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import net.gogame.gowrap.Bootstrap;
import net.gogame.gowrap.Constants;

public class GetSocialDeepLinkingActivity extends Activity {
    /* renamed from: a */
    private static cjrhisSQCL f2231a = upgqDBbsrL.m1274a(GetSocialDeepLinkingActivity.class);

    /* renamed from: a */
    private Class<?> m2155a() {
        try {
            return Class.forName(getPackageManager().getLaunchIntentForPackage(getPackageName()).getComponent().getClassName());
        } catch (Exception e) {
            f2231a.mo4396d("Unable to find Main Activity Class, error: " + e.getMessage());
            return null;
        }
    }

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        requestWindowFeature(1);
        onResume();
        startActivity(new Intent(this, m2155a()));
        onPause();
        finish();
        try {
            Bootstrap.init(this);
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
        }
    }
}
