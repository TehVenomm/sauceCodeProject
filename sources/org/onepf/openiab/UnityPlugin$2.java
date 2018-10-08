package org.onepf.openiab;

class UnityPlugin$2 implements Runnable {
    final /* synthetic */ UnityPlugin this$0;

    UnityPlugin$2(UnityPlugin unityPlugin) {
        this.this$0 = unityPlugin;
    }

    public void run() {
        UnityPlugin.access$000(this.this$0).queryInventoryAsync(this.this$0._queryInventoryListener);
    }
}
