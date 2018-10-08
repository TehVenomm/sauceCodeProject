package org.onepf.openiab;

import java.util.Arrays;

class UnityPlugin$3 implements Runnable {
    final /* synthetic */ UnityPlugin this$0;
    final /* synthetic */ String[] val$itemSkus;

    UnityPlugin$3(UnityPlugin unityPlugin, String[] strArr) {
        this.this$0 = unityPlugin;
        this.val$itemSkus = strArr;
    }

    public void run() {
        UnityPlugin.access$000(this.this$0).queryInventoryAsync(true, Arrays.asList(this.val$itemSkus), this.this$0._queryInventoryListener);
    }
}
