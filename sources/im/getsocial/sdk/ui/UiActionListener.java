package im.getsocial.sdk.ui;

import im.getsocial.sdk.ui.UiAction.Pending;

public interface UiActionListener {
    public static final UiActionListener PROCEED_ALL = new C10681();

    /* renamed from: im.getsocial.sdk.ui.UiActionListener$1 */
    static final class C10681 implements UiActionListener {
        C10681() {
        }

        public final void onUiAction(UiAction uiAction, Pending pending) {
            pending.proceed();
        }
    }

    void onUiAction(UiAction uiAction, Pending pending);
}
