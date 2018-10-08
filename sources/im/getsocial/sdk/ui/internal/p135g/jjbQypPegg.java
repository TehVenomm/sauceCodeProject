package im.getsocial.sdk.ui.internal.p135g;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentManager;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;

/* renamed from: im.getsocial.sdk.ui.internal.g.jjbQypPegg */
public final class jjbQypPegg {
    /* renamed from: a */
    private static final cjrhisSQCL f2950a = upgqDBbsrL.m1274a(jjbQypPegg.class);
    /* renamed from: b */
    private static final String f2951b = upgqDBbsrL.class.getName();

    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static void m3290a(Activity activity) {
        FragmentManager fragmentManager = activity.getFragmentManager();
        Fragment findFragmentByTag = fragmentManager.findFragmentByTag(f2951b);
        if (findFragmentByTag != null) {
            fragmentManager.beginTransaction().remove(findFragmentByTag).commitAllowingStateLoss();
        }
    }

    /* renamed from: a */
    public static void m3291a(Activity activity, im.getsocial.sdk.ui.internal.p114i.cjrhisSQCL cjrhissqcl) {
        activity.getFragmentManager().beginTransaction().add(upgqDBbsrL.m3293a(cjrhissqcl), f2951b).commitAllowingStateLoss();
    }

    /* renamed from: b */
    public static im.getsocial.sdk.ui.internal.p114i.cjrhisSQCL m3292b(Activity activity) {
        upgqDBbsrL upgqdbbsrl;
        try {
            upgqdbbsrl = (upgqDBbsrL) activity.getFragmentManager().findFragmentByTag(f2951b);
        } catch (RuntimeException e) {
            f2950a.mo4387a("Exception while getting fragment state, just ignore it.");
            upgqdbbsrl = null;
        }
        return upgqdbbsrl == null ? null : upgqdbbsrl.m3294a();
    }
}
