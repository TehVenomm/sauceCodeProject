package net.gogame.gopay.sdk.iab;

import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import net.gogame.gopay.sdk.C1636k;

/* renamed from: net.gogame.gopay.sdk.iab.q */
final class C1402q implements OnItemSelectedListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1117a;

    C1402q(PurchaseActivity purchaseActivity) {
        this.f1117a = purchaseActivity;
    }

    public final void onItemSelected(AdapterView adapterView, View view, int i, long j) {
        if (this.f1117a.f1032Q == i && this.f1117a.f1026K && this.f1117a.f1027L) {
            return;
        }
        if (!this.f1117a.f1026K || this.f1117a.f1025J || !this.f1117a.f1027L) {
            this.f1117a.f1032Q = i;
            this.f1117a.f1024I = false;
            this.f1117a.f1025J = true;
            PurchaseActivity.m798a(this.f1117a, this.f1117a.m790a("paymentMethod"), ((C1636k) this.f1117a.f1017B.getItem(i)).f1300a);
            return;
        }
        
        /*  JADX ERROR: Method code generation error
            jadx.core.utils.exceptions.CodegenException: Error generate insn: 0x003d: INVOKE  (wrap: net.gogame.gopay.sdk.iab.PurchaseActivity
              0x0031: IGET  (r0v17 net.gogame.gopay.sdk.iab.PurchaseActivity) = (r3v0 'this' net.gogame.gopay.sdk.iab.q A[THIS]) net.gogame.gopay.sdk.iab.q.a net.gogame.gopay.sdk.iab.PurchaseActivity), (wrap: net.gogame.gopay.sdk.iab.s
              0x0035: CONSTRUCTOR  (r1v2 net.gogame.gopay.sdk.iab.s) = (r3v0 'this' net.gogame.gopay.sdk.iab.q A[THIS]), (r6v0 'i' int) net.gogame.gopay.sdk.iab.s.<init>(net.gogame.gopay.sdk.iab.q, int):void CONSTRUCTOR), (wrap: net.gogame.gopay.sdk.iab.t
              0x003a: CONSTRUCTOR  (r2v2 net.gogame.gopay.sdk.iab.t) = (r3v0 'this' net.gogame.gopay.sdk.iab.q A[THIS]) net.gogame.gopay.sdk.iab.t.<init>(net.gogame.gopay.sdk.iab.q):void CONSTRUCTOR) net.gogame.gopay.sdk.iab.PurchaseActivity.a(net.gogame.gopay.sdk.iab.PurchaseActivity, android.content.DialogInterface$OnClickListener, android.content.DialogInterface$OnClickListener):void type: STATIC in method: net.gogame.gopay.sdk.iab.q.onItemSelected(android.widget.AdapterView, android.view.View, int, long):void, dex: classes.dex
            	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:245)
            	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:213)
            	at jadx.core.codegen.RegionGen.makeSimpleBlock(RegionGen.java:109)
            	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:55)
            	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:92)
            	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:58)
            	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:92)
            	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:58)
            	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:92)
            	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:58)
            	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:92)
            	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:58)
            	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:92)
            	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:58)
            	at jadx.core.codegen.MethodGen.addRegionInsns(MethodGen.java:210)
            	at jadx.core.codegen.MethodGen.addInstructions(MethodGen.java:203)
            	at jadx.core.codegen.ClassGen.addMethod(ClassGen.java:316)
            	at jadx.core.codegen.ClassGen.addMethods(ClassGen.java:262)
            	at jadx.core.codegen.ClassGen.addClassBody(ClassGen.java:225)
            	at jadx.core.codegen.ClassGen.addClassCode(ClassGen.java:110)
            	at jadx.core.codegen.ClassGen.makeClass(ClassGen.java:76)
            	at jadx.core.codegen.CodeGen.wrapCodeGen(CodeGen.java:44)
            	at jadx.core.codegen.CodeGen.generateJavaCode(CodeGen.java:32)
            	at jadx.core.codegen.CodeGen.generate(CodeGen.java:20)
            	at jadx.core.ProcessClass.process(ProcessClass.java:36)
            	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
            	at jadx.api.JavaClass.decompile(JavaClass.java:62)
            	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
            Caused by: java.util.ConcurrentModificationException
            	at java.base/java.util.ArrayList.removeIf(ArrayList.java:1724)
            	at java.base/java.util.ArrayList.removeIf(ArrayList.java:1689)
            	at jadx.core.dex.instructions.args.SSAVar.removeUse(SSAVar.java:86)
            	at jadx.core.dex.instructions.args.SSAVar.use(SSAVar.java:79)
            	at jadx.core.dex.nodes.InsnNode.attachArg(InsnNode.java:87)
            	at jadx.core.dex.nodes.InsnNode.addArg(InsnNode.java:73)
            	at jadx.core.dex.nodes.InsnNode.copyCommonParams(InsnNode.java:335)
            	at jadx.core.dex.instructions.IndexInsnNode.copy(IndexInsnNode.java:24)
            	at jadx.core.dex.instructions.IndexInsnNode.copy(IndexInsnNode.java:9)
            	at jadx.core.dex.nodes.InsnNode.copyCommonParams(InsnNode.java:333)
            	at jadx.core.dex.instructions.InvokeNode.copy(InvokeNode.java:56)
            	at jadx.core.dex.nodes.InsnNode.copyCommonParams(InsnNode.java:333)
            	at jadx.core.dex.instructions.InvokeNode.copy(InvokeNode.java:56)
            	at jadx.core.dex.nodes.InsnNode.copyCommonParams(InsnNode.java:333)
            	at jadx.core.dex.instructions.InvokeNode.copy(InvokeNode.java:56)
            	at jadx.core.codegen.InsnGen.inlineMethod(InsnGen.java:880)
            	at jadx.core.codegen.InsnGen.makeInvoke(InsnGen.java:669)
            	at jadx.core.codegen.InsnGen.makeInsnBody(InsnGen.java:357)
            	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:239)
            	... 27 more
            */
        /*
            this = this;
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            int r0 = r0.f1032Q
            if (r0 != r6) goto L_0x0019
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            boolean r0 = r0.f1026K
            if (r0 == 0) goto L_0x0019
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            boolean r0 = r0.f1027L
            if (r0 == 0) goto L_0x0019
        L_0x0018:
            return
        L_0x0019:
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            boolean r0 = r0.f1026K
            if (r0 == 0) goto L_0x0041
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            boolean r0 = r0.f1025J
            if (r0 != 0) goto L_0x0041
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            boolean r0 = r0.f1027L
            if (r0 == 0) goto L_0x0041
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            net.gogame.gopay.sdk.iab.s r1 = new net.gogame.gopay.sdk.iab.s
            r1.<init>(r3, r6)
            net.gogame.gopay.sdk.iab.t r2 = new net.gogame.gopay.sdk.iab.t
            r2.<init>(r3)
            new android.app.AlertDialog.Builder(r0).setTitle(r0.f1047o).setMessage(r0.f1046n).setPositiveButton(r0.f1044l, r1).setNegativeButton(r0.f1045m, r2).setCancelable(false).show()
            goto L_0x0018
        L_0x0041:
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            r0.f1032Q = r6
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            r0.f1024I = false
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            r1 = 1
            r0.f1025J = r1
            net.gogame.gopay.sdk.iab.PurchaseActivity r1 = r3.f1117a
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            java.lang.String r2 = "paymentMethod"
            java.lang.String r2 = r0.m790a(r2)
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1117a
            net.gogame.gopay.sdk.iab.b r0 = r0.f1017B
            java.lang.Object r0 = r0.getItem(r6)
            net.gogame.gopay.sdk.k r0 = (net.gogame.gopay.sdk.C1636k) r0
            java.util.List r0 = r0.f1300a
            net.gogame.gopay.sdk.iab.PurchaseActivity.m798a(r1, r2, r0)
            goto L_0x0018
        */
        throw new UnsupportedOperationException("Method not decompiled: net.gogame.gopay.sdk.iab.C1402q.onItemSelected(android.widget.AdapterView, android.view.View, int, long):void");
    }

    public final void onNothingSelected(AdapterView adapterView) {
    }
}
