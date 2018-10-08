package im.getsocial.sdk.ui.internal.views.recycling;

import android.view.View;

public final class Recycler implements ViewVisitor {
    /* renamed from: a */
    public final void mo4747a(View view) {
        if (view instanceof Recyclable) {
            ((Recyclable) view).mo4740d();
        }
    }
}
