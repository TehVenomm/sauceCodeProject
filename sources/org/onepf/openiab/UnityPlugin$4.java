package org.onepf.openiab;

import java.util.Arrays;

class UnityPlugin$4 implements Runnable {
    final /* synthetic */ UnityPlugin this$0;
    final /* synthetic */ String[] val$itemSkus;
    final /* synthetic */ String[] val$subsSkus;

    UnityPlugin$4(UnityPlugin unityPlugin, String[] strArr, String[] strArr2) {
        this.this$0 = unityPlugin;
        this.val$itemSkus = strArr;
        this.val$subsSkus = strArr2;
    }

    public void run() {
        UnityPlugin.access$000(this.this$0).queryInventoryAsync(true, Arrays.asList(this.val$itemSkus), Arrays.asList(this.val$subsSkus), this.this$0._queryInventoryListener);
    }
}
